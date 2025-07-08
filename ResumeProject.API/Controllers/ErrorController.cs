// <copyright file="ErrorController.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.API.Controllers
{
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The ErrorController class handles errors that occur during request processing.
    /// </summary>
    [ApiController]
    public class ErrorController : BaseApiController
    {
        /// <summary>
        /// The HandleError method is invoked when an unhandled exception occurs in the application.
        /// </summary>
        /// <returns>the result.</returns>
        [Route("/error")]
        [HttpGet]
        public IActionResult HandleError()
        {
            var exception = this.HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            return this.Problem(
                title: "Internal Server Error",
                detail: "⚠️ Something went wrong while processing your request. The API might be down or out of credits.",
                statusCode: 503);
        }
    }
}
