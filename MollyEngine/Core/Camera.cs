using MollyEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MollyEngine.Core;

namespace MollyEngine.Core
{
    public class Camera
    {
        Vector2 cameraPos = new Vector2();
        public Camera()
        {
            cameraPos = Vector2.Zero();
        }

        public Camera(float X, float Y)
        {
            cameraPos.X = X;
            cameraPos.Y = Y;
        }

        public void moveTo(Vector2 newPos)
        {
            cameraPos = newPos;
            MollyEngine.translateTransform(cameraPos);
        }

        public void moveTo(float X, float Y)
        {
            moveTo(new Vector2(X, Y));
        }

        public Vector2 getCurrentPos()
        {
            return cameraPos;
        }
    }
}
