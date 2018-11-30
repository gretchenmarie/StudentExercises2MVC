using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercises2MVC.Models
{
    public class Exercise
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Language{ get; set; }
    }
}
