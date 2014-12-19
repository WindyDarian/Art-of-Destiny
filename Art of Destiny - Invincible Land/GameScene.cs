using System;
using System.Collections.Generic;

using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using AODGameLibrary;
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.Cameras;
using AODGameLibrary.Units;
using AODGameLibrary.Weapons;
using AODGameLibrary.Effects;
using AODGameLibrary.Models;
using AODGameLibrary.Interface;
using AODGameLibrary.AODObjects;
using AODGameLibrary.GamePlay;
using AODGameLibrary.Menu;
using Stages;



namespace AOD
{
    /// <summary>
    /// 这个GameComponent管理命运艺术的游戏画面--大地无敌
    /// </summary>
    public class GameScene : Microsoft.Xna.Framework.DrawableGameComponent
    {
        GameWorld gameWorld;
        Menu pauseMenu;
        SpriteBatch spriteBatch;
        SpriteFont msyh;
        Texture2D blank;
        Texture2D pauseTexture;
        Texture2D hyr;
        /// <summary>
        /// 退回主菜单
        /// </summary>
        public event EventHandler ExitToMainMenu;
        //bool end = false;
        //string winner;
        float endTimePast = 0;
        bool gameFailTimeCounting = false;
        bool gameWinTimeCounting = false;
        AODGameLibrary2.SavedUnit MissionPlayerSaver = new AODGameLibrary2.SavedUnit();
        AODGameLibrary2.SavedUnit PlayerSaver = new AODGameLibrary2.SavedUnit();

        public GameScene(Game game)
            : base(game)
        {
            
            // TODO: Construct any child components here
            blank = Game.Content.Load<Texture2D>(@"blank");
            pauseTexture = Game.Content.Load<Texture2D>(@"Textures\PauseMenu");
            hyr = Game.Content.Load<Texture2D>(@"Textures\hyr");
            Reset();
            ExitToMainMenu += new EventHandler(GameScene_ExitToMainMenu);
            try
            {
                 
               PlayerSaver = AODSaver.LoadData<AODGameLibrary2.SavedUnit>(GameConsts.GameSaveDirectory + @"\" + "Player.aod");
               ((Game1)Game).textManager.AddText(new AODGameLibrary.Texts.AODText(Game, "战机信息已读取", 5f, Color.Gold, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), AODGameLibrary.Texts.FadeOutState.Normal, AODGameLibrary.Helpers.RandomHelper.RandomDirection2() * 20, true));
             
            }
            catch
            {

            }
    
        }

