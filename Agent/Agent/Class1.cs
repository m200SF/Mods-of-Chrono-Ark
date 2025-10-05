using GameDataEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using ChronoArkMod;
using System.Net;
using System.Runtime.InteropServices;
using ChronoArkMod.Plugin;
using HarmonyLib;
using System.Reflection.Emit;
using System.Reflection;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Security.Policy;
using I2.Loc;
using ChronoArkMod.ModData.Settings;
using DarkTonic.MasterAudio;
using SF_Standard;
using System.Xml.Serialization;
using ChronoArkMod.ModData;
using ChronoArkMod.Template;

namespace SF_Agent
{
    [PluginConfig("M200_SF_Agent_CharMod", "SF_Agent_CharMod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class SF_Agent_CharacterModPlugin : ChronoArkPlugin
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
        public class AgentDef : ModDefinition
        {

        }

    }
    /*
    public class S_SF_Agent_9_2_1_Particle : CustomSkillGDE<SF_Agent_CharacterModPlugin.AgentDef>
    {
        // Token: 0x060000AB RID: 171 RVA: 0x000055A0 File Offset: 0x000037A0
        public override ModGDEInfo.LoadingType GetLoadingType()
        {
            return ModGDEInfo.LoadingType.Replace;
        }

        // Token: 0x060000AC RID: 172 RVA: 0x000055B4 File Offset: 0x000037B4
        public override string Key()
        {
            return "S_SF_Agent_9_2_1";
        }

        // Token: 0x060000AD RID: 173 RVA: 0x000055CB File Offset: 0x000037CB
        public override void SetValue()
        {
            base.Particle = base.assetInfo.CodeConstuctAsync<GameObject>("Particle/Enemy/S2_MBoss0_0", new ModAssetInfo.ObjectChangeDelegate<GameObject>(this.ParticleChangeDelegate));//"Particle/Twins/Hel_2"
        }

        // Token: 0x060000AE RID: 174 RVA: 0x000055F4 File Offset: 0x000037F4
        public void ParticleColorChange(Transform target, Color color)
        {
            ParticleSystem ps = target.GetComponent<ParticleSystem>();
            var main = ps.main;
            main.startColor = color;

        }

        // Token: 0x060000AF RID: 175 RVA: 0x00005620 File Offset: 0x00003820
        public GameObject ParticleChangeDelegate(GameObject particle)
        {

            if (BattleSystem.instance == null)
            {
                particle.SetActive(false);

            }

            SkillParticle skillParticle = (particle != null) ? particle.GetComponent<SkillParticle>() : null;
            skillParticle.GroundOut = true;

            return particle;

        }

    }
    */


    [HarmonyPatch(typeof(PrintText))]
    [HarmonyPatch("TextInput")]
    public static class PrintTestPlugin
    {
        // Token: 0x06000014 RID: 20 RVA: 0x00002310 File Offset: 0x00000510
        [HarmonyPrefix]
        public static void TextInput_Before_patch(PrintText __instance, ref string inText)
        {
            //inText = "test";
            //MasterAudio.PlaySound("A_agt_4", 1f, null, 0f, null, null, false, false);
            float volume = 0f;
            float volume2 = 0f;
            if (ModManager.getModInfo("SF_Agent").GetSetting<ToggleSetting>("AudioOn").Value)
            {
                volume = ModManager.getModInfo("SF_Agent").GetSetting<SliderSetting>("AudioVolume").Value;
            }
            else
            {
                volume = 0f;
            }

            if (inText.Contains("T_agt_1_1"))
            {
                inText = inText.Replace("T_agt_1_1:", string.Empty);
                MasterAudio.PlaySound("A_agt_1_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_agt_1_2"))
            {
                inText = inText.Replace("T_agt_1_2:", string.Empty);
                MasterAudio.PlaySound("A_agt_1_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_agt_2_1"))
            {
                inText = inText.Replace("T_agt_2_1:", string.Empty);
                MasterAudio.PlaySound("A_agt_2_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_agt_3_1"))
            {
                inText = inText.Replace("T_agt_3_1:", string.Empty);
                MasterAudio.PlaySound("A_agt_3_1", volume, null, 1f, null, null, false, false);
            }
            else if (inText.Contains("T_agt_4_1"))
            {
                inText = inText.Replace("T_agt_4_1:", string.Empty);
                MasterAudio.PlaySound("A_agt_4_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_agt_5_1"))
            {
                inText = inText.Replace("T_agt_5_1:", string.Empty);
                MasterAudio.PlaySound("A_agt_5_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_agt_6_1"))
            {
                inText = inText.Replace("T_agt_6_1:", string.Empty);
                MasterAudio.PlaySound("A_agt_6_1", volume, null, 1f, null, null, false, false);
            }
            else if (inText.Contains("T_agt_6_2"))
            {
                inText = inText.Replace("T_agt_6_2:", string.Empty);
                MasterAudio.PlaySound("A_agt_6_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_agt_7_1"))
            {
                inText = inText.Replace("T_agt_7_1:", string.Empty);
                MasterAudio.PlaySound("A_agt_7_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_agt_8_1"))
            {
                inText = inText.Replace("T_agt_8_1:", string.Empty);
                MasterAudio.PlaySound("A_agt_8_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_agt_9_1"))
            {
                inText = inText.Replace("T_agt_9_1:", string.Empty);
                MasterAudio.PlaySound("A_agt_9_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_agt_10_1"))
            {
                inText = inText.Replace("T_agt_10_1:", string.Empty);
                MasterAudio.PlaySound("A_agt_10_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_agt_11_1"))
            {
                inText = inText.Replace("T_agt_11_1:", string.Empty);
                MasterAudio.PlaySound("A_agt_11_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_agt_11_2"))
            {
                inText = inText.Replace("T_agt_11_2:", string.Empty);
                MasterAudio.PlaySound("A_agt_11_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_agt_11_3"))
            {
                inText = inText.Replace("T_agt_11_3:", string.Empty);
                MasterAudio.PlaySound("A_agt_11_3", volume, null, 0f, null, null, false, false);
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
            bool flag = inputitem.itemkey == "E_SF_Agent_0" && __instance.Info.KeyData != "SF_Agent";
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

            if (CharId == "SF_Agent")
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


    [HarmonyPatch(typeof(SKillCollection))]
    public class SKillCollection_Plugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("Init")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var methodInfo = AccessTools.Method(typeof(SKillCollection_Plugin), nameof(SKillCollection_Plugin.RemoveAgentDoll), new System.Type[] { typeof(List<string>) });


            var codes = instructions.ToList();
            for (int i = 0; i < codes.Count; i++)
            {
                if(i>=4)
                {
                    bool fm4 = codes[i - 4].opcode == OpCodes.Ldsfld;
                    bool fm3 = codes[i - 3].opcode == OpCodes.Ldarg_0;
                    bool fm2 = codes[i - 2].opcode == OpCodes.Ldflda;
                    bool fm1 = codes[i - 1].opcode == OpCodes.Call;
                    bool fm0 = codes[i].opcode == OpCodes.Pop;

                    bool fm4_2 = false;
                    if (codes[i - 4].operand is FieldInfo && ((FieldInfo)(codes[i - 4].operand)).Name.Contains("Character"))
                    {
                        fm4_2= true;
                    }
                    bool fm1_2 = false;
                    if (codes[i - 1].operand is MethodInfo && ((MethodInfo)(codes[i - 1].operand)).Name.Contains("GetAllDataKeysBySchema")) 
                    {
                        fm1_2 = true ;
                    }
                    if (fm4 && fm4_2 && fm3 && fm2 && fm1 && fm1_2 && fm0) 
                    {
                      //Debug.Log("找到修改点" + i);
                      //Debug.Log("找到修改点" + i);
                      //Debug.Log("找到修改点" + i);
                      //Debug.Log("找到修改点" + i);
                      //Debug.Log("找到修改点" + i);

                        yield return codes[i];
                        
                        yield return new CodeInstruction(OpCodes.Ldarg_0);
                        yield return new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(SKillCollection), "CharKeys"));
                        yield return new CodeInstruction(OpCodes.Call, methodInfo);
                        
                        yield return new CodeInstruction(OpCodes.Pop);
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


        public static bool RemoveAgentDoll(List<string> charkeys)
        {
          //Debug.Log("检测代理人召唤物");
            for (int i = 0;i<charkeys.Count;i++)
            {
                if (SKillCollection_Plugin.ModID(charkeys[i]) == "SF_Agent" && charkeys[i] != "SF_Agent")
                {
                    charkeys.RemoveAt(i);
                    i--;
                }
                /*
                GDECharacterData allyData = new GDECharacterData(charkeys[i]);
                Passive_Char passive = new Passive_Char();
                Type type = ModManager.GetType(allyData.PassiveClassName);
                if (type != null)
                {
                    passive = (Passive_Char)Activator.CreateInstance(type);
                }
                if(passive is P_Agent_Doll)
                {
                  //Debug.Log("移除代理人召唤物");
                    charkeys.RemoveAt(i);
                    i--;
                }
                */
            }
            return true;
        }
        public static string ModID(string key)
        {
            if (ToolTipWindow.ModInfoCheck(key))
            {
                foreach (string id in ModManager.LoadedMods)
                {
                    if (ModManager.getModInfo(id).gdeinfo.Added.Contains(key))
                    {
                        //return ModManager.getModInfo(id).Author;
                        return ModManager.getModInfo(id).id;
                    }
                }
            }
            return string.Empty;
        }
    }

    [HarmonyPatch(typeof(SkillButton))]
    public class SkillButton_Plugin//修复原版游戏【选择技能】类技能选择【选择技能】类技能时无反应的bug
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("ChoiceSkill")]
        [HarmonyPostfix]
        public static void ChoiceSkill_After(SkillButton Mybutton)
        {
            if (Mybutton.Myskill.TargetTypeKey == GDEItemKeys.s_targettype_choiceskill)
            {

            
                Skill skill = new Skill();
                skill = Mybutton.Myskill.CloneSkill(true, null, null, true);
                //skill.FreeUse = true;
                if (BattleSystem.instance.SelectedSkill.NotCount)
                {
                    skill.NotCount = true;
                }

                skill.OriginalSelectSkill = BattleSystem.instance.SelectedSkill;

                Skill_Extended sex = new Skill_Extended();
                sex.APChange = skill.OriginalSelectSkill.AP - skill.AP;
                skill.AllExtendeds.Add(sex);

                BattleSystem.instance.TargetSelecting = true;
                BattleSystem.instance.SelectedSkill = skill;
                BattleSystem.instance.ActionAlly = (skill.Master as BattleAlly);
                List<Skill> choiceSkillList = skill.ChoiceSkillList;
                foreach (Skill skill2 in choiceSkillList)
                {
                    skill2.CloneOther_AllExtendeds(skill, true);
                }
                BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(choiceSkillList, new SkillButton.SkillClickDel(SkillButton.ChoiceSkill), ScriptLocalization.System_SkillSelect.EffectSelect, true, false, true, false, true));
            }
        }
    }
    
    [HarmonyPatch(typeof(BattleSystem))]
    public class targetSelect_Plugin//修复原版游戏【选择技能】类技能中选择非【无指向】技能的情况下按选择的技能扣费的bug
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("TargetSelect")]
        [HarmonyPrefix]
        public static void TargetSelect_before(ref Skill sdata, BattleChar Char)
        {

            if (sdata.OriginalSelectSkill!=null)
            {
                //Debug.Log("初始技能AP:" + sdata.OriginalSelectSkill.AP);
                //Debug.Log("原衍生技能AP:" + sdata.AP);
                
                //int n = 0;
                //while (sdata.OriginalSelectSkill.AP != sdata.AP && n<100)
                //{
                    Skill_Extended sex = new Skill_Extended();
                    sex.APChange = 999+Math.Max(0,-sdata._AP);
                    sdata.AllExtendeds.Add(sex);
                    sex.APChange += (sdata.OriginalSelectSkill.AP - sdata.AP);
                sdata.OriginalSelectSkill.UsedApNum = sdata.OriginalSelectSkill.AP;
                    //n++;
                //}
                //Debug.Log(sdata._AP);
                //Debug.Log("n:" + n);
                //Debug.Log("衍生技能AP_last:" + sdata.AP);
            }
        }
    }



    [HarmonyPatch(typeof(Skill))]
    public class ChoiceSkillList_Plugin//修改游戏【选择技能】类技能的技能列表
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("get_ChoiceSkillList")]
        [HarmonyPostfix]
        public static void ChoiceSkillList_Patch_After(Skill __instance,ref List<Skill> __result)
        {

            foreach (IP_ChoiceSkillList_After ip_ChoiceSkillList_After in __instance.IReturn<IP_ChoiceSkillList_After>())
            {
                if(ip_ChoiceSkillList_After!=null)
                {
                    ip_ChoiceSkillList_After.ChoiceSkillList_After(ref __result);
                }
            }
        }
    }
    
    public interface IP_ChoiceSkillList_After
    {
        // Token: 0x06002769 RID: 10089
        void ChoiceSkillList_After(ref List<Skill> result);
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


    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("Damage")]
    public static class DamagePlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void BattleChar_Damage_Before_patch(BattleChar __instance, BattleChar User, int Dmg, bool Cri, ref bool Pain, ref bool NOEFFECT, int PlusPenetration, bool IgnoreHealingPro, bool HealingPro, bool OnlyUnscaleTime)
        {
            if (__instance.Info.Ally)
            {
                foreach (IP_Damage_Before ip_before in __instance.IReturn<IP_Damage_Before>())
                {
                    bool flag = ip_before != null;
                    if (flag)
                    {
                        ip_before.Damage_Before(__instance, User, Dmg, Cri, ref Pain, ref NOEFFECT, PlusPenetration, IgnoreHealingPro, HealingPro, OnlyUnscaleTime);
                    }
                }
                //Debug.Log("成功进入OnlyDamage前的patch");
            }

        }
        //private int recBefore;
    }
    public interface IP_Damage_Before
    {
        // Token: 0x06000001 RID: 1
        void Damage_Before(BattleChar battleChar, BattleChar User, int Dmg, bool Cri, ref bool Pain, ref bool NOEFFECT, int PlusPenetration, bool IgnoreHealingPro, bool HealingPro, bool OnlyUnscaleTime);
    }













    public static class AllySummon
    {
        public static BattleAlly NewAlly(string charKey,Agent_Sum_Extended extended)
        {
            
            Character character = new Character();

            GDECharacterData gde = new GDECharacterData(charKey);

                character.Set_AllyData(gde);

            //gde.Role = new GDECharRoleData("Role_TempAlly");
            

            //extended.PassiveStatSet(character.Passive, extended.BChar);
            //character.LV = 6;
            bool nobasic = false;
            try
            {
                if(!extended.GetBasicKey().IsNullOrEmpty())
                {
                  //Debug.Log("有固定技能");
                    character.BasicSkill = new CharInfoSkillData(extended.GetBasicKey());

                }
                else
                {
                  //Debug.Log("无固定技能");
                    character.BasicSkill = new CharInfoSkillData("S_SF_Agent_0");
                    nobasic = true;
                }
            }
            catch
            {
              //Debug.Log("固定技能报错");
            }
            //Debug.Log("NewAlly_2");
            BattleAlly battleAlly = BattleSystem.instance.CreatTempAlly(character);
            bool flag2 = battleAlly.BasicSkill != null;
            if ( flag2)
            {
                battleAlly.MyBasicSkill.SkillInput(battleAlly.BasicSkill.CloneSkill(true, null, null, false));
                battleAlly.BattleBasicskillRefill = battleAlly.BasicSkill.CloneSkill(true, null, null, false);
            }
            int num = BattleSystem.instance.AllyTeam.Chars.IndexOf(battleAlly);
            bool flag3 = num >= 0;
            if (flag2 &&flag3)
            {
                BattleSystem.instance.AllyTeam.Skills_Basic = BattleSystem.instance.AllyTeam.Skills_Basic.SetAt(num, battleAlly.BasicSkill.CloneSkill(true, null, null, false));
            }
            
            if(nobasic)
            {
                battleAlly.MyBasicSkill.InActive = true;
                battleAlly.MyBasicSkill.CoolDownNum = -1;
                //battleAlly.MyBasicSkill.buttonData = null;

            }
            
            battleAlly.Info.Equip.Clear();

            if(battleAlly.Info.Passive is P_Agent_Doll dollpas)
            {
                dollpas.DollAwake(extended.BChar);
            }
                
            
            /*
            GDECharacterData allyData = new GDECharacterData(charKey);
            Character character = new Character();
            character.Set_AllyData(allyData);
            character.BasicSkill = new CharInfoSkillData(character.GetData.FixedBasicSkill.Key);
            BattleAlly battleAlly = BattleSystem.instance.CreatTempAlly(character);
            Skill skill = Skill.TempSkill(character.GetData.FixedBasicSkill.Key, battleAlly, BattleSystem.instance.AllyTeam);
            battleAlly.MyBasicSkill.SkillInput(skill.CloneSkill(true, null, null, false));
            battleAlly.BattleBasicskillRefill = skill.CloneSkill(true, null, null, false);
            int num = 0;
            for (int i = 0; i < BattleSystem.instance.AllyTeam.Chars.Count; i++)
            {
                if (BattleSystem.instance.AllyTeam.Chars[i] == battleAlly)
                {
                    num = i;
                }
            }
            BattleSystem.instance.AllyTeam.Skills_Basic[num] = skill.CloneSkill(true, null, null, false);
            //battleAlly.BuffAdd(GDEItemKeys.Buff_B_AllyDoll_Hp0Effect, battleAlly, false, 999, false, -1, false);
            character.Equip.Clear();
            */

            //Buff buff=battleAlly.BuffAdd("B_SF_Agent_sum_p", battleAlly, false, 0, false, -1, false);
            //(buff as Bcl_agt_sum_p).user = extended.BChar;
            BattleSystem.instance.StartCoroutine(AllySummon.DelaySetPos());

          //Debug.Log("NewAlly_3");
            return battleAlly;
        }
        private static IEnumerator DelaySetPos()
        {
            yield return new WaitForEndOfFrame();
            AllySummon.SetAlliesTransform();
            yield break;
        }
        public static void SetAlliesTransform()
        {
            List<BattleChar> chars = BattleSystem.instance.AllyTeam.Chars;
            bool flag = chars.Count < 0;
            if (!flag)
            {
                Transform parent = chars[0].transform.parent;
                int num = parent.Cast<Transform>().Count((Transform child) => child.gameObject.activeSelf);
                bool flag2 = num < 0;
                if (!flag2)
                {
                    float num2 = (num <= 4) ? 1f : (4f / (float)num);
                    parent.localPosition = new Vector3(269f - 1000f * (1f - num2), parent.localPosition.y, parent.localPosition.z);
                    parent.localScale = new Vector3(num2, num2, 1f);
                }
            }
        }
        // Token: 0x0600002C RID: 44 RVA: 0x00002C38 File Offset: 0x00000E38
        public static void RemoveAlly(BattleAlly battleAlly)
        {
            int num = BattleSystem.instance.AllyTeam.Chars.IndexOf(battleAlly);
            bool flag = num >= 0;
            if (flag)
            {
                BattleSystem.instance.AllyTeam.Chars.RemoveAt(num);
                BattleSystem.instance.AllyTeam.Skills_Basic.RemoveAt(num);
            }
            UnityEngine.Object.Destroy(battleAlly.gameObject);
            BattleSystem.instance.StartCoroutine(AllySummon.DelaySetPos());
        }
        
        public static T[] SetAt<T>(this T[] arr, int index, T value)
        {
            bool flag = index < 0;
            T[] result;
            if (flag)
            {
                result = arr;
            }
            else
            {
                bool flag2 = index < arr.Length - 1;
                if (flag2)
                {
                    arr[index] = value;
                    result = arr;
                }
                else
                {
                    T[] array = new T[index + 1];
                    Array.Copy(arr, array, arr.Length);
                    array[index] = value;
                    result = array;
                }
            }
            return result;
        }
        public static void RemoveAt<T>(this T[] arr, int index)
        {
            for (int i = index; i < arr.Length - 1; i++)
            {
                arr[i] = arr[i + 1];
            }
            arr[arr.Length - 1] = default(T);
        }


        public static Stat OrgStat(BattleChar bc)
        {
            Stat G_get_stat= default(Stat);

            {
                G_get_stat = bc.Info.OriginStat;
                Stat stat = default(Stat);
                PerStat perStat = default(PerStat);
                if (bc.Info.Ally)
                {
                    G_get_stat += bc.Info.AllyLevelPlusStat(0);
                    stat += PlayData.TSavedata.PartyPlusStat;
                    if (PlayData.TSavedata.SpRule != null)
                    {
                        stat += PlayData.TSavedata.SpRule.PlusStat;
                    }
                    foreach (Item_Passive item_Passive in PlayData.Passive)
                    {
                        if (item_Passive != null)
                        {
                            stat += item_Passive.ItemScript.PlusStat;
                            perStat += item_Passive.ItemScript.PlusPerStat;
                            stat.PlusDiscard = 0;
                            stat.PlusDraw = 0;
                            stat.MPR = 0;
                            stat.spd = 0;
                        }
                    }
                    if (PlayData.TSavedata.HopeModeLevel == 1)
                    {
                        perStat.MaxHP += 50;
                        stat.hit += 10f;
                    }
                    else if (PlayData.TSavedata.HopeModeLevel == 2)
                    {
                        perStat.MaxHP += 66;
                        perStat.Damage += 25;
                        perStat.Heal += 25;
                        stat.hit += 10f;
                    }
                    else if (PlayData.TSavedata.HopeModeLevel == 3)
                    {
                        perStat.MaxHP += 100;
                        perStat.Damage += 33;
                        perStat.Heal += 33;
                        stat.hit += 10f;
                    }
                    for (int i = 0; i < PlayData.TSavedata.Party.Count; i++)
                    {
                        if (PlayData.TSavedata.Party[i] != null && PlayData.TSavedata.Party[i].Equip != null)
                        {
                            for (int j = 0; j < PlayData.TSavedata.Party[i].Equip.Count; j++)
                            {
                                if (bc.Info.GetData != null && PlayData.TSavedata.Party[i].Equip[j] != null)
                                {
                                    (PlayData.TSavedata.Party[i].Equip[j] as Item_Equip).ItemScript.EquipPlusStat_Team(bc.Info, ref stat, ref perStat);
                                }
                            }
                        }
                    }
                    if (bc.Info.Passive != null)
                    {
                        stat += bc.Info.Passive.PlusStat;
                        perStat += bc.Info.Passive.PlusPerStat;
                    }
                    for (int k = 0; k < bc.Info.Equip.Count; k++)
                    {
                        if (bc.Info.Equip[k] != null && bc.Info.Equip[k] is Item_Equip)
                        {
                            Item_Equip item_Equip = bc.Info.Equip[k] as Item_Equip;
                            if (item_Equip.ItemScript.MyChar == null)
                            {
                                item_Equip.ItemScript.MyChar = bc.Info;
                                item_Equip.Curse.MyChar = bc.Info;
                                item_Equip.Curse.Init();
                            }
                            item_Equip.ItemScript.MyChar = bc.Info;
                            if (!item_Equip.InitFlag)
                            {
                                item_Equip.ItemScript.Init();
                                item_Equip.InitFlag = true;
                            }
                            stat += item_Equip.ItemScript.PlusStat;
                            stat += item_Equip.Enchant.EnchantData.PlusStat;
                            stat += item_Equip.Curse.PlusStat;
                            perStat += item_Equip.ItemScript.PlusPerStat;
                            perStat += item_Equip.Enchant.EnchantData.PlusPerStat;
                            perStat += item_Equip.Curse.PlusPerStat;
                            item_Equip._Isidentify = true;
                            if (PlayData.Passive.Find((Item_Passive a) => a.itemkey == GDEItemKeys.Item_Passive_SixSixSix) != null && item_Equip.IsCurse)
                            {
                                stat.atk += 2f;
                                stat.reg += 2f;
                                stat.DeadImmune += 25;
                            }
                        }
                    }
                    if (bc.Info.Hp <= 0)
                    {
                        stat.AggroPer += 20;
                    }
                }
                
                stat.atk += Misc.PerToNum(G_get_stat.atk, (float)perStat.Damage);
                stat.reg += Misc.PerToNum(G_get_stat.reg, (float)perStat.Heal);
                stat.maxhp += (int)Misc.PerToNum((float)G_get_stat.maxhp, (float)perStat.MaxHP);
                G_get_stat += stat;
                G_get_stat = Character.StatC(G_get_stat);
                /*
                if (bc.Info.BeforeMaxHP != 0 && bc.Info.BeforeMaxHP != G_get_stat.maxhp)
                {
                    if (bc.Info.Hp >= 1)
                    {
                        int num = (int)Math.Round((double)Misc.PerToNum((float)bc.Info.Hp, Misc.NumToPer((float)bc.Info.BeforeMaxHP, (float)G_get_stat.maxhp)));
                        bc.Info.Hp = ((num > 1) ? num : 1);
                    }
                    if (BattleSystem.instance != null && bc.Info.GetBattleChar != null)
                    {
                        int num2 = (int)Misc.PerToNum((float)bc.Info.GetBattleChar.Recovery, Misc.NumToPer((float)bc.Info.BeforeMaxHP, (float)G_get_stat.maxhp));
                        if (bc.Info.GetBattleChar.Recovery >= 1)
                        {
                            bc.Info.GetBattleChar.Recovery = ((num2 > 1) ? num2 : 1);
                        }
                        else
                        {
                            bc.Info.GetBattleChar.Recovery = num2;
                        }
                    }
                }
                */
            }
            return G_get_stat;
        }

    }

    public class Agent_Sum_Extended: Sex_ImproveClone
    {
        public override bool ButtonSelectTerms()
        {
            //return this.BChar.Overload < 3 && this.BChar.HP>0;
            return P_Agent_Doll.AllSummoner(this.BChar).FindAll(a=>a.Overload >= 3).Count<=0 && this.BChar.HP > 0;//改为根源召唤者
        }

        public override string DescExtended(string desc)
        {
            //Debug.Log("point_2_1");
            string str2 = string.Empty;
            //int n = 0;
            foreach (BattleChar bc in P_Agent_Doll.AllSummoner(this.BChar))
            {
                if(bc==this.BChar)
                {
                    continue;
                }
                //if (n > 0)
                //{
                    str2 = str2 + "&";
                //}
                str2 = str2 + bc.Info.Name;
                //n++;
            }
            //Debug.Log("point_2_2");
            GDEBuffData gde = new GDEBuffData("B_SF_Agent_sum_p");
            string str = gde.Description.Replace("&consumHP", this.ConsumHP().ToString()).Replace("&allSummoner",str2);//改为根源召唤者
            //Debug.Log("point_2_3");
            //GDEBuffData gde = new GDEBuffData("B_SF_Agent_sum_p");
            //string str = gde.Description.Replace("&consumHP", this.ConsumHP().ToString());

            return str+base.DescExtended(desc);
        }
        public int ConsumHP()
        {
            double num1 = this.GetMaxHp(P_Agent_Doll.FirstSummoner(this.BChar));//改为根源召唤者
            //double num1 = this.GetMaxHp(this.BChar);
            //Debug.Log("num1:" + num1);
            double k1 = 12;
            double k2 = 10;
            double num2 = (int)(k1 * Math.Log(1 + num1 / k2));
            //Debug.Log("num2:" + num2);
            return (int)num2;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            
            foreach (BattleChar bc in P_Agent_Doll.AllSummoner(this.BChar))//改为根源召唤者
            {
                bc.Overload ++;
            }
            
            //this.BChar.Overload++;

            this.BChar.Damage(this.BChar, this.ConsumHP(), false, true, true, 0, false, false, false);

            base.SkillUseSingle(SkillD, Targets);
            string summonkey=this.GetAllyKey();
            BattleAlly bally=AllySummon.NewAlly(summonkey,this);

            List<string>list=new List<string>();
            /*
            list.AddRange(this.GetPassiveBuff());
            foreach (string str in list)
            {
                bally.BuffAdd(str, bally, false, 0, false, -1, false);
            }
            */
            list.AddRange(this.GetUserBuff());
            foreach (string str in list)
            {
                bally.BuffAdd(str, this.BChar, false, 0, false, -1, false);
            }

            //BattleExtended_Shiranui test = new BattleExtended_Shiranui();
            //test.BattleStartUIOnBefore(BattleSystem.instance);
        }

        public virtual string GetAllyKey()
        {
            if(!this.allykey.IsNullOrEmpty())
            {
                return this.allykey;
            }
            else
            {
                return this.BChar.Info.KeyData;
            }
            
        }
        /*
        public virtual List<string> GetPassiveBuff()
        {
            return new List<string>();
        }
        */
        public virtual List<string> GetUserBuff()
        {
            return new List<string>();
        }
        
        public virtual string GetBasicKey()
        {
            return "S_DefultSkill_2";
        }

        public virtual int GetMaxHp(BattleChar bc)
        {
            Stat stat = AllySummon.OrgStat(bc);
            return stat.maxhp;
        }
        /*

        public virtual void PassiveStatSet(Passive_Char passive, BattleChar battlechar)//弃用
        {
          //Debug.Log("virtual PassiveStatSet_1");
            Stat stat=AllySummon.OrgStat(battlechar);
            passive.PlusStat.atk = stat.atk;
            passive.PlusStat.hit = stat.hit;
            passive.PlusStat.def = stat.def;
            passive.PlusStat.dod = stat.dod;
            passive.PlusStat.reg = stat.reg;
            passive.PlusStat.cri = stat.cri;
            passive.PlusStat.HIT_CC = stat.HIT_CC;
            passive.PlusStat.HIT_DEBUFF = stat.HIT_DEBUFF;
            passive.PlusStat.HIT_DOT = stat.HIT_DOT;
            passive.PlusStat.RES_CC = stat.RES_CC;
            passive.PlusStat.RES_DEBUFF = stat.RES_DEBUFF;
            passive.PlusStat.RES_DOT = stat.RES_DOT;

            passive.PlusStat.maxhp = stat.maxhp ;
            passive.MyChar.Hp = passive.PlusStat.maxhp;

          //Debug.Log("virtual PassiveStatSet_2");
        }
        */
        public string allykey = "SF_Agent_shadow";//string.Empty;
    }

    public class Bcl_agt_sum_p : Buff,IP_Dead,IP_SomeOneDead
    {
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            if(this.BChar.Info.Passive is P_Agent_Doll pas)
            {
                this.master=pas.GetMaster;
            }
        }
        public void Dead()
        {
            if(this.BChar is BattleAlly ally)
            {
                //AllySummon.RemoveAlly(ally);
                BattleSystem.instance.StartCoroutine(this.RemoveUI());
            }
        }
        public void SomeOneDead(BattleChar DeadChar)
        {
            if(DeadChar==this.master)
            {
                (this.BChar as BattleAlly).ForceDead();
            }
        }
        public IEnumerator RemoveUI()
        {
            yield return new WaitForSeconds(1f);
            BattleAlly ally = this.BChar as BattleAlly;
            if (ally != null)
            {
                AllySummon.RemoveAlly(ally);
            }
            yield break;
        }
        public BattleChar master = new BattleChar();
    }
    public class Bcl_agt_doll : Buff,IP_PlayerTurn, IP_BuffOnPointerEnter, IP_BuffOnPointerExit
    {
        public void BuffOnPointerEnter(Buff buff)
        {
            //Debug.Log("buff point 1");
            if (this.BChar.Info.Passive is P_Agent_Doll pas && BattleSystem.instance.AllyTeam.AliveChars_Vanish.Find(a => a == pas.GetMaster)!=null)
            {
                TargetSelects.OneTarget(pas.GetMaster);
            }
        }
        public void BuffOnPointerExit(Buff buff)
        {
            if (buff == this)
            {
                TargetSelects.Clear(true);
            }
        }



        public override string DescExtended()
        {
            GDEBuffData gde = new GDEBuffData("B_SF_Agent_doll");
            string str = gde.Description;
            if (this.BChar.Info.Passive is P_Agent_Doll pas && BattleSystem.instance!=null)
            {
                
                string str2 = string.Empty;
                int n = 0;
                if(this.View)
                {
                    str2 = str2 + this.BChar.Info.Name;
                    n++;
                }
                foreach(BattleChar bc in pas.AllMaster())
                {

                    if(n > 0)
                    {
                        str2 = str2 + "&";
                    }
                    str2 = str2 +  bc.Info.Name ;
                    n++;
                }
                str = str.Replace("&master",pas.GetMaster.Info.Name).Replace("&allSummoner", str2);//改为根源召唤者
                
                //str = str.Replace("&master", pas.GetMaster.Info.Name);
            }
            else
            {
                GDECharacterData gde2 = new GDECharacterData("SF_Agent");
                str = str.Replace("&master", gde2.name).Replace("&allSummoner", gde2.name);//改为根源召唤者
            }
            return str + base.DescExtended();
        }
        public override void Init()
        {
            base.Init();
            this.PlusStat.CantOverHealing = true;
        }
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();

            Button button = this.BuffIcon.AddComponent<Button>();
            button.onClick.AddListener(new UnityAction(this.Go));

        }

        public void Go()//模式change
        {
            if (!BattleSystem.instance.DelayWait && BattleSystem.instance.Particles.Count == 0 && BattleSystem.instance.ActWindow.On)
            {
                (this.BChar as BattleAlly).ForceDead();
            }
            
        }
        public void Turn()
        {
            if(this.BChar.Info.Passive is P_Agent_Doll pas )
            {
                //pas.GetMaster.Overload++;//改为根源召唤者
                
                foreach (BattleChar bc in pas.AllMaster())
                {
                    bc.Overload++;
                }
                
            }
        }
        public virtual void TurnAttack()
        {

        }

        public void ActiveOverHeal()
        {

                //Debug.Log("设置可治疗");
                this.PlusStat.CantOverHealing = false;
            //int frame= (int)Traverse.Create(this.MyChar).Field("getstat_BeforeFrameCount").GetValue();
            //Traverse.Create(this.MyChar).Field("getstat_BeforeFrameCount").SetValue(frame-5);
            this.MyChar.ForceGetStat();
            Stat stat=this.MyChar.get_stat;
                BattleSystem.instance.StartCoroutine(this.ResetCantHeal());

        }
        public IEnumerator ResetCantHeal()
        {
            yield return new WaitForFixedUpdate();
            this.PlusStat.CantOverHealing = true;
            //Debug.Log("重置不可治疗");
            yield break;
        }
    }

    public class P_Agent : Passive_Char,IP_SomeOneDead, IP_LevelUp
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
                list.Add(ItemBase.GetItem("E_SF_Agent_0", 1));
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
        public  bool ItemTake2=false;
        public void SomeOneDead(BattleChar DeadChar)
        {

            if (DeadChar is BattleAlly ally && ally != this.BChar)
            {
                if (this.BChar.MyTeam.LucyChar.Overload > 0)
                {
                    this.BChar.MyTeam.LucyChar.Overload--;
                }
                foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars)
                {

                        if (bc.Overload > 0)
                        {
                            bc.Overload--;
                        }
                    
                }
            }
        }
        /*
        public void Turn()
        {
            this.BChar.Overload += BattleSystem.instance.AllyTeam.AliveChars_Vanish.FindAll(a => a.BuffFind("B_SF_Agent_sum_p", false)&& (a.BuffReturn("B_SF_Agent_sum_p", false) as Bcl_agt_sum_p).master==this.BChar).Count;
        }
        */
    }


    
    public class P_Agent_Doll : Passive_Char
    {

        public void DollAwake(BattleChar User)
        {
            /*
            if (User.Info.Passive is P_Agent_Doll dollpas)
            {
                this.master.AddRange(dollpas.master);
            }
            */
            this.master=User;
            this.masterStat = AllySummon.OrgStat(P_Agent_Doll.FirstSummoner(this.master));//改为根源召唤者
            //Debug.Log("最大生命值："+this.masterStat.maxhp);
            this.PassiveStatSet(P_Agent_Doll.FirstSummoner(this.master));//改为根源召唤者
            BattleSystem.DelayInput(this.PassiveAwake());
            
        }
        public IEnumerator PassiveAwake()
        {
            //Debug.Log("PassiveAwake_代理人召唤物");
            List<string> list = new List<string>();
            
            list.AddRange(this.GetSelfBuff());

            foreach (string str in list)
            {
                if(!this.BChar.BuffFind(str,false))
                {
                    this.BChar.BuffAdd(str, this.BChar, false, 0, false, -1, false);

                }
            }
            this.BChar.Recovery = this.BChar.HP;


            yield break;
        }
        public virtual List<string> GetSelfBuff()
        {
            List<string> list=new List<string>();
            list.Add("B_SF_Agent_sum_p");
            return list;
        }
        public virtual void PassiveStatSet(BattleChar battlechar)
        {
            //Debug.Log("virtual PassiveStatSet_1");
            
            this.PlusStat.atk = this.masterStat.atk;
            this.PlusStat.hit = this.masterStat.hit;
            this.PlusStat.def = this.masterStat.def;
            this.PlusStat.dod = this.masterStat.dod;
            this.PlusStat.reg = this.masterStat.reg;
            this.PlusStat.cri = this.masterStat.cri;
            this.PlusStat.PlusCriDmg= this.masterStat.PlusCriDmg;
            this.PlusStat.PlusCriHeal = this.masterStat.PlusCriHeal;
            this.PlusStat.DMGTaken = this.masterStat.DMGTaken;
            this.PlusStat.HEALTaken = this.masterStat.HEALTaken;
            this.PlusStat.HIT_CC = this.masterStat.HIT_CC;
            this.PlusStat.HIT_DEBUFF = this.masterStat.HIT_DEBUFF;
            this.PlusStat.HIT_DOT = this.masterStat.HIT_DOT;
            this.PlusStat.RES_CC = this.masterStat.RES_CC;
            this.PlusStat.RES_DEBUFF = this.masterStat.RES_DEBUFF;
            this.PlusStat.RES_DOT = this.masterStat.RES_DOT;

            this.PlusStat.maxhp = this.masterStat.maxhp;
            this.MyChar.Hp = this.PlusStat.maxhp;

            //Debug.Log("virtual PassiveStatSet_2");
        }
        //[XmlIgnore]
        public BattleChar GetMaster
        {
            get
            {
                return this.master;
            }
            //0704
            /*
            set
            {
                this.master = value;
            }
            */
        }
        //[XmlIgnore]
        public BattleChar GetOriginalMaster
        {
            get
            {
                BattleChar bc =this.BChar;
                while(bc.Info.Passive is P_Agent_Doll pas)
                {
                    bc = pas.master;
                }
                return bc;
            }
        }
        public static BattleChar FirstSummoner(BattleChar bc2)
        {

                BattleChar bc = bc2;
                while (bc.Info.Passive is P_Agent_Doll pas)
                {
                    bc = pas.master;
                }
                //Debug.Log("第一召唤者：" + bc.Info.Name);
                return bc;

        }
        public List<BattleChar> AllMaster()//返回所有上级召唤者
        {
            List<BattleChar> list = new List<BattleChar>();
            BattleChar bc = this.BChar;
            while (bc.Info.Passive is P_Agent_Doll pas)
            {
                list.Add(pas.master);
                bc = pas.master;
            }
            return list;
        }
        public static List<BattleChar> AllSummoner(BattleChar bc2)//返回所有上级召唤者与自身
        {
            List<BattleChar> list = new List<BattleChar>();
            BattleChar bc = bc2;
            list.Add(bc);
            while (bc.Info.Passive is P_Agent_Doll pas)
            {
                list.Add(pas.master);
                bc = pas.master;
            }
            return list;
        }
        //[XmlIgnore]
        public Stat GetStatOfMaster
        {
            get
            {
                return this.masterStat;
            }
            //0704
            /*
            set
            {
                this.masterStat = value;
            }
            */
        }
        //[XmlIgnore]
        private BattleChar master=new BattleChar();
        private Stat masterStat = default(Stat);
    }
    
    public class Agent_Doll_Basic_extended: Sex_ImproveClone
    {
        public override void Init()
        {
            base.Init();
            GDESkillData gde = new GDESkillData(this.MySkill.MySkill.KeyID);
            Traverse.Create(this.MySkill.MySkill).Field("_UseAp").SetValue(gde.UseAp-1);
            this.APChange = -1;
            this.MySkill.BasicSkill = true;
            /*
            if (BattleSystem.instance != null)
            {
                BattleSystem.instance.StartCoroutine(this.Delay());
            }
            */
        }
        public IEnumerator Delay()
        {
            yield return new WaitForFixedUpdate();
            if (this.MySkill.BasicSkill)
            {
                this.APChange = -1;
                //GDESkillData gde = new GDESkillData(this.MySkill.MySkill.KeyID);
                //Traverse.Create(this.MySkill.MySkill).Field("._UseAp").SetValue(gde.UseAp - 1);
            }

            yield break;
        }
    }


    public class Sex_agt_1 : Sex_ImproveClone
    {
        // Token: 0x060013CE RID: 5070 RVA: 0x0009C123 File Offset: 0x0009A323
        public override void Init()
        {
            base.Init();
            this.ChoiceSkillList = new List<string>();
            this.ChoiceSkillList.Add("S_SF_Agent_1_1");
            this.ChoiceSkillList.Add("S_SF_Agent_1_2");
        }
    }
    public class Sex_agt_1_1 : Sex_ImproveClone
    {

    }
    public class Bcl_agt_1_1 : Buff,IP_PlayerTurn
    {
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            this.Turn();
        }
        public void Turn()
        {
            /*
            Character character = new Character();
            GDECharacterData gde = new GDECharacterData("SF_Jaeger");
            character.Set_AllyData(gde);

            Sex_agt_1_2 sex= new Sex_agt_1_2();
            sex.PassiveStatSet(character.Passive, this.BChar);

            //BattleAlly component = Misc.UIInst(BattleSystem.instance.G_Ally).GetComponent<BattleAlly>();
            BattleAlly component=new BattleAlly();
            component.MyTeam = BattleSystem.instance.AllyTeam;
            component.Info = character;
            component.Info.Ally = true;
            BattleChar bc = component;
            */
            BattleChar bc = this.BChar;

            Skill skill = Skill.TempSkill("S_SF_Agent_1_1_1", bc, bc.MyTeam);
            BattleTeam.SkillRandomUse(bc, skill, false, true, false);
        }
    }
    public class Sex_agt_1_1_1 : Sex_ImproveClone
    {
        public override void Init()
        {
            base.Init();
            this.PlusSkillStat.Penetration = 100;
        }
        public override bool TargetSelectExcept(BattleChar ExceptTarget)
        {
            bool alltaunt = true;
            bool taunt = false;
            if (ExceptTarget is BattleEnemy)
            {
                foreach (BattleChar cha in ExceptTarget.MyTeam.AliveChars_Vanish)
                {
                    alltaunt = (cha as BattleEnemy).istaunt && alltaunt;
                }
                taunt = (ExceptTarget as BattleEnemy).istaunt;
            }

            return !alltaunt && taunt;
        }
    }

    public class P_Agent_Doll_1 : P_Agent_Doll
    {
        public override List<string> GetSelfBuff()
        {
            List<string> list = base.GetSelfBuff();
            list.Add("B_SF_Agent_1_2");
            return list;
        }
        public override void PassiveStatSet( BattleChar battlechar)
        {
            base.PassiveStatSet(battlechar);

            this.PlusStat.atk = (int)(1f * this.GetStatOfMaster.atk);
            this.PlusStat.hit = (int)(1f * this.GetStatOfMaster.hit);
            this.PlusStat.def = (int)(0f * this.GetStatOfMaster.def);
            this.PlusStat.dod = (int)(0.5f * this.GetStatOfMaster.dod);
            this.PlusStat.cri = (int)(10f + 1.2f * this.GetStatOfMaster.cri);
            this.PlusStat.maxhp = (int)Math.Max(1, 0.25f * this.GetStatOfMaster.maxhp);
            this.MyChar.Hp = this.PlusStat.maxhp;
        }
    }
    public class Sex_agt_1_2 : Agent_Sum_Extended
    {
        public override string DescExtended(string desc)
        {
            BattleChar bc = P_Agent_Doll.FirstSummoner(this.BChar);//改为根源召唤者
            float atk = (int)(1f * bc.GetStat.atk);
            float hit = (int)(1f * bc.GetStat.hit);
            float def = (int)Math.Min(80, 0f * bc.GetStat.def);
            float dod = (int)Math.Min(80, 0.5f * bc.GetStat.dod);
            float cri = (int)(10f + 1.2f * bc.GetStat.cri);
            float maxhp = (int)Math.Max(1, 0.25f * bc.GetStat.maxhp);
            return base.DescExtended(desc).Replace("&atk",atk.ToString()).Replace("&hit", hit.ToString()).Replace("&def", def.ToString()).Replace("&dod", dod.ToString()).Replace("&cri", cri.ToString()).Replace("&maxhp", maxhp.ToString()).Replace("&user", bc.Info.Name);
        }
        public override int GetMaxHp(BattleChar bc)
        {
            Stat stat = AllySummon.OrgStat(bc);
            return (int)Math.Max(1, 0.25f * this.BChar.GetStat.maxhp);
        }
        public override string GetAllyKey()
        {
            return "SF_Jaeger";
        }

        public override string GetBasicKey()
        {
            return "S_SF_Agent_1_2_2";
        }
        /*
        public override void PassiveStatSet(Passive_Char passive, BattleChar battlechar)
        {
            base.PassiveStatSet(passive, battlechar);

            Stat stat = AllySummon.OrgStat(battlechar);
            passive.PlusStat.atk = (int)(1f * stat.atk);
            passive.PlusStat.hit = (int)(1f * stat.hit);
            passive.PlusStat.def = (int)(0f * stat.def);
            passive.PlusStat.dod = (int)(0.5f * stat.dod);
            passive.PlusStat.cri = (int)(10f +1.2f * stat.cri);
            passive.PlusStat.maxhp = (int)Math.Max(1, 0.25f * stat.maxhp);
            passive.MyChar.Hp = passive.PlusStat.maxhp;
        }
        */
    }
    public class Bcl_agt_1_2 : Bcl_agt_doll, IP_PlayerTurn
    {
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            this.TurnAttack();
        }
        public override void Init()
        {
            base.Init();
            this.PlusStat.AggroPer = -50;
            //this.PlusStat.Penetration = 100;
        }
        public new void Turn()
        {
            /*
            Character character = new Character();
            GDECharacterData gde = new GDECharacterData("SF_Jaeger");
            character.Set_AllyData(gde);

            Sex_agt_1_2 sex= new Sex_agt_1_2();
            sex.PassiveStatSet(character.Passive, this.BChar);

            //BattleAlly component = Misc.UIInst(BattleSystem.instance.G_Ally).GetComponent<BattleAlly>();
            BattleAlly component=new BattleAlly();
            component.MyTeam = BattleSystem.instance.AllyTeam;
            component.Info = character;
            component.Info.Ally = true;
            BattleChar bc = component;
            */
            base.Turn();
            this.TurnAttack();
        }
        public override void TurnAttack()
        {
            BattleChar bc = this.BChar;

            Skill skill = Skill.TempSkill("S_SF_Agent_1_2_1", bc, bc.MyTeam);
            BattleTeam.SkillRandomUse(bc, skill, false, true, false);
        }
    }

    public class Sex_agt_1_2_1 : Sex_ImproveClone
    {
        public override void Init()
        {
            base.Init();
            this.PlusSkillStat.Penetration = 100;
        }

        public override bool TargetSelectExcept(BattleChar ExceptTarget)
        {
            bool alltaunt = true;
            bool taunt = false;
            if (ExceptTarget is BattleEnemy)
            {
                foreach (BattleChar cha in ExceptTarget.MyTeam.AliveChars_Vanish)
                {
                    alltaunt = (cha as BattleEnemy).istaunt && alltaunt;
                }
                taunt = (ExceptTarget as BattleEnemy).istaunt;
            }

            return !alltaunt && taunt;
        }
        
    }
    
    public class Sex_agt_1_2_2 : Agent_Doll_Basic_extended,IP_DamageChange
    {
        public override void Init()
        {
            base.Init();

                
            this.PlusSkillStat.Penetration = 100;
        }
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if(SkillD==this.MySkill && !(Target as BattleEnemy).istaunt)
            {
                Cri = true;
            }
            return Damage;
        }
    }







    public class Sex_agt_2 : Sex_ImproveClone
    {
        // Token: 0x060013CE RID: 5070 RVA: 0x0009C123 File Offset: 0x0009A323
        public override void Init()
        {
            base.Init();
            this.ChoiceSkillList = new List<string>();
            this.ChoiceSkillList.Add("S_SF_Agent_2_1");
            this.ChoiceSkillList.Add("S_SF_Agent_2_2");
        }
    }
    public class Sex_agt_2_1 : Sex_ImproveClone
    {

    }
    public class Bcl_agt_2 : Buff,IP_Hit
    {
        public override void BuffStat()
        {
            base.BuffStat();
            this.PlusStat.crihit = 16 * this.StackNum;
            this.PlusStat.CRIGetDMG = 8 * this.StackNum;
        }
        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            if(Cri)
            {
                this.SelfStackDestroy();
            }
        }
    }
    public class P_Agent_Doll_2 : P_Agent_Doll
    {
        public override List<string> GetSelfBuff()
        {
            List<string> list = base.GetSelfBuff();
            list.Add("B_SF_Agent_2_2");
            return list;
        }
        public override void PassiveStatSet(BattleChar battlechar)
        {
            base.PassiveStatSet(battlechar);

            Stat stat = AllySummon.OrgStat(battlechar);
            this.PlusStat.atk = (int)(1f * this.GetStatOfMaster.atk);
            this.PlusStat.hit = (int)(1f * this.GetStatOfMaster.hit);
            this.PlusStat.def = (int)(10f + 1f * this.GetStatOfMaster.def);
            this.PlusStat.dod = (int)(0f * this.GetStatOfMaster.dod);
            this.PlusStat.maxhp = (int)Math.Max(1, 0.4f * this.GetStatOfMaster.maxhp);

            this.MyChar.Hp = this.PlusStat.maxhp;
        }
    }
    public class Sex_agt_2_2 : Agent_Sum_Extended
    {
        public override string DescExtended(string desc)
        {
            BattleChar bc = P_Agent_Doll.FirstSummoner(this.BChar);//改为根源召唤者
            float atk = (int)(1f * bc.GetStat.atk);
            float hit = (int)(1f * bc.GetStat.hit);
            float def = (int)Math.Min(80, 10f +1f * bc.GetStat.def);
            float dod = (int)Math.Min(80, 0f * bc.GetStat.dod);
            float maxhp = (int)Math.Max(1, 0.4f * bc.GetStat.maxhp);
            return base.DescExtended(desc).Replace("&atk", atk.ToString()).Replace("&hit", hit.ToString()).Replace("&def", def.ToString()).Replace("&dod", dod.ToString()).Replace("&maxhp", maxhp.ToString()).Replace("&user", bc.Info.Name);
        }
        public override int GetMaxHp(BattleChar bc)
        {
            Stat stat = AllySummon.OrgStat(bc);
            return (int)Math.Max(1, 0.4f * this.BChar.GetStat.maxhp);
        }
        public override string GetAllyKey()
        {
            return "SF_Nemeum";
        }

        public override string GetBasicKey()
        {
            return "S_SF_Agent_2_2_2";
        }
        /*
        public override void PassiveStatSet(Passive_Char passive, BattleChar battlechar)
        {
            base.PassiveStatSet(passive, battlechar);

            Stat stat = AllySummon.OrgStat(battlechar);
            passive.PlusStat.atk = (int)(1f * stat.atk);
            passive.PlusStat.hit = (int)(1f * stat.hit);
            passive.PlusStat.def = (int)(10f + 1f * stat.def);
            passive.PlusStat.dod = (int)(0f * stat.dod);
            passive.PlusStat.maxhp = (int)Math.Max(1, 0.4f * stat.maxhp);

            passive.MyChar.Hp = passive.PlusStat.maxhp;
        }
        */
    }
    public class Bcl_agt_2_2 : Bcl_agt_doll, IP_PlayerTurn//,IP_Damage_Before
    {
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            this.TurnAttack();
        }
        public override void Init()
        {
            base.Init();
            this.PlusStat.Penetration = 50;
        }
        public new void Turn()
        {
            base.Turn();
            this.TurnAttack();

        }
        public override void TurnAttack()
        {
            BattleChar bc = this.BChar;

            Skill skill = Skill.TempSkill("S_SF_Agent_2_2_1", bc, bc.MyTeam);
            if (BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Contains(this.tar))
            {
                if (BattleSystem.IsSelect(this.tar, skill))
                {
                    BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, this.tar, false, false, true));
                    return;
                }
            }
            BattleTeam.SkillRandomUse(bc, skill, false, true, false);
        }
        /*
        public void Damage_Before(BattleChar battleChar, BattleChar User, int Dmg, bool Cri, ref bool Pain, ref bool NOEFFECT, int PlusPenetration, bool IgnoreHealingPro, bool HealingPro, bool OnlyUnscaleTime)
        {
            if (battleChar == this.BChar && Pain)
            {
                Pain = false;
            }
        }
        */
        public BattleChar tar=new BattleChar();
    }

    public class Sex_agt_2_2_1 : Sex_ImproveClone,IP_DamageChange_sumoperation, IP_SpecialEnemyTargetSelect
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(1 * this.MySkill.TargetDamageOriginal)).ToString());
        }
        public bool SpecialEnemyTargetSelect(Skill skill, BattleEnemy Target)
        {
            if(skill==this.MySkill)
            {
                if (this.BChar.BuffFind("B_SF_Agent_2_2"))
                {
                    Bcl_agt_2_2 buff = this.BChar.BuffReturn("B_SF_Agent_2_2") as Bcl_agt_2_2;
                    if(buff.tar==Target)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if(SkillD==this.MySkill)
            {
                int num = 0;
                num = (int)(Target.GetStat.maxhp * 0.02);
                num=Math.Min(num, ((int)(1*this.MySkill.TargetDamageOriginal)));
                num = Math.Max(num, 1);
                if(num>0)
                {
                    PlusDamage += num;
                }
            }
        }
    }

    public class Sex_agt_2_2_2 : Agent_Doll_Basic_extended
    {
        public override void Init()
        {
            base.Init();
            
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            if(this.BChar.BuffFind("B_SF_Agent_2_2"))
            {
                Bcl_agt_2_2 buff = this.BChar.BuffReturn("B_SF_Agent_2_2") as Bcl_agt_2_2;
                buff.tar = Targets[0];
            }

            foreach(CastingSkill cast in BattleSystem.instance.CastSkills)
            {
                if(cast.skill.Master==this.BChar && cast.skill.MySkill.KeyID== "S_SF_Agent_2_2_1")
                {
                    cast.Target= Targets[0];
                }
            }
        }
    }




    public class Sex_agt_3 : Sex_ImproveClone
    {
        // Token: 0x060013CE RID: 5070 RVA: 0x0009C123 File Offset: 0x0009A323
        public override void Init()
        {
            base.Init();
            this.ChoiceSkillList = new List<string>();
            this.ChoiceSkillList.Add("S_SF_Agent_3_1");
            this.ChoiceSkillList.Add("S_SF_Agent_3_2");
        }
    }
    public class Sex_agt_3_1 : Sex_ImproveClone
    {

    }
    public class Bcl_agt_3_1 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.BarrierHP += (int)(this.Usestate_L.GetStat.maxhp * 0.4);
        }

    }
    public class P_Agent_Doll_3 : P_Agent_Doll
    {
        public override List<string> GetSelfBuff()
        {
            List<string> list = base.GetSelfBuff();
            list.Add("B_SF_Agent_3_2");
            return list;
        }
        public override void PassiveStatSet(BattleChar battlechar)
        {
            base.PassiveStatSet(battlechar);

            Stat stat = AllySummon.OrgStat(battlechar);
            this.PlusStat.atk = (int)(1f * this.GetStatOfMaster.atk);
            this.PlusStat.hit = (int)(1f * this.GetStatOfMaster.hit);
            this.PlusStat.def = (int)(5 + 0.5f * this.GetStatOfMaster.def);
            this.PlusStat.dod = (int)(5 + 1f * this.GetStatOfMaster.dod);
            this.PlusStat.cri = (int)(0.7f * this.GetStatOfMaster.cri);
            this.PlusStat.maxhp = (int)Math.Max(1, 0.65f * this.GetStatOfMaster.maxhp);

            this.MyChar.Hp = this.PlusStat.maxhp;
        }
    }
    public class Sex_agt_3_2 : Agent_Sum_Extended
    {
        public override string DescExtended(string desc)
        {
            BattleChar bc = P_Agent_Doll.FirstSummoner(this.BChar);//改为根源召唤者
            float atk = (int)(1f * bc.GetStat.atk);
            float hit = (int)(1f * bc.GetStat.hit);
            float def = (int)Math.Min(80, 5 +0.5f * bc.GetStat.def);
            float dod = (int)Math.Min(80, 5 +1f * bc.GetStat.dod);
            float cri = (int)(0.7f * bc.GetStat.cri);
            float maxhp = (int)Math.Max(1, 0.55f * bc.GetStat.maxhp);
            return base.DescExtended(desc).Replace("&atk", atk.ToString()).Replace("&hit", hit.ToString()).Replace("&def", def.ToString()).Replace("&dod", dod.ToString()).Replace("&cri", cri.ToString()).Replace("&maxhp", maxhp.ToString()).Replace("&user", bc.Info.Name);
        }
        public override int GetMaxHp(BattleChar bc)
        {
            Stat stat = AllySummon.OrgStat(bc);
            return (int)Math.Max(1, 0.65f * this.BChar.GetStat.maxhp);
        }
        public override string GetAllyKey()
        {
            return "SF_Dragoon";
        }

        public override List<string> GetUserBuff()
        {
            List<string> list = new List<string>();
            list.Add("B_SF_Agent_3_1");
            list.Add("B_SF_Agent_3_1");
            return list;
        }
        public override string GetBasicKey()
        {
            return "S_SF_Agent_3_2_2";
        }
        /*
        public override void PassiveStatSet(Passive_Char passive, BattleChar battlechar)
        {
            base.PassiveStatSet(passive, battlechar);

            Stat stat = AllySummon.OrgStat(battlechar);
            passive.PlusStat.atk = (int)(1f * stat.atk);
            passive.PlusStat.hit = (int)(1f * stat.hit);
            passive.PlusStat.def = (int)(5 + 0.5f * stat.def);
            passive.PlusStat.dod = (int)(5 + 1f * stat.dod);
            passive.PlusStat.cri = (int)(0.7f * stat.cri);
            passive.PlusStat.maxhp = (int)Math.Max(1, 0.65f * stat.maxhp);

            passive.MyChar.Hp = passive.PlusStat.maxhp;
        }
        */
    }
    public class Bcl_agt_3_2 : Bcl_agt_doll, IP_PlayerTurn
    {
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            this.TurnAttack();

        }
        public override void Init()
        {
            base.Init();
            this.PlusStat.AggroPer = 20;
            this.PlusStat.spd = 1;
            //this.PlusStat.Penetration = 100;
        }
        public new void Turn()
        {
            base.Turn();
            this.TurnAttack();
           

            BattleSystem.instance.AllyTeam.WaitCount += 2;
        }
        public override void TurnAttack()
        {
            BattleChar bc = this.BChar;

            Skill skill = Skill.TempSkill("S_SF_Agent_3_2_1", bc, bc.MyTeam);
            BattleTeam.SkillRandomUse(bc, skill, false, true, false);
        }
    }

    public class Sex_agt_3_2_1 : Sex_ImproveClone
    {


    }
    public class Bcl_agt_3_2_1 : Buff
    {
        public override void BuffStat()
        {
            base.BuffStat();
            this.PlusPerStat.Damage=-8*this.StackNum;
            this.PlusStat.hit = -8 * this.StackNum;
        }
    }

    public class Sex_agt_3_2_2 : Agent_Doll_Basic_extended
    {
        public override void Init()
        {
            base.Init();
            
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





    public class Sex_agt_4 : Sex_ImproveClone
    {
        // Token: 0x060013CE RID: 5070 RVA: 0x0009C123 File Offset: 0x0009A323
        public override void Init()
        {
            base.Init();
            this.ChoiceSkillList = new List<string>();
            this.ChoiceSkillList.Add("S_SF_Agent_4_1");
            this.ChoiceSkillList.Add("S_SF_Agent_4_2");
        }
    }
    public class Sex_agt_4_1 : Sex_ImproveClone
    {

    }
    public class Bcl_agt_4_1 : Buff, IP_SkillUse_Target//, IP_LogUpdate
    {
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if(!this.locking&& DMG>0 )
            {
                if(this.recs.Find(a=>a.sp==SP)!=null)
                {
                    B41_rec rec = this.recs.Find(a => a.sp == SP);
                    rec.targets.Add(hit);
                    rec.dmgs.Add(Math.Max(1, (int)(0.5 * DMG)));
                }
                else
                {
                    B41_rec rec = new B41_rec();
                    rec.sp = SP;
                    rec.particleName = SP.ParticleName;
                    rec.criper = SP.SkillData.MySkill.Effect_Target.CRI;
                    rec.targets.Add(hit);
                    rec.dmgs.Add(Math.Max(1,(int)(0.5 * DMG)));
                    this.recs.Add(rec);
                }
                if(!this.recording)
                {
                    this.recording = true;
                    BattleSystem.DelayInputAfter(this.ShadowAttack());
                }
            }
        }
        public IEnumerator ShadowAttack()
        {
            yield return new WaitForSecondsRealtime(0.3f);

            this.locking = true;

            for (int i = 0; i < this.recs.Count; i++)
            {
                B41_rec rec = this.recs[i];
                while (BattleSystem.instance.Particles.Count != 0)
                {
                    yield return new WaitForSecondsRealtime(0.1f);
                }
                Skill skill = Skill.TempSkill("S_SF_Agent_4_1_1", this.BChar, this.BChar.MyTeam);
                skill.UseOtherParticle_Path = rec.particleName;
                skill.FreeUse = true;
                skill.PlusHit = true;
                foreach (Skill_Extended sex in skill.AllExtendeds) 
                {
                    if (sex is Sex_agt_4_1_1 sex411)
                    {
                        sex411.rec = rec;
                        sex411.PlusSkillStat.cri = rec.criper;

                    }
                }
                List<BattleChar> list = new List<BattleChar>();
                list.AddRange(rec.targets);
                this.BChar.ParticleOut(skill, list);
            }
            /*
            while (BattleSystem.instance.Particles.Count != 0)
            {
                yield return new WaitForSecondsRealtime(0.1f);
            }
            */
            this.recs.RemoveAll(a => true);
            this.recording = false;

            yield break;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(this.locking)
            {
                if(!BattleSystem.instance.DelayWait && BattleSystem.instance.Particles.Count == 0 )//&& BattleSystem.instance.ActWindow.On)
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
            if (!this.recording && !Standard_Tool.OriginSkill(log.UsedSkill).FreeUse)
            {
                this.locking = false;
                this.frame = 0;
            }
        }
        public int frame = 0;
        public bool locking = false;
        //public bool unlockAllow = false;
        public bool recording = false;
        public List<B41_rec> recs = new List<B41_rec>();
    }
    public class B41_rec
    {
        public SkillParticle sp = new SkillParticle();
        public int criper = 0;
        public string particleName = string.Empty;
        public List<BattleChar> targets = new List<BattleChar>();
        public List<int>dmgs = new List<int>();
    }
    public class Sex_agt_4_1_1 : Sex_ImproveClone,IP_DamageChange_sumoperation
    {
        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if(SkillD==this.MySkill)
            {
                if(this.rec.targets.Contains(Target))
                {
                    int index = this.rec.targets.FindIndex(a=>a==Target);
                    PlusDamage += (this.rec.dmgs[index]-1);
                }
            }
        }
        public B41_rec rec=new B41_rec();
    }
    public class P_Agent_Doll_4 : P_Agent_Doll
    {
        public override List<string> GetSelfBuff()
        {
            List<string> list = base.GetSelfBuff();
            list.Add("B_SF_Agent_4_2");
            return list;
        }
        public override void PassiveStatSet(BattleChar battlechar)
        {
            base.PassiveStatSet( battlechar);

            Stat stat = AllySummon.OrgStat(battlechar);
            this.PlusStat.def = (int)(10f + 1f * this.GetStatOfMaster.def);
            this.PlusStat.maxhp = (int)Math.Max(1, 0.75f * this.GetStatOfMaster.maxhp);
            this.MyChar.Hp = this.PlusStat.maxhp;
        }
    }
    public class Sex_agt_4_2 : Agent_Sum_Extended
    {
        public override string DescExtended(string desc)
        {
            BattleChar bc = P_Agent_Doll.FirstSummoner(this.BChar);//改为根源召唤者
            float atk = (int)(1f * bc.GetStat.atk);
            float hit = (int)(1f * bc.GetStat.hit);
            float def = (int)Math.Min(80, 10f + 1f * bc.GetStat.def);
            float dod = (int)Math.Min(80, 1f * bc.GetStat.dod);
            float cri = (int)(1f * bc.GetStat.cri);
            float maxhp = (int)Math.Max(1, 0.75f * bc.GetStat.maxhp);

            return base.DescExtended(desc).Replace("&atk", atk.ToString()).Replace("&hit", hit.ToString()).Replace("&def", def.ToString()).Replace("&dod", dod.ToString()).Replace("&cri", cri.ToString()).Replace("&maxhp", maxhp.ToString()).Replace("&user", bc.Info.Name);
        }
        public override int GetMaxHp(BattleChar bc)
        {
            Stat stat = AllySummon.OrgStat(bc);
            return (int)Math.Max(1, 0.75f * this.BChar.GetStat.maxhp);
        }
        public override string GetAllyKey()
        {
            return "SF_Agent_shadow";
        }

        public override string GetBasicKey()
        {
            return "S_SF_Agent_4_2_2";
        }
        /*
        public override void PassiveStatSet(Passive_Char passive, BattleChar battlechar)
        {
            base.PassiveStatSet(passive, battlechar);

            Stat stat = AllySummon.OrgStat(battlechar);
            passive.PlusStat.def = (int)(10f+1f * stat.def);
            passive.PlusStat.maxhp = (int)Math.Max(1, 0.75f * stat.maxhp);
            passive.MyChar.Hp = passive.PlusStat.maxhp;
        }
        */
    }
    public class Bcl_agt_4_2 : Bcl_agt_doll, IP_PlayerTurn
    {
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            this.TurnAttack();

        }
        public override void Init()
        {
            base.Init();

        }
        public new void Turn()
        {
            base.Turn();
            this.TurnAttack();

        }
        public override void TurnAttack()
        {
            BattleChar bc = this.BChar;

            //Skill skill = Skill.TempSkill("S_SF_Agent_4_2_1", bc, bc.MyTeam);//S_SF_Agent_11
            Skill skill = Skill.TempSkill("S_SF_Agent_11", bc, bc.MyTeam);//
            skill.ExtendedAdd(new Skill_Extended { Counting = 2 });

            BattleTeam.SkillRandomUse(bc, skill, false, true, false);
        }
    }

    public class Sex_agt_4_2_1 : Sex_ImproveClone
    {
        public override void Init()
        {
            base.Init();

            BuffTag btg = new BuffTag();
            btg.BuffData = new GDEBuffData("B_SF_Agent_11");
            btg.User = this.BChar;
            this.SelfBuff.Add(btg);
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            Skill skill = this.MySkill.CloneSkill(true, null, null, true);
            skill.isExcept = true;
            skill.FreeUse = true;
            skill.PlusHit = true;


                    BattleSystem.instance.StartCoroutine(this.Plushit(Targets[0], skill,3));

        }
        public IEnumerator Plushit(BattleChar tar0, Skill skill, int num)//追加射击
        {
            for (int i = 0; i < num; i++)
            {
                //Debug.Log("次数检测: " + i);
                if (BattleSystem.instance.EnemyTeam.AliveChars.Count >= 1)
                {
                    yield return new WaitForSecondsRealtime(0.3f);
                    if (tar0.IsDead || tar0 == null)
                    {
                        this.BChar.ParticleOut(this.MySkill, skill, tar0.MyTeam.AliveChars.Random(this.BChar.GetRandomClass().Main));
                    }
                    else
                    {
                        this.BChar.ParticleOut(this.MySkill, skill, tar0);
                    }
                }

            }


            yield break;
        }
    }

    public class Sex_agt_4_2_2 : Agent_Doll_Basic_extended,IP_LogUpdate,IP_PlayerTurn//,IP_ChoiceSkillList_After
    {
        public override void Init()
        {
            base.Init();
            
        
            if(BattleSystem.instance != null)
            {
                BattleSystem.DelayInputAfter(this.DelayCheackLog());
            }
        }
        public void Turn()
        {
            this.CheckLog();
        }
        public void LogUpdate(BattleLog log)
        {
            this.CheckLog();
            /*
            bool basic = false;
            try
            {
                basic = this.MySkill.BasicSkill && this.MySkill.BasicSkillButton.InActive;
            }
            catch { }
            if (basic)
            {
                return;
            }
            if (skill.Master.Info.Ally && !skill.FreeUse && skill. && this.ChoiceSkillList.FindAll(a => a == skill.MySkill.KeyID).Count <= 0)
            {
                this.ChoiceSkillList.Add(skill.MySkill.KeyID);
            }
            */
        }
        public IEnumerator DelayCheackLog()
        {
            this.CheckLog();
            yield break;
        }
        public void CheckLog()
        {
            List<Skill> logs = new List<Skill>();              
            logs.AddRange(BattleSystem.instance.BattleLogs.getSkills((BattleLog log) => log.WhoUse .Info.Ally && log.WhoUse!=this.BChar, (Skill s) => true ,BattleSystem.instance.TurnNum));
            /*//增加上回合的技能至列表
            if(BattleSystem.instance.TurnNum-1>0)
            {
                logs.AddRange(BattleSystem.instance.BattleLogs.getSkills((BattleLog log) => log.WhoUse.Info.Ally && log.WhoUse != this.BChar, (Skill s) => true, BattleSystem.instance.TurnNum-1));
            }
            */
            this.ChoiceSkillList = new List<string>();
            foreach (Skill skill in logs)
            {
                //Skill skill2=skill.CloneSkill(true,this.BChar,null,true);
                if(skill.OriginalSelectSkill == null && !skill.FreeUse)
                {
                    if (this.ChoiceSkillList.FindAll(a => a == skill.MySkill.KeyID).Count <= 0 && skill.MySkill.KeyID!=this.MySkill.MySkill.KeyID)
                    {
                        this.ChoiceSkillList.Add(skill.MySkill.KeyID);
                    }
                    /*
                    if (this.selfChoiceRecords.FindAll(a => a.skillkey == skill.MySkill.KeyID && a.master==skill.Master).Count <= 0 && skill.MySkill.KeyID != this.MySkill.MySkill.KeyID)
                    {
                        S4_skill_rec rec=new S4_skill_rec();
                        rec.master = skill.Master;
                        rec.skillkey = skill.MySkill.KeyID;
                        this.selfChoiceRecords.Add(rec);
                    }
                    */
                }
                else if(skill.OriginalSelectSkill!=null && !skill.OriginalSelectSkill.FreeUse)
                {
                    if (this.ChoiceSkillList.FindAll(a => a == skill.OriginalSelectSkill.MySkill.KeyID).Count <= 0 && skill.OriginalSelectSkill.MySkill.KeyID != this.MySkill.MySkill.KeyID)
                    {
                        this.ChoiceSkillList.Add(skill.OriginalSelectSkill.MySkill.KeyID);
                    }
                    /*
                    if (this.selfChoiceRecords.FindAll(a => a.skillkey == skill.OriginalSelectSkill.MySkill.KeyID && a.master == skill.OriginalSelectSkill.Master).Count <= 0 && skill.OriginalSelectSkill.MySkill.KeyID != this.MySkill.MySkill.KeyID)
                    {
                        S4_skill_rec rec = new S4_skill_rec();
                        rec.master = skill.OriginalSelectSkill.Master;
                        rec.skillkey = skill.OriginalSelectSkill.MySkill.KeyID;
                        this.selfChoiceRecords.Add(rec);
                    }
                    */
                }
                
            }
        }
        /*
        public void ChoiceSkillList_After(ref List<Skill> result)
        {
            result.Clear();

            List<Skill> list = new List<Skill>();
            if (this.MySkill.MySkill.Target.Key == GDEItemKeys.s_targettype_choiceskill)
            {
                foreach (Skill_Extended skill_Extended in this.MySkill.AllExtendeds)
                {
                    if (skill_Extended.ChoiceSkillList != null && skill_Extended!=this)
                    {
                        foreach (string key in skill_Extended.ChoiceSkillList)
                        {
                            Skill skill = Skill.TempSkill(key, this.MySkill.Master, this.MySkill.Master.MyTeam);
                            skill._Counting += this.MySkill._Counting;
                            skill.BasicSkill = this.MySkill.BasicSkill;
                            list.Add(skill);
                        }
                    }
                }
                foreach (S4_skill_rec rec in this.selfChoiceRecords)
                {
                    if(rec.master.IsDead || rec.master==null)
                    {
                        continue;
                    }
                    Skill skill = Skill.TempSkill(rec.skillkey, rec.master, rec.master.MyTeam);
                    skill._Counting += this.MySkill._Counting;
                    skill.BasicSkill = this.MySkill.BasicSkill;
                    list.Add(skill);
                }
            }

            result.AddRange(list);
        }
        */
        public List<S4_skill_rec> selfChoiceRecords = new List<S4_skill_rec>();
    }
    public class S4_skill_rec
    {
        public BattleChar master=new BattleChar();
        public string skillkey = string.Empty;
    }


    public class Sex_agt_5: Sex_ImproveClone
    {

        public override void Init()
        {
            base.Init();
            this.ChoiceSkillList = new List<string>();
            this.ChoiceSkillList.Add("S_SF_Agent_5_1");
            this.ChoiceSkillList.Add("S_SF_Agent_5_2");
        }
    }
    public class Sex_agt_5_1 : Sex_ImproveClone
    {

    }
    public class Bcl_agt_5_1 : Buff,IP_DamageTakeChange_sumoperation
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&arm", this.armor.ToString());
        }
    
        public override void Init()
        {
            base.Init();
            this.armor = Math.Max(1, (int)(2 + this.Usestate_L.GetStat.def / 20));//15 + 1.2f * this.GetStatOfMaster.def
            this.armor = Math.Max(1, (int)((40f+15f + 1.2f * this.Usestate_L.GetStat.def) / 25));
        }
        public void DamageTakeChange_sumoperation(BattleChar Hit, BattleChar User, int Dmg, bool Cri, ref int PlusDmg, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {
            if (Hit == this.BChar && !NODEF)
            {
                PlusDmg = -this.armor;
            }
        }
        public int armor = 0;
    }
    public class P_Agent_Doll_5 : P_Agent_Doll
    {
        public override List<string> GetSelfBuff()
        {
            List<string> list = base.GetSelfBuff();
            list.Add("B_SF_Agent_5_2");
            return list;
        }
        public override void PassiveStatSet(BattleChar battlechar)
        {
            base.PassiveStatSet(battlechar);

            Stat stat = AllySummon.OrgStat(battlechar);
            this.PlusStat.atk = (int)(1f * this.GetStatOfMaster.atk);
            this.PlusStat.hit = (int)(1f * this.GetStatOfMaster.hit);
            this.PlusStat.def = Math.Min(80, (int)(15 + 1.2f * this.GetStatOfMaster.def));
            this.PlusStat.dod = Math.Min(80, (int)(0f * this.GetStatOfMaster.dod));
            //this.PlusStat .cri = (int)(1f*this.GetStatOfMaster.cri);
            this.PlusStat.maxhp = (int)Math.Max(1, 0.75f * this.GetStatOfMaster.maxhp);

            this.MyChar.Hp = this.PlusStat.maxhp;
        }
    }
    public class Sex_agt_5_2 : Agent_Sum_Extended
    {
        public override string DescExtended(string desc)
        {
            //Debug.Log("point_1_1");
            BattleChar bc = P_Agent_Doll.FirstSummoner(this.BChar);//改为根源召唤者
            //Debug.Log("point_1_2");
            float atk = (int)(1f * bc.GetStat.atk);
            float hit = (int)(1f * bc.GetStat.hit);
            float def = (int)Math.Min(80, 15+1.2f * bc.GetStat.def);
            float dod =  (int)Math.Min(80, 0f * bc.GetStat.dod);
            //float cri = (int)(10f + 1.2f * bc.GetStat.cri);
            float maxhp = (int)Math.Max(1, 0.75f * bc.GetStat.maxhp);
            //Debug.Log("point_1_3");

            return base.DescExtended(desc).Replace("&atk", atk.ToString()).Replace("&hit", hit.ToString()).Replace("&def", def.ToString()).Replace("&dod", dod.ToString()).Replace("&maxhp", maxhp.ToString()).Replace("&user", bc.Info.Name);

        }
        public override int GetMaxHp(BattleChar bc)
        {
            Stat stat = AllySummon.OrgStat(bc);
            return (int)Math.Max(1, 0.75f * this.BChar.GetStat.maxhp);
        }
        public override string GetAllyKey()
        {
            return "SF_Aegis";
        }

        public override string GetBasicKey()
        {
            return "S_SF_Agent_5_2_2";
        }
        /*
        public override void PassiveStatSet(Passive_Char passive, BattleChar battlechar)
        {
            base.PassiveStatSet(passive, battlechar);

            Stat stat = AllySummon.OrgStat(battlechar);
            passive.PlusStat.atk = (int)(1f * stat.atk);
            passive.PlusStat. hit = (int)(1f * stat.hit);
            passive.PlusStat .def = Math.Min(80, (int)(20 + 1.2f * stat.def));
            passive.PlusStat .dod = Math.Min(80, (int)(0f * stat.dod));
            //passive.PlusStat .cri = (int)(1f*stat.cri);
            passive.PlusStat. maxhp = (int)Math.Max(1, 0.75f * stat.maxhp);

            passive.MyChar.Hp = passive.PlusStat.maxhp;
        }
        */
    }
    public class Bcl_agt_5_2 : Bcl_agt_doll, IP_PlayerTurn,IP_Hit, IP_DamageTakeChange_sumoperation//,IP_Damage_Before
    {

        public override void Init()
        {
            base.Init();
            this.PlusStat.AggroPer = 80;
            //this.PlusStat.Penetration = 100;
            float master_def = this.Usestate_L.GetStat.def;
            if (this.View)
            {
                master_def = master_def * 1.2f + 15f;
            }

            this.armor = Math.Max(1, (int)((40+ master_def) /25));
        }
        
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&arm", this.armor.ToString()).Replace("&anti", ((int)(0.5*this.BChar.GetStat.atk)).ToString());
        }
        /*
        public void Damage_Before(BattleChar battleChar, BattleChar User, int Dmg, bool Cri, ref bool Pain, ref bool NOEFFECT, int PlusPenetration, bool IgnoreHealingPro, bool HealingPro, bool OnlyUnscaleTime)
        {
            if(battleChar==this.BChar && Pain)
            {
                Pain = false;
            }
        }
        */
        public void DamageTakeChange_sumoperation(BattleChar Hit, BattleChar User, int Dmg, bool Cri, ref int PlusDmg, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {
            if(Hit==this.BChar && !NODEF )
            {
                PlusDmg = -this.armor;
            }
        }
        
        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            if(!SP.UseStatus.Info.Ally )//&& Dmg>0)
            {
                Skill skill = Skill.TempSkill("S_SF_Agent_5_2_1", this.BChar, this.BChar.MyTeam);
                skill.isExcept = true;
                skill.FreeUse = true;
                skill.PlusHit = true;
                BattleSystem.instance.StartCoroutine(this.Counter(skill, SP.UseStatus));
            }
        }
        public IEnumerator Counter(Skill skill,BattleChar target)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            this.BChar.ParticleOut(skill, target);

            yield break;
        }
        public int armor = 0;
    }

    public class Sex_agt_5_2_1 : Sex_ImproveClone
    {


    }

    public class Sex_agt_5_2_2 : Agent_Doll_Basic_extended
    {
        public override void Init()
        {
            base.Init();
            
        }
    }
    public class Bcl_agt_5_2_2:Buff, IP_Awake, IP_TargetedAlly
    {
        // Token: 0x06001437 RID: 5175 RVA: 0x0009D6A4 File Offset: 0x0009B8A4
        public void Awake()
        {
            foreach (BattleChar battleChar in this.BChar.BattleInfo.GetGlobalChar())
            {
                if ((battleChar.BuffFind("B_SF_Agent_5_2_2", false) || battleChar.BuffFind("B_SF_Agent_5_2_2", false)) && battleChar.BuffReturn("B_SF_Agent_5_2_2", false) != this && battleChar.BuffReturn("B_SF_Agent_5_2_2", false).Usestate_L == base.Usestate_L)
                {
                    battleChar.BuffReturn("B_SF_Agent_5_2_2", false).SelfDestroy(false);
                }
            }
            if (base.Usestate_L.BuffFind("B_SF_Agent_5_2_2", false))
            {
                base.Usestate_L.BuffRemove("B_SF_Agent_5_2_2", false);
            }
        }

        // Token: 0x06001438 RID: 5176 RVA: 0x0009D77C File Offset: 0x0009B97C
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.Usestate_L != null)
            {
                if (base.Usestate_L.IsDead)
                {
                    base.SelfDestroy(false);
                    return;
                }
                this.PlusStat.def = base.Usestate_L.GetStat.def;
            }
        }
        public override void Init()
        {
            base.Init();
            this.PlusStat.Strength = true;
        }
        // Token: 0x06001439 RID: 5177 RVA: 0x0009D7E8 File Offset: 0x0009B9E8
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", base.Usestate_L.Info.Name);
        }

        // Token: 0x0600143A RID: 5178 RVA: 0x0009D80C File Offset: 0x0009BA0C
        public IEnumerator Targeted(BattleChar Attacker, List<BattleChar> SaveTargets, Skill skill)
        {
            bool flag = false;
            for (int i = 0; i < SaveTargets.Count; i++)
            {
                if (SaveTargets[i] == base.Usestate_L)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                for (int j = 0; j < SaveTargets.Count; j++)
                {
                    if (SaveTargets[j] == this.BChar)
                    {
                        SaveTargets[j] = base.Usestate_L;
                        EffectView.TextOutSimple(this.BChar, this.BuffData.Name);
                        return null;
                    }
                }
            }
            return null;
        }
    }




    public class Sex_agt_6 : Sex_ImproveClone
    {

        // Token: 0x060013CE RID: 5070 RVA: 0x0009C123 File Offset: 0x0009A323

        public override void Init()
        {
            base.Init();
            this.ChoiceSkillList = new List<string>();
            this.ChoiceSkillList.Add("S_SF_Agent_6_1");
            this.ChoiceSkillList.Add("S_SF_Agent_6_2");
        }
    }
    public class Sex_agt_6_1 : Sex_ImproveClone
    {

    }
    public class Bcl_agt_6_1 : Buff, IP_Hit,IP_Dodge
    {


        public override void Init()
        {
            base.Init();
            this.PlusStat.dod = 20 + this.plusdod;
        }
        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
          //Debug.Log("hit");
            if(SP.SkillData.IsDamage && Dmg > 0)
            {
                this.plusdod += 30;
                this.PlusStat.dod = 20 + this.plusdod;
            }
            
        }
        public void Dodge(BattleChar Char, SkillParticle SP)
        {
            if(Char==this.BChar)
            {
                this.plusdod = 0;
                this.PlusStat.dod = 20 + this.plusdod;

            }
        }
        public int plusdod = 0;
    }
    public class P_Agent_Doll_6 : P_Agent_Doll
    {
        public override List<string> GetSelfBuff()
        {
            List<string> list = base.GetSelfBuff();
            list.Add("B_SF_Agent_6_2");
            return list;
        }
        public override void PassiveStatSet(BattleChar battlechar)
        {
            base.PassiveStatSet(battlechar);

            Stat stat = AllySummon.OrgStat(battlechar);
            this.PlusStat.atk = (int)(1f * this.GetStatOfMaster.atk);
            this.PlusStat.hit = (int)(1f * this.GetStatOfMaster.hit);
            this.PlusStat.def = Math.Min(80, (int)(0.7f * this.GetStatOfMaster.def));
            this.PlusStat.dod = Math.Min(80, (int)(15 + 1.1f * this.GetStatOfMaster.dod));
            //this.PlusStat. cri = (int)(10f + 1.2f * this.GetStatOfMaster.cri);
            this.PlusStat.maxhp = (int)Math.Max(1, 0.6f * this.GetStatOfMaster.maxhp);

            this.MyChar.Hp = this.PlusStat.maxhp;
        }
    }
    public class Sex_agt_6_2 : Agent_Sum_Extended
    {
        public override string DescExtended(string desc)
        {
            BattleChar bc = P_Agent_Doll.FirstSummoner(this.BChar);//改为根源召唤者
            float atk = (int)(1f * bc.GetStat.atk);
            float hit = (int)(1f * bc.GetStat.hit);
            float def = (int)Math.Min(80, 0.7f * bc.GetStat.def);
            float dod =  (int)Math.Min(80, 15 +1.1f * bc.GetStat.dod);
            //float cri = (int)(10f + 1.2f * bc.GetStat.cri);
            float maxhp = (int)Math.Max(1, 0.6f * bc.GetStat.maxhp);
            return base.DescExtended(desc).Replace("&atk", atk.ToString()).Replace("&hit", hit.ToString()).Replace("&def", def.ToString()).Replace("&dod", dod.ToString()).Replace("&maxhp", maxhp.ToString()).Replace("&user", bc.Info.Name);
        }
        public override int GetMaxHp(BattleChar bc)
        {
            Stat stat = AllySummon.OrgStat(bc);
            return (int)Math.Max(1, 0.6f * this.BChar.GetStat.maxhp);
        }
        public override string GetAllyKey()
        {
            return "SF_Ripper";
        }
        

        


        public override string GetBasicKey()
        {
            return "S_SF_Agent_6_2_2";
        }
        /*
        public override void PassiveStatSet(Passive_Char passive, BattleChar battlechar)
        {
            base.PassiveStatSet(passive, battlechar);

            Stat stat = AllySummon.OrgStat(battlechar);
            passive.PlusStat.atk = (int)(1f * stat.atk);
            passive.PlusStat.hit = (int)(1f * stat.hit);
            passive.PlusStat.def = Math.Min(80, (int)(0.7f * stat.def));
            passive.PlusStat.dod = Math.Min(80, (int)(15 + 1.1f * stat.dod));
            //passive.PlusStat. cri = (int)(10f + 1.2f * stat.cri);
            passive.PlusStat.maxhp = (int)Math.Max(1, 0.6f * stat.maxhp);

            passive.MyChar.Hp = passive.PlusStat.maxhp;
        }
        */
    }
    public class Bcl_agt_6_2 : Bcl_agt_doll, IP_PlayerTurn, IP_HPChange1,IP_DamageTake
    {
        
        public override string DescExtended()
        {
            GDEBuffData gde = new GDEBuffData("B_SF_Agent_6_2_p");
            return base.DescExtended().Replace("&used1",(this.unprotect?gde.Description:string.Empty));//<p><span style="text-decoration: line-through;">test</span></p>
        }
        
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            this.TurnAttack();
        }
        public new void Turn()
        {
            base.Turn();
            this.TurnAttack();

        }
        public override void TurnAttack()
        {
            BattleChar bc = this.BChar;

            Skill skill = Skill.TempSkill("S_SF_Agent_6_2_1", bc, bc.MyTeam);
            BattleTeam.SkillRandomUse(bc, skill, false, true, false);
        }
        public override void Init()
        {
            base.Init();
            this.PlusStat.AggroPer = 50;
            this.PlusStat.cri = (int)(50 * this.BChar.HP / Math.Max(1,this.BChar.GetStat.maxhp));
            this.PlusStat.dod = (int)(50 -50*this.BChar.HP / Math.Max(1, this.BChar.GetStat.maxhp));
        }
        public void HPChange1(BattleChar Char, bool Healed, int PreHPNum, int NewHPNum)
        {
            if(Char==this.BChar&&!this.unprotect)
            {
                if(NewHPNum<(0.15*this.BChar.GetStat.maxhp))
                {
                    this.unprotect = true;
                    this.protecting = true;
                    this.proTurn = BattleSystem.instance.TurnNum;
                    this.proActionNum = BattleSystem.instance.AllyTeam.TurnActionNum;

                    this.BChar.HP = (int)(0.15 * this.BChar.GetStat.maxhp);
                    GDEBuffData gde = new GDEBuffData("B_SF_Agent_6_2_p");
                    this.BChar.SimpleTextOut(gde.Name);
                }
            }

            if(Char == this.BChar)
            {
                this.Init();
            }
        }
        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if(Target==this.BChar&& this.protecting && this.proTurn == BattleSystem.instance.TurnNum && this.proActionNum == BattleSystem.instance.AllyTeam.TurnActionNum)
            {
                resist = true;
            }
            else if(Target == this.BChar && this.protecting)
            {
                this.protecting = false;
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(this.protecting && !BattleSystem.instance.DelayWait && BattleSystem.instance.Particles.Count == 0 && BattleSystem.instance.ActWindow.On)
            {
                this.protecting = false;
            }
        }
        public bool unprotect = false;
        public bool protecting = false;
        public int proTurn = 0;
        public int proActionNum = 0;
    }

    public class Sex_agt_6_2_1 : Sex_ImproveClone
    {
        public override void Init()
        {
            base.Init();
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            Skill skill = this.MySkill.CloneSkill(true, null, null, true);
            skill.isExcept = true;
            skill.FreeUse = true;
            skill.PlusHit = true;

            for (int i = 1; i <= 2; i++)
            {
                if (BattleSystem.instance.EnemyTeam.AliveChars.Count >= 1)
                {
                    BattleSystem.DelayInput(this.Plushit(Targets[0], skill));
                }
            }
        }
        public IEnumerator Plushit(BattleChar tar0, Skill skill)//追加射击
        {
            yield return new WaitForSecondsRealtime(0.3f);
            this.BChar.ParticleOut(this.MySkill, skill, tar0.MyTeam.AliveChars.Random(this.BChar.GetRandomClass().Main));

            
            yield break;
        }

    }

    public class Sex_agt_6_2_2 : Agent_Doll_Basic_extended
    {
        public override void Init()
        {
            base.Init();
            
        }
    }
    public class Bcl_agt_6_2_2 : Buff, IP_BeforeHit,IP_DamageTakeChange
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", ((int)Math.Max(this.BChar.GetStat.dod, 0)).ToString());
        }
        // Token: 0x06001437 RID: 5175 RVA: 0x0009D6A4 File Offset: 0x0009B8A4
        public override void Init()
        {
            base.Init();
            this.PlusStat.dod = 25;
            this.PlusStat.Strength = true;
        }
        public void BeforeHit(SkillParticle SP, int DMG, bool Cri)
        {
            if(SP.SkillData.IsDamage && DMG>0)
            {
                this.pflag1 = true;
            }
        }
        public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {
            if (this.pflag1)
            {
                this.pflag1 = false;
                this.TurnUpdate();
                return (int)(Dmg * (1 - 0.01 * Math.Max(this.BChar.GetStat.dod, 0)));
                
            }
            return Dmg;
        }

        public bool pflag1 = false;
    }



    public class Sex_agt_7 : Sex_ImproveClone
    {
        // Token: 0x060013CE RID: 5070 RVA: 0x0009C123 File Offset: 0x0009A323
        public override string DescExtended(string desc)
        {
            int num = 0;
            if(BattleSystem.instance!=null)
            {
                List<BattleLog> logs = BattleSystem.instance.BattleLogs.getLogs((BattleLog log) => log.WhoUse.Info.Ally, BattleSystem.instance.TurnNum);
                foreach (BattleLog log in logs)
                {
                    if ((log.UsedSkill.OriginalSelectSkill == null && !log.UsedSkill.FreeUse) || (log.UsedSkill.OriginalSelectSkill != null && !log.UsedSkill.OriginalSelectSkill.FreeUse))
                    {
                        //if (log.Targets.Contains(Targets[0]))
                        if (log.Targets.FindAll(a => !a.Info.Ally).Count > 0)
                        {
                            num++;
                        }
                    }
                }
            }

            return base.DescExtended(desc).Replace("&anum", num.ToString());
        }
        public override void Init()
        {
            
            base.Init();
            this.ChoiceSkillList = new List<string>();
            this.ChoiceSkillList.Add("S_SF_Agent_7_1");
            this.ChoiceSkillList.Add("S_SF_Agent_7_2");
        }
    }
    public class Sex_agt_7_1 : Sex_ImproveClone
    {
        public override string DescExtended(string desc)
        {
            int num = 0;
            if (BattleSystem.instance != null)
            {
                List<BattleLog> logs = BattleSystem.instance.BattleLogs.getLogs((BattleLog log) => log.WhoUse.Info.Ally, BattleSystem.instance.TurnNum);
                foreach (BattleLog log in logs)
                {
                    if ((log.UsedSkill.OriginalSelectSkill == null && !log.UsedSkill.FreeUse) || (log.UsedSkill.OriginalSelectSkill != null && !log.UsedSkill.OriginalSelectSkill.FreeUse))
                    {
                        //if (log.Targets.Contains(Targets[0]))
                        if (log.Targets.FindAll(a => !a.Info.Ally).Count > 0)
                        {
                            num++;
                        }
                    }
                }
            }
            return base.DescExtended(desc).Replace("&anum", num.ToString());
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            int num = 0;
            List<BattleLog> logs = BattleSystem.instance.BattleLogs.getLogs((BattleLog log) => log.WhoUse.Info.Ally, BattleSystem.instance.TurnNum);
            foreach (BattleLog log in logs)
            {
                if ((log.UsedSkill.OriginalSelectSkill == null && !log.UsedSkill.FreeUse) || (log.UsedSkill.OriginalSelectSkill != null && !log.UsedSkill.OriginalSelectSkill.FreeUse))
                {
                    //if (log.Targets.Contains(Targets[0]))
                    if (log.Targets.FindAll(a=>!a.Info.Ally).Count>0)
                    {
                        num++;
                    }
                }
            }

            Skill skill = this.MySkill.CloneSkill(true, null, null, true);
            skill.isExcept = true;
            skill.FreeUse = true;
            skill.PlusHit = true;

            for (int i = 1; i <= num; i++)
            {
                if (BattleSystem.instance.EnemyTeam.AliveChars.Count >= 1)
                {
                    BattleSystem.DelayInput(this.Plushit(Targets[0], skill));
                }
            }
        }
        public IEnumerator Plushit(BattleChar tar0, Skill skill)//追加射击
        {
            if (tar0.IsDead || tar0 == null)
            {
                this.BChar.ParticleOut(this.MySkill, skill, tar0.MyTeam.AliveChars.Random(this.BChar.GetRandomClass().Main));
            }
            else
            {
                this.BChar.ParticleOut(this.MySkill, skill, tar0);
            }

            yield return new WaitForSecondsRealtime(0.3f);
            yield break;
        }
    }
    

    public class P_Agent_Doll_7 : P_Agent_Doll
    {
        public override List<string> GetSelfBuff()
        {
            List<string> list = base.GetSelfBuff();
            list.Add("B_SF_Agent_7_2");
            return list;
        }
        public override void PassiveStatSet( BattleChar battlechar)
        {
            base.PassiveStatSet( battlechar);

            Stat stat = AllySummon.OrgStat(battlechar);
            this.PlusStat.atk = (int)(1f * this.GetStatOfMaster.atk);
            this.PlusStat.hit = (int)(1f * this.GetStatOfMaster.hit);
            this.PlusStat.def = (int)(0.4f * this.GetStatOfMaster.def);
            this.PlusStat.dod = (int)(0f * this.GetStatOfMaster.dod);
            this.PlusStat.cri = (int)(1f * this.GetStatOfMaster.cri);
            this.PlusStat.maxhp = (int)Math.Max(1, 0.4f * this.GetStatOfMaster.maxhp);
            this.MyChar.Hp = this.PlusStat.maxhp;
        }
    }
    public class Sex_agt_7_2 : Agent_Sum_Extended
    {
        public override string DescExtended(string desc)
        {
            BattleChar bc = P_Agent_Doll.FirstSummoner(this.BChar);//改为根源召唤者
            float atk = (int)(1f * bc.GetStat.atk);
            float hit = (int)(1f * bc.GetStat.hit);
            float def = (int)Math.Min(80, 0.4f * bc.GetStat.def);
            float dod = (int)Math.Min(80, 0f * bc.GetStat.dod);
            float cri = (int)(1f * bc.GetStat.cri);
            float maxhp = (int)Math.Max(1, 0.4f * bc.GetStat.maxhp);
            return base.DescExtended(desc).Replace("&atk", atk.ToString()).Replace("&hit", hit.ToString()).Replace("&def", def.ToString()).Replace("&dod", dod.ToString()).Replace("&cri", cri.ToString()).Replace("&maxhp", maxhp.ToString()).Replace("&user", bc.Info.Name);
        }
        public override int GetMaxHp(BattleChar bc)
        {
            Stat stat = AllySummon.OrgStat(bc);
            return (int)Math.Max(1, 0.4f * this.BChar.GetStat.maxhp);
        }
        public override string GetAllyKey()
        {
            return "SF_Striker";
        }

        public override string GetBasicKey()
        {
            return string.Empty;
            //return "S_SF_Agent_7_2_2";
        }
        /*
        public override void PassiveStatSet(Passive_Char passive, BattleChar battlechar)
        {
            base.PassiveStatSet(passive, battlechar);

            Stat stat = AllySummon.OrgStat(battlechar);
            passive.PlusStat.atk = (int)(1f * stat.atk);
            passive.PlusStat.hit = (int)(1f * stat.hit);
            passive.PlusStat.def = (int)(1f * stat.def);
            passive.PlusStat.dod = (int)(0f * stat.dod);
            passive.PlusStat.cri = (int)(1f * stat.cri);
            passive.PlusStat.maxhp = (int)Math.Max(1, 0.4f * stat.maxhp);
            passive.MyChar.Hp = passive.PlusStat.maxhp;
        }
        */
    }
    public class Bcl_agt_7_2 : Bcl_agt_doll, IP_PlayerTurn
    {
        public override string DescExtended()
        {

            return base.DescExtended().Replace("&num1", this.turn.ToString());
        }
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            this.TurnAttack();
        }
        public override void Init()
        {
            base.Init();
            this.PlusStat.AggroPer = -20;
            //this.PlusStat.Penetration = 100;
        }
        public new void Turn()
        {
            base.Turn();
            this.turn++;
            this.TurnAttack();
        }
        public override void TurnAttack()
        {

            BattleChar bc = this.BChar;

            Skill skill = Skill.TempSkill("S_SF_Agent_7_2_1", bc, bc.MyTeam);
            BattleTeam.SkillRandomUse(bc, skill, false, true, false);
            if (this.turn <= 2)
            {
                Skill skill2 = Skill.TempSkill("S_SF_Agent_7_2_1", bc, bc.MyTeam);
                Skill_Extended sex = skill2.ExtendedAdd(new Skill_Extended());
                sex.Counting = 1;
                BattleTeam.SkillRandomUse(bc, skill2, false, true, false);
            }
        }
        public int turn = 1;
    }

    public class Sex_agt_7_2_1 : Sex_ImproveClone
    {
        public override void Init()
        {
            base.Init();
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            bool randomshot = true;
            List<BattleLog> logs = BattleSystem.instance.BattleLogs.getLogs((BattleLog log) => log.WhoUse.Info.Ally ,  -1);
            logs.Reverse();
            foreach (BattleLog log in logs)
            {
                if((log.UsedSkill.OriginalSelectSkill==null && !log.UsedSkill.FreeUse)||(log.UsedSkill.OriginalSelectSkill != null&& !log.UsedSkill.OriginalSelectSkill.FreeUse))
                {
                    if(log.WhoUse.Info.Ally &&log.Targets.Count ==1 && !log.Targets[0].Info.Ally)
                    {

                        //Debug.Log("改变目标1："+ log.Targets[0].Info.Name);
                        Targets[0] = log.Targets[0];
                        //Debug.Log("改变目标2：" + Targets[0].Info.Name);
                        randomshot = false;
                        break;
                    }
                }
            }
            Skill skill = this.MySkill.CloneSkill(true, null, null, true);
            skill.isExcept = true;
            skill.FreeUse = true;
            skill.PlusHit = true;

            for (int i = 1; i <4; i++)
            {
                if (BattleSystem.instance.EnemyTeam.AliveChars.Count >= 1)
                {
                    BattleSystem.DelayInput(this.Plushit(Targets[0], skill, randomshot));
                }
            }
        }
        public IEnumerator Plushit(BattleChar tar0, Skill skill,bool randomshot)//追加射击
        {
            yield return new WaitForSecondsRealtime(0.15f);
            if (tar0.IsDead || tar0 == null || randomshot)
            {
                this.BChar.ParticleOut(this.MySkill, skill, tar0.MyTeam.AliveChars.Random(this.BChar.GetRandomClass().Main));
            }
            else
            {
                this.BChar.ParticleOut(this.MySkill, skill, tar0);
            }

            
            yield break;
        }

    }

    public class Sex_agt_7_2_2 : Agent_Doll_Basic_extended
    {
        public override void Init()
        {
            base.Init();

            
        }
    }




    public class Sex_agt_8 : Sex_ImproveClone
    {
        // Token: 0x060013CE RID: 5070 RVA: 0x0009C123 File Offset: 0x0009A323
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", ((int)(this.BChar.GetStat.atk * 0.9)).ToString());
        }
        public override void Init()
        {
            base.Init();
            this.ChoiceSkillList = new List<string>();
            this.ChoiceSkillList.Add("S_SF_Agent_8_1");
            this.ChoiceSkillList.Add("S_SF_Agent_8_2");
        }
    }
    public class Sex_agt_8_1 : Sex_ImproveClone
    {
        

    }


    public class P_Agent_Doll_8 : P_Agent_Doll
    {
        public override List<string> GetSelfBuff()
        {
            List<string> list = base.GetSelfBuff();
            list.Add("B_SF_Agent_8_2");
            return list;
        }
        public override void PassiveStatSet(BattleChar battlechar)
        {
            base.PassiveStatSet(battlechar);

            Stat stat = AllySummon.OrgStat(battlechar);
            this.PlusStat.atk = (int)(1f * this.GetStatOfMaster.atk);
            this.PlusStat.hit = (int)(1f * this.GetStatOfMaster.hit);
            this.PlusStat.def = (int)(0.6f * this.GetStatOfMaster.def);
            this.PlusStat.dod = (int)(0f * this.GetStatOfMaster.dod);
            this.PlusStat.cri = (int)(1f * this.GetStatOfMaster.cri);
            this.PlusStat.maxhp = (int)Math.Max(1, 0.75f * this.GetStatOfMaster.maxhp);
            this.MyChar.Hp = this.PlusStat.maxhp;
        }
    }
    public class Sex_agt_8_2 : Agent_Sum_Extended
    {
        public override string DescExtended(string desc)
        {
            BattleChar bc = P_Agent_Doll.FirstSummoner(this.BChar);//改为根源召唤者
            float atk = (int)(1f * bc.GetStat.atk);
            float hit = (int)(1f * bc.GetStat.hit);
            float def = (int)Math.Min(80, 0.6f * bc.GetStat.def);
            float dod = (int)Math.Min(80, 0f * bc.GetStat.dod);
            float cri = (int)(1f * bc.GetStat.cri);
            float maxhp = (int)Math.Max(1, 0.75f * bc.GetStat.maxhp);
            return base.DescExtended(desc).Replace("&atk", atk.ToString()).Replace("&hit", hit.ToString()).Replace("&def", def.ToString()).Replace("&dod", dod.ToString()).Replace("&cri", cri.ToString()).Replace("&maxhp", maxhp.ToString()).Replace("&user", bc.Info.Name);
        }
        public override int GetMaxHp(BattleChar bc)
        {
            Stat stat = AllySummon.OrgStat(bc);
            return (int)Math.Max(1, 0.75f * this.BChar.GetStat.maxhp);
        }
        public override string GetAllyKey()
        {
            return "SF_Goliath";
        }

        public override string GetBasicKey()
        {
            return string.Empty;
            //return "S_SF_Agent_7_2_2";
        }
        /*
        public override void PassiveStatSet(Passive_Char passive, BattleChar battlechar)
        {
            base.PassiveStatSet(passive, battlechar);

            Stat stat = AllySummon.OrgStat(battlechar);
            passive.PlusStat.atk = (int)(1f * stat.atk);
            passive.PlusStat.hit = (int)(1f * stat.hit);
            passive.PlusStat.def = (int)(1f * stat.def);
            passive.PlusStat.dod = (int)(0f * stat.dod);
            passive.PlusStat.cri = (int)(1f * stat.cri);
            passive.PlusStat.maxhp = (int)Math.Max(1, 0.4f * stat.maxhp);
            passive.MyChar.Hp = passive.PlusStat.maxhp;
        }
        */
    }
    public class Bcl_agt_8_2 : Bcl_agt_doll, IP_PlayerTurn
    {

        public override void Init()
        {
            base.Init();
            this.PlusStat.AggroPer = 99;
            this.PlusStat.CantHeal = true;
            //this.PlusStat.Penetration = 100;
        }
        public override string DescExtended()
        {

            return base.DescExtended().Replace("&num1", this.turn.ToString());
        }
        public override void TurnUpdate()
        {
            base.TurnUpdate();
            this.turn++;
            if(this.turn >= 2)
            {
                Skill skill = Skill.TempSkill("S_SF_Agent_8_2_1", this.BChar, this.BChar.MyTeam);
                Skill_Extended sex = skill.ExtendedAdd(new Skill_Extended());
                sex.SkillBasePlus.Target_BaseDMG = this.BChar.HP * 10 - 1;
                BattleSystem.DelayInputAfter(this.Burst(skill));
            }
        }
        public IEnumerator Burst(Skill skill)
        {
            BattleTeam.SkillRandomUse(this.BChar,skill,false,true,false);
            yield return new WaitForFixedUpdate();
            (this.BChar as BattleAlly).ForceDead();

            yield break;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(this.pflag1)
            {
                return;
            }
            if(this.BChar.Recovery>this.BChar.HP)
            {
                this.BChar.Recovery = this.BChar.HP;
            }
            if(this.BChar.HP<1)
            {
                this.pflag1 = true;
                (this.BChar as BattleAlly).ForceDead();
            }
        }
        public int turn = 0;
        private bool pflag1 = false;
    }

    public class Sex_agt_8_2_1 : Sex_ImproveClone
    {
        

    }










    public class Sex_agt_9 : Sex_ImproveClone
    {
        // Token: 0x060013CE RID: 5070 RVA: 0x0009C123 File Offset: 0x0009A323
        public override void Init()
        {
            base.Init();
            this.ChoiceSkillList = new List<string>();
            this.ChoiceSkillList.Add("S_SF_Agent_9_1");
            this.ChoiceSkillList.Add("S_SF_Agent_9_2");
        }
    }
    public class Sex_agt_9_1 : Sex_ImproveClone
    {

    }
    public class Bcl_agt_9_1 : Buff, IP_SkillUse_Target
    {
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if(SP.UseStatus==this.BChar && DMG>0 && !SP.SkillData.PlusHit)
            {
                int num = (int)Math.Max(1, 0.33 * DMG);
                if(this.recs.Find(a=>a.sp==SP)==null)
                {
                    B91_rec rec=new B91_rec(); 
                    rec.sp=SP;
                    rec.dmg=Math.Max(rec.dmg, num);
                    rec.targets.Add(hit);
                    this.recs.Add(rec);

                    BattleSystem.DelayInput(this.ShadowAttack(rec));
                }
                else
                {
                    B91_rec rec = this.recs.Find(a => a.sp == SP);
                    rec.dmg = Math.Max(rec.dmg, num);
                    rec.targets.Add(hit);

                    //BattleSystem.DelayInput(this.ShadowAttack(rec));
                }
            }
        }
        public IEnumerator ShadowAttack(B91_rec record)
        {
            yield return new WaitForSecondsRealtime(0.1f);

            Skill skill = Skill.TempSkill("S_SF_Agent_9_1_1", this.BChar, this.BChar.MyTeam);
            skill.FreeUse = true;
            skill.PlusHit = true;

            Skill_Extended sex=skill.ExtendedAdd(new Skill_Extended());
            sex.SkillBasePlus.Target_BaseDMG=record.dmg-1;

            List<BattleChar> list = new List<BattleChar>();
            foreach(BattleChar bc in record.targets)
            {
                list.AddRange(bc.MyTeam.AliveChars.FindAll(a => !list.Contains(a)));
            }
            this.BChar.ParticleOut(skill, list);



            yield break;
        }
        public List<B91_rec> recs=new List<B91_rec>();
    }
    public class B91_rec
    {
        public SkillParticle sp = new SkillParticle();
        public List<BattleChar> targets = new List<BattleChar>();
        public int dmg = 0;
    }
    public class Sex_agt_9_1_1 : Sex_ImproveClone
    {
        public override void Init()
        {
            base.Init();
            
        }

    }

    public class P_Agent_Doll_9 : P_Agent_Doll
    {
        public override List<string> GetSelfBuff()
        {
            List<string> list = base.GetSelfBuff();
            list.Add("B_SF_Agent_9_2");
            return list;
        }
        public override void PassiveStatSet(BattleChar battlechar)
        {
            base.PassiveStatSet(battlechar);

            this.PlusStat.atk = (int)(1f * this.GetStatOfMaster.atk);
            this.PlusStat.hit = (int)(1f * this.GetStatOfMaster.hit);
            this.PlusStat.def = (int)(20+1.2f * this.GetStatOfMaster.def);
            this.PlusStat.dod = (int)(0f * this.GetStatOfMaster.dod);
            this.PlusStat.cri = (int)(1f * this.GetStatOfMaster.cri);
            this.PlusStat.maxhp = (int)Math.Max(1, 1f * this.GetStatOfMaster.maxhp);
            this.MyChar.Hp = this.PlusStat.maxhp;
        }
    }
    public class Sex_agt_9_2 : Agent_Sum_Extended
    {
        public override string DescExtended(string desc)
        {
            BattleChar bc = P_Agent_Doll.FirstSummoner(this.BChar);//改为根源召唤者
            float atk = (int)(1f * bc.GetStat.atk);
            float hit = (int)(1f * bc.GetStat.hit);
            float def = (int)Math.Min(80, 20 + 1.2f * bc.GetStat.def);
            float dod = (int)Math.Min(80, 0f * bc.GetStat.dod);
            float cri = (int)(1f * bc.GetStat.cri);
            float maxhp = (int)Math.Max(1, 1f * bc.GetStat.maxhp);
            return base.DescExtended(desc).Replace("&atk", atk.ToString()).Replace("&hit", hit.ToString()).Replace("&def", def.ToString()).Replace("&dod", dod.ToString()).Replace("&cri", cri.ToString()).Replace("&maxhp", maxhp.ToString()).Replace("&user", bc.Info.Name);
        }
        public override int GetMaxHp(BattleChar bc)
        {
            Stat stat = AllySummon.OrgStat(bc);
            return (int)Math.Max(1, 1f * this.BChar.GetStat.maxhp);
        }
        public override string GetAllyKey()
        {
            return "SF_Manticore";
        }

        public override string GetBasicKey()
        {
            return "S_SF_Agent_9_2_2";
        }

    }
    public class Bcl_agt_9_2 : Bcl_agt_doll, IP_PlayerTurn,IP_DamageTakeChange_sumoperation//,IP_Damage_Before
    {
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            this.TurnAttack();

        }
        public override void Init()
        {
            base.Init();
            this.PlusStat.AggroPer = 50;
            this.PlusStat.Penetration = 100;

            float master_def = this.Usestate_L.GetStat.def;
            if (this.View)
            {
                master_def = master_def * 1.2f + 20f;
            }
            this.armor = Math.Max(1, (int)((master_def + 30) / 20));
        }

        public override string DescExtended()
        {
            return base.DescExtended().Replace("&arm", this.armor.ToString());
        }
        /*
        public void Damage_Before(BattleChar battleChar, BattleChar User, int Dmg, bool Cri, ref bool Pain, ref bool NOEFFECT, int PlusPenetration, bool IgnoreHealingPro, bool HealingPro, bool OnlyUnscaleTime)
        {
            if (battleChar == this.BChar && Pain)
            {
                Pain = false;
            }
        }
        */
        public void DamageTakeChange_sumoperation(BattleChar Hit, BattleChar User, int Dmg, bool Cri, ref int PlusDmg, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {
            if (Hit == this.BChar && !NODEF)
            {
                PlusDmg = -this.armor;
            }
        }
        
        public int armor = 0;
        public new void Turn()
        {
            base.Turn();
            this.TurnAttack();

        }
        public override void TurnAttack()
        {
            BattleChar bc = this.BChar;

            Skill skill = Skill.TempSkill("S_SF_Agent_9_2_1", bc, bc.MyTeam);
            BattleTeam.SkillRandomUse(bc, skill, false, true, false);
        }
    }

    public class Sex_agt_9_2_1 : Sex_ImproveClone
    {


    }

    public class Sex_agt_9_2_2 : Agent_Doll_Basic_extended, IP_SkillUse_Target,IP_DamageChange_sumoperation
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", (this.BChar.GetStat.HIT_CC + 100).ToString());
        }
        public override void Init()
        {
            base.Init();


        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            foreach (BattleChar bc in Targets)
            {
                if(bc.MyTeam.AliveChars.Find(a=> a!=bc && a.HP>bc.HP)==null)
                {
                    bc.BuffAdd("B_Common_Rest", this.BChar, false, 100, false, -1, false);
                }
                if (bc.MyTeam.AliveChars.Find(a => a != bc && a.HP < bc.HP) == null)
                {
                    this.plist1.Add(bc);
                }
            }
        }
        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (SkillD == this.MySkill && !View)
            {
                //Debug.Log("DamageChange_sumoperation:" + Target.Info.Name);
                //Debug.Log("DamageChange_sumoperation:" + Target.HP);
                this.targetHP = Target.HP;

                if (this.plist1.Contains(Target))
                {
                    Cri = true;
                }
            }

        }
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if(hit.IsDead && SP.SkillData==this.MySkill)
            {
                //Debug.Log("AttackEffect:" + hit.Info.Name);
                //Debug.Log("AttackEffect:" + DMG);
                //Debug.Log("AttackEffect:" + this.targetHP);
                int num = DMG - this.targetHP;
                if(num>0)
                {
                    BattleSystem.DelayInput(this.PlusAttack(hit.MyTeam, num));
                }
            }
        }

        public IEnumerator PlusAttack(BattleTeam team,int num)
        {
            yield return new WaitForSecondsRealtime(0.4f);

            Skill skill = Skill.TempSkill("S_SF_Agent_9_2_3", this.BChar, this.BChar.MyTeam);
            skill.FreeUse = true;
            skill.PlusHit = true;
            skill.NeverCri = true;

            Skill_Extended sex = skill.ExtendedAdd(new Skill_Extended());
            sex.SkillBasePlus.Target_BaseDMG = num-1;//-----------------------

            List<BattleChar> list = new List<BattleChar>();
            list.AddRange(team.AliveChars);
            list.RemoveAll(a => list.Find(b=>b!=a && b.HP<a.HP)!=null);
            if(list.Count>0)
            {
                this.BChar.ParticleOut(skill, list.Random(this.BChar.GetRandomClass().Main));

            }



            yield break;
        }

        public int targetHP = 0;
        public List<BattleChar> plist1= new List<BattleChar>(); 
    }


    public class Sex_agt_10 : Sex_ImproveClone
    {


    }
    public class Bcl_agt_10_1 :Buff
    {
        public override string DescExtended()
        {
            //Debug.Log("this.BarrierHP:" + this.BarrierHP);
            //Debug.Log("this.maxRecord:" + this.maxRecord);
            return base.DescExtended().Replace("&a", Math.Min(30, (int)(30*this.BarrierHP/this.maxRecord)).ToString());
        }
        public override void Init()
        {
            base.Init();
            this.PlusStat.AggroPer = 80;
            this.BarrierHP += (int)(this.Usestate_L.GetStat.atk * (30 + this.Usestate_L.GetStat.maxhp) / 35);
            this.maxRecord = Math.Max(this.BarrierHP,1);
        }
        public override void DestroyByTurn()
        {
            base.DestroyByTurn();
            if(this.BarrierHP > 0)
            {
                int num = Math.Min(30, (int)(30*this.BarrierHP / this.maxRecord));
                foreach (BattleChar battleChar in BattleSystem.instance.AllyTeam.AliveChars)
                {
                    Buff buff = battleChar.BuffAdd("B_SF_Agent_10_2", this.BChar, false, 0, false, -1, false);
                    buff.PlusPerStat.Damage = num;
                    battleChar.BuffAdd("B_SF_Agent_10_3", this.BChar, false, 0, false, -1, false);

                }
                /*
                foreach(Skill skill in BattleSystem.instance.AllyTeam.Skills)
                {
                    Skill_Extended sex = skill.ExtendedAdd(new Skill_Extended());
                    sex.APChange = -1;
                }
                */
            }

        }
        private int maxRecord = 1;
    }
    public class Bcl_agt_10_2 : Buff
    {

    }
    public class Bcl_agt_10_3 : Buff, IP_SkillUse_Team
    {
        // Token: 0x06001148 RID: 4424 RVA: 0x00090D5C File Offset: 0x0008EF5C
        public override void Init()
        {
            base.Init();
            this.PlusStat.PlusMPUse.PlusMP_Skills = -1;
            this.PlusStat.PlusMPUse.PlusMP_Fixed = -1;
        }

        // Token: 0x06001149 RID: 4425 RVA: 0x00090D71 File Offset: 0x0008EF71
        public void SkillUseTeam(Skill skill)
        {
            if (skill.Master == this.BChar)
            {
                base.SelfDestroy(false);
            }
        }
    }

    public class Sex_agt_11 : Sex_ImproveClone
    {
        public override void Init()
        {
            base.Init();
            if(!this.shooting)
            {
                BuffTag btg = new BuffTag();
                btg.BuffData = new GDEBuffData("B_SF_Agent_11");
                btg.User = this.BChar;
                this.SelfBuff.Add(btg);

            }
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            List<BuffTag> tags = new List<BuffTag>();
            tags.AddRange(this.SelfBuff.FindAll(a => a.BuffData.Key == "B_SF_Agent_11"));
            this.SelfBuff.RemoveAll(a => tags.Contains(a));
            foreach (BuffTag tag in tags)
            {
                this.BChar.BuffAdd(tag.BuffData.Key, this.BChar, false, 0, false, -1, false);
            }
            this.shooting = true;
            
            base.SkillUseSingle(SkillD, Targets);
            Skill skill = this.MySkill.CloneSkill(true, null, null, true);
            skill.isExcept = true;
            skill.FreeUse = true;
            skill.PlusHit = true;



            BattleSystem.instance.StartCoroutine(this.Plushit(Targets[0], skill,3));

        }
        public IEnumerator Plushit(BattleChar tar0, Skill skill,int num)//追加射击
        {
            for (int i = 0; i < num; i++)
            {
                //Debug.Log("次数检测: " + i);
                if (BattleSystem.instance.EnemyTeam.AliveChars.Count >= 1)
                {
                    yield return new WaitForSecondsRealtime(0.3f);
                    if (tar0.IsDead || tar0 == null)
                    {
                        this.BChar.ParticleOut(this.MySkill, skill, tar0.MyTeam.AliveChars.Random(this.BChar.GetRandomClass().Main));
                    }
                    else
                    {
                        this.BChar.ParticleOut(this.MySkill, skill, tar0);
                    }
                }

            }


            yield break;
        }
        private bool shooting = false;
    }
    public class Bcl_agt_11 : Buff, IP_ParticleOut_After_Global
    {
        public override string DescExtended()
        {
            
            return base.DescExtended().Replace("&a1", (this.BChar.GetStat.HIT_CC + 70).ToString()).Replace("&a2", (this.BChar.GetStat.HIT_DEBUFF + 110).ToString()).Replace("&b", (0.5*this.doubleNum).ToString()).Replace("&c", this.maxnum.ToString()).Replace("&user",this.BChar.Info.Name);
        }
        public IEnumerator ParticleOut_After_Global(Skill SkillD, List<BattleChar> Targets)
        {
            if(SkillD.Master==this.BChar || Bcl_agt_11.CheckSummon(this.BChar,SkillD.Master))
            {
                if(SkillD.IsDamage)
                {
                    if(!SkillD.PlusHit)
                    {
                        this.doubleNum += 2;
                    }
                    else
                    {
                        this.doubleNum += 1;
                    }
                    if (this.doubleNum >= (2 * this.maxnum)) 
                    {
                        this.doubleNum -= (2 * this.maxnum);
                        /*
                        BuffTag btg = new BuffTag();
                        btg.BuffData = new GDEBuffData("B_Common_Rest");
                        btg.PlusTagPer = 100;
                        btg.User = this.BChar;
                        Skill_Extended sex = SkillD.ExtendedAdd(new Skill_Extended());
                        sex.TargetBuff.Add(btg);
                        */
                        foreach(BattleChar tar in Targets)
                        {
                            EffectView.TextOutSimple(this.BChar, this.BuffData.Name);
                            tar.BuffAdd("B_SF_Agent_11_1", this.BChar, false, 0, false, -1, false);
                        }
                    }
                }
            }
            yield break;
        }
        public static bool CheckSummon(BattleChar masterchar, BattleChar checkchar)
        {
            BattleChar bc = checkchar;
        Tele_001:
            if (bc.Info.Passive is P_Agent_Doll pas)
            {

                //Bcl_agt_sum_p buff =bc.BuffReturn("B_SF_Agent_sum_p", false) as Bcl_agt_sum_p;
                
                if(pas.GetMaster== masterchar)
                {
                    return true;
                }
                else if(pas.GetMaster.Info.Passive is P_Agent_Doll)
                {
                    bc = pas.GetMaster;
                    goto Tele_001;
                }
                
            }
            return false ;
        }

        public int doubleNum = 0;
        public int maxnum = 6;
    }
    public class Bcl_agt_11_1 : Common_Buff_Rest,IP_DebuffResist
    {
        public void Resist()
        {
            this.BChar.BuffAdd("B_SF_Agent_11_2", this.Usestate_L, false, 0, false, -1, false);
        }
    }
    public class Bcl_agt_11_2 : Buff
    {
        public override void BuffStat()
        {
            base.BuffStat();
            this.PlusStat.DMGTaken = 12 * this.StackNum;
        }
    }
    public class Sex_agt_12_LD : Sex_ImproveClone,IP_SomeOneDead,IP_BuffUpdate
    {
        public void SomeOneDead(BattleChar DeadChar)
        {
            int num = BattleSystem.instance.AllyTeam.AliveChars_Vanish.FindAll(a => a.BuffFind("B_SF_Agent_sum_p", false)).Count;
            this.APChange=-Math.Max(0, num);
        }
        public override void Init()
        {
            base.Init();
            if(BattleSystem.instance!=null)
            {
                int num = BattleSystem.instance.AllyTeam.AliveChars_Vanish.FindAll(a => a.BuffFind("B_SF_Agent_sum_p", false)).Count;
                this.APChange = -Math.Max(0, num);
            }
        }
        public void BuffUpdate(Buff MyBuff)
        {
            int num = BattleSystem.instance.AllyTeam.AliveChars_Vanish.FindAll(a => a.BuffFind("B_SF_Agent_sum_p", false)).Count;
            this.APChange = -Math.Max(0, num);
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            for (int i = 0; i < 4; i++)
            {
                BattleSystem.DelayInputAfter(this.Draw());
            }
        }

        public IEnumerator Draw()
        {
            List<Skill> list_skill = new List<Skill>();
            foreach (Skill skill in BattleSystem.instance.AllyTeam.Skills_Deck)
            {
                if(this.drawlist.Find(a=>a.Master==skill.Master)==null)
                {
                    yield return BattleSystem.instance.AllyTeam._ForceDraw(skill, new BattleTeam.DrawInput(this.Drawinput));
                    //this.drawlist.Add(skill);
                    goto Tele_001;
                }
            }


            BattleSystem.instance.AllyTeam.Draw(1, new BattleTeam.DrawInput(this.Drawinput));
            Tele_001:
            yield return null;
            yield break;
        }
        public void Drawinput(Skill skill)
        {
            //Debug.Log("Drawinput");
            this.drawlist.Add(skill);
        }
        public  List<Skill> drawlist=new List<Skill>();
    }















    public class E_SF_Agent_0 : EquipBase, IP_SomeOneDead
    {
        public override void Init()
        {
            base.Init();

            this.PlusPerStat.Damage = 15;
            this.PlusPerStat.MaxHP = 10;
            this.PlusStat.def = 10;
        }
        public void SomeOneDead(BattleChar DeadChar)
        {
            if (DeadChar is BattleAlly ally && ally != this.BChar)
            {
                this.BChar.Overload = 0;
            }
        }
    }


    public class SkillEn_SF_Agent_0 : Sex_ImproveClone//, IP_SkillUse_Target
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return true;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            foreach(BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars)
            {
                foreach(Buff buff in bc.Buffs)
                {
                    if(buff is Bcl_agt_doll dollbuff)
                    {
                        dollbuff.TurnAttack();
                    }
                }
            }
        }

    }
    public class SkillEn_SF_Agent_1 : Sex_ImproveClone//, IP_SkillUse_Target
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.IsHeal;
        }

        public override void BeforeHeal(BattleChar hit, SkillParticle SP, float Heal, bool Cri)
        {
            base.BeforeHeal(hit, SP, Heal, Cri);
            if(hit.BuffFind("B_SF_Agent_sum_p",false))
            {
                //Debug.Log("找到buff");
                this.SkillBasePlus.TargetForceHeal = true;
                if(hit.Buffs.Find(a=>a is Bcl_agt_doll)!=null)
                {
                    List<Buff> list = hit.Buffs.FindAll(a => a is Bcl_agt_doll);
                    foreach(Buff buff in list)
                    {
                        (buff as Bcl_agt_doll).ActiveOverHeal();
                    }
                }
            }
            else
            {
                //Debug.Log("未找到buff");
                this.SkillBasePlus.TargetForceHeal = false;
            }
        }

    }
}
