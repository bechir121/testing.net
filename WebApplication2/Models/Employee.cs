using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{

    // represent entity on dataBase 
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string Address  { get; set; }
        public string DateOfJoining { get; set; }
        public int MartialStatuts { get; set; }
        public bool  IsEligebleForLoad{ get; set; }
        public decimal Salary { get; set; }
        public string CreatedBy { get; set; }
        public string CreateDate { get; set; }


    }
}