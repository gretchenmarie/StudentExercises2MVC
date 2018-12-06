
using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using StudentExercises2MVC.Models.StudentExercisesAPI.Data;

namespace StudentExercises2MVC.Models.ViewModels
{
    public class ExerciseCreateViewModel
    {
        private readonly IConfiguration _config;

        public List<SelectListItem> Students { get; set; }
        public Exercise Exercise { get; set; }
       

        public ExerciseCreateViewModel() { }

        public ExerciseCreateViewModel(IConfiguration config)
        {
            _config = config;
        }
    }
}