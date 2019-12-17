namespace SlotsDataCapture.ENTITIES
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SlotName
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SlotName()
        {
            this.Tbl_Slots = new HashSet<Slots>();
        }

        [Key]
        public int SlotNameID { get; set; }
        [Required]
        [MinLength(2), MaxLength(30)]
        [Display(Name = "Slot Machine Name:")]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Slots> Tbl_Slots { get; set; }
    }
}
