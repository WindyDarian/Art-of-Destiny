using System;
using System.Collections.Generic;

using System.Text;
using AODGameLibrary.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using AODGameLibrary.Cameras;
using AODGameLibrary.Weapons;
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.Effects;
using AODGameLibrary.Effects.ParticleShapes;
using Microsoft.Xna.Framework.Graphics;
using AODGameLibrary.Interface;
using AODGameLibrary.AODObjects;
using AODGameLibrary.GamePlay;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using AODGameLibrary.Texts;
using AODGameLibrary.Ambient;
using AODGameLibrary.CollisionChecking;
using AODGameLibrary.GameManagers;
namespace AODGameLibrary
{
    /// <summary>
    /// 表示一个游戏场景，用来管理物件的更新和绘制
    ///由大地无敌-范若余于2009年7月26日创建
    /// </summary>
    public class GameWorld
    {
        private List<GameManager> Managers = new List<GameManager>(3);
        public GameItemManager GameItemManager;
        public List<Unit> units
        {
            get { return GameItemManager.units; }
        }
        public List<Camera> cameras = new List<Camera>(0);
        public List<StateShower> stateShowers = new List<StateShower>(0);
        public List<Bullet> bullets
        {
            get { return GameItemManager.bullets; }
        }
        public List<Missile> missiles
        {
            get { return GameItemManager.missiles; }
        }
        public List<LootItem> lootItems
        {
            get { return GameItemManager.lootItems; }
        }
        List<Timer> timers = new List<Timer>(0);
        public Effect CPUParEffect;
        public Effect GPUParEffect;
        public Camera currentCamera;
        public Camera playerChaseCamera;
        public Camera playerAimCamera;
        public Game game;
        public WorldVars variables = new WorldVars();
        public List<UI> uis = new List<UI>(0);
        public GameMessageBox GameMessageBox;
        /// <summary>
        /// 队列中的等待显示的消息文本
        /// </summary>
        public List<AODText> MessagesInLine = new List<AODText>(0);
        public List<AODText> TooltipMessagesInLine = new List<AODText>(0);
        public List<ParticleEffect> ParticleEffects = new List<ParticleEffect>(0);
        public List<AODText> InstantMessages = new List<AODText>(0);
        public List<AODText> FlyingMessages = new List<AODText>(0);
        public List<AODText> FlyingMessagesInLine = new List<AODText>(0);
        float flyingMessageTime = 2;
        public List<ParticleShape> ParticleShapes = new List<ParticleShape>(0);
        public ContentManager Content;
        /// <summary>
        /// 天空包
        /// </summary>
        public SkySphere SkySphere;
        MarksManager marksManager;
        ScreenEffectManager screenEffectManager;
        /// <summary>
        /// 屏幕效果管理器
        /// </summary>
        public ScreenEffectManager ScreenEffectManager
        {
            get { return screenEffectManager; }
        }
        Stage currentStage;
        /// <summary>
        /// 得到或设置当前关卡
        /// </summary>
        public Stage CurrentStage
        {
            get
            {
                return currentStage;
            }
            set
            {
                currentStage = value;
            }
        }
        public SpriteFont gameFont;
        /// <summary>
        /// 得到或设置游戏内基础字体
        /// </summary>
        public SpriteFont GameFont
        {
            get
            {
                return gameFont;
            }
            set
            {
                gameFont = value;
            }
        }
        /// <summary>
        /// 得到游戏的变量
        /// </summary>
        public WorldVars Variables
        {
            get
            {
                return variables;
            }
        }

        

        public SpriteBatch spriteBatch;
        /// <summary>
        /// 弹药碰撞检测等消耗CPU的更新执行的时间间隔
        /// </summary>
        public float sUpdateTime = 0.15f;
        /// <summary>
        /// 显示信息的位置
        /// </summary>
        public Vector2 MessagePosition = new Vector2(125, 250);
        /// <summary>
        /// 显示提示信息的位置
        /// </summary>
        public Vector2 TooltipMessagePosition = new Vector2(125, 200);
        /// <summary>
        /// 上次SUpdate时间每次SUpdate后归零
        /// </summary>
        public float lastSUpdate = 0;
        /// <summary>
        /// 上次SUpdate后Update次数,避免慢的电脑上一直SUpdate
        /// </summary>
        public int lastSUpdateT = 0;
        /// <summary>
        /// 是否呈现玩家摄像机
        /// </summary>
        public bool playerCameraOn = true;

