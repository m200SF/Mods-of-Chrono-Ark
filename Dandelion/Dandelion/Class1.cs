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


using System.Text.RegularExpressions;

using UnityEngine.Events;

using static System.Net.Mime.MediaTypeNames;
using System.Runtime.ConstrainedExecution;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Xml.Linq;
using DG.Tweening;
using System.Threading;
using System.Security.Cryptography;
using SF_Standard;
using ChronoArkMod.ModData;

namespace GFL_Dandelion
{
    [PluginConfig("M200_GFL_Dandelion_CharMod", "GFL_Dandelion_CharMod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class GFL_Dandelion_CharacterModPlugin : ChronoArkPlugin
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
        public class DandelionDef : ModDefinition
        {
        }
    }

    //新职业测试-------------------------

    public struct CustomRoleData
    {
        // Token: 0x04000063 RID: 99
        public string ModKey;
        public string CharKey;
        public string key;
        public string text;
        public string CustomIcon;
        public string CustomIconSmall;
        public string Role_Custom;
        public string RoleB_Custom;
        public string BasicSkillKey;
        public List<string> InitSkillKeys;
        public string BloodMistPlusSkill;
    }
    //this.Role.sprite = this.RoleB_Tank;
    //this.PositionIcon.sprite = this.TankIcon;
    //this.Main.RoleImage.sprite = this.Main.Role_DEF;
    //this.RoleImage.sprite = this.Role_DEF;
    //this.CharacterRoleImage.sprite = this.TankIconSmall;
    public enum CustomRoleEnum
    {
        Role_Ogas
    }
    public class CustomRoleCtrl
    {

        public static Dictionary<CustomRoleEnum, CustomRoleData> roleData = new Dictionary<CustomRoleEnum, CustomRoleData>
        {
            {
                CustomRoleEnum.Role_Ogas,
                new CustomRoleData
                {
                    ModKey="GFL_Dandelion",
                    CharKey="GFL_Dandelion",
                    key = "Role_OGAS",
                    text = "OGAS",
                    CustomIcon = "Role_OGAS//Role_OGAS.png",
                    CustomIconSmall="Role_OGAS//Role_OGAS.png",
                    Role_Custom = "Role_OGAS//Role_OGAS.png",
                    RoleB_Custom = "Role_OGAS//Role_OGAS.png",
                    BasicSkillKey="S_DefultSkill_0",
                    InitSkillKeys=new List<string> { "S_DefultSkill_0" ,
                    "S_DefultSkill_0",
                    "S_DefultSkill_1",
                    "S_DefultSkill_2"},
                    BloodMistPlusSkill="S_DefultSkill_0"
                }
            }
        };
    }

    public static class CustomRoleTools
    {
        public static CustomRoleEnum Enum = CustomRoleEnum.Role_Ogas;

        public static bool CheckRole(string rolekey)
        {
            CustomRoleData roleData = CustomRoleCtrl.roleData[CustomRoleTools.Enum];
            return roleData.key == rolekey;
        }
        public static Sprite CustomIconSprite
        {
            get
            {
                CustomRoleData data = CustomRoleCtrl.roleData[CustomRoleTools.Enum];
                ModInfo modInfo = ModManager.getModInfo(data.ModKey);
                string path = ((modInfo != null) ? modInfo.assetInfo.ImageFromFile(data.CustomIcon) : null);
                Sprite spr = AddressableLoadManager.LoadAsyncCompletion<Sprite>(path, AddressableLoadManager.ManageType.None);
                return spr;
            }
        }
        public static Sprite CustomIconSmallSprite
        {
            get
            {
                CustomRoleData data = CustomRoleCtrl.roleData[CustomRoleTools.Enum];
                ModInfo modInfo = ModManager.getModInfo(data.ModKey);
                string path = ((modInfo != null) ? modInfo.assetInfo.ImageFromFile(data.CustomIconSmall) : null);
                Sprite spr = AddressableLoadManager.LoadAsyncCompletion<Sprite>(path, AddressableLoadManager.ManageType.None);
                return spr;
            }
        }
        public static Sprite Role_CustomSprite
        {
            get
            {
                CustomRoleData data = CustomRoleCtrl.roleData[CustomRoleTools.Enum];
                ModInfo modInfo = ModManager.getModInfo(data.ModKey);
                string path = ((modInfo != null) ? modInfo.assetInfo.ImageFromFile(data.Role_Custom) : null);
                Sprite spr = AddressableLoadManager.LoadAsyncCompletion<Sprite>(path, AddressableLoadManager.ManageType.None);
                return spr;
            }
        }
        public static Sprite RoleB_CustomSprite
        {
            get
            {
                CustomRoleData data = CustomRoleCtrl.roleData[CustomRoleTools.Enum];
                ModInfo modInfo = ModManager.getModInfo(data.ModKey);
                string path = ((modInfo != null) ? modInfo.assetInfo.ImageFromFile(data.RoleB_Custom) : null);
                Sprite spr = AddressableLoadManager.LoadAsyncCompletion<Sprite>(path, AddressableLoadManager.ManageType.None);
                return spr;
            }
        }

    }

    [HarmonyPatch(typeof(CharacterCollection))]
    public static class CharacterCollectionPlugin
    {
        [HarmonyPatch("CharacterInfoOn")]
        [HarmonyPostfix]
        public static void CharacterInfoOn_Patch(CharacterCollection __instance, int n)
        {
            CustomRoleData data = CustomRoleCtrl.roleData[CustomRoleTools.Enum];
            if (__instance.CharacterList[n].Role.Key == data.key)
            {
                __instance.CharacterRoleImage.sprite = __instance.HealIconSmall;
                __instance.CharacterRoleImage.SetNativeSize();
                __instance.CharacterRoleImage.sprite = CustomRoleTools.CustomIconSmallSprite;
            }
        }

        [HarmonyPatch("CharacterInfoOnName")]
        [HarmonyPostfix]
        public static void CharacterInfoOnName_Patch(CharacterCollection __instance, string Name)
        {
            CustomRoleData data = CustomRoleCtrl.roleData[CustomRoleTools.Enum];

            GDECharacterData gdecharacterData = null;
            for (int i = 0; i < __instance.CharacterList.Count; i++)
            {
                if (__instance.CharacterList[i].Key == Name)
                {
                    gdecharacterData = __instance.CharacterList[i];
                }
            }
            if (gdecharacterData != null && gdecharacterData.Role.Key == data.key)
            {
                __instance.CharacterRoleImage.sprite = __instance.HealIconSmall;
                __instance.CharacterRoleImage.SetNativeSize();
                __instance.CharacterRoleImage.sprite = CustomRoleTools.CustomIconSmallSprite;
            }
        }
        [HarmonyPatch("Init")]
        [HarmonyPostfix]
        public static void Init_Patch(CharacterCollection __instance)
        {
            CustomRoleData data = CustomRoleCtrl.roleData[CustomRoleTools.Enum];

            for (int i = 0; i < __instance.Align.transform.childCount; i++)
            {
                Transform child = __instance.Align.transform.GetChild(i);
                CharacterPrefab component = child.GetComponent<CharacterPrefab>();
                if (__instance.CharacterList[component.Index].Role.Key == data.key)
                {
                    component.CharacterRole.sprite = CustomRoleTools.CustomIconSprite;
                }
            }
        }
    }

    [HarmonyPatch(typeof(CharacterDocument))]
    public static class CharacterDocumentPlugin
    {
        [HarmonyPatch("CharInfoSet")]
        [HarmonyPostfix]
        public static void CharInfoSet_Patch(CharacterDocument __instance)
        {
            CustomRoleData data = CustomRoleCtrl.roleData[CustomRoleTools.Enum];

            if (__instance.data.Role.Key == data.key)
            {
                __instance.PositionImage.sprite = CustomRoleTools.CustomIconSprite;
            }
        }

    }
    [HarmonyPatch(typeof(CharacterWindow))]
    public static class CharacterWindowPlugin
    {
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        public static void Update_Patch(CharacterWindow __instance)
        {
            CustomRoleData data = CustomRoleCtrl.roleData[CustomRoleTools.Enum];

            if (__instance.AllyCharacter.Info.GetData.Role.Key == data.key)
            {
                __instance.RoleImage.sprite = CustomRoleTools.Role_CustomSprite;
            }
        }
    }
    [HarmonyPatch(typeof(CharFace))]
    public static class CharFacePlugin
    {
        [HarmonyPatch("Init")]
        [HarmonyPostfix]
        public static void Init_Patch(CharFace __instance, CharStatV4 ___Main)
        {
            CustomRoleData data = CustomRoleCtrl.roleData[CustomRoleTools.Enum];

            if (__instance.AllyCharacter.Info.GetData.Role.Key == data.key)
            {
                ___Main.RoleImage.sprite = CustomRoleTools.Role_CustomSprite;
                ___Main.RoleText.text = data.text;
            }
        }
    }
    [HarmonyPatch(typeof(CharSelectButtonV2))]
    public static class CharSelectButtonV2Plugin
    {
        [HarmonyPatch("Init")]
        [HarmonyPostfix]
        public static void Init_Patch(CharSelectButtonV2 __instance)
        {
            CustomRoleData data = CustomRoleCtrl.roleData[CustomRoleTools.Enum];

            if (__instance.data.Role.Key == data.key)
            {
                __instance.Icon.sprite = CustomRoleTools.CustomIconSprite;
            }
        }
    }
    [HarmonyPatch(typeof(CharSelectMainUIV2))]
    public static class CharSelectMainUIV2Plugin
    {
        [HarmonyPatch("ShowInfo")]
        [HarmonyPostfix]
        public static void Init_Patch(CharSelectMainUIV2 __instance, int num)
        {
            CustomRoleData data = CustomRoleCtrl.roleData[CustomRoleTools.Enum];

            if (__instance.CharDatas[num].Role.Key == data.key)
            {
                __instance.Position.text = data.text;
                __instance.PositionIcon.sprite = CustomRoleTools.CustomIconSprite;
            }
        }
    }


    //非图标相关patch----------
    [HarmonyPatch(typeof(Character))]
    public class TestCharacterPlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("Set_AllyData")]
        [HarmonyPostfix]
        private static void Set_AllyData_Patch(Character __instance, GDECharacterData Data)//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
            CustomRoleData data = CustomRoleCtrl.roleData[CustomRoleTools.Enum];

