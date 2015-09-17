using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;

namespace JobReportGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** DoojC Development ***");

            GetJobsXmlFiles();

            //XDocument jobXml = GetJobXml();

            //InsertJob(jobXml);

            Console.ReadLine();
        }

        public static FileInfo[] GetJobsXmlFiles()
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\Users\edin.dujso.API\Downloads\jobovi");
            FileInfo[] jobXmlFiles = dir.GetFiles("*.xml", SearchOption.AllDirectories);

            Console.WriteLine("Found {0} job files\n", jobXmlFiles.Length);

            foreach (FileInfo f in jobXmlFiles)
            {
                Console.WriteLine(f.Name);
                InsertJob(GetJobXml(f.FullName));
            }

            return jobXmlFiles;
        }

        public static XDocument GetJobXml(string jobXmlFile)
        {
            try
            {
                XDocument jobXml = XDocument.Load(jobXmlFile);
                return jobXml;
            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static void InsertJob(XDocument jobXml)
        {
            string username = jobXml.XPathSelectElement("data/username").Value;

            Console.WriteLine("Username: {0}", username);
            Console.WriteLine("Job type: {0}", jobXml.XPathSelectElement("data/").Value);
        }
    }
}
