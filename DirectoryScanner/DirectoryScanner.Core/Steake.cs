using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DirectoryScanner.Core
{
    internal class Steake
    {
        internal ConcurrentStack<FilesParametrs> filesForSizeChecking;

        internal Steake()
        {
            filesForSizeChecking = new ConcurrentStack<FilesParametrs>();
        }
        internal void getSizes()
        {
            FilesParametrs directory;
            while (filesForSizeChecking.Count > 0)
            {
                if (filesForSizeChecking.TryPop(out directory))
                {
                    long size = 0;

                    foreach (var file in directory.Files)
                    {
                        size += file.Value.Size;
                    }

                    directory.Size = size;
                    Thread.Sleep(100);
                    foreach (var file in directory.Files)
                    {
                        file.Value.Percent = ((double)file.Value.Size / (double)directory.Size) * 100;
                    }
                }
            }
        }


        internal void Add(FilesParametrs file)
        {
            filesForSizeChecking.Push(file);
        }
    }
}
