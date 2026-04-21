using GymeManagementDAL.Entities;


namespace GymeManagementDAL.Repositories.InterFaces
{
    public interface IUnitOfWork
    {
        public ISessionRepository SessionRepository { get; }
        IGenericRepository<TEntity>GetRepository<TEntity>() where TEntity:BaseEntity,new() ;

        int SaveChanges();
    }
}
