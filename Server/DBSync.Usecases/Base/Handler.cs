using DBSync.Models;

namespace DBSync.Usecases.Base
{
    public abstract class Handler
    {
        public Handler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        protected IUnitOfWork _unitOfWork;
    }
}
