namespace SlotsDataCapture.ENTITIES
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Boards
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Boards()
        {
            this.Tbl_BoardImages = new HashSet<BoardImages>();
            this.Tbl_Configuration = new HashSet<Configuration>();
        }

        [Key]
        public int SMIBID { get; set; }
        [Required]
        public int SMIBatchID { get; set; }
        [Required]
        public int SMIBAerialID { get; set; }

        [Required]
        [MinLength(6), MaxLength(10)]
        [Display(Name = "Serial Number:")]
        public string SerialNumber { get; set; }

        [Required]
        [Display(Name = "Board Availability!")]
        public bool IsAvailable { get; set; }
        public string IsAvailableDisplay
        {
            get { return (bool)this.IsAvailable ? "Is available" : "Configured for use"; }

            set { IsAvailable = bool.Parse(IsAvailableDisplay); }
        }
        [Required]
        [Display(Name = "Board Activity!")]
        public bool IsActive { get; set; }
        public string IsActiveDisplay
        {
            get { return (bool)this.IsActive ? "In service" : "Not in service"; }

            set { IsActive = bool.Parse(IsActiveDisplay); }
        }
    
        public virtual BoardAerials Tbl_BoardAerials { get; set; }
        public virtual BoardBatches Tbl_BoardBatches { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BoardImages> Tbl_BoardImages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configuration> Tbl_Configuration { get; set; }
    }
}
