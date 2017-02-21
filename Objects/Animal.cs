using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace AnimalShelter
{
    public class Animal
    {
        private string _name;
        private string _gender;
        private string _date;
        private string _breed;
        private int _typeId;
        private int _id;

        public Animal(string Name, string Gender, string Date, string Breed, int TypeId, int Id = 0)
        {
            _name = Name;
            _gender = Gender;
            _date = Date;
            _breed = Breed;
            _id = Id;
            _typeId = TypeId;
        }

        public override bool Equals(System.Object otherAnimal)
        {
            if (!(otherAnimal is Animal))
            {
                return false;
            }
            else
            {
                Animal newAnimal = (Animal) otherAnimal;
                bool idEquality = (this.GetId() == newAnimal.GetId());
                bool nameEquality = (this.GetName() == newAnimal.GetName());
                bool genderEquality = this.GetGender() == newAnimal.GetGender();
                bool dateEquality = (this.GetDate() == newAnimal.GetDate());
                bool breedEquality = (this.GetBreed() == newAnimal.GetBreed());
                bool typeIdEquality = this.GetTypeId() == newAnimal.GetTypeId();
                return (idEquality && nameEquality && genderEquality && dateEquality && breedEquality && typeIdEquality);
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

        public string GetGender()
        {
            return _gender;
        }

        public string GetBreed()
        {
            return _breed;
        }

        public int GetTypeId()
        {
            return _typeId;
        }

        public string GetDate()
        {
            return _date;
        }

        public static List<Animal> GetAll()
        {
            List<Animal> AllAnimals = new List<Animal>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM animal;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int animalId = rdr.GetInt32(0);
                string animalName = rdr.GetString(1);
                string animalGender = rdr.GetString(2);
                string animalDate = rdr.GetString(3);
                string animalBreed = rdr.GetString(4);
                int animalTypeId = rdr.GetInt32(5);
                Animal newAnimal = new Animal(animalName, animalGender, animalDate, animalBreed, animalTypeId, animalId);
                AllAnimals.Add(newAnimal);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return AllAnimals;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO animal (name, gender, date, breed, type_id) OUTPUT INSERTED.id VALUES (@AnimalName, @AnimalGender, @AnimalDate, @AnimalBreed, @AnimalTypeId);", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@AnimalName";
            nameParameter.Value = this.GetName();

            SqlParameter genderParameter = new SqlParameter();
            genderParameter.ParameterName = "@AnimalGender";
            genderParameter.Value = this.GetGender();

            SqlParameter dateParameter = new SqlParameter();
            dateParameter.ParameterName = "@AnimalDate";
            dateParameter.Value = this.GetDate();

            SqlParameter breedParameter = new SqlParameter();
            breedParameter.ParameterName = "@AnimalBreed";
            breedParameter.Value = this.GetBreed();

            SqlParameter typeIdParameter = new SqlParameter();
            typeIdParameter.ParameterName = "@AnimalTypeId";
            typeIdParameter.Value = this.GetTypeId();

            cmd.Parameters.Add(nameParameter);
            cmd.Parameters.Add(genderParameter);
            cmd.Parameters.Add(dateParameter);
            cmd.Parameters.Add(breedParameter);
            cmd.Parameters.Add(typeIdParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM animal;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
