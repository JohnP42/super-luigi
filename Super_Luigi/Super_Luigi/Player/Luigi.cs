using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Super_Luigi.World.Levels;
using Super_Luigi.World.Blocks;
using Super_Luigi.World.Particles;
using Super_Luigi.World.Items;
using Super_Luigi.World.Enemies;
using Super_Luigi.World.Auras;
using Super_Luigi.Player.Projectiles;

namespace Super_Luigi.Player
{
    class Luigi
    {
        //Power ups
        int transform;
        int star;
        bool fireLuigi;
        bool throwing;
        string toTransform;
        Aura aura;
        List<Projectile> projectiles;

        //Physics
        bool running;
        float acc;
        float runAcc;
        float maxSpeed;
        float maxRunSpeed;
        float maxVspeed;
        float jumpForce;
        float friction;
        float air;

        Vector2 velocity;
        float gravity;

        bool onGround;
        bool onWall;
        float superJump;
        int jumpLevel;
        int currentJump;
        int jumpCount;
        bool canJump;
        bool ducking;
        int hurt, tripped;

        String badge;

        //Position
        Rectangle body;
        bool facingRight;

        //Sprites
        Vector2 offDraw;
        Texture2D spriteSheet;
        int imageIndex;
        float imageSpeed, imageTime, rotation;
        SpriteEffects flip = SpriteEffects.None;
        Rectangle[] currentSrcRect;
        Rectangle[] srcStand, srcWalk, srcRun, srcSpeed, srcJump, srcJump2, srcJump3, srcSkid, srcDuck, srcHurt, srcTripped, srcFireball, srcTransform;
        Color color;

        //Particles
        ParticleSystem dust;
        int dustTime;

        //Controlls
        bool[] buttons = new bool[6];
        KeyboardState prevK = Keyboard.GetState();
        int coins;
        int hp;
        int lives;

        //Accessors
        public Rectangle[] SrcStand { get { return srcStand; } set { srcStand = value; } }
        public Rectangle[] SrcWalk { get { return srcWalk; } set { srcWalk = value; } }
        public Rectangle[] SrcRun { get { return srcRun; } set { srcRun = value; } }
        public Rectangle[] SrcSpeed { get { return srcSpeed; } set { srcSpeed = value; } }
        public Rectangle[] SrcJump { get { return srcJump; } set { srcJump = value; } }
        public Rectangle[] SrcJump2 { get { return srcJump2; } set { srcJump2 = value; } }
        public Rectangle[] SrcJump3 { get { return srcJump3; } set { srcJump3 = value; } }
        public Rectangle[] SrcSkid { get { return srcSkid; } set { srcSkid = value; } }
        public Rectangle[] SrcDuck { get { return srcDuck; } set { srcDuck = value; } }
        public Rectangle[] SrcHurt { get { return srcHurt; } set { srcHurt = value; } }
        public Rectangle[] SrcTripped { get { return srcTripped; } set { srcTripped = value; } }
        public Rectangle[] SrcFireball { get { return srcFireball; } set { srcFireball = value; } }
        public Rectangle[] SrcTransform { get { return srcTransform; } set { srcTransform = value; } }

        public int Coins { get { return coins; } set { coins = value; } }
        public int Hp { get { return hp; } set { hp = value; } }
        public int Lives { get { return lives; } set { lives = value; } }

        public int Star { get { return star; } set { star = value; } }


        public bool[] Buttons { get { return buttons; } set { buttons = value; } }
        public Rectangle Body { get { return body; } set { body = value; } }

        public Luigi(Vector2 pos)
        {

            coins = 0;
            hp = 3;
            lives = 3;

            running = false;
            acc = .2f;
            runAcc = .2f;
            maxSpeed = 3f;
            maxRunSpeed = 6f;
            maxVspeed = 6f;
            jumpForce = 7f;
            friction = .05f;
            air = .01f;
            hurt = 0;
            tripped = 0;

            velocity = Vector2.Zero;
            gravity = .7f;

            body = new Rectangle((int)pos.X + 4, (int)pos.Y + 2, 16, 32);
            facingRight = true;

            canJump = true;
            ducking = false;
            onGround = false;
            onWall = false;
            superJump = 0f;
            jumpLevel = 0;
            currentJump = 0;
            jumpCount = 0;

            badge = "None";
            fireLuigi = false;
            star = 0;
            transform = 0;
            toTransform = "";
            aura = null;
            throwing = false;
            projectiles = new List<Projectile>();

            dust = new ParticleSystem();
            dustTime = 0;

            color = Color.White;
            spriteSheet = Game1.sprLuigi;
            srcRects();
        }

