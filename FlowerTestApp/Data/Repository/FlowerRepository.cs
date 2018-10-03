using FlowerTestApp.Data.Interfaces;
using FlowerTestApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerTestApp.Data.Repository
{
    public class FlowerRepository : Repository<Flower>, IFlowerRepository
    {
        public FlowerRepository(FlowerDbContext context) : base(context)
        {

        }
    }
}
