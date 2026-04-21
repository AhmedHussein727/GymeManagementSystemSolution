using GymeManagementDAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;


namespace GymeManagementDAL.Entities
{
    public abstract class GymeUser : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; }= null!;
        public string Phone { get; set; }=null!;
        public DateOnly DateOfBirth { get; set; }

        public Gender Gender { get; set; }
        public Address Address { get; set; } = null!;

    }
    [Owned]
    
    public class Address
    {
        public int BuildingNumber { get; set; } 
        public string Streat { get; set; } = null!;
        public string City { get; set; } = null!;

    }
}
