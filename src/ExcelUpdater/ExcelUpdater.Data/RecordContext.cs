using ExceUpdater.Domain;
using Microsoft.EntityFrameworkCore;


namespace ExcelUpdater.Data
{
    public class RecordContext : DbContext
    {
        public DbSet<Record> Records { get; set; }

        public RecordContext(DbContextOptions<RecordContext> opts) : base(opts){ 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           builder.Entity<Record>().HasKey(rec => new { rec.My_Id, rec.My_Date });
           base.OnModelCreating(builder);
        }
    }
}
