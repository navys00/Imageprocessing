using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Hist : Form
    {
        int[] items;

        public Hist(int[] charitems)
        {
            InitializeComponent();
            this.items = charitems;
            this.label1.Text = trackBar1.Value.ToString();
            Definechart(1);
        }
        private void Definechart(int step)
        {
            int[] seriesArray = new int[items.Length / step];
            for (int i = 0; i < seriesArray.Length; i++)
                for (int j = 0; j < step; j++)
                    seriesArray[i] = items[i * step + j];


            chart1.Series[0].Points.DataBindY(seriesArray);
        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.label1.Text = trackBar1.Value.ToString();
            Definechart(trackBar1.Value);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
