using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CrudOperationUsingJqueryCodeFirst.Models
{
    public class CDBContext : DbContext
    {
        public CDBContext() : base("StringDbContext") { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}