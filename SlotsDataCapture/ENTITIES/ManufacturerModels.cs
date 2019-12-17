namespace SlotsDataCapture.ENTITIES
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ManufacturerModels
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ManufacturerModels()
        {
            this.Tbl_Slots = new HashSet<Slots>();
        }


        [Key]
        public int ManufacturerModelID { get; set; }
        [Required]
        public int ManufacturerID { get; set; }
        [Required]
        [Display(Name = "Model Name:")]

        [MinLength(3), MaxLength(60)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Slots> Tbl_Slots { get; set; }
        public virtual Manufacturers Tbl_Manufacturers { get; set; }
    }
}
