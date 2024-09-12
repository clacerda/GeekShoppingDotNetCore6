using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderAPI.Model.Context
{
    public class MySqlContext : DbContext
    {
 
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {

        }

      //  public DbSet<Product> Products { get; set; }

        public DbSet<OrderDetail> Details { get; set; }

        public DbSet<OrderHeader> Headers { get; set; }
    }
}
