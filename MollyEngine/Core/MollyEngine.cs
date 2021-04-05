using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System;
using MollyEngine.Core;
using System.Threading.Tasks;

namespace MollyEngine.Core
{

    class Canvas : Form 
    {
        public Canvas()
        {
            this.DoubleBuffered = true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Canvas
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Canvas";
            this.ResumeLayout(false);
        }
    }

    public abstract class MollyEngine
    {
        List<int> renderingPriority = new List<int>();

        // Register/Unregister queues
        private static List<GameObject> registerQueue = new List<GameObject>();
        private static List<GameObject> unregisterQueue = new List<GameObject>();
        private static List<Vector2> cameraPositions = new List<Vector2>();

        private Scale ScreenSize = new Scale(512, 512);
        private string Title;
        private static Canvas Window = null;
        private Thread GameLoopThread;
        private static bool isRendering;
        
        public Color BackgroundColor = Color.Aqua;

        private static List<GameObject> AllGameObjects = new List<GameObject>();

        public MollyEngine(Scale ScreenSize, string Title, FormWindowState state)
        {
            createWindow(ScreenSize, Title, state); 
        }


        public MollyEngine(Scale ScreenSize, string Title)
        {
            createWindow(ScreenSize, Title, FormWindowState.Normal);
        }

        public MollyEngine(string Title)
        {
            Scale size = new Scale(512, 512);
            createWindow(size, Title, FormWindowState.Normal);
        }

        public MollyEngine(Scale ScreenSize)
        {
            createWindow(ScreenSize, "", FormWindowState.Normal);
        }

        private void createWindow(Scale ScreenSize, string Title, FormWindowState state)
        {
            this.ScreenSize = ScreenSize;
            this.Title = Title;

            Window = new Canvas();
            Window.Size = new Size((int)ScreenSize.Width, (int)ScreenSize.Height);
            Window.Text = Title;
            Window.Paint += Renderer;
            Window.KeyDown += KeyDown;
            Window.KeyUp += KeyUp;
            Window.MouseDown += MouseDown;
            Window.MouseUp += MouseUp;
            Window.MouseMove += MouseMove;
            Window.Closing += new System.ComponentModel.CancelEventHandler(CloseGame);
            Window.MouseDoubleClick += MouseDoubleClick;

            Window.WindowState = state;

            // Lets Start our Game Loop

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(Window);
        }
        //
        // Bindings
        //
        private void KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        private void KeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
        }

        private void CloseGame(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OnClosed();
            GameLoopThread.Abort();
            e.Cancel = false;
        }

        private void MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        private void MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMoved(e);
        }

        private void MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnMouseDoubleClicked(e);
        }

        //
        // Game Loop
        //

        private void GameLoop()
        {
            // If you have set an OnLoad method then you can run code before your game starts
            OnLoad();
            HandleQueue();
            while (GameLoopThread.IsAlive)
            {
                try
                {
                    // Render
                    OnDraw();
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    OnUpdate();

                    // Register/Unregister objects
                    HandleQueue();

                    Thread.Sleep(1);
                }
                catch (Exception e)
                {
                    OnException(e);
                }
            }
            OnClosed();
        }

        private void HandleQueue()
        {
            while (isRendering)
            {
                // Wait
            }
            EmptyUnregisterQueue();
            EmptyRegisterQueue();
        }

        private void EmptyRegisterQueue()
        {
            foreach (GameObject gameObject in registerQueue)
            {
                AllGameObjects.Add(gameObject);
            }
            registerQueue.Clear();
        }

        private void EmptyUnregisterQueue()
        {
            foreach (GameObject gameObject in unregisterQueue)
            {
                AllGameObjects.Remove(gameObject);
            }
            unregisterQueue.Clear();
        }

        public static void RegisterGameObject(GameObject gameObject)
        {
            registerQueue.Add(gameObject as GameObject);
        }

        public static void UnregisterGameObject(GameObject gameObject)
        {
            unregisterQueue.Add(gameObject);
        }

        public void setTitle(string title)
        {
            this.Title = title;
            Window.Text = this.Title;
        }

        public string getTitle()
        {
            return this.Title;
        }

        public static List<GameObject> getAllGameObjects()
        {
            return AllGameObjects;
        }

        private void Renderer(object sender, PaintEventArgs e)
        {
            // Start rendering
            isRendering = true;

            Graphics graphics = e.Graphics;
            graphics.Clear(BackgroundColor);

            // RenderCamera
            foreach (Vector2 tranform in cameraPositions)
            {
                graphics.TranslateTransform(tranform.X, tranform.Y);
            }
            cameraPositions.Clear();

            List<GameObject> elementsToRender = new List<GameObject>(AllGameObjects);

            foreach (GameObject gameObject in elementsToRender)
            {
                if (gameObject.type == null)
                {
                    if (gameObject.GetType() == typeof(Sprite2D))
                    {
                        gameObject.type = typeof(Sprite2D);
                    }
                    else
                    {
                        gameObject.type = typeof(Shape2D);
                    }
                }
            }

            uint currentPriority = 0;
            List<GameObject> renderedElements = new List<GameObject>();

            while (elementsToRender.Count != 0)
            {
                foreach (GameObject gameObject in elementsToRender)
                {
                    if (gameObject.renderingPriority == currentPriority)
                    {
                        if (gameObject.type == typeof(Sprite2D))
                        {
                            Sprite2D sprite = gameObject as Sprite2D;
                            graphics.DrawImage(sprite.Sprite, sprite.Position.X, sprite.Position.Y, sprite.Scale.Width, sprite.Scale.Height);
                            renderedElements.Add(gameObject);
                        }
                        else
                        {
                            Shape2D shape = gameObject as Shape2D;
                            graphics.FillRectangle(new SolidBrush(shape.Color), shape.Position.X, shape.Position.Y, shape.Scale.Width, shape.Scale.Height);
                            renderedElements.Add(gameObject);
                        }
                    }
                }

                for (int i = 0; i < renderedElements.Count; i++)
                {
                    elementsToRender.Remove(renderedElements[i]);
                }
                renderedElements.Clear(); // Memory Management
                currentPriority++; // Increase priority
            }

            // Stop rendering
            isRendering = false;
        }

        public static void translateTransform(Vector2 transform)
        {
            cameraPositions.Add(transform);
        }

        //
        // Methods the user can implement
        //

        public virtual void OnLoad()
        {

        }

        public virtual void OnDraw()
        {

        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnClosed()
        {
            GameLoopThread.Abort();
        }

        public virtual void OnException(Exception e)
        {
            Log.Error(e.Message);
            GameLoopThread.Abort();
        }

        public virtual void OnKeyDown(KeyEventArgs e)
        {

        }

        public virtual void OnKeyUp(KeyEventArgs e)
        {

        }

        public virtual void OnMouseDown(MouseEventArgs e)
        {

        }
        public virtual void OnMouseUp(MouseEventArgs e)
        {

        }

        public virtual void OnMouseMoved(MouseEventArgs e)
        {

        }
        public virtual void OnMouseDoubleClicked(MouseEventArgs e)
        {

        }
    }
}
