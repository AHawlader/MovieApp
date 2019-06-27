using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MovieApp.Models;

namespace MovieApp.Manegers
{
    public class StoreMenager
    {
        public string ConnectionString => ConfigurationManager.ConnectionStrings["MovieDb"].ConnectionString;

        public List<Store> GetAllStore()
        {
            string query = "Select * from Store";
            var  connection=new SqlConnection(ConnectionString);
            connection.Open();
            var command=new SqlCommand(query,connection);
            SqlDataReader reader = command.ExecuteReader();

            List<Store> stores=new List<Store>();
            while (reader.Read())
            {
                var store =new Store();
                store.StoreId = (int) reader["StoreId"];
                store.StoreName = reader["StoreName"].ToString();
                store.Address = reader["Address"].ToString();
                store.City = reader["City"].ToString();
                store.ZipCode = reader["ZipCode"].ToString();
                store.Country = reader["Country"].ToString();
                stores.Add(store);
            }
            return stores;
        }

        public bool InsertStore(Store store)
        {
            string query = "Insert into Store(StoreName,Address,City,ZipCode,Country)Values('" + store.StoreName + "','" + store.Address + "','" + store.City + "','" +
                           store.ZipCode + "','" + store.Country + "')";
            var connection=new SqlConnection(ConnectionString);
            connection.Open();
            var  command=new SqlCommand(query,connection);
            int rowEffected = command.ExecuteNonQuery();
            connection.Close();
            return rowEffected > 0;

        }

        public Store GetStoreById(int id)
        {
            Store store = new Store();
            string query = "Select * from Store Where StoreId=" + id;
            var  con=new SqlConnection(ConnectionString);
            con.Open();
            var cmd=new SqlCommand(query,con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                
                store.StoreId =(int)reader["StoreId"];
                store.StoreName = reader["StoreName"].ToString();
                store.Address = reader["Address"].ToString();
                store.City = reader["City"].ToString();
                store.ZipCode = reader["ZipCode"].ToString();
                store.Country = reader["Country"].ToString();
            }
            con.Close();
            return store;
        }

        public bool UpdateStore(Store store)
        {
            string query = "Update Store set StoreName='" + store.StoreName + "',Address='" + store.Address +
                           "',City='" + store.City + "',ZipCode='" + store.ZipCode + "',Country='" + store.Country +
                           "'Where StoreId=" + store.StoreId;
            var con=new SqlConnection(ConnectionString);
            con.Open();
            var cmd=new  SqlCommand(query,con);
            int rowEffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowEffected > 0;
        }

        public bool DeleteStore(Store store)
        {
            string query = "Delete Store where StoreId=" + store.StoreId;
            var con = new SqlConnection(ConnectionString);
            con.Open();
            var cmd = new SqlCommand(query, con);

            int rowEffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowEffected > 0;
        }

        public bool Details(Store store)
        {
            string query = "Select *from Store StoreId=" + store.StoreId;
            var con=new SqlConnection(ConnectionString);
            con.Open();
            var cmd=new SqlCommand(query,con);
            int rowEffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowEffected > 0;
        }

    }
}