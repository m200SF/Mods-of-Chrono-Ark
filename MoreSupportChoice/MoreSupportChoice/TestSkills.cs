using GameDataEditor;
using I2.Loc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoreSupportChoice
{
    public class Sex_test_1 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            foreach (Buff buff in Targets[0].Buffs)
            {
                if (!buff.BuffData.Debuff && !buff.CantDisable && !buff.IsHide)
                {
                    buff.SelfDestroy();
                }
            }
        }
    }



    public class Bcl_test_3 : Buff, IP_BuffAdd_Before
    {
        public void BuffAdd_Before(BattleChar BC, string key, BattleChar UseState, bool hide, int PlusTagPer, bool debuffnonuser, int RemainTime, bool StringHide)
        {
            GDEBuffData gdebuffData = new GDEBuffData(key);
            int frameCount = Time.frameCount;
            //Debug.Log("time:" + Time.frameCount);
            //Debug.Log("timelast:" + BC.BuffCheck.TimeSeed);

            if (BC == this.BChar && gdebuffData.Debuff && !gdebuffData.Hide && !hide && !(BC.BuffCheck.Lastbuff == key && BC.BuffCheck.TimeSeed == frameCount))
            {


                LastBuffRsisCheck lastBuffRsisCheck = new LastBuffRsisCheck();
                lastBuffRsisCheck.TimeSeed = Time.frameCount;
                lastBuffRsisCheck.Lastbuff = key;
                lastBuffRsisCheck.IsAdd = false;
                BC.BuffCheck = lastBuffRsisCheck;
                BC.BuffCheck.PlusResist = true;
                this.BChar.SimpleTextOut(ScriptLocalization.UI_Battle.DebuffGuard);

            }

        }
    }

    public class Sex_test_4 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            foreach (Buff buff in Targets[0].Buffs)
            {
                if (buff.BuffData.Debuff && !buff.IsHide)
                {
                    buff.SelfStackDestroy();
                }
            }
        }
    }

    public class Bcl_test_5 : Buff,IP_ChangeDamageState//IP_DamageTake// IP_DamageTakeChange,IP_DamageTakeChange_sumoperation,IP_DamageChange,IP_DamageChange_sumoperation, IP_DamageChange_Hit_sumoperation
    {
        public void ChangeDamageState(SkillParticle SP, BattleChar Target, int DMG, bool Cri, ref bool ToHeal, ref bool ToPain)
        {
            if(SP.UseStatus==this.BChar)
            {
                ToHeal = true;
            }
        }
        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if(Target==this.BChar)
            {
                resist = true;
            }
        }
        /*
        public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {


            if (Hit == this.BChar)
            {
                return (int)(0.5f * Dmg);

            }
            return Dmg;
        }
        public void DamageTakeChange_sumoperation(BattleChar Hit, BattleChar User, int Dmg, bool Cri, ref int PlusDmg, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {
            if (Hit == this.BChar)
            {
                PlusDmg = -5;

            }
        }

        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if(SkillD.Master==this.BChar)
            {
                return (int)(0.5f * Damage);
            }
            return Damage;
        }
        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (SkillD.Master == this.BChar)
            {
                PlusDamage = -5;
            }
        }
        public void DamageChange_Hit_sumoperation(Skill SkillD, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {

            PlusDamage = -5;
        }
        */
    }
}
