using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementBLL.ViewModels.SessionViewModels
{
    public class UpdateSessionViewModel
    {
        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Must Be Between 10 and 500")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Start Date is required")]
        [Display(Name = "Start Date & Time ")]

        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [Display(Name = "End Date & Time ")]


        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Trainer is required")]

        [Display(Name = "Trainer")]

        public int TrainerId { get; set; }
    }
}
