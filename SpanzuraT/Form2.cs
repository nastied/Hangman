using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SpanzuraT
{
    public partial class Form2 : Form
    {
        int cnt;
        int mistakes = 0;
        int[] fr = { 0, 0, 0, 0, 0 };
        Form1 formainitiala = null;

        bool wordFound = false;
        string cuvant;
        int nrl;

        List<DrawnLetter> correctLetters = new List<DrawnLetter>();

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string nume1, string nume2, string nume3, string nume4, string nume5, Form1 form1)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            int i = 0;
            fr[i++] = (nume1 != "0") ? 0 : -1;
            fr[i++] = (nume2 != "0") ? 1 : -1;
            fr[i++] = (nume3 != "0") ? 2 : -1;
            fr[i++] = (nume4 != "0") ? 3 : -1;
            fr[i++] = (nume5 != "0") ? 4 : -1;

            formainitiala = form1;

            panel1.Paint += new PaintEventHandler(panel1_Paint);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\img\\0.jpg";
            pictureBox16.Image = Image.FromFile(path);

            string connstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DataSpanzurata.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection conn = new SqlConnection(connstring))
            {
                Random rnd = new Random();

                while (!wordFound)
                {
                    int a = rnd.Next(1, 25);
                    conn.Open();

                    string sqlCommand = "SELECT * from cuv WHERE numar='" + a + "';";
                    using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            for (int i = 0; i <= 4; i++)
                            {
                                if (fr[i] != -1 && fr[i] == Convert.ToInt32(reader[0]))
                                {
                                    wordFound = true;
                                    cuvant = reader[3].ToString().Trim().ToUpper();
                                    nrl = cuvant.Length;

                                    cnt = 0;
                                    break;  
                                }
                            }
                        }
                    }
                    conn.Close();
                }
            }

            for (int i = 1; i <= 15; i++)
            {
                PictureBox pbx = this.Controls.Find("pictureBox" + i, true).FirstOrDefault() as PictureBox;
                if (pbx != null)
                {
                    pbx.Visible = (i <= nrl);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Font fontMesaj = new Font("Poor Richard", 40);
            SolidBrush brush = new SolidBrush(Color.Black);

            foreach (DrawnLetter letter in correctLetters)
            {
                e.Graphics.DrawString(letter.Letter, fontMesaj, brush, letter.X, letter.Y);
            }
            brush.Dispose();
            fontMesaj.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string letterClicked = btn.Text.ToUpper();
            bool guessedRight = false;

            for (int i = 0; i < nrl; i++)
            {
                if (cuvant[i].ToString() == letterClicked)
                {
                    int letterX = 65 * i + (i - 1) * 8 + 20;

                    correctLetters.Add(new DrawnLetter { Letter = letterClicked, X = letterX, Y = 10 });

                    cnt++;
                    guessedRight = true;
                }
            }

            btn.Enabled = false;

            panel1.Invalidate();

            if (!guessedRight)
            {
                btn.ForeColor = Color.Red;
                mistakes++;
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\img\\" + mistakes + ".jpg";
                pictureBox16.Image = Image.FromFile(path);
            }

            if (mistakes == 7)
            {
                MessageBox.Show("You lost! Better luck next time. Your word was: " + cuvant);
                CloseGame();
            }
            else if (cnt == nrl)
            {
                MessageBox.Show("You won! Your word is: " + cuvant);
                CloseGame();
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Your word was: " + cuvant + ". Better luck next time! I believe in you!");
            CloseGame();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void CloseGame()
        {
            this.Hide();
            formainitiala.Show();
        }

        private void Form2_Click(object sender, EventArgs e) { }
        private void button1_MouseClick(object sender, MouseEventArgs e) { }
    }

    public class DrawnLetter
    {
        public string Letter;
        public int X;
        public int Y;
    }
}