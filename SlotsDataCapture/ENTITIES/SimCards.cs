namespace SlotsDataCapture.ENTITIES
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SimCards
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SimCards()
        {
            this.Tbl_Configuration = new HashSet<Configuration>();
        }

        [Key]
        public int SIMCardID { get; set; }
        [Required]
        public int SIMBatchID { get; set; }

        [Required]
        [MinLength(18), MaxLength(20)]
        [Display(Name = "Serial Number:")]
        public string SerialNumber { get; set; }
        [MinLength(7), MaxLength(10)]
        [Display(Name = "Mobile Number:")]
        public string MobileNumber { get; set; }
        [Required]
        [Display(Name = "Activated On:")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> ActivatedOn { get; set; }

        [Required]
        [Display(Name = "Availability")]
        public bool IsAvailable { get; set; }
        public string IsAvailableDisplay
        {
            get { return (bool)this.IsAvailable ? "Card Available!" : "Card In Use!"; }

            set { IsAvailable = bool.Parse(IsAvailableDisplay); }
        }
        [Required]
        [Display(Name = "Card Activity!")]
        public bool IsActive { get; set; }
        public string IsActiveDisplay
        {
            get { return (bool)this.IsActive ? "Card Assigned To Slot!" : "Card Inactive!"; }

            set { IsActive = bool.Parse(IsActiveDisplay); }
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configuration> Tbl_Configuration { get; set; }
        public virtual SimCardBatches Tbl_SimCardBatches { get; set; }
    }
}
