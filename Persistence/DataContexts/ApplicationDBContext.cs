using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Domain.Common;
using System.Data;
using Domain.Drivers;
using Domain.Vendors;
using Domain.Prices;
using Domain.Orders;
using Domain.AppUsers;

namespace Persistence.DataContexts
{
    public class ApplicationDbContext : DbContext
    {

        private IDbContextTransaction _currentTransaction;

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<VendorPrice> VendorPrices { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableDetailedErrors();
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddAuditInfo()
        {
            var timestamp = DateTime.UtcNow;


            foreach (var entry in ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified)))
            {

                if (entry.State == EntityState.Added)
                {

                    ((BaseEntity)entry.Entity).CreatedDate = timestamp;
                }
                else
                {
                    ((BaseEntity)entry.Entity).UpdatedDate = timestamp;
                    ((BaseEntity)entry.Entity).CreatedDate = (DateTime?)entry.Property("CreatedDate").OriginalValue;
                }

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VendorPrice>()
            .HasKey(vp => new { vp.VendorId, vp.PriceId });

            modelBuilder.Entity<VendorPrice>()
                .HasOne(vp => vp.Vendors)
                .WithMany(v => v.VendorPrices)
                .HasForeignKey(vp => vp.VendorId);

            modelBuilder.Entity<VendorPrice>()
                .HasOne(vp => vp.Prices)
                .WithMany(p => p.VendorPrices)
                .HasForeignKey(vp => vp.PriceId);
            //-----------

            modelBuilder.Entity<Order>()
          .HasKey(x => new { x.VendorId, x.PriceId });

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Vendors)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.VendorId);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Prices)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.PriceId);

            modelBuilder.Entity<Order>()
              .HasOne(x => x.Drivers)
              .WithMany(x => x.Orders)
              .HasForeignKey(x => x.DriverId);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
