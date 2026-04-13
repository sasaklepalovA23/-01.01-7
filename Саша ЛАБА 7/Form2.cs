using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Саша_ЛАБА_7
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int m = (int)numRows.Value;
            int n = (int)numCols.Value;

            dgvMatrix.RowCount = m;
            dgvMatrix.ColumnCount = n;

            Random rnd = new Random();

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    dgvMatrix.Rows[i].Cells[j].Value = rnd.Next(-27, 44);
                    dgvMatrix.Columns[j].Width = 40;
                }
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (dgvMatrix.RowCount == 0) return;

            int maxCount = -1;
            int bestColumn = -1;
            for (int j = 0; j < dgvMatrix.ColumnCount; j++)
            {
                int countDiv5 = 0;
                for (int i = 0; i < dgvMatrix.RowCount; i++)
                {
                    if (dgvMatrix.Rows[i].Cells[j].Value != null)
                    {
                        int val = Convert.ToInt32(dgvMatrix.Rows[i].Cells[j].Value);
                        if (val % 5 == 0) countDiv5++;
                    }
                }
                if (countDiv5 > maxCount)
                {
                    maxCount = countDiv5;
                    bestColumn = j + 1; 
                }
            }

            lblResult.Text = $"Результат: Столбец №{bestColumn} (чисел: {maxCount})";
        }

        private void bthSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "Text|*.txt" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {
                    sw.WriteLine($"{dgvMatrix.RowCount}|{dgvMatrix.ColumnCount}");
                    for (int i = 0; i < dgvMatrix.RowCount; i++)
                    {
                        for (int j = 0; j < dgvMatrix.ColumnCount; j++)
                            sw.Write(dgvMatrix.Rows[i].Cells[j].Value + ";");
                        sw.WriteLine();
                    }
                }
            }
        }
        private void bthLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "Text|*.txt" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string[] lines = File.ReadAllLines(ofd.FileName);
                string[] dim = lines[0].Split('|');
                dgvMatrix.RowCount = int.Parse(dim[0]);
                dgvMatrix.ColumnCount = int.Parse(dim[1]);

                for (int i = 0; i < dgvMatrix.RowCount; i++)
                {
                    string[] vals = lines[i + 1].Split(';');
                    for (int j = 0; j < dgvMatrix.ColumnCount; j++)
                        dgvMatrix.Rows[i].Cells[j].Value = vals[j];
                }
            }
        }
    }
}