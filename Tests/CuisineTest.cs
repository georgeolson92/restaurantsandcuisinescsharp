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
    public void Test_FindFindsTaskInDatabase()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("doughnut related tourist trap");
      testCuisine.Save();

      //Act
      Cuisine foundCuisine = Cuisine.Find(testCuisine.GetId());

      //Assert
      Assert.Equal(testCuisine, foundCuisine);
    }

    [Fact]
    public void Test_Update_UpdatesCuisineInDatabase()
    {
      //Arrange
      string name = "doughnut related tourist trap";
      Cuisine testCuisine = new Cuisine(name);
      testCuisine.Save();
      string newName = "ice cream related tourist trap";

      //Act
      testCuisine.Update(newName);

      string result = testCuisine.GetName();

      //Assert
      Assert.Equal(newName, result);
    }

    [Fact]
    public void Test_Delete_DeletesCuisineFromDatabase()
    {
      //Arrange
      string name1 = "Chinese";
      Cuisine testCuisine1 = new Cuisine(name1);
      testCuisine1.Save();

      string name2 = "ice cream";
      Cuisine testCuisine2 = new Cuisine(name2);
      testCuisine2.Save();

      //Act
      testCuisine1.Delete();
      List<Cuisine> resultCuisine = Cuisine.GetAll();
      List<Cuisine> testCuisineList = new List<Cuisine> {testCuisine2};

      //Assert
      Assert.Equal(testCuisineList, resultCuisine);
    }

    // [Fact]
    // public void Test_UpdateId_updateIdToNewNumber()
    // {
    //   //Arrange
    //   string name = "doughtnut";
    //   Cuisine testCuisine = new Cuisine(name);
    //   testCuisine.Save();
    //   int newId = 3;
    //
    //   //Act
    //   testCuisine.UpdateId(newId);
    //
    //   int result = testCuisine.GetId();
    //
    //   //Assert
    //   Assert.Equal(newId, result);
    // }
  }
}
