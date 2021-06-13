using DBSync.Models;
using DBSync.Usecases.Base;
using System;

namespace DBSync.Usecases
{
    public class DeleteUserHandler:Handler
    {
        public DeleteUserHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public bool Handle(Guid id)
        {
            _unitOfWork.UserRepository.Remove(id);
            return _unitOfWork.CommitToDatabase();
        }
    }
}