            bool flag = Data.Role.Key == data.key;
            if (flag)
            {

                __instance.BasicSkill = new CharInfoSkillData(data.BasicSkillKey);
                foreach(string str in data.InitSkillKeys)
                {
                    __instance.SkillDatas.Add(new CharInfoSkillData(str));
                }

                bool flag5 = PlayData.TSavedata.bMist != null && PlayData.TSavedata.bMist.Level >= 2;
                if (flag5)
                {
                    __instance.SkillDatas.Add(new CharInfoSkillData(data.BloodMistPlusSkill));
                }  
            }

        }
    }
    //
    [HarmonyPatch(typeof(BloodyMist))]
    public class BloodyMist_IncreaseLevel_Plugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("IncreaseLevel")]
        [HarmonyPostfix]
        private static void IncreaseLevel_Patch(BloodyMist __instance)
        {
            if(__instance.Level==2)
            {
                CustomRoleData data = CustomRoleCtrl.roleData[CustomRoleTools.Enum];

                foreach (Character character in PlayData.TSavedata.Party)
                {
                    if (character.GetData.Role.Key == data.key)
                    {
                        character.SkillDatas.Add(new CharInfoSkillData(data.BloodMistPlusSkill));
                    }
                }
            }

        }
    }

    [HarmonyPatch(typeof(Character))]
    public class Character_SkillAutoRemove_DefultSkill_Plugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("SkillAutoRemove_DefultSkill")]
        [HarmonyPrefix]
        private static bool SkillAutoRemove_DefultSkill_Patch(Character __instance)
        {
            CustomRoleData data = CustomRoleCtrl.roleData[CustomRoleTools.Enum];
            if (__instance.GetData.Role.Key == data.key)
            {
                if (__instance.SkillDatas.Count > PlayData.TSavedata.SkillMinNum)
                {
                    if (__instance.SkillDatas.Any((CharInfoSkillData s) => s.SkillInfo.KeyID == GDEItemKeys.Skill_S_SacrificeSkill))
                    {
                        CharInfoSkillData item = __instance.SkillDatas.Find((CharInfoSkillData s) => s.SkillInfo.KeyID == GDEItemKeys.Skill_S_SacrificeSkill);
                        __instance.SkillDatas.Remove(item);
                    }
                }
                while (__instance.SkillDatas.Count > PlayData.TSavedata.SkillMinNum)
                {
                    if (!__instance.SkillDatas.Any((CharInfoSkillData s) => s.SkillInfo.Category.Key == GDEItemKeys.SkillCategory_DefultSkill))
                    {
                        break;
                    }
                    List<CharInfoSkillData> list = __instance.SkillDatas.FindAll((CharInfoSkillData s) => s.SkillInfo.Category.Key == GDEItemKeys.SkillCategory_DefultSkill);
                    if (list.Count > 0)
                    {
                        CharInfoSkillData charInfoSkillData = list.Find((CharInfoSkillData s) => s.SkillInfo.KeyID == GDEItemKeys.Skill_S_DefultSkill_2);
                        list.Remove(charInfoSkillData);
                        if (list.Count > 0)
                        {
                            charInfoSkillData = list[0];
                        }
                        if (charInfoSkillData != null)
                        {
                            __instance.SkillDatas.Remove(charInfoSkillData);
                        }
                    }


                }

                    return false;
            }
            return true;
        }
    }





    //新职业测试end-------------------------

    [HarmonyPatch(typeof(FriendShipUI))]
    [HarmonyPatch("Gift")]
    public static class GiftPlugin
    {
        [HarmonyPostfix]
        public static void Gift_AfterPatch(FriendShipUI __instance, string CharId)
        {

            if (CharId == "GFL_Dandelion")
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
            bool flag = inputitem.itemkey == "E_GFL_Dandelion_0" && __instance.Info.GetData.Key != "GFL_Dandelion";
            if (flag)
            {
                __result = true;
            }
            //Debug.Log("成功进入OnDropSlot的patch");
        }
    }

    [HarmonyPatch(typeof(Skill))]
    [HarmonyPatch("GetCriPer")]
    public static class GetCriPerPlugin
    {
        [HarmonyPostfix]
        public static void GetCriPer_After(Skill __instance, BattleChar Target, int Plus, ref int __result)
        {
            //Debug.Log("抵抗减益:" + Time.frameCount);
            foreach (IP_GetCriPer_After ip_patch in __instance.Master.IReturn<IP_GetCriPer_After>(__instance))
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    ip_patch.GetCriPer_After(__instance, Target, Plus, ref __result);
                }
            }
        }
    }
    public interface IP_GetCriPer_After
    {
        // Token: 0x06000001 RID: 1
        void GetCriPer_After(Skill skill, BattleChar Target, int Plus, ref int result);
    }


    [HarmonyPatch(typeof(BattleSystem))]
    [HarmonyPatch("DebuffResist")]
    public static class DebuffResistPlugin
    {
        [HarmonyPostfix]
        public static void DebuffResist_After(BattleSystem __instance, Buff buff)
        {
            //Debug.Log("抵抗减益:" + Time.frameCount);
            foreach (IP_DebuffResist_After ip_patch in BattleSystem.instance.IReturn<IP_DebuffResist_After>())
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    ip_patch.DebuffResist_After(buff);
                }
            }
        }
    }
    public interface IP_DebuffResist_After
    {
        // Token: 0x06000001 RID: 1
        void DebuffResist_After(Buff buff);
    }

    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("BuffAdd")]
    public static class BuffAddPlugin
    {
        [HarmonyPriority(-200)]
        [HarmonyPrefix]
        public static void BuffAdd_Before(BattleChar __instance, string key, BattleChar UseState, bool hide, ref int PlusTagPer, bool debuffnonuser, int RemainTime, bool StringHide)
        {
            if (BattleSystem.instance != null)
            {
                foreach (IP_BuffAdd_Before ip_patch in BattleSystem.instance.IReturn<IP_BuffAdd_Before>())
                {
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.BuffAdd_Before(__instance, key, UseState, hide, ref PlusTagPer, debuffnonuser, RemainTime, StringHide);
                    }
                }
            }

        }
        [HarmonyPriority(-1)]
        [HarmonyPostfix]
        public static void BuffAdd_After(BattleChar __instance, string key, BattleChar UseState, bool hide, int PlusTagPer, bool debuffnonuser, int RemainTime, bool StringHide, Buff __result)
        {
            //Debug.Log("BuffAdd_After:" + PlusTagPer);
            if (BattleSystem.instance != null)
            {
                foreach (IP_BuffAdd_After2 ip_patch in BattleSystem.instance.IReturn<IP_BuffAdd_After2>())
                {
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.BuffAdd_After2(__instance, key, UseState, hide, PlusTagPer, debuffnonuser, RemainTime, StringHide, __result);
                    }
                }
            }

        }
    }
    public interface IP_BuffAdd_Before
    {
        // Token: 0x06000001 RID: 1
        void BuffAdd_Before(BattleChar bchar, string key, BattleChar UseState, bool hide, ref int PlusTagPer, bool debuffnonuser, int RemainTime, bool StringHide);
    }

    public interface IP_BuffAdd_After2
    {
        // Token: 0x06000001 RID: 1
        void BuffAdd_After2(BattleChar bchar, string key, BattleChar UseState, bool hide, int PlusTagPer, bool debuffnonuser, int RemainTime, bool StringHide, Buff __result);
    }
    //-------------
    [HarmonyPatch(typeof(SkillTargetTooltip))]
    [HarmonyPatch("InputInfo")]
    public static class SkillTargetTooltip_InputInfoPlugin
    {
        [HarmonyPrefix]
        public static void InputInfo_Before(BattleChar bc, Skill useskill)
        {
            if (BattleSystem.instance != null)
            {
                foreach (IP_InputInfo_Before ip_patch in BattleSystem.instance.IReturn<IP_InputInfo_Before>())
                {
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.InputInfo_Before(bc, useskill);
                    }
                }
            }

        }
        [HarmonyPostfix]
        public static void InputInfo_After(BattleChar bc, Skill useskill)
        {
            //Debug.Log("BuffAdd_After:" + PlusTagPer);
            if (BattleSystem.instance != null)
            {
                foreach (IP_InputInfo_After ip_patch in BattleSystem.instance.IReturn<IP_InputInfo_After>())
                {
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.InputInfo_After(bc, useskill);
                    }
                }
                foreach (IP_InputInfo_After ip_patch in useskill.IReturn<IP_InputInfo_After>())
                {
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.InputInfo_After(bc, useskill);
                    }
                }
            }

        }
    }
    public interface IP_InputInfo_Before
    {
        // Token: 0x06000001 RID: 1
        void InputInfo_Before(BattleChar bc, Skill useskill);
    }
    public interface IP_InputInfo_After
    {
        // Token: 0x06000001 RID: 1
        void InputInfo_After(BattleChar bc, Skill useskill);
    }



    //----------------

    [HarmonyPatch(typeof(BattleAlly))]
    [HarmonyPatch("Dead")]
    public static class BattleAllyDeadPlugin
    {
        [HarmonyPostfix]
        public static void BattleAllyDead_After(BattleAlly __instance)
        {
            //Debug.Log("排查点A");
            BattleSystem.DelayInputAfter(BattleAllyDeadPlugin.NewTarget(__instance));
            //Debug.Log("排查点B");
            /*
            for (int i = 0; __instance is BattleAlly && i < BattleSystem.instance.EnemyCastSkills.Count; i++)
            {
                List<BattleChar> list = new List<BattleChar>();
                if (BattleSystem.instance.EnemyCastSkills[i].skill.TargetTypeKey == GDEItemKeys.s_targettype_enemy)
                {
                    list.Add(((BattleSystem.instance.EnemyCastSkills[i].skill.Master as BattleEnemy).Ai).AgrroReturn(__instance));
                }
                if (BattleSystem.instance.EnemyCastSkills[i].skill.TargetTypeKey == GDEItemKeys.s_targettype_enemy_PlusRandom)
                {
                    list.Add(((BattleSystem.instance.EnemyCastSkills[i].skill.Master as BattleEnemy).Ai).AgrroReturn(__instance));
                    //Debug.Log(BattleSystem.instance.AllyList.Count);
                    if (BattleSystem.instance.AllyList.Count >= 2)
                    {
                        BattleChar battleChar = ((BattleSystem.instance.EnemyCastSkills[i].skill.Master as BattleEnemy).Ai).AgrroReturn(list[0] as BattleAlly);
                        if (battleChar != null)
                        {
                            list.Add(battleChar);
                        }
                    }
                }
                //list.AddRange((BattleSystem.instance.EnemyCastSkills[i].Usestate as BattleEnemy).Ai.TargetSelect(BattleSystem.instance.EnemyCastSkills[i].skill));
                //Debug.Log("新目标");
                foreach (BattleChar bc in list)
                {
                    //Debug.Log(bc.Info.Name + (bc.IsDead ? "死亡" : "存活"));
                }
                int count = list.Count;
                if (list.Count>=1 && (BattleSystem.instance.EnemyCastSkills[i].Target.IsDead || BattleSystem.instance.EnemyCastSkills[i].Target==null))
                {
                    BattleSystem.instance.EnemyCastSkills[i].Target = list[0];
                    list.RemoveAt(0);
                }
                if (BattleSystem.instance.EnemyCastSkills[i].skill.MySkill.Target.Key == GDEItemKeys.s_targettype_enemy_PlusRandom && count >= 2 && (BattleSystem.instance.EnemyCastSkills[i].TargetPlus.IsDead || BattleSystem.instance.EnemyCastSkills[i].TargetPlus == null))
                {
                    BattleSystem.instance.EnemyCastSkills[i].TargetPlus = list[0];
                }
                if (BattleSystem.instance.EnemyCastSkills[i].Usestate.BuffFind(GDEItemKeys.Buff_B_Control_P, false))
                {
                    EffectView.TextOutSimple(BattleSystem.instance.EnemyCastSkills[i].Usestate, ScriptLocalization.UI_Battle.TargetChanged);
                }
            }
            */
        }

        public static IEnumerator NewTarget(BattleAlly ally)
        {
          //Debug.Log("排查点0");
          //Debug.Log("死亡者："+ally.Info.Name);
            for (int i = 0; ally is BattleAlly && i < BattleSystem.instance.EnemyCastSkills.Count; i++)
            {
                List<BattleChar> list = new List<BattleChar>();
              //Debug.Log("排查点1-1");
              //Debug.Log("排查点1-1:"+ BattleSystem.instance.EnemyCastSkills[i].skill.MySkill.Name);
                if(BattleSystem.instance.EnemyCastSkills[i].Usestate is BattleEnemy)
                {
                    list.AddRange((BattleSystem.instance.EnemyCastSkills[i].Usestate as BattleEnemy).Ai.TargetSelect(BattleSystem.instance.EnemyCastSkills[i].skill));
                }
              //Debug.Log("排查点1-2");
                try
                {
                    //Debug.Log(BattleSystem.instance.EnemyCastSkills[i].Target.Info.Name + (BattleSystem.instance.EnemyCastSkills[i].Target.IsDead ? "死亡" : "存活"));
                    //Debug.Log(BattleSystem.instance.EnemyCastSkills[i].TargetPlus.Info.Name + (BattleSystem.instance.EnemyCastSkills[i].TargetPlus.IsDead ? "死亡" : "存活"));
                }
                catch { }
                //Debug.Log("新目标");
                foreach (BattleChar bc in list)
                {
                    //Debug.Log(bc.Info.Name + (bc.IsDead ? "死亡" : "存活"));
                }
              //Debug.Log("排查点2");
                int count = list.Count;
              //Debug.Log("排查点3");
                if (list.Count >= 1 && (BattleSystem.instance.EnemyCastSkills[i].Target==null || BattleSystem.instance.EnemyCastSkills[i].Target.IsDead))
                {
                    BattleSystem.instance.EnemyCastSkills[i].Target = list[0];

                    if (BattleSystem.instance.EnemyCastSkills[i].TargetPlus == BattleSystem.instance.EnemyCastSkills[i].Target)
                    {
                        BattleSystem.instance.EnemyCastSkills[i].TargetPlus = new BattleChar();
                    }
                }
              //Debug.Log("排查点4");
                list.Remove(BattleSystem.instance.EnemyCastSkills[i].Target);
                if (BattleSystem.instance.EnemyCastSkills[i].skill.MySkill.Target.Key == GDEItemKeys.s_targettype_enemy_PlusRandom && list.Count >= 1 && (BattleSystem.instance.EnemyCastSkills[i].TargetPlus.IsDead || BattleSystem.instance.EnemyCastSkills[i].TargetPlus == null))
                {
                    BattleSystem.instance.EnemyCastSkills[i].TargetPlus = list[0];

                }
              //Debug.Log("排查点5");
                /*
                BattleSystem.instance.EnemyCastSkills[i].Target = list[0];
                if (BattleSystem.instance.EnemyCastSkills[i].skill.MySkill.Target.Key == GDEItemKeys.s_targettype_enemy_PlusRandom && list.Count >= 2)
                {
                    BattleSystem.instance.EnemyCastSkills[i].TargetPlus = list[1];
                }
                */
                //Debug.Log("最终目标");
                try
                {
                    //Debug.Log(BattleSystem.instance.EnemyCastSkills[i].Target.Info.Name + (BattleSystem.instance.EnemyCastSkills[i].Target.IsDead ? "死亡" : "存活"));
                    //Debug.Log(BattleSystem.instance.EnemyCastSkills[i].TargetPlus.Info.Name + (BattleSystem.instance.EnemyCastSkills[i].TargetPlus.IsDead ? "死亡" : "存活"));
                }
                catch { }
                /*
                if (BattleSystem.instance.EnemyCastSkills[i].Usestate.BuffFind(GDEItemKeys.Buff_B_Control_P, false))
                {
                    EffectView.TextOutSimple(BattleSystem.instance.EnemyCastSkills[i].Usestate, ScriptLocalization.UI_Battle.TargetChanged);
                }
                */
                //Debug.Log("-----------");

            }
            //Debug.Log("+++++++++++");
            yield break;
        }
    }


    [HarmonyPatch(typeof(BattleSystem))]
    [HarmonyPatch("EnemyActionScene")]
    public static class EnemyActionScenePlugin
    {
        [HarmonyPrefix]
        public static void EnemyActionScene_Before(CastingSkill Skill, bool IsDelayWait)
        {

            bool flag1 = Skill.Usestate.BuffFind("B_GFL_Dandelion_2_2", false) && !B_Control_P.UnknownTarget(Skill.skill.MySkill) && !Skill.Target.IsDead && Skill.Target != null;
            if (flag1)
            {
                EnemyActionScenePlugin.castskill = Skill;
            }
            else
            {
                EnemyActionScenePlugin.castskill = new CastingSkill();
            }

        }

        public static CastingSkill castskill = new CastingSkill();
    }

    [HarmonyPatch(typeof(BattleEnemy))]
    [HarmonyPatch("SkillSelect")]
    public static class SkillSelectPlugin
    {
        [HarmonyPostfix]
        public static void SkillSelect_After(BattleEnemy __instance, Skill Select_Skill)
        {
            bool flag1 = __instance.BuffFind("B_GFL_Dandelion_2_2", false) && !B_Control_P.UnknownTarget(Select_Skill.MySkill);
            if (flag1 && EnemyActionScenePlugin.castskill != new CastingSkill())
            {

                if (EnemyActionScenePlugin.castskill.Target != null || EnemyActionScenePlugin.castskill.TargetPlus != null)
                {
                    __instance.SaveTarget.Clear();

                    if (EnemyActionScenePlugin.castskill.Target != null)
                    {
                        __instance.SaveTarget.Add(EnemyActionScenePlugin.castskill.Target);
                    }
                    if (EnemyActionScenePlugin.castskill.TargetPlus != null)
                    {
                        __instance.SaveTarget.Add(EnemyActionScenePlugin.castskill.TargetPlus);
                    }

                }

                EnemyActionScenePlugin.castskill = new CastingSkill();

            }

        }
    }
    [HarmonyPatch(typeof(BattleEnemy))]
    [HarmonyPatch("PointerEnter")]
    public static class PointerEnterPlugin
    {
        [HarmonyPostfix]
        public static void PointerEnter_Patch(BattleEnemy __instance)
        {
            if (BattleSystem.instance != null)
            {

                //Debug.Log("PointerEnter:" + __instance.Info.Name);
                /*
                bool flag1 = !BattleSystem.instance.TargetSelecting && __instance.BuffFind("B_GFL_Dandelion_2_2", false) && __instance.SkillQueue.Count != 0 && !B_Control_P.UnknownTarget(__instance.SkillQueue[0].skill.MySkill);
                if(flag1)
                {
                    BattleSystem.instance.CastTargetView(__instance.SkillQueue[0], false);
                }
                */
                foreach (IP_PointerEnter_Patch ip_patch in BattleSystem.instance.IReturn<IP_PointerEnter_Patch>())
                {
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.PointerEnter_Patch(__instance);
                    }
                }
            }
        }

        public static BattleEnemy rec = new BattleEnemy();
    }
    public interface IP_PointerEnter_Patch
    {
        // Token: 0x06000001 RID: 1
        void PointerEnter_Patch(BattleEnemy enemy);
    }


    [HarmonyPatch(typeof(BattleEnemy))]
    [HarmonyPatch("PointerExit")]
    public static class PointerExitPlugin
    {
        [HarmonyPrefix]
        public static void PointerExit_After(BattleEnemy __instance)
        {
            if (BattleSystem.instance != null)
            {
                //Debug.Log("PointerExit:" + __instance.Info.Name);
                foreach (IP_PointerExit_After ip_patch in BattleSystem.instance.IReturn<IP_PointerExit_After>())
                {
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.PointerExit_After(__instance);
                    }
                }

            }
        }
    }
    public interface IP_PointerExit_After
    {
        // Token: 0x06000001 RID: 1
        void PointerExit_After(BattleEnemy enemy);
    }


    [HarmonyPatch(typeof(BuffObject))]//雾月猎场显示目标
    [HarmonyPatch("OnPointerEnter")]
    public static class BuffOnPointerEnterPlugin
    {
        [HarmonyPostfix]
        public static void BuffOnPointerEnter(BuffObject __instance)
        {
            if (BattleSystem.instance != null && __instance.MyBuff!=null)
            {
                foreach (IP_BuffOnPointerEnter ip_patch in BattleSystem.instance.IReturn<IP_BuffOnPointerEnter>())
                {
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.BuffOnPointerEnter(__instance.MyBuff);
                    }
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

            if (BattleSystem.instance != null && __instance.MyBuff != null)
            {
                foreach (IP_BuffOnPointerExit ip_patch in BattleSystem.instance.IReturn<IP_BuffOnPointerExit>())
                {
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.BuffOnPointerExit(__instance.MyBuff);
                    }
                }
            }
        }

    }
    public interface IP_BuffOnPointerExit
    {
        // Token: 0x06000001 RID: 1
        void BuffOnPointerExit(Buff buff);
    }















    [HarmonyPatch(typeof(SkillToolTip))]
    [HarmonyPatch("Input")]
    public static class SkillToolTip_Input_Plugin
    {
        [HarmonyPostfix]
        public static void SkillToolTip_Input_Patch(SkillToolTip __instance, Skill Skill)
        {
            if (Skill.MySkill.PlusKeyWords.Find(a => a.Key == "KeyWord_SF_Dandelion_antiVanish") != null)
            {
                //Debug.Log("input_patch");
                string text1 = string.Empty;
                string text2 = string.Empty;



                //text2= Misc.DesStringsRest(list2);

                GDESkillKeywordData keyw = new GDESkillKeywordData("KeyWord_SF_Dandelion_antiVanish");

                text2 = SkillToolTip.ColorChange("FF7C34", ScriptLocalization.Battle_Keyword.IgnoreTaunt);
                text1 = text2 + ". " + SkillToolTip.ColorChange("FF7C34", keyw.Name);


                //text1 = Misc.DesStringsRest(list1);
                Regex re1 = new Regex(text2);
                string text3 = re1.Replace(__instance.Desc.text, text1, 1);
                if (text3.Contains(text2))
                {
                    __instance.Desc.text = text3;


                }

            }

        }
    }

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
        [HarmonyFinalizer, HarmonyPriority(-401)]
        public static void IsSelect_Patch(ref bool __result, BattleSystem __instance, BattleChar Target, Skill skill)
        {

            //Debug.Log("isselect");
            bool antiVanish = false;
            bool antiTaunt = false;

            foreach (IP_AntiVanish ip_patch in skill.Master.IReturn<IP_AntiVanish>(skill))
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

            antiTaunt = antiTaunt || antiVanish;

            if (antiVanish || antiTaunt)
            {
                string targetTypeKey = skill.TargetTypeKey;
                bool ally = skill.Master.Info.Ally;


                if (!antiVanish && (Target.GetStat.Vanish))//changed
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
                    flag = BattleSystem_dan.TargetSelectCheck_dan(antiTaunt, Target, skill, flag);
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
                    flag = (ally == Target.Info.Ally || BattleSystem_dan.TargetSelectCheck_dan(antiTaunt, Target, skill, flag));
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
        [HarmonyFinalizer, HarmonyPriority(-401)]
        public static void SkillTargetReturn_Patch(ref List<BattleChar> __result, BattleSystem __instance, Skill sk, BattleChar Target, BattleChar PlusTarget = null)
        {

            bool antiVanish = false;

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

                __result = list;

            }
        }
    }

    [HarmonyPriority(-1)]





    public class BattleSystem_dan : MonoBehaviour
    {
        public static bool TargetSelectCheck_dan(bool antiTaunt, BattleChar Target, Skill skill, bool selected)
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









    public interface IP_Equip_Dandelion
    {
        // Token: 0x06000001 RID: 1
        bool Equip_Dandelion(BattleChar bchar);
    }




    public class P_Dandelion : Passive_Char, IP_DebuffResist_After, IP_BuffAdd_Before, IP_BuffAddAfter, IP_LevelUp, IP_BuffAdd, IP_InputInfo_Before
    {
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {
            //Debug.Log(addedbuff.BuffData.Key);
        }
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
                list.Add(ItemBase.GetItem("E_GFL_Dandelion_0", 1));
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
        public void DebuffResist_After(Buff buff)
        {
            
            bool equip = false;
            try
            {
                foreach (IP_Equip_Dandelion ip_patch in this.BChar.IReturn<IP_Equip_Dandelion>())
                {
                    //Debug.Log("point_resisi");
                    if (equip)
                    {
                        break;
                    }
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        bool flag2 = ip_patch.Equip_Dandelion(this.BChar);
                        equip = equip || flag2;
                    }
                }
            }
            catch
            {
                //Debug.Log("error_resisi");
            }

            
            bool active = (buff.Usestate_L == this.BChar||(equip && this.BChar.MyTeam.AliveChars_Vanish.Contains(buff.Usestate_L)));//后续可修改条件

            if (active && !buff.IsHide && !this.BChar.MyTeam.AliveChars_Vanish.Contains(buff.BChar))
            {
                BattleSystem.DelayInput(this.AtkRecordsUpdata(buff.BChar, buff.BuffData.BuffTag.Key));

            }

            bool active2 = (buff.BChar == this.BChar || (equip && this.BChar.MyTeam.AliveChars_Vanish.Contains(buff.BChar)));
            if (buff.BChar.Buffs.Find(a=>a is Bcl_dan_6)!=null && active2 && !buff.IsHide && !this.BChar.MyTeam.AliveChars_Vanish.Contains(buff.Usestate_L))
            {
                BattleSystem.DelayInput(this.DefRecordsUpdata(buff.Usestate_L, buff.BuffData.BuffTag.Key));
            }

        }

        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            
            int frame = Time.frameCount;
            
            bool equip = false;
            try
            {
                foreach (IP_Equip_Dandelion ip_patch in this.BChar.IReturn<IP_Equip_Dandelion>())
                {
                    //Debug.Log("point_add");
                    if (equip)
                    {
                        break;
                    }
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        bool flag2 = ip_patch.Equip_Dandelion(this.BChar);
                        equip = equip || flag2;
                    }
                }
            }
            catch
            {
                //Debug.Log("error_Add");
            }

            bool active = (BuffTaker == this.BChar|| (equip && this.BChar.MyTeam.AliveChars_Vanish.Contains(BuffTaker)));//后续可修改条件

            for(int i = 0;i<this.buffRecords.Count;i++)
            {
                if (this.buffRecords[i].frame<frame)
                {
                    this.buffRecords.RemoveAt(i);
                    i--;
                }
            }
            if (addedbuff.BuffData.Debuff && active && !addedbuff.IsHide && !this.BChar.MyTeam.AliveChars_Vanish.Contains(BuffUser))
            {
                //Debug.Log("point_add_2");
                if (this.buffRecords.Find(a => a.bchar == BuffUser && a.buffkey==addedbuff.BuffData.Key && a.frame==frame) != null)
                {
                    BuffAddRecord buffrec = this.buffRecords.Find(a => a.bchar == BuffUser && a.buffkey == addedbuff.BuffData.Key);
                    //Debug.Log("Find1!:"+buffrec.frame);
                    //Debug.Log("Find2!:" + frame);
                    if (/*buffrec.buffkey == addedbuff.BuffData.Key*/buffrec.takers.Contains(BuffTaker)/* && buffrec.frame == frame*/)
                    {
                        goto JP_P_001;
                    }
                    else
                    {
                        //buffrec.buffkey = addedbuff.BuffData.Key;
                        //buffrec.frame = frame;
                        buffrec.takers.Add(BuffTaker); 
                    }
                }
                else
                {
                    
                    BuffAddRecord buffrec = new BuffAddRecord();
                    buffrec.bchar = BuffUser;
                    buffrec.buffkey = addedbuff.BuffData.Key;
                    buffrec.frame = frame;
                    buffrec.takers.Add(BuffTaker);
                    this.buffRecords.Add(buffrec);
                    //Debug.Log(frame);
                }

                BattleSystem.DelayInput(this.DefRecordsUpdata(BuffUser, addedbuff.BuffData.BuffTag.Key));
                /*
                if (this.defRecords.Find(a => a.bchar == BuffUser) == null)
                {
                    defRecord rec = new defRecord { bchar = BuffUser };
                    this.defRecords.Add(rec);
                }
                defRecord rec2 = this.defRecords.Find(a => a.bchar == BuffUser);

                if (addedbuff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT)
                {
                    rec2.dot++;
                    rec2.cc++;
                    rec2.debuff++;
                }
                if (addedbuff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_CrowdControl)
                {
                    rec2.dot++;
                    rec2.cc++;
                    rec2.debuff++;
                }
                if (addedbuff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_Debuff)
                {
                    rec2.dot++;
                    rec2.cc++;
                    rec2.debuff++;
                }

                //Debug.Log("施加者:" + rec2.bchar.Info.Name);
                foreach (defRecord rec in this.defRecords)
                {
                    //Debug.Log("抵抗增加对象:" + rec.bchar.Info.Name + ":" + rec.dot + "/" + rec.cc + "/" + rec.debuff);
                }
                //Debug.Log("---------");
                */
            }
        JP_P_001:;
            if (addedbuff.BuffData.Debuff && BuffUser == this.BChar && !BuffTaker.Info.Ally&& !addedbuff.IsHide && (addedbuff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_CrowdControl || addedbuff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_Debuff))
            {
                if (this.overloadRecords.Find(a => a.bchar == BuffTaker) != null)
                {
                    BuffAddRecord overrec = this.overloadRecords.Find(a => a.bchar == BuffTaker);
                    //Debug.Log("Find1!:"+buffrec.frame);
                    //Debug.Log("Find2!:" + frame);
                    if (overrec.buffkey == addedbuff.BuffData.Key && overrec.frame == frame)
                    {
                        //Debug.Log("point1");
                        return;
                    }
                    else
                    {
                        //Debug.Log("point2");
                        overrec.buffkey = addedbuff.BuffData.Key;
                        overrec.frame = frame;
                    }
                }
                else
                {
                    //Debug.Log("point3");
                    BuffAddRecord overrec = new BuffAddRecord();
                    overrec.bchar = BuffTaker;
                    overrec.buffkey = addedbuff.BuffData.Key;
                    overrec.frame = frame;
                    this.overloadRecords.Add(overrec);
                }
                if (this.overloading.Count <= 0)
                {
                    this.overloading.Add(BuffTaker);
                    BattleSystem.DelayInput(this.CloudOverload(BuffTaker));
                }
                else
                {
                    this.overloading.Add(BuffTaker);
                }
            }
        }
        public IEnumerator AtkRecordsUpdata(BattleChar bcher, string bufftype)
        {
            if (this.atkRecords.Find(a => a.bchar == bcher) == null)
            {
                atkRecord rec = new atkRecord { bchar = bcher };
                this.atkRecords.Add(rec);
            }
            atkRecord rec2 = this.atkRecords.Find(a => a.bchar == bcher);

            if (bufftype == GDEItemKeys.BuffTag_DOT)
            {
                rec2.dot++;
                rec2.cc++;
                rec2.debuff++;
            }
            else if (bufftype == GDEItemKeys.BuffTag_CrowdControl)
            {
                rec2.dot++;
                rec2.cc++;
                rec2.debuff++;
            }
            else if (bufftype == GDEItemKeys.BuffTag_Debuff)
            {
                rec2.dot++;
                rec2.cc++;
                rec2.debuff++;
            }
            else
            {
                rec2.dot++;
                rec2.cc++;
                rec2.debuff++;
            }
            
            if (!this.texting)
            {
                this.texting = true;
                BattleSystem.DelayInputAfter(this.TextOut(bcher));
            }
            
            yield break;
        }
        public IEnumerator DefRecordsUpdata(BattleChar bcher, string bufftype)
        {
            if (this.defRecords.Find(a => a.bchar == bcher) == null)
            {
                defRecord rec = new defRecord { bchar = bcher };
                this.defRecords.Add(rec);
            }
            defRecord rec2 = this.defRecords.Find(a => a.bchar == bcher);

            if (bufftype == GDEItemKeys.BuffTag_DOT)
            {
                rec2.dot++;
                rec2.cc++;
                rec2.debuff++;
            }
            else if (bufftype == GDEItemKeys.BuffTag_CrowdControl)
            {
                rec2.dot++;
                rec2.cc++;
                rec2.debuff++;
            }
            else if (bufftype == GDEItemKeys.BuffTag_Debuff)
            {
                rec2.dot++;
                rec2.cc++;
                rec2.debuff++;
            }
            else
            {
                rec2.dot++;
                rec2.cc++;
                rec2.debuff++;
            }

            if (!this.texting)
            {
                this.texting = true;
                BattleSystem.DelayInputAfter(this.TextOut(bcher));
            }
            
            yield break;
        }
        
        private bool texting = false;
        public IEnumerator TextOut(BattleChar bchar)
        {
            
            
            if(BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Contains(bchar))//|| BattleSystem.instance.AllyTeam.AliveChars_Vanish.Contains(bchar))
            {
                atkRecord rec2 = new atkRecord();
                defRecord rec3 = new defRecord();
                if (this.atkRecords.Find(a => a.bchar == bchar) != null)
                {
                    rec2 = this.atkRecords.Find(a => a.bchar == bchar);
                }
                if (this.defRecords.Find(a => a.bchar == bchar) != null)
                {
                    rec3 = this.defRecords.Find(a => a.bchar == bchar);
                }
                int num = rec2.dot + rec3.dot;

                GDEBuffData gde1 = new GDEBuffData("B_GFL_Dandelion_b_1");
                this.BChar.SimpleTextOut(bchar.Info.Name + ": "+ gde1.Name + num);
                //EffectView.TextOutSimple(bchar, "解析:" + num);
                //EffectView.SimpleTextout(this.BChar.transform, text, 1f, false, 1f);
                //this.BChar.Info.TextOut(bchar.Info.Name + ":" + "解析" + num);

            }


            this.texting = false;
            yield break;
        }
        
        public IEnumerator CloudOverload(BattleChar target)
        {
            yield return new WaitForFixedUpdate();
            for (int i = 0; i < this.overloading.Count; i++)
            {
                if (!this.overloading[i].IsDead)
                {
                    this.overloading[i].BuffAdd("B_GFL_Dandelion_0_3", this.BChar, false, 0, false, -1, false);

                }
            }
            this.overloading.Clear();
            yield break;
        }


        public void InputInfo_Before(BattleChar bc, Skill useskill)
        {
            bool equip = false;

            foreach (IP_Equip_Dandelion ip_patch in this.BChar.IReturn<IP_Equip_Dandelion>())
            {
                //Debug.Log("point_add");
                if (equip)
                {
                    break;
                }
                bool flag = ip_patch != null;
                if (flag)
                {
                    bool flag2 = ip_patch.Equip_Dandelion(this.BChar);
                    equip = equip || flag2;
                }
            }
            bool active = (useskill.Master == this.BChar || (equip && this.BChar.MyTeam.AliveChars_Vanish.Contains(useskill.Master)));
            if (active)
            {
                atkRecord rec2 = new atkRecord();
                defRecord rec3 = new defRecord();
                if (this.atkRecords.Find(a => a.bchar == bc) != null)
                {
                    rec2 = this.atkRecords.Find(a => a.bchar == bc);
                }
                if (this.defRecords.Find(a => a.bchar == bc) != null)
                {
                    rec3 = this.defRecords.Find(a => a.bchar == bc);
                }
                Sex_dan_p_tooltip sex = new Sex_dan_p_tooltip();
                sex.Setup(this.EnforceNum(rec2.cc + rec3.cc), this.EnforceNum(rec2.debuff + rec3.debuff), this.EnforceNum(rec2.dot + rec3.dot));
                useskill.AllExtendeds.Add(sex);
            }
        }


        public void BuffAdd_Before(BattleChar bchar, string key, BattleChar UseState, bool hide, ref int PlusTagPer, bool debuffnonuser, int RemainTime, bool StringHide)
        {
            if (BattleSystem.instance == null)
            {
                return;
            }

            bool equip = false;

                foreach (IP_Equip_Dandelion ip_patch in this.BChar.IReturn<IP_Equip_Dandelion>())
                {
                    //Debug.Log("point_add");
                    if (equip)
                    {
                        break;
                    }
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        bool flag2 = ip_patch.Equip_Dandelion(this.BChar);
                        equip = equip || flag2;
                    }
                }
            bool active1 = (UseState == this.BChar || (equip && this.BChar.MyTeam.AliveChars_Vanish.Contains(UseState)));

            bool active2 = (bchar == this.BChar || (equip && this.BChar.MyTeam.AliveChars_Vanish.Contains(bchar)));

            if (active1 && (this.atkRecords.Find(a => a.bchar == bchar) != null || this.defRecords.Find(a => a.bchar == bchar) != null))
            {
                GDEBuffData gdebuffdata = new GDEBuffData(key);
                if (gdebuffdata.Debuff && !hide && !gdebuffdata.Hide)
                {
                    atkRecord rec2 = new atkRecord();
                    defRecord rec3 = new defRecord();
                    if (this.atkRecords.Find(a => a.bchar == bchar) != null)
                    {
                        rec2 = this.atkRecords.Find(a => a.bchar == bchar);
                    }
                    if (this.defRecords.Find(a => a.bchar == bchar) != null)
                    {
                        rec3 = this.defRecords.Find(a => a.bchar == bchar);
                    }


                    if (gdebuffdata.BuffTag.Key == GDEItemKeys.BuffTag_DOT)
                    {
                        PlusTagPer += (this.EnforceNum(rec2.dot + rec3.dot));
                    }
                    if (gdebuffdata.BuffTag.Key == GDEItemKeys.BuffTag_CrowdControl)
                    {
                        PlusTagPer += (this.EnforceNum(rec2.cc + rec3.cc));
                    }
                    if (gdebuffdata.BuffTag.Key == GDEItemKeys.BuffTag_Debuff)
                    {
                        PlusTagPer += (this.EnforceNum(rec2.debuff + rec3.debuff));
                    }
                }
            }

            if (active2 && (this.atkRecords.Find(a => a.bchar == UseState) != null || this.defRecords.Find(a => a.bchar == UseState) != null))
            {
                GDEBuffData gdebuffdata = new GDEBuffData(key);
                if (gdebuffdata.Debuff && !hide && !gdebuffdata.Hide)
                {
                    atkRecord rec2 = new atkRecord();
                    defRecord rec3 = new defRecord();
                    if (this.atkRecords.Find(a => a.bchar == UseState) != null)
                    {
                        rec2 = this.atkRecords.Find(a => a.bchar == UseState);
                    }
                    if (this.defRecords.Find(a => a.bchar == UseState) != null)
                    {
                        rec3 = this.defRecords.Find(a => a.bchar == UseState);
                    }

                    if (gdebuffdata.BuffTag.Key == GDEItemKeys.BuffTag_DOT)
                    {
                        PlusTagPer -= (this.EnforceNum(rec2.dot + rec3.dot));
                    }
                    if (gdebuffdata.BuffTag.Key == GDEItemKeys.BuffTag_CrowdControl)
                    {
                        PlusTagPer -= (this.EnforceNum(rec2.cc + rec3.cc));
                    }
                    if (gdebuffdata.BuffTag.Key == GDEItemKeys.BuffTag_Debuff)
                    {
                        PlusTagPer -= (this.EnforceNum(rec2.debuff + rec3.debuff));
                    }
                }
            }
        }

        public int EnforceNum(int num)
        {
            bool flag1 = false;
            foreach (IP_EnforcePassive ip_patch2 in BattleSystem.instance.IReturn<IP_EnforcePassive>())
            {
                bool flag2 = ip_patch2 != null;
                if (flag2)
                {
                    flag1=flag1||ip_patch2.EnforcePassive(this.BChar);
                }
            }
            if(flag1)
            {
                int result = 0;
                for (int i = 0; i < num && result<1050000000; i++)
                {
                    result += (int)(Math.Pow(2, i) * this.pluspertime);
                }
                return result;
            }
            else
            {
                return this.pluspertime * num;
            }
        }
        public bool IsEnforce()
        {
            bool flag1 = false;
            foreach (IP_EnforcePassive ip_patch2 in BattleSystem.instance.IReturn<IP_EnforcePassive>())
            {
                bool flag2 = ip_patch2 != null;
                if (flag2)
                {
                    flag1 = flag1 || ip_patch2.EnforcePassive(this.BChar);
                }
            }
            return flag1;
        }

        private List<atkRecord> atkRecords = new List<atkRecord>();
        private List<defRecord> defRecords = new List<defRecord>();

        private List<BuffAddRecord> buffRecords = new List<BuffAddRecord>();


        private List<BattleChar> overloading = new List<BattleChar>();
        private List<BuffAddRecord> overloadRecords = new List<BuffAddRecord>();


        public int pluspertime = 20;

    }
    public class atkRecord
    {
        public BattleChar bchar = new BattleChar();
        public int dot = 0;
        public int cc = 0;
        public int debuff = 0;

    }
    public class defRecord
    {
        public BattleChar bchar = new BattleChar();
        public int dot = 0;
        public int cc = 0;
        public int debuff = 0;

    }
    public class BuffAddRecord
    {
        public BattleChar bchar = new BattleChar();
        public int frame = 0;
        public string buffkey = string.Empty;
        public List<BattleChar> takers = new List<BattleChar>();
    }

    public class Sex_dan_p_tooltip : Sex_ImproveClone, IP_InputInfo_After
    {
        public void Setup(float num_cc, float num_debuff, float num_dot)
        {
            this.PlusSkillStat.HIT_CC = num_cc;
            this.PlusSkillStat.HIT_DEBUFF = num_debuff;
            this.PlusSkillStat.HIT_DOT = num_dot;
        }
        public void InputInfo_After(BattleChar bc, Skill useskill)
        {
            if (useskill == this.MySkill || useskill.AllExtendeds.Contains(this))
            {
                useskill.AllExtendeds.RemoveAll(a => a == this);
            }
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            //Debug.Log("Sex_dan_p_tooltip:error2");
        }
    }
    public class Bcl_dan_0_1 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.dod -= 4;
            this.PlusStat.hit -= 4;
        }
    }

    public class Bcl_dan_0_2 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusPerStat.Damage += 3;
            this.PlusStat.hit += 3;
            this.PlusHealTick = 3;
        }
    }
    public class Bcl_dan_0_3 : Buff
    {

    }


    public class Sex_dan_2 : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars_Vanish)
            {
                bc.BuffAdd("B_GFL_Dandelion_0_2", this.BChar);
            }
            foreach (BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {
                bc.BuffAdd("B_GFL_Dandelion_0_1", this.BChar);
            }

            //this.BChar.MyTeam.Add(Skill.TempSkill("S_GFL_Dandelion_2_1", this.BChar, this.BChar.MyTeam), true);
            //this.BChar.MyTeam.Add(Skill.TempSkill("S_GFL_Dandelion_2_1", this.BChar, this.BChar.MyTeam), true);

        }
    }
    public class Bcl_dan_2_1 : Buff,/*IP_BuffOnPointerEnter,IP_BuffOnPointerExit*/ IP_PointerEnter_Patch, IP_PointerExit_After
    {
        // Token: 0x060013C0 RID: 5056 RVA: 0x00099C94 File Offset: 0x00097E94
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (this.tick < 20 && this.tick >= 0)
            {
                tick++;
            }
            else
            {
                tick = 0;

                foreach (BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
                {
                    if (!bc.BuffFind("B_GFL_Dandelion_2_2", false))
                    {
                        bc.BuffAdd("B_GFL_Dandelion_2_2", this.BChar);
                    }
                }
            }
        }
        public override void BuffOneAwake()
        {
            this.BChar.MyTeam.Add(Skill.TempSkill("S_GFL_Dandelion_2_1", this.BChar, this.BChar.MyTeam), true);
        }
        public override void TurnUpdate()
        {
            base.TurnUpdate();
            if (this.StackInfo.Count > 0)
            {
                this.BChar.MyTeam.Add(Skill.TempSkill("S_GFL_Dandelion_2_1", this.BChar, this.BChar.MyTeam), true);

            }
        }

        private int tick = 0;
        public void PointerEnter_Patch(BattleEnemy enemy)
        //public void BuffOnPointerEnter(Buff buff)
        {
            //BattleEnemy enemy = buff.BChar as BattleEnemy;
            bool flag1 = !BattleSystem.instance.TargetSelecting && !BattleSystem.instance.WasteMode /*&& enemy.BuffFind("B_GFL_Dandelion_2_2", false)*/ && enemy.SkillQueue.Count != 0;//&& !B_Control_P.UnknownTarget(enemy.SkillQueue[0].skill.MySkill) ;
            if (flag1 && enemy.Info.Ally!=this.BChar.Info.Ally)
            {
                bool flag2 = !enemy.SkillQueue[0].skill.MySkill.EnemyPreview;//|| B_Control_P.UnknownTarget(enemy.SkillQueue[0].skill.MySkill);// || (enemy.SkillQueue[0].skill.AllExtendeds.Find(x => x.EnemyPreviewNoArrow == true) != null);
                if(flag2)
                {
                    BattleSystem.DelayInput(this.DandelionTargetView(enemy, enemy.SkillQueue[0]));
                }
                //this.DandelionTargetView_test(enemy, enemy.SkillQueue[0]);


                //BattleChar.SkillNameOutOrigin(this.Usestate_L.BattleInfo, enemy.SkillQueue[0].skill.MySkill.Name, false);


                //enemy.Info.TextOut(enemy.SkillQueue[0].skill.MySkill.Name);
                //EffectView.SimpleTextout(enemy.GetTopPos() /*+ new Vector3(0.5f, 0f)*/, enemy.SkillQueue[0].skill.MySkill.Name, false, 0.6f, false, 0f);
            }
        }
        private List<GameObject> objectList = new List<GameObject>();

        public void PointerExit_After(BattleEnemy enemy)
        //public void BuffOnPointerExit(Buff buff)
        {
            foreach (GameObject obj in this.objectList)
            {
                UnityEngine.Object.Destroy(obj);
            }
            this.objectList.Clear();
        }
        public void DandelionTargetView_test(BattleEnemy enemy, CastingSkill Cast)
        {
            BattleSystem.instance.CastTargetView(Cast, false);

            GameObject gameObject = Misc.UIInst(this.BChar.BattleInfo.SkillName);
            gameObject.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = Cast.skill.MySkill.Name;
            this.objectList.Add(gameObject);

            //enemy.BattleInfo.ActWindow.Enemystatv.ViewUpdate(enemy);
        }
        public IEnumerator DandelionTargetView(BattleEnemy enemy, CastingSkill Cast)
        {
            //yield return new WaitForSeconds(0.5f);

            BattleSystem.instance.CastTargetView(Cast, false);
            
            GameObject gameObject = Misc.UIInst(this.BChar.BattleInfo.SkillName);
            gameObject.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = Cast.skill.MySkill.Name;
            this.objectList.Add(gameObject);

            //enemy.BattleInfo.ActWindow.Enemystatv.ViewUpdate(enemy);


            //enemy.BattleInfo.ActWindow.Enemystatv.gameObject.SetActive(true);

        /*
        if (Cast.skill.MySkill.Target.Key == GDEItemKeys.s_targettype_all_enemy || Cast.skill.MySkill.Target.Key == GDEItemKeys.s_targettype_random_enemy)
        {
            using (List<BattleChar>.Enumerator enumerator = enemy.BattleInfo.AllyTeam.AliveChars.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    BattleChar target = enumerator.Current;
                    Bcl_dan_2_1.DandelionOneTarget(target);
                }
                goto IL_bcl_2_1;
            }
        }
        if (Cast.skill.MySkill.Target.Key == GDEItemKeys.s_targettype_all_ally)
        {
            using (List<BattleChar>.Enumerator enumerator = enemy.BattleInfo.EnemyTeam.AliveChars.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    BattleChar target2 = enumerator.Current;
                    Bcl_dan_2_1.DandelionOneTarget(target2);
                }
                goto IL_bcl_2_1;
            }
        }
        if (Cast.skill.MySkill.Target.Key == GDEItemKeys.s_targettype_all)
        {
            foreach (BattleChar target3 in enemy.BattleInfo.EnemyTeam.AliveChars)
            {
                Bcl_dan_2_1.DandelionOneTarget(target3);
            }
            using (List<BattleChar>.Enumerator enumerator = enemy.BattleInfo.AllyTeam.AliveChars.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    BattleChar target4 = enumerator.Current;
                    Bcl_dan_2_1.DandelionOneTarget(target4);
                }
                goto IL_bcl_2_1;
            }
        }
        if (Cast.TargetPlus != null)
        {
            using (List<BattleChar>.Enumerator enumerator = new List<BattleChar>
        {
            Cast.Target,
            Cast.TargetPlus
        }.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    BattleChar target5 = enumerator.Current;
                    Bcl_dan_2_1.DandelionOneTarget(target5);
                }
                goto IL_bcl_2_1;
            }
        }
        if (Cast.Target != null)
        {
            Bcl_dan_2_1.DandelionOneTarget(Cast.Target);
        }
        */
        IL_bcl_2_1:;
            
            yield break;
        }

        public static void DandelionOneTarget(BattleChar Target)
        {

            BattleSystem.DelayInput(Bcl_dan_2_1.OneTaregt2(Target));
        }

        public static IEnumerator OneTaregt2(BattleChar Target)
        {
            //TargetSelects.Clear(true);

            TargetSelects.OneSelect = UnityEngine.Object.Instantiate<GameObject>(TargetSelects.TargetSelect).GetComponent<TargetSelect>();
            if (Target.Info.Ally)
            {
                TargetSelects.OneSelect.gameObject.transform.SetParent(TargetSelects.ThisObject.transform);
            }
            else
            {
                TargetSelects.OneSelect.gameObject.transform.SetParent(TargetSelects.TargetSelect3D.transform);
            }
            Misc.UIInit(TargetSelects.OneSelect.gameObject);
            TargetSelects.OneSelect.Init(Target.GetTopPos(), Target.Info.Ally, Target);
            //yield break;

            /*
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(TargetSelects.TargetSelect);
            if (Target.Info.Ally)
            {
                gameObject.transform.SetParent(TargetSelects.ThisObject.transform);
            }
            else
            {
                gameObject.transform.SetParent(TargetSelects.TargetSelect3D.transform);
            }
            Misc.UIInit(TargetSelects.OneSelect.gameObject);
            gameObject.transform.position = Target.GetTopPos();
            Bcl_dan_2_1.DandelionTargets.Add(gameObject);
            */
            /*
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(TargetSelects.ControlTarget);
            if (Target.Info.Ally)
            {
                gameObject.transform.SetParent(TargetSelects.ThisObject.transform);
            }
            else
            {
                gameObject.transform.SetParent(TargetSelects.TargetSelect3D.transform);
            }
            Misc.UIInit(gameObject);
            gameObject.transform.position = Target.GetTopPos();
            */
            yield break;
        }
        public static void DandelionTargetClear()
        {
            for (int i = 0; i < Bcl_dan_2_1.DandelionTargets.Count; i++)
            {
                UnityEngine.Object.Destroy(Bcl_dan_2_1.DandelionTargets[i]);
                Bcl_dan_2_1.DandelionTargets[i] = null;
                Bcl_dan_2_1.DandelionTargets.RemoveAt(i);
                i--;
            }
        }

        public static List<GameObject> DandelionTargets = new List<GameObject>();

    }

    public class Bcl_dan_2_2 : Buff
    {

    }
    public class Bcl_dan_2_3 : Buff, IP_Hit
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.DMGTaken = -50;
            this.PlusStat.Strength = true;
            //this.BarrierHP += 5;
        }
        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            if (SP.UseStatus.Info.Ally != this.BChar.Info.Ally)
            {
                base.SelfStackDestroy();
            }
        }
    }

    public class Sex_dan_3 : Sex_ImproveClone
    {


    }


    public class Bcl_dan_3 : Buff, IP_BuffAddAfter, IP_SkillUse_User, IP_PlayerTurn_EnemyAfter, IP_ParticleOut_Before
    {

        public override void Init()
        {
            base.Init();
            //this.OnePassive = true;
        }
        /*
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            this.tauntTar = this.Usestate_L;
        }
        */
        /*
        public void Awake()
        {
            Buff.TauntSkill(this.BChar, this.BChar, true);
        }
        */
        public override string DescExtended()
        {
            if (this.tauntTar != null)
            {
                return this.BuffData.Description.Replace("&target", this.tauntTar.Info.Name);

            }
            else
            {
                GDESkillKeywordData gde = new GDESkillKeywordData("KeyWord_SF_Dandelion_3");
                return this.BuffData.Description.Replace("&target", gde.Name);
            }
        }

        public override void FixedUpdate()
        {
            //base.FixedUpdate();
            
            if (!BattleSystem.instance.TargetSelecting && !this.needselect && ((this.tauntTar != null && this.tauntTar.IsDead)||(this.tauntTar==null)))
            {
                base.SelfStackDestroy();
            }   
            
            /*
            if(this.needselect && !this.Usestate_L.BattleInfo.TargetSelecting)
            {
                Skill skill = Skill.TempSkill("S_GFL_Dandelion_3_2", this.Usestate_L);

                Sex_dan_3_1 sex = skill.AllExtendeds.Find(a => a is Sex_dan_3_1) as Sex_dan_3_1;
                sex.mainbuff = this;
                this.Usestate_L.BattleInfo.TargetSelect(skill, this.Usestate_L);
            }*/
        }
        public IEnumerator ForceSelect()
        {
            int n = 10;
            while (this.needselect)
            {
                if (this.needselect && !this.Usestate_L.BattleInfo.TargetSelecting)
                {
                    //BattleChar.SkillNameOutOrigin(this.Usestate_L.BattleInfo, "选择强制攻击对象", true);
                    Skill skill = Skill.TempSkill("S_GFL_Dandelion_3_1", this.Usestate_L);
                    skill.FreeUse = true;
                    Sex_dan_3_1 sex = skill.AllExtendeds.Find(a => a is Sex_dan_3_1) as Sex_dan_3_1;
                    sex.mainbuff = this;
                    this.Usestate_L.BattleInfo.TargetSelect(skill, this.Usestate_L);
                }
                n++;
                if(n>9)
                {
                    GDESkillKeywordData gde = new GDESkillKeywordData("KeyWord_SF_Dandelion_3");

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

            yield break;
        }
        public List<GameObject> objectList = new List<GameObject>();
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (Targets.Find(a => a.Info.Ally != this.BChar.Info.Ally))
            {
                Targets.Clear();
                Targets.Add(this.tauntTar);
            }
            //base.SkillUse(SkillD, Targets);

            if (SkillD.IsDamage && Targets.Find((BattleChar a) => a == this.tauntTar) != null)
            {
                BattleSystem.DelayInputAfter(this.SkillUseAfter());
            }

        }
        public void ParticleOut_Before(Skill SkillD, List<BattleChar> Targets)
        {
            {
                if (Targets.Find(a => a.Info.Ally != this.BChar.Info.Ally))
                {
                    Targets.Clear();
                    Targets.Add(this.tauntTar);
                }
            }
        }
        // Token: 0x06000F2F RID: 3887 RVA: 0x00089A9D File Offset: 0x00087C9D
        public IEnumerator SkillUseAfter()
        {
            base.SelfStackDestroy();
            yield return null;
            yield break;
        }


        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (addedbuff == this)
            {
                /*
                Skill skill = Skill.TempSkill("S_GFL_Dandelion_3_1", this.Usestate_L);
                

                Sex_dan_3_1 sex =skill.AllExtendeds.Find(a=>a is Sex_dan_3_1) as Sex_dan_3_1 ;
                sex.mainbuff = this;
                */
                //skill.ExtendedAdd(sex);
                this.needselect = true;
                BattleSystem.DelayInput(this.ForceSelect());
                //BattleChar.SkillNameOutOrigin(this.Usestate_L.BattleInfo, "选择强制攻击对象", true);

                //this.Usestate_L.BattleInfo.TargetSelect( skill,this.Usestate_L);

            }
        }
        public override void TurnUpdate()
        {
            base.TurnUpdate();
            if (this.BChar is BattleEnemy)
            {
                this.SetViewTarget((this.BChar as BattleEnemy).SkillQueue.Count );
            }

        }
        public void PlayerTurn_EnemyAfter()
        {
            if (this.BChar is BattleEnemy)
            {
                this.SetViewTarget((this.BChar as BattleEnemy).SkillQueue.Count + 1);
            }

        }
        public async void SetViewTarget(int num)
        {
            //while(!(!BattleSystem.instance.DelayWait && BattleSystem.instance.Particles.Count == 0 && BattleSystem.instance.ActWindow.On))
            if(this.BChar is BattleEnemy)
            {
                try
                {
                    while ((this.BChar as BattleEnemy).SkillQueue.Count >= num || (this.BChar as BattleEnemy).SkillQueue.Count <= 0)
                    {
                        await Task.Delay(50);
                    }
                }
                catch
                {
                    Debug.Log("错误SetViewTarget_Point_1");
                }

                try
                {
                    if (this.StackNum > 0 && (this.BChar as BattleEnemy).SkillQueue.Count > 0 && (this.BChar as BattleEnemy).SkillQueue[0].Target.Info.Ally != this.BChar.Info.Ally)
                    {
                        (this.BChar as BattleEnemy).SkillQueue[0].Target = this.tauntTar;
                        (this.BChar as BattleEnemy).SkillQueue[0].TargetPlus = null;
                    }
                }
                catch
                {
                    Debug.Log("错误SetViewTarget_Point_2");
                }
            }


        }

        public void SetTarget(BattleChar target)
        {
            //Debug.Log("SetTarget");

            //yield return new WaitForSeconds(3.1f);
            //this.tauntTar.Clear();
            this.needselect = false;

            if (this.BChar.Info.Ally==target.Info.Ally)
            {
                this.Usestate_L.Overload++;
                BattleSystem.instance.AllyTeam.AP--;
            }

            this.tauntTar = target;
            if(this.BChar is BattleEnemy)
            {
                this.SetViewTarget((this.BChar as BattleEnemy).SkillQueue.Count + 1);

            }

            //Buff.TauntSkill(this.BChar, this.BChar, true);
            /*
            foreach(StackBuff sta in this.StackInfo)
            {
                sta.UseState = target;
            }
            */
            //base.Awake();
            /*
            foreach(CastingSkill cast in (this.BChar as BattleEnemy).SkillQueue)
            {
                if(cast.Target!=target && cast.TargetPlus!=target)
                {
                    if(cast.TargetPlus!=null)
                    {
                        cast.TargetPlus = cast.Target;
                    }
                    cast.Target = target;
                }
            }
            */
            //Debug.Log("SetTarget:" + this.tauntTar.Info.Name);
        }
        public BattleChar tauntTar = new BattleChar();
        public bool needselect = false;
        public bool needsettarget = false;
    }
    public class Sex_dan_3_1 : Sex_ImproveClone, IP_ParticleOut_Before
    {
        public override bool TargetSelectExcept(BattleChar ExceptTarget)
        {

            return /*ExceptTarget == this.mainbuff.BChar ||*/ (BattleSystem.instance.AllyTeam.AP < 1 && this.mainbuff.BChar.Info.Ally==ExceptTarget.Info.Ally);
        }
        public void ParticleOut_Before(Skill SkillD, List<BattleChar> Targets)
        {
            //base.SkillUseSingle(SkillD, Targets);
            if (SkillD == this.MySkill)
            {
                //Debug.Log("SkillUseSingle_1");


                this.mainbuff.SetTarget(Targets[0]);
            }

        }

        public Bcl_dan_3 mainbuff = new Bcl_dan_3();
    }

    public class Sex_dan_4 : Sex_ImproveClone, IP_BuffAdd_After2,IP_BuffAdd_Before//IP_BuffAddAfter
    {
        public override void Init()
        {
            base.Init();
            //this.OnePassive = true;
        }
        /*public override bool Terms()
        {
            
            if (this.MySkill.BasicSkill && (this.BChar as BattleAlly).MyBasicSkill.InActive)
            {
                Sex_dan_4 sex4 = (this.BChar as BattleAlly).BasicSkill.AllExtendeds.Find(a => a is Sex_dan_4) as Sex_dan_4; ;

                    sex4.debufflist.Clear();
                    sex4.bufflist.Clear();
                

            }
            if (this.MySkill.BasicSkill && !(this.BChar as BattleAlly).MyBasicSkill.InActive)
            {
                Sex_dan_4 sex4 = (this.BChar as BattleAlly).BasicSkill.AllExtendeds.Find(a => a is Sex_dan_4) as Sex_dan_4; ;
                if (sex4.bufflist.Count > 0 || sex4.debufflist.Count > 0)
                {
                    foreach(Sex4_BuffRecord rec in sex4.bufflist)
                    {
                        if(this.bufflist.Find(a=>a.key == rec.key)!=null)
                        {
                            this.bufflist.Find(a => a.key == rec.key).stacks += rec.stacks;
                        }
                        else
                        {
                            this.bufflist.Add(rec);
                        }
                    }
                    foreach (Sex4_BuffRecord rec in sex4.debufflist)
                    {
                        if (this.debufflist.Find(a => a.key == rec.key) != null)
                        {
                            this.debufflist.Find(a => a.key == rec.key).stacks += rec.stacks;
                        }
                        else
                        {
                            this.debufflist.Add(rec);
                        }
                    }
                    sex4.debufflist.Clear();
                    sex4.bufflist.Clear();
                }

            }
            
            if (this.bufflist.Count > 0 || this.debufflist.Count > 0)
            {
                return base.Terms();
            }

            return false;

        }
        */
        public override string DescExtended(string desc)
        {
            //-----固定技能是同步给按钮技能
            /*
            if (this.MySkill.BasicSkill && !(this.BChar as BattleAlly).MyBasicSkill.InActive)
            {
                Sex_dan_4 sex4 = (this.BChar as BattleAlly).BasicSkill.AllExtendeds.Find(a => a is Sex_dan_4) as Sex_dan_4; ;
                foreach (Sex4_BuffRecord rec in sex4.bufflist)
                {
                    if (this.bufflist.Find(a => a.key == rec.key) != null)
                    {
                        this.bufflist.Find(a => a.key == rec.key).stacks += rec.stacks;
                    }
                    else
                    {
                        this.bufflist.Add(rec);
                    }
                }
                foreach (Sex4_BuffRecord rec in sex4.debufflist)
                {
                    if (this.debufflist.Find(a => a.key == rec.key) != null)
                    {
                        this.debufflist.Find(a => a.key == rec.key).stacks += rec.stacks;
                    }
                    else
                    {
                        this.debufflist.Add(rec);
                    }
                }
                sex4.debufflist.Clear();
                sex4.bufflist.Clear();
            }*/
            //-----
            string str1 = string.Empty;
            for(int i=0;i<this.bufflist.Count;i++)
            {
                if(i>0)
                {
                    str1 = str1 + "/";
                }
                GDEBuffData gde = new GDEBuffData(this.bufflist[i].key);
                str1 = str1 + gde.Name + "(" + this.bufflist[i].stacks.ToString() +")" ;
            }
            string str2 = string.Empty;
            for (int i = 0; i < this.debufflist.Count; i++)
            {
                if (i > 0)
                {
                    str2 = str2 + "/";
                }
                GDEBuffData gde = new GDEBuffData(this.debufflist[i].key);
                str2 = str2 + gde.Name + "(" + this.debufflist[i].stacks.ToString() + ")";
            }
            return base.DescExtended(desc).Replace("&a", str1).Replace("&b", str2);

        }
        public override void FixedUpdate()//检查多余EX
        {
            bool basic = true;
            try
            {
                basic = this.MySkill.BasicSkill;
            }
            catch { }
            if (!basic)
            {
                List<Skill_Extended> listex = this.MySkill.AllExtendeds;
                bool delete = false;
                for (int i = 0; i < this.MySkill.AllExtendeds.Count; i++)
                {
                    if (this.MySkill.AllExtendeds[i] is EX_dan_4)
                    {
                        if (delete)
                        {
                            this.MySkill.AllExtendeds.RemoveAt(i);
                            i--;
                        }
                        else
                        {
                            delete = true;
                        }
                    }
                }
                if (!delete)
                {
                    this.MySkill.ExtendedAdd(Skill_Extended.DataToExtended("EX_GFL_Dandelion_4"));
                }
            }



            if (this.battlestaring)
            {
                if(!BattleSystem.instance.DelayWait)
                {
                    this.battlestaring = false;
                }
            }/*
            else
            {

                if (this.MySkill.BasicSkill && !(this.BChar as BattleAlly).MyBasicSkill.InActive)
                {

                    if (!this.BChar.BuffFind("B_GFL_Dandelion_4", false) )
                    {
                        //Debug.Log("Fixed_1_1");
                        Bcl_dan_4 bcl = this.BChar.BuffAdd("B_GFL_Dandelion_4", this.BChar) as Bcl_dan_4;
                        bcl.mainsex = this;
                    }
                }
            }*/

        }
        
        public bool battlestaring = true;

        private bool newbuff = false;
        public void BuffAdd_Before(BattleChar bchar, string key, BattleChar UseState, bool hide, ref int PlusTagPer, bool debuffnonuser, int RemainTime, bool StringHide)
        {
            this.newbuff = true;
        }
        public void BuffAdd_After2(BattleChar BuffTaker, string key, BattleChar BuffUser, bool hide, int PlusTagPer, bool debuffnonuser, int RemainTime, bool StringHide, Buff addedbuff)
        {
            bool basic = false;
            try
            {
                basic = this.MySkill.BasicSkill && this.MySkill.BasicSkillButton.InActive;
            }
            catch { }
            if (!this.newbuff || basic)
            {
                return;
            }
            else
            {
                this.newbuff = false;
            }
            //Debug.Log("BuffAdd_After2_1");
            GDEBuffData gde = new GDEBuffData(key);
            Buff buff = new Buff();
            if (addedbuff != null)
            {
                buff = addedbuff;
            }
            else
            {
                buff = Buff.DataToBuff(gde, BuffTaker, BuffUser, RemainTime, false, 0);
            }

            bool debuffcheck = gde.Debuff && (BattleSystem.instance.AllyTeam.AliveChars_Vanish.Contains(BuffTaker) || BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Contains(BuffTaker));
            if (this.battlestaring ||  key == "B_GFL_Dandelion_4" || gde.Hide || buff.IsHide || hide || key == GDEItemKeys.Buff_B_Neardeath || (gde.Debuff && gde.Cantdisable && BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Find(a => a == BuffUser) != null))//(addedbuff.BuffData.Debuff && addedbuff.BuffData.BuffTag.Key==null))
            {

            }
            else if (BuffUser.Info.Ally == this.BChar.Info.Ally || BuffTaker.Info.Ally == this.BChar.Info.Ally || debuffcheck)//(BattleSystem.instance.AllyTeam.AliveChars_Vanish.Find(a=>a==BuffTaker)!=null || BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Find(a => a == BuffTaker) != null)
            {
                //Debug.Log("BuffAdd_After2_2");
                if (gde.Debuff)
                {
                    if (this.debufflist.Find(a => a.key == key) != null)
                    {
                        this.debufflist.Find(a => a.key == key).stacks++;
                        //Debug.Log("debuff_add");
                        if (addedbuff != null)
                        {
                            BattleSystem.DelayInput(this.SpecialShieldRecord(this.debufflist.Find(a => a.key == key), addedbuff, addedbuff.BarrierHP));

                        }
                    }
                    else
                    {
                        Sex4_BuffRecord rec = new Sex4_BuffRecord();
                        rec.key = key;
                        rec.stacks++;
                        this.debufflist.Add(rec);
                        //Debug.Log("debuff_add");
                        if (addedbuff != null)
                        {
                            BattleSystem.DelayInput(this.SpecialShieldRecord(rec, addedbuff, addedbuff.BarrierHP));
                        }
                    }
                }
                else
                {
                    if (this.bufflist.Find(a => a.key == key) != null)
                    {
                        this.bufflist.Find(a => a.key == key).stacks++;
                        //Debug.Log("debuff_add");
                        if (addedbuff != null)
                        {
                            BattleSystem.DelayInput(this.SpecialShieldRecord(this.bufflist.Find(a => a.key == key), addedbuff, addedbuff.BarrierHP));
                        }
                    }
                    else
                    {
                        Sex4_BuffRecord rec = new Sex4_BuffRecord();
                        rec.key = key;
                        rec.stacks++;
                        this.bufflist.Add(rec);
                        //Debug.Log("debuff_add");
                        if (addedbuff != null)
                        {
                            BattleSystem.DelayInput(this.SpecialShieldRecord(rec, addedbuff, addedbuff.BarrierHP));
                        }

                    }
                }
            }
        }

        
        public IEnumerator SpecialShieldRecord(Sex4_BuffRecord record, Buff buff, int shieldOld)
        {
            //Debug.Log("SpecialShieldRecord_1:"+ shieldOld+"/"+ buff.BarrierHP);
            yield return new WaitForFixedUpdate();
            if (buff.BarrierHP > shieldOld)
            {
                //Debug.Log("SpecialShieldRecord_2");
                record.plusBarrier += Math.Max(0, buff.BarrierHP - shieldOld);
            }
            /*
            if((this.BChar as BattleAlly).BasicSkill.AllExtendeds.Contains(this))
            {
                Sex_dan_4 sex = (this.BChar as BattleAlly).MyBasicSkill.buttonData.AllExtendeds.Find(a => a is Sex_dan_4) as Sex_dan_4;
                sex.bufflist.Clear();
                sex.debufflist.Clear();
                sex.bufflist.AddRange(this.bufflist);
                sex.debufflist.AddRange(this.bufflist);
            }
            */
            yield break;
        }

        /*
        public override void SkillTargetSingle(List<Skill> Targets)
        {
            this.skilltargets.Clear();
            this.skilltargets.AddRange(Targets);

            this.Del_0(this.MySkill.BasicSkill);
        }
        */
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            /*List<Skill> SkillList = new List<Skill>();

                if (Misc.NullCheck((Targets[0] as BattleAlly).Info.GetData.FixedBasicSkill))
                {
                    SkillList.Add((Targets[0] as BattleAlly).BasicSkill);

                }

            SkillList.AddRange((Targets[0] as BattleAlly).Skills);
            List<Skill> list2 = new List<Skill>();
            foreach (Skill skill in SkillList)
            {
                if(!skill.MySkill.Rare && skill.MySkill.KeyID!=this.MySkill.MySkill.KeyID)
                {
                    Skill skill2 = skill.CloneSkill(true, this.BChar, null, true);
                    skill2.isExcept = true;
                    list2.Add(skill2);
                }
            }

            BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(list2, new SkillButton.SkillClickDel(this.Del), ScriptLocalization.System_SkillSelect.CreateSkill, false, true, true, false, true));
            */
            this.targets = Targets;
            this.Del_0(this.MySkill.BasicSkill,false);

        }
        public List<Skill> skilltargets = new List<Skill>();
        public List<BattleChar> targets= new List<BattleChar>();

        public void Del(SkillButton Mybutton)
        {
            Skill skill = Mybutton.Myskill.CloneSkill(true, this.BChar, null, true);
            skill.isExcept = true;
            skill.Master = this.BChar;

            this.skilltargets.Add(skill);
            this.BChar.MyTeam.Add(skill, true);
            //Debug.Log("添加复制技能");
            this.Del_0(this.MySkill.BasicSkill,false);
        }
        public void Del_0(bool isbasic,bool view)
        {
            this.isbasic = isbasic;
            this.isview = view;
            /*//mark1023
            if(isbasic)
            {
                Sex_dan_4 sex4 = (this.BChar as BattleAlly).BasicSkill.AllExtendeds.Find(a => a is Sex_dan_4) as Sex_dan_4; ;
                this.bufflist.AddRange(sex4.bufflist);
                this.debufflist.AddRange(sex4.debufflist);
                sex4.debufflist.Clear();
                sex4.bufflist.Clear();
            }
            */
            /*//排序，未启用
            this.bufflist = new List<Sex4_BuffRecord>();
            this.debufflist = new List<Sex4_BuffRecord>();
            List<Sex4_BuffRecord> debufflist_0 = new List<Sex4_BuffRecord>();


            List<Skill_Extended> list1 = new List<Skill_Extended>();
            list1.AddRange(this.MySkill.AllExtendeds.FindAll(a => a is Sex_dan_4));
            foreach (Skill_Extended sex in list1)
            {
                this.bufflist.AddRange((sex as Sex_dan_4).bufflist);
                debufflist_0.AddRange((sex as Sex_dan_4).debufflist);
            }
            this.debufflist.AddRange(debufflist_0.FindAll(a => (new GDEBuffData(a.key)).BuffTag.OriginalKey == GDEItemKeys.BuffTag_DOT));
            this.debufflist.AddRange(debufflist_0.FindAll(a => (new GDEBuffData(a.key)).BuffTag.OriginalKey == GDEItemKeys.BuffTag_CrowdControl));
            this.debufflist.AddRange(debufflist_0.FindAll(a => (new GDEBuffData(a.key)).BuffTag.OriginalKey == GDEItemKeys.BuffTag_Debuff));
            */

            List<Skill> lists = new List<Skill>();

            //取消动作与清除buff
            //if (this.isview)
            
                GDESkillData skilldatac = new GDESkillData("S_GFL_Dandelion_4_c");
                Skill skillc = new Skill();
                skillc.Init(skilldatac);
                Sex_dan_4_c exc = new Sex_dan_4_c();
                skillc.ExtendedAdd(exc);
                lists.Add(skillc);
            
            /*
            GDESkillData skilldata0 = new GDESkillData("S_GFL_Dandelion_4_0");
            Skill skill0 = new Skill();
            skill0.Init(skilldata0);
            lists.Add(skill0);
            */

            GDESkillData skilldata1 = (new GDESkillData("S_GFL_Dandelion_4_1")).ShallowClone();
            bool hasskill1 = false;
            foreach (Sex4_BuffRecord rec in this.bufflist)
            {
                skilldata1.Description = skilldata1.Description + (new GDEBuffData(rec.key)).Name;
                skilldata1.Description = skilldata1.Description + "(" + rec.stacks + ")";
                skilldata1.Description = skilldata1.Description + "\n";
                hasskill1 = true;
            }
            Skill skill1 = new Skill();
            skill1.Init(skilldata1);
            EX_dan_4_0 ex1 = new EX_dan_4_0();
            ex1.canchoose = hasskill1 ;
            ex1.debuff = false;
            skill1.ExtendedAdd(ex1);

            lists.Add(skill1);
            //····················
            GDESkillData skilldata2 = (new GDESkillData("S_GFL_Dandelion_4_2")).ShallowClone();
            bool hasskill2 = false;
            foreach (Sex4_BuffRecord rec in this.debufflist)
            {
                skilldata2.Description = skilldata2.Description + (new GDEBuffData(rec.key)).Name;
                skilldata2.Description = skilldata2.Description + "(" + rec.stacks+")";
                skilldata2.Description = skilldata2.Description + "\n";
                hasskill2 = true;
            }
            Skill skill2 = new Skill();
            skill2.Init(skilldata2);
            EX_dan_4_0 ex2 = new EX_dan_4_0();
            ex2.canchoose = hasskill2 ;
            ex2.debuff = true;
            skill2.ExtendedAdd(ex2);

            lists.Add(skill2);

            if(lists.Find(a=>a.AllExtendeds.Find(b=>!b.ButtonSelectTerms())==null)!=null)
            {
                BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(lists, new SkillButton.SkillClickDel(this.Del_1), ScriptLocalization.System_SkillSelect.EffectSelect, false, false, true, false, false));
            }

        }
        public bool isbasic = false;
        public bool isview = false;
        public void Del_1(SkillButton Mybutton)
        {
            //Debug.Log("Del_1_point_1:" + Mybutton.Myskill.MySkill.KeyID);
            if (Mybutton.Myskill.AllExtendeds.Find(a => a is EX_dan_4_0) == null)//Mybutton.Myskill.MySkill.KeyID == "S_GFL_Dandelion_4_0")
            {
                //Sex_dan_4_buffsex buffsex = new Sex_dan_4_buffsex();
                //this.skilltargets[0].AllExtendeds.Add(buffsex);

                return;
                /*
                if (Mybutton.Myskill.AllExtendeds.Find(a => a is Sex_dan_4_c) != null)
                {
                    return;
                }
                else
                {
                    this.TargetBuff.Clear();
                    if (this.isbasic && (this.BChar as BattleAlly).MyBasicSkill.buttonData.AllExtendeds.Find(a => a is EX_dan_4_basic) != null)
                    {
                        Skill_Extended sex = (this.BChar as BattleAlly).MyBasicSkill.buttonData.AllExtendeds.Find(a => a is EX_dan_4_basic);
                        sex.TargetBuff = new List<BuffTag>();
                    }
                }

                */
            }
            else
            {
                List<Skill> lists = new List<Skill>();
                if (!(Mybutton.Myskill.AllExtendeds.Find(a => a is EX_dan_4_0) as EX_dan_4_0).debuff)//Mybutton.Myskill.MySkill.KeyID == "S_GFL_Dandelion_4_1")//增益列表
                {
                    //Debug.Log("Del_1_point_2_1");
                    //if(this.isview)
                    
                        GDESkillData skilldatac = new GDESkillData("S_GFL_Dandelion_4_c");
                        Skill skillc = new Skill();
                        skillc.Init(skilldatac);
                        Sex_dan_4_c exc = new Sex_dan_4_c();
                        skillc.ExtendedAdd(exc);
                        lists.Add(skillc);
                    

                    
                    GDESkillData skilldata0 = new GDESkillData("S_GFL_Dandelion_4_0").ShallowClone();
                    Skill skill0 = new Skill();
                    skill0.Init(skilldata0);
                    lists.Add(skill0);
                    
                    foreach (Sex4_BuffRecord rec in this.bufflist)//生成所有的展示技能
                    {
                        GDESkillData skilldata1 = (new GDESkillData("S_GFL_Dandelion_4_1")).ShallowClone();
                        GDEBuffData gdebuff = new GDEBuffData(rec.key).ShallowClone();
                        //gdebuff.OriginalKey= rec.key;
                        //Debug.Log("gdebuff:" + gdebuff.OriginalKey);
                        //Traverse.Create(gdebuff).Field("_key").SetValue(rec.key);
                        try { int x = gdebuff.Tick.DMG_Per; }
                        catch { gdebuff.Tick = new GDESkillEffectData("null"); }

                        skilldata1.Name = (skilldata1.Name + ": " + gdebuff.Name+"("+rec.stacks.ToString()+")");//展示的技能名字与描述修改与层数
                        skilldata1.Description = string.Empty;
                        skilldata1.Effect_Target.Buffs.Clear();

                        for (int i = 0; i < rec.stacks; i++)//给展示的技能附加buff，附加在技能本体版
                        {
                            skilldata1.Effect_Target.Buffs.Add(gdebuff);
                            break;//为避免init内未判断预览导致的bug，预览buff里仅显示一层
                        }

                        //--------------附加特殊护盾
                        
                        if (skilldata1.Effect_Target.Buffs.Count > 0 && rec.plusBarrier > 0)
                        {
                            skilldata1.Effect_Target.Buffs[0].Barrier = rec.plusBarrier;
                        }
                        
                        //---------------

                        Skill skill1 = new Skill();
                        skill1.Init(skilldata1);
                        skill1.Master = this.BChar;

                        skill1.AllExtendeds.Clear();

                        GDESkillExtendedData gdesex = new GDESkillExtendedData("EX_GFL_Dandelion_4_1").ShallowClone();
                        gdesex.Debuff = false;

                        gdesex.IsIcon_Path = gdebuff.Icon_Path;

                        gdesex.Name = gdebuff.Name;
                        Skill_Extended sex1 = DataToExtended(gdesex);
                        skill1.AllExtendeds.Add(sex1);

                        EX_dan_4_0 ex2 = new EX_dan_4_0();
                        ex2.debuff = false;
                        ex2.orgkey = rec.key;
                        ex2.stacks = rec.stacks;
                        if(this.isview)
                        {
                            ex2.canchoose = false;
                        }
                        //------
                        /*
                        BuffTag buffTag = new BuffTag();
                        buffTag.User = this.BChar;
                        buffTag.BuffData = gdebuff;
                        for (int i = 0; i < rec.stacks; i++)//给展示的技能附加buff，附加在技能本体版
                        {
                            ex2.TargetBuff.Add(buffTag.Clone());
                        }
                        */
                        //------
                        skill1.ExtendedAdd(ex2);

                        lists.Add(skill1);
                    }
                }
                if ((Mybutton.Myskill.AllExtendeds.Find(a => a is EX_dan_4_0) as EX_dan_4_0).debuff)//(Mybutton.Myskill.MySkill.KeyID == "S_GFL_Dandelion_4_2")//减益列表
                {
                    //if (this.isview)
                    
                        GDESkillData skilldatac = new GDESkillData("S_GFL_Dandelion_4_c");
                        Skill skillc = new Skill();
                        skillc.Init(skilldatac);
                        Sex_dan_4_c exc = new Sex_dan_4_c();
                        skillc.ExtendedAdd(exc);
                        lists.Add(skillc);
                    

                    GDESkillData skilldata0 = new GDESkillData("S_GFL_Dandelion_4_0").ShallowClone();
                    Skill skill0 = new Skill();
                    skill0.Init(skilldata0);
                    lists.Add(skill0);
                    
                    foreach (Sex4_BuffRecord rec in this.debufflist)//生成所有的展示技能
                    {
                        GDESkillData skilldata1 = (new GDESkillData("S_GFL_Dandelion_4_2")).ShallowClone();
                        GDEBuffData gdebuff = new GDEBuffData(rec.key).ShallowClone();
                        //gdebuff.OriginalKey = rec.key;
                        //Debug.Log("gdebuff:" + gdebuff.OriginalKey);
                        //Traverse.Create(gdebuff).Field("_key").SetValue(rec.key);
                        try { int x = gdebuff.Tick.DMG_Per; }
                        catch { gdebuff.Tick = new GDESkillEffectData("null"); }

                        skilldata1.Name = (skilldata1.Name + ": " + gdebuff.Name + "(" + rec.stacks.ToString() + ")");
                        skilldata1.Description = string.Empty;
                        skilldata1.Effect_Target.Buffs.Clear();
                        skilldata1.Effect_Target.BuffPlusTagPer.Clear();
                        //gdebuff.TagPer=Math.Max(gdebuff.TagPer, 100);
                        for (int i = 0; i < rec.stacks; i++)
                        {
                            skilldata1.Effect_Target.Buffs.Add(gdebuff);
                            skilldata1.Effect_Target.BuffPlusTagPer.Add(Math.Max(gdebuff.TagPer, 100) - gdebuff.TagPer);
                            break;//为避免init内未判断预览导致的bug，预览buff里仅显示一层
                        }
                        //--------------附加特殊护盾

                        if (skilldata1.Effect_Target.Buffs.Count > 0 && rec.plusBarrier > 0)
                        {
                            skilldata1.Effect_Target.Buffs[0].Barrier = rec.plusBarrier;
                        }

                        //---------------

                        Skill skill1 = new Skill();
                        skill1.Init(skilldata1);
                        skill1.Master = this.BChar;
                        skill1.AllExtendeds.Clear();

                        GDESkillExtendedData gdesex = new GDESkillExtendedData("EX_GFL_Dandelion_4_1").ShallowClone();
                        gdesex.Debuff = true;

                        gdesex.IsIcon_Path = gdebuff.Icon_Path;

                        gdesex.Name = gdebuff.Name;
                        Skill_Extended sex1 = DataToExtended(gdesex);
                        skill1.AllExtendeds.Add(sex1);

                        EX_dan_4_0 ex2 = new EX_dan_4_0();
                        ex2.debuff = true;
                        ex2.orgkey = rec.key;
                        ex2.stacks = rec.stacks;
                        if (this.isview)
                        {
                            ex2.canchoose = false;
                        }

                        skill1.ExtendedAdd(ex2);

                        lists.Add(skill1);
                    }
                }
                BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(lists, new SkillButton.SkillClickDel(this.Del_3), ScriptLocalization.System_SkillSelect.EffectSelect, false, false, true, false, false));

            }

        }

        
        public void Del_3(SkillButton Mybutton)//选择具体buff后附加buff
        {
            //Debug.Log("Del_2_point_1");
            if (Mybutton.Myskill.AllExtendeds.Find(a => a is EX_dan_4_0) == null)//Mybutton.Myskill.MySkill.KeyID == "S_GFL_Dandelion_4_0")
            {
                if(Mybutton.Myskill.AllExtendeds.Find(a => a is Sex_dan_4_c) != null)//Sex_dan_4_c
                {
                    return;
                }
                    this.Del_0(false, this.isview );
            }
            else
            {
                if(this.isview)
                {
                    return;
                }

                EX_dan_4_0 ex_4_0 = (Mybutton.Myskill.AllExtendeds.Find(a => a is EX_dan_4_0) as EX_dan_4_0);
                string key= ex_4_0.orgkey;
                int num = ex_4_0.stacks;
                List<BuffTag> tags= new List<BuffTag>();
                int barriernum = 0;
                foreach (GDEBuffData gdebuff in Mybutton.Myskill.MySkill.Effect_Target.Buffs)
                {
                    barriernum += gdebuff.Barrier;
                }


                for (int i = 0; i < num; i++)
                {
                    GDEBuffData gdebuff = new GDEBuffData(key).ShallowClone();
                    try { int x = gdebuff.Tick.DMG_Per; }
                    catch { gdebuff.Tick = new GDESkillEffectData("null"); }

                    BuffTag buffTag = new BuffTag();
                    buffTag.User = this.BChar;
                    buffTag.BuffData = gdebuff;
                    if (i <= 0 && barriernum > 0)
                    {
                        gdebuff.Barrier = barriernum;
                    }
                    if (ex_4_0.debuff)
                    {
                        buffTag.PlusTagPer = Math.Max(gdebuff.TagPer, 100) - gdebuff.TagPer;
                    }

                    tags.Add(buffTag.Clone());
                }

                /*
                foreach (GDEBuffData gdebuff in Mybutton.Myskill.MySkill.Effect_Target.Buffs)
                {
                    BuffTag buffTag = new BuffTag();
                    buffTag.User = this.BChar;
                    buffTag.BuffData = gdebuff;

                    if ((Mybutton.Myskill.AllExtendeds.Find(a => a is EX_dan_4_0) as EX_dan_4_0).debuff)
                    {
                        buffTag.PlusTagPer = Math.Max(gdebuff.TagPer, 100) - gdebuff.TagPer;
                    }
                    tags.Add(buffTag.Clone());
                }
                */

                //Debug.Log("Del_3_p_test");
                this.AddBuff2(key, tags, this.targets, this.BChar);
                //-------------------------------

            }

        }

        public void AddBuff2(string orgkey, List<BuffTag> tags, List<BattleChar> Targets, BattleChar User)
        {
            //yield return new WaitForFixedUpdate();
            //Debug.Log("AddBuff2_p_test");
            int barriernum = 0;
            foreach (BuffTag btg in tags)
            {
                barriernum += btg.BuffData.Barrier;
                btg.BuffData.Barrier = 0;
            }
            //Debug.Log("point2:"+tags.Count);
            try
            {
                foreach (BattleChar target in Targets)
                {
                    int oldbar = 0;
                    string key = orgkey;//tags[0].BuffData.OriginalKey;
                    int per = tags[0].PlusTagPer;

                    if (target.BuffFind(key, false))
                    {
                        oldbar = target.BuffReturn(key, false).BarrierHP;
                    }
                    //Debug.Log("oldbar:" + oldbar);
                    //Debug.Log("barriernum:" + barriernum);
                    foreach (BuffTag btg in tags)
                    {
                        try
                        {
                            target.BuffAdd(key, User, false, btg.PlusTagPer, false, -1, false);
                        }
                        catch { //Debug.Log("error2");
                                }
                    }
                    if (barriernum > 0)
                    {
                        Buff buff = target.BuffReturn(key, false);
                        buff.BarrierHP = oldbar + barriernum;
                    }

                }
            }
            catch
            {
                //Debug.Log("error1");
            }

            //yield break;
        }
        /*
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            if (this.TargetBuff[0].BuffData.Barrier>0)
            {
                this.TargetBuff[0].BuffData.Barrier = 0;
                List < Sex4_BuffRecord > alllist= new List<Sex4_BuffRecord>();
                alllist.AddRange(this.bufflist);
                alllist.AddRange(this.debufflist);
                if (alllist.Find(a=>a.key== this.TargetBuff[0].BuffData.Key)!=null)
                {
                    Sex4_BuffRecord rec = alllist.Find(a => a.key == this.TargetBuff[0].BuffData.Key);
                    foreach(BattleChar target in Targets)
                    {
                        if (target.BuffFind(this.TargetBuff[0].BuffData.Key, false))
                        {
                            int old = target.BuffReturn(this.TargetBuff[0].BuffData.Key, false).BarrierHP;
                            BattleSystem.DelayInput(this.AddPlusSheld(target, this.TargetBuff[0].BuffData.Key, old, rec.plusBarrier));
                        }
                        else
                        {
                            BattleSystem.DelayInput(this.AddPlusSheld(target, this.TargetBuff[0].BuffData.Key, 0, rec.plusBarrier));

                        }
                    }

                }
            }
            

        }
        public IEnumerator AddPlusSheld(BattleChar target, string key,int oldnum,int plusnum)
        {
            yield return new WaitForFixedUpdate();
            if (target.BuffFind(this.TargetBuff[0].BuffData.Key, false))
            {
                Buff buff = target.BuffReturn(this.TargetBuff[0].BuffData.Key, false);
                if(buff.BarrierHP<oldnum+plusnum)
                {
                    buff.BarrierHP+=Math.Min(plusnum,oldnum+plusnum-buff.BarrierHP);
                }
            }
                yield break;
        }
        */
        public List<Sex4_BuffRecord> bufflist = new List<Sex4_BuffRecord>();
        public List<Sex4_BuffRecord> debufflist = new List<Sex4_BuffRecord>();
    }
    public class Sex4_BuffRecord
    {
        public string key = string.Empty;
        public int stacks = 0;
        public int plusBarrier = 0;
    }
    public class EX_dan_4 : Sex_ImproveClone
    {


        // Token: 0x06000005 RID: 5 RVA: 0x000020FC File Offset: 0x000002FC
        public void go()
        {
            if (!BattleSystem.instance.DelayWait)
            {
                Sex_dan_4 sex = this.MySkill.AllExtendeds.Find(a => a is Sex_dan_4) as Sex_dan_4;
                //Debug.Log("有sex4？："+sex!=null);
                sex.Del_0(false,true);
            }
        }




        public List<Sex4_BuffRecord> bufflist = new List<Sex4_BuffRecord>();
        public List<Sex4_BuffRecord> debufflist = new List<Sex4_BuffRecord>();

        
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            bool flag = this.BuffIcon != null;
            if (flag)
            {
                bool flag2 = this.BuffIcon.GetComponent<EX_dan_4.EX_dan_4_click>() == null;
                if (flag2)
                {
                    this.BuffIcon.AddComponent<EX_dan_4.EX_dan_4_click>().Init(this);
                }
            }

        }
        
        private EX_dan_4.EX_dan_4_click behavior;

        public class EX_dan_4_click : MonoBehaviour, IEventSystemHandler, IPointerClickHandler
        {
            // Token: 0x06000194 RID: 404 RVA: 0x00008639 File Offset: 0x00006839
            public void Init(EX_dan_4 main)
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
            private EX_dan_4 Main;
        }
        
    }
    /*
    public class Bcl_dan_4 : Buff
    {
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();

            Button button = this.BuffIcon.AddComponent<Button>();
            button.onClick.AddListener(new UnityAction(this.Go));


        }

        public override void FixedUpdate()
        {
            if((this.BChar as BattleAlly).BasicSkill.AllExtendeds.Find(a => a is Sex_dan_4)==null)
            {
                this.SelfDestroy();
            }
            if((this.BChar as BattleAlly).MyBasicSkill.InActive)
            {
                this.SelfDestroy();
            }
        }
        public void Go()
        {
            if (!BattleSystem.instance.DelayWait)
            {
                Sex_dan_4 sex = (this.BChar as BattleAlly).BasicSkill.AllExtendeds.Find(a => a is Sex_dan_4) as Sex_dan_4;
                //Debug.Log("有sex4？："+sex!=null);
                sex.Del_0(true);

            }
        }
        public Sex_dan_4 mainsex =new Sex_dan_4();

    }
    */
    public class EX_dan_4_1 : Sex_ImproveClone
    {

    }
    public class EX_dan_4_0 : Sex_ImproveClone
    {
        public override bool ButtonSelectTerms()
        {
            return this.canchoose;
        }

        public bool debuff = false;
        public bool canchoose = true;
        public string orgkey = string.Empty;
        public int stacks = 0;
    }
    public class EX_dan_4_basic : Sex_ImproveClone
    {

    }
    public class Sex_dan_4_c : Sex_ImproveClone
    {

    }
    public class Sex_dan_4_buffsex : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {

            /*
            if (this.TargetBuff.Count>0&& this.TargetBuff[0].BuffData.Barrier > 0)
            {
                int barriernum = this.TargetBuff[0].BuffData.Barrier;
                this.TargetBuff[0].BuffData.Barrier = 0;


                    foreach (BattleChar target in Targets)
                    {
                        if (target.BuffFind(this.TargetBuff[0].BuffData.Key, false))
                        {
                            int old = target.BuffReturn(this.TargetBuff[0].BuffData.Key, false).BarrierHP;
                            BattleSystem.DelayInput(this.AddPlusSheld(target, this.TargetBuff[0].BuffData.Key, old, barriernum));
                        }
                        else
                        {
                            BattleSystem.DelayInput(this.AddPlusSheld(target, this.TargetBuff[0].BuffData.Key, 0, barriernum));

                        }
                    }

                
            }
            //Debug.Log("buffsex_2");
            if (this.SelfBuff.Count>0&&this.SelfBuff[0].BuffData.Barrier > 0)
            {
                //Debug.Log("buffsex_2_1");
                int barriernum = this.SelfBuff[0].BuffData.Barrier;
                this.SelfBuff[0].BuffData.Barrier = 0;
                //Debug.Log("buffsex_2_2");


                if (SkillD.Master.BuffFind(this.SelfBuff[0].BuffData.Key, false))
                {
                    int old = SkillD.Master.BuffReturn(this.SelfBuff[0].BuffData.Key, false).BarrierHP;
                    BattleSystem.DelayInput(this.AddPlusSheld(SkillD.Master, this.SelfBuff[0].BuffData.Key, old, barriernum));
                }
                else
                {
                    BattleSystem.DelayInput(this.AddPlusSheld(SkillD.Master, this.SelfBuff[0].BuffData.Key, 0, barriernum));

                }



            }
            */

            //Traverse.Create(gdebuff).Field("_key").SetValue(rec.key);
            //Debug.Log("test1:" + (this.MySkill == SkillD));
            //Debug.Log("test1:" + ( SkillD.AllExtendeds.Contains(this)));
            if (this.TargetBuff.Count > 0 )//&& this.TargetBuff.Find(a=>a.BuffData.Barrier>0)!=null)
            {
                
                List <BuffTag> tags = new List<BuffTag>();
                tags.AddRange(this.TargetBuff);

                this.TargetBuff.Clear();

                //SkillD.AllExtendeds.RemoveAll(a=>a is Sex_dan_4_buffsex);


                this.AddPlusShield2(this.orgkey,tags, Targets, SkillD.Master);
                //BattleSystem.DelayInputAfter(this.AddPlusShield2(tags, Targets, SkillD.Master));
                /*
                //Debug.Log("point1" );
                int barriernum = 0;
                foreach(BuffTag btg in tags)
                {
                    barriernum += btg.BuffData.Barrier;
                    btg.BuffData.Barrier = 0;
                }
                //Debug.Log("point2:"+tags.Count);
                try
                {
                    foreach (BattleChar target in Targets)
                    {
                        int oldbar = 0;
                        string key = tags[0].BuffData.Key;
                        int per = tags[0].PlusTagPer;

                        if (target.BuffFind(key, false))
                        {
                            oldbar = target.BuffReturn(key, false).BarrierHP;
                        }
                        //Debug.Log("oldbar:" + oldbar);
                        //Debug.Log("barriernum:" + barriernum);
                        foreach (BuffTag btg in tags)
                        {
                            try
                            {
                                target.BuffAdd(key, SkillD.Master, false, btg.PlusTagPer, false, -1, false);
                            }
                            catch { //Debug.Log("error2"); }
                        }
                        Buff buff = target.BuffReturn(key, false);


                        buff.BarrierHP = oldbar + barriernum;
                    }
                }
                catch
                {
                    //Debug.Log("error1");
                }
                */
            }

            if (this.SelfBuff.Count > 0 )//&& this.SelfBuff.Find(a => a.BuffData.Barrier > 0) != null)
            {
                List<BuffTag> tags = new List<BuffTag>();
                tags.AddRange(this.SelfBuff);
                this.SelfBuff.Clear();

                this.AddPlusShield2(this.orgkey,tags, new List<BattleChar> { SkillD.Master }, SkillD.Master);
                //BattleSystem.DelayInputAfter(this.AddPlusShield2(tags, new List<BattleChar> { SkillD.Master},SkillD.Master));
                /*
                int barriernum = 0;
                foreach (BuffTag btg in tags)
                {
                    barriernum += btg.BuffData.Barrier;
                    btg.BuffData.Barrier = 0;
                }

                try
                {

                    int oldbar = 0;
                    string key = tags[0].BuffData.Key;
                    int per = tags[0].PlusTagPer;

                    if (SkillD.Master.BuffFind(key, false))
                    {
                        oldbar = SkillD.Master.BuffReturn(key, false).BarrierHP;
                    }
                    foreach (BuffTag btg in tags)
                    {
                        try
                        {
                            SkillD.Master.BuffAdd(key, SkillD.Master, false, btg.PlusTagPer, false, -1, false);
                        }
                        catch { }
                    }
                    SkillD.Master.BuffReturn(key, false).BarrierHP = oldbar + barriernum;

                }
                catch
                {
                    //Debug.Log("error");
                }
                */

            }
            base.SkillUseSingle(SkillD, Targets);

        }
        public IEnumerator AddPlusSheld(BattleChar target, string key, int oldnum, int plusnum)
        {
            yield return new WaitForFixedUpdate();
            if (target.BuffFind(key, false))
            {
                Buff buff = target.BuffReturn(key, false);
                if (buff.BarrierHP != oldnum + plusnum)
                {
                    buff.BarrierHP += plusnum;//Math.Min(plusnum, oldnum + plusnum - buff.BarrierHP);
                }
            }
            yield break;
        }

        public void AddPlusShield2(string orgkey,List<BuffTag> tags,List<BattleChar> Targets,BattleChar User)
        {
            //yield return new WaitForFixedUpdate();

            int barriernum = 0;
            foreach (BuffTag btg in tags)
            {
                barriernum += btg.BuffData.Barrier;
                btg.BuffData.Barrier = 0;
            }
            //Debug.Log("point2:"+tags.Count);
            try
            {
                foreach (BattleChar target in Targets)
                {
                    int oldbar = 0;
                    string key = orgkey;//tags[0].BuffData.OriginalKey;
                    int per = tags[0].PlusTagPer;

                    if (target.BuffFind(key, false))
                    {
                        oldbar = target.BuffReturn(key, false).BarrierHP;
                    }
                    //Debug.Log("oldbar:" + oldbar);
                    //Debug.Log("barriernum:" + barriernum);
                    foreach (BuffTag btg in tags)
                    {
                        try
                        {
                            target.BuffAdd(key, User, false, btg.PlusTagPer, false, -1, false);
                        }
                        catch { //Debug.Log("error2");
                                }
                    }
                    if(barriernum > 0)
                    {
                        Buff buff = target.BuffReturn(key, false);
                        buff.BarrierHP = oldbar + barriernum;
                    }

                }
            }
            catch
            {
                //Debug.Log("error1");
            }

            //yield break;
        }
        public string orgkey = string.Empty;
    }

    public class Sex_dan_5 : Sex_ImproveClone, IP_AntiVanish
    {
        public bool AntiVanish(BattleChar Target,Skill skill)
        {
            if(skill==this.MySkill)
            {
                return true;
            }
            return false;
        }
    }
    public class Bcl_dan_5 : Buff,IP_AntiVanish//,IP_TurnEnd
    {
        /*
        public override void TurnUpdate()
        {
            //base.TurnUpdate();
        }
        public void TurnEnd()
        {
            base.TurnUpdate();
        }*/
        public bool AntiVanish(BattleChar Target, Skill skill)
        {
            if (Target==this.BChar && skill.Master.Info.Ally==this.Usestate_L.Info.Ally)
            {
                return true;
            }
            return false;
        }
        public override void Init()
        {
            base.Init();
            this.PlusStat.crihit = 30;
            this.PlusStat.DMGTaken = 15;
            this.PlusStat.dod = -15;
        }
    }

    public class Sex_dan_6 : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);   
            foreach (BattleChar Target in Targets)
            {

                List<Buff> list = new List<Buff>();//解除随机debuff
                foreach (Buff buff in Target.Buffs)
                {
                    if (buff.BuffData.Debuff && buff.BuffData.BuffTag.Key != "null" && !buff.BuffData.Cantdisable && !buff.BuffData.Hide && !buff.DestroyBuff)
                    {
                        list.Add(buff);
                    }
                }
                if (list.Count != 0)
                {
                    Target.BuffRemove(list.Random(Target.GetRandomClass().Main).BuffData.Key, false);
                }
            }
        }
    }
    public class Bcl_dan_6 : Buff,IP_BuffAdd_After2,IP_DebuffResist_After, IP_BuffAdd_Before
    {
        public override string DescExtended()
        {
            bool equip = false;
            foreach(BattleChar bc in this.BChar.MyTeam.AliveChars_Vanish)
            {
                if(bc.Info.Passive is P_Dandelion)
                {
                    try
                    {
                        foreach (IP_Equip_Dandelion ip_patch in bc.IReturn<IP_Equip_Dandelion>())
                        {
                            bool flag = ip_patch != null;
                            if (flag)
                            {
                                bool flag2 = ip_patch.Equip_Dandelion(bc);
                                equip = equip || flag2;
                            }
                        }
                    }
                    catch { }
                }
            }
            if(equip)
            {
                GDESkillKeywordData gde = new GDESkillKeywordData("KeyWord_SF_Dandelion_6");
                string str = this.BuffData.Description + gde.Desc;
                return str.Replace("&dan", "");
            }
            else if((this.View && this.BChar.Info.Passive is P_Dandelion)||(this.View && BattleSystem.instance==null&&BChar.Info.KeyData=="GFL_Dandelion"))
            {
                GDESkillKeywordData gde = new GDESkillKeywordData("KeyWord_SF_Dandelion_6");
                string str = this.BuffData.Description + gde.Desc;
                return str.Replace("&dan",this.Usestate_L.Info.Name);
            }
            else if (this.BChar.Info.Passive is P_Dandelion)
            {
                GDESkillKeywordData gde = new GDESkillKeywordData("KeyWord_SF_Dandelion_6");
                string str = this.BuffData.Description +  gde.Desc;
                return str.Replace("&dan", "");

            }

                return this.BuffData.Description;

        }
        public override void Init()
        {
            base.Init();
            this.PlusStat.RES_CC += 400;
            this.PlusStat.RES_DEBUFF += 400;
            this.PlusStat.RES_DOT += 400;

        }
        public IEnumerator CleanList()
        {
            yield return new WaitForFixedUpdate();
            this.strlist.Clear();
            yield break;
        }

        public void BuffAdd_Before(BattleChar bchar, string key, BattleChar UseState, bool hide, ref int PlusTagPer, bool debuffnonuser, int RemainTime, bool StringHide)
        {
            GDEBuffData gde = new GDEBuffData(key);
            if(bchar==this.BChar&& gde.Debuff && !gde.Hide && !hide)
            {
                int frameCount = Time.frameCount;
                //Debug.Log("丹德莱检查点1");
                if (this.BChar.BuffCheck.Lastbuff == key && this.BChar.BuffCheck.TimeSeed == frameCount && (this.BChar.BuffCheck.PlusResist||!this.BChar.BuffCheck.IsAdd))
                {
                    //Debug.Log("丹德莱检查点1-r");
                    return;
                }
                if(key==GDEItemKeys.Buff_B_Neardeath)
                {
                    return;
                }
                //Debug.Log("丹德莱检查点2");
                LastBuffRsisCheck lastBuffRsisCheck = new LastBuffRsisCheck();
                lastBuffRsisCheck.TimeSeed = Time.frameCount;
                lastBuffRsisCheck.Lastbuff = key;
                lastBuffRsisCheck.IsAdd = false;
                //lastBuffRsisCheck.PlusResist = false;
                bchar.BuffCheck = lastBuffRsisCheck;
                //Debug.Log("丹德莱检查点3");
                Buff buff = Buff.DataToBuff(gde, this.BChar, UseState, RemainTime, false, 0);
                if (BattleSystem.instance != null)
                {
                    //Debug.Log("丹德莱检查点4");
                    BattleSystem.DelayInputAfter(BattleSystem.instance.DebuffResist(buff));
                }
                else
                {
                    this.BChar.Info.TextOut(buff.BuffData.Name + " " + ScriptLocalization.UI_Battle.DebuffResist);
                }

                if (!(BattleSystem.instance == null))
                {
                    if (!lastBuffRsisCheck.IsAdd)
                    {
                        IP_DebuffResist ip_DebuffResist = buff as IP_DebuffResist;
                        if (ip_DebuffResist != null)
                        {
                            ip_DebuffResist.Resist();
                        }
                    }
                }
                
            }
        }
        public void DebuffResist_After(Buff buff)
        {
            if(buff.BChar==this.BChar)
            {
                this.strlist.Add(buff.BuffData.Key);
                this.framecount = Time.frameCount;

                BattleSystem.DelayInput(this.CleanList());
            }
        }
        public void BuffAdd_After2(BattleChar bchar, string key, BattleChar UseState, bool hide, int PlusTagPer, bool debuffnonuser, int RemainTime, bool StringHide, Buff __result)
        {
            if(bchar==this.BChar && this.strlist.Count > 0 && __result==null )
            {
                string buffkey = this.strlist.Find(a => a == key);
                if(buffkey != null && buffkey != string.Empty) 
                {
                    GDEBuffData gdebuffData = new GDEBuffData(buffkey);
                    int per = 0;
                    if (gdebuffData.BuffTag.Key == GDEItemKeys.BuffTag_CrowdControl)
                    {
                        if (!debuffnonuser)
                        {
                            per = (int)(UseState.GetStat.HIT_CC + (float)gdebuffData.TagPer+ (float)PlusTagPer) - (int)this.BChar.GetStat.RES_CC + (int)this.PlusStat.RES_CC;
                        }
                        else
                        {
                            per = gdebuffData.TagPer + PlusTagPer - (int)this.BChar.GetStat.RES_CC + (int)this.PlusStat.RES_CC;
                        }
                    }
                    if (gdebuffData.BuffTag.Key == GDEItemKeys.BuffTag_Debuff)
                    {
                        if (!debuffnonuser)
                        {
                            per = (int)(UseState.GetStat.HIT_DEBUFF + (float)gdebuffData.TagPer + (float)PlusTagPer) - (int)this.BChar.GetStat.RES_DEBUFF + (int)this.PlusStat.RES_DEBUFF;
                        }
                        else
                        {
                            per = gdebuffData.TagPer + PlusTagPer - (int)this.BChar.GetStat.RES_DEBUFF + (int)this.PlusStat.RES_DEBUFF;
                        }
                    }
                    if (gdebuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT)
                    {
                        if (!debuffnonuser)
                        {
                            per = (int)(UseState.GetStat.HIT_DOT + (float)gdebuffData.TagPer + (float)PlusTagPer) - (int)this.BChar.GetStat.RES_DOT + (int)this.PlusStat.RES_DOT;
                        }
                        else
                        {
                            per = gdebuffData.TagPer + PlusTagPer - (int)this.BChar.GetStat.RES_DOT + (int)this.PlusStat.RES_DOT;
                        }
                    }

                    per=Math.Max(0, per);

                    this.PlusStat.RES_CC -= per;
                    this.PlusStat.RES_DEBUFF -= per;
                    this.PlusStat.RES_DOT -= per;

                    if(this.PlusStat.RES_CC<=0 || this.PlusStat.RES_DEBUFF<0 || this.PlusStat.RES_DOT<0)
                    {
                        this.SelfDestroy();
                    }
                }
            }
        }
        public List<string> strlist = new List<string>();
        public int framecount = 0;
    }

    public class Sex_dan_7 : Sex_ImproveClone
    {

    }
    public class Bcl_dan_7 : Buff//,IP_BuffUpdate
    {
        /*
        public void BuffUpdate(Buff MyBuff)
        {
            int num = 0;
            foreach (Buff buff in this.BChar.Buffs)
            {
                if(buff!=this && buff.BuffData.Debuff && !buff.IsHide)
                num += buff.StackNum;
            }

            this.PlusDamageTick = num * BattleChar.CalculationResult(this.Usestate_L.GetStat.atk, this.BuffData.Tick.DMG_Per, this.BuffData.Tick.DMG_Base);
            //base.TurnUpdate();
            //this.PlusDamageTick = 0;
        }
        */
        public override void TurnUpdate()
        {

            int dmg = this.Tick();
            BattleSystem.DelayInput(this.PlusTick(dmg,this.BChar));
            base.TurnUpdate();

            /*
            int dmg = this.Tick();
            if(dmg > 0) 
            {
                for (int i = 0; i < num; i++)
                {
                    if (this.BChar.Info.Ally)
                    {
                        this.BChar.Damage(BattleSystem.instance.DummyChar, dmg, false, true, true, 0, false, false, false);
                    }
                    else
                    {
                        this.BChar.Damage(BattleSystem.instance.DummyCharEnemy, dmg, false, true, true, 0, false, false, false);
                    }
                }

            }
            */
        }
        public IEnumerator PlusTick(int dmg,BattleChar target)
        {

            if (dmg > 0)
            {
                int num = 0;
                foreach (Buff buff in this.BChar.Buffs)
                {
                    if (buff != this && buff.BuffData.Debuff && !buff.IsHide)
                    {
                        //num += buff.StackNum;
                        num++;
                    }
                }
                for (int i = 0; i < num; i++)
                {
                    if(i>0)
                    {
                        yield return new WaitForSeconds(0.2f);
                    }
                    if (target.Info.Ally)
                    {
                        target.Damage(BattleSystem.instance.DummyChar, dmg, false, true, true, 0, false, false, false);
                    }
                    else
                    {
                        target.Damage(BattleSystem.instance.DummyCharEnemy, dmg, false, true, true, 0, false, false, false);
                    }
                    
                }

            }
            yield break;
        }
    }

    public class Sex_dan_8 : Sex_ImproveClone
    {

    }
    public class Bcl_dan_8_1 : Buff, IP_BuffAddAfter//,IP_BuffUpdate
    {
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if(BuffTaker == this.BChar && addedbuff == this)
            {
                BattleSystem.DelayInput(this.Setup());
            }
            if (BuffTaker==this.BChar && addedbuff!=this && !addedbuff.BuffData.Debuff && stackBuff.RemainTime != 0 )
            {
                stackBuff.RemainTime++;
            }
        }
        public IEnumerator Setup()
        {
            yield return new WaitForFixedUpdate();
            this.canadd = true;
            yield break;
        }
        public bool canadd = false;
    }
    public class Bcl_dan_8_2 : Buff//,IP_BuffUpdate
    {
        public override void Init()
        {
            base.Init();

            this.BarrierHP += (int)(0.21 * this.Usestate_L.GetStat.atk+0.42*this.Usestate_L.GetStat.reg+0.14*this.Usestate_L.GetStat.maxhp);
        }


    }

    public class Sex_dan_9 : Sex_ImproveClone
    {

    }
    public class Bcl_dan_9 : Buff, IP_SkillUse_User//,IP_BuffUpdate
    {
        public override void Init()
        {
            base.Init();
            this.PlusPerStat.Damage = -15;
            this.PlusStat.Weak = true;
        }
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.IsDamage )
            {
                BattleSystem.DelayInput(this.SkillUseAfter());
            }
        }
        public IEnumerator SkillUseAfter()
        {
            base.SelfStackDestroy();
            yield return null;
            yield break;
        }
    }

    public class Sex_dan_10 : Sex_ImproveClone
    {
        public override void BattleStartDeck(List<Skill> Skills_Deck)
        {
            //BattleSystem.instance.AllyTeam.LucyChar.BuffAdd("B_GFL_Dandelion_10",this.BChar);
            this.BChar.BuffAdd("B_GFL_Dandelion_10", this.BChar);
            BattleSystem.instance.AllyTeam.Skills_Deck.Remove(this.MySkill);
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            this.BChar.BuffAdd("B_GFL_Dandelion_10", this.BChar);
        }
        public override string DescExtended(string desc)
        {

            return base.DescExtended(desc).Replace("&a", this.BChar.Info.Name);

        }
    }
    public class Bcl_dan_10 : Buff, IP_BattleEndOutBattle//,IP_BuffUpdate
    {
        public override void Init()
        {
            if(!this.userlist.Contains(this.Usestate_L.Info))
            {
                this.userlist.Add(this.Usestate_L.Info);
            }
            foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars_Vanish)
            {
                this.chalist.Add(bc.Info);
            }
        }

        public void BattleEndOutBattle()
        {
            //Debug.Log("Bcl_dan_10");
            if(this.userlist.Find(a=>!a.Incapacitated)!=null)
            {
                foreach (Character character in PlayData.TSavedata.Party)
                {
                    if (this.chalist.Contains(character) && character.Incapacitated)
                    {
                        character.Incapacitated = false;
                        if (character.Hp <= 0)
                        {
                            character.Hp = 1;
                        }
                        character.GetBattleChar.BuffAdd("B_GFL_Dandelion_10_1", character.GetBattleChar);
                    }
                }
            }
            this.BChar.Info.Buffs_Field.Remove(this);
            if (this.BuffIcon != null)
            {
                UnityEngine.Object.Destroy(this.BuffIcon);
            }

            //this.SelfDestroy();
        }
        public List<Character> chalist = new List<Character>();
        public List<Character> userlist = new List<Character>();
    }

    public class Bcl_dan_10_1 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusPerStat.Damage = -20;
            this.PlusPerStat.Heal = -20;
            this.PlusStat.def = -20;
            //this.BuffData.Tick.HEAL_Base = (int)(this.BChar.GetStat.maxhp * 0.3);
            //this.BuffData.Tick.ForceHeal = true;
        }
        public override void TurnUpdate()
        {
            base.TurnUpdate();
            this.BChar.Heal(this.BChar, (float)(this.BChar.GetStat.maxhp * 0.35), false, true, null);
        }
    }

    public class Sex_dan_11 : Sex_ImproveClone
    {
        public override bool Terms()
        {
            if(this.BChar.MyTeam.AliveChars.Count>=2)
            {
                return base.Terms();
            }
            return false;
        }

    }

    public interface IP_B11check
    {
        // Token: 0x06000001 RID: 1
        void B11check(Buff buff);
    }
    public class Bcl_dan_11 : Buff,IP_BuffAddAfter//, IP_B11check//,IP_BuffUpdate
    {
        public override string DescExtended()
        {
            string str=string.Empty;

            for (int i = 0; i < this.links.Count; i++)
            {
                if (i > 0)
                {
                    str = str+ "/";
                }
                str = str + this.links[i].Info.Name;
            }

            return this.BuffData.Description.Replace("&a", str);
        }
        public void B11check(Buff buff)
        {
            this.framecut = 2;
            if(this.loopsinframe<50 )//&& buff!=this)
            {
                
                this.SetData();
            }

        }
        public void SetData()
        {
            this.loopsinframe++;
            //Debug.Log("loops:" + this.loopsinframe);

            for (int i = 1; i < this.links.Count; i++)
            {
                if (this.links[i].IsDead)
                {
                    this.links.RemoveAt(i);
                    i--;
                }
            }
            int atk = 0;
            float def = 0;
            float reg = 0;
            float hit = 0;
            float dod = 0;
            float cri = 0;
            foreach (BattleChar link in this.links)
            {
                /*
                atk += (int)this.Clamp0((float)(0.5*link.GetStat.atk), 0, (float)(1 * this.BChar.GetStat.atk));
                def += this.Clamp0((float)(0.5*link.GetStat.def), 0, (float)(1 * this.BChar.GetStat.def));
                reg += this.Clamp0((float)(0.5*link.GetStat.reg), 0, (float)(1 * this.BChar.GetStat.reg));
                hit += this.Clamp0((float)(0.5*(link.GetStat.hit-100)), 0, (float)(1 * (this.BChar.GetStat.hit-100)));
                dod += this.Clamp0((float)(0.5*link.GetStat.dod), 0, (float)(1 * this.BChar.GetStat.dod));
                cri += this.Clamp0((float)(0.5*link.GetStat.cri), 0, (float)(1 * this.BChar.GetStat.cri));
                */
                atk += (int)Math.Max((float)(0.5 * link.GetStat.atk), 0);
                def += Math.Max((float)(0.5 * link.GetStat.def), 0);
                reg += Math.Max((float)(0.5 * link.GetStat.reg), 0);
                hit += Math.Max((float)(0.5 * (link.GetStat.hit - 100)), 0);
                dod += Math.Max((float)(0.5 * link.GetStat.dod), 0);
                cri += Math.Max((float)(0.5 * link.GetStat.cri), 0);
            }
            bool flag1 = (this.PlusStat.atk == atk && this.PlusStat.def == def && this.PlusStat.reg == reg && this.PlusStat.hit == hit && this.PlusStat.dod == dod && this.PlusStat.cri == cri);



            this.PlusStat.atk = atk;
            this.PlusStat.def = def;
            this.PlusStat.reg = reg;
            this.PlusStat.hit = hit;
            this.PlusStat.dod = dod;
            this.PlusStat.cri = cri;


            if (!flag1)
            {
                List<BattleChar> list = new List<BattleChar>();
                list.AddRange(BattleSystem.instance.AllyTeam.AliveChars_Vanish);
                list.AddRange(BattleSystem.instance.EnemyTeam.AliveChars_Vanish);
                foreach (BattleChar bc in list)
                {
                    foreach(Buff buff in bc.Buffs)
                    {
                        if(buff is Bcl_dan_11)
                        {
                            (buff as Bcl_dan_11).B11check(this);
                        }
                    }
                }
                /*
                foreach (IP_B11check ip_1 in BattleSystem.instance.IReturn<IP_B11check>())
                {
                    bool flag = ip_1 != null;
                    if (flag)
                    {
                        ip_1.B11check(this);
                    }
                }
                */
            }

            if (this.links.Count <= 0)
            {
                this.SelfDestroy();
            }
        }
        public float Clamp0(float num,float min, float max)
        {
            return Math.Max(min, Math.Min(max, num));
        }
        public override void FixedUpdate()
        {
            /*if (this.needselect && !this.Usestate_L.BattleInfo.TargetSelecting)
            {
                if(this.BChar.MyTeam.AliveChars.Find(a=>a!=this.BChar))
                {
                    Skill skill = Skill.TempSkill("S_GFL_Dandelion_11_1", this.Usestate_L);
                    skill.FreeUse = true;
                    Sex_dan_11_1 sex = skill.AllExtendeds.Find(a => a is Sex_dan_11_1) as Sex_dan_11_1;
                    sex.mainbuff = this;
                    this.Usestate_L.BattleInfo.TargetSelect(skill, this.Usestate_L);
                }
                else
                {
                    this.SetData();
                }
            }
            */

            this.loopsinframe = 0;

            if (!this.needselect)
            {
                this.frame++;
                if (this.frame > 24 || this.frame<0 || this.framecut>0)
                {
                    //Debug.Log("setdata:" + this.frame);
                    this.frame = 0;
                    this.framecut=Math.Max(0, this.framecut-1);
                    this.SetData();
                }
            }

            
        }
        private int frame = 0;
        private int framecut = 2;
        private int loopsinframe = 0;
        /*public void BuffUpdate(Buff MyBuff)
        {
            if(!this.needselect)
            {
                this.SetData();
            }
        }*/

        public IEnumerator ForceSelect()
        {
            int n = 10;
            while (this.needselect)
            {
                if (this.BChar.MyTeam.AliveChars.Find(a => a != this.BChar)!=null && !this.BChar.BattleInfo.TargetSelecting)
                {
                    //BattleChar.SkillNameOutOrigin(this.Usestate_L.BattleInfo, "选择链接对象", true);
                    Skill skill = Skill.TempSkill("S_GFL_Dandelion_11_1", this.Usestate_L);
                    skill.FreeUse = true;
                    Sex_dan_11_1 sex = skill.AllExtendeds.Find(a => a is Sex_dan_11_1) as Sex_dan_11_1;
                    sex.mainbuff = this;
                    this.Usestate_L.BattleInfo.TargetSelect(skill, this.Usestate_L);
                }
                else if(this.BChar.MyTeam.AliveChars.Find(a => a != this.BChar) == null)
                {
                    this.needselect = false;
                    this.SetData();
                }
                n++;
                if(n>9)
                {

                    GameObject gameObject = Misc.UIInst(this.BChar.BattleInfo.SkillName);
                    gameObject.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "选择链接对象";
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
            yield break;
        }
        public List<GameObject> objectList = new List<GameObject>();
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (addedbuff == this)
            {
                this.needselect = true;
                //BattleChar.SkillNameOutOrigin(this.Usestate_L.BattleInfo, "选择链接对象", true);
                BattleSystem.DelayInput(this.ForceSelect());
                //this.Usestate_L.BattleInfo.TargetSelect( skill,this.Usestate_L);

            }
        }


        public void SetTarget(BattleChar target)
        {
            this.needselect = false;
            if(!this.links.Contains(target))
            {
                this.links.Clear();
                this.links.Add(target);
            }
            this.SetData();
        }
        

        public bool needselect = false;
        public List<BattleChar> links = new List<BattleChar>();
    }
    public class Sex_dan_11_1 : Sex_ImproveClone, IP_ParticleOut_Before
    {
        public override bool TargetSelectExcept(BattleChar ExceptTarget)
        {

            return ExceptTarget == this.mainbuff.BChar || ExceptTarget.Info.Ally!= this.mainbuff.BChar.Info.Ally;
        }
        public void ParticleOut_Before(Skill SkillD, List<BattleChar> Targets)
        {
            //base.SkillUseSingle(SkillD, Targets);
            if (SkillD == this.MySkill)
            {

                this.mainbuff.SetTarget(Targets[0]);
            }

        }

        public Bcl_dan_11 mainbuff = new Bcl_dan_11();
    }

    public class Sex_dan_12 : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            for (int i = 0; i < 2; i++)
            {
                BattleSystem.DelayInputAfter(this.Draw());
            }
        }

        // Token: 0x06000B8F RID: 2959 RVA: 0x0007EEE8 File Offset: 0x0007D0E8
        public IEnumerator Draw()
        {
            List<Skill> list_skill = new List<Skill>();
            foreach(Skill skill in BattleSystem.instance.AllyTeam.Skills_Deck)
            {
                List<GDEBuffData> list_buffdata= new List<GDEBuffData>();
                try
                {
                    list_buffdata.AddRange(skill.MySkill.Effect_Target.Buffs);
                }
                catch { }


                foreach (Skill_Extended sex in skill.AllExtendeds)
                {
                    foreach(BuffTag btg in sex.TargetBuff)
                    {
                        list_buffdata.Add(btg.BuffData);
                    }
                }
                if(list_buffdata.Find(a=>a.Debuff && (a.BuffTag.Key== GDEItemKeys.BuffTag_CrowdControl || a.BuffTag.Key == GDEItemKeys.BuffTag_Debuff))!=null)
                {
                    list_skill.Add(skill);
                }
            }

            
            if (list_skill.Count<=0)
            {
                BattleSystem.instance.AllyTeam.Draw();
            }
            else
            {
                Skill_Extended sex=new Skill_Extended();
                sex.PlusSkillStat.HIT_CC = 50;
                sex.PlusSkillStat.HIT_DEBUFF = 50;
                sex.PlusSkillStat.HIT_DOT = 50;
                list_skill[0].ExtendedAdd(sex);
                yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyTeam._ForceDraw(list_skill[0], null));
            }
            yield return null;
            yield break;
        }
    }

    public class Sex_dan_13 : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            this.SMaster = Targets[0];



            for (int i = 0; i < BattleSystem.instance.AllyTeam.Skills_UsedDeck.Count; i++)//将弃牌库中自己的技能洗入牌库
            {
                if (BattleSystem.instance.AllyTeam.Skills_UsedDeck[i].Master == Targets[0])
                {
                    int index = UnityEngine.Random.Range(0, BattleSystem.instance.AllyTeam.Skills_Deck.Count + 1);
                    BattleSystem.instance.AllyTeam.Skills_Deck.Insert(index, BattleSystem.instance.AllyTeam.Skills_UsedDeck[i]);
                    BattleSystem.instance.AllyTeam.Skills_UsedDeck.RemoveAt(i);
                    i--;
                }
            }



                BattleSystem.DelayInput(this.Delay());//抽2张牌


        }
        public IEnumerator Delay()
        {
            List<Skill> list = new List<Skill>();
            list.AddRange(this.BChar.MyTeam.Skills_Deck);

            if (list.Count != 0)
            {
                BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(this.Del), ScriptLocalization.System_SkillSelect.DrawSkill, false, true, true, false, true));//目前跳过了Del2，效果为抽二
            }

            yield return null;
            yield break;

        }

        public void Del(SkillButton Myskill)
        {
            if (Myskill.Myskill.Master == this.SMaster)
            {
                BattleSystem.instance.AllyTeam.Draw(Myskill.Myskill, new BattleTeam.DrawInput(this.Drawinput));

            }
            else
            {
                BattleSystem.instance.AllyTeam.Draw(Myskill.Myskill, null);

            }
        }

        // Token: 0x06000E08 RID: 3592 RVA: 0x000867CC File Offset: 0x000849CC
        public void Drawinput(Skill skill)
        {
            skill.ExtendedAdd(new Skill_Extended
            {
                APChange = -1
            });
        }
        private BattleChar SMaster;
    }

    public interface IP_EnforcePassive
    {
        // Token: 0x06000001 RID: 1
        bool EnforcePassive(BattleChar bc);
    }
    public class Sex_dan_14 : Sex_ImproveClone
    {
        /*
        public override void BattleStartDeck(List<Skill> Skills_Deck)
        {
            this.BChar.MyTeam.Draw(this.MySkill, null);
        }
        */
    }
    public class Bcl_dan_14 : InBuff, IP_EnforcePassive
    {
        public override bool GetForce()
        {
            return true;
        }
        public bool EnforcePassive(BattleChar bc)
        {
            if(bc==this.BChar)
            {
                return true;
            }
            return false;
        }
    }







    public class SkillEn_GFL_Dandelion_0 : Sex_ImproveClone//, IP_SkillUse_Target
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return (MainSkill.TargetTypeKey == "enemy" || MainSkill.TargetTypeKey == "random_enemy" || MainSkill.TargetTypeKey == "all_onetarget" || MainSkill.TargetTypeKey == "all_enemy");
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            foreach (BattleChar hit in Targets)
            {
                foreach (Buff buff in hit.Buffs)
                {
                    //Debug.Log("test0_3");
                    if (buff.BuffData.Debuff && !buff.IsHide)
                    {
                        int lifetime = (int)buff.BuffData.LifeTime;
                        foreach (StackBuff sta in buff.StackInfo)
                        {
                            sta.RemainTime = Math.Max(lifetime, sta.RemainTime);
                        }
                    }
                }
            }
        }
        /*
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            //Debug.Log("test0_1");
            if(SP.SkillData.AllExtendeds.Find(a=>a==this)!=null)
            {
                //Debug.Log("test0_2");
                foreach (Buff buff in hit.Buffs)
                {
                    //Debug.Log("test0_3");
                    if (buff.BuffData.Debuff && !buff.IsHide)
                    {
                        int lifetime = (int)buff.BuffData.LifeTime;
                        foreach (StackBuff sta in buff.StackInfo)
                        {
                            sta.RemainTime = Math.Max(lifetime, sta.RemainTime);
                        }
                    }
                }

            }

        }*/
    }

    public class SkillEn_GFL_Dandelion_1 : Sex_ImproveClone, IP_DamageChange, IP_GetCriPer_After
    {

        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return (MainSkill.TargetTypeKey == "enemy" || MainSkill.TargetTypeKey == "random_enemy" || MainSkill.TargetTypeKey == "all_onetarget") && MainSkill.IsDamage;
        }


        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if(Target!=null &&SkillD.AllExtendeds.Find(a=>a==this) != null)
            {
                int per = 0;
                if (Target.Buffs.Find(a=>!a.IsHide && a.BuffData.Debuff && a.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_CrowdControl)!=null)//GetBuffs(BattleChar.GETBUFFTYPE.CC, false, false).Count != 0)
                {
                    per += 1;
                }
                if (Target.Buffs.Find(a => !a.IsHide && a.BuffData.Debuff && a.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_Debuff) != null)//GetBuffs(BattleChar.GETBUFFTYPE.CC, false, false).Count != 0)
                {
                    per += 1;
                }
                if (Target.Buffs.Find(a => !a.IsHide && a.BuffData.Debuff && a.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT) != null)//GetBuffs(BattleChar.GETBUFFTYPE.CC, false, false).Count != 0)
                {
                    per += 1;
                }
                //Debug.Log("per=" + per);
                if(per>0)
                {
                    return (int)((1 + 0.1 * per) * Damage);
                }
            }
            return Damage;
        }
        
        
        public void GetCriPer_After(Skill skill, BattleChar Target, int Plus, ref int result)
        {
            if (Target != null && skill.AllExtendeds.Find(a => a == this) != null)
            {

                if (Target.Buffs.Find(a => !a.IsHide && a.BuffData.Debuff && a.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_CrowdControl) != null)//GetBuffs(BattleChar.GETBUFFTYPE.CC, false, false).Count != 0)
                {
                    result += 33;
                }
                if (Target.Buffs.Find(a => !a.IsHide && a.BuffData.Debuff && a.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_Debuff) != null)//GetBuffs(BattleChar.GETBUFFTYPE.CC, false, false).Count != 0)
                {
                    result += 33;
                }
                if (Target.Buffs.Find(a => !a.IsHide && a.BuffData.Debuff && a.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT) != null)//GetBuffs(BattleChar.GETBUFFTYPE.CC, false, false).Count != 0)
                {
                    result += 33;
                }
                //Debug.Log("CriPerChange" );
            }
        }
        
    }

    public class E_dan_0 : EquipBase,IP_Equip_Dandelion,IP_PlayerTurn
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.HIT_CC = 15;
            this.PlusStat.HIT_DEBUFF = 15;
            this.PlusStat.HIT_DOT = 15;
            this.PlusStat.RES_CC = 15;
            this.PlusStat.RES_DEBUFF = 15;
            this.PlusStat.RES_DOT = 15;
        }
        public bool Equip_Dandelion(BattleChar bchar)
        {
            //Debug.Log("战术核心");
            if(bchar==this.BChar)
            {
                return true;
            }
            return false;
        }
        
        public void Turn()
        {
            int n = this.BChar.Buffs.FindAll(a => !a.IsHide && !a.BuffData.Debuff).Count;
            if(n>0)
            {
                {
                    Buff buff = this.BChar.BuffAdd("B_GFL_Dandelion_E", this.BChar, false, 0, false, -1, false);
                    buff.BarrierHP += (n*3);
                }
            }
        }
    }
    public class Bcl_dan_E : Buff
    {

    }

    public class E_dan_1 : EquipBase,IP_BuffAddAfter
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.HIT_CC = 25;
            this.PlusStat.HIT_DEBUFF = 25;
            this.PlusStat.HIT_DOT = 25;
            this.PlusStat.RES_CC = 25;
            this.PlusStat.RES_DEBUFF = 25;
            this.PlusStat.RES_DOT = 25;
        }
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if(BuffUser==this.BChar && stackBuff.RemainTime>0)
            {
                stackBuff.RemainTime++;
                //BattleSystem.instance.StartCoroutine(this.Delay(stackBuff));
            }
        }
        public IEnumerator Delay(StackBuff stackBuff)
        {
            yield return new WaitForFixedUpdate();
            stackBuff.RemainTime=2*stackBuff.RemainTime;
            yield break;
        }
    }
}
