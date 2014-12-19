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
using Microsoft.Xna.Framework.Graphics;
using AODGameLibrary.Interface;
using AODGameLibrary.AODObjects;
using AODGameLibrary.GamePlay;
using Microsoft.Xna.Framework.Content;
using AODGameLibrary.Texts;
using Microsoft.Xna.Framework.Input;

namespace AODGameLibrary
{
    /// <summary>
    /// 游戏常数类,存放各类常数,由大地无敌-范若余在2009年8月23日添加
    /// </summary>
    public class GameConsts
    {
        ///// <summary>
        ///// 存放AODModelType的目录
        ///// </summary>
        //public const string AODModelTypesFolder = @"AODModelTypes\";
        ///// <summary>
        ///// 存放DModelType的目录
        ///// </summary>
        //public const string DModelTypesFolder = @"AODModelTypes\DModelTypes\";
        //public const string AISettingsFolder = @"AISettings\";
        //public const string EffectsFolder = @"effects\";
        //public const string InterfaceFolder = @"Interface\";
        //public const string MissileTypesFolder = @"MissileTypes\";
        /// <summary>
        /// 单位间碰撞检测的最大距离
        /// </summary>
        public const float BoundingDistance = 1500;
        /// <summary>
        /// 声音播放的最大距离
        /// </summary>
        public const float SoundDistance = 5000;
        public readonly static Vector2 LootInfPosition = new Vector2(300, 350);
        public const float LootInfSpeed = 100;
        public static Random Random = new Random();
        public readonly static Vector2 LootTipPosition = new Vector2(100, 350);
        public const float PlayerSightDistance = 2000;
        public const float PlayerSightStartRadius = 1;
        public const float PlayerSightEndRadius = 100;
        public const float BulletCheckingRange = 4000;
        public const string SettingsDirectory = @"WTF";
        public const string GameSaveDirectory = @"Save";
        public const string SettingsFile = @"WTF\Settings.aod";
        public const string CheckpointFile = @"Save\SavedCheckPoint.aod";
        public const string MenuSelectSound = @"Audio\menuselect";
        public const string MenuMoveSound = @"Audio\menumove";
        public const string GameWinMusic = @"Audio\Evan_LE_NY_-_You_Win";
        public const string GameFailMusic = "";
        public const float Sound3DScale = 200;
        /// <summary>
        /// 游戏失败后的退出间隔
        /// </summary>
        public const float FailEndTime = 5.0f;
        /// <summary>
        /// 游戏胜利后的退出间隔
        /// </summary>
        public const float VictoryEndTime = 13.5f;
        public const string MarksTexture = @"Textures\Mark";
        public const float  ModelDrawDistance = 8000;
        public const float ParticleDrawDistance = 6000;
        /// <summary>
        /// 游戏计算范围
        /// </summary>
        public const float GameViewDistance = 4000;
        public const float MaxLootNum = 40;
        //public static Keys Key_Missile = Keys.F;
        //public static Keys Key_Skill = Keys.F;


    }
}
