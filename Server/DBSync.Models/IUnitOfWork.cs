namespace DBSync.Models
{
    public interface IUnitOfWork
    {
        bool CommitToDatabase();
        IUserRepository UserRepository { get; }
    }
}
