using System.ComponentModel.DataAnnotations;
using CatalogAPI.DTOs;
using DomainLayer.Models;

namespace CatalogAPI.Services
{
    public interface IStudentsService
    {
        Task<StudentToGet> CreateStudentAsync(StudentToCreate studentToCreate, int addressId);
        int GetStudentIdByNameAndAge(StudentToCreate studentToCreate);
        Student GetStudentByIdWithAddress([Range(1, int.MaxValue)] int id, bool includeAddress);
        List<StudentDataToGet> GetAllStudents();
        Task UpdateAddressIdAsync(int studentId, int addressId);
        Task<Student> UpdateStudentDataAsync(int id, StudentDataToUpdate studentData);
        Task DeleteStudentAsync([Range(1, int.MaxValue)] int id);
    }
}