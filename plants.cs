using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nature_simatulator2
{
    public class Plants
    {
       public int posx;
       public int posy;
       public int age;
       public int id;
       public int width;
       public int height;
       public int age_of_germination;
       public bool reset_last_frame;
       public bool actually_having_baby;
       public PictureBox image;
       public other_functions other_funcs = new other_functions();

        public void dies(bool respawn,Random rnd) {
            if (respawn) {
                List<int> a = other_funcs.random_x_y_on_initial_screen(rnd);
               // Console.WriteLine(a[0]+" aaaaaaaaaaaaaaaaaaaaaaaaaa " + a[1]);
                this.posx = a[0];
                this.posy = a[1];
                this.age = 1; // if age == 0 then age%age_germination = 0  so avoid
                this.reset_last_frame = true;
               // Console.WriteLine("grass new pos    "+ this.posx + "  " + this.posy);
            }
        }
    }




    public class Grass : Plants
    {
        public Grass(Form this_form, Random rnd, int this_id)
        {
            List<int> a = other_funcs.random_x_y_on_initial_screen(rnd);
            posx = a[0];
            posy = a[1];
            age = rnd.Next(0,500);
            id = this_id;
            width = 3;
            height = 3;
            actually_having_baby = false;
            age_of_germination = 800;
            reset_last_frame = false;
            image = other_funcs.addpicbox(this_form,this.posx, this.posy, this.width, this.height, "light_green", "grass_box" + this_id);
        }

        public void have_baby(List<Grass> grass_list,int this_id, Form this_form, Random rnd)
        {
            
            grass_list.Add(new Grass(this_form,rnd, this_id));
            this.actually_having_baby = false;
        }

        public void act(bool is_day)
        {
            if (is_day) { this.age += 1; } // plant only grow during day
        }


    }
}
