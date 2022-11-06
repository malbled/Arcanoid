using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arcanoid
{
    public partial class GameForm : Form
    {
        private int panelX, panelY, sharX, sharY;
        private int gopanel = 10;
        private int gosharX = 7;
        private int gosharY = 4;
        private int sharXX, sharYY;
        private int LabelAll;
        
        public GameForm()
        {
            InitializeComponent();
        }

        private void PreformGame()
        {
            panelX = panel.Location.X;
            panelY = panel.Location.Y;
            sharX = shar.Location.X;
            sharY = shar.Location.Y;
            sharXX = gosharX;
            sharYY = - gosharY;
            LabelAll = 25;
        }

        private void GameForm_Shown(object sender, EventArgs e)
        {
            PreformGame();
        }
        private void Gopanel(int xx)
        {
            var zx1 = panelX + xx;
            var zx2 = zx1 + panel.Width;
            if(zx1 < 0)
            {
                zx1 = 0;
            }
            if(zx2 > this.ClientSize.Width)
            {
                zx1 = this.ClientSize.Width  - panel.Width;
            }
            panelX = zx1;
            panel.Location = new Point(panelX, panelY);
        }
        


        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyCode;
            switch (key)
            {
                case Keys.Left:
                    Gopanel(-gopanel);
                    break;
                case Keys.Right:
                    Gopanel(gopanel);
                    break;
                case Keys.Enter:
                    timer.Enabled = true;
                    break;
            }
        }
        private void MoveShar()
        {
            var bx1 = sharX;
            var bx2 = sharX + shar.Width;
            var by1 = sharY;
            var by2 = sharY + shar.Height;

            if(bx1 + sharXX < 0)
            {
                sharXX = gosharX;
            }
            if(bx2 + sharXX > this.ClientSize.Width)
            {
                sharXX = -gosharX;
            }
            if(by1 + sharYY < 0)
            {
                sharYY = gosharY;
            }
            if( by2 + sharYY > panelY)
            {
                var bx0 = (bx1 + bx2) / 2;
                var by0 = (by1 + by2) / 2;
                if (panelX <= bx0 && bx0 <= (panelX + panel.Width))
                {
                    sharYY = -gosharY;
                }
                else
                if (panelX <= bx2 + shar.Width && bx2 + shar.Width <= (panelX + panel.Width))
                {
                    sharYY = -gosharY;
                    sharXX = -gosharX;
                }
                else
                if (panelX <= bx1 - shar.Width && bx1 - shar.Width <= (panelX + panel.Width))
                {
                    sharYY = -gosharY;
                    sharXX = gosharX;
                }
                else
                    MissShar();
            }
            sharX += sharXX;
            sharY += sharYY;
            shar.Location = new Point(sharX, sharY);
            #region Destroy
            Destroy(label1); Destroy(label2); Destroy(label3); Destroy(label4); Destroy(label5); Destroy(label6); Destroy(label7); Destroy(label8); Destroy(label9);
            Destroy(label10); Destroy(label11); Destroy(label12); Destroy(label13); Destroy(label14); Destroy(label15); Destroy(label16); Destroy(label17);
            Destroy(label18); Destroy(label19); Destroy(label20); Destroy(label21);
            Destroy(label22); Destroy(label23); Destroy(label24); Destroy(label25);
            #endregion
        }

        private void Destroy(Label label)
        {
            if (!label.Visible)
                return;
            var bx1 = sharX;
            var bx2 = sharX + shar.Width;
            var by1 = sharY;
            var by2 = sharY + shar.Height;
            var bx0 = (bx1 + bx2) / 2;
            var by0 = (by1 + by2) / 2;
            var rx1 = label.Location.X;
            var ry1 = label.Location.Y;
            var rx2 = rx1 + label.Width;
            var ry2 = ry1 + label.Height;

            if(rx1 <= bx0 && bx0 <= rx2 && ry1 <= by2 && by2 <= ry2)
            {
                Fall(label);
                sharYY = -sharYY;
                return;
            }
            if (rx1 <= bx0 && bx0 <= rx2 && ry1 <= by1 && by1 <= ry2)
            {
                Fall(label);
                sharYY = -sharYY;
                return;
            }
            if (rx1 <= bx2 && bx2 <= rx2 && ry1 <= by0 && by0 <= ry2)
            {
                Fall(label);
                sharXX = -sharXX;
                return;
            }
            if (rx1 <= bx1 && bx1 <= rx2 && ry1 <= by0 && by0 <= ry2)
            {
                Fall(label);
                sharXX = -sharXX;
                return;
            }
            if ((rx1 <= bx2 && bx2 <= rx2 && ry1 <= by2 && by2 <= ry2) ||
                (rx1 <= bx1 && bx1 <= rx2 && ry1 <= by2 && by2 <= ry2) ||
                (rx1 <= bx1 && bx1 <= rx2 && ry1 <= by1 && by1 <= ry2) ||
                (rx1 <= bx2 && bx2 <= rx2 && ry1 <= by1 && by1 <= ry2))
            {
                Fall(label);
                sharXX = -sharXX;
                sharYY = -sharYY;
                return;
            }

        }
        private void Fall(Label label)
        {
            label.Visible = false;
            LabelAll--;
            if(LabelAll == 0)
            {
                timer.Enabled = false;
                Win m = new Win();
                m.ShowDialog(this);
                this.Close();
            }
        }

        private void MissShar()
        {
            timer.Enabled = false;
            msg m = new msg();
            m.ShowDialog(this);
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            MoveShar();
        }
    }
}
