using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryScanner.Core
{
    public class Tracer : IPropertyChanged
    {
        private string MainPath;
        //public string[] allfiles { get; set; }
        public Tracer()
        {
            allfiles = new string[0];
        }
        public void Start(string Path)
        {
            MainPath = Path;
            DirFil();
            
        }
        public void DirFil()
        {
            allfiles = Directory.GetFiles(MainPath);
            foreach (string filename in allfiles)
            {
                int n = filename.Length;
                //Console.WriteLine(filename);
            }
        }
        private string[] allfiles { get; set; }
        public string[] Allfiles
        {
            get { return allfiles; }
            set { allfiles = value; Change(nameof(Allfiles)); }
        }
    }
}
