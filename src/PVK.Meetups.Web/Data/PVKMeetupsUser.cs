using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PVK.Meetups.Web.Data
{
    public class PVKMeetupsUser : IdentityUser
    {
        [StringLength(256)]
        public string? LocationDescription { get; set; }

        public ICollection<MeetupGroupMember> GroupMemberships { get; set; }

        public ICollection<MeetupGroupEventAttendee> GroupEventAttendance { get; set; }

    }
}
