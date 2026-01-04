using Dela.Toolkit.Application;
using Microsoft.AspNetCore.Mvc;

namespace Dela.Toolkit.ServiceHost;

[ApiController]
[Route("/api/[controller]")]
public class BaseController : ControllerBase
{
    protected readonly IQueryDispatcher QueryDispatcher;
    protected readonly ICommandDispatcher CommandDispatcher; 
    public BaseController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        QueryDispatcher = queryDispatcher;
        CommandDispatcher = commandDispatcher;
    }

    public BaseController()
    {
    }
}