using Microsoft.AspNetCore.Mvc;
using PVK.Meetups.Web.Data;
using PVK.Meetups.Web.Models;

namespace PVK.Meetups.Web.Controllers
{
    [Route("search")]
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly ISearchRepository _repo;

        public SearchController(ILogger<SearchController> logger, ISearchRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repo = repository ?? throw new ArgumentNullException(nameof(repository));

        }

        [HttpGet]
        public async Task<IActionResult> FindNearbyMeetups(SearchInputModel model)
        {
            var resultModel = new FindMeetupsResultModel();
            if (string.IsNullOrWhiteSpace(model.Topics) && string.IsNullOrWhiteSpace(model.Location))
                return View(resultModel);

            //TODO this search should be an AND - that is, if both are set, filter for matching topics near the location
            if (!string.IsNullOrWhiteSpace(model.Topics))
            {
                var resGroups = await _repo.FindGroupsByFreeTextAsync(model.Topics, 10);
                var resEvents = await _repo.FindEventsByFreeTextAsync(model.Topics, 10);

                var knownGroups = resGroups.Select(g => g.Id).ToList();
                resEvents = resEvents.Where(e => !knownGroups.Contains(e.OwningMeetupGroup.Id)).ToList();

                resultModel.Results.AddRange(resGroups.Select(g => new MeetupSearchResult(g.Id, null, g.Name, g.LocationDescription)));
                resultModel.Results.AddRange(resEvents.Select(e => new MeetupSearchResult(e.OwningMeetupGroup.Id, e.Id, e.Name, e.LocationDescription)));

            }

            if (!string.IsNullOrWhiteSpace(model.Location))
            {
                var resGroups = await _repo.FindGroupsByLocationAsync(model.Location, 10);
                var resEvents = await _repo.FindEventsByLocationAsync(model.Location, 10);

                var knownGroups = resGroups.Select(g => g.Id).ToList();
                resEvents = resEvents.Where(e => !knownGroups.Contains(e.OwningMeetupGroup.Id)).ToList();

                resultModel.Results.AddRange(resGroups.Select(g => new MeetupSearchResult(g.Id, null, g.Name, g.LocationDescription)));
                resultModel.Results.AddRange(resEvents.Select(e => new MeetupSearchResult(e.OwningMeetupGroup.Id, e.Id, e.Name, e.LocationDescription)));
            }

            return View(resultModel);
        }

    }
}
