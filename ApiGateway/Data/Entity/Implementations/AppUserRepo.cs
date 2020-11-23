using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ApiGateway.Data.Entity.AppUser
{
    public class AppUserRepo : IAppUserRepo
    {
        private readonly AppUserDbContext _context;

        public AppUserRepo(AppUserDbContext context)
        {
            _context = context;
        }

        private IQueryable<AppUser> AppUsers => _context.AppUsers;

        public List<AppUser> GetListByFilter(Expression<Func<AppUser, bool>> filter)
            => filter == null ? AppUsers.ToList() : AppUsers.Where(filter).ToList();

        public AppUser GetByFilter(Expression<Func<AppUser, bool>> filter)
            => filter == null ? null : AppUsers.FirstOrDefault(filter);

        public void Update(AppUser user)
        {
            user.LastModifiedDate = DateTime.Now;

            var updatedEntity = _context.Entry(user);
            updatedEntity.State = EntityState.Modified;

            _context.SaveChanges();
        }

        public void Add(AppUser user)
        {
            var addedEntity = _context.Entry(user);
            addedEntity.State = EntityState.Added;

            _context.SaveChanges();
        }
    }
}