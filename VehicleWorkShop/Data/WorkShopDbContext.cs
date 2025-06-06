using Microsoft.EntityFrameworkCore;
using VehicleWorkShop.Models;

namespace VehicleWorkShop.Data
{
    public class WorkShopDbContext:DbContext
    {
        public WorkShopDbContext(DbContextOptions<WorkShopDbContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<WorkShop> WorkShops { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockType> StockTypes { get; set; }
        public DbSet<Ledger> Ledgers { get; set; }
        public DbSet<InventoryType> InventoryTypes { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetails> SalesDetails { get; set; }
        public DbSet<SalesReturn> SalesReturns { get; set; }
        public DbSet<SalesReturnDetail> SalesReturnDetails { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetail> PurchasesDetails { get;set; }
        public DbSet<PurchaseReturn> PurchaseReturns { get; set; }
        public DbSet<PurchaseReturnDetail> PurchasesReturnDetails { get;set;}
        public DbSet<Damage> Damages { get; set; }
        public DbSet <DamageDetail> DamageDetails { get; set;}
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<TransferDetail> TransferDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<DamageDetail>()
                .HasOne(d => d.Product)
                .WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PurchaseDetail>()
                .HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PurchaseReturnDetail>()
                .HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SaleDetails>()
                .HasOne(sd => sd.Product)
                .WithMany()
                .HasForeignKey(sd => sd.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SaleDetails>()
                .HasOne(sd => sd.Sale)
                .WithMany(s => s.SaleDetails)
                 .HasForeignKey(sd => sd.SaleId)
             .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<SalesReturnDetail>()
                 .HasOne(s => s.Product)
                 .WithMany()
                 .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Product)
                .WithMany()
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Sale)
                .WithMany()
                .HasForeignKey(p => p.SaleId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Customer)
                .WithMany()
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.PaymentType)
                .WithMany()
                .HasForeignKey(p => p.PaymentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany()
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TransferDetail>()
                  .HasOne(td => td.SourceStore)
                  .WithMany()
                  .HasForeignKey(td => td.SourceStoreId)
                  .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<TransferDetail>()
                .HasOne(td => td.DestinationStore)
                .WithMany()
                .HasForeignKey(td => td.DestinationStoreId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
