using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace AnimalShelter
{
    public class Type
    {
        private int _id;
        private string _name;

        public Type(string Name, int Id = 0)
        {
            _id = Id;
            _name = Name;
        }

        public override bool Equals(System.Object otherType)
        {
            if (!(otherType is Type))
            {
                return false;
            }
            else
            {
                Type newType = (Type) otherType;
                bool idEquality = this.GetId() == newType.GetId();
                bool nameEquality = this.GetName() == newType.GetName();
                return (idEquality && nameEquality);
            }
        }

        public int GetId()
        {
            return _id;
        }
        public string GetName()
        {
            return _name;
        }

        public static List<Type> GetAll()
        {
            List<Type> allTypes = new List<Type>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM type;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                int typeId = rdr.GetInt32(0);
                string typeName = rdr.GetString(1);
                Type newType = new Type(typeName, typeId);
                allTypes.Add(newType);
            }

            if (rdr != null)
            {
                rdr.Close();
            }

            if (conn != null)
            {
                rdr.Close();
            }

            return allTypes;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO type (name) OUTPUT INSERTED.id VALUES (@TypeName);", conn);

            SqlParameter nameParameter = new SqlParameter("@TypeName", this.GetName());
            // nameParameter.ParameterName = "@TypeName";
            // nameParameter.Value = this.GetName();
            cmd.Parameters.Add(nameParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
        }

        public static Type Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM type WHERE id = @TypeId;", conn);
            SqlParameter typeIdParameter = new SqlParameter();
            typeIdParameter.ParameterName = "@TypeId";
            typeIdParameter.Value = id.ToString();
            cmd.Parameters.Add(typeIdParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            int foundTypeId = 0;
            string foundTypeName = null;

            while(rdr.Read())
            {
                foundTypeId = rdr.GetInt32(0);
                foundTypeName = rdr.GetString(1);
            }
            Type foundType = new Type(foundTypeName, foundTypeId);

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return foundType;
        }

        public List<Animal> GetAnimals()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM animal WHERE type_id = @TypeId;", conn);
            SqlParameter typeIdParameter = new SqlParameter();
            typeIdParameter.ParameterName = "@TypeId";
            typeIdParameter.Value = this.GetId();
            cmd.Parameters.Add(typeIdParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            List<Animal> animals = new List<Animal> {};
            while(rdr.Read())
            {
            int animalId = rdr.GetInt32(0);
            string animalName = rdr.GetString(1);
            string animalGender = rdr.GetString(2);
            string animalDate = rdr.GetString(3);
            string animalBreed = rdr.GetString(4);
            int animalTypeId = rdr.GetInt32(5);
            Animal newAnimal = new Animal(animalName, animalGender, animalDate, animalBreed, animalTypeId, animalId);
            animals.Add(newAnimal);
            }
            if (rdr != null)
            {
            rdr.Close();
            }
            if (conn != null)
            {
            conn.Close();
            }
            return animals;
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM type;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }
}