        public void input(KeyboardState k)
        {

            if (k.IsKeyDown(Keys.Right))
                buttons[0] = true;
            else
                buttons[0] = false;

            if (k.IsKeyDown(Keys.Left))
                buttons[1] = true;
            else
                buttons[1] = false;

            if (k.IsKeyDown(Keys.Up))
                buttons[2] = true;
            else
                buttons[2] = false;

            if (k.IsKeyDown(Keys.Down))
                buttons[3] = true;
            else
                buttons[3] = false;

            if (k.IsKeyDown(Keys.Space))
            {
                buttons[4] = true;
            }
            else
            {
                buttons[4] = false;
                canJump = true;
            }

            if (k.IsKeyDown(Keys.X))
                buttons[5] = true;
            else
                buttons[5] = false;



            prevK = Keyboard.GetState();
        }

        public void input(GamePadState g)
        {

            if (g.ThumbSticks.Left.X >= .1f)
                buttons[0] = true;
            else
                buttons[0] = false;

            if (g.ThumbSticks.Left.X <= -.1f)
                buttons[1] = true;
            else
                buttons[1] = false;

            if (g.ThumbSticks.Left.Y >= .1f)
                buttons[2] = true;
            else
                buttons[2] = false;

            if (g.ThumbSticks.Left.Y <= -.1f)
                buttons[3] = true;
            else
                buttons[3] = false;

            if (g.Buttons.A == ButtonState.Pressed)
            {
                buttons[4] = true;
            }
            else
            {
                buttons[4] = false;
                canJump = true;
            }

            if (g.Buttons.X == ButtonState.Pressed)
                buttons[5] = true;
            else
                buttons[5] = false;



            prevK = Keyboard.GetState();
        }

        public void emptyParticleSystems()
        {
            if(dustTime > 0)
                dustTime--;

            for (int i = dust.Particles.Count - 1; i >= 0; i--)
            {
                if (!dust.Particles[i].Alive)
                    dust.Particles.RemoveAt(i);
            }

            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                if (MathHelper.Distance(projectiles[i].Position.X, body.X) > 300)
                    projectiles[i].Alive = false;

                if (!projectiles[i].Alive)
                    projectiles.RemoveAt(i);
            }

        }

        public void update(Level level)
        {
            if (hurt > 0)
                hurt--;

            if (aura != null)
                aura.update(level, this);

            powerUps();
            if(transform == 0)
                movement(level);
            animation();
            sprites();
            dust.update();
            emptyParticleSystems();

        }

        public void powerUps()
        {
            //Transform
            if (transform > 0)
                transform--;

            if (transform == 0 && aura != null)
                aura = null;

            if (transform == 60)
            {
                if (toTransform == "Star")
                    star = 840;
                else if (toTransform == "Fire")
                    fireLuigi = true;

                Game1.luigiSounds[6].Play();
                aura = new Aura(toTransform, this);
                toTransform = "";

            }

            if (star > 0)
            {
                star--;
                color = new Color(Particle.random.Next(0, 256), Particle.random.Next(0, 256), Particle.random.Next(0, 256), 255);
                dust.Particles.Add(new ParticleStar(body.Left + body.Width / 2, body.Top + body.Height / 2));
            }
            else
                color = Color.White;


        }

        public void wallJump(Level level)
        {
            if (!onGround && velocity.Y > 0)
            {
                foreach (Block b in level.Blocks)
                {

                    if (b.Destroyed)
                        continue;

                    if (b.Solid)
                    {
                        if(buttons[0])
                        {
                            if (new Rectangle(body.X + 1, body.Y, body.Width, body.Height).Intersects(b.Position))
                            {

                                    onWall = true;

                                    if (buttons[4] && canJump)
                                    {
                                        currentJump = 0;
                                        Game1.luigiSounds[3].Play();
                                        velocity.X -= jumpForce;
                                        velocity.Y = -jumpForce;
                                        onWall = false;
                                        canJump = false;
                                        superJump = .55f;
                                        facingRight = false;
                                    }

                                    return;
                            }
                        }
                        else if (buttons[1])
                        {
                            if (new Rectangle(body.X - 1, body.Y, body.Width, body.Height).Intersects(b.Position))
                            {

                                    onWall = true;

                                    if (buttons[4] && canJump)
                                    {
                                        currentJump = 0;
                                        Game1.luigiSounds[3].Play();
                                        velocity.X += jumpForce;
                                        velocity.Y = -jumpForce;
                                        onWall = false;
                                        canJump = false;
                                        superJump = .55f;
                                        facingRight = true;
                                    }

                                    return;

                            }
                        }
                    }

                }
            }

            onWall = false;

        }

