namespace SlotsDataCapture.ENTITIES
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SimCardManufacturers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SimCardManufacturers()
        {
            this.Tbl_SimCardBatches = new HashSet<SimCardBatches>();
        }

        [Key]
        public int SIM_CardManufacturerID { get; set; }

        [Required]
        [MinLength(2), MaxLength(50)]
        [Display(Name = "Card Manufacturer:")]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SimCardBatches> Tbl_SimCardBatches { get; set; }
    }
}
