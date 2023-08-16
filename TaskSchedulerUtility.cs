using TaskScheduler = Microsoft.Win32.TaskScheduler;

namespace ScheduledTaskAPI
{
    public class TaskSchedulerUtility
    {
        private const string RootFolderName = "MDMAF";
        private readonly TaskScheduler.TaskService taskService;

        public TaskSchedulerUtility()
        {
            taskService = new TaskScheduler.TaskService();
        }

        public TaskScheduler.Task CreateTask(string taskName, string subFolder, TaskScheduler.Trigger trigger, TaskScheduler.Action action)
        {
            TaskScheduler.TaskDefinition taskDefinition = taskService.NewTask();
            taskDefinition.Triggers.Add(trigger);
            taskDefinition.Actions.Add(action);

            TaskScheduler.TaskFolder targetFolder = EnsureFolder(subFolder);

            return targetFolder.RegisterTaskDefinition(taskName, taskDefinition);
        }

        public TaskScheduler.TaskFolder EnsureFolder(string subFolder)
        {
            // Ensure root folder exists
            TaskScheduler.TaskFolder rootFolder = FolderExists(taskService.RootFolder, RootFolderName)
                ? taskService.GetFolder(RootFolderName)
                : taskService.RootFolder.CreateFolder(RootFolderName);

            // Ensure sub folder exists
            return FolderExists(rootFolder, subFolder)
                ? rootFolder.SubFolders[subFolder]
                : rootFolder.CreateFolder(subFolder);
        }

        private bool FolderExists(TaskScheduler.TaskFolder parentFolder, string folderName)
        {
            foreach (TaskScheduler.TaskFolder folder in parentFolder.SubFolders)
            {
                if (folder.Name == folderName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
