using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using StudentExercises2MVC.Models.StudentExercisesAPI.Data;
using StudentExercises2MVC.Models.ViewModels;

namespace StudentExercises2MVC.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly IConfiguration _config;

        public ExerciseController(IConfiguration config)

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
        // GET: Exercises
        public async Task<ActionResult> Index()
        {
            using (IDbConnection conn = Connection)
            {

                IEnumerable<Exercise> exercises = await conn.QueryAsync<Exercise>(@"
                    SELECT 
                        e.Id,
                        e.Name,
                        e.Language
                       
                    FROM Exercise e
                ");
                return View(exercises);
            }
        }


        // GET: Exercises/Details/5
        public async Task<ActionResult> Details(int id)
        {
            string sql = $@"
            SELECT
                e.Id,
                e.Name,
                e.Language
               
            FROM Exercise e
            WHERE e.Id = {id}
            ";

            using (IDbConnection conn = Connection)
            {
                Models.StudentExercisesAPI.Data.Exercise exercise = await conn.QueryFirstAsync<Models.StudentExercisesAPI.Data.Exercise>(sql);
                return View(exercise);
            }
        }

        // GET: Exercises/Create


        public ActionResult Create()
        {
            var model = new ExerciseCreateViewModel(_config);
            return View(model);
        }

        // POST: Exercises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ExerciseCreateViewModel model)
        {
            string sql = $@"INSERT INTO Exercise
           (Name, Language)
    VALUES
    (
        '{model.exercise.Name}',
        '{model.exercise.Language}'       
        
    );";

            using (IDbConnection conn = Connection)
            {
                await conn.ExecuteAsync(sql);
                return RedirectToAction(nameof(Index));
            }

        }
        // GET: Exercises/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            string sql = $@"
            SELECT
                e.Id,
                e.Name,
                e.Language
               
            FROM Exercise e
            WHERE e.Id = {id}
            ";
            using (IDbConnection conn = Connection)
            {
                Models.StudentExercisesAPI.Data.Exercise exercise = await conn.QueryFirstAsync<Models.StudentExercisesAPI.Data.Exercise>(sql);
                return View(exercise);
            }
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Edit(int id, Models.StudentExercisesAPI.Data.Exercise exercise)
        {
            try
            {
                // TODO: Add update logic here

                string sql = $@"INSERT INTO Exercise
            (Name, Language)
             VALUES
                 (
                  '{exercise.Name}',
                  '{exercise.Language}'          
                     );";

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


        // GET: Students/Delete/5
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            string sql = $@"
            SELECT
                e.Id,
                e.Name,
                e.Language              
            FROM Exercise e
            WHERE e.Id = {id}
            ";

            using (IDbConnection conn = Connection)
            {
                Exercise exercise = await conn.QueryFirstAsync<Exercise>(sql);
                return View(exercise);
            }
        }

        // POST: Students/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            string sql = $@"DELETE FROM Exercise WHERE Id = {id}";

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
