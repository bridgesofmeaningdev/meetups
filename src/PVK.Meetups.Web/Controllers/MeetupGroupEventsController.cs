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
    public class MeetupGroupEventsController : Controller
    {
        //TODO use repository instead of context directly
        private readonly PVKMeetupsDbContext _context;

        public MeetupGroupEventsController(PVKMeetupsDbContext context)
        {
            _context = context;
        }

        // GET: MeetupGroupEvents
        public async Task<IActionResult> Index()
        {
            if (_context.MeetupGroups == null)
                Problem("Entity set 'PVKMeetupsDbContext.MeetupGroupEvents'  is null.");


            var items = await _context.MeetupGroupEvents.ToListAsync();
            return View(items.Select(i => new MeetupGroupEventModel(i)).ToList());

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
        public IActionResult Create()
        {
            return View();
        }

        // POST: MeetupGroupEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MeetupGroupEventModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dbItem = model.ToEvent();

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

            var meetupGroupEvent = await _context.MeetupGroupEvents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meetupGroupEvent == null)
            {
                return NotFound();
            }

            return View(new MeetupGroupEventModel(meetupGroupEvent));
        }

        // POST: MeetupGroupEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MeetupGroupEvents == null)
            {
                return Problem("Entity set 'PVKMeetupsDbContext.MeetupGroupEvents'  is null.");
            }
            var meetupGroupEvent = await _context.MeetupGroupEvents.FindAsync(id);

            if (meetupGroupEvent == null)
                return RedirectToAction(nameof(Index));


            _context.MeetupGroupEvents.Remove(meetupGroupEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
