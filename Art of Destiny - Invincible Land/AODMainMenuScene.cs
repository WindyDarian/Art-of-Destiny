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
using AODGameLibrary;
using AODGameLibrary.Menu;
using AODGameLibrary.GamePlay;
using Stages;
using Stages;
using AODGameLibrary.Texts;


namespace AOD
{
    /// <summary>
    /// 命运艺术主菜单,由大地无敌-范若余在2009年10月18日建立
    /// </summary>
    public class AODMainMenuScene : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D background;
        Menu mainMenu;
        Menu settingMenu;
        Menu singleGameMenu;
        Menu stageSelectMenu;
        Menu educateMenu;
        Settings settings;
        bool reseting = true;
        Texture2D logo;
        bool b1 = false;
        TextManager texts = new TextManager();
        float textTime = 30;
        int currentTextNum = 0;
        Texture2D blank;
        Texture2D[] help = new Texture2D[3];
        float t = 0;
        private List<Menu> menus = new List<Menu>();
        /// <summary>
        /// 菜单
        /// </summary>
        public List<Menu> Menus
        {
            get { return menus; }
            set { menus = value; }
        }
  
        public AODMainMenuScene(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
            background = Game.Content.Load<Texture2D>("Background");
            logo = Game.Content.Load<Texture2D>("logo");
            blank = Game.Content.Load<Texture2D>("blank");
            help[0] = Game.Content.Load<Texture2D>(@"Textures\aodhelp");
            help[1] = Game.Content.Load<Texture2D>(@"Textures\aodhelp2");
            help[2] = Game.Content.Load<Texture2D>(@"Textures\aodhelp3");
        }
        protected override void LoadContent()
        {
            base.LoadContent();
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            textTime += elapsedTime;
            if (textTime > 30)
            {
                int y = 0;//字在y上的值
                switch (currentTextNum)
                {
                    case 0:
                        /*
                        texts.AddText(new AODText(Game, "这是一个黑暗的时代。", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "人类在星际中", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "迷失了。", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "似乎是命运。", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "不！绝对不会！", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "无边无际的黑夜，", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "不会是我们的命运。", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "献给所有", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "为了掌握命运而战斗着的人们，", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "并召集所有心中的火焰尚未泯灭的战士，", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero, false));
                        texts.AddText(new AODText(Game, "将命运的艺术，进行到底……", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "——第三舰队前指挥官Bill Warden", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero, false));
                        texts.AddText(new AODText(Game, "——一段在星际中广为流传的电波", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero, false));
                         */
                        texts.AddText(new AODText(Game, "This is an age of darkness。", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "The human gets lost", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "in the stars.", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "The destiny.", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "No, not like that,", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "The endliss night", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "shall not be our fate.", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "To all the warriors", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "who fights for their own destiny.", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "Fight, and", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero, false));
                        texts.AddText(new AODText(Game, "wield your destiny", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, " -Bill Warden, Commander of the 3rd Fleet", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero, false));
                        texts.AddText(new AODText(Game, "(A radio spreading in the space)", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero, false));


                        break;
                    case 1:
                        /*
                        texts.AddText(new AODText(Game, "黑夜……", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "在这漫无边际的黑夜中，", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "噩梦正在吞噬一切……", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "如果今天的命运已经注定，那么", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "这个世界", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "将永远不会有明天……", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "但是……一切还没有结束……", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "至少不是现在……", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "现在，", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "牢牢握住命运，", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "只要有一线希望。", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "All is not lost...", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "Not yet.", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "Our destiny awaits.", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));

                         */
                        texts.AddText(new AODText(Game, "Darkness...", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "It never ends.", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "The nightmare has come.", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "If this shall be our fate today,", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "then in this world", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "there would be no tomorrow", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "But, all is not lost.", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "At least not for now", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "wield it.", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero, false));
           
                        break;
                    case 2:
                        /*
                        texts.AddText(new AODText(Game, "挽歌，", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "我们不需要挽歌。", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "因为我们的旅程尚未结束。", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "到了传说的最后，", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "等待着的不是挽歌，", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "而是凯旋，", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "和新的序幕……", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                         */

                        texts.AddText(new AODText(Game, "Elegy，", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero, false));
                        texts.AddText(new AODText(Game, "we don't need elegy,", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "for we are still on our journey/", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "At the end of the legend,", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "the triumph awaits.", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));



                        break;
                    case 3:
                        /*
                        texts.AddText(new AODText(Game, "Art of Destiny –", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "With your Destiny,", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "Wield Your Destiny!", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "命运艺术，", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "掌握在手中的命运，", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "也是一种艺术。", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "敢于抓住自己命运的人，", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "便是艺术的编织者。", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "破晓……", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        texts.AddText(new AODText(Game, "", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero,false));
                        */
                        texts.AddText(new AODText(Game, "Art of Destiny –", 28, Color.LightYellow, new Vector2(50, 60 + y++ * 40), FadeOutState.OneFourth, Vector2.Zero, false));
                        


                        //don't ask me why I wrote these, now I don't know why I did this 5 years ago...
                        break;
                    default:
                        break;
                }
                currentTextNum++;
                if (currentTextNum>3)
                {
                    currentTextNum = 0;
                }
                textTime = 0;
            }
            if (t <1)
            {
                t = MathHelper.Clamp(t + elapsedTime, 0, 1);
            }
            if (t>=1&& b1 == false)
            {
                b1 = true;
                mainMenu.Open();
                //Game.Content.Load<SoundEffect>(@"Audio\Blast4").Play();
            }
            if (reseting)
            {
                Reset();
                reseting = false;
            }
            foreach (Menu m in menus)
            {
                if (m!= null )
                {
                    m.Update(gameTime);
                }
            }
            texts.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            int h = GraphicsDevice.Viewport.Height;
            int w = GraphicsDevice.Viewport.Width;

            int bw = GraphicsDevice.Viewport.Height * background.Width / background.Height;

            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, bw, h), Color.White);
            spriteBatch.Draw(logo, Vector2.Lerp(new Vector2(w, h) / 2, new Vector2(w-260,h-140), t / 1), null, Color.White, 0, new Vector2(256, 128),MathHelper.Lerp(2,1,t/1), SpriteEffects.None, 0.1f);
            spriteBatch.End();

            texts.Draw(gameTime);
            spriteBatch.Begin();
            if (settingMenu.Visable||stageSelectMenu.Visable)
            {
                Color c = Color.Black;
                c.A = 220;
                spriteBatch.Draw(blank, new Rectangle(160, 80, (int)(w * 0.75f) - 180, (int)(h * 0.75f) - 180), c);
            }
            if (educateMenu.Visable)
            {
                Texture2D tx = help[educateMenu.ButtonFromKeyword("翻页").SelectedIndex];
                spriteBatch.Draw(tx, new Vector2(w, h) / 2, null, Color.White, 0, new Vector2(tx.Width / 2, tx.Height / 2), 1, SpriteEffects.None, 0.15f);
            }
            //spriteBatch.DrawString((SpriteFont)Game.Services.GetService(typeof(SpriteFont)), "BY大地无敌@http://www.agrp.info 2011-12-25 转换至 XNA 4.0", new Vector2(30, GraphicsDevice.Viewport.Height - 30), new Color(Color.CornflowerBlue.R, Color.CornflowerBlue.G, Color.CornflowerBlue.B, 225), 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0.1f);

            spriteBatch.DrawString((SpriteFont)Game.Services.GetService(typeof(SpriteFont)), "originally developed by Windy Darian(http://windy.moe) in 2009-2010, translated to English in Dec 2014", new Vector2(30, GraphicsDevice.Viewport.Height - 30), new Color(Color.CornflowerBlue.R, Color.CornflowerBlue.G, Color.CornflowerBlue.B, 225), 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0.1f);


            spriteBatch.End();

            foreach (Menu m in menus)
            {
                if (m != null)
                {
                    m.Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }
        void mainMenu_Click(object sender,EventArgs e)
        {
            switch (mainMenu.SelectedKeyword)
            {
                case "单人游戏":
                    mainMenu.Close();
                    singleGameMenu.Open();

                    break;

                case "设置":
                    mainMenu.Close();
                    settingMenu.Open();
                    break;
                case "制作者":

                    this.Close();
                    StartGame(new Stages.TheMaker(), null);
                    break;
                case "结束":
                    Game.Exit();
                    break;
            }
        }
        void settingMenu_Click(object sender, EventArgs e)
        {
            switch (settingMenu.SelectedKeyword)
            {
                case "完成":
                    settings.SettingFromKeyword("分辨率").SettingValue = settingMenu.ButtonFromKeyword("分辨率").SelectedIndex;
                    settings.SettingFromKeyword("全屏幕").SettingValue = settingMenu.ButtonFromKeyword("全屏幕").SelectedIndex;
                    settings.SettingFromKeyword("Bloom").SettingValue = settingMenu.ButtonFromKeyword("Bloom").SelectedIndex;
                    settings.SettingFromKeyword("音乐").SettingValue = settingMenu.ButtonFromKeyword("音乐").SelectedIndex;

                    settings.SettingFromKeyword("音效").SettingValue = settingMenu.ButtonFromKeyword("音效").SelectedIndex;

                    SettingsChanged(this, new EventArgs());
                    this.ResetSoon();
                   
                    break;
                case "取消":
                    settingMenu.Close();
                    settingMenu.Reset();
                    mainMenu.Open();
                    Reset();
                    mainMenu.SelectedIndex = 1;
                    break;

            }
        }
        void singleGameMenu_Click(Object Sender, EventArgs e)
        {
            switch (singleGameMenu.SelectedKeyword)
            {
                case "继续":
                    StartStage(((Game1)Game).CurrentStage);
                    break;

                case "新游戏":
                    this.Close();
                    StartGame(null, null);
                    break;
                case "选择关卡":
                    singleGameMenu.Close();
                    stageSelectMenu.Open();
                    break;
                case "战斗手册":
                    singleGameMenu.Close();
                    educateMenu.Open();
                    break;
                case "取消":
                    singleGameMenu.Close();
                    singleGameMenu.Reset();
                    mainMenu.Open();
                    break;


            }
        }
        public event StartGameHandler StartGame;
        /// <summary>
        /// 下一帧时重置
        /// </summary>
        public void ResetSoon()
        {
            reseting = true;
        }
        /// <summary>
        /// 重设菜单
        /// </summary>
        public void Reset()
        {
            settings = (Settings)Game.Services.GetService(typeof(Settings));
            menus = new List<Menu>();
            #region 添加菜单
            mainMenu = new Menu(Game);
            mainMenu.Position = new Vector2(Game.GraphicsDevice.Viewport.Width - 280, 100);
            //mainMenu.Items.Add(new MenuButton("开始游戏",true,"单人游戏"));
            mainMenu.Items.Add(new MenuButton("Campaign", true, "单人游戏"));

            //mainMenu.Items.Add(new MenuButton("设置",true,"设置"));
            mainMenu.Items.Add(new MenuButton("Options", true, "设置"));


            //mainMenu.Items.Add(new MenuButton("制作者", true,"制作者"));

            //mainMenu.Items.Add(new MenuButton("结束",true, "结束"));
            mainMenu.Items.Add(new MenuButton("Exit", true, "结束"));
            
            mainMenu.EscIndex = 2;//需改?
            mainMenu.Click += new EventHandler(mainMenu_Click);
            menus.Add(mainMenu);
            settingMenu = new Menu(Game);
            settingMenu.Position = new Vector2(200, 100);
            List<string> a = new List<string>();
            //a.Add("自动适应");
            a.Add("Auto");
            a.Add("1024x768");
            a.Add("1280x1024");
            a.Add("1280x960");
            a.Add("1280x800");
            a.Add("1152x864");
            a.Add("1440x900");
            a.Add("1600x1200");
            a.Add("1680x1050");
            a.Add("1920x1200");
            settingMenu.Items.Add(new MenuButton("Resolution：", true, "分辨率", a,settings.SettingFromKeyword("分辨率").SettingValue));
            List<string> b = new List<string>();
            b.Add("Off");
            b.Add("On");
            settingMenu.Items.Add(new MenuButton("Fullscreen：", true, "全屏幕", b, settings.SettingFromKeyword("全屏幕").SettingValue));

            settingMenu.Items.Add(new MenuButton("Bloom：", true, "Bloom", b, settings.SettingFromKeyword("Bloom").SettingValue));

            
            settingMenu.Items.Add(new MenuButton("Invert Mouse Y", true, "invertMouseY", b, settings.SettingFromKeyword("invertMouseY").SettingValue));

            settingMenu.Items.Add(new MenuButton("Invert Xbox Controller Y", true, "invertPadY", b, settings.SettingFromKeyword("invertPadY").SettingValue));

            List<string> ss= new List<string> ();
            //ss.Add("关");
            ss.Add("Off");
            ss.Add("I");
             ss.Add("II");
             ss.Add("III");
             ss.Add("IIII");
             ss.Add("IIIII");
             ss.Add("IIIIII");
             ss.Add("IIIIIII");
             ss.Add("IIIIIIII");
             ss.Add("IIIIIIIII");
            ss.Add("IIIIIIIIII");
            //settingMenu.Items.Add(new MenuButton("音乐音量", true, "音乐", ss, settings.SettingFromKeyword("音乐").SettingValue));
            //settingMenu.Items.Add(new MenuButton("音效音量", true, "音效", ss, settings.SettingFromKeyword("音效").SettingValue));
            
            //settingMenu.Items.Add(new MenuButton("完成", true, "完成"));
            
            //settingMenu.Items.Add(new MenuButton("取消",true,"取消"));

            settingMenu.Items.Add(new MenuButton("Music Volume", true, "音乐", ss, settings.SettingFromKeyword("音乐").SettingValue));
            settingMenu.Items.Add(new MenuButton("Sfx Volume", true, "音效", ss, settings.SettingFromKeyword("音效").SettingValue));

            settingMenu.Items.Add(new MenuButton("Confirm", true, "完成"));

            settingMenu.Items.Add(new MenuButton("Cancel",true,"取消"));

            settingMenu.EscIndex = 8;
            settingMenu.Click += new EventHandler(settingMenu_Click);
            menus.Add(settingMenu);
            singleGameMenu = new Menu(Game);
            singleGameMenu.Position = mainMenu.Position;
            singleGameMenu.Click+=new EventHandler(singleGameMenu_Click);
            if (((Game1)Game).CurrentStage != 0)
            {

                //singleGameMenu.Items.Add(new MenuButton("继续", true, "继续"));
                singleGameMenu.Items.Add(new MenuButton("Continue", true, "继续"));
            }
            else
            {
                //singleGameMenu.Items.Add(new MenuButton("继续", false, "继续"));
                singleGameMenu.Items.Add(new MenuButton("Continue", false, "继续"));
            }
            
            //singleGameMenu.Items.Add(new MenuButton("新游戏",true,"新游戏"));
            //singleGameMenu.Items.Add(new MenuButton("选择关卡",true,"选择关卡"));
            //singleGameMenu.Items.Add(new MenuButton("战斗手册", true, "战斗手册"));
            //singleGameMenu.Items.Add(new MenuButton("取消", true, "取消"));
            singleGameMenu.Items.Add(new MenuButton("New Game", true, "新游戏"));
            singleGameMenu.Items.Add(new MenuButton("Select Chapter", true, "选择关卡"));
            //singleGameMenu.Items.Add(new MenuButton("Manual (Untranslated)", true, "战斗手册"));
            singleGameMenu.Items.Add(new MenuButton("Cancel", true, "取消"));
            singleGameMenu.EscIndex = 3;//!
            menus.Add(singleGameMenu);
            stageSelectMenu = new Menu(Game);
            stageSelectMenu.Position = settingMenu.Position;
            stageSelectMenu.Click += new EventHandler(stageSelectMenu_Click);
            List<int> l = ((Game1)Game).EnabledStages;
            /*
            if (l.Contains(1))
            stageSelectMenu.Items.Add(new MenuButton("第一章-诸神的黄昏", true, "1"));
            else stageSelectMenu.Items.Add(new MenuButton("第一章-??", false, "1"));
            if (l.Contains(2))
            stageSelectMenu.Items.Add(new MenuButton("第二章-斜月沉沉", true, "2"));
            else stageSelectMenu.Items.Add(new MenuButton("第二章-??", false , "2"));
            if (l.Contains(3))
                stageSelectMenu.Items.Add(new MenuButton("第三章-幻月流光", true, "3"));
            else stageSelectMenu.Items.Add(new MenuButton("第三章-??", false , "3"));
            if (l.Contains(4))
                stageSelectMenu.Items.Add(new MenuButton("第四章-午夜之环", true, "4"));
            else stageSelectMenu.Items.Add(new MenuButton("第四章-??", false , "4"));
            if (l.Contains(5))
                stageSelectMenu.Items.Add(new MenuButton("第五章-命运的艺术", true, "5"));
            else stageSelectMenu.Items.Add(new MenuButton("第五章-??", false , "5"));
            if (l.Contains(6))
                stageSelectMenu.Items.Add(new MenuButton("第六章-重铸秩序", true, "6"));
            else stageSelectMenu.Items.Add(new MenuButton("第六章-??", false , "6"));
            if (l.Contains(11))
                stageSelectMenu.Items.Add(new MenuButton("BOSS战测试关卡-末日", true, "11"));
            else stageSelectMenu.Items.Add(new MenuButton("??", false , "11"));
            stageSelectMenu.Items.Add(new MenuButton("取消", true, "取消"));
             */
            if (l.Contains(1))
                stageSelectMenu.Items.Add(new MenuButton("Chapter 1 - Twilight of Gods", true, "1"));
            else stageSelectMenu.Items.Add(new MenuButton("Chapter 1 - ??", false, "1"));
            if (l.Contains(2))
                stageSelectMenu.Items.Add(new MenuButton("Chapter 2 - The Moon", true, "2"));
            else stageSelectMenu.Items.Add(new MenuButton("Chapter 2 - ??", false, "2"));
            if (l.Contains(3))
                stageSelectMenu.Items.Add(new MenuButton("Chapter 3 - Flowing Light", true, "3"));
            else stageSelectMenu.Items.Add(new MenuButton("Chapter 4 - ??", false, "3"));
            if (l.Contains(4))
                stageSelectMenu.Items.Add(new MenuButton("Chapter 4 - Ring of Midnight", true, "4"));
            else stageSelectMenu.Items.Add(new MenuButton("Chapter 4 - ??", false, "4"));
            if (l.Contains(5))
                stageSelectMenu.Items.Add(new MenuButton("Chapter 5 - Art of Destiny", true, "5"));
            else stageSelectMenu.Items.Add(new MenuButton("Chapter 5 - ??", false, "5"));
            if (l.Contains(6))
                stageSelectMenu.Items.Add(new MenuButton("Chapter 6 - Reforge", true, "6"));
            else stageSelectMenu.Items.Add(new MenuButton("Chapter 6 - ??", false, "6"));
            if (l.Contains(11))
                stageSelectMenu.Items.Add(new MenuButton("BOSS fight test", true, "11"));
            else stageSelectMenu.Items.Add(new MenuButton("??", false, "11"));
            stageSelectMenu.Items.Add(new MenuButton("Cancel", true, "取消"));


            stageSelectMenu.EscIndex = 7;
            menus.Add(stageSelectMenu);
            educateMenu = new Menu(Game);
            educateMenu.Position = new Vector2(Game.GraphicsDevice.Viewport.Width - 200, 15);
            educateMenu.Click += new EventHandler(educateMenu_Click);
            List<string> c = new List<string>();
            c.Add("第1页");
            c.Add("第2页");
            c.Add("第3页");
            educateMenu.Items.Add(new MenuButton("翻页", true, "翻页", c));
            educateMenu.Items.Add(new MenuButton("取消", true, "取消"));
            educateMenu.EscIndex = 1;
            menus.Add(educateMenu);
            #endregion
            if (t >= 1)
            {

                mainMenu.Open();
            }
        }

        void educateMenu_Click(object sender, EventArgs e)
        {
            switch (educateMenu.SelectedKeyword)
            {
                case "翻页":
                    
                    break;
                default:
                    educateMenu.Reset();
                    educateMenu.ButtonSelectefReset();
                    educateMenu.Close();

                    singleGameMenu.Open();
                    break;
            }
        }

        void stageSelectMenu_Click(object sender, EventArgs e)
        {
            switch (stageSelectMenu.SelectedKeyword)
            {
                case "1":
                    StartGame(new Stage1(), null);
                    break;
                case "2":
                    StartGame(new Stage2(), null);
                    break;
                case "3":
                    StartGame(new Stage3(), null);
                    break;
                case "4": 
                    StartGame(new Stage4(), null);
                    break;
                case "5":
                    StartGame(new Stage5(), null);
                    break;
                case "6":
                    StartGame(new Stage6(), null);
                    break;
                case "11":
                    StartGame(new TestStage(), null);
                    break;
                default:
                    stageSelectMenu.Close();
                    stageSelectMenu.Reset();
                    singleGameMenu.Open();
                    break;
            }

        }
        
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            Enabled = false;
            Visible = false;
        }
        /// <summary>
        /// 开启
        /// </summary>
        public void Open()
        {
            Enabled = true ;
            Visible = true;
        }
        public void StartStage(int i)
        {
            switch (i)
            {
                case 1:

                    StartGame(new Stage1(),null);
                    break;
                case 2:
                    StartGame(new Stage2(), null);
                    break;
                case 3:
                    StartGame(new Stage3(), null);
                    break;
                case 4:
                    StartGame(new Stage4(), null);
                    break;
                case 5:
                    StartGame(new Stage5(), null);
                    break;
                case 6:
                    StartGame(new Stage6(), null);
                    break;
                case 11:
                    StartGame(new TestStage(), null);
                    break;
                default:
                    break;
            }
        }
        public event EventHandler SettingsChanged;
        
    }
}