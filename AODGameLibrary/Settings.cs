using System;
using System.Collections.Generic;

using System.Text;

namespace AODGameLibrary
{
    [Serializable]
    /// <summary>
    /// 表示一系列游戏设置，由大地无敌-范若余在2009年10月24日建立
    /// </summary>
    public class Settings
    {
        private List<SettingEntry> settingsList = new List<SettingEntry>();
        /// <summary>
        /// 设置列表
        /// </summary>
        public List<SettingEntry> SettingsList
        {
            get { return settingsList; }
            set { settingsList = value; }
        }
        /// <summary>
        /// 通过关键字查找存在的设置项
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回第一个符合的设置项</returns>
        public SettingEntry SettingFromKeyword(string keyword)
        {
            foreach (SettingEntry e in settingsList)
            {
                if (e.Keyword == keyword)
                {
                    return e;
                }

            }
            throw new ApplicationException("无法找到设置项 " + keyword);
            
        }
        public Settings Clone()
        {
            return (Settings)this.MemberwiseClone();
        }
    }
    [Serializable]
    /// <summary>
    /// 表示一项游戏设置，由大地无敌-范若余在2009年10月24日建立
    /// </summary>
    public class SettingEntry
    {
        private string keyword;
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword
        {
            get { return keyword; }
            set { keyword = value; }
        }
        private int settingValue;
        /// <summary>
        /// 整数值
        /// </summary>
        public int SettingValue
        {
            get { return settingValue; }
            set { settingValue = value; }
        }
    }
}
