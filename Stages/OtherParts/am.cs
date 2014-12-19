using System;
using System.Linq;
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

namespace Stages.OtherParts
{
    /// <summary>
    /// 测试环境物
    /// </summary>
    public class am : StagePart
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            Stage.GameWorld.CreateDecoration(Content.Load<DecorationType>(@"DecorationTypes\Rock1"), new Vector3(500, 0, 0) );
            Stage.GameWorld.CreateDecoration(Content.Load<DecorationType>(@"DecorationTypes\Rock1"), new Vector3(-500, 0, 0) );
            Stage.GameWorld.CreateDecoration(Content.Load<DecorationType>(@"DecorationTypes\Rock1"), new Vector3(0, 500, 0) );
            Stage.GameWorld.CreateDecoration(Content.Load<DecorationType>(@"DecorationTypes\Rock1"), new Vector3(0, -500, 0));
            Stage.GameWorld.CreateDecoration(Content.Load<DecorationType>(@"DecorationTypes\Rock1"), new Vector3(0, 0, 500) );
            Stage.GameWorld.CreateDecoration(Content.Load<DecorationType>(@"DecorationTypes\Rock1"), new Vector3(0, 0, -500) );
        
            base.Initialize();
        }
        /// <summary>
        /// 从该片段继续时进行的处理
        /// </summary>
        public override void StartFormThis()
        {
            Stage.Player.Position = new Vector3(0, 0, 0);//初始位置
            base.StartFormThis();
        }
        /// <summary>
        /// 关卡判断
        /// </summary>
        public override void Touch()
        {

        }
    }
}
