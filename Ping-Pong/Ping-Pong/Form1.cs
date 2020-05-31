using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ping_Pong
{
    public partial class Form1 : Form
    {
        Bitmap canvas;
        Graphics canvasGraphics;
        Timer timer = new Timer();

        GamePanel player = new GamePanel(Brushes.GreenYellow);
        GamePanel npc = new GamePanel(Brushes.GreenYellow);

        Ball ball = new Ball(Brushes.IndianRed);

        int playerPoints, npcPoints;

        public Form1()
        {
            InitializeComponent();
            GameObject.canvas = pictureBox1;
            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            canvasGraphics = Graphics.FromImage(canvas);

            startGame();

            timer.Tick += draw;
            timer.Interval = 1;
            timer.Start();
        }
    
        public void draw(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            canvasGraphics.Clear(Color.White);

            checkCollision();
            npcControl();
            player.draw(canvasGraphics);
            npc.draw(canvasGraphics);
            ball.draw(canvasGraphics);

            pictureBox1.Image = canvas;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.X < pictureBox1.Width/2)
                player.move(e.X, e.Y);
            else
                player.move(pictureBox1.Width / 2, e.Y);
        }

        public void npcControl()
        {
            if (npc.y + npc.height / 2 < ball.y)
                npc.y += 2;
            else npc.y -= 2;
        }

        public void checkCollision()
        {
            if(ball.x > player.x - ball.width && ball.x < player.x + player.width)
                if (ball.y > player.y - ball.height && ball.y < player.y + player.height)
                {
                    ball.speedx *= -1;
                    if (ball.x > player.x)
                        ball.x = player.x + player.width + 1;
                    else
                        ball.x = player.x -ball.width-1;
                }

            if (ball.x > npc.x-ball.width && ball.x < npc.x + npc.width)
                if (ball.y > npc.y - ball.height && ball.y < npc.y + npc.height)
                {
                    ball.speedx *= -1;
                    if (ball.x > npc.x)
                        ball.x = npc.x + npc.width + 1;
                    else
                        ball.x = npc.x - ball.width - 1;
                }

            if (ball.x >= pictureBox1.Width - ball.width)
            {
                startGame();
                playerPoints++;
                label1.Text = playerPoints + ":" + npcPoints;
            }
            else if (ball.x <= 0)
            {
                startGame();
                npcPoints++;
                label1.Text = playerPoints + ":" + npcPoints;
            }
        }

        public void startGame()
        {
            if ((float)new Random().Next(0, 2) == 0)
            {
                ball.speedx = -3;
                ball.speedy = 3;
            }
            else
            {
                ball.speedx = 3;
                ball.speedy = 3;
            }

            npc.x = pictureBox1.Width - npc.width;
            npc.y = pictureBox1.Height / 2 - npc.height / 2;
            ball.x = pictureBox1.Width/2;
            ball.y = pictureBox1.Height / 2;
        }
    }
}
