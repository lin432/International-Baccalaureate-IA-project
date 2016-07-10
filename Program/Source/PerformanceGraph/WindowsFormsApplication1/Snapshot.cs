using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic.Devices;

namespace WindowsFormsApplication1
{

    class Snapshot
    {
        //variables saved
        private float cpu, ram, hdd;
        private long hddUsed = 0, milli;
        DateTime time;

        //processes and then stores data
        public Snapshot(float ramT, long hddT, float ramA, float cpuInst)
        {
            ram = ((ramT - ramA) / ramT) * 100;//calculates ram used and then the percent
            cpu = cpuInst;//saves cpu%

            //finds and stores Hdd info
            DriveInfo[] v = DriveInfo.GetDrives();

            //adavnced for loop that goes through array
            foreach (DriveInfo drive in v)
            {
                if (drive.IsReady) //makes sure the drive is not accessing other processes
                { hddUsed += drive.TotalSize - drive.AvailableFreeSpace; }
            }


            hdd = ((float)hddUsed / hddT) * 100;//calculates percentage of hdd used
            //

            //records time
            time = DateTime.Now;
        }


        //get methods as well as a set method for calculated millisecond
        public double getCpu() { return cpu; }
        public double getRam() { return ram; }
        public double getHdd() { return hdd; }
        public DateTime getTime() { return time; }
        public void setMilli(long t) { milli = t; }
        public long getMilli() { return milli; }

    }
}
