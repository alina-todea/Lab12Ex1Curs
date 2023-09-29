using System;
using System.Net;
using CatalogAPI.DTOs;
using DomainLayer.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CatalogAPI.Extensions
{
	public static class MarkExtensions
	{
        /// <summary>
        /// this returns a mark
        /// </summary>
        /// <param name="mark">mark's data</param>
        /// <returns>returns mark data</returns>
        public static MarkToGet ToDto(this Mark mark)
        {
            if (mark == null)
            {
                return null;
            }
           
            return new MarkToGet
            {
            Value = mark.Value,
            StudentId = mark.StudentId,
            Date = mark.Date
        };
        }

    }
}

