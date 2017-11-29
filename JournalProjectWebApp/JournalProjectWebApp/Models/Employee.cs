﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalProjectWebApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
    }
}