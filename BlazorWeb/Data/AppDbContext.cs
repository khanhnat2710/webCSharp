using BlazorWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorWeb.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartDetail> CartDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("brands");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.ToTable("admins");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("customers");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Address).HasColumnName("address");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Ram).HasColumnName("ram");
            entity.Property(e => e.Rom).HasColumnName("rom");
            entity.Property(e => e.Chip).HasColumnName("chip");
            entity.Property(e => e.Screen_size).HasColumnName("screen_size");
            entity.Property(e => e.Camera).HasColumnName("camera");
            entity.Property(e => e.Color).HasColumnName("color");
            entity.Property(e => e.Price).HasColumnName("price").HasColumnType("decimal(18,2)");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");

            entity.HasOne(e => e.brand)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.BrandId);

            entity.HasOne(e => e.category)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.CategoryId);
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.ToTable("paymentmethods");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("orders");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderDate).HasColumnName("orderDate").HasColumnType("date");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");

            entity.HasOne(e => e.PaymentMethod)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.PaymentId);

            entity.HasOne(e => e.Admin)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.AdminId);

            entity.HasOne(e => e.Customer)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.CustomerId);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("orderdetail");
            entity.HasKey(e => new { e.OrderId, e.ProductId });
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Price).HasColumnName("price").HasColumnType("decimal(18,2)");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(e => e.Order)
                .WithMany(e => e.OrderDetails)
                .HasForeignKey(e => e.OrderId);

            entity.HasOne(e => e.Product)
                .WithMany(e => e.OrderDetails)
                .HasForeignKey(e => e.ProductId);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("carts");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateDate).HasColumnName("createDate").HasColumnType("date");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");

            entity.HasOne(e => e.Customer)
                .WithMany(e => e.Carts)
                .HasForeignKey(e => e.CustomerId);
        });

        modelBuilder.Entity<CartDetail>(entity =>
        {
            entity.ToTable("cartdetail");
            entity.HasKey(e => new { e.CartId, e.ProductId });
            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(e => e.Cart)
                .WithMany(e => e.CartDetails)
                .HasForeignKey(e => e.CartId);

            entity.HasOne(e => e.Product)
                .WithMany(e => e.CartDetails)
                .HasForeignKey(e => e.ProductId);
        });
    }
}
