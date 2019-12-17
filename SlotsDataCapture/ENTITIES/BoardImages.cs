namespace SlotsDataCapture.ENTITIES
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    public partial class BoardImages
    {

        [Key]
        public int ImageID { get; set; }
        [Required]
        public int SMIBID { get; set; }
        [Required]
        public int ImageTypeID { get; set; }
        [Required]
        public int VenueID { get; set; }

        [Display(Name = "Created On:")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> CreatedOn { get; set; }


        [Required]
        public string ImagePath { get; set; }


        [AllowHtml]
        [DisplayName("File Upload:")]
        [Required(ErrorMessage = "Please select file.")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public HttpPostedFileBase ImageFile { get; set; }


        [MinLength(5), MaxLength(600)]
        [Display(Name = "Description:")]
        public string Description { get; set; }
    
        public virtual ImageTypes Tbl_ImageTypes { get; set; }
        public virtual OperatorVenues Tbl_OperatorVenues { get; set; }
        public virtual Boards Tbl_Boards { get; set; }
    }
}
