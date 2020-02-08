using FurnitureShopApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureShopApp.DAL.Interfaces
{
    public interface ISubtypeRepository : IRepository<Subtype>
    {
        IEnumerable<Subtype> GetSubtypeByName(string subtypeName);
        IEnumerable<Subtype> GetSubtypeWithType();
    }
}
