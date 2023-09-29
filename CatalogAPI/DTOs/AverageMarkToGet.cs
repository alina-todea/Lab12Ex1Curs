using System;
using DomainLayer.Models;

namespace CatalogAPI.DTOs
{
    /// <summary>
    /// this represents the average mark
    /// </summary>
    public class AverageMarkToGet
	{

        /// <summary>
        /// average mark per student per subject
        /// </summary>
        public double AvgMark { get; set; }
        /// <summary>
        /// subject id
        /// </summary>
        public int ? SubjectId { get; set; }
        public SubjectToGet ? SubjectToGet { get; set; }


        public AverageMarkToGet(StudentEnrollment x)
            {
            

            AvgMark = x.AvgMark;
            SubjectId = x.SubjectId;
            //SubjectToGet = x.SubjectToGet;
              
            }
        }
    }



