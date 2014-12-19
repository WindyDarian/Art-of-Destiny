using System;
using System.Collections.Generic;

using System.Text;
using AODGameLibrary.GamePlay;

namespace AODGameLibrary.GamePlay
{
    /// <summary>
    /// 表示一个在游戏内触发的事件，由大地无敌-范若余在2009年8月7日创建
    /// </summary>
    public class Event
    {
        public GameWorld gameWorld;
        /// <summary>
        /// 事件是否已关闭
        /// </summary>
        bool closed = false;
        /// <summary>
        /// 事件是否已关闭
        /// </summary>
        public bool IsClosed
        {
            get
            {
                return closed;
            }
            set
            {
                closed = value;
            }
        }

        bool doOnce;
        /// <summary>
        /// 是否只发生一次
        /// </summary>
        public bool DoOnce
        {
            get
            {
                return doOnce;
            }
            set
            {
                doOnce = value;
            }
        }
        public void  Close()
        {
            closed = true;
        }

        /// <summary>
        /// 判断并触发条件满足的开启事件
        /// </summary>
        public void Check()
        {
            if (CheckCondition() && (IsClosed == false))
            {
                Occur();
            }
        }
        
        /// <summary>
        /// 判断条件是否合适
        /// </summary>
        /// <returns></returns>
        bool CheckCondition()
        {
            return false;
        }
        /// <summary>
        /// 触发事件
        /// </summary>
        public void Occur()
        {
            if (DoOnce)
            {
                this.Close();
            }
        }
        public Event Clone()
        {
            return (Event)this.MemberwiseClone();
        }
    
    }
}
