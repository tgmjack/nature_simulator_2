using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Controls;
///////////////////////////////// 
//  fix wandering babies 
//  females should stay fat after pregnancy just not as fat 
//  vibrating babies
//  key detection (alternatives for buttons)
//  burrows
//  arrow buttons and panning
// drop down box of all attributes they have 
// change github logo
////////////////////////////////

namespace nature_simatulator2
{
    public partial class main_screen : Form
    {
        public int game_timer = 0;
        public int length_of_day = 1000;
        public int left_edge = -200;
        public int right_edge = 200;
        public List<string> text_for_textboxes;
        other_functions funcs = new other_functions();
        public int top_edge = -200;
        public int bottom_edge = 200;
        PictureBox pb_time = new PictureBox();
        public Form form_2;
        System.Windows.Forms.TextBox tb1 = new System.Windows.Forms.TextBox();
        System.Windows.Forms.RichTextBox tb2 = new System.Windows.Forms.RichTextBox();
        System.Windows.Forms.RichTextBox tb3 = new System.Windows.Forms.RichTextBox();
        System.Windows.Forms.Button deselect_but = new System.Windows.Forms.Button();
        Game_script gsx = new Game_script(); // reference to game script
        public main_screen()
        {
            Debug.WriteLine(" wwwwwwwwwwwwwwww   ");
            gsx.still_initializing(this, 50, 50);
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            
            //  Debug.WriteLine(" frame printed from main   ");
            gsx.run_frame(game_timer, this);
            if (gsx.change_grass)
            {change_grass_colour(gsx.is_day);gsx.change_grass=false; }
            game_timer += 1;
            Console.WriteLine(" cccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc ");

            if (game_timer > 1)
            {
                text_for_textboxes = gsx.textbox1_text();

                tb1.Text = text_for_textboxes[0];
                tb2.Text = text_for_textboxes[1];
                tb3.Text = text_for_textboxes[3];
                bool hungrycolour = false;
                if (text_for_textboxes[2] == "hungry")
                {
                    for (int i = 0; i < text_for_textboxes[1].Length ; i++)
                    {
                        if (text_for_textboxes[1][i].ToString() == "h" && text_for_textboxes[1][i+1].ToString() == "u" && text_for_textboxes[1][i+2].ToString() == "n")
                        { hungrycolour = true; tb2.SelectionStart = i + 8;  }
                    }
                    tb2.SelectionLength = text_for_textboxes[1].Length - tb2.SelectionStart;
                    tb2.SelectionColor = Color.Orange;
                }
                if (text_for_textboxes[1] == "") { deselect_but.Text = ""; } else { deselect_but.Text = "deselect"; }
            }

            Console.WriteLine(" cccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc ");
            if (game_timer == 1) {           
                form_2 = form2(); }
        }


        public System.Drawing.Image im_converter(System.Windows.Controls.Image img)
        {
            // from controls to drawing
            MemoryStream ms = new MemoryStream();
            System.Windows.Media.Imaging.BmpBitmapEncoder bbe = new BmpBitmapEncoder();
            bbe.Frames.Add(BitmapFrame.Create(new Uri(img.Source.ToString(), UriKind.RelativeOrAbsolute)));

            bbe.Save(ms);
            System.Drawing.Image img2 = System.Drawing.Image.FromStream(ms);
            return (img2);
        }

        public System.Drawing.Image mekimage(string str)
        {
            System.Windows.Controls.Image myImage3 = new System.Windows.Controls.Image();
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri(str + ".png", UriKind.Relative);
            bi3.EndInit();
            // myImage3.Stretch = Stretch.Fill;
            myImage3.Source = bi3;
            System.Drawing.Image real_im = im_converter(myImage3);
            return real_im;
        }
        public Form form2()
        {
            Form form_2 = new Form();
            form_2.Show();
            //////////////////////////////////////////////////
            tb1.Location = new Point(25, 1);
            tb1.Width = 250;
            tb1.Text = "click on a thing";
            tb1.Font = new Font(tb1.Font.FontFamily, 12);
            form_2.Controls.Add(tb1);
            //////////////////////////////////////////////////
            tb2.Location = new Point(20, 27);
            tb2.Width = 260;
            tb2.Height = 21;
            tb2.Text = "";
            tb2.Font = new Font(tb2.Font.FontFamily, 10);
            form_2.Controls.Add(tb2);
            //////////////////////////////////////////////////
            tb3.Location = new Point(50, 41);
            tb3.Width = 200;
            tb3.Height = 20;
            tb3.Text = "";
            tb3.Font = new Font(tb3.Font.FontFamily, 10);
            form_2.Controls.Add(tb3);
            /////////////////////////////////////////////////////
            deselect_but.Location = new Point(60, 54);
            deselect_but.Width = 90;
            deselect_but.Height = 27;
            deselect_but.MouseClick += new System.Windows.Forms.MouseEventHandler(gsx.deselect_all);
            form_2.Controls.Add(deselect_but);

            //////////////////// time_image //////////////////////
            pb_time.Location = new Point(0, 52);
            form_2.Controls.Add(pb_time);
            ///////////////// middle bar ///////////////////////
            PictureBox pb1 = new PictureBox();
            pb1.Location = new Point(0, 80);
            pb1.Width = form_2.Width;
            pb1.Height = 2;
            pb1.BackColor = Color.FromArgb(0, 0, 0);
            form_2.Controls.Add(pb1);
            ///////////////////////////////////////////////////////

            /* PictureBox pb1 = new PictureBox(); // 4 arrow keys and a few other buttons to click.
               pb1.Location = new Point(0, 80);
               pb1.Width = form_2.Width;
               pb1.BackColor = Color.FromArgb(0, 0, 0);
               form_2.Controls.Add(pb1); 
               */
            // top half of screen is text 
            // Bottom half is ui
            return form_2;
        }


        public void change_grass_colour(bool day)
        {
            if (day == false)
            { this.BackColor = Color.FromArgb(0, 33, 0);
                pb_time.Image = mekimage("moon");
                pb_time.Height = 42;
                pb_time.Width = 66;
                pb_time.Location = new Point (pb_time.Location.X, pb_time.Location.Y-14);

            }
            if (day == true)
            {   this.BackColor = Color.FromArgb(0, 48, 0);
                pb_time.Image = mekimage("sun");
                pb_time.Height = 28;
                pb_time.Width = 40;
                if (game_timer>10){pb_time.Location = new Point(pb_time.Location.X, pb_time.Location.Y + 14);
                     }

                }
        }




    }
}
