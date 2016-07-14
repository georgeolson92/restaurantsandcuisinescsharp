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
      Cuisine.DeleteAll();
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Cuisine.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfNamesAreTheSame()
    {
      //Arrange, Act
      Cuisine firstCuisine = new Cuisine("steakhouse", 1);
      Cuisine secondCuisine = new Cuisine("steakhouse", 1);

      //Assert
      Assert.Equal(firstCuisine, secondCuisine);
    }
    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("doughnut related tourist trap");

      //Act
      testCuisine.Save();
      List<Cuisine> result = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine>{testCuisine};


      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_CreateNewCuisine()
    {
      //Arrange, Act
      Cuisine testCuisine = new Cuisine("American diner breakfast");
      string expectedResult = "American diner breakfast";
      string result = testCuisine.GetName();
      //Assert
      Assert.Equal(expectedResult, result);
    }

  }
}
