using System;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatalogAPI.DTOs
{

    /// <summary>
    /// this represents the teacher data: name and rank
    /// </summary>
    public class TeacherRankToGet
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string RankValue  { get; set; }

        public TeacherRankToGet(Teacher x)
        {
            Id = x.Id;
            Name = x.Name;
            RankValue = x.RankValue;
        }

    }
}

