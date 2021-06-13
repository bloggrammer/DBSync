using DBSync.Models;
using DBSync.Usecases.Base;

namespace DBSync.Usecases
{
    public class CreateUserHandler:Handler
    {
        public CreateUserHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public UserDto Handle(UserArg arg)
        {
            var user = new User(arg.Name) { Age = arg.Age };

            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.CommitToDatabase(); 
            
            return new UserDto(user.Name, user.Age) {Id=user.Id};
        }
    }
}
