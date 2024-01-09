using Microsoft.AspNetCore.Mvc.Filters;
namespace LibraryManagementSystem.Filters;
public class LoggingActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        string controllerName = context.Controller.GetType().Name;
        string actionName = context.ActionDescriptor.RouteValues["action"];
        string logMessage = $"Executing action filter for {actionName} in controller {controllerName}";
        Log(logMessage);
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        string controllerName = context.Controller.GetType().Name;
        string actionName = context.ActionDescriptor.RouteValues["action"];
        string logMessage = $"Executed action filter for {actionName} in controller {controllerName}";
        Log(logMessage);
    }

    private void Log(string message)
    {
        Console.WriteLine(message);
    }
}
