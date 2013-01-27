using System;
using System.IO;
using System.Reactive.Linq;

namespace Sandbox
{
    public static class FswEx
    {
        public static IObservable<string> GetChanged(this FileSystemWatcher fsw)
        {
            return GetEvent(fsw, "Changed");
        }

        public static IObservable<string> GetDeleted(this FileSystemWatcher fsw)
        {
            return GetEvent(fsw, "Deleted");
        }

        public static IObservable<string> GetCreated(this FileSystemWatcher fsw)
        {
            return GetEvent(fsw, "Created");
        }

        private static IObservable<string> GetEvent(FileSystemWatcher fsw, string evnt)
        {
            return Observable
                .FromEventPattern<FileSystemEventArgs>(fsw, evnt)
                .Select(
                    e => e.EventArgs.Name)
                ;
        }
    }
}
