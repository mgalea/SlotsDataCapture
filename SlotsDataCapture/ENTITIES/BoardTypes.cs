namespace SlotsDataCapture.ENTITIES
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class BoardTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BoardTypes()
        {
            this.Tbl_BoardBatches = new HashSet<BoardBatches>();
            this.Tbl_BoardManufacturers = new HashSet<BoardManufacturers>();
        }
        [Key]
        public int SMIBoardTypeID { get; set; }
        [Required]
        [MinLength(2), MaxLength(50)]
        [Display(Name = "Type Of Board:")]
        public string Name { get; set; }
                      
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BoardBatches> Tbl_BoardBatches { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BoardManufacturers> Tbl_BoardManufacturers { get; set; }
    }
}
