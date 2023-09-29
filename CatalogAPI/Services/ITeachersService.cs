using System.ComponentModel.DataAnnotations;
using CatalogAPI.DTOs;
using DomainLayer.Models;

namespace CatalogAPI.Services
{
    public interface ITeachersService
    {
        Task<TeacherToGet> CreateTeacherAsync(TeacherToCreate teacherToCreate, int addressId);
        int GetTeacherIdByNameAndRank(TeacherToCreate teacherToCreate);
        void UpdateTeacherRank(Teacher teacherToCreate);
        Teacher GetTeacherById([Range(1, int.MaxValue)] int id);
        Task UpdateAddressIdAsync(int teacherId, int addressId);
        Task<Teacher> UpdateTeacherDataAsync(int id, TeacherDataToUpdate teacherData);
        List<TeacherRankToGet> GetAllTeachers();
        Task DeleteTeacher([Range(1, int.MaxValue)] int id);
    }
}