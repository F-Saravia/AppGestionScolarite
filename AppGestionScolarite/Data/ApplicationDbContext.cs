using AppGestionScolarite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppGestionScolarite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
         

        }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Parcour> Parcours { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<AppGestionScolarite.Models.UnitePedagogique>? UnitePedagogique { get; set; }
        public DbSet<AppGestionScolarite.Models.CartItem>? CartItems { get; set; }
        public DbSet<AppGestionScolarite.Models.ShoppingCart>? ShoppingCarts { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    #region Parcour Module

        //    modelBuilder.Entity<ParcourModule>().HasKey(fg => new { fg.ParcourId, fg.ModuleId });




        //    #endregion

        //}


    }
}