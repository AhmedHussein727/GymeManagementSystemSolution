using GymeManagementDAL.Data.Contexts;
using GymeManagementDAL.Entities;
using GymeManagementDAL.Repositories.InterFaces;


namespace GymeManagementDAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(GymeDbContext dbContext,ISessionRepository sessionRepository)
        {
            DbContext = dbContext;
            this.SessionRepository = sessionRepository;
        }
        public ISessionRepository SessionRepository { get; }

        private readonly Dictionary<Type, object>repositories = new Dictionary<Type, object>();

        public GymeDbContext DbContext { get; }


        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var EntityType=typeof(TEntity);
            if(repositories.TryGetValue(EntityType, out var repo))
                return (IGenericRepository<TEntity>)repo;
            var newRepo=new GenericRepository<TEntity>(DbContext);
            repositories.Add(EntityType, newRepo);
            return newRepo;
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }
    }
}
