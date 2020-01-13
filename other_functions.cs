using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Forms;
namespace nature_simatulator2
{
    public class other_functions
    {
        
        public List<int> random_x_y_on_initial_screen(Random rnd)
        {
            int x = rnd.Next(50, 500);
            int y = rnd.Next(50, 300);
            List<int> co_ords = new List<int>();
            co_ords.Add(x); co_ords.Add(y);
            //Console.WriteLine("  new co-ords ===  ");
            //foreach (int i in co_ords) { Console.WriteLine(i); }
            Console.WriteLine(co_ords[0] + "  " + co_ords[1] + "  new random co-ords  ");
            return (co_ords); 
        }
        public double Pythagoras(int x1,int y1,int x2,int y2) { int dx = x2 - x1; int dy = y2 - y1; return Math.Round(Math.Sqrt(Math.Pow(dx ,2) + Math.Pow(dy ,2))); }

        // add pic box with images
        public PictureBox addpicbox(Form form_1, int posx, int posy, bool has_image, string image_loc, string name)
        {
            PictureBox newbox = new PictureBox();
            newbox.Location = new System.Drawing.Point(posx, posy);
            newbox.Name = name;
            //newbox.Image = ""; // add heart images
            form_1.Controls.Add(newbox);
            return newbox;
        }

        public void move(int startx, int starty,int endx,int endy, int speed)
        {

            
            double dist_x = endx - startx;
            double dist_y = endy - starty;
            double angle = 999999;
            if (dist_y != 0 && dist_x != 0)
            {
                angle = Math.Abs(Math.Atan(dist_y / dist_x));


                // Console.WriteLine("angle set as   " + angle + "because dist_y = " + dist_y + "   because dist_x =" + dist_x + "  and Math.Atan(dist_y / dist_x)" + Math.Atan(dist_y / dist_x));
            }
            if (dist_y == 0) { Console.WriteLine("=1=  disty = 0    " + dist_y); if (dist_x > 0) { angle = 0; } if (dist_x < 0) { angle = 3.1415; } }
            if (dist_x == 0) { Console.WriteLine("=2=  distx = 0    " + dist_x); if (dist_y > 0) { angle = 3.1415 / 2; } if (dist_y < 0) { angle = 3 * 3.1415 / 2; } }
            if (dist_y == 0 && dist_x == 0) { Console.WriteLine("contact "); }
            // Console.WriteLine(" 666 666 666 angle    " + angle);
            // Console.WriteLine("##### rabbit " + this.id + " at " + this.pos_x + " " + this.pos_y + " going to " + endx + " " + endy + " has displacement x " + move_speed * Math.Cos(angle) + " y " + move_speed * Math.Sin(angle));
            double x_speed = Math.Abs(speed * Math.Cos(angle));
            double y_speed = Math.Abs(speed * Math.Sin(angle));

            if (dist_x < 0)
            { startx += Convert.ToInt32(-x_speed); Console.WriteLine("1"); }
            if (dist_x > 0)
            { startx += Convert.ToInt32(x_speed); Console.WriteLine("2"); }
            if (dist_y < 0)
            { starty += Convert.ToInt32(-y_speed); Console.WriteLine("3"); }
            if (dist_y > 0)
            { starty += Convert.ToInt32(y_speed); Console.WriteLine("4"); }
        }


        public void update_r_list_for_other(List<Rabbit> rabbit_list) {
            rabbit_list_for_other = rabbit_list;
        }

        public List<Rabbit> rabbit_list_for_other;
        public PictureBox addpicbox(Form form_1,int posx, int posy, int width, int height, string col1, string name)
        {// rabbit
            PictureBox newbox = new PictureBox();
            newbox.Location = new System.Drawing.Point(posx, posy);
            newbox.Name = name;
            newbox.Size = new System.Drawing.Size(width, height);
            if (col1 == "white"){newbox.BackColor = Color.White; }
            if (col1 == "light_green") { newbox.BackColor = Color.Green; }
            
            newbox.MouseClick += new System.Windows.Forms.MouseEventHandler(mouseClickedResponse_r);
            
            //    newbox.BackColor = Color.FromArgb(0, 128, 43);
            form_1.Controls.Add(newbox);
            return newbox;
        }


        public void mouseClickedResponse_r(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            foreach (Rabbit r in rabbit_list_for_other)
            { r.selected = false; }
        
            Console.WriteLine("mouse clicked response");
            Console.WriteLine(e.Location.X + " = x       y = " + e.Location.Y);
            
            foreach (Rabbit r in rabbit_list_for_other){
                if (sender == r.image)
                {
                    Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                    r.selected = true;

                }
                //{
                //    Console.WriteLine(sender + " sender ");
                //}
            }
        }


        public void move_to_point_away_from_others(Random rnd, int speed, int id, int posx, int posy, List<Rabbit> rabbit_list, int min_dist) {
            // pick random point away from others
            int newx = posx; int newy = posy;
            foreach (Rabbit r in rabbit_list)
            { if (r.id == id) {
                    newx = Convert.ToInt32(r.pos_x + rnd.Next(0, 200));
                    newy = Convert.ToInt32(r.pos_y + rnd.Next(0, 200));
                    while (Pythagoras(Convert.ToInt32(r.pos_x), Convert.ToInt32(r.pos_y), Convert.ToInt32(r.pos_x + rnd.Next(0,200)), Convert.ToInt32(r.pos_y + rnd.Next(0,200))) < min_dist)
          {
            newx = Convert.ToInt32(r.pos_x + rnd.Next(0, 200));
            newy = Convert.ToInt32(r.pos_y + rnd.Next(0, 200));
            
            } } }
            move(posx, posy, newx, newy, speed);
            
        }

    }
}