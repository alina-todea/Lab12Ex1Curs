using System.ComponentModel.DataAnnotations;
using CatalogAPI.DTOs;
using CatalogAPI.Extensions;
using CatalogAPI.Services;
using Microsoft.AspNetCore.Mvc;
using CatalogAPI.Filters;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AddressManagementController : Controller
    {
        private IAddressesService addressesService;
        private IStudentsService studentsService;

        public AddressManagementController(IStudentsService studentsService,  IAddressesService addressesService)
        {
            this.addressesService = addressesService;
            this.studentsService = studentsService;
        }

        // GET: api/values
        /// <summary>
        /// returns all addresses
        /// </summary>
        /// <returns>addresses list</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(addressesService.GetAllAddresses());
        }

        // GET api/values/5
        /// <summary>
        /// returns an address by id
        /// </summary>
        /// <param name="id">id of the address</param>
        /// <returns>address</returns>
        [HttpGet("{id}")]
        public IActionResult GetById([Range(1, int.MaxValue)]int id)
        {
            try
            {
                return Ok(addressesService.GetAddressById(id).ToDto());
            }
            catch (InvalidIdException e)
            {
                return BadRequest(e.Message);
            }
        }
       
         // PUT api/values/5
         /// <summary>
         /// updates an address
         /// </summary>
         /// <param name="id">id of the address to update</param>
         /// <param name="address">new address data</param>
         /// <returns>ne address data</returns>
         [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentToCreate))]
        public async Task<ActionResult<int>> UpdateAddress([Range(1, int.MaxValue)] int id, [FromBody] AddressToCreate address)

        {
            var addressId = await addressesService.UpdateAddressAsync(id, address);

            return CreatedAtAction(nameof(UpdateAddress), new { id = addressId });
        }
    }
}


