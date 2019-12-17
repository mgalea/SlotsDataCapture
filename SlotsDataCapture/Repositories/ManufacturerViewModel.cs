using SlotsDataCapture.ENTITIES;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SlotsDataCapture.Repositories
{
    public class ManufacturerViewModel
    {
        public ManufacturerViewModel()
        {
            this.Tbl_Configuration = new HashSet<Configuration>();
            this.Tbl_Reports = new HashSet<Reports>();
            this.Tbl_SlotHarnesses = new HashSet<SlotHarnesses>();
            this.Tbl_SlotImages = new HashSet<SlotImages>();
            this.Tbl_Tracker = new HashSet<Tracker>();
        }



        [Key]
        public int SlotID { get; set; }

        public IEnumerable<SelectListItem> SlotTypeID { get; set; }
        public IEnumerable<SelectListItem> SlotStatusID { get; set; }

        public IEnumerable<SelectListItem> SlotModelID { get; set; }

        public IEnumerable<SelectListItem> SlotNameID { get; set; }
        public IEnumerable<SelectListItem> FaultStatusID { get; set; }
        public bool IsOriginal { get; set; }
        public bool IsActive { get; set; }

        [Required]
        [Display(Name = "Device Commissioned On")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime DeviceCommissioned { get; set; }
        [Display(Name = "Device Deactivated On")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> DeviceDecommissioned { get; set; }

        [Required]
        [MinLength(5), MaxLength(25)]
        [Display(Name = "Serial N0")]
        public string SerialNumber { get; set; }
        [Required]
        [MinLength(4), MaxLength(25)]
        [Display(Name = "Asset N0")]
        public string AssetNumber { get; set; }


        [DataType(DataType.MultilineText)]
        [MinLength(5), MaxLength(200)]
        [Display(Name = "Physical Location")]
        public string DeviceLocation { get; set; }

        public string Selected_Name { get; set; }
        public IEnumerable<SelectListItem> Manufacturers { get; set; }

        public string Selected_Model { get; set; }
        public IEnumerable<SelectListItem> ManufacturerModels { get; set; }
        
        public virtual ICollection<Configuration> Tbl_Configuration { get; set; }
     //   public virtual FaultStatus Tbl_FaultStatus { get; set; }
        public virtual ManufacturerModels Tbl_ManufacturerModels { get; set; }
        public virtual Manufacturers Tbl_Manufacturers { get; set; }
        public virtual OperatorVenues Tbl_OperatorVenues { get; set; }
        public virtual ICollection<Reports> Tbl_Reports { get; set; }
       
        public virtual ICollection<SlotHarnesses> Tbl_SlotHarnesses { get; set; }
       
        public virtual ICollection<SlotImages> Tbl_SlotImages { get; set; }
        public virtual SlotModels Tbl_SlotModels { get; set; }
        public virtual SlotName Tbl_SlotName { get; set; }
      
        public virtual ICollection<Tracker> Tbl_Tracker { get; set; }
        public virtual SlotStatus Tbl_SlotStatus { get; set; }
        public virtual SlotTypes Tbl_SlotTypes { get; set; }
        
    }
}