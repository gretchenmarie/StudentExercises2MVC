﻿using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercises2MVC.Models.ViewModels
    {
        public class CohortCreateViewModel
        {
            private readonly IConfiguration _config;

        
            public Cohort Cohort { get; set; }

            public CohortCreateViewModel() { }

           
        }
    }

