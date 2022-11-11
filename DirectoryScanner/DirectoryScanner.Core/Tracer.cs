using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DirectoryScanner.Core
{
    public class Tracer : IPropertyChanged
    {
        private string MainPath;
        private Semaphore pool;
        public FilesParametrs filesParametrs { get; set; }
        public Queue queue { get; set; }

        private Dispatcher dispatcher;
        public FilesCollection Files { get; set; }
        //public string[] allfiles { get; set; }
        private const int MAX_THREADS = 10;
        private object locker;
        private CancellationTokenSource cancelToken;
        private ParallelOptions parOpts;

        public Tracer()
        {
            Files = new FilesCollection();
            dispatcher = Dispatcher.CurrentDispatcher;
            pool = new Semaphore(initialCount: MAX_THREADS, maximumCount: MAX_THREADS);

            parOpts = new ParallelOptions();
            queue = new Queue(parOpts, pool);
        }
        public void Start(string Path)
        {
            MainPath = Path;
            queue.AddToQueue(new WaitCallback(handle), MainPath, Files);

            Task.Factory.StartNew(() => queue.InvokeThreadInQueue());
            DirFil();
            
        }
        public void handle(object stateInfo)
        {
            Array argArray = (Array)stateInfo;
            string path = (string)argArray.GetValue(0);
            FilesCollection node = (FilesCollection)argArray.GetValue(1);
            //  getValuesFromObject(stateInfo,out path,out node);
            AddDirectory(node, path);
            pool.Release();

        }
        private void AddDirectory(FilesCollection node, string directory)
        {
            var currDirectory = new FilesParametrs(directory, dispatcher);
            
            Thread.Sleep(200);
            node.Add(currDirectory);

            // AddDirectories(currDirectory);
            try
            {
                string[] directoryList = Directory.GetDirectories(currDirectory.FullName);

                DirectoryInfo directoryInfo = new DirectoryInfo(currDirectory.FullName);
                DirectoryInfo[] files = directoryInfo.GetDirectories();
                var filtered = files.Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden));

                foreach (var d in filtered)
                {

                    if (parOpts.CancellationToken.IsCancellationRequested)
                    {
                        break;
                    }
                    queue.AddToQueue(new WaitCallback(handle), d.FullName, currDirectory.Files);
                }
            }
            catch (UnauthorizedAccessException)
            {

            }
            AddFiles(currDirectory);
        }

        private void AddFiles(FilesParametrs currDirectory)
        {
            try
            {

                DirectoryInfo directoryInfo = new DirectoryInfo(currDirectory.FullName);
                FileInfo[] files = directoryInfo.GetFiles();
                var filtered = files.Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden));

                foreach (var f in filtered)
                {
                    if (parOpts.CancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    Thread.Sleep(200); //under the question
                    currDirectory.Files.Add(new FilesParametrs(f.FullName, f.Length, dispatcher));
                }
            }
            catch (UnauthorizedAccessException)
            {

            }
        }
        public void DirFil()
        {
            
        }
        
    }
}
