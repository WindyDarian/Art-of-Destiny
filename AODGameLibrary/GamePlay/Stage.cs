using System;
using System.Collections.Generic;

using System.Text;
using AODGameLibrary.Units;
using Microsoft.Xna.Framework;
using AODGameLibrary.Cameras;
using AODGameLibrary.Weapons;
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.Effects;
using Microsoft.Xna.Framework.Graphics;
using AODGameLibrary.Interface;
using AODGameLibrary.AODObjects;
using AODGameLibrary.GamePlay;
using Microsoft.Xna.Framework.Content;
using AODGameLibrary.Ambient;
using AODGameLibrary.Texts;

namespace AODGameLibrary.GamePlay
{
    /// <summary>
    /// 大地无敌-范若余在2009年8月5日创建,表示一个关卡
    /// </summary>
    public abstract class Stage
    {

        //private Dictionary<string, Event> events = new Dictionary<string, Event>();
        private WorldVars variables;
        /// <summary>
        /// 游戏变量
        /// </summary>
        public WorldVars Variables
        {
            get { return variables; }
        }
        private GameWorld gameWorld;
        /// <summary>
        /// 游戏世界
        /// </summary>
        public GameWorld GameWorld
        {
            get { return gameWorld; }
        }
        public Game Game
        {
            get { return gameWorld.game; }
        }
        //private StagePart currentStagePart;
        private int currentStagePartIndex = -1;

        public int CurrentStagePartIndex
        {
            get { return currentStagePartIndex; }
        }
        /// <summary>
        /// 当前关卡部分
        /// </summary>
        public StagePart CurrentStagePart
        {
            get { return stageParts[currentStagePartIndex]; }
        }
        private List<StagePart> stageParts = new List<StagePart>(5);
        /// <summary>
        /// 当前关卡含有的关卡片段
        /// </summary>
        public List<StagePart> StageParts
        {
            get { return stageParts; }
        }

        private Unit player;
        /// <summary>
        /// 获取或设置玩家
        /// </summary>
        public Unit Player
        {
            get { return player; }
            set
            {
                player = value;
                GameWorld.SetPlayer(value);
            }
        }
        bool failWhenPlayerDied = true;
        /// <summary>
        /// 是否在玩家死亡后失败
        /// </summary>
        public bool FailWhenPlayerDied
        {
            get { return failWhenPlayerDied; }
            set { failWhenPlayerDied = value; }
        }
        bool failed = false;
        /// <summary>
        /// 得到关卡是否已失败
        /// </summary>
        public bool Failed
        {
            get { return failed; }
        }
        bool won = false;
        /// <summary>
        /// 得到关卡是否已经胜利
        /// </summary>
        public bool Won
        {
            get { return won; }
            set { won = value; }
        }
        string failMessage = "";
        /// <summary>
        /// 得到或设置关卡失败后显示的信息
        /// </summary>
        public string FailMessage
        {
            get { return failMessage; }
            set { failMessage = value; }
        }
        string victoryMessage = "任务完成！";
        /// <summary>
        /// 得到或设置胜利之后显示的信息
        /// </summary>
        public string VictoryMessage
        {
            get { return victoryMessage; }
            set { victoryMessage = value; }
        }
        /// <summary>
        /// 关卡是否已经结束
        /// </summary>
        public bool Over
        {
            get
            {
                return won || failed;
            }
        }
        bool overconfirmed= false;
        /// <summary>
        /// 屏幕效果管理器
        /// </summary>
        public ScreenEffectManager ScreenEffectManager
        {
            get
            {
                return gameWorld.ScreenEffectManager;
            }
        }
        ///// <summary>
        ///// 关卡包含的事件
        ///// </summary>
        //public Dictionary<string, Event> Events
        //{
        //    get
        //    {
        //        return events;
        //    }

        //}

