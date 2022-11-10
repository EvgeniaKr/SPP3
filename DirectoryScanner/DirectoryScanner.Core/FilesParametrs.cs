using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DirectoryScanner.Core
{
    public class FilesParametrs : IPropertyChanged//INotifyPropertyChangeded
    {

        private string name;
        public string Name { get { return name; } set { name = value; Change(nameof(Name)); } }
        public string FullName { get; set; }

        public FilesCollection Files { get; set; }
        public FilesParametrs(string fullname, long size, Dispatcher dispatcher)
        {
            FullName = fullname;
            Name = System.IO.Path.GetFileName(fullname);
            Files = new FilesCollection(dispatcher);
        }

        public FilesParametrs(string fullname, Dispatcher dispatcher)
        {
            FullName = fullname;
            Name = System.IO.Path.GetFileName(fullname);
            Files = new FilesCollection(dispatcher);
        }
    }
}
