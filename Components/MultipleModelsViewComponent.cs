using HastaneOtomasyonSistemi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HastaneOtomasyonSistemi.Components
{
    public class MultipleModelsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Doktor model1, Hasta model2)
        {
            var viewModel = new MultipleModelsViewModel
			{
                doktor = model1,
                hasta = model2
            };

            return View(viewModel);
        }
    }
}
