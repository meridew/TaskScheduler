using ScheduledTaskAPI.Cron.Strategies; 

namespace ScheduledTaskAPI.Cron
{
    public class CronStrategyFactory
    {
        private readonly List<ICronStrategy> strategies;

        public CronStrategyFactory()
        {
            // ordering is important here as strategies are evaluated in order
            // it's possible for one strategy to match multiple cron patterns
            // which would incorrectly identify the cron pattern and create 
            // an invalid trigger or incorrect defined trigger

            strategies = new List<ICronStrategy>
            {
                new EveryXHoursStrategy(),
                new HourlyStrategy(),
                new EveryWeekDaysStrategy(),
                new WeeklyStrategy(),
                new EveryMonthDaysStrategy(),
                new MonthlyStrategy(),
            };
        }

        public ICronStrategy GetStrategy(string cron)
        {
            var cronParts = cron.Split(' ');

            foreach (var strategy in strategies)
            {
                if (strategy.CanHandle(cronParts))
                {
                    return strategy;
                }
            }

            throw new InvalidOperationException($"No strategy found to handle cron pattern: {cron}");
        }
    }
}
