using BugTracker.Data;
using BugTracker.Models;
using BugTracker.Models.ViewModels;
using BugTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBTRoleService _roleService;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(
            ILogger<HomeController> logger,
             IBTRoleService roleService, 
             ApplicationDbContext dbContext
            )
        {
            _logger = logger;
            _roleService = roleService;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            DashboardViewModel model = new DashboardViewModel();
            var tickets = _dbContext.Ticket
                .Include(t => t.TicketStatus)
                .Include(t => t.TicketPriority)
                .ToList();

            var projects = _dbContext.Project
                .Include(p => p.Company)
                .Include(p => p.Members)
                .ToList();

            model.Tickets = tickets;
            model.Projects = projects;

            return View(model);
        }

       

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ManageRoles()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(List<string> userIds, string roleName)
        {
            foreach (var userId in userIds)
            {
                BTUser user = await _dbContext.Users.FindAsync(userId);
                if (!await _roleService.IsUserInRoleAsync(user, roleName))
                {
                    var userRoles = await _roleService.ListUserRolesAsync(user);
                    foreach (var role in userRoles)
                    {
                        await _roleService.RemoveUserFromRoleAsync(user, role);
                    }
                    await _roleService.AddUserToRoleAsync(user, roleName);
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
