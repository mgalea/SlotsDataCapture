
namespace SlotsDataCapture.ENTITIES
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SlotTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SlotTypes()
        {
            this.Tbl_Slots = new HashSet<Slots>();
        }

        [Key]
        public int SlotTypeID { get; set; }

        [Required]
        [MinLength(5), MaxLength(30)]
        [Display(Name = "Type Of Slot:")]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Slots> Tbl_Slots { get; set; }
    }
}
