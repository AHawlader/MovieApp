using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieApp.Models;


namespace MovieApp.Manegers
{
    
    public class MovieManeger
    {
        public string ConnectionString => ConfigurationManager.ConnectionStrings["MovieDB"].ConnectionString;

        public List<Movie> GetAllMovies()
        {
            string query = "Select * from Movie order by Title Asc";
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand smd=new SqlCommand(query,connection);
            SqlDataReader sdr = smd.ExecuteReader();
            List<Movie> movies=new List<Movie>();
            while (sdr.Read())
            {
                var  m=new Movie();
                m.ID =(int) sdr["MovieId"];
                m.Title = sdr["Title"].ToString();
                m.ReleaseDate =(DateTime) sdr["RelaseDate"];
                m.Genre = sdr["Genre"].ToString();
                m.Price = (decimal) sdr["Price"];

                movies.Add(m);
            }
            connection.Close();
            return movies;
        }

        public Tuple<List<Movie>, int> PageListMovies(int page , int pagesize,string title)
        {
            int toalRow = 0;
            int offset = pagesize * (page - 1);
            SqlConnection connection=new SqlConnection(ConnectionString);
            connection.Open();
            string pageQuery= @"Select MovieId, Title, relaseDate,Genre,Price  from Movie 
                where Movie.Title like '%"+title+@"%' 
                Order by Title asc
                offset " + offset + @" rows
                fetch Next  "+pagesize+ @" row only;
                select count(m.MovieId) as total_row from Movie m where m.Title like '%" + title + @"%' ";
            SqlCommand cmd=new SqlCommand(pageQuery,connection);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Movie> movies = new List<Movie>();
            while (reader.Read())
            {
                var m = new Movie();
                m.ID = (int)reader["MovieId"];
                m.Title = reader["Title"].ToString();
                m.ReleaseDate = (DateTime)reader["RelaseDate"];
                m.Genre = reader["Genre"].ToString();
                m.Price = (decimal)reader["Price"];

                movies.Add(m);
            }
            if (reader.NextResult())
            {
                if (reader.Read())
                {
                    toalRow = (int) reader["total_row"];
                }
            }
            connection.Close();
            return new Tuple<List<Movie>, int>(movies, toalRow);


        }

        public bool HasMovieTitle(string title)
        {
            SqlConnection connection=new SqlConnection(ConnectionString);
            connection.Open();
            string query = "Select * from Movie Where Title='" + title + "'";
            SqlCommand cmd=new SqlCommand(query,connection);
            SqlDataReader reader = cmd.ExecuteReader();
            bool hasmovie = reader.Read();
            connection.Close();
            return hasmovie;
            
        }

        public bool InsertMovie(Movie movie)
        {
            if (HasMovieTitle(movie.Title))
            {
                return false;
            }
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            string query = "Insert Into Movie(Title,RelaseDate,Genre,Price)VALUES ('" +
                           movie.Title + "',cast('" + movie.ReleaseDate.Date.ToString("yyyy-MM-dd HH:mm:ss.ff") + "'as datetime),'" + movie.Genre + "','" + movie.Price + "')";

            SqlCommand cmd = new SqlCommand(query, connection);
            int rowEffected = cmd.ExecuteNonQuery();
            connection.Close();
            return rowEffected > 0;
        }

        public bool UpdateMovie(Movie movie)
        {
            
            string query = "Update Movie set Title='"+movie.Title+"',RelaseDate='"+movie.ReleaseDate.Date+"',Genre='"+movie.Genre+"',Price='"+movie.Price+"' Where MovieId=" +movie.ID;
            SqlConnection connection=new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand cmd=new SqlCommand(query,connection);
            int RowEffected = cmd.ExecuteNonQuery();
            connection.Close();
            return RowEffected > 0;
       
        }

        public Movie GetMovieById(int id)
        {
            var m = new Movie();
            string query = "Select * from Movie where MovieId=" + id;
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand smd = new SqlCommand(query, connection);
            SqlDataReader sdr = smd.ExecuteReader();
            if (sdr.Read())
            {
                m.ID = (int)sdr["MovieId"];
                m.Title = sdr["Title"].ToString();
                m.ReleaseDate = (DateTime)sdr["RelaseDate"];
                m.Genre = sdr["Genre"].ToString();
                m.Price = (decimal)sdr["Price"];
            }
            connection.Close();
            return m;
        }

        public bool DeleteMovie(Movie movie)
        {
            string query = "Delete Movie where MovieId=" + movie.ID;
            var con=new SqlConnection(ConnectionString);
            con.Open();
            var cmd=new SqlCommand(query,con);

            int rowEffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowEffected > 0;
        }

        public bool Details(Movie movie)
        {
            string query = "Select * from Movie where MovieId=" + movie.ID;
            var con=new SqlConnection(ConnectionString);
            con.Open();
            var  cmd=new SqlCommand(query,con);
            int rowEffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowEffected > 0;
        }
 
    }
}