using AppCore.Entities;
using AppCore.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Infrastructure
{
    public class PostRepositoryADO : IPostRepository
    {
        //Inject the database connection string from appsettings.json file
        private readonly IConfiguration _config;
        private string connStr;

        public PostRepositoryADO(IConfiguration config)
        {
            _config = config;
            connStr = _config.GetConnectionString("DefaultConnection");
        }

        private string selectAllQuery = "SELECT DISTINCT p.*, AVG(r.Score) average_rating, COUNT(r.Id) reviews "
            + "FROM Posts p LEFT OUTER JOIN Ratings r "
            + "ON p.Id = r.PostId GROUP BY p.Id, p.Author, p.Title, p.Permalink, p.PostContent, p.CreateDate, p.UpdateDate ";

        private string selectByIdClause = "WHERE ID = @id";

        private string insertQuoteQuery = "INSERT INTO Posts "
            + "(AuthorFirstName, AuthorLastName, Quote) "
            + "VALUES(@FirstName, @LastName, @Quote)";

        private string updateQuery = "UPDATE Quotes "
            + "SET AuthorFirstName = @FirstName, "
            + "AuthorLastName = @LastName, "
            + "Quote = @Quote ";

        private string deleteQuery = "DELETE FROM Posts ";

        public List<Post> GetAll()
        {
            List<Post> posts = new List<Post>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand command = new SqlCommand(selectAllQuery, conn);
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        decimal rating = 0M;
                        if (!reader.IsDBNull(7))
                        {
                            rating = reader.GetDecimal(7);
                        }
                        Post returnedPost = new Post
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Title = reader[1].ToString(),
                            Author = reader[2].ToString(),
                            Permalink = reader[3].ToString(),
                            PostContent = reader[4].ToString(),
                            CreateDate = reader.GetDateTime(5),
                            UpdateDate = reader.GetDateTime(6),
                            AverageRating = rating,
                            RatingsCount = int.Parse(reader[8].ToString())
                        };

                        posts.Add(returnedPost);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

                return posts;
        }

        public void Create(Post post)
        {
            throw new NotImplementedException();
        }

        public void CreateRating(Rating rating)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public decimal GetAvgRating()
        {
            throw new NotImplementedException();
        }

        public Post GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int GetRatingCount()
        {
            throw new NotImplementedException();
        }

        public void Update(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
