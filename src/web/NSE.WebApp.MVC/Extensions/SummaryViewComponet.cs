using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Extensions
{
    public class SummaryViewComponet : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
