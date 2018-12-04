using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StudentExercises2MVC.Models.StudentExercisesAPI.Data;

namespace StudentExercisesAPI.Data
{
    public class Student
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "How to find me on Slack")]
        public string SlackHandle { get; set; }

        public int CohortId { get; set; }
        public Cohort Cohort { get; set; }
        public string FullName { get; internal set; }
        public List<Exercise> AssignedExercises { get; set; } = new List<Exercise>();
    }

}