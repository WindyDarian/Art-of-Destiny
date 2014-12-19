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

namespace AODGameLibrary.Menu
{
    /// <summary>
    /// 菜单，由大地无敌-范若余于2009年10月20日建立
    /// </summary>
    public class Menu
    {
       
        private List<MenuButton> items = new List<MenuButton>(10);
        /// <summary>
        /// 菜单按钮
        /// </summary>
        public List<MenuButton> Items
        {
            get { return items; }
            set { items = value; }
        }
        private Vector2 position = Vector2.Zero;
        /// <summary>
        /// 菜单左上角位置
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private float spacing = 40;
        /// <summary>
        /// 菜单项间距
        /// </summary>
        public float Spacing
        {
            get { return spacing; }
            set { spacing = value; }
        }
        private int selectedIndex = 0;
        /// <summary>
        /// 现在选中的菜单项,循环
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                int si = value;
                int f = Math.Sign(value - selectedIndex);
                for (int c = 0; c < items.Count; c++)
                {

                    int b = items.Count;
                    si = (si % b + b) % b;

                    if (items[si].Enabled)
                    {
                        selectedIndex = si;
                        break;
                    }
                    else
                    {
                        si += f;
                    }
                }
                //if (si > items.Count - 1)
                //{
                //    si = (int)MathHelper.Clamp(si - items.Count, 0, items.Count - 1);
                //}
                //else if (si < 0)
                //{
                //    si = (int)MathHelper.Clamp(si + items.Count, 0, items.Count - 1);
                //}
                //if (items[si].Enabled)
                //{
                //    selectedIndex = si;
                //}
                //else
                //{
                //    if (f != 0)
                //    {

