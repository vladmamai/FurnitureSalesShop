using FurnitureShopApp.DAL.Interfaces;
using FurnitureShopApp.DAL.Models;
using System;

namespace FurnitureShopApp.DAL.Repositories
{
    public class FurnitureShopUnitOfWork : IFurnitureShopUnitOfWork
    {
        private FurnitureSaleContext db;

        public FurnitureShopUnitOfWork(FurnitureSaleContext context)
        {
            db = context;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
