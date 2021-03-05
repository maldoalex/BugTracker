//using BugTracker.Data;
//using BugTracker.Data.Enums;
//using BugTracker.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BugTracker.Services
//{
//    public class BasicBTTicketService : IBTTicketService
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly UserManager<BTUser> _userManager;
//        private readonly IHttpContextAccessor _contextAccessor;
//        private readonly IBTRoleService _roleService;

//        public BasicBTTicketService(
//            ApplicationDbContext context,
//            UserManager<BTUser> userManager,
//            IHttpContextAccessor contextAccessor,
//            IBTRoleService roleService)
//        {
//            _context = context;
//            _userManager = userManager;
//            _contextAccessor = contextAccessor;
//            _roleService = roleService;
//        }
//        //public async Task AddUserToTicketAsync(string userId, int projectId)
//        //{
//            //Need to check for Developer - only 1 allowed
//            //try
//            //{
//            //    if (!await IsUserOnTicketAsync(userId, projectId))
//            //    {
//            //        BTUser user = await _userManager.FindByIdAsync(userId);

//            //        if (await _roleService.IsUserInRoleAsync(user, Roles.ProjectManager.ToString()))
//            //        {
//            //            var oldDeveloper = await DevelopersOnProjectAsync(projectId);
//            //            if (oldDeveloper != null)
//            //            {
//            //                await RemoveUserFromTicketAsync(oldDeveloper.Id, projectId);
//            //            }
//            //        }
//            //        Project project = await _context.Project.FindAsync(projectId);

//            //        try
//            //        {
//            //            project.Members.Add(user);
//            //            await _context.SaveChangesAsync();
//            //        }
//            //        catch (Exception)
//            //        {
//            //            throw;
//            //        }
//            //    }


//            //}
//            //catch (Exception ex)
//            //{
//            //    Debug.WriteLine($"****ERROR**** - Error adding user to project {ex.Message}");
//            //}
//        //}

//        public async Task<IEnumerable<BTUser>> DevelopersOnProjectAsync(int projectId)
//        {
//            var developers = await _userManager.GetUsersInRoleAsync(Roles.Developer.ToString());
//            var onProject = await UsersOnProjectAsync(projectId);
//            var devsOnProject = developers.Intersect(onProject);
//            return devsOnProject.ToList();
//        }

//        public Task<IEnumerable<BTUser>> DevelopersOnTicketAsync(int ticketId)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<bool> IsUserOnProjectAsync(string userId, int projectId)
//        {
//            var project = await _context.Project.Include(p => p.Members).FirstOrDefaultAsync(p => p.Id == projectId);
//            var flag = project.Members.Any(u => u.Id == userId);
//            return flag;
//        }

//        public Task<bool> IsUserOnTicketAsync(string userId, int ticketId)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<IEnumerable<Project>> ListUserProjectsAsync(string userId)
//        {
//            var user = await _userManager.FindByIdAsync(userId);

//            if (await _roleService.IsUserInRoleAsync(user, Roles.Admin.ToString()))
//            {
//                return await _context.Project.ToListAsync();
//            }
//            //var output = new List<Project>();
//            //var allProjects = await _context.Project.ToListAsync();
//            //foreach (var project in allProjects)
//            //{
//            //    var isUserOnProject = await IsUserOnProjectAsync(userId, project.Id);
//            //    if (await IsUserOnProjectAsync(userId, project.Id))
//            //    {
//            //        output.Add(project);
//            //    }
//            //}
//            var output = new List<Project>();
//            foreach (var project in await _context.Project.ToListAsync())
//            {
//                if (await IsUserOnProjectAsync(userId, project.Id))
//                {
//                    output.Add(project);
//                }
//            }
//            return output;
//        }

//        public Task<IEnumerable<Ticket>> ListUserTicketsAsync(string userId)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<BTUser> ProjectManagerOnProjectAsync(int projectId)
//        {
//            var projectManagers = await _userManager.GetUsersInRoleAsync(Roles.ProjectManager.ToString());
//            var onProject = await UsersOnProjectAsync(projectId);
//            var projectManager = projectManagers.Intersect(onProject).FirstOrDefault();
//            return projectManager;
//        }

//        public Task<BTUser> ProjectManagerOnTicketAsync(int ticketId)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task RemoveUserFromProjectAsync(string userId, int projectId)
//        {
//            //Need to check for Project Manager - only 1 allowed
//            try
//            {
//                if (await IsUserOnProjectAsync(userId, projectId))
//                {
//                    BTUser user = await _userManager.FindByIdAsync(userId);
//                    Project project = await _context.Project.FindAsync(projectId);

//                    try
//                    {
//                        project.Members.Remove(user);
//                        await _context.SaveChangesAsync();
//                    }
//                    catch (Exception)
//                    {
//                        throw;
//                    }
//                }


//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine($"****ERROR**** - Error removing user to project {ex.Message}");
//            }
//        }

//        public Task RemoveUserFromTicketAsync(string userId, int ticketId)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<IEnumerable<BTUser>> SubmittersOnProjectAsync(int projectId)
//        {
//            var submitters = await _userManager.GetUsersInRoleAsync(Roles.Submitter.ToString());
//            var onProject = await UsersOnProjectAsync(projectId);
//            var subsOnProject = submitters.Intersect(onProject);
//            return subsOnProject.ToList();
//        }

//        public Task<IEnumerable<BTUser>> SubmittersOnTicketAsync(int ticketId)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<IEnumerable<BTUser>> UsersNotOnProjectAsync(int projectId)
//        {
//            var output = new List<BTUser>();
//            foreach (var user in await _context.Users.ToListAsync())
//            {
//                if (await IsUserOnProjectAsync(user.Id, projectId))
//                {
//                    output.Add(user);
//                }
//            }
//            return output;
//        }

//        public Task<IEnumerable<BTUser>> UsersNotOnTicketAsync(int ticketId)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<IEnumerable<BTUser>> UsersOnProjectAsync(int projectId)
//        {
//            var output = new List<BTUser>();
//            foreach (var user in await _context.Users.ToListAsync())
//            {
//                if (await IsUserOnProjectAsync(user.Id, projectId))
//                {
//                    output.Add(user);
//                }
//            }
//            return output;
//        }

//        public Task<IEnumerable<BTUser>> UsersOnTicketAsync(int ticketId)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
