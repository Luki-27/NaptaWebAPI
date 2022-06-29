using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaptaBackend
{
    public class Context : IdentityDbContext
    {
        public Context()
            : base(@"ConNaptaDB")
        {//Data Source=LAPTOP-TP8ROTBT\SQLEXPRESS;Initial Catalog=NaptaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        }

        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }

        public virtual DbSet<Nationality> Nationalities{ get; set; }
        public virtual DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Result>().
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<NaptaBackend.ApplicationUser> IdentityUsers { get; set; }

        public System.Data.Entity.DbSet<NaptaBackend.Plant> Plants { get; set; }

        public System.Data.Entity.DbSet<NaptaBackend.Fertilizer> Fertilizers { get; set; }

        public System.Data.Entity.DbSet<NaptaBackend.PlanFertilizer> PlanFertilizers { get; set; }

        public System.Data.Entity.DbSet<NaptaBackend.Comment> Comments { get; set; }

        public System.Data.Entity.DbSet<NaptaBackend.Disease> Diseases { get; set; }

        //public System.Data.Entity.DbSet<NaptaWebAPI.Models.ET> ETs { get; set; }
    }
}
