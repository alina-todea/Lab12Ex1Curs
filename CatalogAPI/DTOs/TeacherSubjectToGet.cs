using System;
namespace CatalogAPI.DTOs
{
    /// <summary>
    /// this represents teacher data: name
    /// </summary>
    public class TeacherSubjectToGet
	{
        public int Id { get; set; }

        public string Name { get; set; }

        //public SubjectToGet SubjectToGet { get; set; }
    }
}

