using ChronoArkMod.Plugin;
using ChronoArkMod;
using GameDataEditor;
using HarmonyLib;
using I2.Loc;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

using System.Collections;
using System.Linq;



using System.Threading.Tasks;
using UnityEngine;

using UnityEngine.EventSystems;


using DarkTonic.MasterAudio;
using TMPro;
using UnityEngine.SceneManagement;
using Steamworks;
using System.Xml.Serialization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using Curse;

namespace SF_Standard
{
    [PluginConfig("M200_SF_Standard_CharMod", "SF_Standard_CharMod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class SF_Standard_CharacterModPlugin : ChronoArkPlugin
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
        public class StandardDef : ModDefinition
        {
            
            public override List<object> BattleChar_ModIReturn(Type type, BattleChar battleChar)
            {
                
                //--------------------------------------------
                if (!SexRecords.activeProtect_ModIreturn)
                {
                    return base.BattleSystem_ModIReturn(type);
                }

                List<object> list_ans = base.BattleChar_ModIReturn(type, battleChar);

                if(battleChar is BattleAlly && battleChar.BattleInfo != null)
                {
                    if (battleChar.MyTeam != null)
                    {
                        //检查手中的技能
                        List<Skill_Extended> inHandSexs = new List<Skill_Extended>();
                        foreach (Skill skill in battleChar.MyTeam.Skills)
                        {

                                if (SexRecords.sexSkillList.Find(a => a.recSkill == skill) == null)
                                {
                                    inHandSexs.AddRange(SexRecords.RecordAndCheckSkill(skill));
                                }
                                else
                                {
                                    inHandSexs.AddRange(SexRecords.CheckSkill_Low(skill, type));
                                }
                                //SexRecords.RestoreSkill(skill, SexRecords.priorNew_Restore);
                            
                            
                        }
                        foreach(Skill_Extended sex in inHandSexs)
                        {
                            if (!sex.OnePassive)
                            {
                                list_ans.Add(sex);
                            }
                        }

                        //检查固定技能
                        List<Skill_Extended> basicSexs = new List<Skill_Extended>();
                        foreach (BattleChar battleChar2 in battleChar.MyTeam.AliveChars)
                        {
                            
                            if ((battleChar2 as BattleAlly).MyBasicSkill != null && (battleChar2 as BattleAlly).MyBasicSkill.buttonData != null)
                            {
                                Skill skill = (battleChar2 as BattleAlly).MyBasicSkill.buttonData;

                                    if (SexRecords.sexSkillList.Find(a => a.recSkill == skill) == null)
                                    {
                                        basicSexs.AddRange(SexRecords.RecordAndCheckSkill(skill));
                                    }
                                    else
                                    {
                                        basicSexs.AddRange(SexRecords.CheckSkill_Low(skill, type));
                                    }
                                    //SexRecords.RestoreSkill(skill, SexRecords.priorNew_Restore);
                                

                            }

                        }
                        foreach (Skill_Extended sex in basicSexs)
                        {
                            if (!sex.OnePassive)
                            {
                                list_ans.Add(sex);
                            }
                        }

                        //检查选中的技能
                        if(battleChar.BattleInfo.SelectedSkill!=null)
                        {

                                if (SexRecords.sexSkillList.Find(a => a.recSkill == battleChar.BattleInfo.SelectedSkill) == null)
                                {
                                    list_ans.AddRange(SexRecords.RecordAndCheckSkill(battleChar.BattleInfo.SelectedSkill));
                                }
                                else
                                {
                                    list_ans.AddRange(SexRecords.CheckSkill_Low(battleChar.BattleInfo.SelectedSkill, type));
                                }
                                //SexRecords.RestoreSkill(battleChar.BattleInfo.SelectedSkill, SexRecords.priorNew_Restore);
                            
                                
                        }
                        else if(battleChar.BattleInfo.G_Particle.Count>0 && battleChar.BattleInfo.G_Particle[0].SkillData!=null)
                        {
                            List<Skill> skills = new List<Skill>();
                            foreach (SkillParticle sp in battleChar.BattleInfo.G_Particle)
                            {
                                if (sp.SkillData!=null && !skills.Contains(sp.SkillData ))
                                {
                                    skills.Add(sp.SkillData);
                                }
                            }
                            foreach (Skill skill in skills)
                            {

                                    if (SexRecords.sexSkillList.Find(a => a.recSkill == skill) == null)
                                    {
                                        list_ans.AddRange(SexRecords.RecordAndCheckSkill(skill));
                                    }
                                    else
                                    {
                                        list_ans.AddRange(SexRecords.CheckSkill_Low(skill, type));
                                    }
                                    //SexRecords.RestoreSkill(skill, SexRecords.priorNew_Restore);
                                


                                //if (type.Name == "IP_DamageChange_sumoperation")
                                //{
                                    //if(list.Count>0)
                                    //{
                                        //UnityEngine.Debug.Log("分割线-------------------------------" + Time.frameCount);
                                        //UnityEngine.Debug.Log("还原技能：" + skill.MySkill.Name+" -: "+list.Count);
                                    //}
                                //}
                            }

                            
                        }
                    }
                }

                return list_ans;
            }

            public override List<object> BattleSystem_ModIReturn(Type type)
            {
                if(type.Name== "IP_BattleStart_UIOnBefore")
                {
                    //UnityEngine.Debug.Log("保护模式初始化");
                    if (SexRecords.SexEnsureType==1)
                    {
                        //UnityEngine.Debug.Log("保护模式初始化：弱");
                        SexRecords.activeProtect_Fixupdata = true;
                        SexRecords.activeProtect_Override = true;
                        SexRecords.activeProtect_ModIreturn = false;
                    }
                    else if(SexRecords.SexEnsureType == 2)
                    {
                        //UnityEngine.Debug.Log("保护模式初始化：强");
                        SexRecords.activeProtect_Fixupdata = true;
                        SexRecords.activeProtect_Override = true;
                        SexRecords.activeProtect_ModIreturn = true;
                    }
                    else
                    {
                        SexRecords.activeProtect_Fixupdata = false;
                        SexRecords.activeProtect_Override = false;
                        SexRecords.activeProtect_ModIreturn = false;
                    }

                    SexRecords.sexSkillList.Clear();
                }

                //--------------------------------------------
                if (!SexRecords.activeProtect_ModIreturn)
                {
                    return base.BattleSystem_ModIReturn(type);
                }
                List<object> list_ans = base.BattleSystem_ModIReturn(type);

                if(BattleSystem.instance!=null)
                {
                    //检查手中的技能
                    List<Skill_Extended> inHandSexs = new List<Skill_Extended>();
                    foreach (Skill skill in BattleSystem.instance.AllyTeam.Skills)
                    {

                            if (SexRecords.sexSkillList.Find(a => a.recSkill == skill) == null)
                            {
                                inHandSexs.AddRange(SexRecords.RecordAndCheckSkill(skill));
                            }
                            else
                            {
                                inHandSexs.AddRange(SexRecords.CheckSkill_Low(skill, type));
                            }
                            //SexRecords.RestoreSkill(skill, SexRecords.priorNew_Restore);
                        

                    }
                    foreach (Skill_Extended sex in inHandSexs)
                    {
                        if (sex.OnePassive)
                        {
                            list_ans.Add(sex);
                        }
                    }

                    //检查固定技能
                    List<Skill_Extended> basicSexs = new List<Skill_Extended>();
                    foreach (Skill skill in BattleSystem.instance.AllyTeam.Skills_Basic)
                    {
                        if(skill!=null)
                        {

                                if (SexRecords.sexSkillList.Find(a => a.recSkill == skill) == null)
                                {
                                    basicSexs.AddRange(SexRecords.RecordAndCheckSkill(skill));
                                }
                                else
                                {
                                    basicSexs.AddRange(SexRecords.CheckSkill_Low(skill, type));
                                }
                                //SexRecords.RestoreSkill(skill, SexRecords.priorNew_Restore);
                            

                        }
                    }
                    foreach (Skill_Extended sex in basicSexs)
                    {
                        if (sex.OnePassive)
                        {
                            list_ans.Add(sex);
                        }
                    }
                }
                return list_ans;
            }
            
        }
    }

    /*
    [HarmonyPatch(typeof(BattleChar))]
    public class ModIreturn_char_Plugin
    {
        public static bool pflag = false;

        [HarmonyPriority(-1)]
        [HarmonyPatch("ModIReturn_Type")]
        [HarmonyPrefix]
        private static void ModIReturnPrefix(BattleChar __instance,  Type t)
        {
            ModIreturn_char_Plugin.pflag = true;
        }

        [HarmonyPriority(-2147483648)]
        [HarmonyPatch("ModIReturn_Type")]
        [HarmonyPostfix]
        private static void ModIReturnPostfix(BattleChar __instance, List<object> __result, Type t)
        {
            if (ModIreturn_char_Plugin.pflag)
            {
                ModIreturn_char_Plugin.pflag = false;

                if (!SexRecords.activeProtect_ModIreturn)
                {
                    return;
                }

                List<object> list_ans = new List<object>();

                if (__instance is BattleAlly && __instance.BattleInfo != null)
                {
                    if (__instance.MyTeam != null)
                    {
                        //检查手中的技能
                        List<Skill_Extended> inHandSexs = new List<Skill_Extended>();
                        foreach (Skill skill in __instance.MyTeam.Skills)
                        {

                            if (SexRecords.sexSkillList.Find(a => a.recSkill == skill) == null)
                            {
                                inHandSexs.AddRange(SexRecords.RecordAndCheckSkill(skill));
                            }
                            else
                            {
                                inHandSexs.AddRange(SexRecords.CheckSkill_Low(skill, t));
                            }
                            //SexRecords.RestoreSkill(skill, SexRecords.priorNew_Restore);


                        }
                        foreach (Skill_Extended sex in inHandSexs)
                        {
                            if (!sex.OnePassive)
                            {
                                list_ans.Add(sex);
                            }
                        }

                        //检查固定技能
                        List<Skill_Extended> basicSexs = new List<Skill_Extended>();
                        foreach (BattleChar battleChar2 in __instance.MyTeam.AliveChars)
                        {

                            if ((battleChar2 as BattleAlly).MyBasicSkill != null && (battleChar2 as BattleAlly).MyBasicSkill.buttonData != null)
                            {
                                Skill skill = (battleChar2 as BattleAlly).MyBasicSkill.buttonData;

                                if (SexRecords.sexSkillList.Find(a => a.recSkill == skill) == null)
                                {
                                    basicSexs.AddRange(SexRecords.RecordAndCheckSkill(skill));
                                }
                                else
                                {
                                    basicSexs.AddRange(SexRecords.CheckSkill_Low(skill, t));
                                }
                                //SexRecords.RestoreSkill(skill, SexRecords.priorNew_Restore);


                            }

                        }
                        foreach (Skill_Extended sex in basicSexs)
                        {
                            if (!sex.OnePassive)
                            {
                                list_ans.Add(sex);
                            }
                        }

                        //检查选中的技能
                        if (__instance.BattleInfo.SelectedSkill != null)
                        {

                            if (SexRecords.sexSkillList.Find(a => a.recSkill == __instance.BattleInfo.SelectedSkill) == null)
                            {
                                list_ans.AddRange(SexRecords.RecordAndCheckSkill(__instance.BattleInfo.SelectedSkill));
                            }
                            else
                            {
                                list_ans.AddRange(SexRecords.CheckSkill_Low(__instance.BattleInfo.SelectedSkill, t));
                            }
                            //SexRecords.RestoreSkill(battleChar.BattleInfo.SelectedSkill, SexRecords.priorNew_Restore);


                        }
                        else if (__instance.BattleInfo.G_Particle.Count > 0 && __instance.BattleInfo.G_Particle[0].SkillData != null)
                        {
                            List<Skill> skills = new List<Skill>();
                            foreach (SkillParticle sp in __instance.BattleInfo.G_Particle)
                            {
                                if (sp.SkillData != null && !skills.Contains(sp.SkillData))
                                {
                                    skills.Add(sp.SkillData);
                                }
                            }
                            foreach (Skill skill in skills)
                            {

                                if (SexRecords.sexSkillList.Find(a => a.recSkill == skill) == null)
                                {
                                    list_ans.AddRange(SexRecords.RecordAndCheckSkill(skill));
                                }
                                else
                                {
                                    list_ans.AddRange(SexRecords.CheckSkill_Low(skill, t));
                                }

                            }


                        }
                    }
                }

                __result.AddRange(list_ans);
            }

        }

    }


    [HarmonyPatch(typeof(BattleSystem))]
    public class ModIreturn_Syatem_Plugin
    {
        public static bool pflag = false;

        [HarmonyPriority(-1)]
        [HarmonyPatch("ModIReturn_Type")]
        [HarmonyPrefix]
        private static void ModIReturnPrefix(BattleSystem __instance, Type t)
        {
            ModIreturn_Syatem_Plugin.pflag = true;
        }



        [HarmonyPriority(-2147483648)]
        [HarmonyPatch("ModIReturn_Type")]
        [HarmonyPostfix]
        private static void ModIReturnPostfix(BattleSystem __instance, List<object> __result, Type t)
        {
            if(ModIreturn_Syatem_Plugin.pflag)
            {
                ModIreturn_Syatem_Plugin.pflag = false;

                if (!SexRecords.activeProtect_ModIreturn)
                {
                    return;
                }


                List<object> list_ans = new List<object>();

                if (__instance != null)
                {
                    //检查手中的技能
                    List<Skill_Extended> inHandSexs = new List<Skill_Extended>();
                    foreach (Skill skill in __instance.AllyTeam.Skills)
                    {

                        if (SexRecords.sexSkillList.Find(a => a.recSkill == skill) == null)
                        {
                            inHandSexs.AddRange(SexRecords.RecordAndCheckSkill(skill));
                        }
                        else
                        {
                            inHandSexs.AddRange(SexRecords.CheckSkill_Low(skill, t));
                        }
                        //SexRecords.RestoreSkill(skill, SexRecords.priorNew_Restore);


                    }
                    foreach (Skill_Extended sex in inHandSexs)
                    {
                        if (sex.OnePassive)
                        {
                            list_ans.Add(sex);
                        }
                    }

                    //检查固定技能
                    List<Skill_Extended> basicSexs = new List<Skill_Extended>();
                    foreach (Skill skill in __instance.AllyTeam.Skills_Basic)
                    {
                        if (skill != null)
                        {

                            if (SexRecords.sexSkillList.Find(a => a.recSkill == skill) == null)
                            {
                                basicSexs.AddRange(SexRecords.RecordAndCheckSkill(skill));
                            }
                            else
                            {
                                basicSexs.AddRange(SexRecords.CheckSkill_Low(skill, t));
                            }
                            //SexRecords.RestoreSkill(skill, SexRecords.priorNew_Restore);


                        }
                    }
                    foreach (Skill_Extended sex in basicSexs)
                    {
                        if (sex.OnePassive)
                        {
                            list_ans.Add(sex);
                        }
                    }

                    __result.AddRange(list_ans);
                }
            }
            
        }
        
    }



    */
    
    [HarmonyPatch(typeof(Character))]
    
    public class CharacterPasFixPlugin
    {
        
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("get_Passive")]
        [HarmonyPriority(-2147483648)]
        [HarmonyFinalizer]//(-2147483648)
        public static void PasProtect_Patch(Character __instance,ref Passive_Char __result)
        {
            if (!SexRecords.activePassive)
            {
                return;
            }

            bool flag1 = false;
            if(__instance.KeyData!=null)
            {
                flag1 = flag1 || __instance.KeyData.StartsWith("SF_");
                flag1 = flag1 || __instance.KeyData.StartsWith("GFL_");
                flag1 = flag1 || __instance.KeyData.StartsWith("PD_");
            }



            //if ( ToolTipWindowPlugin.ModAuthor(__instance.KeyData) == "m200")
            if (flag1) 
            //if (__instance.KeyData.StartsWith("SF_")||__instance.KeyData.StartsWith("GFL_")|| __instance.KeyData.StartsWith("PD_"))
            {
                //UnityEngine.Debug.Log("角色作者：" + ToolTipWindowPlugin.ModAuthor(__instance.KeyData));
                //UnityEngine.Debug.Log("监测点get_Passive：" + (__instance._Passive != null));

                bool flag = false;
                if (__instance.GetData != null && __instance.GetData.PassiveUnlockLV1)
                {
                    flag = true;
                }
                else if (__instance.LV >= 2)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }

                if (flag)
                {
                    __result = __instance._Passive;


                }
               
                
            }



        }
        public static bool print = false;
        public static int frame = 0;
    }
    [HarmonyPriority(-1)]
    

    //----------强制buff与无法移除buff 受保护Sex记录
    [HarmonyPatch(typeof(BattleSystem))]
    public class BattleSystemFixPlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("FixedUpdate")]
        [HarmonyPostfix]
        public static void FixedUpdate_Patch()
        {
            //Debug.Log("FixedUpdate"+Time.frameCount);
            if(BattleSystemFixPlugin.framenum>10 || BattleSystemFixPlugin.framenum < 0)
            {
                BattleSystemFixPlugin.framenum = 0;

                //----------------------------------------------buff检测
                foreach(InBuff inbuff in InBuffRecords.inBuffs)
                {
                    if(BattleSystem.instance==null || (!BattleSystem.instance.AllyTeam.AliveChars_Vanish.Contains(inbuff.BChar)&& !BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Contains(inbuff.BChar)))
                    {
                        inbuff.remove = true;
                    }
                    else if(!inbuff.BChar.Buffs.Contains(inbuff) && !inbuff.remove)
                    {
                        //Debug.Log("标记1");
                        //Debug.Log(inbuff.BuffData.Name+ ": " + inbuff.BChar.Info.Name);
                        //Debug.Log("标记2");
                        inbuff.DestroyBuff = false;
                        inbuff.isDestroyDone = false;
                        if (inbuff.StackInfo.Count<=0)
                        {
                            StackBuff sta=new StackBuff();
                            sta.UseState = inbuff.userrec;
                            inbuff.StackInfo.Add(sta);
                        }
                        //inbuff.BChar.BuffAddDelay(inbuff, true);
                        //Debug.Log("标记3");
                        inbuff.Set(null);
                        inbuff.BChar.Buffs.Add(inbuff);
                        BattleSystem.DelayInputAfter(BattleSystem.instance.BuffAddDelay(inbuff, false));
                        //Debug.Log("标记4");
                    }       
                }
                InBuffRecords.inBuffs.RemoveAll(a => a.remove);

                foreach (StaRecord staRec in InBuffRecords.staInRecs)
                {
                    StaInBuff.CheckAndSet(staRec);
                }
                InBuffRecords.staInRecs.RemoveAll(a => a.buff.remove);



                //------------------------------Sex检测
                
                if(SexRecords.activeProtect_Fixupdata)
                {
                    if(BattleSystemFixPlugin.framenum2<Time.frameCount+50)
                    {
                        BattleSystemFixPlugin.framenum2 = Time.frameCount + 50;
                        SexRecords.AllRecord();
                    }
                    

                }           
                

                /*
                if(BattleSystem.instance != null && (BattleSystemFixPlugin.framenum2+100)<Time.frameCount)
                {
                    BattleSystemFixPlugin.framenum2=Time.frameCount;
                    foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars)
                    {
                        if(bc.Info.KeyData=="SF_Dreamer")
                        {
                            CharacterPasFixPlugin.print = true;
                            UnityEngine.Debug.Log("检测被动："+(bc.Info.Passive!=null));
                            UnityEngine.Debug.Log("检测被动_：" + (bc.Info._Passive != null));
                            CharacterPasFixPlugin.print = false;
                        }
                    }
                }
                */



            }
            BattleSystemFixPlugin.framenum++;
        }
        private static int framenum=0;

        private static int framenum2=0;
    }
    



    public class InBuffRecords
    {

        public static List<InBuff> inBuffs = new List<InBuff>();
        public static List<StaRecord> staInRecs = new List<StaRecord>();
    }
    public class StaRecord
    {
        public StaInBuff buff = null;
        public List<StackBuff> stas = new List<StackBuff>();
    }
    public class InBuff : ForBuff
    {
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            if(!InBuffRecords.inBuffs.Contains(this)&& !(this is StaInBuff))
            {
                InBuffRecords.inBuffs.Add(this);
                this.userrec = this.Usestate_L;
            }
            //Debug.Log("Inbuffs:"+InBuffRecords.inBuffs.Count);
        }

        public void InBuffDestroy()
        {
            this.remove = true;
            this.SelfDestroy();
        }
        
        public BattleChar userrec=new BattleChar();
        public bool remove = false;
    }

    public class StaInBuff:InBuff
    {
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            this.infield = true;
            this.UpdateRecord();
            //Debug.Log("Inbuffs:"+InBuffRecords.inBuffs.Count);
        }
        public override void Init()
        {
            base.Init();
            if(this.infield )
            {
                this.UpdateRecord();
            }

        }
        public bool infield = false;
        public void UpdateRecord()
        {
            if (InBuffRecords.staInRecs.Find(a => a.buff == this) == null)
            {
                StaRecord rec = new StaRecord { buff = this };
                rec.stas.AddRange(this.StackInfo);
                InBuffRecords.staInRecs.Add(rec);
            }
            else
            {
                StaRecord rec = InBuffRecords.staInRecs.Find(a => a.buff == this);
                rec.stas.Clear();
                rec.stas.AddRange(this.StackInfo);

            }
        }
        public void RemoveStack(int x)
        {
            if(x>=0 && x<this.StackNum)
            {
                StackBuff sta = this.StackInfo[x];
                if (InBuffRecords.staInRecs.Find(a => a.buff == this) != null)
                {
                    StaRecord rec = InBuffRecords.staInRecs.Find(a => a.buff == this);
                    rec.stas.Remove(sta);
                }
                this.StackInfo.RemoveAt(x);
            }
        }

        public void RemoveStack(StackBuff sta)
        {

                if (InBuffRecords.staInRecs.Find(a => a.buff == this) != null)
                {
                    StaRecord rec = InBuffRecords.staInRecs.Find(a => a.buff == this);
                    rec.stas.Remove(sta);
                }
                this.StackInfo.Remove(sta);
            
        }

        public void CheckAndSet()
        {
            foreach (StaRecord staRec in InBuffRecords.staInRecs)
            {
                if(staRec.buff == this)
                {
                    StaInBuff.CheckAndSet(staRec);
                }
            }
        }
        public static void CheckAndSet(StaRecord staRec)
        {
            if (BattleSystem.instance == null || (!BattleSystem.instance.AllyTeam.AliveChars_Vanish.Contains(staRec.buff.BChar) && !BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Contains(staRec.buff.BChar)))
            {
                staRec.buff.remove = true;
            }
            else if (!staRec.buff.remove)
            {
                if (!staRec.buff.BChar.Buffs.Contains(staRec.buff))
                {
                    staRec.buff.DestroyBuff = false;
                    staRec.buff.isDestroyDone = false;

                    staRec.buff.StackInfo.Clear();
                    staRec.buff.StackInfo.AddRange(staRec.stas);


                    //inbuff.BChar.BuffAddDelay(inbuff, true);
                    //Debug.Log("标记3");
                    staRec.buff.Set(null);
                    staRec.buff.BChar.Buffs.Add(staRec.buff);
                    BattleSystem.DelayInputAfter(BattleSystem.instance.BuffAddDelay(staRec.buff, false));
                }
                else
                {
                    foreach (StackBuff stack in staRec.stas)
                    {
                        if (!staRec.buff.StackInfo.Contains(stack))
                        {
                            staRec.buff.StackInfo.Add(stack);
                        }
                    }
                }
                //Debug.Log("标记1");
                //Debug.Log(inbuff.BuffData.Name+ ": " + inbuff.BChar.Info.Name);
                //Debug.Log("标记2");

                //Debug.Log("标记4");
            }
        }
    }






    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("BuffAdd")]
    public static class BuffAddPlugin
    {
        
        [HarmonyPrefix]
        public static void BuffAdd_Before(BattleChar __instance, string key, BattleChar UseState, bool hide, int PlusTagPer, bool debuffnonuser, int RemainTime, bool StringHide,out ForCheckRec __state)
        {
            __state = null;
            if(BattleSystem.instance==null)
            {
                return;
            }
            //Debug.Log("BuffAdd检查点1");
            GDEBuffData gdebuffData = new GDEBuffData(key);
            //Buff buff = Buff.DataToBuff(gdebuffData, __instance, UseState, RemainTime, true, 0);
            //Buff buff = Buff.DataToBuff(gdebuffData, __instance, UseState, RemainTime, true, 0);
            Buff buff1 = new Buff();
            Type type = ModManager.GetType(gdebuffData.ClassName);
            if (type != null)
            {
                buff1 = (Buff)Activator.CreateInstance(type);
            }
            bool flag1 = false;
            if(buff1 is ForBuff )
            {
                
                if((buff1 as ForBuff).GetForce())
                {
                    flag1 = true;
                }
            }

            if (flag1)//(buff is ForBuff && (buff as ForBuff).force)
            {
                //Buff buff = Buff.DataToBuff(gdebuffData, __instance, UseState, RemainTime, true, 0);
                StackBuff stackBuff = new StackBuff();
                if (gdebuffData.LifeTime != 0f)
                {
                    if (RemainTime != -1)
                    {
                        stackBuff.RemainTime = RemainTime;
                    }
                    else
                    {
                        stackBuff.RemainTime = (int)gdebuffData.LifeTime;
                    }
                }
                stackBuff.UseState = UseState;

                ForCheckRec rec= new ForCheckRec();
                rec.buffchar = __instance;
                rec.buffkey = key;
                rec.hide = hide;
                rec.RemainTime = RemainTime;
                rec.StringHide = StringHide;
                if(__instance.BuffFind(key,false))
                {
                    rec.oldstacks.AddRange(__instance.BuffReturn(key, false).StackInfo);
                }

                //rec.newstacks.AddRange(buff.StackInfo);
                rec.newstacks.Add(stackBuff);

                __state = rec;
                //BuffAddPlugin.checkingrec = rec;
                //BuffAddPlugin.pflag = true;

                //Debug.Log("方法待匹配" + key);
            }
            //Debug.Log("BuffAdd检查点3");
        }
        //[HarmonyFinalizer]
        
        [HarmonyPostfix]
        public static void BuffAdd_Final(BattleChar __instance, string key, BattleChar UseState, bool hide, int PlusTagPer, bool debuffnonuser, int RemainTime, bool StringHide, ForCheckRec __state)
        {
            if (BattleSystem.instance == null)
            {
                BuffAddPlugin.pflag = false;
                return;
            }
            
            if (__state!=null)//(BuffAddPlugin.pflag && BuffAddPlugin.checkingrec != null)
            {

                //Debug.Log("BuffAdd_post检查点1:"+key);
                //BuffAddPlugin.pflag = false;
                //ForCheckRec rec = BuffAddPlugin.checkingrec;
                //BuffAddPlugin.checkingrec = null;
                ForCheckRec rec = __state;

                if (rec.buffchar.IsDead || rec.buffchar==null)
                {
                    return;
                }
                if (!rec.buffchar.BuffFind(rec.buffkey, false))
                {
                    foreach (StackBuff sta in rec.newstacks)
                    {
                        ForBuff.ForBuffAdd(rec.buffchar, rec.buffkey, sta.UseState, rec.hide, rec.RemainTime, rec.StringHide);
                    }

                }
                else
                {
                    Buff buff = rec.buffchar.BuffReturn(rec.buffkey, false);
                    List<StackBuff> stas = new List<StackBuff>();
                    stas.AddRange(buff.StackInfo);
                    stas.RemoveAll(a => rec.oldstacks.Contains(a));
                    foreach (StackBuff sta in rec.newstacks)
                    {
                        if (stas.FindAll(a => a.UseState == sta.UseState).Count <= 0)
                        {
                            ForBuff.ForBuffAdd(rec.buffchar, rec.buffkey, sta.UseState, rec.hide, rec.RemainTime, rec.StringHide);
                        }
                    }
                }
            }
        
            //BuffAddPlugin.pflag = false;
            //Debug.Log("BuffAdd检查点3");
        }
        
        public static ForCheckRec checkingrec= null; 
        public static bool pflag = false;
    }
    public class ForCheckRec
    {
        public int match = 0;
        public BattleChar buffchar = new BattleChar();
        public string buffkey = string.Empty;
        public bool hide = false;
        public int RemainTime = -1;
        public bool StringHide = false;
        public List<StackBuff> oldstacks = new List<StackBuff>();
        public List<StackBuff> newstacks = new List<StackBuff>();
    }


    public class ForBuff : Buff
    {
        public virtual void ForBuff_Awake()
        {

        }
        public virtual void ForBuffAdded_Before(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {
            //Debug.Log("ForBuffAdded_Before");
            //Debug.Log("ForBuffadd 1:"+addedbuff.BuffData.Key);
            //Debug.Log("only damage drm");
            if(BuffTaker.BuffFind(addedbuff.BuffData.Key,false))
            {
                IP_BuffAdd ip_patch = BuffTaker.BuffReturn(addedbuff.BuffData.Key, false) as IP_BuffAdd;
                bool flag = ip_patch != null;
                if (flag)
                {
                    ip_patch.Buffadded(BuffUser, BuffTaker, addedbuff);
                }
            }

            //Debug.Log("ForBuffadd 2");
        }
        public virtual void ForBuffAdded_After(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            //Debug.Log("ForBuffaddAfter 1");
            //Debug.Log("ForBuffAdded_After");
            IP_BuffAddAfter ip_patch = addedbuff as IP_BuffAddAfter;
            bool flag = ip_patch != null;
            if (flag)
            {
                ip_patch.BuffaddedAfter(BuffUser, BuffTaker, addedbuff, stackBuff);
            }
            //Debug.Log("ForBuffaddAfter 2");
        }
        public virtual bool GetForce()
        {
            return false;
        }
        public static void ForBuffAdd(BattleChar Taker,string key, BattleChar UseState, bool hide = false,  int RemainTime = -1, bool StringHide = false)
        {
            GDEBuffData gdebuffData = new GDEBuffData(key);
            Buff buff = Buff.DataToBuff(gdebuffData, Taker, UseState, RemainTime, false, 0);
            //GDEBuffData gdebuffData = buff.BuffData;
            if (hide || gdebuffData.Hide)
            {
                buff.IsHide = true;
            }
            if (RemainTime != -1)
            {
                buff.LifeTime = RemainTime;
            }
            if (BattleSystem.instance != null)
            {
                if (Taker.BuffFind(key, false))
                {
                    ForBuff forbuff = Taker.BuffReturn(key, false) as ForBuff;
                    forbuff.ForBuffAdded_Before(UseState, Taker, buff);
                }
            }


            List<Buff> list = new List<Buff>();
            list.AddRange(Taker.Buffs);
            list.AddRange(Taker.Info.Buffs_Field);

            foreach (Buff buff2 in list)
            {
                if (buff2 is ForBuff&&!buff2.DestroyBuff && buff2.BuffData.Key == gdebuffData.Key)
                {
                    int num = gdebuffData.MaxStack;
                    if (gdebuffData.MaxStack <= 0)
                    {
                        num = 1;
                    }
                    if (buff2.StackNum >= num)
                    {
                        bool flag2 = false;
                        if (RemainTime != -1)
                        {
                            for (int i = 0; i < buff2.StackInfo.Count; i++)
                            {
                                if (RemainTime > buff2.StackInfo[i].RemainTime)
                                {
                                    buff2.StackInfo.RemoveAt(i);
                                    flag2 = true;
                                    break;
                                }
                            }
                        }
                        if (!flag2)
                        {
                            buff2.StackInfo.RemoveAt(0);
                        }
                    }
                    StackBuff stackBuff = new StackBuff();
                    if (RemainTime != -1)
                    {
                        stackBuff.RemainTime = RemainTime;
                    }
                    else
                    {
                        stackBuff.RemainTime = (int)gdebuffData.LifeTime;
                    }
                    stackBuff.UseState = UseState;
                    buff2.StackInfo.Add(stackBuff);
                    buff2.FirstTurn = false;
                    buff2.EffectOut = false;
                    buff2.Init();
                    if (BattleSystem.instance != null)
                    {
                        if (!buff2.IsHide && StringHide)
                        {
                            BattleSystem.DelayInputAfter(BattleSystem.instance.BuffAddDelay(buff2, false));
                        }
                        else if (!buff2.IsHide)
                        {
                            BattleSystem.DelayInputAfter(BattleSystem.instance.BuffAddDelay(buff2, true));
                        }
                    }
                    (buff2 as ForBuff).ForBuff_Awake();
                    if (BattleSystem.instance != null)
                    {
                        (buff2 as ForBuff).ForBuffAdded_After(UseState, Taker, buff2, stackBuff);
                    }
                    return;
                }
            }

            buff.Set(null);
            //buff.BChar.Buffs.Add(buff);
            if (!buff.BuffData.IsFieldBuff)
            {
                Taker.Buffs.Add(buff);
            }
            else
            {
                if (BattleSystem.instance != null)
                {
                    Taker.Buffs.Add(buff);
                }
                Taker.Info.Buffs_Field.Add(buff);
            }
            BattleSystem.DelayInputAfter(BattleSystem.instance.BuffAddDelay(buff, false));
            if (BattleSystem.instance != null)
            {
                (buff as ForBuff).ForBuffAdded_After(UseState, Taker, buff, buff.StackInfo[0]);
            }
        }
        public bool force = false;
    }

    


    //----------------滚轮浏览技能
    [HarmonyPatch(typeof(ToolTipWindow))]
    public class ToolTipWindowPlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("SkillToolTip")]
        [HarmonyPostfix]
        public static void SkillToolTip_Patch(Transform ParentTransform, Skill SkillData, BattleChar data, int pivotx = 0, int pivoty = 1, bool tooltipdestroy = true, bool ViewSkill = false, bool ForceManaView = false)//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
            if (ToolTipWindowPlugin.viewSkill == true && ToolTipWindowPlugin.skillkey == SkillData.MySkill.KeyID)
            {
                //Debug.Log("重复SkillToolTip");
            }
            if (ToolTipWindowPlugin.ModAuthor(SkillData.MySkill.KeyID) == "m200")
            {
                ToolTipWindowPlugin.viewSkill = true;
                ToolTipWindowPlugin.skillkey = SkillData.MySkill.KeyID;
                ToolTipWindowPlugin.skill = SkillData;
            }

        }
        [HarmonyPatch("SkillToolTip_Collection")]
        [HarmonyPostfix]
        public static void SkillToolTip_Collection_Patch(Transform ParentTransform, Skill SkillData, BattleChar data, int pivotx = 0, int pivoty = 1, bool tooltipdestroy = true, bool ViewSkill = false, bool ForceManaView = false)//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
            if(ToolTipWindowPlugin.viewSkill == true && ToolTipWindowPlugin.skillkey == SkillData.MySkill.KeyID)
            {
                //Debug.Log("重复SkillToolTip_Collection");
            }
            if (ToolTipWindowPlugin.ModAuthor(SkillData.MySkill.KeyID) == "m200")
            {
                ToolTipWindowPlugin.viewSkill = true;
                ToolTipWindowPlugin.skillkey = SkillData.MySkill.KeyID;
                ToolTipWindowPlugin.skill = SkillData;
            }


        }
        [HarmonyPatch("ToolTipDestroy")]
        [HarmonyPostfix]
        public static void ToolTipDestroy_Patch()
        {
            ToolTipWindowPlugin.viewSkill = false;
            ToolTipWindowPlugin.skillkey = string.Empty;
            ToolTipWindowPlugin.skill = new Skill();
        }


        public static string ModAuthor(string key)
        {
            if (ToolTipWindow.ModInfoCheck(key))
            {
                foreach (string id in ModManager.LoadedMods)
                {
                    if (ModManager.getModInfo(id).gdeinfo.Added.Contains(key))
                    {
                        return ModManager.getModInfo(id).Author;
                    }
                }
            }
            return string.Empty;
        }
        public static bool viewSkill = false;
        public static string skillkey = string.Empty;
        public static Skill skill=new Skill();
    }

    [HarmonyPatch(typeof(Collections))]//阻止图鉴中的滚轮效果
    public class CollectionsPlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("get_Scroll")]
        [HarmonyPostfix]
        public static void SkillToolTip_Patch(ref float __result)//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
            if (ToolTipWindowPlugin.viewSkill && ToolTipWindowPlugin.ModAuthor(ToolTipWindowPlugin.skillkey) == "m200")
            {
                __result = 0f;
            }
        }
    }

    [HarmonyPatch(typeof(ScrollEvent))]//阻止倒计时栏中的滚轮效果
    public class OnScrollPlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("OnScroll")]
        [HarmonyPrefix]
        public static bool OnScroll_Patch()//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
            if (ToolTipWindowPlugin.viewSkill && ToolTipWindowPlugin.ModAuthor(ToolTipWindowPlugin.skillkey) == "m200")
            {
                if(ToolTipWindowPlugin.skill.MyButton!=null)// && ToolTipWindowPlugin.skill.MyButton.castskill != null)
                {
                    return false;
                }
            }
            return true;
        }
    }

    




    [HarmonyPatch(typeof(SkillToolTip))]
    public class UpdatePlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        public static void Update_Patch()//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
            if (ToolTipWindow.ToolTip != null && Input.GetAxis("Mouse ScrollWheel") != 0 && ToolTipWindowPlugin.viewSkill && ToolTipWindowPlugin.ModAuthor(ToolTipWindowPlugin.skillkey) == "m200"&&UpdatePlugin.frame != Time.frameCount)
            {
                UpdatePlugin.frame = Time.frameCount;
                float sizey = ToolTipWindow.ToolTip.GetComponent<RectTransform>().rect.yMax - ToolTipWindow.ToolTip.GetComponent<RectTransform>().rect.yMin;
                float sizey_plus = ToolTipWindow.ToolTip.GetComponent<SkillToolTip>().PlusTooltip.GetComponent<RectTransform>().rect.yMax - ToolTipWindow.ToolTip.GetComponent<SkillToolTip>().PlusTooltip.GetComponent<RectTransform>().rect.yMin;

                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    float x = ToolTipWindow.ToolTip.transform.localPosition.x;
                    float y = ToolTipWindow.ToolTip.transform.localPosition.y;
                    float z = ToolTipWindow.ToolTip.transform.localPosition.z;
                    if (y > 540)
                    {
                        y = Math.Max(540, y - 100);
                    }

                    ToolTipWindow.ToolTip.transform.localPosition = new Vector3(x, y, z);
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    float x = ToolTipWindow.ToolTip.transform.localPosition.x;
                    float y = ToolTipWindow.ToolTip.transform.localPosition.y;
                    float z = ToolTipWindow.ToolTip.transform.localPosition.z;
                    if (y - sizey < -540 || y - sizey_plus < -540)
                    {
                        y = Math.Min(y + 100, -540 + Math.Max(sizey_plus, sizey));
                    }

                    ToolTipWindow.ToolTip.transform.localPosition = new Vector3(x, y, z);
                }


            }
            //Debug.Log("检查点1:SkillToolTip_Collection");
        }
        public static int frame = -1;
    }



    /*--------------额外伤害修正
    [HarmonyPatch(typeof(SkillTargetTooltip))]
    public class InputInfo_Plugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("InputInfo")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var methodInfo = AccessTools.Method(typeof(DamageCaculate), nameof(DamageCaculate.DamageChangePlus_View), new System.Type[] { typeof(Skill), typeof(BattleChar), typeof(int), typeof(bool), typeof(bool) });
            //var FieldInfo = AccessTools.Field(typeof(SkillParticle), "SkillData");
            var codes = instructions.ToList();

            bool triggered = false;
            
            for (int i = 0; i < codes.Count; i++)
            {
                if (i > 7)
                {
                    bool f0 = true;
                    f0 = f0 && codes[i - 6].opcode == OpCodes.Ldarg_2;
                    f0 = f0 && codes[i - 5].opcode == OpCodes.Ldarg_1;
                    f0 = f0 && codes[i - 4].opcode == OpCodes.Ldloc_0;
                    f0 = f0 && codes[i - 3].opcode == OpCodes.Ldloc_S;
                    
                    try
                    {
                        f0 = f0 && (codes[i - 3].operand is int && ((int)codes[i - 3].operand) == 14);
                    }
                    catch { f0 = false; }
                    
                    f0 = f0 && codes[i - 2].opcode == OpCodes.Ldc_I4_1;
                    f0 = f0 && codes[i - 1].opcode == OpCodes.Call;
                    try
                    {
                        f0 = f0 && (codes[i - 1].operand is MethodInfo && ((MethodInfo)(codes[i - 1].operand)).DeclaringType == typeof(DamageCaculate) && ((MethodInfo)(codes[i - 1].operand)).Name == "DamageChangePlus_View");
                    }
                    catch { f0 = false; }
                    f0 = f0 && codes[i - 0].opcode == OpCodes.Stloc_0;

                    if (f0)
                    {
                        //Debug.Log("test:" + codes[i - 3].operand.GetType());
                        triggered = true;
                    }
                }
            }
            
            for (int i = 0; i < codes.Count; i++)
            {
                if (i > 5)
                {

                    bool f1 = true;
                    f1 = f1 && codes[i - 3].opcode == OpCodes.Ldloc_0;
                    f1 = f1 && codes[i - 2].opcode == OpCodes.Ldloc_S;
                    try
                    {
                        f1 = f1 && (codes[i - 2].operand is LocalBuilder && ((LocalBuilder)codes[i - 2].operand).LocalIndex == 5);
                    }
                    catch { f1 = false; }
                    f1 = f1 && codes[i - 1].opcode == OpCodes.Add;
                    f1 = f1 && codes[i - 0].opcode == OpCodes.Stloc_0;
                    if (f1)
                    {
                        yield return codes[i];

                        if (!triggered)
                        {
                            Debug.Log("InputInfo_Pluginx修改：" + i);
                            yield return new CodeInstruction(OpCodes.Ldarg_2);
                            //yield return new CodeInstruction(OpCodes.Ldfld,FieldInfo);
                            yield return new CodeInstruction(OpCodes.Ldarg_1);
                            yield return new CodeInstruction(OpCodes.Ldloc_0);
                            yield return new CodeInstruction(OpCodes.Ldloc_S,14);
                            yield return new CodeInstruction(OpCodes.Ldc_I4_1);

                            yield return new CodeInstruction(OpCodes.Call, methodInfo);
                            yield return new CodeInstruction(OpCodes.Stloc_0);
                        }
                        else
                        {
                            Debug.Log("InputInfo_Pluginx阻止修改：" + i);
                        }

                        continue;
                    }
                }
                yield return codes[i];
            }
        }

    }
    
    [HarmonyPatch(typeof(BattleChar))]
    public class Effect_Plugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("Effect")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var methodInfo = AccessTools.Method(typeof(DamageCaculate), nameof(DamageCaculate.DamageChangePlus), new System.Type[] { typeof(SkillParticle), typeof(BattleChar), typeof(int), typeof(bool), typeof(bool) });
            //var FieldInfo = AccessTools.Field(typeof(SkillParticle), "SkillData");
            var codes = instructions.ToList();

            bool triggered = false;
            for (int i = 0; i < codes.Count; i++)
            {
                if (i > 7)
                {
                    bool f0 = true;
                    f0 = f0 && codes[i - 7].opcode == OpCodes.Ldarg_1;
                    f0 = f0 && codes[i - 6].opcode == OpCodes.Ldarg_0;
                    f0 = f0 && codes[i - 5].opcode == OpCodes.Ldloc_2;
                    f0 = f0 && codes[i - 4].opcode == OpCodes.Conv_I4;
                    f0 = f0 && codes[i - 3].opcode == OpCodes.Ldloc_3;
                    f0 = f0 && codes[i - 2].opcode == OpCodes.Ldc_I4_0;
                    f0 = f0 && codes[i - 1].opcode == OpCodes.Call;
                    try
                    {
                        f0 = f0 && (codes[i - 1].operand is MethodInfo && ((MethodInfo)(codes[i - 1].operand)).DeclaringType == typeof(DamageCaculate) && ((MethodInfo)(codes[i - 1].operand)).Name== "DamageChangePlus");
                    }
                    catch { f0 = false; }
                    f0 = f0 && codes[i - 0].opcode == OpCodes.Stloc_2;

                    if(f0 )
                    {
                        triggered = true;
                    }
                }
            }
         
            for (int i = 0; i < codes.Count; i++)
            {
                if (i > 5)
                {

                    bool f1 = true;
                    f1 = f1 && codes[i - 4].opcode == OpCodes.Ldloc_2;
                    f1 = f1 && codes[i - 3].opcode == OpCodes.Ldloc_S;
                    
                    try
                    {
                        f1 = f1 && (codes[i - 3].operand is LocalBuilder && ((LocalBuilder)codes[i - 3].operand).LocalIndex== 4);
                    }
                    catch { f1 = false;}
                    
                    f1 = f1 && codes[i - 2].opcode == OpCodes.Conv_R4;             
                    f1 = f1 && codes[i - 1].opcode == OpCodes.Add;
                    f1 = f1 && codes[i - 0].opcode == OpCodes.Stloc_2;
                    if (f1)
                    {
                        yield return codes[i];

                        if (!triggered)
                        {
                            Debug.Log("Effect_Pluginx修改：" + i);
                            yield return new CodeInstruction(OpCodes.Ldarg_1);
                            //yield return new CodeInstruction(OpCodes.Ldfld,FieldInfo);
                            yield return new CodeInstruction(OpCodes.Ldarg_0);
                            yield return new CodeInstruction(OpCodes.Ldloc_2);
                            yield return new CodeInstruction(OpCodes.Conv_I4);
                            yield return new CodeInstruction(OpCodes.Ldloc_3);
                            yield return new CodeInstruction(OpCodes.Ldc_I4_0);

                            yield return new CodeInstruction(OpCodes.Call, methodInfo);
                            yield return new CodeInstruction(OpCodes.Stloc_2);
                        }
                        else
                        {
                            Debug.Log("Effect_Pluginx阻止修改：" + i);
                        }
                        
                        continue;
                    }
                }
                yield return codes[i];
            }
        }
        
    }
    public static class DamageCaculate
    {
        public static int DamageChangePlus_View(Skill skill, BattleChar Target, int Damage, bool Cri, bool View)
        {
            Debug.Log("触发:IP_DamageChangeFinal_V——1");
            float result = Damage;
            foreach (IP_DamageChangeFinal ip_1 in skill.Master.IReturn<IP_DamageChangeFinal>(skill))
            {
                try
                {
                    if (ip_1 != null)
                    {
                        result = (float)ip_1.DamageChangeFinal(skill, Target, (int)result, Cri, View);
                    }
                }
                catch
                {
                }
            }
            Debug.Log("触发:IP_DamageChangeFinal_V——2");
            return (int)result;
        }
        public static float DamageChangePlus(SkillParticle SP, BattleChar Target, int Damage, bool Cri, bool View)
        {
            Debug.Log("触发:IP_DamageChangeFinal——1");
            float result = Damage;
            foreach (IP_DamageChangeFinal ip_1 in SP.UseStatus.IReturn<IP_DamageChangeFinal>(SP.SkillData))
            {
                try
                {
                    if (ip_1 != null)
                    {
                        result = (float)ip_1.DamageChangeFinal(SP.SkillData, Target, (int)result, Cri, View);
                    }
                }
                catch
                {
                }
            }
            Debug.Log("触发:IP_DamageChangeFinal——2");
            return result;
        }
        //public static bool changed = false;
    }
    public interface IP_DamageChangeFinal
    {
        // Token: 0x06002783 RID: 10115
        int DamageChangeFinal(Skill SkillD, BattleChar Target, int Damage, bool Cri, bool View);
    }
    */






    public class Sex_ImproveClone:Skill_Extended
    {
        public override object Clone()
        {
            var clone = base.Clone(); ;

            // 获取所有实例字段（包括私有）
            var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                // 检查是否为List<T>
                if (field.FieldType.IsGenericType &&
                    field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    // 获取原始列表
                    var originalList = (IEnumerable)field.GetValue(this);

                    if (originalList == null)
                        continue;

                    // 创建新列表并复制元素
                    var elementType = field.FieldType.GetGenericArguments()[0];
                    var newList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));

                    foreach (var item in originalList)
                        newList.Add(item);

                    // 将新列表赋值给克隆对象
                    field.SetValue(clone, newList);
                }
            }

            return clone;
        }
        /*
        public virtual bool Recorded()
        {
            return true;
        }
        */
        public virtual bool RecordAsDataSex()
        {
            return true;
        }
        public virtual bool RecordAsItemSex()
        {
            return true;
        }


        public virtual void RemoveSexFrom(Skill skill)
        {
            skill.AllExtendeds.Remove(this);
            if(SexRecords.sexSkillList.Find(a=>a.recSkill==skill)!=null)
            {
                SexRec rec = SexRecords.sexSkillList.Find(a => a.recSkill == skill);
                rec.recExtended.Remove(this);
            }
        }

        public bool needRecorded = true;
    }

    public static class Standard_Tool
    {
        public static Skill OriginSkill(Skill skillD)
        {
            Skill skill = skillD;
            while(skill.OriginalSelectSkill != null)
            {
                skill = skill.OriginalSelectSkill;
            }
            return skill;
        }

        public static CastingSkill GetCastSkill(Skill_Extended ex)
        {
            return Standard_Tool.GetCastSkill(ex.MySkill);
        }
        public static CastingSkill GetCastSkill(Skill skill)
        {
            if (skill.MyButton != null && skill.MyButton.castskill != null && skill.MyButton.castskill.skill == skill)
            {
                return skill.MyButton.castskill;
            }
            else if (BattleSystem.instance != null)
            {
                if (BattleSystem.instance.EnemyCastSkills.Find(a => a.skill == skill) != null)
                {
                    return BattleSystem.instance.EnemyCastSkills.Find(a => a.skill == skill);
                }
            }
            return null;
        }



        public static bool JudgeSkillSex(Skill skill,Skill_Extended sex)
        {
            return (sex.MySkill == skill)||(skill.AllExtendeds.Contains(sex));
        }
    }












    public class Item_SF_1 : UseitemBase
    {

        public override bool Use(Character CharInfo)
        {
            /*
            bool flag = false;
            for (int i = 0; i < PartyInventory.InvenM.InventoryItems.Count; i++)
            {
                if (PartyInventory.InvenM.InventoryItems[i] != null && PartyInventory.InvenM.InventoryItems[i] == FieldSystem.instance.UseingItemObject)
                {
                    flag = true;
                }
            }
            if(!flag)
            {
                return false;
            }
            */
            //Debug.Log("使用时点");
            List<Skill> list = new List<Skill>();
            List<GDESkillData> mySkills =new List<GDESkillData>();
                mySkills.AddRange(PlayData.GetMySkills(CharInfo.KeyData, false));
            mySkills.AddRange(PlayData.GetMySkills(CharInfo.KeyData, true));
            foreach (Skill skill in CharInfo.GetBattleChar.Skills)
            {
                if (!skill.Enforce_CantUse && /*!skill.Enforce &&  !skill.Enforce_Weak && */mySkills.Find(a=>a.KeyID== skill.MySkill.KeyID) !=null)//skill.MySkill.Category.Key != GDEItemKeys.SkillCategory_DefultSkill && skill.MySkill.Category.Key != GDEItemKeys.SkillCategory_PublicSkill)
                {
                    list.Add(skill);
                }
            }
            if (list.Count >= 1)
            {
                this.pinfo = CharInfo;

                GDESkillKeywordData k1 = new GDESkillKeywordData("KeyWord_SF_0");
                SelectSkillList.NewSelectSkillList(true, k1.Desc, list, new SkillButton.SkillClickDel(this.ButtonDel), true, true, false, true);
                //return true;
                this.useditem = FieldSystem.instance.UseingItemObject;
                //Debug.Log(this.useditem.GetName);
                return false;
            }
            else
            {
                return false;
            }
        }

        public void ButtonDel(SkillButton Mybutton)
        {
            bool rare = Mybutton.Myskill.MySkill.Rare;

            CharInfoSkillData item = Mybutton.Myskill.Master.Info.SkillDatas.Find((CharInfoSkillData a) => a == Mybutton.Myskill.CharinfoSkilldata);
            //----Mybutton.Myskill.Master.Info.SkillDatas.Remove(item);
            this.oldSkillData = item;//----
            this.oldSkill = Mybutton.Myskill;
            //Mybutton.Myskill.Master.Info.SkillAdd(new GDESkillData(GDEItemKeys.Skill_S_SacrificeSkill), null);

            List<Skill> list = new List<Skill>();//生成可选技能
            List<GDESkillData> characterSkillNoOverLap = PlayData.GetCharacterSkillNoOverLap(this.pinfo, rare, null);
            if(characterSkillNoOverLap.FindAll(a=>a.KeyID == Mybutton.Myskill.MySkill.KeyID).Count<=0)//生成自身同名技能
            {
                characterSkillNoOverLap.Add(new GDESkillData(Mybutton.Myskill.MySkill.KeyID));
            }

            List<Skill_Extended> sexlist = this.oldSkill.AllExtendeds.FindAll(a => a.Data != null && a.Data.Drop);//记录强弱化
            List<Skill_Extended> sexlist2 = new List<Skill_Extended>();
            foreach (Skill_Extended sex in sexlist)
            {
                sexlist2.Add(Skill_Extended.DataToExtended(sex.Data));
            }

            for (int j = 0; j < characterSkillNoOverLap.Count; j++)
            {
                bool flag1 = true;
                Skill skill_1 = Skill.TempSkill(characterSkillNoOverLap[j].KeyID, (this.pinfo.GetBattleChar) as BattleAlly, PlayData.TempBattleTeam);
                foreach (Skill_Extended sex in sexlist2)
                {
                    if(sex.CanSkillEnforce(skill_1) && sex.CanSkillEnforceChar(skill_1))
                    {
                        skill_1.ExtendedAdd(sex);
                    }
                    else
                    {
                        flag1 = false;
                    }
                }
                if(flag1)
                {
                    list.Add(skill_1);

                }
            }

            foreach (Skill skill in list)
            {
                if (!SaveManager.IsUnlock(skill.MySkill.KeyID, SaveManager.NowData.unlockList.SkillPreView))
                {
                    SaveManager.NowData.unlockList.SkillPreView.Add(skill.MySkill.KeyID);
                }
            }

            SelectSkillList.NewSelectSkillList(true, ScriptLocalization.System_Item.SkillAdd, list, new SkillButton.SkillClickDel(this.SkillAdd), true, true, false, true);

            //FieldSystem.DelayInput(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(this.SkillAdd), ScriptLocalization.System_Item.SkillAdd, true, true, true, true, false));
        }
        public void SkillAdd(SkillButton Mybutton)
        {
            /*
            //----------强化
            if (this.oldSkill.Enforce )
            {
                //Debug.Log("Enforce_1");
                List<Skill_Extended> list = new List<Skill_Extended>();
                System.Random random = new System.Random(RandomManager.RandomInt(RandomClassKey.GetEnforce(false), 0, int.MaxValue));
                //list.AddRange(PlayData.GetBattleEnforce(Mybutton.Myskill).Random(Mybutton.Myskill.Master.GetRandomClass().Main, 2));
                list.AddRange(PlayData.GetEnforce(false, Mybutton.Myskill).Random(random, 2));
                if(list.Count <= 0)
                {
                    goto Point_001;
                }
                //Debug.Log("Enforce_2");
                //Debug.Log(list.Count);

                //this.EnforceList.AddRange(list);
                List<Skill> list2 = new List<Skill>();
                for (int i = 0; i < list.Count; i++)
                {
                    Skill skill = Mybutton.Myskill.CloneSkill(true, null, null, false);
                    skill.ExtendedAdd(list[i]);
                    list2.Add(skill);
                }
                //Debug.Log("Enforce_3");
                //Debug.Log(list2.Count);

                FieldSystem.DelayInput(BattleSystem.I_OtherSkillSelect(list2, new SkillButton.SkillClickDel(this.EnforceSkillAdd), ScriptLocalization.UI_Enforce.SelectEnforce, false, true, true, true, false));
                return;
            }
            else if(this.oldSkill.Enforce_Weak)
            {
                //Debug.Log("Weak_1");
                List<Skill_Extended> list = new List<Skill_Extended>();

                System.Random random = new System.Random(RandomManager.RandomInt(RandomClassKey.GetEnforce(true), 0, int.MaxValue));
                list.AddRange(PlayData.GetEnforce(true, Mybutton.Myskill).Random(random, 2));
                if (list.Count <= 0)
                {
                    goto Point_001;
                }

                //this.EnforceList.AddRange(list);
                List<Skill> list2 = new List<Skill>();
                for (int i = 0; i < list.Count; i++)
                {
                    Skill skill = Mybutton.Myskill.CloneSkill(true, null, null, false);
                    skill.ExtendedAdd(list[i]);
                    list2.Add(skill);
                }

                FieldSystem.DelayInput(BattleSystem.I_OtherSkillSelect(list2, new SkillButton.SkillClickDel(this.EnforceSkillAdd), ScriptLocalization.UI_Enforce.SelectEnforce, false, true, true, true, false));
                return;
            }
            Point_001:
            */
            //----------无强化无弱化
            Mybutton.Myskill.Master.Info.SkillDatas.Remove(this.oldSkillData);//----
            Mybutton.Myskill.Master.Info.UseSoulStone(Mybutton.Myskill);
            UIManager.inst.CharstatUI.GetComponent<CharStatV4>().SkillUPdate();

            //Debug.Log(this.useditem.GetName);

            //FieldSystem.instance.UseingItemObject.MyManager.UseItem(this.useditem);
            
            for (int i = 0; i < this.useditem.MyManager.InventoryItems.Count; i++)
            {
                if (this.useditem.MyManager.InventoryItems[i] == this.useditem)
                {
                    this.useditem.MyManager.UseItem(this.useditem.MyManager, i);
                }
            }
            
        }
        /*
        public void EnforceSkillAdd(SkillButton Mybutton)
        {


            Mybutton.Myskill.Master.Info.SkillDatas.Remove(this.oldSkillData);//----
            Mybutton.Myskill.Master.Info.UseSoulStone(Mybutton.Myskill);
            UIManager.inst.CharstatUI.GetComponent<CharStatV4>().SkillUPdate();

            //Debug.Log(this.useditem.GetName);
            //FieldSystem.instance.UseingItemObject.MyManager.UseItem(this.useditem);
            for (int i = 0; i < this.useditem.MyManager.InventoryItems.Count; i++)
            {
                if (this.useditem.MyManager.InventoryItems[i] == this.useditem)
                {
                    this.useditem.MyManager.UseItem(this.useditem.MyManager, i);
                }
            }
        }
        */

        private Character pinfo = new Character();
        private ItemBase useditem = new ItemBase();
        private CharInfoSkillData oldSkillData=new CharInfoSkillData();
        private Skill oldSkill=new Skill();

        //private List<Skill_Extended> EnforceList = new List<Skill_Extended>();

    }









}