        public Stage()
        {
            
        }
        /// <summary>
        /// 读取资源，在Initialize()后调用
        /// </summary>
        public virtual void LoadContent()
        {

        }
        public void LoadAmbient(string stageAmbientAssetName)
        {
            StageAmbient sa = gameWorld.Content.Load<StageAmbient>(stageAmbientAssetName);
            foreach (DecorationEntry de in sa.Decorations)
            {
                if (de.RandomScaleAndRotation)
                {
                    gameWorld.CreateDecoration(Content.Load<DecorationType>(de.DecorationType), de.Position);

                }
                else
                {
                    gameWorld.CreateDecoration(Content.Load<DecorationType>(de.DecorationType), de.Position, de.Scale, de.Rotation);
                }

            }
            gameWorld.SetSkySphere(new SkySphere(sa.SkySphere.SkySphereModel, sa.SkySphere.Texture, gameWorld, sa.SkySphere.Rotation));
        }
        /// <summary>
        /// 关卡总初始化(必须定义玩家和拥有的关卡片段)
        /// </summary>
        public virtual void Initialize()
        {
            if (Player == null)
            {
                throw new ApplicationException("没有设置玩家！");
            }
            if (stageParts.Count == 0)
            {
                throw new ApplicationException("没有载入关卡片段！");
            }
        }
        /// <summary>
        /// 关卡从头开始
        /// </summary>
        public virtual void StartNew()
        {
            if (currentStagePartIndex == -1)
            {
                throw new ApplicationException("没有载入初始关卡片段！");
            }
        }
        /// <summary>
        /// 使关卡尝试触发事件
        /// </summary>
        public virtual void Touch()
        {
            if (CurrentStagePart != null)
            {
                CurrentStagePart.Touch();
            }

        }
        public void Check()
        {
            if (overconfirmed== false)
            {
                if (won)
                {
                    if (IsMessageEnd)
                    {
                        StageWon(this, EventArgs.Empty);
                        overconfirmed = true;
                    }
                }
                else if (failed)
                {
                    if (IsMessageEnd)
                    {
                        StageFailed(this, EventArgs.Empty);
                        overconfirmed = true;
                    }
                }
            }

        }

        /// <summary>
        /// 单位死亡后触发的事件
        /// </summary>
        /// <param name="deadUnit">死去的单位</param>
        public virtual void Event_UnitDied(Unit deadUnit)
        {
            if (CurrentStagePart != null)
            {
                CurrentStagePart.Event_UnitDied(deadUnit);
                if (deadUnit == Player)
                {
                    Event_PlayerDied(deadUnit);
                }
            }
        }
        /// <summary>
        /// 计时器时间到
        /// </summary>
        /// <param name="timer">时间到的计时器</param>
        public virtual void Event_TimerRing(Timer timer)
        {

            if (CurrentStagePart != null)
            {
                CurrentStagePart.Event_TimerRing(timer);
            }

        }
        /// <summary>
        /// 玩家死亡后触发的事件
        /// </summary>
        /// <param name="player"></param>
        public virtual void Event_PlayerDied(Unit player)
        {
            if (CurrentStagePart != null)
            {
                CurrentStagePart.Event_PlayerDied(player);
            }
            if (failWhenPlayerDied)
            {
                Fail("玩家已经死亡");
            }
        }
        /// <summary>
        /// 关卡失败
        /// </summary>
        /// <param name="FailMessage">失败时给出的信息</param>
        public virtual void Fail(string failMessage)
        {
            if (failed == false && won == false)
            {

                failed = true;
                this.failMessage = failMessage;

                
            }

        }
        public virtual void Victory(string victoryMessage)
        {
            if (failed == false && won == false)
            {

                won = true;


                this.victoryMessage = victoryMessage;

               
            }
        }
        /// <summary>
        /// 关卡失败
        /// </summary>
        public event EventHandler StageFailed;
        /// <summary>
        /// 关卡胜利
        /// </summary>
        public event EventHandler StageWon;
        public event StageEventHandlers.StagePartChangeHandler PartChanged;


