using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PVK.Meetups.Web.Data;
using PVK.Meetups.Web.Models;

namespace PVK.Meetups.Web.Controllers
{
    public class MeetupGroupsController : Controller
    {
        //TODO replace direct context use with a repository interface
        private readonly PVKMeetupsDbContext _context;

        public MeetupGroupsController(PVKMeetupsDbContext context)
        {
            _context = context;
        }

        // GET: MeetupGroups
        public async Task<IActionResult> Index()
        {
            if (_context.MeetupGroups == null)
                return Problem("Entity set 'PVKMeetupsDbContext.MeetupGroups'  is null.");

            // TODO add sorting, filtering, paging? 
            var groups = await _context.MeetupGroups
                .AsNoTracking()
                .ToListAsync();

            return View(groups.Select(m => new MeetupGroupModel(m)).ToList());
        }

        // GET: MeetupGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MeetupGroups == null)
            {
                return NotFound();
            }

            var meetupGroup = await _context.MeetupGroups
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (meetupGroup == null)
            {
                return NotFound();
            }

            return View(new MeetupGroupModel(meetupGroup));
        }

        // GET: MeetupGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MeetupGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MeetupGroupModel meetupGroup)
        {
            if (!ModelState.IsValid)
            {
                return View(meetupGroup);
            }

            var emailAddress = this.User.FindFirst(ClaimTypes.Email)?.Value;
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == emailAddress);

            meetupGroup.Id = null;
            var group = meetupGroup.ToGroup();

            var organizer = new MeetupGroupMember
            {
                Group = group,
                IsGroupOrganizer = true,
                IsPrimaryGroupOrganizer = true,
                Member = dbUser,
                MemberId = dbUser.Id
            };

            _context.Add(group);
            _context.Add(organizer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = group.Id });
        }


        // GET: MeetupGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MeetupGroups == null)
            {
                return NotFound();
            }

            var meetupGroup = await _context.MeetupGroups.FindAsync(id);
            if (meetupGroup == null)
            {
                return NotFound();
            }
            return View(new MeetupGroupModel(meetupGroup));
        }

        // POST: MeetupGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MeetupGroupModel meetupGroup)
        {
            if (id != meetupGroup.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return View(meetupGroup);


            var group = await _context.MeetupGroups.FirstOrDefaultAsync(g => g.Id == meetupGroup.Id);
            if (group == null)
                return NotFound();

            group.Name = meetupGroup.Name;
            group.Description = meetupGroup.Description;
            group.LocationDescription = meetupGroup.LocationDescription;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = meetupGroup.Id });
        }

        // GET: MeetupGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MeetupGroups == null)
            {
                return NotFound();
            }

            var meetupGroup = await _context.MeetupGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meetupGroup == null)
            {
                return NotFound();
            }

            return View(new MeetupGroupModel(meetupGroup));
        }

        // POST: MeetupGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MeetupGroups == null)
            {
                return Problem("Entity set 'PVKMeetupsDbContext.MeetupGroups'  is null.");
            }

            var meetupGroup = await _context.MeetupGroups.FindAsync(id);
            if (meetupGroup != null)
            {
                _context.MeetupGroups.Remove(meetupGroup);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
