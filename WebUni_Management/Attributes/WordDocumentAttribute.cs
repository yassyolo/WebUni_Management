using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebUni_Management.Controllers;

namespace WebUni_Management.Attributes
{
	public class WordDocumentAttribute : ActionFilterAttribute
	{

        public string DefaultFilename { get; set; }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.Controller as MenuController;
            if (controller != null)
            {
                controller.ViewBag.WordDocumentMode = true;
            }

            base.OnActionExecuted(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ViewResult)
            {
                var controller = context.Controller as MenuController;
                if (controller != null)
                {
                    var filename = controller.ViewBag.WordDocumentFileName;
                    filename = filename ?? DefaultFilename ?? "Document";

                    context.HttpContext.Response.Headers.Add("Content-Disposition", $"attachment; filename={filename}.doc");
                    context.HttpContext.Response.ContentType = "application/msword";
                }
            }

            base.OnResultExecuting(context);
        }
    }

}

