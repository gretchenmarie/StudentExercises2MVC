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

namespace StudentExerciseMVC.Models.ViewModels
    {
        public class CohortCreateViewModel
        {
            private readonly IConfiguration _config;

        
            public Cohort cohort { get; set; }

            public CohortCreateViewModel() { }

           
        }
    }

