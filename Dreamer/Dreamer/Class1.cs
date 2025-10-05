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
using DG.Tweening;
using System.Diagnostics;




namespace SF_Dreamer
{

    [PluginConfig("M200_SF_Dreamer_CharMod", "SF_Dreamer_CharMod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class SF_Dreamer_CharacterModPlugin : ChronoArkPlugin
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
        public class DreamerDef : ModDefinition
        {
        }

        /*
        
        [HarmonyPatch(typeof(Character))]
        private class TestCharacterPlugin
        {
            // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
            [HarmonyPatch("Set_AllyData")]
            [HarmonyPrefix]
            private static bool Set_AllyData_Patch(Character __instance, GDECharacterData Data)//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
            {
                bool flag = Data.Key == "SF_Dreamer";
                bool result;
                if (flag)
                {
                    __instance.Set_Default();
                    __instance.OriginStat.atk = Data.ATK.x;
                    __instance.OriginStat.cri = Data.CRI.x;
                    __instance.OriginStat.def = Data.DEF.x;
                    __instance.OriginStat.dod = Data.DODGE.x;
                    __instance.OriginStat.hit = Data.HIT.x;
                    __instance.OriginStat.maxhp = (int)Data.MAXHP.x;
                    __instance.OriginStat.reg = Data.REG.x;
                    __instance.OriginStat.RES_CC = Data.RES_CC.x;
                    __instance.OriginStat.RES_DEBUFF = Data.RES_DEBUFF.x;
                    __instance.OriginStat.RES_DOT = Data.RES_DOT.x;
                    __instance.OriginStat.HIT_CC = Data.HIT_CC.x;
                    __instance.OriginStat.HIT_DEBUFF = Data.HIT_DEBUFF.x;
                    __instance.OriginStat.HIT_DOT = Data.HIT_DOT.x;
                    __instance.BasicSkill = new CharInfoSkillData("S_SF_Dreamer_0");
                    __instance.SkillDatas.Add(new CharInfoSkillData("S_DefultSkill_0"));
                    __instance.SkillDatas.Add(new CharInfoSkillData("S_DefultSkill_0"));
                    __instance.SkillDatas.Add(new CharInfoSkillData("S_DefultSkill_1"));
                    __instance.SkillDatas.Add(new CharInfoSkillData("S_DefultSkill_1"));
                    bool flag2 = SaveManager.NowData.GameOptions.Difficulty == 2;
                    if (flag2)
                    {
                        __instance.OriginStat.DeadImmune = 5;
                    }
                    bool flag3 = SaveManager.NowData.GameOptions.Difficulty == 0;
                    if (flag3)
                    {
                        __instance.OriginStat.DeadImmune = 15;
                    }
                    bool storyPart = PlayData.StoryPart;
                    if (storyPart)
                    {
                        __instance.OriginStat.DeadImmune = 0;
                    }
                    bool flag4 = SaveManager.Difficalty != 2 && new GDECharacterData(Data.Key).FirstSkill != null;
                    if (flag4)
                    {
                        __instance.SkillDatas.Add(new CharInfoSkillData(Data.FirstSkill.Key));
                    }
                    bool flag5 = PlayData.TSavedata.bMist != null && PlayData.TSavedata.bMist.Level >= 2;
                    if (flag5)
                    {
                        __instance.SkillDatas.Add(new CharInfoSkillData("S_DefultSkill_0"));
                    }
                    __instance.PassiveName = Data.PassiveName;
                    __instance.PassiveDes = Data.PassiveDes;
                    __instance.Hp = __instance.OriginStat.maxhp;
                    __instance.Name = Data.name;
                    __instance.KeyData = Data.Key;
                    //__instance._Data = Data;
                    Traverse.Create(__instance).Field("_Data").SetValue(Data);
                    __instance.Ally = true;
                    __instance._LV = 1;
                    bool flag6 = __instance.Equip.Count == 0;
                    if (flag6)
                    {
                        bool flag7 = PlayData.TSavedata.StageNum != 0;
                        if (flag7)
                        {
                            bool flag8 = PlayData.SpalcialRule == GDEItemKeys.SpecialRule_SR_Solo;
                            if (flag8)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    __instance.Equip.Add(null);
                                }
                            }
                            else
                            {
                                for (int j = 0; j < 2; j++)
                                {
                                    __instance.Equip.Add(null);
                                }
                            }
                        }
                        else
                        {
                            for (int k = 0; k < 2; k++)
                            {
                                __instance.Equip.Add(null);
                            }
                        }
                    }
                    Type type = ModManager.GetType(Data.PassiveClassName);
                    bool flag9 = type != null;
                    if (flag9)
                    {
                        __instance._Passive = (Passive_Char)Activator.CreateInstance(type);
                        __instance._Passive.Init();
                        __instance._Passive.MyChar = __instance;
                    }
                    result = false;
                }
                else
                {
                    result = true;
                }
                return result;
            }
        }
        */
        



        public class S_SF_Dreamer_16_Particle : CustomSkillGDE<SF_Dreamer_CharacterModPlugin.DreamerDef>
        {
            // Token: 0x060000AB RID: 171 RVA: 0x000055A0 File Offset: 0x000037A0
            public override ModGDEInfo.LoadingType GetLoadingType()
            {
                return ModGDEInfo.LoadingType.Replace;
            }

            // Token: 0x060000AC RID: 172 RVA: 0x000055B4 File Offset: 0x000037B4
            public override string Key()
            {
                return "S_SF_Dreamer_16";
            }

            // Token: 0x060000AD RID: 173 RVA: 0x000055CB File Offset: 0x000037CB
            public override void SetValue()
            {
                base.Particle = base.assetInfo.CodeConstuctAsync<GameObject>("Particle/Priest/Priest_new_1", new ModAssetInfo.ObjectChangeDelegate<GameObject>(this.ParticleChangeDelegate));//"Particle/Twins/Hel_2"
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


                this.ParticleColorChange(particle.transform, new Color(1f, 0.0274509f, 0.0274509f, 1f));
                this.ParticleColorChange(particle.transform.GetChild(0), new Color(1f, 1f, 1f, 0f));
                this.ParticleColorChange(particle.transform.GetChild(1), new Color(1f, 1f, 1f, 0f));
                this.ParticleColorChange(particle.transform.GetChild(2), new Color(1f, 1f, 1f, 0.5f));
                this.ParticleColorChange(particle.transform.GetChild(3), new Color(1f, 0.3f, 0.3f, 1f));
                this.ParticleColorChange(particle.transform.GetChild(3).GetChild(0), new Color(1f, 0.3f, 0.3f, 1f));
                this.ParticleColorChange(particle.transform.GetChild(3).GetChild(1), new Color(1f, 0.3f, 0.3f, 1f));
                this.ParticleColorChange(particle.transform.GetChild(3).GetChild(2), new Color(1f, 0.3f, 0.3f, 1f));
                this.ParticleColorChange(particle.transform.GetChild(3).GetChild(3), new Color(1f, 0.3f, 0.3f, 1f));
                this.ParticleColorChange(particle.transform.GetChild(3).GetChild(4), new Color(1f, 0.3f, 0.3f, 1f));
                this.ParticleColorChange(particle.transform.GetChild(3).GetChild(5), new Color(1f, 0.3f, 0.3f, 1f));



                return particle;
            }

        }
    }


    
    [HarmonyPatch(typeof(Character))]
    public class TestCharacterPlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("Set_AllyData")]
        [HarmonyPostfix]
        public static void Set_AllyData_Patch(Character __instance, GDECharacterData Data)//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
            bool flag = Data.Key == "SF_Dreamer";
            if (flag)
            {
                //__instance.BasicSkill = new GDESkillData("S_SF_Dreamer_0");
                    __instance.BasicSkill = new CharInfoSkillData("S_SF_Dreamer_0");

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
            //MasterAudio.PlaySound("A_drm_4", 1f, null, 0f, null, null, false, false);
            float volume = 0f;
            float volume2 = 0f;
            if (ModManager.getModInfo("SF_Dreamer").GetSetting<ToggleSetting>("AudioOn").Value)
            {
                volume = ModManager.getModInfo("SF_Dreamer").GetSetting<SliderSetting>("AudioVolume").Value;
            }
            else
            {
                volume = 0f;
            }

            if (inText.Contains("T_drm_1_1"))
            {
                inText = inText.Replace("T_drm_1_1:", string.Empty);
                MasterAudio.PlaySound("A_drm_1_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_2_1"))
            {
                inText = inText.Replace("T_drm_2_1:", string.Empty);
                MasterAudio.PlaySound("A_drm_2_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_2_2"))
            {
                inText = inText.Replace("T_drm_2_2:", string.Empty);
                MasterAudio.PlaySound("A_drm_2_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_2_3"))
            {
                inText = inText.Replace("T_drm_2_3:", string.Empty);
                MasterAudio.PlaySound("A_drm_2_3", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_3_1"))
            {
                inText = inText.Replace("T_drm_3_1:", string.Empty);
                MasterAudio.PlaySound("A_drm_3_1", volume, null, 1f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_3_2"))
            {
                inText = inText.Replace("T_drm_3_2:", string.Empty);
                MasterAudio.PlaySound("A_drm_3_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_4_1"))
            {
                inText = inText.Replace("T_drm_4_1:", string.Empty);
                MasterAudio.PlaySound("A_drm_4_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_4_2"))
            {
                inText = inText.Replace("T_drm_4_2:", string.Empty);
                MasterAudio.PlaySound("A_drm_4_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_5_1"))
            {
                inText = inText.Replace("T_drm_5_1:", string.Empty);
                MasterAudio.PlaySound("A_drm_5_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_5_2"))
            {
                inText = inText.Replace("T_drm_5_2:", string.Empty);
                MasterAudio.PlaySound("A_drm_5_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_5_3"))
            {
                inText = inText.Replace("T_drm_5_3:", string.Empty);
                MasterAudio.PlaySound("A_drm_5_3", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_6_1"))
            {
                inText = inText.Replace("T_drm_6_1:", string.Empty);
                MasterAudio.PlaySound("A_drm_6_1", volume, null, 1f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_6_2"))
            {
                inText = inText.Replace("T_drm_6_2:", string.Empty);
                MasterAudio.PlaySound("A_drm_6_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_6_3"))
            {
                inText = inText.Replace("T_drm_6_3:", string.Empty);
                MasterAudio.PlaySound("A_drm_6_3", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_7_1"))
            {
                inText = inText.Replace("T_drm_7_1:", string.Empty);
                MasterAudio.PlaySound("A_drm_7_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_7_2"))
            {
                inText = inText.Replace("T_drm_7_2:", string.Empty);
                MasterAudio.PlaySound("A_drm_7_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_8_1"))
            {
                inText = inText.Replace("T_drm_8_1:", string.Empty);
                MasterAudio.PlaySound("A_drm_8_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_8_2"))
            {
                inText = inText.Replace("T_drm_8_2:", string.Empty);
                MasterAudio.PlaySound("A_drm_8_2", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_9_1"))
            {
                inText = inText.Replace("T_drm_9_1:", string.Empty);
                MasterAudio.PlaySound("A_drm_9_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_9_2"))
            {
                inText = inText.Replace("T_drm_9_2:", string.Empty);
                MasterAudio.PlaySound("A_drm_9_2", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_10_1"))
            {
                inText = inText.Replace("T_drm_10_1:", string.Empty);
                MasterAudio.PlaySound("A_drm_10_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_11_1"))
            {
                inText = inText.Replace("T_drm_11_1:", string.Empty);
                MasterAudio.PlaySound("A_drm_11_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_11_2"))
            {
                inText = inText.Replace("T_drm_11_2:", string.Empty);
                MasterAudio.PlaySound("A_drm_11_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_drm_11_3"))
            {
                inText = inText.Replace("T_drm_11_3:", string.Empty);
                MasterAudio.PlaySound("A_drm_11_3", volume, null, 0f, null, null, false, false);
            }
        }
    }

    [HarmonyPriority(-1)]

    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("OnlyDamage")]
    public static class RecoveryPlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void BattleChar_OnlyDamage_Before_patch(BattleChar __instance)
        {
            if (__instance.Info.Ally)
            {
                foreach (IP_OnlyDamage_Before ip_before in BattleSystem.instance.IReturn<IP_OnlyDamage_Before>())
                {
                    //Debug.Log("only damage drm");
                    bool flag = ip_before != null;
                    if (flag)
                    {
                        ip_before.OnlyDamage_Before(__instance);
                    }
                }
                //Debug.Log("成功进入OnlyDamage前的patch");
            }

        }

        [HarmonyPostfix]
        public static void BattleChar_OnlyDamage_After_patch(BattleChar __instance)
        {
            if (__instance.Info.Ally)
            {
                foreach (IP_OnlyDamage_After ip_after in BattleSystem.instance.IReturn<IP_OnlyDamage_After>())
                {
                    bool flag = ip_after != null;
                    if (flag)
                    {
                        ip_after.OnlyDamage_After(__instance);
                    }
                }
                //Debug.Log("成功进入OnlyDamage后的patch");
            }

        }
        //private int recBefore;
    }
    public interface IP_OnlyDamage_Before
    {
        // Token: 0x06000001 RID: 1
        void OnlyDamage_Before(BattleChar battleChar);
    }
    public interface IP_OnlyDamage_After
    {
        // Token: 0x06000001 RID: 1
        void OnlyDamage_After(BattleChar battleChar);
    }


    [HarmonyPatch(typeof(BuffObject))]
    public class BuffObjectUpdatePlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("Update")]
        /*
        [HarmonyPrefix]
        private static bool Update_Patch(BuffObject __instance)//BuffObjectUpdate_Patch(BuffObject __instance)
        {

            if (__instance.SkillEx != null && __instance.SkillEx.Data.Key== "EX_SF_Dreamer_2")
            {
                EX_drm_2 ex2=__instance.SkillEx as EX_drm_2;
                if(ex2.ismax)
                {
                    __instance.StackText.text = "<color=#32CD32>max </color>";
                }
                else
                {
                    __instance.StackText.text = "<color=#F62817>min </color>";
                }
                return false;
            }
            if (__instance.MyBuff != null && __instance.MyBuff.BuffData.Key == "B_SF_Dreamer_0")
            {
                string str1 = (((__instance.MyBuff) as Bcl_drm_0).range).ToString();
                //string str2 = (((__instance.MyBuff) as Bcl_drm_0).maxrange).ToString();


                __instance.StackText.text = str1;
                return false;
            }
            return true;
        }
        */
        [HarmonyPostfix]
        public static void Update_Patch(BuffObject __instance)//BuffObjectUpdate_Patch(BuffObject __instance)
        {

            if (__instance.MyBuff != null && __instance.MyBuff.BuffData.Key == "B_SF_Dreamer_0")
            {
                IP_BuffObject_Updata ip_patch = __instance.MyBuff as IP_BuffObject_Updata;
                if(ip_patch != null)
                {
                    ip_patch.BuffObject_Updata(__instance);
                }

                //string str1 = (((__instance.MyBuff) as Bcl_drm_0).range).ToString();
                //__instance.StackText.text = str1;
                return;
            }
            return;
        }
    }

    public interface IP_BuffObject_Updata
    {
        // Token: 0x06000001 RID: 1
        void BuffObject_Updata(BuffObject obj);
    }
    //.......................................
    [HarmonyPatch(typeof(CharEquipInven))]
    [HarmonyPatch("OnDropSlot")]
    public static class CharEquipInvenPlugin
    {
        // Token: 0x06000015 RID: 21 RVA: 0x00002410 File Offset: 0x00000610
        [HarmonyPostfix]
        public static void CharEquipInven_OnDropSlot_patch(ItemBase inputitem, ref bool __result, CharEquipInven __instance)
        {
            bool flag = inputitem.itemkey == "E_SF_Dreamer_0" && __instance.Info.KeyData != "SF_Dreamer";
            if (flag)
            {
                __result = true;
            }
            //Debug.Log("成功进入OnDropSlot的patch");
        }
    }

    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("DodgeCheck")]
    public static class DodgeCheckPlugin
    {
        [HarmonyPriority(-2147483648)]
        [HarmonyFinalizer]
        public static void DodgeCheck(ref bool __result, BattleChar __instance, SkillParticle SP)
        {
            bool dod = __result;
            //Debug.Log("闪避检测检查点1");
            /*
            foreach (IP_DodgeCheck_After2 ip_patch2 in SP.SkillData.IReturn<IP_DodgeCheck_After2>())
            {
                bool flag2 = ip_patch2 != null;
                if (flag2)
                {
                    bool candod = dod;
                    ip_patch2.DodgeCheck_After2(SP, ref candod);
                    dod = candod || dod;
                }
            }
            */
            foreach (IP_DodgeCheck_After ip_patch in BattleSystem.instance.IReturn<IP_DodgeCheck_After>())
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    bool candod = dod;
                    ip_patch.DodgeCheck_After(__instance,SP, ref candod);
                    dod = candod && dod;
                }
            }
            __result = dod;
        }
        [HarmonyPriority(0)]
        [HarmonyPostfix]
        public static void DodgeCheck2(ref bool __result, BattleChar __instance, SkillParticle SP)
        {
            bool dod = __result;
            //Debug.Log("闪避检测检查点2");
            foreach (IP_DodgeCheck_After2 ip_patch2 in SP.SkillData.Master.IReturn<IP_DodgeCheck_After2>(SP.SkillData))
            {
                bool flag2 = ip_patch2 != null;
                if (flag2)
                {
                    bool candod = dod;
                    ip_patch2.DodgeCheck_After2(__instance,SP, ref candod);
                    dod = candod || dod;
                }
            }
            /*
            foreach (IP_DodgeCheck_After ip_patch in BattleSystem.instance.IReturn<IP_DodgeCheck_After>())
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    bool candod = dod;
                    ip_patch.DodgeCheck_After(SP, ref candod);
                    dod= candod && dod;
                }
            }
            */
            __result = dod;
        }
        

    }
    public interface IP_DodgeCheck_After
    {
        // Token: 0x06000001 RID: 1
        void DodgeCheck_After(BattleChar hit,SkillParticle SP, ref bool dod);
    }

    public interface IP_DodgeCheck_After2
    {
        // Token: 0x06000001 RID: 1
        void DodgeCheck_After2(BattleChar hit, SkillParticle SP, ref bool dod);
    }
    [HarmonyPriority(-1)]

    [HarmonyPatch(typeof(Skill))]
    [HarmonyPatch("GetCriPer")]
    public static class GetCriPerPlugin
    {


        // Token: 0x06000015 RID: 21 RVA: 0x00002410 File Offset: 0x00000610
        [HarmonyFinalizer]
        public static void GetCriPer_Patch_Final(Skill __instance, ref int __result, BattleChar Target, int Plus)
        {
            if (BattleSystem.instance != null )// BattleSystem.instance.Particles.Find(a=>a.SkillData==__instance)!=null )
            {


                foreach (IP_GetCriPer_Final ip_before in BattleSystem.instance.IReturn<IP_GetCriPer_Final>())
                {
                    //Debug.Log("only damage drm");
                    bool flag = ip_before != null;
                    if (flag)
                    {
                        ip_before.GetCriPer_Final(__instance, __result, Target);
                    }
                }
            }
        }
    }
    public interface IP_GetCriPer_Final
    {
        // Token: 0x06000001 RID: 1
        void GetCriPer_Final(Skill skill, int cri_Result,BattleChar Target);
    }




    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("ParticleOutEnd")]
    public static class ParticleOutEndPlugin
    {

        [HarmonyPostfix]
        public static void ParticleOutEnd_After( BattleChar __instance, Skill skill, List<BattleChar> Target)
        {
            foreach (IP_ParticleOutEnd_After ip_patch in BattleSystem.instance.IReturn<IP_ParticleOutEnd_After>())
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    BattleSystem.DelayInput(ip_patch.ParticleOutEnd_After(__instance, skill,Target));
                }
            }
        }

    }

    /*
    public interface IP_ParticleOutEnd_Before
    {
        // Token: 0x06000001 RID: 1
        IEnumerator ParticleOutEnd_Before(BattleChar Attacker, Skill skill, List<BattleChar> SaveTargets);
    }
    */

    public interface IP_ParticleOutEnd_After
    {
        // Token: 0x06000001 RID: 1
        IEnumerator ParticleOutEnd_After(BattleChar BC, Skill skill, List<BattleChar> Target);
    }


    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("SkillEffectBuffAdd")]
    public static class SkillEffectBuffAddPlugin
    {

        [HarmonyPostfix]
        public static void SkillEffectBuffAdd_After(BattleChar __instance, Skill UseSkill)
        {
            foreach (IP_SkillEffectBuffAdd ip_patch in UseSkill.Master.IReturn<IP_SkillEffectBuffAdd>(UseSkill))
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    ip_patch.SkillEffectBuffAdd_After(__instance, UseSkill.Master, UseSkill);
                }
            }
        }

    }
    public interface IP_SkillEffectBuffAdd
    {
        // Token: 0x06000001 RID: 1
        void SkillEffectBuffAdd_After(BattleChar taker,BattleChar user, Skill skill);
    }


    [HarmonyPatch(typeof(BuffObject))]//雾月猎场显示目标
    [HarmonyPatch("OnPointerEnter")]
    public static class BuffOnPointerEnterPlugin
    {
        [HarmonyPostfix]
        public static void BuffOnPointerEnter(BuffObject __instance)
        {

            if (__instance.MyBuff != null)
            {
                IP_BuffOnPointerEnter ip_patch = __instance.MyBuff as IP_BuffOnPointerEnter;
                if (ip_patch != null)
                {
                    ip_patch.BuffOnPointerEnter(__instance.MyBuff);
                }
            }
        }

    }
    public interface IP_BuffOnPointerEnter
    {
        // Token: 0x06000001 RID: 1
        void BuffOnPointerEnter(Buff buff);
    }

