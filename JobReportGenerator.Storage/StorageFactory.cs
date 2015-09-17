using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobReportGenerator.Interfaces;
using System.Configuration;


namespace JobReportGenerator.Storage
{
    public class StorageFactory
    {
        public static IStorage Create()
        {
            StorageType type = (StorageType)Enum.Parse(typeof(StorageType), ConfigurationManager.AppSettings.Get("JobReportGenerator.Factories.Storage"));

            switch (type)
            {
                case StorageType.Database:
                    return new DatabaseStorage();
                default:
                    throw new ArgumentException(String.Format("Unsupported storage type '{0}'.", type));
            }
        }
    }
}
