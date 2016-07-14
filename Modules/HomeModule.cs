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
        return View["index.cshtml"];
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
