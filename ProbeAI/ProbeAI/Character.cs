using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProbeAI
{
    public class Character
    {
        public static PictureBox ch = new PictureBox();
        public static Random r = new Random();
        public static double[] vision;
        public static double[] decision;
        static NeuralNet brain;
        public static int lifeleft = 200, size = 20;
        public static void Spawn()
        {
            ch.BackColor = Color.Red;
            ch.Size = new Size(size, size);
            brain = new NeuralNet(8, 6, 4, 1);
            int[] num = loc();
            ch.Location = new Point(num[0], num[1]);
            Form.ActiveForm.Controls.Add(ch);
        }
        public static void Move(string dir)
        {
            switch (dir)
            {
                case "Up":
                {
                        if (ch.Location.Y > 0)
                        {
                            ch.Location = new Point(ch.Location.X, ch.Location.Y - size / 4);
                            lifeleft--;
                            Interact();
                        }
                    break;
                }
                case "Down":
                {
                        if (ch.Location.Y + size < General.height)
                        {
                            ch.Location = new Point(ch.Location.X, ch.Location.Y + size / 4);
                            lifeleft--;
                            Interact();
                        }
                    break;
                }
                case "Left":
                {
                        if (ch.Location.X > 0)
                        {
                            ch.Location = new Point(ch.Location.X - size / 4, ch.Location.Y);
                            lifeleft--;
                            Interact();
                        }
                    break;
                }
                case "Right":
                {
                        if (ch.Location.X + size < General.width)
                        {
                            ch.Location = new Point(ch.Location.X + size / 4, ch.Location.Y);
                            lifeleft--;
                            Interact();
                        }
                    break; 
                }
            }
            Collision();
        }
        public static int[] loc()
        {
            int[] num = new int[2];
            num[0] = r.Next(0, General.width - size);
            num[1] = r.Next(0, General.height - size);
            return num;
        }
        public static void Interact()
        {
            if (ch.Bounds.IntersectsWith(Goal.goal.Bounds))
                MessageBox.Show("geg");                
        }
        public static void look()
        {
            vision = new double[8];
            if (ch.Location.X != 0 && (double)Goal.goal.Location.X / (double)ch.Location.X <= 1)
            {
                vision[0] = (double)Goal.goal.Location.X / (double)ch.Location.X; //лево: гг-фрукт
                vision[1] = 0;
            }
            else
            {
                vision[0] = 0;
                vision[1] = (double)ch.Location.X / (double)Goal.goal.Location.X; //право: фрукт - гг
            }
            if(ch.Location.Y != 0 && (double)Goal.goal.Location.Y / (double)ch.Location.Y <= 1)
            {
                vision[2] = (double)Goal.goal.Location.Y / (double)ch.Location.Y; //вверх: гг - фрукт
                vision[3] = 0;
            }
            else
            {
                vision[2] = 0;
                vision[3] = (double)ch.Location.Y / (double)Goal.goal.Location.Y; //вниз: фрукт - гг
            }
            
            vision[4] = (double)General.width/((double)General.width + (double)ch.Location.X);         //лево: стена
            vision[4] = (vision[4] < 0.5) ? 0 : vision[4];
            vision[5] = ((double)ch.Location.X + (double)size) / (double)General.width;           //право: стена
            vision[6] = (double)General.height/((double)General.height + (double)ch.Location.Y);        //вверх: стена
            vision[6] = (vision[6] < 0.5) ? 0 : vision[6];
            vision[7] = ((double)ch.Location.Y + (double)size) / (double)General.height;          //вниз: стена
        }
        public static void think()
        {
            decision = brain.output(vision);
            int maxIndex = 0;
            double max = 0;
            for (int i = 0; i < decision.Length; i++)
            {
                if (decision[i] > max)
                {
                    max = decision[i];
                    maxIndex = i;
                }
            }

            switch (maxIndex)
            {
                case 0:
                    Move("Left");
                    break;
                case 1:
                    Move("Right");
                    break;
                case 2:
                    Move("Up");
                    break;
                case 3:
                    Move("Down");
                    break;
            }
        }
        public static double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
        public static void Collision()
        {
            //Лево право вврех вниз
            if (ch.Location.X < 0)
                ch.Location = new Point(0, ch.Location.Y);

            if (ch.Location.X > General.height)
                ch.Location = new Point(General.height + size, ch.Location.Y);

            if (ch.Location.Y < 0)
                ch.Location = new Point(ch.Location.X, 0);

            if (ch.Location.Y > General.width)
                ch.Location = new Point(ch.Location.X, General.width + size);
        }
    }
}
