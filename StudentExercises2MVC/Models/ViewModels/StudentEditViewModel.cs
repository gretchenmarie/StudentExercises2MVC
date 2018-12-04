using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using StudentExercises2MVC.Models.StudentExercisesAPI.Data;
using StudentExercisesAPI.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExerciseMVC.Models.ViewModels
{
    public class StudentEditViewModel
    {
        private readonly IConfiguration _config;

        public List<SelectListItem> Exercises { get; set; }
        public Student student { get; set; }

        public StudentEditViewModel() { }

        public StudentEditViewModel(IConfiguration config)
        {

            using (IDbConnection conn = new SqlConnection(config.GetConnectionString("DefaultConnection")))
            {
                Exercises = conn.Query<Exercise>(@"
                    SELECT Id, Name FROM Exercise;
                ")
                .Select(li => new SelectListItem
                {
                    Text = li.Name,
                    Value = li.Id.ToString()
                }).ToList();
                ;
            }
        }
    }
}