        void GameScene_ExitToMainMenu(object sender, EventArgs e)
        {
            MissionPlayerSaver.Clear();
        }
        void Reset()
        {
            if (gameWorld!= null)
            {
                gameWorld.Unload();
            }
            gameWorld = null;
            pauseMenu = new Menu(Game);
            pauseMenu.Items.Add(new MenuButton("Continue", true, "继续"));
            pauseMenu.Position = new Vector2(100, 100);
            List<string> s = new List<string>(2);
            s.Add("否");
            s.Add("是");
            pauseMenu.Items.Add(new MenuButton("Restart from checkpoint", true, "检查点", s, 0));
            pauseMenu.Items.Add(new MenuButton("Return to title", true, "主菜单", s, 0));
            pauseMenu.EscIndex = 0;
            pauseMenu.Click += new EventHandler(pauseMenu_Click);
            endTimePast = 0;
            gameFailTimeCounting = false;
            gameWinTimeCounting = false;
            endTimePast = 0;
            
        }
        void pauseMenu_Click(object sender, EventArgs e)
        {
            switch (pauseMenu.SelectedKeyword)
            {
                case "继续":
                    pauseMenu.Close();
                    gameWorld.Paused = false;

                    break;
                case "检查点":
                    if (pauseMenu.SelectedButton.SelectedIndex == 1)
                    {
                        
                        RollBackToLatestCheckpoint();
                    }
                    break;
                case "主菜单":
                    if (pauseMenu.SelectedButton.SelectedIndex == 1)
                    {
                        pauseMenu.Close();

                        GameWorldExit();
                    }
                    break;
                default:
                    break;
            }
        }
        void GameWorldExit()
        {
            Reset();
            ExitToMainMenu(this, new EventArgs());
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
           
            // TODO: Add your initialization code here
            

            
            msyh = Game.Content.Load<SpriteFont>("msyh");

            

            //gameWorld = new GameWorld(Game, new TestStage());
            

            base.Initialize();

        }
        /// <summary>
        /// 重新开始默认关卡
        /// </summary>
        public void StartStage()
        {

            LoadGameWorld(new Stage1(), null);
            //gameWorld = new GameWorld(game, new Loot());
        }

     
        /// <summary>
        /// 重新开始特定关卡
        /// </summary>
        /// <param name="stage"></param>
        public void StartStage(Stage stage)
        {
            LoadGameWorld(stage, null);
            
        }
        /// <summary>
        /// 开始特定关卡
        /// </summary>
        /// <param name="stage"></param>
        /// <param name="part"></param>
        public void StartStage(Stage stage,int? part)
        {
            if (stage == null)
            {
                StartStage();
            }
            else
            {

                LoadGameWorld(stage, part);
                if (part!= null)
                {
                    
                }
            }
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (gameFailTimeCounting)
            {
                endTimePast += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (endTimePast >= GameConsts.FailEndTime)
                {
                    gameFailTimeCounting = false;
                    gameWorld.Paused = true;
                }
            }
            else if (gameWinTimeCounting)
            {
                endTimePast += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (endTimePast >= GameConsts.VictoryEndTime)
                {
                    gameWinTimeCounting = false;
                    ExitToMainMenu(this, new EventArgs());
                    if (gameWorld.CurrentStage is Stages.Stage6 == false)
                    ((Game1)Game).mainMenu.StartStage(((Game1)Game).CurrentStage);
                }
            }
            if (gameWorld != null)
            {

                gameWorld.Update(gameTime);
            }
            pauseMenu.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            if (gameWorld != null)
            {
                
                gameWorld.DrawGameScene(gameTime);
            }
            if (endTimePast > 0.1f)
            {

                Color c = Color.White;
                if (gameWinTimeCounting)
                {

                    c.A = (byte)(MathHelper.Clamp(endTimePast / GameConsts.VictoryEndTime, 0, 1) * 85);
                }
                else
                {

                    c.A = (byte)(MathHelper.Clamp(endTimePast / GameConsts.FailEndTime, 0, 1) * 85);
                }

                spriteBatch.Begin();
          
                spriteBatch.Draw(blank, GetFullScreenRectangle(), c);
          
                spriteBatch.End();
            }
          
            base.Draw(gameTime);
        }
        /// <summary>
        /// 绘制UI,由大地无敌-范若余加入的方法
        /// </summary>
        /// <param name="gameTime"></param>
        public void DrawUI(GameTime gameTime)
        {
            if (gameWorld != null && this.Visible)
            {

                spriteBatch.Begin();
                if (gameWorld.CurrentStage.Over == false)
                {

                    gameWorld.DrawUI(gameTime);
                    if (gameWorld.Paused)
                    {

                        Color c = Color.Gray;
                        c.A = 100;
                        spriteBatch.Draw(blank, GetFullScreenRectangle(), c);
                    }
                }
                if (pauseMenu.Visable)
                {
                  
                
                   

                    int h = GraphicsDevice.Viewport.Height;
                    int w = GraphicsDevice.Viewport.Width;

                    int bw = GraphicsDevice.Viewport.Height * pauseTexture.Width / pauseTexture.Height;

                    spriteBatch.Draw(pauseTexture, new Rectangle(0, 0, bw, h), Color.White);
                  
                     
                }
                if (gameWorld.CurrentStage.Failed)
                {
                    Color c = Color.White;
                    c.A = (byte)(MathHelper.Clamp(endTimePast / GameConsts.FailEndTime, 0, 1) * 205);
                    spriteBatch.Draw(hyr, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2)
                       , null, c, 0, new Vector2(hyr.Width, hyr.Height) / 2, 1, SpriteEffects.None, 0.3f);
                   

                }
                spriteBatch.End();
                pauseMenu.Draw(gameTime);
            }
        }
        Rectangle GetFullScreenRectangle()
        {
            return new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            
        }
        void gameWorld_Event_PauseStateChanged(object sender, EventArgs e)
        {
            if (gameWorld.Paused)
            {
                pauseMenu.ButtonSelectefReset();
                pauseMenu.Reset();
                pauseMenu.Open();
                if (gameWorld.CurrentStage.Won)
                {
                    ExitToMainMenu(this, new EventArgs());
                    if (gameWorld.CurrentStage is Stages.Stage6 == false)
                    ((Game1)Game).mainMenu.StartStage(((Game1)Game).CurrentStage);
                }
                else if (gameWorld.CurrentStage.Over)
                {
                    pauseMenu.Items[0].Enabled = false;
                    pauseMenu.Items[0].Text = " ";
                    pauseMenu.Items[1].SelectedIndex = 1;
                    pauseMenu.EscIndex = 2;
                }
            }
            else
            {
                pauseMenu.Close();
            }
        }
        /// <summary>
        /// 回到最近的检查点（即从目前的关卡片段开始）
        /// </summary>
        void RollBackToLatestCheckpoint()
        {
            LoadGameWorld(gameWorld.CurrentStage.GetNewStage(), gameWorld.CurrentStage.CurrentStagePartIndex);
        }

