using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]  
    public class TodoController : ControllerBase
    {

        private readonly ITodoService _service;

        public TodoController(ITodoService service)
        {
            _service = service;
        }

        [HttpPost("Add")]        
        public async Task<IActionResult> Add([FromBody] TodoModel model)
        {
            var currentUser = User.Claims.SingleOrDefault(x => x.Type == "userId")?.Value;
            model.UserId = Guid.Parse(currentUser);
            var result = await _service.Add(model);
            return Ok(result);
        }

        [HttpGet("List")]        
        public async Task<IActionResult> List()
        {
            var currentUser = User.Claims.SingleOrDefault(x => x.Type == "userId")?.Value;
            Guid userId = Guid.Parse(currentUser);
            var result = await _service.ListAll(userId);
            return Ok(result);
        }

        [HttpPut("SetStatus")]        
        public async Task<IActionResult> SetStatus(int id, bool completed)
        {
            await _service.SetStatus(id, completed);
            return Ok();
        }

        [HttpDelete("Delete")]        
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}
