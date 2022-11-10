using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DirectoryScanner.Core
{
    public class Queue : IPropertyChanged
    {
        private ConcurrentQueue<WaitCallbackFunction> queue;


        internal Queue(ParallelOptions parallelOptions, Semaphore pool)
        {
            queue = new ConcurrentQueue<WaitCallbackFunction>();
        }


        internal void AddToQueue(WaitCallback waitCallback, String path, FilesCollection files)
        {
            queue.Enqueue(new WaitCallbackFunction(waitCallback, path, files));
        }
    }
}
