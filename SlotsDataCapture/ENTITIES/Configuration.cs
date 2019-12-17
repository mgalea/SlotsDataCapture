namespace SlotsDataCapture.ENTITIES
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Configuration
    {
        [Key]
        public int ConfigurationID { get; set; }
        [Required]
        public int SIMCardID { get; set; }

        [Display(Name = "Sim-Card Activated On:")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime SIM_ActivatedOn { get; set; }
        [Display(Name = "Sim-Card  Deactivated On:")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> SIM_DeactivatedOn { get; set; }

        [Required]
        [Display(Name = "Sim-Card Activity!")]
        public bool SIMCardNowAssigned { get; set; }
        public string SIMCardNowAssignedDisplay
        {
            get { return (bool)this.SIMCardNowAssigned ? "Sim-Card Assigned!" : "Sim-Card Not Assigned!"; }

            set { SIMCardNowAssigned = bool.Parse(SIMCardNowAssignedDisplay); }
        }
        [Required]
        public int SMIBID { get; set; }

        [Display(Name = "Board Activated On:")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime SMIB_ActivatedOn { get; set; }
        [Display(Name = "Board Deactivated On:")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> SMIB_DeactivatedOn { get; set; }

        [Required]
        [Display(Name = "Board Assigned!")]
        public bool SMIBoardNowAssigned { get; set; }
        public string SMIBoardNowAssignedDisplay
        {
            get { return (bool)this.SMIBoardNowAssigned ? "Board Assigned To Slot" : "Board Not Assigned!"; }

            set { SMIBoardNowAssigned = bool.Parse(SMIBoardNowAssignedDisplay); }
        }

        [Required]
        [Display(Name = "Configure Slot Machine:")]
        public int SlotID { get; set; }
    
        public virtual Boards Tbl_Boards { get; set; }
        public virtual SimCards Tbl_SimCards { get; set; }
        public virtual Slots Tbl_Slots { get; set; }
    }
}
