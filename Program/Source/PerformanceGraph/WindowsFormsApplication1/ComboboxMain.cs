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
    class ComboboxMain : Panel
    {
        private Boolean HDDvar = true;
        private Boolean RAMvar = true;
        private Boolean CPUvar = true;
        private Boolean queue = false;

        private ChartArea Area = new ChartArea();

        public ChartArea getChartArea() { return Area; }


        //get the timer to do this
        //the form will send comboboxgraph to a form method and it will remove/add CPU as neccesary


        //
        public Boolean getHDDVar() { return HDDvar; }
        public Boolean getRAMVar() { return RAMvar; }
        public Boolean getCPUVar() { return CPUvar; }
        public Boolean getQueue() { return queue; }
        public void resetQueue() { queue = false; }


        //sets boolean for timer to change lines
        private void HDDLine_CheckedChanged(object sender, EventArgs e)
        {
            HDDvar = HDDLine.Checked; queue = true;
        }
        private void RAMLine_CheckedChanged(object sender, EventArgs e)
        {
            RAMvar = RAMLine.Checked; queue = true;
        }
        private void CPULine_CheckedChanged(object sender, EventArgs e)
        {
            CPUvar = CPULine.Checked; queue = true;
        }



        //ChartArea will be stored here, change the chartarea and send it back
        private void RangeY_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(RangeY.Text) > Area.AxisY.Minimum)
                {
                    Area.AxisY.Maximum = int.Parse(RangeY.Text);
                }
                else { RangeY.Text = ""; }
            }
            catch (FormatException) { RangeY.Text = ""; }
            catch (ArgumentOutOfRangeException) { RangeY.Text = ""; }
        }

        private void RangeX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(RangeX.Text) < Area.AxisY.Maximum)
                {
                    Area.AxisY.Minimum = int.Parse(RangeX.Text);
                }
                else { RangeX.Text = ""; }
            }
            catch (FormatException) { RangeX.Text = ""; }
            catch (ArgumentOutOfRangeException) { RangeX.Text = ""; }
        }

        private void DomainY_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(DomainY.Text) > double.Parse(DomainX.Text))
                {
                    Area.AxisX.Maximum = int.Parse(DomainY.Text);
                }
                else { DomainY.Text = ""; }
            }
            catch (FormatException) { DomainY.Text = ""; }
            catch (ArgumentOutOfRangeException) { DomainY.Text = ""; }
        }

        private void DomainX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(DomainX.Text) < Area.AxisX.Maximum)
                {
                    Area.AxisX.Minimum = int.Parse(DomainX.Text);
                }
                else { DomainX.Text = ""; }
            }
            catch (FormatException) { DomainX.Text = ""; }
            catch (ArgumentOutOfRangeException) { DomainX.Text = ""; }
        }


        public ComboboxMain()
        {
            this.Size = new Size(182, 265);

            Domain.Text = "Domain:";
            Domain.Size = new Size(50, 20);


            DomainX.Text = "0";
            DomainY.Text = "120";
            DomainX.Size = new Size(40, 20);
            DomainY.Size = new Size(40, 20);

            Domain.Location = new Point(5, 2);
            DomainX.Location = new Point(60, 0);
            DomainY.Location = new Point(100, 0);
            DomainX.TextChanged += new EventHandler(this.DomainX_TextChanged);
            DomainY.TextChanged += new EventHandler(this.DomainY_TextChanged);



            Range.Text = "Range:";
            Range.Size = new Size(50, 20);


            RangeX.Text = "0";
            RangeY.Text = "100";
            RangeX.Size = new Size(40, 20);
            RangeY.Size = new Size(40, 20);

            Range.Location = new Point(5, 32);
            RangeX.Location = new Point(60, 30);
            RangeY.Location = new Point(100, 30);
            RangeX.TextChanged += new EventHandler(this.RangeX_TextChanged);
            RangeY.TextChanged += new EventHandler(this.RangeY_TextChanged);


            CPULine.Text = "CPU Line";
            CPULine.Checked = true;
            CPULine.Location = new Point(10, 80);
            CPULine.CheckedChanged += new EventHandler(this.CPULine_CheckedChanged);


            RAMLine.Text = "RAM Line";
            RAMLine.Checked = true;
            RAMLine.Location = new Point(10, 100);
            RAMLine.CheckedChanged += new EventHandler(this.RAMLine_CheckedChanged);


            HDDLine.Text = "HDD Line";
            HDDLine.Checked = true;
            HDDLine.Location = new Point(10, 120);
            HDDLine.CheckedChanged += new EventHandler(this.HDDLine_CheckedChanged);

            Area.AxisX.Minimum = double.Parse(DomainX.Text);
            Area.AxisX.Maximum = double.Parse(DomainY.Text);
            Area.AxisY.Minimum = double.Parse(RangeX.Text);
            Area.AxisY.Maximum = double.Parse(RangeY.Text);
            Area.AxisX.Title = "Time(s)";
            Area.AxisY.Title = "Percent (%)";

            this.Controls.Add(Domain);
            this.Controls.Add(DomainX);
            this.Controls.Add(DomainY);
            this.Controls.Add(Range);
            this.Controls.Add(RangeX);
            this.Controls.Add(RangeY);
            this.Controls.Add(CPULine);
            this.Controls.Add(RAMLine);
            this.Controls.Add(HDDLine);
        }

        private Label Domain = new Label();
        private TextBox DomainX = new TextBox();
        private TextBox DomainY = new TextBox();
        private Label Range = new Label();
        private TextBox RangeX = new TextBox();
        private TextBox RangeY = new TextBox();
        private CheckBox CPULine = new CheckBox();
        private CheckBox RAMLine = new CheckBox();
        private CheckBox HDDLine = new CheckBox();

    }
}
