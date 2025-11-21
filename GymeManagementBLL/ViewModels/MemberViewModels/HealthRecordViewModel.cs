using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementBLL.ViewModels.MemberViewModels
{
    public class HealthRecordViewModel
    {
        [Required(ErrorMessage ="Height Is Required ")]
        [Range(0.1,300,ErrorMessage ="Must be between 0.1 and 300 ")]
        public decimal Height { get; set; }
        [Required(ErrorMessage = "weight Is Required ")]
        [Range(0.1, 500, ErrorMessage = "Must be between 0.1 and 500 ")]
        public Decimal Weight { get; set; }
        [Required(ErrorMessage = "BloodType Is Required ")]
        [StringLength(3,ErrorMessage ="must be greater than 3")]
        public string BloodType { get; set; }
        public string? Note { get; set; }

    }
}
