﻿using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Services
{
    public interface IBTProjectService
    {
        //Is the user on the project

        public Task<bool> IsUserOnProjectAsync(string userId, int projectId);

        //Check all users in the project
        public Task<IEnumerable<BTUser>> UsersOnProjectAsync(int projectId);
        //Check all users not on the project
        public Task<IEnumerable<BTUser>> UsersNotOnProjectAsync(int projectId);

        //Assign/Add user to a project
        public Task AddUserToProjectAsync(string userId, int projectId);
        //Remove from project
        public Task RemoveUserFromProjectAsync(string userId, int projectId);

        //All projects for one user
        public Task<IEnumerable<Project>> ListUserProjectsAsync(string userId);
        //Developers on Project
        public Task<IEnumerable<BTUser>> DevelopersOnProjectAsync(int projectId);

        //Submitters on Projects
        public Task<IEnumerable<BTUser>> SubmittersOnProjectAsync(int projectId);

        //Project Manager on Project
        public Task<BTUser> ProjectManagerOnProjectAsync(int projectId);
    }
}
