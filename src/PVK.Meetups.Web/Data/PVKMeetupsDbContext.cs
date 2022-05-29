using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PVK.Meetups.Web.Data
{
    public partial class PVKMeetupsDbContext : IdentityDbContext<PVKMeetupsUser>
    {
        public PVKMeetupsDbContext(DbContextOptions<PVKMeetupsDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<MeetupGroup>().HasMany(g => g.Events).WithOne(e => e.OwningMeetupGroup);

            builder.Entity<MeetupGroupMember>().HasKey(m => new { m.GroupId, m.MemberId });
            builder.Entity<MeetupGroupMember>().HasOne(m => m.Group).WithMany(g => g.Members).HasForeignKey(m => m.GroupId);
            builder.Entity<MeetupGroupMember>().HasOne(m => m.Member).WithMany(u => u.GroupMemberships).HasForeignKey(m => m.MemberId);

            builder.Entity<MeetupGroupEventAttendee>().HasKey(m => new { m.EventId, m.AttendeeId });
            builder.Entity<MeetupGroupEventAttendee>().HasOne(m => m.Event).WithMany(g => g.Attendees).HasForeignKey(m => m.EventId);
            builder.Entity<MeetupGroupEventAttendee>().HasOne(m => m.Attendee).WithMany(u => u.GroupEventAttendance).HasForeignKey(m => m.AttendeeId);

        }


        public DbSet<MeetupGroup> MeetupGroups { get; set; }
        public DbSet<MeetupGroupEvent> MeetupGroupEvents { get; set; }

        public DbSet<MeetupGroupMember> GroupMembers { get; set; }
        public DbSet<MeetupGroupEventAttendee> EventAttendees { get; set; }

    }
}
