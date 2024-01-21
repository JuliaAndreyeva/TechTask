using JsonTree.Models;
using Microsoft.EntityFrameworkCore;

namespace JsonTree.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ObjectChild>()
        //        .HasKey(oc => new { oc.ParentId, oc.ChildId });


        //    base.OnModelCreating(modelBuilder);
        //}
        public DbSet<ObjectModel> ObjectModels { get; set; }
        public DbSet<ObjectChild> ObjectChilds { get; set; }
    }
}
