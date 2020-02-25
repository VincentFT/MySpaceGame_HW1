using System;
using System.Windows.Forms;
using System.Drawing;

namespace MySpaceGame
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static BaseObject[] _objs;

        const int commonSpeed = 6;
        const int numOfAsteroids = 15;
        const int numOfStars = 50;
        const int maxSize = 30;
        const int minSize = 10;
                               
        public static int Width { get; set; }
        
        public static int Height { get; set; }

        static Game()
        {
        }
                
        public static void Load()
        {
            Random rand = new Random();
            _objs = new BaseObject[ numOfAsteroids + numOfStars];

            for (int i = 0; i < _objs.Length - numOfAsteroids; i++)
            {
                _objs[i] = new Stars(new Point(Convert.ToInt32(rand.NextDouble() * (double)Game.Width),
                    Convert.ToInt32(rand.NextDouble() * (double)Game.Height)), new Point(rand.Next(-commonSpeed, -1), 0), new Size(1, 1));
            }
           
            for (int i = _objs.Length - numOfAsteroids; i < _objs.Length; i++)
            {
                int size = rand.Next(minSize, maxSize);
                _objs[i] = new BaseObject(new Point(Convert.ToInt32(rand.NextDouble() * (double)(Game.Width - size)),
                    Convert.ToInt32(rand.NextDouble() * (double)(Game.Height - size))), new Point(rand.Next(-commonSpeed, commonSpeed),
                    rand.Next(-commonSpeed, commonSpeed)), new Size(size, size));
            }
        }
                
        static public Timer timer = new Timer { Interval = 100 };
                
        public static void Init(Form form)
        {
                       
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            timer.Start();
            timer.Tick += Timer_Tick;
        }
                
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
                
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            Buffer.Render();
        }
                
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
        }

    }
}

