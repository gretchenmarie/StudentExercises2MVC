using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentExercises2MVC.Models
{

    using System.Collections.Generic;
  

    namespace StudentExercisesAPI.Data
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public List<Student> AssignedStudents { get; set; } = new List<Student>();

            internal static void Insert(int v, SelectListItem selectListItem)
            {
                throw new NotImplementedException();
            }
        }
}
    
}
