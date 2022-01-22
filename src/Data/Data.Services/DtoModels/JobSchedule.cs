namespace Data.Services.DtoModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class JobSchedule
    {
        public JobSchedule(Type jobType, string cronExpression)
        {
            this.JobType = jobType;
            this.CronExpression = cronExpression;
        }

        public Type JobType { get; }

        public string CronExpression { get; }
    }
}
