using System;
namespace CatalogAPI.DTOs
{
    /// <summary>
    /// this represents a teacher's assignement to a subject
    /// </summary>
    public class TeacherAssignementsToGet
	{
        /// <summary>
        /// assignement id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// teachwer id
        /// </summary>
        public int ? TeacherId { get; set; }
        /// <summary>
        /// subject id
        /// </summary>
        public int ? SubjectId { get; set; }
    }
}

