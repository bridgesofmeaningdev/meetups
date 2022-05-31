using Microsoft.EntityFrameworkCore;

namespace PVK.Meetups.Web.Data
{
    public interface ISearchRepository
    {
        /// <summary>
        /// Searches via "contains" for matches in the <see cref="MeetupGroup.Name"/> and <see cref="MeetupGroup.Description"/>
        /// </summary>
        /// <param name="title">the text to search for</param>
        /// <param name="limit">the maximum number of records to return. Defaults to 10, max of 50</param>
        /// <returns></returns>
       Task<List<MeetupGroup>> FindGroupsByFreeTextAsync(string title, int? limit = null);

        /// <summary>
        /// Searches via "contains" for matches in the <see cref="MeetupGroup.LocationDescription"/>
        /// </summary>
        /// <param name="title">the text to search for</param>
        /// <param name="limit">the maximum number of records to return. Defaults to 10, max of 50</param>
        /// <returns></returns>
        Task<List<MeetupGroup>> FindGroupsByLocationAsync(string location, int? limit = null);

        /// <summary>
        /// Searches via "contains" for matches in the <see cref="MeetupGroupEvent.Name"/> and <see cref="MeetupGroupEvent.Description"/>
        /// </summary>
        /// <param name="title">the text to search for</param>
        /// <param name="limit">the maximum number of records to return. Defaults to 10, max of 50</param>
        /// <returns></returns>
        Task<List<MeetupGroupEvent>> FindEventsByFreeTextAsync(string title, int? limit = null);

        /// <summary>
        /// Searches via "contains" for matches in the <see cref="MeetupGroupEvent.LocationDescription"/>
        /// </summary>
        /// <param name="title">the text to search for</param>
        /// <param name="limit">the maximum number of records to return. Defaults to 10, max of 50</param>
        /// <returns></returns>
        Task<List<MeetupGroupEvent>> FindEventsByLocationAsync(string location, int? limit = null);



    }

    public class SearchRepository : ISearchRepository
    {
        private readonly ILogger<SearchRepository> _logger;
        private readonly PVKMeetupsDbContext _context;

        public SearchRepository(ILogger<SearchRepository> logger, PVKMeetupsDbContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private int ConvertToInternalLimit(int? limit)
        {
            return limit == null ? 10 : (limit.Value < 1 ? 10 : (limit.Value > 50 ? 50 : limit.Value));
        }

        public async  Task<List<MeetupGroup>> FindGroupsByFreeTextAsync(string title, int? limit = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                return new List<MeetupGroup>();

            var internalLimit = ConvertToInternalLimit(limit);

            return await _context.MeetupGroups
                .Where(g => g.Name.Contains(title) || g.Description.Contains(title))
                .Take(internalLimit)
                .ToListAsync();
        }

        public async Task<List<MeetupGroup>> FindGroupsByLocationAsync(string location, int? limit = null)
        {
            if (string.IsNullOrWhiteSpace(location))
                return new List<MeetupGroup>();

            var internalLimit = ConvertToInternalLimit(limit);

            return await _context.MeetupGroups
                .Where(g => g.LocationDescription.Contains(location))
                .Take(internalLimit)
                .ToListAsync();
        }

        public async Task<List<MeetupGroupEvent>> FindEventsByFreeTextAsync(string title, int? limit = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                return new List<MeetupGroupEvent>();

            var internalLimit = ConvertToInternalLimit(limit);

            return await _context.MeetupGroupEvents
                .AsNoTracking()
                .Include(g => g.OwningMeetupGroup)
                .Where(g => g.Name.Contains(title) || g.Description.Contains(title))
                .Take(internalLimit)
                .ToListAsync();
        }

        public async Task<List<MeetupGroupEvent>> FindEventsByLocationAsync(string location, int? limit = null)
        {
            if (string.IsNullOrWhiteSpace(location))
                return new List<MeetupGroupEvent>();

            var internalLimit = ConvertToInternalLimit(limit);

            return await _context.MeetupGroupEvents
                .AsNoTracking()
                .Include(g => g.OwningMeetupGroup)
                .Where(g => g.LocationDescription.Contains(location))
                .Take(internalLimit)
                .ToListAsync();
        }

    }
}
