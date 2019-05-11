using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using L300TestApp.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;

namespace L300TestApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Todoes myTodoes = new Todoes();
            DataSet ds = new DataSet();

            //Get database configuration from app settings - connection string
            string connectionString = ConfigurationManager.ConnectionStrings["SQLDBConnection"].ToString();
            string queryString = "SELECT Id, Description, DueDate FROM Todoes ORDER BY DueDate;";

            try
            {
                Trace.TraceInformation("get connection");
                Trace.TraceInformation(connectionString);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    Trace.TraceInformation("opening connection");
                    connection.Open();
                    Trace.TraceInformation("connection made!");

                    // Create the Command and Parameter objects.
                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataAdapter da = new SqlDataAdapter(command);

                    ds = new DataSet("ToDo");
                    da.FillSchema(ds, SchemaType.Source, "ToDo");
                    da.Fill(ds, "ToDo");
                }
            }
            catch (Exception ex)
            {
                Trace.TraceInformation("ToDo FAILED!");
                Trace.TraceInformation(ex.Message.ToString());
                if (ex.InnerException != null)
                {
                    Trace.TraceInformation(ex.InnerException.Message.ToString());
                }
            }

            //Create list of Todoes
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                myTodoes.toDoes.Add(new Todo
                {
                    ID = Convert.ToInt32(dr["Id"]),
                    Description = dr["Description"].ToString(),
                    DueDate = (DateTime)dr["DueDate"]
                });
            }


                return View(myTodoes);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}