        public Unit PlayerLockedTarget;

        public bool InvertMouseY = false;
        public bool InvertPadY = false;

        List<Timer> preAddTimers = new List<Timer>(0);//等待添加的Timer
        bool hideUI = false;
        /// <summary>
        /// 是否隐藏UI
        /// </summary>
        public bool HideUI
        {
            get { return hideUI; }
            set { hideUI = value; }
        }
        Song songInqueue;
        float fadeoutTime;
        float fadeotTimeleft;
        bool changingmusic;
        string lootTipInf = "";
        bool reseting = false;
        bool Reseting
        {
            get
            {
                return reseting;
            }
            set
            {
                reseting = value;
            }
        }
        bool paused = false;
        /// <summary>
        /// 游戏是否暂停
        /// </summary>
        public bool Paused
        {
            get { return paused; }
            set
            {
                if (value != paused)
                {

                    paused = value;
                    Event_PauseStateChanged(this, new EventArgs());
                }
            }
        }

       public float MVolume=1;
       float mv = 1;
       public float MusicVolume
       {
           get
           {
               return mv;
           }
           set
           {
               mv = value;
               MediaPlayer.Volume = MVolume * value;
           }
       }

        /// <summary>
        /// 暂停状态改变时所触发的事件
        /// </summary>
        public event EventHandler Event_PauseStateChanged;
        public GameWorld(Game game,Stage stage)
        {
            this.game = game;
            this.Content = game.Content;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            Initialize();
            //LoadStage(stage);
            
        }
        /// <summary>
        /// 从特定片段开始游戏世界
        /// </summary>
        /// <param name="game"></param>
        /// <param name="stage"></param>
        /// <param name="part"></param>
        public GameWorld(Game game, Stage stage, int part)
        {
            this.game = game;
            this.Content = game.Content;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            Initialize();
            //LoadStage(stage,part);
        }
        /// <summary>
        /// 为退出该游戏世界作准备
        /// </summary>
        public void Unload()
        {
            MediaPlayer.Stop();
        }
        /// <summary>
        /// 读取关卡（从头开始）
        /// </summary>
        public void LoadStage(Stage stage)
        {

            this.currentStage = stage.Clone(this);
            currentStage.StageFailed += new EventHandler(currentStage_StageFailed);
            currentStage.StageWon += new EventHandler(currentStage_StageWon);
            Event_StageLoaded(this, EventArgs.Empty);
            currentStage.Initialize();
            currentStage.LoadContent();
            currentStage.StartNew();

        }
        /// <summary>
        /// 读取关卡（从特定片段开始）
        /// </summary>
        public void LoadStage(Stage stage, int part)
        {
            this.currentStage = stage.Clone(this);
            currentStage.StageFailed += new EventHandler(currentStage_StageFailed);
            currentStage.StageWon += new EventHandler(currentStage_StageWon);
            Event_StageLoaded(this, EventArgs.Empty);
            currentStage.Initialize();
            currentStage.LoadContent();
            currentStage.StartFromStagePart(part);
        }
        public event EventHandler Event_StageLoaded;
        void currentStage_StageWon(object sender, EventArgs e)
        {
            Event_StageWon(sender, e);
        }
        public event EventHandler Event_StageWon;

        void currentStage_StageFailed(object sender, EventArgs e)
        {
            Event_StageFailed(sender, e);
        }
        public event EventHandler Event_StageFailed;

        //void Reset()
        //{
        //    units = new List<Unit>();
        //    cameras = new List<Camera>();
        //    stateShowers = new List<StateShower>();
        //    bullets = new List<Bullet>();
        //    missiles = new List<Missile>();
        //    currentCamera = null;
        //    uis = new List<UI>();
        //    skySphere = null;
        //    Reseting = false;
        //    variables = new WorldVars();
        //    Initialize();

