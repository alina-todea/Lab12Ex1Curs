using System.ComponentModel.DataAnnotations;
using CatalogAPI.DTOs;
using DomainLayer.Models;

namespace CatalogAPI.Services
{
    public interface ISubjetcsService
    {
        Subject GetSubjectById([Range(1, int.MaxValue)] int id);
        Subject CreateSubject(SubjectToCreate subjectToCreate);
        Subject GetSubjectByName(string subjectName);
        Task RemoveSubjectAsync(Subject subject);
    }
}