using System.ComponentModel.DataAnnotations;
using CatalogAPI.DTOs;
using DomainLayer.Models;

namespace CatalogAPI.Services
{
    public interface IMarksService
    {
        Task<int> DisableMarkRecordByIdAsync(int id, [RegularExpression("student|teacher|subject", ErrorMessage = "Invalid Rank")] string type);
        List<AverageMarkToGet> GetAvgMarkByStudentId(int id);
        List<MarkToDisplay> GetMarkByStudentId(int id, string subjectName);
        void SaveAverageMarkStudentSubject(MarkToCreate markToCreate);
        Task<Mark> GiveMarkAsync(MarkToCreate markToCreate);
        List<MarkToDisplay> GetAllMarks();
    }
}