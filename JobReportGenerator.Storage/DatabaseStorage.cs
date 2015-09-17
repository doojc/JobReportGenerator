using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using JobReportGenerator.Interfaces;
using JobReportGenerator.Factories;
using JobReportGenerator.Implementation;
using System.Data.SqlClient;
using System.Data;

namespace JobReportGenerator.Storage
{
    public class DatabaseStorage : IStorage
    {
        #region Members

        private string _connectionString;

        #endregion

        #region Constructors

        public DatabaseStorage()
        {
            _connectionString = ConfigurationManager.AppSettings.Get("JobReportGenerator.Database.ConnectionString");
        }

        #endregion

        #region Methods

        public bool ImportJob(out string status, Job job)
        {
            StringBuilder messages = new StringBuilder();

            try
            {
                if (job == null)
                    throw new ArgumentException("Job parameter is null or not initialized.");

                using ( SqlConnection connection = new SqlConnection(_connectionString) )
                {
                    connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        using (SqlCommand command = new SqlCommand("[dbo].[Job_Import]", connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@Username", job.Username);
                            command.Parameters.AddWithValue("@JobType", job.JobType);
                            command.Parameters.AddWithValue("@ServerName", job.ServerName);
                            command.Parameters.AddWithValue("@ServerVersion", job.ServerVersion);
                            command.Parameters.AddWithValue("@PrinterAddress", job.PrinterAddress);
                            command.Parameters.AddWithValue("@Timestamp", job.Timestamp);
                            command.Parameters.AddWithValue("@File", job.File);

                            int result = command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                messages.AppendLine(String.Format("Failed to import job in database. Details: {0}", ex.ToString()));

                return false;
            }
            finally
            {
                status = messages.ToString();
            }
        }

        #endregion

    }
}
