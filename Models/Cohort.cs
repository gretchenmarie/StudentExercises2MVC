using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentExercisesAPI.Data
{
    public class Cohort
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
       
    }

}
