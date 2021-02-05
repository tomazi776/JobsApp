using DataLib.Models;
using System.Data.Entity;

namespace DataLib
{
 public class ZavenContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Log> Logs { get; set; }

        public ZavenContext() : base("name=DataLibDbCnn")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ZavenContext>());
        }
    }
}
