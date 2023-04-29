using EventManagement.Application.Commmon.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Events.Commands.ReserveTicket
{
    public class ReserveTicketCommand : IRequest<CommandResponse<string>>
    {
        public ReserveTicketCommandModel Model { get; set;}
    }
    public class ReserveTicketCommandModel
    {
        //public int UserId { get; set; }
        public string EventId { get; set; }
    }
}
