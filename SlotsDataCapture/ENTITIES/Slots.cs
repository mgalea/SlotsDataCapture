namespace SlotsDataCapture.ENTITIES
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Slots
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Slots()
        {
            this.Tbl_Configuration = new HashSet<Configuration>();
            this.Tbl_SlotHarnesses = new HashSet<SlotHarnesses>();
            this.Tbl_SlotImages = new HashSet<SlotImages>();
            this.Tbl_Tracker = new HashSet<Tracker>();
        }

        [Key]
        public int SlotID { get; set; }
        [Required]
        public int VenueID { get; set; }
        [Required]
        public int SlotTypeID { get; set; }
        [Required]
        public int SlotStatusID { get; set; }
        [Required]
        public int SlotModelID { get; set; }
        [Required]
        public int SlotNameID { get; set; }
        [Required]
        public int ManufacturerID { get; set; }
        [Required]
        public int ManufacturerModelID { get; set; }


        [Display(Name = "Original Motherboard")]
        [Required]
        public bool IsOriginal { get; set; }
        public string IsOriginalDisplay
        {
            get { return (bool)this.IsOriginal ? "Original Motherboard" : "replacement unit"; }

            set { IsOriginal = bool.Parse(IsOriginalDisplay); }
        }

        [Display(Name = "In Service")]
        [Required]
        public bool IsActive { get; set; }
        public string IsActiveDisplay
        {
            get { return (bool)this.IsActive ? "Operational" : "Out of Service"; }

            set { IsActive = bool.Parse(IsActiveDisplay); }
        }




        [Display(Name = "Is Available")]
        [Required]
        public bool IsAvailable { get; set; }
        public string IsAvailableDisplay
        {
            get { return (bool)this.IsAvailable ? "In Use!" : "Not In Use!"; }

            set { IsAvailable = bool.Parse(IsAvailableDisplay); }
        }

        [Required]
        [Display(Name = "Configured On")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime DeviceCommissioned { get; set; }

        [Display(Name = "Deactivated On")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> DeviceDecommissioned { get; set; }

        [Required]
        [MinLength(5), MaxLength(25)]
        [Display(Name = "Serial Number:")]
        public string SerialNumber { get; set; }
        [Required]
        [MinLength(4), MaxLength(25)]
        [Display(Name = "Asset Number:")]
        public string AssetNumber { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Machine Located")]
        public string DeviceLocation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configuration> Tbl_Configuration { get; set; }
        public virtual ManufacturerModels Tbl_ManufacturerModels { get; set; }
        public virtual Manufacturers Tbl_Manufacturers { get; set; }
        public virtual OperatorVenues Tbl_OperatorVenues { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SlotHarnesses> Tbl_SlotHarnesses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SlotImages> Tbl_SlotImages { get; set; }
        public virtual SlotModels Tbl_SlotModels { get; set; }
        public virtual SlotName Tbl_SlotName { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tracker> Tbl_Tracker { get; set; }
        public virtual SlotStatus Tbl_SlotStatus { get; set; }
        public virtual SlotTypes Tbl_SlotTypes { get; set; }
    }
}
