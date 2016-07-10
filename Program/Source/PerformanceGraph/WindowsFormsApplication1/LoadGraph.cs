using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication1
{
    class LoadGraph : Panel
    {
        LinkedList<ComboboxGraph> List = new LinkedList<ComboboxGraph>();
        Dictionary<Int32, String> Unicode = new Dictionary<int, string>();

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {//use foreach in order to gothrough list and find value, can do the same for show visibility
            foreach (ComboboxGraph graph in List)
            {
                Console.WriteLine(graph.getQueue());
                if (graph.getQueue()==true)
                {
                    Console.WriteLine("queued process ");
                    if (graph.RemoveThisGraph()) 
                    {
                        Console.WriteLine("removing ");
                        comboBox1.Items.Remove(comboBox1.SelectedItem);
                        chart1.Series.Remove(graph.getCPU());
                        chart1.Series.Remove(graph.getRAM());
                        chart1.Series.Remove(graph.getHDD());
                        List.Remove(graph);
                        setInvisible();
                        comboboxMain1.Visible = true;
                        Console.WriteLine("removed ");
                        break; 
                    }
                    if (graph.getVisible())
                    {
                        if (comboboxMain1.getCPUVar())
                        {
                            graph.getCPU().Enabled = true;
                        }
                        if (comboboxMain1.getRAMVar())
                        {
                            graph.getRAM().Enabled = true;
                        }
                        if (comboboxMain1.getHDDVar())
                        {
                            graph.getHDD().Enabled = true;
                        }
                    }
                    else { graph.getCPU().Enabled = false; graph.getRAM().Enabled = false; graph.getHDD().Enabled = false; }
                }
                if (comboboxMain1.getQueue())
                {
                    if (graph.getVisible())
                    {
                        if (comboboxMain1.getCPUVar()) { graph.getCPU().Enabled = true; } else { graph.getCPU().Enabled = false; }
                        if (comboboxMain1.getRAMVar()) { graph.getRAM().Enabled = true; } else { graph.getRAM().Enabled = false; }
                        if (comboboxMain1.getHDDVar()) { graph.getHDD().Enabled = true; } else { graph.getHDD().Enabled = false; }
                    }
                }
                graph.resetQueue();
            }
            comboboxMain1.resetQueue();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //sets the panels that are visible
            setInvisible();

            int text = comboBox1.SelectedIndex;
            if (text == 0) { comboboxMain1.Visible = true; }
            else
            {
                Panel temp = List.ElementAt(text - 1);
                temp.Visible = true;
            }
        }

        //button loads a file
        private void button1_Click(object sender, EventArgs e)
        {
            ComboboxGraph temp = new ComboboxGraph();

            LinkedList<double> axisX = new LinkedList<double>();
            LinkedList<double> CPU = new LinkedList<double>();
            LinkedList<double> RAM = new LinkedList<double>();
            LinkedList<double> HDD = new LinkedList<double>();
            try
            {
                using (StreamReader read = File.OpenText(textBox1.Text))
                {
                    temp.setDate(read.ReadLine());
                    temp.setTotalRam(findSpace(read, ""));
                    temp.setTotalHdd(findSpace(read, ""));

                    while (read.Peek() != -1)
                    {
                        read.Read();
                        if (read.Peek() == -1) { break; }
                        axisX.AddLast(double.Parse(findSpace(read, "")));
                        CPU.AddLast(double.Parse(findSpace(read, "")));
                        RAM.AddLast(double.Parse(findSpace(read, "")));
                        HDD.AddLast(double.Parse(findSpace(read, "")));

                    }
                }


                temp.setSeries(axisX, CPU, RAM, HDD, List.Count + 1);

                chart1.Series.Add(temp.getCPU());
                chart1.Series.Add(temp.getRAM());
                chart1.Series.Add(temp.getHDD());

                temp.Location = PanelsPoint;
                temp.Visible = true;

                List.AddLast(temp);
                comboBox1.Items.Add(List.Count + ". Chart" + List.Count);
                this.Controls.Add(temp);
            }
            catch (FileNotFoundException) { textBox1.Text = "File not found"; }
            catch (UnauthorizedAccessException) { textBox1.Text = "Not accessible by the program"; }
            catch (DirectoryNotFoundException) { textBox1.Text = "File not found"; }
        }
        //recursively searches a Stream in order to isolate a word (in my case extra restrictions to find a number)

        private String findSpace(StreamReader read, String hold)
        {
            if (read.Peek() == 32 || read.Peek() == 13 || read.Peek() == 10)
            {
                read.Read();
                return hold;
            }
            else
            {
                hold += Unicode[read.Read()];

                return findSpace(read, hold);
            }
        }

        private void initializeUnicode()
        {
            Unicode.Add(97, "a");
            Unicode.Add(98, "b");
            Unicode.Add(99, "c");
            Unicode.Add(100, "d");
            Unicode.Add(101, "e");
            Unicode.Add(102, "f");
            Unicode.Add(103, "g");
            Unicode.Add(104, "h");
            Unicode.Add(105, "i");
            Unicode.Add(106, "j");
            Unicode.Add(107, "k");
            Unicode.Add(108, "l");
            Unicode.Add(109, "m");
            Unicode.Add(110, "n");
            Unicode.Add(111, "o");
            Unicode.Add(112, "p");
            Unicode.Add(113, "q");
            Unicode.Add(114, "r");
            Unicode.Add(115, "s");
            Unicode.Add(116, "t");
            Unicode.Add(117, "u");
            Unicode.Add(118, "v");
            Unicode.Add(119, "w");
            Unicode.Add(120, "x");
            Unicode.Add(121, "y");
            Unicode.Add(122, "z");
            Unicode.Add(48, "0");
            Unicode.Add(49, "1");
            Unicode.Add(50, "2");
            Unicode.Add(51, "3");
            Unicode.Add(52, "4");
            Unicode.Add(53, "5");
            Unicode.Add(54, "6");
            Unicode.Add(55, "7");
            Unicode.Add(56, "8");
            Unicode.Add(57, "9");
            Unicode.Add(32, " ");
            Unicode.Add(46, ".");
            Unicode.Add(13, "\n");

        }

        private void setInvisible()
        {
            comboboxMain1.Visible = false;

            foreach (ComboboxGraph graph in List)
            {
                graph.Visible = false;
            }
        }



        public LoadGraph() 
        {
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {"0. Main"});
            this.comboBox1.Location = new System.Drawing.Point(13, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(182, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // chart1
            // 
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(219, 0);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(977, 389);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 351);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(182, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Import";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 323);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(182, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "C:\\Documents\\example.txt";
            // 
            // comboboxMain1
            // 
            this.comboboxMain1.Location = new System.Drawing.Point(13, 41);
            this.comboboxMain1.Name = "comboboxMain1";
            this.comboboxMain1.Size = new System.Drawing.Size(182, 265);
            this.comboboxMain1.TabIndex = 6;
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Interval = 200;
            this.UpdateTimer.Enabled = true;
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            //
            //panel
            //
            this.Size = new Size(1169, 416);
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            this.BackColor = Color.White;

            //
            //add
            //
            this.Controls.Add(this.comboboxMain1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.comboBox1);

            initializeUnicode();

            this.chart1.ChartAreas.Add(comboboxMain1.getChartArea());

        }

        private ComboBox comboBox1 = new ComboBox();
        private Chart chart1 = new Chart();
        private Button button1 = new Button();
        private TextBox textBox1 = new TextBox();
        private ComboboxMain comboboxMain1 = new ComboboxMain();
        private Timer UpdateTimer = new Timer();

        Point PanelsPoint = new Point(13, 41);

    }
}
