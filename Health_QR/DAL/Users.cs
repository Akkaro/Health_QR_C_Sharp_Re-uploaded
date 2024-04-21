using Health_QR.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Health_QR.DAL
{
    public class Users
    {
        private string _connectionString = "Server=DESKTOP-84OLRP2;Database=health_QR;User Id=sa;Password=Witcher2003";

        public User GetUserIdByName(string Name)
        {
            string commandText = $"SELECT * FROM [dbo].[AspNetUsers] WHERE UserName LIKE '%{Name}%'";

            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(commandText, connection as SqlConnection);

            List<User> users = new List<User>();

            using (var adapter = new SqlDataAdapter(command))
            {
                var resultTable = new DataTable();
                adapter.Fill(resultTable);

                foreach (var row in resultTable.AsEnumerable())
                {
                    User user = new User()
                    {
                        Id = row["Id"].ToString(),
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        UserName = row["UserName"].ToString(),

                    };
                    users.Add(user);
                }
            }
            return users.First();
        }
    }
}
