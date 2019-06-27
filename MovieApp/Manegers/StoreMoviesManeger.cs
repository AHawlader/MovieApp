using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Web;
using MovieApp.Models;

namespace MovieApp.Manegers
{
    public class StoreMoviesManeger
    {
        public string ConnnectionString => ConfigurationManager.ConnectionStrings["MovieDB"].ConnectionString;

        public List<StoreMovies> GetAllStoreMovies(string StoreName=null)

        {
            string query = @" Select StoreMovies.StoreMoviesId,Store.StoreName,Store.StoreId,Movie.MovieId, Movie.Title,StoreMovies.Quntity,StoreMovies.RelaseDate 
                     from StoreMovies
                     inner join Movie on StoreMovies.MovieId=Movie.MovieId
                     inner join Store on StoreMovies.StoreId=Store.StoreId 
					 where Store.StoreName like'%"+StoreName+"%'";
            SqlConnection connection=new SqlConnection(ConnnectionString);
            
            connection.Open();
            SqlCommand cmd=new SqlCommand(query,connection);
            SqlDataReader reader = cmd.ExecuteReader();
            List<StoreMovies>storeMovieses=new List<StoreMovies>();
            while (reader.Read())
            {
                
                StoreMovies movies=new StoreMovies();
                movies.StoreMovieId = (int) reader["StoreMoviesId"];
                movies.StoreId = (int) reader["StoreId"];
                movies.Store=new Store();
                movies.Store.StoreName = reader["StoreName"].ToString();
                movies.MovieId = (int)reader["MovieId"];
                movies.Movie=new Movie();
                movies.Movie.Title = reader["Title"].ToString();
                movies.Quintity = (int) reader["Quntity"];
                movies.RelaseDate = (DateTime) reader["RelaseDate"];
                storeMovieses.Add(movies);
            }
            connection.Close();
            return storeMovieses;
        }

        public bool InsertMovies(StoreMovies storeMovies)
        {
            string query = "Insert Into StoreMovies(StoreId,MovieId,Quntity,RelaseDate)values('" + storeMovies.StoreId + "','"+storeMovies.MovieId+"','" +
                           storeMovies.Quintity + "', cast('" + storeMovies.RelaseDate.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' as datetime))";

            var con = new SqlConnection(ConnnectionString);
            con.Open();
            var cmd = new SqlCommand(query, con);
            int rowEffected = cmd.ExecuteNonQuery();
            
            con.Close();
            return rowEffected > 0;
        }

        public StoreMovies GetStoreMovieByID(int id)
        {
            StoreMovies movies = null;
            string query = @"select StoreMoviesId,Store.StoreId,Store.StoreName,Movie.MovieId,Movie.Title,StoreMovies.Quntity,StoreMovies.RelaseDate 
            from StoreMovies
            join Store on Store.StoreId = StoreMovies.StoreId
            join Movie on Movie.MovieId = StoreMovies.MovieId
            where StoreMoviesId =" + id;

            var con=new SqlConnection(ConnnectionString);
            con.Open();
            var command=new SqlCommand(query,con);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                movies=new StoreMovies();
                movies.StoreMovieId = (int)reader["StoreMoviesId"];
                movies.StoreId = (int)reader["StoreId"];
                movies.Store = new Store();
                movies.Store.StoreName = reader["StoreName"].ToString();
                movies.MovieId = (int)reader["MovieId"];
                movies.Movie=new Movie();
                movies.Movie.Title = reader["Title"].ToString();
                movies.Quintity = (int)reader["Quntity"];
                movies.RelaseDate = (DateTime)reader["RelaseDate"];
                
            }
            con.Close();
            return movies;
        }

        public bool UpdateStoreMovie(StoreMovies storeMovies)
        {
            string query = "Update StoreMovies set StoreId='" + storeMovies.StoreId + "',MovieId='" + storeMovies.MovieId +
                           "',Quntity='" + storeMovies.Quintity + "' Where StoreMoviesId=" + storeMovies.StoreMovieId;
            SqlConnection connection=new SqlConnection(ConnnectionString);
            connection.Open();
            SqlCommand cmd=new SqlCommand(query,connection);
            int rowEffected = cmd.ExecuteNonQuery();
            connection.Close();
            return rowEffected > 0;
        }

        public bool DeleteStoreMovie(StoreMovies storeMovies)
        {
            string query = "Delete StoreMovies Where StoreMoviesId=" + storeMovies.StoreMovieId;
            SqlConnection connection=new SqlConnection(ConnnectionString);
            connection.Open();
            SqlCommand cmd=new SqlCommand(query,connection);
            int rowEffected = cmd.ExecuteNonQuery();
            connection.Close();
            return rowEffected > 0;
        }

        public bool DetailsStoreMovies(StoreMovies storeMovies)
        {
            string query = "Select * From StoreMovies where StoreMoivesId=" + storeMovies.StoreMovieId;
            SqlConnection con=new SqlConnection(ConnnectionString);
            con.Open();
            SqlCommand cmd=new SqlCommand(query,con);
            int rowEffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowEffected > 0;
        }
        
    }
}