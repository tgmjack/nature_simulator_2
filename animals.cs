using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;

namespace nature_simatulator2
{
    public class Animal
    {
        # region
        public int length_of_day = 1000;
        #endregion

        public double pos_x;
        public double pos_y;
        public int age;
        public int adulthood_age;
        public int gender; // 1 = male    0 = female
        public bool pregnant;
        public int breeding_ticker;
        public int breeding_frequency;
        public int pregnancy_ticker;
        public int duration_of_pregnancy; 
        public int hunger;
        public int hunger_limit;
        public int docile_counter;
        public int docile_limit;
        public int docile_end_limit;
        public int starve_limit;
        public int id;
        public int burrow_pos_x;
        public int burrow_pos_y;
        public int[] mud_spray_ticker;
        public bool first_frame;
        public int speed;
        public string behaviour;
        public int target_id;
        public int width;
        public int sex_ticker;
        public int height;
        public bool random_point_not_chosen;
        public bool actually_having_baby;
        public int targetx;
        public int targety;
        public int mother_id;
        public int mate_id;
        public bool selected;
        public bool burrow_image_set;
        public int stuck_breeding_counter;
        public PictureBox image;
        public List<PictureBox> mud_pics;
        public other_functions other_funcs = new other_functions();
        public int rabbit_norm_width = 12;
        public int rabbit_norm_height = 6;
        public int rabbit_preg_width = 18;
        public int rabbit_preg_height = 8;
        public void follow_mother(Random rand,List<Rabbit> rabbit_list) {
            Console.WriteLine(this.id + " is following its mother " + this.mother_id);
            // choose rnd point around mother
            int max_dist = 15;
            if (this.targetx == 999999 || this.targety == 999999 || this.docile_counter > this.docile_limit) {
                this.targetx =  rand.Next(Convert.ToInt32(rabbit_list[this.mother_id].pos_x-max_dist), Convert.ToInt32(rabbit_list[this.mother_id].pos_x + max_dist));
                this.targety = rand.Next(Convert.ToInt32(rabbit_list[this.mother_id].pos_y - max_dist), Convert.ToInt32(rabbit_list[this.mother_id].pos_y + max_dist));
                this.docile_counter = 0;
            }
            if (this.targetx != 999999 && this.targety != 999999 && this.speed < Math.Abs(other_funcs.Pythagoras(Convert.ToInt32(this.pos_x), Convert.ToInt32(this.pos_y), Convert.ToInt32(this.targetx), Convert.ToInt32(this.targety))))
            {
                Console.WriteLine(this.id + " is now propperly following its mother " + this.mother_id);

                this.move_to(this.targetx, this.targety, this.speed);
            }
        }

        public void move_to(int endx, int endy, int move_speed)
        {
            //Console.WriteLine("move to called [[[[[ target x == " + endx + " and start x =" + startx + "target y == " + endy + " and start y = " + starty);
            double dist_x = endx -this.pos_x;
            double dist_y = endy - this.pos_y;
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
            double x_speed = Math.Abs(this.speed * Math.Cos(angle));
            double y_speed = Math.Abs(this.speed * Math.Sin(angle));

            if (dist_x < 0)
            { this.pos_x += -x_speed; Console.WriteLine("1"); }
            if (dist_x > 0)
            {this.pos_x +=  x_speed; Console.WriteLine("2"); }
            if (dist_y  <0)
            { this.pos_y += -y_speed; Console.WriteLine("3"); }
            if (dist_y > 0)
            { this.pos_y += y_speed; Console.WriteLine("4"); }
            // 2 and 4 = wrong
        }

    }

    public class Rabbit : Animal
    {
        public int burrow_posx;
        public int burrow_posy;

