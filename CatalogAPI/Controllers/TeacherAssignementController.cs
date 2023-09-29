using System.ComponentModel.DataAnnotations;
using CatalogAPI.DTOs;
using CatalogAPI.Filters;
using CatalogAPI.Services;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    public class TeacherAssignementController : Controller
    {
        private ITeacherAssignementsServices teacherAssignementsServices;
        private ISubjetcsService subjetcsService;
        private ITeachersService teachersService;

        public TeacherAssignementController(ITeachersService teachersService ,ISubjetcsService subjetcsService, ITeacherAssignementsServices teacherAssignementsServices)
        {
            this.teacherAssignementsServices = teacherAssignementsServices;
            this.subjetcsService = subjetcsService;
            this.teachersService = teachersService;
        }
        // POST api/values
        /// <summary>
        /// assigns a teacher to a subject
        /// </summary>
        /// <param name="assignement">id of the teacher and of the subject</param>
        /// <returns>returns created assignement</returns>
        [HttpPost("assign teacher to subject")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status201Created, Type=typeof(TeacherAssignement))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type=typeof(string))]
        public async Task<ActionResult> AssignTeacherToSubject([FromBody] TeacherAssignementsToCreate assignement)
        {
            /*if (assignement.TeacherId <= 0 || assignement.SubjectId <= 0)
            {
                return BadRequest();
            }*/

            var existingTeacher = teachersService.GetTeacherById(assignement.TeacherId);
            if (existingTeacher == null)
            {
                return NotFound("subject not found");
            }

            var existingSubject = subjetcsService.GetSubjectById(assignement.SubjectId);

            if( existingSubject == null)
            {
                return NotFound("subject not found");
            }

            var teacherAssignementToAdd = await teacherAssignementsServices.CreateTeacherAssignementAsync(assignement);
               
            return Created("", teacherAssignementToAdd);
        }


        // DELETE api/values/5
        /// <summary>
        /// removes a teacher from a subject
        /// </summary>
        /// <param name="id">id of the teacher</param>
        /// <param name="subjectId"> subject id</param>
        /// <returns>returnes removed teacher's id</returns>
        [HttpDelete("RemoveFromSubject/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(string))]
        public IActionResult Delete([Range(1, int.MaxValue)] int id, [FromQuery] int subjectId)
        {
            try
            {
                var subject = subjetcsService.GetSubjectById(subjectId);
            }
            catch(InvalidIdException e)
            {
                return BadRequest(e.Message);
            }

            teacherAssignementsServices.RemoveTeacherAssignement(id, subjectId);

            return Ok("{id}");
        }
    }
}