        void SwitchStagePart(int part)
        {
            if (part < 0 || part > stageParts.Count - 1)
            {

                throw new ApplicationException("该序号的关卡片段不存在");
            }
            else
            {
                if (part <= currentStagePartIndex)
                {
                    throw new ApplicationException("该片段顺序在当前片段之前");
                }
                else
                {
                    this.currentStagePartIndex = part;
                    CurrentStagePart.Stage = this;
                    if (part>0)
                    {
                        GameWorld.InstantMessages.Add(new AODText(GameWorld, "Checkpoint", 3, Color.White, new Vector2(Game.GraphicsDevice.Viewport.Width / 2,
Game.GraphicsDevice.Viewport.Height / 2 - 30), FadeOutState.HalfFade, Vector2.Zero, true, 1.2f));
                    }
            
                }
            }
        }
        /// <summary>
        /// 读取关卡片段（从该片段开始游戏）
        /// </summary>
        /// <param name="stagePart"></param>
        public virtual void StartFromStagePart(int part)
        {
            SwitchStagePart(part);
            CurrentStagePart.StartFormThis();
            CurrentStagePart.Initialize();
        }
        public void NextPart()
        {
            LoadStagePart(currentStagePartIndex + 1);

        }
        /// <summary>
        /// 读取关卡片段（用于游戏中切换）
        /// </summary>
        /// <param name="stagePart"></param>
        public void LoadStagePart(int part)
        {
            
            SwitchStagePart(part);
            PartChanged(part, currentStagePartIndex);
            CurrentStagePart.Initialize();
            
        }
        //public void PreviousPart()
        //{
        //    currentStagePartIndex =(int) MathHelper.Clamp(currentStagePartIndex - 1, 0, stageParts.Count - 1);
        //}

        //public void CheckEvents(GameTime gameTime)
        //{
        //    foreach (Event e in Events.Values)
        //    {
        //        if (e!= null)
        //        {
        //            e.Check();
        //        }
        //    }
        //}
        ///// <summary>
        ///// 添加事件
        ///// </summary>
        ///// <param name="e">事件</param>
        ///// <param name="s">事件名</param>
        //public void AddEvent(Event e,string s)
        //{
        //    if (Events[s] != null)
        //    {

        //        Events[s] = e.Clone();
        //    }
        //    else
        //    {
        //        throw new ArgumentException("事件已存在!");
        //    }
        //}
        /// <summary>
        /// 克隆当前关卡副本并设置游戏世界
        /// </summary>
        /// <param name="gameWorld"></param>
        /// <returns></returns>
        public Stage Clone(GameWorld gameWorld)
        {
            Stage s = (Stage)this.MemberwiseClone();
            s.gameWorld = gameWorld;
            s.variables = gameWorld.Variables;
            return s;
        }
        public Stage GetNewStage()
        {

            return (Stage)Activator.CreateInstance(this.GetType());
        }
        public void AddGameMessage(string str, Color c, float lifeTime)
        {
            GameWorld.AddGameMessage(str, c, lifeTime);
        }
        public void AddRealtimeGameMessage(string str, Color c, float lifeTime)
        {
            GameWorld.AddRealtimeGameMessage(str, c, lifeTime);
        }
        /// <summary>
        /// 创造一个拥有无尽弹药且不会拾取东西的敌对或友好单位
        /// </summary>
        /// <param name="unitType"></param>
        /// <param name="group"></param>
        /// <param name="position"></param>
        public void CreateNPCUnit(UnitType unitType, int group, Vector3 position)
        {
            gameWorld.CreateNPCUnit(unitType, group, position);
        }
        /// <summary>
        /// 创造一个弹药有限且可以拾取东西的玩家单位
        /// </summary>
        /// <param name="unitType"></param>
        /// <param name="group"></param>
        /// <param name="position"></param>
        public void CreatePlayerUnit(UnitType unitType, int group, Vector3 position)
        {
            gameWorld.CreatePlayerUnit(unitType, group, position);
        }
        /// <summary>
        /// 创造一个单位
        /// </summary>
        /// <param name="unitType"></param>
        /// <param name="group"></param>
        /// <param name="position"></param>
        /// <param name="isLootable"></param>
        /// <param name="endlessBullets"></param>
        public void CreateUnit(UnitType unitType, int group, Vector3 position, bool isLootable, bool endlessBullets)
        {
            gameWorld.CreateUnit(unitType, group, position, isLootable, endlessBullets);
        }
        /// <summary>
        /// 创造一个Loot
        /// </summary>
        /// <param name="ls"></param>
        /// <param name="position"></param>
        public void CreateLoot(LootSettings ls, Vector3 position)
        {
            gameWorld.CreateLoot(ls, position);
        }
        public void CreateDecoration(DecorationType dT, Vector3 position)
        {
            gameWorld.CreateDecoration(dT, position);
        }
        public void CreateDecoration(DecorationType dt, Vector3 positon, float scale, Vector3 rotation)
        {
            gameWorld.CreateDecoration(dt, positon, scale, rotation);
        }
        /// <summary>
        /// 创建一个计时器
        /// </summary>
        /// <param name="t">计时器的结束时间</param>
        /// <returns></returns>
        public Timer CreateTimer(float t)
        {
            return gameWorld.CreateTimer(t);
        }
        /// <summary>
        /// 放音乐
        /// </summary>
        /// <param name="songAssetName"></param>
        /// <param name="isRepeating"></param>
        /// <param name="fadeTime"></param>
        public void PlayMusic(string songAssetName, bool isRepeating, float fadeTime)
        {

            gameWorld.PlayMusic(songAssetName, isRepeating, fadeTime);



        }
        /// <summary>
        /// 使所有消息消失
        /// </summary>
        public void ClearMessages()
        {
            gameWorld.ClearMessages();
        }
        public void AddTooltipMessage(string message, Color color, float lifeTime)
        { gameWorld.ShowTooltipMessage(message, color, lifeTime); }
        /// <summary>
        /// 添加位置标记
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Mark AddPositionMark(Vector3 position)
        {
            return gameWorld.AddPositionMark(position);
        }

