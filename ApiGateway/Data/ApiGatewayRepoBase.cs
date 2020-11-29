using Infrastructure.Data;

namespace ApiGateway.Data
{
    public class ApiGatewayRepoBase<TEntity> : EFEntityRepositoryBase<TEntity, ApiGatewayDbContext>
        where TEntity : class, IEntity, new()
    { }
}