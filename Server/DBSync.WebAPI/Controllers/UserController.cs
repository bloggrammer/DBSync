using DBSync.Models;
using DBSync.Usecases;
using DBSync.Usecases.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBSync.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;


        [HttpPost("create")]
        public async Task<ActionResult<UserDto>> CreateUserAsync([FromBody] UserArg arg)
        {
            return await Task.Run(() =>
            {
                var handler = new CreateUserHandler(_unitOfWork);
                var data = handler.Handle(arg);
                return new ActionResult<UserDto>(data);
            });
        }

        [HttpPost("update")]
        public async Task<ActionResult<UserDto>> UpdateUserAsync([FromBody] UserArg arg)
        {
            return await Task.Run(() =>
            {
                var handler = new UpdateUserHandler(_unitOfWork);
                var data = handler.Handle(arg);
                return new ActionResult<UserDto>(data);
            });
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserDto>>> FetchUserAsync()
        {
            return await Task.Run(() =>
            {
                var handler = new FetchAllUsersHandler(_unitOfWork);
                var data = handler.Handle();
                return new ActionResult<IEnumerable<UserDto>>(data);
            });
        }

        [HttpPost("delete")]
        public async Task UpdateUserAsync([FromBody] Guid id)
        {
            await Task.Run(() =>
            {
                new DeleteUserHandler(_unitOfWork).Handle(id);
            });
        }

        private readonly IUnitOfWork _unitOfWork;
    }
}
