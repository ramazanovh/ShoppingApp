using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;

namespace EFProjectApp.DataAccess;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=LAPTOP-0RJ53JCA\SQLEXPRESS;Database=ShopDb2;Trusted_Connection=true;encrypt=false;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<DeliveryAddress>()
            .HasKey(da => da.Id);

        modelBuilder.Entity<Wallet>()
            .HasKey(w => w.Id);

        modelBuilder.Entity<Card>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Invoice>()
            .HasKey(i => i.Id);

        modelBuilder.Entity<Basket>()
            .HasKey(b => b.Id);

        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<ProductInvoices>()
            .HasKey(pi => pi.Id);

        modelBuilder.Entity<Brand>()
            .HasKey(b => b.Id);

        modelBuilder.Entity<Category>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Discount>()
            .HasKey(d => d.Id);

        modelBuilder.Entity<DeliveryAddress>()
            .HasOne(da => da.User)
            .WithMany(u => u.DeliveryAddresses) 
            .HasForeignKey(da => da.UserId)
            .IsRequired();

        modelBuilder.Entity<Wallet>()
           .HasOne(w => w.User)
           .WithMany(u => u.Wallets) 
           .HasForeignKey(w => w.UserId)
           .IsRequired();

        modelBuilder.Entity<Card>()
           .HasIndex(c => new { c.CardNumber, c.Cvc })
           .IsUnique();

        modelBuilder.Entity<Invoice>()
            .HasOne<User>(i => i.User)
            .WithMany(u => u.Invoices) 
            .HasForeignKey(i => i.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Card>()
            .HasOne(c => c.Wallet)
            .WithMany(w => w.Cards)
            .HasForeignKey(c => c.WalletId)
            .OnDelete(DeleteBehavior.Restrict);
            

        modelBuilder.Entity<User>()
            .HasOne(u => u.Basket)
            .WithOne(b => b.User)
            .HasForeignKey<Basket>(b => b.UserId)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasIndex(u => new { u.UserName, u.Email })
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => new { u.UserName, u.Phone })
            .IsUnique();

        modelBuilder.Entity<Invoice>()
            .HasOne<Wallet>(i => i.Wallet)
            .WithMany(w => w.Invoices)
            .HasForeignKey(i => i.WalletId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Basket)
            .WithMany(b => b.Products) 
            .HasForeignKey(p => p.BasketId)
            .IsRequired();

        modelBuilder.Entity<ProductInvoices>()
            .HasOne<Product>(pi => pi.Product)
            .WithMany(p => p.ProductInvoices)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProductInvoices>()
            .HasOne(pi => pi.Invoice)
            .WithMany(i => i.ProductInvoices) 
            .HasForeignKey(pi => pi.InvoiceId)
            .IsRequired();


        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products) 
            .HasForeignKey(p => p.CategoryId)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Brand)
            .WithMany(b => b.Products) 
            .HasForeignKey(p => p.BrandId)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Discount)
            .WithMany(d => d.Products) 
            .HasForeignKey(p => p.DiscountId)
            .IsRequired();
    }
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Card> Cards { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Wallet> Wallets { get; set; } = null!;
    public DbSet<Basket> Baskets { get; set; } = null!;
    public DbSet<DeliveryAddress> DeliveryAddresses { get; set; } = null!;
    public DbSet<Invoice> Invoices { get; set; } = null!;
    public DbSet<ProductInvoices> ProductInvoices { get; set; } = null!;
    public DbSet<Discount> Discounts { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Brand> Brands { get; set; } = null!;





}