using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentralizedAuth.Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PermissionsController : ControllerBase
    {
        [HttpGet]
        public IActionResult SendPermissions()
        {
            Response.Headers.Add("x-permissions-token", "abc-123");
            return Ok();
        }
    }
}