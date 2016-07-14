using System.Collections.Generic;
using System;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Restaurants
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Cuisine> allCuisine = Cuisine.GetAll();
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object> {};
        model.Add("cuisine", allCuisine);
        model.Add("restaurants", allRestaurants);
        return View["index.cshtml", model];
      };
      Post["/restaurant/new"] = _ => {
        Restaurant newRestaurant = new Restaurant(Request.Form["restaurantName"], Request.Form["cuisineSelect"]);
        newRestaurant.Save();
        List<Cuisine> allCuisine = Cuisine.GetAll();
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object> {};
        model.Add("cuisine", allCuisine);
        model.Add("restaurants", allRestaurants);
        return View["index.cshtml", model];
      };
      Get["/restaurant/delete/all"] = _ => {
        Restaurant.DeleteAll();
        List<Cuisine> allCuisine = Cuisine.GetAll();
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object> {};
        model.Add("cuisine", allCuisine);
        model.Add("restaurants", allRestaurants);
        return View["index.cshtml", model];
      };
      Post["/cuisine/sortby"] = _ => {
        List<Cuisine> allCuisine = Cuisine.GetAll();
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        int sortId = Request.Form["cuisineSort"];
        allRestaurants[0].SetSortValue(sortId);
        Dictionary<string, object> model = new Dictionary<string, object> {};
        model.Add("cuisine", allCuisine);
        model.Add("restaurants", allRestaurants);
        return View["index.cshtml", model];
      };
      Post["/cuisine/new"] = _ => {
        Cuisine newCuisine = new Cuisine(Request.Form["cuisineName"]);
        newCuisine.Save();
        List<Cuisine> allCuisine = Cuisine.GetAll();
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object> {};
        model.Add("cuisine", allCuisine);
        model.Add("restaurants", allRestaurants);
        return View["index.cshtml", model];
      };
      Get["/cuisine/delete"] = _ => {
        List<Cuisine> allCuisine = Cuisine.GetAll();
        return View["remove_cuisine.cshtml", allCuisine];
      };
      Delete["/cuisine/delete/"] = _ => {
        int searchId = Request.Form["cuisineName"];
        Cuisine SelectedCuisine = Cuisine.Find(searchId);
        SelectedCuisine.Delete();
        List<Cuisine> allCuisine = Cuisine.GetAll();
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object> {};
        model.Add("cuisine", allCuisine);
        model.Add("restaurants", allRestaurants);
        return View["index.cshtml", model];
      };
      Get["/cuisine/delete/all"] = _ => {
        return View["delete_all_confirmation.cshtml"];
      };
      Post["/cuisine/delete/all/confirmation"] = _ => {
        if (Request.Form["confirm"]) Cuisine.DeleteAll();
        List<Cuisine> allCuisine = Cuisine.GetAll();
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object> {};
        model.Add("cuisine", allCuisine);
        model.Add("restaurants", allRestaurants);
        return View["index.cshtml", model];
      };
      Get["/cuisine/edit"] = _ => {
        List<Cuisine> allCuisine = Cuisine.GetAll();
        return View["edit_cuisine.cshtml", allCuisine];
      };

      Patch["/cuisine/edit"] = _ => {
        int searchId = Request.Form["cuisineName"];
        Cuisine SelectedCuisine = Cuisine.Find(searchId);
        SelectedCuisine.Update(Request.Form["newName"]);
        List<Cuisine> allCuisine = Cuisine.GetAll();
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object> {};
        model.Add("cuisine", allCuisine);
        model.Add("restaurants", allRestaurants);
        return View["index.cshtml", model];
      };

      // Get["/categories/{id}"] = parameters => {
      //   Dictionary<string, object> model = new Dictionary<string, object>();
      //   var SelectedCategory = Category.Find(parameters.id);
      //   var CategoryTasks = SelectedCategory.GetTasks();
      //   model.Add("category", SelectedCategory);
      //   model.Add("tasks", CategoryTasks);
      //   return View["category.cshtml", model];
      // };
    }
  }
}
