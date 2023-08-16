using System;
using System.Collections.Generic;
using Microsoft.Win32.TaskScheduler;
using ScheduledTaskAPI.Cron;

namespace ScheduledTaskAPI
{
    class Program
    {
        static void Main()
        {
            // Define your tasks
            var tasksToSchedule = new List<ScheduledTaskInfo>
            {
                new ScheduledTaskInfo
                {
                    TaskName = "HourlyTask",
                    CronString = "0 * * * *",
                    ExePath = @"C:\path\to\hourly.exe",
                    FolderName = "HourlyTasks"
                },
                new ScheduledTaskInfo
                {
                    TaskName = "DailyTask",
                    CronString = "0 0 * * *",
                    ExePath = @"C:\path\to\daily.exe",
                    FolderName = "DailyTasks"
                },
                new ScheduledTaskInfo
                {
                    TaskName = "MonthlyTask",
                    CronString = "0 0 1 * *",
                    ExePath = @"C:\path\to\monthly.exe",
                    FolderName = "MonthlyTasks"
                }
            };

            var taskUtility = new TaskSchedulerUtility();
            var strategyFactory = new CronStrategyFactory();

            foreach (var taskInfo in tasksToSchedule)
            {
                var strategy = strategyFactory.GetStrategy(taskInfo.CronString);
                var trigger = strategy.ToTrigger(taskInfo.CronString.Split(' '), taskInfo.StartDate, taskInfo.EndDate);

                ExecAction action = new ExecAction(taskInfo.ExePath);

                taskUtility.CreateTask(taskInfo.TaskName, taskInfo.FolderName, trigger, action);
            }
        }
    }

    public class ScheduledTaskInfo
    {
        public string TaskName { get; set; }
        public string CronString { get; set; }
        public string ExePath { get; set; }
        public string FolderName { get; set; }
        public DateTime? StartDate { get; set; }  // Optional start date
        public DateTime? EndDate { get; set; }    // Optional end date
    }

}
