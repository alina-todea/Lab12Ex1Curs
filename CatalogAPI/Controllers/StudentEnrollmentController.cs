using Microsoft.AspNetCore.Mvc;
using CatalogAPI.DTOs;
using CatalogAPI.Extensions;
using System.ComponentModel.DataAnnotations;
using CatalogAPI.Services;
using CatalogAPI.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentEnrollmentController : Controller
    {
        private IStudentEnrollmentService studentEnrollmentService;
        private IStudentsService studentsService;
        private ISubjetcsService subjetcsService;

        public StudentEnrollmentController(ISubjetcsService subjetcsService,IStudentsService studentsService,IStudentEnrollmentService studentEnrollmentService)
        {
            this.studentEnrollmentService = studentEnrollmentService;
            this.studentsService = studentsService;
            this.subjetcsService = subjetcsService;
        }
      
        // POST api/values
        /// <summary>
        /// enrolls a student in a subject
        /// </summary>
        /// <param name="enrollment">student id an subject id</param>
        /// <returns>returns created enrollment</returns>
        [HttpPost("enroll student")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(StudentEnrollmentToGet))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        public async Task<ActionResult> EnrollStudentInSubjectAsync([FromBody] StudentEnrollmentToCreate enrollment)
        {
            try
            { 
            var existingStudent = studentsService.GetStudentByIdWithAddress(enrollment.StudentId, false);
            }
            catch (InvalidIdException e)
            {
                return BadRequest(e.Message);
            }

            try
            {
                var existingSubject = subjetcsService.GetSubjectById(enrollment.SubjectId);
            }
            catch (InvalidIdException e)
            {
                return BadRequest(e.Message);
            }

            try
            {
                var studentEnrollment = await studentEnrollmentService.EnrollStudentAsync(enrollment.StudentId, enrollment.SubjectId);
                return Created("", (studentEnrollment.ToDto()));
            }
            catch(DuplicateException e)
            {
                return Conflict(e.Message);
            }
        }

        /// <summary>
        /// inactivates the enrollment of a student in a subject. If student is to be deleted, the enrollment is disabled in the StudentManagement, through the deletion of the student
        /// </summary>
        /// <param name="id"> id of the student</param>
        /// <param name="subjectId">subject id</param>
        /// <returns>returns removed student's id</returns>
        [HttpDelete("InactivateEnrollment/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public  async Task<IActionResult> InactivateEnrollment([Range(1, int.MaxValue)] int id,  [FromQuery][Range(1, int.MaxValue)] int subjectId)

        {
            try
            {
                var subject = subjetcsService.GetSubjectById(subjectId);
            }
            catch (InvalidIdException e)
            {
                return BadRequest(e.Message);
            }

            try
            {
              var studentId=await studentEnrollmentService.InactivateStudentEnrollmentAsync(id, subjectId);
            }
            catch (InvalidIdException e)
            {
                return BadRequest(e.Message);
            }

            return Ok("this one is not working");
        }
    }
}

