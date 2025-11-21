using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementBLL.ViewModels.PlanViewModels
{
    public class UpdatePlanViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Plan Name Is Required")]
        [StringLength(50,ErrorMessage ="PlanName Must Be Less than 50 chars")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Plan Discription Is Required")]
        [StringLength(200,MinimumLength =5, ErrorMessage = "Discription Must Be between 5 and 200")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "Duration Days Is Required")]
        [Range(1,365,ErrorMessage ="Must Be Between 1 And 365")]
        public int DurationDays { get; set; }
        [Required(ErrorMessage = "Price Is Required")]
        [Range(0.1,1000,ErrorMessage ="Must Be Between 0.1 And 1000")]

        public decimal Price { get; set; }
    }
}
