using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using AODGameLibrary.Units;
using AODGameLibrary.AODObjects;
using AODGameLibrary.GamePlay;
using System;

namespace AODGameLibrary.Cameras
{
    /// <summary>
    /// 在3D空间内观察物体的相机类别，由大地无敌-范若余于2009年10月12日改进，加入弹簧相机功能
    /// </summary>
    public class Camera 
    {

        //private Vector3 upVector = Vector3.Up;

        //public Vector3 UpVector
        //{
        //    get { return upVector; }
        //    set { upVector = value; }
        //}
        /// <summary>
        /// 注视点
        /// </summary>
        private Vector3 lookAt = Vector3.Zero;

        public Vector3 LookAt
        {
            get { return lookAt; }

        }
        private Matrix view;
        /// <summary>
        /// 得到相机目前的视觉矩阵
        /// </summary>
        public Matrix View
        {
            get { return view; }
        }
        private Matrix projection;
        /// <summary>
        /// 得到相机的投影矩阵
        /// </summary>
        public Matrix Projection
        {
            get { return projection; }
        }
        public Matrix skyprojection;
        GraphicsDevice device;
        float aspectRatio;//宽高比
        /// <summary>
        /// 正在追踪的单位
        /// </summary>
        public Unit targetUnit;
        /// <summary>
        /// 相机位置
        /// </summary>
        private Vector3 position = new Vector3(0, 0, 10.0f);

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
   
        /// <summary>
        /// 相对被追踪单位位置偏移量
        /// </summary>
        public Vector3 cameraObjOffset = new Vector3(0, 10, 40);
        /// <summary>
        /// 目标上方向量
        /// </summary>
        public Vector3 ObjUp;
        /// <summary>
        /// 目标位置
        /// </summary>
        public Vector3 ObjPosition;
        /// <summary>
        /// 目标方向
        /// </summary>
        public Vector3 ObjDirection;

        ///// <summary>
        ///// 是否弹簧相机
        ///// </summary>
        //private bool springCamera = true;
        /// <summary>
        /// 相机类型
        /// </summary>
        public CameraType cameraType = CameraType.NormalCamera;
     
        //public bool SpringCamera
        //{
        //    get { return springCamera; }
        //    set
        //    {
        //        if (value != springCamera)
        //        {
        //            Reset();
        //        }
        //        springCamera = value;
        //    }
        //}
        /// <summary>
        /// 相机旋转矩阵
        /// </summary>
        public Matrix cameraRotation;
        public Matrix targetCameraRotation;
        /// <summary>
        /// 注视点偏移值
        /// </summary>
        public Vector3 lookAtOffset;

        #region 弹簧相机参数
        public Vector3 sC_CameraPosition; //弹簧相机的相机位置
        public float sC_stiffness = 1800; // 弹簧相机的劲度系数
        public float sC_damping = 600;    // 弹簧相机的阻尼
        public float sC_mass = 50;       // 弹簧相机的质量
        public Vector3 sC_Xvelocity;  //弹簧相机相对速度部分(这部分速度忽略阻尼的影响)
        private Vector3 sC_velocity;  // 弹簧相机绝对速度部分
        #endregion
        #region 内切相机参数
        public float aC_scale = 1.0f;
        public float aC_minScale = 0.7f;
        public float aC_maxScale = 1.6f;
        public float aC_scaleTolerance = 0.01f;//误差允许值
        public float aC_rotationlerpScale = 3.25f;
        public float aC_scaleLerpScale = 1;
        #endregion
        #region 观察相机参数
        Vector3 observedPoint;
        ObjectUnit observedUnit;
        #endregion

