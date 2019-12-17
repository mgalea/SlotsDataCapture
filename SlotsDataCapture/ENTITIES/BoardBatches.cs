namespace SlotsDataCapture.ENTITIES
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class BoardBatches
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BoardBatches()
        {
            this.Tbl_Boards = new HashSet<Boards>();
        }
        [Key]
        public int SMIBatchID { get; set; }
        [Required]
        public Nullable<int> SMIB_ManufacturerID { get; set; }
        [Required]
        public Nullable<int> SMIBoardTypeID { get; set; }

        [Required]
        [Display(Name = "Batch received on:")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime ReceivedOn { get; set; }
        [Required]
        [MinLength(6), MaxLength(10)]
        [Display(Name = "Batch Ref:")]
        public string BatchNumber { get; set; }

        [Required]
        [MinLength(1), MaxLength(4)]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        [Display(Name = "Quantity:")]
        public string UnitsReceived { get; set; }
        [Display(Name = "Storage location:")]
        [DataType(DataType.MultilineText)]
        public string BoardLocation { get; set; }
                                  
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Boards> Tbl_Boards { get; set; }
        public virtual BoardManufacturers Tbl_BoardManufacturers { get; set; }
        public virtual BoardTypes Tbl_BoardTypes { get; set; }
    }
}
