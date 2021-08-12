using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Config;
using System.Data;

namespace WebApplication2.Controllers
{
    public class ProductController : Controller
    {

        public ActionResult Create()
        {

            return View(new Product { Id = 0});
        }

        [HttpPost]
        public ActionResult SaveProduct(Product product)
        {   
            
            using (SqlConnection con = new SqlConnection(StoreConnexion.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Products_SaveOrUpdate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", product.Id);
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@Supplier", product.Supplier);

                    if (con.State != System.Data.ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("GetAll");
        }
        public ActionResult GetAll()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection con = new SqlConnection(StoreConnexion.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Products_GetAllProduct", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != System.Data.ConnectionState.Open)
                        con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    DataTable dtProducts = new DataTable();
                    dtProducts.Load(sdr);
                    foreach (DataRow row in dtProducts.Rows)
                    {
                        products.Add(
                            new Product
                            {
                                Id = Convert.ToInt32(row["Id"]),
                                Name = row["Name"].ToString(),
                                Price = Convert.ToDecimal(row["Price"]),
                                Supplier = row["Supplier"].ToString()
                            }
                            );
                    }
                }
            }
        
            return View(products);
        }

        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return HttpNotFound();
            }

            using (SqlConnection con = new SqlConnection(StoreConnexion.GetConnection()))
            {
                
                using (SqlCommand cmd = new SqlCommand("Products_DeleteById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id); 
                    if (con.State != System.Data.ConnectionState.Open)
                     con.Open();
                    cmd.ExecuteNonQuery();
                                    }
            }
            return RedirectToAction("GetAll");

        }

        public ActionResult Edit(int id)
        {
            if (id < 0)
                return HttpNotFound();
            var _product = new Product();
            using (SqlConnection con = new SqlConnection(StoreConnexion.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Products_RetreiveAll", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                     if (con.State != ConnectionState.Open)
                        con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();

                    if (sdr.HasRows)
                    {
                        dt.Load(sdr);
                        DataRow row = dt.Rows[0];
                        _product.Id = Convert.ToInt32(row["Id"]);
                        _product.Name = row["Name"].ToString();
                        _product.Price = Convert.ToDecimal(row["Price"]);
                        _product.Supplier = row["Supplier"].ToString();

                        return View("Create", _product);

                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
            }
        }

    }
}


