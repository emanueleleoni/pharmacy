using DAL.Model;
using Marcucci.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Marcucci
{
    public class MessagesRepository
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public IEnumerable<Message> GetAllMessages()
        {
            var messages = new List<Message>();
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"SELECT [Id], 
                [Rfid], [Cf], [Date], [Payment] FROM [dbo].[Messages] WHERE readed = 0", connection))
                {
                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        messages.Add(item: new Message
                        {
                            Id      = (int)reader["Id"],
                            Rfid    = reader["Rfid"] != DBNull.Value ? (string)reader["Rfid"] : "",
                            Cf      = reader["Cf"] != DBNull.Value ? (string)reader["Cf"] : "",
                            Payment = reader["Payment"] != DBNull.Value ? (string)reader["Payment"] : "",
                            Date    = Convert.ToDateTime(reader["Date"])
                        });
                    }
                }
            }
            return messages;
        }

        public void SetToReaded()
        {
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"UPDATE [dbo].[Messages] SET readed = 1 WHERE date <= @Now", connection))
                {
                    command.Parameters.Add("@Now", SqlDbType.DateTime);
                    command.Parameters["@Now"].Value = DateTime.Now;

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                MessagesHub.SendMessages();
            }
        }
    }
}