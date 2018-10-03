using FlowerTestApp.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerTestApp.Data
{
    public class FlowerDbContext : DbContext
    {
        public FlowerDbContext(DbContextOptions<FlowerDbContext> options) : base(options)
        {

        }
        public DbSet<Flower> Flowers { get; set; }
    }
}