        //constructor for random pos
        public Rabbit(Form the_form, Random rnd, int this_id)
        {
            
            // this constructor creates adult rabbits at random position.
            pos_x = (rnd.Next(0, 100) * 2) + 80;
            pos_y = (rnd.Next(0, 100) * 2) + 80;
            age = 300;
            gender = rnd.Next(0, 2);
            adulthood_age = 300;
            if (gender == 0) { pregnant = false; }
            breeding_ticker = rnd.Next(0, 500);
            breeding_frequency = 350;
            pregnancy_ticker = 0;
            duration_of_pregnancy = 15;
            docile_end_limit = 100;
            hunger = rnd.Next(0, docile_end_limit);
            speed = 4;
            docile_counter = rnd.Next(0, docile_end_limit);
            docile_limit = docile_end_limit/2;// wanders to random point nearby after this many frames
            // end of docile behaviour loop
            stuck_breeding_counter = 0;
            mud_spray_ticker = new int[] {0,0,0,0,0};
            hunger_limit = length_of_day/6;
            starve_limit = length_of_day*2;
            random_point_not_chosen = true;
            width = rabbit_norm_width;
            height = rabbit_norm_height;
            id = this_id;
            behaviour = "docile";
            target_id = 999999; // 6 9s means not set 
            burrow_posx = 999999;
            burrow_posy = 999999;
            mother_id = 999999;
            mate_id = 999999;
            sex_ticker = 0;
            burrow_image_set = false;
            selected = false;
            actually_having_baby = false;
            mud_pics  = new List<PictureBox>();
            image = other_funcs.addpicbox(the_form, Convert.ToInt32(this.pos_x), Convert.ToInt32(this.pos_y), width, height, "white", "rabbit_box" + this_id );

        }

        // pick a target of opposite gender and approach ... really need to pair_off
        public void breed(List<Rabbit> rabbit_list, Form the_form, Random rnd)
        {
             
            Console.WriteLine("breed called" + this.id +" has a target " + this.target_id);
            
            if (this.behaviour == "ready_to_breed" && this.mate_id == 999999)
            {  
                foreach (Rabbit r in rabbit_list)
                {
                    if (this.gender != r.gender && r.behaviour == "ready_to_breed" && r.mate_id == 999999)
                    {
                        Console.WriteLine("BBBBBBBBBBBBBBBBBBBBBBBBB");
                        Console.WriteLine("breed called     " + this.id + " id  " + this.behaviour + "behaviour                 traget"+ r.id);
                        this.mate_id = r.id;
                        r.mate_id = this.id;
                        this.behaviour = "breeding";
                        r.behaviour = "breeding";
                        Console.WriteLine("now breeding");
                    }
                }
            }

            if(this.behaviour == "breeding")
            {
                Console.WriteLine(" i love jess");
                Console.WriteLine("target id " + this.target_id + "              id" +this.id);
                Console.WriteLine(" i love jess");
                this.move_to(Convert.ToInt32(rabbit_list[this.mate_id].pos_x), Convert.ToInt32(rabbit_list[this.mate_id].pos_y), this.speed);
                if (Math.Abs(other_funcs.Pythagoras(Convert.ToInt32(this.pos_x), Convert.ToInt32(this.pos_y), Convert.ToInt32(rabbit_list[this.mate_id].pos_x), Convert.ToInt32(rabbit_list[this.mate_id].pos_y))) < this.speed)
                {
                    if (this.gender == 0)
                    {
                        // big red love heart for a few frames
                        this.have_baby(rabbit_list, the_form ,rnd,rabbit_list.Count+1,this.id,this.pos_x,this.pos_y);
                    }
                    
                    this.sex_ticker +=1;
                    
                    Console.WriteLine(this.id + " this has just got a target of 999999 [][][]");
                }

                if (this.sex_ticker > 10) {
                    if (this.gender == 1)
                    {
                        this.behaviour = "docile";
                    }
                    else { this.behaviour = "pregnant"; }
                    this.breeding_ticker = 0;
                    this.target_id = 999999;
                    Console.WriteLine(this.id +" id     behavior "+ this.behaviour);
                }
            }
        }

        public void hang_out_near(double pos_x, double pos_y) { }
        public void have_baby(List<Rabbit> rabbit_list, Form the_form, Random rnd, int this_id, int mothers_id, double mothers_posx, double mothers_posy)
        { //  have baby describes what to do while pregnant
            width = rabbit_preg_width;
            height = rabbit_preg_height;
            this.image.Width = rabbit_preg_width;
            this.image.Height = rabbit_preg_height;
            this.pregnant = true;
            this.speed = this.speed / 2;
            this.behaviour = "pregnant";
            this.pregnancy_ticker += 1;
            Console.WriteLine(this.id+ " preggy      pregnancy ticker " + this.pregnancy_ticker + "  "+ this.mate_id);
            this.hang_out_near(this.burrow_pos_x, this.burrow_pos_y);
            if (this.pregnancy_ticker > this.duration_of_pregnancy)
            {
                Console.WriteLine(this.id + " had baby      pregnancy ticker " + this.pregnancy_ticker + "  " + this.mate_id);
                width = rabbit_norm_width;
                height = rabbit_norm_height;
                this.image.Width = rabbit_norm_width;  // or w/es normal
                this.image.Height = rabbit_norm_height;
                this.pregnancy_ticker = 0;
                this.pregnant = false;
                this.speed = this.speed * 2;
                this.actually_having_baby = true;

            }
        }
        public void actuallyaddrabbit(Form the_form, Random rnd, List<Rabbit> rabbit_list, int this_id, int mothers_id, int mothers_posx, int mothers_posy) {
            Console.WriteLine("addbaby      mother = " + mothers_id + "    new baby =  " + this_id );
            rabbit_list.Add(new Rabbit(the_form, rnd, this_id, mothers_id, mothers_posx, mothers_posy));
            rabbit_list[mothers_id].actually_having_baby = false;
            rabbit_list[mothers_id].behaviour = "docile";

            other_funcs.update_r_list_for_other(rabbit_list);

        }

