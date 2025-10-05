using ChronoArkMod.Plugin;

using GameDataEditor;
using I2.Loc;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using UnityEngine;

using UnityEngine.EventSystems;

using HarmonyLib;
using ChronoArkMod;
using DarkTonic.MasterAudio;

using ChronoArkMod.ModData;
using ChronoArkMod.Template;
using ChronoArkMod.ModData.Settings;
using UnityEngine.UI;
using static DemoToonVFX;
using DG.Tweening.Core.Easing;
using static CharacterDocument;
using System.Text;
using System.Text.RegularExpressions;
using Steamworks;
using UnityEngine.Events;
using UnityEngine.Assertions.Must;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Linq;
using SF_Standard;
using System.Runtime.InteropServices;

namespace SF_Hunter
{
    [PluginConfig("M200_SF_Hunter_CharMod", "SF_Hunter_CharMod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class SF_Hunter_CharacterModPlugin : ChronoArkPlugin
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
        public class HunterDef : ModDefinition
        {
        }
    }

    [HarmonyPatch(typeof(PrintText))]
    [HarmonyPatch("TextInput")]
    public static class PrintTestPlugin
    {
        // Token: 0x06000014 RID: 20 RVA: 0x00002310 File Offset: 0x00000510
        [HarmonyPrefix]
        public static void TextInput_Before_patch(PrintText __instance, ref string inText)
        {
            //inText = "test";
            //MasterAudio.PlaySound("A_hut_4", 1f, null, 0f, null, null, false, false);
            float volume = 0f;
            float volume2 = 0f;
            if (ModManager.getModInfo("SF_Hunter").GetSetting<ToggleSetting>("AudioOn").Value)
            {
                volume = ModManager.getModInfo("SF_Hunter").GetSetting<SliderSetting>("AudioVolume").Value;
            }
            else
            {
                volume = 0f;
            }

            if (inText.Contains("T_hut_1_1"))
            {
                inText = inText.Replace("T_hut_1_1:", string.Empty);
                MasterAudio.PlaySound("A_hut_1_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_hut_1_2"))
            {
                inText = inText.Replace("T_hut_1_2:", string.Empty);
                MasterAudio.PlaySound("A_hut_1_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_hut_2_1"))
            {
                inText = inText.Replace("T_hut_2_1:", string.Empty);
                MasterAudio.PlaySound("A_hut_2_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_hut_2_2"))
            {
                inText = inText.Replace("T_hut_2_2:", string.Empty);
                MasterAudio.PlaySound("A_hut_2_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_hut_3_1"))
            {
                inText = inText.Replace("T_hut_3_1:", string.Empty);
                MasterAudio.PlaySound("A_hut_3_1", volume, null, 1f, null, null, false, false);
            }
            else if (inText.Contains("T_hut_4_1"))
            {
                inText = inText.Replace("T_hut_4_1:", string.Empty);
                MasterAudio.PlaySound("A_hut_4_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_hut_4_2"))
            {
                inText = inText.Replace("T_hut_4_2:", string.Empty);
                MasterAudio.PlaySound("A_hut_4_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_hut_5_1"))
            {
                inText = inText.Replace("T_hut_5_1:", string.Empty);
                MasterAudio.PlaySound("A_hut_5_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_hut_6_1"))
            {
                inText = inText.Replace("T_hut_6_1:", string.Empty);
                MasterAudio.PlaySound("A_hut_6_1", volume, null, 1f, null, null, false, false);
            }
            else if (inText.Contains("T_hut_6_2"))
            {
                inText = inText.Replace("T_hut_6_2:", string.Empty);
                MasterAudio.PlaySound("A_hut_6_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_hut_7_1"))
            {
                inText = inText.Replace("T_hut_7_1:", string.Empty);
                MasterAudio.PlaySound("A_hut_7_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_hut_8_1"))
            {
                inText = inText.Replace("T_hut_8_1:", string.Empty);
                MasterAudio.PlaySound("A_hut_8_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_hut_9_1"))
            {
                inText = inText.Replace("T_hut_9_1:", string.Empty);
                MasterAudio.PlaySound("A_hut_9_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_hut_10_1"))
            {
                inText = inText.Replace("T_hut_10_1:", string.Empty);
                MasterAudio.PlaySound("A_hut_10_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_hut_11_1"))
            {
                inText = inText.Replace("T_hut_11_1:", string.Empty);
                MasterAudio.PlaySound("A_hut_11_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_hut_11_2"))
            {
                inText = inText.Replace("T_hut_11_2:", string.Empty);
                MasterAudio.PlaySound("A_hut_11_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_hut_11_3"))
            {
                inText = inText.Replace("T_hut_11_3:", string.Empty);
                MasterAudio.PlaySound("A_hut_11_3", volume, null, 0f, null, null, false, false);
            }
        }
    }



    [HarmonyPatch(typeof(FriendShipUI))]
    [HarmonyPatch("Gift")]
    public static class GiftPlugin
    {
        [HarmonyPostfix]
        public static void Gift_AfterPatch(FriendShipUI __instance, string CharId)
        {

            if (CharId == "SF_Hunter")
            {
                Statistics_Character sc = SaveManager.NowData.statistics.GetCharData(CharId);
                int NowLV = sc.FriendshipLV;
                if (NowLV >= 3)
                {
                    if (NowLV == 3)
                    {
                        sc.IsCrimsonDialogue = true;
                        UIManager.InstantiateActiveAddressable(UIManager.inst.CompleteUI, AddressableLoadManager.ManageType.EachStage).GetComponent<FriendShipCompleteUI>().MaxLevel(CharId);
                    }
                }
            }
        }
    }
    [HarmonyPatch(typeof(CharEquipInven))]
    [HarmonyPatch("OnDropSlot")]
    public static class CharEquipInvenPlugin
    {
        // Token: 0x06000015 RID: 21 RVA: 0x00002410 File Offset: 0x00000610
        [HarmonyPostfix]
        public static void CharEquipInven_OnDropSlot_patch(ItemBase inputitem, ref bool __result, CharEquipInven __instance)
        {
            bool flag = inputitem.itemkey == "E_SF_Hunter_0" && __instance.Info.KeyData != "SF_Hunter";
            if (flag)
            {
                __result = true;
            }
            //Debug.Log("成功进入OnDropSlot的patch");
        }
    }


    /*

    [HarmonyPatch(typeof(BattleSystem))]
    [HarmonyPatch("SkillTargetAdd")]
    [HarmonyPatch(new Type[] { typeof(BattleChar) })]
    public static class SkillTargetAdd_1_Plugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void UseSkill_Before_patch(BattleSystem __instance, BattleChar Target)
        {
            if (!BattleSystem.IsSelect(Target, __instance.SelectedSkill))
            {
                return;
            }
            if (__instance.ActionAlly != null)
            {
                foreach (IP_SkillUsePatch_Before ip_patch in __instance.SelectedSkill.IReturn<IP_SkillUsePatch_Before>())
                {
                    //Debug.Log("only damage drm");
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.SkillUsePatch_Before(Target, null);
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
                foreach (IP_SkillUsePatch_Before ip_patch in __instance.SelectedSkill.IReturn<IP_SkillUsePatch_Before>())
                {
                    //Debug.Log("only damage drm");
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.SkillUsePatch_Before(null, Target);
                    }
                }
            }
        }
    }
    public interface IP_SkillUsePatch_Before
    {
        // Token: 0x06000001 RID: 1
        void SkillUsePatch_Before(BattleChar TargetChar, Skill TargetSkill);
    }
    */
    [HarmonyPatch(typeof(BattleAlly))]
    [HarmonyPatch("UseSkillAfter")]
    public static class UseSkillAfter_Plugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void UseSkillAfter_Before_patch(BattleAlly __instance, Skill skill)
        {
            if (!skill.FreeUse)
            {
                foreach (IP_UseSkillAfter_Before ip_patch in skill.IReturn<IP_UseSkillAfter_Before>())
                {
                    //Debug.Log("only damage drm");
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.UseSkillAfter_Before(skill);
                    }
                }
            }
        }

