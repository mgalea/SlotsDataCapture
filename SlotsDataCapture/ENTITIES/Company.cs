namespace SlotsDataCapture.ENTITIES
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Company
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Company()
        {
            this.Tbl_Operators = new HashSet<Operators>();
        }

        [Key]
        public int CompanyID { get; set; }

        [Required]
        [MinLength(2), MaxLength(70)]
        [Display(Name = "Created By:")]
        public string CreatedBy { get; set; }

        [Required]
        [Display(Name = "Created On:")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime CreatedOn { get; set; }

        [Display(Name = "Company Activity:")]
        public bool IsActive { get; set; }
        public string IsActiveDisplay
        {
            get { return (bool)this.IsActive ? "Company Active!" : "Company Inactive!"; }

            set { IsActive = bool.Parse(IsActiveDisplay); }
        }

        [Required]
        [MinLength(2), MaxLength(70)]
        [Display(Name = "Name:")]
        public string Name { get; set; }


        [MinLength(5), MaxLength(10)]
        [Display(Name = "Corp-Reg N0")]
        public string Number { get; set; }

        [MinLength(6), MaxLength(18)]
        [Display(Name = "Phone:")]
        public string Phone { get; set; }

        [Display(Name = "Email:")]
        [MinLength(5), MaxLength(120)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }


        [MinLength(6), MaxLength(120)]
        [Display(Name = "Company:")]
        public string Website { get; set; }


        [MinLength(4), MaxLength(60)]
        [Display(Name = "Address/1")]
        public string AddressOne { get; set; }


        [MinLength(4), MaxLength(60)]
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
        public int CountryID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Operators> Tbl_Operators { get; set; }
        public virtual Countries Tbl_Countries { get; set; }
    }
}
