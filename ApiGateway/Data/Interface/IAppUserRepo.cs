using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApiGateway.Data.AppUser
{
    public interface IAppUserRepo
    {
        List<AppUser> GetListByFilter(Expression<Func<AppUser, bool>> filter);
        AppUser GetByFilter(Expression<Func<AppUser, bool>> filter);
        void Add(AppUser user);
        void Update(AppUser user);
    }
}