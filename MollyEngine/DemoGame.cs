using System;
using System.Windows.Forms;
using MollyEngine;
using MollyEngine.Animation;
using MollyEngine.Core;
using System.IO;
using MollyEngine.Extensions;

namespace MollyEngine
{
    public class DemoGame : Core.MollyEngine
    {
        //Shape2D shape;
        Sprite2D sprite;
        ulong speed = 1;
        Random rnd = new Random();
        PlayerController playerController;
        Camera camera = new Camera();
        Vector2 cameraPos = new Vector2();

        public DemoGame() : base(new Scale(512, 512), "New Game")
        {

        }

        public override void OnLoad()
        {
            sprite = new Sprite2D(new Vector2(100, 100), new Scale(100, 100), Path.GetFullPath(@"Images\img1.jpg") , "Player", 1);
            playerController = new PlayerController(sprite, speed);
            for (int i = 0; i < 30; i++)
            {
                Sprite2D coin = new Sprite2D(new Vector2(rnd.Next(0, 1200), rnd.Next(0, 800)), new Scale(50, 50), Path.GetFullPath(@"Images\coin.jpg"), $"coin{i}");
            }
        }

        public override void OnUpdate()
        {
            playerController.Update();
            GameObject collide;
            if (sprite.isColliding(out collide))
            {
                Sprite2D coin = (Sprite2D)collide;
                coin.DestroySelf();
            }
        }

        public override void OnKeyDown(KeyEventArgs e)
        {
            playerController.OnKeyDownHandler(e);
        }

        public override void OnKeyUp(KeyEventArgs e)
        {
            playerController.OnKeyUpHandler(e);
        }
    }
}