        public Rabbit(Form the_form, Random rnd, int this_id, int mothers_id, double mothers_posx, double mothers_posy)
        {
            // this constructor creates baby rabbits
            pos_x = mothers_posx + 50;
            pos_y = mothers_posy + 50;
            age = 0;
            gender = rnd.Next(0, 1);
            adulthood_age = 30000;
            if (gender == 0) { pregnant = false; }
            hunger = 0;
            speed = 4;
            docile_end_limit = 100;
            docile_counter = rnd.Next(0, docile_end_limit);
            docile_limit = docile_end_limit / 2;// wanders to random point nearby after this many frames
            pregnancy_ticker = 0;
            burrow_posx = 999999;
            burrow_posy = 999999;
            // end of docile behaviour loop
            hunger_limit = length_of_day / 6;
            starve_limit = length_of_day * 2;
            random_point_not_chosen = true;
            width = 7;
            height = 3;
            id = this_id;
            behaviour = "baby";
            target_id = 999999; // 6 9s means not set 
            burrow_posx = 999999;
            burrow_posy = 999999;
            mother_id = mothers_id;
            image = other_funcs.addpicbox(the_form, Convert.ToInt32(this.pos_x), Convert.ToInt32(this.pos_y), width, height, "white", "rabbit_box" + this_id);
        }

        public void docile_act(Random rnd)
        {
            Console.WriteLine("docile act called        id and counter = " + this.id + "  " + this.docile_counter);
            if (this.docile_counter > this.docile_limit)
            {
                if (this.random_point_not_chosen == true)
                {
                    Console.WriteLine("random_docile point chosen and tid = -1");
                    this.targetx = Convert.ToInt32(this.pos_x + rnd.Next(-10, 10) * 5);
                    this.targety = Convert.ToInt32(this.pos_y + rnd.Next(-10, 10) * 5);
                    this.target_id = -1;
                    this.random_point_not_chosen = false;
                }
                if (this.target_id == -1)
                {
                    this.move_to(this.targetx, this.targety, this.speed);
                    Console.WriteLine(other_funcs.Pythagoras(Convert.ToInt32(this.pos_x), Convert.ToInt32(this.pos_y), this.targetx, this.targety));
                    if (other_funcs.Pythagoras(Convert.ToInt32(this.pos_x), Convert.ToInt32(this.pos_y), this.targetx, this.targety) < this.speed)
                    { this.docile_counter = 0; this.target_id = 999999; Console.WriteLine(this.id + " [][][]  this target = 999999 "); }   
                }
            }
            this.docile_counter += 1;
            if (this.docile_counter > this.docile_end_limit) { this.docile_counter = 0; this.target_id = 999999; this.random_point_not_chosen = true; }
        }

