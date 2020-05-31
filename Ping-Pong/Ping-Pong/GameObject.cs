using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ping_Pong
{
    class GameObject
    {
        public static PictureBox canvas;
        public float x, y, width, height;
        public Brush color;
        public virtual void draw(Graphics g)
        {

        }
    }

    class GamePanel:GameObject
    {
        public GamePanel(Brush color)
        {
            this.color = color;
            width = 20;
            height = 150;
        }
        public override void draw(Graphics g)
        {
            g.FillRectangle(color, x, y, width, height);
        }

        public void move(float x, float y)
        {
            Console.WriteLine(y);
            if(x > width/2 && x < canvas.Width-width/2)
                this.x = x-width/2;
            else
            {
                if (x <= width / 2)
                    this.x = width / 2;
                else if (x >= canvas.Width - width / 2)
                    this.x = canvas.Width - width / 2;
            }

            if (y > height / 2 && y < canvas.Height - height / 2)
                this.y = y-height/2;
            else
            {
                if (y < height / 2)
                    this.y = 0;
                else if (y >= canvas.Height )
                    this.y= canvas.Height;
            }
        }
    }

    class Ball : GameObject
    {
        public float speedx, speedy;
        public Ball(Brush color)
        {
            this.color = color;
            width = 15;
            height = 15;
        }
        public override void draw(Graphics g)
        {
            x += speedx;
            y += speedy;

            if (y >= canvas.Height - height || y <= 0)
                speedy *= -1;

            g.FillEllipse(color, x, y, width, height);
        }
    }

}
