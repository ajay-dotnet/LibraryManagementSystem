/// <copyright file="TitleCategory.cs" company="Door Step Schools">
/// Copyright (c) All Right Reserved
/// </copyright>
/// <author>Ajay Gupta</author>
/// <summary>Class representing an TitleCategory entity</summary>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.ObjectModel
{
    [Table("TitleCategory")]
    public class TitleCategory : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}