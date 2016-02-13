using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Super_Luigi.World.Items;
using Super_Luigi.World.Levels;
using Microsoft.Xna.Framework.Audio;

namespace Super_Luigi.World.Blocks
{
    abstract class Block
    {
        bool solid, breakable, destroyed;
        Rectangle position;
        Rectangle[] srcRect;
        int imageIndex;
        float imageTime, imageSpeed, imageCount;
        int offsetX, offsetY;
        Item heldItem;
        SoundEffect hitSound, breakSound;
        Color color = Color.White;

        public Rectangle Position { get { return position; } set { position = value; } }
        public Rectangle[] SrcRect { get { return srcRect; } set { srcRect = value; } }
        public int ImageIndex { get { return imageIndex; } set { imageIndex = value; } }
        public float ImageTime { get { return imageTime; } set { imageTime = value; } }
        public float ImageSpeed { get { return imageSpeed; } set { imageSpeed = value; } }
        public float ImageCount { get { return imageCount; } set { imageCount = value; } }
        public bool Solid { get { return solid; } set { solid = value; } }
        public bool Breakable { get { return breakable; } set { breakable = value; } }
        public bool Destroyed { get { return destroyed; } set { destroyed = value; } }
        public Item HeldItem { get { return heldItem; } set { heldItem = value; } }
        public int OffsetX { get { return offsetX; } set { offsetX = value; } }
        public int OffsetY { get { return offsetY; } set { offsetY = value; } }
        public SoundEffect HitSound { get { return hitSound; } set { hitSound = value; } }
        public SoundEffect BreakSound { get { return breakSound; } set { breakSound = value; } }
        public Color Colour { get { return color; } set { color = value; } }

        public virtual void onDestroyed(Level l)
        {
            breakSound.Play();

            if (heldItem is ItemCoin)
                l.ParticleSystems.Add(new Particles.ParticleSystemCoin(Position.X / 24, Position.Y / 24));

            if (HeldItem != null)
                l.Items.Add(heldItem);

        }

        public virtual void onHit(Level l)
        {
            if(!breakable)
                offsetY -= 5;
            else
                destroy(l);

            hitSound.Play();
        }

        public virtual void destroy(Level l)
        {
            destroyed = true;
            onDestroyed(l);
        }

        public void update()
        {

            if (offsetY < 0)
                offsetY++;

            animate();

        }

        public void animate()
        {
            if (imageCount >= imageTime)
            {
                if (imageIndex < srcRect.Length - 1)
                {
                    imageIndex++;
                }
                else
                    imageIndex = 0;
                imageCount = 0;
            }
            else
                imageCount += imageSpeed;
        }

    }
}
