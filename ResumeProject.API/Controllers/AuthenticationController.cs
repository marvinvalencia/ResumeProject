// <copyright file="AuthenticationController.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.API.Controllers
{
    using System.Security.Claims;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ResumeProject.Application.Commands;
    using ResumeProject.Application.DTOs;

    /// <summary>
    /// The AuthenticationController class handles user authentication and registration operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public AuthenticationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// The Login method authenticates a user and generates a JWT token.
        /// </summary>
        /// <param name="loginDto">The login dto.</param>
        /// <returns>The result.</returns>
        [HttpPost("Login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            var result = await this.mediator.Send(new LoginCommand(loginDto.Email, loginDto.Password));
            return this.Ok(result);
        }

        /// <summary>
        /// The Register method registers a new user and assigns them a default role.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>The result.</returns>
        [HttpPost("Register")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            try
            {
                var message = await this.mediator.Send(command);
                return this.Ok(message);
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return this.Problem(ex.Message, statusCode: 400);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// The UserClaims method retrieves the claims of the authenticated user.
        /// For testing purposes, it returns the claims of the currently authenticated user.
        /// </summary>
        /// <returns>The result.</returns>
        [HttpGet("UserClaims")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<Claim>), StatusCodes.Status200OK)]
        public IActionResult UserClaims()
        {
            var allClaims = this.User.Claims
                .Select(c => new { c.Type, c.Value })
                .ToList();

            return this.Ok(allClaims);
        }
    }
}