        //}
        public void Initialize()
        {
            
            CPUParEffect = game.Content.Load<Effect>(@"effects\Particle");
            GPUParEffect = game.Content.Load<Effect>(@"effects\ParticleEffect");
            GameFont = game.Content.Load<SpriteFont>(@"msyh");
            
            Camera a = new Camera(this.game);
            playerAimCamera = a;
            playerChaseCamera = a;
            currentCamera = a;
            GameMessageBox = new GameMessageBox(this);
            cameras.Add(a);
            #region 重置UI
           
            this.AddUI(new AODBar(this));
            this.AddUI(new AODWeaponUI(this));
            this.AddUI(new AODSkillCast(this));
            this.AddUI(new TargetInf(this));
            this.AddUI(new AODSpeed(this));
            this.AddUI(new AimPoint(this));
           
            
            marksManager = new MarksManager(this);
            this.AddUI(marksManager);
            #endregion
            screenEffectManager = new ScreenEffectManager(this);
            MessagePosition = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height - 100);
            TooltipMessagePosition = new Vector2(game.GraphicsDevice.Viewport.Width / 2, 200);
            
            
            GameItemManager = new GameItemManager(this);
            Managers.Add(GameItemManager);

            Settings st = (Settings)game.Services.GetService(typeof(Settings));
            switch (st.SettingFromKeyword("invertMouseY").SettingValue)
            {
                case 0:
                    InvertMouseY = false;
                    break;
                case 1:
                    InvertMouseY = true;
                    break;
                default:
                    break;
            }

            switch (st.SettingFromKeyword("invertPadY").SettingValue)
            {
                case 0:
                    InvertPadY = false;
                    break;
                case 1:
                    InvertPadY = true;
                    break;
                default:
                    break;
            }
           
        }

        public void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            #region 音乐管理
            if (changingmusic)
            {
                if (fadeoutTime != 0)
                {
                    MusicVolume = MathHelper.Clamp(fadeotTimeleft / fadeoutTime, 0, 1);
                }
                fadeotTimeleft -= elapsedTime;
                if (fadeotTimeleft <= 0)
                {
                    MusicVolume = 1;
                    changingmusic = false;
                    MediaPlayer.Stop();
                    if (songInqueue != null)
                    {

                        MediaPlayer.Play(songInqueue);
                    }
                    songInqueue = null;
                }
            }
            #endregion


            #region 游戏管理
            if (Paused == false)
            {
                GameMessageBox.Update(gameTime);

                bool textShowing = false;
                if (GameMessageBox.Visible)
                {
                    textShowing = true;
                }

                if (textShowing == false)
                {
                    #region 更新计时器
                    foreach (Timer t in timers)
                    {
                        if (t != null)
                        {

                            t.Update(gameTime);
                            if (t.IsEnd && (t.Rung == false))
                            {
                                t.Rung = true;
                                if (currentStage.Over == false)
                                {

                                    currentStage.Event_TimerRing(t);
                                }

                            }
                        }

                    }
                    foreach (Timer t in preAddTimers)
                    {
                        if (t != null)
                        {
                            timers.Add(t);
                        }
                    }
                    preAddTimers = new List<Timer>(0);
                    #endregion

                    foreach (GameManager manager in Managers)
                    {
                        manager.Update(gameTime);
                    }


                    #region 更新粒子效果
                    List<ParticleEffect> removingEffects = new List<ParticleEffect>(5);
                    foreach (ParticleEffect pe in ParticleEffects)
                    {
                        if (pe != null)
                        {
                            if (!pe.IsDead)
                            {

                                pe.Update(gameTime);
                            }
                            else
                            {
                                removingEffects.Add(pe);
                            }

                        }
                    }
                    foreach (ParticleEffect pe in removingEffects)
                    {
                        if (pe != null)
                        {
                            ParticleEffects.Remove(pe);
                        }
                    }
                    List<ParticleShape> removingShapes = new List<ParticleShape>(5);
                    foreach (ParticleShape pS in ParticleShapes)
                    {

                        pS.Update(gameTime);
                        if (pS.IsDead == true)
                        {
                            removingShapes.Add(pS);
                        }
                    }
                    foreach (ParticleShape ps in removingShapes)
                    {
                        ParticleShapes.Remove(ps);
                    }
                    #endregion

                    #region 更新游戏中时间
                    Variables.TotalGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    #endregion

                    #region 更新浮动文字
                    flyingMessageTime += elapsedTime;
                    if (flyingMessageTime >= 2)
                    {
                        flyingMessageTime -= 2;

                        if (FlyingMessagesInLine.Count > 0)
                        {
                            FlyingMessages.Add(FlyingMessagesInLine[0]);
                            FlyingMessagesInLine.Remove(FlyingMessagesInLine[0]);
                        }
                    }
                    List<AODText> rfm = new List<AODText>(1);
                    foreach (AODText f in FlyingMessages)
                    {
                        f.Update(gameTime);
                        if (f.IsDead)
                        {
                            rfm.Add(f);
                        }
                    }
                    foreach (AODText f in rfm)
                    {
                        FlyingMessages.Remove(f);

                    }
                    #endregion

                    #region 进行SUpdate
                    lastSUpdate += elapsedTime;
                    lastSUpdateT += 1;
                    if (lastSUpdate >= sUpdateTime && lastSUpdateT >= 4)
                    {
                        SUpdate();
                        lastSUpdate = 0;
                        lastSUpdateT = 0;
                    }

                    #endregion
                }



            }
            #endregion

