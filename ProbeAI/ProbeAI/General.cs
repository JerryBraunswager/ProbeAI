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
    public class General
    {
        public static int num = 100;
        public static Label l, d, v, b;
        public static Timer timer;
        public static bool timon = false, timestop = false;
        public static int width = 600, height = 600;
        public static void Start()
        {
            Form.ActiveForm.Controls.Clear();
            Form.ActiveForm.Size = new Size(width, height);
            Character.lifeleft = 200;
            Character.Spawn(); 
            Goal.Spawn();
            l = new Label();
            l.Text = Character.lifeleft.ToString();
            l.AutoSize = true;
            l.Location = new Point(10, 10);
            Form.ActiveForm.Controls.Add(l);
            if (timon == false)
            {
                timer = new Timer();
                timer.Interval = 100;
                timer.Tick += Timer_Tick;
                timer.Start();
                timon = true;
            }
            d = new Label();
            d.AutoSize = true;
            d.Location = new Point(10, 30);
            Form.ActiveForm.Controls.Add(d);

            v = new Label();
            v.AutoSize = true;
            v.Location = new Point(10, 50);
            Form.ActiveForm.Controls.Add(v);

            b = new Label();
            b.AutoSize = true;
            b.Location = new Point(10, 70);
            Form.ActiveForm.Controls.Add(b);
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            l.Text = Character.lifeleft.ToString();
            if (Character.lifeleft != 0 && timestop == false)
            {
                Character.look();
                Character.think();
            }
            d.Text = Character.decision[0].ToString() + " " + Character.decision[1].ToString() + " " + Character.decision[2].ToString() + " " + Character.decision[3].ToString();
            v.Text = Character.vision[0].ToString() + " " + Character.vision[1].ToString() + " " + Character.vision[2].ToString() + " " + Character.vision[3].ToString() + " " + Character.vision[4].ToString() + " " + Character.vision[5].ToString() + " " + Character.vision[6].ToString() + " " + Character.vision[7].ToString();
            b.Text = Character.ch.Location.ToString() + " " + Goal.goal.Location.ToString();
        }
    }
}
