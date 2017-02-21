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





        public void Dispose()
        {
            Type.DeleteAll();
            Animal.DeleteAll();
        }


    }

}
