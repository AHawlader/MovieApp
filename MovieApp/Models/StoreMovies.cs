using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieApp.Models
{
    public class StoreMovies
    {
        public int StoreMovieId { get; set; }
        [DisplayName("Store")]
        public int StoreId { get; set; }
        [DisplayName("Movie Title")]
        public int MovieId { get; set; }
        public int Quintity { get; set; }
        [DisplayName("Create Date")]
        [DataType(DataType.Date)]
        public DateTime RelaseDate { get; set; }

        public Store Store { get; set; }
        public Movie Movie { get; set; }


    }
}