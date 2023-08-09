using AcmeBank.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcmeBank.Persistence
{
    public partial class AcmeBankDbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            // TPT table per type
            modelBuilder.Entity<Person>().UseTptMappingStrategy();
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(k => k.Id)
                        .ValueGeneratedOnAdd()
                        .IsRequired();
            });
            ////Handling concurrency conflicts
            //modelBuilder.Entity<Account>()
            //    .Property(p => p.Version)
            //    .IsRowVersion();
        }
    }
}
