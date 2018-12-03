using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentExercisesAPI.Data;

namespace StudentExercises2MVC.Models
{


    namespace StudentExercisesAPI.Data
    {
        public class Exercise
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Language { get; set; }
            public List<Student> AssignedStudents { get; set; } = new List<Student>();
        }
    }

}
