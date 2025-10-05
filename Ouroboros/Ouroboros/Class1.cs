using ChronoArkMod.Plugin;
using ChronoArkMod;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SF_Standard;
using System.Collections;
using UnityEngine;
using GameDataEditor;

namespace SF_Ouroboros
{
    [PluginConfig("M200_SF_Ouroboros_CharMod", "SF_Ouroboros_CharMod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class SF_Ouroboros_CharacterModPlugin : ChronoArkPlugin
    {
        // Token: 0x06000003 RID: 3 RVA: 0x00002050 File Offset: 0x00000250
        public override void Dispose()
        {
            this.harmony.UnpatchSelf();
        }

        // Token: 0x06000004 RID: 4 RVA: 0x0000205F File Offset: 0x0000025F
        public override void Initialize()
        {
            this.harmony = new Harmony(base.GetGuid());
            this.harmony.PatchAll();
        }

        // Token: 0x06000005 RID: 5 RVA: 0x0000207F File Offset: 0x0000027F
        public override void OnModLoaded()
        {
            base.OnModLoaded();
            this.OnModSettingUpdate();
        }
        private Harmony harmony;
        public class OuroborosDef : ModDefinition
        {
        }
    }

    [HarmonyPatch(typeof(BattleEnemy))]
    public static class BattleEnemy_UseSkill_Plugin
    {
        // Token: 0x06000005 RID: 5 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPatch("UseSkill")]
        [HarmonyPostfix]
        public static void UseSkill_Patch_After(BattleEnemy __instance)
        {
            if (BattleSystem.instance != null)
            {
                //Debug.Log("Patch_point_1");
                foreach (IP_EnemySkillUse ip_patch in BattleSystem.instance.IReturn<IP_EnemySkillUse>())
                {
                    bool flag = ip_patch != null;
                    bool flag2 = flag;
                    if (flag2)
                    {
                        ip_patch.EnemySkillUse(__instance, __instance.SelectedSkill, __instance.SaveTarget);
                    }
                }
            }

        }
    }
    public interface IP_EnemySkillUse
    {
        // Token: 0x06000006 RID: 6
        void EnemySkillUse(BattleEnemy user, Skill skill, List<BattleChar> targets);
    }

    [HarmonyPatch(typeof(BattleSystem))]
    [HarmonyPatch("SkillTargetAdd")]
    [HarmonyPatch(new Type[] { typeof(BattleChar) })]
    public static class SkillTargetAdd_1_Plugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void UseSkill_Before_patch(BattleSystem __instance, BattleChar Target)
        {
            /*//----
            StackTrace stackTrace = new StackTrace();
            StackFrame callerFrame = stackTrace.GetFrame(0);
            if (callerFrame != null)
            {
                // 获取调用方法的名称
                string callerMethodName = callerFrame.GetMethod().Name;
                //UnityEngine.Debug.Log("方法名：" + callerMethodName);
            }
            StackFrame callerFrame1 = stackTrace.GetFrame(1);
            if (callerFrame != null)
            {
                // 获取调用方法的名称
                string callerMethodName = callerFrame1.GetMethod().Name;
                //UnityEngine.Debug.Log("方法名1：" + callerMethodName);
            }
            StackFrame callerFrame2 = stackTrace.GetFrame(2);
            if (callerFrame != null)
            {
                // 获取调用方法的名称
                string callerMethodName = callerFrame2.GetMethod().Name;
                //UnityEngine.Debug.Log("方法名2：" + callerMethodName);
            }
            *///----
            if (!BattleSystem.IsSelect(Target, __instance.SelectedSkill))
            {
                return;
            }
            if (__instance.ActionAlly != null)
            {
                foreach (IP_SkillUsePatch_Before ip_patch in BattleSystem.instance.IReturn<IP_SkillUsePatch_Before>())
                {
                    //Debug.Log("only damage drm");
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.SkillUsePatch_Before(Target, null, __instance.SelectedSkill);
                    }
                }
            }
        }
    }
    [HarmonyPatch(typeof(BattleSystem))]
    [HarmonyPatch("SkillTargetAdd")]
    [HarmonyPatch(new Type[] { typeof(Skill) })]
    public static class SkillTargetAdd_2_Plugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void UseSkill_Before_patch(BattleSystem __instance, Skill Target)
        {
            if (__instance.ActionAlly != null)
            {
                foreach (IP_SkillUsePatch_Before ip_patch in BattleSystem.instance.IReturn<IP_SkillUsePatch_Before>())
                {
                    //Debug.Log("only damage drm");
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.SkillUsePatch_Before(null, Target, __instance.SelectedSkill);
                    }
                }
            }
        }
    }
    public interface IP_SkillUsePatch_Before
    {
        // Token: 0x06000001 RID: 1
        void SkillUsePatch_Before(BattleChar TargetChar, Skill TargetSkill, Skill SelectSkill);
    }














    public class P_Ouroboros : Passive_Char, IP_LevelUp, IP_SkillUse_PlayerTeam_SkillExOnly
    {

        public override void Init()
        {
            base.Init();
            this.OnePassive = true;
        }

        public void LevelUp()
        {

            FieldSystem.DelayInput(this.Delay());

        }
        public IEnumerator Delay()
        {
            if (!this.ItemTake && this.MyChar.LV >= 5)
            {
                this.ItemTake = true;
                List<ItemBase> list = new List<ItemBase>();
                list.Add(ItemBase.GetItem("Item_SF_1", 1));
                list.Add(ItemBase.GetItem("Item_SF_2", 1));
                //list.Add(ItemBase.GetItem("E_SF_Hunter_0", 1));
                InventoryManager.Reward(list);
            }
            if (!this.ItemTake2 && this.MyChar.LV >= 3)
            {
                this.ItemTake2 = true;
                InventoryManager.Reward(ItemBase.GetItem("Item_SF_1", 1));
            }
            yield return null;
            yield break;
        }
        public bool ItemTake;
        public bool ItemTake2;

       
        public void SkillUsePlayer_SkillEx(Skill SkillD, List<BattleChar> Targets)
        {
            
            if(SkillD.Master.Info.Ally==this.BChar.Info.Ally && SkillD.IsNowCasting && !SkillD.FreeUse)
            {
                
                if (BattleSystem.instance.TurnNum==this.currentTurn && BattleSystem.instance.AllyTeam.TurnActionNum==this.currentActionTime)
                {
                    
                    this.actionCount++;
                }
                else
                {
                   
                    this.currentTurn = BattleSystem.instance.TurnNum;
                    this.currentActionTime = BattleSystem.instance.AllyTeam.TurnActionNum;
                    this.actionCount = 1;
                }
                if(this.actionCount>=2)
                {
                    
                    BattleSystem.instance.AllyTeam.AP++;
                    BattleSystem.instance.AllyTeam.WaitCount++;
                }
                if (this.actionCount >= 4)
                {

                    BattleSystem.instance.AllyTeam.Draw();
                }
            }
        }
        public int currentTurn = 0;
        public int currentActionTime = 0;
        public int actionCount = 0;
    }

    public class Sex_our_1:Sex_ImproveClone, IP_EnemySkillUse,IP_CriPerChange
    {
        public override void Init()
        {
            base.Init();
            this.OnePassive = true;
            this.onfire = false;
            this.CountingExtedned = true;
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_MissChain_Ex_P).Particle_Path;
        }
        public void EnemySkillUse(BattleEnemy user, Skill skill, List<BattleChar> targets)
        {
            if(this.MySkill.IsNowCasting&&!this.onfire && user is BattleEnemy )
            {
                try
                {
                    if(Standard_Tool.GetCastSkill(this.MySkill).TargetReturn().Contains(user))
                    {
                        this.onfire = true;
                        this.PlusSkillStat.cri = 100;
                        Traverse.Create(this.MySkill.MySkill).Field("_NODOD").SetValue(true);
                        base.SkillParticleOn();
                    }
                }
                catch { }

            }
        }
        public void CriPerChange(Skill skill, BattleChar Target, ref float CriPer)
        {
            if(this.MySkill.IsNowCasting && this.onfire && skill.Master.Info.Ally==this.MySkill.Master.Info.Ally)
            {
                try
                {
                    if (Standard_Tool.GetCastSkill(this.MySkill).TargetReturn().Contains(Target))
                    {
                        CriPer += 50;
                    }
                }
                catch { }
            }
        }
        public bool onfire = false;
    }

    public class Sex_our_2 : Sex_ImproveClone, IP_EnemySkillUse, IP_SkillUsePatch_Before
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&num1",((int)(0.4f*this.BChar.GetStat.atk)).ToString());
        }
        public override void Init()
        {
            base.Init();
            this.OnePassive = true;
            this.plusCount = 0;
            this.CountingExtedned = true;
        }
        public void EnemySkillUse(BattleEnemy user, Skill skill, List<BattleChar> targets)
        {
            if (this.MySkill.IsNowCasting &&  user is BattleEnemy)
            {
                try
                {
                    if (Standard_Tool.GetCastSkill(this.MySkill).TargetReturn().Contains(user))
                    {
                        this.plusCount++;
                        this.PlusSkillPerStat.Damage = 40 * this.plusCount;
                        this.SkillBasePlus.Target_BaseDMG = (int)(0.4f * this.plusCount * this.BChar.GetStat.atk);
                    }
                }
                catch { }

            }
        }
        public void SkillUsePatch_Before(BattleChar TargetChar, Skill TargetSkill, Skill SelectSkill)
        {
            if(this.MySkill.IsNowCasting&& SelectSkill.Master.Info.Ally==this.BChar.Info.Ally&&TargetChar!=null)
            {
                try
                {
                    List<BattleChar> list1 = BattleSystem.instance.SkillTargetReturn(SelectSkill, TargetChar, null);
                    List<BattleChar> list2 = Standard_Tool.GetCastSkill(this.MySkill).TargetReturn();
                    if (list1.Find(a => list2.Contains(a)) != null)
                    {
                        this.plusCount++;
                        this.SkillBasePlus.Target_BaseDMG = (int)(0.4f*this.plusCount * this.BChar.GetStat.atk);
                    }
                }
                catch { }


            }
        }
        public int plusCount = 0;
    }
    public class Sex_our_2_a : Sex_ImproveClone, IP_SpecialEnemyTargetSelect
    {
        public override void Init()
        {
            base.Init();
            this.CountingExtedned = true;

        }
        public bool SpecialEnemyTargetSelect(Skill skill, BattleEnemy Target)
        {
            if (this.MySkill.IsNowCasting && skill.Master.Info.Ally == this.BChar.Info.Ally )
            {
                try
                {
                    List<BattleChar> list2 = Standard_Tool.GetCastSkill(this.MySkill).TargetReturn();
                    if (list2.Contains(Target))
                    {
                        return true;
                    }
                }
                catch {  }


            }
            return false;
        }
    }

    public class Sex_our_3 : Sex_ImproveClone,IP_CriPerChange
    {
        public override void Init()
        {
            base.Init();
            this.OnePassive = true;
            this.CountingExtedned = true;
        }
        
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(this.MySkill.IsNowCounting)
            {
                this.frame++;
                if(this.frame<0 || this.frame>10)
                {
                    this.frame = 0;

                    foreach (BattleChar bc in Standard_Tool.GetCastSkill(this.MySkill).TargetReturn())
                    {
                        if (bc.BuffFind("B_SF_Ouroboros_3_h_1", false))
                        {
                            Bcl_our_3_h_1 bcl3= bc.BuffReturn("B_SF_Ouroboros_3_h_1", false) as Bcl_our_3_h_1;
                            if(!bcl3.skillList.Contains(this.MySkill))
                            {
                                bcl3.skillList.Add(this.MySkill);
                            }
                        }
                        else
                        {
                            Bcl_our_3_h_1 bcl3 = bc.BuffAdd("B_SF_Ouroboros_3_h_1",this.BChar) as Bcl_our_3_h_1;
                            if (!bcl3.skillList.Contains(this.MySkill))
                            {
                                bcl3.skillList.Add(this.MySkill);
                            }
                        }
                    }
                }

            }
        }
        public int frame = 0;

        public void CriPerChange(Skill skill, BattleChar Target, ref float CriPer)
        {
            if (this.MySkill.IsNowCasting && skill.Master.Info.Ally == this.MySkill.Master.Info.Ally)
            {
                try
                {
                    if (Standard_Tool.GetCastSkill(this.MySkill).TargetReturn().Contains(Target))
                    {
                        skill.Master.Info.G_get_stat.HitMaximum = true;
                        int num = Target.HitPerNum(skill.Master, skill, false);
                        int num2 = 0;
                        if (num > 100)
                        {
                            num2 = num - 100;
                        }
                        if (num2 > 0)
                        {
                            float num3 = 0.5f * num2;
                            CriPer += num3;
                            //Debug.Log("增加爆伤："+num3);
                            if (BattleSystem.instance.G_Particle.Find(a => a.SkillData == skill) != null )
                            {
                                if (Target.BuffFind("B_SF_Ouroboros_3_h_1", false))
                                {
                                    Bcl_our_3_h_1 bcl3 = Target.BuffReturn("B_SF_Ouroboros_3_h_1", false) as Bcl_our_3_h_1;
                                    bcl3.PlusStat.CRIGetDMG += (int)num3;
                                }
                                else
                                {
                                    Bcl_our_3_h_1 bcl3 = Target.BuffAdd("B_SF_Ouroboros_3_h_1", this.BChar) as Bcl_our_3_h_1;
                                    bcl3.PlusStat.CRIGetDMG += (int)num3;
                                }
                                Target.Info.ForceGetStat();
                            }

                        }
                    }
                }
                catch { }
            }
        }
        
    }
    public class Bcl_our_3_h_1:Buff,IP_Hit
    {
        public override void FixedUpdate()
        {
            base.FixedUpdate();

            this.frame++;
            if (this.frame < 0 || this.frame > 10)
            {
                this.frame = 0;
                for (int i = 0; i < this.skillList.Count; i++)
                {
                    if (!this.skillList[i].IsNowCounting)
                    {
                        this.skillList.RemoveAt(i);
                        i--;
                    }
                }
                this.PlusStat.dod=-30*this.skillList.Count;
                if(this.skillList.Count <= 0)
                {
                    this.SelfDestroy();
                }
            }

        }
        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            //Debug.Log("解除爆伤：" + this.PlusStat.CRIGetDMG);
            this.PlusStat.CRIGetDMG = 0;

            this.BChar.Info.ForceGetStat();
        }
        public List<Skill> skillList = new List<Skill>();
        public int frame = 0;
    }


    public class Sex_our_4 : Sex_ImproveClone
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&num1", this.plusCount.ToString());
        }
        public override void Init()
        {
            base.Init();
            this.OnePassive = true;
            this.plusCount = 0;
            this.CountingExtedned = true;
        }
        public void EnemySkillUse(BattleEnemy user, Skill skill, List<BattleChar> targets)
        {
            if (this.MySkill.IsNowCasting && user is BattleEnemy)
            {
                this.plusCount++;

            }
        }
        public void SkillUsePatch_Before(BattleChar TargetChar, Skill TargetSkill, Skill SelectSkill)
        {
            if (this.MySkill.IsNowCasting && SelectSkill.Master==this.BChar && SelectSkill.Counting>0)
            {
                this.plusCount++;
            }
        }
        public int plusCount = 0;
    }
}
