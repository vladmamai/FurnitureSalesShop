using FurnitureShopApp.DAL.Interfaces;
using FurnitureShopApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureShopApp.DAL.Repositories
{

    public class SubtypeRepository : Repository<Subtype, FurnitureSaleContext>, ISubtypeRepository
    {
        public SubtypeRepository(FurnitureSaleContext context)
           : base(context)
        {
        }

        public IEnumerable<Subtype> GetSubtypeByName(string subtypeName)
        {
            return Context.Subtype
              .Where(c => c.SubtypeName == subtypeName).Include(s => s.Type)
              .ToList();
        }

        public IEnumerable<Subtype> GetSubtypeWithType()
        {
            return Context.Subtype.Include(s => s.Type).ToList();
        }
    }
}
