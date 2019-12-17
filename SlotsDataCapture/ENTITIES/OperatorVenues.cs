namespace SlotsDataCapture.ENTITIES
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class OperatorVenues
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OperatorVenues()
        {
            this.Tbl_BoardImages = new HashSet<BoardImages>();
            this.Tbl_Contacts = new HashSet<Contacts>();
            this.Tbl_SlotImages = new HashSet<SlotImages>();
            this.Tbl_Slots = new HashSet<Slots>();
        }
        [Key]
        public int VenueID { get; set; }
        [Required]
        public int OperatorID { get; set; }
        [Required]
        public int OperatorTypeID { get; set; }

        [Display(Name = "Establishment Activity!")]
        [Required]
        public bool IsActive { get; set; }
        public string IsActiveDisplay
        {
            get { return (bool)this.IsActive ? "Establishment Active!" : "Establishment Inactive!"; }

            set { IsActive = bool.Parse(IsActiveDisplay); }
        }

        [Required]
        [MinLength(5), MaxLength(25)]
        [Display(Name = "Permit:")]
        public string Permit { get; set; }

        [Required]
        [MinLength(5), MaxLength(50)]
        [Display(Name = "Manager Name:")]
        public string Manager { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        [Display(Name = "Premises Name:")]
        public string Name { get; set; }

        [Required]
        [MinLength(6), MaxLength(18)]
        [Display(Name = "Phone:")]
        public string Phone { get; set; }

        [Display(Name = "Email:")]
        [MinLength(5), MaxLength(120)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }


        [MinLength(4), MaxLength(60)]
        [Display(Name = "Address/1")]
        public string AddressOne { get; set; }


        [MinLength(4), MaxLength(100)]
        [Display(Name = "Address/2")]
        public string AddressTwo { get; set; }


        [MinLength(4), MaxLength(8)]
        [Display(Name = "Postcode:")]
        public string PostCode { get; set; }

        [Required]
        [MinLength(4), MaxLength(18)]
        [Display(Name = "Town/City:")]
        public string Town { get; set; }

        [Required]
        [Display(Name = "Country Name:")]
        public int CountryID { get; set; }


        
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BoardImages> Tbl_BoardImages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contacts> Tbl_Contacts { get; set; }
        public virtual Countries Tbl_Countries { get; set; }
        public virtual Operators Tbl_Operators { get; set; }
        public virtual OperatorTypes Tbl_OperatorTypes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SlotImages> Tbl_SlotImages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Slots> Tbl_Slots { get; set; }
    }
}
