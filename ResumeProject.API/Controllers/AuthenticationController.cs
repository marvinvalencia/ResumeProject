// <copyright file="AuthenticationController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ResumeProject.API.Controllers
{
    using System.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ResumeProject.Application.DTOs;
    using ResumeProject.Application.Services;
    using ResumeProject.Domain.Entities;
    using ResumeProject.Domain.Enum;

    /// <summary>
    /// The AuthenticationController class handles user authentication and registration operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly TokenService tokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="tokenService">The token service.</param>
        public AuthenticationController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, TokenService tokenService)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.roleManager = roleManager;
        }

        /// <summary>
        /// The Login method authenticates a user and generates a JWT token.
        /// </summary>
        /// <param name="loginDto">The login dto.</param>
        /// <returns>The result.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await this.userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await this.userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return this.Unauthorized();
            }

            var userRoles = await this.userManager.GetRolesAsync(user);
            var token = this.tokenService.GenerateToken(user);

            return this.Ok(new { token });
        }

        /// <summary>
        /// The Register method registers a new user and assigns them a default role.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>The result.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
            {
                return this.BadRequest("Passwords do not match.");
            }

            var userExists = await this.userManager.FindByEmailAsync(dto.Email);
            if (userExists != null)
            {
                return this.BadRequest("User already exists.");
            }

            var user = new User
            {
                Email = dto.Email,
                UserName = dto.Email,
            };

            var result = await this.userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return this.BadRequest(result.Errors);
            }

            var role = Role.User.ToString();
            if (!await this.roleManager.RoleExistsAsync(role))
            {
                await this.roleManager.CreateAsync(new IdentityRole(role));
            }

            await this.userManager.AddToRoleAsync(user, role);

            return this.Ok("User registered successfully.");
        }

        /// <summary>
        /// The UserClaims method retrieves the claims of the authenticated user.
        /// For testing purposes, it returns the claims of the currently authenticated user.
        /// </summary>
        /// <returns>The result.</returns>
        [HttpGet("UserClaims")]
        [Authorize]
        public IActionResult UserClaims()
        {
            var allClaims = this.User.Claims
                .Select(c => new { c.Type, c.Value })
                .ToList();

            return this.Ok(allClaims);
        }
    }
}
