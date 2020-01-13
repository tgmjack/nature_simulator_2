using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
namespace nature_simatulator2
{
    
    class Game_script
    {
        public int number_of_rabbits = 13;
        public int number_of_grass = 16;
        public List<Rabbit> rabbit_list = new List<Rabbit>();
        public List<Grass> grass_list = new List<Grass>();
        public bool is_day = false;
        public bool change_grass = false;
        public int length_of_day = 1000; // 24 hours      day and night
        public Random rnd = new Random();
        
        public void still_initializing(Form the_form, int posx,int posy) {
            for (int i = 0; i < number_of_grass; i++)
            { grass_list.Add(new Grass(the_form,rnd, i));}
            for (int i = 0; i < number_of_rabbits; i++) {
                rabbit_list.Add(new Rabbit(the_form, rnd, i)); }
            
        }
        




        public void run_frame(int game_timer, Form the_form)
        {
            
            // night n day

            if (game_timer% (length_of_day/2) == 0) { if (is_day == true) { is_day = false; change_grass = true; } else { is_day = true; change_grass = true; } }
            Console.WriteLine("rabbit n targets below");
            //foreach (Rabbit r in rabbit_list) { Console.WriteLine(r.id + " " + r.mate_id); }
            int index_counter = 0;
        //    foreach (Rabbit r in rabbit_list)
          //  { r.describe_this_rabbit(r.id , rabbit_list); }
                List<int> actually_having_baby_ints = new List<int>();
            foreach (Rabbit r in rabbit_list)
            {
                actually_having_baby_ints.Add(0);
                //r.describe_this_rabbit(r.id, rabbit_list);
                r.act(grass_list,rabbit_list, the_form, rnd, is_day);
                r.image.Location = new System.Drawing.Point(Convert.ToInt32(r.pos_x), Convert.ToInt32(r.pos_y));
                if (r.actually_having_baby) { actually_having_baby_ints[index_counter] = 1; r.actually_having_baby = false; r.behaviour = "docile"; }
                index_counter += 1;
            }
            Console.WriteLine(game_timer + " frame <<<<<<<<<<<<>>>>>>>> ");
            Console.WriteLine(number_of_rabbits);
            Console.WriteLine(actually_having_baby_ints);
            foreach (int i in actually_having_baby_ints) { Console.WriteLine(i); }
            // cant create new rabbits while iterating through list of existing rabbits
            int number_of_new_rabbits = 0;
            for (int i = 0; i < rabbit_list.Count - 1; i++) {
                Console.WriteLine(" on frame  " + game_timer + "  " + rabbit_list[i].pregnancy_ticker.ToString() + "preg tick of rabbit " + rabbit_list[i].id + "   " + rabbit_list[i].pos_x + "x                y " + rabbit_list[i].pos_y + "       " + rabbit_list[i].targetx + "target x         target y" + rabbit_list[i].targety + "  behaviour  " + rabbit_list[i].behaviour + " hunger " + rabbit_list[i].hunger + "  preggerz?  " + rabbit_list[i].pregnant );
                if (actually_having_baby_ints[i] == 1)
                {
                    Console.WriteLine("  ");
                    int r_num = rabbit_list.Count;
                    rabbit_list[i].actuallyaddrabbit(the_form, rnd, rabbit_list, r_num, i, Convert.ToInt32(rabbit_list[i].pos_x), Convert.ToInt32(rabbit_list[i].pos_y)); 
                    number_of_new_rabbits += 1;
                    actually_having_baby_ints.Add(0);


                }
            }




            number_of_rabbits += number_of_new_rabbits;





            int number_of_new_grass = 0;
            List<int> grass_actually_having_baby_ints = new List<int>();
            index_counter = 0;
            foreach (Grass g in grass_list)
            {
                grass_actually_having_baby_ints.Add(0);
                g.act(is_day);
                if (g.age % g.age_of_germination == 0) {   g.actually_having_baby = true; grass_actually_having_baby_ints[index_counter] = 1; g.age += 1; } // age has to += 1 here otherwise if it has a baby at same time as night starts it breeds all night
                g.image.Location = new System.Drawing.Point(g.posx, g.posy);
                if (g.reset_last_frame) { foreach (Rabbit r in rabbit_list) { if (r.target_id == g.id) { r.pick_target(grass_list); } } g.reset_last_frame = false; } // otherwise rabbits continue to chase grass when it teleports far away
                index_counter += 1;
            }

            Console.WriteLine("ricky gervais");
            for (int i = 0; i < grass_actually_having_baby_ints.Count - 1; i++)
            {
                Console.WriteLine(grass_actually_having_baby_ints[i]);
            }

                for (int i = 0; i < number_of_grass - 1; i++)
            {
                Console.WriteLine(number_of_grass);
                Console.WriteLine(" i " + i + "   ");
                Console.WriteLine(grass_actually_having_baby_ints);
                Console.WriteLine(number_of_new_grass);
                if (grass_actually_having_baby_ints[i] == 1) //
                {                    
                    int g_num = grass_list.Count + 1;
                    grass_list[i].have_baby(grass_list,g_num,the_form,rnd);
                    number_of_new_grass += 1;
                }
            }
            number_of_grass += number_of_new_grass;
            // if (keydown == up) { foreach(e in everything){ e.posy +=1;}}
        }






        public List<string> textbox1_text()
        {
            List<string> strings  = new List<string>();
            string string1 = "click something";
            string string2 = "";
            string other_context = " ";
            string string4 = "";
            foreach (Rabbit r in rabbit_list) {
                if (r.selected) {
                    string1 = "rabbit " + r.id + "  x = " + Math.Ceiling(r.pos_x) + "  y =" + Math.Ceiling(r.pos_y);
                    string2 = "behaviour = " + r.behaviour + "    hunger = " + r.hunger + "/" + r.starve_limit;
                    if (r.hunger > r.hunger_limit) { other_context = "hungry"; }
                    string4 = "age: " + r.age + "  gender:" + r.gender;
                 }
                
            }
            strings.Add(string1);
            strings.Add(string2);
            strings.Add(other_context);
            strings.Add(string4);
            return (strings);
        }

        public void deselect_all(object sender, System.Windows.Forms.MouseEventArgs e) {
            foreach(Rabbit r in rabbit_list)
            {
                if (r.selected) { r.selected = false; }
            }

         /*   foreach (Grass g in grass_list)
            {
                if (g.selected) { g.selected = false; }
            }   */

        }


    }
}
