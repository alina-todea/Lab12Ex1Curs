using System;
namespace CatalogAPI.DTOs
{
    /// <summary>
    /// this represents a teacxher's data
    /// </summary>
    public class TeacherToGet
    {
        /// <summary>
        /// teacher id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// teacher name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// teacher address
        /// </summary>
        public string Rankvalue { get; set; }


        public AddressToGet ? AddressToGet { get; set; }






    }
}

