using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace MollyEngine.Core.GraphicComponents.Pipelines
{
    class MollyNewPipeline : Pipeline
    {
        public MollyNewPipeline(MollyEngine mollyEngine, Graphics graphics)
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

            List<uint> layers = new List<uint>();
            
            foreach(GameObject gameObject in elementsToRender)
            {
                if (!layers.Contains(gameObject.renderingPriority))
                {
                    layers.Add(gameObject.renderingPriority);
                }
            }

            uint currentPriority = 0;
            List<GameObject> renderedElements = new List<GameObject>();

            while (elementsToRender.Count != 0)
            {
                for (int i = 0; i < layers.Count; i++)
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

                    for (int j = 0; j < renderedElements.Count; j++)
                    {
                        elementsToRender.Remove(renderedElements[j]);
                    }
                    renderedElements.Clear(); // Memory Management
                    currentPriority = layers[i]; // Increase priority
                }
            }
        }
    }
}
