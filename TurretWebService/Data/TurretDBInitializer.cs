﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TurretWebService.Models;

namespace TurretWebService.Data
{
    public class TurretDBInitializer: DropCreateDatabaseIfModelChanges<TurretDBContext>
    {
        protected override void Seed(TurretDBContext context)
        {
            base.Seed(context);

            var userList = new List<User>()
            {
                new User {Name = "Винни-Пух", Password = "123456", MaxLevel = 0, MaxScore = 0},
                new User {Name = "Пятачок", Password = "123456", MaxLevel = 0, MaxScore = 0},
                new User {Name = "Кролик", Password = "123456", MaxLevel = 0, MaxScore = 0},
            };

            userList.ForEach(u => context.Users.Add(u));
            context.SaveChanges();
        }
    }
}