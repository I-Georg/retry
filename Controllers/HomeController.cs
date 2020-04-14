using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecreatedSurvey.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace RecreatedSurvey.Controllers
{
    public class HomeController : Controller 
    {

        public IConfiguration Configuration { get; }
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            List<Survey> AnswerList = new List<Survey>();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //SqlDataReader
                connection.Open();

                 string sql = "Select * From [Table]";
               
                 SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                       Survey survey = new Survey();
                        survey.AnswerOne = Convert.ToInt32(dataReader["AnswerOne"]);
                        survey.AnswerTwo = Convert.ToInt32(dataReader["AnswerTwo"]);
                         survey.AnswerThree = Convert.ToInt32(dataReader["AnswerThree"]);
                        survey.AnswerFour = Convert.ToInt32(dataReader["AnswerFour"]);
                        survey.AnswerFive = Convert.ToInt32(dataReader["AnswerFive"]);
                        int finalRating = (survey.AnswerOne + survey.AnswerTwo + survey.AnswerThree + survey.AnswerFour + survey.AnswerFive)/5;
                        ViewData["rating"] = finalRating;
                        AnswerList.Add(survey);
                    }
                   
                }
                connection.Close();
            }
            return View(AnswerList);
        }

                       
        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Survey survey)
        {
            if (ModelState.IsValid)
            {
                string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $"Insert Into [Table](AnswerOne, AnswerTwo, AnswerThree, AnswerFour,AnswerFive) Values ('{survey.AnswerOne}', '{survey.AnswerTwo}','{survey.AnswerThree}','{survey.AnswerFour}', '{survey.AnswerFive}')";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                         command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return RedirectToAction("Index");
                }
            }
            else
                return View();
        }
    }
}



//public IActionResult Privacy()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}
