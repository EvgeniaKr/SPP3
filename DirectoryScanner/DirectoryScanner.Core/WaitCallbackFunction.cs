using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DirectoryScanner.Core
{
    internal class WaitCallbackFunction
    {
        internal WaitCallback waitCallback;
        internal Object[] sources;
        internal WaitCallbackFunction(WaitCallback waitCallBack, String path, FilesCollection node)
        {
            sources = new object[] { path, node };
            waitCallback = waitCallBack;
        }
    }
}
