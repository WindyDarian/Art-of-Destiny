using System;
using System.Collections.Generic;

using System.Text;

namespace AODGameLibrary.Menu
{
    public class MenuButton
    {
        private string text;
        /// <summary>
        /// 按钮文字
        /// </summary>
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        public MenuButton(string text)
        {
            this.text = text;
            this.enabled = true;
            this.keyword = text;
            this.options = null;
            this.selectedIndex = 0;
        }
        public MenuButton(string text, bool enabled)
        {
            this.text = text;
            this.enabled = enabled;
            this.keyword = text;
            this.options = null;
            this.selectedIndex = 0;
        }
        public MenuButton(string text, bool enabled,string keyWord)
        {
            this.text = text;
            this.enabled = enabled;
            this.keyword = keyWord;
            this.options = null;
            this.selectedIndex = 0;
        }
        public MenuButton(string text, bool enabled, string keyWord,List<string> selects)
        {
            this.text = text;
            this.enabled = enabled;
            this.keyword = keyWord;
            if (selects != null)
            {

                options = new List<string>(selects);
            }
            else this.options = null;
            this.selectedIndex = 0;
        }
        public MenuButton(string text, bool enabled, string keyWord, List<string> selects,int selectedIndex)
        {
            this.text = text;
            this.enabled = enabled;
            this.keyword = keyWord;
            if (selects != null)
            {

                options = new List<string>(selects);
            }
            else this.options = null;
            this.selectedIndex = selectedIndex;
        }
        bool enabled;
        /// <summary>
        /// 按钮是否激活
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }
        string keyword;
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword
        {
            get { return keyword; }
            set { keyword = value; }
        }
        private List<string> options;
        /// <summary>
        /// 选项
        /// </summary>
        public List<string> Options
        {
            get { return options; }
            set { options = value; }
        }

        private int selectedIndex;
        /// <summary>
        /// 当前选中的选项
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (value > Options.Count - 1)
                {
                    selectedIndex = 0;
                }
                else if (value < 0)
                {
                    selectedIndex = Options.Count - 1;
                }
                else
                {
                    selectedIndex = value;
                }

            }
        }
        public string SelectedOption
        {
            get
            {
                return options[selectedIndex];
            }
        }

        public void Reset()
        {
            selectedIndex = 0;
        }
    }
}
