using ChronoArkMod.Plugin;
using GameDataEditor;
using I2.Loc;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using ChronoArkMod;
using DarkTonic.MasterAudio;
using ChronoArkMod.ModData.Settings;
using System.Linq.Expressions;
using DG.Tweening;
using SF_Standard;


namespace SF_Intruder
{
    [PluginConfig("M200_SF_Intruder_CharMod", "SF_Intruder_CharMod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class SF_Intruder_CharacterModPlugin : ChronoArkPlugin
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
        public class IntruderDef : ModDefinition
        {
        }


        [HarmonyPatch(typeof(BuffObject))]
        private class BuffObjectUpdatePlugin
        {
            // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
            [HarmonyPatch("Update")]
            [HarmonyPrefix]
            public static bool BuffObjectUpdate_Patch(BuffObject __instance)
            {
                
                if(__instance.MyBuff!=null && __instance.MyBuff.BuffData.Key=="B_SF_Intruder_p")
                {
                    __instance.StackText.text = (((__instance.MyBuff)as Bcl_int_p).Hack).ToString();
                    return false;
                }
                return true;
            }
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
            //MasterAudio.PlaySound("A_int_4", 1f, null, 0f, null, null, false, false);
            float volume = 0f;
            float volume2 = 0f;
            if (ModManager.getModInfo("SF_Intruder").GetSetting<ToggleSetting>("AudioOn").Value)
            {
                volume = ModManager.getModInfo("SF_Intruder").GetSetting<SliderSetting>("AudioVolume").Value;
            }
            else
            {
                volume = 0f;
            }

            if (inText.Contains("T_int_1_1"))
            {
                inText = inText.Replace("T_int_1_1:", string.Empty);
                MasterAudio.PlaySound("A_int_1_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_int_1_2"))
            {
                inText = inText.Replace("T_int_1_2:", string.Empty);
                MasterAudio.PlaySound("A_int_1_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_int_2_1"))
            {
                inText = inText.Replace("T_int_2_1:", string.Empty);
                MasterAudio.PlaySound("A_int_2_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_int_3_1"))
            {
                inText = inText.Replace("T_int_3_1:", string.Empty);
                MasterAudio.PlaySound("A_int_3_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_int_4_1"))
            {
                inText = inText.Replace("T_int_4_1:", string.Empty);
                MasterAudio.PlaySound("A_int_4_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_int_4_2"))
            {
                inText = inText.Replace("T_int_4_2:", string.Empty);
                MasterAudio.PlaySound("A_int_4_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_int_5_1"))
            {
                inText = inText.Replace("T_int_5_1:", string.Empty);
                MasterAudio.PlaySound("A_int_5_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_int_5_2"))
            {
                inText = inText.Replace("T_int_5_2:", string.Empty);
                MasterAudio.PlaySound("A_int_5_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_int_6_1"))
            {
                inText = inText.Replace("T_int_6_1:", string.Empty);
                MasterAudio.PlaySound("A_int_6_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_int_6_2"))
            {
                inText = inText.Replace("T_int_6_2:", string.Empty);
                MasterAudio.PlaySound("A_int_6_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_int_7_1"))
            {
                inText = inText.Replace("T_int_7_1:", string.Empty);
                MasterAudio.PlaySound("A_int_7_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_int_7_2"))
            {
                inText = inText.Replace("T_int_7_2:", string.Empty);
                MasterAudio.PlaySound("A_int_7_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_int_8_1"))
            {
                inText = inText.Replace("T_int_8_1:", string.Empty);
                MasterAudio.PlaySound("A_int_8_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_int_9_1"))
            {
                inText = inText.Replace("T_int_9_1:", string.Empty);
                MasterAudio.PlaySound("A_int_9_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_int_10_1"))
            {
                inText = inText.Replace("T_int_10_1:", string.Empty);
                MasterAudio.PlaySound("A_int_10_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_int_11_1"))
            {
                inText = inText.Replace("T_int_11_1:", string.Empty);
                MasterAudio.PlaySound("A_int_11_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_int_11_2"))
            {
                inText = inText.Replace("T_int_11_2:", string.Empty);
                MasterAudio.PlaySound("A_int_11_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_int_11_3"))
            {
                inText = inText.Replace("T_int_11_3:", string.Empty);
                MasterAudio.PlaySound("A_int_11_3", volume, null, 0f, null, null, false, false);
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

            if (CharId == "SF_Intruder")
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



    [HarmonyPatch(typeof(Buff))]
    [HarmonyPatch("set_PartyBarrierHP")]
    public static class set_PartyBarrierHPPlugin
    {
        [HarmonyPrefix]
        public static void set_PartyBarrierHP_Patch( Buff __instance, ref int value)
        {

            //Debug.Log("编制保护检查点1");
            foreach (IP_Set_PartyBarrierHP ip_patch in __instance.BChar.IReturn<IP_Set_PartyBarrierHP>())
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    ip_patch.Set_PartyBarrierHP(__instance, ref value);
                }
            }

            //Debug.Log("编制保护检查点2");
        }

    }
    public interface IP_Set_PartyBarrierHP
    {
        // Token: 0x06000001 RID: 1
        void Set_PartyBarrierHP(Buff buff, ref int num);
    }







    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("BuffAdd")]
    public static class BuffAddPlugin
    {
        [HarmonyPrefix]
        public static void BuffAdd_Before(BattleChar __instance, string key, BattleChar UseState, bool hide, int PlusTagPer, bool debuffnonuser , int RemainTime , bool StringHide)
        {

            //Debug.Log("BuffAdd检查点1");
            foreach (IP_BuffAdd_Before ip_patch in __instance.IReturn<IP_BuffAdd_Before>())
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    //Debug.Log("BuffAdd检查点2");
                    ip_patch.BuffAdd_Before(__instance, key,UseState,hide,PlusTagPer,debuffnonuser,RemainTime,StringHide);
                }
            }

            //Debug.Log("BuffAdd检查点3");
        }

    }
    public interface IP_BuffAdd_Before
    {
        // Token: 0x06000001 RID: 1
        void BuffAdd_Before(BattleChar BC, string key, BattleChar UseState, bool hide, int PlusTagPer, bool debuffnonuser , int RemainTime , bool StringHide );
    }

    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("OnlyDamage")]
    public static class RecoveryPlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void BattleChar_OnlyDamage_Before_patch(BattleChar __instance, ref int Dmg, ref bool Weak, bool IgnoreHealPro, BattleChar User , bool Cri)
        {
            if (__instance.Info.Ally)
            {
                foreach (IP_OnlyDamage_Before ip_before in __instance.IReturn<IP_OnlyDamage_Before>())
                {
                    bool flag = ip_before != null;
                    if (flag)
                    {
                        ip_before.OnlyDamage_Before(__instance,ref Dmg,ref Weak);
                    }
                }
                //Debug.Log("成功进入OnlyDamage前的patch");
            }

        }
        //private int recBefore;
    }
    public interface IP_OnlyDamage_Before
    {
        // Token: 0x06000001 RID: 1
        void OnlyDamage_Before(BattleChar battleChar, ref int Dmg, ref bool Weak);
    }

    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("Dead")]
    public static class DeadPlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void Dead_after_patch(BattleChar __instance, bool notdeadeffect, bool NoTimeSlow)
        {
            //Debug.Log("Dead_Patch");
            if (__instance.Info.KeyData != GDEItemKeys.Enemy_ProgramMaster2 && !__instance.IsDead)
            {
                if (__instance.Info.Ally)
                {
                    DeadAllySkill newdead = new DeadAllySkill();
                    newdead.DeadAlly = __instance;

                    foreach (Skill sk in BattleSystem.instance.AllyTeam.Skills)
                    {
                        if (sk.Master == __instance)
                        {
                            //Debug.Log("记录技能：" + sk.MySkill.Name);
                            newdead.DeadSkill.Add(sk.CloneSkill(true, null, null, false));
                        }

                    }
                    foreach (Skill sk in BattleSystem.instance.AllyTeam.Skills_Deck)
                    {
                        if (sk.Master == __instance)
                        {
                            //Debug.Log("记录技能：" + sk.MySkill.Name);
                            newdead.DeadSkill.Add(sk.CloneSkill(true, null, null, false));
                        }
                    }
                    foreach (Skill sk in BattleSystem.instance.AllyTeam.Skills_UsedDeck)
                    {
                        if (sk.Master == __instance)
                        {
                            //Debug.Log("记录技能：" + sk.MySkill.Name);
                            newdead.DeadSkill.Add(sk.CloneSkill(true, null, null, false));
                        }
                    }

                    if (DeadPlugin.DeadList.Find(a => a.DeadAlly == newdead.DeadAlly) != null)
                    {
                        int ind = DeadPlugin.DeadList.FindIndex(a => a.DeadAlly == newdead.DeadAlly);
                        DeadPlugin.DeadList.RemoveAt(ind);

                    }
                    DeadPlugin.DeadList.Add(newdead);

                }

            }
        }
        public static List<DeadAllySkill> DeadList = new List<DeadAllySkill>();
        public static List<int> PartyHP = new List<int>();
        public static List<bool> PartyAlive = new List<bool>();

    }

    [HarmonyPatch(typeof(BattleSystem))]
    [HarmonyPatch("BattleStart")]
    public static class BattleStartPlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void BattleStart_before_patch(BattleSystem __instance)
        {
            //Debug.Log("BattleStart_Patch");
            DeadPlugin.DeadList=new List<DeadAllySkill>();
            DeadPlugin.PartyAlive.Clear();
            DeadPlugin.PartyHP.Clear();
            foreach (Character character in PlayData.TSavedata.Party)
            {
                DeadPlugin.PartyAlive.Add(character.Incapacitated);
                DeadPlugin.PartyHP.Add(character.Hp);
            }
        }
    }

    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("Heal")]
    [HarmonyPatch(new Type[] { typeof(BattleChar), typeof(float), typeof(bool), typeof(bool), typeof(BattleChar.ChineHeal) })]
    public static class HealPlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void Heal_before_patch(BattleChar __instance, BattleChar User, float Hel, bool Cri, ref bool Force , BattleChar.ChineHeal heal)
        {
            //Debug.Log("BattleStart_Patch");
            foreach (IP_Heal_Before ip_before in User.IReturn<IP_Heal_Before>())
            {
                bool flag = ip_before != null;
                if (flag)
                {
                    ip_before.Heal_Before(__instance, User,  Hel,  Cri, ref  Force,  heal);
                }
            }
        }
    }
    public interface IP_Heal_Before
    {
        // Token: 0x06000001 RID: 1
        void Heal_Before(BattleChar Taker, BattleChar User, float Hel, bool Cri, ref bool Force, BattleChar.ChineHeal heal);
    }


    [HarmonyPatch(typeof(CharEquipInven))]
    [HarmonyPatch("OnDropSlot")]
    public static class CharEquipInvenPlugin
    {
        // Token: 0x06000015 RID: 21 RVA: 0x00002410 File Offset: 0x00000610
        [HarmonyPostfix]
        public static void CharEquipInven_OnDropSlot_patch(ItemBase inputitem, ref bool __result, CharEquipInven __instance)
        {
            bool flag = inputitem.itemkey == "E_SF_Intruder_0" && __instance.Info.GetData.Key != "SF_Intruder";
            if (flag)
            {
                __result = true;
            }
            //Debug.Log("成功进入OnDropSlot的patch");
        }
    }



    
    [HarmonyPatch(typeof(BattleSystem))]
    [HarmonyPatch("IsSelect")]
    public static class IsSelectPlugin
    {


        // Token: 0x06000015 RID: 21 RVA: 0x00002410 File Offset: 0x00000610
        [HarmonyPostfix]
        public static void IsSelect_Patch(ref bool __result, BattleSystem __instance, BattleChar Target, Skill skill)
        {
            string targetTypeKey = skill.TargetTypeKey;
            bool ally = skill.Master.Info.Ally;

            bool case_S_int_9 = skill.MySkill.KeyID=="S_SF_Intruder_9";
            //bool antiVanishCase2 = Target.BuffFind("B_drm_4",false) && skill.Master== Target.BuffReturn("B_drm_4", false).Usestate_L;
            bool caseall = case_S_int_9;
            if (caseall)
            {
                if (Target.GetStat.Vanish)
                {
                    __result=false;
                }
                else
                {
                    bool flag = false;
                    if (targetTypeKey == GDEItemKeys.s_targettype_all_enemy && ally != Target.Info.Ally)
                    {
                        flag = true;
                    }
                    if (targetTypeKey == GDEItemKeys.s_targettype_all_ally && ally == Target.Info.Ally)
                    {
                        flag = true;
                    }
                    if ((targetTypeKey == GDEItemKeys.s_targettype_enemy || targetTypeKey == GDEItemKeys.s_targettype_enemy_PlusRandom) && ally != Target.Info.Ally)
                    {
                        flag = BattleSystem_int.TargetSelectCheck_int(Target, skill, flag);
                    }
                    if (targetTypeKey == GDEItemKeys.s_targettype_ally && ally == Target.Info.Ally)
                    {
                        flag = true;
                    }
                    if (targetTypeKey == GDEItemKeys.s_targettype_self && Target.Info == skill.Master.Info)
                    {
                        flag = true;
                    }
                    if (targetTypeKey == GDEItemKeys.s_targettype_random_enemy && ally != Target.Info.Ally)
                    {
                        flag = true;
                    }
                    if (targetTypeKey == GDEItemKeys.s_targettype_otherally && Target != skill.Master && ally == Target.Info.Ally)
                    {
                        flag = true;
                    }
                    if (targetTypeKey == GDEItemKeys.s_targettype_all_onetarget)
                    {
                        flag = (ally == Target.Info.Ally || BattleSystem_int.TargetSelectCheck_int(Target, skill, flag));
                    }
                    if (targetTypeKey == GDEItemKeys.s_targettype_all)
                    {
                        flag = true;
                    }
                    if (targetTypeKey == GDEItemKeys.s_targettype_deathally && Target != skill.Master && Target.Info.Incapacitated)
                    {
                        flag = true;
                    }
                    else if (Target.IsDead && !case_S_int_9)
                    {
                        if(case_S_int_9)
                        {

                        }
                        else
                        {
                            flag = false;
                        }

                    }
                    if (flag)
                    {
                        using (List<Skill_Extended>.Enumerator enumerator = skill.AllExtendeds.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                if (enumerator.Current.TargetSelectExcept(Target))
                                {
                                    flag = false;
                                }
                            }
                        }
                    }
                    __result = flag;
                }
                
            }
        }
    }
    
    
    [HarmonyPatch(typeof(BattleSystem))]
    [HarmonyPatch("SkillTargetReturn")]
    [HarmonyPatch(new Type[] { typeof(Skill), typeof(BattleChar), typeof(BattleChar) })]
    public static class SkillTargetReturnPlugin
    {
        // Token: 0x06000015 RID: 21 RVA: 0x00002410 File Offset: 0x00000610
        [HarmonyPostfix]
        public static void SkillTargetReturn_Patch(ref List<BattleChar> __result, BattleSystem __instance, Skill sk, BattleChar Target, BattleChar PlusTarget = null)
        {
            bool case_S_int_9 = sk.MySkill.KeyID == "S_SF_Intruder_9";
            bool caseall = case_S_int_9;
            if (caseall)
            {

                List<BattleChar> list = new List<BattleChar>();
                if (sk.MySkill.Target.Key == GDEItemKeys.s_targettype_all_enemy)
                {
                    if (sk.Master.Info.Ally)
                    {
                        using (List<BattleEnemy>.Enumerator enumerator = __instance.EnemyList.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                BattleEnemy item = enumerator.Current;
                                list.Add(item);
                            }
                            goto IL_264;
                        }
                    }
                    using (List<BattleAlly>.Enumerator enumerator2 = __instance.AllyList.GetEnumerator())
                    {
                        while (enumerator2.MoveNext())
                        {
                            BattleAlly item2 = enumerator2.Current;
                            list.Add(item2);
                        }
                        goto IL_264;
                    }
                }
                if (sk.MySkill.Target.Key == GDEItemKeys.s_targettype_all_ally)
                {
                    if (!sk.Master.Info.Ally)
                    {
                        using (List<BattleEnemy>.Enumerator enumerator = __instance.EnemyList.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                BattleEnemy item3 = enumerator.Current;
                                list.Add(item3);
                            }
                            goto IL_264;
                        }
                    }
                    using (List<BattleAlly>.Enumerator enumerator2 = __instance.AllyList.GetEnumerator())
                    {
                        while (enumerator2.MoveNext())
                        {
                            BattleAlly item4 = enumerator2.Current;
                            list.Add(item4);
                        }
                        goto IL_264;
                    }
                }
                if (sk.MySkill.Target.Key == GDEItemKeys.s_targettype_all)
                {
                    foreach (BattleAlly item5 in __instance.AllyList)
                    {
                        list.Add(item5);
                    }
                    using (List<BattleEnemy>.Enumerator enumerator = __instance.EnemyList.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            BattleEnemy item6 = enumerator.Current;
                            list.Add(item6);
                        }
                        goto IL_264;
                    }
                }
                if (sk.MySkill.Target.Key == GDEItemKeys.s_targettype_random_enemy)
                {
                    if (sk.Master.Info.Ally)
                    {
                        list.Add(__instance.EnemyList.Random(sk.Master.GetRandomClass().Target));
                    }
                    else
                    {
                        list.Add(__instance.AllyList.Random(sk.Master.GetRandomClass().Target));
                    }
                }
                else if (!Target.IsDead)
                {
                    list.Add(Target);
                }
                else if(case_S_int_9)
                {
                    list.Add(Target);
                }
            IL_264:
                if (sk.MySkill.Target.Key == GDEItemKeys.s_targettype_enemy_PlusRandom)
                {
                    if (PlusTarget != null && !PlusTarget.IsDead)
                    {
                        list.Add(PlusTarget);
                    }
                    else
                    {
                        List<BattleChar> list2 = new List<BattleChar>();
                        if (sk.Master.Info.Ally)
                        {
                            foreach (BattleEnemy item7 in __instance.EnemyList)
                            {
                                list2.Add(item7);
                            }
                            list2.Remove(list[0]);
                            if (list2.Count != 0)
                            {
                                list.Add(list2.Random(sk.Master.GetRandomClass().Target));
                            }
                        }
                        else
                        {
                            foreach (BattleAlly item8 in __instance.AllyList)
                            {
                                list2.Add(item8);
                            }
                            list2.Remove(list[0]);
                            if (list2.Count != 0)
                            {
                                list.Add(list2.Random(sk.Master.GetRandomClass().Target));
                            }
                        }
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].GetStat.Vanish)
                    {
                        list.RemoveAt(i);
                        i--;
                    }
                }


                    __result = list;
                

            }
        }
    }

    
    public class BattleSystem_int : MonoBehaviour
    {
        public static bool TargetSelectCheck_int(BattleChar Target, Skill skill, bool selected)
        {
            if (skill.ForceAction)
            {
                selected = true;
            }
            else if (Target is BattleEnemy)
            {
                bool flag = false;
                foreach (IP_ForceEnemyTargetSelect ip_ForceEnemyTargetSelect in skill.IReturn<IP_ForceEnemyTargetSelect>())
                {
                    if (ip_ForceEnemyTargetSelect != null)
                    {
                        flag = true;
                        if (ip_ForceEnemyTargetSelect.ForceEnemyTargetSelect(skill, Target as BattleEnemy))
                        {
                            selected = true;
                            break;
                        }
                        break;
                    }
                }
                if (!flag)
                {
                    foreach (IP_SpecialEnemyTargetSelect ip_SpecialEnemyTargetSelect in skill.Master.IReturn<IP_SpecialEnemyTargetSelect>(skill))
                    {
                        if (ip_SpecialEnemyTargetSelect != null && ip_SpecialEnemyTargetSelect.SpecialEnemyTargetSelect(skill, Target as BattleEnemy))
                        {
                            selected = true;
                        }
                    }
                    if (!selected)
                    {
                        bool flag2 = false;
                        using (List<Skill_Extended>.Enumerator enumerator3 = skill.AllExtendeds.GetEnumerator())
                        {
                            while (enumerator3.MoveNext())
                            {
                                if (enumerator3.Current.CanIgnoreTauntTarget(Target))
                                {
                                    flag2 = true;
                                    break;
                                }
                            }
                        }
                        if (flag2 || skill.IgnoreTaunt || !(Target as BattleEnemy).TauntNoTarget || Target.GetStat.IgnoreTaunt_EnemySelf)
                        {
                            selected = true;
                        }
                    }
                }
            }
            return selected;
        }
    }



    public class P_Intruder : Passive_Char, IP_SkillUse_Target, IP_EnemyAwake,IP_LevelUp
    {
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
                list.Add(ItemBase.GetItem("E_SF_Intruder_0", 1));
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
        public void EnemyAwake(BattleChar Enemy)
        {
            Enemy.BuffAdd("B_SF_Intruder_p", this.BChar, false, 0, false, -1, false);
        }
        public override void Init()
        {
            base.Init();
            this.OnePassive = true;
        }
        //public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        //{
            //Debug.Log("目标数量:"+Targets.Count);
            //if(!SkillD.BasicSkill)
            //{
                //this.IntruderPassive(SkillD,Targets);
            //}
        //}
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            //Debug.Log("目标数量:" + SP.TargetChar.Count);

                this.IntruderPassive(SP.SkillData, hit);
            
        }
        public void IntruderPassive(Skill skill, BattleChar BC)
        {
            //foreach (BattleChar BC in Targets)
            //{
                //if (BC.Info.Ally)
                //{
                //tar = this.BChar.BattleInfo.EnemyList.Random(this.BChar.GetRandomClass().Main);
                //}
                //else
                //{
                //tar = BC;
                //
                //}
                if (!BC.Info.Ally)
                {
                    if (!BC.BuffFind("B_SF_Intruder_p", false) || BC.BuffReturn("B_SF_Intruder_p", false).Usestate_L != this.BChar)
                    {
                        BC.BuffAdd("B_SF_Intruder_p", this.BChar, false, 0, false, -1, false);
                    }

                    Bcl_int_p bcl_p = BC.BuffReturn("B_SF_Intruder_p", false) as Bcl_int_p;
                    bcl_p.HackAdd(1, true);
                }

                
            //}
        }
    }

    public class Bcl_int_p : InBuff, IP_BuffAddAfter//,IP_BuffUpdate
    {

        public override string DescExtended()
        {
            //string text = "";
            //if (this.stunturn > 0)
            //{
            //text = "无法行动(" + this.stunturn + "回合)\n";
            //}
            int num1 = 110 + (int)(this.Usestate_L.GetStat.HIT_CC);
            int num2 = 40 * this.stunplus ;
            string str=string.Empty;
            if(this.stunplus > 0)
            {
                str = num1.ToString() + "%+" + num2.ToString();
            }
            else
            {
                str = num1.ToString();
            }
            return base.DescExtended().Replace("&a", this.hack.ToString()).Replace("&b", this.maxNum.ToString()).Replace("&c",str);
        }
        public override void TurnUpdate()
        {
            int debuffnum = 0;
            foreach(Buff buff in this.BChar.Buffs)
            {
                if(!buff.IsHide && buff.BuffData.Debuff)
                {
                    debuffnum++;
                }
            }

            //if(this.stunturn>0)
            //{
                //this.stunturn--;
            //}

            //for(int i = 1;i<=debuffnum;i++)
            //{
            //this.BChar.BuffAdd("B_SF_Intruder_p", this.BChar, false, 0, false, -1, false);
            //}
            this.HackAdd(debuffnum, true);
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if(!addedbuff.IsHide && BuffTaker == this.BChar  && addedbuff.BuffData.Debuff && addedbuff.BuffData.Key!=this.BuffData.Key && addedbuff.BuffData.Key != "B_SF_Intruder_p_stun")
            {
                //Debug.Log("test point 1");
                //this.BChar.BuffAdd("B_SF_Intruder_p", this.BChar, false, 0, false, -1, false);
                this.HackAdd(1,true);
            }
        }

        public void HackAdd(int addNum,bool actIP)
        {



            if(actIP)
            {
                foreach (IP_HackAdd_Before ip_patch in BattleSystem.instance.IReturn<IP_HackAdd_Before>())
                {
                    //Debug.Log("IP测试1");
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.HackAdd_Before(this.BChar, this.Usestate_L , this, ref addNum);
                    }
                }
            }
            this.hack += addNum;
            this.Update();

        }

        public void Update() 
        {


            if ( this.hack != this.typelist.Count)
            {
                if(this.hack>this.typelist.Count)
                {
                    //BattleSystem.DelayInputAfter(this.HackText(this.hack - this.typelist.Count));

                    while (this.hack > this.typelist.Count)
                    {
                        this.typelist.Add(RandomManager.RandomInt(this.BChar.GetRandomClass().Main, 1, 5));
                    }
                }
                if (this.hack < this.typelist.Count)
                {
                    //BattleSystem.DelayInputAfter(this.HackText(this.hack - this.typelist.Count));

                    while (this.hack < this.typelist.Count)
                    {
                        if(this.recordon)
                        {
                            this.historylist.Add(this.typelist[this.typelist.Count - 1]);
                        }
                        this.typelist.RemoveAt(this.typelist.Count-1);
                    }
                }
                this.PlusPerStat.Damage = -3 * (this.typelist.FindAll(a=>a==1).Count) ;
                this.PlusStat.def = -3 * (this.typelist.FindAll(a => a == 2).Count);
                this.PlusStat.hit = -3 * (this.typelist.FindAll(a => a == 3).Count);
                this.PlusStat.dod = -3 * (this.typelist.FindAll(a => a == 4).Count);

                if(this.historylist.Count>0)
                {
                    this.PlusPerStat.Damage -= (int)(1.5 * (this.historylist.FindAll(a => a == 1).Count));
                    this.PlusStat.def -= (int)(1.5 * (this.historylist.FindAll(a => a == 2).Count));
                    this.PlusStat.hit -= (int)(1.5 * (this.historylist.FindAll(a => a == 3).Count));
                    this.PlusStat.dod -= (int)(1.5 * (this.historylist.FindAll(a => a == 4).Count));
                }
            }

            if (this.hack >= this.maxNum)
            {
                //this.SelfDestroy();
                //this.hack -= this.maxNum;
                //this.Hack = 0;
                //int n = (int)(this.hack / this.maxNum);

                this.HackAdd(-this.maxNum, true);

                //for (int i = 0; i < n; i++)
                //{
                    //BattleSystem.DelayInput(this.AddStun());
                    if(this.BChar.BuffCheck.Lastbuff== "B_SF_Intruder_p_stun" )
                    {
                        this.BChar.BuffCheck.Lastbuff = string.Empty;
                    }
                    this.BChar.BuffAdd("B_SF_Intruder_p_stun", this.Usestate_L, false, 40 * this.stunplus, false, -1, false);


                //}

                //int stuntime = 0;
                //if(this.BChar.BuffFind("B_SF_Intruder_p_stun",false))
                //{
                //stuntime = this.BChar.BuffReturn("B_SF_Intruder_p_stun", false).StackInfo[0].RemainTime;
                //}
                //if(!this.BChar.BuffFind("B_SF_Intruder_p_stun", false)|| this.BChar.BuffReturn("B_SF_Intruder_p_stun", false).StackInfo[0].RemainTime<=stuntime)
                //{
                //this.HackAdd((int)(0.5*this.maxNum), true);
                //this.stunplus++;
                //}
            }


            //if(this.stunturn>0)
            //{
                //this.PlusStat.Stun = true;
            //}
            //else
            //{
                //this.PlusStat.Stun = false;
            //}
            //if(this.PlusStat.Stun)
            //{
                //if(!this.BChar.BuffFind("B_SF_Intruder_p_stun",false))
                //{
                    //this.BChar.BuffAdd("B_SF_Intruder_p_stun", this.Usestate_L, false, 999, true, -1, false);

                    //GDEBuffData gdebuffData = new GDEBuffData("B_SF_Intruder_p_stun");
                    //Buff buff = Buff.DataToBuff(gdebuffData, this.BChar, this.Usestate_L, 0, false, 0);
                    //BattleSystem.DelayInputAfter(BattleSystem.instance.BuffAddDelay(buff, false));
                //}
            //}
            //else
            //{
                //if (this.BChar.BuffFind("B_SF_Intruder_p_stun", false))
                //{
                    //this.BChar.BuffReturn("B_SF_Intruder_p_stun", false).SelfDestroy();
                //}
            //}

            //this.recordon = false;
            //yield break;
        }


        //public IEnumerator HackText(int num)//输出文字，因出现太频繁停用
        //{
            //GameObject gameObject = Misc.UIInst(BattleSystem.instance.BuffView);
            //if (this.BChar.Info.Ally)
            //{
                //gameObject.transform.position = this.BChar.GetPos();
            //}
            //else
            //{
                //gameObject.transform.position = this.BChar.GetTopPos();
            //}
            //BuffView buffView = gameObject.GetComponent<BuffView>();
            //buffView.transform.localScale = new Vector3(1f, 1f, 1f);
            //buffView.transform.localRotation = Quaternion.identity;
            //if (!string.IsNullOrEmpty(this.BuffData.Icon_Path))
            //{
                //buffView.Icon.sprite = AddressableLoadManager.LoadAsyncCompletion<Sprite>(this.BuffData.Icon_Path, AddressableLoadManager.ManageType.Stage);
                //buffView.BuffEffectIcon.sprite = buffView.Icon.sprite;
            //}
            //if (this.BE == null )
            //{
                //buffView.BuffEffectIcon.gameObject.SetActive(true);
            //}
            //buffView.Name.text = this.BuffData.Name;
            //if(num > 0 )
            //{
                //buffView.Name.text = buffView.Name.text + " +"+num.ToString();
            //}
            //else
            //{
                //buffView.Name.text = buffView.Name.text + " " + num.ToString();
            //}
            //buffView.transform.SetParent(GameObject.FindGameObjectWithTag("3DUICanvas").transform);
            //buffView.transform.localScale = new Vector3(1f, 1f, 1f);
            //buffView.gameObject.SetActive(false);
            //gameObject.SetActive(true);
            //yield return new WaitForSeconds(0.5f);
            //yield break;
        //}


        public int Hack
        {
            get
            {
                return this.hack;
            }
            set
            {
                int num=value-this.hack;
                this.HackAdd(num,true);
            }
        }
        private List<int> typelist = new List<int>();
        private int hack = 0;
        public int maxNum = 20;

        public bool recordon;
        private List<int> historylist = new List<int>();

        private int stunturn = 0;
        public int stunplus = 0;
    }
    public interface IP_HackAdd_Before
    {
        // Token: 0x06000001 RID: 1
        void HackAdd_Before(BattleChar taker, BattleChar user, Buff buff,ref int addNum);
    }

    public class Bcl_int_p_stun : Buff,IP_BuffAdd,IP_BuffAddAfter, IP_DebuffResist
    {

        public override void Init()
        {
            this.PlusStat.Stun = true;
            //this.NoShowTimeNum_Tooltip = true;
        }
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {
            if (BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key)
            {
                this.old = true;
                this.oldremain= this.StackInfo[this.StackNum - 1].RemainTime;
                //addedbuff.StackInfo[0].RemainTime += this.StackInfo[this.StackNum - 1].RemainTime;
            }
        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (this.old && BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key)
            {
                stackBuff.RemainTime += this.oldremain;

                this.old = false;
                this.oldremain =0;
                //addedbuff.StackInfo[0].RemainTime += this.StackInfo[this.StackNum - 1].RemainTime;


            }
            if (BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key && !this.BChar.IsDead)
            {
                if (this.BChar.BuffFind("B_SF_Intruder_p", false))
                {
                    //Debug.Log("被瘫痪");
                    Bcl_int_p buffp = this.BChar.BuffReturn("B_SF_Intruder_p", false) as Bcl_int_p;
                    buffp.stunplus = 0;
                }
            }
        }
        public override void SelfdestroyPlus()
        {
            this.BChar.BuffAdd(GDEItemKeys.Buff_B_Common_CCRsis, this.BChar, false, 0, false, -1, false);
            this.BChar.BuffAdd(GDEItemKeys.Buff_B_Common_CCRsis, this.BChar, false, 0, false, -1, false);
            this.BChar.BuffAdd(GDEItemKeys.Buff_B_Common_CCRsis, this.BChar, false, 0, false, -1, false);
            this.BChar.BuffAdd(GDEItemKeys.Buff_B_Common_CCRsis, this.BChar, false, 0, false, -1, false);
            this.BChar.BuffAdd(GDEItemKeys.Buff_B_Common_CCRsis, this.BChar, false, 0, false, -1, false);
            this.BChar.BuffAdd(GDEItemKeys.Buff_B_Common_CCRsis, this.BChar, false, 0, false, -1, false);
        }
        public void Resist()
        {
            //Debug.Log("瘫痪被抵抗");
            if (!this.BChar.IsDead)
            {
                if(this.BChar.BuffFind("B_SF_Intruder_p", false))
                {
                    Bcl_int_p buffp = this.BChar.BuffReturn("B_SF_Intruder_p", false) as Bcl_int_p;
                    buffp.stunplus++;
                    /*
                    buffp.HackAdd((int)(0.5*buffp.maxNum), true);
                */
                }
            }
        }
        private bool old;
        private int oldremain;
    }




    public class Sex_int_1 : Sex_ImproveClone
    {
        
        public override void Special_PointerEnter(BattleChar Char)
        {
            base.Special_PointerEnter(Char);
            if (Char is BattleEnemy)
            {
                this.IsDamage = true;
            }
        }
        public override void Special_PointerExit()
        {
            if(!this.effecting)
            {
                this.IsDamage = false;
            }
        }
        
        public override void Init()
        {
            base.Init();
            this.SkillBasePlus.Target_BaseDMG = 0;
            this.PlusSkillPerStat.Heal = 0;
            //this.IsDamage = true;
            this.SkillBasePlusPreview.Target_BaseDMG = (int)((float)this.MySkill.TargetHeal * 1.2);

            if (this.TargetBuff.Find(a => a.BuffData.Key == "B_SF_Intruder_1_A") == null || this.TargetBuff.Find(a => a.BuffData.Key == "B_SF_Intruder_1_E") != null)
            {
                this.TargetBuff.RemoveAll(a => a.BuffData.Key == "B_SF_Intruder_1_A" || a.BuffData.Key == "B_SF_Intruder_1_E");

                GDEBuffData gde1 = new GDEBuffData("B_SF_Intruder_1_A");
                BuffTag btg1 = new BuffTag();
                btg1.User = this.BChar;
                btg1.BuffData = gde1;
                this.TargetBuff.Insert(0, btg1);
            }
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            this.effecting = true;
            this.IsDamage = false;
            this.SkillBasePlus.Target_BaseDMG = 0;
            this.PlusSkillPerStat.Heal = 0;
            if (!Targets[0].Info.Ally)
            {
                this.IsDamage = true;
                this.SkillBasePlus.Target_BaseDMG = (int)((float)SkillD.TargetHeal*1.2);
                this.PlusSkillPerStat.Heal = -99999;
                this.MySkill.UseOtherParticle_Path = "Particle/SilverStein/SilverStein_Shot";
                /*
                if (SkillD.MySkill.Effect_Target.Buffs.FindIndex(a => a.Key == "B_SF_Intruder_1_A") != -1)
                {
                    int ind = SkillD.MySkill.Effect_Target.Buffs.FindIndex(a => a.Key == "B_SF_Intruder_1_A");//取消技能自带的buff，然后等待重新释放，避免bug
                    SkillD.MySkill.Effect_Target.Buffs.RemoveAt(ind);
                    GDEBuffData toenemy= new GDEBuffData("B_SF_Intruder_1_E");
                    SkillD.MySkill.Effect_Target.Buffs.Add(toenemy);
                    base.SkillUseSingle(SkillD, Targets);
                }*/
                this.TargetBuff.RemoveAll(a => a.BuffData.Key == "B_SF_Intruder_1_A" );
                GDEBuffData gde1 = new GDEBuffData("B_SF_Intruder_1_E");
                BuffTag btg1 = new BuffTag();
                btg1.User = this.BChar;
                btg1.BuffData = gde1;
                this.TargetBuff.Insert(0, btg1);
            }
        }
        public bool effecting = false;
    }

    public class Bcl_int_1_A : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusPerStat.Damage = (int)(this.Usestate_F.GetStat.reg * 0.6f);
            this.PlusPerStat.Heal = (int)(this.Usestate_F.GetStat.reg * 0.6f);
            this.PlusStat.def = (int)(this.Usestate_F.GetStat.reg * 0.6f);
            this.PlusStat.hit = (int)(this.Usestate_F.GetStat.reg * 0.6f);
            this.PlusStat.dod = (int)(this.Usestate_F.GetStat.reg * 0.6f);
        }

    }
    public class Bcl_int_1_E : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusPerStat.Damage = -(int)(this.Usestate_F.GetStat.reg*0.6f);
            this.PlusPerStat.Heal = -(int)(this.Usestate_F.GetStat.reg * 0.6f);
            this.PlusStat.def = -(int)(this.Usestate_F.GetStat.reg * 0.6f);
            this.PlusStat.hit = -(int)(this.Usestate_F.GetStat.reg * 0.6f);
            this.PlusStat.dod = -(int)(this.Usestate_F.GetStat.reg * 0.6f);
        }


    }

    public class Sex_int_2 : Sex_ImproveClone, IP_SkillCastingStart,IP_LogUpdate
    {
        /*
        public override void HandInit()
        {
            base.HandInit();
            this.PlusSkillPerStat.Heal = 0;
            //this.attackPhase = 3;
        }
        */

        // Token: 0x060010CE RID: 4302 RVA: 0x0008B31F File Offset: 0x0008951F
        public override void Init()
        {
            base.Init();
            this.CountingExtedned = true;
        }
        public void SkillCasting(CastingSkill ThisSkill)
        {
            this.tar = ThisSkill.Target;
            List<Skill> logs = BattleSystem.instance.BattleLogs.getSkills((BattleLog log) => log.WhoUse == this.tar, (Skill s) => !s.FreeUse, BattleSystem.instance.TurnNum);
            int n = logs.Count;

                this.PlusSkillPerStat.Heal = (int)(this.MySkill.MySkill.Effect_Target.HEAL_Per * (Math.Pow(0.7, n) - 1));
            
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            List<Skill> logs = BattleSystem.instance.BattleLogs.getSkills((BattleLog log) => log.WhoUse == this.tar, (Skill s) => !s.FreeUse, BattleSystem.instance.TurnNum);
            this.deheal = logs.Count;
            this.PlusSkillPerStat.Heal = (int)(this.MySkill.MySkill.Effect_Target.HEAL_Per * (Math.Pow(0.7, this.deheal) - 1));
        }




        public void LogUpdate(BattleLog log)
        {
            /*
            if (this.MySkill.IsNowCounting && skill.Master == this.tar)
            {
                this.deheal++;
                this.PlusSkillPerStat.Heal = (int)(this.MySkill.MySkill.Effect_Target.HEAL_Per * (-deheal/3));

            }
            */
            /*
            List<BattleLog> logs = this.BChar.BattleInfo.BattleLogs.ReturnAllLogs();
            for (int i = logs.Count - 1; i >= 0; i--)
            {
                if (logs[i].Turn < BattleSystem.instance.TurnNum)
                {
                    break;
                }
                else if (logs[i].WhoUse == this.tar)
                {
                    this.deheal++;
                    this.PlusSkillPerStat.Heal = (int)(this.MySkill.MySkill.Effect_Target.HEAL_Per * (Math.Pow(0.65,this.deheal)-1));
                }
            }
            */
            if(this.MySkill.IsNowCasting)
            {
                List<Skill> logs = BattleSystem.instance.BattleLogs.getSkills((BattleLog log2) => log2.WhoUse == this.tar, (Skill s) => !s.FreeUse, BattleSystem.instance.TurnNum);
                int n = logs.Count;

                    this.PlusSkillPerStat.Heal = (int)(this.MySkill.MySkill.Effect_Target.HEAL_Per * (Math.Pow(0.7, n) - 1));


            }
        }
        
        private int deheal=0;
        private BattleChar tar=new BattleChar();
    }
    public class Sex_int_3_LucyDraw : Sex_ImproveClone
    {
        public override void SkillTargetSingle(List<Skill> Targets)
        {
            Targets[0].Delete(false);//弃牌

            List<Buff> list = new List<Buff>();//解除随机debuff
            foreach (Buff buff in Targets[0].Master.Buffs)
            {
                if (buff.BuffData.Debuff && buff.BuffData.BuffTag.Key != "null" && !buff.BuffData.Cantdisable && !buff.BuffData.Hide && !buff.DestroyBuff)
                {
                    list.Add(buff);
                }
            }
            if (list.Count != 0)
            {
                Targets[0].Master.BuffRemove(list.Random(Targets[0].Master.GetRandomClass().Main).BuffData.Key, false);
            }

            for (int i = 0; i < BattleSystem.instance.AllyTeam.Skills_UsedDeck.Count; i++)//将弃牌库中目标的技能洗入牌库
            {
                if (BattleSystem.instance.AllyTeam.Skills_UsedDeck[i].Master == Targets[0].Master)
                {
                    int index = UnityEngine.Random.Range(0, BattleSystem.instance.AllyTeam.Skills_Deck.Count + 1);
                    BattleSystem.instance.AllyTeam.Skills_Deck.Insert(index, BattleSystem.instance.AllyTeam.Skills_UsedDeck[i]);
                    BattleSystem.instance.AllyTeam.Skills_UsedDeck.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                BattleSystem.instance.AllyTeam.CharacterDraw(Targets[0].Master, null);
            }
        }
    }

    public class Sex_int_4 : Sex_ImproveClone
    {

        public override void Special_PointerEnter(BattleChar Char)
        {
            base.Special_PointerEnter(Char);
            if (Char is BattleEnemy)
            {
                this.IsDamage = true;
            }
        }
        public override void Special_PointerExit()
        {
            if (!this.effecting)
            {
                this.IsDamage = false;
            }
        }
        public bool effecting = false;
        public override void Init()
        {
            base.Init();
            this.SkillBasePlus.Target_BaseDMG = 0;
            this.PlusSkillPerStat.Heal = 0;
            //this.IsDamage = true;
            this.SkillBasePlusPreview.Target_BaseDMG = (int)((float)this.MySkill.TargetHeal * 1.2);

            if (this.TargetBuff.Find(a => a.BuffData.Key == "B_SF_Intruder_4_A") == null || this.TargetBuff.Find(a => a.BuffData.Key == "B_SF_Intruder_4_E") == null)
            {
                this.TargetBuff.RemoveAll(a => a.BuffData.Key == "B_SF_Intruder_4_A" || a.BuffData.Key == "B_SF_Intruder_4_E");

                GDEBuffData gde1 = new GDEBuffData("B_SF_Intruder_4_E");
                BuffTag btg1 = new BuffTag();
                btg1.User = this.BChar;
                btg1.BuffData = gde1;
                GDEBuffData gde2 = new GDEBuffData("B_SF_Intruder_4_A");
                BuffTag btg2 = new BuffTag();
                btg2.User = this.BChar;
                btg2.BuffData = gde2;
                this.TargetBuff.Insert(0, btg1);
                this.TargetBuff.Insert(0, btg2);
            }
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            this.effecting = true;
            this.IsDamage = false;
            this.SkillBasePlus.Target_BaseDMG = 0;
            this.PlusSkillPerStat.Heal = 0;
            if (!Targets[0].Info.Ally)
            {
                this.IsDamage = true;
                this.IsHeal = false;
                this.SkillBasePlus.Target_BaseDMG = (int)((float)SkillD.TargetHeal*1.2f);
                this.PlusSkillPerStat.Heal = -99999;

                this.TargetBuff.RemoveAll(a => a.BuffData.Key == "B_SF_Intruder_4_A");
            }
            else
            {
                this.MySkill.UseOtherParticle_Path = "Particle/flare_lv1";

                this.TargetBuff.RemoveAll(a => a.BuffData.Key == "B_SF_Intruder_4_E");

                List<Buff> buffs = Targets[0].GetBuffs(BattleChar.GETBUFFTYPE.DOT, true, false);
                if (buffs.Count >= 1)
                {
                    buffs.Random(this.BChar.GetRandomClass().Main).SelfDestroy(false);
                }
                Targets[0].Info.ForceGetStat();
            }


        }
        
    }
    public class Bcl_int_4_A : Buff, IP_DamageTake, IP_Healed, IP_HealChange
    {
        //public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)//IP_DamageTake
        //{
        //if (NODEF)
        //{
                //this.sumPain += Dmg;
                //resist = true;
        //}
        //}
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", this.sumPain.ToString());
        }
        /*
        public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {
            if (NODEF)
            {
                this.sumPain += Dmg;
                return 0;
            }
            return Dmg;
        }
        */
        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if (NODEF)
            {
                this.sumPain += Dmg;
                resist = true;
            }

        }
        public override void SelfdestroyPlus()
        {
            if (this.sumPain >= 1)
            {
                Buff buff = this.BChar.BuffAdd(GDEItemKeys.Buff_B_Momori_P_NoDead, this.BChar, false, 0, false, -1, false);
                this.BChar.Damage(this.BChar, this.sumPain, false, true, false, 0, false, false, false);
                buff.SelfDestroy(false);
            }
        }
        public void HealChange(BattleChar Healer, BattleChar HealedChar, ref int HealNum, bool Cri, ref bool Force)
        {
            //this.isForce = Force;
            if(Force)
            {
                this.forceH = HealNum;
            }
            else
            {
                this.forceH = 0;
            }
        }
        public void Healed(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal)
        {
            //if ((HealedChar.Info.KeyData != GDEItemKeys.Character_Phoenix && OverHeal >= 1) || this.isForce)
            //{
                //this.sumPain = (int)(0.5 * this.sumPain);
            //}
            if(this.forceH > 0)
            {
                this.sumPain -= (2*HealNum);
                this.forceH = 0;
            }
            if(OverHeal >= 1)
            {
                //Debug.Log("overheal=" + OverHeal);
                this.sumPain -= (2 * OverHeal);
            }
            this.sumPain=Math.Max(0, this.sumPain);
        }
        public override void Init()
        {
            base.Init();
            this.PlusPerStat.Damage = 20;
            this.PlusStat.hit = -10;
        }
        private int sumPain = 0;
        private bool isForce;
        private int forceH;
    }


    public class Bcl_int_4_E : Buff, IP_DamageTakeChange
    {
        public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {
            if (NODEF)
            {
                return  (int)(1.5*Dmg);
            }
            return Dmg;
        }

        public override void Init()
        {
            base.Init();
            this.PlusStat.def = -10;
        }
    }

    public class Bcl_int_5 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.DMGTaken = -20;
        }


    }

    public class Sex_int_6 : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            int looptrun = 2+2*Targets[0].MyTeam.AliveChars.Count;
            Skill skill = this.MySkill.CloneSkill(true, null, null, true);
            skill.isExcept = true;
            skill.FreeUse = true;
            skill.PlusHit = true;

            for (int i = 1; i <= looptrun; i++)
            {
                if (BattleSystem.instance.EnemyTeam.AliveChars.Count >= 1)
                {
                    BattleSystem.DelayInput(this.Plushit(Targets[0],skill));
                }
            }
        }

        public IEnumerator Plushit(BattleChar tar0,Skill skill)//追加射击
        {

            this.BChar.ParticleOut(this.MySkill, skill, tar0.MyTeam.AliveChars.Random(this.BChar.GetRandomClass().Main));
            yield return new WaitForSecondsRealtime(0.3f);
            yield break;
        }
    }
    public class Bcl_int_6 : Buff, IP_SkillUse_User_After
    {
        public override void BuffStat()
        {
            base.BuffStat();
            this.PlusStat.hit = -32+32*(float)Math.Pow(0.5,this.StackNum);
        }

        public void SkillUseAfter(Skill SkillD)
        {
            BattleSystem.DelayInput(this.SkillUseAfter());
        }

        // Token: 0x060010B7 RID: 4279 RVA: 0x0008EE2C File Offset: 0x0008D02C
        public IEnumerator SkillUseAfter()
        {
            base.SelfStackDestroy();
            yield return null;
            yield break;
        }
    }

    public class Sex_int_7 : Sex_ImproveClone
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(1.1 * this.BChar.GetStat.reg)).ToString());
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            foreach (BattleChar bc in Targets)
            {
                if (bc.Info.GetData.Role.Key == GDEItemKeys.CharRole_Role_Tank)
                {
                    bc.BuffAdd("B_SF_Intruder_7_1", this.BChar, false, 0, false, -1, false);
                }
                else
                {
                    bc.BuffAdd("B_SF_Intruder_7_2", this.BChar, false, 0, false, -1, false);
                }
            }
        }
    }

    public class Bcl_int_7_1 : Buff
    {
        // Token: 0x060013BE RID: 5054 RVA: 0x00099C4B File Offset: 0x00097E4B
        public override void Init()
        {
            base.Init();
            this.BarrierHP += (int)(1.1*this.Usestate_F.GetStat.reg) ;
        }
    }

    public class Bcl_int_7_2 : Buff, IP_SkillUseHand_Team
    {
        // Token: 0x06001148 RID: 4424 RVA: 0x00090D5C File Offset: 0x0008EF5C
        public override void Init()
        {
            base.Init();
            this.PlusStat.PlusMPUse.PlusMP_Skills = -1;
        }

        // Token: 0x06001149 RID: 4425 RVA: 0x00090D71 File Offset: 0x0008EF71
        public void SKillUseHand_Team(Skill skill)
        {
            if (skill.Master == this.BChar)
            {
                base.SelfDestroy(false);
            }
        }
    }

    public class Sex_int_8 : Sex_ImproveClone, IP_SkillUse_Target
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(this.BChar.GetStat.reg * this.MySkill.MySkill.Effect_Target.HEAL_Per/100)).ToString());


        }
        /*
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (Targets[0].HP < 1)
            {
                int num = 1 - Targets[0].HP;
                //Targets[0].Heal(this.BChar, (float)num, false, false, null);

                num=Math.Min(((int)(this.BChar.GetStat.reg * this.MySkill.MySkill.Effect_Target.HEAL_Per / 100)), num);
                this.SkillBasePlus.Target_BaseHeal += num;

                //this.MySkill.Except();
            }
        }
        */
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if(SP.SkillData==this.MySkill)
            {
                if(hit.HP<1)
                {
                    hit.HP = 1;
                }
            }
        }
    }

    /*
    public class Sex_int_9 : Sex_ImproveClone//, IP_BattleStart_Ones
    {
     
        public override void FixedUpdate()
        {
            
            if(!this.selecting && BattleSystem.instance.TargetSelecting && BattleSystem.instance.SelectedSkill==this.MySkill)
            {
                foreach(BattleChar bc in BattleSystem.instance.AllyTeam.Chars)
                {
                    if(!(bc as BattleAlly).UI.isActiveAndEnabled)
                    {
                        bc.Info.Incapacitated = false;
                        (bc as BattleAlly).UI.gameObject.SetActive(true) ;



                        this.chars.Add(bc) ;
                    }
                }
                this.selecting = true ;
            }
            if(this.selecting && (!BattleSystem.instance.TargetSelecting || BattleSystem.instance.SelectedSkill != this.MySkill))
            {
                foreach (BattleChar bc in this.chars)
                {
                    

                    //bc.IsDead = false;
                    bc.Info.Incapacitated = true;

                    //(bc as BattleAlly).ForceDead();

                    (bc as BattleAlly).UI.gameObject.SetActive(false);

                    //BattleSystem.instance.StartCoroutine(this.Delay(bc));

                }
                this.chars.Clear();
                this.selecting = false ;



            }
        }
        public bool selecting = false;
        public List<BattleChar> chars=new List<BattleChar>();
        
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (!BattleSystem.instance.AllyTeam.LucyChar.BuffFind("B_SF_Intruder_9_1", false)) 
            {
                BattleSystem.instance.AllyTeam.LucyChar.BuffAdd("B_SF_Intruder_9_1", this.BChar, false, 0, false, -1, false);
            }
            Bcl_int_9_1 bcl9 = BattleSystem.instance.AllyTeam.LucyChar.BuffReturn("B_SF_Intruder_9_1", false) as Bcl_int_9_1;
            bcl9.rebuildList.AddRange(Targets);

        }
    }
     */
    public class Sex_int_9 : Sex_ImproveClone,IP_SkillCastingStart,IP_BattleEnd//, IP_BattleStart_Ones
    {
        public override void Init()
        {
            base.Init();
            this.CountingExtedned = true;
            this.OnePassive = true;
        }
        public override void FixedUpdate()
        {
            
            if(!this.selecting && BattleSystem.instance.TargetSelecting && BattleSystem.instance.SelectedSkill==this.MySkill)
            {
                foreach(BattleChar bc in BattleSystem.instance.AllyTeam.Chars)
                {
                    if(!(bc as BattleAlly).UI.isActiveAndEnabled)
                    {
                        bc.Info.Incapacitated = false;
                        (bc as BattleAlly).UI.gameObject.SetActive(true) ;



                        this.chars.Add(bc) ;
                    }
                }
                this.selecting = true ;
            }
            if(this.selecting && (!BattleSystem.instance.TargetSelecting || BattleSystem.instance.SelectedSkill != this.MySkill))
            {
                foreach (BattleChar bc in this.chars)
                {
                    

                    //bc.IsDead = false;
                    bc.Info.Incapacitated = true;

                    //(bc as BattleAlly).ForceDead();

                    (bc as BattleAlly).UI.gameObject.SetActive(false);

                    //BattleSystem.instance.StartCoroutine(this.Delay(bc));

                }
                this.chars.Clear();
                this.selecting = false ;



            }
        }
        public bool selecting = false;
        public List<BattleChar> chars=new List<BattleChar>();


        public void SkillCasting(CastingSkill ThisSkill)
        {
            this.tar = ThisSkill.Target;
            this.tars.AddRange(ThisSkill.TargetReturn());
            this.cast = ThisSkill;
            foreach (BattleChar bc in this.tars)
            {
                if (!bc.IsDead)
                {
                    try
                    {
                        (bc as BattleAlly).ForceDead();
                    }
                    catch
                    {

                    }
                }
            }

        }

        public BattleChar tar = null;
        public List<BattleChar> tars = new List<BattleChar>();
        public CastingSkill cast = null;
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if(this.tars.Count>0)
            {

                Targets.RemoveAll(a => true);
                Targets.AddRange(this.tars);
                //Targets.Add(this.tar);
                //Targets.AddRange(this.cast.TargetReturn());
            }
            foreach(BattleChar bc in Targets)
            {
                //Debug.Log(bc.Info.Name);
                if(!bc.IsDead)
                {
                    try
                    {
                        (bc as BattleAlly).ForceDead();
                    }
                    catch
                    {

                    }
                }
            }
            /*
            if (!BattleSystem.instance.AllyTeam.LucyChar.BuffFind("B_SF_Intruder_9_1", false)) 
            {
                BattleSystem.instance.AllyTeam.LucyChar.BuffAdd("B_SF_Intruder_9_1", this.BChar, false, 0, false, -1, false);
            }
            Bcl_int_9_1 bcl9 = BattleSystem.instance.AllyTeam.LucyChar.BuffReturn("B_SF_Intruder_9_1", false) as Bcl_int_9_1;
            bcl9.rebuildList.AddRange(Targets);
            */
            foreach(BattleChar bc in Targets)
            {
                // BattleSystem.DelayInput(this.Rebuild_IE(bc));
                this.Rebuild(bc);
            }
        }
        public void BattleEnd()
        {
            //Debug.Log("battleend");
            foreach (BattleChar bc in this.tars)
            {
                this.Rebuild(bc);
            }
        }
        public IEnumerator Rebuild_IE(BattleChar Target)
        {
            this.Rebuild(Target);
            yield return null;
            yield break;
        }
        public void Rebuild(BattleChar Target)
        {
            bool trail = false;
            foreach (BattleChar bc in this.BChar.MyTeam.AliveChars_Vanish)
            {
                trail = trail || bc.BuffFind("B_TrialofStrength_Ally", false);
            }
            if (trail)
            {
                FieldSystem.instance.BattleAfterDelegate = new FieldSystem.BattleAfterDel(this.PartyStatusReset);
            }


            if (Target.IsDead)
            {
                if (Target is BattleAlly)
                {
                    (Target as BattleAlly).UI.gameObject.SetActive(true);


                }
                //yield return new WaitForSeconds(1);

                Target.IsDead = false;
                Target.Info.Incapacitated = false;
                List<Skill> sklist = new List<Skill>();
                bool flag = true;
                /*
                if (DeadPlugin.DeadList.Find(a => a.DeadAlly == Target) != null)
                {
                    DeadAllySkill dead = DeadPlugin.DeadList.Find(a => a.DeadAlly == Target);
                    sklist = dead.DeadSkill;
                    flag = false;
                    //Debug.Log("找到死亡记录");
                }
                */
                if (flag)
                {
                    sklist = Skill.CharToSkills(Target, Target.MyTeam);
                    //Debug.Log("未找到死亡记录");
                }

                try
                {
                    foreach (Skill skill in sklist)
                    {
                        BattleSystem.instance.AllyTeam.Skills_UsedDeck.Add(skill);
                    }
                }
                catch { }
            }

            if (Target.HP < Target.GetStat.maxhp)
            {
                Target.Recovery = Target.GetStat.maxhp;
                Target.HP = Target.GetStat.maxhp;
                Target.Recovery = Target.GetStat.maxhp;
            }
            /*
            for (int j = 0; j < Target.Buffs.Count; j++)
            {
                Buff buff = Target.Buffs[j];
                if (!buff.BuffData.Hide)
                {
                    if (buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT || buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_Debuff || buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_CrowdControl)

                    {
                        Target.Buffs.RemoveAt(j);
                        if (buff.BuffIcon != null)
                        {
                            UnityEngine.Object.Destroy(buff.BuffIcon);
                        }
                        GamepadManager.RefreshTarget();
                        j--;
                    }
                }
            }
            */
            //GamepadManager.RefreshTarget();


            //Target.BuffAdd("B_SF_Intruder_9_2", this.BChar, false, 0, false, -1, false);

        }
        public void PartyStatusReset()
        {

            for (int i = 0; i < PlayData.TSavedata.Party.Count; i++)
            {
                PlayData.TSavedata.Party[i].Incapacitated = DeadPlugin.PartyAlive[i];
                PlayData.TSavedata.Party[i].Hp = DeadPlugin.PartyHP[i];
            }

        }
    }

    public class DeadAllySkill
    {
        public BattleChar DeadAlly=new BattleChar();
        public List<Skill> DeadSkill=new List<Skill>();
    }

    
    public class Bcl_int_9_1 : Buff, IP_PlayerTurn, IP_BattleEnd
    {

        
        public void Turn()
        {
            for (int i = 0;i<this.rebuildList.Count;i++)
            {
                BattleSystem.DelayInput(this.Rebuild(this.rebuildList[i]));
                this.rebuildList.RemoveAt(i);
                i--;
            }
            if(this.rebuildList.Count<=0)
            {
                this.SelfDestroy();
            }
        }
        public void BattleEnd()
        {
            bool trail = false;
            foreach(BattleChar bc in this.BChar.MyTeam.AliveChars_Vanish)
            {
                trail=trail||bc.BuffFind("B_TrialofStrength_Ally",false);
            }
            if(trail)
            {
                FieldSystem.instance.BattleAfterDelegate = new FieldSystem.BattleAfterDel(this.PartyStatusReset);
            }

            for (int i = 0; i < this.rebuildList.Count; i++)
            {
                if (this.rebuildList[i].IsDead)
                {
                    if (this.rebuildList[i] is BattleAlly )
                    {
                        (this.rebuildList[i] as BattleAlly).UI.gameObject.SetActive(true);
                    }

                    this.rebuildList[i].IsDead = false;
                    this.rebuildList[i].Info.Incapacitated = false;
                    //this.rebuildList[i].Recovery = 1;
                    List<Skill> sklist = new List<Skill>();
                    bool flag = true;



                    if (DeadPlugin.DeadList.Find(a => a.DeadAlly == this.rebuildList[i]) != null)
                    {
                        DeadAllySkill dead = DeadPlugin.DeadList.Find(a => a.DeadAlly == this.rebuildList[i]);
                        sklist = dead.DeadSkill;
                        flag = false;
                        //Debug.Log("找到死亡记录");
                    }
                    if (flag)
                    {
                        sklist = Skill.CharToSkills(this.rebuildList[i], this.rebuildList[i].MyTeam);
                        //Debug.Log("未找到死亡记录");
                    }

                }

                if (this.rebuildList[i].HP < this.rebuildList[i].GetStat.maxhp)//回血
                {
                    //int num = this.rebuildList[i].GetStat.maxhp - this.rebuildList[i].HP;
                    //this.rebuildList[i].Heal(this.rebuildList[i], (float)num, false, true, null);
                    this.rebuildList[i].Recovery = this.rebuildList[i].GetStat.maxhp;
                    this.rebuildList[i].HP = this.rebuildList[i].GetStat.maxhp;
                    this.rebuildList[i].Recovery = this.rebuildList[i].GetStat.maxhp;
                }

                for (int j = 0; j < this.rebuildList[i].Buffs.Count; j++)
                {
                    Buff buff = this.rebuildList[i].Buffs[j];
                    if (!buff.BuffData.Hide)
                    {
                        if (buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT || buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_Debuff || buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_CrowdControl)

                        {
                            this.rebuildList[i].Buffs.RemoveAt(j);
                            if (buff.BuffIcon != null)
                            {
                                UnityEngine.Object.Destroy(buff.BuffIcon);
                            }
                            GamepadManager.RefreshTarget();
                            j--;
                        }
                    }
                }

            }
        }
        public IEnumerator Rebuild(BattleChar Target)
        {
            bool trail = false;
            foreach (BattleChar bc in this.BChar.MyTeam.AliveChars_Vanish)
            {
                trail = trail || bc.BuffFind("B_TrialofStrength_Ally", false);
            }
            if (trail)
            {
                FieldSystem.instance.BattleAfterDelegate = new FieldSystem.BattleAfterDel(this.PartyStatusReset);
            }


            if (Target.IsDead)
            {
                
                //Target.Info.Incapacitated = false;
                if (Target is BattleAlly)
                {
                    (Target as BattleAlly).UI.gameObject.SetActive(true);


                }
                yield return new WaitForSeconds(1);

                Target.IsDead = false;
                Target.Info.Incapacitated = false;
                //Target.Recovery = 1;
                List<Skill> sklist = new List<Skill>();
                bool flag = true;

                //if(BattleSystem.instance.AllyTeam.LucyChar.BuffFind("B_SF_Intruder_9_0",false))
                //{
                //Bcl_int_9_0 bcl9 = BattleSystem.instance.AllyTeam.LucyChar.BuffReturn("B_SF_Intruder_9_0", false) as Bcl_int_9_0;
                //if (bcl9.DeadList.Find(a => a.DeadAlly == Target)!=null)
                //{
                //DeadAllySkill dead = bcl9.DeadList.Find(a => a.DeadAlly == Target);
                //sklist = dead.DeadSkill;
                //flag = false;
                //Debug.Log("找到死亡记录");
                //}
                //}

                if (DeadPlugin.DeadList.Find(a => a.DeadAlly == Target) != null)
                {
                    DeadAllySkill dead = DeadPlugin.DeadList.Find(a => a.DeadAlly == Target);
                    sklist = dead.DeadSkill;
                    flag = false;
                    //Debug.Log("找到死亡记录");
                }
                if (flag)
                {
                    sklist = Skill.CharToSkills(Target, Target.MyTeam);
                    //Debug.Log("未找到死亡记录");
                }

                foreach (Skill skill in sklist)
                {
                    //Debug.Log("生成技能：" + skill.MySkill.Name);

                    BattleSystem.instance.AllyTeam.Skills_UsedDeck.Add(skill);
                }



                //(Target as BattleAlly).UI.CharAni.SetBool("NearDead", true);

                //(Target as BattleAlly).UI.CharAni.SetBool("NearDead", false);
                //AllyWindow.AliveAni.SetBool("Dead", this.Info.Incapacitated);
            }

            if (Target.HP < Target.GetStat.maxhp)
            {
                //int num = Target.GetStat.maxhp - Target.HP;
                //Target.Heal(Target, (float)num, false, true, null);
                Target.Recovery = Target.GetStat.maxhp;
                Target.HP = Target.GetStat.maxhp;
                Target.Recovery = Target.GetStat.maxhp;
            }

            for (int j = 0; j < Target.Buffs.Count; j++)
            {
                Buff buff = Target.Buffs[j];
                if (!buff.BuffData.Hide)
                {
                    if (buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT || buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_Debuff || buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_CrowdControl)

                    {
                        Target.Buffs.RemoveAt(j);
                        if (buff.BuffIcon != null)
                        {
                            UnityEngine.Object.Destroy(buff.BuffIcon);
                        }
                        GamepadManager.RefreshTarget();
                        j--;
                    }
                }
            }

            //GamepadManager.RefreshTarget();


            Target.BuffAdd("B_SF_Intruder_9_2", this.Usestate_L, false, 0, false, -1, false);

            yield return null;
            yield break;
        }

        public void PartyStatusReset()
        {
            
            for (int i = 0; i < PlayData.TSavedata.Party.Count; i++)
            {
            			PlayData.TSavedata.Party[i].Incapacitated = DeadPlugin.PartyAlive[i];    
                    PlayData.TSavedata.Party[i].Hp = DeadPlugin.PartyHP[i];
            }
            
        }

        //public override void FixedUpdate()
        //{

        //base.FixedUpdate();
        //if (BattleSystem.instance.TurnNum>this.turnN)
        //{
        //this.SelfDestroy();
        //}

        //if (BattleSystem.instance.AllyTeam.AliveChars.Count == 1 && BattleSystem.instance.AllyTeam.AliveChars[0]==this.BChar)
        //{
        //BattleSystem.DelayInputAfter(this.Del());
        //}
        //}

        //public IEnumerator Del()
        //{
        //yield return new WaitForFixedUpdate();

        //this.BChar.Dead(false, false);
        //yield break;
        //}

        public List<BattleChar> rebuildList=new List<BattleChar>();

        private int turnN;
    }
    
    public class Bcl_int_9_2 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusPerStat.Damage = -40;
            this.PlusPerStat.Heal = -40;
            this.PlusStat.def = -40;
            this.PlusStat.dod = -40;
            this.PlusStat.hit = -40;
        }
    }



    public class Sex_int_10 : Sex_ImproveClone
    {
        // Token: 0x060010FC RID: 4348 RVA: 0x0008FA21 File Offset: 0x0008DC21
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            this.pflag = Targets[0].BuffFind("B_SF_Intruder_10_1", false);
        }
        public override void SkillKill(SkillParticle SP)
        {
            List<BattleChar> list = new List<BattleChar>();
            list.AddRange(SP.TargetChar[0].MyTeam.AliveChars);
            list.Remove(SP.TargetChar[0]);

            if (list.Count > 0 && !this.pflag)
            {
                List<BattleChar> list2 = new List<BattleChar>();
                list2.AddRange(list);
                for (int i = 0; i < list2.Count; i++)
                {
                    if (list2[i].BuffFind("B_SF_Intruder_10_1", false))
                    {
                        list2.RemoveAt(i);
                        i--;
                    }
                }
                int n = 0;
                for (int i = 0; i < 1 && list2.Count > 0; i++)
                {
                    BattleChar bc = list2.Random(this.BChar.GetRandomClass().Main);
                    list2.Remove(bc);
                    bc.BuffAdd("B_SF_Intruder_10_1",this.BChar, false, 0, false, -1, true);
                    n++;
                }

                if (n < 1)
                {
                    List<BattleChar> list3 = new List<BattleChar>();
                    list3.AddRange(list);
                    for (int i = 0; i < list3.Count; i++)
                    {
                        if (list3[i].BuffFind("B_SF_Intruder_10_1", false) && list3[i].BuffReturn("B_SF_Intruder_10_1", false).StackInfo[0].RemainTime >= list3[i].BuffReturn("B_SF_Intruder_10_1", false).LifeTime)
                        {
                            list3.RemoveAt(i);
                            i--;
                        }

                    }
                    for (int i = n; i < 1 && list3.Count > 0; i++)
                    {
                        BattleChar bc2 = list3.Random(this.BChar.GetRandomClass().Main);
                        list3.Remove(bc2);
                        bc2.BuffAdd("B_SF_Intruder_10_1", this.BChar, false, 0, false,-1, true);
                    }
                }
            }
        }
        private bool pflag;
    }
    public class Bcl_int_10_1 : Buff, IP_BuffAdd, IP_Dead, IP_TurnEnd
    {
        public override void TurnUpdate()
        {

        }
        public void TurnEnd()
        {
            base.TurnUpdate();
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(!this.active)
            {
                this.frame++;
                if(this.frame>10 || this.frame<0)
                {
                    this.frame = 0;
                    if(!this.BChar.BuffFind("B_SF_Intruder_10_2",false))
                    {
                        this.active = true;
                    }
                }
            }
        }
        public int frame = 0;
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        //public void BuffAdd_Before(BattleChar BC, string key, BattleChar UseState, bool hide, int PlusTagPer, bool debuffnonuser, int RemainTime, bool StringHide)

        {
            //GDEBuffData gdebuffData = new GDEBuffData(key);
            if (this.active && !this.spreading && BuffTaker == this.BChar &&!addedbuff.IsHide&& addedbuff.BuffData.Key != "B_SF_Intruder_10_2" && addedbuff.BuffData.Key != "B_SF_Intruder_10_1" && addedbuff.BuffData.Debuff)
            {

                this.SelfDestroy();
                //BattleSystem.DelayInput(this.Del(addedbuff,  BuffUser));
                this.Del(addedbuff.BuffData.Key, BuffUser);


            }
        }

        public void Dead()
        {
            List<BattleChar> list = new List<BattleChar>();
            list.AddRange(this.BChar.MyTeam.AliveChars);
            list.Remove(this.BChar);

            if (list.Count > 0)
            {
                List<BattleChar> list2 = new List<BattleChar>();
                list2.AddRange(list);
                for (int i = 0; i < list2.Count; i++)
                {
                    if (list2[i].BuffFind(this.BuffData.Key, false))
                    {
                        list2.RemoveAt(i);
                        i--;
                    }
                }
                int n = 0;
                for (int i = 0; i < 1 && list2.Count > 0; i++)
                {
                    BattleChar bc = list2.Random(this.BChar.GetRandomClass().Main);
                    list2.Remove(bc);
                    bc.BuffAdd(this.BuffData.Key, base.Usestate_L, false, 0, false, this.StackInfo[0].RemainTime, true);
                    n++;
                }

                if (n < 1)
                {
                    List<BattleChar> list3 = new List<BattleChar>();
                    list3.AddRange(list);
                    for (int i = 0; i < list3.Count; i++)
                    {
                        if (list3[i].BuffFind(this.BuffData.Key, false) && list3[i].BuffReturn(this.BuffData.Key, false).StackInfo[0].RemainTime >= this.StackInfo[0].RemainTime)
                        {
                            list3.RemoveAt(i);
                            i--;
                        }

                    }
                    for (int i = n; i < 1 && list3.Count > 0; i++)
                    {
                        BattleChar bc2 = list3.Random(this.BChar.GetRandomClass().Main);
                        list3.Remove(bc2);
                        bc2.BuffAdd(this.BuffData.Key, base.Usestate_L, false, 0, false, this.StackInfo[0].RemainTime, true);
                    }
                }
            }
            
        }
        public void Del(string addedbuffkey, BattleChar BuffUser)
        {
            //yield return new WaitForFixedUpdate();


            List<BattleChar> list = new List<BattleChar>();//除自身外所有友军
            list.AddRange(this.BChar.MyTeam.AliveChars);
            list.Remove(this.BChar);

            if(list.Count > 0)
            {
                List<BattleChar> list2 = new List<BattleChar>();
                list2.AddRange(list);
                for (int i = 0; i <  list2.Count ; i++)
                {
                    if (list2[i].BuffFind(this.BuffData.Key,false))
                    {
                        list2.RemoveAt(i);
                        i--;
                    }
                }
                int n = 0;
                for (int i = 0; i<2 && list2.Count>0; i++)
                {
                    BattleChar bc = list2.Random(this.BChar.GetRandomClass().Main);
                    list2.Remove(bc);
                    bc.BuffAdd(this.BuffData.Key, base.Usestate_L, false, 0, false, this.StackInfo[0].RemainTime, true);
                    n++;
                }

                if(n<2)
                {
                    List<BattleChar> list3 = new List<BattleChar>();
                    list3.AddRange(list);
                    for (int i = 0; i < list3.Count; i++)
                    {
                        if (list3[i].BuffFind(this.BuffData.Key, false)&& list3[i].BuffReturn(this.BuffData.Key, false).StackInfo[0].RemainTime >= this.StackInfo[0].RemainTime)
                        {
                            list3.RemoveAt(i);
                            i--;
                        }

                    }
                    for (int i = n; i < 2 && list3.Count > 0; i++)
                    {
                        BattleChar bc2 = list3.Random(this.BChar.GetRandomClass().Main);
                        list3.Remove(bc2);
                        bc2.BuffAdd(this.BuffData.Key, base.Usestate_L, false, 0, false, this.StackInfo[0].RemainTime, true);
                    }
                }


                List<Bcl_int_10_1> listbuff = new List<Bcl_int_10_1>();//传播中
                foreach (BattleChar bc in this.BChar.MyTeam.AliveChars)
                {
                    if (bc.BuffFind("B_SF_Intruder_10_1", false))
                    {
                        listbuff.Add(bc.BuffReturn("B_SF_Intruder_10_1", false) as Bcl_int_10_1);
                    }
                }
                List<Bcl_int_10_2> listbuff2 = new List<Bcl_int_10_2>();//传播中
                foreach (BattleChar bc in this.BChar.MyTeam.AliveChars)
                {
                    if (bc.BuffFind("B_SF_Intruder_10_2", false))
                    {
                        listbuff2.Add(bc.BuffReturn("B_SF_Intruder_10_2", false) as Bcl_int_10_2);
                    }
                }

                foreach (Bcl_int_10_1 bcl in listbuff)
                {
                    if(bcl.spreading)
                    {
                        //Debug.Log("1触发b10_1_错误");
                    }
                    bcl.spreading = true;
                }
                foreach (Bcl_int_10_2 bcl in listbuff2)
                {
                    if (bcl.spreading)
                    {
                        //Debug.Log("1触发b10_2_错误");
                    }
                    bcl.spreading = true;
                }

                foreach (BattleChar bc2 in list)
                {
                    if(bc2.BuffFind(this.BuffData.Key, false))
                    {
                        bc2.BuffAdd(addedbuffkey, BuffUser, false, 999, false, -1, true);
                    }
                }

                foreach (Bcl_int_10_1 bcl in listbuff)//解除传播中
                {
                    bcl.spreading = false;
                }
                foreach (Bcl_int_10_2 bcl in listbuff2)
                {
                    bcl.spreading = false;
                }


                if (!this.BChar.BuffFind("B_SF_Intruder_10_2", false))
                {
                    this.BChar.BuffAdd("B_SF_Intruder_10_2", this.BChar, false, 0, false, -1, false);

                }
                Bcl_int_10_2 bcl10_2 = this.BChar.BuffReturn("B_SF_Intruder_10_2", false) as Bcl_int_10_2;

                int frameCount = Time.frameCount;

                bcl10_2.pflag = true;
                bcl10_2.pkey = addedbuffkey;
                bcl10_2.ptime = frameCount;
            }
            
            //yield break;
        }
        public bool spreading = false;
        public bool active = false;
    }

    public class Bcl_int_10_2 : Buff, IP_BuffAdd
    {
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        //public void BuffAdd_Before(BattleChar BC, string key, BattleChar UseState, bool hide, int PlusTagPer, bool debuffnonuser, int RemainTime, bool StringHide)
        {
            int frameCount = Time.frameCount;
            //Debug.Log("Bcl_int_10_2 Check Point 1");
            //Debug.Log("this.pflag:"+ this.pflag);
            this.ptime=Math.Min(this.ptime+0,frameCount);
            //Debug.Log(frameCount +"/"+ this.ptime);
            if (!this.spreading && this.pflag && BuffTaker == this.BChar && !addedbuff.IsHide && addedbuff.BuffData.Key != "B_SF_Intruder_10_2" && addedbuff.BuffData.Key != "B_SF_Intruder_10_1" && !this.BChar.BuffFind("B_SF_Intruder_10_1", false)&&(/*this.pkey == addedbuff.BuffData.Key &&*/ this.ptime >= frameCount))
            {
                //Debug.Log("Bcl_int_10_2 Check Point 2");
                List<BattleChar> list = new List<BattleChar>();
                list.AddRange(this.BChar.MyTeam.AliveChars);
                list.Remove(this.BChar);

                List<Bcl_int_10_1> listbuff = new List<Bcl_int_10_1>();//传播中
                foreach (BattleChar bc in this.BChar.MyTeam.AliveChars)
                {
                    if (bc.BuffFind("B_SF_Intruder_10_1", false))
                    {
                        listbuff.Add(bc.BuffReturn("B_SF_Intruder_10_1", false) as Bcl_int_10_1);
                    }
                }
                List<Bcl_int_10_2> listbuff2 = new List<Bcl_int_10_2>();//传播中
                foreach (BattleChar bc in this.BChar.MyTeam.AliveChars)
                {
                    if (bc.BuffFind("B_SF_Intruder_10_2", false))
                    {
                        listbuff2.Add(bc.BuffReturn("B_SF_Intruder_10_2", false) as Bcl_int_10_2);
                    }
                }

                foreach (Bcl_int_10_1 bcl in listbuff)
                {
                    if (bcl.spreading)
                    {
                        //Debug.Log("2触发b10_1_错误");
                    }
                    bcl.spreading = true;
                }
                foreach (Bcl_int_10_2 bcl in listbuff2)
                {
                    if (bcl.spreading)
                    {
                        //Debug.Log("2触发b10_2_错误");
                    }
                    bcl.spreading = true;
                }

                foreach (BattleChar bc2 in list)
                {
                    if (bc2.BuffFind("B_SF_Intruder_10_1", false))
                    {
                        bc2.BuffAdd(addedbuff.BuffData.Key, BuffUser, false, 999, false, -1, true);
                    }
                }

                foreach (Bcl_int_10_1 bcl in listbuff)
                {
                    bcl.spreading = false;
                }
                foreach (Bcl_int_10_2 bcl in listbuff2)
                {
                    bcl.spreading = false;
                }


            }
            else if (this.ptime < frameCount)
            {
                //this.BChar.Buffs.Remove(this);
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(!BattleSystem.instance.DelayWait && BattleSystem.instance.Particles.Count == 0)
            {
                this.SelfDestroy();
            }
        }
        public bool pflag;
        public string pkey;
        public int ptime;
        public bool spreading;
    }

    public class Sex_int_11 : Sex_ImproveClone
    {

        public override void Special_PointerEnter(BattleChar Char)
        {
            base.Special_PointerEnter(Char);
            if (Char is BattleEnemy)
            {
                this.IsDamage = true;
            }
        }
        public override void Special_PointerExit()
        {
            if (!this.effecting)
            {
                this.IsDamage = false;
            }
        }
        public bool effecting = false;
        public override void Init()
        {
            base.Init();
            this.SkillBasePlus.Target_BaseDMG = 0;
            this.PlusSkillPerStat.Heal = 0;
            //this.IsDamage = true;
            this.SkillBasePlusPreview.Target_BaseDMG = (int)((float)this.MySkill.TargetHeal * 1.2);
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            int sumnum = 0;
            foreach(BattleChar enemy in BattleSystem.instance.EnemyTeam.AliveChars)
            {
                if(enemy.BuffFind("B_SF_Intruder_p",false) && enemy != Targets[0])
                {
                    Bcl_int_p bclp = enemy.BuffReturn("B_SF_Intruder_p", false) as Bcl_int_p;
                    sumnum += bclp.Hack;
                    bclp.Hack = 0;
                }

            }


            this.effecting = true;
            this.IsDamage = false;
            this.SkillBasePlus.Target_BaseDMG = 0;
            this.PlusSkillPerStat.Heal = 0;
            if (!Targets[0].Info.Ally)
            {
                this.IsDamage = true;
                this.IsHeal = false;
                this.SkillBasePlus.Target_BaseDMG = (int)((float)SkillD.TargetHeal * 1.2f);
                this.PlusSkillPerStat.Heal = -99999;


                if (!Targets[0].BuffFind("B_SF_Intruder_p", false))
                {
                    Targets[0].BuffAdd("B_SF_Intruder_p", this.BChar, false, 0, false, -1, false);
                }

                Bcl_int_p bclp = Targets[0].BuffReturn("B_SF_Intruder_p", false) as Bcl_int_p;
                bclp.HackAdd(sumnum, true);

            }
            else if (Targets[0].Info.Ally)
            {
                this.MySkill.UseOtherParticle_Path = "Particle/flare_lv1";

                Targets[0].BuffAdd("B_SF_Intruder_11_1", this.BChar, false, 0, false, -1, false);
                Bcl_int_11_1 bclp2 = Targets[0].BuffReturn("B_SF_Intruder_11_1", false) as Bcl_int_11_1;
                bclp2.sumnum += sumnum;
                bclp2.Init();
            }
        }
    }
    public class Bcl_int_11_1 : Buff
    {
        public override void Init()
        {
            base.Init();
            if (this.BChar.Info.GetData.Role.Key == GDEItemKeys.CharRole_Role_Tank)
            {
                this.PlusStat.def = 3 * this.sumnum;
            }
            else if (this.BChar.Info.GetData.Role.Key == GDEItemKeys.CharRole_Role_Support)
            {
                this.PlusPerStat.Heal=3*this.sumnum;
            }
            else if (this.BChar.Info.GetData.Role.Key == GDEItemKeys.CharRole_Role_DPS)
            {
                this.PlusPerStat.Damage = 3 * this.sumnum;
            }
            else
            {
                this.PlusStat.def = 1 * this.sumnum;
                this.PlusPerStat.Heal = 1 * this.sumnum;
                this.PlusPerStat.Damage = 1 * this.sumnum;
            }
        }
        public int sumnum;
    }

    public class Bcl_int_12_S : Buff,IP_PlayerTurn//, IP_TurnEnd
    {
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            for (int i = 0; i < 3; i++)
            {

                Skill skill = Skill.TempSkill("S_SF_Intruder_12_2", this.BChar, this.BChar.MyTeam);
                skill.Counting = 10;

                BattleTeam.SkillRandomUse(this.BChar, skill, false, true, false);

            }


            //Skill skill = Skill.TempSkill("S_SF_Intruder_12_1", this.BChar, this.BChar.MyTeam);
            //this.BChar.MyTeam.Add(skill, true);

        }

        public void Turn()
        {
            //base.TurnUpdate();
            //this.beforeTurn = false;

            //if (this.StackInfo[StackInfo.Count - 1].RemainTime>0)
            //{
                //Debug.Log("军势侵扰检查点1");
                //Skill skill = Skill.TempSkill("S_SF_Intruder_12_1", this.BChar, this.BChar.MyTeam);
                //this.BChar.MyTeam.Add(skill, true);

            for (int i = 0; i < 3; i++)
            {

            Skill skill = Skill.TempSkill("S_SF_Intruder_12_2", this.BChar, this.BChar.MyTeam);
            skill.Counting = 10;

            BattleTeam.SkillRandomUse(this.BChar, skill, false, true, false);

            }

            //}
            //Debug.Log("军势侵扰检查点2");
        }
        //public void TurnEnd()
        //{
        //this.beforeTurn = true;
        //this.turnShooted = false;
        //}
        //public override void FixedUpdate()
        //{

        //
        //if (!this.turnShooted && !this.beforeTurn && BattleSystem.instance.EnemyTeam.AliveChars.Count >= 1)
        //{
        //for (int i = 0; i < 3; i++)
        //{

        //Skill skill = Skill.TempSkill("S_SF_Intruder_12_1", this.BChar, this.BChar.MyTeam);
        //skill.Counting = 10;

        //BattleTeam.SkillRandomUse(this.BChar, skill, false, true, false);
        //this.BChar.MyTeam.Add(skill, true);
        //}
        //this.turnShooted = true;
        //}
        //}

        public bool turnShooted;
        public bool beforeTurn;
    }

    public class Sex_int_12_2 : Sex_ImproveClone, IP_TargetedAlly
    {
        //public override string DescExtended(string desc)
        //{
        //return base.DescExtended(desc).Replace("&a", (this.defNum).ToString());
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if(this.tar!=null && BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Find(a=>a==this.tar)!=null)
            {
                Targets[0] = this.tar;
                base.SkillUseSingle(SkillD, Targets);
            }
        }

        //}
        public override void Init()
        {
            base.Init();
            this.CountingExtedned = true;
        }
        public IEnumerator Targeted(BattleChar Attacker, List<BattleChar> SaveTargets, Skill skill)
        {
            //Debug.Log("被瞄准");
            bool flag = skill.TargetTypeKey == GDEItemKeys.s_targettype_all_enemy || skill.TargetTypeKey == GDEItemKeys.s_targettype_all_ally || skill.TargetTypeKey == GDEItemKeys.s_targettype_all;
            if (flag && skill.IsDamage)
            {
                /*
                for (int i = 0; i < BattleSystem.instance.CastSkills.Count; i++)
                {
                    if (BattleSystem.instance.CastSkills[i].skill==this.MySkill)
                    {
                        //BattleSystem.instance.ActWindow.CastingWaste(BattleSystem.instance.CastSkills[i].skill);
                        this.tar = Attacker;
                        BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyCastingSkillUse(BattleSystem.instance.CastSkills[i], false));
                        BattleSystem.instance.CastSkills.RemoveAt(i);
                        i--;
                    }
                }
                for (int i = 0; i < BattleSystem.instance.SaveSkill.Count; i++)
                {
                    if (BattleSystem.instance.SaveSkill[i].skill == this.MySkill)
                    {
                        //BattleSystem.instance.ActWindow.CastingWaste(BattleSystem.instance.SaveSkill[i].skill);
                        this.tar = Attacker;
                        BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyCastingSkillUse(BattleSystem.instance.SaveSkill[i], false));
                        BattleSystem.instance.SaveSkill.RemoveAt(i);
                        i--;
                    }
                }
                */
                for (int i = 0; i < BattleSystem.instance.AllyTeam.AliveChars.Count; i++)
                {

                    if (!BattleSystem.instance.AllyTeam.AliveChars[i].BuffFind("B_SF_Intruder_12_1", false))
                    {
                        Bcl_int_12_1 bcl11=BattleSystem.instance.AllyTeam.AliveChars[i].BuffAdd("B_SF_Intruder_12_1", this.MySkill.Master, false, 0, false, -1, false) as Bcl_int_12_1;
                        bcl11.isall = true;
                    }
                }
            }
            else if(skill.IsDamage)
            {
                //Debug.Log("准备代替承受伤害");
                //for (int i = 0; i < SaveTargets.Count; i++)
                for (int i = 0; i < BattleSystem.instance.AllyTeam.AliveChars.Count; i++)
                {
                    
                    if (!BattleSystem.instance.AllyTeam.AliveChars[i].BuffFind("B_SF_Intruder_12_1",false))
                    {
                        BattleSystem.instance.AllyTeam.AliveChars[i].BuffAdd("B_SF_Intruder_12_1", this.MySkill.Master, false, 0, false, -1, false);
                    }
                }
            }

            yield break;
        }
        /*
        public override void FixedUpdate()
        {
            this.frame++;
            if (this.frame > 20 || this.frame < 0)
            {
                this.frame = 0;
                for (int i = 0; i < BattleSystem.instance.AllyTeam.AliveChars.Count; i++)
                {

                    if (!BattleSystem.instance.AllyTeam.AliveChars[i].BuffFind("B_SF_Intruder_12_1", false))
                    {
                        BattleSystem.instance.AllyTeam.AliveChars[i].BuffAdd("B_SF_Intruder_12_1", this.MySkill.Master, false, 0, false, -1, false);
                    }
                }

            }

        }
        
        private int frame = 0;
        */
        public BattleChar tar=new BattleChar();
    }

    public class Bcl_int_12_1 : Buff, IP_OnlyDamage_Before,IP_DamageTakeChange
    {


        
        public IEnumerator WeakEnd()
        {
            this.SelfDestroy();
            yield return null;
            yield break;
        }
        // Token: 0x06000AEC RID: 2796 RVA: 0x0007AD58 File Offset: 0x00078F58
        public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        //public void OnlyDamage_Before(BattleChar battleChar, ref int Dmg, ref bool Weak)
        {
            //Debug.Log("代替承伤前1:"+Dmg);
            int dmgc = Dmg;
            if(!this.isall && Hit==this.BChar)
            {
                if (dmgc > 0 && BattleSystem.instance.CastSkills.Find(a => a.skill.MySkill.KeyID == "S_SF_Intruder_12_2") != null)
                {
                    //Debug.Log("代替承伤前2，CastSkills："+ BattleSystem.instance.CastSkills.Count);
                    int n = BattleSystem.instance.CastSkills.FindIndex(a => a.skill.MySkill.KeyID == "S_SF_Intruder_12_2");
                    //BattleSystem.instance.ActWindow.CastingWaste(BattleSystem.instance.CastSkills[n].skill);
                    (BattleSystem.instance.CastSkills[n].skill.AllExtendeds.Find(a => a is Sex_int_12_2) as Sex_int_12_2).tar = User;
                    BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyCastingSkillUse(BattleSystem.instance.CastSkills[n], false));
                    BattleSystem.instance.CastSkills.RemoveAt(n);

                    dmgc = (int)(0.55 * dmgc);
                    this.pflag1 = true;
                    //Weak = true;
                    //Debug.Log("代替承伤后2");
                    goto IL_Bcl_int_12_1_a;
                }
                if (dmgc > 0 && BattleSystem.instance.SaveSkill.Find(a => a.skill.MySkill.KeyID == "S_SF_Intruder_12_2") != null)
                {
                    //Debug.Log("代替承伤前2，SaveSkill：" + BattleSystem.instance.SaveSkill.Count);
                    int m = BattleSystem.instance.SaveSkill.FindIndex(a => a.skill.MySkill.KeyID == "S_SF_Intruder_12_2");
                    //BattleSystem.instance.ActWindow.CastingWaste(BattleSystem.instance.SaveSkill[m].skill);
                    (BattleSystem.instance.SaveSkill[m].skill.AllExtendeds.Find(a => a is Sex_int_12_2) as Sex_int_12_2).tar = User;
                    BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyCastingSkillUse(BattleSystem.instance.SaveSkill[m], false));
                    BattleSystem.instance.SaveSkill.RemoveAt(m);

                    dmgc = (int)(0.55 * dmgc);
                    this.pflag1 = true;
                    //Weak = true;
                    //Debug.Log("代替承伤后2");
                    goto IL_Bcl_int_12_1_a;
                }

            }
            else if(dmgc > 0 && Hit == this.BChar && ( BattleSystem.instance.CastSkills.Find(a => a.skill.MySkill.KeyID == "S_SF_Intruder_12_2") != null)|| BattleSystem.instance.SaveSkill.Find(a => a.skill.MySkill.KeyID == "S_SF_Intruder_12_2") != null|| this.pflag3)//|| this.pflag3)
            {
                int antnum = 0;

                
                for (int i = 0; i < BattleSystem.instance.CastSkills.Count; i++)
                {
                    if (BattleSystem.instance.CastSkills[i].skill.MySkill.KeyID == "S_SF_Intruder_12_2")
                    {
                        antnum++;
                        //BattleSystem.instance.ActWindow.CastingWaste(BattleSystem.instance.CastSkills[i].skill);
                        (BattleSystem.instance.CastSkills[i].skill.AllExtendeds.Find(a => a is Sex_int_12_2) as Sex_int_12_2).tar = User;
                        BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyCastingSkillUse(BattleSystem.instance.CastSkills[i], false));
                        BattleSystem.instance.CastSkills.RemoveAt(i);
                        i--;
                    }
                }
                for (int i = 0; i < BattleSystem.instance.SaveSkill.Count; i++)
                {
                    if (BattleSystem.instance.SaveSkill[i].skill.MySkill.KeyID == "S_SF_Intruder_12_2")
                    {
                        antnum++;
                        //BattleSystem.instance.ActWindow.CastingWaste(BattleSystem.instance.SaveSkill[i].skill);
                        (BattleSystem.instance.SaveSkill[i].skill.AllExtendeds.Find(a => a is Sex_int_12_2) as Sex_int_12_2).tar = User;
                        BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyCastingSkillUse(BattleSystem.instance.SaveSkill[i], false));
                        BattleSystem.instance.SaveSkill.RemoveAt(i);
                        i--;
                    }
                }

                if(!this.pflag3)
                {
                    this.pantnum = antnum;

                    for (int i = 0; i < BattleSystem.instance.AllyTeam.AliveChars.Count; i++)
                    {

                        if (BattleSystem.instance.AllyTeam.AliveChars[i].BuffFind("B_SF_Intruder_12_1", false))
                        {
                            (BattleSystem.instance.AllyTeam.AliveChars[i].BuffReturn("B_SF_Intruder_12_1", false) as Bcl_int_12_1).pflag3 = true;
                            (BattleSystem.instance.AllyTeam.AliveChars[i].BuffReturn("B_SF_Intruder_12_1", false) as Bcl_int_12_1).pantnum=this.pantnum;
                        }
                    }
                }



                dmgc = (int)((1-0.15*this.pantnum) * dmgc);
                this.pflag1 = true;

                this.pflag3 = false;
                this.pantnum = 0;
            }
            //Debug.Log("代替承伤后1:"+dmgc);
            IL_Bcl_int_12_1_a:;
            //this.SelfDestroy();
            //Dmg = dmgc;

            //BattleSystem.DelayInputAfter(this.WeakEnd());
            for (int i = 0; i < BattleSystem.instance.AllyTeam.AliveChars.Count; i++)
            {

                if (BattleSystem.instance.AllyTeam.AliveChars[i].BuffFind("B_SF_Intruder_12_1", false))
                {
                    //Bcl_int_12_1 bcl=(BattleSystem.instance.AllyTeam.AliveChars[i].BuffReturn("B_SF_Intruder_12_1", false) as Bcl_int_12_1);
                    BattleSystem.DelayInputAfter((BattleSystem.instance.AllyTeam.AliveChars[i].BuffReturn("B_SF_Intruder_12_1", false) as Bcl_int_12_1).WeakEnd());
                }
            }
            return dmgc;

        }
        public void OnlyDamage_Before(BattleChar battleChar, ref int Dmg, ref bool Weak)
        {
            if(battleChar==this.BChar&&this.pflag1)
            {
                Weak = true;
            }
            this.pflag1 = false;
        }

        /*
        public override void FixedUpdate()
        {
            if (!this.pflag2 && !BattleSystem.instance.DelayWait) 
            {
                this.pflag2 = true;
                BattleSystem.DelayInputAfter(this.WeakEnd());

            }
            /*
            this.frame++;
            if(this.frame>20 || this.frame<0)
            {
                this.frame = 0;

                if (!this.pflag1 && !this.pflag3)
                {

                    if(BattleSystem.instance.CastSkills.Find(a => a.skill.MySkill.KeyID == "S_SF_Intruder_12_2") == null && BattleSystem.instance.SaveSkill.Find(a => a.skill.MySkill.KeyID == "S_SF_Intruder_12_2") != null)
                    {
                        if(!this.pflag2)
                        {
                            this.pflag2 = true;
                            this.SelfDestroy();

                        }
                    }
                }
            }
               
        }*/
    

        private bool pflag1 = false;
        private int frame = 0;
        private bool pflag2 = false;
        public bool pflag3 = false;
        public int pantnum = 0;

        public bool isall = false;
    }


    public class Sex_int_13 : Sex_ImproveClone
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(this.BChar.GetStat.reg * 2f)).ToString()).Replace("&b", ((int)Math.Ceiling(this.BChar.GetStat.reg * 2f / 5)).ToString());


        }
        public override void BattleStartDeck(List<Skill> Skills_Deck)
        {
            //base.BattleStartDeck(Skills_Deck);
            //Skills_Deck.Remove(this.MySkill);
            //Skills_Deck.Insert(0, this.MySkill);
            this.BChar.MyTeam.Draw(this.MySkill, null);

        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {

            Bcl_int_13 b13 = this.BChar.BuffAdd("B_SF_Intruder_13", this.MySkill.Master, false, 0, false, -1, false) as Bcl_int_13;

            //Bcl_int_13 b13 = this.BChar.BuffReturn("B_SF_Intruder_13", false)as Bcl_int_13;
            b13.B13Updata();
        }

    }
    
    
    
    public class Bcl_int_13 : Buff, IP_Set_PartyBarrierHP, IP_PartybarrierDamage
    {

        //public override void Init()
        //{
        //base.Init();


        //}
        /*
        public void B13Updata()
        {

            int num = (int)(this.Usestate_F.GetStat.reg * 2f);
            this.PartyBarrierHP += num;
            this.protectNum = (int)Math.Ceiling((double)num / 5);
            BattleSystem.instance.AllyTeam.partybarrier.BarrierTextUpadte();

        }
        */
        public void PartybarrierDamage(int Damage)
        {
            //this.PartyBarrierHP = Math.Min(this.lastNum,this.BChar.MyTeam.partybarrier.BarrierHP);
            //Traverse.Create(this).Field("_PartyBarrrierHP").SetValue(Math.Min(this.lastNum, this.BChar.MyTeam.partybarrier.BarrierHP));
            Traverse.Create(this).Field("_PartyBarrrierHP").SetValue(this.lastNum);
            BattleSystem.instance.StartCoroutine(this.Delay_1());
        }
        public void Set_PartyBarrierHP(Buff buff, ref int num)
        {
            int HPold = this.PartyBarrierHP;
            if(buff==this)
            {
                int frameCount = Time.frameCount;
                if(this.timerem > frameCount)
                {
                    this.timerem = frameCount;
                }
                if ((this.timerem+5) < frameCount )
                {
                    this.timeprotect = this.PartyBarrierHP - this.protectNum;
                }
                this.timerem = frameCount;

                //Debug.Log("this.PartyBarrierHP：" + this.PartyBarrierHP );
                //Debug.Log("this.timeprotect：" + this.timeprotect);

                //if (num<this.PartyBarrierHP-this.protectNum)
                //{
                //num = this.PartyBarrierHP - this.protectNum;
                //}
                num = Math.Max(this.timeprotect, num);
                num = Math.Max(0, num);

                int numnew = num;
                this.lastNum = this.lastNum - HPold + numnew;
                num = 0;

                //Debug.Log("HPold：" + HPold);
                //Debug.Log("numnew：" + numnew);
                int partyBar=(int)Traverse.Create(this.BChar.MyTeam.partybarrier).Field("_BarrierHP").GetValue();
                //Debug.Log("编制保护前：" + (int)Traverse.Create(this.BChar.MyTeam.partybarrier).Field("_BarrierHP").GetValue());
                Traverse.Create(this.BChar.MyTeam.partybarrier).Field("_BarrierHP").SetValue(Math.Max(0, partyBar - HPold + numnew));
                //this.BChar.MyTeam.partybarrier.BarrierHP = Math.Max(0, partyBar - HPold + numnew);
                //Debug.Log("编制保护后：" + (int)Traverse.Create(this.BChar.MyTeam.partybarrier).Field("_BarrierHP").GetValue());



                if (this.lastNum <= 0 || this.BChar.MyTeam.partybarrier.BarrierHP<=0)
                {
                    this.SelfDestroy();
                }
                BattleSystem.instance.AllyTeam.partybarrier.BarrierTextUpadte();
            }
        }
        public IEnumerator Delay_1()
        {
            yield return new WaitForFixedUpdate();
            if(this.PartyBarrierHP > 0 )
            {
                Debug.Log("临时防护墙未正常解除");
                Traverse.Create(this).Field("_PartyBarrrierHP").SetValue(0);
                GameObject gameObject = Misc.UIInst(PlayData.BattleLucy.BattleInfo.SkillName);
                gameObject.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Temporary Partybarrier not reduced properly, please check outpout_log";
            }
            else
            {
                //GameObject gameObject = Misc.UIInst(PlayData.BattleLucy.BattleInfo.SkillName);
                //gameObject.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Temporary Partybarrier educed properly";
            }
            yield break;
        }
        /*
        public override void FixedUpdate()
        {
            if(this.PartyBarrierHP <= 0)
            {
                this.SelfDestroy();
            }
        }
        */
        
        public void B13Updata()
        {

            int num = (int)(this.Usestate_F.GetStat.reg * 2f);
            int partyBar = (int)Traverse.Create(this.BChar.MyTeam.partybarrier).Field("_BarrierHP").GetValue();
            this.BChar.MyTeam.partybarrier.BarrierHP = num+partyBar;
            //this.BChar.MyTeam.partybarrier.BarrierHP  += num;
            this.lastNum += num;
            this.protectNum = (int)Math.Ceiling((double)num / 5);
            //BattleSystem.instance.AllyTeam.partybarrier.BarrierTextUpadte();

        }
        /*
        public void PartybarrierDamage(int Damage)
        {
            this.PartyBarrierHP = 1;
        }
        */
        /*
        public void Set_PartyBarrierHP(Buff buff, ref int num)
        {
            //Debug.Log("point1：" + (num));
            if (buff == this && this.PartyBarrierHP>0)
            {
                num--;
                
                int frameCount = Time.frameCount;
                if (this.timerem < frameCount - 5)
                {
                    this.timeprotect =  - this.protectNum;
                }
                this.timerem = frameCount;
                
                //Debug.Log("编制保护前：" + ( - num));

                num = Math.Max(this.timeprotect, num);
                //num = Math.Max(-this.protectNum, num);
                num = Math.Max(-this.lastNum, num);
                this.timeprotect -= num;
                this.lastNum += num;
                //Debug.Log("编制保护后：" + (- num));
                
                if(this.lastNum<=0)
                {
                    this.SelfDestroy();
                }
            }
        }
        */
        private int lastNum;
        private int protectNum;

        private int timerem;
        private int timeprotect;
    }


    public class Sex_int_14 : Sex_ImproveClone
    {
        public override void Init()
        {
            base.Init();
            this.CanUseStun = true;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            List<Buff> list1 = new List<Buff>();//解除随机debuff
            foreach (Buff buff in Targets[0].Buffs)
            {
                if (buff.BuffData.Debuff && buff.BuffData.BuffTag.Key != "null" && !buff.BuffData.Cantdisable && !buff.BuffData.Hide && !buff.DestroyBuff)
                {
                    list1.Add(buff);
                    //Debug.Log("可解除buff："+buff.BuffData.Name);
                }
            }


            if (list1.Count > 0)
            {
                //base.SkillUseSingle(SkillD, Targets);
                List<Skill> lists = new List<Skill>();
                foreach (Buff buff in list1)
                {
                    GDESkillData skilldata_1 = new GDESkillData("S_SF_Intruder_14_1");
                    GDESkillData skilldata= skilldata_1.ShallowClone();
                    //skilldata.ResetAll
                    //string default_name=skilldata.Name;
                    skilldata.Name = skilldata.Name+buff.BuffData.Name;//"解除："+buff.BuffData.Name;
                    //skilldata.Description = buff.BuffData.Description;
                    //skilldata.User = Targets[0];
                    skilldata.Effect_Target.Buffs.Clear();
                    for (int i = 0; i < buff.StackNum; i++)
                    {
                        skilldata.Effect_Target.Buffs.Add(buff.BuffData);
                    }

                    //Debug.Log("SKillExtendedItem：" + skilldata.SKillExtendedItem.Count);
                    //skilldata.SKillExtendedItem[0].IsIcon_Path = buff.BuffData.Icon_Path;
                    //skilldata.SKillExtendedItem[0].Name = buff.BuffData.Name;

                    Skill skill = new Skill();
                    skill.Init(skilldata);
                    //skilldata.Name = default_name;

                    skill.Master = buff.Usestate_L;

                    skill.AllExtendeds.Clear();

                    GDESkillExtendedData gdesex = new GDESkillExtendedData("EX_SF_Intruder_14_1");
                    

                    gdesex.Debuff = true;
                    gdesex.IsIcon_Path = buff.BuffData.Icon_Path;
                    gdesex.Name = buff.BuffData.Name;
                    //gdesex.Des = buff.Usestate_L.Info.Name;
                    Skill_Extended sex1=DataToExtended(gdesex);

                    (sex1 as EX_int_14_1).BuffIconStackNum = skilldata.Effect_Target.Buffs.Count;
                    (sex1 as EX_int_14_1).user = buff.Usestate_L;
                    (sex1 as EX_int_14_1).tar = Targets[0] ;

                    skill.AllExtendeds.Add(sex1);

                    lists.Add(skill);
                }
                GDESkillData skilldata2 = new GDESkillData("S_SF_Intruder_14_2");//防御下一个减益
                //skilldata2.Name = "防御下一个减益";
                Skill skill2 = new Skill();
                skill2.Init(skilldata2);

                skill2.AllExtendeds.Clear();

                GDESkillExtendedData gdesex2 = new GDESkillExtendedData("EX_SF_Intruder_14_1");
                gdesex2.IsIcon_Path = "";
                gdesex2.Name = this.MySkill.MySkill.Name;//gdesex2.Name = "耐受反制";
                Skill_Extended sex2 = DataToExtended(gdesex2);
                (sex2 as EX_int_14_1).user = this.BChar;
                (sex2 as EX_int_14_1).tar = Targets[0];
                skill2.AllExtendeds.Add(sex2);
                lists.Insert(0,skill2);

                BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(lists, new SkillButton.SkillClickDel(this.Del), ScriptLocalization.System_SkillSelect.EffectSelect, false, false, true, false, false));
                //BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(lists, new SkillButton.SkillClickDel(this.Del), ScriptLocalization.System_SkillSelect.CreateSkill, false, true, true, false, true));




                //Buff buff1 = list1.Random(this.BChar.GetRandomClass().Main);

                //Skill_Extended skill_Extended = new Skill_Extended();
                //skill_Extended.TargetBuff = new List<BuffTag>();
                //BuffTag buffTag = new BuffTag();
                //buffTag.User = buff1.Usestate_L;
                //buffTag.PlusTagPer = 300;
                //buffTag.BuffData = buff1.BuffData;
                //for (int i = 0; i < buff1.StackNum; i++)
                //{
                //skill_Extended.TargetBuff.Add(buffTag.Clone());
                //}
                //buff1.SelfDestroy(false);

                //Targets[0].BuffAdd("B_SF_Intruder_14", this.BChar, false, 0, false, -1, false);
                //Bcl_int_14 bcl14= Targets[0].BuffReturn("B_SF_Intruder_14",false)as Bcl_int_14;
                //bcl14.skill_Extended = skill_Extended;
            }
            else
            {
                //Debug.Log("add b14_1 check");
                //Targets[0].BuffAdd("B_SF_Intruder_14_1", this.BChar, false, 0, false, -1, false);//changed
                Targets[0].BuffAdd("B_SF_Intruder_14", this.BChar, false, 0, false, -1, false);//changed
            }
        }
        //public IEnumerator ReflectBuff(BattleChar taker,BattleChar user,string buffkey)//追加射击
        //{
        //taker.BuffAdd(buffkey, user, false, 999, false, -1, false);
        //yield return new WaitForSecondsRealtime(0.0f);
        //yield break;
        //}
        public void Del(SkillButton Mybutton)
        {
            BattleChar tar = new BattleChar();
            if (Mybutton.Myskill.MySkill.Effect_Target.Buffs.Count<=0)
            {
                foreach (Skill_Extended sex in Mybutton.Myskill.AllExtendeds)
                {
                    if (sex is EX_int_14_1)
                    {
                        tar = (sex as EX_int_14_1).tar;
                        break;
                    }
                }
                //tar.BuffAdd("B_SF_Intruder_14_1", this.BChar, false, 0, false, -1, false);//changed
                tar.BuffAdd("B_SF_Intruder_14", this.BChar, false, 0, false, -1, false);//changed
            }
            else
            {
                Skill_Extended skill_Extended = new Skill_Extended();
                skill_Extended.TargetBuff = new List<BuffTag>();

                BuffTag buffTag = new BuffTag();
                foreach (Skill_Extended sex in Mybutton.Myskill.AllExtendeds)
                {
                    if (sex is EX_int_14_1)
                    {
                        buffTag.User = (sex as EX_int_14_1).user;
                        tar = (sex as EX_int_14_1).tar;
                        break;
                    }
                }
                buffTag.PlusTagPer = 300;

                for (int i = 0; i < Mybutton.Myskill.MySkill.Effect_Target.Buffs.Count; i++)
                {

                    buffTag.BuffData = Mybutton.Myskill.MySkill.Effect_Target.Buffs[i];

                    skill_Extended.TargetBuff.Add(buffTag.Clone());
                    //skill_Extended.TargetBuff.Add(buffTag.Clone());
                }
                if (tar.BuffFind(Mybutton.Myskill.MySkill.Effect_Target.Buffs[0].Key, false))
                {
                    tar.BuffReturn(Mybutton.Myskill.MySkill.Effect_Target.Buffs[0].Key, false).SelfDestroy(false);
                }


                tar.BuffAdd("B_SF_Intruder_14", this.BChar, false, 0, false, -1, false);
                Bcl_int_14 bcl14 = tar.BuffReturn("B_SF_Intruder_14", false) as Bcl_int_14;
                bcl14.skill_Extended = skill_Extended;
                bcl14.pflag = true;//changed
                bcl14.ChangeIcon();
            }


        }

    }
    public class EX_int_14_1 : Sex_ImproveClone
    {
        public BattleChar user=new BattleChar();
        public BattleChar tar = new BattleChar();
    }

    public class Bcl_int_14 : Buff, IP_SkillUse_Team_Target, IP_BuffAdd_Before
    {
        public override string DescExtended()
        {
            if(!this.pflag)
            {
                GDEBuffData gde = new GDEBuffData("B_SF_Intruder_14_1");
                return gde.Description;
            }
            return base.DescExtended().Replace("&a", this.skill_Extended.TargetBuff.Count.ToString()).Replace("&b", this.skill_Extended.TargetBuff[0].BuffData.Name);
        }


        public void SkillUseTeam_Target(Skill skill, List<BattleChar> Targets)
        {
            //Debug.Log("SkillUseTeam_Target检查点1:"+ this.skill_Extended.TargetBuff[0].BuffData.Key);
            if(!this.pflag)
            {
                return;
            }
            bool des = false;
            foreach (BattleChar tar in Targets)
            {
                if (!tar.Info.Ally)
                {
                    //skill.ExtendedAdd(this.skill_Extended);

                    for (int i = 0; i < this.skill_Extended.TargetBuff.Count; i++)
                    {
                        //tar.BuffAdd(this.skill_Extended.TargetBuff[i].BuffData.Key, this.skill_Extended.TargetBuff[i].User, false, 999, false, -1, false);
                        BattleSystem.DelayInput(ReflectBuff(tar, this.skill_Extended.TargetBuff[i].User, this.skill_Extended.TargetBuff[i].BuffData.Key));
                    }
                    des = true;

                    //base.SelfDestroy(false);
                    //Debug.Log("SkillUseTeam_Target检查点2");
                }
            }
            if (des)
            {
                base.SelfDestroy(false);
            }
        }
        public IEnumerator ReflectBuff(BattleChar taker, BattleChar user, string buffkey)//追加射击
        {
            taker.BuffAdd(buffkey, user, false, 999, false, -1, false);

            yield return new WaitForSecondsRealtime(0.0f);
            yield break;

        }

        public void BuffAdd_Before(BattleChar BC, string key, BattleChar UseState, bool hide, int PlusTagPer, bool debuffnonuser, int RemainTime, bool StringHide)
        {
            GDEBuffData gdebuffData = new GDEBuffData(key);
            int frameCount = Time.frameCount;
            //Debug.Log("time:" + Time.frameCount);
            //Debug.Log("timelast:" + BC.BuffCheck.TimeSeed);
            if (this.pflag && BC == this.BChar && gdebuffData.Debuff && !gdebuffData.Hide && !hide && (this.pkey == key && this.ptime == frameCount))//&& BC.BuffCheck.PlusResist)
            {


                BuffTag buffTag = new BuffTag();
                buffTag.User = UseState;
                buffTag.BuffData = gdebuffData;

                this.skill_Extended.TargetBuff.Add(buffTag.Clone());
            }
            else if (!this.pflag && BC == this.BChar && gdebuffData.Debuff && gdebuffData.BuffTag.Key != "null" && !gdebuffData.Hide && !hide && !(BC.BuffCheck.Lastbuff == key && BC.BuffCheck.TimeSeed == frameCount))
            {
                this.pkey = key;
                this.ptime = frameCount;

                LastBuffRsisCheck lastBuffRsisCheck = new LastBuffRsisCheck();
                lastBuffRsisCheck.TimeSeed = Time.frameCount;
                lastBuffRsisCheck.Lastbuff = key;
                lastBuffRsisCheck.IsAdd = false;
                BC.BuffCheck = lastBuffRsisCheck;
                BC.BuffCheck.PlusResist = true;
                this.BChar.SimpleTextOut(ScriptLocalization.UI_Battle.DebuffGuard);


                Bcl_int_14 bcl14 = this;

                Skill_Extended skill_Extended = new Skill_Extended();
                skill_Extended.TargetBuff = new List<BuffTag>();
                BuffTag buffTag = new BuffTag();
                buffTag.User = UseState;
                buffTag.BuffData = gdebuffData;
                skill_Extended.TargetBuff.Add(buffTag.Clone());
                bcl14.skill_Extended = skill_Extended;

                bcl14.pflag = true;
                bcl14.pkey = key;
                bcl14.ptime = frameCount;
                //bcl14.skill_Extended.TargetBuff.Add(buffTag.Clone());
                //this.pflag = true;

                this.ChangeIcon();
            }

        }
        public void ChangeIcon()
        {
            GDEBuffData bp = new GDEBuffData("B_SF_Intruder_14_1");
            this.BuffData.Name = bp.Name;
            string iconpath = bp.Icon_Path;
            if (!string.IsNullOrEmpty(iconpath))
            {
                this.BuffIconScript.Icon.sprite = AddressableLoadManager.LoadAsyncCompletion<Sprite>(iconpath, AddressableLoadManager.ManageType.Stage);
            }
        }


        public Skill_Extended skill_Extended = new Skill_Extended();

        public bool pflag = false;
        public string pkey;
        public int ptime;

    }
    /*
    public class Bcl_int_14 : Buff, IP_SkillUse_Team_Target, IP_BuffAdd_Before
    {
        public override string DescExtended()
        {

            return base.DescExtended().Replace("&a", this.skill_Extended.TargetBuff.Count.ToString()).Replace("&b", this.skill_Extended.TargetBuff[0].BuffData.Name);
        }


        public void SkillUseTeam_Target(Skill skill, List<BattleChar> Targets)
        {
            //Debug.Log("SkillUseTeam_Target检查点1:"+ this.skill_Extended.TargetBuff[0].BuffData.Key);
            bool des = false;
            foreach (BattleChar tar in Targets)
            {
                if (!tar.Info.Ally)
                {
                    //skill.ExtendedAdd(this.skill_Extended);

                    for (int i = 0;i< this.skill_Extended.TargetBuff.Count;i++)
                    {
                        //tar.BuffAdd(this.skill_Extended.TargetBuff[i].BuffData.Key, this.skill_Extended.TargetBuff[i].User, false, 999, false, -1, false);
                        BattleSystem.DelayInput(ReflectBuff(tar, this.skill_Extended.TargetBuff[i].User, this.skill_Extended.TargetBuff[i].BuffData.Key));
                    }
                    des = true;

                    //base.SelfDestroy(false);
                    //Debug.Log("SkillUseTeam_Target检查点2");
                }
            }
            if (des)
            {
                base.SelfDestroy(false);
            }
        }
        public IEnumerator ReflectBuff(BattleChar taker, BattleChar user, string buffkey)//追加射击
        {
            taker.BuffAdd(buffkey, user, false, 999, false, -1, false);

            yield return new WaitForSecondsRealtime(0.0f);
            yield break;
        
        }

        public void BuffAdd_Before(BattleChar BC, string key, BattleChar UseState, bool hide, int PlusTagPer, bool debuffnonuser, int RemainTime, bool StringHide)
        {
            GDEBuffData gdebuffData = new GDEBuffData(key);
            int frameCount = Time.frameCount;
            //Debug.Log("time:" + Time.frameCount);
            //Debug.Log("timelast:" + BC.BuffCheck.TimeSeed);
            if (this.pflag && BC == this.BChar && gdebuffData.Debuff && !gdebuffData.Hide && !hide && (this.pkey == key && this.ptime == frameCount))//&& BC.BuffCheck.PlusResist)
            {


                BuffTag buffTag = new BuffTag();
                buffTag.User = UseState;
                buffTag.BuffData = gdebuffData;

                this.skill_Extended.TargetBuff.Add(buffTag.Clone());
            }

        }

        public Skill_Extended skill_Extended = new Skill_Extended();

        public bool pflag = false;
        public string pkey;
        public int ptime;

    }
    */
    public class Bcl_int_14_1 : Buff,IP_BuffAdd_Before
    {
        public void BuffAdd_Before(BattleChar BC, string key, BattleChar UseState, bool hide, int PlusTagPer, bool debuffnonuser, int RemainTime, bool StringHide)
        {
            GDEBuffData gdebuffData = new GDEBuffData(key);
            int frameCount = Time.frameCount;
            //Debug.Log("time:"+ Time.frameCount);
            //Debug.Log("timelast:" + BC.BuffCheck.TimeSeed);
            if (  BC == this.BChar && gdebuffData.Debuff && gdebuffData.BuffTag.Key != "null" && !gdebuffData.Hide && !hide && !(BC.BuffCheck.Lastbuff == key && BC.BuffCheck.TimeSeed == frameCount))
            {
                this.pkey = key;
                this.ptime = frameCount;
                
                LastBuffRsisCheck lastBuffRsisCheck = new LastBuffRsisCheck();
                lastBuffRsisCheck.TimeSeed = Time.frameCount;
                lastBuffRsisCheck.Lastbuff = key;
                lastBuffRsisCheck.IsAdd = false;
                BC.BuffCheck = lastBuffRsisCheck;
                BC.BuffCheck.PlusResist = true;
                this.BChar.SimpleTextOut(ScriptLocalization.UI_Battle.DebuffGuard);

                if (!this.BChar.BuffFind("B_SF_Intruder_14", false))
                {
                    this.BChar.BuffAdd("B_SF_Intruder_14", this.BChar, false, 0, false, -1, false);

                }
                Bcl_int_14 bcl14 = this.BChar.BuffReturn("B_SF_Intruder_14", false) as Bcl_int_14;

                Skill_Extended skill_Extended = new Skill_Extended();
                skill_Extended.TargetBuff = new List<BuffTag>();
                BuffTag buffTag = new BuffTag();
                buffTag.User = UseState;
                buffTag.BuffData = gdebuffData;
                skill_Extended.TargetBuff.Add(buffTag.Clone());
                bcl14.skill_Extended = skill_Extended;

                bcl14.pflag = true;
                bcl14.pkey = key;
                bcl14.ptime = frameCount;
                //bcl14.skill_Extended.TargetBuff.Add(buffTag.Clone());
                //this.pflag = true;

                this.SelfDestroy();
            }
            //else if (false && this.pflag && BC == this.BChar && gdebuffData.Debuff && !gdebuffData.Hide && !hide && (this.pkey == key && this.ptime == frameCount) )//&& BC.BuffCheck.PlusResist)
            //{
                //if (!this.BChar.BuffFind("B_SF_Intruder_14", false))
                //{
                    //this.BChar.BuffAdd("B_SF_Intruder_14", this.BChar, false, 0, false, -1, false);
                //}
                //Bcl_int_14 bcl14 = this.BChar.BuffReturn("B_SF_Intruder_14", false) as Bcl_int_14;

                //BuffTag buffTag = new BuffTag();
                //buffTag.User = UseState;
                //buffTag.BuffData = gdebuffData;

                //bcl14.skill_Extended.TargetBuff.Add(buffTag.Clone());
            //}

        }



        public string pkey;
        public int ptime;
    }
    
    public class SkillEn_SF_Intruder_0 : Sex_ImproveClone
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return true;
        }
        public override void Init()
        {
            base.Init();
            this.CanUseStun = true;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            bool hascant=false;
            for (int i = 0; i < SkillD.Master.Buffs.Count; i++)
            {
                if (!SkillD.Master.Buffs[i].BuffData.Hide && !SkillD.Master.Buffs[i].BuffData.Cantdisable && SkillD.Master.Buffs[i].PlusStat.Stun)
                {
                    SkillD.Master.Buffs[i].SelfDestroy(false);
                }
                else if(SkillD.Master.Buffs[i].PlusStat.Stun)
                {
                    hascant = true;
                }
            }

            if(hascant)
            {
                SkillD.Master.BuffAdd("B_SF_Intruder_ex0", this.BChar, false, 0, false, -1, false);
            }
        }
    }
    public class Bcl_int_ex0 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.LucySkillExBuff = (BuffSkillExHand)Skill_Extended.DataToExtended("EX_SF_Intruder_ex0");
        }
        public override bool CanSkillBuffAdd(Skill AddedSkill, int Index)
        {
            return AddedSkill.Master == this.BChar;
        }
    }
    public class Extended_int_ex0 : BuffSkillExHand
    {
        public override void Init()
        {
            base.Init();
            this.CanUseStun = true;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            BattleSystem.DelayInputAfter(this.Del());
        }

        // Token: 0x060027FC RID: 10236 RVA: 0x0010514E File Offset: 0x0010334E
        public IEnumerator Del()
        {
            this.MainBuff.SelfStackDestroy();
            yield break;
        }
    }


    public class SkillEn_SF_Intruder_1 : Sex_ImproveClone, IP_DamageChange
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return (MainSkill.TargetTypeKey == "enemy" || MainSkill.TargetTypeKey == "random_enemy" || MainSkill.TargetTypeKey == "all_onetarget")&& MainSkill.IsDamage;
        }

        public override void Init()
        {
            base.Init();    
            this.OnePassive = true;
        }
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            
            if (Target.GetStat.Stun)
            {
                Damage += Damage;
            }
            return Damage;
        }
    }

    public class E_int_0 : EquipBase, IP_HackAdd_Before
    {
        public override void Init()
        {
            base.Init();
            //this.PlusPerStat.Damage = 0;
            this.PlusPerStat.Heal = 15;
            this.PlusPerStat.Damage = 10;
            //this.PlusStat.HIT_CC = 20;
            this.PlusStat.HIT_DEBUFF = 20;

        }
        public void HackAdd_Before(BattleChar taker, BattleChar user, Buff buff, ref int addNum)
        {
            Bcl_int_p bclp=buff as Bcl_int_p;
            bclp.recordon = true;
        }
    }

    public class E_int_1 : EquipBase, IP_Heal_Before
    {
        public override void Init()
        {
            base.Init();

            this.PlusStat.cri = 50;
            this.PlusStat.PlusCriHeal = -50;
        }
        public void Heal_Before(BattleChar Taker, BattleChar User, float Hel, bool Cri, ref bool Force, BattleChar.ChineHeal heal)
        {
            if(User==this.BChar && Cri)
            {
                Force = true;
            }
        }
    }

    /*
    public class Item_SF_1 : UseitemBase
    {

        public override bool Use(Character CharInfo)
        {
            List<Skill> list = new List<Skill>();
            foreach (Skill skill in CharInfo.GetBattleChar.Skills)
            {
                if (!skill.Enforce && !skill.Enforce_CantUse && !skill.Enforce_Weak && skill.MySkill.Category.Key != GDEItemKeys.SkillCategory_DefultSkill && skill.MySkill.Category.Key != GDEItemKeys.SkillCategory_PublicSkill)
                {
                    list.Add(skill);
                }
            }
            if (list.Count >= 1)
            {
                this.pinfo = CharInfo;

                GDESkillKeywordData k1 = new GDESkillKeywordData("KeyWord_SF_0");
                SelectSkillList.NewSelectSkillList(false, k1.Desc, list, new SkillButton.SkillClickDel(this.ButtonDel), true, true, false, true);
                return true;
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
            Mybutton.Myskill.Master.Info.SkillDatas.Remove(item);
            //Mybutton.Myskill.Master.Info.SkillAdd(new GDESkillData(GDEItemKeys.Skill_S_SacrificeSkill), null);

            List<Skill> list = new List<Skill>();
            List<GDESkillData> characterSkillNoOverLap = PlayData.GetCharacterSkillNoOverLap(this.pinfo, rare, null);
            for (int j = 0; j < characterSkillNoOverLap.Count; j++)
            {
                list.Add(Skill.TempSkill(characterSkillNoOverLap[j].KeyID, (this.pinfo.GetBattleChar) as BattleAlly, PlayData.TempBattleTeam));
            }

            foreach (Skill skill in list)
            {
                if (!SaveManager.IsUnlock(skill.MySkill.KeyID, SaveManager.NowData.unlockList.SkillPreView))
                {
                    SaveManager.NowData.unlockList.SkillPreView.Add(skill.MySkill.KeyID);
                }
            }

            FieldSystem.DelayInput(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(this.SkillAdd), ScriptLocalization.System_Item.SkillAdd, false, true, true, true, false));
        }
        public void SkillAdd(SkillButton Mybutton)
        {
            Mybutton.Myskill.Master.Info.UseSoulStone(Mybutton.Myskill);
            UIManager.inst.CharstatUI.GetComponent<CharStatV4>().SkillUPdate();
        }

        private Character pinfo = new Character();
    }*/
}
