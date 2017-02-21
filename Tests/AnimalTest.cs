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
        public void Test_EqualOverrideTrueForSameDescription()
        {
          //Arrange, Act
          Animal firstAnimal = new Animal("fred", "female", "2017-01-28", "pug", 1);
          Animal secondAnimal = new Animal("fred", "female", "2017-01-28", "pug", 1);

          //Assert
          Assert.Equal(firstAnimal, secondAnimal);
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

        public void Dispose()
        {
            Animal.DeleteAll();
            // Type.DeleteAll();
        }
    }
}
