namespace SlotsDataCapture.ENTITIES
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SimCardBatches
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SimCardBatches()
        {
            this.Tbl_SimCards = new HashSet<SimCards>();
        }
        [Key]
        public int SimBatchID { get; set; }
        [Required]
        public int SIM_ManufacturerID { get; set; }
        [Required]
        public int SIM_CardTypeID { get; set; }

        [Required]
        [Display(Name = "Received On:")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime ReceivedOn { get; set; }



        [Display(Name = "Activated On:")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> ActivatedOn { get; set; }


        [Required]
        [MinLength(5), MaxLength(150)]
        [Display(Name = "Batch Number:")]
        public string BatchReference { get; set; }

        [Required]
        [MinLength(1), MaxLength(5)]
        [Display(Name = "Quantity:")]
        public string UnitsReceived { get; set; }

        [MinLength(5), MaxLength(100)]
        [Display(Name = "Item Location:")]
        public string CardLocation { get; set; }

        
        public virtual SimCardManufacturers Tbl_SimCardManufacturers { get; set; }
        public virtual SimCardTypes Tbl_SimCardTypes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SimCards> Tbl_SimCards { get; set; }
    }
}
