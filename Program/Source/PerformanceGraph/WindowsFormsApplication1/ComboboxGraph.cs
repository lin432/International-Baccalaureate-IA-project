using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;


namespace WindowsFormsApplication1
{
    class ComboboxGraph : Panel
    {

        public void setSeries(LinkedList<double> X, LinkedList<double> C, LinkedList<double> R, LinkedList<double> H, int count)
        {
            CPU.LegendText = "CPU Line " + count;
            RAM.LegendText = "RAM Line " + count;
            HDD.LegendText = "HDD Line " + count;

            while (X.Count != 0)
            {
                CPU.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                RAM.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                HDD.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                double x = (double)X.First<double>();
                CPU.Points.AddXY(x, C.First<double>());
                RAM.Points.AddXY(x, R.First<double>());
                HDD.Points.AddXY(x, H.First<double>());

                X.RemoveFirst();
                C.RemoveFirst();
                R.RemoveFirst();
                H.RemoveFirst();
            }
        }

        public Series getCPU() { return CPU; }
        public Series getRAM() { return RAM; }
        public Series getHDD() { return HDD; }

        public void setCPU(Series serie) { CPU = serie; }
        public void setRAM(Series serie) { RAM = serie; }
        public void setHDD(Series serie) { HDD = serie; }

        public void setDate(String Date) { DateName.Text = "Recorded: " + Date; }
        public void setTotalRam(String ram) { TotalRam.Text = "Total RAM: " + ram + " Bytes"; }
        public void setTotalHdd(String hdd) { TotalHdd.Text = "HDD Size: " + hdd + " Bytes"; }

        //change the series and the timer will just add and remove the series in question

        private void ColorCPU()
        {
            int r = int.Parse(CPUColorR.Text);
            int g = int.Parse(CPUColorG.Text);
            int b = int.Parse(CPUColorB.Text);

            CPU.Color = Color.FromArgb(r, g, b);
        }

