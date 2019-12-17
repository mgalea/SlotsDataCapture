using System.ComponentModel.DataAnnotations;

namespace SlotsDataCapture.ENTITIES
{
    using System;
    using System.Collections.Generic;
    
    public partial class SlotBoardVersion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SlotBoardVersion()
        {
            this.Tbl_Slots = new HashSet<Slots>();
        }
    
        [Key]
        public int SlotBoardVersionID { get; set; }

        [Required]
        [MinLength(2), MaxLength(50)]
        [Display(Name = "Board version")]
        public string Version { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Slots> Tbl_Slots { get; set; }
    }
}
