using System;
using System.Windows.Forms;

namespace SpanzuraT
{
    public partial class Form1 : Form
    {
        string c1 = "0", c2 = "0", c3 = "0", c4 = "0", c5 = "0";
        int ok = 0;

        public Form1()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) { c1 = checkBox1.Text; ok++; }
            else { c1 = "0"; ok--; }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked) { c2 = checkBox2.Text; ok++; }
            else { c2 = "0"; ok--; }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked) { c3 = checkBox3.Text; ok++; }
            else { c3 = "0"; ok--; }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked) { c4 = checkBox4.Text; ok++; }
            else { c4 = "0"; ok--; }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked) { c5 = checkBox5.Text; ok++; }
            else { c5 = "0"; ok--; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ok > 0)
            {
                Form2 form2 = new Form2(c1, c2, c3, c4, c5, this);
                this.Hide();
                form2.Show();

                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
            }
            else
            {
                MessageBox.Show("Please select one or more categories first.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void groupBox1_Enter(object sender, EventArgs e) { }
    }
}