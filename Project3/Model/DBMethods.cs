﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Model
{
    public static class DBMethods
    {
        public static void SaveGroup(List<User> users, AppContext db)
        {
            var Group = new Group() { SaveDateTime = DateTime.Now, Users = users };

            //using AppContext db = new();
            db.Groups.Add(Group);
            db.SaveChanges();
        }
    }
}
