using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalXNAGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //initialize variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D knights, knightDie, bowL, bowR, bow, arrowL, arrowR, arrow, fireballL, fireballR, fireball, batL, batR, skeletonL, skeletonR, gorgonL, gorgonR, bowUpgL, bowUpgR, magicUpgL, magicUpgR, background, caveBackground, forestBackground, jungleBackground, templeBackground, titleBackground, shopBackground, arrowBundle, hpPotion, mpPotion, item, hpTexture, mpTexture, swampMonster, gooShot;
        int count, spriteSect, slashSpeed, bowSpeed, arrowSpeed, magicSpeed, fireSpeed, dir, hp, maxHp, mp, maxMp, attck, def, arrAttck, magicAttck, arrows, totEnemies, numEnemies, level, exp, armUpgCst, swdUpgCst, bowUpgCst, mgcUpgCst;
        double slashTime, bowTime, magicTime, hitTime;
        Vector2 speed, pos;
        Rectangle source, draw, bowDraw, arrowDraw, magicDraw, backgroundDraw, itemDraw, hpBar, mpBar;
        bool slash, shoot;
        enemy[] enemies = new enemy[6];
        SpriteFont gameFont, titleFont;
        Random rand = new Random();
        string gameState, upgMessage;
        KeyboardState oldState;
        SoundEffect swordSlash, bowShot, magicCast, hit, hurt, batKilled, skeletonKilled, gorgonkilled, bossKilled;
        Song titleMusic, shopMusic, gameOverMusic, caveMusic, forestMusic, jungleMusic, bossMusic;
        
        ///Variable Dictionary
        ///graphics     -holds information about graphics creation and screen
        ///spritebatch  -used to draw sprites and text on screen
        ///gameTime     -holds the elapsed time of the game
        ///knights      -holds the sprite sheet for the knight sprites
        ///knightDie    -holds the image of the knight dieing
        ///bowL         -holds the left image of the bow
        ///bowR         -holds the right image for the bow
        ///bow          -holds the image of the bow to be drawn
        ///arrowL       -holds the left image of the arrow
        ///arrowR       -holds the right image of the arrow
        ///arrow        -holds the image of the arrow to be drawn
        ///fireballL    -holds the left image of the fireball
        ///fireballR    -holds the right image for the fireball
        ///fireball     -holds the image of the fireball to be drawn
        ///batL         -holds the left sprites of the bat
        ///batR         -holds the right sprites of the bat
        ///skeletonL    -holds the left sprites of the skeleton
        ///skeletonR    -holds the right sprites of the skeleton
        ///gorgonL      -holds the left sprites of the gorgon
        ///gorgonR      -holds the right sprites of the gorgon
        ///bowUpgL      -holds the left image of the upgraded bow
        ///bowUpgR      -holds the right image of the upgraded bow
        ///magicUpgL    -holds the left image of the upgraded magic attack
        ///magicUpgR    -holds the right image of the upgraded magic attack
        ///background   -holds image of the background to be drawn
        ///caveBackground   -holds image of the cave background
        ///forestBackground -holds image of the forest background
        ///jungleBackground -holds image of the jungle background
        ///templeBackground -holds image of the temple background
        ///titleBackground  -holds image of the title screen background
        ///shopBackground   -holds image of the shop background
        ///arrowBundle  -holds image of arrow bundle item
        ///hpPotion     -holds image of hp potion item
        ///mpPotion     -holds image of mp potion item
        ///item         -holds image of item to be drawn
        ///hpTexture    -holds texture for hp bar
        ///mpTexture    -holds texture for mp bar
        ///swampMonster -holds sprites of swamp monster boss
        ///gooShot      -holds image of boss's goo projectile
        ///count        -used to cycle through knight sprite animations
        ///spriteSect   -holds the section of the sprite sheet to get the sprites from
        ///slashSpeed   -time user must wait between sword slashes
        ///bowSpeed     -time user must wait between bow uses
        ///magicSpeed   -time user must wait between magic uses
        ///arrowSpeed   -holds speed of the arrows movement
        ///fireSpeed    -holds speed of the fireball's movement
        ///dir          -holds the direction the player is facing
        ///hp           -holds the players current health
        ///maxHp        -holds the maximum health of the player
        ///mp           -holds the player's current magic points
        ///maxMp        -holds the maximum magic points of the player
        ///attack       -holds the player's attack power
        ///def          -holds the player's defense value
        ///arrAttck     -holds the attack power of the arrows
        ///magicAttck   -holds the attack power of the magic attack
        ///arrows       -holds the number of arrows held by th player
        ///totEnemies   -holds the total number of enemies in a level
        ///numEnemies   -holds the number of enemies on the screen at a time
        ///level        -holds the current level number
        ///exp          -holds the number of experience points the player has
        ///armUpgCost   -holds the cost of upgrading armour using experience points
        ///swdUpgCost   -holds the cost of upgrading sword using experience points
        ///bowUpgCost   -holds the cost of upgrading bow using experience points
        ///mgcUpgCost   -holds the cost of upgrading magic using experience points
        ///slashTime    -holds the previous time that the player used the sword
        ///bowTime      -holds the previous time that the player used the bow
        ///magicTime    -holds the previous time that the player used the magic attack
        ///hitTime      -holds the previous time that the player hit an enemy
        ///speed        -holds the movement speed of the player
        ///pos          -holds the position of the player
        ///source       -holds position and size of current knight sprite on knights sprite sheet
        ///draw         -holds position and size of knight to be drawn
        ///bowDraw      -holds position and size of bow to be drawn
        ///arrowDraw    -holds position and size of arrow to be drawn
        ///magicDraw    -holds position and size of magic spell to be drawn
        ///backgroundDraw   -holds position and size of background to be drawn
        ///itemdraw     -holds position and size of item to be drawn
        ///hpBar        -holds position and size of hp bar to be drawn
        ///mpBar        -holds position and size of mp bar to be drawn
        ///slash        -holds whether or not player is currently using sword
        ///shoot        -holds whether or not player is currently using bow
        ///enemies      -holds all information of and controls all the enemies
        ///gameFont     -holds the standard font usied in the game
        ///titleFont    -holds the font used for titles
        ///rand         -used for randomization in the game
        ///gameState    -holds the current screen that the game is on
        ///upgMessage   -holds message shown when upgrading equipment
        ///oldState     -holds the previous state of the keyboard
        ///swordSlash   -holds sound effect for slashing sword
        ///bowShot      -holds sound effect for shooting bow
        ///magicCast    -holds sound effect for casting magic
        ///hit          -holds sound effect for hitting enemy
        ///hurt         -holds sound effect for getting hurt
        ///batKilled    -holds sound effect for killing bat
        ///skeletonKilled   -holds sound effect for killing skeleton
        ///gorgonKilled -holds sound effect for killing gorgon
        ///bossKilled   -holds sound effect for killing boss
        ///titleMusic   -holds title screen background music
        ///shopMusic    -holds shop screen background music
        ///gameOverMusic-holds game over screen background music
        ///caveMusic    -holds cave level background music
        ///forestMusic  -holds forest level background music
        ///jungleMusic  -holds jungle level background music
        ///bossMusic    -holds boss level background music
        ///
        ///Enemy Class
        ///direction    -holds the direction the enemy is facing
        ///hp           -holds the enemy's current health
        ///maxHp        -holds the maximum health of the enemy
        ///attack       -holds the enemy's attack power
        ///defense      -holds the enemy's defense value
        ///attackSpeed  -holds the time between attacks by the enemy
        ///knockback    -holds the distance the enemy knocks the player back when attacking
        ///experience   -holds the experience points given when the enemy is killed
        ///cols         -holds the number of columns on the enemy's sprite sheet
        ///rows         -holds the number of rows on the enemy's sprite sheet
        ///countX       -holds the current row of the sprite animation on the sprite sheet
        ///countY       -holds the current column of the sprite animation on the sprite sheet
        ///imagesL      -holds the left images of the enemy
        ///imagesR      -holds the right images of the enemy
        ///drawImage    -holds the images being drawn
        ///projectile   -holds the image of the projectile
        ///source       -holds the position and size of the current sprite on the sprite sheet
        ///draw         -draw holds the position and size of the sprite to be drawn
        ///projDraw     -holds the position and size of the projectile being drawn
        ///attackTime   -holds the last time that the enemy attacked the player
        ///isBoss       -holds whether or not the enemy is a boss
        ///projVel      -holds the velocity of the projectile
        ///dieSound     -holds the sound of the enemy being killed
        ///collide      -holds the rectangle of intersection betweeen two rectangles for collsion detection

        public Game1()
        {
            //sets graphics device manager and content directory
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //set all variables to zero or their default values
            count = 3;
            source.Y = 0;
            source.X = 0;
            speed.X = 0;
            speed.Y = 0;
            pos.Y = 0;
            slashTime = 0;            
            slashSpeed = 1000;
            bowTime = 0;
            bowSpeed = 1000;
            bowDraw.X = 0;
            bowDraw.Y = 0;
            arrowSpeed = 0;
            arrowDraw.X = -100;
            arrowDraw.Y = -100;
            magicSpeed = 1000;
            magicTime = 0;
            fireSpeed = 0;
            magicDraw.Y = -100;
            magicDraw.X = -100;
            dir = -1;
            maxHp = 100;
            maxMp = 50;
            arrows = 20;
            attck = 40;
            arrAttck = 20;
            magicAttck = 30;
            def = 30;
            level = 1;
            exp = 0;
            gameState = "Title";
            armUpgCst = 10;
            swdUpgCst = 10;
            bowUpgCst = 10;
            mgcUpgCst = 10;
            upgMessage = "";
            itemDraw.X = 0;
            itemDraw.Y = -100;
            hpBar.Height = 20;
            hpBar.Width = 500;
            hpBar.X = 160;
            hpBar.Y = 10;
            mpBar.Height = 20;
            mpBar.Width = 500;
            mpBar.X = 160;
            mpBar.Y = 40;
            //gets the initial keyboard state
            oldState = Keyboard.GetState();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //loads in knight sprites
            knights = this.Content.Load<Texture2D>("Graphics\\Knight");
            knightDie = this.Content.Load<Texture2D>("Graphics//KnightDie");
            //sets the width and height of one sprite on the sprite sheet
            source.Width = knights.Width / 8;
            source.Height = knights.Height / 2;
            //sets the height for gthe player sprite to be drawn as 100 and makes the width proportional
            draw.Height = 100;
            draw.Width = (int)(100.0 / (knights.Height / 2) * knights.Width / 8);
            //loads bow sprites
            bowL = this.Content.Load<Texture2D>("Graphics//bow");
            bowR = this.Content.Load<Texture2D>("Graphics//bowR");
            bowUpgL = this.Content.Load<Texture2D>("Graphics//Sacred_Bow");
            bowUpgR = this.Content.Load<Texture2D>("Graphics//Sacred_BowR");
            //sets bow image to left-facing bow
            bow = bowL;
            //sets the drawing size of bow to half the images original size
            bowDraw.Height = bowL.Height / 2;
            bowDraw.Width = bowL.Width / 2;
            //loads arrow sprites
            arrowL = this.Content.Load<Texture2D>("Graphics//arrow");
            arrowR = this.Content.Load<Texture2D>("Graphics//arrowR");
            //set arrow image to left arrow
            arrow = arrowL;
            //makes drawing size with height of 10 and proportional width
            arrowDraw.Height = 10;
            arrowDraw.Width = (int)(10.0 / arrowL.Height * arrowL.Width); ;
            //loads sprites for magic attacks
            fireballL = this.Content.Load<Texture2D>("Graphics//Fireball");
            fireballR = this.Content.Load<Texture2D>("Graphics//FireballR");
            magicUpgL = this.Content.Load<Texture2D>("Graphics//energy_ball");
            magicUpgR = this.Content.Load<Texture2D>("Graphics//energy_ballR");
            //sets drawing size to half original size
            magicDraw.Height = fireballL.Height / 2;
            magicDraw.Width = fireballL.Width / 2;
            //sets fireball image to left image
            fireball = fireballL;
            //loads all enemy sprites
            gorgonL = this.Content.Load<Texture2D>("Graphics//gorgon");
            gorgonR = this.Content.Load<Texture2D>("Graphics//gorgonR");
            batL = this.Content.Load<Texture2D>("Graphics//bat");
            batR = this.Content.Load<Texture2D>("Graphics//batR");
            skeletonL = this.Content.Load<Texture2D>("Graphics//skeleton");
            skeletonR = this.Content.Load<Texture2D>("Graphics//skeletonR");
            swampMonster = this.Content.Load<Texture2D>("Graphics//swampMonster");
            gooShot = this.Content.Load<Texture2D>("Graphics//gooShot");
            //loads all background images
            caveBackground = this.Content.Load<Texture2D>("Graphics//caveBackground");
            forestBackground = this.Content.Load<Texture2D>("Graphics//background_forest");
            jungleBackground = this.Content.Load<Texture2D>("Graphics//jungleBackground");
            templeBackground = this.Content.Load<Texture2D>("Graphics//templeBackground");
            shopBackground = this.Content.Load<Texture2D>("Graphics//shopBackground");
            titleBackground = this.Content.Load<Texture2D>("Graphics//titleBackground");
            //sets background height to height of screen and makes width proportional
            backgroundDraw.Height = graphics.GraphicsDevice.Viewport.Height;
            backgroundDraw.Width = (int)((double)backgroundDraw.Height / titleBackground.Height * titleBackground.Width);
            //loads in item sprites
            arrowBundle = this.Content.Load<Texture2D>("Graphics//arrowBundle");
            hpPotion = this.Content.Load<Texture2D>("Graphics//hp_potion");
            mpPotion = this.Content.Load<Texture2D>("Graphics//mp_potion");
            //sets item image to hp potion
            item = hpPotion;
            //following code found from http://stackoverflow.com/questions/2792694/draw-rectangle-with-xna
            //creates a blank red and blue texture for hp and mp bars
            hpTexture = new Texture2D(GraphicsDevice, 1, 1);
            hpTexture.SetData(new Color[] { Color.Red });
            mpTexture = new Texture2D(GraphicsDevice, 1, 1);
            mpTexture.SetData(new Color[] { Color.Blue });
            //loads in fonts
            gameFont = this.Content.Load<SpriteFont>("Fonts//GameFont");
            titleFont = this.Content.Load<SpriteFont>("Fonts//TitleFont");
            //loads in all sound effects
            swordSlash = this.Content.Load<SoundEffect>("Sounds//swordSlash");
            bowShot = this.Content.Load<SoundEffect>("Sounds//bowShot");
            magicCast = this.Content.Load<SoundEffect>("Sounds//magicCast");
            hit = this.Content.Load<SoundEffect>("Sounds//hit");
            hurt = this.Content.Load<SoundEffect>("Sounds//hurt");
            batKilled = this.Content.Load<SoundEffect>("Sounds//batKilled");
            skeletonKilled = this.Content.Load<SoundEffect>("Sounds//skeletonKilled");
            gorgonkilled = this.Content.Load<SoundEffect>("Sounds//gorgonKilled");
            bossKilled = this.Content.Load<SoundEffect>("Sounds//bossKilled");
            //loads in all background music
            titleMusic = this.Content.Load<Song>("Sounds//little-traveller");
            shopMusic = this.Content.Load<Song>("Sounds//Shop Theme");
            gameOverMusic = this.Content.Load<Song>("Sounds//sad-story");
            caveMusic = this.Content.Load<Song>("Sounds//boring-cavern");
            forestMusic = this.Content.Load<Song>("Sounds//lost-hero");
            jungleMusic = this.Content.Load<Song>("Sounds//mazy-jungle");
            bossMusic = this.Content.Load<Song>("Sounds//evil-throne");
            //sets media player to loop and starts title music
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(titleMusic);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            this.Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            //gets the keys pressed on the keyboard
            KeyboardState keyPress = Keyboard.GetState();
            //state machine for all stages of the game
            switch (gameState)
            {
                //title screen
                case "Title":
                    //starts the game
                    if (keyPress.IsKeyDown(Keys.Enter))
                    {
                        gameState = "Play";
                        Reset();
                    }
                    //goes to help screen
                    else if (keyPress.IsKeyDown(Keys.H))
                        gameState = "Help";
                    break;
                //help screen
                case "Help":
                    //goes to title screen
                    if (keyPress.IsKeyDown(Keys.Escape))
                        gameState = "Title";
                    break;
                //main gameplay
                case "Play":
                    //lets player pause game
                    if (keyPress.IsKeyDown(Keys.P))
                        gameState = "Pause";
                    //checks for directional keypresses
                    if (keyPress.IsKeyDown(Keys.Right))
                    {
                        //sets horizontal speed
                        speed.X = 6;
                        //used to show player direction
                        dir = 1;
                        //sets bow to right image
                        bow = bowR;
                        //sets knight image source to right facing sprites
                        source.Y = knights.Height / 2;
                    }
                    else if (keyPress.IsKeyDown(Keys.Left))
                    {
                        //sets horizontal speed
                        speed.X = -6;
                        //shows players direction
                        dir = -1;
                        //sets bow image to left image
                        bow = bowL;
                        //sets knight image source to let facing sprites
                        source.Y = 0;
                    }
                    else
                    {
                        //sets horizontal speed to zero if no key is pressed
                        speed.X = 0;
                    }
                    
                    if (keyPress.IsKeyDown(Keys.Up))
                    {
                        //makes sure player is on the ground
                        if (GraphicsDevice.Viewport.Height - draw.Height <= pos.Y)
                        {
                            //sets vertical speed
                            speed.Y = -10;
                        }
                    }
                    //lets player use sword if they are not already using their bow and enough time has passed
                    if (keyPress.IsKeyDown(Keys.Z) && !shoot)
                    {
                        if (gameTime.TotalGameTime.TotalMilliseconds - slashTime > slashSpeed)
                        {
                            //sets sprite section to section with sword sprites
                            spriteSect = knights.Width / 2;
                            //shows that player is using sword
                            slash = true;
                            //sets time that player used sword
                            slashTime = gameTime.TotalGameTime.TotalMilliseconds;
                            //resets count to beginning
                            count = 3;
                            //plays sword slash sound
                            swordSlash.Play();
                        }
                    }
                    //lets player use bow if they are not using sword
                    else if (keyPress.IsKeyDown(Keys.X) && !slash)
                    {
                        //makes sure enouhg time has passed and the player has enough arrows left
                        if (gameTime.TotalGameTime.TotalMilliseconds - bowTime > bowSpeed && arrows > 0)
                        {
                            //removes an arrow
                            arrows--;
                            //shows that player is using bow
                            shoot = true;
                            //sets arrow position based on bows position
                            arrowDraw.X = bowDraw.X;
                            arrowDraw.Y = bowDraw.Y + bowDraw.Height / 2;
                            //sets arrow speed based on player's direction
                            arrowSpeed = 20 * dir;
                            //saves the time when player used bow
                            bowTime = gameTime.TotalGameTime.TotalMilliseconds;
                            //plays bow shot sound
                            bowShot.Play();
                            //sets arrow image based on player's direction
                            if (dir == 1)
                                arrow = arrowR;
                            else
                                arrow = arrowL;
                        }
                    }
                    //lets player use magic if they are not already using sword or bow
                    else if (keyPress.IsKeyDown(Keys.C) && !slash && !shoot)
                    {
                        //makes sure enough time has passed and player has enouhg mp left
                        if (gameTime.TotalGameTime.TotalMilliseconds - magicTime > magicSpeed && mp >= 5)
                        {
                            //removes some mp to use magic
                            mp -= 5;
                            //sets mp bar size to show how musch mp is left
                            mpBar.Width = (int)((double)mp / maxMp * 500);
                            //saves the time when player used magic
                            magicTime = gameTime.TotalGameTime.TotalMilliseconds;
                            //sets magic image location based on player's position
                            magicDraw.X = (int)pos.X;
                            magicDraw.Y = (int)pos.Y + draw.Height / 2 - magicDraw.Height / 2;
                            //sets movement speed of fireball based on player direction
                            fireSpeed = 10 * dir;
                            //plays magic sound
                            magicCast.Play();
                            //sets fireball image based on player direction
                            if (dir == 1)
                                fireball = fireballR;
                            else
                                fireball = fireballL;
                        }
                    }

                    //once enough time has passed since player used bow remove the bow
                    if (gameTime.TotalGameTime.TotalMilliseconds - bowTime > bowSpeed)
                    {
                        shoot = false;
                    }
                    //sets bow position based on player's position and direction
                    bowDraw.X = (int)pos.X + (bowDraw.Width - 30) * (dir - 1) / 2 + (draw.Width - 30) * (dir + 1) / 2;
                    bowDraw.Y = (int)pos.Y + draw.Height / 2 - bowDraw.Height / 2;

                    //moves arrows and fireball horizontally by their speed
                    arrowDraw.X += arrowSpeed;
                    magicDraw.X += fireSpeed;

                    //moves player based on speed
                    pos += speed;
                    //makes player accelerate downwards when in the air
                    if (pos.Y < graphics.GraphicsDevice.Viewport.Height - draw.Height)
                    {
                        speed.Y += 0.3f;
                    }
                    //sets vertical speed back to zero when player reaches ground and sets vertical position to the ground
                    else
                    {
                        speed.Y = 0;
                        pos.Y = graphics.GraphicsDevice.Viewport.Height - draw.Height;
                    }

                    //prevents player from leaving boundaries
                    if (pos.X > graphics.GraphicsDevice.Viewport.Width - draw.Width)
                    {
                        pos.X = graphics.GraphicsDevice.Viewport.Width - draw.Width;
                    }
                    else if (pos.X < 0)
                    {
                        pos.X = 0;
                    }
                    //sets the draw rectangle position to player position
                    draw.X = (int)pos.X;
                    draw.Y = (int)pos.Y;

                    //checks if player has picked up item
                    if (Rectangle.Intersect(draw, itemDraw).Width > 40)
                    {
                        //checks what the item is
                        if (item == hpPotion)
                        {
                            //increases hp
                            hp += 20;
                            //makes sure hp does not exceed max hp
                            if (hp > maxHp)
                                hp = maxHp;
                            //sets hp bar size
                            hpBar.Width = (int)((double)hp / maxHp * 500);
                        }
                        else if (item == mpPotion)
                        {
                            //increases mp
                            mp += 10;
                            //makes sure mp does not exceed max mp
                            if (mp > maxMp)
                                mp = maxMp;
                            //sets mp bar size
                            mpBar.Width = (int)((double)mp / maxMp * 500);
                        }
                        else
                            //increases bumber of arrows
                            arrows += 5;
                        //removes the item
                        itemDraw.Y = -100;
                    }
                    //performs all update logic for enemies
                    for (int i = 1; i <= numEnemies; i++)
                    {
                        //updates enemy position and animation
                        enemies[i].Update(draw, rand, gameTime, graphics);
                        //prevents enemies form overlapping
                        enemies[i].checkOverlap(numEnemies, enemies, i);
                        //checks if an arrow hit the enemy
                        if (shoot && enemies[i].CheckCollision(arrowDraw, arrAttck))
                            //removes arrow
                            arrowDraw.Y = -100;
                        //checks if magic attack hit enemy
                        if (enemies[i].CheckCollision(magicDraw, magicAttck))
                            //removes magic image
                            magicDraw.Y = -100;
                        //makes sure only one hit is counted per slash, and checks that the animation frame is at a point when the sword could hit the enemy
                        if (slash && gameTime.TotalGameTime.TotalMilliseconds - hitTime > slashSpeed && count < 2)
                        {
                            //checks if sword hit enemy
                            if (enemies[i].CheckSwordHit(draw, dir, attck))
                            {
                                //sets time when player hit enemy
                                hitTime = gameTime.TotalGameTime.TotalMilliseconds;
                                //plays sound for hitting an enemy
                                hit.Play();
                            }
                        }
                        //checks if an enemy has touched the player
                        if (enemies[i].CheckHit(draw, gameTime))
                        {
                            //reduces players hp and changes hp bar width
                            hp -= enemies[i].CheckDamage(def, enemies[i].Attack);
                            hpBar.Width = (int)((double)hp / maxHp * 500);
                            //moves player back based on enemies knockback
                            pos.X += enemies[i].Knockback * enemies[i].Direction;
                            //plays sound for getting hurt
                            hurt.Play();
                        }
                        //checks if player is hit by projectile from boss
                        if (enemies[i].BossProjectile(draw))
                        {
                            //reduces players hp and changes hp bar width
                            hp -= enemies[i].CheckDamage(def, enemies[i].Attack);
                            hpBar.Width = (int)((double)hp / maxHp * 500);
                            //plays sound for getting hurt
                            hurt.Play();
                        }
                        //checks if enemy has been killed
                        if (enemies[i].HP <= 0)
                        {
                            //gives player esperience points
                            exp += enemies[i].Experience;
                            //redueces number of enemies remaining
                            totEnemies--;
                            //plays sound for killing enemy
                            enemies[i].KillSound.Play();
                            //checks that there are still enemies remaining to be spawned
                            if (numEnemies > totEnemies)
                            {
                                //if not reduce number of enemies onscreen
                                enemies[i] = enemies[numEnemies];
                                numEnemies--;
                            }
                            else
                                //respawn enemy
                                enemies[i].ReInitialize(graphics, rand);
                        }
                    }
                    //checks if all enemies have been killed
                    if (numEnemies <= 0)
                    {
                        //goes to next level
                        level++;
                        //checks if player has finished final level
                        if (level > 4)
                        {
                            //goes to win screen and plays title music
                            gameState = "Win";
                            MediaPlayer.Play(titleMusic);
                        }
                        else
                        {
                            //goes to shop screen and plays shop music
                            gameState = "Shop";
                            MediaPlayer.Play(shopMusic);
                            //sets background height to height of screen and makes width proportional
                            backgroundDraw.Height = graphics.GraphicsDevice.Viewport.Height;
                            backgroundDraw.Width = (int)((double)backgroundDraw.Height / shopBackground.Height * shopBackground.Width);
                        }
                    }
                    //checks if player has lost all hp
                    if (hp <= 0)
                    {
                        //goes to game over screen and plays game over music
                        gameState = "Game Over";
                        MediaPlayer.Play(gameOverMusic);
                    }
                    //occurs every 0.1 seconds
                    if ((int)gameTime.TotalGameTime.TotalMilliseconds % 100 == 0)
                    {
                        //randomly places items
                        RandomizeItems();
                        //sets sprite image based on animation counter and section of sprite sheet beign used
                        source.X = count * knights.Width / 8 + spriteSect;
                        //checks if the player is moving or slashing sword
                        if (Math.Abs(speed.X) > 0 || slash)
                        {
                            //decreases animation counter
                            count--;
                            //checks if animation counter reached end
                            if (count < 0)
                            {
                                //resets animation counter to beginning
                                count = 3;
                                //if player was using sword return to normal sprite section and show that player is finished using sword
                                if (slash)
                                {
                                    spriteSect = 0;
                                    slash = false;
                                }
                            }
                        }
                    }
                    break;
                //pause screen
                case "Pause":
                    //returns to game
                    if (keyPress.IsKeyDown(Keys.Enter))
                        gameState = "Play";
                    //restarts level and returns to game
                    else if (keyPress.IsKeyDown(Keys.R))
                    {
                        Reset();
                        gameState = "Play";
                    }
                    //exits game
                    else if (keyPress.IsKeyDown(Keys.Escape))
                        this.Exit();
                    break;
                //shop screen
                case "Shop":
                    //makes sure only one key press is registered
                    if (keyPress.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
                    {
                        //makes sure player has enough experience points for upgrade
                        if (exp >= armUpgCst)
                        {
                            //uses experience points
                            exp -= armUpgCst;
                            //increases defense
                            def += def / 2;
                            //increases cost of upgrade
                            armUpgCst += armUpgCst / 2;
                            //shows player that they have upgraded their defense
                            upgMessage = "Armour Upgraded!";
                        }
                        else
                            //tells player they cannot upgrade
                            upgMessage = "You do not have enough experience points!";
                    }
                    else if (keyPress.IsKeyDown(Keys.S) && oldState.IsKeyUp(Keys.S))
                    {
                        //makes sure player has enough esperience points
                        if (exp >= swdUpgCst)
                        {
                            //uses experience pints for upgrade
                            exp -= swdUpgCst;
                            //increases attack and upgrade cost
                            attck += attck / 2;
                            swdUpgCst += swdUpgCst / 2;                            
                            upgMessage = "Sword Upgraded!";
                        }
                        else
                            upgMessage = "You do not have enough experience points!";
                    }
                    else if (keyPress.IsKeyDown(Keys.B) && oldState.IsKeyUp(Keys.B))
                    {
                        //checks that player has enough experience points
                        if (exp >= bowUpgCst)
                        {
                            //uses up experience points
                            exp -= bowUpgCst;
                            //increases bow attack and reduces time between bow shots
                            arrAttck += arrAttck / 2;
                            bowSpeed -= bowSpeed / 4;
                            //changes bow images after second upgrade
                            if (bowUpgCst >= 15)
                            {
                                bowL = bowUpgL;
                                bowR = bowUpgR;
                                bowDraw.Height = 120;
                                bowDraw.Width = (int)(120.0 / bowUpgL.Height * bowUpgL.Width);
                            }
                            //increases upgrade cost
                            bowUpgCst += bowUpgCst / 2;
                            upgMessage = "Bow Upgraded!";
                        }
                        else
                            upgMessage = "You do not have enough experience points!";
                    }
                    else if (keyPress.IsKeyDown(Keys.M) && oldState.IsKeyUp(Keys.M))
                    {
                        //checks that player has enough experience points
                        if (exp >= mgcUpgCst)
                        {
                            //uses experience points for upgrade
                            exp -= mgcUpgCst;
                            //increases magic attack power and reduce time between magic attacks
                            magicAttck += magicAttck / 2;
                            magicSpeed -= magicSpeed / 4;
                            //changes magic image after second upgrade
                            if (mgcUpgCst >= 15)
                            {
                                fireballL = magicUpgL;
                                fireballR = magicUpgR;
                                magicDraw.Height = 70;
                                magicDraw.Width = (int)(70.0 / magicUpgL.Height * magicUpgL.Width);
                            }
                            //increases magic upgrade cost
                            mgcUpgCst += mgcUpgCst / 2;
                            upgMessage = "Magic Upgraded!";
                        }
                        else
                            upgMessage = "You do not have enough experience points!";
                    }
                    //returns to game and starts next level
                    if (keyPress.IsKeyDown(Keys.Enter))
                    {
                        //resets upgrade message for next time player enters shop
                        upgMessage = "";
                        Reset();
                        gameState = "Play";
                    }
                    break;
                //game over screen
                case "Game Over":
                    //lets player restart levek
                    if (keyPress.IsKeyDown(Keys.R))
                    {
                        Reset();
                        gameState = "Play";
                    }
                    //lets player exit game
                    else if (keyPress.IsKeyDown(Keys.Escape))
                        this.Exit();
                    break;
                //win screen
                case "Win":
                    //lets player restart game
                    if (keyPress.IsKeyDown(Keys.R))
                    {
                        level = 1;
                        gameState = "Play";
                        Reset();
                    }
                    //lets player exit game
                    if (keyPress.IsKeyDown(Keys.Escape))
                        this.Exit();
                    break;
            }
            //sets previous keybourd state to current keyboard state
            oldState = keyPress;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            //switch for different game screens
            switch (gameState)
            {
                //title screen
                case "Title":
                    //shows title screen background
                    spriteBatch.Draw(titleBackground, backgroundDraw, Color.White);
                    //shows title and instructions for starting game or going to help screen
                    spriteBatch.DrawString(titleFont, "The Mysterious Temple", new Vector2(220, 60), Color.Gold);
                    spriteBatch.DrawString(gameFont, "Press 'Enter' to play", new Vector2(300, 200), Color.Gold);
                    spriteBatch.DrawString(gameFont, "Press 'H' for help", new Vector2(300, 230), Color.Gold);
                    break;
                //help screen
                case "Help":
                    //shows backgorund infromation about game and how to play game
                    spriteBatch.DrawString(titleFont, "Help", new Vector2(300, 60), Color.Gold);
                    spriteBatch.DrawString(gameFont, "In this game you must venture deep into the jungle" + "\n" + "and fight through hordes of enemies to get to the mysterious temple" +"\n"+ "where it is said a wonderful treasure is waiting.", new Vector2(50, 100), Color.Gold);
                    spriteBatch.DrawString(gameFont, "Use the left and right arrow keys to move the character left and right " + "\n" + "and the up arrow key to jump. Use the 'z' key to use your sword, the 'x' key" + "\n" + "to use your arrows and the'c' key to use your magic attack", new Vector2(50, 200), Color.Gold);
                    spriteBatch.DrawString(gameFont, "Press 'Esc' to return to title screen", new Vector2(120, 300), Color.Gold);
                    break;
                //main game screen
                case "Play":
                    //draws level background
                    spriteBatch.Draw(background, backgroundDraw, Color.White);
                    //draws player
                    spriteBatch.Draw(knights, draw, source, Color.White);
                    //draws bow if player is using bow
                    if (shoot)
                        spriteBatch.Draw(bow, bowDraw, Color.White);
                    //draws all enemies
                    for (int i = 1; i <= numEnemies; i++)
                    {
                        enemies[i].Draw(spriteBatch);
                    }
                    //draws item
                    spriteBatch.Draw(item, itemDraw, Color.White);
                    //draws magic attacks and arrow
                    spriteBatch.Draw(fireball, magicDraw, Color.White);
                    spriteBatch.Draw(arrow, arrowDraw, Color.White);
                    //draws hp and mp bars
                    spriteBatch.Draw(hpTexture, hpBar, Color.White);
                    spriteBatch.Draw(mpTexture, mpBar, Color.White);
                    //draws labels for mp and hp bars
                    spriteBatch.DrawString(gameFont, "HP:", new Vector2(120, 10), Color.Red);
                    spriteBatch.DrawString(gameFont, "MP:", new Vector2(120, 40), Color.Blue);
                    //shows player their eperience points and number of arrows
                    spriteBatch.DrawString(gameFont, "Exp: " + exp.ToString(), new Vector2(10, 10), Color.Red);
                    spriteBatch.DrawString(gameFont, "Arrows: " + arrows.ToString(), new Vector2(10, 40), Color.Red);
                    //shows player how to pause
                    spriteBatch.DrawString(gameFont, "Press 'P' to pause", new Vector2(10, 70), Color.Red);
                    break;
                //pause screen
                case "Pause":
                    //draws level background
                    spriteBatch.Draw(background, backgroundDraw, Color.White);
                    //shows that game is paused and gices instructions for how to resume or restart level and how to exit
                    spriteBatch.DrawString(titleFont, "Paused", new Vector2(300, 150), Color.Yellow);
                    spriteBatch.DrawString(gameFont, "Press 'Enter' to resume game", new Vector2(300, 200), Color.Yellow);
                    spriteBatch.DrawString(gameFont, "Press 'R' to restart level", new Vector2(300, 230), Color.Yellow);
                    spriteBatch.DrawString(gameFont, "Press 'Esc' to close game", new Vector2(300, 260), Color.Yellow);
                    break;
                //shop screen
                case "Shop":
                    //draws shop background
                    spriteBatch.Draw(shopBackground, backgroundDraw, Color.White);
                    //shows player their experience points and instructions for how to upgrade different things and their costs
                    spriteBatch.DrawString(gameFont, "Exp: " + exp.ToString(), new Vector2(20, 20), Color.Yellow);
                    spriteBatch.DrawString(gameFont, "Press 'A' to upgrade armor Cost: " + armUpgCst.ToString(), new Vector2(100, 250), Color.DarkBlue);
                    spriteBatch.DrawString(gameFont, "Press 'S' to upgrade sword Cost: " + swdUpgCst.ToString(), new Vector2(100, 280), Color.DarkBlue);
                    spriteBatch.DrawString(gameFont, "Press 'B' to upgrade bow   Cost: " + bowUpgCst.ToString(), new Vector2(100, 310), Color.DarkBlue);
                    spriteBatch.DrawString(gameFont, "Press 'M' to upgrade magic Cost: " + mgcUpgCst.ToString(), new Vector2(100, 340), Color.DarkBlue);
                    //shows a message when player tries to upgrade something
                    spriteBatch.DrawString(gameFont, upgMessage, new Vector2(20, 410), Color.Yellow);
                    //shows player how to get to next level
                    spriteBatch.DrawString(gameFont, "Press 'Enter' to continue", new Vector2(20, 440), Color.Yellow);
                    break;
                //game over screen
                case "Game Over":
                    //draws level background
                    spriteBatch.Draw(background, backgroundDraw, Color.White);
                    //shows image of player dead
                    spriteBatch.Draw(knightDie, draw, Color.White);
                    //shows player that they have lost and how to restart or exit
                    spriteBatch.DrawString(titleFont, "Game Over", new Vector2(300, 150), Color.DarkRed);
                    spriteBatch.DrawString(gameFont, "Press 'R' to restart level", new Vector2(300, 230), Color.Yellow);
                    spriteBatch.DrawString(gameFont, "Press 'Esc' to close game", new Vector2(300, 260), Color.Yellow);
                    break;
                //win screen
                case "Win":
                    //draws title background
                    spriteBatch.Draw(titleBackground, backgroundDraw, Color.White);
                    //tells player that they have won and hwo to restart or close the game
                    spriteBatch.DrawString(titleFont, "You have found the treasure!", new Vector2(180, 150), Color.Yellow);
                    spriteBatch.DrawString(gameFont, "Press 'R' to restart game and continue upgrading your equipment", new Vector2(100, 300), Color.Yellow);
                    spriteBatch.DrawString(gameFont, "Press 'Esc' to close the game", new Vector2(280, 330), Color.Yellow);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        //resets all parts of level
        private void Reset()
        {
            //resets player's hp and mp and changes the mp and hp bar widths
            hp = maxHp;
            mp = maxMp;
            hpBar.Width = 500;
            mpBar.Width = 500;
            //resets number of arrows
            arrows = 20;
            //resets player posiion
            pos.X = graphics.GraphicsDevice.Viewport.Width / 2;
            pos.Y = graphics.GraphicsDevice.Viewport.Height - draw.Height;
            //resets items
            itemDraw.Y = -100;
            //checks what level the player is on
            if (level == 1)
            {
                //sets number of onscreen enenmies and total enemies
                numEnemies = 4;
                totEnemies = 8;
                //changes level background
                background = caveBackground;
                backgroundDraw.Height = graphics.GraphicsDevice.Viewport.Height;
                backgroundDraw.Width = (int)((double)backgroundDraw.Height / caveBackground.Height * caveBackground.Width);
                //intializes all enemies
                for (int i = 1; i <= numEnemies; i++)
                {                    
                    enemies[i] = new enemy(20, 10, 30, 400, 30, 30, 5, batL, batR, 9, 1, false, batKilled, graphics, rand);
                }
                //plays level music
                MediaPlayer.Play(caveMusic);
            }
            else if (level == 2)
            {
                numEnemies = 3;
                totEnemies = 6;
                background = forestBackground;
                backgroundDraw.Height = graphics.GraphicsDevice.Viewport.Height;
                backgroundDraw.Width = (int)((double)backgroundDraw.Height / forestBackground.Height * forestBackground.Width);
                for (int i = 1; i <= numEnemies; i++)
                {
                    enemies[i] = new enemy(40, 20, 25, 800, 40, 60, 10, skeletonL, skeletonR, 1, 15, false, skeletonKilled,graphics, rand);
                }
                MediaPlayer.Play(forestMusic);
            }
            else if (level == 3)
            {
                numEnemies = 2;
                totEnemies = 4;
                background = jungleBackground;
                backgroundDraw.Height = graphics.GraphicsDevice.Viewport.Height;
                backgroundDraw.Width = (int)((double)backgroundDraw.Height / jungleBackground.Height * jungleBackground.Width);
                for (int i = 1; i <= numEnemies; i++)
                {
                    enemies[i] = new enemy(80, 20, 20, 1000, 40, 80, 20, gorgonL, gorgonR, 9, 1, false, gorgonkilled,graphics, rand);
                }
                MediaPlayer.Play(jungleMusic);
            }
            else if (level == 4)
            {
                numEnemies = 1;
                totEnemies = 1;
                background = templeBackground;
                backgroundDraw.Height = graphics.GraphicsDevice.Viewport.Height;
                backgroundDraw.Width = (int)((double)backgroundDraw.Height / templeBackground.Height * templeBackground.Width);
                for (int i = 1; i <= numEnemies; i++)
                {
                    enemies[i] = new enemy(100, 40, 20, 1200, 40, 120, 20, swampMonster, swampMonster, 1, 3, true, bossKilled, graphics, rand);
                    enemies[i].Projectile = gooShot;
                }
                MediaPlayer.Play(bossMusic);
            }
        }
        public void RandomizeItems()
        {
            //1% chance of item spawning every 0.1 seconds
            //checks that item is not already on screen
            if (rand.Next(1, 101) == 1 && itemDraw.Y < 0)
            {
                //sets item height
                itemDraw.Height = 50;
                //randomly selects an item
                int randItem = rand.Next(1, 4);
                //checks what item  was selected
                if (randItem == 1)
                {
                    //sets item image to hp potion
                    item = hpPotion;
                    //makes itme width proportional to height
                    itemDraw.Width = (int)(50.0 / hpPotion.Height * hpPotion.Width);
                }
                else if (randItem == 2)
                {
                    //sets item image to mp potion
                    item = mpPotion;
                    itemDraw.Width = (int)(50.0 / mpPotion.Height * mpPotion.Width);
                }
                else
                {
                    //sets item image to arrow bundle
                    item = arrowBundle;
                    itemDraw.Width = (int)(50.0 / arrowBundle.Height * arrowBundle.Width);
                }
                //randomly sets a horizontal position for item and puts itme on ground
                itemDraw.X = rand.Next(0, graphics.GraphicsDevice.Viewport.Width - itemDraw.Width);
                itemDraw.Y = graphics.GraphicsDevice.Viewport.Height - itemDraw.Height - 20;
            }
        }
    }
}
