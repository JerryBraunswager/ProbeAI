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
    public class Goal
    {
        public static PictureBox goal = new PictureBox();
        public static void Spawn()
        {
            goal.BackColor = Color.Yellow;
            goal.Size = new Size((int)Character.size, (int)Character.size);
            int[] num = Character.loc();
            goal.Location = new Point(num[0], num[1]);
            Form.ActiveForm.Controls.Add(goal);
        }
    }
}
