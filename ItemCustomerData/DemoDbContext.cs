using ItemCustomerData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemCustomerData
{
    public class DemoDbContext : DbContext
    {
        
        public DbSet<ItemMaster> ItemMasters { get; set; }
        public DbSet<CustomerMaster> CustomerMasters { get; set; }



        public DemoDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-P3O2VMI\SQLEXPRESS;Database=EFOrderDb;Trusted_Connection=True;");

        }
    }
}
