using StudentExercisesAPI.Data;
using Microsoft.Extensions.Configuration;
using StudentExercises2MVC.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using StudentExercises2MVC.Models.StudentExercisesAPI.Data;

namespace StudentExercises2MVC.Models.ViewModels
{
    public class ExerciseCreateViewModel
    {
        private readonly IConfiguration _config;

        public List<SelectListItem> Students { get; set; }
        public StudentExercisesAPI.Data.Exercise exercise { get; set; }
        public object Exercises { get; }

        public ExerciseCreateViewModel() { }
        public ExerciseCreateViewModel(IConfiguration config)
        {

            using (IDbConnection conn = new SqlConnection(config.GetConnectionString("DefaultConnection")))
            {
                Exercises = conn.Query<Exercise>(@"
                    SELECT Id, Name, Language FROM Exercise;
                ")
                .AsEnumerable()
                .Select(li => new SelectListItem
                {
                    Text = li.Name,
                    Value = li.Id.ToString()
                }).ToList();
                ;
            }

            //Exercises.Insert(0, new SelectListItem
           // {
           //     Text = "Choose Exercise...",
           //     Value = "0"
           // });
        }

    }
}