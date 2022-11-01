using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryScanner.Core
{
    public class IPropertyChanged : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler? PropertyChanged;

       

        protected void Change(string path) 
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(path));
            }
        }
    }
}
