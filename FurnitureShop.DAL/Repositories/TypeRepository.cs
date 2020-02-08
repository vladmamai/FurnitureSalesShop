using FurnitureShopApp.DAL.Interfaces;
using FurnitureShopApp.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace FurnitureShopApp.DAL.Repositories
{
    public class TypesRepository : Repository<Type, FurnitureSaleContext>, ITypesRepository
    {
        public TypesRepository(FurnitureSaleContext context)
           : base(context)
        {
        }

        public IEnumerable<Type> GetTypeByName(string typeName)
        {
            return Context.Type
              .Where(c => c.TypeName == typeName)
              .ToList();
        }
    }
}
