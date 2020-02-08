using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureShopApp.DAL.Interfaces
{
    public interface IFurnitureShopUnitOfWork : IDisposable
    {
        void Save();
    }
}