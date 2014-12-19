using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Xml;
using System.Xml.Serialization;
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.Units;
using AODGameLibrary.Weapons;
using AODGameLibrary.Effects;
using AODGameLibrary;
using System.IO;
using AODGameLibrary.Texts;


namespace AOD
{
    /// <summary>
    /// 2010年9月12日完成。
    /// 由大地无敌-范若余在2011年12月25日经过艰苦卓绝的修改移植到XNA4.0
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphicsDeviceManager;
        SpriteBatch spriteBatch;

        public GameScene gameScene;
        Settings settings;
        FpsShower fpsShower;

        SpriteFont msyh;

       public  AODMainMenuScene mainMenu;
        BloomComponent bloom;

        Song titleSong;
        Song titleSong2;

        public TextManager textManager;
        public List<int> EnabledStages;
        public int CurrentStage;
        public float  MV;

        public Game1()
        {
            //this.TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 25);


            //IsFixedTimeStep = false;

            graphicsDeviceManager = new GraphicsDeviceManager(this);
            graphicsDeviceManager.IsFullScreen = false;

            graphicsDeviceManager.PreferredBackBufferWidth = 800;
            graphicsDeviceManager.PreferredBackBufferHeight = 600;

            SoundEffect.DistanceScale = GameConsts.Sound3DScale;



            Content.RootDirectory = "Content";

         

            ////写XML数据存为XML文件
            //ParticleEffectType nr = new ParticleEffectType();
            //nr.particleGroups.Add("effects\\ParticleGroupTypes");
            //XmlSerializer x = new XmlSerializer(typeof(ParticleEffectType));
            //FileStream fs = new FileStream("TEST.xml", FileMode.Create);
            //x.Serialize(fs, nr);
            //fs.Close();


        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            try
            {
                CurrentStage = AODSaver.LoadData<int>(GameConsts.GameSaveDirectory + @"\" + "Cs.aod");
                EnabledStages = AODSaver.LoadData<List<int>>(GameConsts.GameSaveDirectory + @"\" + "Es.aod");
            }
            catch
            {
                CurrentStage = 0;
                EnabledStages = new List<int>(10);
                EnabledStages.Add(1);
#if DEBUG
         EnabledStages.Add(2);
         EnabledStages.Add(3);
         EnabledStages.Add(4);
         EnabledStages.Add(5);
         EnabledStages.Add(6);
#endif
                EnabledStages.Add(11);
            }

            if (Directory.Exists(GameConsts.SettingsDirectory) == false)
            {
                Directory.CreateDirectory(GameConsts.SettingsDirectory);
            }
            if (Directory.Exists(GameConsts.GameSaveDirectory) == false)
            {
                Directory.CreateDirectory(GameConsts.GameSaveDirectory);
            }


            mainMenu = new AODMainMenuScene(this);
            mainMenu.StartGame += new StartGameHandler(this.StartGame);
            mainMenu.SettingsChanged += new EventHandler(this.SettingsChanged);
            Components.Add(mainMenu);
            //添加GameComponent
            gameScene = new GameScene(this);
            gameScene.ExitToMainMenu += new EventHandler(gameScene_ExitToMainMenu);
            Components.Add(gameScene);
            gameScene.Enabled = false;
            bloom = new BloomComponent(this);
            Components.Add(bloom);
            bloom.Enabled = false;
            bloom.Visible = false;
            fpsShower = new FpsShower(this);
            Components.Add(fpsShower);
            

            base.Initialize();

        }


        void gameScene_ExitToMainMenu(object sender, EventArgs e)
        {
            MediaPlayer.Volume = MV;
            MediaPlayer.Play(titleSong2);
            MediaPlayer.IsRepeating = true;
            gameScene.Enabled = false;
            gameScene.Visible = false;
            bloom.Enabled = false;
            bloom.Visible = false;
            mainMenu.Reset();
            mainMenu.Enabled = true;
            mainMenu.Visible = true;
            AODSaver.SaveData(EnabledStages, GameConsts.GameSaveDirectory + @"\" + "Es.aod");
            AODSaver.SaveData(CurrentStage, GameConsts.GameSaveDirectory + @"\" + "Cs.aod");
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //添加服务
            Services.AddService(typeof(SpriteBatch), spriteBatch);
            msyh = Content.Load<SpriteFont>("msyh");
            Services.AddService(typeof(SpriteFont), msyh);
            Services.AddService(typeof(GraphicsDeviceManager), graphicsDeviceManager);
            try
            {
                settings = AODSaver.LoadData<Settings>(GameConsts.SettingsFile).Clone();

                // settings = Content.Load<Settings>("DefaultSettings").Clone();
            }
            catch
            {

                settings = Content.Load<Settings>("DefaultSettings").Clone();
            }
            ApplySettingChanges();
            Services.AddService(typeof(Settings), settings);
            

            Content.Load<Texture2D>(@"logo");
            Content.Load<Song>(@"Audio\Marcello_Morgese_-_Space_Travel");
            Content.Load<Song>(@"Audio\Antti_Martikainen_-_Through_Enemy_Lines");
            titleSong = Content.Load<Song>(@"Audio\TitanSlayer_-_Dawning_of_Darkness");
            Content.Load<Song>(@"Audio\Kai_Engel_-_Beneath_The_Stronghold");
            Content.Load<Song>(@"Audio\Marcello_Morgese_-_Sounds_Of_The_Night");
            Content.Load<Song>(@"Audio\Marcello_Morgese_-_Space_Travel");
            titleSong2 = Content.Load<Song>(@"Audio\Moreno_Visintin_-_Mdnel-Inn");
            Content.Load<SoundEffect>(@"Audio\Blast4");
            
            // 先读取部分档案,避免游戏中读取导致速度减慢
            MediaPlayer.Volume = MV;
            MediaPlayer.Play(titleSong);

            MediaPlayer.IsRepeating = true;

            textManager = new TextManager();
            Services.AddService(typeof(TextManager), textManager);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            InputState.UpdateInput(this);
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            if (mainMenu.Enabled == false)
            {

            }

            if (InputState.IsKeyPressed(Keys.PrintScreen))
            {
                //SaveScreenshot();
                textManager.AddText(new AODText(this, "Screenshot Saved", 1, Color.White, new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height) / 2
                    , FadeOutState.Normal, new Vector2(0, -40), true));
            }
            textManager.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            bloom.BeginDraw();

            base.Draw(gameTime);
            gameScene.DrawUI(gameTime);
            textManager.Draw(gameTime);
            


        }
        void StartGame(AODGameLibrary.GamePlay.Stage stage, int? part)
        {
            MediaPlayer.Stop();
            gameScene.Enabled = true;
            gameScene.Visible = true;
            if (settings.SettingFromKeyword("Bloom").SettingValue == 1)
            {

                bloom.Enabled = true;
                bloom.Visible = true;
            }
            else
            {
                bloom.Enabled = false;
                bloom.Visible = false;
            }
            mainMenu.Enabled = false;
            mainMenu.Visible = false;
            gameScene.StartStage(stage, part);
            bloom.Reset();
        }
        void SettingsChanged(Object sender, EventArgs e)
        {
            ApplySettingChanges();
        }

        void ApplySettingChanges()
        {

            MV = ((float)settings.SettingFromKeyword("音乐").SettingValue) * 0.1f;
            MediaPlayer.Volume = MV;
            SoundEffect.MasterVolume = ((float)settings.SettingFromKeyword("音效").SettingValue) * 0.1f;
            float w = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            float h = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            switch (settings.SettingFromKeyword("分辨率").SettingValue)
            {
                case 0:
                    break;
                case 1:
                    w = 1024;
                    h = 768;
                    break;
                case 2:
                    w = 1280;
                    h = 1024;
                    break;
                case 3:
                    w = 1280;
                    h = 960;
                    break;

                case 4:
                    w = 1280;
                    h = 800;
                    break;
                case 5:
                    w = 1152;
                    h = 864;
                    break;
                case 6:
                    w = 1440;
                    h = 900;
                    break;
                case 7:
                    w = 1600;
                    h = 1200;
                    break;
                case 8:
                    w = 1680;
                    h = 1050;
                    break;
                case 9:
                    w = 1920;
                    h = 1200;
                    break;

            }
            graphicsDeviceManager.PreferredBackBufferWidth = (int)w;
            graphicsDeviceManager.PreferredBackBufferHeight = (int)h;
            switch (settings.SettingFromKeyword("全屏幕").SettingValue)
            {
                case 0:
                    graphicsDeviceManager.IsFullScreen = false;
                    break;
                case 1:
                    graphicsDeviceManager.IsFullScreen = true;
                    break;
            }
            switch (settings.SettingFromKeyword("Bloom").SettingValue)
            {
                case 0:
                    break;
                case 1:
                    break;
            }

            try
            {
                graphicsDeviceManager.ApplyChanges();
            }
            catch
            {

            }
            bloom.Reset();
        }
        protected override void OnExiting(object sender, EventArgs args)
        {
            if (Directory.Exists(GameConsts.SettingsDirectory) == false)
            {
                Directory.CreateDirectory(GameConsts.SettingsDirectory);
            }
            if (Directory.Exists(GameConsts.GameSaveDirectory) == false)
            {
                Directory.CreateDirectory(GameConsts.GameSaveDirectory);
            }
       
            AODSaver.SaveData(settings, GameConsts.SettingsFile);
            base.OnExiting(sender, args);
        }
        ///// <summary>
        ///// 截屏 4.0需要重写
        ///// </summary>
        //public void SaveScreenshot()
        //{

        //    if (Directory.Exists("Screenshots") == false)
        //    {
        //        Directory.CreateDirectory("Screenshots");
        //    }
        //    // Find a free name
        //    int number = 0;
        //    string filename = String.Format("ScreenShots\\screenshot{0:00}.jpg", number);
        //    while (System.IO.File.Exists(filename))
        //    {
        //        filename = String.Format("ScreenShots\\screenshot{0:00}.jpg", ++number);
        //    }

        //    // Take the screenshot
        //    GraphicsDevice device = GraphicsDevice;
        //    int w = device.PresentationParameters.BackBufferWidth;
        //    int h = device.PresentationParameters.BackBufferHeight;
        //    using (ResolveTexture2D screenshot = new ResolveTexture2D(device, w, h, 1, SurfaceFormat.Color))
        //    {
        //        // Grab the screenshot
        //        device.ResolveBackBuffer(screenshot);
        //        // Set the alpha to full
        //        Color[] data = new Color[screenshot.Width * screenshot.Height];
        //        screenshot.GetData<Color>(data);
        //        int pos = 0;
        //        foreach (Color c in data)
        //        {
        //            data[pos++] = new Color(c.R, c.G, c.B, 255);
        //        }

        //        // Write to disk
        //        screenshot.SetData<Color>(data);
        //        screenshot.Save(filename, ImageFileFormat.Jpg);
        //        screenshot.Dispose();
        //    }
        //}

    }
}
