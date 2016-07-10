using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;




namespace WindowsFormsApplication1
{
    class Data
    {
        //performance counters to get available ram and cpu%
        PerformanceCounter CpuCounter = new PerformanceCounter();
        PerformanceCounter RamCounter = new PerformanceCounter();

        //gets total Ram by referencing Microsoft.VisualBasic.dll
        private ulong ramTotal = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
        //Hdd total variable
        private long hddTotal = 0;
        //
        private DateTime time = DateTime.Now;//time of start

        //storage of snapshot articles
        private LinkedList<Snapshot> storage = new LinkedList<Snapshot>();

        //string for location of file to save to
        String store = "a";



        //gets values to send and initializes performance counters
        public Data()
        {
            //calls CPUCounter's definitions
            CpuCounter.CategoryName = "Processor";
            CpuCounter.CounterName = "% Processor Time";
            CpuCounter.InstanceName = "_Total";

            //calls RAMCounter's definitions
            RamCounter.CategoryName = "Memory";
            RamCounter.CounterName = "Available Bytes";

            //gets the deadvalues in order to let the performance counters warm up
            CpuCounter.NextValue();//dead values
            RamCounter.NextValue();//dead values

            //asks method to calculate hdd
            findTotalHDD();
        }

        //finds total HDD space for calculations
        private void findTotalHDD()
        {
            //records total system storage
            DriveInfo[] v = DriveInfo.GetDrives();

            //goes through list of drives
            foreach (DriveInfo drive in v)
            {
                if (drive.IsReady)
                {
                    hddTotal += drive.TotalSize;
                }
            }//
        }


        //Deals with exporting data
        ///exports into a new file or appends or creates a new file
        public void export(String location)
        {
            // a is default string if store has not been overwritten
            if (store.Equals("a"))
            {
                //creates a file writer
                try
                {
                    using (StreamWriter write = File.CreateText(location))
                    {
                        //records static properties such as total ram,hdd and time
                        write.WriteLine(time.Month + "/" + time.Day + "/" + time.Year + " " + time.Hour + ":" + time.Minute);
                        write.WriteLine(ramTotal + " " + hddTotal);

                        //sends to list writer
                        writeOutput(write);
                    }
                    //sets the stored location
                    store = location;
                }
                catch (UnauthorizedAccessException) { throw new UnauthorizedAccessException(); }
                catch (DirectoryNotFoundException) { throw new DirectoryNotFoundException(); }
            }
            else
            {
                //not equal so it takes info from old position and moves it into new one
                if (!location.Equals(store))
                {
                    //starts writer
                    using (StreamWriter write = File.CreateText(location))
                    {
                        //starts reader
                        using (StreamReader read = File.OpenText(store))
                        {
                            //takes info and writes it from previous position
                            write.WriteLine(read.ReadLine());
                            write.WriteLine(read.ReadLine());

                            //loops and writes until it reaches the end of the file
                            while (read.EndOfStream == false)
                            {
                                write.WriteLine(read.ReadLine());
                            }
                        }
                        store = location;
                        //changes store to new location
                    }
                    appendData();//adds rest that has backlogged (if any)
                }
                else { appendData(); }
                //no changes in position so it just adds any missed text
            }

        }
        ///adds info to already set file
        public void appendData()
        {
            //checks that a file exists
            if (File.Exists(store))
            {
                using (StreamWriter write = File.AppendText(store))
                {
                    //sends to list writer
                    writeOutput(write);
                }
            }
            else
            { }
        }
        ///base code that is used to go through linkedlist
        private void writeOutput(StreamWriter write)
        {
            //iterates through list
            foreach (Snapshot piece in storage)
            {
                write.WriteLine(((double)piece.getMilli() / 1000) + " " + piece.getCpu() + " " + piece.getRam() + " " + piece.getHdd());
            }
            //clears list
            storage.Clear();
        }


        //creates data to store and use
        public void createNewData()
        {
            //gets new values
            float ramA = RamCounter.NextValue();
            float cpuInst = CpuCounter.NextValue();

            //creates and immeadiatly adds the new snapshot
            storage.AddLast(new Snapshot(ramTotal, hddTotal, ramA, cpuInst));
        }

        //get methods
        public double locationCpu()
        {
            //finds the value from las position
            double location = storage.Last<Snapshot>().getCpu();
            return location;
        }
        //
        public double locationRam()
        {
            double location = storage.Last<Snapshot>().getRam();
            return location;
        }
        //
        public double locationHdd()
        {
            double location = storage.Last<Snapshot>().getHdd();
            return location;
        }
        //
        public long locationTime()
        {
            DateTime compare = storage.Last<Snapshot>().getTime();
            long tosend = 0;

            //Conversion to millisecond amounts after subtracting by
            //the respective dates in order to make calculations a bit easier
            tosend += (compare.Year - time.Year) * 31536000000;
            tosend += (compare.Month - time.Month) * 2628000000;
            tosend += (compare.Day - time.Day) * 86400000;
            tosend += (compare.Hour - time.Hour) * 3600000;
            tosend += (compare.Minute - time.Minute) * 60000;
            tosend += (compare.Second - time.Second) * 1000;
            tosend += compare.Millisecond - time.Millisecond;

            storage.Last<Snapshot>().setMilli(tosend);



            return tosend;
        }


    }
}