                //        for (int i = si; i < items.Count && i >= 0; i += f)
                //        {
                //            if (items[i].Enabled)
                //            {
                //                selectedIndex = i;
                //                break;
                //            }
                //        }
                //    }
                //}
            }
        }
        /// <summary>
        /// 得到选中项的关键字
        /// </summary>
        public string SelectedKeyword
        {
            get
            {
                return items[selectedIndex].Keyword;
            }
        }
        public MenuButton SelectedButton
        {
            set
            {
                items[selectedIndex] = value;
            }
            get
            {
                return items[selectedIndex];
            }
        }
        private Texture2D buttonBackground;
        /// <summary>
        /// 按钮背景图
        /// </summary>
        public Texture2D ButtonBackground
        {
            get { return buttonBackground; }
            set { buttonBackground = value; }
        }
        private Texture2D selectingBox;
        /// <summary>
        /// 选取方块图片
        /// </summary>
        public Texture2D SelectingBox
        {
            get { return selectingBox; }
            set { selectingBox = value; }
        }
        private Color stringColor = Color.White;
        /// <summary>
        /// 文字颜色
        /// </summary>
        public Color StringColor
        {
            get { return stringColor; }
            set { stringColor = value; }
        }
        private Color selectedStringColor = Color.CornflowerBlue;
        /// <summary>
        /// 选中的文字颜色
        /// </summary>
        public Color SelectedStringColor
        {
            get { return selectedStringColor; }
            set { selectedStringColor = value; }
        }
        private float stringScale = 1.0f;
        /// <summary>
        /// 文字缩放
        /// </summary>
        public float StringScale
        {
            get { return stringScale; }
            set { stringScale = value; }
        }
        private float selectedStringScale = 1.0f;
        /// <summary>
        /// 选中的文字缩放
        /// </summary>
        public float SelectedStringScale
        {
            get { return selectedStringScale; }
            set { selectedStringScale = value; }
        }
        private Color grayTextColor = Color.Gray;
        /// <summary>
        /// 无效的选项颜色
        /// </summary>
        public Color GrayTextColor
        {
            get { return grayTextColor; }
            set { grayTextColor = value; }
        }
        private SpriteFont font;
        /// <summary>
        /// 字体
        /// </summary>
        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }
        private bool actived = false;
        /// <summary>
        /// 该菜单是否被激活
        /// </summary>
        public bool Actived
        {
            get { return actived; }
            set { actived = value; }
        }
        private bool visable = false;
        /// <summary>
        /// 是否可见
        /// </summary>
        public bool Visable
        {
            get { return visable; }
            set { visable = value; }
        }
        private SoundEffect menuSelectSound;
        /// <summary>
        /// 菜单选择声音
        /// </summary>
        public SoundEffect MenuSelectSound
        {
            get { return menuSelectSound; }
            set { menuSelectSound = value; }
        }
        private SoundEffect menuMoveSound;
        /// <summary>
        /// 选项移动声音
        /// </summary>
        public SoundEffect MenuMoveSound
        {
            get { return menuMoveSound; }
            set { menuMoveSound = value; }
        }
        private int? escIndex = null;
        /// <summary>
        /// 按ESC键选中的项
        /// </summary>
        public int? EscIndex
        {
            get { return escIndex; }
            set { escIndex = value; }
        }
         

        private SpriteBatch spriteBatch;
        private Game game;
        public event EventHandler Click;
        bool newlyOpened = true;
        public Menu(Game game)
        {
            font = game.Content.Load<SpriteFont>(@"menufont");
            this.game = game;
            menuSelectSound = game.Content.Load<SoundEffect>(GameConsts.MenuSelectSound);
            menuMoveSound = game.Content.Load<SoundEffect>(GameConsts.MenuMoveSound);
        }
        public virtual void Update(GameTime gameTime)
        {
            
            if (actived && (newlyOpened == false))
            {
                if (SelectedButton.Enabled == false)
                {
                    SelectedIndex += 1;
                }
                else
                {
                    if (InputState.IsKeyPressed(Keys.W) || InputState.IsKeyPressed(Keys.Up) || InputState.IsPadButtonPressed(Buttons.DPadUp) || InputState.IsPadButtonPressed(Buttons.LeftThumbstickUp))
                    {
                        SelectedIndex -= 1;
                        menuMoveSound.Play();
                        
                    }
                    else if (InputState.IsKeyPressed(Keys.S) || InputState.IsKeyPressed(Keys.Down) || InputState.IsPadButtonPressed(Buttons.DPadDown) || InputState.IsPadButtonPressed(Buttons.LeftThumbstickDown))
                    {
                        SelectedIndex += 1;
                        menuMoveSound.Play();
                    }
                    else if (InputState.IsKeyPressed(Keys.A) || InputState.IsKeyPressed(Keys.Left) || InputState.IsPadButtonPressed(Buttons.DPadLeft) || InputState.IsPadButtonPressed(Buttons.LeftThumbstickLeft))
                    {
                        if (SelectedButton.Options != null)
                        {

                            SelectedButton.SelectedIndex -= 1;
                            menuMoveSound.Play();
                        }
                    }
                    else if (InputState.IsKeyPressed(Keys.D) || InputState.IsKeyPressed(Keys.Right) || InputState.IsPadButtonPressed(Buttons.DPadRight) || InputState.IsPadButtonPressed(Buttons.LeftThumbstickRight))
                    {
                        if (SelectedButton.Options != null)
                        {

                            SelectedButton.SelectedIndex += 1;
                            menuMoveSound.Play();
                        }
                    }
                    if (InputState.IsKeyPressed(Keys.Enter) || InputState.IsKeyPressed(Keys.Space) || InputState.IsPadButtonPressed(Buttons.A) || InputState.IsPadButtonPressed(Buttons.RightTrigger))
                    {
                        if (this.Click != null)
                        {

                            this.Click(this, new EventArgs());
                            
                        }
                        if (SelectedButton.Options != null  && newlyOpened == false)
                        {

                            SelectedButton.SelectedIndex += 1;
                            
                        }
                        menuSelectSound.Play();
 
                    }
                    else if (InputState.IsKeyPressed(Keys.Escape) || InputState.IsPadButtonPressed(Buttons.B) || InputState.IsPadButtonPressed(Buttons.LeftTrigger))
                    {
                        if (escIndex!= null)
                        {
                            if (items[escIndex.Value].Enabled)
                            {
                               
                                SelectedIndex = escIndex.Value;
                                if (this.Click != null)
                                {

                                    this.Click(this, new EventArgs());

                                }
                                if (SelectedButton.Options != null && newlyOpened == false)
                                {

                                    SelectedButton.SelectedIndex += 1;

                                }
                                menuSelectSound.Play();
                            }
                        }
                    }
                }
            }
            newlyOpened = false;


        }
        public virtual void Draw(GameTime gameTime)
        {
            if (visable)
            {
                spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
                spriteBatch.Begin();
                for (int i = 0; i < items.Count; i++)
                {
                    string t = items[i].Text;
                    if (items[i].Options != null)
                    {
                        t += "  < " + items[i].Options[items[i].SelectedIndex]+" >";
                    }
                    if (items[i].Enabled)
                    {
                        if (i != selectedIndex)
                        {
                            spriteBatch.DrawString(font, t, position + new Vector2(0, i * spacing), stringColor, 0, new Vector2(font.MeasureString(items[i].Text).Y / 2, 0)
                    , stringScale, SpriteEffects.None, 0.2f);
                        }
                        else
                        {
                            spriteBatch.DrawString(font, t, position + new Vector2(0, i * spacing), selectedStringColor, 0, new Vector2(font.MeasureString(items[i].Text).Y / 2, 0)
     , selectedStringScale, SpriteEffects.None, 0.2f);
                        }
                    }
                    else
                    {
                        spriteBatch.DrawString(font, t, position + new Vector2(0, i * spacing), grayTextColor, 0, new Vector2(font.MeasureString(items[i].Text).Y / 2, 0)
    , selectedStringScale, SpriteEffects.None, 0.2f);
                    }



                }
                spriteBatch.End();
            }

        }
        public MenuButton ButtonFromKeyword(string keyword)
        {
            foreach (MenuButton m in items)
            {
                if (m.Keyword == keyword)
                {
                    return m;
                }
            }
            return null;
        }
        /// <summary>
        /// 显示
        /// </summary>
        public void Open()
        {
            visable = true;
            actived = true;
            newlyOpened = true;
        }
        /// <summary>
        /// 隐藏
        /// </summary>
        public void Close()
        {
            visable = false;
            actived = false;
        }
        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            selectedIndex = 0;
            newlyOpened = true;
        }
        /// <summary>
        /// 重置按钮选中的项
        /// </summary>
        public void ButtonSelectefReset()
        {
            foreach (MenuButton item in items)
            {
                item.Reset();
            }
        }
       
     


    }
}
