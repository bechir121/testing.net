using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class StudentViewModel
    {
        public List<Student> Student { get; set; }
        public List<Subjects> Subjects { get; set; }
    }
}