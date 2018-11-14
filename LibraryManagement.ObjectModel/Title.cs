// <copyright file="Title.cs" company="Door Step Schools">
/// Copyright (c) All Right Reserved
/// </copyright>
/// <author>Ajay Gupta</author>
/// <summary>Class representing an Title entity</summary>

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.ObjectModel
{
    [Table("Title")]
    public class Title : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public decimal Price { get; set; }

        [ForeignKey("TitleCategory")]
        public int TitleCategory_Id { get; set; }

        public virtual TitleCategory TitleCategory { get; set; }

        [ForeignKey("Level")]
        public int Level_Id { get; set; }

        public virtual Level Level { get; set; }

        public bool IsActive { get; set; }
    }
}