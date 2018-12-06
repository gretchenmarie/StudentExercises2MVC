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
using StudentExercises2MVC.Models.ViewModels;

using StudentExercises2MVC.Models.StudentExercisesAPI.Data;
using StudentExercises2MVC.Models;

namespace StudentExercises2MVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IConfiguration _config;
        private readonly object id;

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
        {//creating a new instance of student to hold the generated student and exercises assigned to that student
            var Student = new Student();
            using (IDbConnection conn = Connection)
            {
                IEnumerable<Student> StudAndExerc = conn.Query<Student, Exercise, Student>(
                 $@"
           Select
           s.Id,
           s.FirstName,
           s.LastName,
           s.SlackHandle,
           e.Id,
           e.Name,
            e.Language
           From Student as s
           Join StudentExercise as ex
            on s.Id = ex.StudentId
           Join exercise as e on e.Id = ex.ExerciseId
           Where s.Id = {id}
            ",
           //multimapping with dapper
                (generatedStudent, generatedExercise) =>
                {
                    if (Student.FirstName == null)
                    {
                        Student = generatedStudent;
                    }
                    Student.AssignedExercises.Add(generatedExercise);

                    return generatedStudent;
                }
                );

               // Console.WriteLine();
                return View(Student);

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
                StudentEditViewModel model = new StudentEditViewModel(_config)
                {
                    student = student
                };
                return View(model);
            }
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Edit(StudentEditViewModel model)
        {

            // TODO: Add update logic here
            string sql = $@"
                    UPDATE Student
                    SET FirstName = '{model.student.FirstName}',
                        LastName = '{model.student.LastName}',
                        SlackHandle = '{model.student.SlackHandle}',
                        CohortId = {model.student.CohortId}
                      
                    ";

            if (model.SelectedExercises != null)
            {
                model.SelectedExercises.ForEach(i => sql += $@"
                        INSERT INTO StudentExercise
                        ( ExerciseId, StudentId, InstructorId )
                        VALUES
                        ({i},{model.student.Id}, 1);
                         
                    ");
                Console.WriteLine(sql);
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
            else
            {
                return View(model);
            }
        }






        // GET: Students/Delete/5
        public async Task<ActionResult> DeleteConfirm(int id)
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

        // POST: Students/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            string sql = $@"DELETE FROM Student WHERE Id = {id}";

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


