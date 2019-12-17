using System.ComponentModel.DataAnnotations;

namespace SlotsDataCapture.ENTITIES
{

    public partial class SlotHarnesses
    {
        [Key]
        public int SlotHarnessID { get; set; }
        public int SlotID { get; set; }
        [Required]
        [MinLength(3), MaxLength(50)]
        [Display(Name = "Harness Name:")]

        public string Name { get; set; }
        [MinLength(1)]
        [Display(Name = "Cable Length (cms)")]
        public string CableLength { get; set; }

        [MinLength(2), MaxLength(50)]
        [Display(Name = "COM Jumper N0:")]
        public string COM_Jumper_Number { get; set; }

        [MinLength(2), MaxLength(50)]
        [Display(Name = "COM jumper Type:")]
        public string COM_Jumper_Type { get; set; }

        [MinLength(2), MaxLength(50)]
        [Display(Name = "Power Jumper N0:")]
        public string COM_Power_Jumper_Number { get; set; }

        [MinLength(2), MaxLength(50)]
        [Display(Name = "BS [Data-Connect]")]
        public string BS_DataConnect { get; set; }


        [MinLength(2), MaxLength(50)]
        [Display(Name = "BS [Power Connect]")]
        public string BS_PowerConnect { get; set; }


        [MinLength(2), MaxLength(50)]
        [Display(Name = "BS [Data Connect Type]")]
        public string BS_DataConnectType { get; set; }

        [MinLength(2), MaxLength(50)]
        [Display(Name = "BS [Data Connect Pin-Out]")]
        public string BS_DataConnect_PinOut { get; set; }


        [MinLength(2), MaxLength(50)]
        [Display(Name = "BS [Data Connect Power Pin-Out]")]
        public string BS_PowerConnect_PinOut { get; set; }


        // add here

        [MinLength(2), MaxLength(50)]
        [Display(Name = "DS [Data Connect:]")]
        public string DS_DataConnect { get; set; }


        [MinLength(2), MaxLength(50)]
        [Display(Name = "DS [Power Connect:]")]
        public string DS_PowerConnect { get; set; }


        [MinLength(2), MaxLength(50)]
        [Display(Name = "DS [Data Connect Type:]")]
        public string DS_DataConnectType { get; set; }

        [MinLength(2), MaxLength(50)]
        [Display(Name = "DS [Data Connect Pin-Out]")]
        public string DS_DataConnect_PinOut { get; set; }


        [MinLength(2), MaxLength(50)]
        [Display(Name = "DS [Data Connect Power Pin-Out]")]
        public string DS_PowerConnect_PinOut { get; set; }

        public virtual Slots Tbl_Slots { get; set; }
    }
}