        public void pick_behaviour(List<Rabbit> rabbit_list, Form the_form, Random rnd)
        {
            //  Console.WriteLine("pick_behaviour ============= id " + this.id + "    behaviour " + this.behaviour + "      age " + this.age +"       adulthood age"+ this.adulthood_age );
            // Console.WriteLine("and gender = " + this.gender +  "       breed freq " + this.breeding_frequency + "     breed ticker " + this.breeding_ticker);
            if (this.behaviour == "hungry" && this.hunger_limit > this.hunger) { this.behaviour = "docile"; }
            Console.WriteLine(this.target_id + " pb1   " + this.behaviour) ;
            if (this.adulthood_age < this.age) { this.breeding_ticker += 1;
            if (this.breeding_ticker > this.breeding_frequency && this.behaviour != "breeding" && this.behaviour != "pregnant") { Console.WriteLine("breeding ticker > breeding freq" + this.behaviour);  this.behaviour = "ready_to_breed";}  }
            Console.WriteLine(this.target_id + " pb2   " + this.behaviour);
            if (this.hunger_limit < this.hunger && this.behaviour != "breeding" && this.behaviour != "pregnant") // finish this with || or it is breeding and 90% starved
            { this.behaviour = "hungry"; }
            Console.WriteLine(this.target_id + " pb3   " + this.behaviour);
            if (this.age< this.adulthood_age) { this.behaviour = "baby"; }
            Console.WriteLine(this.target_id + " pb4   " + this.behaviour);
            if (this.behaviour == "docile" && this.burrow_pos_x == 999999)
            { this.behaviour = "build burrow";}
        }

        public void eat(List<Grass> grass_list,Random rnd)
        {
            {
                foreach (Grass gr in grass_list)
                {
                    if (this.target_id == gr.id)
                    {
                       // Console.WriteLine("going for food");
                        this.move_to(gr.posx, gr.posy, this.speed);
                        // Console.WriteLine("displacements=" + displacements[0] + " and " + displacements[1] + "       target " + this.target_id);
                        if (other_funcs.Pythagoras(Convert.ToInt32(this.pos_x), Convert.ToInt32(this.pos_y),gr.posx,gr.posy) < this.speed) { this.hunger = rnd.Next(0,this.hunger_limit); gr.dies(true, rnd); this.target_id = 999999; this.behaviour = "docile"; this.docile_counter = 0; }
                    }

                }
            }
        }


        public void describe_this_rabbit( int the_id, List<Rabbit> rabbit_list)
        {
            foreach (Rabbit r in rabbit_list)
            { if (r.id==the_id)
                {
                    Console.WriteLine("describe called    id " + r.id + "    behaviour " + this.behaviour);
                    Console.WriteLine("    target id " + r.target_id + "    docile counter " + this.docile_counter);
                    Console.WriteLine("gender " + r.gender + "         pregnancy ticker  " + this.pregnancy_ticker);


                }
                foreach (Rabbit r2 in rabbit_list) { if (r.id == r2.mate_id && r.mate_id == r2.id)
                    { Console.WriteLine("mutually exclusive couple " +r.id + "  " + r2.id); }
                }
            }
        }


        public void act(List<Grass> grass_list, List<Rabbit> rabbit_list ,Form the_form,  Random rnd, bool is_day)
        {
            if (first_frame) { if (this.id == 0) { this.first_list_to_other_funcs(rabbit_list); first_frame = false; Console.WriteLine("just passed the bloody thing"); } }
            other_funcs.rabbit_list_for_other = rabbit_list;
            Console.WriteLine("act called    id "+ this.id + "    behaviour " + this.behaviour);
            this.pick_behaviour(rabbit_list, the_form, rnd);
            Console.WriteLine("after pick_behaviour    id " + this.id + "    behaviour " + this.behaviour);


            if (this.behaviour == "hungry" && this.target_id == 999999)
            { this.pick_target(grass_list); }

            if (this.behaviour == "hungry" && this.target_id != 999999)
            { eat(grass_list,rnd); }

            if (this.behaviour == "ready_to_breed") { this.breed(rabbit_list, the_form, rnd); this.breed_helper(rabbit_list, rnd); }

            if (this.behaviour == "docile") { Console.WriteLine("aarrssee"); this.docile_act(rnd); }
           // if (this.hunger > this.hunger_limit) { this.behaviour = "hungry"; } // and burrow isnt being built + not having sex either
            Console.WriteLine("before crit    id " + this.id + "    behaviour " + this.behaviour);
            if (this.behaviour == "build burrow") { this.build_burrow(the_form, rnd); }
            if (this.behaviour == "breeding") { this.breed(rabbit_list,the_form,rnd); Console.WriteLine("it is called though ppppppppppppppppppppppppppppppppp"); }
            if (this.behaviour == "pregnant") { this.have_baby(rabbit_list,the_form, rnd, this.id+1, this.id ,this.pos_x,this.pos_y); }
            if (this.behaviour == "baby") { this.follow_mother(rnd, rabbit_list ); }
            if (is_day == false && this.hunger < 4/5 *this.hunger_limit ) { this.go_to_burrow(); }
            this.hunger += 1;
            this.age += 1;

        }
        public void mud_spray(Form the_form)
        {
            int single_mud_time = 10;
            for (int i = 0; i < this.mud_spray_ticker.Length; i++) {
                if (this.mud_spray_ticker[i] > 3) {
                    this.mud_spray_ticker[i + 1] += 1; }

                if (this.mud_spray_ticker[i] < single_mud_time) {
                    if (this.mud_pics[i] == null) { this.mud_pics.Add(other_funcs.addpicbox(the_form, this.burrow_pos_x , this.burrow_pos_y, 20, 20, "brown", "mud_box_"+i)); }
                    else {other_funcs.move(this.mud_pics[i].Location.X, this.mud_pics[i].Location.Y, this.burrow_pos_x , this.burrow_pos_y +50  , 6); }
                }
            }
            if (this.mud_spray_ticker[mud_spray_ticker.Length] > single_mud_time)
            {
                // draw burrow
                this.burrow_image_set = true;

            }
            this.mud_spray_ticker[0] += 1;
            
        }