    [HarmonyPatch(typeof(BuffObject))]//雾月猎场显示目标
    [HarmonyPatch("OnPointerExit")]
    public static class BuffOnPointerExitPlugin
    {
        [HarmonyPostfix]
        public static void BuffOnPointerExit(BuffObject __instance)
        {

            if (__instance.MyBuff != null)
            {
                IP_BuffOnPointerExit ip_patch = __instance.MyBuff as IP_BuffOnPointerExit;
                if (ip_patch != null)
                {
                    ip_patch.BuffOnPointerExit(__instance.MyBuff);
                }
            }
        }

    }
    public interface IP_BuffOnPointerExit
    {
        // Token: 0x06000001 RID: 1
        void BuffOnPointerExit(Buff buff);
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
                foreach (IP_SkillUsePatch_Before ip_patch in __instance.SelectedSkill.Master.IReturn<IP_SkillUsePatch_Before>(__instance.SelectedSkill))
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
                foreach (IP_SkillUsePatch_Before ip_patch in __instance.SelectedSkill.Master.IReturn<IP_SkillUsePatch_Before>(__instance.SelectedSkill))
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
    /*
    [HarmonyPatch(typeof(FriendShipUI))]
    [HarmonyPatch("FriendShipLevelUp")]
    public static class FriendShipLevelUpPlugin
    {
        [HarmonyPostfix]
        public static void FriendShipLevelUp_AfterPatch(FriendShipUI __instance, string CharId, int NowLV)
        {
            if (CharId == "SF_Dreamer" && NowLV >= 3)
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

            if (CharId == "SF_Dreamer")
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

    [HarmonyPatch(typeof(CharStatV4))]
    [HarmonyPatch("ToBasicSkill")]
    public static class ToBasicSkill_V4_Plugin
    {
        [HarmonyPrefix]
        public static void ToBasicSkill_V4_Patch(ref CharStatV4 __instance, SkillButton Mybutton)
        {
            //Debug.Log("ToBasicSkill_V4");
            if (__instance.CharInfos[__instance.NowCharIndex].AllyCharacter.Info.KeyData == "SF_Dreamer" && __instance.CharInfos[__instance.NowCharIndex].AllyCharacter.BasicSkill.MySkill.KeyID == "S_SF_Dreamer_0")
            {
                __instance.CharInfos[__instance.NowCharIndex].AllyCharacter.Info.BasicSkill = null;

                
            }

            if(Mybutton.Myskill.MySkill.KeyID== "S_SF_Dreamer_0" && !Mybutton.CharData.Info.SkillDatas.Contains(Mybutton.Myskill.CharinfoSkilldata))
            {
                Mybutton.CharData.Info.SkillDatas.Add(Mybutton.Myskill.CharinfoSkilldata);
            }
        }
    }
    [HarmonyPatch(typeof(CharStatV4))]
    [HarmonyPatch("BasicSkillChangeBtn")]
    public static class BasicSkillChangeBtn_V4_Plugin
    {
        [HarmonyPrefix]
        public static void BasicSkillChangeBtn_V4_Before_Patch(ref CharStatV4 __instance)
        {
            //Debug.Log("BasicSkillChangeBtn_V4,KeyID:" + __instance.CharInfos[__instance.NowCharIndex].AllyCharacter.Info.BasicSkill.KeyID);
            if(__instance.CharInfos[__instance.NowCharIndex].AllyCharacter.Info.KeyData=="SF_Dreamer" && __instance.CharInfos[__instance.NowCharIndex].AllyCharacter.BasicSkill.MySkill.KeyID!="S_SF_Dreamer_0")
            {
                __instance.CharInfos[__instance.NowCharIndex].AllyCharacter.Skills.Add(Skill.TempSkill("S_SF_Dreamer_0", __instance.CharInfos[__instance.NowCharIndex].AllyCharacter, PlayData.TempBattleTeam));
                //Debug.Log("BasicSkillChangeBtn_V4_point2,KeyID:"+ __instance.CharInfos[__instance.NowCharIndex].AllyCharacter.Info.BasicSkill.KeyID);
            }
        }
        
        [HarmonyPostfix]
        public static void BasicSkillChangeBtn_V4_After_Patch(ref CharStatV4 __instance)
        {
            if (__instance.CharInfos[__instance.NowCharIndex].AllyCharacter.Info.KeyData == "SF_Dreamer")
            {
                for (int i =0; i < __instance.CharInfos[__instance.NowCharIndex].AllyCharacter.Skills.Count; i++)
                {
                    if (__instance.CharInfos[__instance.NowCharIndex].AllyCharacter.Skills[i].MySkill.KeyID == "S_SF_Dreamer_0")
                    {
                        __instance.CharInfos[__instance.NowCharIndex].AllyCharacter.Skills.RemoveAt(i);
                        i--;
                    }
                }
            }

        }
    }




    [HarmonyPatch(typeof(Buff))]
    public class BuddDes_Plugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("BuffDes")]

        [HarmonyTranspiler]//ldarg.0 >> this
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var methodInfo = AccessTools.Method(typeof(BuffDes_plus), nameof(BuffDes_plus.NameExtended), new System.Type[] { typeof(Buff), typeof(string) });
            var codes = instructions.ToList();

            for (int i = 0; i < codes.Count; i++)
            {
                if (i > 5)
                {

                    bool f1 = true;
                    /*
                    f1 = f1 && codes[i - 3].opcode == OpCodes.Ldarg_0;
                    f1 = f1 && codes[i - 2].opcode == OpCodes.Ldloc_1;
                    f1 = f1 && codes[i - 1].opcode == OpCodes.Callvirt;
                    try
                    {
                        f1 = f1 && (codes[i - 1].operand is MethodInfo && ((MethodInfo)(codes[i - 1].operand)).Name.Contains("NameExtended"));
                    }
                    catch
                    {f1 = false;}
                    f1 = f1 && codes[i - 0].opcode == OpCodes.Stloc_1;
                    */
                    f1 = f1 && codes[i - 5].opcode == OpCodes.Ldloc_1;
                    f1 = f1 && codes[i - 4].opcode == OpCodes.Ldarg_0;
                    f1 = f1 && codes[i - 3].opcode == OpCodes.Ldfld;
                    f1 = f1 && codes[i - 2].opcode == OpCodes.Callvirt;
                    try
                    {
                        f1 = f1 && (codes[i - 2].operand is MethodInfo && ((MethodInfo)(codes[i - 2].operand)).Name.Contains("get_Name"));
                    }
                    catch{ f1 = false; }
                    f1 = f1 && codes[i - 1].opcode == OpCodes.Call;
                    f1 = f1 && codes[i - 0].opcode == OpCodes.Stloc_1;


                    if (f1 )
                    {

                        yield return codes[i];

                        yield return new CodeInstruction(OpCodes.Ldarg_0);
                        yield return new CodeInstruction(OpCodes.Ldloc_1);
                        yield return new CodeInstruction(OpCodes.Call, methodInfo);
                        //yield return codes[i];
                        yield return new CodeInstruction(OpCodes.Stloc_1);
                        continue;
                    }


                }