        /// <summary>
        /// 读取游戏世界
        /// </summary>
        /// <param name="part">关卡片段号，从头开始可使用null</param>
        void LoadGameWorld(Stage stage, int? part)
        {

            Reset();
            if (part != null)
            {

                gameWorld = new GameWorld(Game, stage, part.Value);
                gameWorld.Event_StageLoaded += new EventHandler(gameWorld_Event_StageLoaded);
                gameWorld.LoadStage(stage, part.Value);
            }

            else
            {
                gameWorld = new GameWorld(Game, stage);
                gameWorld.Event_StageLoaded += new EventHandler(gameWorld_Event_StageLoaded);
                gameWorld.LoadStage(stage);
            }
            gameWorld.Event_PauseStateChanged += new EventHandler(gameWorld_Event_PauseStateChanged);
            gameWorld.Event_StageFailed += new EventHandler(gameWorld_Event_StageFailed);
            gameWorld.Event_StageWon += new EventHandler(gameWorld_Event_StageWon);
            gameWorld.CurrentStage.PartChanged += new StageEventHandlers.StagePartChangeHandler(CurrentStage_PartChanged);
            gameWorld.MVolume = ((Game1)Game).MV;

        }

        void gameWorld_Event_StageLoaded(object sender, EventArgs e)
        {
            
            gameWorld.CurrentStage.Event_LoadPlayer += new PlayerLoadHandler(CurrentStage_LoadPlayer);
            gameWorld.CurrentStage.Event_SavePlayer += new EventHandler(CurrentStage_Event_SavePlayer);
            gameWorld.CurrentStage.Event_EnableStage += new EnableStageHandler(CurrentStage_Event_EnableStage);
        }