        [HarmonyPostfix]
        public static void UseSkillAfter_After_patch(BattleAlly __instance, Skill skill)
        {
            if (!skill.FreeUse)
            {
                foreach (IP_UseSkillAfter_After ip_patch in skill.IReturn<IP_UseSkillAfter_After>())
                {
                    //Debug.Log("only damage drm");
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.UseSkillAfter_After(skill);
                    }
                }
            }
        }
    }
    public interface IP_UseSkillAfter_Before
    {
        // Token: 0x06000001 RID: 1
        void UseSkillAfter_Before(Skill skill);
    }
    public interface IP_UseSkillAfter_After
    {
        // Token: 0x06000001 RID: 1
        void UseSkillAfter_After(Skill skill);
    }




    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("DodgeCheck")]
    public static class DodgeCheckPlugin
    {
        [HarmonyPriority(0)]
        [HarmonyPostfix]
        public static void DodgeCheck(ref bool __result, BattleChar __instance, SkillParticle SP)
        {
            bool dod = __result;
            //Debug.Log("闪避检测检查点1");

            foreach (IP_DodgeCheck_After2 ip_patch2 in BattleSystem.instance.IReturn<IP_DodgeCheck_After2>())
            {
                bool flag2 = ip_patch2 != null;
                if (flag2)
                {
                    bool candod = dod;
                    ip_patch2.DodgeCheck_After2(__instance,SP, ref candod);
                    dod = candod || dod;
                }
            }

            __result = dod;
        }
    }
    public interface IP_DodgeCheck_After2
    {
        // Token: 0x06000001 RID: 1
        void DodgeCheck_After2(BattleChar hit,SkillParticle SP, ref bool dod);
    }
















    public class P_Hunter : Passive_Char, IP_SkillUse_Target, IP_DamageChange,IP_PlayerTurn,IP_SpecialEnemyTargetSelect, IP_LevelUp//, IP_LogUpdate
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
                list.Add(ItemBase.GetItem("E_SF_Hunter_0", 1));
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
        
        //追击部分
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if(SkillD.Master==this.BChar && !this.locking)
            {
                if(SkillD.MySkill.KeyID!= "S_SF_Hunter_p" && SkillD.AllExtendeds.Find(a=>a is Sex_hut_p)==null && Damage>0 && SkillD.AllExtendeds.Find(a => a is Sex_hut_PassiveDisable) == null)
                {
                    //Debug.Log("猎手被动_原伤害:"+SkillD.MySkill.Name+ ":" + Damage);
                    int dmg= Math.Max(1, (int)(0.5 * Damage));
                    //Debug.Log("猎手被动_新伤害:" + SkillD.MySkill.Name + ":" + dmg);
                    return dmg;
                }
            }
            return Damage;
        }
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (!this.locking && DMG > 0 && SP.SkillData.AllExtendeds.Find(a => a is Sex_hut_p) == null && SP.SkillData.AllExtendeds.Find(a => a is Sex_hut_PassiveDisable) == null)
            {
                if (this.recs.Find(a => a.sp == SP) != null)
                {
                    Phut_rec rec = this.recs.Find(a => a.sp == SP);
                    Phut_tar tarrec = new Phut_tar();
                    tarrec.target = hit;
                    tarrec.dmg = DMG;
                    tarrec.cri= Cri;
                    rec.targetRecs.Add(tarrec);
                    //rec.targets.Add(hit);
                    //rec.dmgs.Add(Math.Max(1, (int)(1 * DMG)));
                }
                else
                {
                    Phut_rec rec = new Phut_rec();
                    rec.sp = SP;
                    rec.particleName = SP.ParticleName;
                    rec.criper = SP.SkillData.MySkill.Effect_Target.CRI;

                    Phut_tar tarrec = new Phut_tar();
                    tarrec.target = hit;
                    tarrec.dmg = DMG;
                    tarrec.cri = Cri;
                    rec.targetRecs.Add(tarrec);
                    //rec.targets.Add(hit);
                    //rec.dmgs.Add(Math.Max(1, (int)(1 * DMG)));
                    this.recs.Add(rec);
                }
                if (!this.recording)
                {
                    this.recording = true;
                    BattleSystem.DelayInputAfter(this.ShadowAttack());
                }
            }
        }
        public IEnumerator ShadowAttack()
        {
            yield return new WaitForSecondsRealtime(0.1f);

            this.locking = true;

            for (int i = 0; i < this.recs.Count; i++)
            {
                Phut_rec rec = this.recs[i];

                while (BattleSystem.instance.Particles.Count != 0)
                {
                    yield return new WaitForSecondsRealtime(0.1f);
                }
                yield return new WaitForSecondsRealtime(0.1f);

                Skill skill = Skill.TempSkill("S_SF_Hunter_p", this.BChar, this.BChar.MyTeam);
                skill.UseOtherParticle_Path = rec.particleName;
                skill.FreeUse = true;
                skill.PlusHit = true;

                List<BattleChar> list = new List<BattleChar>();
                /*
                Phut_rec rec2 = new Phut_rec();
                rec2.sp=rec.sp;
                rec2.criper=rec.criper;
                rec2.particleName = rec.particleName;
                rec2.targetRecs.AddRange(rec.targetRecs);
                */



                foreach (Phut_tar a in rec.targetRecs)
                {
                    if(!a.target.IsDead)
                    {
                        list.Add(a.target);
                    }
                    else if(BattleSystem.instance.EnemyTeam.AliveChars.Count > 0)
                    {
                        
                        a.target = BattleSystem.instance.EnemyTeam.AliveChars.Random(this.BChar.GetRandomClass().Main);
                        list.Add(a.target);
                    }
                    else
                    {
                        list.Add(a.target);
                    }
                }

                foreach (Skill_Extended sex in skill.AllExtendeds)
                {
                    if (sex is Sex_hut_p sex411)
                    {
                        sex411.rec = rec;
                        sex411.PlusSkillStat.cri = rec.criper;

                        sex411.targetList.AddRange(rec.targetRecs);
                    }
                }


                //list.AddRange(rec.targets);
                this.BChar.ParticleOut(skill, list);

            }
            

            this.recs.RemoveAll(a => true);
            this.recording = false;

            yield break;
        }

        
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (this.locking )
            {      
                if (!BattleSystem.instance.DelayWait && BattleSystem.instance.Particles.Count == 0)// && BattleSystem.instance.ActWindow.On)
                {
                    this.frame++;
                }
                if (this.frame > 1 )
                {
                    this.locking = false;
                    this.frame = 0;
                }
            }
        }
        public void LogUpdate(BattleLog log)
        { 
            if(!this.recording && !Standard_Tool.OriginSkill(log.UsedSkill).FreeUse)
            {
                this.locking = false;
                this.frame = 0;
            }
        }
        public int frame = 0;
        public bool locking = false;
        //public bool unlockAllow = false;
        public bool recording = false;
        private List<Phut_rec> recs = new List<Phut_rec>();
        
        //猎物印记部分
        public void Turn()
        {
            if(BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Find(a=>a.BuffFind("B_SF_Hunter_p",false) && a.BuffReturn("B_SF_Hunter_p", false).Usestate_L == this.BChar) ==null)
            {
                Skill skill = Skill.TempSkill("S_Priest_3", this.BChar);
                List<BattleChar> list = new List<BattleChar>();
                list.AddRange(BattleSystem.instance.EnemyTeam.AliveChars_Vanish.FindAll(a => BattleSystem.IsSelect(a, skill)));
                if(list.Count>0)
                {
                    BattleChar tar = list.Random(this.BChar.GetRandomClass().Main);
                    tar.BuffAdd("B_SF_Hunter_p", this.BChar);
                }
            }
            else if(BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Find(a => a.BuffFind("B_SF_Hunter_p", false) && a.BuffReturn("B_SF_Hunter_p", false).Usestate_L == this.BChar) != null)
            {
                bool remain = false;
                foreach(BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
                {
                    if(bc.BuffFind("B_SF_Hunter_p", false) && bc.BuffReturn("B_SF_Hunter_p", false).Usestate_L == this.BChar)
                    {
                        Bcl_hut_p buff = bc.BuffReturn("B_SF_Hunter_p", false)as Bcl_hut_p;
                        if(buff.turnDmg>0 )
                        {
                            remain = true;
                        }
                        buff.turnDmg = 0;
                    }
                }
                if(!remain)
                {
                    Skill skill = Skill.TempSkill("S_Priest_3", this.BChar);
                    List<BattleChar> list = new List<BattleChar>();
                    list.AddRange(BattleSystem.instance.EnemyTeam.AliveChars_Vanish.FindAll(a => BattleSystem.IsSelect(a, skill)));
                    list.RemoveAll(a => a.BuffFind("B_SF_Hunter_p", false) && a.BuffReturn("B_SF_Hunter_p", false).Usestate_L == this.BChar);
                    if (list.Count > 0)
                    {
                        BattleChar tar = list.Random(this.BChar.GetRandomClass().Main);
                        tar.BuffAdd("B_SF_Hunter_p", this.BChar);
                    }
                }
            }

        }

        public bool SpecialEnemyTargetSelect(Skill skill, BattleEnemy Target)
        {
            if(skill.Master==this.BChar && Target.BuffFind("B_SF_Hunter_p",false)&&Target.BuffReturn("B_SF_Hunter_p", false).Usestate_L == this.BChar)
            {
                return true;
            }
            return false;
        }
        
    }
    public class Phut_rec//追击部分
    {
        public SkillParticle sp = new SkillParticle();
        public int criper = 0;
        public string particleName = string.Empty;
        public List<Phut_tar> targetRecs = new List<Phut_tar>();
        //public List<BattleChar> targets = new List<BattleChar>();
        //public List<bool> cris=new List<bool>();
        //public List<int> dmgs = new List<int>();
    }
    public class Phut_tar//追击部分
    {
        public BattleChar target=new BattleChar();
        public bool cri = false;
        public int dmg = 0;
    }
    public class Sex_hut_p: Sex_ImproveClone, IP_DamageChange_sumoperation//追击部分
    {
        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (SkillD == this.MySkill)
            {
                //Debug.Log("point1");
                //if (this.rec.targets.Contains(Target))
                if (this.targetList.Find(a=>a.target==Target)!=null)
                {
                    //Debug.Log("point2");
                    //int index = this.rec.targets.FindIndex(a => a == Target);
                    //PlusDamage += (this.rec.dmgs[index] - 1);
                    Phut_tar recTarget = this.targetList.Find(a => a.target == Target);
                    PlusDamage += (recTarget.dmg - 1);
                    this.targetList.Remove(recTarget);

                }
            }
        }
        public Phut_rec rec = new Phut_rec();
        public List<Phut_tar> targetList = new List<Phut_tar>();
    }
    public class Sex_hut_PassiveDisable : Sex_ImproveClone
    {

    }
    public class Bcl_hut_p : SF_Standard.InBuff,IP_Dead,IP_DamageTake,IP_BuffAddAfter//猎物印记部分
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&user", this.Usestate_L.Info.Name).Replace("&num1", this.ThresholdDmg.ToString()) .Replace("&num2", (this.ThresholdDmg-this.sumDmg).ToString()).Replace("&num3", (10*(this.doubleReward?2:1)).ToString());
        }
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (addedbuff==this)
            {
                foreach (BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
                {
                    if (bc!=this.BChar && bc.BuffFind("B_SF_Hunter_p", false) && bc.BuffReturn("B_SF_Hunter_p", false).Usestate_L == this.Usestate_L)
                    {
                        (bc.BuffReturn("B_SF_Hunter_p", false) as InBuff).InBuffDestroy();
                        //(bc.BuffReturn("B_SF_Hunter_p", false) ).SelfDestroy();
                    }
                }
                foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars_Vanish)
                {
                    if (bc != this.BChar && bc.BuffFind("B_SF_Hunter_p", false) && bc.BuffReturn("B_SF_Hunter_p", false).Usestate_L == this.Usestate_L)
                    {
                        (bc.BuffReturn("B_SF_Hunter_p", false) as InBuff).InBuffDestroy();
                        //(bc.BuffReturn("B_SF_Hunter_p", false)).SelfDestroy();
                    }
                }
            }
        }
        public override void Init()
        {
            base.Init();


            if(this.Usestate_L!=this.userL)
            {
                this.sumDmg = 0;
                this.turnDmg = 0;
                this.doubleReward = false;
                this.userL = this.Usestate_L;
            }
        }
        public void Dead()
        {
            Buff buff = this.Usestate_L.BuffAdd("B_SF_Hunter_p_1", this.Usestate_L);
            (buff as Bcl_hut_p_1).killnum++;
            if(this.doubleReward)
            {
                (buff as Bcl_hut_p_1).killnum++;
                this.doubleReward = false;
            }
            buff.BuffStat();
        }
        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if(User==this.Usestate_L && Dmg>0 && Target==this.BChar)
            {
                this.sumDmg += Dmg;
                this.turnDmg += Dmg;
                while(this.sumDmg>=this.ThresholdDmg)
                {
                    this.sumDmg-= this.ThresholdDmg;
                    Buff buff = this.Usestate_L.BuffAdd("B_SF_Hunter_p_1", this.Usestate_L);
                    (buff as Bcl_hut_p_1).killnum++;
                    if (this.doubleReward)
                    {
                        (buff as Bcl_hut_p_1).killnum++;
                        this.doubleReward = false;
                    }
                    buff.BuffStat();
                }
            }
        }
        public int ThresholdDmg
        {
            get
            {
                Stat stat = this.Usestate_L.Info.OriginStat;

                if (this.Usestate_L.Info.Ally)
                {
                    stat += this.Usestate_L.Info.AllyLevelPlusStat(0);
                }
                int num = (int)(10 * stat.atk);
                return num;
            }
        }
        public int sumDmg = 0;
        public int turnDmg = 0;
        public bool doubleReward = false;
        public BattleChar userL=new BattleChar();
    }
    public class Bcl_hut_p_1 : SF_Standard.InBuff//猎物印记部分
    {
        public override void BuffStat()
        {
            base.BuffStat();
            this.PlusPerStat.Damage = 10 * this.killnum;
            this.PlusStat.hit = 10 * this.killnum;
        }
        public int killnum = 0;
    }
    
    public class Sex_hut_0 : Sex_ImproveClone
    {

    }
    public class Bcl_hut_0 : SF_Standard.InBuff, IP_Dead, IP_DamageTake//, IP_BuffAddAfter//非被动猎物印记部分
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&user", this.Usestate_L.Info.Name).Replace("&num", (this.ThresholdDmg - this.sumDmg).ToString());
        }
        public override void Init()
        {
            base.Init();


            if (this.Usestate_L != this.userL)
            {
                this.sumDmg = 0;
                this.userL = this.Usestate_L;
            }
        }
        public void Dead()
        {
            this.DrawBack("S_SF_Hunter_0");
            Buff buff = this.Usestate_L.BuffAdd("B_SF_Hunter_0_1", this.Usestate_L);
            (buff as Bcl_hut_0_1).killnum++;
            buff.BuffStat();
            
        }
        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if (User == this.Usestate_L && Dmg > 0 && Target == this.BChar)
            {
                this.sumDmg += Dmg;
                while (this.sumDmg >= this.ThresholdDmg)
                {
                    this.DrawBack("S_SF_Hunter_0");
                    Buff buff = this.Usestate_L.BuffAdd("B_SF_Hunter_0_1", this.Usestate_L);
                    (buff as Bcl_hut_0_1).killnum++;
                    buff.BuffStat();
                    this.SelfDestroy();
                }
            }
        }
        public int ThresholdDmg
        {
            get
            {
                Stat stat = this.Usestate_L.Info.OriginStat;

                if (this.Usestate_L.Info.Ally)
                {
                    stat += this.Usestate_L.Info.AllyLevelPlusStat(0);
                }
                int num = (int)(10 * stat.atk);
                return num;
            }
        }
        public void DrawBack(string str)
        {
            foreach (Skill skill in BattleSystem.instance.AllyTeam.Skills_UsedDeck)
            {
                if (skill.MySkill.KeyID == str)
                {
                    BattleSystem.instance.AllyTeam.ForceDrawF(skill);
                    return;
                }
            }
            foreach (Skill skill2 in BattleSystem.instance.AllyTeam.Skills_Deck)
            {
                if (skill2.MySkill.KeyID == str)
                {
                    BattleSystem.instance.AllyTeam.ForceDrawF(skill2);
                    break;
                }
            }
        }
        public int sumDmg = 0;
        public BattleChar userL = new BattleChar();
    }
    public class Bcl_hut_0_1 : SF_Standard.InBuff//非被动猎物印记部分
    {
        public override void BuffStat()
        {
            base.BuffStat();
            this.PlusPerStat.Damage = 15 * this.killnum;
            this.PlusStat.hit = 15* this.killnum;
        }
        public int killnum = 0;
    }

    public class Sex_hut_1:Sex_ImproveClone, IP_DodgeCheck_After2, IP_ParticleOut_After_Global, IP_UseSkillAfter_Before,IP_UseSkillAfter_After,IP_SkillCastingStart
    {
        public override void Init()
        {
            base.Init();
            //Debug.Log("回转探戈Init");
            this.CountingExtedned = true;
            this.OnePassive = true;
        }
        public void UseSkillAfter_Before(Skill skill)
        {
            //this.data = this.MySkill.CharinfoSkilldata;
            if(!this.MySkill.IsNowCasting)
            {
                this.recact=this.BChar.MyTeam.TurnActionNum;
            }
        }
        public void UseSkillAfter_After(Skill skill)
        {
            //this.data = this.MySkill.CharinfoSkilldata;
            if (!this.MySkill.IsNowCasting)
            {
                if(this.recact>=0 && this.BChar.MyTeam.TurnActionNum<=this.recact)
                {

                    BattleSystem.instance.AllyTeam.TurnActionNum = this.recact + 1;
                    BattleSystem.instance.StartCoroutine(BattleSystem.instance.EnemyTurn(false));
                }

                this.recact = -1;
            }
        }
        public void SkillCasting(CastingSkill ThisSkill)
        {
            this.castskill = ThisSkill;
            /*
            if (ThisSkill.skill.MyButton != null)
            {
                Debug.Log("CountingLeft:"+ThisSkill.skill.MyButton.CountingLeft);
            }
            Debug.Log("TurnActionNum:" + BattleSystem.instance.AllyTeam.TurnActionNum);
            Debug.Log("CastSpeed:"+ThisSkill.CastSpeed);
            Debug.Log("CastSpeed_b:" + ThisSkill.CastButton.castskill.CastSpeed);
            Debug.Log("_CastSpeed:"+ThisSkill._CastSpeed);
            */
        }
        public CastingSkill castskill = null;
        public int recact = -1;
        public void DodgeCheck_After2(BattleChar hit, SkillParticle SP, ref bool dod)
        {
            try
            {
                if (this.MySkill.IsNowCounting)
                {
                    //Debug.Log("猎手test1_button:" + (this.MySkill.MyButton != null));
                    //Debug.Log("猎手test1_cast:" + (this.MySkill.MyButton.castskill != null));
                    if (this.castskill.TargetReturn().Contains(SP.UseStatus) || this.MySkill.MyButton.castskill.TargetReturn().Contains(SP.UseStatus) )
                    {

                        if (hit == this.MySkill.Master)
                        {
                            dod = true;
                        }

                    }
                }

            }
            catch
            {
                //Debug.Log("猎手_报错1" );
            }
        }
        public IEnumerator ParticleOut_After_Global(Skill SkillD, List<BattleChar> Targets)
        {
            if (!this.triggered&& this.MySkill.IsNowCounting && SkillD.IsDamage && (this.castskill.TargetReturn().Contains(SkillD.Master) || this.MySkill.MyButton.castskill.TargetReturn().Contains(SkillD.Master)))
            {
                //Debug.Log("猎手test1_button:" + (this.MySkill.MyButton != null));
                //Debug.Log("猎手test1_cast:" + (this.MySkill.MyButton.castskill != null));
                foreach (Skill skill in BattleSystem.instance.AllyTeam.Skills_UsedDeck)
                {
                    if (skill.CharinfoSkilldata == this.MySkill.CharinfoSkilldata)
                    {
                        this.triggered = true;
                        BattleSystem.instance.AllyTeam.ForceDrawF(skill);
                        yield break; 
                    }
                }
                foreach (Skill skill2 in BattleSystem.instance.AllyTeam.Skills_Deck)
                {
                    if (skill2.CharinfoSkilldata == this.MySkill.CharinfoSkilldata)
                    {
                        this.triggered = true;
                        BattleSystem.instance.AllyTeam.ForceDrawF(skill2);
                        yield break;
                    }
                }
            }
            yield break;
        }
        public bool triggered = false;
        /*
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            if(this.getback)
            {
                //Debug.Log("point_single");
                foreach (Skill skill in BattleSystem.instance.AllyTeam.Skills_UsedDeck)
                {
                    if (skill.CharinfoSkilldata == this.MySkill.CharinfoSkilldata)
                    {
                        BattleSystem.instance.AllyTeam.ForceDrawF(skill);
                        return;
                    }
                }
                foreach (Skill skill2 in BattleSystem.instance.AllyTeam.Skills_Deck)
                {
                    if (skill2.CharinfoSkilldata == this.MySkill.CharinfoSkilldata)
                    {
                        BattleSystem.instance.AllyTeam.ForceDrawF(skill2);
                        break;
                    }
                }
            }

        }
        public bool getback = false;
        */
        //public CharInfoSkillData data=new CharInfoSkillData();
    }

    public class Sex_hut_2 : Sex_ImproveClone
    {
        /*
        public override void Init()
        {
            base.Init();
            BuffTag btg=new BuffTag();
            btg.BuffData = new GDEBuffData("B_SF_Hunter_p");
            btg.User = this.BChar;
            this.TargetBuff.Clear();
            this.TargetBuff.Add(btg);
        }
        */
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            //this.TargetBuff.Clear();
            Buff buff = Targets[0].BuffAdd("B_SF_Hunter_p", this.BChar);
            (buff as Bcl_hut_p).doubleReward = true;
        }
    }
    public class Sex_hut_3: Sex_ImproveClone
    {
        /*
        public override bool Terms()
        {
            List<CastingSkill> allskills = new List<CastingSkill>();
            allskills.AddRange(BattleSystem.instance.CastSkills.FindAll(a => !allskills.Contains(a)));
            allskills.AddRange(BattleSystem.instance.EnemyCastSkills.FindAll(a => !allskills.Contains(a)));
            allskills.AddRange(BattleSystem.instance.SaveSkill.FindAll(a => !allskills.Contains(a)));
            List<CastingSkill> enemyskills = new List<CastingSkill>();
            enemyskills.AddRange(allskills.FindAll(a => !a.Usestate.Info.Ally));
            return base.Terms()&& enemyskills.Count>0;
        }
        */
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            //BattleSystem.instance.AllyTeam.Draw(1);

            BattleSystem.instance.StartCoroutine(this.Delay1(Targets));
        }

        public IEnumerator Delay1(List<BattleChar> bcs)
        {
            foreach(BattleChar bc in bcs)
            {
                yield return BattleSystem.instance.AllyTeam._CharacterDraw(bc, null);

            }


            bool flag1 = true;
            int sumcast = 0;
            while (flag1)
            {
                List<CastingSkill> allskills = new List<CastingSkill>();
                allskills.AddRange(BattleSystem.instance.CastSkills.FindAll(a=>!allskills.Contains(a)));
                allskills.AddRange(BattleSystem.instance.EnemyCastSkills.FindAll(a => !allskills.Contains(a)));
                allskills.AddRange(BattleSystem.instance.SaveSkill.FindAll(a => !allskills.Contains(a)));
                List<CastingSkill> castskills = new List<CastingSkill>();
                List<CastingSkill> enemyskills = new List<CastingSkill>();
                castskills.AddRange(allskills.FindAll(a=>a.Usestate.Info.Ally));
                enemyskills.AddRange(allskills.FindAll(a => !a.Usestate.Info.Ally));

                if (enemyskills.Count <= 0)
                {
                    //yield break;
                    break;
                }
                int mincast = 2147483647;
                int minenemy = 2147483647;
                foreach (CastingSkill cast in castskills)
                {
                    mincast = Math.Min(mincast, cast.CastSpeed);
                }
                foreach (CastingSkill cast in enemyskills)
                {
                    minenemy = Math.Min(minenemy, cast.CastSpeed);
                }
                if (minenemy - 1 <= mincast)
                {
                    flag1 = false;
                    BattleSystem.instance.AllyTeam.TurnActionNum += (minenemy - 1);
                    sumcast+= (minenemy - 1);
                }
                else
                {
                    BattleSystem.instance.AllyTeam.TurnActionNum += mincast;
                    sumcast += mincast;
                }
                yield return BattleSystem.instance.EnemyTurn(false);
                yield return new WaitForSeconds(0.1f);
            }

            if(sumcast>=1)
            {
                //yield return BattleSystem.instance.AllyTeam._Draw(1, null);

                for (int i = 0; i < BattleSystem.instance.AllyTeam.Skills_UsedDeck.Count; i++)
                {
                    if (BattleSystem.instance.AllyTeam.Skills_UsedDeck[i].CharinfoSkilldata==this.MySkill.CharinfoSkilldata)
                    {
                        //Debug.Log("Recharge2");
                        int index = UnityEngine.Random.Range(0, BattleSystem.instance.AllyTeam.Skills_Deck.Count + 1);
                        BattleSystem.instance.AllyTeam.Skills_Deck.Insert(index, BattleSystem.instance.AllyTeam.Skills_UsedDeck[i]);

                        BattleSystem.instance.AllyTeam.Skills_UsedDeck.RemoveAt(i);
                        i--;
                        //Debug.Log("Recharge3");
                    }


                }
            }
            yield break;
        }
    }
    public class Sex_hut_4 : Sex_ImproveClone
    {
        public override void Init()
        {
            base.Init();
            BuffTag btg=new BuffTag();
            btg.BuffData = new GDEBuffData("B_SF_Hunter_4_2");
            btg.User = this.BChar;
            this.SelfBuff.Clear();
            this.SelfBuff.Add(btg);
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);

                this.BChar.BuffAdd("B_SF_Hunter_4_2", this.BChar, false, 0, false, -1, false);
            
            this.SelfBuff.Clear();
        }
    }
    public class Bcl_hut_4_1 :Buff,IP_SkillUse_Target,IP_BuffAdd
    {
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {
            if(addedbuff.BuffData.Key==this.BuffData.Key && BuffTaker==this.BChar)
            {
                //this.remove = false;
                this.active = false;
                this.newstacks++;
            }

        }
        public override void Init()
        {
            
            this.stacks++;
            base.Init();
        }
        public override void BuffStat()
        {
            base.BuffStat();
            if(this.View || this.active)
            {
                this.PlusStat.PlusCriDmg = 25 * this.stacks;
                this.PlusStat.cri = 15 * this.stacks;

                if (this.stacks<=0 && !this.View)
                {
                    this.SelfDestroy();
                }
            }

        }
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if(SP.UseStatus==this.BChar && Cri && this.active)
            {
                this.remove = true;
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!this.active)
            {
                if (!BattleSystem.instance.DelayWait && BattleSystem.instance.Particles.Count == 0 )
                {
                    this.active = true;
                    this.BuffStat();
                }
            }
            if(this.remove)
            {
                if (!BattleSystem.instance.DelayWait && BattleSystem.instance.Particles.Count == 0 )
                {
                    this.remove = false;
                    /*
                    for (int i = 0; i < this.StackNum - this.newstacks; i++)
                    {
                        this.SelfStackDestroy();
                    }
                    */
                    this.stacks -= (this.stacks - this.newstacks);
                    this.newstacks = 0;
                    this.BuffStat();
                }
            }
        }
        /*
        public void LogUpdate(BattleLog log)
        {
            if (!this.active && !Standard_Tool.OriginSkill(log.UsedSkill).FreeUse)
            {
                this.active = true;
                this.BuffStat();
            }
            if (this.remove && !Standard_Tool.OriginSkill(log.UsedSkill).FreeUse)
            {
                this.remove = false;
                this.stacks -= (this.stacks - this.newstacks);
                this.newstacks = 0;
                this.BuffStat();
            }
        }
        */
        public int stacks = 0;
        public bool active = false;
        public bool remove = false;
        public int newstacks = 0;
    }
    public class Bcl_hut_4_2 : Buff, IP_SkillUse_Target,IP_BuffAdd//,IP_LogUpdate
    {
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {
            if(addedbuff.BuffData.Key==this.BuffData.Key)
            {
                this.extra = true;
            }
        }
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            //Debug.Log("判定:" + (SP.UseStatus == this.BChar) + !Cri + this.active);
            if (SP.UseStatus == this.BChar && !Cri && this.active)
            {
                //Debug.Log("非暴击触发");
                BattleSystem.DelayInputAfter(this.DrawBack("S_SF_Hunter_4"));
                if(this.extra)
                {
                    this.extra = false;
                    this.active = false;
                }
                else
                {
                    this.SelfDestroy();

                }
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!this.active)
            {
                if (!BattleSystem.instance.DelayWait && BattleSystem.instance.Particles.Count == 0 )
                {
                    this.active = true;
                }
            }

        }
        /*
        public void LogUpdate(BattleLog log)
        {
            if (!this.active && !Standard_Tool.OriginSkill(log.UsedSkill).FreeUse)
            {
                this.active = true;
            }
        }
        */
        public IEnumerator DrawBack(string str)
        {
            foreach (Skill skill in BattleSystem.instance.AllyTeam.Skills_UsedDeck)
            {
                if (skill.MySkill.KeyID == str)
                {
                    BattleSystem.instance.AllyTeam.ForceDrawF(skill);
                    yield break;
                }
            }
            foreach (Skill skill2 in BattleSystem.instance.AllyTeam.Skills_Deck)
            {
                if (skill2.MySkill.KeyID == str)
                {
                    BattleSystem.instance.AllyTeam.ForceDrawF(skill2);
                    yield break;
                }
            }
            yield break;
        }

        public bool used = false;
        public bool active = false;
        public bool extra = false;
    }

    public class Sex_hut_5: Sex_ImproveClone
    {

    }
    public class Bcl_hut_5_1 : Common_Buff_Rest
    {
        /*
        public override void Init()
        {
            base.Init();
            this.PlusStat.Stun = true;
        }
        */
    }
    public class Bcl_hut_5_2: Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.dod = -30;
            this.PlusStat.crihit = 25;

        }
    }

    public class Sex_hut_6 : Sex_ImproveClone,IP_DamageChange_sumoperation
    {
        // Token: 0x06001255 RID: 4693 RVA: 0x00096DBC File Offset: 0x00094FBC
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)Misc.PerToNum(this.BChar.GetStat.atk, 30f)).ToString());
        }

        // Token: 0x06001256 RID: 4694 RVA: 0x00096E48 File Offset: 0x00095048
        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if(SkillD==this.MySkill)
            {
                bool flag1 = Target is BattleEnemy && Target.MyTeam.AliveChars.Find(a => a != Target && (Target as BattleEnemy).istaunt == (a as BattleEnemy).istaunt) == null;
                bool flag2 = Target.GetBuffs(BattleChar.GETBUFFTYPE.CC, false, false).Count != 0;

                if (flag1 || flag2)
                {
                    PlusDamage = BattleChar.CalculationResult(this.BChar.Info.get_stat.atk, 30, 0);
                }
            }

        }
    }

    public class Sex_hut_7 : Sex_ImproveClone, IP_DamageChange_sumoperation
    {
        // Token: 0x0600128E RID: 4750 RVA: 0x00097EB8 File Offset: 0x000960B8
        public override string DescExtended(string desc)
        {
            if(this.targets.Count > 0)
            {
                string str = new GDESkillData("S_SF_Hunter_7_1").Description;
                return str.Replace("&num1", ((int)(this.BChar.GetStat.atk * 1.5f)).ToString());
            }
            return base.DescExtended(desc).Replace("&num1", ((int)(this.BChar.GetStat.atk * 1.5f)).ToString());
        }

        // Token: 0x0600128F RID: 4751 RVA: 0x00097EF5 File Offset: 0x000960F5


        // Token: 0x06001290 RID: 4752 RVA: 0x00097F34 File Offset: 0x00096134
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            if (this.targets.Count<=0)
            {
                //Skill skill = Skill.TempSkill("S_SF_Hunter_7_1", this.BChar, this.BChar.MyTeam);
                this.targets.AddRange(Targets);
                Skill skill = this.MySkill.CloneSkill(true, null, null, true);
                this.targets.Clear();
                skill.isExcept = true;
                //skill.NotCount = true;
                skill.AutoDelete = 2;
                skill.BasicSkill = false;


                for (int i = 0; i < skill.AllExtendeds.Count; i++)
                {
                    if (!skill.AllExtendeds[i].BattleExtended && !skill.AllExtendeds[i].isDataExtended)
                    {
                        skill.AllExtendeds[i].SelfDestroy();
                    }
                }

                BattleSystem.instance.AllyTeam.Add(skill, true);
            }

        }
        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if(SkillD==this. MySkill&& this.targets.Contains(Target))
            {
                int num = Math.Min((int)(0.12 * Target.GetStat.maxhp), (int)(this.BChar.GetStat.atk * 1.5f));
                if(num > 0)
                {
                    PlusDamage = num;
                }
            }
        }
        public bool additional=false;
        public List<BattleChar> targets = new List<BattleChar>();
    }

    public class Sex_hut_8 : Sex_ImproveClone
    {

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            //BattleSystem.instance.AllyTeam.Draw(1);


                if (this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == this.BChar && a.CharinfoSkilldata != SkillD.CharinfoSkilldata) != null)
                {
                    this.BChar.MyTeam.ForceDraw(this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == this.BChar));
                }
                else if (this.BChar.MyTeam.Skills_UsedDeck.Find((Skill a) => a.Master == this.BChar && a.CharinfoSkilldata != SkillD.CharinfoSkilldata) != null)
                {
                    this.BChar.MyTeam.ForceDraw(this.BChar.MyTeam.Skills_UsedDeck.Find((Skill a) => a.Master == this.BChar));
                }
                else
                {
                    this.BChar.MyTeam.Draw(1);
                }
            
        }
    }
    public class Bcl_hut_8:Buff,IP_DamageChange_sumoperation
    {
        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (SkillD .Master==this.BChar && SkillD.AllExtendeds.Find(a=>a is Sex_hut_p)!=null)
            {
                //if (this.rec.targets.Contains(Target))
                Sex_hut_p sex= SkillD.AllExtendeds.Find(a => a is Sex_hut_p) as Sex_hut_p;

                if (sex.rec.targetRecs.Find(a => a.target == Target) != null )
                {
                    
                    Cri = Cri || sex.rec.targetRecs.Find(a => a.target == Target).cri;


                }
            }
        }
    }

    public class Sex_hut_9 : Sex_ImproveClone,IP_DamageChange_sumoperation
    {
        public override void Init()
        {
            base.Init();
            this.flag = false;
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_Public_1_Ex).Particle_Path;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.frame++;
            if(this.frame<0||this.frame>=10)
            {
                this.frame = 0;
                if (this.recTurn != BattleSystem.instance.TurnNum)
                {
                    List<BattleLog> logs = BattleSystem.instance.BattleLogs.getLogs((BattleLog log) => log.WhoUse==this.BChar, BattleSystem.instance.TurnNum);
                    if(logs.Find(a=>!Standard_Tool.OriginSkill(a.UsedSkill).FreeUse)==null)
                    {
                        this.NotCount = true;
                        base.SkillParticleOn();
                    }
                    else
                    {
                        this.recTurn = BattleSystem.instance.TurnNum;
                        this.NotCount = false;
                        base.SkillParticleOff();
                    }

                }
                else
                {
                    this.NotCount = false;
                    base.SkillParticleOff();
                }
            }

        }
        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if(SkillD==this.MySkill)
            {
                List<BattleLog> logs = BattleSystem.instance.BattleLogs.getLogs((BattleLog log) => log.WhoUse == this.BChar, BattleSystem.instance.TurnNum);

                if (logs.Find(a => !Standard_Tool.OriginSkill(a.UsedSkill).FreeUse && a.UsedSkill!=this.MySkill) == null)
                {
                    Cri = true;
                }
            }
        }
        public int frame = 0;
        public int recTurn = -1;
        private bool flag;
    }

    public class Sex_hut_10 : Sex_ImproveClone
    {

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            //this.TargetBuff.Clear();
            Buff buff = Targets[0].BuffAdd("B_SF_Hunter_p", this.BChar);
        }
    }
    public class Bcl_hut_10 : Buff,IP_SkillUse_Target,IP_DamageChange
    {
        public override string DescExtended()
        {
            if (this.recordTarget != null && !this.recordTarget.IsDead && this.recordTarget.BuffFind("B_SF_Hunter_p", false) && this.recordTarget.BuffReturn("B_SF_Hunter_p", false).Usestate_L == this.BChar)
            {

            }
            else
            {
                this.plusnum = 0;
                this.recordTarget = new BattleChar();
            }
            return base.DescExtended().Replace("&num1",this.plusper.ToString()).Replace("&num2",(this.plusper*this.plusnum).ToString());
        }
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if(SP.UseStatus==this.BChar)
            {
                if(hit.BuffFind("B_SF_Hunter_p", false) && hit.BuffReturn("B_SF_Hunter_p", false).Usestate_L == this.BChar)
                {
                    if(this.recordTarget!=hit)
                    {
                        this.plusnum = 0;
                        this.recordTarget = hit;
                    }
                    this.plusnum++;
                }
            }
        }
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if(SkillD.Master==this.BChar && Target.BuffFind("B_SF_Hunter_p", false) && Target.BuffReturn("B_SF_Hunter_p", false).Usestate_L == this.BChar)
            {
                if(this.plusnum > 0)
                {
                    if (this.recordTarget != Target)
                    {
                        this.plusnum = 0;
                        this.recordTarget = Target;
                    }
                    return (int)(Damage*this.plusper*this.plusnum/100f+Damage);
                }
            }
            return Damage;
        }

        public int plusper = 8;
        public int plusnum = 0;
        public BattleChar recordTarget=new BattleChar();
    }

    public class Sex_hut_11 : Sex_ImproveClone
    {
        public override void BattleStartDeck(List<Skill> Skills_Deck)
        {
            this.BChar.MyTeam.Draw(this.MySkill, null);
        }

    }
    public class Bcl_hut_11 : Buff, IP_SkillUse_Target
    {
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            //Debug.Log("Buff11命中，暴击：" + Cri);
            //未暴击加暴击率
            if (SP.UseStatus == this.BChar && !Cri)
            {
                this.PlusStat.cri += 10;
            }
            if (SP.UseStatus == this.BChar && Cri && !SP.SkillData.PlusHit)
            {
                this.clearCri = true;
            }

            //双暴击回费    
            if (SP.UseStatus==this.BChar && SP.SkillData.AllExtendeds.Find(a=>a is Sex_hut_p)!=null)
            {
                //Debug.Log("Buff11命中暴击,检测回费_1");
                Sex_hut_p sex= SP.SkillData.AllExtendeds.Find(a => a is Sex_hut_p) as Sex_hut_p;
                if(sex.rec.targetRecs.Find(a=>a.target==hit) != null)
                {
                    //Debug.Log("Buff11命中暴击,检测回费_2");
                    Phut_tar obj = sex.rec.targetRecs.Find(a => a.target == hit);
                    if(obj.cri && Cri)
                    {
                        //Debug.Log("Buff11命中暴击,检测回费_3");
                        this.BChar.MyTeam.AP++;
                    }
                }
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (this.clearCri)
            {
                if (!BattleSystem.instance.DelayWait && BattleSystem.instance.Particles.Count == 0)
                {
                    this.clearCri = false;
                    this.PlusStat.cri = 0;
                }
            }
        }
        public bool clearCri = false;
    }


    public class Sex_hut_12 : Sex_ImproveClone,IP_SkillUse_Target
    {
        public override string DescExtended(string desc)
        {
            List<int> alldmg = new List<int>();
            foreach (Hut_12_rec rec in this.recs)
            {
                alldmg.AddRange(rec.dmglist);
            }
            int sum = 0;
            foreach(int n in alldmg)
            {
                sum += n;
            }
            return base.DescExtended(desc).Replace("&num",sum.ToString());
        }
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if(SP.UseStatus==this.BChar && SP.SkillData.PlusHit && DMG>0)
            {
                if(this.recs.Find(a=>a.target == hit)!=null)
                {
                    Hut_12_rec rec = this.recs.Find(a => a.target == hit);
                    rec.dmglist.Add(DMG);
                }
                else
                {
                    Hut_12_rec rec = new Hut_12_rec();
                    rec.target = hit;
                    rec.dmglist.Add(DMG);
                    this.recs.Add(rec);
                }
            }
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            BattleSystem.DelayInputAfter(this.PlusAttack(Targets));
        }
        public IEnumerator PlusAttack(List<BattleChar> Targets)
        {
            /*
            bool finish = false;
            while (!finish)
            {
                finish = true;
                //只使用同目标的追击
                foreach(BattleChar tar in Targets)
                {
                    if(this.recs.Find(a => a.target == tar)!=null)
                    {
                        Hut_12_rec rec = this.recs.Find(a => a.target == tar);
                        if(rec.dmglist.Count > 0)
                        {
                            if (finish)
                            {
                                yield return new WaitForSeconds(0.15f);
                            }
                            Skill skill = Skill.TempSkill("S_SF_Hunter_12_1", this.BChar);
                            Skill_Extended sex=new Skill_Extended();
                            skill.ExtendedAdd(sex);
                            sex.SkillBasePlus.Target_BaseDMG = rec.dmglist[0] - 1;
                            sex.PlusSkillStat.Penetration = 100;
                            rec.dmglist.RemoveAt(0);
                            this.BChar.ParticleOut(skill, rec.target);
                            finish = false;
                        }
                    }
                }
                
            }
            */
            //使用全部目标的追击
            List<int> alldmg = new List<int>();

            foreach (Hut_12_rec rec in this.recs)
            {

                alldmg.AddRange(rec.dmglist);
            }
            while (alldmg.Count > 0 && Targets.Find(a => a.MyTeam.AliveChars.Count > 0) != null)
            {

                yield return new WaitForSeconds(0.15f);

                Skill skill = Skill.TempSkill("S_SF_Hunter_12_1", this.BChar);
                Skill_Extended sex = new Skill_Extended();
                skill.ExtendedAdd(sex);
                sex.SkillBasePlus.Target_BaseDMG = alldmg[0] - 1;
                sex.PlusSkillStat.Penetration = 100;
                alldmg.RemoveAt(0);

                List<BattleChar> attackTarget = new List<BattleChar>();
                foreach (BattleChar bc in Targets)
                {
                    if (!bc.IsDead && bc != null)
                    {
                        attackTarget.Add(bc);
                    }
                    else if (bc.MyTeam.AliveChars.Count > 0)
                    {
                        attackTarget.Add(bc.MyTeam.AliveChars.Random(this.BChar.GetRandomClass().Main));
                    }
                }
                if(attackTarget.Count > 0)
                {
                    this.BChar.ParticleOut(skill, attackTarget);
                }
            }




            yield break;
        }
        public List<Hut_12_rec> recs = new List<Hut_12_rec>();
    }
    public class Hut_12_rec
    {
        public BattleChar target=new BattleChar();
        public List<int> dmglist=new List<int>();
    }

    public class Sex_hut_12_1 : Sex_ImproveClone
    {
        

    }

    public class E_SF_Hunter_0 : EquipBase,IP_CriPerChange
    {
        public override void Init()
        {
            base.Init();
            this.PlusPerStat.Damage = 10;
            this.PlusStat.hit = 20;
        }
        
        public void CriPerChange(Skill skill, BattleChar Target, ref float CriPer)
        {
            if(skill.Master==this.BChar)
            {
                int num = BattleSystem.instance.EnemyTeam.AliveChars.Count;
                if (num < 5)
                {
                    CriPer += (16* (5 - num));
                }
            }

        }
    }


    public class SkillEn_SF_Hunter_0 : Sex_ImproveClone,IP_SpecialEnemyTargetSelect,IP_DamageChange_sumoperation//, IP_SkillUse_User_After
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return (MainSkill.TargetTypeKey == "enemy" || MainSkill.TargetTypeKey == "random_enemy" || MainSkill.TargetTypeKey == "all_onetarget")&& MainSkill.IsDamage;
        }

        public bool SpecialEnemyTargetSelect(Skill skill, BattleEnemy Target)
        {
            if(skill.AllExtendeds.Contains(this) && Target.BuffFind("B_SF_Hunter_p",false))
            {
                return true;
            }
            return false;
        }

        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (SkillD.AllExtendeds.Contains(this) && Target.BuffFind("B_SF_Hunter_p", false))
            {
                Cri = true;
            }
        }


    }
    public class SkillEn_SF_Hunter_1 : Sex_ImproveClone//, IP_SkillUse_User_After
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return (MainSkill.TargetTypeKey == "enemy" || MainSkill.TargetTypeKey == "random_enemy" || MainSkill.TargetTypeKey == "all_onetarget");
        }

        public override void Init()
        {
            base.Init();
            this.TargetBuff.Clear();
            BuffTag btg=new BuffTag();
            btg.BuffData = new GDEBuffData("B_SF_Hunter_en_1");
            btg.User = this.BChar;
            this.TargetBuff.Add(btg);
        }


    }
    public class Bcl_hut_en_1:Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.CRIGetDMG = 25;
        }
    }
}
