
namespace SlotsDataCapture.ENTITIES
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Reports
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reports()
        {
            this.Tbl_Returns = new HashSet<Returns>();
        }

        [Key]
        public int ReportID { get; set; }
        [Required]
        [Display(Name = "Created On:")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime CreatedOn { get; set; }

        [Required]
        [MinLength(2), MaxLength(50)]
        [Display(Name = "Created By:")]
        public string CreatedBy { get; set; }

        [Display(Name = "Comments:")]
        [DataType(DataType.MultilineText)]
        [MinLength(10), MaxLength(500)]
        public string Comments { get; set; }
        [Required]
        [MinLength(2), MaxLength(50)]
        [Display(Name = "Device Type")]
        public string DeviceType { get; set; }
        [Required]
        [MinLength(2), MaxLength(50)]
        [Display(Name = "Device Serial Number")]
        public string DeviceSerialNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Returns> Tbl_Returns { get; set; }
    }
}
