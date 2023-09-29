using CatalogAPI.Extensions;
using Microsoft.AspNetCore.Mvc;
using CatalogAPI.DTOs;
using System.ComponentModel.DataAnnotations;
using CatalogAPI.Services;
using CatalogAPI.Filters;
//using System.Data.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StudentsManagementController : Controller
    {
        private IAddressesService addressesService;
        private IStudentEnrollmentService studentEnrollmentService;
        private IMarksService marksService;
        private IStudentsService studentsService;

        public StudentsManagementController(IStudentsService studentsService,IMarksService marksService, IAddressesService addressesService,  IStudentEnrollmentService studentEnrollmentService)
        {
            this.addressesService = addressesService;
            this.studentEnrollmentService = studentEnrollmentService;
            this.marksService = marksService;
            this.studentsService = studentsService;
        }

        // GET: api/values
        /// <summary>
        /// preturns all students
        /// </summary>
        /// <returns> a list of all students</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetAll()
        {
            return Ok(studentsService.GetAllStudents());
        }

        // GET api/values/5
        /// <summary>
        /// eturns a student data by id
        /// </summary>
        /// <param name="id">id of the student</param>
        /// <param name="includeAddress">if true, returns also the address of the student</param>
        /// <returns>student data</returns>
        [HttpGet("Student Data/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentToGet))]

        public IActionResult GetByIdWithAddress([Range(1, int.MaxValue)] int id, [FromQuery] bool includeAddress = false)
        {
            try
            {
                return Ok(studentsService.GetStudentByIdWithAddress(id, includeAddress).ToDto());
            }

            catch (InvalidIdException e)
            {
                return BadRequest(e.Message);
            }
        }
        // POST api/values

        /// <summary>
        /// creates a new student and the student address
        /// </summary>
        /// <param name="studentToCreate">data of the student to be created</param>
        /// <returns>returns created student</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(StudentToGet))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type=typeof(string))]

        public async Task<ActionResult<StudentToGet>> CreateStudentAsync([FromBody] StudentToCreate studentToCreate)
        {
            var studentId = studentsService.GetStudentIdByNameAndAge(studentToCreate);

            if (studentId > 0)
            {
                return Conflict("Student already exists");
            }

            int addressId;
            try
            {
                addressId = await addressesService.IdentifyOrCreateAddressAsync(studentToCreate.AddressToCreate);
            }
            catch(InvalidAddressException e)
            {
                return NotFound(e.Message);
            }

            var student = await studentsService.CreateStudentAsync(studentToCreate, addressId);

            return CreatedAtAction(nameof(CreateStudentAsync), new { id = student.Id});
        }


        // PUT api/values/5

        /// <summary>
        /// updates a student's address. If the student has an address no one else owns, the address will be updated. if the current address is owned by someone else, a new address will be created. If the new address already exists, the student will receive the address id.
        /// </summary>
        /// <param name="id">id of the student</param>
        /// <param name="address">address of the student</param>
        /// <returns>returns updated student</returns>
        [HttpPut("UpdateStudentAddress/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentToCreate))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(StudentToCreate))]

        public async Task<ActionResult<StudentToGet>> UpdateStudentAsync([Range(1, int.MaxValue)] int id, [FromBody] AddressToCreate address)
        {
            var studentToUpdate = studentsService.GetStudentByIdWithAddress(id, false);

            if (studentToUpdate == null)
            {
                throw new InvalidIdException("Teacher not found");
            }

            var newAddressId = await addressesService.IdentifyOrUpdateAddressAsync(id, studentToUpdate.AddressId, address);

            await studentsService.UpdateAddressIdAsync(id, newAddressId);

            return CreatedAtAction(nameof(UpdateStudentAsync), new { id = studentToUpdate.Id }, studentToUpdate.ToDto());

        }
        /// <summary>
        /// updates a student's name and age
        /// </summary>
        /// <param name="id">student id</param>
        /// <param name="student">student data</param>
        /// <returns>student</returns>
        [HttpPut("UpdateStudentData/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentToCreate))]
        
        public async Task<ActionResult<StudentToGet>> UpdateStudentDataAsync([Range(1, int.MaxValue)] int id,[FromBody] StudentDataToUpdate student)
        {
            try
            {
                var studentToUpdate = await studentsService.UpdateStudentDataAsync(id, student);
                return CreatedAtAction(nameof(UpdateStudentDataAsync), new { id = studentToUpdate.Id});
            }
            catch(InvalidIdException e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/values/5
        /// <summary>
        /// deletes a student, sets enrollment to inactive, removes link to the marks and to the subjects, and deletes the student address if no other owner of the address exists.
        /// </summary>
        /// <param name="id">id of the student to be deleted</param>
        /// <param name="includeAddress">if true deletes also the address </param>
        /// <returns>returns deleted student's id</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<ActionResult<StudentToGet>> DeleteStudentAsync([Range(1, int.MaxValue)] int id,  [FromQuery] bool includeAddress)
        {
            try
            {
                var stud = studentsService.GetStudentByIdWithAddress(id, true);
                var otherOwners = addressesService.ExistOtherOwners(id, stud.AddressId);

                try
                {
                    var address = addressesService.GetAddressById(stud.AddressId);
                    if (address != null)
                    {
                        if (includeAddress && !otherOwners)
                        {
                            await addressesService.DeleteAddressAsync(stud.AddressId);
                        }
                    }
                }
                catch (InvalidIdException e)
                {
                    return BadRequest(e.Message);
                }
            }
            catch(InvalidIdException e)
            {
                return BadRequest(e.Message);
            }

            //var address = ctx.Addresses.FirstOrDefault((a => a.Id == stud.AddressId));
            try
            {
                await studentEnrollmentService.RemoveStudentFromSubjectByIdAsync(id, "student");

            }
            catch (InvalidIdException e)
            {
                return BadRequest(e.Message);
            }
            try
            {
                await marksService.DisableMarkRecordByIdAsync(id, "student");
            }
            catch (InvalidIdException e)
            {
                return BadRequest(e.Message);
            }

            try
            {
                await studentsService.DeleteStudentAsync(id);
            }
            catch(InvalidIdException e)
            {
                return BadRequest(e.Message);
            }

                return Ok(id);
        }
    }
}