        public Camera(Game game)
        {
            device = game.GraphicsDevice;
            aspectRatio = device.Viewport.AspectRatio;
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60.0f),
                                                             aspectRatio,
                                                             1.0f,
                                                             12000.0f);
            skyprojection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60.0f),
                                                 aspectRatio,
                                                 1.0f,
                                                 100000.0f);
        }
        public Camera(Game game, Vector3 position, Vector3 target,Vector3 Up):this(game)
        {
            this.cameraType = CameraType.Observer;
            this.position = position;
            this.ObjUp = Up;
            this.observedPoint = target;

        }

        public Camera(Game game, Vector3 position, ObjectUnit target, Vector3 Up)
            : this(game)
        {
            this.cameraType = CameraType.Observer;
            this.position = position;
            this.ObjUp = Up;
            this.observedUnit = target;
            this.observedPoint = target.Position;
        }

        public static Camera CreateSpringFollowCamera(GameWorld gameWorld,Unit unit)
        {
            Camera c = new Camera(gameWorld.game);
            c.cameraType = CameraType.SpringCamera;
            c.cameraObjOffset = new Vector3(0, 10, 40);
            c.lookAtOffset = new Vector3(0, 4, -8);
            c.follow(unit);
            return c;
        }
        public static Camera CreateAODChaseCamera(GameWorld gameWorld, Unit unit)
        {
            Camera c = new Camera(gameWorld.game);
            c.cameraType = CameraType.AODChaseCamera;

            c.cameraObjOffset = new Vector3(0, 10, 40);
            c.lookAtOffset = new Vector3(0, 0, -1000);

            c.follow(unit);
            return c;
        }
        public static Camera CreateNormalFollowCamera(GameWorld gameWorld, Unit unit)
        {
            Camera c = new Camera(gameWorld.game);
            c.cameraType = CameraType.NormalCamera;

            c.cameraObjOffset = new Vector3(0, 7, 20);
            c.lookAtOffset = new Vector3(0, 0, -1000);
           
            c.follow(unit);

            return c;
        }
        /// <summary>
        /// 使该相机位置和旋转与目标相机保持同步注意,如果是该相机为普通相机则永久改变位置
        /// </summary>
        /// <param name="target"></param>
        public void Sync(Camera target)
        {
            this.position = target.position;
            this.cameraRotation = target.cameraRotation;
       
        }
        /// <summary>
        /// 创建跟随单位的跨肩视角或其它第三人称视角的相机
        /// </summary>
        /// <param name="aunit">得到跟随的单位</param>
        /// <param name="unitTranslation">相对于单位原位置的平移</param>
        //public void follow(Unit aunit,Vector3 unitTranslation)
        //{
        //    chasingUnit = aunit;
        //    cameraObjOffset  = unitTranslation;
        //    chasing = true;
        //}
        public void follow(Unit aunit)
        {
            targetUnit = aunit;

            UpdateCameraTarget(targetUnit.Position, targetUnit.Face, targetUnit.Up);

            Reset();
            
        }
        public void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            sC_Xvelocity = Vector3.Zero;
            if (targetUnit!= null)
            {
                if (targetUnit.UnitState == UnitState.alive)
                {


                    {
                        UpdateCameraTarget(targetUnit.Position, targetUnit.Face, targetUnit.Up);
                        /*
                        InspectionPoint = Vector3.Transform(new Vector3(0, 0, -20000), chasingUnit.rotation) + chasingUnit.position;
                        position = Vector3.Transform(relativlyPosition, chasingUnit.rotation) + chasingUnit.position;
                        upVector = Vector3.TransformNormal(Vector3.Up, chasingUnit.rotation);
                         */
                        if (targetUnit.Speed > targetUnit.MaxSpeed/3 && targetUnit.Speed != 0 && cameraType == CameraType.SpringCamera)
                        {
                          //适当加入一些对单位的相对速度避免单位速度太快导致无法正常转动镜头
                            sC_Xvelocity = targetUnit.Velocity - Vector3.Normalize(targetUnit.Velocity) * targetUnit.MaxSpeed/3;
                        }

                     
                    }

                }
                

            }
            UpdateWorldPositions();
            switch (cameraType)
            {
                case CameraType.NormalCamera:
                    {

                    }
                    break;
                case CameraType.SpringCamera:
                    {
                        
                        Vector3 stretch = sC_CameraPosition - position;

                        Vector3 force = sC_stiffness * stretch - sC_damping * sC_velocity;
                    
                        Vector3 acceleration = force / sC_mass;

                        sC_velocity += acceleration * elapsedTime;

                        position += (sC_velocity + sC_Xvelocity) * elapsedTime;
                    }
                    break;
                case CameraType.AODChaseCamera:
                    {
                        cameraRotation = Matrix.Lerp(cameraRotation, targetCameraRotation, MathHelper.Clamp( aC_rotationlerpScale * elapsedTime,0,1));
                        if (cameraObjOffset != Vector3.Zero)
                        {

                            aC_scale = Vector3.Distance(position, targetUnit.Position) / cameraObjOffset.Length();
                        }
                        
                        float d = aC_scale - 1;
                        if (Math.Abs(d) <= aC_scaleTolerance)
                        {
                            aC_scale = 1.0f;
                        }
                        else aC_scale -= d * elapsedTime * aC_scaleLerpScale;
                        aC_scale = MathHelper.Clamp(aC_scale, aC_minScale, aC_maxScale);

                        Vector3 ps = Vector3.TransformNormal(cameraObjOffset, cameraRotation);
                        ps *= aC_scale;
                        position = ObjPosition + ps;

                        ObjUp = cameraRotation.Up;
                    }
                    break;
                case CameraType.Observer:
                    {
                        if (observedUnit!= null)
                        {
                            observedPoint = observedUnit.Position;
                        }
                        ObjPosition = observedPoint;
                    }
                    break;
                default:
                    break;
            }

            lookAt = ObjPosition + Vector3.TransformNormal(lookAtOffset, cameraRotation);
            view = Matrix.CreateLookAt(position, lookAt, ObjUp);
        }
        //相机世界更新
        private void UpdateWorldPositions()
        {
            
            
       
                Matrix transform = Matrix.Identity;
                transform.Forward = ObjDirection;
                transform.Up = ObjUp;
                transform.Right = Vector3.Cross(ObjUp, ObjDirection);
                targetCameraRotation = transform;
                
                switch (cameraType)
                {
                    case CameraType.NormalCamera:
                        cameraRotation = targetCameraRotation;
                        position = ObjPosition + Vector3.TransformNormal(cameraObjOffset, cameraRotation);
                        break;
                    case CameraType.SpringCamera:
                        cameraRotation = targetCameraRotation;
                        sC_CameraPosition = ObjPosition + Vector3.TransformNormal(cameraObjOffset, cameraRotation);
                        break;
                    case CameraType.AODChaseCamera:
                        
                        break;
                    default:
                        break;
                }
               
          //   position = ObjPosition + Vector3.TransformNormal(
     
        }
        private void UpdateCameraTarget(Vector3 ObjPosition, Vector3 ObjDirection, Vector3 ObjUp)
        {
            this.ObjDirection = ObjDirection;
            this.ObjPosition = ObjPosition;
            this.ObjUp = ObjUp;
        }
        public void Reset()
        {
            UpdateWorldPositions();

            cameraRotation = targetCameraRotation;
            sC_velocity = Vector3.Zero;
            position = ObjPosition + Vector3.TransformNormal(cameraObjOffset, cameraRotation);
            lookAt = ObjPosition + Vector3.TransformNormal(lookAtOffset, cameraRotation);
            view = Matrix.CreateLookAt(position, lookAt, ObjUp);
            aC_scale = 1.0f;

            
        }
        /// <summary>
        /// 返回位置是否在视野内
        /// </summary>
        /// <param name="position"></param>
        public bool PositionInCamera(Vector3 position)
        {
            Vector3 k = device.Viewport.Project(position, Projection, View, Matrix.Identity);
            if (k.X > 0 && k.X < device.Viewport.Width && k.Y > 0 && k.Y < device.Viewport.Height)
            {
                if (Vector3.Dot(Forward,position-this.position)>0)
                {
                    return true;
                }
            }
      
            return false;
        }
        /// <summary>
        /// 获得目标位置和观察平面中心所成的角（上方为0，逆时针为正）
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public float  GetPlaneAngle(Vector3 position)
        {
            if (position!= this.position)
            {
                Matrix m = Matrix.Identity;

                m.Up = ObjUp;
                m.Forward = Forward;
                m.Right = Vector3.Cross(ObjUp, Forward);
                Vector3 p = Vector3.TransformNormal(position - this.position, Matrix.Invert(m));//得到相对于相机的位置
                Vector2 x = Vector2.Normalize(new Vector2(p.X, p.Y));

                //计算出向量的内积并求向量构成的角
                float f = (float)Math.Acos(Vector2.Dot(x, new Vector2(0, 1)));
                if (x.X > 0)
                {
                    //如果在屏幕中线的右边就取负角
                    f *= -1;
                }
                return f;

            }

            return 0;
        }
        public Vector3 Forward
        {
            get
            {
                return Vector3.Normalize(lookAt - position);
            }
        }
    }
    /// <summary>
    /// 表示相机类型的枚举
    /// </summary>
    public enum CameraType
    {
        /// <summary>
        /// 普通追踪相机
        /// </summary>
        NormalCamera,
        /// <summary>
        /// 标准弹簧相机
        /// </summary>
        SpringCamera,
        /// <summary>
        /// 内插追逐相机
        /// </summary>
        AODChaseCamera,
        /// <summary>
        /// 普通观察相机
        /// </summary>
        Observer,
    }
}
