namespace SlotsDataCapture.ENTITIES
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class SlotImages
    {
        [Key]
        public int ImageID { get; set; }
        [Required]
        public int SlotID { get; set; }
        [Required]
        public int VenueID { get; set; }
        [Required]
        public int ImageTypeID { get; set; }


        [DisplayName("File Upload:")]
        [Required(ErrorMessage = "Please select your file.")]
        public string ImagePath { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Created On:")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> CreatedOn { get; set; }

        [MinLength(5), MaxLength(600)]
        [Display(Name = "Description:")]
        public string Description { get; set; }
        public virtual ImageTypes Tbl_ImageTypes { get; set; }
        public virtual OperatorVenues Tbl_OperatorVenues { get; set; }
        public virtual Slots Tbl_Slots { get; set; }
    }
}