        public void build_burrow(Form the_form, Random rnd)
        {
            int min_dist_between_burrows = 15;
            if (this.burrow_posx == 999999 || this.burrow_posy == 999999)
            {
                int b_pos_x = rnd.Next();
                int b_pos_y = rnd.Next();
                while (other_funcs.Pythagoras(b_pos_x,b_pos_y,Convert.ToInt32(this.pos_x),Convert.ToInt32(this.pos_y)) < min_dist_between_burrows)
                {
                    b_pos_x = rnd.Next();
                    b_pos_y = rnd.Next();
                }
                this.burrow_posx = b_pos_x;
                this.burrow_posx = b_pos_y;
            }
            if (this.burrow_image_set == false)
                {
                if (other_funcs.Pythagoras(Convert.ToInt32(this.pos_x), Convert.ToInt32(this.pos_y), this.burrow_posx, this.burrow_posy) > this.speed) { this.move_to(this.burrow_pos_x, this.burrow_pos_y, this.speed); }
                else {
                    this.mud_spray(the_form);
                    for (int i = 0; i < this.mud_spray_ticker.Length; i++) {
                        this.mud_spray_ticker[i] += 1; }
                     }
            }
        }
        public void go_to_burrow()
        {
            if (this.burrow_posx == 999999 || this.burrow_posy == 999999)
            { }
            //else { this.move_to(burrow); }
        }

        public void pick_target(List<Grass> grass_list)
        {// this function should just find nearest
            
            
            List<int> dist_list = new List<int>();
            List<int> id_list = new List<int>();
            foreach (Grass gr in grass_list)
            {
                dist_list.Add(Convert.ToInt32(other_funcs.Pythagoras(Convert.ToInt32(this.pos_x), Convert.ToInt32(this.pos_y), gr.posx, gr.posy)));
                id_list.Add(gr.id);
            }
            int min_dist = dist_list[0];
            int min_id = id_list[0];
            int counter = 0;
            foreach (int i in dist_list)
            {
               // Console.WriteLine(i + "    in dist list");

                if (i < min_dist) { min_dist = i; min_id = id_list[counter]; }
                counter += 1;
            }
           // Console.WriteLine(this.id + "  at "+ this.pos_x+" / "+ this.pos_y +" is going for    " +  min_id + " at " + dist_list[min_id] + " / " + dist_list[min_id]);
            this.target_id = min_id;
        }

        public void first_list_to_other_funcs(List<Rabbit> rabbit_list)
        {
            other_funcs.update_r_list_for_other(rabbit_list);
        }


        public void breed_helper( List<Rabbit> rabbit_list , Random rnd ) {
            // breeding is a high priority but for many reasons can be difficult.
            // if attempts to find breeding partner has failed over and over reset 
            // breeding counter so that other behaviours can take part 
            if (this.behaviour == "ready_to_breed") { this.stuck_breeding_counter += 1; }
            if (this.stuck_breeding_counter > 50) { Console.WriteLine(this.id + "   Failed to breed  gender is  " + this.gender); this.breeding_ticker = 0; this.behaviour = "docile"; this.stuck_breeding_counter = 0; other_funcs.move_to_point_away_from_others(rnd, this.speed, this.id, Convert.ToInt32(this.pos_x), Convert.ToInt32(this.pos_y), rabbit_list,40); ; }
            
        }
    }
}
