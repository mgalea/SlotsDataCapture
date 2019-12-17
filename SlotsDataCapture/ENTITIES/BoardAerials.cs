namespace SlotsDataCapture.ENTITIES
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class BoardAerials
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BoardAerials()
        {
            this.Tbl_Boards = new HashSet<Boards>();
        }

        [Key]
        public int SMIBAerialID { get; set; }

        [Required]
        [MinLength(5), MaxLength(25)]
        [Display(Name = "Aerial:")]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Boards> Tbl_Boards { get; set; }
    }
}
