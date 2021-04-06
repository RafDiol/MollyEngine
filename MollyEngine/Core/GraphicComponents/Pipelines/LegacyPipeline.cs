using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace MollyEngine.Core.GraphicComponents.Pipelines
{
    public class LegacyPipeline : Pipeline
    {
        public LegacyPipeline(MollyEngine mollyEngine, Graphics graphics)
        {
            gameEngine = mollyEngine;
            this.graphics = graphics;
        }

        public void Render(ref List<Vector2> cameraPositions, List<GameObject> AllGameObjects)
        {
            graphics.Clear(gameEngine.BackgroundColor);

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
                            graphics.DrawImage(sprite.getImage(), sprite.Position.X, sprite.Position.Y, sprite.Scale.Width, sprite.Scale.Height);
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
        }
    }
}
