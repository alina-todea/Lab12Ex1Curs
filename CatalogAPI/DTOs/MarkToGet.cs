using System;
using DomainLayer.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CatalogAPI.DTOs
{
    /// <summary>
    /// this represents a student's mark
    /// </summary>
	public class MarkToGet
	{
        /// <summary>
        /// mark id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// mark value 1-10
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// student Id
        /// </summary>
        public int ? StudentId { get; set; }
        /// <summary>
        /// date and time the mark was given
        /// </summary>
        public DateTime Date { get; set; } = DateTime.UtcNow;
        /*
        /// <summary>
        /// average mark per student per subject
        /// </summary>
        public double AvgStudentSubject { get; set; }*/

        /*public MarkToGet(Mark mark)
        {
            Value = mark.Value;
            StudentId = mark.StudentId;
            Date = mark.Date;

            // Following assigning value from the properties of Participant to properties of ParticipantDto 
        }
        */

    }
}

