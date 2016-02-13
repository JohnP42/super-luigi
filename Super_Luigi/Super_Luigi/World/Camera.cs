using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Super_Luigi.Player;
using Super_Luigi.World.Levels;

namespace Super_Luigi.World
{
    class Camera
    {
        Vector2 center;
        float zoom;
        Matrix transform;
        Viewport viewport;

        public Vector2 Center { get { return center; } set { center = value; } }
        public float Zoom { get { return zoom; } set { zoom = value; } }
        public Matrix Transform { get { return transform; } set { transform = value; } }


        public Camera(Viewport v)
        {
            viewport = v;
            zoom = 2f;
        }

        public void Update(Luigi luigi, Level level)
        {

            center = new Vector2(MathHelper.Clamp(luigi.Body.X + 6, viewport.Width / 4, level.XSize * 24 - viewport.Width / 4),
                MathHelper.Clamp(luigi.Body.Y + 16, level.YSize * -24 + viewport.Height / 4, -viewport.Height / 4));

            transform = Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0)) *
                                                Matrix.CreateRotationZ(0) *
                                                Matrix.CreateScale(new Vector3(zoom, zoom, 0)) *
                                                Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0));
        }

    }
}
