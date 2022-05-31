namespace PVK.Meetups.Web.Models
{
    public class FindMeetupsResultModel
    {
        public List<MeetupSearchResult> Results { get; set; } = new List<MeetupSearchResult>();
    }

    public class MeetupSearchResult
    {

        public int GroupId { get; set; }
        public int? EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public MeetupSearchResult(int groupId, int? eventId, string name, string description)
        {
            GroupId = groupId;
            EventId = eventId;
            Name = name;
            Description = description;
        }
        }
}
