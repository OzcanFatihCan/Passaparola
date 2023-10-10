using EntityLayer;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Passaparola
{
    public partial class Form1 : Form
    {

        private List<EntitySorular> sorular;
        private char[] harfler = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'İ', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'R', 'S', 'T', 'U', 'V', 'Y', 'Z' };
        int soruno = 0, dogru = 0, yanlis = 0;

        public Form1()
        {
            InitializeComponent();     
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Harfler();
            sorular = LogicSorular.LLSorular();
            textBox1.Enabled = false;

            
        }

        private void Harfler()
        {
            int n = harfler.Length;
            double aciAraligi = 360.0 / n;
   
            for (int i = 0; i < n; i++)
            {
                char currentHarf = harfler[i];
                Button yuvarlakDugme = new Button();
                yuvarlakDugme.Size = new Size(45, 45); 
                yuvarlakDugme.Margin = new Padding(5); 

                // Düğmeyi bir daire üzerinde yerleştirme
                double aci = i * aciAraligi;
                int x = (int)(Math.Cos(Math.PI * aci / 180) * 120) + 240; 
                int y = (int)(Math.Sin(Math.PI * aci / 180) * 120) + 250; 

                // Butonun konumunu bir miktar içeri alarak boşluk bırak
                int xOffset = (int)(Math.Cos(Math.PI * aci / 180) * 80);
                int yOffset = (int)(Math.Sin(Math.PI * aci / 180) * 80);

                yuvarlakDugme.Location = new Point(x + xOffset, y + yOffset);

                yuvarlakDugme.FlatStyle = FlatStyle.Flat;               
                yuvarlakDugme.BackColor = Color.Gray;
                yuvarlakDugme.FlatAppearance.BorderSize = 0;
                yuvarlakDugme.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 192, 192);
                yuvarlakDugme.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 224, 224);
                yuvarlakDugme.Cursor = Cursors.Hand;

                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(0, 0, yuvarlakDugme.Width, yuvarlakDugme.Height);
                yuvarlakDugme.Region = new Region(path);

                // Her bir butonun click olayına farklı bir işlev atama
                yuvarlakDugme.Click += (sender, e) => Harfler_Click(currentHarf);

                yuvarlakDugme.Text = harfler[i].ToString(); // Butonun üzerine harfi yaz
                yuvarlakDugme.TextAlign = ContentAlignment.MiddleCenter;

                Controls.Add(yuvarlakDugme);
            }


            // Ana yuvarlak butonların çevresinde bir yuvarlak oluştur
            Button anaYuvarlak = new Button();
            anaYuvarlak.Size = new Size(340, 340);
            anaYuvarlak.Location = new Point(93, 104); // Formun ortasına yerleştir
            anaYuvarlak.FlatStyle = FlatStyle.Flat;
            anaYuvarlak.BackColor = Color.Gray;
            anaYuvarlak.FlatAppearance.BorderSize = 0;
            anaYuvarlak.Cursor = Cursors.Hand;

            System.Drawing.Drawing2D.GraphicsPath anaPath = new System.Drawing.Drawing2D.GraphicsPath();
            anaPath.AddEllipse(0, 0, anaYuvarlak.Width, anaYuvarlak.Height);
            anaYuvarlak.Font = new Font(anaYuvarlak.Font.FontFamily, 48, anaYuvarlak.Font.Style);
            anaYuvarlak.Region = new Region(anaPath);
            anaYuvarlak.Click += AnaYuvarlak_Click;

            // Ana yuvarlak butonu form'a ekle
            Controls.Add(anaYuvarlak);
        }
      

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.Text = "Sonraki";
            textBox1.Enabled = true;
            textBox1.Text = "";
            soruno++;
            this.Text = soruno + ". Soru";

            if (soruno <= sorular.Count)
            {
                richTextBox1.Text = sorular[soruno - 1].SORU;
                RenkAyarla(sorular[soruno - 1].CEVAP[0], Color.Violet) ;
                linkLabel1.Enabled = false;
                
                
            }
            else
            {
                MessageBox.Show("Sorular Bitti!");
            }

        }

        private class Soru
        {
            public string Metin { get; }
            public char Harf { get; }

            public Soru(string metin, char harf)
            {
                Metin = metin;
                Harf = harf;
            }
        }


        private void RenkAyarla(char harf, Color arkaplanRenk)
        {
            // Harf'e sahip butonu bul ve arkaplan rengini ayarla
            foreach (Control control in this.Controls)
            {
                if (control is Button && control.Text == harf.ToString())
                {
                    control.BackColor = arkaplanRenk;
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode==Keys.Enter)
            {
                linkLabel1.Enabled = true;
                textBox1.Enabled = false;
                if (soruno<=sorular.Count)
                {
                    string veritabaniCevap = sorular[soruno - 1].CEVAP;
                    string kullaniciCevap = textBox1.Text.Trim();

                    if (kullaniciCevap.Equals(veritabaniCevap, StringComparison.OrdinalIgnoreCase))
                    {
                        RenkAyarla(sorular[soruno - 1].CEVAP[0], Color.Green);
                        dogru++;
                        label3.Text = dogru.ToString();
                    }
                    else
                    {
                        RenkAyarla(sorular[soruno - 1].CEVAP[0], Color.Red);
                        yanlis++;
                        label4.Text = yanlis.ToString();
                    }
                }
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Red;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Harfler_Click(char harf)
        {
            //HARFLER TIKLAMA İŞLEVİ
        }

        private void AnaYuvarlak_Click(object sender, EventArgs e)
        {
            //ANA YUVARLAK TIKLAMA İŞLEVİ
        }
    }
}
