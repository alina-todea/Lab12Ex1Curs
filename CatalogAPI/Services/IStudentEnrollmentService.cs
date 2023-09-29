using System.ComponentModel.DataAnnotations;
using DomainLayer.Models;

namespace CatalogAPI.Services
{
    public interface IStudentEnrollmentService
    {
        Task<int> RemoveStudentFromSubjectByIdAsync(int id, [RegularExpression("student|subject", ErrorMessage = "Invalid Rank")] string type);
        StudentEnrollment GetStudentEnrollment(int studentId, int subjectId);
        Task<StudentEnrollment> EnrollStudentAsync(int studentId, int subjectId);
        Task<int> InactivateStudentEnrollmentAsync(int studentId, int subjectId);
    }
}