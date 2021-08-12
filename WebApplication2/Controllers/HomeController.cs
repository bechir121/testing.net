using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Config;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.FirstName = "BechirSolution";
            ViewBag.LastName = "My project";
            ViewBag.Adress = "Tunsisia";

            TempData["Employex"] = "zaouali bechir";
            return View();
        }
        /*
        public ActionResult GetEmployee()
        {

            List<Employee> employees = new List<Employee>()
            
            {
                new Employee()
                {
                    EmployeeId = 1,
                    EmployeeName = "ZAOUALI Bechir",
                    Address = "London",
                    DateOfJoining = System.DateTime.Now,
                    MartialStatuts =2,
                    IsEligebleForLoad = true,
                    Salary = 155000,
                    CreatedBy = "Admin",
                    CreateDate = System.DateTime.Now
                },
                new Employee()
                {
                    EmployeeId = 2,
                    EmployeeName = "Bechir",
                    Address = "London",
                    DateOfJoining = System.DateTime.Now,
                    MartialStatuts = 1,
                    IsEligebleForLoad = true,
                    Salary = 155000,
                    CreatedBy = "Admin",
                    CreateDate = System.DateTime.Now
                },
                new Employee()
                {
                    EmployeeId = 3,
                    EmployeeName = "ZAOUALI ",
                    Address = "London",
                    DateOfJoining = System.DateTime.Now,
                    MartialStatuts = 2,
                    IsEligebleForLoad = true,
                    Salary = 155000,
                    CreatedBy = "Admin",
                    CreateDate = System.DateTime.Now
                }
            };
                 ViewBag.Employee = employees;
            return View();
        }
        */
        public ActionResult Create()
        {

            return View();
        }
       

        [HttpPost]
        public ActionResult SaveEmployees(Employee employee)
        {
            /*
            var vaOne = DateTime.Parse(employee.DateOfJoining);
            var vaTwo = DateTime.Parse(employee.CreateDate);
            */
            using (SqlConnection con = new SqlConnection(StoreConnexion.GetConnection()))
            {
               using (SqlCommand cmd = new SqlCommand("INSERT INTO Employees(EmployeeName , Address , DateOfJoining, MartialStatuts, IsEligebleForLoad , Salary, CreatedBy , CreateDate  ) VALUES('" + employee.EmployeeName+ "','" + employee.Address+ "','" + employee.DateOfJoining+ "','" + employee.MartialStatuts + "','" + employee.IsEligebleForLoad + "','" + employee.Salary + "','" + employee.CreateDate + "','" + employee.CreateDate+ "')", con))
                {

                    if (con.State != System.Data.ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("GetAllEmployee");
        }


        public ActionResult GetAllEmployee()
        {
            List<Employee> employeees= new List<Employee>();
            using (SqlConnection con = new SqlConnection(StoreConnexion.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employees", con))
                {
                    if (con.State != System.Data.ConnectionState.Open)
                        con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    DataTable dtEmployees = new DataTable();
                    dtEmployees.Load(sdr);
                    foreach (DataRow row in dtEmployees.Rows)
                    {
                        employeees.Add(
                            new Employee
                            {
                                Id = Convert.ToInt32(row["Id"]),
                                EmployeeName = row["EmployeeName"].ToString(),
                                Address = row["Address"].ToString(),
                                DateOfJoining = row["DateOfJoining"].ToString(),
                                MartialStatuts= Convert.ToInt32(row["MartialStatuts"]),
                                IsEligebleForLoad= Convert.ToBoolean(row["IsEligebleForLoad"]),
                                Salary = Convert.ToDecimal(row["Salary"]),
                                CreatedBy = row["CreatedBy"].ToString(),
                                CreateDate= row["CreateDate"].ToString()
                            });
                    }
                }
            }
            return View(employeees);
        }

        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return HttpNotFound();
            }
            using (SqlConnection con = new SqlConnection(StoreConnexion.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Employees WHERE Id = '" + id + "'", con))
                {
                    if (con.State != System.Data.ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("GetAllEmployee");

        }

     


    }
}