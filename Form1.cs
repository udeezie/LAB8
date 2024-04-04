using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB8
{
    public partial class Form1 : Form
    {

        delegate void AddListItem(double number);
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int count))
            {
                if (count <= 0 || count > 1000)
                {
                    MessageBox.Show("Please enter a positive integer less than or equal to 1000.");
                    return;
                }

                listBox1.Items.Clear();
                try
                {
                    await Task.Run(() => CalculateFibonacci(count));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter an appropriate positive integer.");
            }
        }

        private void CalculateFibonacci(int n)
        {
            double a = 0;
            double b = 1;

            try
            {
                for (int i = 0; i < n; i++)
                {
                    double temp = a;
                    a = b;
                    b = temp + b;

                    UpdateListBoxSafe(b);
                }
            }
            catch (Exception)
            {
                
                throw; 
            }
        }
        
        private void UpdateListBoxSafe(double number)
        {
            if (listBox1.InvokeRequired)
            {
                try
                {
                    listBox1.Invoke(new MethodInvoker(delegate { listBox1.Items.Add(number); }));
                }
                catch (ObjectDisposedException)
                {
                   
                }
                catch (InvalidOperationException)
                {
                    
                }
            }
            else
            {
                listBox1.Items.Add(number);
            }
        }
    }
}


