using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace WindowsFormsApplication1
{
    class Realtime : Panel
    {
        //lines and area for the graph
        Series cpuData = new Series("CPU Line");
        Series ramData = new Series("RAM Line");
        Series hddData = new Series("HDD Line");
        ChartArea chartArea1 = new ChartArea();

        //colors for graph lines, values are default entry
        Color cpuColor = Color.FromArgb(255, 55, 55), ramColor = Color.FromArgb(55, 255, 55), hddColor = Color.FromArgb(55, 55, 255);

        //data manager
        Data Freeze = new Data();

        //does data display
        private void RunGraph_Tick(object sender, EventArgs e)
        {
            Freeze.createNewData();
            //tells data to create new info

            double time = (double)Freeze.locationTime() / 1000;
            //divides by 1000 since time is still in ms

            cpuData.Points.AddXY(time, Freeze.locationCpu());
            ramData.Points.AddXY(time, Freeze.locationRam());
            hddData.Points.AddXY(time, Freeze.locationHdd());
            //adds points to line

            Freeze.appendData();
            //adds points to file
        }


        //Set visibility of various lines
        private void CPUBoolean_CheckedChanged(object sender, EventArgs e)
        {
            //set different Graph objects as invisible/visible by removing and adding line
            if (CPUBoolean.Checked == true)
            { this.Graph.Series.Add(cpuData); }

            else
            { this.Graph.Series.Remove(cpuData); }
        }

        private void RAMBoolean_CheckedChanged(object sender, EventArgs e)
        {
            //set different Graph objects as invisible/visible
            if (RAMBoolean.Checked == true)
            { this.Graph.Series.Add(ramData); }

            else
            { this.Graph.Series.Remove(ramData); }
        }

        private void HDDBoolean_CheckedChanged(object sender, EventArgs e)
        {
            //set different Graph objects as invisible/visible
            if (HDDBoolean.Checked == true)
            { this.Graph.Series.Add(hddData); }

            else
            { this.Graph.Series.Remove(hddData); }
        }


        //changes the rate at which data is acquired
        private void GraphInterval_TextChanged(object sender, EventArgs e)
        {
            //gets value by parsing
            try
            { RunGraph.Interval = int.Parse(GraphInterval.Text) * 1000; }
            catch (FormatException) { GraphInterval.Text = ""; }
            catch (ArgumentOutOfRangeException) { GraphInterval.Text = ""; }
        }


        //exports
        private void ButtonExport_Click(object sender, EventArgs e)
        {
            try
            {
                Freeze.export(ExportLocation.Text);
            }
            catch (UnauthorizedAccessException) { ExportLocation.Text = "UnauthorizedAccess, choose a different location"; }
            catch (DirectoryNotFoundException) { ExportLocation.Text = "Directory not found"; }
        }


        //deals with graph color
        ///Cpu
        private void ColorCPU()
        {
            int r = int.Parse(CPUColorR.Text);
            int g = int.Parse(CPUColorG.Text);
            int b = int.Parse(CPUColorB.Text);

            cpuColor = Color.FromArgb(r, g, b);
            cpuData.Color = cpuColor;
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
        ///

        ///Ram
        private void ColorRAM()
        {
            int r = int.Parse(RAMColorR.Text);
            int g = int.Parse(RAMColorG.Text);
            int b = int.Parse(RAMColorB.Text);

            ramColor = Color.FromArgb(r, g, b);
            ramData.Color = ramColor;
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
        ///

        ///HDD
        private void ColorHDD()
        {
            int r = int.Parse(HDDColorR.Text);
            int g = int.Parse(HDDColorG.Text);
            int b = int.Parse(HDDColorB.Text);

            hddColor = Color.FromArgb(r, g, b);
            hddData.Color = hddColor;
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
        ///
        //


        //Changes the domain and Range
        private void DomainX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(DomainX.Text) < chartArea1.AxisX.Maximum)
                {
                    chartArea1.AxisX.Minimum = int.Parse(DomainX.Text);
                }
                else { DomainX.Text = ""; }
            }
            catch (FormatException) { DomainX.Text = ""; }
            catch (ArgumentOutOfRangeException) { DomainX.Text = ""; }
        }

        private void DomainY_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(DomainY.Text) > chartArea1.AxisX.Minimum)
                {
                    chartArea1.AxisX.Maximum = int.Parse(DomainY.Text);
                }
                else { DomainY.Text = ""; }
            }
            catch (FormatException) { DomainY.Text = ""; }
            catch (ArgumentOutOfRangeException) { DomainY.Text = ""; }
        }

        private void RangeX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(RangeX.Text) < chartArea1.AxisY.Maximum)
                {
                    chartArea1.AxisY.Minimum = int.Parse(RangeX.Text);
                }
                else { RangeX.Text = ""; }
            }
            catch (FormatException) { RangeX.Text = ""; }
            catch (ArgumentOutOfRangeException) { RangeX.Text = ""; }
        }

        private void RangeY_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(RangeY.Text) > chartArea1.AxisY.Minimum)
                {
                    chartArea1.AxisY.Maximum = int.Parse(RangeY.Text);
                }
                else { RangeY.Text = ""; }
            }
            catch (FormatException) { RangeY.Text = ""; }
            catch (ArgumentOutOfRangeException) { RangeY.Text = ""; }
        }
        //



        public Realtime() 
        {
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            // 
            // DomainLabel
            // 
            DomainLabel.AutoSize = true;
            DomainLabel.Location = new System.Drawing.Point(122, 42);
            DomainLabel.Name = "DomainLabel";
            DomainLabel.Size = new System.Drawing.Size(57, 13);
            DomainLabel.TabIndex = 12;
            DomainLabel.Text = "Domain (s)";
            // 
            // RangeLabel
            // 
            RangeLabel.AutoSize = true;
            RangeLabel.Location = new System.Drawing.Point(122, 82);
            RangeLabel.Name = "RangeLabel";
            RangeLabel.Size = new System.Drawing.Size(56, 13);
            RangeLabel.TabIndex = 15;
            RangeLabel.Text = "Range (%)";
            // 
            // CPUBoolean
            // 
            this.CPUBoolean.AutoSize = true;
            this.CPUBoolean.Checked = true;
            this.CPUBoolean.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CPUBoolean.Location = new System.Drawing.Point(12, 12);
            this.CPUBoolean.Name = "CPUBoolean";
            this.CPUBoolean.Size = new System.Drawing.Size(71, 17);
            this.CPUBoolean.TabIndex = 0;
            this.CPUBoolean.Text = "CPU Line";
            this.CPUBoolean.UseVisualStyleBackColor = true;
            this.CPUBoolean.CheckedChanged += new System.EventHandler(this.CPUBoolean_CheckedChanged);
            // 
            // CPUColorR
            // 
            this.CPUColorR.Location = new System.Drawing.Point(16, 35);
            this.CPUColorR.Name = "CPUColorR";
            this.CPUColorR.Size = new System.Drawing.Size(26, 20);
            this.CPUColorR.TabIndex = 1;
            this.CPUColorR.Text = "255";
            this.CPUColorR.TextChanged += new System.EventHandler(this.CPUColorR_TextChanged);
            // 
            // CPUColorG
            // 
            this.CPUColorG.Location = new System.Drawing.Point(48, 35);
            this.CPUColorG.Name = "CPUColorG";
            this.CPUColorG.Size = new System.Drawing.Size(26, 20);
            this.CPUColorG.TabIndex = 2;
            this.CPUColorG.Text = "55";
            this.CPUColorG.TextChanged += new System.EventHandler(this.CPUColorG_TextChanged);
            // 
            // CPUColorB
            // 
            this.CPUColorB.Location = new System.Drawing.Point(80, 35);
            this.CPUColorB.Name = "CPUColorB";
            this.CPUColorB.Size = new System.Drawing.Size(26, 20);
            this.CPUColorB.TabIndex = 3;
            this.CPUColorB.Text = "55";
            this.CPUColorB.TextChanged += new System.EventHandler(this.CPUColorB_TextChanged);
            // 
            // RAMColorB
            // 
            this.RAMColorB.Location = new System.Drawing.Point(80, 84);
            this.RAMColorB.Name = "RAMColorB";
            this.RAMColorB.Size = new System.Drawing.Size(26, 20);
            this.RAMColorB.TabIndex = 7;
            this.RAMColorB.Text = "55";
            this.RAMColorB.TextChanged += new System.EventHandler(this.RAMColorB_TextChanged);
            // 
            // RAMColorG
            // 
            this.RAMColorG.Location = new System.Drawing.Point(48, 84);
            this.RAMColorG.Name = "RAMColorG";
            this.RAMColorG.Size = new System.Drawing.Size(26, 20);
            this.RAMColorG.TabIndex = 6;
            this.RAMColorG.Text = "255";
            this.RAMColorG.TextChanged += new System.EventHandler(this.RAMColorG_TextChanged);
            // 
            // RAMColorR
            // 
            this.RAMColorR.Location = new System.Drawing.Point(16, 84);
            this.RAMColorR.Name = "RAMColorR";
            this.RAMColorR.Size = new System.Drawing.Size(26, 20);
            this.RAMColorR.TabIndex = 5;
            this.RAMColorR.Text = "55";
            this.RAMColorR.TextChanged += new System.EventHandler(this.RAMColorR_TextChanged);
            // 
            // RAMBoolean
            // 
            this.RAMBoolean.AutoSize = true;
            this.RAMBoolean.Checked = true;
            this.RAMBoolean.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RAMBoolean.Location = new System.Drawing.Point(12, 61);
            this.RAMBoolean.Name = "RAMBoolean";
            this.RAMBoolean.Size = new System.Drawing.Size(70, 17);
            this.RAMBoolean.TabIndex = 4;
            this.RAMBoolean.Text = "RAMLine";
            this.RAMBoolean.UseVisualStyleBackColor = true;
            this.RAMBoolean.CheckedChanged += new System.EventHandler(this.RAMBoolean_CheckedChanged);
            // 
            // HDDColorB
            // 
            this.HDDColorB.Location = new System.Drawing.Point(80, 133);
            this.HDDColorB.Name = "HDDColorB";
            this.HDDColorB.Size = new System.Drawing.Size(26, 20);
            this.HDDColorB.TabIndex = 11;
            this.HDDColorB.Text = "255";
            this.HDDColorB.TextChanged += new System.EventHandler(this.HDDColorB_TextChanged);
            // 
            // HDDColorG
            // 
            this.HDDColorG.Location = new System.Drawing.Point(48, 133);
            this.HDDColorG.Name = "HDDColorG";
            this.HDDColorG.Size = new System.Drawing.Size(26, 20);
            this.HDDColorG.TabIndex = 10;
            this.HDDColorG.Text = "55";
            this.HDDColorG.TextChanged += new System.EventHandler(this.HDDColorG_TextChanged);
            // 
            // HDDColorR
            // 
            this.HDDColorR.Location = new System.Drawing.Point(16, 133);
            this.HDDColorR.Name = "HDDColorR";
            this.HDDColorR.Size = new System.Drawing.Size(26, 20);
            this.HDDColorR.TabIndex = 9;
            this.HDDColorR.Text = "55";
            this.HDDColorR.TextChanged += new System.EventHandler(this.HDDColorR_TextChanged);
            // 
            // HDDBoolean
            // 
            this.HDDBoolean.AutoSize = true;
            this.HDDBoolean.Checked = true;
            this.HDDBoolean.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HDDBoolean.Location = new System.Drawing.Point(12, 110);
            this.HDDBoolean.Name = "HDDBoolean";
            this.HDDBoolean.Size = new System.Drawing.Size(73, 17);
            this.HDDBoolean.TabIndex = 8;
            this.HDDBoolean.Text = "HDD Line";
            this.HDDBoolean.UseVisualStyleBackColor = true;
            this.HDDBoolean.CheckedChanged += new System.EventHandler(this.HDDBoolean_CheckedChanged);
            // 
            // DomainX
            // 
            this.DomainX.Location = new System.Drawing.Point(113, 59);
            this.DomainX.Name = "DomainX";
            this.DomainX.Size = new System.Drawing.Size(32, 20);
            this.DomainX.TabIndex = 13;
            this.DomainX.Text = "0";
            this.DomainX.TextChanged += new System.EventHandler(this.DomainX_TextChanged);
            // 
            // DomainY
            // 
            this.DomainY.Location = new System.Drawing.Point(151, 59);
            this.DomainY.Name = "DomainY";
            this.DomainY.Size = new System.Drawing.Size(32, 20);
            this.DomainY.TabIndex = 14;
            this.DomainY.Text = "120";
            this.DomainY.TextChanged += new System.EventHandler(this.DomainY_TextChanged);
            // 
            // RangeY
            // 
            this.RangeY.Location = new System.Drawing.Point(152, 99);
            this.RangeY.Name = "RangeY";
            this.RangeY.Size = new System.Drawing.Size(32, 20);
            this.RangeY.TabIndex = 17;
            this.RangeY.Text = "100";
            this.RangeY.TextChanged += new System.EventHandler(this.RangeY_TextChanged);
            // 
            // RangeX
            // 
            this.RangeX.Location = new System.Drawing.Point(114, 99);
            this.RangeX.Name = "RangeX";
            this.RangeX.Size = new System.Drawing.Size(32, 20);
            this.RangeX.TabIndex = 16;
            this.RangeX.Text = "0";
            this.RangeX.TextChanged += new System.EventHandler(this.RangeX_TextChanged);
            // 
            // GraphInterval
            // 
            this.GraphInterval.Location = new System.Drawing.Point(16, 176);
            this.GraphInterval.Name = "GraphInterval";
            this.GraphInterval.Size = new System.Drawing.Size(32, 20);
            this.GraphInterval.TabIndex = 19;
            this.GraphInterval.Text = "1";
            this.GraphInterval.TextChanged += new System.EventHandler(this.GraphInterval_TextChanged);
            // 
            // GraphIntervalLabel
            // 
            this.GraphIntervalLabel.AutoSize = true;
            this.GraphIntervalLabel.Location = new System.Drawing.Point(13, 160);
            this.GraphIntervalLabel.Name = "GraphIntervalLabel";
            this.GraphIntervalLabel.Size = new System.Drawing.Size(166, 13);
            this.GraphIntervalLabel.TabIndex = 18;
            this.GraphIntervalLabel.Text = "Seconds between displayed Data";
            // 
            // ExportLocation
            // 
            this.ExportLocation.Location = new System.Drawing.Point(14, 312);
            this.ExportLocation.Name = "ExportLocation";
            this.ExportLocation.Size = new System.Drawing.Size(182, 20);
            this.ExportLocation.TabIndex = 22;
            this.ExportLocation.Text = "Libraries\\Documents\\example.txt";
            // 
            // ButtonExport
            // 
            this.ButtonExport.Location = new System.Drawing.Point(18, 338);
            this.ButtonExport.Name = "ButtonExport";
            this.ButtonExport.Size = new System.Drawing.Size(182, 23);
            this.ButtonExport.TabIndex = 23;
            this.ButtonExport.Text = "Export";
            this.ButtonExport.UseVisualStyleBackColor = true;
            this.ButtonExport.Click += new System.EventHandler(this.ButtonExport_Click);
            // 
            // ExportLabel
            // 
            this.ExportLabel.AutoSize = true;
            this.ExportLabel.Location = new System.Drawing.Point(15, 296);
            this.ExportLabel.Name = "ExportLabel";
            this.ExportLabel.Size = new System.Drawing.Size(79, 13);
            this.ExportLabel.TabIndex = 35;
            this.ExportLabel.Text = "Location of File";
            // 
            // RunGraph
            // 
            this.RunGraph.Enabled = true;
            this.RunGraph.Interval = 1000;
            this.RunGraph.Tick += new System.EventHandler(this.RunGraph_Tick);
            // 
            // Graph
            // 
            this.Graph.BackColor = System.Drawing.Color.Transparent;
            legend1.Name = "Legend1";
            this.Graph.Legends.Add(legend1);
            this.Graph.Location = new System.Drawing.Point(219, 0);
            this.Graph.Name = "Graph";
            this.Graph.Size = new System.Drawing.Size(977, 389);
            this.Graph.TabIndex = 0;
            this.Graph.Text = "chart1";

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
            this.Controls.Add(this.Graph);
            this.Controls.Add(this.ExportLabel);
            this.Controls.Add(this.ButtonExport);
            this.Controls.Add(this.ExportLocation);
            this.Controls.Add(this.GraphInterval);
            this.Controls.Add(this.GraphIntervalLabel);
            this.Controls.Add(this.RangeY);
            this.Controls.Add(this.RangeX);
            this.Controls.Add(RangeLabel);
            this.Controls.Add(this.DomainY);
            this.Controls.Add(this.DomainX);
            this.Controls.Add(DomainLabel);
            this.Controls.Add(this.HDDColorB);
            this.Controls.Add(this.HDDColorG);
            this.Controls.Add(this.HDDColorR);
            this.Controls.Add(this.HDDBoolean);
            this.Controls.Add(this.RAMColorB);
            this.Controls.Add(this.RAMColorG);
            this.Controls.Add(this.RAMColorR);
            this.Controls.Add(this.RAMBoolean);
            this.Controls.Add(this.CPUColorB);
            this.Controls.Add(this.CPUColorG);
            this.Controls.Add(this.CPUColorR);
            this.Controls.Add(this.CPUBoolean);

            //sets graph area properties
            chartArea1.AxisX.Title = "Time (s)";
            chartArea1.AxisY.Title = "Percent (%)";
            chartArea1.AxisX.Minimum = int.Parse(DomainX.Text);
            chartArea1.AxisX.Maximum = int.Parse(DomainY.Text);
            chartArea1.AxisY.Minimum = int.Parse(RangeX.Text);
            chartArea1.AxisY.Maximum = int.Parse(RangeY.Text);
            //adds to form
            this.Graph.ChartAreas.Add(chartArea1);

            //sets line properties
            cpuData.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            ramData.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            hddData.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            //sets line color
            cpuData.Color = cpuColor;
            ramData.Color = ramColor;
            hddData.Color = hddColor;

            //adds lines to chart
            this.Graph.Series.Add(cpuData);
            this.Graph.Series.Add(ramData);
            this.Graph.Series.Add(hddData);
        }

        Label DomainLabel = new Label();
        Label RangeLabel = new Label();
        private CheckBox CPUBoolean = new CheckBox();
        private TextBox CPUColorR = new TextBox();
        private TextBox CPUColorG = new TextBox();
        private TextBox CPUColorB = new TextBox();
        private TextBox RAMColorB = new TextBox();
        private TextBox RAMColorG = new TextBox();
        private TextBox RAMColorR = new TextBox();
        private CheckBox RAMBoolean = new CheckBox();
        private TextBox HDDColorB = new TextBox();
        private TextBox HDDColorG = new TextBox();
        private TextBox HDDColorR = new TextBox();
        private CheckBox HDDBoolean = new CheckBox();
        private TextBox DomainX = new TextBox();
        private TextBox DomainY = new TextBox();
        private TextBox RangeY = new TextBox();
        private TextBox RangeX = new TextBox();
        private TextBox GraphInterval = new TextBox();
        private Label GraphIntervalLabel = new Label();
        private TextBox ExportLocation = new TextBox();
        private Button ButtonExport = new Button();
        private Label ExportLabel = new Label();
        private Timer RunGraph= new Timer();
        private Chart Graph = new Chart();

        internal Data Data
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }



    }
}
