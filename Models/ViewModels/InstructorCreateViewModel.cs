using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

using StudentExercisesAPI.Data;

namespace StudentExerciseMVC.Models.ViewModels
{
    public class InstructorCreateViewModel
    {
        private readonly IConfiguration _config;


        public Instructor instructor { get; set; }
      
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SlackHandle { get; set; }
        public string Specialty { get; set; }
        public Cohort Cohort { get; set; }
        public int CohortId { get; set; }

        public List<SelectListItem> Cohorts { get; set; }

       

        public InstructorCreateViewModel() { }

        public InstructorCreateViewModel(IConfiguration config)
        {
            using (IDbConnection conn = new SqlConnection(config.GetConnectionString("DefaultConnection")))
            {
                Cohorts = conn.Query<Cohort>(@"
                    SELECT Id, Name FROM Cohort;
                ")
                .AsEnumerable()
                .Select(li => new SelectListItem
                {
                    Text = li.Name,
                    Value = li.Id.ToString()
                }).ToList();
                ;
            }

            Cohorts.Insert(0, new SelectListItem
            {
                Text = "Choose cohort...",
                Value = "0"
            });
        }


    }
}