        /// <summary>
        /// 得到当前单位集合
        /// </summary>
        public List<Unit> Units
        {
            get
            {
                return gameWorld.units;
            }
        }
        public ContentManager Content
        {
            get
            {
                return gameWorld.Content;
            }
        }
        public bool IsMessageEnd
        {
            get
            {
                if (gameWorld.MessagesInLine.Count == 0&& gameWorld.FlyingMessages.Count== 0 && gameWorld.FlyingMessagesInLine.Count==0)
                {
                    return true;

                }
                return false;
            }

        }
        /// <summary>
        /// 组中活着的敌人
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public List<Unit> AliveUnitsInUnitGroup(int group)
        {
            return gameWorld.AliveUnitsInUnitGroup(group);
        }
        public void StopMusic()
        {
            gameWorld.StopMusic();
        }
        public void ShowInstantMessage(string message, Vector2 position, Vector2 velocity, Color color, float lifeTime)
        {
            gameWorld.ShowInstantMessage(message, position, velocity, color, lifeTime);
        }
        public void SetSkySphere(string texture)
        {
            gameWorld.SetSkySphere(texture);
        }
        public void AddFlyingMessage(String message, Color color)
        {

            gameWorld.AddFlyingMessage(message, color);
        }

        /// <summary>
        /// 得到或设置是否隐藏UI界面
        /// </summary>
        public bool HideUI
        {
            get
            {
                return gameWorld.HideUI;
            }
            set
            {
                gameWorld.HideUI = value;
            }
        }
        public void ClearLoots()
        {
            gameWorld.ClearLoots();
        }
        public event PlayerLoadHandler Event_LoadPlayer;
        public Unit LoadPlayer(int Group ,Vector3 position)
        {
            return Event_LoadPlayer(Group, true, position);
        }
        public event EventHandler Event_SavePlayer;
        /// <summary>
        /// 过关之后保存玩家信息
        /// </summary>
        public void SavePlayer()
        {
            Event_SavePlayer(this, EventArgs.Empty);
        }
        public event EnableStageHandler Event_EnableStage;
        public void EnableStage(int i)
        {
            Event_EnableStage(i);
        }
    }
    public delegate Unit PlayerLoadHandler(int Group,bool Player,Vector3 position);
    public delegate void EnableStageHandler(int i);
}
