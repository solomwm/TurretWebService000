﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurretWebService.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxLevel { get; set; }
        public int MaxScore { get; set; }
    }
}