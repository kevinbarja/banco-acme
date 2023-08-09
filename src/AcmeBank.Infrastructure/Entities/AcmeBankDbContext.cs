using System;
using System.Collections.Generic;
using AcmeBank.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcmeBank.Persistence;

public partial class AcmeBankDbContext : DbContext
{
    public AcmeBankDbContext(DbContextOptions<AcmeBankDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Movement> Movements { get; set; }

    public virtual DbSet<Person> People { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.InitialBalance).HasColumnType("decimal(17, 5)");
            entity.Property(e => e.Number).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Password).HasMaxLength(100);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Movement>(entity =>
        {
            entity.Property(e => e.Amount).HasColumnType("decimal(17, 5)");
            entity.Property(e => e.Balance).HasColumnType("decimal(17, 5)");
            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Account).WithMany(p => p.Movements)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
