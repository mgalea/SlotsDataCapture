namespace SlotsDataCapture.ENTITIES
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class OperatorTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OperatorTypes()
        {
            this.Tbl_OperatorVenues = new HashSet<OperatorVenues>();
        }

        [Key]
        public int OperatorTypeID { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)]
        [Display(Name = "Operator Type:")]
        public string Name { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OperatorVenues> Tbl_OperatorVenues { get; set; }
    }
}
