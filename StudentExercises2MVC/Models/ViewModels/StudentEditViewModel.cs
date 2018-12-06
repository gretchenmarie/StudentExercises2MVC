using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using StudentExercises2MVC.Models.StudentExercisesAPI.Data;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercises2MVC.Models.ViewModels
{
    public class StudentEditViewModel
    {
        private readonly IConfiguration _config;
       // internal object instructorl

        public List<SelectListItem> Exercises { get; set; }
       // public MultiSelectList SelectedExercises { get; private set; }
        public List<int> SelectedExercises{ get; set; }

        public Student student { get; set; }

        public Instructor instructor { get; set; }

        public StudentEditViewModel() { }

        public StudentEditViewModel(IConfiguration config)
        {

            using (IDbConnection conn = new SqlConnection(config.GetConnectionString("DefaultConnection")))
            {
                Exercises = conn.Query<Exercise>(@"
                    SELECT Id, Name FROM Exercise;
                ")
                .AsEnumerable()
                .Select(li => new SelectListItem
                {
                    Text = li.Name,
                    Value = li.Id.ToString()
                }).ToList();
                ;
            }
            Exercises.Insert(0, new SelectListItem
            {
                Text = "Choose exercise...",
                Value = "0"
            });
        }
    }
}

