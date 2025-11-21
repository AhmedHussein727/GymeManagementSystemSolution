using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementBLL.ViewModels.MemberViewModels
{
    public class MemberToUpdateViewModel
    {
        public string Name { get; set; }
        public string? Photo { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email Must Be Between 5 And 100")]
        [EmailAddress(ErrorMessage = "Invalid Format")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Phone Is Required")]
        [Phone(ErrorMessage = "InValidate Phone Format")]
        [RegularExpression(@"^(011)||(012)||(015)\d{8}$", ErrorMessage = "Phone Number Must Be Valid Egyption Number ")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "BuildingNumber Is Required")]
        [Range(1, 9000, ErrorMessage = "Must Be From 1 to 9000")]
        public int BuildingNumber { get; set; }
        [Required(ErrorMessage = "Street Is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Must Be From 2 to 30 ")]
        
        public string Street { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "City Can Conatin Only Letters And Space")]
        [Required(ErrorMessage = "City Is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Must Be From 2 to 30 ")]

        public string City { get; set; }


    }
}
