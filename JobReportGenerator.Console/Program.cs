using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobReportGenerator.Management;
using JobReportGenerator.Implementation;
using JobReportGenerator.Interfaces;
using JobReportGenerator.Storage;
using JobReportGenerator.Factories;

namespace JobReportGenerator.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine("*** DoojC Development ***\n");

            try
            {
                JobManager jobManager = new JobManager();

                jobManager.ImportJobs();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}