        public void movement(Level level)
        {

            //Gravity
            if (!onGround)
            {
                velocity.Y += gravity - superJump;

                if (!onWall)
                {
                    if (velocity.Y > maxVspeed)
                        velocity.Y = maxVspeed;
                }
                else if (velocity.Y > maxVspeed * .25)
                    velocity.Y -= velocity.Y * .3f;

            }
            else
            {
                if (jumpCount > 0)
                    jumpCount--;

                if (jumpCount == 0)
                {
                    jumpLevel = 0;
                    currentJump = 0;
                }
            }

            if (hurt == 0 && tripped == 0)
            {

                //Duck
                if (buttons[3])
                {
                    if (onGround)
                    {
                        if (!ducking)
                            body.Y += 12;

                        ducking = true;
                        body.Height = 24;
                    }
                }
                else
                {
                    if (ducking)
                        body.Y -= 12;

                    ducking = false;
                    body.Height = 36;
                }

                if (!ducking)
                {
                    //Walk and Run
                    if (buttons[5])
                    {
                        if(!running)
                        {
                            if(fireLuigi && projectiles.Count < 4 && !throwing)
                            {
                                throwing = true;
                            }
                        }

                        running = true;

                    }
                    else
                        running = false;

                    float inAir = 1f;

                    if (!onGround)
                        inAir = .65f;

                    if (!running)
                    {
                        if (buttons[0] && velocity.X < maxSpeed)
                            velocity.X += acc * inAir;

                        if (buttons[1] && velocity.X > -maxSpeed)
                            velocity.X -= acc * inAir;
                    }
                    else
                    {
                        if (badge != "Speed" && star == 0)
                        {
                            if (buttons[0] && velocity.X < maxRunSpeed)
                                velocity.X += runAcc * inAir;

                            if (buttons[1] && velocity.X > -maxRunSpeed)
                                velocity.X -= runAcc * inAir;
                        }
                        else
                        {
                            if (buttons[0] && velocity.X < maxRunSpeed + 4f)
                                velocity.X += runAcc * inAir;

                            if (buttons[1] && velocity.X > -maxRunSpeed - 4f)
                                velocity.X -= runAcc * inAir;
                        }
                    }


                    wallJump(level);

                }

            }
                if (velocity.X != 0 && onGround)
                {
                    if (velocity.X > 0)
                        velocity.X -= friction;
                    else
                        velocity.X += friction;

                    if (Math.Abs(velocity.X) < friction)
                        velocity.X = 0;

                }

                if (velocity.X != 0 && !onGround)
                {
                    if (velocity.X > 0)
                        velocity.X -= air;
                    else
                        velocity.X += air;

                    if (Math.Abs(velocity.X) < air)
                        velocity.X = 0;

                }

                if (Math.Abs(velocity.X) < 4.5f)
                    jumpLevel = 0;

                if (hurt == 0 && tripped == 0)
                {

                    //jump
                    if (buttons[4])
                    {
                        if (onGround && canJump)
                        {
                            canJump = false;
                            velocity.Y = 0;
                            if (jumpLevel == 0)
                            {
                                Game1.luigiSounds[0].Play();
                                velocity.Y -= jumpForce;
                                if (Math.Abs(velocity.X) >= 4.5f)
                                {
                                    superJump = .55f;
                                    jumpCount = 10;
                                    jumpLevel = 1;
                                }
                                else
                                    superJump = .5f;

                                currentJump = 0;
                            }
                            else if (jumpLevel == 1)
                            {
                                Game1.luigiSounds[1].Play();
                                velocity.Y -= jumpForce;
                                if (Math.Abs(velocity.X) >= 4.5f)
                                {
                                    superJump = .58f;
                                    jumpCount = 10;
                                    jumpLevel = 2;
                                }
                                else
                                    superJump = .55f;

                                currentJump = 1;
                            }
                            else if (jumpLevel == 2)
                            {
                                Game1.luigiSounds[2].Play();
                                velocity.Y -= jumpForce;
                                if (Math.Abs(velocity.X) >= 4.5f)
                                {
                                    superJump = .61f;
                                }
                                else
                                    superJump = .58f;

                                jumpLevel = 0;
                                currentJump = 2;
                            }
                        }
                    }
                    else
                        superJump = 0f;
                }

            
            //final
            body.X += (int)velocity.X;
            body.Y += (int)velocity.Y;

            //Projectiles
            foreach(Projectile p in projectiles)
            {
                p.update(level);
            }

            //Collision
            blockCollision(level);
            itemCollision(level);
            if(hurt == 0)
                enemyCollision(level);

        }

