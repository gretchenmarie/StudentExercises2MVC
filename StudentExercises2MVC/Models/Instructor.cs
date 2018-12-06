using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace StudentExercises2MVC.Models
{
    public class Instructor
    {


        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string SlackHandle { get; set; }
        [Required]
        public string Specialty { get; set; }
        [Required]
        public Cohort Cohort { get; set; }
        [Required]
        public int CohortId { get; set; }

    }
}
