using FlowerTestApp.Data.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerTestApp.Data
{
    public static class DbInitialize
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<FlowerDbContext>();
                Flower flower1 = new Flower();
                flower1.PedalLength = 3;
                flower1.PedalWidth = 1.5;
                flower1.Type = 1;
                context.Flowers.Add(flower1);

                Flower flower2 = new Flower();
                flower2.PedalLength = 2;
                flower2.PedalWidth = 1;
                flower2.Type = 0;
                context.Flowers.Add(flower2);

                Flower flower3  = new Flower();
                flower3.PedalLength = 4;
                flower3.PedalWidth = 1.5;
                flower3.Type = 1;
                context.Flowers.Add(flower3);

                Flower flower4 = new Flower();
                flower4.PedalLength = 3;
                flower4.PedalWidth = 1;
                flower4.Type = 0;
                context.Flowers.Add(flower4);

                Flower flower5 = new Flower();
                flower5.PedalLength = 3;
                flower5.PedalWidth = 0.5;
                flower5.Type = 1;
                context.Flowers.Add(flower5);

                Flower flower6 = new Flower();
                flower6.PedalLength = 2;
                flower6.PedalWidth = 0.5;
                flower6.Type = 0;
                context.Flowers.Add(flower6);

                Flower flower7 = new Flower();
                flower7.PedalLength = 5.5;
                flower7.PedalWidth = 1;
                flower7.Type = 1;
                context.Flowers.Add(flower7);

                Flower flower8 = new Flower();
                flower8.PedalLength = 1;
                flower8.PedalWidth = 1;
                flower8.Type = 0;
                context.Flowers.Add(flower8);

                context.SaveChanges();
            }
        }
    }
}
