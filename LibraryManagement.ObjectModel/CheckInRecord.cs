using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.ObjectModel
{
    [Table("CheckInRecord")]
    public class CheckInRecord
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Book")]
        public int Book_Id { get; set; }

        public virtual Book Book { get; set; }

        [ForeignKey("Fairy")]
        public int Fairy_Id { get; set; }

        public virtual Fairy Fairy { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime CheckInDate { get; set; }
    }
}
