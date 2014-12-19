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
using AODGameLibrary.Weapons;
using AODGameLibrary;
using AODGameLibrary.Cameras;
using AODGameLibrary.Models;
using AODGameLibrary.AIs;
using AODGameLibrary.Gamehelpers;
using AODGameLibrary.AODObjects;
using AODGameLibrary.GamePlay;
using AODGameLibrary.CollisionChecking;

namespace AODGameLibrary.Units
{
    /// <summary>
    /// 战斗单位，是最早创建的类之一
    /// </summary>
    public class Unit:VioableUnit
    {
        #region 声明字段


        ///// <summary>
        ///// 单位的名字
        ///// </summary>
        //public string Name = "未知目标";
        /// <summary>
        /// 驾驶员名字
        /// </summary>
        public string RiderName = "";
        /// <summary>
        /// move()方法向前移动中的最大推力大小，即在达到最大功率前的推力,应大于frictionForce
        /// </summary>
        public float forwardThrustForce; 
        /// <summary>
        /// 转向速率,转向时每秒旋转角度
        /// </summary>
        public float angularRate;
        /// <summary>
        /// 前推力最大功率,数值上等于frictionForce与最大向前速度的乘积
        /// </summary>
        public float maxPower;


        /// <summary>
        /// 设定单位是否由玩家控制
        /// </summary>
        public bool isPlayerControlling = false;
  
        /// <summary>
        /// 编号为1~4单位的武器
        /// </summary>
        public List<Weapon> weapons = new List<Weapon>(4);
        /// <summary>
        /// 导弹武器列表
        /// </summary>
        public List<MissileWeapon> missiles = new List<MissileWeapon>(4);
        /// <summary>
        /// 技能列表
        /// </summary>
        public List<Skill> skills = new List<Skill>(8);

        /// <summary>
        /// 0~4之间，正在使用的武器
        /// </summary>
        int currentWeaponNumber;

        public int CurrentWeaponNumber
        {
            get { return currentWeaponNumber; }
            set { currentWeaponNumber = value; }
        }
        /// <summary>
        /// 0~4之间，正在使用的导弹
        /// </summary>
        int currentMissileWeaponNumber;

        public int CurrentMissileWeaponNumber
        {
            get { return currentMissileWeaponNumber; }
            set { currentMissileWeaponNumber = value; }
        }
        /// <summary>
        /// 当前切换到的技能
        /// </summary>
        int currentSkillNumber;

        public int CurrentSkillNumber
        {
            get { return currentSkillNumber; }
            set
            {
                if (value <= skills.Count - 1 && value >= 0)
                {
                    currentSkillNumber = value;
                }
                else
                {
                    currentSkillNumber = 0;
                }
            }
        }
        public Skill CurrentSkill
        {
            get
            {
                //try
                //{
                if (currentSkillNumber <= skills.Count - 1)
                {

                    return skills[currentSkillNumber];
                }
                else
                {
                    currentSkillNumber = 0;
                    return null;
                }
                //}
                //catch
                //{
                //    currentSkillNumber = 0;
                //    return null;
                //}
            }

        }
        /// <summary>
        /// 技能相对模型位置
        /// </summary>
        List<Vector3> skillPositions
        {
            get
            {
                return Model.SkillPositions; 
            }
        }
        /// <summary>
        /// 技能绝对位置
        /// </summary>
        public Vector3 SkillPosition(int n)
        {
            return Vector3.Transform(skillPositions[n], ModelWorld);
        }
        /// <summary>
        /// 武器相对模型位置
        /// </summary>
        List<Vector3> weaponPositions
        {
            get
            {
                return Model.WeaponPositions;
            }
        }
        /// <summary>
        /// 武器绝对位置
        /// </summary>
        public Vector3 GetTransformedWeaponPosition(int n)
        {
            return Vector3.Transform(weaponPositions[n], ModelWorld);
        }
        public Vector3 GetWeaponPosition(int n)
        {
            return weaponPositions[n];
     
        }
        /// <summary>
        /// 最大武器数
        /// </summary>
        public float maxWeaponNum = 4;
        public float maxMissileWeaponNum = 4;
        public float maxSkillNum = 3;

 
        /// <summary>
        /// 单位的AI
        /// </summary>
        public AI unitAI;

