using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


using AODGameLibrary.Cameras;

namespace AODGameLibrary
{
    /// <summary>
    /// 获取玩家输入的类（静态），由大地无敌在2009年9月28日重建
    /// 大地无敌-范若余由AOD导入AR 在 2011/11/3 并改名为InputState(由AREngine.InputState)
    /// 大地无敌-范若余2011/12/3增加功能
    /// </summary>
    public static class InputState
    {
        public enum Controller
        {
            Keyboard,
            Pad,
        }

        public static Controller CurrentController = Controller.Keyboard;

        private static Vector2 currentMousePosition;
        /// <summary>
        /// 当前鼠标位置
        /// </summary>
        public static Vector2 CurrentMousePosition
        {
            get { return currentMousePosition; }
        }

        private static KeyboardState previousKeyboardState = Keyboard.GetState();

        public static KeyboardState PreviousKeyboardState
        {
            get { return InputState.previousKeyboardState; }
        }
        private static KeyboardState currentKeyboardState = Keyboard.GetState();

        public static KeyboardState CurrentKeyboardState
        {
            get { return InputState.currentKeyboardState; }
        }

        private static MouseState previousMouseState = Mouse.GetState();

        public static MouseState PreviousMouseState
        {
            get { return InputState.previousMouseState; }
        }

        private static MouseState currentMouseState = Mouse.GetState();

        public static MouseState CurrentMouseState
        {
            get { return InputState.currentMouseState; }
        }

        private static GamePadState previousPadState;

        public static GamePadState PreviousPadState
        {
            get { return InputState.previousPadState; }
        }

        private static GamePadState currentPadState;

        public static GamePadState CurrentPadState
        {
            get { return InputState.currentPadState; }
        }


        public static KeyboardState KeyboardState
        {
            get
            {
                return CurrentKeyboardState;
            }
        }
        /// <summary>
        /// 更新输入 由ARGameBase调用
        /// </summary>
        /// <param name="game"></param>
        public static void UpdateInput(Game game)
        {

            previousKeyboardState = currentKeyboardState;
            previousMouseState = currentMouseState;
            previousPadState = currentPadState;
            //if (previousKeyboardState == null)
            //{
            //    previousKeyboardState = Keyboard.GetState();
            //}
            if (game.IsActive)
            {

                currentKeyboardState = Keyboard.GetState();
                currentMouseState = Mouse.GetState();
                currentPadState = GamePad.GetState(PlayerIndex.One);
            }

            //if (currentKeyboardState.GetPressedKeys().Length >0)
            //{
            //    CurrentController = Controller.Keyboard;
            //}
            //else if (currentPadState.IsButtonDown(Buttons.A)||currentPadState.IsButtonDown(Buttons.B)||currentPadState.IsButtonDown(Buttons.X)||currentPadState.IsButtonDown(Buttons.Y))
            //{

            //}

        }
        /// <summary>
        /// 是否按了某个键
        /// </summary>
        /// <param name="key"></param>

        public static bool IsKeyPressed(Keys key)
        {

            if (previousKeyboardState.IsKeyUp(key) && CurrentKeyboardState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 是否按住某个键
        /// </summary>
        /// <param name="key"></param>

        public static bool IsKeyDown(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }
        /// <summary>
        /// 是否按了某个鼠标键
        /// </summary>
        /// <param name="button"></param>

        public static bool IsMouseButtonPressed(MouseButton button)
        {
            ButtonState cbs = GetCurrentButtonState(button);
            ButtonState pbs = GetPreviousButtonState(button);

            if (cbs == ButtonState.Pressed && pbs == ButtonState.Released)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// 是否按住某个鼠标键
        /// </summary>
        /// <param name="button"></param>

        public static bool IsMouseButtonDown(MouseButton button)
        {
            ButtonState cbs = GetCurrentButtonState(button);

            if (cbs == ButtonState.Pressed)
            {
                return true;
            }

            return false;
        }
        public static ButtonState GetCurrentButtonState(MouseButton button)
        {
            ButtonState cbs = ButtonState.Released;
            switch (button)
            {
                case MouseButton.LeftButton:
                    cbs = currentMouseState.LeftButton;
                    break;
                case MouseButton.MiddleButton:
                    cbs = currentMouseState.MiddleButton;
                    break;
                case MouseButton.RightButton:
                    cbs = currentMouseState.RightButton;
                    break;
                case MouseButton.XButton1:
                    cbs = currentMouseState.XButton1;
                    break;
                case MouseButton.XButton2:
                    cbs = currentMouseState.XButton2;
                    break;
                default:
                    break;
            }
            return cbs;
        }
        public static ButtonState GetPreviousButtonState(MouseButton button)
        {
            ButtonState pbs = ButtonState.Released;
            switch (button)
            {
                case MouseButton.LeftButton:
                    pbs = previousMouseState.LeftButton;
                    break;
                case MouseButton.MiddleButton:
                    pbs = previousMouseState.MiddleButton;
                    break;
                case MouseButton.RightButton:
                    pbs = previousMouseState.RightButton;
                    break;
                case MouseButton.XButton1:
                    pbs = previousMouseState.XButton1;
                    break;
                case MouseButton.XButton2:
                    pbs = previousMouseState.XButton2;
                    break;
                default:
                    break;
            }
            return pbs;
        }
        /// <summary>
        /// 获得鼠标对应投影在屏幕上的射线
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="viewport"></param>

        public static Ray GetMouseRay(Camera camera, Viewport viewport)
        {
            Vector3 near = new Vector3(currentMouseState.X, currentMouseState.Y, 0.0f);
            Vector3 far = new Vector3(currentMouseState.X, currentMouseState.Y, 1.0f);
            Vector3 near_transformed = viewport.Unproject(near, camera.Projection, camera.View, Matrix.Identity);
            Vector3 far_transformed = viewport.Unproject(far, camera.Projection, camera.View, Matrix.Identity);
            Vector3 direction = Vector3.Normalize(far_transformed - near_transformed);
            return new Ray(near_transformed, direction);




        }
        /// <summary>
        /// 获得鼠标在XZ平面上的投影点
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="viewport"></param>

        public static Vector3 GetMousePointXZ(Camera camera, Viewport viewport)
        {
            Ray r1 = GetMouseRay(camera, viewport);
            if (r1.Direction.Y != 0)
            {

                Vector3 t = r1.Position + r1.Position.Y / (-Vector3.Dot(r1.Direction, Vector3.Up)) * r1.Direction;
                return new Vector3(t.X, 0, t.Z);
            }
            else return new Vector3(r1.Position.X, 0, r1.Position.Z);

        }

        public static bool IsPadButtonDown(Buttons button)
        {
            if (!CurrentPadState.IsConnected) return false;

            return currentPadState.IsButtonDown(button);
        }

        public static bool IsPadButtonPressed(Buttons button)
        {
            if (!CurrentPadState.IsConnected) return false;

            return currentPadState.IsButtonDown(button) && !previousPadState.IsButtonDown(button);
        }
    }
    /// <summary>
    /// 鼠标按键的枚举，由大地无敌在2009年9月29日建立
    /// </summary>
    public enum MouseButton
    {
        LeftButton,
        MiddleButton,
        RightButton,
        XButton1,
        XButton2,
    }
}
