using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AnimalShelter
{
    public class AnimalShelterTest : IDisposable
    {
        public AnimalShelterTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=animal_shelter_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Equals_TestIfEqual_true()
        {
          //Arrange, Act
          Animal firstAnimal = new Animal("fred", "female", "2017-01-28", "pug", 1);
          Animal secondAnimal = new Animal("fred", "female", "2017-01-28", "pug", 1);

          //Assert
          Assert.Equal(firstAnimal, secondAnimal);
        }

        [Fact]
        public void Save_TestIfSaved_true()
        {
          //Arrange
          Animal firstAnimal = new Animal("fred", "female", "2017-01-28", "pug", 1);
          firstAnimal.Save();

          //Act
          List<Animal> result = Animal.GetAll();
          List<Animal> testAnimalList = new List<Animal> {firstAnimal};


          //Assert
          Assert.Equal(testAnimalList, result);
        }

        [Fact]
        public void GetAll_ReturnAll_ListAll()
        {
            Animal firstAnimal = new Animal("fred", "female", "2017-01-28", "pug", 1);
            Animal secondAnimal = new Animal("wilma", "male", "2017-02-14", "great dane", 1);
            firstAnimal.Save();
            secondAnimal.Save();

            List<Animal> testAnimalList = new List<Animal> {firstAnimal, secondAnimal};
            List<Animal> resultAnimalList = Animal.GetAll();
            Assert.Equal(testAnimalList, resultAnimalList);
        }

        [Fact]
        public void GetId_TestIfCanGetId_true()
        {
          //Arrange
          Animal testAnimal = new Animal("fred", "female", "2017-01-28", "pug", 1);
          testAnimal.Save();

          //Act
          Animal savedAnimal = Animal.GetAll()[0];

          int result = savedAnimal.GetId();
          int testId = testAnimal.GetId();

          //Assert
          Assert.Equal(testId, result);
        }

        [Fact]
        public void Find_FindAnimalById_true()
        {
            Animal testAnimal = new Animal("mr. snuggles", "male", "2017-02-12", "english bulldog", 1);
            testAnimal.Save();

            Animal foundAnimal = Animal.Find(testAnimal.GetId());
            Console.WriteLine("foundANimal= " + foundAnimal.GetName() + " " + foundAnimal.GetId());
            Console.WriteLine("testAnimal= " + testAnimal.GetName() + " " + testAnimal.GetId());
            Assert.Equal(testAnimal, foundAnimal);
        }

        [Fact]
        public void GetSortedBreedList_GetListSortedByBreed_true()
        {
            Animal firstAnimal = new Animal("fred", "female", "2017-01-28", "pug", 1);
            Animal secondAnimal = new Animal("wilma", "male", "2017-02-14", "great dane", 1);
            Animal thirdAnimal = new Animal("betty", "female", "2017-08-28", "golden retriever", 1);
            Animal fourthAnimal = new Animal("barney", "male", "2016-07-14", "pit bull", 1);
            firstAnimal.Save();
            secondAnimal.Save();
            thirdAnimal.Save();
            fourthAnimal.Save();

            List<Animal> testAnimalList = new List<Animal> {thirdAnimal, secondAnimal, fourthAnimal, firstAnimal};
            List<Animal> resultAnimalList = Animal.GetSortedBreedList();

            Assert.Equal(testAnimalList, resultAnimalList);
        }

        [Fact]
        public void GetSortedTypeList_GetListSortedByType_true()
        {
            Animal firstAnimal = new Animal("fred", "female", "2017-01-28", "pug", 1);
            Animal secondAnimal = new Animal("wilma", "male", "2017-02-14", "great dane", 2);
            Animal thirdAnimal = new Animal("betty", "female", "2017-08-28", "golden retriever", 4);
            Animal fourthAnimal = new Animal("barney", "male", "2016-07-14", "pit bull", 3);
            firstAnimal.Save();
            secondAnimal.Save();
            thirdAnimal.Save();
            fourthAnimal.Save();

            List<Animal> testAnimalList = new List<Animal> {firstAnimal, secondAnimal, fourthAnimal, thirdAnimal};
            List<Animal> resultAnimalList = Animal.GetSortedTypeList();

            Assert.Equal(testAnimalList, resultAnimalList);
        }

        [Fact]
        public void GetSortedTypeList_GetListSortedByDate_true()
        {
            Animal firstAnimal = new Animal("fred", "female", "2017-01-28", "pug", 1);
            Animal secondAnimal = new Animal("wilma", "male", "2017-02-14", "great dane", 2);
            Animal thirdAnimal = new Animal("betty", "female", "2017-08-28", "golden retriever", 4);
            Animal fourthAnimal = new Animal("barney", "male", "2016-07-14", "pit bull", 3);
            firstAnimal.Save();
            secondAnimal.Save();
            thirdAnimal.Save();
            fourthAnimal.Save();

            List<Animal> testAnimalList = new List<Animal> {fourthAnimal, firstAnimal, secondAnimal, thirdAnimal};
            List<Animal> resultAnimalList = Animal.GetSortedDateList();

            Assert.Equal(testAnimalList, resultAnimalList);
        }

        public void Dispose()
        {
            Animal.DeleteAll();
            // Type.DeleteAll();
        }
    }
}
