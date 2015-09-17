using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobReportGenerator.Implementation;

namespace JobReportGenerator.Interfaces
{
    public interface IStorage
    {
        bool ImportJob(out string status, Job job);
    }
}
