namespace PVK.Meetups.Web.Data
{
    /// <summary>
    /// Join table for the many-to-many relationship between <see cref="MeetupGroupEvent"/> and <see cref="PVKMeetupsUser"/>
    /// </summary>
    public class MeetupGroupEventAttendee
    {
        public int EventId { get; set; }
        public MeetupGroupEvent Event { get; set; }

        public string AttendeeId { get; set; }
        public PVKMeetupsUser Attendee { get; set; }
        public bool IsPrimaryEventHost { get; set; }

    }
}
