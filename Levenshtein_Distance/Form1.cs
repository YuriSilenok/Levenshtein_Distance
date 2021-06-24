using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Levenshtein_Distance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0 && textBox2.Text.Length != 0)
                label1.Text = D(textBox1.Text.Length - 1, textBox2.Text.Length - 1).ToString();
            else label1.Text = "";
        }

        private int D(int i, int j)
        {
            if (i == 0 && j == 0) return 0;
            if (j == 0 && i > 0) return i;
            if (i == 0 && j > 0) return j;
            if (j > 0 && i > 0)
            {
                return min(
                    D(i, j - 1) + 1,
                    D(i - 1, j) + 1,
                    D(i - 1, j - 1) + ((textBox1.Text[i] == textBox2.Text[j]) ? 0 : 1)
                   );
            }
            throw new Exception("Входные параметры не могут быть отрицательными");
        }
        private int min(int a, int b, int c)
        {
            return (a < b) ? ((a < c) ? a : c) : ((b < c) ? b : c);
        }

        int[] fibo = new int[] { 1, 2, 3, 5, 8, 13, 21 };
        
        private int fiboF(int n)
        {
            int first = 1;
            int second = 1;
            for (int i = 0; i < n; i++)
            {
                int f = first + second;
                first = second;
                second = f;
            }
            return second;
        }

        private int indexfOfFib(int[] array, int val, int indStart, int indStop)
        {

            if (val == array[indStart]) return indStart;
            if (val == array[indStop]) return indStop;
            ///если границы стали малы на столько что границы стоят 
            ///рядом или накладываются друг на друга, 
            ///то искомого значения нет в масиве
            if (indStop - indStart < 2) return -1;
            
            for (int i = 1; ; i++)
            {
                int newIndStart = indStart + fiboF(i - 1);
                int newIndStop = indStop + fiboF(i);
                if (newIndStop > indStop) newIndStop = indStop;
                if (array[newIndStop] == val) return newIndStop;
                ///нужно взять интервал который ограничен числами фибоначи относительно левой границы массива
                if (val < array[newIndStop]) return indexfOfFib(array, val, newIndStart, newIndStop);
                if (newIndStop == indStart && val > array[newIndStop]) return -1;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if(textBox3.Text.Length != 0 && textBox4.Text.Length == 1)
            {
                List<int> list = new List<int>();
                foreach (char ch in textBox3.Text)
                    list.Add((int)ch);
                int[] array = list.ToArray();
                int val = textBox4.Text[0];
                label2.Text = indexfOfFib(array, val, 0, array.Length - 1).ToString();
            }
        }
    }
}
