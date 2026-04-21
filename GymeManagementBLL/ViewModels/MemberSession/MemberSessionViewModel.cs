

namespace GymeManagementBLL.ViewModels.MemberSession
{
    public class MemberSessionViewModel
    {
        public int MemberSessionId { get; set; }

        public int MemberId { get; set; }
        public string MemberName { get; set; } = null!;

        public DateTime BookingDate { get; set; }

        public string BookingDateDisplay => BookingDate.ToString("g");

        public bool ISAttended { get; set; }
    }
}
