namespace SlotsDataCapture.ENTITIES
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Tracker
    {
        [Key]
        public int TrackerID { get; set; }
        [Required]
        public int SlotID { get; set; }

        [Required]
        [Display(Name = "Longitude value")]
        [Range(0, 9999999999.99)]
        public decimal Longitude { get; set; }
        [Required]
        [Display(Name = "Latitude value")]
        [Range(0, 9999999999.99)]
        public decimal Latitude { get; set; }


        [MinLength(5), MaxLength(25)]
        [Display(Name = "Distance in (km's")]
        public Nullable<double> DistanceInKilometers { get; set; }

        [Required]
        [Display(Name = "Map Detail")]
        public int Scale { get; set; }
        public virtual Slots Tbl_Slots { get; set; }
    }
}