        public void enemyCollision(Level level)
        {

            foreach (Enemy e in level.Enemies)
            {
                if (!e.Alive || e.Stomped != -1 || e.Hurt != -1)
                    continue;

                if (body.Intersects(e.Position))
                {
                    if (star == 0)
                    {
                        if ((Rectangle.Intersect(body, e.Position).Width >= Rectangle.Intersect(body, e.Position).Height || velocity.Y > 1) && tripped == 0)
                        {

                            if (Rectangle.Intersect(body, e.Position).Top == e.Position.Top)
                            {
                                velocity.Y = -jumpForce;
                                superJump = .55f;
                                e.onStomp(level);
                            }
                            else if (Rectangle.Intersect(body, e.Position).Bottom == e.Position.Bottom)
                            {
                                if (tripped == 0)
                                {
                                    if (Math.Abs(velocity.X) < maxSpeed + 1)
                                    {
                                        hurt = 20;
                                        velocity = new Vector2(0, 3);
                                        Game1.luigiSounds[4].Play();
                                    }
                                    else
                                    {
                                        tripped = 20;
                                        velocity = new Vector2(velocity.X, -3);
                                        Game1.luigiSounds[5].Play();
                                    }

                                    Game1.otherSounds[1].Play();
                                    hp--;
                                    throwing = false;
                                    if (fireLuigi)
                                    {
                                        fireLuigi = false;
                                        Game1.otherSounds[7].Play();
                                    }
                                }
                                else if (imageIndex == 3)
                                {
                                    e.onHit(level);
                                }
                            }
                        }
                        else
                        {

                            if (Rectangle.Intersect(body, e.Position).Left == e.Position.Left)
                            {
                                if (e is Koopa && ((Koopa)e).InShell && (e.Velocity.X == 0 || ((Koopa)e).Lethal > 0))
                                {
                                    e.Velocity = new Vector2(velocity.X + 2, e.Velocity.Y);
                                    Game1.otherSounds[5].Play();
                                    ((Koopa)e).Lethal = 10;
                                }
                                else
                                {
                                    if (tripped == 0)
                                    {
                                        if (Math.Abs(velocity.X) < maxSpeed + 1)
                                        {
                                            hurt = 20;
                                            velocity = new Vector2(-3, -3);
                                            Game1.luigiSounds[4].Play();
                                        }
                                        else
                                        {
                                            tripped = 20;
                                            velocity = new Vector2(velocity.X, -3);
                                            Game1.luigiSounds[5].Play();
                                        }

                                        Game1.otherSounds[1].Play();
                                        hp--;
                                        throwing = false;
                                        if (fireLuigi)
                                        {
                                            fireLuigi = false;
                                            Game1.otherSounds[7].Play();
                                        }
                                    }
                                    else if (imageIndex == 3)
                                    {
                                        e.onHit(level);
                                    }
                                }

                            }
                            else if (Rectangle.Intersect(body, e.Position).Right == e.Position.Right)
                            {

                                if (e is Koopa && ((Koopa)e).InShell && (e.Velocity.X == 0 || ((Koopa)e).Lethal > 0))
                                {
                                    e.Velocity = new Vector2(velocity.X - 2, e.Velocity.Y);
                                    Game1.otherSounds[5].Play();
                                    ((Koopa)e).Lethal = 10;
                                }
                                else
                                {
                                    if (tripped == 0)
                                    {
                                        if (Math.Abs(velocity.X) < maxSpeed + 1)
                                        {
                                            hurt = 20;
                                            velocity = new Vector2(3, -3);
                                            Game1.luigiSounds[4].Play();
                                        }
                                        else
                                        {
                                            tripped = 20;
                                            velocity = new Vector2(velocity.X, -3);
                                            Game1.luigiSounds[5].Play();
                                        }

                                        Game1.otherSounds[1].Play();
                                        hp--;
                                        throwing = false;
                                        if (fireLuigi)
                                        {
                                            fireLuigi = false;
                                            Game1.otherSounds[7].Play();
                                        }
                                    }
                                    else if (imageIndex == 3)
                                    {
                                        e.onHit(level);
                                    }
                                }
                            }

                        }
                    }
                    else
                    {
                        if ((Rectangle.Intersect(body, e.Position).Width >= Rectangle.Intersect(body, e.Position).Height || velocity.Y > 1) && tripped == 0)
                        {
                            if (Rectangle.Intersect(body, e.Position).Top == e.Position.Top)
                            {
                                velocity.Y = -jumpForce;
                                superJump = .55f;
                                e.onStomp(level);
                            }
                            else
                                e.onHit(level);
                        }
                        else
                            e.onHit(level);
                    }

                    break;

                }

            }

        }

