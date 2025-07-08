// <copyright file="BaseApiController.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The BaseApiController class serves as a base controller for all API controllers in the application.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public class BaseApiController : ControllerBase
    {
    }
}
