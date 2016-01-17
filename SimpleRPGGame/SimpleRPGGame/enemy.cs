using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalXNAGame
{
    class enemy
    {
        private int attack, defense, speed, attackSpeed, knockback, maxHp, hp, experience, cols, rows, countX, countY, direction;
        private Texture2D imagesL, imagesR, drawImage, projectile;
        private Rectangle source, draw, projDraw;
        private double attackTime;
        private bool isBoss;
        private Vector2 projVel;
        private SoundEffect dieSound;


        public enemy(int att, int def, int spd, int attckSpd, int knkbk, int health, int exp, Texture2D imgL, Texture2D imgR, int col, int row, bool boss, SoundEffect killSound, GraphicsDeviceManager screen, Random rand)
        {
            //sets enemies attack, defense, movement speed, attack speed, knockback, hp, and experience points given when killed
            attack = att;
            defense = def;
            speed = spd;
            attackSpeed = attckSpd;
            knockback = knkbk;
            maxHp = health;
            hp = health;
            experience = exp;
            //sets enemy images
            imagesL = imgL;
            imagesR = imgR;
            //sets draw image to left image
            drawImage = imagesL;
            //sets number of rows and columns on sprite sheet
            cols = col;
            rows = row;
            //shows whether enemy is a boss
            isBoss = boss;
            //sets sound for when enemy is killed
            dieSound = killSound;
            //finds size of one sprite on sheet
            source.Width = (int)((double)imagesL.Width / cols + .5);
            source.Height = (int)((double)imagesL.Height / rows + .5);
            //sets draw height to 100
            draw.Height = 100;
            //doubles size if enemy is a boss
            if (isBoss)
                draw.Height *= 2;
            //makes width proportional
            draw.Width = draw.Height * source.Width / source.Height;
            //sets enemy poeition so they are on the ground
            draw.Y = screen.GraphicsDevice.Viewport.Height - draw.Height;
            //randomly spawns enemy on right or left of screen
            draw.X = rand.Next(0, 2) * screen.GraphicsDevice.Viewport.Width;
            //randomly selects animation frame
            countX = rand.Next(0, cols);
            countY = rand.Next(0, rows);
            //resets projectile location
            projDraw.Y = -100;
            projDraw.X = -100;
            //resets projectile speed
            projVel.X = 0;
            projVel.Y = 0;
        }

        //allows program to access attack, defense, hp, experience points, knockback, direction, position, or kill sound of enemy
        public int Attack
        {
            get { return attack; }
        }

        public int Defense
        {
            get { return defense; }
        }
        
        public int HP
        {
            get { return hp; }
        }

        public int Experience
        {
            get { return experience; }
        }
        
        public int Knockback
        {
            get { return knockback; }
        }
        
        public int Direction
        {
            get { return direction; }
        }

        public Rectangle Position
        {
            get { return draw; }
        }

        public SoundEffect KillSound
        {
            get { return dieSound; }
        }

        //lets program set an image for the projectile
        public Texture2D Projectile
        {
            set 
            { 
                projectile = value;
                //sets height of projectile and makes width proportional
                projDraw.Height = 50;
                projDraw.Width = (int)(50.0 /projectile.Height * projectile.Width);
            }
        }

        public void Update(Rectangle player, Random rand, GameTime time, GraphicsDeviceManager graphics)
        {
            //sets direction of enemy to face player
            if (player.X < draw.X)
            {
                direction = -1;
                drawImage = imagesL;
            }
            else
            {
                direction = 1;
                drawImage = imagesR;
            }
            //moves enemy in direction of player based on speed
            draw.X += speed * direction / 10;
            //sets sprite image based on animation counters
            source.X = countX * source.Width;
            source.Y = countY * source.Height;
            //occurs every 0.1 seconds
            if ((int)time.TotalGameTime.TotalMilliseconds % 100 == 0)
            {
                //if enemy is a boss and projectile is not currently on the screen randomly creat a projectile
                if (rand.Next(1, 11) == 1 && isBoss && projDraw.Y > graphics.GraphicsDevice.Viewport.Height)
                {
                    //set intitial projectile position based on enemy position and direction
                    projDraw.X = draw.X + (direction + 1) * draw.Width / 2 - projDraw.Width * direction;
                    projDraw.Y = draw.Y;
                    //set projectile moving up and in the direction the enemy is moving
                    projVel.Y = -50;
                    projVel.X = 20 * direction;
                }
                //accelerate projectile downwards
                projVel.Y += 10f;
                //move projectile using porjectiles velocity
                projDraw.X += (int)projVel.X;
                projDraw.Y += (int)projVel.Y;
                //increment horizontalanimation counter
                countX++;
                //checks if horizontal animation counter has reached end
                if (countX >= cols)
                {
                    //set horizonal counter to beginning
                    countX = 0;
                    //increment vertical counter
                    countY++;
                    //checks if vertical counter has readhed end
                    if (countY >= rows)
                    {
                        //set vertical counter to beginning
                        countY = 0;
                    }
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            //draws enemy
            sb.Draw(drawImage, draw, source, Color.White);
            //if enemy is boss draws projectile
            if (isBoss)
            {
                sb.Draw(projectile, projDraw, Color.White);
            }
        }

        //check if enemy hit player
        public bool CheckHit(Rectangle player, GameTime time)
        {
            //checks if enough time has passed
            if (time.TotalGameTime.TotalMilliseconds - attackTime > attackSpeed)
            {
                //creates rectangle of intersection with player and enemy
                Rectangle collide = Rectangle.Intersect(player, draw);
                //checks if enemy overlaps player by more than 40 pixels
                if (collide.Width > 40)
                {

                    //saves time when enemy hit player and retuens true
                    attackTime = time.TotalGameTime.TotalMilliseconds;
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        //checks if player hit enemy with sword
        public bool CheckSwordHit(Rectangle player, int dir, int damage)
        {
            //creates rectangle of intersection with player and enemy
            Rectangle collide = Rectangle.Intersect(player, draw);
            //checks if player is facing enemy and sword is overlapping enemy
            if (((collide.X > player.X && dir == 1) || (collide.X == player.X && dir == -1)) && collide.Width > 10)
            {
                //reduces enemy's hp based on damage of attack
                hp -= CheckDamage(defense, damage);
                //moves enemy back from knockback
                draw.X += dir * 30;
                return true;
            }
            else
                return false;
        }

        //checks if enemy is hit by projectile
        public bool CheckCollision(Rectangle projectile, int damage)
        {
            //checks for intersection between enemy and projectile
            if (projectile.Intersects(draw))
            {
                //reduces enemy's hp based on damage of projectile
                hp -= CheckDamage(defense, damage);                
                return true;
            }
            else
                return false;
        }

        //checks if boss's projetcile hit player
        public bool BossProjectile(Rectangle player)
        {
            //checks that enemy is boss and that projectile intersects with player
            if (projDraw.Intersects(player) && isBoss)
            {
                //removes projectile
                projDraw.X = -1000;
                return true;
            }
            else
                return false;
        }

        //prevent overlap between enemies
        public void checkOverlap(int numEnemies, enemy[] enemies, int enemyNum)
        {
            //goes through all other enemies
            for (int i = 1; i <= numEnemies - 1; i++)
            {
                //prevent checking for collisions with itself
                if (i != enemyNum)
                {
                    //creates rectangle of intersection between the enemies
                    Rectangle collide = Rectangle.Intersect(enemies[i].Position, draw);
                    //checks if the enemies are overlapping by more than 10
                    if (collide.Width - 10 > 0)
                    {
                        //moves enemies by amount that they are overlapping
                        if (draw.X > enemies[i].Position.X)
                            draw.X += collide.Width - 10;
                        else
                            draw.X -= collide.Width - 10;
                    }
                }
            }
        }

        public int CheckDamage(int def, int attck)
        {
            //returns amount of damge based on the defense and attack
            return (int)((double)attck / ((double)def / 2));
        }

        public void ReInitialize(GraphicsDeviceManager screen, Random rand)
        {
            //resets enemy's hp
            hp = maxHp;
            //randomly moves enemy to righ or left side of screen
            draw.X = rand.Next(2) * screen.GraphicsDevice.Viewport.Width;
            //randomly selects and animation frame
            countX = rand.Next(0, cols);
            countY = rand.Next(0, rows);
        }

    }
}
