using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Services
{
    public interface IBTTicketService
    {
        //Is the user on the ticket

        public Task<bool> IsUserOnTicketAsync(string userId, int ticketId);

        //Check all users in the ticket
        public Task<IEnumerable<BTUser>> UsersOnTicketAsync(int ticketId);
        //Check all users not on the ticket
        public Task<IEnumerable<BTUser>> UsersNotOnTicketAsync(int ticketId);

        //Assign/Add user to a ticket
        public Task AddUserToTicketAsync(string userId, int ticketId);
        //Remove from ticket
        public Task RemoveUserFromTicketAsync(string userId, int ticketId);

        //All projects for one user
        public Task<IEnumerable<Ticket>> ListUserTicketsAsync(string userId);
        //Developers on Ticket
        public Task<IEnumerable<BTUser>> DevelopersOnTicketAsync(int ticketId);

        //Submitters on Ticket
        public Task<IEnumerable<BTUser>> SubmittersOnTicketAsync(int ticketId);

        //Project Manager on Ticket
        public Task<BTUser> ProjectManagerOnTicketAsync(int ticketId);
    }
}
