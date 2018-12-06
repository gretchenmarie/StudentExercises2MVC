using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentExercises2MVC.Models
{
    public class Cohort
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
       
    }

}
