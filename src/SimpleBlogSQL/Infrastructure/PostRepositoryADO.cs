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
            + "ON p.Id = r.PostId ";

        private string groupByClause = "GROUP BY p.Id, p.Author, p.Title, p.Permalink, p.PostContent, p.CreateDate, p.UpdateDate";

        private string selectByIdClause = "WHERE p.Id = @id ";

        private string selectByPermalinkClause = "WHERE p.Permalink = @Permalink ";

        private string insertQuery = "INSERT INTO Posts "
            + "(Title, Author, Permalink, PostContent) "
            + "VALUES(@Title, @Author, @Permalink, @PostContent)";

        private string updateQuery = "UPDATE Posts "
            + "SET Title = @Title, "
            + "Author = @Author, "
            + "Permalink = @Permalink, "
            + "PostContent = @PostContent, "
            + "UpdateDate = @UpdateDate "
            + "WHERE Id = @Id";

        private string deleteQuery = "DELETE FROM Posts WHERE Id = @id";

        public List<Post> GetAll()
        {
            List<Post> posts = new List<Post>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand command = new SqlCommand(selectAllQuery + groupByClause, conn);
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

        public Post GetById(int id)
        {
            Post postById = new Post();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand command = new SqlCommand(selectAllQuery + selectByIdClause + groupByClause, conn);
                    command.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.IsDBNull(0))
                        {
                            throw new Exception("No Data Returned by Query");
                        }
                        decimal rating = 0M;
                        if (!reader.IsDBNull(7))
                        {
                            rating = reader.GetDecimal(7);
                        }
                        postById = new Post
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
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return postById;
        }

        public Post GetByPermalink(string permalink)
        {
            Post postByPermalink = new Post();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand command = new SqlCommand(selectAllQuery + selectByPermalinkClause + groupByClause, conn);
                    command.Parameters.AddWithValue("@Permalink", permalink);
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.IsDBNull(0))
                        {
                            throw new Exception("No Data Returned by Query");
                        }
                        decimal rating = 0M;
                        if (!reader.IsDBNull(7))
                        {
                            rating = reader.GetDecimal(7);
                        }
                        postByPermalink = new Post
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
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return postByPermalink;
        }

        public void Create(Post post)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertQuery, conn);
                    command.Parameters.AddWithValue("@Title", post.Title);
                    command.Parameters.AddWithValue("@Author", post.Author);
                    command.Parameters.AddWithValue("@Permalink", post.Permalink);
                    command.Parameters.AddWithValue("@PostContent", post.PostContent);
                    conn.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public void Update(Post post)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand command = new SqlCommand(updateQuery, conn);
                    command.Parameters.AddWithValue("@Id", post.Id);
                    command.Parameters.AddWithValue("@Title", post.Title);
                    command.Parameters.AddWithValue("@Author", post.Author);
                    command.Parameters.AddWithValue("@Permalink", post.Permalink);
                    command.Parameters.AddWithValue("@PostContent", post.PostContent);
                    command.Parameters.AddWithValue("@UpdateDate", DateTime.Now);
                    conn.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand command = new SqlCommand(deleteQuery, conn);
                    command.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void CreateRating(Rating rating)
        {
            throw new NotImplementedException();
        }

        public decimal GetAvgRating()
        {
            throw new NotImplementedException();
        }

        public int GetRatingCount()
        {
            throw new NotImplementedException();
        }
    }
}
