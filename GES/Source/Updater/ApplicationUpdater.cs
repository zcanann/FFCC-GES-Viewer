namespace GES.Source.Updater
{
    using GES.Engine.Common;
    using GES.Engine.Common.Logging;
    using GES.Source.Tasks;
    using Squirrel;
    using Squirrel.Sources;
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for automatically downloading and applying application updates.
    /// </summary>
    static class ApplicationUpdater
    {
        /// <summary>
        /// The url for the Github repository from which updates are fetched.
        /// </summary>
        private static readonly String GithubRepositoryUrl = "https://github.com/GES/GES";

        /// <summary>
        /// Fetches and applies updates from the Github repository. The application is not restarted. Refer to https://github.com/clowd/Clowd.Squirrel for details.
        /// </summary>
        public static void UpdateApp()
        {
            if (!GESSettings.AutomaticUpdates)
            {
                Logger.Log(LogLevel.Info, "Automatic updates disabled. GES will not check for updates this session.");
                return;
            }

            SquirrelAwareApp.HandleEvents(
                onInitialInstall: ApplicationUpdater.OnAppInstall,
                onAppUninstall: ApplicationUpdater.OnAppUninstall,
                onEveryRun: ApplicationUpdater.OnAppRun);

            if (!ApplicationUpdater.IsSquirrelInstalled())
            {
                Logger.Log(LogLevel.Warn, "Updater not found. Automatic updates will not be available.");
                return;
            }

            Task.Run(async () =>
            {
                try
                {
                    using (UpdateManager manager = new UpdateManager(new GithubSource(ApplicationUpdater.GithubRepositoryUrl, String.Empty, false)))
                    {
                        UpdateInfo updates = await manager.CheckForUpdate();

                        TrackableTask<Boolean> checkForUpdatesTask = TrackableTask<Boolean>
                            .Create("Checking for Updates", out UpdateProgress updateProgress, out CancellationToken cancellationToken)
                            .With(Task<Boolean>.Run(
                            () =>
                            {
                                try
                                {
                                    manager.CheckForUpdate(false, (progress) => updateProgress(progress)).Wait();
                                }
                                catch (Exception ex)
                                {
                                    Logger.Log(LogLevel.Error, "Error checking for application updates.", ex);
                                    return false;
                                }

                                return true;
                            },
                            cancellationToken));

                        TaskTrackerViewModel.GetInstance().TrackTask(checkForUpdatesTask);

                        if (!checkForUpdatesTask.Result)
                        {
                            return;
                        }

                        ReleaseEntry lastVersion = updates?.ReleasesToApply?.OrderBy(x => x.Version).LastOrDefault();

                        if (lastVersion == null)
                        {
                            Logger.Log(LogLevel.Info, "GES is up to date.");
                            return;
                        }

                        Logger.Log(LogLevel.Info, "New version of GES found. Downloading files in background...");

                        TrackableTask<Boolean> updateTask = TrackableTask<Boolean>
                            .Create("Updating", out updateProgress, out cancellationToken)
                            .With(Task<Boolean>.Run(
                            () =>
                            {
                                try
                                {
                                    manager.UpdateApp((progress) => updateProgress(progress)).Wait();
                                }
                                catch (Exception ex)
                                {
                                    Logger.Log(LogLevel.Error, "Error applying updates.", ex);
                                    return false;
                                }

                                return true;
                            },
                            cancellationToken));

                        TaskTrackerViewModel.GetInstance().TrackTask(updateTask);

                        if (!updateTask.Result)
                        {
                            return;
                        }

                        Logger.Log(LogLevel.Info, "New GES version downloaded. Restart the application to apply updates.");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(LogLevel.Error, "Error updating GES.", ex);
                }
            });
        }

        /// <summary>
        /// Determines if the current application was installed via Squirrel.
        /// </summary>
        /// <returns>A value indicating if the current application was installed via Squirrel.</returns>
        private static Boolean IsSquirrelInstalled()
        {
            try
            {
                Assembly assembly = Assembly.GetEntryAssembly();
                String updateDotExe = Path.Combine(new DirectoryInfo(Path.GetDirectoryName(assembly.Location)).Parent.FullName, "Update.exe");
                Boolean isInstalled = File.Exists(updateDotExe);

                return isInstalled;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, "Error determining if GES was installed by the installer.", ex);
                return false;
            }
        }

        private static void OnAppInstall(SemanticVersion version, IAppTools tools)
        {
            tools.CreateShortcutForThisExe(ShortcutLocation.StartMenu | ShortcutLocation.Desktop);
        }

        private static void OnAppUninstall(SemanticVersion version, IAppTools tools)
        {
            tools.RemoveShortcutForThisExe(ShortcutLocation.StartMenu | ShortcutLocation.Desktop);
        }

        private static void OnAppRun(SemanticVersion version, IAppTools tools, Boolean firstRun)
        {
            tools.SetProcessAppUserModelId();
        }
    }
    //// End class
}
//// End namespace