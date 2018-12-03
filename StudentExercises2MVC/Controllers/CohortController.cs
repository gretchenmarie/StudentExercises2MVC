using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentExercises2MVC.Models.ViewModels;
using StudentExercisesAPI.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StudentExercises2MVC.Controllers
{
    public class CohortController : Controller
    {
        private readonly IConfiguration _config;

        public CohortController(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        // GET: Cohort
        public async Task<ActionResult> Index()
        {
            using (IDbConnection conn = Connection)
            {

                IEnumerable<Cohort> cohorts = await conn.QueryAsync<Cohort>(@"
                    SELECT 
                        c.Id,
                        c.Name
                       
                    FROM Cohort c
                ");
                return View(cohorts);
            }
        }

        // GET: Cohort/Details/5
        public async Task<ActionResult> Details(int id)
        {
            string sql = $@"
            SELECT
                c.Id,
                c.Name
            FROM Cohort c
            WHERE c.Id = {id}
            ";

            using (IDbConnection conn = Connection)
            {
                Cohort cohort = await conn.QueryFirstAsync<Cohort>(sql);
                return View(cohort);
            }
        }

        // GET: Cohort/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cohort/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CohortCreateViewModel model)
        {
            string sql = $@"INSERT INTO Cohort 
            (Name)
            VALUES
            (              
                 '{model.cohort.Name}'       
            );";

            using (IDbConnection conn = Connection)
            {
                var newId = await conn.ExecuteAsync(sql);
                return RedirectToAction(nameof(Index));
            }

        }

        // GET: Cohort/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {

            string sql = $@"
            SELECT
                c.Id,
                c.Name
            FROM Cohort c
            WHERE c.Id = {id}
            ";

            using (IDbConnection conn = Connection)
            {
                Cohort cohort = await conn.QueryFirstAsync<Cohort>(sql);
                return View(cohort);
            }
        }


        // POST: Cohort/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Cohort cohort)
        {
            try
            {
                // TODO: Add update logic here
                string sql = $@"
                    UPDATE Cohort
                    SET Name = '{cohort.Name}',                     
                    WHERE Id = {id}";

                using (IDbConnection conn = Connection)
                {
                    int rowsAffected = await conn.ExecuteAsync(sql);
                    if (rowsAffected > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return BadRequest();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Cohort/Delete/5
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            string sql = $@"
            SELECT
                c.Id,
                c.Name
            FROM Cohort c
            WHERE c.Id = {id}
            ";

            using (IDbConnection conn = Connection)
            {
                Cohort cohort = await conn.QueryFirstAsync<Cohort>(sql);
                return View(cohort);
            }
        }

        // POST: Cohort/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<ActionResult> Delete(int id)
        {
            string sql = $@"DELETE FROM Cohort WHERE Id = {id}";

            using (IDbConnection conn = Connection)
            {
                int rowsAffected = await conn.ExecuteAsync(sql);
                if (rowsAffected > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                throw new Exception("No rows affected");
                
            }
           
        }
    }
}