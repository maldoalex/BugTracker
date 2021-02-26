﻿using BugTracker.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Services
{
    public interface IBTRoleService
    {
        public Task<bool> AddUserToRoleAsync(BTUser user, string roleName);

        public Task<bool> IsUserInRoleAsync(BTUser user, string roleName);

        public Task<IEnumerable<string>> ListUserRolesAsync(BTUser user);

        public Task<bool> RemoveUserFromRoleAsync(BTUser user, string roleName);

        public Task<IEnumerable<BTUser>> UsersInRoleAsync(string roleName);

        public Task<IEnumerable<BTUser>> UsersNotInRoleAsync(string roleName);

        public IEnumerable<IdentityRole> NonDemoRoles();
    }
}
