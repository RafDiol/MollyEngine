using MollyEngine.MollyEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MollyEngine.Animation
{
    public class Animator
    {
        List<string> frames = new List<string>();
        int? totalTime = null, milsecPerFrame = null;
        Sprite2D sprite;
        System.Threading.Timer animationThread;
        int frameCount = 0;

        public bool isPlaying = false;

        public Animator(Sprite2D sprite, int milsecPerFrame, string[] frames)
        {
            this.sprite = sprite;
            foreach (string s in frames)
            {
                this.frames.Add(s);
            }
            this.milsecPerFrame = milsecPerFrame;
        }

        public Animator(Sprite2D sprite, string[] frames, int totalAnimationTime)
        {
            this.sprite = sprite;
            foreach (string s in frames)
            {
                this.frames.Add(s);
            }
            this.totalTime = totalAnimationTime;
        }

        public Animator(Sprite2D sprite, List<string> frames, int totalAnimationTime)
        {
            this.sprite = sprite;
            this.frames = frames;
            this.totalTime = totalAnimationTime;
        }

        public Animator(Sprite2D sprite, int milsecPerFrame, List<string> frames)
        {
            this.sprite = sprite;
            this.frames = frames;
            this.milsecPerFrame = milsecPerFrame;
        }

        public void Start()
        {
            if (milsecPerFrame != null)
            {
                totalTime = frames.Count * milsecPerFrame;
            }
            else
            {
                milsecPerFrame = totalTime / frames.Count;
            }

            animationThread = new System.Threading.Timer(_animate, null, 0, (int)milsecPerFrame);
            isPlaying = true;
        }

        public void Stop()
        {
            animationThread.Dispose();
            isPlaying = false;
        }

        private void _animate(object state)
        {
            if (frameCount > frames.Count - 1)
            {
                frameCount = 0;
                sprite.setImage(frames[frameCount]);
            }
            else
            {
                sprite.setImage(frames[frameCount]);
                frameCount++;
            }
        }
    }
}