        void CurrentStage_Event_EnableStage(int i)
        {
            
             ((Game1)Game).CurrentStage = i;
            if (!((Game1)Game).EnabledStages.Contains(i))
            {
                ((Game1)Game).EnabledStages.Add(i);
                ((Game1)Game).textManager.AddText(new AODGameLibrary.Texts.AODText(Game, "New chapter available", 5f, Color.Gold, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), AODGameLibrary.Texts.FadeOutState.Normal, AODGameLibrary.Helpers.RandomHelper.RandomDirection2() * 20, true));
           
            }
            //try
            //{
                AODSaver.SaveData(((Game1)Game).EnabledStages, GameConsts.GameSaveDirectory + @"\" + "Es.aod");
                AODSaver.SaveData(((Game1)Game).CurrentStage, GameConsts.GameSaveDirectory + @"\" + "Cs.aod");
            //}
        }

        void CurrentStage_Event_SavePlayer(object sender, EventArgs e)
        {
            PlayerSaver.SaveUnit(gameWorld.CurrentStage.Player,true);
            if (Directory.Exists(GameConsts.GameSaveDirectory) == false)
            {
                Directory.CreateDirectory(GameConsts.GameSaveDirectory);
            }
            //try
            //{
                  AODSaver.SaveData(PlayerSaver, GameConsts.GameSaveDirectory + @"\" + "Player.aod");
                  ((Game1)Game).textManager.AddText(new AODGameLibrary.Texts.AODText(Game, "Player data saved", 5f, Color.Gold, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), AODGameLibrary.Texts.FadeOutState.Normal, AODGameLibrary.Helpers.RandomHelper.RandomDirection2() * 20, true));
              
            //}
            //catch
            //{
            //    //((Game1)Game).textManager.AddText(new AODGameLibrary.Texts.AODText(Game, "战机信息保存失败", 5f, Color.Red, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), AODGameLibrary.Texts.FadeOutState.Normal, GameHelpers.GameHelper.RandomDirection2() * 20, true));
           
            //}
            //占楼待编辑
        }
        

        Unit CurrentStage_LoadPlayer(int Group, bool Player,Vector3 position)
        {
            if (MissionPlayerSaver.Exist)
            {
                ((Game1)Game).textManager.AddText(new AODGameLibrary.Texts.AODText(Game, "Checkpoint loaded", 5f, Color.Gold, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), AODGameLibrary.Texts.FadeOutState.Normal, AODGameLibrary.Helpers.RandomHelper.RandomDirection2() * 20, true));
           
                return LoadSavedUnit(MissionPlayerSaver, Group, Player, position);
            }
            else if (PlayerSaver.Exist)
            {
                ((Game1)Game).textManager.AddText(new AODGameLibrary.Texts.AODText(Game, "Player data loaded", 5f, Color.Gold, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), AODGameLibrary.Texts.FadeOutState.Normal, AODGameLibrary.Helpers.RandomHelper.RandomDirection2() * 20, true));
           
                return LoadSavedUnit(PlayerSaver, Group, Player, position);
            }
            else
            {
                ((Game1)Game).textManager.AddText(new AODGameLibrary.Texts.AODText(Game, "New player data", 5f, Color.Gold, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), AODGameLibrary.Texts.FadeOutState.Normal, AODGameLibrary.Helpers.RandomHelper.RandomDirection2() * 20, true));
              
                Unit u = gameWorld.CreatePlayerUnit(Game.Content.Load<UnitType>(@"UnitTypes\Falcon"), Group, position);
                u.skills.Clear();
                return u;
            }
        }

        void CurrentStage_PartChanged(int targetPart, int formerPart)
        {
            if (gameWorld.CurrentStage.Player.Dead != true)
            {
                MissionPlayerSaver.SaveUnit(gameWorld.CurrentStage.Player, false);
            }
            else MissionPlayerSaver.Clear();
        }

        void gameWorld_Event_StageWon(object sender, EventArgs e)
        {
            if (gameFailTimeCounting == false)
            {
                gameWinTimeCounting = true;
                if (GameConsts.GameWinMusic != "")
                {

                    gameWorld.PlayMusic(GameConsts.GameWinMusic , false, 0.5f);
                }
            }
        }
    

        void gameWorld_Event_StageFailed(object sender, EventArgs e)
        {
            if (gameWinTimeCounting == false)
            {

                gameFailTimeCounting = true;
                gameWorld.StopMusic();
            }
        }

        Unit LoadSavedUnit(AODGameLibrary2.SavedUnit su, int group, bool player, Vector3 position)
        {
            Unit u;
            if (player)
            {

                u = Unit.Create(gameWorld, su.UnitType, group, position, true, false);

            }
            else u = Unit.Create(gameWorld, su.UnitType, group, position, false, true);
            gameWorld.units.Add(u);
            gameWorld.variables.LastCreatedUnit = u;
            u.weapons.Clear();
            u.skills.Clear();
            u.missiles.Clear();
            foreach (AODGameLibrary2.SavedInf s in su.Weapons)
            {

                Weapon w = new Weapon(gameWorld, gameWorld.game.Content.Load<WeaponType>(s.AssetName),u);
                w.AmmoNum = s.AmmoNum;
                 u.weapons.Add(w);
                


            }
            foreach (AODGameLibrary2.SavedInf s in su.Missiles)
            {

                MissileWeapon w = new MissileWeapon(gameWorld, gameWorld.game.Content.Load<MissileWeaponType>(s.AssetName));
                w.Num =(int) s.AmmoNum;
                u.missiles.Add(w);



            } 
            foreach (AODGameLibrary2.SavedInf s in su.Spells)
            {

                u.AddSkill(s.AssetName);



            }
            return u;
        }
             
    }
}