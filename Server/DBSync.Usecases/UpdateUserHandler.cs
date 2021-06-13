using DBSync.Models;
using DBSync.Usecases.Base;
using System;

namespace DBSync.Usecases
{
    public class UpdateUserHandler:Handler
    {
        public UpdateUserHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public UserDto Handle(UserArg arg)
        {
            var user = _unitOfWork.UserRepository.FindById(arg.Id);        

            if (user != null)
            {
                user.Age = arg.Age;
                user.Name = arg.Name;
                _unitOfWork.UserRepository.Update(user);
                _unitOfWork.CommitToDatabase();
            }
            return new UserDto(user.Name, user.Age) { Id = user.Id };
        }
    }
}
