using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
     //   public ActionResult Index()
       // {
         //   return View();
        //}

        public ActionResult GetStudent()
        {
            var Student= new List<Student>()
            {
                new Student {Id = 1,
                Name = "zaouali bechir",
                Address = "tunsia"
}           };

             var subjects = new List<Subjects>()
            {
                new Subjects(){ Id=1 , Name="Computer Science" },
                new Subjects(){ Id=2 , Name="deep learning" },
                new Subjects(){ Id=3 , Name="Inductive Statistics" }
            };
            
            var viewmodel = new StudentViewModel
            {
                Student = Student,
                Subjects = subjects
            };

                                 
            return View(viewmodel);
        }

        public ActionResult CreateStudent()
        {
            var student = new Student() { Id = 1, Name = "ZAOUALI", Address = "London" };
            return View(student); 
        }


        public ActionResult EditStudent(int id)
        {

            return Content("Student Id " + id); 
        }

        //restriction on route
        //range , min , max , regex for regular expression
        // data types
        [Route( "Student/bypassingyear/{mounth:int:(1,12)}/{year;min(1990)}")]
        public ActionResult Bypassingyear(int mounth , int year)
        {
            return Content("Mounth :" + mounth + "year :" + year);

        }


        public ActionResult somme()
        {

            var somme = 1 + 2;
            ViewBag.Somme = somme;
            return View(somme); 
        }
    }

    
    /*
        List<Student> Students = new List<Student>()

        {
            new Student()
            {
                Id = 1,
                Name = "Albert Enstein",
                Address = "London"
                                },
            new Student()
            {
                Id = 1,
                Name = "ZAOUALI Bechir",
                Address = "Tunisia",
                                },
new Student()
            {
                Id = 1,
                Name = "Asma Jelaasi",
                Address = "Algeria",
                                } };


        ViewBag.pluss = "YesWeCan";
        */
}