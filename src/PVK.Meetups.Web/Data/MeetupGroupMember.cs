namespace PVK.Meetups.Web.Data
{
    /// <summary>
    /// Join table for the many-to-many relationship between <see cref="MeetupGroup"/> and <see cref="PVKMeetupsUser"/>
    /// </summary>
    public class MeetupGroupMember
    {
        public int GroupId { get; set; }
        public MeetupGroup Group { get; set; }

        public string MemberId { get; set; }
        public PVKMeetupsUser Member { get; set; }

        public bool IsGroupOrganizer { get; set; }
        public bool IsPrimaryGroupOrganizer { get; set; }

    }
}
