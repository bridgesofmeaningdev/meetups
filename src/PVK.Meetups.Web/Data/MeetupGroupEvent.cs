using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PVK.Meetups.Web.Data
{
    public class MeetupGroupEvent
    {
        public int Id { get; set; }

        [StringLength(256)]
        public string Name { get; set; } = string.Empty;

        [StringLength(2048)]
        public string Description { get; set; } = string.Empty;

        [StringLength(256)]
        public string LocationDescription { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public MeetupGroup OwningMeetupGroup { get; set; }

        [NotMapped]
        public MeetupGroupEventAttendee PrimaryHost { get; set; }

        public ICollection<MeetupGroupEventAttendee> Attendees { get; set; }


    }
}
