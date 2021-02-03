using DataLib.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLib
{
 public class ZavenContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Log> Logs { get; set; }

        public ZavenContext() : base("name=DataLibDbCnn")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ZavenContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<ZavenDotNetInterviewContext>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ZavenDotNetInterviewContext, Migrations.Configuration>());
        }
    }
}
