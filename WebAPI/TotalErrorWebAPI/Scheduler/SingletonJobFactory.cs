namespace TotalErrorWebAPI.Scheduler
{
    using Quartz;
    using Quartz.Spi;

    public class SingletonJobFactory : IJobFactory
    {
        private readonly IServiceProvider serviceProvider;

        public SingletonJobFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return this.serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job) { }
    }
}
