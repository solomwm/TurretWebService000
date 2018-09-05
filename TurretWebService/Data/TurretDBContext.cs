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
        
        // Статический конструктор класса контекста БД позволяет установить инициализтор. 
        static TurretDBContext() => Database.SetInitializer(new TurretDBInitializer());

        public TurretDBContext(string ConnectionString): base(ConnectionString)
        { }

        public DbSet<User> Users { get; set; }
    }
}