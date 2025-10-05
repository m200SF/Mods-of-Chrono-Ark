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
using ChronoArkMod.ModData.Settings;
using System.Linq.Expressions;
using ChronoArkMod.ModData;
using ChronoArkMod.Template;
using UnityEngine.UI;
using Steamworks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

using static UnityEngine.UI.Image;
using static DemoToonVFX;

namespace M200_MirrorImageFixed
{
    [PluginConfig("M200_MirrorImageFixed_CharMod", "M200_MirrorImageFixed_Mod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class M200_MirrorImageFixed_ModPlugin : ChronoArkPlugin
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
        public class M200_MirrorImageFixed_Def : ModDefinition
        {
        }
    }


    /*
    [HarmonyPatch(typeof(BattleAlly))]
    [HarmonyPatch("UseSkillAfter")]
    //[HarmonyPatch(new System.Type[] { typeof(Skill) })]
    public static class UseSkillAfter_Plugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void UseSkillAfter_Before_patch(BattleAlly __instance, Skill skill)
        {

                foreach (IP_UseSkillAfter_Before ip_patch in skill.IReturn<IP_UseSkillAfter_Before>())
                {
                    //Debug.Log("only damage drm");
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.UseSkillAfter_Before(__instance, skill);
                    }
                }
            
        }
    }
    
    public interface IP_UseSkillAfter_Before
    {
        // Token: 0x06000001 RID: 1
        void UseSkillAfter_Before(BattleChar user, Skill SkillD);
    }
    */
    [HarmonyPatch(typeof(BattleSystem))]
    [HarmonyPatch("SkillTargetAdd")]
    [HarmonyPatch(new System.Type[] { typeof(BattleChar) })]
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
    [HarmonyPatch(new System.Type[] { typeof(Skill) })]
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



    [HarmonyPatch(typeof(Extended_Trisha_2))]
    [HarmonyPatch("SkillTargetSingle")]
    public static class SkillTargetSinglePlugin
    {
        // Token: 0x06000015 RID: 21 RVA: 0x00002410 File Offset: 0x00000610
        [HarmonyPrefix]
        public static bool SkillTargetSingle_before_patch(Extended_Trisha_2 __instance, List<Skill> Targets)
        {
            if (Targets[0].Master != __instance.BChar.MyTeam.LucyChar && Targets[0].MySkill.KeyID != GDEItemKeys.Skill_S_Trisha_2)
            {
                Skill skill = Targets[0].CloneSkill(true, __instance.BChar, null, true);
                skill.ExtendedAdd_Battle(new Sex_Teisha_2_1_Fixed());
                skill.Master = __instance.BChar;
                __instance.BChar.MyTeam.Add(skill, true);
            }
            return false;
        }
    }
    
    public class Sex_Teisha_2_1_Fixed:Skill_Extended, IP_SkillUsePatch_Before
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc) + "\n" + ScriptLocalization.CharText_Trisha.Skill_2_PlusDesc;
        }

        // Token: 0x0600130D RID: 4877 RVA: 0x00099B34 File Offset: 0x00097D34
        public void SkillUsePatch_Before(BattleChar TargetChar, Skill TargetSkill)
        {
            if (this.MySkill.Disposable || this.MySkill.isExcept)
            {
                return;
            }
            this.MySkill.isExcept = true;
            this.BChar.MyTeam.Skills_UsedDeck.Add(Skill.TempSkill(GDEItemKeys.Skill_S_Trisha_2, this.BChar, this.BChar.MyTeam));
        }
    }
}
