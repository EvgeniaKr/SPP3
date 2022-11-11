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
        private double percent;
        public double Percent
        {
            get { return percent; }
            set { percent = value; Change(nameof(Percent)); }
        }
        private long size;
        public long Size { get { return size; } set { size = value; Change(nameof(Size)); } }
        private bool directory;
        public bool Directory { get { return directory; } set { directory = value; Change(nameof(Name)); } }

        public FilesCollection Files { get; set; }
        public FilesParametrs(string fullname, long size, Dispatcher dispatcher, bool ddirectory)
        {
            FullName = fullname;
            Name = System.IO.Path.GetFileName(fullname);
            Size = size;
            Files = new FilesCollection(dispatcher);
            this.Directory = ddirectory;
        }

    }
}
