namespace SlotsDataCapture.ENTITIES
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ImageTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ImageTypes()
        {
            this.Tbl_BoardImages = new HashSet<BoardImages>();
            this.Tbl_SlotImages = new HashSet<SlotImages>();
        }

        [Key]
        public int ImageTypeID { get; set; }

        [Required]
        [Display(Name = "Image Type:")]
        [MinLength(4), MaxLength(20)]
        public string Name { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BoardImages> Tbl_BoardImages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SlotImages> Tbl_SlotImages { get; set; }
    }
}
