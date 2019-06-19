using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatMod
{
    public partial class Form3 : Form
    {
        int R;
        int RW;
        public Form3()
        {
            InitializeComponent();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "") R = 1;
            else
            {
                R = Convert.ToInt16(textBox1.Text);
            }
            dataGridView1.RowCount = R;
            for (int i = 0; i < R; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = Convert.ToString(i + 1);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int[] mas = new int[R];
            int[] w = new int[R];
            int k = Convert.ToInt32(textBox2.Text);
            double a = Convert.ToDouble(textBox3.Text);
            for (int i = 0; i < R; i++)
            {
                mas[i] = Convert.ToInt32(dataGridView1[0, i].Value);
            }
            for (int i = 0; i < RW; i++)
            {
                w[i] = Convert.ToInt32(dataGridView2[0, i].Value);
            }
            dataGridView1.Rows.Add();
            for (int i = k; i <= R; i++)
            {
                double s = 0;
                for (int j = i - k; j < i; j++)
                {
                    s += mas[j];
                }
                dataGridView1[1, i].Value = s / k;
            }
            for (int i = k; i <= R; i++)
            {
                double s = 0;
                double sw = 0;
                int c = 0;
                for (int j = i - k; j < i; j++)
                {
                    s += mas[j] * w[c];
                    sw += w[c];
                    c++;
                }
                dataGridView1[2, i].Value = s / sw;
            }
            dataGridView1[3, 0].Value = dataGridView1[0, 0].Value;
            for (int i = 1; i < R + 1; i++)
            {
                dataGridView1[3, i].Value = Convert.ToDouble(dataGridView1[3, 0].Value) + a * (Convert.ToDouble(dataGridView1[0, i].Value) - Convert.ToDouble(dataGridView1[3, 0].Value));
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "") RW = 1;
            else
            {
                RW = Convert.ToInt16(textBox2.Text);
            }
            dataGridView2.RowCount = RW;
            for (int i = 0; i < RW; i++)
            {
                dataGridView2.Rows[i].HeaderCell.Value = Convert.ToString(i + 1);
            }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainWindow nw = new MainWindow();
            nw.Show();
        }
    }
}
