using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
//using System.Windows.Threading;


namespace DirectoryScanner.Core
{
    public class FilesCollection : ConcurrentDictionary<String, File>, INotifyCollectionChanged
    {

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
       

        public FilesCollection() : base()
        {
        }

       
    }
}