        Unit target;
        /// <summary>
        /// 单位射出导弹的目标(仅在玩家操作时有效,电脑操作时目标取决于AI的目标)
        /// </summary>
        public Unit Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
                unitAI.ChangeTarget();
                if (value == null)
                {

                    if (this == gameWorld.CurrentStage.Player && gameWorld.PlayerLockedTarget != null)
                    {
                        gameWorld.PlayerLockedTarget = null;
                    }
                }
            }
        }
        
 
        #region AI相关
        /// <summary>
        /// 是否正在射击,每次射击后都会变为false
        /// </summary>
        bool isShotting = false;
        /// <summary>
        /// 要旋转的方向
        /// </summary>
        Vector3 rotateT = Vector3.Zero;
        /// <summary>
        /// 是否朝前移动
        /// </summary>
        bool isEngineOn = false;
        bool isShottingMissile = false;
        bool isAIControlling = true;
        /// <summary>
        /// 是否正在由AI控制，优先级比玩家控制低
        /// </summary>
        public  bool IsAIControlling
        {
            get
            {
                return isAIControlling;
            }
            set
            {
                isAIControlling = value;
            }
        }
        #endregion 

        /// <summary>
        /// 属于的GameWorld
        /// </summary>
        GameWorld gameWorld;
        ///<summary>
        ///获取game
        ///</summary>
        Game game;
        /// <summary>
        /// 是否正在由事件控制旋转
        /// </summary>
        bool isRotating= false;
        /// <summary>
        /// 事件控制旋转方向
        /// </summary>
        Vector3 rotationDirection;

        bool isMoving = false;
        /// <summary>
        /// 是否正在由事件控制移动
        /// </summary>
        public bool IsMoving
        {
            get
            {
                return isMoving;
            }
           
        }

        bool isUsingSkill = false;
        public bool IsUsingSkill
        {
            get
            {
                return isUsingSkill;
            }
        }

        Skill usingSkill;
        public Skill UsingSkill
        {
            get
            {
                return usingSkill;
            }
        }

        /// <summary>
        /// 移动目标
        /// </summary>
        Vector3 movingPosition;

        /// <summary>
        /// 是否多技能单位
        /// </summary>
        public bool SkillControlUnit;

        #region 已抛弃的代码
        /*

                ///// <summary>
        ///// 表示由此单位射出的弹药
        ///// </summary>
        //public List<Bullet> Bullets;
                ///// <summary>
        ///// 单位当前的速度
        ///// </summary>
        //public Vector3 velocity = Vector3.Zero;
                ///// <summary>
        ///// 单位模型
        ///// </summary>
        ////public AODModel model;
        ///// <summary>
        ///// 单位位置
        ///// </summary>
        //public Vector3 position = Vector3.Zero;
        ///// <summary>
        ///// 单位旋转矩阵
        ///// </summary>
        //public Matrix rotation = Matrix.Identity;
                 ///// <summary>
        ///// 侧推力大小，即飞船在向侧面平移或后退时的推力大小,应大于frictionForce
        ///// </summary>
        //public float sideThrustForce;  
         ///// <summary>
        ///// 模型缩放值
        ///// </summary>
        //public float scale;
        ///// <summary>
        ///// 模型Y轴旋转角度
        ///// </summary>
        //public float modelRotationY;
        */
        #endregion

        public Vector3 ShotDirection
        {
            get
            {
                if (Velocity == Vector3.Zero)
                {
                    return Face;
                }
                else return Vector3.Normalize(Velocity + Face * CurrentWeapon.basicSpeed);
            }
        }
        public Weapon CurrentWeapon
        {
            get
            {
                if (weapons.Count==0)
                {
                    return null;
                }
                return weapons[currentWeaponNumber];
            }
            set
            {
                weapons[currentWeaponNumber] = value;
            }
        }
        public MissileWeapon CurrentMissileWeapon
        {
            get
            {
                if (missiles.Count > 0 && currentMissileWeaponNumber < missiles.Count)
                {

                    return missiles[currentMissileWeaponNumber];
                }
                else return null;
            }
            set
            {
                missiles[currentMissileWeaponNumber] = value;
            }
        }
        public float MaxSpeed
        {
            get
            {
                if (FrictionForce != 0)
                {

                    return maxPower / FrictionForce;
                }
                else return 1000000.0f;
            }
        }

        /// <summary>
        /// 是否可以从废墟上拾取物体
        /// </summary>
        private bool isLootable = true;
        /// <summary>
        /// 是否可以从废墟上拾取物体
        /// </summary>
        public bool IsLootAble
        {
            get { return isLootable; }
            set { isLootable = value; }
        }

        /// <summary>
        /// 是否有无尽弹药
        /// </summary>
        private bool endlessBullets = false;
        /// <summary>
        /// 是否有无尽弹药
        /// </summary>
        public bool EndlessBullets
        {
            get { return endlessBullets; }
            set { endlessBullets = value; }
        }
        /// <summary>
        /// 正准备Loot的东西
        /// </summary>
        public LootItem lootingItem;
        public List<LootEntry> loots = new List<LootEntry>(3);
      
        /// <summary>
        /// 在单位前方视线中的（只对玩家)
        /// </summary>
        public List<Unit> facingUnits = new List<Unit>(10);
        /// <summary>
        /// 面前的东西(只对玩家）
        /// </summary>
        public ObjectUnit facingUnit;
        /// <summary>
        /// 前方的地方单位数量
        /// </summary>
        public int enemyUnintsInFront = 0;
        private UnitType unitType;
        /// <summary>
        /// 所属单位类型
        /// </summary>
        public UnitType UnitType
        {
            get { return unitType; }
            set { unitType = value; }
        }
        bool b0 = false;
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化一个单位
        /// </summary>
        /// <param name="game">指定使用的Game类</param>
        /// <param name="unitType">指定单位类型</param>
        public Unit(GameWorld gameWorld,UnitType unitType)
        {
            this.Group = 0;
            this.game = gameWorld.game;
            #region 初始化
           
            this.UnitState = UnitState.alive;
     
            LoadType(gameWorld, unitType);


            #endregion
            
        }
        public Unit(GameWorld gameWorld, UnitType unitType, int group, Vector3 position, bool isLootable, bool endlessBullets)
        {
            this.Group = group;
            this.game = gameWorld.game;
            this.Position = position;
            this.isLootable = isLootable;
            this.endlessBullets = endlessBullets;
            #region 初始化
           

            LoadType(gameWorld, unitType);


            #endregion

        }
        protected virtual void LoadType(GameWorld gameWorld, UnitType unitType)
        {
            this.unitType = unitType;
            this.Name = unitType.Name;
            this.UnitState = UnitState.alive;
            this.unitAI = new RegularAI(this, gameWorld, unitType.AISettings);
            
            this.gameWorld = gameWorld;
            this.maxWeaponNum = unitType.MaxWeaponNum;
            this.maxMissileWeaponNum = unitType.MaxMissileWeaponNum;
            this.maxSkillNum = unitType.MaxSkillNum;
            this.currentMissileWeaponNumber = 0;
            this.currentWeaponNumber = 0;
            this.Scale = unitType.Scale;
            this.modelRotation = AODGameLibrary.Helpers.RandomHelper.RotationVector3ToMatrix(unitType.ModelRotation);
            if (unitType.Modelname != "")
                Model = new AODModel(gameWorld, gameWorld.game.Content.Load<AODModelType>(unitType.Modelname), Position, ModelRotation, Scale);
            else Model = null;
            this.Mass = unitType.Mass;
            this.maxPower = unitType.MaxPower;
            this.forwardThrustForce = unitType.ThrustForce;
            this.FrictionForce = unitType.FrictionForce;
            //this.sideThrustForce = unitType.sideThrustForce;
            this.angularRate = unitType.AngularRate;
            this.MaxArmor = unitType.MaxArmor;
            this.MaxShield = unitType.MaxShield;
            this.Armor = this.MaxArmor;
            this.Shield = this.MaxShield;
            this.ShieldRestoreRate = unitType.ShieldRestoreRate;
            this.ShieldRestoreTime = unitType.ShieldRestoreTime;
            this.ShieldRestoreTimeleft = this.ShieldRestoreTime;
            this.ArmorRestoreRate = unitType.ArmorRestoreRate;
            base.Bounding = unitType.Bounding;
            base.Heavy = unitType.Heavy;
            for (int i = 0; i < 4; i++)
            {
                if (unitType.Weapons[i] != "")
                {
                    this.weapons.Add(new Weapon(gameWorld, gameWorld.game.Content.Load<WeaponType>(unitType.Weapons[i]), this));

                }
                if (unitType .MissileWeapons[i]!= "")
                {
                    this.missiles.Add(new MissileWeapon(gameWorld, gameWorld.game.Content.Load<MissileWeaponType>(unitType.MissileWeapons[i])));

                }
            }
          
            foreach (string skill in unitType.Skills)
            {
                if (skill != "")
                {
                    AddSkill(skill);
                }
            }
            this.loots.AddRange(unitType.Loots);
            //this.weapons[0] = new Weapon(gameWorld, game.Content.Load<WeaponType>("WeaponTypes\\PlanetaryAssaultGun"), this,new Vector3(4.5f, 2, -1.4f),new Vector3(-4.5f, 2, -1.4f));
            //this.missiles[0] = new MissileWeapon(gameWorld, gameWorld.game.Content.Load<MissileWeaponType>(@"MissileWeaponTypes\Ghost"));




        }
        void ResetCollision()
        {

        }
        #endregion

        #region 单位的更新
        /// <summary>
        /// 执行单位位置等的更新
        /// </summary>
        /// <param name="gameTime">请赋予一个Gametime</param>
        public override void Update(GameTime gameTime)
        {   
            
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector3 thrust = Vector3.Zero;// 推力
            float rotationX = 0.0f;
            float rotationY = 0.0f;
            float rotationZ = 0.0f;

            if (UnitState == UnitState.alive)
            {
               
                #region 临时变量

                bool moved = false;//表示该单位在该桢是否已移动

                Vector3 frictionDirection = Vector3.Zero;//以飞船为原点，表示摩擦力作用方向
                Vector3 sideForceDirection = Vector3.Zero;//以飞船为原点，表示侧推力作用方向
                Vector3 frontForceDirection = Vector3.Zero;//以飞船为原点，表示前推力作用方向，若在向前加速则为Vector3.Forward否则为Vector3.Zero

                #endregion

                #region 控制状态判断

                if (isMoving)
                {
                    if (movingPosition != Position)
                    {

                        if (Vector3.Dot(Vector3.Normalize(movingPosition - this.Position), this.Face) <= 0.99)
                        //if (Vector3.Normalize(movingPosition - this.position) != this.Face)
                        {
                            this.RotateRealWorldFlame(movingPosition);
                        }
                        else
                        {
                            this.EngineOnFlame();
                        }
                    }
                    if (Vector3.Distance(movingPosition, Position) <= Velocity.Length())
                    {
                        isMoving = false;
                    }
                }
                else if (isRotating)
                {
                    if (rotationDirection != Position)
                    {
                        if (Vector3.Dot(Vector3.Normalize(movingPosition - this.Position), this.Face) <= 0.99)
                        //if (Vector3.Normalize(movingPosition - this.position) != this.Face)
                        {
                            this.RotateRealWorldFlame(rotationDirection);

                        }
                        else
                        {
                            isRotating = false;
                        }
                    }
                    else
                    {
                        isRotating = false;
                    }

                }
                else if (isPlayerControlling)
                {
                    PlayerInput(elapsedTime);

                }
                else if (unitAI != null && IsAIControlling)
                {

                    unitAI.Update(gameTime);
                }

                #endregion


                #region 计算推力
                if (isEngineOn)
                {
                    frontForceDirection = Vector3.Normalize(Vector3.TransformNormal(Vector3.Forward, Rotation));
                }

                if (moved == false)
                {


                    if (Vector3.Dot(Velocity, frontForceDirection * forwardThrustForce) < maxPower)
                    {
                        thrust = frontForceDirection * forwardThrustForce;
                    }
                    else if (Vector3.Dot(Velocity, frontForceDirection) != 0)
                    {
                        thrust = maxPower / Vector3.Dot(Velocity, frontForceDirection) * frontForceDirection;
                    }
                    moved = true;

                }
                #endregion

                #region 旋转
                if (playerRotating)
                {
                    if (playerRotateValue.Length() >= 1)
                        playerRotateValue = Vector2.Normalize(playerRotateValue);
                    
                    rotationY = -MathHelper.ToDegrees((float)Math.Asin(Math.Sin(MathHelper.ToRadians(angularRate) * elapsedTime) * playerRotateValue.X));
                    rotationX = -MathHelper.ToDegrees((float)Math.Asin(Math.Sin(MathHelper.ToRadians(angularRate) * elapsedTime) * playerRotateValue.Y));


                }
                if (playerRolling)
                {
                    rotationZ -= MathHelper.Clamp(playerRollValue, -1f, 1f) * angularRate * elapsedTime;
                }

                rotation = Matrix.CreateRotationX(MathHelper.ToRadians(rotationX)) * rotation;
                rotation = Matrix.CreateRotationY(MathHelper.ToRadians(rotationY)) * rotation;
                rotation = Matrix.CreateRotationZ(MathHelper.ToRadians(rotationZ)) * rotation;

                if (rotateT != Vector3.Forward && rotateT != Vector3.Zero)
                {

                    Vector3 s;
                    Vector3 rt = rotateT;
                    float m = MathHelper.ToRadians(angularRate * elapsedTime);
                    if (rt == Vector3.Backward)
                    {
                        rt = Vector3.Right;
                    }


                    float a = (float)Math.Acos(Vector3.Dot(rt, Vector3.Forward));
                    if (m >= a)
                    {
                        s = rt;

                    }
                    else
                    {
                        float b = (MathHelper.Pi - a) / 2;
                        float c = (MathHelper.Pi - m - b);

                        float h = -(float)(Math.Sin(b) / Math.Sin(c) * Math.Cos(m));

                        s = Vector3.Normalize(Vector3.Lerp(Vector3.Forward, rt, (h + 1) / (rt.Z + 1)));
                    }
                    if (s != Vector3.Forward)
                    {

                        Matrix rr = Matrix.CreateFromAxisAngle(Vector3.Normalize(Vector3.Cross(s, Vector3.Forward)), -(float)Math.Acos(Vector3.Dot(Vector3.Forward, s)));
                        rotation = rr * rotation;
                    }
                   

                }
                #endregion


            }


            //造成推力
            base.GetThrust(thrust);




            #region 清空临时变量
            rotateT = Vector3.Zero;

            isEngineOn = false;
            playerRolling = false;
            playerRotating = false;

            playerRollValue = 0;
            playerRotateValue = Vector2.Zero;
 
            #endregion
            base.Update(gameTime);
        }

        bool playerRolling = false;
        bool playerRotating = false;
        float playerRollValue;
        Vector2 playerRotateValue;
        //public bool IsMovingPreventedFrame = false;
        Vector2 prevRotation = Vector2.Zero;
        bool keepEngineOn = true;
        void PlayerInput(float elapsedTime)
        {
            MouseState mousestate = InputState.CurrentMouseState;
            KeyboardState keyboardstate = InputState.CurrentKeyboardState;

            float mouseMove = 400.0f * elapsedTime;

            if (game.IsActive)
            {
                bool pad = InputState.CurrentPadState.IsConnected;

                if (lootingItem != null)
                {
                    gameWorld.ShowLootTipInf(this, lootingItem);
                }

                #region 切换武器
                if (InputState.IsKeyPressed(Keys.D1)||(pad&& InputState.IsPadButtonPressed(Buttons.DPadUp)))
                {
                    if (SkillControlUnit)
                    {
                        if (skills.Count >= 1)
                        {
                            currentSkillNumber = 0;
                        }
                    }
                    else if (weapons.Count >= 1)
                    {

                        if (weapons[0] != null)
                        {
                            currentWeaponNumber = 0;
                        }
                    }
                }
                if (InputState.IsKeyPressed(Keys.D2) || (pad && InputState.IsPadButtonPressed(Buttons.DPadDown)))
                {

                    if (SkillControlUnit)
                    {
                        if (skills.Count >= 2)
                        {
                            currentSkillNumber = 1;
                        }
                    }
                    else if (weapons.Count >= 2)
                    {

                        if (weapons[1] != null)
                        {
                            currentWeaponNumber = 1;
                        }
                    }
                }
                if (InputState.IsKeyPressed(Keys.D3) || (pad && InputState.IsPadButtonPressed(Buttons.DPadRight)))
                {


                    if (SkillControlUnit)
                    {
                        if (skills.Count >= 3)
                        {
                            currentSkillNumber = 2;
                        }
                    }
                    else if (weapons.Count >= 3)
                    {

                        if (weapons[2] != null)
                        {
                            currentWeaponNumber = 2;
                        }
                    }
                }
                if (InputState.IsKeyPressed(Keys.D4) || (pad && InputState.IsPadButtonPressed(Buttons.DPadLeft)))
                {

                    if (SkillControlUnit)
                    {
                        if (skills.Count >= 4)
                        {
                            currentSkillNumber = 3;
                        }
                    }
                    else if (weapons.Count >= 4)
                    {

                        if (weapons[3] != null)
                        {
                            currentWeaponNumber = 3;
                        }
                    }
                }
                #endregion

                #region 拾取
                if (InputState.IsKeyPressed(Keys.E) || (pad && InputState.IsPadButtonPressed(Buttons.X)))
                {

                    if (lootingItem != null)
                    {
                        lootingItem.Loot(this);
                    }
                }
                #endregion

                if (InputState.IsKeyPressed(Keys.W) || (pad && InputState.IsPadButtonPressed(Buttons.B)))
                {
                    keepEngineOn = !keepEngineOn;
                }

                bool engineBlock = InputState.IsKeyDown(Keys.S) || InputState.IsPadButtonDown(Buttons.A);

                if (keepEngineOn && !engineBlock) EngineOnFlame();

                


                //if (keyboardstate.IsKeyDown(Keys.F))
                if (InputState.IsKeyPressed(Keys.F) || (pad && InputState.IsPadButtonPressed(Buttons.RightShoulder)))
                {
                    ShotMissileFlame();
                }
                if (InputState.IsKeyPressed(Keys.R) || (pad && InputState.IsPadButtonPressed(Buttons.LeftShoulder)))
                {
                    if (!SkillControlUnit)
                        CastSkill();
                }

                float tX = 0.0f;
                float tY = 0.0f;

                playerRollValue = 0;

                //旋转
                if (InputState.IsKeyDown(Keys.Space) || (pad && InputState.IsPadButtonDown(Buttons.LeftStick)))
                {
                    if (gameWorld.PlayerLockedTarget != null)
                    {

                        RotateRealWorldFlame(gameWorld.PlayerLockedTarget.Position);
                    }
                    else
                    {
                        b0 = true;
                    }
                }
                else
                {
                    if (pad)
                    {
                        Vector2 t = new Vector2(InputState.CurrentPadState.ThumbSticks.Left.X, InputState.CurrentPadState.ThumbSticks.Left.Y);
                        if (t != Vector2.Zero)
                        {
                            if (!gameWorld.InvertPadY)
                            {
                                t.Y *= -1;
                            }

                            if (InputState.IsPadButtonDown(Buttons.LeftTrigger))
                            {
                                //if aiming, don't roll
                                playerRotateValue = t;
                                playerRotating = true;
                                
                            }
                            else
                            {
                                playerRotateValue = new Vector2(0, t.Y);
                                playerRotating = true;
                                playerRollValue = t.X;
                                playerRolling = true;
                            }
                        }
                    }
                    if (mouseMove != 0 && !playerRotating)
                    {

                        tX = ((float)(mousestate.X - 400)) / mouseMove;
                        tY = ((float)(mousestate.Y - 400)) / mouseMove;
                        if (gameWorld.InvertMouseY)
                        {
                            tY *= -1;
                        }

               
                        Vector2 rv = new Vector2(tX, tY);
                        rv = Vector2.Lerp(prevRotation, rv, 0.7f);
                        prevRotation = rv;

                        if (InputState.IsMouseButtonDown(MouseButton.RightButton))
                        {

                            //if aiming, don't roll

                            playerRotateValue = rv;
                            playerRotating = true;
                        }
                        else
                        {
                            playerRotateValue = new Vector2(0, rv.Y);
                            playerRotating = true;
                            playerRollValue = rv.X;
                            playerRolling = true;

                        }
                    }
                }



                if (InputState.CurrentKeyboardState.IsKeyDown(Keys.A))
                {
                    playerRotating = true;
                    playerRotateValue = new Vector2(-1,0);
                }
                else if (InputState.CurrentKeyboardState.IsKeyDown(Keys.D))
                {
                    playerRotating = true;
                    playerRotateValue = new Vector2(1, 0);
                }
                else if (pad)
                {
                    float r2 = InputState.CurrentPadState.ThumbSticks.Right.X;
                    if (r2 != 0 && InputState.CurrentPadState.ThumbSticks.Right.Y < 0.5f)
                    {
                        playerRotateValue += new Vector2(r2,0);
                        playerRotating = true;
                    }
                }
                if (!SkillControlUnit)
                {
                    if (CurrentWeapon != null)
                    {
                        if (CurrentWeapon.isFastWeapon)
                        {

                            if (mousestate.LeftButton == ButtonState.Pressed || (pad && InputState.IsPadButtonDown(Buttons.RightTrigger)))
                            {
                                ShotFlame();
                            }
                        }
                        else
                        {
                            if (InputState.IsMouseButtonPressed(MouseButton.LeftButton)||InputState.IsPadButtonPressed(Buttons.RightTrigger))
                            {
                                ShotFlame();
                            }
                        }
                    }
                }
                else
                {
                    if (CurrentSkill != null)
                    {
                        if (InputState.IsMouseButtonPressed(MouseButton.LeftButton)||InputState.IsPadButtonPressed(Buttons.Y))
                        {
                            CastSkill();
                        }
                    }
                }



                Mouse.SetPosition(400, 400);
                if (InputState.IsKeyPressed(Keys.Q)||InputState.IsPadButtonPressed(Buttons.Y))
                {
                    b0 = true;
                }

            }
        }

        public void UpdateWeapons(GameTime gameTime)
        {
            if (UnitState == UnitState.alive)
            {
                #region 更新武器及技能
                foreach (MissileWeapon mw in missiles)
                {
                    if (mw != null)
                    {
                        mw.Update(gameTime);
                    }
                }
                foreach (Weapon w in weapons)
                {
                    if (w != null)
                    {
                        w.Update(gameTime);
                    }
                }


                isUsingSkill = false;
                usingSkill = null;
                foreach (Skill s in skills)
                {
                    if (s != null)
                    {
                        s.Update(gameTime);
                        if (s.IsActive)
                        {
                            isUsingSkill = true;
                            usingSkill = s;
                        }
                    }
                }
                #endregion

                #region 射击
                if (isShotting == true)
                {
                    if (CurrentWeapon!= null)
                    {
                        CurrentWeapon.Shot(gameTime);
                    }
                    
                }
                if (isShottingMissile == true && CurrentMissileWeapon != null)
                {
                    if (Target != null)
                    {

                        CurrentMissileWeapon.Shot(gameTime, this, Target);
                    }
                }


                #endregion

                isShottingMissile = false;
                isShotting = false;
            }


            if (Target != null)
            {
                if (Target.UnitState != UnitState.alive)
                {
                    Target = null;
                    gameWorld.PlayerLockedTarget = null;
                }
            }
        }

        /// <summary>
        /// 超级更新
        /// </summary>
        /// <param name="gameTime"></param>
        public override void SUpdate()
        {
            if (UnitState != UnitState.dead)
            {
               
                facingUnit = null;
                enemyUnintsInFront = 0;//前方宽范围内的敌人数量
                float ld = 0;//到目前为止最近的视野中单位与该单位的位置差在该单位前方直线上的投影长度
                Barrel b = new Barrel(Position, Face, GameConsts.PlayerSightDistance,GameConsts.PlayerSightStartRadius,GameConsts.PlayerSightEndRadius);
                Barrel c = new Barrel(Position, Face, GameConsts.PlayerSightDistance, 20, 1000);

                if (isPlayerControlling == true)
                {
                    facingUnits = new List<Unit>(10);
                    #region 检查面前的单位（只对玩家）
                    foreach (Unit u in gameWorld.units)
                    {
                        if (u != null)
                        {
                            if (u.Position != Position)
                            {
                                if (Distance(this, u) <= GameConsts.PlayerSightDistance)
                                {
                                    if (Collision.IsCollided(u, c) && u.Group != this.Group && u.Dead == false)
                                    {
                                        enemyUnintsInFront += 1;
                                    }

                                    if (u != this && u.Dead == false && ((Collision.IsCollided(u, c) && u.Group != this.Group) || (Collision.IsCollided(u, b) && u.Group == this.Group)))
                                    {

                                        facingUnits.Add(u);
                                        if (facingUnit != null)
                                        {
                                            if (u.Position != this.Position)
                                            {
                                                float k = Vector3.Dot(Vector3.Normalize(u.Position - this.Position), this.Face);
                                                if (k > ld)
                                                {
                                                    ld = k;
                                                    facingUnit = (ObjectUnit)u;
                                                }
                                            }

                                        }
                                        else
                                        {
                                            facingUnit = (ObjectUnit)u;
                                            if (u.Position != this.Position)
                                            {

                                                ld = Vector3.Dot(Vector3.Normalize(u.Position - this.Position), this.Face);
                                            }
                                            else ld = 0;

                                        }
                                    }
                                }

                            }

                        }

                    }
                    #endregion
                    if (facingUnit == null)
                    {
                        if (b0)
                        {
                            b0 = false;
                            if (gameWorld.PlayerLockedTarget != null)
                            {

                                gameWorld.PlayerLockedTarget = null;
                            }
                            else if (Target != null)
                            {
                                gameWorld.PlayerLockedTarget = Target;
                            }

                        }
                        #region 检查面前的loot
                        foreach (LootItem l in gameWorld.lootItems)
                        {

                            if (l != null)
                            {
                                if (l.Position != Position)
                                {
                                    if (Distance(this, l) <= GameConsts.PlayerSightDistance)
                                    {

                                        if (Collision.IsCollided(l.Position, b) && l.Dead == false)
                                        {

                                            if (facingUnit != null)
                                            {
                                                float k = Vector3.Dot(l.Position - this.Position, this.Face);
                                                if (k < ld)
                                                {
                                                    ld = k;
                                                    facingUnit = (ObjectUnit)l;
                                                }
                                            }
                                            else
                                            {
                                                facingUnit = (ObjectUnit)l;
                                                ld = Vector3.Dot(l.Position - this.Position, this.Face);

                                            }
                                        }
                                    }

                                }

                            }
                        }
                        #endregion
                    }
                    else 
                    {
                        if (((Unit)facingUnit).Group != this.Group)
                        {
                            if (gameWorld.PlayerLockedTarget == null)
                            {

                                Target = (Unit)facingUnit;
                            }
                            if (b0)
                            {
                                b0 = false;
                                Target = (Unit)facingUnit;
                                gameWorld.PlayerLockedTarget = Target;

                            }
                        }
                    }
                }

            }
           
            lootingItem = null;
            base.SUpdate();
        }
        #endregion
        public override void DrawEffects(GameTime gameTime, Camera camera)
        {
            foreach (Weapon w in weapons)
            {
                if (w != null)
                {

                    w.DrawEffects(gameTime, camera);
                }
            }
            base.DrawEffects(gameTime, camera);
        }

        public override void BeginToDie()
        {
            Stop();
            Target = null;
            if (this == gameWorld.CurrentStage.Player)
            {
                gameWorld.PlayerLockedTarget = null;
            }
     
            foreach (Skill skill in skills)
            {
                skill.Interrupt();
            }

            foreach (LootEntry l in loots)
            {
                if (l != null)
                {
                    if (AODGameLibrary.Helpers.RandomHelper.Random.NextDouble() <= l.Prob)
                    {

                        Vector3 p = new Vector3((float)AODGameLibrary.Helpers.RandomHelper.Random.NextDouble() - 0.5f, (float)AODGameLibrary.Helpers.RandomHelper.Random.NextDouble() - 0.5f, (float)AODGameLibrary.Helpers.RandomHelper.Random.NextDouble() - 0.5f) * 2;
                        gameWorld.CreateLoot(gameWorld.Content.Load<LootSettings>(l.LootAssetName), p * l.MaxRadius + Position);

                    }
                }
            }

            base.BeginToDie();
            if (gameWorld.CurrentStage.Over == false)
            {

                gameWorld.CurrentStage.Event_UnitDied(this);//触发死亡事件
            }
        }
 
        /// <summary>
        /// 表示下一次运行Update(gameTime)时执行射击
        /// </summary>
        public void ShotFlame()
        {
            isShotting = true;
        }
        public void ShotMissileFlame()
        {
            isShottingMissile = true;
        }
        /// <summary>
        /// 表示下一次运行Update(gameTime)时执行移动
        /// </summary>
        public void EngineOnFlame()
        {
            isEngineOn = true;
        }

        /// <summary>
        /// 表示下一次运行Update(gameTime)时向目标点执行旋转
        /// </summary>
        /// <param name="t">旋转的目标点</param>
        public void RotateRealWorldFlame(Vector3 t)
        {
            if (t != Position)
            {
                rotateT = Vector3.Normalize(Vector3.Transform(t, Matrix.Invert(World)));
            }


        }
        /// <summary>
        /// 表示下一次运行Update(gameTime)时向目标点执行旋转,目标点为与该单位的相对位置
        /// </summary>
        /// <param name="t">旋转的目标点</param>
        public void RotateFlame(Vector3 t)
        {
            if (t != Vector3.Zero)
            {
                rotateT = Vector3.Normalize(t);
            }


        }
        public bool IsShotHittable(ObjectUnit target)
        {
            bool s = false;
            Ray ra;
            if (Velocity != Vector3.Zero)
            {

                ra = new Ray(Position, Vector3.Normalize(Velocity + CurrentWeapon.basicSpeed * Face));
            }
            else ra = new Ray(Position, Face);

            if (Collision.IsCollided(target, ra))
            {

                s = true;
            }
            return s;
        }

        /// <summary>
        /// 命令单位移动到一个点
        /// </summary>
        /// <param name="position"></param>
        public void MoveTo(Vector3 position)
        {
            this.movingPosition = position;
            this.isMoving = true;
            this.isRotating = false;
        }
        /// <summary>
        /// 命令单位旋转直到面向一个点
        /// </summary>
        /// <param name="position"></param>
        public void RotateTo(Vector3 position)
        {
            this.rotationDirection = position;
            this.isRotating = true;

        }
        public void Stop()
        {
            isMoving = false;
            isRotating = false;
        }

        public void GetWeapon(string weaponAssetName,float bnum)
        {
            Weapon w = new Weapon(gameWorld, gameWorld.game.Content.Load<WeaponType>(weaponAssetName), this);
            w.AmmoNum = bnum;
            if (weapons.Count < maxWeaponNum)
            {

               
                this.weapons.Add(w);
                
            }
            else
            {
                this.CurrentWeapon = w;
            }

        }
        public void GetMissileWeapon(string missileWeaponAssetName, float bnum)
        {
            MissileWeapon w = new MissileWeapon(gameWorld, gameWorld.Content.Load<MissileWeaponType>(missileWeaponAssetName));
            if (missiles.Count < maxMissileWeaponNum)
            {
                this.missiles.Add(w);
            }
            else
            {
                this.CurrentMissileWeapon = w;
            }
        }

        public void AddSkill(string skillAssetName)
        {
            if (skills.Count < maxSkillNum)
            {

                skills.Add(Skill.CreateSkill(this, skillAssetName, this.gameWorld));
            }
        }
        /// <summary>
        /// 施放当前技能
        /// </summary>
        public void CastSkill()
        {
            CastSkill(Target);
        }
        /// <summary>
        /// 对指定目标施放当前技能
        /// </summary>
        /// <param name="tar"></param>
        public void CastSkill(Unit tar)
        {
            if (CurrentSkill != null && !this.isUsingSkill)
            {
                CurrentSkill.Cast(tar);
            }
        }
        /// <summary>
        /// 施放指定名称的技能
        /// </summary>
        /// <param name="skillName">技能名</param>
        public void CastSkill(string skillName)
        {
            CastSkill(skillName, Target);
        }
        /// <summary>
        /// 施放技能
        /// </summary>
        /// <param name="s"></param>
        public void CastSkill(Skill s)
        {
            if (skills.Contains(s) && s != null && !this.isUsingSkill)
            {
                s.Cast(Target);
            }
        }
        /// <summary>
        /// 向目标施放技能
        /// </summary>
        /// <param name="s"></param>
        /// <param name="tar"></param>
        public void CastSkill(Skill s,Unit tar)
        {
            if (skills.Contains(s) && s != null && !this.isUsingSkill)
            {
                s.Cast(tar);
            }
        }
        /// <summary>
        /// 对指定目标施放指定名称的技能
        /// </summary>
        /// <param name="skillName">技能名</param>
        /// <param name="tar">目标</param>
        public void CastSkill(string skillName, Unit tar)
        {
            if (!this.isUsingSkill)
            {


                Skill s = null;
                foreach (Skill sk in skills)
                {
                    if (sk != null)
                    {
                        if (sk.SkillName == skillName)
                        {
                            s = sk;
                            break;
                        }
                    }
                }
                if (s != null)
                {
                    s.Cast(tar);
                }
            }
        }
        public Skill SkillFromName(string skillName)
        {
            Skill s = null;
            foreach (Skill sk in skills)
            {
                if (sk != null)
                {
                    if (sk.SkillName == skillName)
                    {
                        s = sk;
                        break;
                    }
                }
            }
            return s;
        }
        public Weapon WeaponFromName(string weaponName)
        {
            Weapon w = null;
            foreach (Weapon wp in weapons)
            {
                if (wp != null)
                {
                    if (wp.name == weaponName)
                    {
                        w = wp;
                        break;
                    }
                }
            }
            return w;
        }
        public Weapon WeaponFromAssetName(string weaponAssetName)
        {
            Weapon w = null;
            foreach (Weapon wp in weapons)
            {
                if (wp != null)
                {
                    if (wp.assetName == weaponAssetName)
                    {
                        w = wp;
                        break;
                    }
                }
            }
            return w;
        }
        public MissileWeapon MissileWeaponFromAssetName(string weaponAssetName)
        {
            MissileWeapon w = null;
            foreach (MissileWeapon wp in missiles)
            {
                if (wp != null)
                {
                    if (wp.missileWeaponType.AssetName == weaponAssetName)
                    {
                        w = wp;
                        break;
                    }
                }
            }
            return w;
        }
        public Skill SkillFromAssetName(string skillAssetName)
        {
            Skill s = null;
            foreach (Skill sk in skills)
            {
                if (sk != null)
                {
                    if (sk.AssetName == skillAssetName)
                    {
                        s = sk;
                        break;
                    }
                }
            }
            return s;
        }
        /// <summary>
        /// 打断技能
        /// </summary>
        public void Interrupt()
        {
            if (isUsingSkill)
            {
                usingSkill.Interrupt();
            }
        }
        public override void GetDamage(float shield, float armor,Unit attacker)
        {
            if (unitAI != null)
            {
                if (isPlayerControlling == false && IsAIControlling == true)
                {

                    unitAI.GetDamage(attacker, (shield + armor) / (MaxArmor + MaxShield));
                }
            }
            base.GetDamage(shield, armor,attacker);
        }
        
        #region 静态方法们

        /// <summary>
        /// 返回A要射到B应面朝的方向（相对A）
        /// </summary>
        /// <param name="a">要射的单位</param>
        /// <param name="target">射到的位置</param>
        /// <returns>方向</returns>
        public static Vector3 SDirection(Unit a, Vector3 target)
        {
            if (a.Position != target  && a.CurrentWeapon != null)
            {
                Vector3 t = Vector3.Normalize(Vector3.Transform(target, Matrix.Invert(a.World)));
                Vector3 m = Vector3.Normalize(Vector3.TransformNormal(Vector3.Normalize(a.Face * a.CurrentWeapon.basicSpeed + a.Velocity), Matrix.Invert(a.Rotation)));
                return Vector3.Normalize(t - m);
            }
            else return Vector3.Forward;
        }
        public static Unit Create(GameWorld gameWorld, UnitType unitType, int group, Vector3 position,bool isLootable,bool endlessBullets)
        {
            return new Unit(gameWorld, unitType, group, position, isLootable,endlessBullets);
        }
        public void SetAI(AI ai)
        {
            ai.Bound(this, gameWorld);
            unitAI = ai;
        }
        /// <summary>
        /// 恢复
        /// </summary>
        public void Restore()
        {
            this.Armor = MaxArmor;
            this.Shield = MaxShield;
        }
        #endregion
    }
}
