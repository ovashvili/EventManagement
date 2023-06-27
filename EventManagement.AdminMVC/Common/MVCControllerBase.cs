using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.AdminMVC.Common
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
