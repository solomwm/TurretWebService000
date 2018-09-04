using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TurretWebService.Models;

namespace TurretWebService.Data
{
    public class TurretDBContext: DbContext
    {
        public TurretDBContext():base("DBConnection")
        { }

        public DbSet<User> Users { get; set; }
    }
}