using FurnitureShopApp.DAL.Models;
using System.Collections.Generic;
using System.Text;

namespace FurnitureShopApp.DAL.Interfaces
{
    public interface ITypesRepository : IRepository<Type>
    {
        IEnumerable<Type> GetTypeByName(string typeName);
    }
}
