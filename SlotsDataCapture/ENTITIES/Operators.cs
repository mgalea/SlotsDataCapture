namespace SlotsDataCapture.ENTITIES
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Operators
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Operators()
        {
            this.Tbl_OperatorVenues = new HashSet<OperatorVenues>();
        }
        [Key]
        public int OperatorID { get; set; }
        [Required]
        public int CompanyID { get; set; }

        [Required]
        [Display(Name = "Created On:")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime CreatedOn { get; set; }

        [Display(Name = "Operator Activity!")]
        [Required]
        public bool IsActive { get; set; }
        public string IsActiveDisplay
        {
            get { return (bool)this.IsActive ? "Operator Is Active!" : "Operator Is Inactive!"; }

            set { IsActive = bool.Parse(IsActiveDisplay); }
        }

        [Required]
        [MinLength(4), MaxLength(70)]
        [Display(Name = "Name:")]
        public string Name { get; set; }

        [MinLength(4), MaxLength(60)]
        [Display(Name = "Address/1")]
        public string AddressOne { get; set; }


        [MinLength(4), MaxLength(60)]
        [Display(Name = "Address/2")]
        public string AddressTwo { get; set; }

        [Required]
        [MinLength(4), MaxLength(18)]
        [Display(Name = "Town/City:")]
        public string Town { get; set; }

        [MinLength(6), MaxLength(70)]
        [Display(Name = "Website:")]
        public string Website { get; set; }

        [Required]
        public int CountryID { get; set; }


    
        public virtual Company Tbl_Company { get; set; }
        public virtual Countries Tbl_Countries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OperatorVenues> Tbl_OperatorVenues { get; set; }
    }
}
