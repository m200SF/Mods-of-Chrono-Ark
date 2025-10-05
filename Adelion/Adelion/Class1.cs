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
using System.Diagnostics.Eventing.Reader;
using static UnityEngine.Random;
using SF_Standard;
using System.Xml.Serialization;

namespace PD_Adeline
{
    [PluginConfig("M200_PD_Adeline_CharMod", "PD_Adeline_CharMod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class PD_Adeline_CharacterModPlugin : ChronoArkPlugin
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
        public class AdelineDef : ModDefinition
        {
        }

        public class S_PD_Adeline_6_Particle : CustomSkillGDE<PD_Adeline_CharacterModPlugin.AdelineDef>
        {
            // Token: 0x060000AB RID: 171 RVA: 0x000055A0 File Offset: 0x000037A0
            public override ModGDEInfo.LoadingType GetLoadingType()
            {
                return ModGDEInfo.LoadingType.Replace;
            }

            // Token: 0x060000AC RID: 172 RVA: 0x000055B4 File Offset: 0x000037B4
            public override string Key()
            {
                return "S_PD_Adeline_6";
            }

            // Token: 0x060000AD RID: 173 RVA: 0x000055CB File Offset: 0x000037CB
            public override void SetValue()
            {
                base.Particle = base.assetInfo.CodeConstuctAsync<GameObject>("Particle/IronHeart/IronHeart_new_1", new ModAssetInfo.ObjectChangeDelegate<GameObject>(this.ParticleChangeDelegate));//"Particle/Twins/Hel_2"
            }

            // Token: 0x060000AE RID: 174 RVA: 0x000055F4 File Offset: 0x000037F4
            public void ParticleColorChange(Transform target, Color color)
            {
                ParticleSystem component = target.GetComponent<ParticleSystem>();
                UnityEngine.ParticleSystem.MainModule i = component.main;
                i.startColor = color;
            }

            // Token: 0x060000AF RID: 175 RVA: 0x00005620 File Offset: 0x00003820
            public GameObject ParticleChangeDelegate(GameObject particle)
            {

                if (BattleSystem.instance == null)
                {
                    particle.SetActive(false);

                }


                ParticleColorChange(particle.transform.GetChild(0), new Color(1, 0.2f, 0.2f, 0.7254902f));
                ParticleColorChange(particle.transform.GetChild(0).GetChild(0), new Color(1, 0.3f, 0.3f, 1));
                ParticleColorChange(particle.transform.GetChild(1).GetChild(0), new Color(1, 0.6557567f, 0, 1));
                ParticleColorChange(particle.transform.GetChild(1).GetChild(1), new Color(1, 0.7434377f, 0.16f, 0.682353f));
                ParticleColorChange(particle.transform.GetChild(1).GetChild(2).GetChild(0), new Color(1, 0.7035021f, 0.67f, 1));
                ParticleColorChange(particle.transform.GetChild(1).GetChild(5), new Color(1, 0.8370219f, 0.4f, 0.6f));
                ParticleColorChange(particle.transform.GetChild(1).GetChild(6), new Color(1, 1, 1, 1));
                ParticleColorChange(particle.transform.GetChild(1).GetChild(2).GetChild(1), new Color(1, 1, 1, 1));
                ParticleColorChange(particle.transform.GetChild(1).GetChild(2), new Color(1, 0.2f, 0.2f, 1));
                return particle;
            }



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
            var methodInfo = AccessTools.Method(typeof(Camp_Adeline_1), nameof(Camp_Adeline_1.Check1), new System.Type[] { typeof(CharSelect_CampUI), typeof(GDECharacterData) });
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
                    
                    if (f0 && f1 && f2 && f3 && f4 && f5 &&f6)
                    {
                        //Debug.Log("艾德琳_change!"+i);

                        yield return codes[i];

                        yield return new CodeInstruction(OpCodes.Ldarg_0);
                        yield return new CodeInstruction(OpCodes.Ldloc_S, 13);
                        yield return new CodeInstruction(OpCodes.Call, methodInfo);
                        //yield return codes[i];
                        yield return new CodeInstruction(OpCodes.Stloc_S, 13);
                        //Camp_Adeline_1.pflag = true;
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
                if (i < codes.Count-6)
                {
                    bool f1 = codes[i ].opcode == OpCodes.Ldarg_0;
                    bool f2 = codes[i +1].opcode == OpCodes.Ldfld;
                    bool f3 = codes[i +2].opcode == OpCodes.Ldarg_0;
                    bool f4 = codes[i + 3].opcode == OpCodes.Ldfld;
                    bool f5 = codes[i +4].opcode == OpCodes.Call;
                    bool f6 = codes[i +5].opcode == OpCodes.Callvirt;
                    bool f7 = codes[i+6].opcode == OpCodes.Stloc_S;




                    if (f1 && f2 && f3 && f4 && f5 && f6 && f7)
                    {
                        //Debug.Log("艾德琳_change!:"+i);

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
    public static class Camp_Adeline_1
    {
        public static GDECharacterData Check1(CharSelect_CampUI CSC, GDECharacterData gDECharacterData)
        {
            //Debug.Log("选人艾德琳1");
            if (PlayData.TSavedata.Party.Find(a => a.GetData.Key == "PD_Alina") != null && PlayData.TSavedata.Party.Find(a => a.GetData.Key == "PD_Adeline") == null && CSC.CharList.Find(a => a.data.Key == "PD_Adeline") == null && CSC.CharDatas.Find(a => a.Key == "PD_Adeline") != null)
            {
                //Debug.Log("选人艾德琳2");
                GDECharacterData gde = CSC.CharDatas.Find(a => a.Key == "PD_Adeline");
                if (gde != null)
                {
                    //Debug.Log("选人艾德琳:" + gde.Key);
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
            //MasterAudio.PlaySound("A_adl_4", 1f, null, 0f, null, null, false, false);
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

            if (inText.Contains("T_adl_1_1"))
            {
                inText = inText.Replace("T_adl_1_1:", string.Empty);
                MasterAudio.PlaySound("A_adl_1_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_adl_2_1"))
            {
                inText = inText.Replace("T_adl_2_1:", string.Empty);
                MasterAudio.PlaySound("A_adl_2_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_adl_2_2"))
            {
                inText = inText.Replace("T_adl_2_2:", string.Empty);
                MasterAudio.PlaySound("A_adl_2_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_adl_3_1"))
            {
                inText = inText.Replace("T_adl_3_1:", string.Empty);
                MasterAudio.PlaySound("A_adl_3_1", volume, null, 1f, null, null, false, false);
            }
            else if (inText.Contains("T_adl_4_1"))
            {
                inText = inText.Replace("T_adl_4_1:", string.Empty);
                MasterAudio.PlaySound("A_adl_4_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_adl_5_1"))
            {
                inText = inText.Replace("T_adl_5_1:", string.Empty);
                MasterAudio.PlaySound("A_adl_5_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_adl_5_2"))
            {
                inText = inText.Replace("T_adl_5_2:", string.Empty);
                MasterAudio.PlaySound("A_adl_5_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_adl_6_1"))
            {
                inText = inText.Replace("T_adl_6_1:", string.Empty);
                MasterAudio.PlaySound("A_adl_6_1", volume, null, 1f, null, null, false, false);
            }
            else if (inText.Contains("T_adl_6_2"))
            {
                inText = inText.Replace("T_adl_6_2:", string.Empty);
                MasterAudio.PlaySound("A_adl_6_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_adl_7_1"))
            {
                inText = inText.Replace("T_adl_7_1:", string.Empty);
                MasterAudio.PlaySound("A_adl_7_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_adl_8_1"))
            {
                inText = inText.Replace("T_adl_8_1:", string.Empty);
                MasterAudio.PlaySound("A_adl_8_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_adl_9_1"))
            {
                inText = inText.Replace("T_adl_9_1:", string.Empty);
                MasterAudio.PlaySound("A_adl_9_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_adl_11_1"))
            {
                inText = inText.Replace("T_adl_11_1:", string.Empty);
                MasterAudio.PlaySound("A_adl_11_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_adl_11_2"))
            {
                inText = inText.Replace("T_adl_11_2:", string.Empty);
                MasterAudio.PlaySound("A_adl_11_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_adl_11_3"))
            {
                inText = inText.Replace("T_adl_11_3:", string.Empty);
                MasterAudio.PlaySound("A_adl_11_3", volume, null, 0f, null, null, false, false);
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
            bool flag = inputitem.itemkey == "E_PD_Adeline_0" && __instance.Info.KeyData != "PD_Adeline";
            if (flag)
            {
                __result = true;
            }
            //Debug.Log("成功进入OnDropSlot的patch");
        }
    }
    /*
    [HarmonyPatch(typeof(FriendShipUI))]
    [HarmonyPatch("FriendShipLevelUp")]
    public static class FriendShipLevelUpPlugin
    {
        [HarmonyPostfix]
        public static void FriendShipLevelUp_AfterPatch(FriendShipUI __instance, string CharId, int NowLV)
        {
            
            if (CharId == "PD_Adeline" && NowLV >= 3)
            {
                Statistics_Character sc = SaveManager.NowData.statistics.GetCharData(CharId);
                //Debug.Log("友谊等级：" + sc.FriendshipLV);
                //Debug.Log("友谊进度：" + sc.FriendshipAmount);
                //Debug.Log("深红对话：" + sc.IsCrimsonDialogue);
                if (NowLV == 3)
                {
                    //Debug.Log("梦想家升级");
                    sc.IsCrimsonDialogue = true;
                    UIManager.InstantiateActiveAddressable(UIManager.inst.CompleteUI, AddressableLoadManager.ManageType.EachStage).GetComponent<FriendShipCompleteUI>().MaxLevel(CharId);
                }
            }
            
        }

    }
    */
    [HarmonyPatch(typeof(FriendShipUI))]
    [HarmonyPatch("Gift")]
    public static class GiftPlugin
    {
        [HarmonyPostfix]
        public static void Gift_AfterPatch(FriendShipUI __instance, string CharId)
        {

            if (CharId == "PD_Adeline")
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

    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("Effect")]
    public static class EffectPlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void Effect_Before_patch(SkillParticle SP, bool Dodge)
        {
            EffectPlugin.effecting = true;
        }
        [HarmonyFinalizer]
        public static void Effect_After_patch(SkillParticle SP, bool Dodge)
        {
            EffectPlugin.effecting = false;
        }
        public static bool effecting = false;
    }

            
    [HarmonyPatch(typeof(Skill))]
    [HarmonyPatch("GetCriPer")]
    public static class GetCriPerPlugin
    {


        // Token: 0x06000015 RID: 21 RVA: 0x00002410 File Offset: 0x00000610
        [HarmonyFinalizer]
        public static void GetCriPer_Patch(Skill __instance,ref int __result, BattleChar Target, int Plus )
        {
            if(BattleSystem.instance!=null && EffectPlugin.effecting)// BattleSystem.instance.Particles.Find(a=>a.SkillData==__instance)!=null )
            {
                //Debug.Log("GetCriPer_Patch_1:"+ Target.Info.Name);
                int overhit = 0;
                int overcri = 0;

                GDESkillEffectData effect_Target = __instance.MySkill.Effect_Target;
                if (__instance.Master != Target.BattleInfo.DummyChar)
                {
                    float num = Misc.PerToNum(__instance.Master.GetStat.hit, (float)effect_Target.HIT + __instance.PlusSkillStat.hit);
                    num -= Target.GetStat.dod;
                    overhit= (int)Math.Max(num - 100, 0);
                }
                else
                {
                    overhit = 0;
                }

                overcri = Math.Max(__result - 100, 0);

                foreach (IP_GetCriPer_Final ip_before in BattleSystem.instance.IReturn<IP_GetCriPer_Final>())
                {
                    //Debug.Log("only damage drm");
                    bool flag = ip_before != null;
                    if (flag)
                    {
                        ip_before.GetCriPer_Final(__instance, overcri,overhit,Target);
                    }
                }
            }
        }
    }
    public interface IP_GetCriPer_Final
    {
        // Token: 0x06000001 RID: 1
        void GetCriPer_Final(Skill skill,int overcri,int overhit, BattleChar Target);
    }
    

    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("Damage")]
    public static class DamagePlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPostfix]
        public static void EffectView_Damage_After_patch(BattleChar __instance,BattleChar User, int Dmg, bool Cri, bool Pain, bool NOEFFECT, int PlusPenetration , bool IgnoreHealingPro , bool HealingPro, bool OnlyUnscaleTime)
        {

                foreach (IP_BattleChar_Damage_After ip_before in BattleSystem.instance.IReturn<IP_BattleChar_Damage_After>())
                {
                    //Debug.Log("only damage drm");
                    bool flag = ip_before != null;
                    if (flag)
                    {
                        ip_before.BattleChar_Damage_After(__instance,  User, Dmg,Cri,  Pain,  NOEFFECT, PlusPenetration, IgnoreHealingPro, HealingPro,  OnlyUnscaleTime);
                    }
                }
                //Debug.Log("成功进入OnlyDamage前的patch");
            

        }
    }
    public interface IP_BattleChar_Damage_After
    {
        // Token: 0x06000001 RID: 1
        void BattleChar_Damage_After(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool Pain, bool NOEFFECT, int PlusPenetration, bool IgnoreHealingPro, bool HealingPro, bool OnlyUnscaleTime);
    }
    [HarmonyPatch(typeof(BattleAlly))]
    [HarmonyPatch("UseSkillAfter")]
    public static class UseSkillAfterPlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void UseSkillAfter_Before(BattleAlly __instance, Skill skill)
        {
            if (!skill.IsNowCasting)
            {
                foreach (IP_UseSkillAfter_Before_Patch ip_patch in skill.IReturn<IP_UseSkillAfter_Before_Patch>())
                {
                    //Debug.Log("only damage drm");
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.UseSkillAfter_Before_Patch(__instance, skill);
                    }
                }
            }

        }
        [HarmonyPostfix]
        public static void UseSkillAfter_After(BattleAlly __instance, Skill skill)
        {
            if (!skill.IsNowCasting)
            {
                foreach (IP_UseSkillAfter_After_Patch ip_patch in skill.IReturn<IP_UseSkillAfter_After_Patch>())
                {
                    //Debug.Log("only damage drm");
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.UseSkillAfter_After_Patch(__instance, skill);
                    }
                }
            }

        }
    }
    public interface IP_UseSkillAfter_Before_Patch
    {
        // Token: 0x06000001 RID: 1
        void UseSkillAfter_Before_Patch(BattleAlly user, Skill skill);
    }
    public interface IP_UseSkillAfter_After_Patch
    {
        // Token: 0x06000001 RID: 1
        void UseSkillAfter_After_Patch(BattleAlly user, Skill skill);
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
        void SkillUsePatch_Before(BattleChar TargetChar,Skill TargetSkill);
    }



    public class P_Adeline : Passive_Char, IP_ParticleOut_After, IP_CriPerChange, IP_LevelUp
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
                list.Add(ItemBase.GetItem("E_PD_Adeline_0", 1));
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
        public bool ItemTake2=false;
        public IEnumerator ParticleOut_After(Skill SkillD, List<BattleChar> Targets)//mark
        {
            if (SkillD.IsDamage && !SkillD.PlusHit && SkillD.Master==this.BChar)
            {
                foreach (BattleChar bc in Targets)
                {
                    bc.BuffAdd("B_PD_Adeline_p", this.BChar, false, 0, false, -1, false);
                }
            }
            yield break;
        }
        
        public void CriPerChange(Skill skill, BattleChar Target, ref float CriPer)
        {
            
            if(skill.Master==this.BChar && Target!=null && Target.BuffFind("B_PD_Adeline_p",false))
            {
                int num = Target.BuffReturn("B_PD_Adeline_p", false).StackNum;
                CriPer = (float)(CriPer + 15 * num);

                this.PlusStat.PlusCriDmg=10* num;
            }
            else
            {
                this.PlusStat.PlusCriDmg = 0;
            }

        }

    }
    public interface IP_CheckEquipBlack
    {
        bool CheckEquipBlack();
    }

    public class Bcl_adl_p : Buff,IP_BuffAdd,IP_BuffAddAfter, IP_TurnEnd, IP_BuffUpdate
    {
        public override void Init()
        {
            base.Init();
            this.BuffUpdate(this);
        }
        public void BuffUpdate(Buff MyBuff)
        {
            bool flag1 = false;
            GDEBuffData gde=new GDEBuffData("B_PD_Adeline_p");
            //GDEBuffData gde = new GDEBuffData("B_PD_Adeline_p").ShallowClone();
            if (BattleSystem.instance!=null)
            {
                
                foreach (IP_BlackMarkRemain ip_patch in BattleSystem.instance.IReturn<IP_BlackMarkRemain>())
                {
                    //Debug.Log("only damage drm");
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        flag1=flag1 || ip_patch.BlackMarkRemain();
                    }
                }
            }
            if(flag1)
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
                //this.BuffData.MaxStack = gde.MaxStack ;

                Traverse.Create(this.BuffData).Field("_MaxStack").SetValue(gde.MaxStack);

                //this.BuffData.LifeTime = gde.LifeTime ;
            }
        }
        public int SumDamage()
        {
            float sumdmg = 0;
            foreach (StackBuff sta in this.StackInfo)
            {
                float part1dmg = (float)(0.3 * sta.UseState.GetStat.atk);
                float part2dmg = (float)(Math.Min(this.BChar.GetStat.maxhp * 0.01, 0.5 * sta.UseState.GetStat.atk));
                if(this.usedstacks.Contains(sta))
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
            return base.DescExtended().Replace("&a",sumdmg.ToString());
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
            this.PlusStat.def = -5 * num;
            this.PlusStat.dod= -5 * num;

            //this.PlusStat.CRIGetDMG = 15 * num;
        }
        public bool RemainMark()
        {
            bool flag1 = false;
            if (BattleSystem.instance != null)
            {

                foreach (IP_BlackMarkRemain ip_patch in BattleSystem.instance.IReturn<IP_BlackMarkRemain>())
                {
                    //Debug.Log("only damage drm");
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        flag1 = flag1 || ip_patch.BlackMarkRemain();
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
            
            if(addedbuff==this)
            {
                bool flag1 = false;
                flag1=this.RemainMark();
                if(flag1)
                {
                    stackBuff.RemainTime++;
                    //stackBuff.RemainTime = 0;
                }
            }
            
            if(this.adding)
            {
                this.adding = false;
                if (BuffTaker==this.BChar && addedbuff.BuffData.Key == "B_PD_Alina_p")
                {
                    int sumdmg = 0;
                    sumdmg += this.SumDamage();
                    if (sumdmg > 0)
                    {
                        BattleSystem.DelayInput(this.ActiveDamage(sumdmg, stackBuff.UseState));
                        //this.BChar.Damage(BattleSystem.instance.DummyChar, sumdmg, false, false, false, 100, false, false, false);
                        //this.SelfDestroy();
                        this.DamageAfter();
                    }
                }
            }
        }
        public IEnumerator ActiveDamage(int dmg, BattleChar bc)
        {
            yield return new WaitForFixedUpdate();
            Skill skill2 = Skill.TempSkill("S_PD_Adeline_m", bc, bc.MyTeam);
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
            for (int i = 0; i < this.usedstacks.Count;i++)
            {
                if (!this.StackInfo.Contains(this.usedstacks[i]))
                {
                    this.usedstacks.RemoveAt(i);
                    i--;
                }
            }

            for(int i = 0;i < this.StackInfo.Count; i++)
            {
                /*
                bool remain = false;
                remain = this.RemainMark();
                if(remain)
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
            if(this.StackInfo.Count<=0)
            {
                this.SelfDestroy();
            }

        }
        private bool adding = false;
        private List<StackBuff> usedstacks = new List<StackBuff>();
    }

    public interface IP_BlackMarkRemain
    {
        // Token: 0x06000001 RID: 1
        bool BlackMarkRemain(StackBuff sta=null);
    }

    public class Sex_adl_1: Sex_ImproveClone, IP_SkillUsePatch_Before
    {
        public override string DescExtended(string desc)
        {
            if (BattleSystem.instance != null && this.tar.Count>0)
            {
                return base.DescExtended(desc).Replace("&a", this.tar[0].Info.Name);
            }
            return base.DescExtended(desc).Replace("&a", "");
        }
        public override bool TargetSelectExcept(BattleChar ExceptTarget)
        {
            if(this.repeating)
            {
                return !this.tar.Contains(ExceptTarget);
            }
            else
            {
                return false;
            }

        }
        
        //public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        public void SkillUsePatch_Before(BattleChar TargetChar, Skill TargetSkill)
        {
            if(TargetChar == null)
            {
                return;
            }
            Skill skill = Skill.TempSkill("S_PD_Adeline_1", this.BChar, this.BChar.MyTeam);
            //Skill skill = this.MySkill.CloneSkill(true, null, null, true);

            skill.isExcept = true;
            skill.AutoDelete = 1;
            skill.BasicSkill = false;


            for (int i = 0; i < skill.AllExtendeds.Count; i++)
            {
                if(skill.AllExtendeds[i] is Sex_adl_1)
                {
                    //Debug.Log("SetTarget");
                    Sex_adl_1 sex = skill.AllExtendeds[i] as Sex_adl_1;
                    sex.repeating = true;
                    sex.tar=new List<BattleChar>();
                    //sex.tar.AddRange(Targets);
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
        public bool repeating = false;
        public List<BattleChar> tar=new List<BattleChar>();
    }
    public class Sex_adl_2 : Sex_ImproveClone, IP_DamageChange_sumoperation,IP_BattleChar_Damage_After
    {
        public override string DescExtended(string desc)
        {
            int dmg = 0;
            if (BattleSystem.instance != null && this.MySkill.IsNowCounting)
            {
                if(BattleSystem.instance.CastSkills.Find((CastingSkill a) => a.skill == this.MySkill) != null )
                {
                    CastingSkill cast = BattleSystem.instance.CastSkills.Find((CastingSkill a) => a.skill == this.MySkill);
                    if(this.records.Find(a => a.target == cast.Target) != null)
                    {
                        dmgRec rec = this.records.Find(a => a.target == cast.Target);
                        dmg = (int)(0.5 * rec.sumDmg);
                    }
                }
                else if( BattleSystem.instance.SaveSkill.Find((CastingSkill a) => a.skill == this.MySkill) != null)
                {
                    CastingSkill cast = BattleSystem.instance.SaveSkill.Find((CastingSkill a) => a.skill == this.MySkill);
                    if (this.records.Find(a => a.target == cast.Target) != null)
                    {
                        dmgRec rec = this.records.Find(a => a.target == cast.Target);
                        dmg = (int)(0.5 * rec.sumDmg);
                    }
                }
            }
                return base.DescExtended(desc).Replace("&a",dmg.ToString());
        }
        public override void Init()
        {
            base.Init();
            this.OnePassive = true;
            this.CountingExtedned = true;
        }
        public void BattleChar_Damage_After(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool Pain, bool NOEFFECT, int PlusPenetration, bool IgnoreHealingPro, bool HealingPro, bool OnlyUnscaleTime)
        {
            if (this.MySkill.IsNowCounting)
            {
                if (this.records.Find(a => a.target == Hit) != null)
                {
                    dmgRec rec = this.records.Find(a => a.target == Hit);
                    rec.sumDmg += Dmg;
                }
                else
                {
                    dmgRec rec = new dmgRec();
                    rec.target = Hit;
                    rec.sumDmg += Dmg;
                    this.records.Add(rec);
                }
            }

        }
        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (SkillD==this.MySkill)
            {
                if (this.records.Find(a => a.target == Target) != null)
                {

                    dmgRec rec = this.records.Find(a => a.target == Target);
                    PlusDamage =(int) (0.5*rec.sumDmg);
                }
            }
        }
        public bool counting = false;
        public List<dmgRec> records = new List<dmgRec>();
    }
    public class dmgRec
    {
        public BattleChar target=new BattleChar();
        public int sumDmg = 0;
    }

    public class Sex_adl_3 : Sex_ImproveClone, IP_DamageChange//, IP_BuffUpdate
    {
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if(SkillD==this.MySkill)
            {
                int num = this.PlusPer(Target);
                int dmg = (int)(Damage * (1 + 0.01 * num));
                return dmg;
            }
            return Damage;
        }
        /*
        public void BuffUpdate(Buff MyBuff)
        {
            if (active)
            {
                this.Refresh();
            }
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            this.active = false;
            this.Refresh();

            foreach (BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {
                if (!Targets.Contains(bc))
                {
                    foreach (Buff buff in bc.Buffs)
                    {
                        if (buff.BuffData.Key == "B_PD_Adeline_p")
                        {
                            buff.SelfDestroy();
                        }
                    }
                }
            }
        }
        */
        public int PlusPer(BattleChar target)
        {
            int num = 0;
            foreach (BattleChar bc in target.MyTeam.AliveChars_Vanish)
            {
                foreach (Buff buff in bc.Buffs)
                {
                    if (buff.BuffData.Key == "B_PD_Adeline_p" || buff.BuffData.Key == "B_PD_Alina_p")
                    {
                        if(bc==target)
                        {
                            num += (12*buff.StackNum);
                        }
                        else
                        {
                            num += (12 * buff.StackNum);
                        }
                    }
                }
            }
            return num;
            //this.PlusPerStat.Damage = 30 * num;
        }
        
        public bool active = true;
    }

    public class Sex_adl_4 : Sex_ImproveClone
    {

    }
    public class Bcl_adl_4 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.hit = -(int)(0.8 * this.Usestate_L.GetStat.atk);
            this.PlusStat.dod = -(int)(0.8 * this.Usestate_L.GetStat.atk);
            //this.PlusPerStat.Damage = -(int)(0.4 * this.Usestate_L.GetStat.atk);
        }
    }
    public class Sex_adl_5 : Sex_ImproveClone
    {
        
        public override void Special_PointerEnter(BattleChar Char)
        {
            base.Special_PointerEnter(Char);
            int num = 0;
            if(Char.BuffFind("B_PD_Adeline_p",false))
            {
                num = 25 * Char.BuffReturn("B_PD_Adeline_p", false).StackNum;
            }
            this.PlusSkillStat.HIT_CC = num;
            this.PlusSkillStat.HIT_DEBUFF = num;
            this.PlusSkillStat.HIT_DOT = num;
        }
        public override void Special_PointerExit()
        {
            base.Special_PointerExit();
            this.PlusSkillStat.HIT_CC = 0;
            this.PlusSkillStat.HIT_DEBUFF = 0;
            this.PlusSkillStat.HIT_DOT = 0;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            int num = 0;
            if (Targets[0].BuffFind("B_PD_Adeline_p", false))
            {
                num = 25 * Targets[0].BuffReturn("B_PD_Adeline_p", false).StackNum;
            }
            this.PlusSkillStat.HIT_CC = num;
            this.PlusSkillStat.HIT_DEBUFF = num;
            this.PlusSkillStat.HIT_DOT = num;
        }
    }

    public class Bcl_adl_5_2 : Buff,IP_BuffUpdate//, IP_DamageTake
    {
        // Token: 0x06000FE6 RID: 4070 RVA: 0x0008DD22 File Offset: 0x0008BF22
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a",this.DmgPlusStack.ToString());
        }
        public override void BuffStat()
        {
            base.BuffStat();
            float num = 0;
            if (this.BChar.BuffFind("B_PD_Adeline_p", false))
            {
                num = this.DmgPlusStack * this.BChar.BuffReturn("B_PD_Adeline_p", false).StackNum;
            }

            this.PlusStat.DMGTaken = this.DmgPlusStack * 2 + num;
            this.PlusStat.dod = -this.DmgPlusStack * 2 - num;
        }
        public void BuffUpdate(Buff MyBuff)
        {
            float num = 0;
            if (this.BChar.BuffFind("B_PD_Adeline_p", false))
            {
                num =  this.DmgPlusStack * this.BChar.BuffReturn("B_PD_Adeline_p", false).StackNum;
            }
            this.PlusStat.DMGTaken = this.DmgPlusStack*2 + num;
            this.PlusStat.dod = -this.DmgPlusStack*2 - num;
        }

        private int DmgPlusStack
        {
            get
            {
                return (int)(0.3 * this.Usestate_L.GetStat.atk);
            }
        }
        /*
        // Token: 0x06000FE7 RID: 4071 RVA: 0x0007ECD5 File Offset: 0x0007CED5
        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            base.SelfStackDestroy();
        }
        */
    }

    public class Sex_adl_6 : Sex_ImproveClone, IP_DamageChange_sumoperation
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(0.5 * this.MySkill.TargetDamageOriginal)).ToString());
        }
        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (SkillD == this.MySkill)
            {
                PlusDamage = this.PlusDamage(Target);
            }
        }
        public int PlusDamage(BattleChar target)
        {
            float part1 = 0;
            float part2 = 0;
            if(target.BuffFind("B_PD_Adeline_p",false))
            {
                part1 = (float)(0.05 * target.BuffReturn("B_PD_Adeline_p", false).StackNum) * (target.GetStat.maxhp - target.HP);
            }
            if (target.BuffFind("B_PD_Alina_p", false))
            {
                double limit = (0.5 * target.BuffReturn("B_PD_Alina_p", false).StackNum* this.MySkill.TargetDamageOriginal);
                part2 = (float)Math.Min((0.05 * target.BuffReturn("B_PD_Alina_p", false).StackNum) * target.HP, limit);
            }
            return (int)(part1+part2);
        }


    }
    public class Sex_adl_7 : Sex_ImproveClone, /*IP_ParticleOut_After,*/ IP_SkillCastingQuit,IP_SkillCastingStart, IP_SkillUse_Team,IP_TurnEnd
    {
        
        public override void Init()
        {
            base.Init();
            //this.OnePassive = true;
            this.CountingExtedned = true;
        }
        public void TurnEnd()
        {
            //Debug.Log("行动数1：" + BattleSystem.instance.AllyTeam.TurnActionNum);
            //Debug.Log("行动数2：" + BattleSystem.instance.EnemyTeam.TurnActionNum);
            if(this.MySkill.IsNowCasting)
            {
                this.startActionNum -= BattleSystem.instance.AllyTeam.TurnActionNum;
            }
        }
        public void SkillCasting(CastingSkill ThisSkill)
        {
            if(ThisSkill.skill==this.MySkill)
            {
                //Debug.Log("SkillCasting:" + BattleSystem.instance.AllyTeam.TurnActionNum);
                this.startActionNum = BattleSystem.instance.AllyTeam.TurnActionNum;
                //this.startTurn = BattleSystem.instance.TurnNum;

                this.endActionNum = BattleSystem.instance.AllyTeam.TurnActionNum;
                //this.endTurn = BattleSystem.instance.TurnNum;
            }
        }
        public void SkillCastingQuit(CastingSkill ThisSkill)
        {
            if (ThisSkill.skill == this.MySkill)
            {
                //Debug.Log("SkillCastingQuit:" + BattleSystem.instance.AllyTeam.TurnActionNum);
                this.endActionNum = BattleSystem.instance.AllyTeam.TurnActionNum;
                //this.endTurn = BattleSystem.instance.TurnNum;
            }
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            this.remainHit = 2;
        }
        public void SkillUseTeam(Skill skill)
        {
            if (this.MySkill.IsNowCounting)
            {
                //Debug.Log("skill_AP:" + skill.UsedApNum);
                int num = skill.UsedApNum;
                if(!skill.isExcept)
                {
                    num=Math.Max(num, 1);
                }
                //Debug.Log("num:" + num);
                try
                {
                    this.MySkill.MyButton.CountingLeft += num;
                }
                catch { }
                /*
                if (BattleSystem.instance.CastSkills.Find((CastingSkill a) => a.skill == this.MySkill) != null)
                {
                    CastingSkill cast = BattleSystem.instance.CastSkills.Find((CastingSkill a) => a.skill == this.MySkill);
                    cast.CastSpeed += Math.Max(skill.UsedApNum, skill.isExcept ? 0 : 1);
                }
                else if (BattleSystem.instance.SaveSkill.Find((CastingSkill a) => a.skill == this.MySkill) != null)
                {
                    CastingSkill cast = BattleSystem.instance.SaveSkill.Find((CastingSkill a) => a.skill == this.MySkill);
                    cast.CastSpeed += Math.Max(skill.UsedApNum, skill.isExcept ? 0 : 1);
                }
                */
            }
        }
        public override void AttackEffectSingle(BattleChar hit, SkillParticle SP, int DMG, int Heal)
        {
            this.damage = DMG;
            Skill SkillD = SP.SkillData;
            if (SkillD == this.MySkill && this.remainHit > 0)
            {
                this.remainHit--;
                this.SkillBasePlus.Target_BaseDMG = (int)(this.damage * (this.endActionNum - this.startActionNum) * 0.125);
                this.damage = 0;
                Skill skill = SkillD.CloneSkill(true, this.BChar, null, false);
                skill.FreeUse = true;
                skill.PlusHit = true;

                this.SkillBasePlus.Target_BaseDMG = 0;
                this.remainHit++;

                //Debug.Log("剩余次数：" + this.remainHit);
                List<BattleChar> list = new List<BattleChar> { hit};
                BattleSystem.DelayInputAfter(this.PlusHit(list, skill));
            }
        }
        /*
        public IEnumerator ParticleOut_After(Skill SkillD, List<BattleChar> Targets)
        {
            if(SkillD == this.MySkill && this.remainHit>0)
            {
                this.remainHit--;
                this.SkillBasePlus.Target_BaseDMG = (int)(this.damage*(this.endActionNum-this.startActionNum)*0.125);
                this.damage = 0;
                Skill skill=SkillD.CloneSkill(true,this.BChar,null,false);
                skill.FreeUse = true;
                skill.PlusHit = true;

                this.SkillBasePlus.Target_BaseDMG = 0;

                Debug.Log("剩余次数：" + this.remainHit);
                BattleSystem.DelayInputAfter(this.PlusHit(Targets, skill));
            }
            yield break;
        }*/
        
        public IEnumerator PlusHit(List<BattleChar> Targets, Skill skill)
        {
            if(Targets.Find(a=>!a.IsDead)!=null)
            {
                this.BChar.ParticleOut(skill, Targets);
            }
            else
            {
                BattleChar tar= this.BChar.BattleInfo.EnemyList.Random(this.BChar.GetRandomClass().Main);
                this.BChar.ParticleOut(skill, tar);
            }
            yield break;
        }
        //public int startTurn = 0;
        //public int endTurn = 0;
        public int startActionNum = 0;
        public int endActionNum = 0;    
        public int damage = 0;
        public int remainHit = 2;
    }


    public class Sex_adl_8 : Sex_ImproveClone,IP_SkillUsePatch_Before,IP_UseSkillAfter_After_Patch
    {
        public void SkillUsePatch_Before(BattleChar TargetChar, Skill TargetSkill)
        {
            if(TargetChar==this.BChar)
            {
                //this.NotCount = true;
                //int num = this.BChar.Overload;
                //BattleSystem.instance.EffectDelays.Insert(Math.Min(BattleSystem.instance.EffectDelays.Count,1),this.RemainOverload(num));
                //BattleSystem.DelayInput(this.RemainOverload(num));
                //his.EffectDelays.Enqueue(this.RemainOverload(num));

                this.pflag = true;
                this.overload = this.BChar.Overload;
            }
        }
        /*
        public IEnumerator RemainOverload( int num)
        {
            //Debug.Log("test");
            yield return new WaitForFixedUpdate();
            this.BChar.Overload = num;
            yield break;
        }
        */
        public void UseSkillAfter_After_Patch(BattleAlly user, Skill skill)
        {
            if(skill==this.MySkill && this.pflag)
            {
                this.pflag=false;
                this.BChar.Overload = this.overload;
                this.overload = 0;
            }
        }
        private bool pflag = false;
        private int overload = 0;
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            foreach(BattleChar bc in Targets)
            {
                if (this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == bc) != null)
                {
                    this.BChar.MyTeam.ForceDraw(this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == bc));
                }
                else if (this.BChar.MyTeam.Skills_UsedDeck.Find((Skill a) => a.Master == bc) != null)
                {
                    this.BChar.MyTeam.ForceDraw(this.BChar.MyTeam.Skills_UsedDeck.Find((Skill a) => a.Master == bc));
                }
                else
                {
                    this.BChar.MyTeam.Draw(1);
                }
            }

        }
        //public List<IEnumerator> EffectDelays = new List<IEnumerator>();
    }

    public class Bcl_adl_8 : Buff,IP_SkillUse_Team
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.hit = -20;
        }
        public void SkillUseTeam(Skill skill)
        {
            if(skill.Master == this.BChar && !skill.isExcept /*&& this.charInfoSkills.Find(a=>a==skill.CharinfoSkilldata)==null*/)
            {
                //this.charInfoSkills.Add(skill.CharinfoSkilldata);
                BattleSystem.instance.AllyTeam.AP++;

            }
        }
        public override void TurnUpdate()
        {
            base.TurnUpdate();
            this.charInfoSkills.Clear();
        }
        public List<CharInfoSkillData> charInfoSkills = new List<CharInfoSkillData>();
    }

    public class Sex_adl_9 : Sex_ImproveClone
    {
        public override void BattleStartDeck(List<Skill> Skills_Deck)
        {
            this.BChar.MyTeam.Draw(this.MySkill, null);
        }
    }
    public class Bcl_adl_9 : SF_Standard.InBuff, IP_BlackMarkRemain
    { 
        public bool BlackMarkRemain(StackBuff sta=null)
        {
            if(sta==null || sta.UseState==this.BChar)
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
            foreach (E_adl_rec rec in this.plist)
            {
                num += rec.plusper;
            }
            this.PlusPerStat.Damage =  num;
        }

        public bool CheckEquipBlack()
        {
            return true;
        }
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if(addedbuff.BuffData.Key== "B_PD_Adeline_p" && BuffUser==this.BChar)
            {
                if (BuffTaker.BuffFind("B_PD_Adeline_p", false))
                {
                    int num = BuffTaker.BuffReturn("B_PD_Adeline_p", false).StackNum;
                    BattleSystem.DelayInputAfter(this.AddRec(num, 2));
                }

            }
        }


        public IEnumerator AddRec(int num = 0, int time = 2)
        {
            if (num > 0)
            {
                this.plist.Add(new E_adl_rec { plusper = num, remaintime = time });
                this.Init();
            }
            yield break;
        }
        public override void TurnUpdate()
        {
            base.TurnUpdate();
            foreach (E_adl_rec rec in this.plist)
            {
                rec.remaintime--;
            }
            this.plist.RemoveAll(rec => rec.remaintime <= 0);
            this.Init();
        }

        private List<E_adl_rec> plist = new List<E_adl_rec>();
        */
    }

    public class Sex_adl_10 : Sex_ImproveClone
    {
        public override void AttackEffectSingle(BattleChar hit, SkillParticle SP, int DMG, int Heal)
        {
            base.AttackEffectSingle(hit, SP, DMG, Heal);
            this.sumdmg += DMG;
            BattleSystem.DelayInputAfter(this.GetShield(DMG));
        }
        public IEnumerator GetShield(int DMG)
        {
            List<Skill> lists = new List<Skill>();
            foreach(BattleChar bc in this.BChar.MyTeam.AliveChars_Vanish)
            {
                GDESkillData skilldata = new GDESkillData("S_PD_Adeline_10_1");
                Skill skill = new Skill();
                skill.Init(skilldata);
                skill.Master = bc;
                Sex_adl_10_1 ex = new Sex_adl_10_1();
                BuffTag tag = new BuffTag();
                GDEBuffData gdebuff = new GDEBuffData("B_PD_Alina_S").ShallowClone();
                gdebuff.Barrier = DMG;
                tag.BuffData = gdebuff;
                tag.User = this.BChar;
                //tag.IsBarriarHP = DMG;
                ex.TargetBuff.Add(tag);
                skill.AllExtendeds.Add(ex);
                lists.Add(skill);
            }
            BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(lists, new SkillButton.SkillClickDel(this.Del_1), ScriptLocalization.System_SkillSelect.EffectSelect, false, false, true, false, true));

            yield break;
        }

        public void Del_1(SkillButton Mybutton)
        {
            Sex_adl_10_1 ex = Mybutton.Myskill.AllExtendeds.Find(a => a is Sex_adl_10_1) as Sex_adl_10_1;
            int num = 0;
            foreach (BuffTag  btg in ex.TargetBuff)
            {
                num += btg.BuffData.Barrier;
            }
            if (num > 0)
            {
                Buff buff=Mybutton.Myskill.Master.BuffAdd("B_PD_Alina_S", this.BChar, false, 0, false, -1, false);
                buff.BarrierHP += num;
            }
        }
        public int sumdmg = 0;
    }
    public class Sex_adl_10_1 : Sex_ImproveClone
    {

    }

    public class Sex_adl_11 : Sex_ImproveClone
    {

    }
    public class Bcl_adl_11 : Buff, IP_BuffAdd, IP_BuffUpdate, IP_SkillUse_User
    {
        public override string DescExtended()
        {


            string allname = base.StackInfo[0].UseState.Info.Name;
            for (int i = 1; i < base.StackInfo.Count; i++)
            {
                allname = allname + "/" + base.StackInfo[i].UseState.Info.Name;
            }
            string alldam = ((int)(base.StackInfo[0].UseState.GetStat.atk * this.dmgper/100)).ToString();
            for (int i = 1; i < base.StackInfo.Count; i++)
            {
                alldam = alldam + "/" + ((int)(base.StackInfo[i].UseState.GetStat.atk * this.dmgper/100)).ToString();
            }


            return base.DescExtended().Replace("&alldam", alldam).Replace("&allname", allname);
        }
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)//mark
        {
            if (BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key && this.StackNum >= this.BuffData.MaxStack && this.StackInfo.Find(a => a.UseState == BuffUser) == null)
            {
                this.StackInfo.Insert(0, this.StackInfo[0]);
            }
            else if (BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key && this.StackInfo.Find(a => a.UseState == BuffUser) != null)
            {
                int id = this.StackInfo.FindIndex(a => a.UseState == BuffUser);
                StackBuff st = this.StackInfo[id];
                this.StackInfo.RemoveAt(id);
                this.StackInfo.Insert(0, st);

            }
        }
        public void BuffUpdate(Buff MyBuff)
        {
            if (this.StackNum > this.BuffData.MaxStack)
            {
                List<BattleChar> listc = new List<BattleChar>();
                for (int i = 0; i < this.StackInfo.Count; i++)
                {
                    if (listc.Find(a => a == this.StackInfo[i].UseState) == null)
                    {
                        listc.Add(this.StackInfo[i].UseState);
                    }
                    else
                    {
                        this.StackInfo.RemoveAt(i);
                        i--;
                    }

                }
            }
        }
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            foreach (StackBuff sta in this.StackInfo)
            {
                if (!sta.UseState.IsDead && SkillD.Master==this.BChar && SkillD.IsDamage && BattleSystem.instance.EnemyList.Count != 0 && Targets.Count <= 1 && !SkillD.PlusHit && !SkillD.FreeUse && Targets[0].BuffFind("B_PD_Adeline_p",false))// && hit==this.BChar)
                {
                    BattleSystem.DelayInput(this.Attack(sta.UseState, Targets[0]));
                }
            }
        }

        public IEnumerator Attack(BattleChar user, BattleChar Target)
        {
            if(!Target.IsDead&&Target.BuffFind("B_PD_Adeline_p", false))
            {
                Buff buff = Target.BuffReturn("B_PD_Adeline_p", false);

                Skill skill2 = Skill.TempSkill("S_PD_Adeline_11_1", user, user.MyTeam);
                skill2.PlusHit = true;
                skill2.FreeUse = true;
                Skill_Extended sex = new Skill_Extended();
                sex.PlusSkillPerStat.Damage = this.dmgper * Target.BuffReturn("B_PD_Adeline_p", false).StackNum;
                sex.IsDamage = true;
                skill2.AllExtendeds.Add(sex);
                yield return new WaitForSecondsRealtime(0.2f);
                user.ParticleOut(skill2, Target);
            }

            yield break;
        }
        private int dmgper = 25;
    }
    public class Sex_adl_12 : Sex_ImproveClone//, IP_SkillUsePatch_Before, IP_UseSkillAfter_After_Patch
    {
        /*
        public void SkillUsePatch_Before(BattleChar TargetChar, Skill TargetSkill)
        {
            if (TargetChar == this.BChar)
            {
                this.pflag = true;
                this.overload = this.BChar.Overload;
            }
        }

        public void UseSkillAfter_After_Patch(BattleAlly user, Skill skill)
        {
            if (skill == this.MySkill && this.pflag)
            {
                this.pflag = false;
                this.BChar.Overload = this.overload;
                this.overload = 0;
            }
        }
        private bool pflag = false;
        private int overload = 0;
        */
    }
    public class Bcl_adl_12 : Buff,  IP_SkillUse_Team
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a",this.lastTimes.ToString());
        }
        public override void Init()
        {
            base.Init();
            this.lastTimes++;
        }
        public void SkillUseTeam(Skill skill)
        {
            if (this.active&&skill.Master == this.BChar /*&& !skill.BasicSkill*/ && !skill.FreeUse  && skill.AllExtendeds.Find(a => a is Sex_adl_12_1) == null)
            {
                this.active = false;
                Skill skill2 = skill.CloneSkill(true, null, null, true);
                //skill2.FreeUse = true;
                this.needselect = true;

                //BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill2, target, false, false, true, null));
                BattleSystem.DelayInputAfter(this.RepeatAttack(skill2));

            }
        }

        public IEnumerator RepeatAttack(Skill SkillD)
        {


            int n = 10;
            while (this.needselect && (BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Find(a=>BattleSystem.IsSelect(a,SkillD))!=null || BattleSystem.instance.AllyTeam.AliveChars_Vanish.Find(a => BattleSystem.IsSelect(a, SkillD)) != null))
            {
                if (this.needselect && !this.Usestate_L.BattleInfo.TargetSelecting )
                {
                    if(SkillD.AllExtendeds.Find(a=>a is Sex_adl_12_1)==null)
                    {
                        SkillD.ExtendedAdd(new Sex_adl_12_1());
                    }
                    Sex_adl_12_1 sex = SkillD.AllExtendeds.Find(a => a is Sex_adl_12_1) as Sex_adl_12_1;
                    sex.mainbuff = this;
                    //sex.MySkill = SkillD;
                    //SkillD.AllExtendeds.Add(sex);
                    if (SkillD.MySkill.Target.Key == GDEItemKeys.s_targettype_Misc)
                    {
                        needselect = false;
                    }
                    this.BChar.BattleInfo.TargetSelect(SkillD, this.BChar);
                }
                n++;
                if (n > 9)
                {
                    GDESkillKeywordData gde = new GDESkillKeywordData("KeyWord_PD_Adeline_12");

                    GameObject gameObject = Misc.UIInst(this.BChar.BattleInfo.SkillName);
                    gameObject.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = gde.Desc;
                    this.objectList.Add(gameObject);
                    n = 0;
                }
                yield return new WaitForSeconds(0.1f);
            }
            foreach (GameObject obj in this.objectList)
            {
                UnityEngine.Object.Destroy(obj);
            }
            this.objectList.Clear();
            this.lastTimes--;
            if(this.lastTimes <= 0)
            {
                this.SelfDestroy();
            }
            else
            {
                this.active = true;
            }

            yield break;
        }
        public bool needselect = false;
        public bool active = true;
        public List<GameObject> objectList = new List<GameObject>();
        public int lastTimes = 0;
    }
    public class Sex_adl_12_1 : Sex_ImproveClone, IP_UseSkillAfter_After_Patch,IP_UseSkillAfter_Before_Patch
    {
        
        public void UseSkillAfter_Before_Patch(BattleAlly user, Skill skill)
        {
            //base.SkillUseSingle(SkillD, Targets);
            if (skill == this.MySkill)
            {
                this.mainbuff.needselect = false;
                this.MySkill.FreeUse = true;

            }

        }
        
        public void UseSkillAfter_After_Patch(BattleAlly user, Skill skill)
        {
            //base.SkillUseSingle(SkillD, Targets);
            if (skill == this.MySkill)
            {
                //Debug.Log("SkillUseTeam_1");
                //this.mainbuff.needselect = false;
                this.MySkill.FreeUse = false;
                //Debug.Log("SkillUseTeam_2");
            }

        }
        
        public Bcl_adl_12 mainbuff = new Bcl_adl_12();
    }


    public class Sex_adl_13 : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {

            BattleSystem.instance.AllyTeam.CharacterDraw(Targets[0]);
            int num = 0;
            foreach (BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {
                if (bc.BuffFind("B_PD_Adeline_p", false))
                {
                    if (num <= 0)
                    {
                        num = bc.BuffReturn("B_PD_Adeline_p", false).StackNum;
                    }
                    else
                    {
                        num = Math.Min(num, bc.BuffReturn("B_PD_Adeline_p", false).StackNum);
                    }
                }
            }
            if (num > 0)
            {
                BattleSystem.instance.AllyTeam.AP += num;

            }
            else
            {
                BattleSystem.instance.AllyTeam.CharacterDraw(Targets[0]);

            }
        }
    }

    public class Sex_adl_14 : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            foreach (BattleChar bc in Targets)
            {
                if (this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == bc && a.CharinfoSkilldata!=SkillD.CharinfoSkilldata) != null)
                {
                    this.BChar.MyTeam.ForceDraw(this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == bc && a.CharinfoSkilldata != SkillD.CharinfoSkilldata));
                }
                else if (this.BChar.MyTeam.Skills_UsedDeck.Find((Skill a) => a.Master == bc && a.CharinfoSkilldata != SkillD.CharinfoSkilldata) != null)
                {
                    this.BChar.MyTeam.ForceDraw(this.BChar.MyTeam.Skills_UsedDeck.FindLast((Skill a) => a.Master == bc && a.CharinfoSkilldata != SkillD.CharinfoSkilldata));
                }
                else
                {
                    this.BChar.MyTeam.Draw(1);
                }
            }

        }
    }
    public class Bcl_adl_14 : Buff,IP_GetCriPer_Final, IP_SkillUse_Team,IP_ParticleOut_Before, IP_SkillUse_Target//, IP_ChangeDamageState
    {


        public override void Init()
        {
            base.Init();
            this.PlusStat.PlusMPUse.PlusMP_OnlyHand = 1;
            //this.PlusStat.cri = 30;
            //this.PlusStat.hit = 20;
            //this.LucySkillExBuff = new Sex_adl_14_1();
            /*
            if(!this.View)
            {
                if (this.reclist.Count > 0)
                {
                    this.PlusStat.PlusCriDmg = this.reclist[0].pluscridmg;
                }
                else
                {
                    this.PlusStat.PlusCriDmg = 0;
                }
                //Debug.Log("额外暴击伤害(init):" + this.PlusStat.PlusCriDmg );
            }
            */
        }
        
        public void SkillUseTeam(Skill skill)
        {
            if (skill.Master == this.BChar && !skill.BasicSkill )
            {
                //Debug.Log("SkillUseTeam_1");
                BattleSystem.instance.AllyTeam.AP++;
                /*
                if(skill.AllExtendeds.Find(a => a is Sex_adl_14_1) == null)
                {
                    //Debug.Log("SkillUseTeam_2");
                    Sex_adl_14_1 sex = new Sex_adl_14_1();
                    sex.Fatal = true;
                    sex.MySkill = skill;
                    skill.AllExtendeds.Add(sex);
                }
                */
            }
        }
        
        public void ParticleOut_Before(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == this.BChar && !SkillD.BasicSkill && SkillD.AllExtendeds.Find(a=>a is Sex_adl_14_1)==null)
            {
                //Debug.Log("ParticleOut_Before_1");
                //BattleSystem.instance.AllyTeam.AP++;
                SkillD.ExtendedAdd(new Sex_adl_14_1());
                /*
                Sex_adl_14_1 sex = new Sex_adl_14_1();
                sex.Fatal = true;
                sex.MySkill = SkillD;
                SkillD.AllExtendeds.Add(sex);
                */
                //Debug.Log("ParticleOut_Before_2:"+SkillD.Fatal);
            }
        }
        
        public void GetCriPer_Final(Skill skill, int overcri, int overhit, BattleChar Target)
        {
            if (skill.Master == this.BChar)
            {
                //Debug.Log("has sex:"+(skill.AllExtendeds.Find(a => a is Sex_adl_14_1) != null));
                if(skill.Fatal)
                {
                    //Debug.Log("fatal");
                }
                else
                {
                    //Debug.Log("no_fatal");
                }
                
                B14_rec rec= new B14_rec();
                rec.skill = skill;
                rec.User = this.BChar;
                rec.Target = Target;
                rec.pluscridmg =(int)(1*( overcri + overhit));//调整此处修改倍率
                this.reclist.Add(rec);

                //Debug.Log("GetCriPer_Final_1:" + this.BChar.GetStat.PlusCriDmg);
                if (this.reclist.Count > 0)
                {
                    this.PlusStat.PlusCriDmg = this.reclist[this.reclist.Count - 1].pluscridmg;
                    //Traverse.Create(this.BChar.Info).Field("getstat_BeforeFrameCount").SetValue(Time.frameCount-10);
                    this.BChar.Info.ForceGetStat();
                    Stat stat = this.BChar.Info.get_stat;
                    //this.BChar.Info.G_get_stat.PlusCriDmg += this.reclist[this.reclist.Count - 1].pluscridmg;
                }
                else
                {
                    this.PlusStat.PlusCriDmg = 0;
                    //Traverse.Create(this.BChar.Info).Field("getstat_BeforeFrameCount").SetValue(Time.frameCount - 10);
                    this.BChar.Info.ForceGetStat();
                    Stat stat = this.BChar.Info.get_stat;
                }

                //this.Init();
                    /*//加buff版本
                    this.BChar.Buffs.RemoveAll(a => a is Bcl_adl_14_1);
                    if (this.reclist.Count > 0)
                    {
                        //Debug.Log("额外暴击伤害(GetCriPer_Final):" + this.reclist[0].pluscridmg);
                        //this.PlusStat.PlusCriDmg = this.reclist[0].pluscridmg;
                        Buff buff=this.BChar.BuffAdd("B_PD_Adeline_14_1", this.BChar, false, 0, false, -1, false);
                        buff.PlusStat.PlusCriDmg = this.reclist[this.reclist.Count-1].pluscridmg;
                    }
                    else
                    {
                        //Buff buff = this.BChar.BuffAdd("B_PD_Adeline_14_1", this.BChar, false, 0, false, -1, false);
                        //buff.PlusStat.PlusCriDmg = 0;
                    }
                    */

            }
        }
        
        /*
        public void ChangeDamageState(SkillParticle SP, BattleChar Target, int DMG, bool Cri, ref bool ToHeal, ref bool ToPain)
        {
            if(SP.UseStatus==this.BChar)
            {
                int overhit = 0;
                int overcri = 0;

                GDESkillEffectData effect_Target = SP.SkillData.MySkill.Effect_Target;
                if (SP.SkillData.Master != Target.BattleInfo.DummyChar)
                {
                    float num = Misc.PerToNum(SP.UseStatus.GetStat.hit, (float)effect_Target.HIT + SP.SkillData.PlusSkillStat.hit);
                    num -= Target.GetStat.dod;
                    overhit = (int)Math.Max(num - 100, 0);
                }
                else
                {
                    overhit = 0;
                }

                overcri = Math.Max(SP.SkillData.GetCriPer(Target, 0) - 100, 0);

                B14_rec rec = new B14_rec();
                rec.SP = SP;
                rec.User = this.BChar;
                rec.Target = Target;
                rec.pluscridmg = overcri + overhit;
                this.reclist.Add(rec);
                //Debug.Log("B14_recs:" + this.reclist.Count);
            }
        }
        */
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            /*//施加buff版本
            if(this.reclist.Find(a=>a.skill==SP.SkillData && a.Target==hit && a.User==SP.UseStatus)!=null)
            {
                this.reclist.RemoveAll(a => a.skill == SP.SkillData&& a.Target == hit && a.User == SP.UseStatus);
            }
            this.BChar.Buffs.RemoveAll(a => a is Bcl_adl_14_1);
            */

            //this.Init();

            if (this.reclist.Find(a => a.skill == SP.SkillData && a.Target == hit && a.User == SP.UseStatus) != null)
            {
                B14_rec rec = this.reclist.Find(a => a.skill == SP.SkillData && a.Target == hit && a.User == SP.UseStatus);
                this.reclist.Remove(rec);
            }
            if (this.reclist.Count > 0)
            {
                Debug.Log("自我气焰：命中后残余不为0");
                this.PlusStat.PlusCriDmg = this.reclist[this.reclist.Count - 1].pluscridmg;
                //Traverse.Create(this.BChar.Info).Field("getstat_BeforeFrameCount").SetValue(Time.frameCount - 10);
                this.BChar.Info.ForceGetStat();
                Stat stat = this.BChar.Info.get_stat;
            }
            else
            {
                this.PlusStat.PlusCriDmg = 0;
                //Traverse.Create(this.BChar.Info).Field("getstat_BeforeFrameCount").SetValue(Time.frameCount - 10);
                this.BChar.Info.ForceGetStat();
                Stat stat = this.BChar.Info.get_stat;
            }
            

            //Debug.Log("额外暴击伤害(AttackEffect):");

        }
        public List<B14_rec> reclist = new List<B14_rec>();
    }
    public class B14_rec
    {
        public Skill skill =new Skill();
        public BattleChar User=new BattleChar();
        public BattleChar Target=new BattleChar();
        public int pluscridmg = 0;
    }

    public class Bcl_adl_14_1 : Buff
    {

    }
    public class Sex_adl_14_1: Sex_ImproveClone
    {
        public override void Init()
        {
            base.Init();
            if(this.MySkill.MySkill.Effect_Target.DMG_Per >= 1 || this.MySkill.MySkill.Effect_Target.DMG_Base >= 1)
                    {
                this.Fatal = true;
            }
            
        }
    }


    public class SkillEn_PD_Adeline_0 : Sex_ImproveClone//, IP_SkillUse_User_After
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return (MainSkill.TargetTypeKey == "enemy" || MainSkill.TargetTypeKey == "random_enemy" || MainSkill.TargetTypeKey == "all_onetarget");
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            //this.target = Targets[0];
            List<Sen_adl_1_rec> list=new List<Sen_adl_1_rec>();
            foreach(BattleChar enemy in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {
                Sen_adl_1_rec rec = new Sen_adl_1_rec();
                rec.bc = enemy;


                        if(enemy.BuffFind("B_PD_Adeline_p",false))
                        {
                            Buff buff = enemy.BuffReturn("B_PD_Adeline_p", false);
                            foreach(StackBuff sta in buff.StackInfo)
                            {
                                rec.users.Add(sta.UseState);
                            }
                        }

                if(rec.users.Count>0)
                {
                    list.Add(rec);
                }
            }
            /*
            foreach (BattleChar target in Targets)
            {
                
                foreach (BattleChar bc in target.MyTeam.AliveChars)
                {
                    if (!Targets.Contains(bc))
                    {
                        if (bc.BuffFind("B_PD_Adeline_p", false))
                        {
                            bc.BuffReturn("B_PD_Adeline_p", false).SelfDestroy();
                        }
                    }
                }
            }
            */
            int overstk = 0;
            /*
            foreach(Sen_adl_1_rec rec in list)
            {
                for(int i = 0;i<rec.users.Count;i++)
                {
                    if (rec.bc.BuffFind("B_PD_Adeline_p", false) )
                    {
                        Buff buff = rec.bc.BuffReturn("B_PD_Adeline_p", false);
                        if (buff.StackNum >= buff.BuffData.MaxStack)
                        {
                            overstk++;
                        }
                    }

                    rec.bc.BuffAdd("B_PD_Adeline_p", rec.users[i], false, 0, false, -1, false);
                }

            }
            */
            if(list.Count>0)
            {
                foreach(BattleChar target in Targets)
                {
                    Sen_adl_1_rec rec = list.Find(a => list.Find(b => b.users.Count > a.users.Count) == null);
                    for (int i = 0; i < rec.users.Count; i++)
                    {
                        if (target.BuffFind("B_PD_Adeline_p", false))
                        {
                            Buff buff = target.BuffReturn("B_PD_Adeline_p", false);
                            if (buff.StackNum >= buff.BuffData.MaxStack)
                            {
                                overstk++;
                            }
                        }
                        target.BuffAdd("B_PD_Adeline_p", rec.users[i], false, 0, false, -1, false);
                    }
                }

            }

            if(overstk>0)
            {
                //overstk=Math.Min(overstk, this.MySkill.UsedApNum);
                BattleSystem.instance.AllyTeam.AP += overstk;
            }
            

        }




    }
    public class Sen_adl_1_rec
    {
        public BattleChar bc=new BattleChar();
        public int stk = 0;
        public List<BattleChar> users = new List<BattleChar>();
    }

    public class E_PD_Adeline_0 : EquipBase, IP_CheckEquipBlack,IP_BuffAdd,IP_BattleEndOutBattle
    {
        public override void Init()
        {
            base.Init();
            //this.PlusPerStat.Damage = 15;
            this.PlusStat.hit = 15;
            this.PlusStat.cri = 20;
            this.PlusStat.Penetration = 50;
            
            int num = 0;
            
            foreach(E_adl_rec rec in this.plist) 
            {
                num += rec.plusper;
            }
            this.PlusPerStat.Damage = 15+num;
            
        }
        
        public bool CheckEquipBlack()
        {
            return true;
        }

        /*
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if(SkillD.Master==this.BChar && !SkillD.FreeUse)
            {
                int num = 0;
                foreach(BattleChar bc in Targets)
                {
                    if(bc.BuffFind("B_PD_Adeline_p",false))
                    {
                        num += bc.BuffReturn("B_PD_Adeline_p", false).StackNum;
                    }
                }
                if(num>0)
                {
                    BattleSystem.DelayInputAfter(this.AddRec(num,2));
                }
            }
        }*/
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {
            if (addedbuff.BuffData.Key == "B_PD_Adeline_p" && BuffUser == this.BChar)
            {
                if (BuffTaker.BuffFind("B_PD_Adeline_p", false))
                {
                    int num = BuffTaker.BuffReturn("B_PD_Adeline_p", false).StackNum;
                    BattleSystem.DelayInputAfter(this.AddRec(num, 2));
                }

            }
        }
        public IEnumerator AddRec(int num=0,int time=2)
        {
            if(num>0)
            {
                this.plist.Add(new E_adl_rec { plusper = num ,remaintime=time}) ;
                this.Init();
            }
            yield break;
        }
        public override void TurnUpdate()
        {
            base.TurnUpdate();
            foreach(E_adl_rec rec in this.plist)
            {
                rec.remaintime--;
            }
            this.plist.RemoveAll(rec => rec.remaintime<=0);
            this.Init();
        }

        public void BattleEndOutBattle()
        {
            this.plist=new List<E_adl_rec>();
            this.Init();
        }

        private List<E_adl_rec> plist=new List<E_adl_rec>();
        
    }
    public class E_adl_rec
    {
        public int plusper = 0;
        public int remaintime = 0;
    }

    public class E_PD_Adeline_1 : EquipBase,IP_DamageChange, IP_SkillUse_User_After, IP_SkillUse_Target
    {
        public override void Init()
        {
            base.Init();
            this.PlusPerStat.Damage = 10;
            //this.PlusStat.hit = 15;
            this.PlusStat.cri = 25;

            //this.PlusStat.Penetration = 50;

        }
        public void SkillUseAfter(Skill SkillD)
        {
            if(BattleSystem.instance==null)
            {
                return;
            }
            SkillParticle main = BattleSystem.instance.G_Particle.Find(a => a.SkillData == SkillD);
            if(main!=null)
            {
                bool isforall = SkillD.TargetTypeKey == "all_enemy" || SkillD.TargetTypeKey == "all_ally" || SkillD.TargetTypeKey == "all" || SkillD.TargetTypeKey == "all_allyorenemy" || SkillD.TargetTypeKey == "all_other";
                if (main.ALLTARGET.Count==1&&SkillD.IsDamage&&!isforall)
                {
                    if(!SkillD.FreeUse && SkillD.UsedApNum>0)
                    {
                        if(this.lastTarget != main.ALLTARGET[0])
                        {
                            this.lastTarget = main.ALLTARGET[0];
                            this.sum1 =1;
                        }
                        this.sum1 += SkillD.UsedApNum;
                    }
                }
            }
        }
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if(SkillD.Master==this.BChar && SkillD.IsDamage&& !View)
            {
                SkillParticle main = BattleSystem.instance.G_Particle.Find(a => a.SkillData == SkillD);
                if (main != null)
                {
                    bool isforall = SkillD.TargetTypeKey == "all_enemy" || SkillD.TargetTypeKey == "all_ally" || SkillD.TargetTypeKey == "all" || SkillD.TargetTypeKey == "all_allyorenemy" || SkillD.TargetTypeKey == "all_other";
                    if (main.ALLTARGET.Count == 1  && !isforall)
                    {
                        this.PlusStat.PlusCriDmg = 10 * this.sum1;
                        this.BChar.Info.ForceGetStat();
                        Stat stat=this.BChar.GetStat;
                    }
                    /*
                    else if(this.PlusStat.PlusCriDmg!=0)
                    {
                        this.PlusStat.PlusCriDmg = 0 * this.sum1;
                        this.BChar.Info.ForceGetStat();
                        Stat stat = this.BChar.GetStat;
                    }
                    */
                }
            }
            
            return Damage;
        }
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (SP.UseStatus == this.BChar && SP.SkillData.IsDamage )
            {
                this.PlusStat.PlusCriDmg = 0;
                this.BChar.Info.ForceGetStat();
                Stat stat = this.BChar.GetStat;
            }      
        }
        public IEnumerator Delay()
        {
            while(BattleSystem.instance.DelayWait || BattleSystem.instance.Particles.Count > 0)
            {
                yield return new WaitForFixedUpdate();
            }
            this.PlusStat.PlusCriDmg = 0;
            this.BChar.Info.ForceGetStat();
            Stat stat = this.BChar.GetStat;
            yield break;
        }
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&num1", (10 * this.sum1).ToString());
        }

        [XmlIgnore]
        public BattleChar lastTarget = null;
        public int sum1 = 1;
    }
    
}