            #region 暂停管理
            if (InputState.IsKeyPressed(Keys.Escape)||InputState.IsPadButtonPressed(Buttons.Start))
            {
                if (Paused == false)
                {
                    Paused = true;
                }
                else
                {
                    if (currentStage.Over == false)
                    {
                        Paused = false;
                    }
                }
            }

            #endregion
            #region 更新消息队列中的文字显示
            if (MessagesInLine.Count > 0)
            {
                if (MessagesInLine[0] != null && MessagesInLine[0].IsDead == false)
                {

                    MessagesInLine[0].Update(gameTime);
                }
                else
                {
                    MessagesInLine.Remove(MessagesInLine[0]);
                }

            }
            if (TooltipMessagesInLine.Count > 0)
            {
                if (TooltipMessagesInLine[0] != null && TooltipMessagesInLine[0].IsDead == false)
                {

                    TooltipMessagesInLine[0].Update(gameTime);
                }
                else
                {
                    TooltipMessagesInLine.Remove(TooltipMessagesInLine[0]);
                }

            }
            {
                if (InstantMessages.Count > 0)
                {
                    List<AODText> removingMessages = new List<AODText>(5);
                    foreach (AODText msg in InstantMessages)
                    {
                        if (msg != null)
                        {

                            msg.Update(gameTime);
                            if (msg.IsDead)
                            {
                                removingMessages.Add(msg);
                            }
                        }
                    }
                    foreach (AODText msg in removingMessages)
                    {
                        if (msg != null)
                        {

                            InstantMessages.Remove(msg);
                        }
                    }
                }

            }

