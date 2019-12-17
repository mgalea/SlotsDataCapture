namespace SlotsDataCapture.ENTITIES
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class BoardManufacturers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BoardManufacturers()
        {
            this.Tbl_BoardBatches = new HashSet<BoardBatches>();
        }

        [Key]
        public int SMIB_CardManufacturerID { get; set; }
        [Required]
        public int SMIBoardTypeID { get; set; }

        [MinLength(2), MaxLength(50)]
        [Display(Name = "Manufacturer Name:")]
        public string Name { get; set; }
        
          
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BoardBatches> Tbl_BoardBatches { get; set; }
        public virtual BoardTypes Tbl_BoardTypes { get; set; }
    }
}
