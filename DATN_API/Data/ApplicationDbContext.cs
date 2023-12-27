using System.Data;
using DATN_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Data
{
	public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<AddressShip> AddressShips { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillItems> BillItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ConsumerPoint> ConsumerPoints { get; set; }
        public DbSet<Formula> Formulas { get; set; }
        public DbSet<HistoryConsumerPoint> HistoryConsumerPoints { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProductItems> ProductItems { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Promotions> Promotions { get; set; }
        public DbSet<PromotionsItem> PromotionsItem { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<VoucherUser> VoucherUsers { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cart>().HasOne(u => u.User).WithOne(c => c.Cart).HasForeignKey<Cart>(c => c.UserId);
            builder.Entity<Bill>().HasOne(u => u.Users).WithMany(b => b.Bills).HasForeignKey(b => b.UserId);
            builder.Entity<BillItems>().HasOne(b => b.Bills).WithMany(bi => bi.BillItems).HasForeignKey(bi => bi.BillId);
            builder.Entity<VoucherUser>().HasOne(u => u.Users).WithMany(vb => vb.VoucherUsers).HasForeignKey(vb => vb.UserId);
            builder.Entity<VoucherUser>().HasOne(v => v.Voucher).WithMany(vb => vb.Voucher_Users).HasForeignKey(vb => vb.VoucherId);
            builder.Entity<AddressShip>().HasOne(u => u.Users).WithMany(a => a.AddressShips).HasForeignKey(a => a.UserId);
            builder.Entity<Reviews>().HasOne(u => u.Users).WithMany(r => r.Reviews).HasForeignKey(r => r.UserId);
            builder.Entity<Image>().HasOne(r => r.Reviews).WithMany(i => i.Images).HasForeignKey(i => i.ReviewId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Image>().HasOne(pi => pi.ProductItems).WithMany(i => i.Images).HasForeignKey(i => i.ProductItemId);
            builder.Entity<ProductItems>().HasOne(c => c.Colors).WithMany(pi => pi.ProductItems).HasForeignKey(pi => pi.ColorId);
            builder.Entity<ProductItems>().HasOne(s => s.Size).WithMany(pi => pi.ProductItems).HasForeignKey(pi => pi.SizeId);
            builder.Entity<PromotionsItem>().HasOne(pi => pi.ProductItems).WithMany(pp => pp.PromotionsProducts).HasForeignKey(pp => pp.ProductItemsId);
            builder.Entity<PromotionsItem>().HasOne(p => p.Promotions).WithMany(pp => pp.PromotionsProducts).HasForeignKey(pp => pp.PromotionsId);
            builder.Entity<ProductItems>().HasOne(p => p.Products).WithMany(pi => pi.ProductItems).HasForeignKey(pi => pi.ProductId);
            builder.Entity<ProductItems>().HasOne(c => c.Categorys).WithMany(p => p.ProductItems).HasForeignKey(p => p.CategoryId);
            builder.Entity<CartItems>().HasOne(pi => pi.ProductItems).WithMany(ci => ci.CartItems).HasForeignKey(ci => ci.ProductItemId);
            builder.Entity<CartItems>().HasOne(c => c.Cart).WithMany(ci => ci.CartItems).HasForeignKey(ci => ci.UserId);
            builder.Entity<BillItems>().HasOne(pi => pi.ProductItems).WithMany(bi => bi.BillItems).HasForeignKey(bi => bi.ProductItemsId);
            builder.Entity<BillItems>().HasOne(b => b.Bills).WithMany(bi => bi.BillItems).HasForeignKey(bi => bi.BillId);


            builder.Entity<Bill>().HasOne(e => e.PaymentMethods).WithMany(b => b.Bills).HasForeignKey(y => y.PaymentMethodId);
            builder.Entity<Bill>().HasOne(e => e.Vouchers).WithMany(b => b.Bills).HasForeignKey(y => y.VoucherId);
            builder.Entity<HistoryConsumerPoint>().HasOne(f => f.Formulas).WithMany(h => h.HistoryConsumerPoints).HasForeignKey(h => h.FormulaId);
            builder.Entity<HistoryConsumerPoint>().HasOne(c => c.ConsumerPoints).WithMany(h => h.HistoryConsumerPoints).HasForeignKey(h => h.ConsumerPointId);
            builder.Entity<HistoryConsumerPoint>().HasOne(a => a.Bill).WithMany(x => x.HistoryConsumerPoints).HasForeignKey(x => x.BillId);
            builder.Entity<ConsumerPoint>().HasOne(c => c.Users).WithOne(u => u.ConsumerPoint).HasForeignKey<ConsumerPoint>(u => u.UserID).OnDelete(DeleteBehavior.NoAction);


            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("AspNetUsers");
            builder.Entity<Role>().ToTable("AspNetRoles");
            CreateRoles(builder);





            builder.Entity<Bill>().Property(x => x.ReducedAmount).IsRequired(false);
            builder.Entity<Bill>().Property(x => x.VoucherId).IsRequired(false);
            builder.Entity<Bill>().Property(x => x.PaymentMethodId).IsRequired(false);
            builder.Entity<HistoryConsumerPoint>().Property(x => x.FormulaId).IsRequired(false);
        }
        protected void CreateRoles(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                    new Role() { Id = Guid.Parse("039d186b-f613-4652-91bb-fa284a2e7e33"), Name = "Admin", NormalizedName = "ADMIN" },
                    new Role() { Id = Guid.Parse("e1813d8e-9ab3-47b6-bb17-cbb48f114d36"), Name = "User", NormalizedName = "USER" },
                    new Role() { Id = Guid.Parse("1048eea1-29e3-4139-9b25-29a4fb2562d7"), Name = "Staff", NormalizedName = "STAFF" }
                );
            var hasher = new PasswordHasher<IdentityUser>();
            builder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("76671898-a7dd-4d40-a1da-e56639a4dbe4"),  // admin mặc định
                    UserName = "admin",
                    NormalizedUserName = "ADMIN@EXAMPLE.COM",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "123456"),
                    SecurityStamp = string.Empty,
                    Status = 1
                },
                new User
                {
                    Id = Guid.Parse("5fcc1c98-da29-4d88-b088-f921528142a2"), // nhan vien mặc định
                    UserName = "nhanvien",
                    NormalizedUserName = "NHANVIEN@EXAMPLE.COM",
                    Email = "nhanvien@example.com",
                    NormalizedEmail = "nhanvien@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "123456"),
                    SecurityStamp = string.Empty,
                    Status = 1
                },

                   new User
                   {
                       Id = Guid.Parse("2e8676b5-daa0-4b9e-bc06-06d5b06f7078"), // nguoi dung mặc định
                       UserName = "user",
                       NormalizedUserName = "user@EXAMPLE.COM",
                       Email = "user@example.com",
                       NormalizedEmail = "user@EXAMPLE.COM",
                       EmailConfirmed = true,
                       PasswordHash = hasher.HashPassword(null, "123456"),
                       SecurityStamp = string.Empty,
                       Status = 1
                   },
                    new User
                    {
                        Id = Guid.Parse("f1dded95-3dae-4568-a66e-66f47fbc4ffd"), // khach vang lai
                        UserName = "khachvanglai",
                        NormalizedUserName = "khachvanglai@EXAMPLE.COM",
                        Email = "khachvanglai@example.com",
                        NormalizedEmail = "khachvanglai@EXAMPLE.COM",
                        EmailConfirmed = true,
                        PasswordHash = null,
                        SecurityStamp = string.Empty,
                        Status = 1
                    }

                );

            builder.Entity<Cart>().HasData(
                new Cart
                {
                    UserId = Guid.Parse("2e8676b5-daa0-4b9e-bc06-06d5b06f7078"),
                    Description = string.Empty,
                    Status = 1
                },
                 new Cart
                 {
                     UserId = Guid.Parse("f1dded95-3dae-4568-a66e-66f47fbc4ffd"),
                     Description = string.Empty,
                     Status = 1
                 }
                );
            builder.Entity<ConsumerPoint>().HasData(
                    new ConsumerPoint
                    {
                        UserID =Guid.Parse("2e8676b5-daa0-4b9e-bc06-06d5b06f7078"),
                        Point ="0",
                        Status =1
                    },
                     new 
                     {
                         UserID = Guid.Parse("f1dded95-3dae-4568-a66e-66f47fbc4ffd"),
                         Point = "0",
                         Status = 1
                     }
                );
            builder.Entity<IdentityUserRole<Guid>>().HasData(
             new IdentityUserRole<Guid> { RoleId = Guid.Parse("039d186b-f613-4652-91bb-fa284a2e7e33"), UserId = Guid.Parse("76671898-a7dd-4d40-a1da-e56639a4dbe4") },
             new IdentityUserRole<Guid> { RoleId = Guid.Parse("e1813d8e-9ab3-47b6-bb17-cbb48f114d36"), UserId = Guid.Parse("2e8676b5-daa0-4b9e-bc06-06d5b06f7078") },
             new IdentityUserRole<Guid> { RoleId = Guid.Parse("1048eea1-29e3-4139-9b25-29a4fb2562d7"), UserId = Guid.Parse("5fcc1c98-da29-4d88-b088-f921528142a2") }
            );
        }
    }
}
