using OdeToFood2.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood2.Data
{
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

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            return
                from r in _restaurants
                where string.IsNullOrEmpty(name) || r.Name.StartsWith(name) 
                orderby r.Name
                select r;
        }

        public Restaurant GetRestaurantById(int id)
        {
            return _restaurants.FirstOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            _restaurants.Add(newRestaurant);
            newRestaurant.Id = _restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = _restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if(restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }
    }
}
