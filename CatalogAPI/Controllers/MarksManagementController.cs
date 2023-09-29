using CatalogAPI.DTOs;
using CatalogAPI.Extensions;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using CatalogAPI.Services;
using System.ComponentModel.DataAnnotations;
using CatalogAPI.Filters;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    public class MarksManagementController : Controller
    {
        private IMarksService marksService;
        private IStudentsService studentsService;
        private ITeacherAssignementsServices teacherAssignements;
        private IStudentEnrollmentService studentEnrollmentService;

        public MarksManagementController(IStudentEnrollmentService studentEnrollmentService,ITeacherAssignementsServices teacherAssignements,IStudentsService studentsService,IMarksService marksService)
        {
            this.marksService = marksService;
            this.studentsService = studentsService;
            this.teacherAssignements = teacherAssignements;
            this.studentEnrollmentService = studentEnrollmentService;
        }

        // GET: api/values
        /// <summary>
        /// returns all marks
        /// </summary>
        /// <returns>list of marks</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetAll()
        {
            return Ok(marksService.GetAllMarks());
        }

        // GET api/values/5
        /// <summary>
        /// returns the list of all marks, or the marks for a certain subject  for a student
        /// </summary>
        /// <param name="id">student id</param>
        /// <param name="subjectName">empty or subject name</param>
        /// <returns>all of a student's marks if subjectName empty or subject does not exist in catalog, or the marsk for that subject, if subjectName is not empty and exists in the catalog</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Mark>))]
        [HttpGet("Marks report/{id}")]
        public IActionResult GetMarksReport([Range(1, int.MaxValue)] int id, [FromQuery] string subjectName = "")
        {
            try
            {
                var myStudent = studentsService.GetStudentByIdWithAddress(id, false);
            }
            catch (InvalidIdException e)
            {
                return BadRequest(e.Message);
            };

            return Ok(marksService.GetMarkByStudentId(id, subjectName));
        }

        /// <summary>
        /// Gets a report of all average marks per subject for a student
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>list of average marks and subject id</returns>
        [HttpGet("Average Marks Report/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentToGet))]
        public IActionResult GetAvgMarksById([Range(1, int.MaxValue)] int id)
        {
            var myStudent = studentsService.GetStudentByIdWithAddress(id, false);
            if (myStudent == null)
            {
                return NotFound("student not found");
            }
            return Ok(marksService.GetAvgMarkByStudentId(id));
        }

        // POST api/values
        /// <summary>
        /// gives a mark to a student on a subject
        /// </summary>
        /// <param name="markToCreate">mark given to the student by a teacher on a subject</param>
        /// <returns>mark</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MarkToGet))]

        public async Task<ActionResult<MarkToGet>> GiveMark([FromBody] MarkToCreate markToCreate)
        {
            try
            {
                var teacherAssignedToSubject = teacherAssignements.GetTeacherAssignement(markToCreate.TeacherId, markToCreate.SubjectId);
            }
            catch (InvalidIdException e)
            {
                return BadRequest(e.Message);
            }
            
            var studentEnrolledInSubject = studentEnrollmentService.GetStudentEnrollment(markToCreate.StudentId, markToCreate.SubjectId);
            if(studentEnrolledInSubject==null)
            {            
                return NotFound("student enrollment not found");
            }

            var newMark = await marksService.GiveMarkAsync(markToCreate);
            
            return CreatedAtAction(nameof(GiveMark), new { id = newMark.Id }, newMark.ToDto());
        }

    }
}