                    yield return codes[i];
                

            }

        }

    }


    public static class BuffDes_plus
    {
        public static string NameExtended(Buff buff,string str)
        {
            //GDECharacterData test = gDECharacterData;
            //test = Test1.Check1(CSC, gDECharacterData);

            string str1 = str;
             IP_BuffNameEx ip=buff as IP_BuffNameEx;
            if (ip != null)
            {
                str1=ip.BuffNameEx(str1);
            }
            return str1;

        }

    }
    public interface IP_BuffNameEx
    {
        // Token: 0x06000001 RID: 1
        string BuffNameEx(string str);
    }

    public static class SkillToolTip_Input_Transpiler_Method
    {
        public static bool IsAntiVanish(Skill skill)
        {
            //Debug.Log("IsAntiVanish:" + (skill.MySkill.PlusKeyWords.Find(a => a.Key == "KeyWord_SF_Dreamer_antiVanish") != null));
            return (skill.MySkill.PlusKeyWords.Find(a => a.Key == "KeyWord_SF_Dreamer_antiVanish") != null);
        }

        public static List<string> AddAntiVanish(Skill skill,List<string> list2)
        {
            //Debug.Log("AddAntiVanish:"+ SkillToolTip_Input_Plugin.ptsr);
            List<string> list3=new List<string>();
            list3.AddRange(list2);
            if (skill.MySkill.PlusKeyWords.Find(a => a.Key == "KeyWord_SF_Dreamer_antiVanish") != null)
            {
                GDESkillKeywordData keyw = new GDESkillKeywordData("KeyWord_SF_Dreamer_antiVanish");

                list3.Add(SkillToolTip.ColorChange("FF7C34", keyw.Name));
            }
            return list3;
        }
    }

    [HarmonyPatch(typeof(SkillToolTip))]
    [HarmonyPatch("Input")]
    public static class SkillToolTip_Input_Plugin
    {
        /*
        [HarmonyTranspiler]//修改函数版本
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var methodInfo_1 = AccessTools.Method(typeof(SkillToolTip_Input_Transpiler_Method), nameof(SkillToolTip_Input_Transpiler_Method.IsAntiVanish), new System.Type[] { typeof(Skill) });
            var methodInfo_2 = AccessTools.Method(typeof(SkillToolTip_Input_Transpiler_Method), nameof(SkillToolTip_Input_Transpiler_Method.AddAntiVanish), new System.Type[] { typeof(Skill),typeof(List<string>)  });

            var codes = instructions.ToList();
            for (int i = 0; i < codes.Count; i++)
            {
                if (i > 10 && i<codes.Count-10)
                {

                    bool fa1 = codes[i - 1].opcode == OpCodes.Callvirt;
                    bool fa2 = codes[i - 2].opcode == OpCodes.Ldfld;
                    bool fa3 = codes[i - 3].opcode == OpCodes.Ldloc_0;
                    bool fa4 = codes[i - 4].opcode == OpCodes.Brtrue;
                    bool fa5 = codes[i - 5].opcode == OpCodes.Callvirt;
                    bool fa6 = codes[i - 6].opcode == OpCodes.Ldfld;
                    bool fa7 = codes[i - 7].opcode == OpCodes.Ldloc_0;

                    bool fa0_1 = false;
                    if(fa1 && fa2 && fa3 && fa4 && fa5&& fa6 && fa7  && codes[i - 1].operand is MethodInfo)
                    {
                        MethodInfo opr1 = (MethodInfo)(codes[i -1].operand);
                        if (opr1.Name.Contains("get_Fatal"))
                        {
                            fa0_1 = true;
                            //SkillToolTip_Input_Plugin.ptsr = opr.Name;
                        }
                    }
                    

                    //---------------------------------------
                    bool fb0 = codes[i].opcode == OpCodes.Ldloc_0;
                    bool fb1 = codes[i - 1].opcode == OpCodes.Callvirt;
                    bool fb2 = codes[i - 2].opcode == OpCodes.Ldc_I4_6;
                    bool fb3 = codes[i - 3].opcode == OpCodes.Ldloc_S;
                    bool fb4 = codes[i - 4].opcode == OpCodes.Callvirt;
                    bool fb5 = codes[i - 5].opcode == OpCodes.Call;
                    bool fb6 = codes[i - 6].opcode == OpCodes.Call;
                    bool fb10 = codes[i - 10].opcode == OpCodes.Callvirt;
                    bool fb10_1 = false;
                    if(fb0 && fb1 && fb2 && fb3 && fb4 && fb5 && fb6 && fb10 && codes[i - 10].operand is MethodInfo)
                    {
                        MethodInfo opr = (MethodInfo)(codes[i - 10].operand);

                        if (opr.Name.Contains("get_IgnoreTaunt"))
                        {
                            fb10_1 = true;
                            
                        }
                    }
                    


                    
                    if (fa1 && fa2 && fa3 && fa4 && fa5 && fa6 && fa7 && fa0_1)
                    {
                        

                        yield return codes[i-4];
                        yield return codes[i - 3];
                        yield return codes[i - 2];

                        yield return new CodeInstruction(OpCodes.Call, methodInfo_1);

                        yield return codes[i];

                    }
                    else if ( fb0 && fb1 && fb2 && fb3 && fb4 && fb5 && fb6 &&fb10 && fb10_1)
                    {
                        yield return codes[i];
                        yield return codes[i+1];

                        yield return new CodeInstruction(OpCodes.Ldloc_S,21);
                        yield return new CodeInstruction(OpCodes.Call, methodInfo_2);
                        yield return new CodeInstruction(OpCodes.Stloc_S, 21);

                        //yield return codes[i];
                        yield return new CodeInstruction(OpCodes.Ldloc_0);
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
        */
        [HarmonyFinalizer]
        public static void SkillToolTip_Input_Patch(SkillToolTip __instance, Skill Skill)
        {
            //替换无视嘲讽版本
            if (Skill.MySkill.PlusKeyWords.Find(a => a.Key == "KeyWord_SF_Dreamer_antiVanish") != null)
            {
                //Debug.Log("input_patch");
                string text1 = string.Empty;
                string text2 = string.Empty;



                //text2= Misc.DesStringsRest(list2);

                GDESkillKeywordData keyw = new GDESkillKeywordData("KeyWord_SF_Dreamer_antiVanish");

                text2 = SkillToolTip.ColorChange("FF7C34", ScriptLocalization.Battle_Keyword.IgnoreTaunt);
                text1 = text2 + ". " + SkillToolTip.ColorChange("FF7C34", keyw.Name);


                //text1 = Misc.DesStringsRest(list1);
                Regex re1 = new Regex(text2);
                string text3 = re1.Replace(__instance.Desc.text, text1, 1);
                if (text3.Contains(text2) && !__instance.Desc.text.Contains(SkillToolTip.ColorChange("FF7C34", keyw.Name)))
                {
                    __instance.Desc.text = text3;


                }

            }

            /*//关键词整体替换版本
            if ( Skill.MySkill.PlusKeyWords.Find(a=> a.Key == "KeyWord_SF_Dreamer_antiVanish")!=null)
             {
                //Debug.Log("input_patch");
                string text1 = string.Empty;
                string text2 = string.Empty;
                if (Skill.MySkill.KeyID != GDEItemKeys.Skill_S_StoryGlitch && (Skill.NotCount || Skill.AutoDelete >= 1 || Skill.NotCount || Skill.Disposable || Skill.isExcept || Skill.IsWaste || Skill.IgnoreTaunt || Skill.Counting >= 1 || Skill.Track || Skill.TargetChainHeal || Skill.NoExchange || Skill.Fatal))
                {
                    List<string> list1 = new List<string>();
                    List<string> list2 = new List<string>();

                    if (Skill.BasicOption && (BattleSystem.instance == null || Skill.BasicSkill))
                    {
                        list2.Add(SkillToolTip.ColorChange("FF7C34", ScriptLocalization.Battle_Keyword.Basic));
                    }
                    if (Skill.Fatal)
                    {
                        list2.Add(SkillToolTip.ColorChange("FF7C34", ScriptLocalization.Battle_Keyword.Fatal));
                    }
                    if (Skill.TargetChainHeal)
                    {
                        list2.Add(SkillToolTip.ColorChange("FF7C34", new GDESkillKeywordData(GDEItemKeys.SkillKeyword_KeyWord_ChainHeal).Name));
                    }
                    if (Skill.Counting >= 1)
                    {
                        list2.Add(SkillToolTip.ColorChange("FF7C34", ScriptLocalization.Battle_Keyword.Count + " " + Skill.Counting.ToString()));
                    }
                    if (Skill.isExcept)
                    {
                        list2.Add(SkillToolTip.ColorChange("FF3535FF", ScriptLocalization.Battle_Keyword.Except));
                    }
                    if (Skill.Disposable && !Skill.isExcept)
                    {
                        list2.Add(SkillToolTip.ColorChange("FF7C34", ScriptLocalization.Battle_Keyword.OneTime));
                    }
                    if (Skill.NotCount)
                    {
                        list2.Add(SkillToolTip.ColorChange("FF7C34", ScriptLocalization.Battle_Keyword.Quick));
                    }
                    if (Skill.Track)
                    {
                        list2.Add(SkillToolTip.ColorChange("FF7C34", ScriptLocalization.Battle_Keyword.Track));
                    }
                    if (Skill.IgnoreTaunt)
                    {
                        list2.Add(SkillToolTip.ColorChange("FF7C34", ScriptLocalization.Battle_Keyword.IgnoreTaunt));
                    }
                    if (Skill.AutoDelete >= 1)
                    {
                        list2.Add(SkillToolTip.ColorChange("FF3535FF", string.Format(ScriptLocalization.Battle_Keyword.Autodelete, Skill.AutoDelete)));
                    }
                    if (Skill.NoExchange)
                    {
                        list2.Add(SkillToolTip.ColorChange("FF3535FF", ScriptLocalization.Battle_Keyword.Fixed));
                    }
                    if (Skill.IsWaste && !Skill.NoExchange && !Skill.isExcept)
                    {
                        list2.Add(SkillToolTip.ColorChange("FF3535FF", ScriptLocalization.Battle_Keyword.Waste));
                    }
                    if (Skill.MySkill.NoBasicSkill && BattleSystem.instance == null)
                    {
                        list2.Add(SkillToolTip.ColorChange("FF3535FF", ScriptLocalization.Battle_Keyword.NoBasicSkill));
                    }

                    //text2= Misc.DesStringsRest(list2);

                    GDESkillKeywordData keyw = new GDESkillKeywordData("KeyWord_SF_Dreamer_antiVanish");
                    list1.AddRange(list2);
                    list1.Add(SkillToolTip.ColorChange("FF7C34", keyw.Name));

                    for (int i = 0; i < list2.Count; i++)
                    {
                        text2 += list2[i];
                        if (i != list2.Count - 1)
                        {
                            text2 += ". ";
                        }
                    }
                    for (int i = 0; i < list1.Count; i++)
                    {
                        text1 += list1[i];
                        if (i != list1.Count - 1)
                        {
                            text1 += ". ";
                        }
                    }


                    //text1 = Misc.DesStringsRest(list1);

                    string text3= __instance.Desc.text.Replace(text2, text1);
                    if(text3.Contains(text2))
                    {
                        __instance.Desc.text = text3;
                        __instance.Desc.text = text3;


                    }
                    //Debug.Log("text1:"+text1);
                    //Debug.Log("text2:"+ text3.Contains(text2)+":" + text2);
                    //Debug.Log("text3:" + text3);
                }
            }
            */

            /*//描述替换版本
            if ( Skill.MySkill.Description.Contains("&replace_drm"))
            {
                bool antivanish = Skill.MySkill.PlusKeyWords.Find(a => a.Key == "KeyWord_SF_Dreamer_antiVanish") != null;
                //Debug.Log("input_patch");
                bool flag_before = false;
                bool flag_after = false;
                flag_before = Skill.MySkill.KeyID != GDEItemKeys.Skill_S_StoryGlitch && (Skill.NotCount || Skill.AutoDelete >= 1 || Skill.NotCount || Skill.Disposable || Skill.isExcept || Skill.IsWaste || Skill.IgnoreTaunt || Skill.Counting >= 1 || Skill.Track || Skill.TargetChainHeal || Skill.NoExchange || Skill.Fatal);
                string str1 = Skill.MySkill.Description.Replace("&antivanish",string.Empty);
                flag_after = !str1.IsEmpty() && str1 != " ";

                string old1 = string.Empty;
                string new1 = string.Empty;
                if(antivanish)
                {
                    GDESkillKeywordData keyw = new GDESkillKeywordData("KeyWord_SF_Dreamer_antiVanish");


                    new1 = SkillToolTip.ColorChange("FF7C34", keyw.Name);
                    if(flag_before)
                    {
                        old1 = Misc.InputLine(30)+ "<color=#BAB6EC>" + "&replace_drm";
                        if(flag_after)
                        {
                            new1 = ". " + new1 + Misc.InputLine(30) + "<color=#BAB6EC>";

                        }
                        else
                        {
                            new1 = ". " + new1 + "<color=#BAB6EC>";
                        }
                    }
                    else
                    {
                        old1 = "<color=#BAB6EC>"+"&replace_drm";
                        if (flag_after)
                        {
                            new1 =  new1 + Misc.InputLine(30) + "<color=#BAB6EC>";
                        }
                        else
                        {
                            new1 = new1 + "<color=#BAB6EC>";
                        }
                    }
                }
                    //text2= Misc.DesStringsRest(list2);

                    string text3= __instance.Desc.text;
                if(text3.Contains(old1))
                {
                    string text4=text3.Replace(old1,new1);
                    __instance.Desc.text = text4;
                }
                    //Debug.Log("text1:"+text1);
                    //Debug.Log("text2:"+ text3.Contains(text2)+":" + text2);
                    //Debug.Log("text3:" + text3);

            }
            */
        }
    }

    /*
    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("SkillEffectBuffAdd")]
    public static class SkillEffectBuffAddPlugin
    {

        [HarmonyPostfix]
        public static void SkillEffectBuffAdd_After(BattleChar __instance, Skill UseSkill)
        {
            foreach (IP_SkillEffectBuffAdd ip_patch in UseSkill.Master.IReturn<IP_SkillEffectBuffAdd>(UseSkill))
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    ip_patch.SkillEffectBuffAdd_After(__instance, UseSkill.Master, UseSkill);
                }
            }
        }

    }
    */
    public interface IP_AntiVanish
    {
        // Token: 0x06000001 RID: 1
        bool AntiVanish(BattleChar Target, Skill skill);
    }


    [HarmonyPatch(typeof(BattleSystem))]
    [HarmonyPatch("IsSelect")]
    public static class IsSelectPlugin
    {


        // Token: 0x06000015 RID: 21 RVA: 0x00002410 File Offset: 0x00000610
        [HarmonyFinalizer, HarmonyPriority(-2147483648)]
        public static void IsSelect_Patch(ref bool __result, BattleSystem __instance, BattleChar Target, Skill skill)
        {

            //bool antiVanishCase1 = !(skill.MySkill.PlusKeyWords.Find(a => (a.Key == "KeyWord_SF_Dreamer_antiVanish")) == null);
            //bool antiVanishCase2 = Target.BuffFind("B_SF_Dreamer_4", false) && Target.BuffReturn("B_SF_Dreamer_4", false).StackInfo.Find(a => a.UseState == skill.Master) != null;
            //bool antiVanishCase3 = Target.BuffFind("B_SF_Dreamer_p", false) && Target.BuffReturn("B_SF_Dreamer_p", false).StackInfo.Find(a => a.UseState == skill.Master) != null;
            //bool antiVanish = antiVanishCase1 || antiVanishCase2||antiVanishCase3;
            bool antiVanish = false;
            bool antiTaunt = false;
            /*
            foreach (IP_AntiVanish ip_patch in __instance.IReturn<IP_AntiVanish>())
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    bool flag2=ip_patch.AntiVanish(Target, skill);
                    antiVanish = antiVanish || flag2;
                }
            }
            */
            foreach (IP_AntiVanish ip_patch in skill.Master.IReturn<IP_AntiVanish>(skill))
            {
                if(antiVanish)
                {
                    break;
                }
                bool flag = ip_patch != null;
                if (flag)
                {
                    bool flag2 = ip_patch.AntiVanish(Target, skill);
                    antiVanish = antiVanish || flag2;
                }
            }
            
            foreach (IP_AntiVanish ip_patch in Target.IReturn<IP_AntiVanish>())
            {
                if (antiVanish)
                {
                    break;
                }
                bool flag = ip_patch != null;
                if (flag)
                {
                    bool flag2 = ip_patch.AntiVanish(Target, skill);
                    antiVanish = antiVanish || flag2;
                }
            }
            
            antiTaunt=antiTaunt || antiVanish;

            if (antiVanish || antiTaunt)
            {
                string targetTypeKey = skill.TargetTypeKey;
                bool ally = skill.Master.Info.Ally;


                if (!antiVanish && (Target.GetStat.Vanish ))//changed
                {

                    __result = false;
                    goto IL_drm001;
                }


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
                    flag = BattleSystem_drm.TargetSelectCheck_drm(antiTaunt, Target, skill, flag);
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
                    flag = (ally == Target.Info.Ally ||  BattleSystem_drm.TargetSelectCheck_drm(antiTaunt, Target, skill, flag));
                }
                if (targetTypeKey == GDEItemKeys.s_targettype_all)
                {
                    flag = true;
                }
                if (targetTypeKey == GDEItemKeys.s_targettype_deathally && Target != skill.Master && Target.Info.Incapacitated)
                {
                    flag = true;
                }
                else if (Target.IsDead)
                {
                    flag = false;
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


                __result = __result || flag;
            IL_drm001:;
            }
            //Debug.Log(skill.Master.Info.Name + "触发IsSelect:" + Target.Info.Name + "-" + __result);
        }
    }
    [HarmonyPatch(typeof(BattleSystem))]
    [HarmonyPatch("SkillTargetReturn")]
    [HarmonyPatch(new Type[] { typeof(Skill), typeof(BattleChar), typeof(BattleChar) })]
    public static class SkillTargetReturnPlugin
    {
        // Token: 0x06000015 RID: 21 RVA: 0x00002410 File Offset: 0x00000610
        [HarmonyFinalizer, HarmonyPriority(-2147483648)]
        public static void SkillTargetReturn_Patch(ref List<BattleChar> __result, BattleSystem __instance, Skill sk, BattleChar Target, BattleChar PlusTarget = null)
        {
            //bool antiVanishCase1 = !(sk.MySkill.PlusKeyWords.Find(a => (a.Key == "KeyWord_SF_Dreamer_antiVanish")) == null);
            //bool antiVanishCase2 = Target.BuffFind("B_SF_Dreamer_4", false) && Target.BuffReturn("B_SF_Dreamer_4", false).StackInfo.Find(a => a.UseState == sk.Master) != null;
            //bool antiVanishCase3 = Target.BuffFind("B_SF_Dreamer_p", false) && Target.BuffReturn("B_SF_Dreamer_p", false).StackInfo.Find(a => a.UseState == sk.Master) != null;

            //bool antiVanish = antiVanishCase1 || antiVanishCase2||antiVanishCase3;
            bool antiVanish = false;
            /*
            foreach (IP_AntiVanish ip_patch in __instance.IReturn<IP_AntiVanish>())
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    bool flag2 = ip_patch.AntiVanish(Target, sk);
                    antiVanish = antiVanish || flag2;
                }
            }
            */
            foreach (IP_AntiVanish ip_patch in sk.Master.IReturn<IP_AntiVanish>(sk))
            {
                if (antiVanish)
                {
                    break;
                }
                bool flag = ip_patch != null;
                if (flag)
                {
                    bool flag2 = ip_patch.AntiVanish(Target, sk);
                    antiVanish = antiVanish || flag2;
                }
            }
            
            foreach (IP_AntiVanish ip_patch in Target.IReturn<IP_AntiVanish>())
            {
                if (antiVanish)
                {
                    break;
                }
                bool flag = ip_patch != null;
                if (flag)
                {
                    bool flag2 = ip_patch.AntiVanish(Target, sk);
                    antiVanish = antiVanish || flag2;
                }
            }
            

            if (antiVanish)
            {


                List<BattleChar> list = new List<BattleChar>();
                List<BattleEnemy> EnemyListVanish = __instance.EnemyTeam.AliveChars_Vanish.Cast<BattleEnemy>().ToList<BattleEnemy>();
                if (sk.MySkill.Target.Key == GDEItemKeys.s_targettype_all_enemy)
                {
                    if (sk.Master.Info.Ally)
                    {
                        using (List<BattleEnemy>.Enumerator enumerator = EnemyListVanish.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                BattleEnemy item = enumerator.Current;
                                list.Add(item);
                            }
                            goto IL_V264;
                        }
                    }
                    using (List<BattleAlly>.Enumerator enumerator2 = __instance.AllyList.GetEnumerator())
                    {
                        while (enumerator2.MoveNext())
                        {
                            BattleAlly item2 = enumerator2.Current;
                            list.Add(item2);
                        }
                        goto IL_V264;
                    }
                }
                if (sk.MySkill.Target.Key == GDEItemKeys.s_targettype_all_ally)
                {
                    if (!sk.Master.Info.Ally)
                    {
                        using (List<BattleEnemy>.Enumerator enumerator = EnemyListVanish.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                BattleEnemy item3 = enumerator.Current;
                                list.Add(item3);
                            }
                            goto IL_V264;
                        }
                    }
                    using (List<BattleAlly>.Enumerator enumerator2 = __instance.AllyList.GetEnumerator())
                    {
                        while (enumerator2.MoveNext())
                        {
                            BattleAlly item4 = enumerator2.Current;
                            list.Add(item4);
                        }
                        goto IL_V264;
                    }
                }
                if (sk.MySkill.Target.Key == GDEItemKeys.s_targettype_all)
                {
                    foreach (BattleAlly item5 in __instance.AllyList)
                    {
                        list.Add(item5);
                    }
                    using (List<BattleEnemy>.Enumerator enumerator = EnemyListVanish.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            BattleEnemy item6 = enumerator.Current;
                            list.Add(item6);
                        }
                        goto IL_V264;
                    }
                }
                if (sk.MySkill.Target.Key == GDEItemKeys.s_targettype_random_enemy)
                {
                    if (sk.Master.Info.Ally)
                    {
                        list.Add(EnemyListVanish.Random(sk.Master.GetRandomClass().Target));
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
            IL_V264:
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
                            foreach (BattleEnemy item7 in EnemyListVanish)
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

            foreach(BattleChar bc in list)
                {
                    if(!__result.Contains(bc))
                    {
                        __result.Add(bc);
                    }
                }
                //__result = list;

            }
        }
    }


    [HarmonyPriority(-1)]


   

    public  class BattleSystem_drm : MonoBehaviour
    {
        public static bool TargetSelectCheck_drm(bool antiTaunt, BattleChar Target, Skill skill, bool selected)
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
                        if (antiTaunt || flag2 || skill.IgnoreTaunt || !(Target as BattleEnemy).TauntNoTarget || Target.GetStat.IgnoreTaunt_EnemySelf)
                        {
                            selected = true;
                        }
                    }
                }
            }
            return selected;
        }
    }





    //被动
    public class P_Dreamer : Passive_Char, /*IP_PlayerTurn,*/IP_SkillUse_Target, IP_ParticleOut_After, IP_LevelUp//, IP_Discard//, IP_SkillUse_User_After//, IP_IsSelect_Before, IP_IsSelect_After
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
            if (!this.ItemTake && this.MyChar.LV>=5)
            {
                this.ItemTake = true;
                List <ItemBase> list = new List<ItemBase>();
                list.Add(ItemBase.GetItem("Item_SF_1", 1));
                list.Add(ItemBase.GetItem("Item_SF_2", 1));
                list.Add(ItemBase.GetItem("E_SF_Dreamer_0", 1));
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


        
        
        public IEnumerator ParticleOut_After(Skill SkillD, List<BattleChar> Targets)//mark
        {
            //UnityEngine.Debug.Log("监测点：ParticleOut_After");
            bool isforall = SkillD.TargetTypeKey == "all_enemy" || SkillD.TargetTypeKey == "all_ally" || SkillD.TargetTypeKey == "all" || SkillD.TargetTypeKey == "all_allyorenemy" || SkillD.TargetTypeKey == "all_other";

            if (!isforall && Targets.Count == 1 && SkillD.IsDamage  && !SkillD.PlusHit && (!Targets[0].BuffFind("B_SF_Dreamer_4", false) || Targets[0].BuffReturn("B_SF_Dreamer_4", false).StackInfo.Find(a => a.UseState == this.BChar) == null))
            //if (Targets.Count == 1 && SkillD.IsDamage  && !SkillD.PlusHit && !Bcl_drm_14.CheckCanActiveB4(Targets[0],this.BChar))

            {
  
                Targets[0].BuffAdd("B_SF_Dreamer_p", this.BChar, false, 0, false, -1, true);
            }
            yield break;
        }


        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            bool isforall = SP.SkillData.TargetTypeKey == "all_enemy" || SP.SkillData.TargetTypeKey == "all_ally" || SP.SkillData.TargetTypeKey == "all" || SP.SkillData.TargetTypeKey == "all_allyorenemy" || SP.SkillData.TargetTypeKey == "all_other";
            if(!isforall && SP.ALLTARGET.Count==1 && SP.SkillData.IsDamage && !SP.SkillData.PlusHit && Cri)
            {


                this.BChar.BuffAdd("B_SF_Dreamer_0", this.BChar, false, 0, false, -1, false);
            }
        }


        /*
        public void Discard(bool Click, Skill skill, bool HandFullWaste)
        {
            if((this.complementing || this.beforeturn) && skill.Master==this.BChar && skill.MySkill.KeyID== "S_SF_Dreamer_0")
            {
                this.fullhandNum=this.BChar.MyTeam.Skills.Count;
                //Debug.Log(this.fullhandNum);
                this.needgetsk = true;
            }
        }
        */
        /*
        public void Turn()
        {
            //base.TurnUpdate();
            this.beforeturn = true;

            Skill skill = Skill.TempSkill("S_SF_Dreamer_0", this.BChar, this.BChar.MyTeam);

            skill.isExcept = true;
            skill.NotCount = true;
            skill.AutoDelete = 1;
            this.BChar.MyTeam.Add(skill, true,0);
            //this.BChar.MyTeam.Skills.Insert(0, skill);
        }
        */

        private bool beforeturn = false;
        private bool complementing = false;
        private bool needgetsk = false;
        private int fullhandNum = 10;
        
        /*
        public override void FixedUpdate()
        {
            if((this.complementing || this.beforeturn) && !BattleSystem.instance.DelayWait && BattleSystem.instance.Particles.Count == 0 && BattleSystem.instance.ActWindow.On)
            {
                this.beforeturn=false;
                this.complementing = false;
            }
            if (BattleSystem.instance.AllyTeam.Skills.Count > this.fullhandNum && !BattleSystem.instance.DelayWait && BattleSystem.instance.Particles.Count == 0 && BattleSystem.instance.ActWindow.On)
            {
                this.fullhandNum = BattleSystem.instance.AllyTeam.Skills.Count;
                //Debug.Log("change:" + this.fullhandNum);
            }
            if (this.needgetsk)
            {

                if (BattleSystem.instance.AllyTeam.Skills.Count < this.fullhandNum && !BattleSystem.instance.DelayWait && BattleSystem.instance.Particles.Count == 0 && BattleSystem.instance.ActWindow.On)
                {
                    this.needgetsk = false;
                    this.complementing = true;

                    Skill skill = Skill.TempSkill("S_SF_Dreamer_0", this.BChar, this.BChar.MyTeam);
                    //GDESkillKeywordData keyw = skill.MySkill.PlusKeyWords.Find(a => a.Key == "KeyWord_SF_Dreamer_0_1");
                    //skill.MySkill.PlusKeyWords.Remove(keyw);
                    skill.isExcept = true;
                    skill.NotCount = true;
                    skill.AutoDelete = 1;
                    this.BChar.MyTeam.Add(skill, true,0);
                    
                }
            }
        }
        */

        //public void BuffUpdate(Buff MyBuff)
        //{
            //base.FixedUpdate();
            //if (this.BChar.BuffFind("B_SF_Dreamer_0",false))
            //{

                //Bcl_drm_0 buff = this.BChar.BuffReturn("B_SF_Dreamer_0", false) as Bcl_drm_0;
                //buff.isDreamer = true;
                //buff.Init();
            //}

            //if (!this.BChar.BuffFind("B_SF_Dreamer_pac", false))
            //{
                //this.BChar.BuffAdd("B_SF_Dreamer_pac", this.BChar, false, 0, false, -1, false);
            //}

        //}
    }




    public class Bcl_drm_p : StaInBuff, IP_TurnEnd, IP_BuffAdd,IP_BuffUpdate,IP_DodgeCheck_After,IP_BuffAddAfter,IP_AntiVanish//, IP_CriPerChange
    {
        public override string DescExtended()//mark
        {
           

            string allname = this.StackInfo[0].UseState.Info.Name;
            for (int i = 1; i <this.StackInfo.Count; i++)
            {
                allname = allname + "/" + this.StackInfo[i].UseState.Info.Name ;
            }
            return base.DescExtended().Replace("&A", allname);//base.Usestate_L.Info.Name);//.ToString());
        }
        public override bool GetForce()
        {
            return true;
        }

        public bool AntiVanish(BattleChar Target, Skill skill)
        {
            if (Target == this.BChar && this.StackInfo.Find(a => a.UseState == skill.Master) != null)
            {
                return true;
            }
            return false;
        }
        /*
        public override void ForBuffAdded_Before(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {
            base.ForBuffAdded_Before(BuffUser, BuffTaker, addedbuff);
            this.Buffadded(BuffUser, BuffTaker, addedbuff);
        }
        public override void ForBuffAdded_After(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            base.ForBuffAdded_After(BuffUser, BuffTaker, addedbuff, stackBuff);
            this.BuffaddedAfter(BuffUser, BuffTaker, addedbuff, stackBuff);
        }
        */
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {

            //Debug.Log("节点1"+addedbuff.BuffData.Name);
            //Debug.Log("修改点test0:"+ Bcl_drm_14.Checkb14());
            if (BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key && this.StackNum >= this.BuffData.MaxStack && this.StackInfo.Find(a => a.UseState == BuffUser) == null)
            {
                //this.BuffData.MaxStack++;
                //Debug.Log("节点2");
                //this.isb4 = true;
                if (Bcl_drm_14.Checkb14())
                {
                    /*
                    foreach(StackBuff sta in this.StackInfo)
                    {
                        this.b4user.Add(sta.UseState);
                    }
                    */
                    this.needclear = true;
                    this.b4user.Add(BuffUser);
                }
                this.StackInfo.Insert(0, this.StackInfo[0]);

            }
            else if (BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key && this.StackInfo.Find(a => a.UseState == BuffUser) != null)//判断是否一个人有两层，应该为修改点之一
            {
                //Debug.Log("节点3");
                //Debug.Log(BuffUser.Info.Name);
                int id = this.StackInfo.FindIndex(a => a.UseState == BuffUser);
                StackBuff st = this.StackInfo[id];
                this.StackInfo.RemoveAt(id);
                this.StackInfo.Insert(0, st);

                this.b4user.Add(BuffUser);
                //Debug.Log("节点4:"+ this.b4user.Count);
            }
            /*
            if (BuffTaker == this.BChar && addedbuff.BuffData.Key == "B_SF_Dreamer_4" )
            {
                for(int i=0;i<this.StackInfo.Count;i++)
                {
                    if(BuffUser == this.StackInfo[i].UseState)
                    {
                        this.StackInfo.RemoveAt(i);
                        i--;
                    }
                }


            }
            */

        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            /*
            if (BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key)
            {
                for (int i = 0; i < this.StackInfo.Count; i++)
                {

                    if (this.StackInfo.FindAll(a => a.UseState == this.StackInfo[i].UseState).Count >= 2)
                    {
                        BattleChar bc = this.StackInfo[i].UseState;
                        for (int n = 0; n < this.StackInfo.Count; n++)
                        {
                            if (this.StackInfo[n].UseState == bc)
                            {
                                this.StackInfo.RemoveAt(n);
                                n--;
                            }
                        }
                        i--;

                        this.BChar.BuffAdd("B_SF_Dreamer_4", bc, false, 0, false, -1, false);



                    }
                }

            }
            */
            //Debug.Log("BuffaddedAfter_1");

                this.BuffStatUpdate();
            
        }
        private List<BattleChar> b4user= new List<BattleChar>();
        private bool needclear = false;
        
        public void BuffUpdate(Buff MyBuff)
        {
            if(!this.buffUpdate_Checking)
            {
                this.buffUpdate_Checking = true;
                BattleSystem.instance.StartCoroutine(this.BuffUpdate_Delay(MyBuff));
            }
        }
        
        public IEnumerator BuffUpdate_Delay(Buff MyBuff)
        {
            yield return new WaitForFixedUpdate();
            //foreach(StackBuff sta in this.StackInfo)
            for (int i = 0; i < this.StackInfo.Count; i++)
            {
                if (this.BChar.BuffFind("B_SF_Dreamer_4", false) && (this.BChar.BuffReturn("B_SF_Dreamer_4", false).StackInfo.Find(a => a.UseState == this.StackInfo[i].UseState) != null ))
                {
                    //this.StackInfo.RemoveAt(i);
                    this.RemoveStack(i);
                    i--;
                }
            }
            if (this.needclear)
            {
                this.needclear = false;
                this.StackInfo.Clear();
            }

            /*
            if (this.StackNum < this.BuffData.MaxStack)
            {
                this.BuffData.MaxStack = Math.Max(1, this.StackNum);
            }
            */
            bool checkover = false;
            IL_001:
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
                        //this.StackInfo.RemoveAt(i);
                        //i--;
                        if (checkover)
                        {
                            //Debug.Log("RemoveStack");
                            this.RemoveStack(i);
                            i--;
                        }
                        else
                        {
                            //Debug.Log("CheckAndSet");
                            this.CheckAndSet();
                            checkover = true;
                            goto IL_001;
                        }
                    }

                }
            }
            

            if(this.b4user.Count > 0)
            {
                //Debug.Log("BuffUpdate_1");
                List<BattleChar> list = new List<BattleChar>();
                list.AddRange(this.b4user);
                this.b4user=new List<BattleChar>();
                foreach(BattleChar user in list)
                {
                    this.BChar.BuffAdd("B_SF_Dreamer_4", user, false, 0, false, -1, false);
                }
            }

            this.HistorySet(null);
            this.buffUpdate_Checking = false;

            this.FixedUpdate();

            yield break;
        }
        private bool buffUpdate_Checking = false;

        public void DodgeCheck_After(BattleChar hit, SkillParticle SP, ref bool dod)//mark
        {
            //Debug.Log("判断锁定");
            bool isforall = SP.SkillData.TargetTypeKey == "all_enemy" || SP.SkillData.TargetTypeKey == "all_ally" || SP.SkillData.TargetTypeKey == "all" || SP.SkillData.TargetTypeKey == "all_allyorenemy" || SP.SkillData.TargetTypeKey == "all_other";

            if (!isforall && SP.SkillData.IsDamage && SP.ALLTARGET.Count <= 1 && hit==this.BChar && /*SP.ALLTARGET[0] == this.BChar &&*/ this.StackInfo.Find(a => a.UseState == SP.UseStatus) != null)//修改点之一，bcl_4_2应该也要对应修改
            {
                dod = false;

                this.HistoryCheck(null);
                this.HistorySet(SP.UseStatus);

                //this.overHitSkills.Add(SP.SkillData);//弱点标记&弹道修正-命中&暴击&爆伤转化
            }
            else if (!isforall && SP.SkillData.IsDamage && SP.ALLTARGET.Count <= 1 &&hit==this.BChar&& /*SP.ALLTARGET[0] == this.BChar &&*/ Bcl_drm_14.Checkb14()) 
            {
                dod = false;

                this.HistoryCheck(null);
                this.HistorySet(this.StackInfo[0].UseState);

                //this.overHitSkills.Add(SP.SkillData);//弱点标记&弹道修正-命中&暴击&爆伤转化
            }
        }
        /*
        public List<Skill> overHitSkills=new List<Skill>();//弱点标记&弹道修正-命中&暴击&爆伤转化
        public void CriPerChange(Skill skill, BattleChar Target, ref float CriPer)//弱点标记&弹道修正-命中&暴击&爆伤转化
        {
            if(Target==this.BChar && this.overHitSkills.Contains(skill))
            {
                this.overHitSkills.Remove(skill);


                int overhit = 0;
                GDESkillEffectData effect_Target = skill.MySkill.Effect_Target;
                if (skill.Master != Target.BattleInfo.DummyChar)
                {
                    float num = Misc.PerToNum(skill.Master.GetStat.hit, (float)effect_Target.HIT + skill.PlusSkillStat.hit);
                    num -= Target.GetStat.dod;
                    overhit = (int)Math.Max(num, 0);
                }
                else
                {
                    overhit = 0;
                }
                if(overhit>0)
                {
                    CriPer = (float)(CriPer + 0.5*overhit);
                }
            }
        }
        */
        public override void FixedUpdate()//mark
        {
            /*
            foreach (StackBuff stack in this.StackInfo)
            {

                if (!stack.UseState.BuffFind("B_SF_Dreamer_4_2", false))
                {
                    stack.UseState.BuffAdd("B_SF_Dreamer_4_2", stack.UseState, false, 0, false, -1, false);
                }
            }
            */
            /*
            for (int i = 0; i < this.StackInfo.Count; i++)
            {
                if ((this.StackInfo.FindAll(a => a.UseState == this.StackInfo[i].UseState)).Count > 1)
                {
                    this.StackInfo.RemoveAt(i);
                    i--;
                }
            }
            */
            if (this.StackInfo.Count <= 0)
            {
                //this.SelfDestroy();
                this.InBuffDestroy();
            }
        }

        public void HistorySet(BattleChar bc)//设置特定角色已触发过，传入null时仅检查并更新记录是否触发过的list
        {
            for (int i = 0; i < this.plist.Count; i++)
            {
                if (this.StackInfo.Find(a => a.UseState == this.plist[i].bc) == null)
                {
                    this.plist.RemoveAt(i);
                    i--;
                }
            }
            foreach (StackBuff sta in this.StackInfo)
            {
                if (this.plist.Find(a => a.bc == sta.UseState) == null)
                {
                    Used_drm used_Drm = new Used_drm();
                    used_Drm.bc = sta.UseState;
                    this.plist.Add(used_Drm);
                }
            }

            if (bc != null)
            {


                for (int i = 0; i < this.plist.Count; i++)
                {
                    if (bc == this.plist[i].bc )
                    {
                        this.plist[i].used = true;
                    }
                }
            }
        }

        public bool HistoryCheck(BattleChar bc)//检测特定角色是否触发过
        {
            bool flag = false;
            if (bc != null)
            {
                for (int i = 0; i < this.plist.Count; i++)
                {
                    if (bc == this.plist[i].bc)
                    {
                        flag=flag || this.plist[i].used;
                    }
                }
            }

            return flag;
        }
        public List<Used_drm> plist = new List<Used_drm>();
        
        public void TurnEnd()
        {



            for (int i = 0; i < this.StackInfo.Count; i++)
            {

                if (this.HistoryCheck(this.StackInfo[i].UseState))
                {
                    //this.StackInfo.RemoveAt(i);
                    this.RemoveStack(i);
                    i--;
                }
            }
            this.HistorySet(null);

            if (this.StackNum <= 0)
            {
                //base.SelfDestroy();
                this.InBuffDestroy();
            }
            this.BuffStatUpdate();
        }
        
        /*
        public void TurnEnd()//IP_TurnEnd
        {

            for (int i = 0; i < this.StackInfo.Count; i++)
            {
                if (this.HistoryCheck(this.StackInfo[i].UseState))
                {
                    this.StackInfo[i].RemainTime--;
                }
                this.HistorySet(this.StackInfo[i].UseState);

                if (this.StackInfo[i].RemainTime == 0)
                {
                    this.StackInfo.RemoveAt(i);
                    //base.SelfDestroy();
                    i--;
                }

            }
            if (this.StackNum <= 0)
            {
                base.SelfDestroy();
            }
            this.BuffStatUpdate();
        }

        public override void TurnUpdate()
        {

        }
        */
    }

    public interface IP_CheckEquip
    {
        // Token: 0x060025CE RID: 9678
        bool CheckEquip(BattleChar bc);
    }
    public interface IP_RangeChange
    {
        // Token: 0x060025CE RID: 9678
        void RangeChange(BattleChar bc,Buff buff,int range_before,int range_after,ref bool overload);
    }
    public interface IP_RangeChangeAfter
    {
        // Token: 0x060025CE RID: 9678
        void RangeChangeAfter(BattleChar bc,Buff buff, int range_before,int range_after,int addnum);
    }


    public class Bcl_drm_0 : InBuff, IP_TurnEnd, IP_PlayerTurn, IP_OnlyDamage_Before, IP_OnlyDamage_After,IP_BuffObject_Updata, IP_BuffNameEx//,IP_AITargetSelect_Before,IP_AITargetSelect_After
    {
        public override bool GetForce()
        {
            return true;
        }
        
        public string BuffNameEx(string str)
        {
            return str + Misc.InputColor(string.Concat(new object[]
            {
                " - ",
                this.range,
                "/",
                (this.CheckSkyCommander()?"∞":this.maxrange.ToString())
            }), "FFF4BF");//FF7C34
        }
        
        public override string DescExtended()//mark//Range Advantage: &b / &c
        {

            if (this.PlusStat.AggroPer<-100)
            {
                GDESkillKeywordData k1 = new GDESkillKeywordData("KeyWord_SF_Dreamer_0_2");

                return base.DescExtended().Replace("&A", "<color=#FFFFFF>"+k1.Desc+"</color>\n").Replace("&b", this.range.ToString()).Replace("&c", this.maxrange.ToString());//base.Usestate_L.Info.Name);//.ToString());

            }
            else
            {
                return base.DescExtended().Replace("&A", "").Replace("&b", this.range.ToString()).Replace("&c", this.maxrange.ToString());//base.Usestate_L.Info.Name);//.ToString());

            }


        }
        public void BuffObject_Updata(BuffObject obj)
        {
            string str1 = this.range.ToString();
            /*
            int num = str1.Length-1;
            for (int i = 0; i < num; i++)
            {
                str1 = str1 + "   ";
            }
            */
            obj.StackText.text = str1;
        }
        /*
        public void AITargetSelect_Before()
        {
            this.PlusStat.Vanish = true;
            this.BuffStatUpdate();
        }
        public void AITargetSelect_After()
        {
            this.PlusStat.Vanish = false;
            this.BuffStatUpdate();
        }
        */
        public bool CheckSkyCommander()
        {
            bool flag1 = false;
            foreach (IP_CheckEquip ip_new in this.BChar.IReturn<IP_CheckEquip>())
            {
                try
                {
                    bool flag2 = false;
                    if (ip_new != null)
                    {
                        flag2=ip_new.CheckEquip(this.BChar);
                    }
                    flag1 = flag1 || flag2;
                }
                catch
                {
                }
            }
            return flag1;
        }
        public int Range
        {
            get
            {
                return this.range;
            }
            set
            {
                bool overload = false;
                int addnum = value - this.range;
                foreach (IP_RangeChange ip_new in this.BChar.IReturn<IP_RangeChange>())
                {
                    try
                    {
                        bool flag = false;
                        if (ip_new != null)
                        {
                            ip_new.RangeChange(this.BChar,this,this.range,value,ref flag);
                        }
                        overload = overload || flag;
                    }
                    catch
                    {
                    }
                }
                int range_before = this.range;
                if(overload)
                {
                    this.range = value;
                }
                else
                {
                    this.range=Math.Min(Math.Max(this.range,this.maxrange),value);
                }


                this.BuffStatUpdate();
                if (this.range <= 0)
                {
                    this.remove = true;
                    this.SelfDestroy();
                }

                foreach (IP_RangeChangeAfter ip_new in this.BChar.IReturn<IP_RangeChangeAfter>())
                {
                    try
                    {
                        if (ip_new != null)
                        {
                            ip_new.RangeChangeAfter(this.BChar, this,range_before,this.range,addnum);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
        public void RangeAddSingle(int num)
        {
            if(num > 0) 
            {
                for (int i = 0; i < num; i++)
                {
                    this.Range += 1;
                }
            }
            else if(num<0)
            {
                for (int i = 0; i < (-num); i++)
                {
                    this.Range -= 1;
                }
            }            
        }
        
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            //Debug.Log("Dreamer_sex_0_point_2:"+this.range);
            this.view = false;
            int n=this.range;
            if(this.range>0)
            {
                this.range = 0;
                this.RangeAddSingle(n);
            }
            //this.range = 0;
            this.BuffStat();
        }
        
        public override void Init()
        {
            this.force = true;
            if(this.view)
            {
                    this.range++;
            }
            else
            {
                //Debug.Log("Dreamer_sex_0_point_1");
                this.Range += 1;
            }

            base.Init();

        }
        // Token: 0x0600000B RID: 11 RVA: 0x000026BC File Offset: 0x000008BC
        public override void BuffStat()
        {
            base.BuffStat();
            /*
            if(this.view)
            {
                this.range++;
            }
            */
            this.PlusStat.dod =(float)(2 * this.range);

            this.PlusStat.AggroPer = (int)(-15 * this.range);
            this.PlusStat.DMGTaken =  (float)(-1 * Math.Max(this.range - 6, 0));


            if(this.BChar.Info.KeyData == "SF_Dreamer" && this.BChar.Info.Passive != null)
            {
                this.PlusPerStat.Damage = 2 * this.range;
            }
            if (this.BChar.Info.KeyData != "SF_Dreamer")
            {
                this.PlusPerStat.Damage = 0;

            }
            //base.BuffStat();


        }



        private bool flagup;
        public void TurnEnd()
        {
            //int AllyNum = 0;
            this.jlnum = this.Range;//8;
            foreach (BattleAlly battleAlly in BattleSystem.instance.AllyList)
            {
                if (!battleAlly.BuffFind("B_SF_Dreamer_0", false))
                {
                    //AllyNum++;
                    this.jlnum = 0;
                }
                else
                {
                    this.jlnum = Math.Min(this.jlnum, (battleAlly.BuffReturn("B_SF_Dreamer_0", false) as Bcl_drm_0).Range);
                }
            }
        }
        public void Turn()
        {
            if (this.jlnum > 0)
            {
                int num = this.jlnum;
                /*
                while (this.BChar.BuffFind("B_SF_Dreamer_0", false) && num >= 1)
                {
                    this.BChar.BuffReturn("B_SF_Dreamer_0", false).SelfStackDestroy();
                    num--;
                }
                */
                if(this.BChar.BuffFind("B_SF_Dreamer_0", false) && num >0)
                {
                    this.RangeAddSingle(-num);
                    num = 0;
                }
            }
        }
        //public override void FixedUpdate()
        //{
        //    foreach (BattleAlly battleAlly in BattleSystem.instance.AllyList)
        //    {
        //        if (!battleAlly.BuffFind("B_SF_Dreamer_0_oth", false))
        //        {
        //            //battleAlly.BuffAdd("B_SF_Dreamer_0_oth", battleAlly, false, 0, false, -1, false);
        //        }
        //    }
        //}

        public void OnlyDamage_Before(BattleChar Target)
        {
            //Debug.Log("only damage before bcl_drm_0");
            this.rec = Target.Recovery;
            /*判断是否为友军中点数最少的
            int jlmin = 255;
            foreach (BattleAlly battleAlly in BattleSystem.instance.AllyList)
            {
                if (!battleAlly.BuffFind("B_SF_Dreamer_0", false))
                {
                    //AllyNum++;
                    jlmin = 0;
                }
                else
                {
                    jlmin = Math.Min(jlmin, (battleAlly.BuffReturn("B_SF_Dreamer_0", false) as Bcl_drm_0).Range);
                }
            }

            this.pflag1 = (!Target.BuffFind("B_SF_Dreamer_0", false) || (Target.BuffReturn("B_SF_Dreamer_0", false) as Bcl_drm_0).Range == jlmin);// && Target.HP < Target.Recovery;
            */
            this.pflag1 = Target is BattleAlly && Target != this.BChar;//判断是其他友军。
        }
        public void OnlyDamage_After(BattleChar Char)
        {
            //Debug.Log("成功进入OnlyDamage后的patch");
            if (Char.Recovery < this.rec && this.pflag1)// && this.turnup)
            {
                int num = 1;
                if (num > 0)
                {
                    this.RangeAddSingle(-num);
                    num = 0;
                    //Debug.Log(Char.Info.Name+"失去体力极限，失去一层距离");
                }
            }
            this.rec = 0;
            this.pflag1 = false;
        }
        private int rec;
        private bool pflag1;

        private int jlnum = 0;

        //public bool isDreamer;
        public int range = 0;
        public int maxrange = 16;
        public bool view = true;
    }
    /*
    public class Bcl_drm_0_oth : Buff, IP_DamageTake, IP_HPChange//, IP_TargetedAlly
    {
        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            //            if (Target != this.BChar && Target.GetStat.Strength == false && Target.HP < Target.Recovery && NODEF == false && Dmg >= 1)
            //this.BChar.BuffAdd("B_SF_Dreamer_7_1", this.BChar, false, 0, false, -1, false);//检测点1
            this.rec = Target.Recovery;

            int jlmin = 16;
            foreach (BattleAlly battleAlly in BattleSystem.instance.AllyList)
            {
                if (!battleAlly.BuffFind("B_SF_Dreamer_0", false))
                {
                    //AllyNum++;
                    jlmin = 0;
                }
                else
                {
                    jlmin = Math.Min(jlmin, battleAlly.BuffReturn("B_SF_Dreamer_0", false).StackNum);
                }
            }

            this.pflag1 = (!this.BChar.BuffFind("B_SF_Dreamer_0", false) || this.BChar.BuffReturn("B_SF_Dreamer_0", false).StackNum == jlmin) && Target.HP < Target.Recovery;
        }

        public void HPChange(BattleChar Char, bool Healed)
        {
            //this.BChar.BuffAdd("B_SF_Dreamer_7_2", this.BChar, false, 0, false, -1, false);//检测点2
            if (Char.Recovery < this.rec && this.pflag1)// && this.turnup)
            {
                foreach (BattleAlly battleAlly in BattleSystem.instance.AllyList)
                {
                    int num = 1;
                    while (battleAlly.BuffFind("B_SF_Dreamer_0", false) && num >= 1)
                    {
                        battleAlly.BuffReturn("B_SF_Dreamer_0", false).SelfStackDestroy();
                        num--;
                    }
                }

                this.turnup = false;
            }
            this.rec = 0;
            this.pflag1 = false;
        }

        //public IEnumerator Targeted(BattleChar Attacker, List<BattleChar> SaveTargets, Skill skill)
        //{

        //this.turnup = true;
        //yield return null;
        //}
        private int rec;
        private bool pflag1;
        private bool turnup;
    }

    */









    public class Sex_drm_0 : Sex_ImproveClone, IP_SkillUse_BasicSkill, IP_BattleStart_Ones//,IP_SkillEffectBuffAdd
    {



        public override void Init()
        {
            base.Init();
            if(BattleSystem.instance!=null)
            {
                GDESkillKeywordData keyw = this.MySkill.MySkill.PlusKeyWords.Find(a => a.Key == "KeyWord_SF_Dreamer_0_1");
                this.MySkill.MySkill.PlusKeyWords.Remove(keyw);
            }

        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            List<GDEBuffData> list1 = this.MySkill.MySkill.Effect_Self.Buffs.FindAll(a => a.Key == "B_SF_Dreamer_0");
            foreach(Skill_Extended sex in this.MySkill.AllExtendeds)
            {
                foreach(BuffTag btg in sex.SelfBuff)
                {
                    if(btg.BuffData.Key== "B_SF_Dreamer_0")
                    {
                        list1.Add(btg.BuffData);
                    }
                }
            }


            //this.BChar.BuffAdd("B_SF_Dreamer_0", this.BChar, false, 0, false, -1, false);
            
        }
        
        public void SkillUseBasicSkill(Skill skill)
        {
            if (this.MySkill.BasicSkill && skill==this.MySkill)
            {
                //base.Init();
                //Debug.Log("SkillUseHandBefore");

                //this.BChar.MyTeam.BasicSkillRefill(this.BChar, this.BChar.BattleBasicskillRefill);
                (this.BChar as BattleAlly).MyBasicSkill.CoolDownNum = 0;

                if ((this.BChar as BattleAlly).MyBasicSkill.buttonData == null || (this.BChar as BattleAlly).MyBasicSkill.ThisSkillUse)
                {

                    Skill coskill = this.MySkill.CloneSkill(false, null, null, false);


                    //coskill.APChange = 2;
                    (this.BChar as BattleAlly).MyBasicSkill.SkillInput(coskill);
                }
                if ((this.BChar as BattleAlly).MyBasicSkill.ThisSkillUse)
                {
                    (this.BChar as BattleAlly).MyBasicSkill.InActive = false;
                    (this.BChar as BattleAlly).MyBasicSkill.ThisSkillUse = false;
                }
                if ((this.BChar as BattleAlly).MyBasicSkill.InActive)
                {
                    (this.BChar as BattleAlly).MyBasicSkill.InActive = false;
                }



            }
        }
        /*
        public override void SkillUseSingleAfter(Skill SkillD, List<BattleChar> Targets)
        {
            if(this.MySkill.BasicSkill)
            {
                //base.Init();
                //Debug.Log("APChange:"+ this.APChange);

                //this.BChar.MyTeam.BasicSkillRefill(this.BChar, this.BChar.BattleBasicskillRefill);
                (this.BChar as BattleAlly).MyBasicSkill.CoolDownNum = 0;

                if ((this.BChar as BattleAlly).MyBasicSkill.buttonData == null || (this.BChar as BattleAlly).MyBasicSkill.ThisSkillUse)
                {

                    Skill coskill = this.MySkill.CloneSkill(false, null, null, false);


                    //coskill.APChange = 2;
                    (this.BChar as BattleAlly).MyBasicSkill.SkillInput(coskill);
                }
                if ((this.BChar as BattleAlly).MyBasicSkill.ThisSkillUse)
                {
                    (this.BChar as BattleAlly).MyBasicSkill.InActive = false;
                    (this.BChar as BattleAlly).MyBasicSkill.ThisSkillUse = false;
                }
                if ((this.BChar as BattleAlly).MyBasicSkill.InActive)
                {
                    (this.BChar as BattleAlly).MyBasicSkill.InActive = false;
                }

                

            }
        }
        */
        public void BattleStart(BattleSystem Ins)//IP_BattleStart_Ones
        {
            if (!SaveManager.IsUnlock(this.MySkill.MySkill.KeyID, SaveManager.NowData.unlockList.SkillPreView))
            {
                SaveManager.NowData.unlockList.SkillPreView.Add(this.MySkill.MySkill.KeyID);
            }

        }


    }



    public class Sex_drm_1 : Sex_ImproveClone,IP_SkillUsePatch_Before
    {

        //目前未使用，技能1用的是Sex_drm_0
        /*
        public override void Init()
        {
            base.Init();
            this.APChange = -1;
        }
        public override void HandInit()
        {
            base.HandInit();
            this.APChange = 0;
        }*/
        /*
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            //获得相同的牌
            //Skill skill = Skill.TempSkill("S_SF_Dreamer_1", this.BChar, this.BChar.MyTeam);

            //Skill_Extended skill_Extended = new Skill_Extended();
            //skill_Extended.PlusSkillStat.cri = 200f;
            //SkillD.ExtendedAdd(skill_Extended);

            //this.SetNonQuick = true;
            Skill skill = this.MySkill.CloneSkill(true, null, null, true);
            //this.SetNonQuick = false;

            skill.isExcept = true;
            skill.AutoDelete = 1;
            skill.BasicSkill = false;
            skill.NotCount = false;
            foreach(Skill_Extended ex in skill.AllExtendeds)
            {
                if(ex.NotCount==true)
                {
                    ex.NotCount = false;
                }
            }
            
            for (int i = 0; i < skill.AllExtendeds.Count; i++) 
            {
                if (!skill.AllExtendeds[i].BattleExtended && !skill.AllExtendeds[i].isDataExtended) 
                {
                    skill.AllExtendeds[i].SelfDestroy();
                }
            }
            //skill.PlusHit = true;


            this.BChar.MyTeam.Add(skill, true);
            //if (this.ovap < this.BChar.Overload)
            //{
            //this.BChar.Overload--;
            //}
            if (SkillD.NotCount == false)
            {
                BattleSystem.instance.AllyTeam.AP += (int)Math.Min(this.ovap,0.5*this.MySkill.UsedApNum);
            }


        }
        */
        public void SkillUsePatch_Before(BattleChar TargetChar, Skill TargetSkill, Skill SelectSkill)
        {
            if(SelectSkill==this.MySkill)
            {
                //UnityEngine.Debug.Log("Sex_1_SkillUsePatch_Before");

                Skill skill = this.MySkill.CloneSkill(true, null, null, true);

                skill.isExcept = true;
                skill.AutoDelete = 1;
                skill.BasicSkill = false;
                skill.NotCount = false;
                //this.SetNonQuick = true;

                foreach (Skill_Extended ex in skill.AllExtendeds)
                {
                    if (ex.NotCount == true)
                    {
                        ex.NotCount = false;
                    }
                    if (ex is Sex_drm_1 sex1)
                    {
                        sex1.plusattack = true;
                    }
                }

                for (int i = 0; i < skill.AllExtendeds.Count; i++)
                {
                    if (!skill.AllExtendeds[i].BattleExtended && !skill.AllExtendeds[i].isDataExtended)
                    {
                        skill.AllExtendeds[i].SelfDestroy();
                    }
                }
                //skill.PlusHit = true;


                this.BChar.MyTeam.Add(skill, true);
                //if (this.ovap < this.BChar.Overload)
                //{
                //this.BChar.Overload--;
                //}
                if (this.plusattack)//this.MySkill.NotCount == false)
                {
                    BattleSystem.instance.AllyTeam.AP += (int)Math.Min(this.BChar.Overload, Math.Max(0, this.MySkill.AP - 1));
                }
            }

            
        }

        public bool plusattack = false;
    }




    public class Sex_drm_2 : Sex_ImproveClone, IP_SkillUse_User_After, IP_RangeChangeAfter,IP_AntiVanish//, IP_BuffAdd, IP_BuffAddAfter//,IP_DamageChange//, IP_SkillCastingStart
    {
        // Token: 0x06000010 RID: 16 RVA: 0x00002448 File Offset: 0x00000648
        public override string DescExtended(string desc)
        {
            //EX_drm_2 testSkill = this.MySkill.ExtendedFind_DataName("EX_SF_Dreamer_2") as EX_drm_2;
            //bool flagT = testSkill.ismax;
            //if(flagT)
            //{
            //return base.DescExtended(desc).Replace("&a", "<color=#32CD32>最高</color>").Replace("&c", ((int)(this.MySkill.TargetDamage)).ToString()).Replace("&d", ((int)(100 * this.plusp)).ToString());

            //}
            //else
            //{
            //return base.DescExtended(desc).Replace("&a", "<color=#F62817>最低</color>").Replace("&c", ((int)(this.MySkill.TargetDamage )).ToString()).Replace("&d", ((int)(100 * this.plusp)).ToString());

            //}
            //this.RecSync();
            return base.DescExtended(desc).Replace("&c", ((int)(this.maxlistnum)).ToString()).Replace("&d", ((int)(this.CaculatePlusPer(this.listjl)*100)).ToString());

        }

        public bool AntiVanish(BattleChar Target, Skill skill)
        {
            if(skill==this.MySkill)
            {
                return true;
            }
            return false;
        }
        
        
        public override void UsedDeckInit()
        {
            //UnityEngine.Debug.Log("UsedDeckInit:" + this.listjl.Count);
            for (int i = 0; i < this.MySkill.AllExtendeds.Count; i++)
            {

                if (this.MySkill.AllExtendeds[i] is EX_drm_2_1 ex21)
                {
                    //UnityEngine.Debug.Log("UsedDeckInit-记录：" + ex21.list_int.Count);
                }
            }
            /*
            this.RecSync();

            //UnityEngine.Debug.Log("UsedDeckInit2:" + this.listjl.Count);
            for (int i = 0; i < this.MySkill.AllExtendeds.Count; i++)
            {

                if (this.MySkill.AllExtendeds[i] is EX_drm_2_1 ex21)
                {
                    //UnityEngine.Debug.Log("UsedDeckInit2-记录：" + ex21.list_int.Count);
                }
            }
            */
        }
        
        
        public override void Init()
        {
            base.Init();
            if(BattleSystem.instance != null)
            {
                BattleSystem.instance.StartCoroutine(this.Init_Delay());
            }
            //this.RecSync();
        }
        
        
        public IEnumerator Init_Delay()
        {
            yield return new WaitForFixedUpdate();
            this.RecSync();
            yield break;
        }
        public void RecSync()
        {
            //List<int> list_ans = this.listjl;
            //bool needRecover = false;
            //UnityEngine.Debug.Log("信息同步:"+ this.listjl.Count);
            for(int i=0;i<this.MySkill.AllExtendeds.Count;i++)
            {
                
                if (this.MySkill.AllExtendeds[i] is EX_drm_2_1 ex21)
                {
                    //UnityEngine.Debug.Log("记录数量：" + ex21.list_int.Count );
                    if (this.CaculatePlusPer(this.listjl) <this.CaculatePlusPer(ex21.list_int))
                    {
                        this.listjl.Clear();
                        this.listjl.AddRange(ex21.list_int);
                    }
                    else
                    {
                        ex21.list_int.Clear();
                        ex21.list_int.AddRange(this.listjl);
                    }
                }
            }

            int exNum = this.MySkill.AllExtendeds.FindAll(a => a is EX_drm_2_1).Count;
            if (exNum <1)
            {
                //UnityEngine.Debug.Log("新增EX_21");

                //battleextended版本
                /*
                EX_drm_2_1 ex21=this.MySkill.ExtendedAdd_Battle(new EX_drm_2_1()) as EX_drm_2_1;
                ex21.CanMultyStack = true;
                ex21.isDataExtended = true;
                ex21.list_int.AddRange(this.listjl);
                */
                //json内写skillExtendedItem版本
                EX_drm_2_1 ex21 = this.MySkill.ExtendedAdd("EX_SF_Dreamer_2_1") as EX_drm_2_1;
                ex21.isDataExtended = true;
                ex21.list_int.AddRange(this.listjl);
            }
            else if(exNum > 1)
            {
                //this.MySkill.AllExtendeds.RemoveAll(a => a is EX_drm_2_1);
                //UnityEngine.Debug.Log("移除EX_21");
                bool flag1 = false;
                for (int i = 0; i < this.MySkill.AllExtendeds.Count; i++)
                {

                    if (this.MySkill.AllExtendeds[i] is EX_drm_2_1)
                    {
                        if (!flag1)
                        {
                            flag1 = true;
                        }
                        else
                        {
                            this.MySkill.AllExtendeds.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }

            this.PlusSkillPerStat.Damage = (int)((this.MySkill.MySkill.Effect_Target.DMG_Per * this.CaculatePlusPer(this.listjl)) - this.MySkill.MySkill.Effect_Target.DMG_Per);

        }
        public void RecClear()
        {
            //UnityEngine.Debug.Log("清除信息" );
            bool flag1 = false;
            for (int i = 0; i < this.MySkill.AllExtendeds.Count; i++)
            {

                if (this.MySkill.AllExtendeds[i] is EX_drm_2_1 ex21)
                {
                    ex21.list_int.Clear();
                    if (!flag1)
                    {
                        flag1 = true;
                    }
                    else
                    {
                        this.MySkill.AllExtendeds.RemoveAt(i);
                        i--;
                    }
                }
            }
            this.listjl.Clear();
        }


        public double CaculatePlusPer(List<int> list_int)
        {
            double per = 1;
            foreach (int jl in list_int)
            {
                per = per * (100 + Math.Min(16, jl)) / 100;
            }
            return per;
        }
        //public override void FixedUpdate()//EX切换索敌逻辑版本，保留作为参考
        //{
        //List<Skill_Extended> listex = this.MySkill.AllExtendeds;
        //bool delete = false;
        //foreach(Skill_Extended sex in listex)
        //{
        //if(sex.Name=="EX_SF_Dreamer_2")
        //{
        //if(delete)
        //{
        //this.MySkill.ExtendedDelete_Dataname("EX_SF_Dreamer_2");
        //}
        //else
        //{
        //delete = true;
        //}
        //}
        //}

        //}
        public override bool TargetSelectExcept(BattleChar ExceptTarget)
        {
            //EX_drm_2 testSkill = this.MySkill.ExtendedFind_DataName("EX_SF_Dreamer_2") as EX_drm_2;EX切换索敌逻辑版本，保留作为参考
            //bool flagT = testSkill.ismax;
            //List<BattleChar> list = new List<BattleChar>();
            //list = EnemyHpSelect(flagT, ExceptTarget);

            List<BattleChar> listmin = new List<BattleChar>();
            listmin = EnemyHpSelect(false, ExceptTarget);
            List<BattleChar> listmax = new List<BattleChar>();
            listmax = EnemyHpSelect(true, ExceptTarget);

            bool test = false;
            if (ExceptTarget.GetStat.HideHP)//list==null)
            {
                test = true;
            }
            else
            {
                for (int i = 0; i <= listmin.Count - 1; i++)
                {
                    if (listmin[i] == ExceptTarget)
                    {
                        test = true;
                    }
                }
                for (int i = 0; i <= listmax.Count - 1; i++)
                {
                    if (listmax[i] == ExceptTarget)
                    {
                        test = true;
                    }
                }
            }
            return !test;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {



            this.BChar.BuffAdd("B_SF_Dreamer_2", this.BChar, false, 0, false, -1, false);//暴伤200%实现备选（曾用）


            //this.SkillBasePlus.Target_BaseDMG = (int)Misc.PerToNum((float)this.MySkill.TargetDamage, (float)(100 * this.plusp - 100));//增伤方式1（曾用）
            //this.PlusSkillPerStat.Damage= (int)(SkillD.MySkill.Effect_Target.DMG_Per * (this.plusp-1));增伤方式2
            //this.plusp = 1;

            //this.listjl.Clear();
            this.RecClear();
        }



        public List<BattleChar> EnemyHpSelect(bool ismax,BattleChar orgTarget)
        {
            List<BattleChar> list = new List<BattleChar>();
            //list.AddRange(BattleSystem.instance.EnemyTeam.AliveChars);
            //list.AddRange(BattleSystem.instance.EnemyTeam.AliveChars_Vanish);
            //list.AddRange(orgTarget.MyTeam.AliveChars);
            list.AddRange(orgTarget.MyTeam.AliveChars_Vanish);


            for (int i = 0; i <= list.Count - 1; i++)
            {
                if (list[i].GetStat.HideHP)
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
            if(list.Count<1)
            {
                return null;
            }

            list = (from x in list
                    orderby x.HP
                    select x).ToList<BattleChar>();


            int tarhp = 0;
            if (ismax)
            {
                tarhp = list[list.Count - 1].HP;
                for (int i = 0; i <= list.Count - 1; i++)
                {
                    if (list[i].HP < tarhp)
                    {
                        list.RemoveAt(i);
                        i--;
                    }
                }
            }
            else
            {
                tarhp = list[0].HP;
                for (int i = 0; i <= list.Count - 1; i++)
                {
                    if (list[i].HP > tarhp)
                    {
                        list.RemoveAt(i);
                        i--;
                    }
                }
            }
            return list;
        }
        //public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        //{
            //Debug.Log("DamageChange point1");
            //if (SkillD==this.MySkill && Cri)
            //{
                //Debug.Log("DamageChange point2");
                //return (int)(Damage * 1.33334);
            //}
                //return Damage;
        //}
        public void SkillUseAfter(Skill SkillD)
        {
        this.BChar.BuffReturn("B_SF_Dreamer_2", false).SelfStackDestroy();
        }

        public override void DiscardSingle(bool Click)
        {
            //this.plusp = 1;

            //this.listjl.Clear();
        }


        public void RangeChangeAfter(BattleChar bc, Buff buff, int range_before, int range_after, int addnum)
        {


            if (bc == this.BChar && addnum>0)
            {

                this.RecSync();
                //this.plusp1 = Math.Min(1000000,this.plusp1 * (100 + Math.Min(16, range_before)) / 100);
                //this.plusp2 = this.plusp2 + this.holdturn * range_after;
                this.listjl.Add(range_before);
                if(listjl.Count>this.maxlistnum)
                {
                    this.listjl.RemoveAt(0);
                }
                this.RecSync();
                //this.plusp3 = 1;
                //foreach (int jl in listjl)
                //{
                    //this.plusp3 = this.plusp3 * (100 + Math.Min(16, jl)) / 100;
                //}


                //this.PlusSkillPerStat.Damage = (int)((this.MySkill.MySkill.Effect_Target.DMG_Per * plusper) - this.MySkill.MySkill.Effect_Target.DMG_Per);

            }
        }
        
        //private double plusp1 = 1;
        //private float plusp2 = 0;
        //private double plusp3 = 1;
        //private double plusp = 100;

        private int holdturn = 1;


        private int maxlistnum = 96;
        bool oldbuff;
        private List<int> listjl =new List<int>();
        //private BattleChar selectChar;
        
    }

    public class Bcl_drm_2 : Buff
    {

        public override void Init()
        {
            base.Init();
            this.PlusStat.PlusCriDmg = 50f;
        }
    }

    public class EX_drm_2_1 : Sex_ImproveClone
    {

        public List<int> list_int = new List<int>();
    }
    public class EX_drm_2 : Sex_ImproveClone
    {
        public override string DescExtended(string desc)
        {
            bool flag = this.ismax;
            string result;
            if (flag)
            {
                result = base.DescExtended(desc).Replace("&a", "<color=#32CD32>最高</color>");
            }
            else
            {
                result = base.DescExtended(desc).Replace("&a", "<color=#F62817>最低</color>");
            }
            return result;
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();
            bool flag = this.BuffIcon != null;
            if (flag)
            {
                bool flag2 = this.BuffIcon.GetComponent<EX_drm_2.EX_drm_2_click>() == null;
                if (flag2)
                {
                    this.BuffIcon.AddComponent<EX_drm_2.EX_drm_2_click>().Init(this);
                }
            }

        }


        // Token: 0x06000005 RID: 5 RVA: 0x000020FC File Offset: 0x000002FC
        public void go()
        {
            this.ismax = !this.ismax;
            //this.MySkill.flagT = this.ismax;
            

        }
        public bool ismax;
        private EX_drm_2.EX_drm_2_click  behavior;

        public class EX_drm_2_click : MonoBehaviour, IEventSystemHandler, IPointerClickHandler
        {
            // Token: 0x06000194 RID: 404 RVA: 0x00008639 File Offset: 0x00006839
            public void Init(EX_drm_2 main)
            {
                this.Main = main;
                main.behavior = this;
            }

            // Token: 0x06000195 RID: 405 RVA: 0x0000864C File Offset: 0x0000684C
            public void OnPointerClick(PointerEventData eventData)
            {

                this.Main.go();

            }

            // Token: 0x0400002D RID: 45
            private EX_drm_2 Main;
        }
    }



    public class Sex_drm_3 : Sex_ImproveClone, IP_SkillUse_Team, IP_SkillCastingStart, IP_DodgeCheck_After2
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", (this.attackPhase).ToString());

            
        }
        public void SkillUseTeam(Skill skill)
        {
            if (this.MySkill.IsNowCounting)
            {
                if (skill.Master == this.BChar)
                {
                    this.attackPhase--;
                    this.attackPhase=Math.Max(this.attackPhase, 0);
                }
                else
                {
                    this.attackPhase++;
                }
            }
        }
        /*
        public override void HandInit()
        {
            base.HandInit();
            this.attackPhase = 3;
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
            this.attackPhase = 3;
        }
        public async override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            
            //Debug.Log("turn act:"+this.MySkill.Master.MyTeam.TurnActionNum);
            //bool flag1 = Targets[0].BuffFind("B_SF_Dreamer_4", false) && Targets[0].BuffReturn("B_SF_Dreamer_4", false).Usestate_L == SkillD.Master;//修复协同设计不受弱点锁定影响的问题
            base.SkillUseSingle(SkillD, Targets);
            Skill skill = this.MySkill.CloneSkill(true, null, null, true);
            skill.isExcept = true;
            skill.FreeUse = true;
            skill.PlusHit = true;

            for (int i = 0; i < this.attackPhase; i++)
            {

                await Task.Delay(200);

                //this.BChar.ParticleOut(this.MySkill, skill, Targets[0]);

                //BattleSystem.DelayInput(this.Plushit(skill,Targets[0]));
                this.Plushit(skill, Targets[0]);

            }

        }
        public void Plushit(Skill skill,BattleChar Target)
        {
            if (this.BChar.BattleInfo.EnemyList.Count != 0)
            {


                if (Target.IsDead)
                {
                    //Debug.Log("Target is dead:"+Target.Info.Name);
                    //this.newTar = this.BChar.BattleInfo.EnemyList.Random(this.BChar.GetRandomClass().Main);
                    //this.BChar.ParticleOut(this.MySkill, skill, this.newTar);
                    if (!this.BChar.BuffFind("B_SF_Dreamer_3_1", false))
                    {
                        this.BChar.BuffAdd("B_SF_Dreamer_3_1", this.BChar, false, 0, false, -1, false);
                    }
                    //Bcl_drm_3_1 buff=this.BChar.BuffAdd("B_SF_Dreamer_3_1", this.BChar, false, 0, false, -1, false)as Bcl_drm_3_1;
                    Bcl_drm_3_1 buff = this.BChar.BuffReturn("B_SF_Dreamer_3_1", false) as Bcl_drm_3_1;
                    buff.skList.Add(skill);
                }
                else
                {
                    //Debug.Log("Target is alive:" + Target.Info.Name);
                    this.BChar.ParticleOut(this.MySkill, skill, Target);
                    //this.BChar.ParticleOut(skill, Target);
                }
                //yield return new WaitForSecondsRealtime(0.2f);
            }
            //yield break;
        }
        public void DodgeCheck_After2(BattleChar hit, SkillParticle SP, ref bool dod)
        {
            //Debug.Log("point1 of ChangeDamageState");
            if(Standard_Tool.JudgeSkillSex(SP.SkillData,this))
            {
                if (SP.SkillData.MySkill.Key == this.MySkill.MySkill.Key)
                {
                    //Debug.Log("point1 of DodgeCheck_After");

                    if (!this.BChar.BuffFind("B_SF_Dreamer_3_1", false))
                    {
                        this.BChar.BuffAdd("B_SF_Dreamer_3_1", this.BChar, false, 0, false, -1, false);
                    }
                    //Bcl_drm_3_1 buff=this.BChar.BuffAdd("B_SF_Dreamer_3_1", this.BChar, false, 0, false, -1, false)as Bcl_drm_3_1;
                    Bcl_drm_3_1 buff = this.BChar.BuffReturn("B_SF_Dreamer_3_1", false) as Bcl_drm_3_1;

                    buff.tar = hit;// SP.ALLTARGET[0];
                    buff.mainex = this;
                    buff.spList.Add(SP);
                    //buff.tarlist.Add(SP.ALLTARGET[0]);
                    //Debug.Log("point2 of DodgeCheck_After");
                }
            }

        }



        public int attackPhase = 3;
        public BattleChar newTar;
    }

    public class Bcl_drm_3_1 : Buff
    {
        public override void FixedUpdate()
        {
            if (this.spList.Count > 0)
            {
                //Debug.Log("帧检测运行中");
                for (int i = 0; i < this.spList.Count; i++)
                {
                    if (this.spList[i].SelfEffect)
                    {
                        this.spList.RemoveAt(i);
                        i--;
                        for (int j = 0; j < this.spList.Count; j++)
                        {
                            this.spList[j].SelfEffect = false;
                        }
                            //Debug.Log("帧检测运行点1,当前队列数量" + this.spList.Count);
                    }
                    else if (this.spList[i].ALLTARGET[0].IsDead)
                    {
                        Skill skill = this.spList[i].SkillData.CloneSkill(true, null, null, true);
                        skill.isExcept = true;
                        skill.FreeUse = true;
                        skill.PlusHit = true;
                        //this.mainex.Plushit(skill, this.spList[i].ALLTARGET[0]);
                        this.skList.Add(skill);


                        this.spList.RemoveAt(i);
                        i--;
                        //Debug.Log("帧检测运行点2,当前队列数量"+this.spList.Count);
                    }
                }
            }

            if(this.skList.Count > 0)
            {
                if(this.trigger)
                {
                    if(this.tar.IsDead && this.BChar.BattleInfo.EnemyList.Count>0)
                    {
                        this.tar = this.BChar.BattleInfo.EnemyList.Random(this.BChar.GetRandomClass().Main);
                    }
                    this.mainex.Plushit(this.skList[0], this.tar);
                    this.skList.RemoveAt(0);
                    this.trigger = false;
                    this.Wait();
                }
            }

            if(this.skList.Count<=0 && this.spList.Count<=0 && this.trigger)
            {
                base.SelfDestroy();
            }
            //Debug.Log("Bcl_drm_3_1 存在");
            //Debug.Log("this.skList.Count"+ this.skList.Count);
            //Debug.Log("this.spList.Count" + this.spList.Count);
            //Debug.Log("this.trigger" + this.trigger);
        }
        public async void Wait()
        {
            await Task.Delay(200);
            this.trigger = true;
        }
        public BattleChar tar;
        public List<SkillParticle> spList = new List<SkillParticle>();

        public List<Skill> skList = new List<Skill>();
        public Sex_drm_3 mainex;

        private bool trigger=true;
    }


    public class Sex_drm_4 : Sex_ImproveClone,IP_AntiVanish
    {
        public bool AntiVanish(BattleChar Target, Skill skill)
        {
            if (skill == this.MySkill)
            {
                return true;
            }
            return false;
        }
    }

    public interface IP_B4_Peneration
    {
        // Token: 0x06002759 RID: 10073
        bool B4_Peneration(BattleChar Hit, SkillParticle SP);
    }

    public class Bcl_drm_4 : StaInBuff, IP_TurnEnd,IP_BuffAdd,IP_BuffUpdate,IP_DodgeCheck_After,IP_AntiVanish//,IP_CriPerChange,IP_GetCriPer_Final//, IP_BuffAddAfter//mark
    {
        public static bool Check_B4_Peneration(BattleChar Hit, SkillParticle SP)
        {
            bool ans = false;
            foreach (IP_B4_Peneration ip_1 in SP.SkillData.Master.IReturn<IP_B4_Peneration>(SP.SkillData))
            {
                if (ip_1 != null)
                {
                    ans =ans ||  ip_1.B4_Peneration(Hit,SP);
                }
            }
            return ans;
        }

        public override string DescExtended()//mark
        {


            string allname = base.StackInfo[0].UseState.Info.Name;
            for (int i = 1;i< base.StackInfo.Count;i++)
            {
                allname = allname + "/" + base.StackInfo[i].UseState.Info.Name;
            }
            return base.DescExtended().Replace("&B4", allname);//base.Usestate_L.Info.Name);//.ToString());
        }
        public override void Init()
        {
            base.Init();
            this.PlusStat.crihit = 20;
        }
        public override bool GetForce()
        {
            return true;
        }
        public bool AntiVanish(BattleChar Target, Skill skill)
        {
            if (Target==this.BChar && this.StackInfo.Find(a=>a.UseState==skill.Master)!=null)
            {
                return true;
            }
            return false;
        }
        /*
        public override void ForBuffAdded_Before(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {
            base.ForBuffAdded_Before(BuffUser, BuffTaker, addedbuff);
            this.Buffadded(BuffUser, BuffTaker, addedbuff);
        }
        */
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)//mark
        {
            //Debug.Log("B_drm_4 Buffadd_1");
            if (BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key && this.StackNum >= this.BuffData.MaxStack && this.StackInfo.Find(a => a.UseState == BuffUser) == null) 
            {
                //this.BuffData.MaxStack++;

                //this.isb4 = true;
                //this.oldb4 = this.StackInfo[0];
                //Debug.Log("B_drm_4 Buffadd_2");
                this.StackInfo.Insert(0, this.StackInfo[0]);
                //Debug.Log("B_drm_4 Buffadd_3:"+this.StackNum);
            }
            else if(BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key && this.StackNum >= this.BuffData.MaxStack && this.StackInfo.Find(a => a.UseState == BuffUser) != null)
            {
                int id = this.StackInfo.FindIndex(a => a.UseState == BuffUser);
                StackBuff st = this.StackInfo[id];
                this.StackInfo.RemoveAt(id);
                this.StackInfo.Insert(0, st);
            }
        }


        public void BuffUpdate(Buff MyBuff)
        {
            /*    
            if (this.StackNum < this.BuffData.MaxStack)
                {
                    this.BuffData.MaxStack = Math.Max(1, this.StackNum);
                }
            */
            bool checkover = false;
        IL_001:
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
                        // this.StackInfo.RemoveAt(i);
                        if(checkover)
                        {
                            //Debug.Log("RemoveStack");
                            this.RemoveStack(i);
                            i--;
                        }
                        else
                        {
                            //Debug.Log("CheckAndSet");
                            this.CheckAndSet();
                            checkover = true;
                            goto IL_001;
                        }
                    }

                }
            }
            //this.Drm4_Update();

            this.HistorySet(null);
        }

        

        public void DodgeCheck_After(BattleChar hit, SkillParticle SP,ref bool dod)//mark//修改点之一
        {
            //Debug.Log("判断锁定");
            bool isforall = SP.SkillData.TargetTypeKey == "all_enemy" || SP.SkillData.TargetTypeKey == "all_ally" || SP.SkillData.TargetTypeKey == "all" || SP.SkillData.TargetTypeKey == "all_allyorenemy" || SP.SkillData.TargetTypeKey == "all_other";

            if (!isforall &&SP.SkillData.IsDamage&& SP.ALLTARGET.Count <= 1 &&hit==this.BChar /*&& SP.ALLTARGET[0]==this.BChar*/ &&  this.StackInfo.Find(a => a.UseState == SP.UseStatus) != null)
            {
                //Skill_Extended skill_Extended = new Skill_Extended();
                //skill_Extended.PlusSkillStat.HitMaximum = true;
                //skill_Extended.PlusSkillStat.Penetration = 100f;
                //SP.SkillData.ExtendedAdd(skill_Extended);

                Extended_drm_4 ex1 = Skill_Extended.DataToExtended("EX_SF_Dreamer_4") as Extended_drm_4;
                ex1.PlusSkillStat.HitMaximum = true;
                ex1.PlusSkillStat.Penetration = 100f;
                
                SP.SkillData.ExtendedAdd(ex1);
                ex1.mainbuff = this;

                dod = dod && false;


                this.HistoryCheck(null);
                this.HistorySet(SP.UseStatus);
                //Debug.Log("判断命中1");
                //this.overHitSkills.Add(SP.SkillData);//弱点标记&弹道修正-命中&暴击&爆伤转化
                //this.overCriSkills.Add(SP.SkillData);//弱点标记&弹道修正-命中&暴击&爆伤转化
            }
            else if (!isforall && SP.SkillData.IsDamage&&SP.ALLTARGET.Count <= 1 && hit == this.BChar &&/* SP.ALLTARGET[0] == this.BChar &&*/ Bcl_drm_14.Checkb14())
            {
                Extended_drm_4 ex1 = Skill_Extended.DataToExtended("EX_SF_Dreamer_4") as Extended_drm_4;
                ex1.PlusSkillStat.HitMaximum = true;
                ex1.PlusSkillStat.Penetration = 100f;

                SP.SkillData.ExtendedAdd(ex1);
                ex1.mainbuff = this;

                dod = dod && false;

                this.HistoryCheck(null);
                this.HistorySet(this.StackInfo[0].UseState);
                //Debug.Log("判断命中2");
                //this.overHitSkills.Add(SP.SkillData);//弱点标记&弹道修正-命中&暴击&爆伤转化
                //this.overCriSkills.Add(SP.SkillData);//弱点标记&弹道修正-命中&暴击&爆伤转化
            }
        }
        /*
        public List<Skill> overHitSkills = new List<Skill>();//弱点标记&弹道修正-命中&暴击&爆伤转化
        public void CriPerChange(Skill skill, BattleChar Target, ref float CriPer)//弱点标记&弹道修正-命中&暴击&爆伤转化
        {
            if (Target == this.BChar && this.overHitSkills.Contains(skill))
            {
                this.overHitSkills.Remove(skill);


                int overhit = 0;
                GDESkillEffectData effect_Target = skill.MySkill.Effect_Target;
                if (skill.Master != Target.BattleInfo.DummyChar)
                {
                    float num = Misc.PerToNum(skill.Master.GetStat.hit, (float)effect_Target.HIT + skill.PlusSkillStat.hit);
                    num -= Target.GetStat.dod;
                    overhit = (int)Math.Max(num, 0);
                }
                else
                {
                    overhit = 0;
                }
                if (overhit > 0)
                {
                    CriPer = (float)(CriPer + 0.5 * overhit);
                }
            }
        }
        
        public void GetCriPer_Final(Skill skill, int cri_Result, BattleChar Target)//弱点标记&弹道修正-命中&暴击&爆伤转化
        {
            if (Target == this.BChar && this.overCriSkills.Contains(skill))
            {
                this.overCriSkills.Remove(skill);

                skill.Master.Buffs.RemoveAll(a => a is Bcl_drm_4_cridmg);

                Buff buff = skill.Master.BuffAdd("B_SF_Dreamer_4_cridmg", skill.Master, false, 0, false, -1, false);
                buff.PlusStat.PlusCriDmg = (int)(0.5*cri_Result);
                skill.Master.Info.ForceGetStat();
                Stat non= skill.Master.GetStat;

            }
        }
        public List<Skill> overCriSkills = new List<Skill>();//弱点标记&弹道修正-命中&暴击&爆伤转化
        */

        //public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        //{
        //if(this.isb4)
        //{
        //this.StackInfo.Insert(0,this.oldb4);
        //this.BuffStatUpdate();
        //}
        //this.isb4 = false;
        //}
        //private StackBuff oldb4;
        //private bool isb4;


        public override void FixedUpdate()//mark
        {
            /*
            foreach(StackBuff stack in this.StackInfo)
            {
                
                if(!stack.UseState.BuffFind("B_SF_Dreamer_4_2", false))
                {
                    stack.UseState.BuffAdd("B_SF_Dreamer_4_2", stack.UseState, false, 0, false, -1, false);
                }
            }
            */
            for(int i=0;i<this.StackInfo.Count;i++)
            {
                if ((this.StackInfo.FindAll(a => a.UseState == this.StackInfo[i].UseState)).Count>1)
                {
                    //this.StackInfo.RemoveAt(i);
                    this.RemoveStack(i);
                    i--;
                }
            }
        }
        
        public void TurnEnd()
        {



            for (int i = 0; i < this.StackInfo.Count; i++)
            {
                
                if (this.HistoryCheck(this.StackInfo[i].UseState) && !(Bcl_drm_17_T.CheckB17T(this.BChar, this.StackInfo[i].UseState)))
                {
                    //this.StackInfo.RemoveAt(i);
                    this.RemoveStack(i);
                    i--;
                }
            }
            this.HistorySet(null);

            if (this.StackNum <= 0)
            {
                //base.SelfDestroy();//mark
                this.InBuffDestroy();
            }
            this.BuffStatUpdate();
        }
        
        public void HistorySet(BattleChar bc)
        {
            for (int i = 0; i < this.plist.Count; i++)
            {
                if (this.StackInfo.Find(a => a.UseState == this.plist[i].bc) == null)
                {
                    this.plist.RemoveAt(i);
                    i--;
                }
            }
            foreach (StackBuff sta in this.StackInfo)
            {
                if (this.plist.Find(a => a.bc == sta.UseState) == null)
                {
                    Used_drm used_Drm = new Used_drm();
                    used_Drm.bc = sta.UseState;
                    this.plist.Add(used_Drm);
                }
            }

            if(bc!=null)
            {


                for (int i = 0; i < this.plist.Count; i++)
                {
                    if (bc == this.plist[i].bc )
                    {
                        this.plist[i].used = true;
                    }
                }
            }
        }

        public bool HistoryCheck(BattleChar bc)
        {
            bool flag = false;
            if (bc != null)
            {
                for (int i = 0; i < this.plist.Count; i++)
                {
                    if (bc == this.plist[i].bc)
                    {
                        flag = flag || this.plist[i].used;
                    }
                }
            }

            return flag;
        }
        public List<Used_drm> plist = new List<Used_drm>();

        /*
        public void TurnEnd()
        {
            for (int i = 0; i < this.StackInfo.Count; i++)
            {
                if (this.HistoryCheck(this.StackInfo[i].UseState))
                {
                    this.StackInfo[i].RemainTime--;
                }
                this.HistorySet(this.StackInfo[i].UseState);

                if (this.StackInfo[i].RemainTime == 0)
                {
                    this.StackInfo.RemoveAt(i);
                    //base.SelfDestroy();
                    i--;
                }

            }
            if (this.StackNum <= 0)
            {
                base.SelfDestroy();
            }
            this.BuffStatUpdate();

        }
        
        public override void TurnUpdate()
        {

        }
        */
    }

    public interface IP_B14Exist
    {
        // Token: 0x06000001 RID: 1
        bool B14Exist();
    }
    public class Used_drm
    {
        public BattleChar bc=new BattleChar();
        public bool used = false;
    }

    public class Bcl_drm_4_cridmg : Buff,IP_SkillUse_Target
    {
        /*
        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            if(SP.TargetChar.Contains(this.BChar))
            {
                this.SelfDestroy();
            }
        }
        */
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {

                this.SelfDestroy();
            
        }
        /*
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            Debug.Log("生成");
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Debug.Log("帧检测");
        }
        */
    }
    public class Bcl_drm_4_2 : Buff, IP_SpecialEnemyTargetSelect//mark
    {
        public bool SpecialEnemyTargetSelect(Skill skill, BattleEnemy Target)//修改点之一
        {
            bool flag1 = Target.BuffFind("B_SF_Dreamer_4", false) &&( Target.BuffReturn("B_SF_Dreamer_4", false).StackInfo.Find(a => a.UseState == this.BChar) != null || Bcl_drm_14.Checkb14());
            bool flag2 = Target.BuffFind("B_SF_Dreamer_p", false) && (Target.BuffReturn("B_SF_Dreamer_p", false).StackInfo.Find(a => a.UseState == this.BChar) != null || Bcl_drm_14.Checkb14());

            return flag1||flag2;// && (!Target.GetStat.Vanish)) ;
        }



        //public void ParticleOut_Before(Skill SkillD, List<BattleChar> Targets)//mark
        //{
        //Debug.Log("判断锁定");
        //if (Targets.Count<=1 && Targets[0].BuffFind("B_SF_Dreamer_4", false) && Targets[0].BuffReturn("B_SF_Dreamer_4", false).StackInfo.Find(a=>a.UseState == this.BChar)!=null)
        //{
        //Skill_Extended skill_Extended = new Skill_Extended();
        //skill_Extended.PlusSkillStat.HitMaximum = true;
        //skill_Extended.PlusSkillStat.Penetration = 100f;
        //SkillD.ExtendedAdd(skill_Extended);

        //Extended_drm_4 ex1 = Skill_Extended.DataToExtended("EX_SF_Dreamer_4") as Extended_drm_4;
        //SkillD.ExtendedAdd(ex1);
        //SkillD.ExtendedAdd(Skill_Extended.DataToExtendedC("Extended_drm_4"));

        //Debug.Log("有锁定");
        //}
        //else
        //{

        //Debug.Log("无锁定");
        //}
        //}


        //public void CriPerChange(Skill skill, BattleChar Target, ref float CriPer)//IP_CriPerChange
        //{
        //if (this.shouldCri && Target==this.criTar)
        //{
        //CriPer = 100f ;
        //}
        //this.shouldCri = false;
        //this.criTar = null;
        //}



        public override void TurnUpdate()//mark
        {
            bool keep = false;
            List<BattleChar> list = new List<BattleChar>();
            //list.AddRange(BattleSystem.instance.EnemyTeam.AliveChars);
            list.AddRange(BattleSystem.instance.EnemyTeam.AliveChars_Vanish);

            foreach (BattleChar enemytar in list)
            {
                keep = keep || (enemytar.BuffFind("B_SF_Dreamer_4", false)&&enemytar.BuffReturn("B_SF_Dreamer_4", false).StackInfo.Find(a => a.UseState == this.BChar) != null);
            }
            if (!keep)
            {
                base.SelfDestroy();
            }
        }


    }

    public class Extended_drm_4 : Sex_ImproveClone,IP_Draw, IP_DamageChange_sumoperation, IP_B4_Peneration,IP_SkillUse_Target//, IP_ChangeDamageState,IP_SkillUse_Target//, IP_CriPerChange//
    {
        //public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        public bool B4_Peneration(BattleChar Hit, SkillParticle SP)
        {
            //if(SP.SkillData.AllExtendeds.Contains(this)||this.MySkill==SP.SkillData)
            if (Standard_Tool.JudgeSkillSex(SP.SkillData,this))
            {
                return true;
            }
            return false;
        }
        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (SkillD == this.MySkill)
            {
                Cri = true;

                /*//直接修改g_get_stat，利用延迟期间的属性的版本。为了避免兼容性问题停用。
                if (Target.GetStat.DMGTaken < 0)
                {
                    Target.Info.G_get_stat.DMGTaken = 0;
                    //int frame = Time.frameCount;
                    //Traverse.Create(Target.Info).Field("getstat_BeforeFrameCount").SetValue(frame + 5);
                }
                */
            }
            //return Damage;

        }

        /*
        public void ChangeDamageState(SkillParticle SP, BattleChar Target, int DMG, bool Cri, ref bool ToHeal, ref bool ToPain)
        {
            if (this.mainbuff != null)
            {
                //Debug.Log("改变减伤");
                //int frame = Time.frameCount;
                //Traverse.Create(Target.Info).Field("getstat_BeforeFrameCount").SetValue(frame - 5);
                
                this.mainbuff.PlusStat.DMGTaken -= Math.Min(0,Target.GetStat.DMGTaken);
                Target.Info.G_get_stat.DMGTaken = Math.Max(0, Target.Info.G_get_stat.DMGTaken);
                Target.Info.ForceGetStat();
                Stat stat = Target.GetStat;
                
                //Traverse.Create(Target.Info).Field("getstat_BeforeFrameCount").SetValue(frame - 5);
                //Stat stat=Target.GetStat;
                
            }
        }
        */
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            //base.AttackEffectSingle(hit, SP, DMG, Heal);
            if(Standard_Tool.JudgeSkillSex(SP.SkillData,this) )
            {
                if (this.mainbuff != null)
                {
                    this.mainbuff.PlusStat.DMGTaken = 0;
                    //int frame = Time.frameCount;
                    //Traverse.Create(hit.Info).Field("getstat_BeforeFrameCount").SetValue(frame - 5);
                    hit.Info.ForceGetStat();
                    Stat stat = hit.GetStat;
                }

                /*
                int frame = Time.frameCount;
                Traverse.Create(hit.Info).Field("getstat_BeforeFrameCount").SetValue(frame - 5);
                */
            }


        }
        
        /*
        public void CriPerChange(Skill skill, BattleChar Target, ref float CriPer)
        {
            if(skill==this.MySkill)
            {
                CriPer = 100;
            }
        }
        */
        public IEnumerator Draw(Skill Drawskill, bool NotDraw)
        {
            this.SelfDestroy();
            yield break;
        }
        public Bcl_drm_4 mainbuff = new Bcl_drm_4(); 
    }








    public class Sex_drm_5 : Sex_ImproveClone//,IP_SkillEffectBuffAdd
    {
        public override void BattleStartDeck(List<Skill> Skills_Deck)
        {
            this.BChar.MyTeam.Draw(this.MySkill, null);
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            //UnityEngine.Debug.Log("测试点Sex_5_SkillUseSingle");
            base.SkillUseSingle(SkillD, Targets);
            List<GDEBuffData> list1 = this.MySkill.MySkill.Effect_Self.Buffs.FindAll(a => a.Key == "B_SF_Dreamer_0");
            foreach (Skill_Extended sex in this.MySkill.AllExtendeds)
            {
                foreach (BuffTag btg in sex.SelfBuff)
                {
                    if (btg.BuffData.Key == "B_SF_Dreamer_0")
                    {
                        list1.Add(btg.BuffData);
                    }
                }
            }

            if (this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == this.BChar  && a.CharinfoSkilldata != SkillD.CharinfoSkilldata) != null)
            {
                this.BChar.MyTeam.ForceDraw(this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == this.BChar && a.CharinfoSkilldata != SkillD.CharinfoSkilldata));
            }
            else if (this.BChar.MyTeam.Skills_UsedDeck.Find((Skill a) => a.Master == this.BChar && a.CharinfoSkilldata != SkillD.CharinfoSkilldata) != null)
            {
                this.BChar.MyTeam.ForceDraw(this.BChar.MyTeam.Skills_UsedDeck.FindLast((Skill a) => a.Master == this.BChar && a.CharinfoSkilldata != SkillD.CharinfoSkilldata));
            }
            else
            {
                this.BChar.MyTeam.Draw(1);
            }
        }

        /*
        public override bool Terms()
        {
            if (BattleSystem.instance != null)
            {
                if (this.BChar.Overload <= 0)
                {
                    return base.Terms();
                }
            }
            return false;
        }
        */

        /*
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Skill skill = this.MySkill.CloneSkill(true, null, null, false);
            //Skill skill = this.skill0;

            for (int i = 0; i < skill.AllExtendeds.Count; i++)
            {
                if (!skill.AllExtendeds[i].BattleExtended && !skill.AllExtendeds[i].isDataExtended)
                {
                    skill.AllExtendeds[i].SelfDestroy();
                }
            }

            Bcl_drm_5 buff=new Bcl_drm_5();
            if (!this.BChar.BuffFind("B_SF_Dreamer_5", false))
            {
                buff=this.BChar.BuffAdd("B_SF_Dreamer_5", this.BChar, false, 0, false, -1, false)as Bcl_drm_5;
            }
            else
            {
                buff = this.BChar.BuffReturn("B_SF_Dreamer_5", false) as Bcl_drm_5;
            }
            if (skill != null)
            {
                buff.Coskill(skill, 3);
            }
        }
        */
    }
    public class EX_drm_5 : Sex_ImproveClone
    {
        public override bool Terms()
        {
            
            if (BattleSystem.instance.TurnNum>=this.last+3)
            {
                this.APChange = 1 * Math.Max(0, this.last + 3 - BattleSystem.instance.TurnNum);
                this.MySkill.NotCount = true;
                //this.NotCount = true;
                return base.Terms();
            }
            else
            {
                this.MySkill.NotCount = false;
                //this.NotCount = false;
                this.APChange = 1 * Math.Max(0, this.last + 3 - BattleSystem.instance.TurnNum);
                return base.Terms();

            }

            //return false;
        }
        /*
        public override void Init()
        {
            base.Init();
            this.NotCount = true;
        }
        */
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            this.last=BattleSystem.instance.TurnNum;
        }
        public override void SkillUseHandBefore()
        {
            this.last = BattleSystem.instance.TurnNum;
            base.SkillUseHandBefore();
        }
        public int last = -99;
    }
    public class Bcl_drm_5 : Buff
    {
        /*
        public void Coskill(Skill skill,int cd)
        {
            this.skillList.Add(skill);
            this.cdList.Add(cd);
        }
        public override void TurnUpdate()
        {
            for (int i = 0; i < this.cdList.Count; i++)
            {
                this.cdList[i]--;
                if (this.cdList[i]== 0)
                {
                    this.BChar.MyTeam.Add(this.skillList[i], true);
                    this.cdList.RemoveAt(i);
                    this.skillList.RemoveAt(i);
                    i--;
                }
            }
            if(this.cdList.Count == 0)
            {
                base.SelfDestroy();
            }
        }
        private List<Skill> skillList=new List<Skill>();
        private List<int> cdList = new List<int>();
        */
    }



    public class Bcl_drm_6 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.DMGTaken = 10;
            this.PlusStat.hit = -30;
        }
    }






        public class Bcl_drm_7 : Buff, IP_Hit, IP_TurnEnd,IP_BuffAdd,IP_BuffUpdate//,IP_BuffAddAfter//IP_SkillUse_Target//协同射击敌人buff
    {
        // Token: 0x0600101D RID: 4125 RVA: 0x0008AC50 File Offset: 0x00088E50
        public override string DescExtended()
        {


            string allname = base.StackInfo[0].UseState.Info.Name;
            for (int i = 1; i < base.StackInfo.Count; i++)
            {
                allname = allname + "/" + base.StackInfo[i].UseState.Info.Name;
            }
            string alldam = ((int)(base.StackInfo[0].UseState.GetStat.atk * 0.6)).ToString();
            for (int i = 1; i < base.StackInfo.Count; i++)
            {
                alldam = alldam + "/" + ((int)(base.StackInfo[i].UseState.GetStat.atk * 0.6)).ToString();
            }


            return base.DescExtended().Replace("&a", alldam).Replace("&B7", allname);
        }
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)//mark
        {
            if (BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key && this.StackNum >= this.BuffData.MaxStack && this.StackInfo.Find(a => a.UseState == BuffUser) == null)
            {
                //this.BuffData.MaxStack++;

                //this.isb4 = true;
                this.StackInfo.Insert(0, this.StackInfo[0]);
            }
            else if (BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key && this.StackInfo.Find(a => a.UseState == BuffUser) != null)
            {

                int id = this.StackInfo.FindIndex(a => a.UseState == BuffUser);
                StackBuff st = this.StackInfo[id];
                this.StackInfo.RemoveAt(id);
                this.StackInfo.Insert(0, st);


                //foreach (StackBuff sta in this.StackInfo)
                //{
                //if (sta.UseState == BuffUser)
                //{
                //sta.RemainTime = Math.Max(sta.RemainTime,addedbuff.LifeTime);
                //}
                //}
            }
        }
        public void BuffUpdate(Buff MyBuff)
        {
            /*
            if (this.StackNum < this.BuffData.MaxStack)
            {
                this.BuffData.MaxStack = Math.Max(1, this.StackNum);
            }
            */
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
        //public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        //{
        //if (this.isb4)
        //{
        //this.StackInfo.Insert(0, this.oldb4);
        //this.BuffStatUpdate();
        //}
        //this.isb4 = false;
        //}
        //private StackBuff oldb4;
        //private bool isb4;


        public override void TurnUpdate()
        {

        }
        public void TurnEnd()
        {

            //foreach (StackBuff stackBuff in this.StackInfo)
            for (int i = 0; i < this.StackInfo.Count; i++)
            {
                this.StackInfo[i].RemainTime--;
                if (this.StackInfo[i].RemainTime == 0)
                {
                    this.StackInfo.RemoveAt(i);
                    //base.SelfDestroy();
                    i--;
                }

            }
            if(this.StackNum<=0)
            {
                base.SelfDestroy();
            }
            this.BuffData.MaxStack = Math.Max(1, this.StackNum);

        }

        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            foreach(StackBuff sta in this.StackInfo)
            {
                if (!sta.UseState.IsDead && SP.SkillData.Master != sta.UseState && SP.SkillData.IsDamage && BattleSystem.instance.EnemyList.Count != 0 && SP.ALLTARGET.Count <= 1 && !SP.SkillData.PlusHit)// && hit==this.BChar)
                {
                    //this.SkillTarget = this.BChar;
                    BattleSystem.DelayInput(this.Attack(sta.UseState,this.BChar));
                }
            }
            
            //if (SP.SkillData.Master != base.Usestate_L && SP.SkillData.IsDamage && BattleSystem.instance.EnemyList.Count != 0 && SP.ALLTARGET.Count == 1 && !SP.SkillData.PlusHit)// && hit==this.BChar)
            //{

                //this.SkillTarget = this.BChar;
                //BattleSystem.DelayInput(this.Attack());
            //}
        }
        public IEnumerator Attack(BattleChar user,BattleChar Target)
        {
            yield return new WaitForSecondsRealtime(0.2f);
            Skill skill = Skill.TempSkill("S_SF_Dreamer_7_1", user, user.MyTeam);//
            skill.PlusHit = true;
            skill.FreeUse = true;
            //bool flag1 = SkillTarget.BuffFind("B_SF_Dreamer_4", false);

            //if (SkillTarget.BuffFind("B_SF_Dreamer_4", false) && SkillTarget.BuffReturn("B_SF_Dreamer_4", false).Usestate_L == base.Usestate_L)//修复协同设计不受弱点锁定影响的问题
            //{
            //this.BChar.BuffAdd("B_SF_Dreamer_7_1", this.BChar, false, 0, false, -1, false);//检测点1

            //this.BChar.BuffAdd("B_SF_Dreamer_7_2", this.BChar, false, 0, false, -1, false);//检测点2

            //Skill_Extended skill_Extended = new Skill_Extended();
            //skill_Extended.PlusSkillStat.HitMaximum = true;
            //skill_Extended.PlusSkillStat.cri = 150f;
            //skill_Extended.PlusSkillStat.hit = 900f;
            //skill_Extended.PlusSkillStat.Penetration = 100f;
            //skill.ExtendedAdd(skill_Extended);
            //}


            user.ParticleOut(skill, Target);
            //BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, this.SkillTarget, false, false, true, null));

            yield break;
        }
        //private BattleChar SkillTarget;
    }



    public class Sex_drm_8 : Sex_ImproveClone
    {
        public override void SkillUseHand(BattleChar Target)
        {
            this.ovap2 = this.BChar.Overload;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {



            for (int i = 0; i < BattleSystem.instance.AllyTeam.Skills_UsedDeck.Count; i++)//将弃牌库中自己的技能洗入牌库
            {
                if (BattleSystem.instance.AllyTeam.Skills_UsedDeck[i].Master == SkillD.Master)
                {
                    int index = UnityEngine.Random.Range(0, BattleSystem.instance.AllyTeam.Skills_Deck.Count + 1);
                    BattleSystem.instance.AllyTeam.Skills_Deck.Insert(index, BattleSystem.instance.AllyTeam.Skills_UsedDeck[i]);
                    BattleSystem.instance.AllyTeam.Skills_UsedDeck.RemoveAt(i);
                    i--;
                }
            }

            this.SMaster = SkillD.Master;
            base.SkillUseSingle(SkillD, Targets);
            List<Skill> list = new List<Skill>();
            list.AddRange(this.BChar.MyTeam.Skills_Deck);
            for (int i = 0; i < list.Count; i++)//列出牌库中自己的所有技能
            {
                if (list[i].Master != SkillD.Master || list[i].MySkill.KeyID == "S_SF_Dreamer_8")
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
            if (list.Count != 0)//选牌
            {
                BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(this.Del), ScriptLocalization.System_SkillSelect.DrawSkill, false, true, true, false, true));
            }

            //if (this.ovap2 < this.BChar.Overload)//回费与保持过载暂时停用
            //{
            //this.BChar.Overload--;
            //}
            //BattleSystem.instance.AllyTeam.AP += this.BChar.Overload;
        }

        public void Del(SkillButton Mybutton)
        {
            Mybutton.Myskill.Master.MyTeam.Draw(Mybutton.Myskill, null);
            //BattleSystem.DelayInput(this.Delay());//继续抽下一张牌
        }

        // Token: 0x06000D28 RID: 3368 RVA: 0x00081827 File Offset: 0x0007FA27
        public IEnumerator Delay()
        {
            List<Skill> list = new List<Skill>();
            list.AddRange(this.BChar.MyTeam.Skills_Deck);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Master != SMaster || list[i].MySkill.KeyID == "S_SF_Dreamer_8")
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
            if (list.Count != 0)
            {
                BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(this.Del2), ScriptLocalization.System_SkillSelect.DrawSkill, false, true, true, false, true));//目前跳过了Del2，效果为抽二
            }

            yield return null;
            yield break;

        }

        public void Del2(SkillButton Mybutton)
        {
            Mybutton.Myskill.Master.MyTeam.Draw(Mybutton.Myskill, null);
            //BattleSystem.DelayInput(this.Delay2());//继续抽下一张牌
        }

        public IEnumerator Delay2()
        {
            List<Skill> list = new List<Skill>();
            list.AddRange(this.BChar.MyTeam.Skills_Deck);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Master != SMaster || list[i].MySkill.KeyID == "S_SF_Dreamer_8")
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
            if (list.Count != 0)
            {
                BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(this.Del3), ScriptLocalization.System_SkillSelect.DrawSkill, false, true, true, false, true));
            }

            yield return null;
            yield break;

        }

        // Token: 0x06000D29 RID: 3369 RVA: 0x00081836 File Offset: 0x0007FA36
        public void Del3(SkillButton Mybutton)
        {
            Mybutton.Myskill.Master.MyTeam.Draw(Mybutton.Myskill, null);
        }
        private BattleChar SMaster;
        private int ovap2;
    }



    public class Sex_drm_9 : Sex_ImproveClone, IP_SkillUsePatch_Before
    {
        public override void Init()
        {
            base.Init();
            this.CanUseStun = true;
        }
        public void SkillUsePatch_Before(BattleChar TargetChar, Skill TargetSkill, Skill SelectSkill)
        {
            if(Standard_Tool.JudgeSkillSex(SelectSkill,this))
            {
                /*
                for (int i = 0; i < SelectSkill.Master.Buffs.Count; i++)
                {
                    if (!SelectSkill.Master.Buffs[i].BuffData.Hide && !SelectSkill.Master.Buffs[i].BuffData.Cantdisable && (SelectSkill.Master.Buffs[i].BuffData.BuffTag.Key == GDEItemKeys.BuffTag_CrowdControl || SelectSkill.Master.Buffs[i].PlusStat.Stun))
                    {
                        SelectSkill.Master.Buffs[i].SelfDestroy(false);
                        this.hascc = true;
                    }
                }
                */


                Buff bcl = this.BChar.BuffAdd("B_SF_Dreamer_9", this.BChar);
                if(bcl!=null && bcl is Bcl_drm_9 bcl9)
                {
                    bcl9.mainskills.Add(SelectSkill);

                }



            }


        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);

            this.SMaster = SkillD.Master;
            
            bool hascc = false;
            
            for (int i = 0; i < SkillD.Master.Buffs.Count; i++)
            {
                if (!SkillD.Master.Buffs[i].BuffData.Hide && !SkillD.Master.Buffs[i].BuffData.Cantdisable && (SkillD.Master.Buffs[i].BuffData.BuffTag.Key == GDEItemKeys.BuffTag_CrowdControl || SkillD.Master.Buffs[i].PlusStat.Stun))
                {
                    SkillD.Master.Buffs[i].SelfDestroy(false);
                    hascc= true;
                }
            }
            
            
            for (int i = 0; i < BattleSystem.instance.AllyTeam.Skills_UsedDeck.Count; i++)//将弃牌库中自己的技能洗入牌库
            {
                if (BattleSystem.instance.AllyTeam.Skills_UsedDeck[i].Master == SkillD.Master)
                {
                    int index = UnityEngine.Random.Range(0, BattleSystem.instance.AllyTeam.Skills_Deck.Count + 1);
                    BattleSystem.instance.AllyTeam.Skills_Deck.Insert(index, BattleSystem.instance.AllyTeam.Skills_UsedDeck[i]);
                    BattleSystem.instance.AllyTeam.Skills_UsedDeck.RemoveAt(i);
                    i--;
                }
            }

            int num=(this.MySkill.BasicSkill?0:1)+(hascc?0:1);
            //if (!this.MySkill.BasicSkill && !hascc)
            if(num>=2)
            {
                //for (int i = 0; i < 2; i++)
                //{
                //BattleSystem.instance.AllyTeam.CharacterDraw(SkillD.Master, null);
                //}

                    BattleSystem.DelayInput(this.Delay());//抽2张牌
                

            }
            //else if (this.MySkill.BasicSkill && !hascc)
            else if(num>=1)
            {
                BattleSystem.DelayInput(this.Delay2());//抽一张牌
            }
            //this.BChar.Overload = 0;

            //SkillD.AllExtendeds[0].APChange = 1;
        }



        // Token: 0x06000D28 RID: 3368 RVA: 0x00081827 File Offset: 0x0007FA27
        public IEnumerator Delay()
        {
            List<Skill> list = new List<Skill>();
            list.AddRange(this.BChar.MyTeam.Skills_Deck);
            for (int i = 0; i < list.Count; i++)
            {
                //if (list[i].Master != this.SMaster || list[i].MySkill.KeyID == this.MySkill.MySkill.KeyID)
                if (list[i].Master != this.SMaster || list[i].CharinfoSkilldata == this.MySkill.CharinfoSkilldata)
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
            if (list.Count != 0)
            {
                BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(this.Del), ScriptLocalization.System_SkillSelect.DrawSkill, false, true, true, false, true));//目前跳过了Del2，效果为抽二
            }

            yield return null;
            yield break;

        }
        public void Del(SkillButton Mybutton)
        {
            Mybutton.Myskill.Master.MyTeam.Draw(Mybutton.Myskill, null);
            BattleSystem.DelayInput(this.Delay2());//继续抽下一张牌
        }
        public IEnumerator Delay2()
        {
            List<Skill> list = new List<Skill>();
            list.AddRange(this.BChar.MyTeam.Skills_Deck);
            for (int i = 0; i < list.Count; i++)
            {
                //if (list[i].Master != this.SMaster || list[i].MySkill.KeyID == this.MySkill.MySkill.KeyID)
                if (list[i].Master != this.SMaster || list[i].CharinfoSkilldata == this.MySkill.CharinfoSkilldata)
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
            if (list.Count != 0)
            {
                BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(this.Del2), ScriptLocalization.System_SkillSelect.DrawSkill, false, true, true, false, true));//目前跳过了Del2，效果为抽二
            }

            yield return null;
            yield break;

        }
        public void Del2(SkillButton Mybutton)
        {
            Mybutton.Myskill.Master.MyTeam.Draw(Mybutton.Myskill, null);
            //BattleSystem.DelayInput(this.Delay2());//继续抽下一张牌
        }

        //public bool hascc = false;
        private BattleChar SMaster;

    }

    public class Bcl_drm_9 : Buff//鹰眼庇护目标友军buff
    {

        public override void Init()
        {
            base.Init();
            this.PlusStat.Stun = true;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            for (int i = 0;i<this.mainskills.Count;i++)
            {

                if (BattleSystem.instance.CastSkills.Find(a => a.skill == this.mainskills[i]) == null && BattleSystem.instance.SaveSkill.Find(a => a.skill == this.mainskills[i]) == null)
                {
                    this.mainskills.RemoveAt(i);
                    i--;
                }
            }
            if(this.mainskills.Count < 1)
            {
                this.SelfDestroy();
            }
        }
        public List<Skill> mainskills= new List<Skill>();   
    }

    //public class Sex_drm_10 : Sex_ImproveClone
    //{


    //public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
    //{
    //foreach (BattleAlly ally in this.BChar.BattleInfo.AllyList)
    //{
    //if (ally != Targets[0] && ally.BuffFind("B_SF_Dreamer_10", false))
    //{
    //ally.BuffReturn("B_SF_Dreamer_10", false).SelfDestroy(false);
    //}
    //}
    //}
    //}


    //public class Bcl_drm_10 : Buff//鹰眼庇护目标友军buff
    //{
    // Token: 0x0600101D RID: 4125 RVA: 0x0008AC50 File Offset: 0x00088E50
    //public override string DescExtended()
    //{
    //return base.DescExtended().Replace("&b", ((int)((float)this.BChar.GetStat.atk * 0.9f)).ToString());
    //}

    //}

    public class Sex_drm_10 : Sex_ImproveClone
    {
        public override string DescExtended(string desc)
        {

            //return base.DescExtended(desc).Replace("&a", "<color=#32CD32>最高</color>").Replace("&c", ((int)(this.MySkill.TargetDamage * this.plusp)).ToString()).Replace("&d", ((int)(100 * this.plusp)).ToString());
            return base.DescExtended(desc).Replace("&a", ((int)(this.BChar.GetStat.atk * 0.6f)).ToString());

        }
        public override void Init()
        {
            //UnityEngine.Debug.Log("测试点Sex_10_Init");

            base.Init();
            //this.SkillBasePlusPreview.Target_BaseDMG =(int) (this.BChar.GetStat.atk * 0.6f);
            this.PlusSkillPerStat.Damage = 70;
            this.IsDamage = true;

            if(this.TargetBuff.Find(a => a.BuffData.Key == "B_SF_Dreamer_10")==null || this.TargetBuff.Find(a => a.BuffData.Key == "B_SF_Dreamer_7") == null)
            {
                this.TargetBuff.RemoveAll(a => a.BuffData.Key == "B_SF_Dreamer_10" || a.BuffData.Key == "B_SF_Dreamer_7");

                GDEBuffData gde1 = new GDEBuffData("B_SF_Dreamer_7");
                BuffTag btg1 = new BuffTag();
                btg1.User = this.BChar;
                btg1.BuffData = gde1;
                GDEBuffData gde2 = new GDEBuffData("B_SF_Dreamer_10");
                BuffTag btg2 = new BuffTag();
                btg2.User = this.BChar;
                btg2.BuffData = gde2;
                this.TargetBuff.Insert(0, btg1);
                this.TargetBuff.Insert(0, btg2);
            }



        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            //UnityEngine.Debug.Log("测试点Sex_10_SkillUseSingle");
            if (!Targets[0].Info.Ally)
            {
                //this.IsDamage = false;
                /*
                if (SkillD.MySkill.Effect_Target.Buffs.FindIndex(a => a.Key == "B_SF_Dreamer_10") != -1)
                {
                    int ind = SkillD.MySkill.Effect_Target.Buffs.FindIndex(a => a.Key == "B_SF_Dreamer_10");//取消技能自带的buff
                    SkillD.MySkill.Effect_Target.Buffs.RemoveAt(ind);

                }
                */
                this.TargetBuff.RemoveAll(a => a.BuffData.Key == "B_SF_Dreamer_10" );

                this.IsDamage = true;
                this.PlusSkillPerStat.Damage = 70;

                this.MySkill.UseOtherParticle_Path = "Particle/SilverStein/SilverStein_new_1";
                //Skill skill = Skill.TempSkill("S_SF_Dreamer_7", this.BChar, this.BChar.MyTeam);
                //this.BChar.ParticleOut(skill, Targets);

                //GDEBuffData toally = new GDEBuffData("B_SF_Dreamer_10");
                //SkillD.MySkill.Effect_Target.Buffs.Add(toally);
                //base.SkillUseSingleAfter(SkillD, Targets);
            }
            else
            {
                this.TargetBuff.RemoveAll(a => a.BuffData.Key == "B_SF_Dreamer_7");

                this.IsDamage = false;
                this.PlusSkillPerStat.Damage = 0;
                //this.PlusSkillPerStat.Damage = -99999;

                /*
                if (SkillD.MySkill.Effect_Target.Buffs.FindIndex(a => a.Key == "B_SF_Dreamer_7") != -1)
                {
                    int ind = SkillD.MySkill.Effect_Target.Buffs.FindIndex(a => a.Key == "B_SF_Dreamer_7");//取消技能自带的buff
                    SkillD.MySkill.Effect_Target.Buffs.RemoveAt(ind);

                }
                */
                
            }
            base.SkillUseSingle(SkillD, Targets);
        }


    }

    public class Bcl_drm_10 : Buff, IP_TargetedAlly,IP_BuffAdd,IP_BuffUpdate//,IP_BuffAddAfter
    {
        public override string DescExtended()
        {


            string allname = base.StackInfo[0].UseState.Info.Name;
            for (int i = 1; i < base.StackInfo.Count; i++)
            {
                allname = allname + "/" + base.StackInfo[i].UseState.Info.Name;
            }
            string alldam = ((int)(base.StackInfo[0].UseState.GetStat.atk * 0.9)).ToString();
            for (int i = 1; i < base.StackInfo.Count; i++)
            {
                alldam = alldam + "/" + ((int)(base.StackInfo[i].UseState.GetStat.atk * 0.9)).ToString();
            }


            return base.DescExtended().Replace("&b", alldam).Replace("&B10", allname);
        }
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)//mark
        {
            if (BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key && this.StackNum >= this.BuffData.MaxStack && this.StackInfo.Find(a => a.UseState == BuffUser) == null)
            {
                //this.BuffData.MaxStack++;

                //this.isb10 = true;
                this.StackInfo.Insert(0, this.StackInfo[0]);
            }
            else if(BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key  && this.StackInfo.Find(a => a.UseState == BuffUser) != null)
            {
                int id = this.StackInfo.FindIndex(a => a.UseState == BuffUser);
                StackBuff st = this.StackInfo[id];
                this.StackInfo.RemoveAt(id);
                this.StackInfo.Insert(0, st);
                //foreach(StackBuff sta in this.StackInfo)
                //{
                //if(sta.UseState== BuffUser)
                //{
                //sta.RemainTime = Math.Max(sta.RemainTime, addedbuff.LifeTime);
                //}
                //}
            }
        }
        public void BuffUpdate(Buff MyBuff)
        {
            /*
            if (this.StackNum < this.BuffData.MaxStack)
            {
                this.BuffData.MaxStack = Math.Max(1, this.StackNum);
            }
            */
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
        //ublic void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        //{
        //if (this.isb10)
        //{
        //this.StackInfo.Insert(0, this.oldb10);
        //this.BuffStatUpdate();
        //}
        //this.isb10 = false;
        //}
        //private StackBuff oldb10;
        //private bool isb10;




        public IEnumerator Targeted(BattleChar Attacker, List<BattleChar> SaveTargets, Skill skill)
        {

            bool flag0 = false;//判断自身是否被攻击的flag
            for (int i = 0; i < SaveTargets.Count; i++)
            {
                if (SaveTargets[i] == this.BChar)
                {
                    flag0 = true;
                }
            }
            foreach(StackBuff sta in this.StackInfo)
            {
                /*
                bool flag1 = false;//判断是否有友军已经触发同一释放者的庇护效果
                foreach (BattleChar battleChar in this.BChar.MyTeam.AliveChars)
                {
                    if (battleChar.BuffFind("B_SF_Dreamer_10_1", false) && battleChar.BuffReturn("B_SF_Dreamer_10_1", false).StackInfo.Find(a=>a.UseState == sta.UseState)!=null)//判断是否有友军已经触发同一释放者的庇护效果
                    {
                        flag1 = true;
                    }
                }


                if (flag0&&!flag1 && !sta.UseState.IsDead)//(!this.BChar.BuffFind("B_SF_Dreamer_10_1", false) && !this.BChar.BuffFind("B_SF_Dreamer_10_2", false) )
                {

                    for (int i = 0; i < SaveTargets.Count; i++)
                    {
                        SaveTargets[i].BuffAdd("B_SF_Dreamer_10_1", sta.UseState, false, 0, false, -1, false);
                    }
                    //Skill_Extended skill_Extended = new Skill_Extended();//test
                    //skill_Extended. = "EX_SF_Dreamer_10_1";
                    //skill_Extended.PlusSkillPerStat.Damage = -(int)(skill.MySkill.Effect_Target.DMG_Per * 0.9);//test
                    //skill_Extended.PlusSkillStat.Weak = true;//test

                    if (Attacker.BuffFind("B_SF_Dreamer_4", false) && Attacker.BuffReturn("B_SF_Dreamer_4", false).StackInfo.Find(a => a.UseState == sta.UseState) != null)
                    {
                        //skill_Extended.PlusSkillStat.hit = -500;//test
                        for (int i = 0; i < SaveTargets.Count; i++)//test not
                        {
                            SaveTargets[i].BuffAdd("B_SF_Dreamer_10_2", sta.UseState, false, 0, false, -1, false);//test not
                        }
                    }
                    //skill.ExtendedAdd(skill_Extended);//test

                    BattleSystem.DelayInput(this.AntiAttack(sta, Attacker));

                    //this.BChar.BuffAdd("B_SF_Dreamer_10_0", sta.UseState, false, 0, false, -1, false);
                }
                */
                if(flag0)
                {
                    bool already_de = false;
                    foreach(Skill_Extended sex in skill.AllExtendeds)
                    {
                        if(sex is EX_drm_10_1 && (sex as EX_drm_10_1).user==sta.UseState)
                        {
                            already_de = true;
                            break;
                        }
                    }

                    if(!already_de)
                    {
                        EX_drm_10_1 ex10=new EX_drm_10_1();
                        ex10.user=sta.UseState;
                        ex10.plus = Attacker.BuffFind("B_SF_Dreamer_4", false) && Attacker.BuffReturn("B_SF_Dreamer_4", false).StackInfo.Find(a => a.UseState == sta.UseState) != null;
                        skill.ExtendedAdd(ex10);
                        //BattleSystem.DelayInputAfter(this.WeakEnd(skill,sta.UseState));

                        BattleSystem.DelayInput(this.AntiAttack(sta, Attacker));
                    }
                }

            }
            //List<BattleChar>.Enumerator enumerator = default(List<BattleChar>.Enumerator);
            yield break;
        }
        
        public IEnumerator WeakEnd(Skill skill, BattleChar bc)
        {
            foreach (Skill_Extended sex in skill.AllExtendeds)
            {
                if (sex is EX_drm_10_1 && (sex as EX_drm_10_1).user == bc)
                {
                    sex.SelfDestroy();

                }
            }
            yield return null;
            yield break;
        }
        public IEnumerator AntiAttack(StackBuff sta, BattleChar Attacker)
        {
            Attacker.BuffAdd("B_SF_Dreamer_10_3", sta.UseState, false, 0, false, -1, false);//test not
            yield return new WaitForSeconds(0.1f);
            Skill skill2 = Skill.TempSkill("S_SF_Dreamer_10_1",sta.UseState, sta.UseState.MyTeam);
            skill2.PlusHit = true;
            skill2.FreeUse = true;


            sta.UseState.ParticleOut(skill2, Attacker);

            //BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill2, Attacker, false, false, true, null));打出技能版本
            yield return new WaitForSeconds(0.2f);
            yield break;
        }
        public bool bcl10f1 = true;
    }
    public class EX_drm_10_1 : Sex_ImproveClone,IP_DodgeCheck_After2
    {
        public override void Init()
        {
            this.PlusSkillStat.Weak = true;
            if (this.plus)
            {
                this.SkillBasePlus.Target_BaseDMG = -(int)Math.Ceiling(Misc.PerToNum((float)this.MySkill.TargetDamage, 25f));

                //this.PlusSkillStat.hit = -999;
            }
            else
            {
                //this.PlusSkillPerStat.Damage = -(int)(this.MySkill.MySkill.Effect_Target.DMG_Per * 0.25);
                this.SkillBasePlus.Target_BaseDMG = -(int)Math.Ceiling(Misc.PerToNum((float)this.MySkill.TargetDamage, 25f));
            }
        }
        public void DodgeCheck_After2(BattleChar hit, SkillParticle SP, ref bool dod)
        {
            if(Standard_Tool.JudgeSkillSex(SP.SkillData,this))
            {
                if (this.plus)
                {
                    dod = true;
                }
            }


        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);

            BattleSystem.DelayInputAfter(this.WeakEnd());
        }
        public IEnumerator WeakEnd()
        {
            this.SelfDestroy();
            yield return null;
            yield break;
        }
        public bool plus = false;
        public BattleChar user=new BattleChar();

    }

    public class Bcl_drm_10_0 : Buff, IP_Hit, IP_Dodge//, IP_Kill//代表已经触发一次庇护，暂未使用
    {
        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            base.SelfDestroy();
        }
        public void Dodge(BattleChar Char, SkillParticle SP)
        {
            base.SelfDestroy();
        }
        //public void KillEffect(SkillParticle SP)
        //{
            //Debug.Log("Kill触发");
            //base.SelfStackDestroy();
        //}
        public override void Init()
        {
            base.Init();
            //this.isStackDestroy = true;
        }
    }
    public class Bcl_drm_10_1 : Buff, IP_Hit, IP_Dodge
    {
        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            base.SelfDestroy();
        }


        public void Dodge(BattleChar Char, SkillParticle SP)
        {
            base.SelfDestroy();
        }

        // Token: 0x06000AEC RID: 2796 RVA: 0x0007AD58 File Offset: 0x00078F58
        public override void Init()
        {
            base.Init();
            //this.PlusStat.def = 30;
            this.PlusStat.DMGTaken = (-25f)* base.StackNum;
            this.PlusStat.Strength = true;
            //this.isStackDestroy = true;
        }
    }
    public class Bcl_drm_10_2 : Buff, IP_Hit, IP_Dodge
    {
        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            base.SelfDestroy();
        }

        public void Dodge(BattleChar Char, SkillParticle SP)
        {
            base.SelfDestroy();
        }

        // Token: 0x06000AEC RID: 2796 RVA: 0x0007AD58 File Offset: 0x00078F58
        public override void Init()
        {
            base.Init();
            //this.PlusStat.dod = 150;
            this.PlusStat.PerfectDodge = true;

            //this.PlusStat.def = 30;
            //this.PlusStat.PerfectShield = true;
            //this.PlusStat.Strength = true;
            //this.isStackDestroy = true;
        }
    }
    public class Bcl_drm_10_3 : Buff, IP_Dead//敌人死亡时解除其他相关buff
    {
        public void Dead()
        {
            int numal = BattleSystem.instance.AllyList.Count;
            for (int i = 0; i < numal; i++)
            {
                if (BattleSystem.instance.AllyList[i].BuffFind("B_SF_Dreamer_10_0", false))
                {
                    BattleSystem.instance.AllyList[i].BuffReturn("B_SF_Dreamer_10_0", false).SelfDestroy();
                }
                if (BattleSystem.instance.AllyList[i].BuffFind("B_SF_Dreamer_10_1", false))
                {
                    BattleSystem.instance.AllyList[i].BuffReturn("B_SF_Dreamer_10_1", false).SelfDestroy();
                }
                if (BattleSystem.instance.AllyList[i].BuffFind("B_SF_Dreamer_10_2", false))
                {
                    BattleSystem.instance.AllyList[i].BuffReturn("B_SF_Dreamer_10_2", false).SelfDestroy();
                }
            }
        }
    }



    public class Sex_drm_11 : Sex_ImproveClone
    {
        public override void Init()
        {
            base.Init();
            this.CanUseStun = true;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            for (int i = 0; i < SkillD.Master.Buffs.Count; i++)
            {
                if(!Targets[0].Buffs[i].BuffData.Hide)
                {
                    if (Targets[0].Buffs[i].BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT || Targets[0].Buffs[i].BuffData.BuffTag.Key == GDEItemKeys.BuffTag_Debuff || Targets[0].Buffs[i].BuffData.BuffTag.Key == GDEItemKeys.BuffTag_CrowdControl)
                    {
                        Targets[0].Buffs[i].SelfDestroy(false);
                    }
                }
            }

            if (Targets[0].HP < Targets[0].GetStat.maxhp)
            {
                //int num = Targets[0].GetStat.maxhp - Targets[0].HP;
                //Targets[0].Heal(this.BChar, (float)num, false, true, null);
                Targets[0].Recovery= Targets[0].GetStat.maxhp;
                Targets[0].HP = Targets[0].GetStat.maxhp;
                Targets[0].Recovery = Targets[0].GetStat.maxhp;
            }

            Skill skill = Skill.TempSkill("S_SF_Dreamer_8", this.BChar, this.BChar.MyTeam);
            skill.APChange = -99;
            skill.Disposable = true;
            skill.NotCount = true;

            this.BChar.MyTeam.Add(skill, true);
            BattleSystem.DelayInput(this.Delay());

            this.BChar.Overload = 0;
        }

        // Token: 0x06000D28 RID: 3368 RVA: 0x00081827 File Offset: 0x0007FA27
        public IEnumerator Delay()
        {
            Skill skill2 = Skill.TempSkill("S_SF_Dreamer_8", this.BChar, this.BChar.MyTeam);
            skill2.APChange = -99;
            skill2.Disposable = true;
            skill2.NotCount = true;

            this.BChar.MyTeam.Add(skill2, true);
            yield break;
        }

    }

    public class Bcl_drm_11 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusPerStat.Damage = -30;
            this.PlusStat.hit = -30;
            this.PlusStat.def = -30;
            this.PlusStat.dod= -30;
            //this.PlusStat.crihit = 50;
        }
    }

    public class Sex_drm_12_LucyDraw : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            
            BattleChar BC = BattleSystem.instance.AllyList.Find((BattleAlly a) => a.Info.KeyData == "SF_Dreamer");
            for (int i = 0; i < 2; i++)
            {
                BattleSystem.DelayInputAfter(this.Draw());
            }
        }
        public IEnumerator Draw()
        {
            BattleChar BC = BattleSystem.instance.AllyList.Find((BattleAlly a) => a.Info.KeyData == "SF_Dreamer");
            Skill skill2 = BattleSystem.instance.AllyTeam.Skills_Deck.Find((Skill skill) => skill.Master == BC);
            if (skill2 == null)
            {
                //BattleSystem.instance.AllyTeam.Draw();
                BattleSystem.instance.AllyTeam.Draw(1, new BattleTeam.DrawInput(this.Drawinput));
            }
            else
            {
                skill2.ExtendedAdd(new Skill_Extended
                {
                    APChange = -1
                });
                yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyTeam._ForceDraw(skill2, null));
            }
            yield return null;
            yield break;
        }
        public void Drawinput(Skill skill)
        {
            skill.ExtendedAdd(new Skill_Extended
            {
                APChange = -1
            });
        }
    }

    public class Sex_drm_13 : Sex_ImproveClone,IP_ParticleOut_After,IP_AntiVanish, IP_SkillUse_Target
    {
        public bool AntiVanish(BattleChar Target, Skill skill)
        {
            if (skill == this.MySkill)
            {
                return true;
            }
            return false;
        }
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(this.BChar.GetStat.atk*0.35)).ToString());


        }
        public override bool TargetSelectExcept(BattleChar ExceptTarget)
        {
            bool alltaunt = true;
            bool taunt = false;
            if(ExceptTarget is BattleEnemy)
            {
                foreach (BattleChar cha in ExceptTarget.MyTeam.AliveChars_Vanish)
                {
                    alltaunt = (cha as BattleEnemy).istaunt && alltaunt;
                }
                taunt = (ExceptTarget as BattleEnemy).istaunt;
            }

            return !alltaunt && taunt;
        }
        
        public override void Special_PointerEnter(BattleChar Char)
        {
            base.Special_PointerEnter(Char);
            //this.SkillBasePlusPreview.Target_BaseDMG = Math.Min((int)(0.1 *Char.GetStat.maxhp), (int)(2 * this.MySkill.TargetDamageOriginal));
            if (Char is BattleEnemy)
            {
                this.NotCount = !(Char as BattleEnemy).istaunt;
            }

        }
        public override void Special_PointerExit()
        {
            base.Special_PointerExit();
            this.NotCount = false;
        }
        public override void FixedUpdate()
        {
            frame++;
            if(frame<20)
            {
                return;
            }
            if(BattleSystem.instance.TargetSelecting && BattleSystem.instance.SelectedSkill==this.MySkill)
            {

            }
            else
            {
                bool alltaunt = true;


                    foreach (BattleChar cha in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
                    {
                        alltaunt = (cha as BattleEnemy).istaunt && alltaunt;
                    }


                this.NotCount = !alltaunt;
            }
        }
        private int frame = 0;
        //public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        //{



        //if (Targets[0] is BattleEnemy)
        //{
        //if (!(Targets[0] as BattleEnemy).istaunt)
        //{
        //if ( Targets[0].HP >= Targets[0].GetStat.maxhp)
        //{
        //Targets[0].BuffAdd("B_SF_Dreamer_4", this.BChar, false, 0, false, -1, false);
        //}
        //else
        //{
        //Skill_Extended skill_Extended = new Skill_Extended();

        //skill_Extended.PlusSkillPerStat.Damage = (int)(SkillD.MySkill.Effect_Target.DMG_Per * 0.5);
        //SkillD.ExtendedAdd(skill_Extended);
        //}

        //Debug.Log("惊魂毒刺增伤");
        //}
        //}

        //}
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (Standard_Tool.JudgeSkillSex(SP.SkillData, this) && Cri)
            {

                hit.BuffAdd("B_SF_Dreamer_13", this.BChar, false, 0, false, -1, false);
                /*
                if (this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == this.BChar && a.IsDamage && a.CharinfoSkilldata != SP.SkillData.CharinfoSkilldata) != null)
                {
                    this.BChar.MyTeam.ForceDraw(this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == this.BChar && a.IsDamage && a.CharinfoSkilldata != SP.SkillData.CharinfoSkilldata),new BattleTeam.DrawInput(this.Drawinput));
                }
                else if (this.BChar.MyTeam.Skills_UsedDeck.Find((Skill a) => a.Master == this.BChar && a.IsDamage && a.CharinfoSkilldata != SP.SkillData.CharinfoSkilldata) != null)
                {
                    this.BChar.MyTeam.ForceDraw(this.BChar.MyTeam.Skills_UsedDeck.FindLast((Skill a) => a.Master == this.BChar && a.IsDamage && a.CharinfoSkilldata != SP.SkillData.CharinfoSkilldata), new BattleTeam.DrawInput(this.Drawinput));
                }
                else if (this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == this.BChar  && a.CharinfoSkilldata != SP.SkillData.CharinfoSkilldata) != null)
                {
                    this.BChar.MyTeam.ForceDraw(this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == this.BChar && a.CharinfoSkilldata != SP.SkillData.CharinfoSkilldata), new BattleTeam.DrawInput(this.Drawinput));
                }
                else if (this.BChar.MyTeam.Skills_UsedDeck.Find((Skill a) => a.Master == this.BChar  && a.CharinfoSkilldata != SP.SkillData.CharinfoSkilldata) != null)
                {
                    this.BChar.MyTeam.ForceDraw(this.BChar.MyTeam.Skills_UsedDeck.FindLast((Skill a) => a.Master == this.BChar && a.CharinfoSkilldata != SP.SkillData.CharinfoSkilldata), new BattleTeam.DrawInput(this.Drawinput));
                }
                else
                {
                    this.BChar.MyTeam.Draw(1, new BattleTeam.DrawInput(this.Drawinput));
                }
                */
            }
        }
        /*
        public void Drawinput(Skill skill)
        {
            skill.ExtendedAdd(new Skill_Extended
            {
                APChange = -1
            });
        }
        */
        public IEnumerator ParticleOut_After(Skill SkillD, List<BattleChar> Targets)//mark
        {
            
           if (SkillD==this.MySkill)
            //if (Targets.Count == 1 && SkillD.IsDamage && !SkillD.PlusHit && Targets[0].Buffs.Find(a => a.BuffData.Key == "B_SF_Dreamer_4" && a.Usestate_L == this.BChar) == null) 
            {
                foreach(BattleChar target in Targets)
                {

                    if (target is BattleEnemy)
                    {
                        if (!(target as BattleEnemy).istaunt)
                        {
                            target.BuffAdd("B_SF_Dreamer_4", this.BChar, false, 0, false, -1, false);
                        }
                    }
                }

            }
            yield break;
        }
    }
    public class Bcl_drm_13 : Buff
    {

    }

    public class Bcl_drm_14 : Buff, IP_ParticleOutEnd_After,IP_B14Exist,IP_AntiVanish//, IP_BuffUpdate
    {
        //public IEnumerator ParticleOut_After(Skill SkillD, List<BattleChar> Targets)//mark
        //{
        //bool flag = this.BChar.Info.KeyData != "SF_Dreamer" || this.BChar.Info.Passive==null;
        //bool flag = !(this.BChar.Info.Passive is P_Dreamer);
        //if (flag && Targets.Count == 1 && SkillD.IsDamage && !SkillD.PlusHit && (!Targets[0].BuffFind("B_SF_Dreamer_4", false) || Targets[0].BuffReturn("B_SF_Dreamer_4", false).StackInfo.Find(a => a.UseState == this.BChar) == null))
        //{
        //Targets[0].BuffAdd("B_SF_Dreamer_p", this.BChar, false, 0, false, -1, false);
        //}
        //yield break;
        //}

        //public void BuffUpdate(Buff MyBuff)
        
        public bool B14Exist()
        {
            return true;
        }
        public bool AntiVanish(BattleChar Target, Skill skill)
        {
            if (Target.BuffFind("B_SF_Dreamer_4",false )&& Target.BuffReturn("B_SF_Dreamer_4", false).StackInfo.Find(a=>a.UseState.Info.Ally==this.BChar.Info.Ally)!=null)
            {
                return true;
            }
            if (Target.BuffFind("B_SF_Dreamer_p", false) && Target.BuffReturn("B_SF_Dreamer_p", false).StackInfo.Find(a => a.UseState.Info.Ally == this.BChar.Info.Ally) != null)
            {
                return true;
            }
            return false;
        }

        public IEnumerator ParticleOutEnd_After(BattleChar BC, Skill skill, List<BattleChar> Target)
        {
            /*
            bool flag = !(BC.Info.Passive is P_Dreamer);
            if (flag && BC.Info.Ally && Target.Count == 1 && !Target[0].Info.Ally && skill.IsDamage && !skill.PlusHit && (!Target[0].BuffFind("B_SF_Dreamer_4", false) || Target[0].BuffReturn("B_SF_Dreamer_4", false).StackInfo.Find(a => a.UseState == this.BChar) == null))
            {
            Target[0].BuffAdd("B_SF_Dreamer_p", this.BChar, false, 0, false, -1, false);
            }
            */
            
            bool flag = !(BC.Info.Passive is P_Dreamer);

            bool isforall = skill.TargetTypeKey == "all_enemy" || skill.TargetTypeKey == "all_ally" || skill.TargetTypeKey == "all" || skill.TargetTypeKey == "all_allyorenemy" || skill.TargetTypeKey == "all_other";

            if (flag && !isforall && BC==this.BChar && Target.Count == 1 && !Target[0].Info.Ally && skill.IsDamage && !skill.PlusHit && (!Target[0].BuffFind("B_SF_Dreamer_4", false) || Target[0].BuffReturn("B_SF_Dreamer_4", false).StackInfo.Find(a => a.UseState == this.BChar) == null))
            //if (flag && BC==this.BChar && Target.Count == 1 && !Target[0].Info.Ally && skill.IsDamage && !skill.PlusHit && !(Bcl_drm_14.CheckCanActiveB4(Target[0], this.BChar)))
            
            {
                Target[0].BuffAdd("B_SF_Dreamer_p", this.BChar, false, 0, false, -1, false);
            }
            
            yield break;
        }

        public static bool CheckCanActiveB4(BattleChar target,BattleChar user)
        {
            bool flag1 = target.BuffFind("B_SF_Dreamer_4", false) && target.BuffReturn("B_SF_Dreamer_4", false).StackInfo.Find(a => a.UseState == user) != null;
            bool flag2 = target.BuffFind("B_SF_Dreamer_4", false) && Bcl_drm_14.Checkb14();
            return flag1 || flag2;
        }

        public static bool Checkb14()
        {
            bool allclear = false;
            foreach (IP_B14Exist ip_patch in BattleSystem.instance.IReturn<IP_B14Exist>())
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    allclear = allclear || ip_patch.B14Exist();
                }
            }
            return allclear;
        }
        /*
        public override void FixedUpdate()//修改点之一，需要删去
        {
            
            foreach(BattleChar emy in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {

                if (emy.BuffFind("B_SF_Dreamer_4",false) && emy.BuffReturn("B_SF_Dreamer_4", false).StackNum< this.BChar.MyTeam.AliveChars_Vanish.Count)
                {
                    List<BattleChar> aly = new List<BattleChar>();
                    aly.AddRange(this.BChar.MyTeam.AliveChars_Vanish);

                    Buff buff4 = emy.BuffReturn("B_SF_Dreamer_4", false);
                    for (int i = 0; i < buff4.StackInfo.Count; i++) 
                    {
                        int ind = aly.FindIndex(a => a == buff4.StackInfo[i].UseState);
                        if (ind!=-1)
                        {
                            aly.RemoveAt(ind);
                            i--;
                        }
                            
                    }
                    foreach(BattleChar bc in aly)
                    {
                        emy.BuffAdd("B_SF_Dreamer_4", bc, false, 0, false, -1, true);
                    }
                }
            }

            foreach (BattleChar emy in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {

                if (emy.BuffFind("B_SF_Dreamer_p", false) && emy.BuffReturn("B_SF_Dreamer_p", false).StackNum < this.BChar.MyTeam.AliveChars_Vanish.Count)
                {
                    List<BattleChar> aly = new List<BattleChar>();
                    aly.AddRange(this.BChar.MyTeam.AliveChars_Vanish);

                    Buff buff4 = emy.BuffReturn("B_SF_Dreamer_p", false);
                    for (int i = 0; i < buff4.StackInfo.Count; i++)
                    {
                        int ind = aly.FindIndex(a => a == buff4.StackInfo[i].UseState);
                        if (ind != -1)
                        {
                            aly.RemoveAt(ind);
                            i--;
                        }

                    }
                    foreach (BattleChar bc in aly)
                    {
                        emy.BuffAdd("B_SF_Dreamer_p", bc, false, 0, false, -1, true);
                    }
                }
            }
        }
        */
    }
    public class Sex_drm_15 : Sex_ImproveClone, IP_SkillUse_Target
    {
        /*
        public override void Special_PointerEnter(BattleChar Char)
        {

                List<Buff> list = new List<Buff>();
                foreach (Buff buff in Char.Buffs)
                {
                    if (buff.BuffData.Debuff && !buff.BuffData.Hide)
                    {
                        list.Add(buff);
                    }
                }
                if (list.Count > 0)
                {
                    this.SkillBasePlusPreview.Target_BaseDMG = (int)(0.3 * this.MySkill.TargetDamage);

                }
                else
                {
                    this.SkillBasePlusPreview.Target_BaseDMG = 0;

                }
            
        }
        
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            List<Buff> list = new List<Buff>();
            foreach (Buff buff in Targets[0].Buffs)
            {
                if (buff.BuffData.Debuff && !buff.BuffData.Hide)
                {
                    list.Add(buff);
                }
            }
            if(list.Count>0)
            {
                Skill_Extended skill_Extended = new Skill_Extended();

                //skill_Extended.PlusSkillPerStat.Damage = (int)(SkillD.MySkill.Effect_Target.DMG_Per * 0.3);
                skill_Extended.SkillBasePlus.Target_BaseDMG = (int)(0.3 * this.MySkill.TargetDamage);
                SkillD.ExtendedAdd(skill_Extended);
            }
        }
        */
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (Standard_Tool.JudgeSkillSex(SP.SkillData, this) && Cri)
            {


                if (this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == this.BChar && a.IsDamage && a.CharinfoSkilldata != SP.SkillData.CharinfoSkilldata) != null)
                {
                    this.BChar.MyTeam.ForceDraw(this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == this.BChar && a.IsDamage && a.CharinfoSkilldata != SP.SkillData.CharinfoSkilldata),new BattleTeam.DrawInput(this.Drawinput));
                }
                else if (this.BChar.MyTeam.Skills_UsedDeck.Find((Skill a) => a.Master == this.BChar && a.IsDamage && a.CharinfoSkilldata != SP.SkillData.CharinfoSkilldata) != null)
                {
                    this.BChar.MyTeam.ForceDraw(this.BChar.MyTeam.Skills_UsedDeck.FindLast((Skill a) => a.Master == this.BChar && a.IsDamage && a.CharinfoSkilldata != SP.SkillData.CharinfoSkilldata), new BattleTeam.DrawInput(this.Drawinput));
                }
                else if (this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == this.BChar  && a.CharinfoSkilldata != SP.SkillData.CharinfoSkilldata) != null)
                {
                    this.BChar.MyTeam.ForceDraw(this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == this.BChar && a.CharinfoSkilldata != SP.SkillData.CharinfoSkilldata), new BattleTeam.DrawInput(this.Drawinput));
                }
                else if (this.BChar.MyTeam.Skills_UsedDeck.Find((Skill a) => a.Master == this.BChar  && a.CharinfoSkilldata != SP.SkillData.CharinfoSkilldata) != null)
                {
                    this.BChar.MyTeam.ForceDraw(this.BChar.MyTeam.Skills_UsedDeck.FindLast((Skill a) => a.Master == this.BChar && a.CharinfoSkilldata != SP.SkillData.CharinfoSkilldata), new BattleTeam.DrawInput(this.Drawinput));
                }
                else
                {
                    this.BChar.MyTeam.Draw(1, new BattleTeam.DrawInput(this.Drawinput));
                }
                
            }
        }
        
        public void Drawinput(Skill skill)
        {
            skill.ExtendedAdd(new Skill_Extended
            {
                APChange = -1
            });
        }
        
    }

    public class Sex_drm_16 : Sex_ImproveClone, IP_DamageChange_sumoperation
    {
        public override string DescExtended(string desc)
        {

            //return base.DescExtended(desc).Replace("&a", "<color=#32CD32>最高</color>").Replace("&c", ((int)(this.MySkill.TargetDamage * this.plusp)).ToString()).Replace("&d", ((int)(100 * this.plusp)).ToString());
            return base.DescExtended(desc).Replace("&a", ((int)(10*this.MySkill.TargetDamageOriginal)).ToString()).Replace("&b", ((int)(this.MySkill.TargetDamageOriginal)).ToString());
        }
        /*
        public override void Special_PointerEnter(BattleChar Char)
        {
            base.Special_PointerEnter(Char);
            //this.SkillBasePlusPreview.Target_BaseDMG = Math.Min((int)(0.1 *Char.GetStat.maxhp), (int)(2 * this.MySkill.TargetDamageOriginal));
            this.SkillBasePlusPreview.Target_BaseDMG = this.PlusDamage(Char);

        }
        public override void Special_PointerExit()
        {
            base.Special_PointerExit();
            this.SkillBasePlusPreview.Target_BaseDMG = 0;
        }
        
        
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {


            Skill_Extended skill_Extended = new Skill_Extended();

                skill_Extended.SkillBasePlus.Target_BaseDMG = this.PlusDamage(Targets[0]);
                SkillD.ExtendedAdd(skill_Extended);
            
        }
        */
        
        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if(SkillD==this.MySkill)
            {
                PlusDamage= this.PlusDamage(Target);
                //UnityEngine.Debug.Log("Sex_16_DamageChange_sumoperation");
            }
        }
        
        public int PlusDamage(BattleChar target)
        {
            float num= (float)(0.1 * target.GetStat.maxhp);
            if(num> this.MySkill.TargetDamageOriginal)
            {
                num = (float)( this.MySkill.TargetDamageOriginal + (num -  this.MySkill.TargetDamageOriginal) * 0.5);
            }
            /*
            int loop = 1;
            while(num>loop* this.MySkill.TargetDamageOriginal)
            {
                num = (float)(loop * this.MySkill.TargetDamageOriginal+(num- loop * this.MySkill.TargetDamageOriginal) *0.65);
                loop++;
            }
            */
            return (int)num;
            //int num = (int)(0.1 * target.GetStat.maxhp);
            //int num2 = num - (int)(0.5 * Math.Max(0, num - this.MySkill.TargetDamageOriginal));
            //return num2;
        }
        
    }
    public class Sex_drm_17 : Sex_ImproveClone
    {
        public override void BattleStartDeck(List<Skill> Skills_Deck)
        {
            this.BChar.MyTeam.Draw(this.MySkill, null);
        }
    }
        
    public class Bcl_drm_17_S : SF_Standard.InBuff,IP_ParticleOut_After,IP_BuffOnPointerEnter,IP_BuffOnPointerExit, IP_B17TExist, IP_DamageChangeFinal, IP_Check_B17S
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.Penetration = 40;
        }
        public override bool GetForce()
        {
            return true;
        }
        public override string DescExtended()
        {

            return base.DescExtended().Replace("&a", this.BChar.Info.Name).Replace("&b", this.plusper.ToString());
        }
        public void BuffOnPointerEnter(Buff buff)
        {
            if(buff==this)
            {
                BattleChar tar = BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Find(a => a==this.memory.bc);
                if(tar!=null)
                {
                    TargetSelects.OneTarget(tar);

                }
            }
        }
        public void BuffOnPointerExit(Buff buff)
        {
            if (buff == this)
            {
                TargetSelects.Clear(true);
            }

        }


        public IEnumerator ParticleOut_After(Skill SkillD, List<BattleChar> Targets)//mark
        {
            bool isforall = SkillD.TargetTypeKey == "all_enemy" || SkillD.TargetTypeKey == "all_ally" || SkillD.TargetTypeKey == "all" || SkillD.TargetTypeKey == "all_allyorenemy" || SkillD.TargetTypeKey == "all_other";

            if (!isforall && Targets.Count == 1 && SkillD.IsDamage && !SkillD.PlusHit /*&& !Targets[0].IsDead*/)
            {
                if (Targets[0] == this.memory.bc)
                {
                    this.memory.damageadd++;
                }
                else
                {
                    B17_Memory memory = new B17_Memory();
                    memory.bc = Targets[0];
                    this.memory = memory;
                    this.memory.damageadd++;
                }
                //------旧实现，现仅用作贴buff标识
                try
                {
                    if (!Targets[0].BuffFind("B_SF_Dreamer_17_T", false) || Targets[0].BuffReturn("B_SF_Dreamer_17_T", false).StackInfo.Find(a => a.UseState == this.BChar) == null)
                    {
                        List<BattleChar> list = new List<BattleChar>();
                        list.AddRange(BattleSystem.instance.AllyTeam.AliveChars_Vanish);
                        list.AddRange(BattleSystem.instance.EnemyTeam.AliveChars_Vanish);
                        foreach (BattleChar bc in list)
                        {
                            if (bc != Targets[0] && bc.BuffFind("B_SF_Dreamer_17_T", false))
                            {
                                Buff buff = bc.BuffReturn("B_SF_Dreamer_17_T", false);
                                for (int i = 0; i < buff.StackInfo.Count; i++)
                                {
                                    if (buff.StackInfo[i].UseState == this.BChar)
                                    {
                                        buff.StackInfo.RemoveAt(i);
                                        i--;
                                    }
                                }
                                if (buff.StackInfo.Count <= 0)
                                {
                                    buff.SelfDestroy();
                                }
                            }
                        }
                        if (!Targets[0].IsDead)
                        {
                            Targets[0].BuffAdd("B_SF_Dreamer_17_T", this.BChar, false, 0, false, -1, true);
                        }

                    }

                    Bcl_drm_17_T buff17 = Targets[0].BuffReturn("B_SF_Dreamer_17_T", false) as Bcl_drm_17_T;
                    buff17.HistorySet(this.BChar);
                }//----------
                catch
                {

                }
            }
            
            yield break;
        }
        public bool B17TExist(BattleChar taker, BattleChar user)
        {
            if (user == this.BChar && taker==this.memory.bc)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int Check_B17S(BattleChar taker, BattleChar user)
        {
            if (user == this.BChar && taker == this.memory.bc)
            {
                return this.memory.damageadd;
            }
            return 0;
        }

        //public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        //public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        public int DamageChangeFinal(Skill SkillD, BattleChar Target, int Damage, bool Cri, bool View)
        {
            int newDmg = Damage;
            if (Target == this.memory.bc && SkillD.Master==this.BChar)
            {
                newDmg = (int)(newDmg * (1 + this.plusper * 0.01 * this.memory.damageadd));
            }
            return newDmg;
        }

        public int plusper = 10;
        public B17_Memory memory = new B17_Memory();
    }

    public class Bcl_drm_17_T : ForBuff,IP_BuffAdd,IP_BuffUpdate,IP_BuffAddAfter,IP_B17TExist
    {
        /*
        public override void Init()
        {
            base.Init();
            this.force = true;
        }
        */
        public override bool GetForce()
        {
            return true;
        }
        public override string DescExtended()
        {
            string allname = string.Empty;//base.StackInfo[0].UseState.Info.Name;
            for (int i = 0; i < base.StackInfo.Count; i++)
            {
                if (allname != string.Empty)
                {
                    allname = allname + "/";
                }
                allname = allname + base.StackInfo[i].UseState.Info.Name;
            }
            string alldam = string.Empty;//(this.HistoryCheck(base.StackInfo[0].UseState) * this.plusper).ToString() + "%";
            for (int i = 0; i < base.StackInfo.Count; i++)
            {
                if (alldam != string.Empty)
                {
                    alldam = alldam + "/";
                }

                alldam = alldam + (this.HistoryCheck(base.StackInfo[i].UseState) * this.plusper).ToString() + "%";
            }


            return base.DescExtended().Replace("&a", allname).Replace("&b", alldam);



        }
        
        public int plusper = 10;

        public bool B17TExist(BattleChar taker, BattleChar user)
        {
            if (taker == this.BChar && this.StackInfo.Find(a=>a.UseState==user)!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
        public override void ForBuffAdded_Before(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {
            base.ForBuffAdded_Before(BuffUser, BuffTaker, addedbuff);
            this.Buffadded(BuffUser, BuffTaker, addedbuff);
        }
        public override void ForBuffAdded_After(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            base.ForBuffAdded_After(BuffUser, BuffTaker, addedbuff, stackBuff);
            this.BuffaddedAfter(BuffUser, BuffTaker, addedbuff, stackBuff);
        }
        */
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)//mark
        {
            if (BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key && this.StackNum >= this.BuffData.MaxStack && this.StackInfo.Find(a => a.UseState == BuffUser) == null)
            {
                this.StackInfo.Insert(0, this.StackInfo[0]);
            }
            else if (BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key && this.StackNum >= this.BuffData.MaxStack && this.StackInfo.Find(a => a.UseState == BuffUser) != null)
            {
                int id = this.StackInfo.FindIndex(a => a.UseState == BuffUser);
                StackBuff st = this.StackInfo[id];
                this.StackInfo.RemoveAt(id);
                this.StackInfo.Insert(0, st);
            }
            
        }
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key)
            {
                List<BattleChar> list = new List<BattleChar>();
                list.AddRange(BattleSystem.instance.AllyTeam.AliveChars_Vanish);
                list.AddRange(BattleSystem.instance.EnemyTeam.AliveChars_Vanish);
                foreach (BattleChar bc in list)
                {
                    if(bc!=this.BChar && bc.BuffFind(this.BuffData.Key,false))
                    {
                        Buff buff=bc.BuffReturn(this.BuffData.Key, false);
                        for (int i = 0;i<buff.StackInfo.Count;i++)
                        {
                            if (buff.StackInfo[i].UseState == BuffUser)
                            {
                                buff.StackInfo.RemoveAt(i);
                                i--;
                            }
                        }
                        if(buff.StackInfo.Count <= 0)
                        {
                            buff.SelfDestroy();
                        }
                    }
                }
            }
            this.BuffStatUpdate();
        }


        public void BuffUpdate(Buff MyBuff)
        {
            //Debug.Log("BuffUpdate");

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

                this.HistorySet(null);

            
        }





        public static bool CheckB17T(BattleChar taker,BattleChar user)
        {
            bool isB17T = false;
            foreach (IP_B17TExist ip_patch in user.IReturn<IP_B17TExist>())
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    isB17T = isB17T || ip_patch.B17TExist(taker,user);
                }
            }
            foreach (IP_B17TExist ip_patch in taker.IReturn<IP_B17TExist>())
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    isB17T = isB17T || ip_patch.B17TExist(taker, user);
                }
            }
            return isB17T;
        }

        public void HistorySet(BattleChar bc)
        {
            for (int i = 0; i < this.plist.Count; i++)
            {
                if (this.StackInfo.Find(a => a.UseState == this.plist[i].bc) == null)
                {
                    this.plist.RemoveAt(i);
                    i--;
                }
            }
            foreach (StackBuff sta in this.StackInfo)
            {
                if (this.plist.Find(a => a.bc == sta.UseState) == null)
                {
                    B17_Memory memory = new B17_Memory();
                   memory.bc = sta.UseState;
                    this.plist.Add(memory);
                }
            }

            if (bc != null)
            {
                //Debug.Log("设置B17");

                for (int i = 0; i < this.plist.Count; i++)
                {
                    if (bc == this.plist[i].bc)
                    {
                        this.plist[i].damageadd ++;
                    }
                }
            }
        }
        public int HistoryCheck(BattleChar bc)
        {
            int num = 0;
            if (bc != null)
            {
                /*
                for (int i = 0; i < this.plist.Count; i++)
                {
                    if (bc == this.plist[i].bc)
                    {
                        num = Math.Max(num, this.plist[i].damageadd);
                    }
                }
                */
                foreach (IP_Check_B17S ip_patch in BattleSystem.instance.IReturn<IP_Check_B17S>())
                {
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        num = Math.Max(num, ip_patch.Check_B17S(this.BChar, bc));
                    }
                }
            }

            return num;
        }
        
        public List<B17_Memory> plist=new List<B17_Memory>();
    }
    public class B17_Memory
    {
        public BattleChar bc = new BattleChar();
        public int damageadd = 0;
    }
    public interface IP_B17TExist
    {
        // Token: 0x06000001 RID: 1
        bool B17TExist(BattleChar taker, BattleChar user);
    }

    public interface IP_Check_B17S
    {
        // Token: 0x06000001 RID: 1
        int Check_B17S(BattleChar taker, BattleChar user);
    }

    //SkillEn
    public class SkillEn_SF_Dreamer_0 : Sex_ImproveClone//, IP_SkillUse_User_After
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return (MainSkill.TargetTypeKey == "enemy" || MainSkill.TargetTypeKey == "random_enemy" || MainSkill.TargetTypeKey == "all_onetarget");
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            //this.target = Targets[0];
            if(BattleSystem.instance.AllyTeam.AliveChars_Vanish.Find((BattleChar a) => a.Info.KeyData == "SF_Dreamer")!=null)
            {
                BattleSystem.DelayInput(this.Delay(Targets[0]));
            }
        }

        public IEnumerator Delay(BattleChar Target)
        {

                if(!Target.Info.Ally)
                {
                    Target.BuffAdd("B_SF_Dreamer_7", BattleSystem.instance.AllyTeam.AliveChars_Vanish.Find((BattleChar a) => a.Info.KeyData == "SF_Dreamer"), false, 0, false, -1, false);
                }
            yield return null;
            yield break;
        }
        /*
        public void SkillUseAfter(Skill SkillD)
        {
            if(this.target !=null && !this.target.Info.Ally)
            {
                target.BuffAdd("B_SF_Dreamer_7", BattleSystem.instance.AllyList.Find((BattleAlly a) => a.Info.KeyData == "SF_Dreamer"), false, 0, false, -1, false);

            }
        }
        */
        private BattleChar target;

    }

    public class SkillEn_SF_Dreamer_1 : Sex_ImproveClone
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return ( MainSkill.TargetTypeKey == "ally" || MainSkill.TargetTypeKey == "self" || MainSkill.TargetTypeKey == "otherally" || MainSkill.TargetTypeKey == "all_onetarget");
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            if (Targets[0].Info.Ally && BattleSystem.instance.AllyTeam.AliveChars_Vanish.Find((BattleChar a) => a.Info.KeyData == "SF_Dreamer") != null)
            {
                Targets[0].BuffAdd("B_SF_Dreamer_10", BattleSystem.instance.AllyTeam.AliveChars_Vanish.Find((BattleChar a) => a.Info.KeyData == "SF_Dreamer"), false, 0, false, -1, false);

            }

        }
    }





    //            bool a=BattleSystem.instance.AllyList.Find((BattleAlly a) => a.Info.KeyData == GDEItemKeys.Character_Azar) == SkillD.Master;


    // Token: 0x06000E24 RID: 3620 RVA: 0x0008495D File Offset: 0x0008}
    public class E_SF_Dreamer_0 : EquipBase,IP_BuffUpdate, IP_BattleEndOutBattle,IP_RangeChange, IP_CheckEquip//,IP_SkillUse_User_After//, IP_BuffAddAfter
    {
        public override void Init()
        {
            base.Init();
            //this.PlusStat.PlusCriDmg=60;
            this.PlusPerStat.Damage = 10;
            this.PlusStat.hit = 20;
            this.PlusStat.cri = 10;
        }

        /*
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)//mark
        {

            if (this.BChar.BuffFind("B_SF_Dreamer_0", false) && BuffTaker == this.BChar && addedbuff.BuffData.Key == "B_SF_Dreamer_0")
            {
                this.dis = this.BChar.BuffReturn("B_SF_Dreamer_0", false);

                if (BuffTaker == this.BChar && addedbuff.BuffData.Key == "B_SF_Dreamer_0" && this.dis.StackNum >= this.dis.BuffData.MaxStack)
                {

                    //this.dis.BuffData.MaxStack++;
                    this.isb0 = true;
                    this.oldb0 = this.dis.StackInfo[0];
                }

            }

        }
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (this.isb0)
            {
                this.dis.StackInfo.Insert(0, this.oldb0);
                this.dis.BuffStatUpdate();
            }
            this.isb0 = false;


        }
        */
        public bool CheckEquip(BattleChar bc)
        {
            if(bc==this.BChar)
            {
                return true;
            }
            return false;
        }
        public void RangeChange(BattleChar bc,Buff buff, int range_before, int range_after, ref bool overload)
        {
            if(bc==this.BChar)
            {
                overload = true;
            }
        }
        public void BuffUpdate(Buff MyBuff)
        {
            //Debug.Log("IP_BuffUpdate触发");
            if (this.BChar.BuffFind("B_SF_Dreamer_0", false))
            {
                Bcl_drm_0 buff = this.BChar.BuffReturn("B_SF_Dreamer_0", false) as Bcl_drm_0;

                //this.PlusStat.PlusCriDmg = 5 * this.disbuff.Range;
                this.PlusStat.PlusCriDmg = 5 * buff.Range;

                /*
                if (this.dis.StackNum != this.dis.BuffData.MaxStack)
                {
                    this.dis.BuffData.MaxStack = Math.Max(16,this.dis.StackNum);
                }
                */
            }
            else
            {
                this.PlusStat.PlusCriDmg = 0;
            }
        }

        public void BattleEndOutBattle()
        {
            this.PlusStat.PlusCriDmg = 0;
        }
        //public Bcl_drm_0 disbuff;
        private StackBuff oldb0= null;
        private bool isb0;
        private bool difSkill=false;
        private Skill lastSkill;
        private bool isjl=false;
    }


    public class E_SF_Dreamer_1 : EquipBase,IP_AntiVanish,IP_DamageChangeFinal
    {
        public override void Init()
        {
            base.Init();
            this.PlusPerStat.Damage = 20;
            this.PlusStat.hit = 10f;
            this.PlusStat.AggroPer = -90;
            this.PlusStat.IgnoreTaunt = true;

        }
        public bool AntiVanish(BattleChar Target, Skill skill)
        {
            if(skill.Master==this.BChar)
            {
                return true;
            }
            return false;
        }
        //public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        public int DamageChangeFinal(Skill SkillD, BattleChar Target, int Damage, bool Cri, bool View)
        {
            if(SkillD.Master==this.BChar && Target is BattleEnemy)
            {
                bool flag1 = false;
                if(Target.GetStat.HideHP)
                {
                    flag1 = true;
                }
                if (BattleSystem.instance.EnemyTeam.AliveChars.Find(a => a.HP > Target.HP && !a.GetStat.HideHP) == null) 
                {
                    flag1 = true;
                }
                if (BattleSystem.instance.EnemyTeam.AliveChars.Find(a => a.HP < Target.HP && !a.GetStat.HideHP) == null)
                {
                    flag1 = true;
                }
                if(flag1)
                {
                    return (int)(1.15f*Damage);
                }
            }
            return Damage;
        }
    }
    /*
    public class Item_drm_1 : UseitemBase
    {
        public override bool CantTarget(Character CharInfo)
        {
            return !(CharInfo.KeyData == "SF_Dreamer");
        }
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
                SelectSkillList.NewSelectSkillList(false, "选择一个技能遗忘", list, new SkillButton.SkillClickDel(this.ButtonDel), true, true, false, true);
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
                list.Add(Skill.TempSkill(characterSkillNoOverLap[j].KeyID, (this.pinfo.GetBattleChar)as BattleAlly, PlayData.TempBattleTeam));
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

        private Character pinfo=new Character();
    }
    */
    /*public class Item_SF_1 : UseitemBase
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
