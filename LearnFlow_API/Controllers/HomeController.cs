using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Unipluss.Sign.ExternalContract.Entities;

public class HomeController : Controller
{
    [Route("Home/Error")]
    public IActionResult Error()
    {
        var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionDetails?.Error;

        return View(new ErrorViewModel { Message = "Algo deu errado, por favor tente novamente mais tarde." });
    }
}
