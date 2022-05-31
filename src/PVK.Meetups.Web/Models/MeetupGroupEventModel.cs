using PVK.Meetups.Web.Data;
using System.ComponentModel.DataAnnotations;

namespace PVK.Meetups.Web.Models
{
    public class MeetupGroupEventModel
    {
        public int OwningMeetupGroupId { get; set; }

        public int? Id { get; set; } = null;

        [StringLength(256)]
        public string Name { get; set; } = string.Empty;

        [StringLength(2048)]
        public string Description { get; set; } = string.Empty;

        [StringLength(256)]
        public string LocationDescription { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        // for model mapping operations
        public MeetupGroupEventModel()
        {

        }

        public MeetupGroupEventModel(MeetupGroupEvent meetupEvent)
        {
            Id = meetupEvent.Id;
            OwningMeetupGroupId = meetupEvent.OwningMeetupGroupId;
            Name = meetupEvent.Name;
            Description = meetupEvent.Description;
            LocationDescription = meetupEvent.LocationDescription;
            StartDateTime = meetupEvent.StartDateTimeUtc.ToLocalTime();
            EndDateTime = meetupEvent.EndDateTimeUtc.ToLocalTime();
        }

        internal MeetupGroupEvent ToEvent()
        {
            return new MeetupGroupEvent
            {
                Id = this.Id ?? 0,
                OwningMeetupGroupId = this.OwningMeetupGroupId,
                Name = this.Name,
                Description = this.Description,
                LocationDescription = this.LocationDescription,
                StartDateTimeUtc = this.StartDateTime.ToUniversalTime(),
                EndDateTimeUtc = this.EndDateTime.ToUniversalTime()
            };
        }
    }
}
