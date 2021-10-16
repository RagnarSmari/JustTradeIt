using JustTradeIt.Software.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JustTradeIt.Software.API.Repositories.Data
{
    public class JustTradeItDbContext : DbContext
    {
        public JustTradeItDbContext(DbContextOptions<JustTradeItDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Manual Configurations of relations (many-to-many)
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCondition> ItemConditions { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<JwtToken> JwtTokens { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<User> Users { get; set; }
    }
}