        public void blockCollision(Level level)
        {
            onGround = false;

            foreach (Block b in level.Blocks)
            {
                if(b.Destroyed)
                    continue;

                if (b.Solid)
                {

                    if (body.Intersects(b.Position))
                    {
                        Block final = b;

                        foreach (Block n in level.getAdjacentBlocks(b))
                        {

                            if (Rectangle.Intersect(body, n.Position).Width * Rectangle.Intersect(body, n.Position).Height > Rectangle.Intersect(body, final.Position).Width * Rectangle.Intersect(body, final.Position).Height)
                                final = n;

                        }
                            if (Rectangle.Intersect(body, final.Position).Width >= Rectangle.Intersect(body, final.Position).Height)
                            {

                                if (Rectangle.Intersect(body, final.Position).Top == final.Position.Top)
                                {
                                    body.Y = final.Position.Top - body.Height;
                                    velocity.Y = 0;
                                    onGround = true;
                                }
                                else if (Rectangle.Intersect(body, final.Position).Bottom == final.Position.Bottom)
                                {
                                    body.Y = final.Position.Bottom;
                                    velocity.Y *= -1;

                                    final.onHit(level);

                                    if (level.getItemAboveBlock(final) != null)
                                        level.getItemAboveBlock(final).Velocity = new Vector2(level.getItemAboveBlock(final).Velocity.X, -6);

                                    if (level.getEnemyAboveBlock(final) != null)
                                        level.getEnemyAboveBlock(final).onHit(level);

                                }
                            }
                            else
                            {

                                 if (Rectangle.Intersect(body, final.Position).Left == final.Position.Left)
                                {
                                    if (velocity.X > 0)
                                    {
                                        velocity.X = 0;
                                        body.X = final.Position.Left - body.Width;
                                    }

                                }
                                else if (Rectangle.Intersect(body, final.Position).Right == final.Position.Right)
                                {
                                    if (velocity.X < 0)
                                    {
                                        velocity.X = 0;
                                        body.X = final.Position.Right;
                                    }
                                }

                            }

                            break;

                    }

                    Rectangle temp = b.Position;
                    temp.Y -=1;

                    if (body.Intersects(temp))
                        onGround = true;
                }
            }


        }

        public void itemCollision(Level level)
        {

            foreach (Item i in level.Items)
            {
                if (body.Intersects(i.Position))
                {
                    if (i is ItemCoin)
                        coins++;

                    if (i is Mushroom && hp < 3)
                        hp++;

                    if (i is Starman)
                    {
                        toTransform = "Star";
                        transform = 120;
                        MediaPlayer.Play(Game1.songs[1]);
                        Game1.luigiSounds[7].Play();
                    }

                    if (i is Fireflower)
                    {
                        toTransform = "Fire";
                        transform = 120;
                        Game1.luigiSounds[7].Play();
                    }

                    i.grabbed(level);
                    break;
                }
            }

        }

        public void spawnDust(int x, int y)
        {
            if (dustTime == 0)
            {
                dust.Particles.Add(new ParticleDust(x, y));
                dust.Particles.Add(new ParticleDust(x, y));
                if(Math.Abs(velocity.X) > 4.5)
                    dust.Particles.Add(new ParticleDust(x, y));

                if(badge != "Speed" && star == 0)
                    dustTime = (int)maxRunSpeed + Particle.random.Next(0, 3) - Math.Abs((int)velocity.X);
                else
                    dustTime = (int)maxRunSpeed + 4 + Particle.random.Next(0, 3) - Math.Abs((int)velocity.X);
            }
        }

