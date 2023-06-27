using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.MVC.Common
{
    public class MVCControllerBase : Controller
    {
        protected readonly IMediator Mediator;
        public MVCControllerBase(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
