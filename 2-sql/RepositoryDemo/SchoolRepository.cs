using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EfDbFirstDemo.DataModel;
using Microsoft.EntityFrameworkCore;

namespace EfDbFirstDemo.ConsoleApp
{
    // the job of a repository class is to expose simple data access operations
    // and implement them with data access logic.
    //  (but business logic is not its job).

    // two different styles - either each method does savechanges itself
    //   (downside, no transaction logic available "through" the repository)
    //   (upside, the calling code doesnt have to remember to call some Save method)
    // ..or, the context is a field, shared among the methods, and it doesn't save unless
    //    calling code calls your Save method.
    class SchoolRepository
    {
        private readonly DbContextOptions<ChinookContext> _contextOptions;

        public SchoolRepository(DbContextOptions<ChinookContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        public List<Student> GetAllStudents()
        {
            using var context = new ChinookContext(_contextOptions);

            // the job of this method is to return some ConsoleApp.Student objects,
            //   NOT some EF-generated DataAccess.Student objects.

            // i have to convert between those right here in this method.

            var dbStudents = context.Students.ToList();

            var appStudents = dbStudents.Select(s => new Student(s.Name, s.Id.ToString())).ToList();

            return appStudents;
        }

        // you should be clear in your repository methods
        // about exactly what related data is going to be returned.
        // returnc courses including their students.
        public Course GetCourseById(string courseId)
        {
            using var context = new ChinookContext(_contextOptions);

            DataModel.Course dbCourse = context.Courses
                .Include(c => c.CourseStudents)
                    .ThenInclude(cs => cs.Student)
                .First(c => c.Id == courseId);

            var course = new Course(dbCourse.Id, dbCourse.Name);
            foreach (CourseStudent cs in dbCourse.CourseStudents)
            {
                course.Students.Add(new Student(cs.StudentId.ToString(), cs.Student.Name));
            }

            return course;
        }

        // look at the Students property and persist any changes to it.
        // any unrecognized students will be ignored
        public void UpdateCourseMemberships(Course course)
        {
            using var context = new ChinookContext(_contextOptions);

            DataModel.Course dbCourse = context.Courses
                .Include(c => c.CourseStudents)
                    .ThenInclude(cs => cs.Student)
                .First(c => c.Id == course.CourseId);

            var dbStudents = course.Students
                .Select(s => context.Students.Find(int.Parse(s.Id)))
                .Where(s => s != null); // ignore the unrecognized ones

            // if there is already a tracked entity for something you ask for with "Find",
            // it will give that existing instance to you instead of sending a new query.

            // three possible cases: a student being added that's not already there
            //                       a student being removed that was in there until now
            //                       a student still in the course that was before also

            // exercise to the reader

            context.SaveChanges();
        }
    }
}
