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
        private ParallelOptions parOpts;
        private Semaphore pooll;
        internal Steake steake;

        private int i;
        public int IsWorking
        {
            get { return i; }
            set
            {
                i = value;
                Change(nameof(IsWorking));
            }
        }
        internal Queue(ParallelOptions parallelOptions, Semaphore pool)
        {
            queue = new ConcurrentQueue<WaitCallbackFunction>();
            parOpts = parallelOptions;
            pooll = pool;
            steake = new Steake();
        }

        internal void AddToStack(FilesParametrs file)
        {
            steake.Add(file);
        }
        internal void AddToQueue(WaitCallback waitCallback, String path, FilesCollection files)
        {
            queue.Enqueue(new WaitCallbackFunction(waitCallback, path, files));
        }
        internal void InvokeThreadInQueue()
        {
            i = 1;
            int internalNumOfThreads;
            int num1, num2;
            ThreadPool.GetAvailableThreads(out internalNumOfThreads, out num2);
            do
            {
                while (!queue.IsEmpty)
                {
                    while (!queue.IsEmpty && !parOpts.CancellationToken.IsCancellationRequested)
                    {
                        pooll.WaitOne();
                        WaitCallbackFunction thread;
                        if (queue.TryDequeue(out thread))
                        {
                            ThreadPool.QueueUserWorkItem(thread.waitCallback, thread.sources);

                        }
                    }
                    if (parOpts.CancellationToken.IsCancellationRequested)
                    {
                        i = 2;
                        break;
                    }
                    Thread.Sleep(500);
                }

                ThreadPool.GetAvailableThreads(out num1, out num2);
            } while (internalNumOfThreads - num1 != 0 && !parOpts.CancellationToken.IsCancellationRequested);
            steake.getSizes();
            i = 2;
            queue.Clear();
            i = 3;
        }

    }
}
