using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DirectoryScanner.Core;
namespace DirectoryScanner.Example
{
    public class ViewModel : DirectoryScanner.Core.IPropertyChanged
    {
        public RelayCommand OpenDialog { get; }
        public RelayCommand StartAnalize { get; }
        public Tracer tracer { get; }
        public ViewModel()
        {
            tracer = new Tracer();
            OpenDialog = new RelayCommand(o => { OpenDirectore(); });
            StartAnalize = new RelayCommand(o => tracer.Start(Path));
        
        }
        
        private void OpenDirectore()
        {
            var FolderPath = new FolderBrowserForWPF.Dialog();
            if (FolderPath.ShowDialog() == true)
            {
                Path = FolderPath.FileName;
            }
        }
        private string str;

        public string Path
        {
            get { return str; }
            set { str = value; Change(nameof(Path)); }
        }
      
    }
}
