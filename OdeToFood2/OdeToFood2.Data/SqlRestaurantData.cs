using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OdeToFood2.Core.Entities;

namespace OdeToFood2.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDb db;

        public SqlRestaurantData(OdeToFoodDb db)
        {
            this.db = db;
        }

        public Restaurant Add(Restaurant restaurant)
        {
            db.Add(restaurant);
            return restaurant;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetRestaurantById(id);
            if(restaurant != null)
            {
                db.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public int GetCountOfRestaurants()
        {
            return db.Restaurants.Count();
        }

        public Restaurant GetRestaurantById(int id)
        {
            return db.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            var query = from r in db.Restaurants
                where r.Name.StartsWith(name) || String.IsNullOrEmpty(name)
                orderby r.Name
                select r;
            return query;
        }

        public Restaurant Update(Restaurant restaurant)
        {
            var entity = db.Restaurants.Attach(restaurant);
            entity.State = EntityState.Modified;
            db.SaveChanges();
            return restaurant;
        }
    }
}
