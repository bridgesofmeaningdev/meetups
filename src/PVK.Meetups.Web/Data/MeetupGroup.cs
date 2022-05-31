using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PVK.Meetups.Web.Data
{
    public class MeetupGroup
    {
        public int Id { get; set; }

        [StringLength(256)]
        public string Name { get; set; } = string.Empty;

        [StringLength(1024)]
        public string Description { get; set; } = string.Empty;

        [StringLength(256)]
        public string LocationDescription { get; set; } = string.Empty;

        public bool IsPrivate { get; set; } = false;
        
        public ICollection<MeetupGroupMember>? Members { get; set; }

        public ICollection<MeetupGroupEvent>? Events { get; set; }


    }
}
