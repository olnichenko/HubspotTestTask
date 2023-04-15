using InternalDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalDAL
{
    public class HubspotDataDbContext : DbContext
    {
        private string _connectionString;
        public HubspotDataDbContext()
        {
            // Better use Dependency Injection
            _connectionString = "Data Source=HubspotData.db;";
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}
