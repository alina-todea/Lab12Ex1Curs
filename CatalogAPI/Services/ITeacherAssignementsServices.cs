using System.ComponentModel.DataAnnotations;
using CatalogAPI.DTOs;
using DomainLayer.Models;

namespace CatalogAPI.Services
{
    public interface ITeacherAssignementsServices
    {
        Task<int> RemoveTeacherFromSubjectByIdAsync(int id, [RegularExpression("teacher|subject", ErrorMessage = "Invalid Rank")] string type);
        TeacherAssignement GetTeacherAssignement(int teacherId, int subjectId);
        void RemoveTeacherAssignement(int teacherId, int subjectiD);
        Task<TeacherAssignementsToGet> CreateTeacherAssignementAsync(TeacherAssignementsToCreate assignement);
    }
}