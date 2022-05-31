namespace PVK.Meetups.Web.Models
{
    public class MeetupGroupEventListModel
    {
        public int OwningMeetupGroupId { get; set; }
        public List<MeetupGroupEventModel> Events { get; set; } = new List<MeetupGroupEventModel>();
    }
}
