using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentExercisesAPI.Data
{
    public class Student
    {
        public int Id { get; set; }
      
        public string FirstName { get; set; }
    
        public string LastName { get; set; }
    
        public string SlackHandle { get; set; }

        public int CohortId { get; set; }
        public Cohort Cohort { get; set; }
       
    }

}