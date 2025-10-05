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
using System.Xml.Serialization;

namespace PD_Alina
{
    [PluginConfig("M200_PD_Alina_CharMod", "PD_Alina_CharMod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class PD_Alina_CharacterModPlugin : ChronoArkPlugin
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
        public class AlinaDef : ModDefinition
        {
        }
    }
    
    [HarmonyPatch(typeof(CharSelect_CampUI))]
    public class CharSelect_CampUI_Plugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("Init")]

        [HarmonyTranspiler]//ldarg.0 >> this
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var methodInfo = AccessTools.Method(typeof(Camp_Alina_1), nameof(Camp_Alina_1.Check1), new System.Type[] { typeof(CharSelect_CampUI), typeof(GDECharacterData) });
            var codes = instructions.ToList();

            for (int i = 0; i < codes.Count; i++)
            {
                if (i > 6 )
                {

                    bool f6 = codes[i - 6].opcode == OpCodes.Ldc_I4_0;
                    bool f5 = codes[i - 5].opcode == OpCodes.Ble;
                    bool f4 = codes[i - 4].opcode == OpCodes.Ldarg_0;
                    bool f3 = codes[i - 3].opcode == OpCodes.Ldfld;
                    bool f2 = codes[i - 2].opcode == OpCodes.Ldsfld;
                    bool f1 = codes[i - 1].opcode == OpCodes.Call;
                    bool f0 = codes[i].opcode == OpCodes.Stloc_S;

                    
                    //bool f1 = codes[i - 6].opcode == OpCodes.Ldarg_0;
                    //bool f2 = codes[i - 5].opcode == OpCodes.Ldfld;
                    //bool f3 = codes[i - 4].opcode == OpCodes.Ldarg_0;
                    //bool f4 = codes[i - 3].opcode == OpCodes.Ldfld;
                    //bool f5 = codes[i - 2].opcode == OpCodes.Call;
                    //bool f6 = codes[i - 1].opcode == OpCodes.Callvirt;
                    //bool f7 = codes[i].opcode == OpCodes.Stloc_S;
                    
                    if (f0 && f1 && f2 && f3 && f4 && f5 & f6)
                    {
                        //Debug.Log("埃莉诺_change!"+i);

                        yield return codes[i];

                        yield return new CodeInstruction(OpCodes.Ldarg_0);
                        yield return new CodeInstruction(OpCodes.Ldloc_S, 13);
                        yield return new CodeInstruction(OpCodes.Call, methodInfo);
                        //yield return codes[i];
                        yield return new CodeInstruction(OpCodes.Stloc_S, 13);
                        //Camp_Alina_1.pflag = true;
                    }
                    else
                    {
                        yield return codes[i];

                    }

                }
                else
                {
                    yield return codes[i];
                }

            }
        }

    }
    
    /*
    [HarmonyPatch(typeof(CharSelect_CampUI))]
    public class CharSelect_CampUI_Plugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("Init")]

        [HarmonyTranspiler]//ldarg.0 >> this
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var methodInfo = AccessTools.Method(typeof(Camp_Adeline_1), nameof(Camp_Adeline_1.Check1), new System.Type[] { typeof(CharSelect_CampUI), typeof(GDECharacterData) });
            var codes = instructions.ToList();

            for (int i = 0; i < codes.Count; i++)
            {
                if (i < codes.Count - 6)
                {
                    bool f1 = codes[i].opcode == OpCodes.Ldarg_0;
                    bool f2 = codes[i + 1].opcode == OpCodes.Ldfld;
                    bool f3 = codes[i + 2].opcode == OpCodes.Ldarg_0;
                    bool f4 = codes[i + 3].opcode == OpCodes.Ldfld;
                    bool f5 = codes[i + 4].opcode == OpCodes.Call;
                    bool f6 = codes[i + 5].opcode == OpCodes.Callvirt;
                    bool f7 = codes[i + 6].opcode == OpCodes.Stloc_S;




                    if (f1 && f2 && f3 && f4 && f5 && f6 && f7)
                    {
                        Debug.Log("埃莉诺_change!:" + i);

                        yield return (new CodeInstruction(OpCodes.Ldarg_0)).MoveLabelsFrom(codes[i]);//new CodeInstruction(OpCodes.Ldarg_0);
                        yield return new CodeInstruction(OpCodes.Ldloc_S, 13);
                        yield return new CodeInstruction(OpCodes.Call, methodInfo);
                        //yield return codes[i];
                        yield return new CodeInstruction(OpCodes.Stloc_S, 13);
                        //Camp_Adeline_1.pflag = true;

                        yield return codes[i];
                    }
                    else
                    {
                        yield return codes[i];

                    }

                }
                else
                {
                    yield return codes[i];
                }

            }
        }
    }
    */
    public static class Camp_Alina_1
    {
        public static GDECharacterData Check1(CharSelect_CampUI CSC, GDECharacterData gDECharacterData)
        {
            //Debug.Log("选人埃莉诺1");
            if (PlayData.TSavedata.Party.Find(a => a.GetData.Key == "PD_Adeline") != null && PlayData.TSavedata.Party.Find(a => a.GetData.Key == "PD_Alina") == null && CSC.CharList.Find(a => a.data.Key == "PD_Alina") == null && CSC.CharDatas.Find(a => a.Key == "PD_Alina") != null)
            {
                //Debug.Log("选人埃莉诺2");
                GDECharacterData gde = CSC.CharDatas.Find(a => a.Key == "PD_Alina");
                if (gde != null)
                {
                    //Debug.Log("选人埃莉诺:" + gde.Key);
                    return gde;
                }
                else
                {
                    return gDECharacterData;
                }
            }
            else
            {
                return gDECharacterData;
            }
        }
        public static bool pflag = false;
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
            //MasterAudio.PlaySound("A_aln_4", 1f, null, 0f, null, null, false, false);
            float volume = 0f;
            float volume2 = 0f;
            if (ModManager.getModInfo("PD_Nyto_Twins").GetSetting<ToggleSetting>("AudioOn").Value)
            {
                volume = ModManager.getModInfo("PD_Nyto_Twins").GetSetting<SliderSetting>("AudioVolume").Value;
            }
            else
            {
                volume = 0f;
            }

            if (inText.Contains("T_aln_1_1"))
            {
                inText = inText.Replace("T_aln_1_1:", string.Empty);
                MasterAudio.PlaySound("A_aln_1_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_aln_1_2"))
            {
                inText = inText.Replace("T_aln_1_2:", string.Empty);
                MasterAudio.PlaySound("A_aln_1_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_aln_2_1"))
            {
                inText = inText.Replace("T_aln_2_1:", string.Empty);
                MasterAudio.PlaySound("A_aln_2_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_aln_2_2"))
            {
                inText = inText.Replace("T_aln_2_2:", string.Empty);
                MasterAudio.PlaySound("A_aln_2_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_aln_3_1"))
            {
                inText = inText.Replace("T_aln_3_1:", string.Empty);
                MasterAudio.PlaySound("A_aln_3_1", volume, null, 1f, null, null, false, false);
            }
            else if (inText.Contains("T_aln_4_1"))
            {
                inText = inText.Replace("T_aln_4_1:", string.Empty);
                MasterAudio.PlaySound("A_aln_4_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_aln_4_2"))
            {
                inText = inText.Replace("T_aln_4_2:", string.Empty);
                MasterAudio.PlaySound("A_aln_4_2", volume, null, 1f, null, null, false, false);
            }
            else if (inText.Contains("T_aln_5_1"))
            {
                inText = inText.Replace("T_aln_5_1:", string.Empty);
                MasterAudio.PlaySound("A_aln_5_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_aln_5_2"))
            {
                inText = inText.Replace("T_aln_5_2:", string.Empty);
                MasterAudio.PlaySound("A_aln_5_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_aln_6_1"))
            {
                inText = inText.Replace("T_aln_6_1:", string.Empty);
                MasterAudio.PlaySound("A_aln_6_1", volume, null, 1f, null, null, false, false);
            }
            else if (inText.Contains("T_aln_6_2"))
            {
                inText = inText.Replace("T_aln_6_2:", string.Empty);
                MasterAudio.PlaySound("A_aln_6_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_aln_7_1"))
            {
                inText = inText.Replace("T_aln_7_1:", string.Empty);
                MasterAudio.PlaySound("A_aln_7_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_aln_8_1"))
            {
                inText = inText.Replace("T_aln_8_1:", string.Empty);
                MasterAudio.PlaySound("A_aln_8_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_aln_9_1"))
            {
                inText = inText.Replace("T_aln_9_1:", string.Empty);
                MasterAudio.PlaySound("A_aln_9_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_aln_11_1"))
            {
                inText = inText.Replace("T_aln_11_1:", string.Empty);
                MasterAudio.PlaySound("A_aln_11_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_aln_11_2"))
            {
                inText = inText.Replace("T_aln_11_2:", string.Empty);
                MasterAudio.PlaySound("A_aln_11_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_aln_11_3"))
            {
                inText = inText.Replace("T_aln_11_3:", string.Empty);
                MasterAudio.PlaySound("A_aln_11_3", volume, null, 0f, null, null, false, false);
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
            bool flag = inputitem.itemkey == "E_PD_Alina_0" && __instance.Info.KeyData != "PD_Alina";
            if (flag)
            {
                __result = true;
            }
            //Debug.Log("成功进入OnDropSlot的patch");
        }
    }
    [HarmonyPatch(typeof(FriendShipUI))]
    [HarmonyPatch("Gift")]
    public static class GiftPlugin
    {
        [HarmonyPostfix]
        public static void Gift_AfterPatch(FriendShipUI __instance, string CharId)
        {

            if (CharId == "PD_Alina")
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

    [HarmonyPatch(typeof(BattleAlly))]
    [HarmonyPatch("UseSkillAfter")]
    public static class UseSkillAfterPlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPostfix]
        public static void UseSkillAfter_After_patch(BattleAlly __instance, Skill skill)
        {
            if(!skill.IsNowCasting)
            {
                foreach (IP_UseSkillAfter_After ip_patch in skill.IReturn<IP_UseSkillAfter_After>())
                {
                    //Debug.Log("only damage drm");
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.UseSkillAfter_After(__instance, skill);
                    }
                }
            }



        }
    }
    public interface IP_UseSkillAfter_After
    {
        // Token: 0x06000001 RID: 1
        void UseSkillAfter_After(BattleAlly user, Skill skill);
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


    public class P_Alina : Passive_Char, IP_ParticleOut_After/*, IP_TargetedAlly*/,IP_DamageTakeChange, IP_Hit,IP_Dodge,IP_LevelUp
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
                list.Add(ItemBase.GetItem("E_PD_Alina_0", 1));
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
        public bool ItemTake = false;
        public bool ItemTake2 = false;
        public IEnumerator ParticleOut_After(Skill SkillD, List<BattleChar> Targets)//mark
        {
            if (SkillD.IsDamage && !SkillD.PlusHit && SkillD.Master == this.BChar)
            {
                foreach (BattleChar bc in Targets)
                {
                    bc.BuffAdd("B_PD_Alina_p", this.BChar, false, 0, false, -1, false);
                }

            }
            yield break;
        }

        /*
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if(BuffUser==this.BChar &&addedbuff.BuffData.Debuff && !addedbuff.BuffData.BuffTag.Key.IsNullOrEmpty() && addedbuff.BuffData.BuffTag.Key!="null")
            {
                BuffTaker.BuffAdd("B_PD_Alina_p", this.BChar, false, 0, false, -1, false);
            }
        }
        */
        /*
        public IEnumerator Targeted(BattleChar Attacker, List<BattleChar> SaveTargets, Skill skill)
        {

            if (SaveTargets.Contains(this.BChar) && Attacker.BuffFind("B_PD_Alina_p", false))
            {
                Skill skill2 = Skill.TempSkill("S_PD_Alina_p", this.BChar, this.BChar.MyTeam);
                skill2.PlusHit = true;
                skill2.FreeUse = true;
                Skill_Extended sex = new Skill_Extended();
                sex.PlusSkillPerStat.Damage = 25 * Attacker.BuffReturn("B_PD_Alina_p", false).StackNum;
                sex.IsDamage = true;
                skill2.AllExtendeds.Add(sex);

                this.BChar.ParticleOut(skill2, Attacker);
                yield return new WaitForSeconds(0.2f);
            }

            yield break;
        }
        */
        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            if ( SP.UseStatus.BuffFind("B_PD_Alina_p", false) && SP.UseStatus!=this.BChar)
            {
                BattleSystem.DelayInput(P_Alina.AntiAttack(SP.UseStatus, this.BChar));
            }

        }

        public void Dodge(BattleChar Char, SkillParticle SP)
        {
            if(Char==this.BChar && SP.UseStatus.BuffFind("B_PD_Alina_p", false) && SP.UseStatus != this.BChar)
            {
                BattleSystem.DelayInput(P_Alina.AntiAttack(SP.UseStatus, this.BChar));
            }
        }
        public static IEnumerator AntiAttack(BattleChar target,BattleChar user)
        {
            if (target.BuffFind("B_PD_Alina_p", false))
            {
                Skill skill2 = Skill.TempSkill("S_PD_Alina_p", user, user.MyTeam);
                skill2.PlusHit = true;
                skill2.FreeUse = true;
                Skill_Extended sex = new Skill_Extended();
                sex.PlusSkillPerStat.Damage = 30 * target.BuffReturn("B_PD_Alina_p", false).StackNum;
                sex.IsDamage = true;
                skill2.AllExtendeds.Add(sex);

                user.ParticleOut(skill2, target);
            }
            yield break;
        }
        public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {
            if(Hit == this.BChar && Dmg > 0 && User.BuffFind("B_PD_Alina_p", false))
            {
                int dmg2 = (int)(Dmg * (1 - 0.1 * User.BuffReturn("B_PD_Alina_p", false).StackNum));
                if(dmg2 <= 0)
                {
                    dmg2 = 1;
                }
                return dmg2;
            }
            return Dmg;
        }
        
    }

    public interface IP_CheckEquipWhite
    {
        bool CheckEquipWhite();
    }
    public class Bcl_aln_p : Buff, IP_BuffAdd, IP_BuffAddAfter,IP_TurnEnd,IP_BuffUpdate
    {
        public override void Init()
        {
            base.Init();
            this.BuffUpdate(this);
        }
        public void BuffUpdate(Buff MyBuff)
        {
            bool flag1 = false;
            GDEBuffData gde = new GDEBuffData("B_PD_Alina_p");
            if (BattleSystem.instance != null)
            {

                foreach (IP_WhiteMarkRemain ip_patch in BattleSystem.instance.IReturn<IP_WhiteMarkRemain>())
                {
                    //Debug.Log("only damage drm");
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        flag1 = flag1 || ip_patch.WhiteMarkRemain();
                    }
                }
            }
            if (flag1)
            {
                //this.BuffData.MaxStack = gde.MaxStack + 2;

                Traverse.Create(this.BuffData).Field("_MaxStack").SetValue(gde.MaxStack + 2);

                //this.BuffData.LifeTime = gde.LifeTime + 1;
                //if (this.View)
                //{
                //this.StackInfo[0].RemainTime = (int)this.BuffData.LifeTime;
                //}
            }
            else
            {
                //this.BuffData.MaxStack = gde.MaxStack;

                Traverse.Create(this.BuffData).Field("_MaxStack").SetValue(gde.MaxStack);

                //this.BuffData.LifeTime = gde.LifeTime;
            }
            
        }
        
        public int SumDamage()
        {
            float sumdmg = 0;
            foreach (StackBuff sta in this.StackInfo)
            {
                float part1dmg = (float)(0.3 * sta.UseState.GetStat.atk);
                float part2dmg = (float)(Math.Min(this.BChar.GetStat.maxhp * 0.01, 0.5 * sta.UseState.GetStat.atk));
                if (this.usedstacks.Contains(sta))
                {
                    if(this.RemainMark())
                    {
                        sumdmg = (float)(sumdmg + (part1dmg + part2dmg) * 0.5);
                    }
                }
                else
                {
                    sumdmg = sumdmg + part1dmg + part2dmg;
                }
            }
            return (int)sumdmg;
        }
        public override string DescExtended()
        {
            int sumdmg = 0;
            sumdmg += this.SumDamage();
            return base.DescExtended().Replace("&a", sumdmg.ToString());
        }
        public override void TurnUpdate()
        {

        }
        public void TurnEnd()
        {
            base.TurnUpdate();
        }
        public override void BuffStat()
        {
            base.BuffStat();
            int num = Math.Min(this.BuffData.MaxStack, this.StackNum);
            this.PlusPerStat.Damage= -5 * num;
            this.PlusStat.hit = -5 * num;
            //this.PlusStat.crihit = 15 * num;
        }
        public bool RemainMark()
        {
            bool flag1 = false;
            if (BattleSystem.instance != null)
            {

                foreach (IP_WhiteMarkRemain ip_patch in BattleSystem.instance.IReturn<IP_WhiteMarkRemain>())
                {
                    //Debug.Log("only damage drm");
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        flag1 = flag1 || ip_patch.WhiteMarkRemain();
                    }
                }
            }
            return flag1;
        }
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)//mark
        {

            this.adding = true;
        }
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (addedbuff == this)
            {
                bool flag1 = false;
                flag1 = this.RemainMark();
                if (flag1)
                {
                    stackBuff.RemainTime++;
                }
            }

            if (this.adding)
            {
                this.adding = false;
                if (BuffTaker == this.BChar && addedbuff.BuffData.Key == "B_PD_Adeline_p")
                {
                    int sumdmg = 0;
                    sumdmg += this.SumDamage();
                    if(sumdmg > 0)
                    {
                        BattleSystem.DelayInput(this.ActiveDamage(sumdmg, stackBuff.UseState));
                        //this.BChar.Damage(BattleSystem.instance.DummyChar, sumdmg, false, false, false, 100, false, false, false);
                        //this.SelfDestroy();
                        this.DamageAfter();
                    }

                }
            }
        }
        public IEnumerator ActiveDamage(int dmg,BattleChar bc)
        {
            yield return new WaitForFixedUpdate();
            Skill skill2 = Skill.TempSkill("S_PD_Alina_m", bc, bc.MyTeam);
            skill2.PlusHit = true;
            skill2.FreeUse = true;
            skill2.NeverCri = true;
            Skill_Extended sex = new Skill_Extended();
            sex.SkillBasePlus.Target_BaseDMG = dmg;
            sex.IsDamage = true;
            sex.PlusSkillStat.Penetration = 100;
            skill2.AllExtendeds.Add(sex);

            BattleSystem.instance.DummyChar.ParticleOut(skill2, this.BChar);
            yield break;
        }
        public void DamageAfter()
        {
            for (int i = 0; i < this.usedstacks.Count; i++)
            {
                if (!this.StackInfo.Contains(this.usedstacks[i]))
                {
                    this.usedstacks.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < this.StackInfo.Count; i++)
            {
                /*
                bool remain = false;
                foreach (IP_WhiteMarkRemain ip_1 in BattleSystem.instance.IReturn<IP_WhiteMarkRemain>())
                {
                    //Debug.Log("only damage drm");
                    bool flag = ip_1 != null;
                    if (flag)
                    {
                        remain = remain || ip_1.WhiteMarkRemain(this.StackInfo[i]);
                    }
                }

                if (remain)
                {
                    if (!this.usedstacks.Contains(this.StackInfo[i]))
                    {
                        this.usedstacks.Add(this.StackInfo[i]);
                    }
                }
                else
                {
                    this.StackInfo.RemoveAt(i);
                    i--;
                }
                */
                if (!this.usedstacks.Contains(this.StackInfo[i]))
                {
                    this.usedstacks.Add(this.StackInfo[i]);
                }
            }
            if (this.StackInfo.Count <= 0)
            {
                this.SelfDestroy();
            }

        }
        private bool adding = false;
        private List<StackBuff> usedstacks = new List<StackBuff>();
    }
    public interface IP_WhiteMarkRemain
    {
        // Token: 0x06000001 RID: 1
        bool WhiteMarkRemain(StackBuff sta=null);
    }
    public class Sex_aln_1 : Sex_ImproveClone,IP_SkillUsePatch_Before
    {

        public override bool TargetSelectExcept(BattleChar ExceptTarget)
        {
            if (this.repeating)
            {
                return this.tar.Contains(ExceptTarget);
            }
            else
            {
                return false;
            }

        }

        //public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
            
        public void SkillUsePatch_Before(BattleChar TargetChar, Skill TargetSkill)
        {
            if (TargetChar == null)
            {
                return;
            }
            //Skill skill = this.MySkill.CloneSkill(true, null, null, true);
            Skill skill = Skill.TempSkill("S_PD_Alina_1", this.BChar, this.BChar.MyTeam);


            skill.isExcept = true;
            skill.AutoDelete = 1;
            skill.BasicSkill = false;


            for (int i = 0; i < skill.AllExtendeds.Count; i++)
            {
                if (skill.AllExtendeds[i] is Sex_aln_1 sex)
                {
                    sex.repeating = true;
                    sex.tar.AddRange(this.tar);
                    sex.tar.Add(TargetChar);
                }
                if (!skill.AllExtendeds[i].BattleExtended && !skill.AllExtendeds[i].isDataExtended)
                {
                    skill.AllExtendeds[i].SelfDestroy();
                }
            }
            //skill.PlusHit = true;


            this.BChar.MyTeam.Add(skill, true);


        }
        private bool repeating = false;
        private List<BattleChar> tar = new List<BattleChar>();
    }
    public class Sex_aln_2: Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            if ((Targets[0] as BattleEnemy).istaunt)
            {
                BuffTag btg = new BuffTag();
                btg.BuffData = new GDEBuffData("B_PD_Alina_2_2");
                btg.User = this.BChar;
                this.TargetBuff.Add(btg);
            }
        }
    }

    public class Bcl_aln_2_2 : B_Taunt, IP_Awake, IP_SkillUse_User
    {
        // Token: 0x06000B04 RID: 2820 RVA: 0x0007D701 File Offset: 0x0007B901
        public override void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (Targets[0].Info.Ally != this.BChar.Info.Ally)
            {
                Targets.Clear();
                Targets.Add(this.Usestate_F);
            }
            base.SkillUse(SkillD, Targets);
        }
    }

    public class Bcl_aln_S : Buff//, IP_BuffUpdate
    {

    }
    public class Sex_aln_3 : Sex_ImproveClone
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&b", ((int)(0.3 * this.BChar.GetStat.atk)).ToString());
        }
        public override void AttackEffectSingle(BattleChar hit, SkillParticle SP, int DMG, int Heal)
        {
            int num = 0;
            foreach (Buff buff in hit.Buffs)
            {
                if (buff.BuffData.Key == "B_PD_Adeline_p" || buff.BuffData.Key == "B_PD_Alina_p")
                {
                    num += buff.StackNum;
                }
            }
            if(num > 0)
            {
                if(!SP.SkillData.Master.BuffFind("B_PD_Alina_S",false))
                {
                    SP.SkillData.Master.BuffAdd("B_PD_Alina_S", SP.SkillData.Master, false, 0, false, -1, false);
                }
                Buff buff = SP.SkillData.Master.BuffReturn("B_PD_Alina_S", false);
                buff.BarrierHP += (num * (int)( 0.3 * SP.SkillData.Master.GetStat.atk));
            }
        }
    }
    public class Bcl_aln_3 : Buff//, IP_BuffUpdate
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.AggroPer = 60;
        }
    }

    public class Sex_aln_4 : Sex_ImproveClone, IP_DamageChange, IP_BuffUpdate//,IP_Kill
    {
        public void BuffUpdate(Buff MyBuff)
        {
            int num1 = 0;
            foreach(BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars)
            {
                if(bc.BuffFind("B_PD_Adeline_p", false))
                {
                    num1 += bc.BuffReturn("B_PD_Adeline_p", false).StackNum;
                }
            }
            int num2 = 0;
            foreach (BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {
                if (bc.BuffFind("B_PD_Adeline_p", false))
                {
                    num2 += bc.BuffReturn("B_PD_Adeline_p", false).StackNum;
                }
            }
            if(num1>= BattleSystem.instance.EnemyTeam.AliveChars.Count || num2>= BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Count)// (BattleSystem.instance.EnemyTeam.AliveChars.Find(a=>!a.BuffFind("B_PD_Adeline_p",false))==null)
            {
                this.APChange = -1;
                this.NotCount = true;
            }
            else
            {
                this.APChange = 0;
                this.NotCount = false;
            }
        }
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if (SkillD == this.MySkill)
            {
                int num = this.PlusPer(Target);
                int dmg = (int)(Damage * (1 + 0.01 * num));
                return dmg;
            }
            return Damage;
        }
        public int PlusPer(BattleChar target)
        {
            int num = 0;

                foreach (Buff buff in target.Buffs)
                {
                    if (buff.BuffData.Key == "B_PD_Alina_p")
                    {
                            num += (20 * buff.StackNum);
                    }
                }
            
            return num;
            //this.PlusPerStat.Damage = 30 * num;
        }

        public override void SkillKill(SkillParticle SP)
        {
            if(SP.SkillData==this.MySkill && !this.killed)
            {
                

                Skill skill = this.MySkill.CloneSkill(true, null, null, true);

                this.killed = true;

                skill.isExcept = true;
                //skill.AutoDelete = 1;
                skill.BasicSkill = false;
                Skill_Extended sex = skill.ExtendedAdd_Battle(new Skill_Extended());
                sex.APChange = -1;


                for (int i = 0; i < skill.AllExtendeds.Count; i++)
                {
                    if (!skill.AllExtendeds[i].BattleExtended && !skill.AllExtendeds[i].isDataExtended)
                    {
                        skill.AllExtendeds[i].SelfDestroy();
                    }
                }

                this.BChar.MyTeam.Add(skill, true);
            }
        }
        private bool killed = false;
    }
    public class Bcl_aln_4 : Buff//, IP_BuffUpdate
    {

    }

    public class Sex_aln_5 : Sex_ImproveClone
    {
        public override void Init()
        {
            base.Init();
            this.PlusSkillStat.Penetration = 100;
        }
    }
    public class Bcl_aln_5 : Buff, IP_BuffUpdate
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.DMGTaken = 15;
        }
        public void BuffUpdate(Buff MyBuff)
        {
            if(this.BChar.GetStat.def<=0 ||this.refreshing)
            {
                return;
            }
            else
            {
                this.refreshing = true;
                this.PlusStat.def = Math.Min(this.PlusStat.def - this.BChar.GetStat.def, 0);
                //Traverse.Create(this.BChar.Info).Field("getstat_BeforeFrameCount").SetValue(Time.frameCount - 10);
                this.BChar.Info.ForceGetStat();
                Stat stat = this.BChar.Info.get_stat;

                BattleSystem.DelayInput(this.DelayCheck());
                //this.BuffStatUpdate();
            }
        }
        public IEnumerator DelayCheck()
        {
            this.refreshing = false ;
            //Traverse.Create(this.BChar.Info).Field("getstat_BeforeFrameCount").SetValue(Time.frameCount - 10);
            this.BChar.Info.ForceGetStat();
            this.PlusStat.def = Math.Min(this.PlusStat.def - this.BChar.GetStat.def, 0);
            //Traverse.Create(this.BChar.Info).Field("getstat_BeforeFrameCount").SetValue(Time.frameCount - 10);
            this.BChar.Info.ForceGetStat();
            Stat stat = this.BChar.Info.get_stat;

            yield break;
        }
        public bool refreshing = false;
    }


    public class SkillExtended_AlinaShield : Sex_ImproveClone//, IP_BuffUpdate
    {
        public override string DescExtended(string desc)
        {
            this.AddShieldPersent();
            return base.DescExtended(desc).Replace("&z", "");


        }
        public void SetShield(bool Tar, int shieldTar, bool Self, int shieldSelf)
        {
            this.Tar = Tar;
            this.Self = Self;
            this.shieldTar = shieldTar;
            this.shieldSelf = shieldSelf;
            if (this.MainBuffTar != null)
            {
                this.MainBuffTar.BuffData.Barrier = shieldTar;
            }
            if (this.MainBuffSelf != null)
            {
                this.MainBuffSelf.BuffData.Barrier = shieldSelf;
            }

        }

        public override void Init()
        {


            base.Init();
            BuffTag buffTag = new BuffTag();
            buffTag.BuffData = new GDEBuffData("B_PD_Alina_S").ShallowClone();
            buffTag.User = this.BChar;
            //this.TargetBuff.Add(buffTag);
            BuffTag buffTag2 = new BuffTag();
            buffTag2.BuffData = new GDEBuffData("B_PD_Alina_S").ShallowClone();
            buffTag2.User = this.BChar;

            this.MainBuffSelf = buffTag;
            this.MainBuffTar = buffTag2;
            this.AddShieldPersent();

            this.SelfBuff.Clear();
            this.TargetBuff.Clear();


            if (this.Self)
            {
                this.SelfBuff.Add(buffTag);

            }
            if (this.Tar)
            {
                this.TargetBuff.Add(buffTag2);

            }


        }

        public virtual void AddShieldPersent()
        {
        }


        // Token: 0x06000C85 RID: 3205 RVA: 0x000812F7 File Offset: 0x0007F4F7
        public string BaseDescExtended(string desc)
        {
            return base.DescExtended(desc);
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            this.AddShieldPersent();

            this.TargetBuff.Clear();
            this.SelfBuff.Clear();
            base.SkillUseSingle(SkillD, Targets);

            if (this.Self)
            {

                this.BuffAdd(this.BChar, this.shieldSelf);

            }
            if (this.Tar)
            {
                foreach (BattleChar bc in Targets)
                {
                    this.BuffAdd(bc, this.shieldTar);
                }

            }

        }
        private void BuffAdd(BattleChar Target, int num)
        {
            Buff buff = Target.BuffAdd("B_PD_Alina_S", this.BChar, false, 0, false, -1, false);
            buff.BarrierHP += num;

        }
        private bool Tar;
        private int shieldTar;
        private bool Self;
        private int shieldSelf;
        public BuffTag MainBuffTar;
        public BuffTag MainBuffSelf;
    }
    public class Sex_aln_6 : Sex_ImproveClone//SkillExtended_AlinaShield
    {
        /*
        public override void Init()
        {
            base.Init();
        }
        */
        /*
        public override void AddShieldPersent()
        {
            base.AddShieldPersent();
            base.SetShield(false,0, true, (int)(0.5* this.BChar.GetStat.maxhp));
        }
        */

    }
    public class Bcl_aln_6 : Buff,IP_TargetedAlly//, IP_BuffUpdate
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", ((int)(0.5 * this.BChar.GetStat.atk)).ToString()).Replace("&b", ((int)(0.2 * this.BChar.GetStat.atk)).ToString());
        }
        public IEnumerator Targeted(BattleChar Attacker, List<BattleChar> SaveTargets, Skill skill)
        {

            if (SaveTargets.Contains(this.BChar) )//&& Attacker.BuffFind("B_PD_Alina_p", false))
            {
                Attacker.BuffAdd("B_PD_Alina_p", this.BChar, false, 0, false, -1, false);

                yield return this.AntiAttack(Attacker, this.BChar);
                yield return new WaitForSeconds(0.2f);
            }

            yield break;
        }
        public override void Init()
        {
            base.Init();
            this.PlusStat.Strength = true;
        }

        public IEnumerator AntiAttack(BattleChar target, BattleChar user)
        {

                Skill skill2 = Skill.TempSkill("S_PD_Alina_p", user, user.MyTeam);
                skill2.PlusHit = true;
                skill2.FreeUse = true;
                Skill_Extended sex = new Skill_Extended();
                sex.PlusSkillPerStat.Damage = 50 ;
            if (target.BuffFind("B_PD_Alina_p", false))
            {
                sex.PlusSkillPerStat.Damage += 20 * target.BuffReturn("B_PD_Alina_p", false).StackNum;
            }
                sex.IsDamage = true;
                skill2.AllExtendeds.Add(sex);

                user.ParticleOut(skill2, target);
            
            yield break;
        }
    }
    public class Sex_aln_7 : Sex_ImproveClone, IP_SkillCastingStart, IP_SkillCastingQuit,IP_UseSkillAfter_After,IP_DamageTake, IP_Targeted,IP_TurnEnd
    {
        
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(0.8 * this.BChar.GetStat.maxhp)).ToString());
        }
        public override void Init()
        {
            base.Init();
            this.CountingExtedned = true;
        }

        public void TurnEnd()
        {
            //Debug.Log("行动数1：" + BattleSystem.instance.AllyTeam.TurnActionNum);
            //Debug.Log("行动数2：" + BattleSystem.instance.EnemyTeam.TurnActionNum);
            if (this.MySkill.IsNowCasting)
            {
                this.startActionNum -= BattleSystem.instance.AllyTeam.TurnActionNum;
            }
        }
        public void UseSkillAfter_After(BattleAlly user, Skill skill)
        {

                Buff buff = this.BChar.BuffAdd("B_PD_Alina_S", this.BChar, false, 0, false, -1, false);
                buff.BarrierHP += ((int)(0.8 * this.BChar.GetStat.maxhp));
            

        }
        public void SkillCasting(CastingSkill ThisSkill)
        {
            if(ThisSkill.skill==this.MySkill)
            {
                this.startActionNum = BattleSystem.instance.AllyTeam.TurnActionNum;
            }

        }
        public void SkillCastingQuit(CastingSkill ThisSkill)
        {
            if (ThisSkill.skill == this.MySkill)
            {
                this.endActionNum = BattleSystem.instance.AllyTeam.TurnActionNum;
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(this.MySkill.IsNowCounting)
            {
                if(this.BChar.BarrierHP<=0 && !this.attacked)
                {
                    this.attacked = true;
                    this.Attack();
                }
            }

                //
            
        }

        //public IEnumerator Targeted(BattleChar Attacker, List<BattleChar> SaveTargets, Skill skill)
        public void Targeted(Skill SkillD, List<BattleChar> Targets)
        {
            if(this.MySkill.IsNowCounting && !SkillD.Master.Info.Ally && SkillD.IsDamage)
            {
                for (int j = 0; j < Targets.Count; j++)
                {
                    if (Targets[j] != this.BChar && this.BChar.MyTeam.AliveChars_Vanish.Contains(Targets[j]))
                    {
                        BattleChar targetally = Targets[j];
                        Targets[j] = this.BChar;
                        EffectView.TextOutSimple(targetally, (new GDEBuffData("B_Common_Protection")).Name);
                    }
                }

            }

        }
        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if (this.MySkill.IsNowCounting)
            {
                if (Target == this.BChar && !NODEF)
                {
                    Buff buff = this.BChar.BuffAdd("B_PD_Alina_7", this.BChar, false, 0, false, -1, false);
                    BattleSystem.DelayInput(this.Destroy(buff));
                }
            }
        }
        public IEnumerator Destroy(Buff buff)
        {
            buff.SelfDestroy();
            yield break;
        }
        public void Attack()
        {
            if (BattleSystem.instance.CastSkills.Find(a => a.skill == this.MySkill) != null)
            {
                int n = BattleSystem.instance.CastSkills.FindIndex(a => a.skill == this.MySkill);
                BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyCastingSkillUse(BattleSystem.instance.CastSkills[n], false));
                BattleSystem.instance.CastSkills.RemoveAt(n);
            }
            else if (BattleSystem.instance.SaveSkill.Find(a => a.skill == this.MySkill) != null)
            {
                int m = BattleSystem.instance.SaveSkill.FindIndex(a => a.skill == this.MySkill);
                BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyCastingSkillUse(BattleSystem.instance.SaveSkill[m], false));
                BattleSystem.instance.SaveSkill.RemoveAt(m);
            }
            else
            {
                this.attacked = false;
            }
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            int num = this.endActionNum - this.startActionNum;
            if(num>0 )
            {
                this.PlusPerStat.Damage = 5 * num;
                this.PlusSkillStat.cri = 10 * num;

            }
            if (num >= 8 )
            {
                BuffTag btg = new BuffTag();
                btg.BuffData = new GDEBuffData("B_PD_Alina_2_2");
                btg.User = this.BChar;
                this.TargetBuff.Add(btg.Clone());
            }
        }
        public bool attacked = false;
        public int startActionNum = 0;
        public int endActionNum = 0;
    }

    public class Bcl_aln_7 : Buff,IP_DeadResist
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.Strength = true;
        }
        public bool DeadResist()
        {
            return true;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (BattleSystem.instance.CastSkills.Find(a => a.skill.MySkill.KeyID == "S_PD_Alina_7") == null && BattleSystem.instance.SaveSkill.Find(a => a.skill.MySkill.KeyID == "S_PD_Alina_7") == null)
            {
                this.SelfDestroy();
            }
        }
    }

    public class Sex_aln_8 : Sex_ImproveClone,IP_DamageTake
    {
        public override void Init()
        {
            base.Init();
            this.CountingExtedned = true;
        }
        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if(this.MySkill.IsNowCounting && Target.Info.Ally==this.BChar.Info.Ally )
            {
                resist = true;
            }
        }
    }
    public class Sex_aln_9 : Sex_ImproveClone
    {
        public override void BattleStartDeck(List<Skill> Skills_Deck)
        {
            this.BChar.MyTeam.Draw(this.MySkill, null);
        }
    }
    public class Bcl_aln_9 : SF_Standard.InBuff, IP_WhiteMarkRemain
    {
        public bool WhiteMarkRemain(StackBuff sta=null)
        {
            if (sta == null || sta.UseState == this.BChar)
            {
                return true;
            }
            return false;
        }
        /*
        public override void Init()
        {
            base.Init();
            int num = 0;
            foreach (E_aln_rec rec in this.plist)
            {
                num += rec.plustime;
            }
            this.PlusPerStat.Damage =  5 * num;
            this.PlusStat.dod = 5 * num;
            this.PlusStat.def = 5 * num;
        }
        public void SomeOneDead(BattleChar DeadChar)
        {
            if (DeadChar != this.BChar && !DeadChar.Info.Ally)
            {
                int num = 2;
                if (DeadChar.BuffFind("B_PD_Alina_p", false))
                {
                    num += DeadChar.BuffReturn("B_PD_Alina_p", false).StackNum;

                }
                BattleSystem.DelayInputAfter(this.AddRec(num, 3));
            }
        }
        public IEnumerator AddRec(int num = 0, int time = 3)
        {
            if (num > 0)
            {
                this.plist.Add(new E_aln_rec { plustime = num, remaintime = time });
                this.Init();
            }
            yield break;
        }


        public override void TurnUpdate()
        {
            base.TurnUpdate();
            foreach (E_aln_rec rec in this.plist)
            {
                rec.remaintime--;
            }
            this.plist.RemoveAll(rec => rec.remaintime <= 0);
            this.Init();
        }

        private List<E_aln_rec> plist = new List<E_aln_rec>();
        */
    }
    public class Sex_aln_10 : SkillExtended_AlinaShield
    {
        public override void Init()
        {
            base.Init();
        }
        
        public override void AddShieldPersent()
        {
            base.AddShieldPersent();
            base.SetShield(true, (int)(0.5 * this.BChar.GetStat.maxhp), false,0 );
        }
        
    }
    public class Bcl_aln_10 : Buff, IP_DamageTake, IP_SkillUse_Team
    {

        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if(Target==this.BChar)
            {
                this.sumDamage += Dmg;
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(  this.frame1>19 || this.frame1 < 0)
            {
                this.frame1 = 0;
                if(this.sumDamage > 0)
                {
                    foreach (Skill skill in BattleSystem.instance.AllyTeam.Skills)
                    {
                        if (skill.Master == this.BChar && skill.IsTargetSkill && skill.IsDamage && skill.AllExtendeds.Find(a => a is Ex_aln_10) == null)
                        {
                            Ex_aln_10 ex = skill.ExtendedAdd(Skill_Extended.DataToExtended("Ex_PD_Alina_10")) as Ex_aln_10;
                            ex.mainbuff = this;
                        }
                    }
                    if((this.BChar as BattleAlly).MyBasicSkill.buttonData.IsTargetSkill && (this.BChar as BattleAlly).MyBasicSkill.buttonData.IsDamage && (this.BChar as BattleAlly).MyBasicSkill.buttonData.AllExtendeds.Find(a => a is Ex_aln_10) == null)
                    {
                        Ex_aln_10 ex = Skill_Extended.DataToExtended("Ex_PD_Alina_10") as Ex_aln_10;
                        ex.mainbuff = this;
                        (this.BChar as BattleAlly).MyBasicSkill.buttonData.AllExtendeds.Add(ex);
                    }
                }

            }
            this.frame1++;
        }
        public int frame1 = 0;
        public void SkillUseTeam(Skill skill)
        {
            if(skill.Master == this.BChar && skill.IsTargetSkill && skill.IsDamage)
            {
                if (skill.AllExtendeds.Find(a => a is Ex_aln_10) != null)
                {
                    Ex_aln_10 ex = skill.AllExtendeds.Find(a => a is Ex_aln_10) as Ex_aln_10;
                    ex.SkillBasePlus.Target_BaseDMG = this.sumDamage;
                    this.sumDamage = 0;
                }
                else
                {
                    Ex_aln_10 ex = skill.ExtendedAdd(Skill_Extended.DataToExtended("Ex_PD_Alina_10")) as Ex_aln_10;
                    ex.SkillBasePlus.Target_BaseDMG = this.sumDamage;
                    this.sumDamage = 0;
                }
            }

        }
        public int sumDamage = 0;
    }
    public class Ex_aln_10: Sex_ImproveClone
    {
        public override string ExtendedDes()
        {
            return base.ExtendedDes().Replace("&a", this.mainbuff .sumDamage.ToString());
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (  this.frame1 > 19 || this.frame1 <0)
            {
                this.frame1 = 0;
                if(this.mainbuff.sumDamage <= 0 || this.mainbuff.DestroyBuff)
                {
                    this.SelfDestroy();
                    //this.MySkill.AllExtendeds.Remove(this);
                }
                else
                {
                    this.SkillBasePlus.Target_BaseDMG = this.mainbuff.sumDamage;
                }
            }
            this.frame1++;
        }
        public int frame1 = 0;
        public Bcl_aln_10 mainbuff=new Bcl_aln_10();
    }

    public class Sex_aln_11 : SkillExtended_AlinaShield
    {
        public override void Init()
        {
            base.Init();
        }

        public override void AddShieldPersent()
        {
            base.AddShieldPersent();
            base.SetShield(true, (int)(0.4 * this.BChar.GetStat.maxhp), false, 0);
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            if (this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == Targets[0]) != null)
            {
                this.BChar.MyTeam.ForceDraw(this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == Targets[0]));
            }
            else if (this.BChar.MyTeam.Skills_UsedDeck.Find((Skill a) => a.Master == Targets[0]) != null)
            {
                this.BChar.MyTeam.ForceDraw(this.BChar.MyTeam.Skills_UsedDeck.Find((Skill a) => a.Master == Targets[0]));
            }
            else
            {
                this.BChar.MyTeam.Draw(1);
            }
        }
    }
    public class Bcl_aln_11 : Buff//, IP_HPChange
    {
        public override void Init()
        {
            base.Init();
            
        }
        public override void BuffStat()
        {
            base.BuffStat();
            if(this.pflag)
            {
                this.PlusStat.dod = 30;
                this.PlusPerStat.Damage = 0;
            }
            else
            {
                this.PlusPerStat.Damage = 15;
                this.PlusStat.dod = 0;
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(!this.pflag)
            {
                if(this.BChar.BarrierHP<=0)
                {
                    this.pflag = true;
                    this.BuffStat();
                }
            }
        }
        public bool pflag = false;
    }
    public class Sex_aln_13 : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            //BattleSystem.instance.AllyTeam.Draw(2);
            if (BattleSystem.instance.EnemyTeam.AliveChars.Find(a => !a.BuffFind("B_PD_Alina_p", false)) == null)
            {
                BattleSystem.instance.AllyTeam.Draw(3);
            }
            else
            {
                BattleChar bc = BattleSystem.instance.AllyTeam.AliveChars_Vanish.Find(a => a.Info.KeyData == "PD_Alina");
                if (bc != null)
                {
                    foreach(BattleChar tar in BattleSystem.instance.EnemyTeam.AliveChars)
                    {
                        tar.BuffAdd("B_PD_Alina_p", bc, false, 0, false, -1, false);
                    }
                }
                BattleSystem.instance.AllyTeam.Draw(2);
            }


        }
    }

    public class SkillEn_PD_Alina_0 : Sex_ImproveClone//, IP_SkillUse_User_After
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return (MainSkill.TargetTypeKey == "enemy" || MainSkill.TargetTypeKey == "random_enemy" || MainSkill.TargetTypeKey == "all_onetarget" || MainSkill.TargetTypeKey == "all_enemy");
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            int num = 0;
            Buff buff =new Buff();
            foreach(BattleChar tar in Targets)
            {
                if (tar.BuffFind("B_PD_Alina_p", false))
                {
                    if(tar.BuffReturn("B_PD_Alina_p", false).StackNum>num)
                    {
                        buff = tar.BuffReturn("B_PD_Alina_p", false);
                        num = Math.Max(num,buff.StackNum);
                    }
                }
            }
            if(num>0)
            {

                    foreach(BattleChar bc in buff.BChar.MyTeam.AliveChars)
                    {
                        for(int i = 0;i<buff.StackNum &&( !bc.BuffFind("B_PD_Alina_p", false)|| bc.BuffReturn("B_PD_Alina_p", false).StackNum<num); i++)
                        {
                            bc.BuffAdd("B_PD_Alina_p", buff.StackInfo[num-1-i].UseState, false, 0, false, -1, false);
                        }
                    }
                
            }
        }
    }

        
    public class E_PD_Alina_0 : EquipBase,IP_CheckEquipWhite,IP_SomeOneDead,IP_BattleEndOutBattle
    {
        public override void Init()
        {
            base.Init();
            int num = 0;

            foreach (E_aln_rec rec in this.plist)
            {
                num += rec.plustime;
            }
            this.PlusPerStat.Damage = 10+5*num;
            this.PlusPerStat.MaxHP = 20;
            this.PlusStat.dod = 20 + 5 * num;
            this.PlusStat.def = 5 * num;
        }
        public bool CheckEquipWhite()
        {
            return true;
        }
        
        public void SomeOneDead(BattleChar DeadChar)
        {
            if(DeadChar!=this.BChar && !DeadChar.Info.Ally)
            {
                int num = 1;
                if (DeadChar.BuffFind("B_PD_Alina_p", false))
                {
                    num += DeadChar.BuffReturn("B_PD_Alina_p", false).StackNum;

                }
                BattleSystem.DelayInputAfter(this.AddRec(num, 3));
            }
        }
        public IEnumerator AddRec(int num = 0, int time = 3)
        {
            if (num > 0)
            {
                this.plist.Add(new E_aln_rec { plustime = num, remaintime = time });
                this.Init();
            }
            yield break;
        }


        public override void TurnUpdate()
        {
            base.TurnUpdate();
            foreach (E_aln_rec rec in this.plist)
            {
                rec.remaintime--;
            }
            this.plist.RemoveAll(rec => rec.remaintime <= 0);
            this.Init();
        }

        public void BattleEndOutBattle()
        {
            this.plist = new List<E_aln_rec>();
            this.Init();
        }
        private List<E_aln_rec> plist = new List<E_aln_rec>();
        
    }
    public class E_aln_rec
    {
        public int plustime = 0;
        public int remaintime = 0;
    }

    public class E_PD_Alina_1 : EquipBase,IP_Hit,IP_Dodge,IP_DamageChange,IP_DamageTakeChange
    {
        public override void Init()
        {
            base.Init();

            this.PlusPerStat.MaxHP = 15;
            this.PlusStat.dod = 15;
            this.PlusStat.def = 15;
        }
        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            if(!SP.UseStatus.Info.Ally  && SP.SkillData.IsDamage )
            {
                this.Record(SP.UseStatus);
            }
        }
        public void Dodge(BattleChar Char, SkillParticle SP)
        {
            if (Char==this.BChar && !SP.UseStatus.Info.Ally && SP.SkillData.IsDamage)
            {
                
                this.Record(SP.UseStatus);
            }
        }
        public void Record(BattleChar bc)
        {
            rec_E_2 rec=new rec_E_2();  
            rec.bc = bc;
            rec.turn = BattleSystem.instance.TurnNum;
            this.recs.Add(rec);

            for (int i = 0; i < this.recs.Count; i++)
            {
                if (!BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Contains( recs[i].bc))
                {
                    recs.RemoveAt(i);
                    i--;
                }
            }
        }
        public int Check(BattleChar bc)
        {
            int i = 0;
            foreach(rec_E_2 rec in this.recs)
            {
                if(rec.bc == bc && rec.turn>BattleSystem.instance.TurnNum-2)
                {
                    i++;
                }
            }
            return i;
        }
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if(SkillD.Master==this.BChar)
            {
                int num=this.Check(Target);
                if(num>0)
                {
                    int dmg2 = (int)((1 + 0.15f * num) * Damage);
                    return dmg2;
                }
            }
            return Damage;
        }
        public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {
            //Debug.Log("测试点1");
            if (Hit == this.BChar)
            {
                //Debug.Log("测试点1");
                int num = this.Check(User);
                if (num > 0)
                {
                    //Debug.Log("测试点1");
                    int dmg2 = (int)((1 - 0.15f * num) * Dmg);
                    return dmg2;
                }
            }
            return Dmg;
        }
        [XmlIgnore]
        public List<rec_E_2> recs = new List<rec_E_2>();
    }
    public class rec_E_2
    {
        public BattleChar bc = null;
        public int turn = 0;
    }
}
