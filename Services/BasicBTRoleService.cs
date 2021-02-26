﻿using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Services
{
    public class BasicBTRoleService : IBTRoleService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BTUser> _userManager;

        public BasicBTRoleService(ApplicationDbContext context, UserManager<BTUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<bool> AddUserToRoleAsync(BTUser user, string roleName)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<bool> IsUserInRoleAsync(BTUser user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<IEnumerable<string>> ListUserRolesAsync(BTUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public IEnumerable<IdentityRole> NonDemoRoles()
        {
            var roles = _context.Roles;
            var output = new List<IdentityRole>();

            foreach (var role in roles)
            {
                if (role.Name != "DemoUser")
                //if (role.Name != roles.DemoUser.ToString())
                {
                    output.Add(role);
                }
            }
            return output;
        }

        public async Task<bool> RemoveUserFromRoleAsync(BTUser user, string roleName)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<IEnumerable<BTUser>> UsersInRoleAsync(string roleName)
        {
            return await _userManager.GetUsersInRoleAsync(roleName);
        }

        public async Task<IEnumerable<BTUser>> UsersNotInRoleAsync(string roleName)
        {
            var inRole = await UsersInRoleAsync(roleName);
            var users = await _context.Users.ToListAsync();
            return users.Except(inRole);
        }
    }
}
