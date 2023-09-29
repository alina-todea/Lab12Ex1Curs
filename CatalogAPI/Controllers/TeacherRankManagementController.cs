using System.ComponentModel.DataAnnotations;
using CatalogAPI.DTOs;
using CatalogAPI.Extensions;
using CatalogAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    public class TeacherRankManagementController : Controller
    {
        private ITeachersService teachersService;

        public TeacherRankManagementController(ITeachersService teachersService)
        {
            this.teachersService = teachersService;
        }

        // PUT api/values/5
        /// <summary>
        /// assigns a rank to a teacher
        /// </summary>
        /// <param name="id">id of the teacher</param>
        /// <returns>returns updated teacher</returns>
        // [HttpPut("{id}")]
        [HttpPut]

        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherToUpdate))]
        public IActionResult UpdateTeacherRank([FromBody][Range(1, int.MaxValue)] int id)
        {
            var teacherToUpdate = teachersService.GetTeacherById(id);

            if (teacherToUpdate == null)
            {
                return NotFound("teacher not found");
            }

            teachersService.UpdateTeacherRank(teacherToUpdate);

            return Ok(teacherToUpdate.ToDto());
        }
    }
}

