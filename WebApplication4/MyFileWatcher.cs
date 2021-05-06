using Microsoft.AspNetCore.SignalR;
using SignalRDemoApp.Hubs;
using System.IO;

namespace SignalRDemoApp
{
    public interface IMyFileWatcher { }

    public class MyFileWatcher : IMyFileWatcher
    {
        private readonly IHubContext<FileWatcherHub> _hubContext;        

        public MyFileWatcher(IHubContext<FileWatcherHub> hubContext)
        {
            _hubContext = hubContext;
            var watcher = new FileSystemWatcher();            

            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);            

            // tell the watcher where to look
            watcher.Path = @"D:\Book\TestFiles\";

            // You must add this line - this allows events to fire.
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            var file = new FileDetails() { Name = e.Name, ChangeType = e.ChangeType.ToString() };
            _hubContext.Clients.All.SendAsync("onFileChange", file);
        }
    }
}
