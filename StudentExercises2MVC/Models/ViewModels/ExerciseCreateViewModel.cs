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
        public Exercise exercise { get; set; }
       

        public ExerciseCreateViewModel() { }

        public ExerciseCreateViewModel(IConfiguration config)
        {
            _config = config;
        }
    }
}