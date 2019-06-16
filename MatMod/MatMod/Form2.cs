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
    public partial class Form2 : Form
    {
        public int R;
        public double[,] table;
        public double[] r;
        public int[] v;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainWindow nw = new MainWindow();
            nw.Show();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "") R = 1;
            else
            {
                R = Convert.ToInt16(textBox1.Text);
                dataGridView1.RowCount = R;
                dataGridView1.ColumnCount = R;
                for (int i = 0; i < R; i++)
                {
                    dataGridView1.Columns[i].Name = Convert.ToString(i + 1);
                    dataGridView1.Rows[i].HeaderCell.Value = Convert.ToString(i + 1);
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            table = new double[R, R];
            r = new double[R];
            v = new int[R];
            int s_i = Convert.ToInt32(textBox2.Text) - 1;
            double tmp, min;
            int mini;
            for (int i = 0; i < R; i++)
            {
                for (int j = 0; j < R; j++)
                {
                    if (dataGridView1[j, i].Value == null) table[i, j] = 0;
                    else table[i, j] = Convert.ToDouble(dataGridView1[j, i].Value);
                }
            }
            for (int i = 0; i < R; i++)
            {
                r[i] = 10000;
                v[i] = 1;
            }
            r[s_i] = 0;
            do
            {
                mini = 10000;
                min = 10000;
                for (int i = 0; i < R; i++)
                {
                    if ((v[i] == 1) && (r[i] < min))
                    {
                        min = r[i];
                        mini = i;
                    }
                }
                if (mini != 10000)
                {
                    for (int i = 0; i < R; i++)
                    {
                        if (table[mini, i] > 0)
                        {
                            tmp = min + table[mini, i];
                            if (tmp < r[i])
                            {
                                r[i] = tmp;
                            }
                        }
                    }
                    v[mini] = 0;
                }
            } while (mini < 10000);
            label2.Text = "Минимальное расстояние: ";
            label2.Text += Convert.ToString(r[Convert.ToInt16(textBox3.Text) - 1]) + " ";
            int[] ver = new int[R];
            int end = Convert.ToInt16(textBox3.Text) - 1;
            ver[0] = end + 1;
            int k = 1;
            double weight = r[end];
            while (end != s_i)
            {
                for (int i = 0; i < R; i++)
                    if (table[end, i] != 0)
                    {
                        tmp = weight - table[end, i];
                        if (tmp == r[i])
                        {
                            weight = tmp;
                            end = i;
                            ver[k] = i + 1;
                            k++;
                        }
                    }
            }
            label3.Text = "Путь: ";
            for (int i = k - 1; i >= 0; i--) label3.Text += Convert.ToString(ver[i]) + " ";
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1[dataGridView1.SelectedCells[0].RowIndex, dataGridView1.SelectedCells[0].ColumnIndex].Value = dataGridView1.SelectedCells[0].Value;
        }
    }
}
