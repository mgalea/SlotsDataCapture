using System.ComponentModel.DataAnnotations;

namespace SlotsDataCapture.ENTITIES
{

    public partial class Returns
    {
        [Key]
        public int ReturnID { get; set; }
        [Required]
        public int ReportID { get; set; }

        [Required]
        public int CourierID { get; set; }
        [Required]
        [Display(Name = "Returned On:")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime ReturnDate { get; set; }

        [Required]
        [MinLength(5), MaxLength(60)]
        [Display(Name = "Created By:")]
        public string ReturnedBy { get; set; }
        [Required]
        [MinLength(5), MaxLength(100)]
        [Display(Name = "Tracking N0:")]
        public string TrackingNumber { get; set; }

        [Display(Name = "Device Despatched?:")]
        public bool Dispatched { get; set; }
        public string IsDispatched
        {
            get { return (bool)this.Dispatched ? "Item Returned!" : "Item Not Returned!"; }

            set { Dispatched = bool.Parse(IsDispatched); }
        }
               
    
        public virtual CourierService Tbl_CourierService { get; set; }
        public virtual Reports Tbl_Reports { get; set; }
    }
}
