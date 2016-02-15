using Microsoft.AspNet.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;

        public HomeController(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        public ViewResult Index()
        {
            var model = _restaurantData.GetAll();

            return View(model);
        }
    }
}
