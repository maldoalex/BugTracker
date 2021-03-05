using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BugTracker.Services
{
    public interface IBTNotificationService
    {
        public Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(ClaimsPrincipal currentUser);
    }
}
