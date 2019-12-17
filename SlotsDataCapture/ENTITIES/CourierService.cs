namespace SlotsDataCapture.ENTITIES
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class CourierService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CourierService()
        {
            this.Tbl_Returns = new HashSet<Returns>();
        }

        [Key]
        public int CourierID { get; set; }
        [Required]
        [MinLength(2), MaxLength(50)]
        [Display(Name = "Courier Name:")]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Returns> Tbl_Returns { get; set; }
    }
}
