using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobReportGenerator.Implementation;
using JobReportGenerator.Interfaces;
using JobReportGenerator.Storage;
using JobReportGenerator.Factories;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Configuration;

namespace JobReportGenerator.Management
{
    public class JobManager
    {
        #region Members

        private IStorage _storage;

        #endregion

        #region Constructors

        public JobManager()
        {
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            _storage = StorageFactory.Create();
        }

        public void ImportJobs()
        {
            StringBuilder messages = new StringBuilder();

            List<Job> jobs = new List<Job>();

            try 
	        {
                string jobDirectory = ConfigurationManager.AppSettings.Get("JobReportGenerater.JobFolder");

                DirectoryInfo dir = new DirectoryInfo(jobDirectory);

                FileInfo[] jobXmlFiles = dir.GetFiles("*.xml", SearchOption.AllDirectories);

                
                    foreach (FileInfo f in jobXmlFiles)
                    {
                        try
                        {
                            Console.WriteLine(f.Name);

                            XDocument jobXml = XDocument.Load(f.FullName);

                            XElement jobParameters = jobXml.Element("data");

                            JobData jd = new JobData()
                            {
                                Parameters = jobParameters
                            };

                            Job job = JobFactory.Create(jd);

                            jobs.Add(job);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }           


                try
                {
                    foreach (Job j in jobs)
                    {
                        string loadOperationDetails;
                        bool success = _storage.ImportJob(out loadOperationDetails, j);

                        if (!success)
                            throw new Exception(String.Format("Failed to import job. Details: '{0}'", loadOperationDetails));                     
                    } 
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }                              
 
	        }
	        catch (Exception ex)
	        {
                Console.WriteLine(ex.Message);
	        }
        }

        #endregion
    }
}
