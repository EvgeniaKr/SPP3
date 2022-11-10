using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DirectoryScanner.Core
{
    public class File : IPropertyChanged//INotifyPropertyChangeded
    {

        private string name;
        public string Name { get { return name; } set { name = value; Change(nameof(Name)); } }
        public string FullName { get; set; }


        public File(string fullname, long size)
        {
            FullName = fullname;
        }

        public File(string fullname)
        {
            FullName = fullname;
        }
    }
}
