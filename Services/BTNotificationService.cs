using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BugTracker.Services
{
    public class BTNotificationService : IBTNotificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BTUser> _userManager;
        public BTNotificationService(
            ApplicationDbContext context,
            UserManager<BTUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(ClaimsPrincipal currentUser)
        {
            var user = await _userManager.GetUserAsync(currentUser);

            var userNotifications = _context.Notification.Where(n => n.RecipientId == user.Id);
            var unreadNotifications = await userNotifications.Where(n => !n.Viewed).ToListAsync();

            return unreadNotifications;
        }
    }
}