        public void sprites()
        {

            if (fireLuigi)
                spriteSheet = Game1.sprFireLuigi;
            else
                spriteSheet = Game1.sprLuigi;

            Rectangle[] temp = currentSrcRect;

            if (facingRight)
                flip = SpriteEffects.None;
            else
                flip = SpriteEffects.FlipHorizontally;

            if (transform == 0)
            {
                if (hurt == 0 && tripped == 0)
                {
                    if (!ducking)
                    {
                        if(!throwing)
                        {
                       
                        if (onGround)
                        {

                            if (buttons[0])
                                facingRight = true;

                            if (buttons[1])
                                facingRight = false;

                            //standing
                            if (velocity.X == 0)
                            {
                                currentSrcRect = srcStand;
                                imageSpeed = 1f;
                            }
                            else if (velocity.X > 0)//Moving Right
                            {
                                spawnDust(body.Left + body.Width / 2, body.Bottom);
                                if (!buttons[1])
                                {

                                    if (velocity.X > maxSpeed && running)
                                    {
                                        if (velocity.X <= maxRunSpeed || badge != "Speed" && star == 0)
                                        {
                                            currentSrcRect = srcRun;
                                            imageSpeed = Math.Abs(velocity.X) * .4f;
                                        }
                                        else
                                        {
                                            currentSrcRect = srcSpeed;
                                            imageSpeed = Math.Abs(velocity.X) * .6f;
                                        }
                                    }
                                    else
                                    {
                                        currentSrcRect = srcWalk;
                                        imageSpeed = Math.Abs(velocity.X) * .25f;
                                    }
                                }
                                else
                                {
                                    if (running)
                                    {
                                        currentSrcRect = srcSkid;
                                        imageSpeed = 0f;
                                        imageIndex = 0;
                                    }
                                    else
                                    {
                                        currentSrcRect = srcWalk;
                                        imageSpeed = Math.Abs(velocity.X) * .25f;
                                    }
                                }
                            }
                            else //Moving Left
                            {
                                spawnDust(body.Left + body.Width / 2, body.Bottom);
                                if (!buttons[0])
                                {
                                    if (velocity.X < -maxSpeed && running)
                                    {
                                        if (velocity.X >= -maxRunSpeed || badge != "Speed" && star == 0)
                                        {
                                            currentSrcRect = srcRun;
                                            imageSpeed = Math.Abs(velocity.X) * .4f;
                                        }
                                        else
                                        {
                                            currentSrcRect = srcSpeed;
                                            imageSpeed = Math.Abs(velocity.X) * .6f;
                                        }
                                    }
                                    else
                                    {
                                        currentSrcRect = srcWalk;
                                        imageSpeed = Math.Abs(velocity.X) * .25f;
                                    }
                                }
                                else
                                {
                                    if (running)
                                    {
                                        currentSrcRect = srcSkid;
                                        imageSpeed = 0f;
                                        imageIndex = 0;
                                    }
                                    else
                                    {
                                        currentSrcRect = srcWalk;
                                        imageSpeed = Math.Abs(velocity.X) * .25f;
                                    }
                                }
                            }


                        }
                        else
                        {
                            if (!onWall)
                            {
                                //jumping
                                if (currentJump == 0)
                                {
                                    currentSrcRect = srcJump;
                                    imageSpeed = 0;

                                    if (velocity.Y < -.5f)
                                        imageIndex = 0;
                                    else if (velocity.Y > 2f)
                                        imageIndex = 2;
                                    else
                                        imageIndex = 1;
                                }
                                else if (currentJump == 1)
                                {
                                    currentSrcRect = srcJump2;
                                    imageSpeed = 0;

                                    if (velocity.Y < -2f)
                                        imageIndex = 0;
                                    else if (velocity.Y < -1f)
                                        imageIndex = 1;
                                    else if (velocity.Y < 0f)
                                        imageIndex = 2;
                                    else if (velocity.Y < 1f)
                                        imageIndex = 3;
                                    else if (velocity.Y < 2f)
                                        imageIndex = 4;
                                    else
                                        imageIndex = 5;

                                }
                                else if (currentJump == 2)
                                {
                                    currentSrcRect = srcJump3;
                                    imageSpeed = 5f;
                                }
                            }
                            else
                            {

                                if (buttons[0])
                                    facingRight = true;

                                if (buttons[1])
                                    facingRight = false;

                                //preping wall jump
                                if (facingRight)
                                    spawnDust(body.Right, body.Bottom);
                                else
                                    spawnDust(body.Left, body.Bottom);

                                currentSrcRect = srcSkid;
                                imageSpeed = 0f;
                                imageIndex = 0;
                            }

                        }

                        }
                        else
                        {
                            currentSrcRect = srcFireball;
                                imageSpeed = 5f;
                                if (fireLuigi && imageIndex == 2 && imageTime == 0)
                                {
                                    projectiles.Add(new Fireball(body.X + body.Width / 2, body.Top + body.Height / 4, facingRight));
                                    projectiles[projectiles.Count - 1].CreateSound.Play();
                                }

                                if (imageIndex == 3 && imageTime == 10)
                                    throwing = false;

                        }

                    }
                    else
                    {
                        if (buttons[0])
                            facingRight = true;

                        if (buttons[1])
                            facingRight = false;

                        if (velocity.X != 0)
                            spawnDust(body.Left + body.Width / 2, body.Bottom);

                        currentSrcRect = srcDuck;
                        imageSpeed = 0f;
                        imageIndex = 0;
                    }

                }
                else if (hurt > 0)
                {
                    currentSrcRect = srcHurt;
                    imageSpeed = 0f;
                    imageIndex = 0;
                }
                else if (tripped > 0)
                {
                    if (imageIndex < 2)
                    {
                        currentSrcRect = srcTripped;
                        imageSpeed = 1f;
                    }
                    else if (velocity.X == 0)
                    {
                        currentSrcRect = srcTripped;
                        imageSpeed = 0f;
                        imageIndex = 4;
                        tripped--;
                    }
                    else
                    {
                        if (onGround)
                        {
                            currentSrcRect = srcTripped;
                            imageSpeed = 0f;
                            imageIndex = 3;
                            spawnDust(body.Left + body.Width / 2, body.Bottom);
                        }
                        else
                        {
                            currentSrcRect = srcTripped;
                            imageSpeed = 0f;
                            imageIndex = 2;
                        }
                    }
                }
            }
            else
            {
                if (transform > 60)
                {
                    currentSrcRect = srcTransform;
                    imageSpeed = 0f;
                    imageIndex = 0;
                }
                else
                {
                    currentSrcRect = srcTransform;
                    imageSpeed = 0f;
                    imageIndex = 1;
                }
            }

            if (!temp.Equals(currentSrcRect))
            {
                imageIndex = 0;
            }

            offDraw.Y = body.Height - currentSrcRect[imageIndex].Height;
            offDraw.X = (body.Width - currentSrcRect[imageIndex].Width) / 2;

        }

