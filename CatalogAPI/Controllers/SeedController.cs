using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogAPI.DTOs;
using DomainLayer.Data;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    public class SeedController : Controller
    {
        

        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status418ImATeapot)]

        public IActionResult Post()
        {
           
            return StatusCode(418);
        }
    }

        
    }


