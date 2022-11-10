﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DirectoryScanner.Core
{
    public class Tracer : IPropertyChanged
    {
        private string MainPath;
        public FilesParametrs filesParametrs { get; set; }
        public Queue queue { get; set; }

        private Dispatcher dispatcher;
        public FilesCollection Files { get; set; }
        //public string[] allfiles { get; set; }
        public Tracer()
        {
            Files = new FilesCollection();
            dispatcher = Dispatcher.CurrentDispatcher;
        }
        public void Start(string Path)
        {
            MainPath = Path;
            queue.AddToQueue(new WaitCallback(handle), MainPath, Files);
            DirFil();
            
        }
        public void handle(object stateInfo)
        {
            Array argArray = (Array)stateInfo;
            string path = (string)argArray.GetValue(0);
            FilesCollection node = (FilesCollection)argArray.GetValue(1);
            //  getValuesFromObject(stateInfo,out path,out node);
            
        }
        public void DirFil()
        {
            
        }
        
    }
}
