using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using StudentExerciseMVC.Models.ViewModels;
using StudentExercises.Models.ViewModels;
using StudentExercisesAPI.Data;

namespace StudentExercises2MVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IConfiguration _config;

        public StudentsController(IConfiguration config)
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
        // GET: Students
        public async Task<ActionResult> Index()
        {
            using (IDbConnection conn = Connection)
            {

                IEnumerable<Student> students = await conn.QueryAsync<Student>(@"
                    SELECT 
                        s.Id,
                        s.FirstName,
                        s.LastName,
                        s.SlackHandle,
                        s.CohortId
                    FROM Student s
                ");
                return View(students);
            }
        }


        // GET: Students/Details/5
        public async Task<ActionResult> Details(int id)
        {
            string sql = $@"
            SELECT
                s.Id,
                s.FirstName,
                s.LastName,
                s.SlackHandle,
                s.CohortId
            FROM Student s
            WHERE s.Id = {id}
            ";

            using (IDbConnection conn = Connection)
            {
                Student student = await conn.QueryFirstAsync<Student>(sql);
                return View(student);
            }
        }

        // GET: Students/Create


        public ActionResult Create()
        {
            var model = new StudentCreateViewModel(_config);
            return View(model);
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StudentCreateViewModel model)
        {
            string sql = $@"INSERT INTO Student
    (FirstName, LastName, SlackHandle, CohortId)
    VALUES
    (
        '{model.student.FirstName}'
        ,'{model.student.LastName}'
        ,'{model.student.SlackHandle}'
        ,{model.student.CohortId}
    );";

            using (IDbConnection conn = Connection)
            {
                await conn.ExecuteAsync(sql);
                return RedirectToAction(nameof(Index));
            }

        }
        // GET: Students/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            string sql = $@"
            SELECT
                s.Id,
                s.FirstName,
                s.LastName,
                s.SlackHandle,
                s.CohortId
            FROM Student s
            WHERE s.Id = {id}
            ";

            using (IDbConnection conn = Connection)
            {
                Student student = await conn.QueryFirstAsync<Student>(sql);
                return View(student);
            }
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<ActionResult> Edit(int id, Student student)
        {
            try
            {
                // TODO: Add update logic here
                string sql = $@"
                    UPDATE Student
                    SET FirstName = '{student.FirstName}',
                        LastName = '{student.LastName}',
                        SlackHandle = '{student.SlackHandle}'
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


        // GET: Students/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Students/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}