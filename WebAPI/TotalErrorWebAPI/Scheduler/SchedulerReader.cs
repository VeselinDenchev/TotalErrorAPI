namespace TotalErrorWebAPI.Scheduler
{
    using Constants;

    using Data.Services.DtoModels;
    using Data.Services.Implementations;
    using Data.TotalErrorDbContext;

    using Quartz;

    [DisallowConcurrentExecution]
    public class SchedulerReader : IJob
    {
        private readonly ILogger<SchedulerReader> logger;

        public SchedulerReader(ILogger<SchedulerReader> logger)
        {
             this.logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            this.logger.LogInformation(SchedulerReaderConstant.TASK_STARTED_MESSAGE);

            BaseService baseService = new BaseService(new TotalErrorDbContext());
            Dictionary<string, List<TransferModel>> transferModelsByFile = baseService
                                                                            .ReadFilesFromDirectory(SchedulerReaderConstant.READ_DIRECTORY);

            DataObject data = baseService.Convert(transferModelsByFile);
            List<string> dates = new List<string>();

            foreach (string date in transferModelsByFile.Keys)
            {
                dates.Add(date);
            }

            List<DateTime> dateTimeList = new List<DateTime>();
            foreach (string date in dates)
            {
                DateTime dateTime = DateTime.Parse(date);
                dateTimeList.Add(dateTime);
            }
            data.LastReadFiles = dateTimeList;

            baseService.Convert(transferModelsByFile);
            baseService.SaveDataToDatabase(data);

            this.logger.LogInformation(SchedulerReaderConstant.TASK_ENDED_MESSAGE);
        }
    }
}
