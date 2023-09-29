using System;
using DomainLayer.Models;

namespace CatalogAPI.DTOs
{
    /// <summary>
    /// this represents mark data
    /// </summary>
    public class MarkToDisplay
	{
		
        /// <summary>
        /// mark value range 1-10
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// student Id
        /// </summary>
        public int? StudentId { get; set; }
        /// <summary>
        /// date and time the mark was given
        /// </summary>
        public DateTime Date { get; set; } = DateTime.UtcNow;
        /*
        /// <summary>
        /// average mark per student per subject
        /// </summary>
        public double AvgStudentSubject { get; set; }*/

        public MarkToDisplay(Mark mark)
        {
            Value = mark.Value;
            StudentId = mark.StudentId;
            Date = mark.Date;

            // Following assigning value from the properties of Participant to properties of ParticipantDto 
        }
    }
}

