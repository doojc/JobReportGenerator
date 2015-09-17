using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Configuration;

namespace JobReportGenerator.Implementation
{
    public class Job
    {
        #region Members

        private JobData _jobData;

        #endregion

        #region Properties
        
        public string Username { get; set; }
        public string JobType { get; set; }
        public string ServerName { get; set; }
        public string ServerVersion { get; set; }
        public string PrinterAddress { get; set; }
        public DateTime Timestamp { get; set; }
        public string File { get; set; }

        #endregion

        #region Constructors

        public Job() { }
        
        public Job(JobData jobData)
        {
            _jobData = jobData;

            Initialize();
        }

        #endregion

        public void Initialize()
        {
            try
            {
                Console.WriteLine("Job.Initialize() started.");

                if (_jobData.Parameters == null)
                    throw new ArgumentException("Job parameters are null or not initialized.");

                if (_jobData.Parameters.XPathSelectElement("username") == null)
                    throw new Exception("Parameter 'username' not found.");
                else
                    Username = _jobData.Parameters.XPathSelectElement("username").Value;

                if (_jobData.Parameters.XPathSelectElement("jobType") == null)
                    throw new Exception("Parameter 'jobType' not found.");
                else
                    JobType = _jobData.Parameters.XPathSelectElement("jobType").Value;

                if (_jobData.Parameters.XPathSelectElement("serverName") == null)
                    throw new Exception("Parameter 'serverName' not found.");
                else
                    ServerName = _jobData.Parameters.XPathSelectElement("serverName").Value;

                if (_jobData.Parameters.XPathSelectElement("serverVersion") == null)
                    throw new Exception("Parameter 'serverVersion' not found.");
                else
                    ServerVersion = _jobData.Parameters.XPathSelectElement("serverVersion").Value;

                if (_jobData.Parameters.XPathSelectElement("printerAddr") == null)
                    throw new Exception("Parameter 'printerAddr' not found.");
                else
                    PrinterAddress = _jobData.Parameters.XPathSelectElement("printerAddr").Value;

                if (_jobData.Parameters.XPathSelectElement("timestamp") == null)
                    throw new Exception("Parameter 'timestamp' not found.");
                else
                    Timestamp = DateTime.Parse(_jobData.Parameters.XPathSelectElement("timestamp").Value);

                if (_jobData.Parameters.XPathSelectElement("files/file") == null)
                    throw new Exception("Parameter 'file' not found.");
                else
                {
                    string jobFolder = ConfigurationManager.AppSettings.Get("JobReportGenerater.JobFolder");
                    File = jobFolder + "\\" + _jobData.Parameters.XPathSelectElement("files/file").Value;  
                }
                     
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Failed to initialize Job. Details: {0}", ex.ToString()));   
            }
        }
    }
}
