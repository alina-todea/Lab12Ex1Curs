using DomainLayer.Data;
using CatalogAPI.Extensions;

using Microsoft.AspNetCore.Mvc;
using CatalogAPI.DTOs;
using System.ComponentModel.DataAnnotations;
using CatalogAPI.Services;
using CatalogAPI.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TeachersManagementController : Controller
    {
        private IAddressesService addressesService;
        private ITeachersService teachersService;
        private IMarksService marksService;
        private ITeacherAssignementsServices teacherAssignementsServices;


        public TeachersManagementController(ITeacherAssignementsServices teacherAssignementsServices,IAddressesService addressesService, ITeachersService teachersService, IMarksService marksService)
        {
            this.addressesService = addressesService;
            this.teachersService = teachersService;
            this.marksService = marksService;
            this.teacherAssignementsServices = teacherAssignementsServices;
        }

        // GET: api/values
        /// <summary>
        /// gets all teachers
        /// </summary>
        /// <returns>list of teachers</returns>
        [HttpGet]

        public IActionResult GetAll()
        {
            return Ok(teachersService.GetAllTeachers());
        }

        // GET api/values/5
        /// <summary>
        /// gets a teacher by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(TeacherToGet))]
        public IActionResult  GetByIdWithAddress([Range(1, int.MaxValue)] int id)
        {
            var myTeacher = teachersService.GetTeacherById(id);

            if (myTeacher == null)
            {
                return NotFound("teacher not found");
            }
            else
            {
                return Ok(myTeacher.ToDto());
            }
        }

        // POST api/values
        /// <summary>
        /// creates a new teacher
        /// </summary>
        /// <param name="teacherToCreate">teacher's data</param>
        /// <returns>returns created teacher</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status201Created, Type= typeof(TeacherToGet))]

        public async Task<ActionResult<TeacherToGet>> CreateTeacherAsync([FromBody] TeacherToCreate teacherToCreate)
        {
            //using var ctx = new UniversityDbContext();

            var teacherId = teachersService.GetTeacherIdByNameAndRank(teacherToCreate);

            if (teacherId >0)
            {
                return Conflict("Teacher already exists");
            }

            var addressId = await addressesService.IdentifyOrCreateAddressAsync(teacherToCreate.AddressToCreate);

            var teacher = await teachersService.CreateTeacherAsync(teacherToCreate, addressId);

            //await ctx.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateTeacherAsync), new { id = teacher.Id });
        }


        // PUT api/values/5
       /// <summary>
       /// updates a teacher's data. if teacher has an address that is not owned by anyone else, it will be updated. if the current address owned by someone else, a new address will be created. If new address already exists, the teacher will receive the address id.
       /// </summary>
       /// <param name="id">id of the teacher</param>
       /// <param name="address">teacher's new address</param>
       /// <returns>returns updated teacher</returns>
        [HttpPut("UpdateTeacherAddress/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(TeacherToUpdate))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(StudentToCreate))]
        public async Task<ActionResult<TeacherToGet>> UpdateTeacher([Range(1, int.MaxValue)] int id,[FromBody] AddressToCreate address)
        {
            var teacherToUpdate = teachersService.GetTeacherById(id);

            if (teacherToUpdate == null)
            {
                throw new InvalidIdException("Teacher not found");
            }
            try
            {
                var newAddressId = await addressesService.IdentifyOrUpdateAddressAsync(id, teacherToUpdate.AddressId, address);

                try
                {
                    await teachersService.UpdateAddressIdAsync(id, newAddressId);
                }
                catch (InvalidIdException e)
                {
                    return BadRequest(e.Message);
                }
            }
            catch (InvalidIdException e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtAction(nameof(UpdateTeacher), new { id = teacherToUpdate.Id }, teacherToUpdate.ToDto());
        }

        /// <summary>
        /// updates teacher's datav(name and rank). Observation: updating the rank only for corrections purpose, otherwise use TeacherRankManagement
        /// </summary>
        /// <param name="id">teacher id</param>
        /// <param name="teacher">teacher name and rank</param>
        /// <returns>teacher data</returns>
        [HttpPut("UpdateTeacherData/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherToUpdate))]

        public async Task<ActionResult<TeacherToGet>> UpdateTeacherData([Range(1, int.MaxValue)] int id, [FromBody] TeacherDataToUpdate teacher)
        {
            try
            {
                var teacherToUpdate = await teachersService.UpdateTeacherDataAsync(id,teacher);
                return CreatedAtAction(nameof(UpdateTeacherData), new { id = teacherToUpdate.Id });
            }
            catch (InvalidIdException e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// deletes a teacher, links to subject and to marks
        /// </summary>
        /// <param name="id">id of the teacher to be deteletd</param>
        /// <returns>returns deleted teacher's id</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]

        public async Task<ActionResult<TeacherToGet>> Delete([Range(1, int.MaxValue)] int id)
        {
            var teacher = teachersService.GetTeacherById(id);

            if (teacher == null)
            {
                throw new InvalidIdException("Teacher not found");
            }

            try
            {
                await teacherAssignementsServices.RemoveTeacherFromSubjectByIdAsync(id, "teacher");
            }
            catch (InvalidIdException e)
            {
                return BadRequest(e.Message);
            }

            try
            { 
            await marksService.DisableMarkRecordByIdAsync(id, "teacher");
            }
            catch (InvalidIdException e)
            {
                return BadRequest(e.Message);
            }

            await teachersService.DeleteTeacher(id);
            return Ok(id);
        }
    }
}

