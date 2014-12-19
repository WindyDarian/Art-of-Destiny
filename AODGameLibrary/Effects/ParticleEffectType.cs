using System;
using System.Collections.Generic;

using System.Text;
using AODGameLibrary.GamePlay;

namespace AODGameLibrary.Effects
{
    [Serializable]
    public class ParticleEffectType
    {
        /// <summary>
        /// 粒子组
        /// </summary>
        public List<string> ParticleGroups;
        /// <summary>
        /// 由CPU运算的粒子组
        /// </summary>
        public List<string> CPUParticleGroups;
        public float Scale;
        /// <summary>
        /// 粒子效果名
        /// </summary>
        public string name;

    }

    
}
