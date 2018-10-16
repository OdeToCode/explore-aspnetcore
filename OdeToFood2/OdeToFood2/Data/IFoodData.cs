using OdeToFood2.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood2.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAllRestaurants();
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> _restaurants;

        public InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name = "Scott's Pizza", Location="Maryland", Cuisine=CuisineType.Indian},
                new Restaurant { Id = 2, Name = "Allen's Burgers", Location="Berryland", Cuisine=CuisineType.Italian},
                new Restaurant { Id = 3, Name = "Foo Fish", Location = "Seattle", Cuisine = CuisineType.Mexican }
            };
        }

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return _restaurants.OrderBy(r => r.Name);
        }
    }
}
