using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AODGameLibrary.Units;
using AODGameLibrary.Weapons;

namespace AODGameLibrary2
{
    [Serializable]
    /// <summary>
    /// 被保存的单位,2010年7月1日 大地无敌-范若余
    /// </summary>
    public class SavedUnit
    {
       public UnitType UnitType;
        public List<SavedInf> Weapons = new List<SavedInf>();
        public List<SavedInf> Spells = new List<SavedInf>();
        public List<SavedInf> Missiles = new List<SavedInf>();

        public void SaveUnit(Unit unit,bool fullammo)
        {
            Clear();


            UnitType = unit.UnitType.Clone();

            foreach (var w in unit.weapons)
            {
                SavedInf inf = new SavedInf();
                inf.AssetName = w.assetName;
                if (fullammo) inf.AmmoNum = w.maxAmmo;
                else inf.AmmoNum = w.AmmoNum;
                Weapons.Add(inf);
            }
            foreach (var s in unit.skills)
            {
                SavedInf inf = new SavedInf();
                inf.AssetName = s.AssetName;
                Spells.Add(inf);
            }
            foreach (var m in unit.missiles)
            {
                SavedInf inf = new SavedInf();
                inf.AssetName = m.missileWeaponType.AssetName;
                if (fullammo)
                {
                    inf.AmmoNum = m.missileWeaponType.maxNum;
                }
                else inf.AmmoNum = m.Num;
                Missiles.Add(inf);

            }
             
        }
        public void Clear()
        {
            UnitType = null;
            Weapons.Clear();
            Missiles.Clear();
            Spells.Clear();
        }
        public bool Exist
        {
            get
            {
                return UnitType != null;
            }
        }
    }
    [Serializable]
    public struct SavedInf
    {
        public string AssetName;
        public float AmmoNum;
       
    }

}
