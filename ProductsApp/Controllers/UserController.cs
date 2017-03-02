using ProductsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Odbc;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductsApp.Controllers
{
    public class UserController : ApiController
    {
        User[] users = new User[]
       {
            new User { Id = 1, Name = "Tomato Soup", Country = "Groceries" },
            new User { Id = 2, Name = "Yo-yo", Country = "Toys" },
            new User { Id = 3, Name = "Hammer", Country = "Hardware" }
       };

        public IEnumerable<User> GetAllUser()
        {
            try
            {
                using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand("SELECT name FROM test_users", connection))
                    using (OdbcDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                            dr.Close();
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }

            return users;
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = users.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
