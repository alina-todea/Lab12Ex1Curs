using DomainLayer.Models;
using CatalogAPI.Extensions;
using CatalogAPI.DTOs;

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CatalogAPI.Services;
using CatalogAPI.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SubjectsManagementController : Controller
    {
        private IStudentEnrollmentService studentEnrollmentService;
        private IMarksService marksService;
        private ITeacherAssignementsServices teacherAssignementsServices;
        private ISubjetcsService subjetcsService;

        public SubjectsManagementController(ISubjetcsService subjetcsService,ITeacherAssignementsServices teacherAssignementsServices, IMarksService marksService, IAddressesService addressesService, IStudentEnrollmentService studentEnrollmentService)
        {
            this.studentEnrollmentService = studentEnrollmentService;
            this.marksService = marksService;
            this.teacherAssignementsServices = teacherAssignementsServices;
            this.subjetcsService = subjetcsService;
        }


        // GET api/values/5
        /// <summary>
        /// gets a subject by id
        /// </summary>
        /// <param name="id">subject id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type=typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(Subject))]

        public IActionResult GetSubjectById([Range(1, int.MaxValue)] int id)
        {
            var subject = subjetcsService.GetSubjectById(id);

            if (subject==null)
            {
                return NotFound("subject not found");
            };
            return Ok(subject.ToDto());
        }

       /// <summary>
       /// creates a new subject
       /// </summary>
       /// <param name="subjectToCreate"> name of the subject to be created</param>
       /// <returns>returns created subject</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Subject))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Subject))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        public IActionResult CreateSubject(SubjectToCreate subjectToCreate)
        {
            var existingSubject = subjetcsService.GetSubjectByName(subjectToCreate.Name);
                
            if (existingSubject != null)
            {
                throw new DuplicateException("Subject already exists");
            }

            var subject = subjetcsService.CreateSubject(subjectToCreate);
               
            return Created("",subject.ToDto());
        }


        // PUT api/values/5
     
        /// <summary>
        /// deletes a subject, removes links to students, teachers and marks
        /// </summary>
        /// <param name="id">id of the subject to be removed</param>
        /// <returns>deleted subject's id</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<ActionResult<StudentToGet>> DeleteAsync([Range(1, int.MaxValue)] int id )
        {
            var subject = subjetcsService.GetSubjectById(id);

            if (subject == null)
            {
                return NotFound("subject not found");
            }
            try
            {
                await studentEnrollmentService.RemoveStudentFromSubjectByIdAsync(id, "subject");
            }
            catch (InvalidIdException e)
            {
                return BadRequest(e.Message);
            }
            try
            {
                await marksService.DisableMarkRecordByIdAsync(id, "subject");
            }
            catch (InvalidIdException e)
            {
                return BadRequest(e.Message);
            }
            try
            {
                await teacherAssignementsServices.RemoveTeacherFromSubjectByIdAsync(id, "subject");
            }
            catch (InvalidIdException e)
            {
                return BadRequest(e.Message);
            }
            try
            {
                await subjetcsService.RemoveSubjectAsync(subject);
            }
            catch (InvalidIdException e)
            {
                return BadRequest(e.Message);
            }
            return Ok(id);
        }
    }
}
