using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Restaurants
{
  public class RestaurantsTest : IDisposable
  {
    public RestaurantsTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=restaurants_tests;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Restaurant.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfNamesAreTheSame()
    {
      //Arrange, Act
      Restaurant firstRestaurant = new Restaurant("Little Big Burger", 1);
      Restaurant secondRestaurant = new Restaurant("Little Big Burger", 1);

      //Assert
      Assert.Equal(firstRestaurant, secondRestaurant);
    }
    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Burgerville");

      //Act
      testRestaurant.Save();
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_FindFindsRestaurantInDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Five Guys");
      testRestaurant.Save();
      //Act
      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());
      //Assert
      Assert.Equal(testRestaurant, foundRestaurant);
    }

    [Fact]
    public void Test_Update_UpdatesRestaurantInDatabase()
    {
      //Arrange
      string name = "Out 'n In";
      Restaurant testRestaurant = new Restaurant(name);
      testRestaurant.Save();
      string newName = "In 'n Out'";

      //Act
      testRestaurant.Update(newName);

      string result = testRestaurant.GetName();

      //Assert
      Assert.Equal(newName, result);
    }

    [Fact]
    public void Test_Delete_DeletesRestaurantFromDatabase()
    {
      //Arrange
      Restaurant testRestaurant1 = new Restaurant("Shari's");
      testRestaurant1.Save();

      Restaurant testRestaurant2 = new Restaurant("Denny's");
      testRestaurant2.Save();

      //Act
      testRestaurant1.Delete();
      List<Restaurant> resultRestaurant = Restaurant.GetAll();
      List<Restaurant> testRestaurantList = new List<Restaurant> {testRestaurant2};

      //Assert
      Assert.Equal(testRestaurantList, resultRestaurant);
    }


  }
}
