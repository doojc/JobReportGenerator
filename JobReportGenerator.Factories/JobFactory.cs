using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobReportGenerator.Implementation;

namespace JobReportGenerator.Factories
{
    public class JobFactory
    {
        public static Job Create(JobData jobData)
        {
            var job = new Job(jobData);

            //job.Initialize();

            return job;
        }
    }
}
