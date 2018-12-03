using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using StudentExercisesAPI.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercises2MVC.Models.ViewModels
{
    public class InstructorCreateViewModel
    {
        private readonly IConfiguration _config;

        public List<SelectListItem> Cohorts { get; set; }
        public Instructor instructor { get; set; }

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
