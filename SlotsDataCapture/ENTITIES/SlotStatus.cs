namespace SlotsDataCapture.ENTITIES
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SlotStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SlotStatus()
        {
            this.Tbl_Slots = new HashSet<Slots>();
        }

        [Key]
        public int SlotStatusID { get; set; }
        [Required]
        [MinLength(5), MaxLength(30)]
        [Display(Name = "Working Status:")]
        public string Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Slots> Tbl_Slots { get; set; }
    }
}
