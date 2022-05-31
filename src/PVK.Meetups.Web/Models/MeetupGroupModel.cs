using PVK.Meetups.Web.Data;
using System.ComponentModel.DataAnnotations;

namespace PVK.Meetups.Web.Models
{
    public class MeetupGroupModel
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(1024)]
        public string Description { get; set; } = string.Empty;

        [Display(Name="Location")]
        [Required]
        [StringLength(256)]
        public string LocationDescription { get; set; } = string.Empty;

        /// <summary>
        /// Only to support model binding
        /// </summary>
        public MeetupGroupModel()
        {

        }

        public MeetupGroupModel(MeetupGroup group)
        {
            Id = group.Id;
            Name = group.Name; 
            Description = group.Description;
            LocationDescription = group.LocationDescription;
        }

        public MeetupGroup ToGroup()
        {
            return new MeetupGroup
            {
                Id = this.Id ?? 0,
                Name = this.Name,
                Description = this.Description,
                LocationDescription = this.LocationDescription
            };
        }

    }
}
