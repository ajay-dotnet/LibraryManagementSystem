using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.ObjectModel
{
    [Table("Book")]
    public class Book : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string Barcode { get; set; }

        [ForeignKey("Title")]
        public int Title_Id { get; set; }

        public virtual Title Title { get; set; }

        public DateTime PurchaseDate { get; set; }

        [ForeignKey("BookCondition")]
        public int BookCondition_Id { get; set; }

        public virtual BookCondition BookCondition { get; set; }

        public DateTime? DamLostDate { get; set; }

        public bool IssuedStatus { get; set; }
    }
}