            #endregion


        }
        /// <summary>
        /// 进行超级更新
        /// </summary>
        /// <param name="gameTime"></param>
        void SUpdate()
        {
   
            if (Variables.Player.lootingItem == null)
            {

                lootTipInf = "";
            }
            foreach (GameManager manager in Managers)
            {
                manager.SUpdate();
            }


            
            #region 更新关卡事件
            if (currentStage != null )
            {
                if (currentStage.Failed != true && currentStage.Won != true)
                {

                    currentStage.Touch();
                }
                currentStage.Check();
            }
            #endregion
        }
        public void DrawGameScene(GameTime gameTime)
        {

            CameraControl();

            #region 更新相机
            foreach (Camera c in cameras)
            {
                if (c != null)
                {
                    c.Update(gameTime);
                }
            }
            if (playerAimCamera != null)
            {
                playerAimCamera.Update(gameTime);
            }
            if (playerChaseCamera!= null )
            {
                playerChaseCamera.Update(gameTime);   
            }
            #endregion
            //game.GraphicsDevice.RenderState.AlphaBlendEnable = false;
            //game.GraphicsDevice.RenderState.AlphaTestEnable = false;

            //game.GraphicsDevice.RenderState.DepthBufferEnable = false;
            //game.GraphicsDevice.RenderState.DepthBufferWriteEnable = false;



            game.GraphicsDevice.DepthStencilState = DepthStencilState.None;



            #region 绘出天空包
            if (SkySphere != null)
            {

                SkySphere.Draw(currentCamera);
            }
            
            #endregion
            game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            foreach (GameManager manager in Managers)
            {
                manager.DrawGameScene(gameTime,currentCamera);
            }

            #region 绘出自由粒子效果
            foreach (ParticleEffect pe in ParticleEffects)
            {
                pe.Draw(gameTime, currentCamera);
            }
            foreach (ParticleShape pS in ParticleShapes)
            {
                pS.Draw(gameTime, currentCamera);
            }
            #endregion

            screenEffectManager.Draw(gameTime);


        }
        void CameraControl()
        {
            if (playerCameraOn)
            {
                if ((InputState.IsMouseButtonDown(MouseButton.RightButton)||InputState.IsPadButtonDown(Buttons.LeftTrigger))&& variables.Player.isPlayerControlling == true)
                {

                    currentCamera = playerAimCamera;
                    playerChaseCamera.Sync(playerAimCamera);
                    
                }
                else
                {
                    currentCamera = playerChaseCamera;
                }
            }
        }
        public void DrawUI(GameTime gameTime)
        {
            game.GraphicsDevice.BlendState = BlendState.NonPremultiplied;
            if (Variables.Player.isPlayerControlling && hideUI == false)
            {
                foreach (UI u in uis)
                {
                    if (u != null)
                    {
                        u.Update(gameTime);
                        u.Draw(gameTime);
                    }
                }
            }
            #region 状态显示
            foreach (StateShower s in stateShowers)
            {
                if (s != null)
                {
                    s.Update(gameTime);
                    s.Draw(gameTime);
                }
            }


            #endregion

            #region 消息文字显示
            
            
            if (MessagesInLine.Count > 0)
            {
                if (MessagesInLine[0] != null && MessagesInLine[0].IsDead == false)
                {

                    MessagesInLine[0].Draw(gameTime);
                }

            }
            if (TooltipMessagesInLine.Count > 0)
            {
                if (TooltipMessagesInLine[0] != null && TooltipMessagesInLine[0].IsDead == false)
                {

                    TooltipMessagesInLine[0].Draw(gameTime);
                }

            }
            if (InstantMessages.Count > 0)
            {
                foreach (AODText msg in InstantMessages)
                {
                    msg.Draw(gameTime);
                }
            }
            if (FlyingMessages.Count > 0)
            {

                foreach (AODText msg in FlyingMessages)
                {
                    msg.Draw(gameTime);
                }
            }
            if (lootTipInf != "")
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(gameFont, lootTipInf, GameConsts.LootTipPosition, Color.White);
                spriteBatch.End();
            }
            GameMessageBox.Draw(gameTime);
            #endregion

            foreach (GameManager manager in Managers)
            {
                manager.DrawUI(gameTime);
            }
        }
        public void AddNewBullet(Bullet bullet)
        {
            bullets.Add(bullet);
        }
        public void AddNewMissile(Missile missile)
        {
            missiles.Add(missile);
        }
        public void AddNewLoot(LootItem loot)
        {
            lootItems.Add(loot);
        }
 
        public void AddGameMessage(string message, Color color,float lifeTime)
        {
            AODText t = AODText.CreateText(message, lifeTime, color, MessagePosition, this, FadeOutState.Normal);
            t.centerize = false;
            GameMessageBox.AddText(t);
            GameMessageBox.Visible = true;
            //MessagesInLine.Add(AODText.CreateText(message, lifeTime, color, MessagePosition, this,FadeOutState.Normal));
        }
        public void AddRealtimeGameMessage(string message, Color color, float lifeTime)
        {
           
            MessagesInLine.Add(AODText.CreateText(message, lifeTime, color, MessagePosition, this,FadeOutState.Normal));
        }
        public void ShowTooltipMessage(string message, Color color, float lifeTime)
        {
            TooltipMessagesInLine.Add(AODText.CreateText(message, lifeTime, color, TooltipMessagePosition, this, FadeOutState.Normal));
        }
        public void ShowInstantMessage(string message, Vector2 position, Vector2 velocity, Color color, float lifeTime)
        {
            InstantMessages.Add(AODText.CreateText(message, lifeTime, color, position, this, FadeOutState.Normal, velocity));
        }
        /// <summary>
        /// 创建一个计时器
        /// </summary>
        /// <param name="t">计时器的结束时间</param>
        /// <returns></returns>
        public Timer CreateTimer(float t)
        {
            Timer tmr = new Timer(t);
            preAddTimers.Add(tmr);
            return tmr;
        }
        /// <summary>
        /// 使所有消息消失
        /// </summary>
        public void ClearMessages()
        {
            MessagesInLine = new List<AODText>(0);
        }
        public void PlayMusic(string songAssetName,bool isRepeating,float fadeTime)
        {
         
            songInqueue = Content.Load<Song>(songAssetName);
            MediaPlayer.IsRepeating = isRepeating;
            if (changingmusic != true)
            {
                MusicVolume = 1;
                fadeoutTime = fadeTime;
                fadeotTimeleft = fadeTime;
                changingmusic = true;
            }
            
     

        }
        public void StopMusic()
        {

            songInqueue = null ;
            if (changingmusic != true)
            {
                MusicVolume = 1;
                fadeoutTime = 3;
                fadeotTimeleft = 3;
                changingmusic = true;
            }
        }
        public void Play3DSound(SoundEffect e, Vector3 position)
        {
            SoundEffectInstance i = e.CreateInstance();
            Play3DSound(i, position);
       
        }
        public void Play3DSound(SoundEffectInstance e, Vector3 position)
        {
            
            AudioListener al = new AudioListener();

            al.Forward = Vector3.Normalize(currentCamera.LookAt - currentCamera.Position);
            al.Up = Vector3.Normalize(currentCamera.ObjUp);
            al.Position = currentCamera.Position;
            AudioEmitter ae = new AudioEmitter();
            ae.Position = (position - currentCamera.Position)  + currentCamera.Position;
            //ae.Position = position;
           
            e.Apply3D(al, ae);
            //if (e.State == SoundState.Stopped)
            //{
            if( Vector3.Distance(currentCamera.Position,position) <= GameConsts.SoundDistance)
            {
               
                e.Play();
            }
            //}
        }

        /// <summary>
        /// 创造一个拥有无尽弹药且不会拾取东西的敌对或友好单位
        /// </summary>
        /// <param name="unitType"></param>
        /// <param name="group"></param>
        /// <param name="position"></param>
        public Unit CreateNPCUnit(UnitType unitType, int group, Vector3 position)
        {
            Unit u =
            CreateUnit(unitType, group, position, false, true);
            return u;
        }
        /// <summary>
        /// 创造一个弹药有限且可以拾取东西的玩家单位
        /// </summary>
        /// <param name="unitType"></param>
        /// <param name="group"></param>
        /// <param name="position"></param>
        public Unit CreatePlayerUnit(UnitType unitType, int group, Vector3 position)
        {
            Unit u =
            CreateUnit(unitType, group, position, true, false);
            return u;
        }
        /// <summary>
        /// 创造一个单位
        /// </summary>
        /// <param name="unitType"></param>
        /// <param name="group"></param>
        /// <param name="position"></param>
        /// <param name="isLootable"></param>
        /// <param name="endlessBullets"></param>
        public Unit CreateUnit(UnitType unitType, int group, Vector3 position, bool isLootable, bool endlessBullets)
        {
            Unit u = Unit.Create(this, unitType, group, position,isLootable,endlessBullets);

            units.Add(u);
            Variables.LastCreatedUnit = u;
            return u;
        }
        public void CreateDecoration(DecorationType dt, Vector3 positon)
        {
            Decoration d = new Decoration(this, dt, positon);
            GameItemManager.decorations.Add(d);
            Variables.LastCreatedDecoration = d;
        }
        public void CreateDecoration(DecorationType dt, Vector3 positon,float scale,Vector3 rotation)
        {
            Decoration d = new Decoration(this, dt, positon, scale, rotation);
            GameItemManager.decorations.Add(d);
            Variables.LastCreatedDecoration = d;
        }
        public void CreateLoot(LootSettings ls, Vector3 position)
        {
            AddNewLoot(new LootItem(position, ls, this)); ;
        }
        /// <summary>
        /// 将一个单位设为玩家
        /// </summary>
        /// <param name="unit"></param>
        public void SetPlayer(Unit unit)
        {
            if (Variables.Player != null)
            {

                Variables.Player.isPlayerControlling = false;
            }
            unit.isPlayerControlling = true;
            Variables.Player = unit;
            playerChaseCamera = Camera.CreateAODChaseCamera(this, unit);
            if (playerCameraOn)
            {

                currentCamera = playerChaseCamera;
            }
            playerAimCamera = Camera.CreateNormalFollowCamera(this, unit);

        }
        public void AddUI(UI ui)
        {
            uis.Add(ui);
        }
        public void AddParticleEffect(ParticleEffect pe)
        {
            ParticleEffects.Add(pe);
        }
        public void RemoveParticleEffect(ParticleEffect pe)
        {
            if (ParticleEffects.Contains(pe))
            {
                ParticleEffects.Remove(pe);
            }
        }
        public void ShowLootTipInf(Unit u, LootItem loot)
        {
            if (u== Variables.Player)
            {
                string b = "";
                string c = "";
                switch (loot.LootSettings.LootType)
                {
                    case LootType.Weapon:
                        {
                            b= "main weapon";
                            WeaponType source = Content.Load<WeaponType>(loot.LootSettings.ObjectName);
                            c = source.weaponName;
                            lootTipInf = "Press E to destroy current " + b + " and equip " + c;
                        }
                        break;
                    case LootType.MissileWeapon:
                        {
                            b= "side weapon";
                            MissileWeaponType source = Content.Load<MissileWeaponType>(loot.LootSettings.ObjectName);
                            c = source.name;
                            lootTipInf = "Press E to destroy current " + b + " and equip " + c;
                        }
                        break;
                    case LootType.SkillItem:
                        {
                            b = "module";
                            Skill source = Content.Load<Skill>(loot.LootSettings.ObjectName);
                            c = source.SkillName;
                            lootTipInf = "Press E to destroy current " + b + " and equip " + c;
                        }
                        break;
                    case LootType.QuestItem:
                        break;
                    default:
                        break;
                }

              
            }
        }
        public void ShowLootWeapon(Unit u, LootItem loot)
        {
            if (u == Variables.Player)
            {
                string s = "得到";
                switch (loot.LootSettings.LootType)
                {
                    case LootType.Weapon:
                        {
                            WeaponType source = Content.Load<WeaponType>(loot.LootSettings.ObjectName);
                            s = s + source.weaponName;
                        }
                        break;
                    case LootType.MissileWeapon:
                        {
                            MissileWeaponType source = Content.Load<MissileWeaponType>(loot.LootSettings.ObjectName);
                            s = s + source.name;
                        }
                        break;
                    default:
                        break;
                }
                Vector2 v = new Vector2((float)AODGameLibrary.Helpers.RandomHelper.Random.NextDouble() - 0.5f, (float)AODGameLibrary.Helpers.RandomHelper.Random.NextDouble() - 0.5f);
                if (v == Vector2.Zero)
                    v = Vector2.UnitX;
                v = Vector2.Normalize(v) * GameConsts.LootInfSpeed;

                ShowInstantMessage(s, GameConsts.LootInfPosition, v, Color.White, 2);
            }
        }
        public void ShowLoot(Unit u, LootItem loot)
        {
            if (u == Variables.Player)
            {
                string s = "得到";
                switch (loot.LootSettings.LootType)
                {
                    case LootType.Weapon:
                        {
                            WeaponType source = Content.Load<WeaponType>(loot.LootSettings.ObjectName);
                            s = s + source.ammoName + "x" + loot.BulletNum;
                        }
                        break;
                    case LootType.MissileWeapon:
                        {
                            MissileWeaponType source = Content.Load<MissileWeaponType>(loot.LootSettings.ObjectName);
                            s = s + source.name + "x" + loot.BulletNum;
                        }
                        break;
                    case LootType.SkillItem:
                        {
                            Skill source = Content.Load<Skill>(loot.LootSettings.ObjectName);
                            s = s + "特殊装置" + source.SkillName;
                        }
                        break;
                    case LootType.QuestItem:
                        break;
                    default:
                        break;
                }
         
                Vector2 v = new Vector2((float)AODGameLibrary.Helpers.RandomHelper.Random.NextDouble() - 0.5f, (float)AODGameLibrary.Helpers.RandomHelper.Random.NextDouble() - 0.5f);
                if (v == Vector2.Zero)
                    v = Vector2.UnitX;
                v = Vector2.Normalize(v) * GameConsts.LootInfSpeed;

                ShowInstantMessage(s, GameConsts.LootInfPosition, v, Color.White, 2);
            }
        }

        public int CountAliveUnitsInUnitGroup(int group)
        {
            int c = 0;
            foreach (Unit u in units)
            {
                if (u!= null )
                {
                    if (u.Group== group && u.Dead == false)
                    {
                        c += 1;
                    }
                }
            }
            return c;
        }
        public List<Unit> AliveUnitsInUnitGroup(int group)
        {
            List<Unit> lu = new List<Unit>(0);
            foreach (Unit u in units)
            {
                if (u!=null )
                {
                    if (u.Group == group && u.Dead==false)
                    {
                        lu.Add(u);
                    }
                }
            }
            return lu;
        }

        public void AddParticleShape(ParticleShape pS)
        {
            ParticleShapes.Add(pS);
            pS.Initialize(this);
        }
        /// <summary>
        /// 将一个3D坐标经过当前camera观察矩阵和投影矩阵转换
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector3 ViewProTransform(Vector3 position)
        {
            return game.GraphicsDevice.Viewport.Project(position, currentCamera.Projection, currentCamera.View, Matrix.Identity);
        }
        public Mark AddPositionMark(Vector3 position)
        {
            return marksManager.AddPositionMark(position);
        }
        public void SetSkySphere(string texture)
        {
            SkySphere = new SkySphere(@"models\skySphere", texture, this, Vector3.Zero);
        }
        public void SetSkySphere(SkySphere sky)
        {
            SkySphere = sky;
        }
        public void AddFlyingMessage(String message, Color color)
        {
            
            FlyingMessagesInLine.Add(AODText.CreateText(message, 15.5f, color, new Vector2((float)game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height + 15), this, FadeOutState.None, new Vector2(0, -(float)game.GraphicsDevice.Viewport.Height / 15)));
        }
        public void ClearLoots()
        {
            lootItems.Clear();
        }
        public void SetCamera(Camera camera)
        {//未使用相机的释放有一定问题
            if (cameras.Contains(camera) == false)
            {
                cameras.Add(camera);
         
            }
            playerCameraOn = false;
            currentCamera = camera;
        }
        public void ReleaseCamera()
        {
            playerCameraOn = true;
        }
        /// <summary>
        /// 开启电影模式
        /// </summary>
        public void MovieModelStart()
        {
            if (currentStage.Player!= null)
            {
                currentStage.Player.isPlayerControlling = false;
            }

        }
        public void MovieModelEnd()
        {
            if (currentStage.Player!= null)
            {
                  currentStage.Player.isPlayerControlling = true;
            }
        }
 
        //public Texture2D LoadTexture(string s)
        //{
        //    foreach (ParticleTexture tx in textures)
        //    {
        //        if (tx != null)
        //        {
        //            if (s == tx.name)
        //            {
        //                return tx.texture;
        //            }
        //        }
        //    }
        //    return null;
        //}
    }
}
