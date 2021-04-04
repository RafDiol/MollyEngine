using MollyEngine.MollyEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MollyEngine.MollyEngine.Extensions
{
    public class PlayerController
    {
        public GameObject gameObject { get; set; }
        public ulong speed { get; set; }

        private bool up, down, left, right;

        public PlayerController(GameObject gameObject, ulong speed)
        {
            this.gameObject = gameObject;
            this.speed = speed;
        }

        public void OnKeyDownHandler(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) { left = true; }
            if (e.KeyCode == Keys.D) { right = true; }
            if (e.KeyCode == Keys.S) { down = true; }
            if (e.KeyCode == Keys.W) { up = true; }
        }

        public void OnKeyUpHandler(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) { left = false; }
            if (e.KeyCode == Keys.D) { right = false; }
            if (e.KeyCode == Keys.S) { down = false; }
            if (e.KeyCode == Keys.W) { up = false; }
        }

        public void Update()
        {
            if (up)
            {
                gameObject.Position.Y -= speed;
            }
            if (down)
            {
                gameObject.Position.Y += speed;
            }
            if (left)
            {
                gameObject.Position.X -= speed;
            }
            if (right)
            {
                gameObject.Position.X += speed;
            }
        }
    }
}
