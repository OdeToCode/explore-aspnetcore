using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood2.Core.Entities;
using OdeToFood2.Data;
using System.Collections.Generic;

namespace OdeToFood2.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> CuisineOptions { get; set; }

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            CuisineOptions = htmlHelper.GetEnumSelectList<CuisineType>();
        }

        public IActionResult OnGet(int? restaurantId)
        {
            if (restaurantId.HasValue)
            {
                Restaurant = restaurantData.GetRestaurantById(restaurantId.Value);
                if (Restaurant == null)
                {
                    return RedirectToPage("NotFound");
                }
            }
            else
            {
                Restaurant = new Restaurant();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Restaurant.Id == 0)
                {
                    Restaurant = restaurantData.Add(Restaurant);
                }
                else
                {
                    Restaurant = restaurantData.Update(Restaurant);
                }
                restaurantData.Commit();
                return RedirectToPage("Detail", new { restaurantId = Restaurant.Id });
            }
            return Page();
        }
    }
}