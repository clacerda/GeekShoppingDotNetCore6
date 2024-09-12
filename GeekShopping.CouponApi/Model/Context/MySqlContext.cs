using GeekShopping.CouponApi.Model;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CoupontApi.Model.Context
{
    public class MySqlContext : DbContext
    {
 
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {

        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 1,
                CouponCode = "Claudio_2024",
                DiscountAmount = 10
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 2,
                CouponCode = "Claudio_2025",
                DiscountAmount = 15
            });
        }

    }
}
