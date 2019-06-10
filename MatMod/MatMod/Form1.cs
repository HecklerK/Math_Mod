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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int R, C;
        public double[,] table;
        public int MainCol, MainRow;
        public void CT()
        {
            if (textBox1.Text == "") R = 1;
            else R = Convert.ToInt16(textBox1.Text);
            if (textBox2.Text == "") C = 1;
            else C = Convert.ToInt16(textBox2.Text);
            dataGridView1.RowCount = R;
            dataGridView1.ColumnCount = C;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            CT();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            CT();
        }

        public void MainItem()
        {
            MainCol = 1;
            for (int j = 2; j < C; j++) if (table[R - 1, j] < table[R - 1, MainCol]) MainCol = j;
            MainRow = 0;
            for (int i = 0; i < R - 1; i++)
                if (table[i, MainCol] > 0)
                {
                    MainRow = i;
                    break;
                }
            for (int i = MainRow + 1; i < R - 1; i++) if ((table[i, MainCol] > 0) && ((table[i, 0] / table[i, MainCol]) < (table[MainRow, 0] / table[MainRow, MainCol]))) MainRow = i;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainWindow nw = new MainWindow();
            nw.Show();
            this.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainWindow nw = new MainWindow();
            nw.Show();
        }

        private bool End()
        {
            bool flag = true;
            for (int j = 1; j < C; j++)
            {
                if (table[R - 1, j] < 0)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            table = new double[R, C + R - 1];
            for (int i = 0; i < R; i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (i == R - 1 && j < C) table[i, j] = Convert.ToDouble(dataGridView1[j, i].Value) * -1;
                    else if (j < C) table[i, j] = Convert.ToDouble(dataGridView1[j, i].Value);
                    else table[i, j] = 0;
                }
            }
            while (!End())
            {
                MainItem();
                double[,] new_table = new double[R, C];
                for (int j = 0; j < C; j++) new_table[MainRow, j] = table[MainRow, j] / table[MainRow, MainCol];
                    for (int i = 0; i < R; i++)
                    {
                        if (i == MainRow) continue;
                        for (int j = 0; j < C; j++) new_table[i, j] = table[i, j] - table[i, MainCol] * new_table[MainRow, j];
                    }
                table = new_table;
                dataGridView2.RowCount = table.GetLength(0);
                dataGridView2.ColumnCount = table.GetLength(1);
                for (int i = 0; i < R; i++)
                {
                    for (int j = 0; j < C; j++)
                    {
                        if (i == R - 1 && j != 0) dataGridView2[j, i].Value = Convert.ToString(table[i, j] * -1);
                        else dataGridView2[j, i].Value = table[i, j].ToString();
                    }
                }
            }
        }
    }
}
