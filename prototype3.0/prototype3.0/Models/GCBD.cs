using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace prototype3._0.Models
{
    public class GCBD : DbContext
    {

        public GCBD() : base("name=test")
        {

        }



        public DbSet<Client> Clients { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Voiture> Voiture { get; set; }
        public DbSet<Modeles> Modeles { get; set; }
        




    }
}