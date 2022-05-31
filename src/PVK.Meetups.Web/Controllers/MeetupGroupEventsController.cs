 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PVK.Meetups.Web.Data;
using PVK.Meetups.Web.Models;

namespace PVK.Meetups.Web.Controllers
{
    [Route("groups/{groupId:int}/events/{action}/{id:int?}")]
    public class MeetupGroupEventsController : Controller
    {
        //TODO use repository instead of context directly
        private readonly PVKMeetupsDbContext _context;

        public MeetupGroupEventsController(PVKMeetupsDbContext context)
        {
            _context = context;
        }

        // GET: MeetupGroupEvents
        public async Task<IActionResult> Index(int groupId)
        {
            if (_context.MeetupGroups == null)
                Problem("Entity set 'PVKMeetupsDbContext.MeetupGroupEvents'  is null.");


            var items = await _context.MeetupGroupEvents.Where(e =>e.OwningMeetupGroupId == groupId).ToListAsync();
            var model = new MeetupGroupEventListModel
            {
                OwningMeetupGroupId = groupId,
                Events = items.Select(i => new MeetupGroupEventModel(i)).ToList()
            };

            return View(model);
        }

        // GET: MeetupGroupEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MeetupGroupEvents == null)
            {
                return NotFound();
            }

            var meetupGroupEvent = await _context.MeetupGroupEvents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meetupGroupEvent == null)
            {
                return NotFound();
            }

            return View(new MeetupGroupEventModel(meetupGroupEvent));
        }

        // GET: MeetupGroupEvents/Create
        public IActionResult Create(int groupId)
        {
            return View(new MeetupGroupEventModel {  OwningMeetupGroupId = groupId });
        }

        // POST: MeetupGroupEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MeetupGroupEventModel model)
        {
            if (!ModelState.IsValid || model.OwningMeetupGroupId == null)
                return View(model);

            var dbItem = model.ToEvent();
            var owningGroup = await _context.MeetupGroups.FindAsync(model.OwningMeetupGroupId.Value);
            if (owningGroup == null)
                return View(model); // TODO need a better error page

            _context.Add(dbItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = dbItem.Id });
        }

        // GET: MeetupGroupEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MeetupGroupEvents == null)
            {
                return NotFound();
            }

            var meetupGroupEvent = await _context.MeetupGroupEvents.FindAsync(id);
            if (meetupGroupEvent == null)
            {
                return NotFound();
            }
            return View(new MeetupGroupEventModel(meetupGroupEvent));
        }

        // POST: MeetupGroupEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MeetupGroupEventModel meetupGroupEvent)
        {
            if (id != meetupGroupEvent.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return View(meetupGroupEvent);

            var dbItem = await _context.MeetupGroupEvents.FindAsync(id);
            if (dbItem == null)
                return NotFound();

            dbItem.Name = meetupGroupEvent.Name;
            dbItem.Description = meetupGroupEvent.Description;
            dbItem.LocationDescription = meetupGroupEvent.LocationDescription;
            dbItem.StartDateTimeUtc = meetupGroupEvent.StartDateTime.ToUniversalTime();
            dbItem.EndDateTimeUtc = meetupGroupEvent.EndDateTime.ToUniversalTime();

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { dbItem.Id });
        }

        // GET: MeetupGroupEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MeetupGroupEvents == null)
            {
                return NotFound();
            }

            var meetupGroupEvent = await _context.MeetupGroupEvents.FirstOrDefaultAsync(m => m.Id == id);
            if (meetupGroupEvent == null)
            {
                return NotFound();
            }

            return View(new MeetupGroupEventModel(meetupGroupEvent));
        }

        // POST: MeetupGroupEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int owningMeetupGroupId)
        {
            if (_context.MeetupGroupEvents == null)
            {
                return Problem("Entity set 'PVKMeetupsDbContext.MeetupGroupEvents'  is null.");
            }
            var meetupGroupEvent = await _context.MeetupGroupEvents.FindAsync(id);

            if (meetupGroupEvent == null)
                return RedirectToAction(nameof(Index), new { groupId = owningMeetupGroupId });


            _context.MeetupGroupEvents.Remove(meetupGroupEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
