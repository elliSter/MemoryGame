using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory
{
    public partial class Form1 : Form
    {
        System.Media.SoundPlayer startSoundPlayer = new System.Media.SoundPlayer(@"C:\Users\Elli\source\repos\WindowsFormsApp1\clap.wav");
        Random random = new Random();

        List<string> images = new List<string>()
        {
            "p","p","f","f","(","(",")",")","Y","Y","'","'","!","!","m","m"
        };

        Label firstClicked, secClicked;

        public Form1()
        {
            InitializeComponent();
            PutImagesIntoSquares();
        }

        private void clickedSquare(object sender, EventArgs e)
        {
            if (firstClicked != null && secClicked != null)
                return; //gia na min mporoun na pati8oun perissotera apo 2 imgs

            Label clickedLbl = sender as Label;

            if (clickedLbl == null)
                return;
            if (clickedLbl.ForeColor == Color.Black)//an einai idi anoigmeno
                return;
            if (firstClicked == null)
            {
                firstClicked = clickedLbl;
                firstClicked.ForeColor = Color.Black; //an pati8ei ki einai to prwto na "anoi3ei"
                return;
            }

            secClicked = clickedLbl;
            secClicked.ForeColor = Color.Black;

            WinOrNot();

            //an exoun anoix8ei idia
            if (firstClicked.Text == secClicked.Text)
            {
                firstClicked = null;
                secClicked = null;
            }
            else
                timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        { //an perasei o xronos na 3anakryftoun
            timer1.Stop();
            firstClicked.ForeColor = firstClicked.BackColor;
            secClicked.ForeColor = secClicked.BackColor;

            firstClicked = null;
            secClicked = null;
        }

        private void PutImagesIntoSquares()
        {
            Label label;
            int randomNumber;

            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                if (tableLayoutPanel1.Controls[i] is Label)
                    label = (Label)tableLayoutPanel1.Controls[i];
                else
                    continue;
                randomNumber = random.Next(0, images.Count);
                label.Text = images[randomNumber];

                images.RemoveAt(randomNumber);
            }

        }

        private void WinOrNot()
        {
            Label label;
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                label = tableLayoutPanel1.Controls[i] as Label;

                if (label != null && label.ForeColor == label.BackColor)//an den exei anoixtei
                    return; //mi kaneis tpt

            }
            startSoundPlayer.Play();
            MessageBox.Show("Well done mate");
            Close();
        }
    }
}