        public void animation()
        {

            if (imageTime >= 10f)
            {
                if (imageIndex < currentSrcRect.Length - 1)
                    imageIndex++;
                else imageIndex = 0;

                imageTime = 0f;
            }
            else
            {
                imageTime += imageSpeed;
            }

        }

        public void draw(SpriteBatch spritebatch)
        {

            //Aura
            if(aura != null)
                aura.Draw(spritebatch);

            //spritebatch.Draw(Game1.sprTiles, body, Game1.sprTiles.Bounds, Color.Black, 0f, Vector2.Zero, SpriteEffects.None, .5f);
            spritebatch.Draw(spriteSheet, new Vector2(body.X, body.Y) + offDraw, currentSrcRect[imageIndex], color, rotation, Vector2.Zero, 1f, flip, .1f);

            //Projectiles
            foreach (Projectile p in projectiles)
            {
                if (p.FacingRight)
                    spritebatch.Draw(spriteSheet, new Vector2(p.Position.X + p.OffsetX, p.Position.Y + p.OffsetY), p.SrcRect[p.ImageIndex], Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, .2f);
                else
                    spritebatch.Draw(spriteSheet, new Vector2(p.Position.X + p.OffsetX, p.Position.Y + p.OffsetY), p.SrcRect[p.ImageIndex], Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, .2f);
            }

            //particle Systems
            dust.draw(spritebatch);

        }

