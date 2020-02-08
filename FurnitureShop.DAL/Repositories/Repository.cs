using FurnitureShopApp.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FurnitureShopApp.DAL.Repositories
{
    public class Repository<TEntity, TContext>
      : IRepository<TEntity> where TEntity : class
                             where TContext : DbContext
    {
        private readonly DbSet<TEntity> dbSet;
        protected TContext Context;

        public Repository(TContext context)
        {
            Context = context;
            dbSet = Context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.AsNoTracking().ToList();
        }

        public void Create(TEntity item)
        {
            if (item == null)
            {
                Console.WriteLine("Create method received a null argument!");
                return;
            }

            dbSet.Add(item);
            Context.SaveChanges();
        }

        public void Update(TEntity item)
        {
            if (item == null)
            {
                Console.WriteLine("Update method received a null argument!");
                return;
            }

            Context.Entry(item).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public TEntity Get(int? id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return dbSet.AsNoTracking().AsEnumerable().Where(predicate).ToList();
        }

        public void Delete(int? id)
        {
            TEntity item = dbSet.Find(id);
            if (item != null)
                dbSet.Remove(item);
            Context.SaveChanges();
        }

        public TEntity FindItem(Func<TEntity, bool> predicate)
        {
            return dbSet.AsNoTracking().AsEnumerable().Where(predicate).ToList().FirstOrDefault();
        }
    }
}
