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
    [Table("vw_titleDetails")]
    public class TitleList : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public decimal Price { get; set; }

        public int TitleCategory_Id { get; set; }

        public string TitleCategory { get; set; }

        public int Level_Id { get; set; }

        public string Level { get; set; }

        public int BookCount { get; set; }
    }
}