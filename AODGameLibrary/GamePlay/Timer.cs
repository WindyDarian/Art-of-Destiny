using System;
using System.Collections.Generic;

using System.Text;
using Microsoft.Xna.Framework;

namespace AODGameLibrary.GamePlay
{
    public class Timer
    {
        bool paused;
        float currentTime;
        /// <summary>
        /// 计时器当前时间
        /// </summary>
        public float CurrentTime
        {
            get
            {
                return currentTime;
            }
            set
            {
                currentTime = MathHelper.Clamp(value, 0, endTime);
            }
        }
        float endTime;
        /// <summary>
        /// 计时器结束时间
        /// </summary>
        public float EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }
        bool rung = false;
        /// <summary>
        /// 计时器是否已触发事件
        /// </summary>
        public bool Rung
        {
            get
            {
                return rung;
            }
            set
            {
                rung = value;
            }
        }
        
        bool isEnd;
        public bool IsEnd
        {
            get 
            {
                return isEnd;
            }
        }
        public Timer(float endTime)
        {
            this.endTime = endTime;
            this.isEnd = false;
            this.currentTime = 0;
        }
        public Timer()
        {
            this.endTime = 0;
            this.isEnd = false;
            this.currentTime = 0;
        }
        public void Update(GameTime gameTime)
        {
            if (isEnd == false && paused == false)
            {
                currentTime = MathHelper.Clamp(currentTime + (float)gameTime.ElapsedGameTime.TotalSeconds, 0, endTime);
                if (currentTime >= endTime && endTime != 0)
                {
                    isEnd = true;
                }
            }

        }
        
        public void Reset()
        {
            isEnd = false;
            rung = false;
            currentTime = 0;
        }
        public void Pause()
        {
            paused = true;
        }
        public void Play()
        {
            paused = false;
        }
        /// <summary>
        /// 获取倒计时剩余时间
        /// </summary>
        /// <returns></returns>
        public string GetTimeRemainsText()
        {
            int x = (int)(endTime - currentTime);
            int f = x / 60;
            int m = x % 60;
            string s;
            if (f!= 0)
            {
                s = f + " 分 " + m + " 秒";
            }
            else
            {
                s = m + " 秒";
            }
            return s;
        }

    }
}
