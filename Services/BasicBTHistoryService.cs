using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Services
{
    public class BasicBTHistoryService : IBTHistoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        //constructor
        public BasicBTHistoryService(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public ApplicationDbContext Context { get; }

        public async Task AddHistoryAsync(Ticket oldTicket, Ticket newTicket, string userId)
        {
            if (oldTicket.Title != newTicket.Title)
            {
                //Ticket History for Title
                TicketHistory history = new TicketHistory
                {
                    TicketId = newTicket.Id,
                    Property = "Title",
                    OldValue = oldTicket.Title,
                    NewValue = newTicket.Title,
                    Created = DateTimeOffset.Now,
                    UserId = userId
                };

                await _context.TicketHistory.AddAsync(history);
            }

            if (oldTicket.Description != newTicket.Description)
            {
                //Ticket History for Description

                TicketHistory history = new TicketHistory
                {
                    TicketId = newTicket.Id,
                    Property = "Description",
                    OldValue = oldTicket.Description,
                    NewValue = newTicket.Description,
                    Created = DateTimeOffset.Now,
                    UserId = userId
                };

                await _context.TicketHistory.AddAsync(history);

            }

            if (oldTicket.TicketTypeId != newTicket.TicketTypeId)
            {
                //Ticket History for Type
                TicketHistory history = new TicketHistory
                {
                    TicketId = newTicket.Id,
                    Property = "Ticket Type",
                    OldValue = oldTicket.TicketType.Name,
                    NewValue = newTicket.TicketType.Name,
                    Created = DateTimeOffset.Now,
                    UserId = userId
                };

                await _context.TicketHistory.AddAsync(history);

            }

            if (oldTicket.TicketPriorityId != newTicket.TicketPriorityId)
            {
                //Ticket History for Priority
                TicketHistory history = new TicketHistory
                {
                    TicketId = newTicket.Id,
                    Property = "Ticket Priority",
                    OldValue = oldTicket.TicketPriority.Name,
                    NewValue = newTicket.TicketPriority.Name,
                    Created = DateTimeOffset.Now,
                    UserId = userId
                };

                await _context.TicketHistory.AddAsync(history);

            }

            if (oldTicket.TicketStatusId != newTicket.TicketStatusId)
            {
                //Ticket History for Status
                TicketHistory history = new TicketHistory
                {
                    TicketId = newTicket.Id,
                    Property = "Ticket Status",
                    OldValue = oldTicket.TicketStatus.Name,
                    NewValue = newTicket.TicketStatus.Name,
                    Created = DateTimeOffset.Now,
                    UserId = userId
                };

                await _context.TicketHistory.AddAsync(history);

            }

            if (oldTicket.DeveloperUserId != newTicket.DeveloperUserId)
            {
                //Ticket History for DeveloperUser Id
                TicketHistory history = new TicketHistory
                {
                    TicketId = newTicket.Id,
                    Property = "Developer",
                    OldValue = "Unassigned",
                    NewValue = newTicket.DeveloperUser.FullName,
                    Created = DateTimeOffset.Now,
                    UserId = userId
                };

                await _context.TicketHistory.AddAsync(history);
            }
            else
            {
                //Ticket History for DeveloperUser Id
                TicketHistory history = new TicketHistory
                {
                    TicketId = newTicket.Id,
                    Property = "Developer",
                    OldValue = oldTicket.DeveloperUser.FullName,
                    NewValue = newTicket.DeveloperUser.FullName,
                    Created = DateTimeOffset.Now,
                    UserId = userId
                };

                await _context.TicketHistory.AddAsync(history);
            }


            Notification notification = new Notification
            {
                TicketId = newTicket.Id,
                Description = "You have a new ticket!",
                Created = DateTimeOffset.Now,
                SenderId = userId,
                RecipientId = newTicket.DeveloperUserId
            };

            await _context.Notification.AddAsync(notification);

            //send an email
            string devEmail = newTicket.DeveloperUser.Email;
            string subject = "New Ticket Assignment";
            string message = $"You have a new ticket for a project: {newTicket.Project.Name}";

            await _emailSender.SendEmailAsync(devEmail, subject, message);

            await _context.SaveChangesAsync();
        }
    }
}
