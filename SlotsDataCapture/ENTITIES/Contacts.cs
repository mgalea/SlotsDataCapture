using System.ComponentModel.DataAnnotations;

namespace SlotsDataCapture.ENTITIES
{

    public partial class Contacts
    {

        [Key]
        public int ContactID { get; set; }
        [Required]
        public int VenueID { get; set; }
        [Required]
        public int TitleID { get; set; }
        [Required]
        [Display(Name = "Created On:")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime CreatedOn { get; set; }

        [Display(Name = "Is Contact Active!")]
        public bool IsActive { get; set; }
        public string IsActiveDisplay
        {
            get { return (bool)this.IsActive ? "Active Contact!" : "Inactive Contact!"; }

            set { IsActive = bool.Parse(IsActiveDisplay); }
        }

        [Required]
        [MinLength(2), MaxLength(30)]
        [Display(Name = "First Name:")]
        public string FName { get; set; }

        [Required]
        [MinLength(2), MaxLength(50)]
        [Display(Name = "Last Name:")]
        public string LName { get; set; }

        [Required]
        [MinLength(2), MaxLength(70)]
        [Display(Name = "Company:")]
        public string Company { get; set; }

        [Required]
        [MinLength(2), MaxLength(70)]
        [Display(Name = "Position:")]
        public string Position { get; set; }


        [Display(Name = "Email:")]
        [MinLength(5), MaxLength(120)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }


        [Required]
        [MinLength(6), MaxLength(8)]
        [Display(Name = "Phone:")]
        public string Phone { get; set; }


        [MinLength(6), MaxLength(12)]
        [Display(Name = "Mobile:")]
        public string Mobile { get; set; }


        [Required]
        [Display(Name = "Country:")]
        public int CountryID { get; set; }
                           
    
        public virtual Countries Tbl_Countries { get; set; }
        public virtual OperatorVenues Tbl_OperatorVenues { get; set; }
        public virtual Titles Tbl_Titles { get; set; }
    }
}
