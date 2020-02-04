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
using Newtonsoft.Json;

namespace SortingApplication
{
    public partial class Form1 : Form
    {
        List<int> numberList = new List<int>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var addNumber =Convert.ToInt32(numericUpDown1.Value);
            numberList.Add(addNumber);
            loadNumberlist();
        }

        private void loadNumberlist()
        {
            lbNumbers.DataSource = new BindingSource(numberList, null);
        }

        //clear sorted list and numbers to sort
        private void button2_Click(object sender, EventArgs e)
        {
            numberList.Clear();
            lbSortedList.Items.Clear();
            loadNumberlist();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {

            if(numberList.Count() != 0)
            {
                var numArr = numberList.ToArray();

               
                var sortedList = "";
                foreach (var i in numArr)
                {
                    if (sortedList == "")
                    {
                        sortedList += i.ToString();
                    }
                    else
                    {
                        sortedList += " ," + i.ToString();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please provide a list of numbers");
            }

        }
        

        class QSObj
        {
            public int NumberProp { get; set; }
            public string StringProp { get; set; }
            public double DoubleProp { get; set; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (StreamReader file = File.OpenText(@"D:\WORLD SKILLS\Notes,References & Resources\QuickSort\QuickSort.json"))
            {
                var json = file.ReadToEnd();

                var qsobjList = JsonConvert.DeserializeObject<List<QSObj>>(json).ToArray();

                var qsobjListOriginal = JsonConvert.DeserializeObject<List<QSObj>>(json);

                //qsobjListOriginal.Sort();


                var x = new List<QSObj>();
                foreach (var i in qsobjListOriginal)
                {
                    //var qsObj = $@"{i.NumberProp}, {i.StringProp}, {i.NumberProp}";
                    //Console.WriteLine(qsObj);
                    x.Add(i);
                }
                Console.WriteLine(x.OrderByDescending(a => a.DoubleProp).FirstOrDefault().NumberProp);

                //So logically, there are 3 properties that we must sort by
                var properties = new List<string>() { "NumberProp", "StringProp", "DoubleProp" };

                foreach(var prop in properties)
                {
                    quick_sort(qsobjList, 0, qsobjList.Length - 1, prop);
                }

                foreach(var item in qsobjList)
                {
                    var qsObj = $@"{item.NumberProp}, {item.StringProp}, {item.NumberProp}";

                    lbSortedList.Items.Add(qsObj);
                }
            }

        }

        private static void quick_sort(QSObj[] numArr, int low, int high, string property)
        {
            if (property == "NumberProp")
            {
                if (low < high)
                {
                    var partIdx = partition(numArr, low, high);

                    quick_sort(numArr, low, partIdx - 1 , property);
                    quick_sort(numArr, partIdx + 1, high, property);
                }
            }
            else if (property == "StringProp")
            {
                if (low < high)
                {
                    var partIdx = Stringpartition(numArr, low, high);

                    quick_sort(numArr, low, partIdx - 1, property);
                    quick_sort(numArr, partIdx + 1, high, property);
                }
            }
            else
            {
                if (low < high)
                {
                    var partIdx = Doublepartition(numArr, low, high);

                    quick_sort(numArr, low, partIdx - 1, property);
                    quick_sort(numArr, partIdx + 1, high, property);
                }
            }

            
        }

        private static int partition(QSObj[] numArr, int low, int high)
        {
            int pivot = numArr[high].NumberProp;

            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                if (numArr[j].NumberProp < pivot)
                {
                    i++;

                    QSObj tempNum = numArr[i];
                    numArr[i] = numArr[j];
                    numArr[j] = tempNum;
                }
            }

            QSObj tempNum1 = numArr[i + 1];
            numArr[i + 1] = numArr[high];
            numArr[high] = tempNum1;

            return i + 1;
        }

        private static int Stringpartition(QSObj[] numArr, int low, int high)
        {
            string pivot = numArr[high].StringProp;

            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                var comparison = string.Compare(numArr[j].StringProp, pivot);

                if (comparison == -1)
                {
                    i++;

                    QSObj tempNum = numArr[i];
                    numArr[i] = numArr[j];
                    numArr[j] = tempNum;
                }
            }

            QSObj tempNum1 = numArr[i + 1];
            numArr[i + 1] = numArr[high];
            numArr[high] = tempNum1;

            return i + 1;
        }

        private static int Doublepartition(QSObj[] numArr, int low, int high)
        {
            double pivot = numArr[high].DoubleProp;

            int i = (low - 1);
            for (int j = low; j < high; j++)
            {

                if (numArr[j].DoubleProp < pivot)
                {
                    i++;

                    QSObj tempNum = numArr[i];
                    numArr[i] = numArr[j];
                    numArr[j] = tempNum;
                }
            }

            QSObj tempNum1 = numArr[i + 1];
            numArr[i + 1] = numArr[high];
            numArr[high] = tempNum1;

            return i + 1;
        }




    }
}
