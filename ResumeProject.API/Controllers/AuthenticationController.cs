// <copyright file="AuthenticationController.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.API.Controllers
{
    using System.Security.Claims;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ResumeProject.Application.Authentication.Commands;
    using ResumeProject.Application.Authentication.DTOs;

    /// <summary>
    /// The AuthenticationController class handles user authentication and registration operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthenticationController : BaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<AuthenticationController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="logger">The logger.</param>
        public AuthenticationController(IMediator mediator, ILogger<AuthenticationController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
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
            try
            {
                var result = await this.mediator.Send(new LoginCommand(loginDto.Email, loginDto.Password));
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                return this.StatusCode(503, "⚠️ Login unavailable. The database used in Azure is out of credits. Please try again later.");
            }
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
                this.logger.LogError(ex, ex.Message);
                return this.StatusCode(503, "⚠️ Register unavailable. The database used in Azure is out of credits. Please try again later.");
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
