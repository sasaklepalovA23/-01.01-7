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

namespace Саша_ЛАБА_7
{
    public partial class Form1 : Form
    {
        int[] A;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int n) && n > 0)
            {
                A = new int[n];
                Random rnd = new Random();
                listBox1.Items.Clear(); // Очищаем список перед заполнением

                for (int i = 0; i < n; i++)
                {
                    A[i] = rnd.Next(-100, 101); // Случайные числа от -100 до 100
                    listBox1.Items.Add($"A[{i}] = {A[i]}");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите целое число элементов в textBox1");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (A == null || A.Length == 0)
            {
                MessageBox.Show("Сначала создайте массив!");
                return;
            }

            // Находим минимальное и максимальное
            int min = A.Min();
            int max = A.Max();

            // Выводим результаты в textBox2 и textBox3
            textBox2.Text = min.ToString();
            textBox3.Text = max.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (A == null) return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Текстовые файлы (*.txt)|*.txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // Записываем каждый элемент массива в файл с новой строки
                File.WriteAllLines(sfd.FileName, A.Select(x => x.ToString()));
                MessageBox.Show("Массив успешно сохранен в файл!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Текстовые файлы (*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Читаем все строки из файла
                string[] lines = File.ReadAllLines(ofd.FileName);
                // Превращаем строки в массив чисел
                A = lines.Select(int.Parse).ToArray();

                // Обновляем визуальные элементы
                listBox1.Items.Clear();
                for (int i = 0; i < A.Length; i++)
                {
                    listBox1.Items.Add($"A[{i}] = {A[i]}");
                }
                textBox1.Text = A.Length.ToString();
                MessageBox.Show("Данные загружены из файла!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }
    }
}