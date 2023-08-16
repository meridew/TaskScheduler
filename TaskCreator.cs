using TaskScheduler = Microsoft.Win32.TaskScheduler;
using ScheduledTaskAPI.Cron;
using Microsoft.Win32.TaskScheduler;

namespace ScheduledTaskAPI
{
    public class TaskCreator
    {
        private readonly CronStrategyFactory strategyFactory;
        private readonly TaskSchedulerUtility taskUtility;

        public TaskCreator()
        {
            strategyFactory = new CronStrategyFactory();
            taskUtility = new TaskSchedulerUtility();
        }

        public TaskScheduler.Task CreateScheduledTask(string taskName, string subFolder, string cronString, string exePath, DateTime? start = null, DateTime? end = null)
        {
            var trigger = GetTriggerFromCron(cronString, start, end);
            ExecAction action = new ExecAction(exePath);

            return taskUtility.CreateTask(taskName, subFolder, trigger, action);
        }

        public TaskScheduler.Trigger GetTriggerFromCron(string cronString, DateTime? start = null, DateTime? end = null)
        {
            var strategy = strategyFactory.GetStrategy(cronString);
            return strategy.ToTrigger(cronString.Split(' '), start, end);
        }
    }
}
