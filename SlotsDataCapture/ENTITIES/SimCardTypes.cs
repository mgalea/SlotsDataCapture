
namespace SlotsDataCapture.ENTITIES
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SimCardTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SimCardTypes()
        {
            this.Tbl_SimCardBatches = new HashSet<SimCardBatches>();
        }

        [Key]
        public int SIM_CardTypeID { get; set; }

        [Required]
        [MinLength(4), MaxLength(50)]
        [Display(Name = "Sim Card Type:")]
        public string Name { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SimCardBatches> Tbl_SimCardBatches { get; set; }
    }
}