        private void CPUColorR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int temp = int.Parse(CPUColorR.Text);
                if (temp > 255 || temp < 0) { CPUColorR.Text = ""; }
                else { ColorCPU(); }
            }
            catch (FormatException) { CPUColorR.Text = ""; }
            catch (ArgumentOutOfRangeException) { CPUColorR.Text = ""; }
        }

        private void CPUColorG_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int temp = int.Parse(CPUColorG.Text);
                if (temp > 255 || temp < 0) { CPUColorG.Text = ""; }
                else { ColorCPU(); }
            }
            catch (FormatException) { CPUColorG.Text = ""; }
            catch (ArgumentOutOfRangeException) { CPUColorG.Text = ""; }
        }

        private void CPUColorB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int temp = int.Parse(CPUColorB.Text);
                if (temp > 255 || temp < 0) { CPUColorB.Text = ""; }
                else { ColorCPU(); }
            }
            catch (FormatException) { CPUColorB.Text = ""; }
            catch (ArgumentOutOfRangeException) { CPUColorB.Text = ""; }
        }


        private void ColorRAM()
        {
            int r = int.Parse(RAMColorR.Text);
            int g = int.Parse(RAMColorG.Text);
            int b = int.Parse(RAMColorB.Text);

            RAM.Color = Color.FromArgb(r, g, b);
        }

        private void RAMColorR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int temp = int.Parse(RAMColorR.Text);
                if (temp > 255 || temp < 0) { RAMColorR.Text = ""; }
                else { ColorRAM(); }
            }
            catch (FormatException) { RAMColorR.Text = ""; }
            catch (ArgumentOutOfRangeException) { RAMColorR.Text = ""; }
        }

        private void RAMColorG_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int temp = int.Parse(RAMColorG.Text);
                if (temp > 255 || temp < 0) { RAMColorG.Text = ""; }
                else { ColorRAM(); }
            }
            catch (FormatException) { RAMColorG.Text = ""; }
            catch (ArgumentOutOfRangeException) { RAMColorG.Text = ""; }
        }

        private void RAMColorB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int temp = int.Parse(RAMColorB.Text);
                if (temp > 255 || temp < 0) { RAMColorB.Text = ""; }
                else { ColorRAM(); }
            }
            catch (FormatException) { RAMColorB.Text = ""; }
            catch (ArgumentOutOfRangeException) { RAMColorB.Text = ""; }
        }


        private void ColorHDD()
        {
            int r = int.Parse(HDDColorR.Text);
            int g = int.Parse(HDDColorG.Text);
            int b = int.Parse(HDDColorB.Text);

            HDD.Color = Color.FromArgb(r, g, b);
        }

        private void HDDColorR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int temp = int.Parse(HDDColorR.Text);
                if (temp > 255 || temp < 0) { HDDColorR.Text = ""; }
                else { ColorHDD(); }
            }
            catch (FormatException) { HDDColorR.Text = ""; }
            catch (ArgumentOutOfRangeException) { HDDColorR.Text = ""; }
        }

        private void HDDColorG_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int temp = int.Parse(HDDColorG.Text);
                if (temp > 255 || temp < 0) { HDDColorG.Text = ""; }
                else { ColorHDD(); }
            }
            catch (FormatException) { HDDColorG.Text = ""; }
            catch (ArgumentOutOfRangeException) { HDDColorG.Text = ""; }
        }

        private void HDDColorB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int temp = int.Parse(HDDColorB.Text);
                if (temp > 255 || temp < 0) { HDDColorB.Text = ""; }
                else { ColorHDD(); }
            }
            catch (FormatException) { HDDColorB.Text = ""; }
            catch (ArgumentOutOfRangeException) { HDDColorB.Text = ""; }
        }


        private void Visibility_CheckedChanged(object sender, EventArgs e)
        {
            visible = Visibility.Checked;
            queue = true;
        }

        public Boolean getQueue() { return queue; }
        public Boolean getVisible() { return visible; }
        public void resetQueue() { queue = false; }
        private void RemoveFromList(object sender, EventArgs e) { Console.WriteLine(queue);  remove = true; queue = true; }
        public Boolean RemoveThisGraph() { return remove; }

        public ComboboxGraph()
        {
            this.Size = new Size(182, 265);


            DateName.Location = new Point(0, 0);
            TotalRam.Location = new Point(0, 25);
            TotalHdd.Location = new Point(0, 50);
            DateName.Size = new Size(182, 20);
            TotalRam.Size = new Size(182, 20);
            TotalHdd.Size = new Size(182, 20);

            Visibility.Text = "Show Line(s)";
            Visibility.Checked = true;
            Visibility.Location = new Point(0, 70);
            Visibility.CheckedChanged += new EventHandler(this.Visibility_CheckedChanged);



            int r, g, b;
            Random randomInt = new Random();

            r = randomInt.Next(255);
            g = randomInt.Next(255);
            b = randomInt.Next(255);



            CPUColor.Text = "CPU Line Color (R,G,B)";
            CPUColor.Location = new Point(0, 100);
            CPUColor.Size = new Size(182, 20);

            CPUColorR.Size = template;
            CPUColorG.Size = template;
            CPUColorB.Size = template;
            CPUColorR.Location = new Point(12, 120);
            CPUColorG.Location = new Point(42, 120);
            CPUColorB.Location = new Point(72, 120);
            CPUColorR.Text = "" + r;
            CPUColorG.Text = "" + g;
            CPUColorB.Text = "" + b;
            CPUColorR.TextChanged += new EventHandler(this.CPUColorR_TextChanged);
            CPUColorG.TextChanged += new EventHandler(this.CPUColorG_TextChanged);
            CPUColorB.TextChanged += new EventHandler(this.CPUColorB_TextChanged);
            CPU.Color = Color.FromArgb(r, g, b);


            r = randomInt.Next(255);
            g = randomInt.Next(255);
            b = randomInt.Next(255);

            RAMColor.Text = "RAM Line Color (R,G,B)";
            RAMColor.Location = new Point(0, 145);
            RAMColor.Size = new Size(182, 20);

            RAMColorR.Size = template;
            RAMColorG.Size = template;
            RAMColorB.Size = template;
            RAMColorR.Location = new Point(12, 165);
            RAMColorG.Location = new Point(42, 165);
            RAMColorB.Location = new Point(72, 165);
            RAMColorR.Text = "" + r;
            RAMColorG.Text = "" + g;
            RAMColorB.Text = "" + b;
            RAMColorR.TextChanged += new EventHandler(this.RAMColorR_TextChanged);
            RAMColorG.TextChanged += new EventHandler(this.RAMColorG_TextChanged);
            RAMColorB.TextChanged += new EventHandler(this.RAMColorB_TextChanged);
            RAM.Color = Color.FromArgb(r, g, b);


            r = randomInt.Next(255);
            g = randomInt.Next(255);
            b = randomInt.Next(255);

            HDDColor.Text = "HDD Line Color (R,G,B)";
            HDDColor.Location = new Point(0, 190);
            HDDColor.Size = new Size(182, 20);

            HDDColorR.Size = template;
            HDDColorG.Size = template;
            HDDColorB.Size = template;
            HDDColorR.Location = new Point(12, 210);
            HDDColorG.Location = new Point(42, 210);
            HDDColorB.Location = new Point(72, 210);
            HDDColorR.Text = "" + r;
            HDDColorG.Text = "" + g;
            HDDColorB.Text = "" + b;
            HDDColorR.TextChanged += new EventHandler(this.HDDColorR_TextChanged);
            HDDColorG.TextChanged += new EventHandler(this.HDDColorG_TextChanged);
            HDDColorB.TextChanged += new EventHandler(this.HDDColorB_TextChanged);
            HDD.Color = Color.FromArgb(r, g, b);

            RemoveThis.Location = new Point(12,240);
            RemoveThis.Click += new EventHandler(this.RemoveFromList);
            RemoveThis.Text = "Remove";



            this.Controls.Add(DateName);
            this.Controls.Add(TotalRam);
            this.Controls.Add(TotalHdd);
            this.Controls.Add(Visibility);
            this.Controls.Add(CPUColor);
            this.Controls.Add(CPUColorR);
            this.Controls.Add(CPUColorG);
            this.Controls.Add(CPUColorB);
            this.Controls.Add(RAMColor);
            this.Controls.Add(RAMColorR);
            this.Controls.Add(RAMColorG);
            this.Controls.Add(RAMColorB);
            this.Controls.Add(HDDColor);
            this.Controls.Add(HDDColorR);
            this.Controls.Add(HDDColorG);
            this.Controls.Add(HDDColorB);
            this.Controls.Add(RemoveThis);
        }



        private Boolean queue = false;
        private Boolean visible = true;
        private Boolean remove = false;

        Button RemoveThis = new Button();

        Label DateName = new Label();
        Label TotalRam = new Label();
        Label TotalHdd = new Label();
        CheckBox Visibility = new CheckBox();

        Label CPUColor = new Label();
        TextBox CPUColorR = new TextBox();
        TextBox CPUColorG = new TextBox();
        TextBox CPUColorB = new TextBox();

        Label RAMColor = new Label();
        TextBox RAMColorR = new TextBox();
        TextBox RAMColorG = new TextBox();
        TextBox RAMColorB = new TextBox();

        Label HDDColor = new Label();
        TextBox HDDColorR = new TextBox();
        TextBox HDDColorG = new TextBox();
        TextBox HDDColorB = new TextBox();

        Series CPU = new Series();
        Series RAM = new Series();
        Series HDD = new Series();

        Size template = new Size(25, 20);
    }
}
