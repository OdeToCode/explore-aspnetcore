using OdeToFood2.Core.Entities;
using System.Collections.Generic;

namespace OdeToFood2.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetRestaurantById(int id);
    }
}