        public void srcRects()
        {
            srcWalk = new Rectangle[5];
            srcWalk[0] = new Rectangle(88, 339, 15, 36);
            srcWalk[1] = new Rectangle(108, 340, 15, 35);
            srcWalk[2] = new Rectangle(128, 341, 15, 34);
            srcWalk[3] = new Rectangle(148, 340, 15, 35);
            srcWalk[4] = new Rectangle(167, 341, 15, 34);

            srcRun = new Rectangle[6];
            srcRun[0] = new Rectangle(256, 1363, 21, 38);
            srcRun[1] = new Rectangle(286, 1362, 21, 37);
            srcRun[2] = new Rectangle(317, 1364, 21, 37);
            srcRun[3] = new Rectangle(346, 1363, 21, 38);
            srcRun[4] = new Rectangle(376, 1362, 21, 37);
            srcRun[5] = new Rectangle(405, 1364, 21, 37);

            srcSpeed = new Rectangle[8];
            srcSpeed[0] = new Rectangle(209, 398, 29, 29);
            srcSpeed[1] = new Rectangle(241, 397, 29, 30);
            srcSpeed[2] = new Rectangle(273, 396, 29, 30);
            srcSpeed[3] = new Rectangle(307, 397, 28, 30);
            srcSpeed[4] = new Rectangle(338, 397, 28, 30);
            srcSpeed[5] = new Rectangle(370, 398, 28, 29);
            srcSpeed[6] = new Rectangle(401, 397, 28, 28);
            srcSpeed[7] = new Rectangle(433, 398, 28, 29);


            srcStand = new Rectangle[3];
            srcStand[0] = new Rectangle(171, 72, 18, 38);
            srcStand[1] = new Rectangle(194, 73, 20, 37);
            srcStand[2] = new Rectangle(219, 74, 23, 36);

            srcJump = new Rectangle[3];
            srcJump[0] = new Rectangle(580, 444, 24, 38);
            srcJump[1] = new Rectangle(609, 445, 24, 37);
            srcJump[2] = new Rectangle(637, 446, 24, 36);

            srcJump2 = new Rectangle[6];
            srcJump2[0] = new Rectangle(9, 935, 33, 34);
            srcJump2[1] = new Rectangle(49, 932, 27, 38);
            srcJump2[2] = new Rectangle(81, 934, 32, 36);
            srcJump2[3] = new Rectangle(120, 937, 28, 31);
            srcJump2[4] = new Rectangle(154, 935, 32, 35);
            srcJump2[5] = new Rectangle(192, 935, 29, 36);

            srcJump3 = new Rectangle[8];
            srcJump3[0] = new Rectangle(12, 1201, 28, 37);
            srcJump3[1] = new Rectangle(42, 1200, 24, 38);
            srcJump3[2] = new Rectangle(69, 1200, 15, 38);
            srcJump3[3] = new Rectangle(86, 1200, 23, 38);
            srcJump3[4] = new Rectangle(110, 1199, 30, 39);
            srcJump3[5] = new Rectangle(14, 1246, 23, 38);
            srcJump3[6] = new Rectangle(46, 1245, 15, 38);
            srcJump3[7] = new Rectangle(70, 1245, 24, 38);

            srcSkid = new Rectangle[2];
            srcSkid[0] = new Rectangle(12, 824, 22, 34);
            srcSkid[1] = new Rectangle(42, 823, 26, 35);

            srcDuck = new Rectangle[1];
            srcDuck[0] = new Rectangle(354, 1672, 16, 26);

            srcHurt = new Rectangle[1];
            srcHurt[0] = new Rectangle(402, 187, 26, 31);

            srcTripped = new Rectangle[5];
            srcTripped[0] = new Rectangle(300, 185, 26, 35);
            srcTripped[1] = new Rectangle(525, 344, 34, 30);
            srcTripped[2] = new Rectangle(564, 347, 29, 27);
            srcTripped[3] = new Rectangle(600, 357, 32, 22);
            srcTripped[4] = new Rectangle(638, 365, 41, 14);

            srcFireball = new Rectangle[4];
            srcFireball[0] = new Rectangle(180, 988, 31, 37);
            srcFireball[1] = new Rectangle(217, 987, 37, 38);
            srcFireball[2] = new Rectangle(264, 987, 29, 38);
            srcFireball[3] = new Rectangle(300, 987, 26, 38);

            srcTransform = new Rectangle[4];
            srcTransform[0] = new Rectangle(16, 1612, 16, 34);
            srcTransform[1] = new Rectangle(39, 1610, 23, 37);

            currentSrcRect = srcStand;
            imageSpeed = 1f;
            imageTime = 0f;
            rotation = 0f;
            imageIndex = 0;
            offDraw = new Vector2(-3, -6);
        }



    }
}
