﻿using System;
using System.Diagnostics;
using System.Windows.Threading;
using Wokhan.Core.ComponentModel;

namespace Wokhan.UI.Diagnostics
{
    public class ResourcesWatcher : NotifierHelper
    {
        //private static Process currentProcess = Process.GetCurrentProcess();
        PerformanceCounter cpuCounter;
        PerformanceCounter memoryCounter;
        PerformanceCounter threadsCounter;

        public ResourcesWatcher()
        {
            string processName;

            using (var p = Process.GetCurrentProcess())
            {
                processName = p.ProcessName;
            }

            memoryCounter = new PerformanceCounter("Process", "Working Set - Private", processName, true);
            threadsCounter = new PerformanceCounter("Process", "Thread Count", processName, true);
            cpuCounter = new PerformanceCounter("Process", "% Processor Time", processName, true);

            var timer = new DispatcherTimer(TimeSpan.FromSeconds(1.0), DispatcherPriority.Normal, TimerTick, System.Windows.Application.Current.Dispatcher);
            timer.Start();
        }

        void TimerTick(object sender, EventArgs e)
        {
            //currentProcess.Refresh();
            NotifyPropertyChanged(nameof(MemoryUseFormatted));
            NotifyPropertyChanged(nameof(CPUUsage));
            NotifyPropertyChanged(nameof(ThreadsCount));
            //MemoryUse = currentProcess.WorkingSet64;
        }

        /*private long _memoryUse;
        public long MemoryUse
        {
            get { return _memoryUse; }
            set { _memoryUse = value; NotifyPropertyChanged("MemoryUse"); NotifyPropertyChanged("MemoryUseFormatted"); }
        }*/

        public string MemoryUseFormatted
        {
            get { return (int)(memoryCounter.NextValue() / 1024 / 1024) + "MB"; }
        }

        public int ThreadsCount
        {
            get { return (int)threadsCounter.NextValue(); }
        }

        public int CPUUsage
        {
            get { return (int)cpuCounter.NextValue(); }
        }

    }
}
