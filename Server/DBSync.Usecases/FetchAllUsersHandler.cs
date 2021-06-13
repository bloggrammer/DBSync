using DBSync.Models;
using DBSync.Usecases.Base;
using System.Collections.Generic;

namespace DBSync.Usecases
{
    public class FetchAllUsersHandler:Handler
    {
        public FetchAllUsersHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public IEnumerable<UserDto> Handle()
        {
            var users = _unitOfWork.UserRepository.FetchAll();

            var userDtos = new List<UserDto>();
            foreach (var user in users)
                userDtos.Add(new UserDto(user.Name, user.Age) { Id = user.Id });

            return userDtos;
        }
    }
}
