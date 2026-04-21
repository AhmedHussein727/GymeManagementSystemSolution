

namespace GymeManagementBLL.ViewModels.PlanViewModels
{
    public class PlanViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DurationDays { get; set; }
        public Decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
