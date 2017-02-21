using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AnimalShelter
{
    public class TypeTest : IDisposable
    {
        public TypeTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=animal_shelter_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void GetAll_EmptyAtFirst_true()
        {
            int result = Type.GetAll().Count;

            Assert.Equal(0, result);
        }

        [Fact]
        public void Equals_ReturnsTrueForSameName_true()
        {
            //Arrange, Act
            Type firstType = new Type("Dog");
            Type secondType = new Type("Dog");

            //Assert
            Assert.Equal(firstType, secondType);
        }

        [Fact]
        public void Save_TestIfTypeSaved_true()
        {
            Type testType = new Type("Dog");
            testType.Save();

            List<Type> result = Type.GetAll();
            List<Type> testList = new List<Type>{testType};

            Assert.Equal(testList, result);
        }

        [Fact]
        public void GetId_GetsIdForType_true()
        {
            //Arrange
            Type testType = new Type("Cat");
            testType.Save();

            //Act
            Type savedType = Type.GetAll()[0];

            int result = savedType.GetId();
            int testId = testType.GetId();

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Find_FindsCategoryInDatabase_true()
        {
            //Arrange
            Type testType = new Type("Cat");
            testType.Save();

            //Act
            Type foundType = Type.Find(testType.GetId());

            //Assert
            Assert.Equal(testType, foundType);
        }



        public void Dispose()
        {
            Type.DeleteAll();
            Animal.DeleteAll();
        }


    }

}
