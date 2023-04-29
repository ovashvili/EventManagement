using EventManagement.Application.Commmon.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Events.Commands.PurchaseTicket
{
    public class PurchaseTicketCommand : IRequest<CommandResponse<string>>
    {
        public PurchaseTicketCommandModel Model { get; set; }
    }
    public class PurchaseTicketCommandModel
    {
        //public int UserId { get; set; }
        public string EventId { get; set; }
    }
}