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

using ChronoArkMod;
using DarkTonic.MasterAudio;

using ChronoArkMod.ModData;
using ChronoArkMod.Template;
using ChronoArkMod.ModData.Settings;
using UnityEngine.UI;
using static DemoToonVFX;
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

using HarmonyLib;
using System.Security.Cryptography;





namespace SF_Executioner
{
    [PluginConfig("M200_SF_Executioner_CharMod", "SF_Executioner_CharMod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class SF_Executioner_CharacterModPlugin : ChronoArkPlugin
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
        public class ExecutionerDef : ModDefinition
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
            //MasterAudio.PlaySound("A_exc_4", 1f, null, 0f, null, null, false, false);
            float volume = 0f;
            float volume2 = 0f;
            if (ModManager.getModInfo("SF_Executioner").GetSetting<ToggleSetting>("AudioOn").Value)
            {
                volume = ModManager.getModInfo("SF_Executioner").GetSetting<SliderSetting>("AudioVolume").Value;
            }
            else
            {
                volume = 0f;
            }

            if (inText.Contains("T_exc_1_1"))
            {
                inText = inText.Replace("T_exc_1_1:", string.Empty);
                MasterAudio.PlaySound("A_exc_1_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_exc_1_2"))
            {
                inText = inText.Replace("T_exc_1_2:", string.Empty);
                MasterAudio.PlaySound("A_exc_1_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_exc_2_1"))
            {
                inText = inText.Replace("T_exc_2_1:", string.Empty);
                MasterAudio.PlaySound("A_exc_2_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_exc_2_2"))
            {
                inText = inText.Replace("T_exc_2_2:", string.Empty);
                MasterAudio.PlaySound("A_exc_2_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_exc_3_1"))
            {
                inText = inText.Replace("T_exc_3_1:", string.Empty);
                MasterAudio.PlaySound("A_exc_3_1", volume, null, 1f, null, null, false, false);
            }
            else if (inText.Contains("T_exc_4_1"))
            {
                inText = inText.Replace("T_exc_4_1:", string.Empty);
                MasterAudio.PlaySound("A_exc_4_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_exc_4_2"))
            {
                inText = inText.Replace("T_exc_4_2:", string.Empty);
                MasterAudio.PlaySound("A_exc_4_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_exc_5_1"))
            {
                inText = inText.Replace("T_exc_5_1:", string.Empty);
                MasterAudio.PlaySound("A_exc_5_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_exc_5_2"))
            {
                inText = inText.Replace("T_exc_5_2:", string.Empty);
                MasterAudio.PlaySound("A_exc_5_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_exc_6_1"))
            {
                inText = inText.Replace("T_exc_6_1:", string.Empty);
                MasterAudio.PlaySound("A_exc_6_1", volume, null, 1f, null, null, false, false);
            }
            else if (inText.Contains("T_exc_6_2"))
            {
                inText = inText.Replace("T_exc_6_2:", string.Empty);
                MasterAudio.PlaySound("A_exc_6_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_exc_7_1"))
            {
                inText = inText.Replace("T_exc_7_1:", string.Empty);
                MasterAudio.PlaySound("A_exc_7_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_exc_7_2"))
            {
                inText = inText.Replace("T_exc_7_2:", string.Empty);
                MasterAudio.PlaySound("A_exc_7_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_exc_8_1"))
            {
                inText = inText.Replace("T_exc_8_1:", string.Empty);
                MasterAudio.PlaySound("A_exc_8_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_exc_9_1"))
            {
                inText = inText.Replace("T_exc_9_1:", string.Empty);
                MasterAudio.PlaySound("A_exc_9_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_exc_9_2"))
            {
                inText = inText.Replace("T_exc_9_2:", string.Empty);
                MasterAudio.PlaySound("A_exc_9_2", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_exc_10_1"))
            {
                inText = inText.Replace("T_exc_10_1:", string.Empty);
                MasterAudio.PlaySound("A_exc_10_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_exc_11_1"))
            {
                inText = inText.Replace("T_exc_11_1:", string.Empty);
                MasterAudio.PlaySound("A_exc_11_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_exc_11_2"))
            {
                inText = inText.Replace("T_exc_11_2:", string.Empty);
                MasterAudio.PlaySound("A_exc_11_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_exc_11_3"))
            {
                inText = inText.Replace("T_exc_11_3:", string.Empty);
                MasterAudio.PlaySound("A_exc_11_3", volume, null, 0f, null, null, false, false);
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
            bool flag = inputitem.itemkey == "E_SF_Executioner_0" && __instance.Info.KeyData != "SF_Executioner";
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

            if (CharId == "SF_Executioner")
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
    
    public class S_SF_Executioner_8_Particle : CustomSkillGDE<SF_Executioner_CharacterModPlugin.ExecutionerDef>
    {
        // Token: 0x060000AB RID: 171 RVA: 0x000055A0 File Offset: 0x000037A0
        public override ModGDEInfo.LoadingType GetLoadingType()
        {
            return ModGDEInfo.LoadingType.Replace;
        }

        // Token: 0x060000AC RID: 172 RVA: 0x000055B4 File Offset: 0x000037B4
        public override string Key()
        {
            return "S_SF_Executioner_8";
        }

        // Token: 0x060000AD RID: 173 RVA: 0x000055CB File Offset: 0x000037CB
        public override void SetValue()
        {
            base.Particle = base.assetInfo.CodeConstuctAsync<GameObject>("Particle/Hein/S_Hein_3", new ModAssetInfo.ObjectChangeDelegate<GameObject>(this.ParticleChangeDelegate));//"Particle/Twins/Hel_2"
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
            skillParticle.AllHit = false;
            skillParticle.AllHitForce = false;
            //skillParticle.GroundOut = true;

            return particle;

        }

    }


    [HarmonyPatch(typeof(SkillParticle))]
    public static class SkillParticlePlugin
    {
        // Token: 0x06000005 RID: 5 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPatch("DeadEvent")]
        [HarmonyPostfix]
        public static void DeadEvent_Patch_After(SkillParticle __instance, BattleChar bc)
        {
            foreach (IP_DeadEvent_After ip_DeadEvent_After in __instance.UseStatus.IReturn<IP_DeadEvent_After>(__instance.SkillData))
            {
                bool flag = ip_DeadEvent_After != null;
                bool flag2 = flag;
                if (flag2)
                {
                    ip_DeadEvent_After.DeadEvent_After(__instance, bc);
                }
            }
        }
    }
    public interface IP_DeadEvent_After
    {
        // Token: 0x06000006 RID: 6
        void DeadEvent_After(SkillParticle sp, BattleChar bc);
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

    [HarmonyPatch(typeof(BattleChar))]
    public static class OnlyDamagePlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x00002110 File Offset: 0x00000310
        [HarmonyPatch("OnlyDamage")]
        [HarmonyPrefix]
        public static void OnlyDamage_Patch_Before(BattleChar __instance, int Dmg, ref bool Weak, bool IgnoreHealPro = false, BattleChar User = null, bool Cri = false)
        {
            foreach (IP_OnlyDamage_Before ip_OnlyDamage_Before in __instance.IReturn<IP_OnlyDamage_Before>(null))
            {
                bool flag = ip_OnlyDamage_Before != null;
                bool flag2 = flag;
                if (flag2)
                {
                    ip_OnlyDamage_Before.OnlyDamage_Before(__instance, Dmg, ref Weak, IgnoreHealPro, User, Cri);
                }
            }
        }
    }
    public interface IP_OnlyDamage_Before
    {
        // Token: 0x06000008 RID: 8
        void OnlyDamage_Before(BattleChar hit, int Dmg, ref bool Weak, bool IgnoreHealPro, BattleChar User, bool Cri);
    }

    [HarmonyPatch(typeof(SkillParticle))]
    public class SkillParticle_init_Plugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("init")]
        [HarmonyPatch(new Type[] { typeof(Skill), typeof(BattleChar), typeof(List<BattleChar>) })]
        [HarmonyPrefix]
        public static void init_Patch(SkillParticle __instance, Skill skill, BattleChar User, List<BattleChar> Target)
        {
            if (skill == null)
            {
                return;
            }
            try
            {

                bool flag1 = false;

                foreach (IP_SetNoCG ip_patch in skill.IReturn<IP_SetNoCG>())
                {

                    if (ip_patch != null)
                    {
                        bool flag2 = ip_patch.SetNoCG();
                        flag1 = flag1 || flag2;
                    }

                }
                if (flag1)
                {
                    Traverse.Create(__instance).Field("CantSpacialCG").SetValue(true);
                }
            }
            catch
            {

            }
        }
    }

    public interface IP_SetNoCG
    {
        // Token: 0x06000001 RID: 1
        bool SetNoCG();
    }










    public class P_Executioner : Passive_Char, IP_ParticleOut_Before, IP_SkillUse_User, IP_LevelUp//, IP_OnlyDamage_Before
    {
        // Token: 0x06000009 RID: 9 RVA: 0x00002180 File Offset: 0x00000380
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
                list.Add(ItemBase.GetItem("E_SF_Executioner_0", 1));
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

        // Token: 0x0600000A RID: 10 RVA: 0x00002194 File Offset: 0x00000394
        public void ParticleOut_Before(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == this.BChar)
            {
                bool flag2 = false;
                if (SkillD.IsDamage)
                {
                    flag2 = (SkillD.AllExtendeds.Find((Skill_Extended a) => a is Sex_exc_p_1) == null);
                }
                else
                {
                    flag2 = false;
                }
                if (flag2)
                {
                    Sex_exc_p_1 sex_exc_p_ = SkillD.ExtendedAdd(new Sex_exc_p_1()) as Sex_exc_p_1;
                    foreach (BattleChar battleChar in Targets)
                    {
                        sex_exc_p_.records.Add(new HPrecord
                        {
                            target = battleChar,
                            remainHP = battleChar.HP,
                            damage = 0
                        });
                    }
                }
            }
        }


        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == this.BChar)
            {
                this.turn = BattleSystem.instance.TurnNum;
                this.actionnum = BattleSystem.instance.AllyTeam.TurnActionNum;
            }
        }
        /*
        
        public void OnlyDamage_Before(BattleChar hit, int Dmg, ref bool Weak, bool IgnoreHealPro, BattleChar User, bool Cri)
        {
            if (hit == this.BChar)
            {
                foreach (CastingSkill castingSkill in BattleSystem.instance.CastSkills)
                {
                    if (castingSkill.Usestate == this.BChar)
                    {
                        Weak = true;
                        return;
                    }
                }
                foreach (CastingSkill castingSkill2 in BattleSystem.instance.SaveSkill)
                {
                    if (castingSkill2.Usestate == this.BChar)
                    {
                        Weak = true;
                        return;
                    }
                }
                if (this.turn == BattleSystem.instance.TurnNum && this.actionnum == BattleSystem.instance.AllyTeam.TurnActionNum)
                {
                    Weak = true;
                }
            }
        }
        */
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.frame++;
            if (this.frame >= 20 || this.frame < 0)
            {
                if (BattleSystem.instance.CastSkills.Find(a => a.Usestate == this.BChar) != null || BattleSystem.instance.SaveSkill.Find(a => a.Usestate == this.BChar) != null)
                {
                    this.PlusStat.Strength = true;
                    this.PlusStat.AggroPer = 60;
                }
                else if (this.turn == BattleSystem.instance.TurnNum && this.actionnum == BattleSystem.instance.AllyTeam.TurnActionNum)
                {
                    this.PlusStat.Strength = true;
                    this.PlusStat.AggroPer = 60;
                }
                else
                {
                    this.PlusStat.Strength = false;
                    this.PlusStat.AggroPer = 0;
                }
            }
        }
        public int frame = 0;
        // Token: 0x04000002 RID: 2
        public int turn = 0;

        // Token: 0x04000003 RID: 3
        public int actionnum = 0;
    }


    public class Sex_exc_p_1 : Sex_ImproveClone, IP_DeadEvent_After, IP_SkillUse_Target
    {
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (SP.SkillData == this.MySkill)
            {
                foreach (HPrecord hprecord in this.records)
                {
                    if (hprecord.target == hit)
                    {
                        hprecord.damage = DMG;
                    }
                }
            }
        }
        public void DeadEvent_After(SkillParticle sp, BattleChar bc)
        {
            if (sp.SkillData == this.MySkill && bc.IsDead)
            {
                if (this.records.Find((HPrecord a) => a.target == bc) != null)
                {
                    HPrecord hprecord = this.records.Find((HPrecord a) => a.target == bc);
                    float num = (float)(hprecord.damage - hprecord.remainHP);



                    if (num > 0f)
                    {
                        bool flag1 = false;
                        foreach (IP_CheckEquip ip_new in this.BChar.IReturn<IP_CheckEquip>())
                        {
                            try
                            {
                                bool flag2 = false;
                                if (ip_new != null)
                                {
                                    flag2 = ip_new.CheckEquip(this.BChar);
                                }
                                flag1 = flag1 || flag2;
                            }
                            catch
                            {
                            }
                        }

                        if (flag1)
                        {
                            this.MySkill.Master.Heal(this.MySkill.Master, (float)(1f * (double)num), false, true, null);
                        }
                        else
                        {
                            this.MySkill.Master.Heal(this.MySkill.Master, (float)(1f * (double)num), false, false, null);
                        }
                    }
                }
            }
        }
        public List<HPrecord> records = new List<HPrecord>();
    }
    public class HPrecord
    {
        // Token: 0x04000005 RID: 5
        public BattleChar target = new BattleChar();

        // Token: 0x04000006 RID: 6
        public int remainHP = 0;

        // Token: 0x04000007 RID: 7
        public int damage = 0;
    }


    public class Bcl_exc_dot_1 : Buff, IP_BuffUpdate
    {
        // Token: 0x06000012 RID: 18 RVA: 0x0000257E File Offset: 0x0000077E
        public override void BuffStat()
        {
            base.BuffStat();
            this.PlusStat.DMGTaken = (float)(5 + 5 * base.StackNum);
        }
        public override void Init()
        {
            base.Init();
            this.BuffUpdate(this);
        }
        // Token: 0x06000013 RID: 19 RVA: 0x000025A0 File Offset: 0x000007A0
        public void BuffUpdate(Buff MyBuff)
        {
            if (MyBuff != this && Time.frameCount > this.updateFrame + 3)
            {
                this.updateFrame = Time.frameCount;
                this.StatCheck();
            }


            int num1 = Bcl_exc_12.StaticCheckB12();
            GDEBuffData gde = new GDEBuffData("B_SF_Executioner_dot_1");
            if (this.BuffData.MaxStack != (num1 + gde.MaxStack))
            {
                Traverse.Create(this.BuffData).Field("_MaxStack").SetValue(gde.MaxStack + num1);
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.frame++;
            if (this.frame > 20 || this.frame < 0)
            {
                this.frame = 0;
                this.StatCheck();
            }

        }
        public void StatCheck()
        {
            GDESkillEffectData tick = new GDESkillEffectData("SE_SF_Executioner_dot");
            if (this.BChar.BuffFind("B_SF_Executioner_dot_2", false))
            {
                if (this.BuffData.Tick.DMG_Per != 2 * tick.DMG_Per)
                {
                    Traverse.Create(this.BuffData.Tick).Field("_DMG_Per").SetValue(2 * tick.DMG_Per);
                    base.BuffStatUpdate();
                }
            }
            else
            {
                if (this.BuffData.Tick.DMG_Per != tick.DMG_Per)
                {
                    Traverse.Create(this.BuffData.Tick).Field("_DMG_Per").SetValue(tick.DMG_Per);
                    base.BuffStatUpdate();
                }
            }
        }
        // Token: 0x04000008 RID: 8
        public int updateFrame = 0;
        public int frame = 0;
    }
    public class Bcl_exc_dot_2 : Buff, IP_BuffUpdate
    {
        // Token: 0x06000015 RID: 21 RVA: 0x000026A1 File Offset: 0x000008A1
        public override void BuffStat()
        {
            base.BuffStat();
            this.PlusPerStat.Damage = -(5 + 5 * base.StackNum);
        }
        public override void Init()
        {
            base.Init();
            this.BuffUpdate(this);
        }
        // Token: 0x06000016 RID: 22 RVA: 0x000026C4 File Offset: 0x000008C4
        public void BuffUpdate(Buff MyBuff)
        {
            if (MyBuff != this && Time.frameCount > this.updateFrame + 3)
            {
                this.updateFrame = Time.frameCount;
                this.StatCheck();
            }

            int num1 = Bcl_exc_12.StaticCheckB12();
            GDEBuffData gde = new GDEBuffData("B_SF_Executioner_dot_2");
            if (this.BuffData.MaxStack != (num1 + gde.MaxStack))
            {
                Traverse.Create(this.BuffData).Field("_MaxStack").SetValue(gde.MaxStack + num1);
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.frame++;
            if (this.frame > 20 || this.frame < 0)
            {
                this.frame = 0;
                this.StatCheck();
            }
        }
        public void StatCheck()
        {
            GDESkillEffectData tick = new GDESkillEffectData("SE_SF_Executioner_dot");
            if (this.BChar.BuffFind("B_SF_Executioner_dot_1", false))
            {
                if (this.BuffData.Tick.DMG_Per != 2 * tick.DMG_Per)
                {
                    Traverse.Create(this.BuffData.Tick).Field("_DMG_Per").SetValue(2 * tick.DMG_Per);
                    base.BuffStatUpdate();
                }
            }
            else
            {
                if (this.BuffData.Tick.DMG_Per != tick.DMG_Per)
                {
                    Traverse.Create(this.BuffData.Tick).Field("_DMG_Per").SetValue(tick.DMG_Per);
                    base.BuffStatUpdate();
                }
            }
        }
        // Token: 0x04000009 RID: 9
        public int updateFrame = 0;
        public int frame = 0;
    }


    public class Sex_exc_1 : Sex_ImproveClone
    {
        // Token: 0x06000018 RID: 24 RVA: 0x000027C8 File Offset: 0x000009C8
        public override void AttackEffectSingle(BattleChar hit, SkillParticle SP, int DMG, int Heal)
        {
            base.AttackEffectSingle(hit, SP, DMG, Heal);
            if (!hit.BuffFind("B_SF_Executioner_dot_1", false))
            {
                //hit.BuffAdd("B_SF_Executioner_dot_1", this.BChar, false, 0, false, -1, false);
                Skill skill = Skill.TempSkill("S_SF_Executioner_1", this.BChar);

                skill.isExcept = true;
                skill.AutoDelete = 1;
                skill.BasicSkill = false;

                this.BChar.MyTeam.Add(skill, true);
            }
        }
    }

    public class Sex_exc_2 : Sex_ImproveClone, IP_DamageTakeChange, IP_Targeted
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&user",this.BChar.Info.Name);
        }
        public override void Init()
        {
            base.Init();
            this.CountingExtedned = true;
        }
        public void Targeted(Skill SkillD, List<BattleChar> Targets)
        {
            if (this.MySkill.IsNowCounting )
            {
                CastingSkill castskill=SF_Standard.Standard_Tool.GetCastSkill(this);
                if(castskill!=null)
                {
                    if(castskill.TargetReturn().Contains(SkillD.Master))
                    {
                        if (Targets.FindAll(a=>a.Info.Ally!=SkillD.Master.Info.Ally).Count>0)
                        {
                            Targets.RemoveAll(a => a.Info.Ally != SkillD.Master.Info.Ally);
                            Targets.Add(this.BChar);
                        }
                    }
                }
            }
        }
        public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {
            if (this.MySkill.IsNowCounting && Dmg > 0)
            {
                CastingSkill castskill = SF_Standard.Standard_Tool.GetCastSkill(this);
                if (castskill!=null && this.MySkill.MyButton.castskill.TargetReturn().Contains(User))
                {
                    return Math.Max(1, (int)(0.5 * Dmg));
                }
            }
            return Dmg;
        }
    }
    public class Bcl_exc_HardTaunt : B_Taunt, IP_Awake, IP_SkillUse_User
    {


        public override void Init()
        {
            base.Init();
            this.PlusStat.Weak = true;
        }
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

    public class Sex_exc_3 : Sex_ImproveClone, IP_SkillUse_Target, IP_DamageChange
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&dmg&", Math.Max((int)(this.BChar.GetStat.atk * 1.35 / 3), 1).ToString());
        }
        public override void Init()
        {
            base.Init();
            this.TargetBuff.Clear();
            BuffTag btg = new BuffTag();
            btg.BuffData = new GDEBuffData("B_SF_Executioner_dot_2");
            this.TargetBuff.Add(btg);
        }
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if (SP.SkillData == this.MySkill && DMG > 0)
            {
                Buff buff = this.MySkill.Master.BuffAdd("B_SF_Executioner_s", this.MySkill.Master);
                buff.BarrierHP += (int)(0.66 * DMG);
            }
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            List<BattleChar> list1 = new List<BattleChar>();
            list1.AddRange(BattleSystem.instance.EnemyTeam.AliveChars);
            list1.RemoveAll(a => Targets.Contains(a));

            Skill skill = SkillD.CloneSkill(true);
            skill.UseOtherParticle_Path = "Particle/Hein/S_Hein_0";
            foreach (Skill_Extended sex in skill.AllExtendeds)
            {
                if (sex is Sex_exc_3 sex3)
                {
                    sex3.plusattack = true;
                    sex3.TargetBuff.Clear();
                }
            }
            if (list1.Count > 0)
            {
                BattleSystem.instance.StartCoroutine(this.PlusAttack(skill, list1));
                //this.MySkill.Master.ParticleOut(skill, list1);
            }

        }
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if (SkillD == this.MySkill)
            {
                if (this.plusattack)
                {
                    return Math.Max((int)(Damage / 3), 1);
                }
            }
            return Damage;

        }
        public override void SkillKill(SkillParticle SP)
        {
            base.SkillKill(SP);
            if (!this.plusattack)
            {
                List<BattleChar> list1 = new List<BattleChar>();
                list1.AddRange(BattleSystem.instance.EnemyTeam.AliveChars);
                list1.RemoveAll(a => SP.ALLTARGET.Contains(a));

                foreach (BattleChar bc in list1)
                {
                    bc.BuffAdd("B_SF_Executioner_dot_2", this.MySkill.Master);
                }
            }
        }
        public IEnumerator PlusAttack(Skill skill, List<BattleChar> targets)
        {
            yield return new WaitForSeconds(0.8f);
            this.MySkill.Master.ParticleOut(skill, targets);
        }

        public bool plusattack = false;
    }
    public class Bcl_ecx_s : Buff
    {

    }
    public class Sex_exc_4 : Sex_ImproveClone, IP_DamageChange,IP_SpecialEnemyTargetSelect
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a&", ""/*"("+((int)(this.BChar.HP )).ToString()+")"*/);
        }
        public bool SpecialEnemyTargetSelect(Skill skill, BattleEnemy Target)
        {
            if(skill==this.MySkill && ((float)Target.HP/ (float)Target.GetStat.maxhp)<0.5f)
            {
                return true;
            }
            return false;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.frame++;
            if (this.frame > 20 || this.frame < 0)
            {
                this.frame = 0;
                if(!this.attacking)
                {
                    this.SkillBasePlus.Target_BaseDMG = Math.Max(0,this.BChar.HP);
                }
            }
        }
        public int frame = 0;
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            this.attacking = true;
            this.SkillBasePlus.Target_BaseDMG = 0;
            if (this.BChar.HP > 0)
            {
                //Buff buff = this.BChar.BuffAdd(GDEItemKeys.Buff_B_Momori_P_NoDead, this.BChar, false, 0, false, -1, false);
                Buff buff = this.BChar.BuffAdd("B_SF_Executioner_4_h", this.BChar, false, 0, false, -1, false);
                int dmg = this.BChar.HP;
                this.BChar.Damage(this.BChar, (int)(this.BChar.HP/*GetStat.maxhp*0.5*/), false, true, false, 0, false, false, false);
                BattleSystem.DelayInputAfter(this.Delay_1(buff));
                //buff.SelfDestroy(false);
                //Skill_Extended sex = this.MySkill.ExtendedAdd(new Skill_Extended());
                this.SkillBasePlus.Target_BaseDMG = dmg;
            }

        }

        public override void SkillKill(SkillParticle SP)
        {
            base.SkillKill(SP);
            if (!this.triggered)
            {
                //this.triggered = true;
                this.attacking = false;
                Skill skill = this.MySkill.CloneSkill(true, null, null, true);
                this.attacking = true;
                this.triggered = true;
                skill.isExcept = true;
                //skill.AutoDelete = 1;
                skill.BasicSkill = false;
                //skill.NotCount = false;
                //skill.APChange = -99;
                //this.SetNonQuick = true;
                Skill_Extended sex=skill.ExtendedAdd_Battle(new Skill_Extended());
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
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if (SkillD == this.MySkill && Damage > 0)
            {
                double per = 1f - ((double)Target.HP / (double)Target.GetStat.maxhp);
                return (int)((1 + per) * Damage);
            }
            return Damage;
        }
        public IEnumerator Delay_1(Buff buff)
        {
            buff.SelfDestroy();
            yield return null;
            yield break;
        }
        public bool attacking = false;
        public bool triggered = false;
    }

    public class Bcl_exc_4_h : Buff, IP_DeadResist
    {
        public bool DeadResist()
        {
            return true;
        }

    }


    public class Sex_exc_5 : Sex_ImproveClone
    {
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.frame++;
            if (this.frame > 20 || this.frame < 0)
            {

                this.frame = 0;
                if (!this.MySkill.IsNowCasting)
                {
                    bool flag = false;
                    if (BattleSystem.instance.EnemyTeam.AliveChars.Count > 0)
                    {
                        flag = true;
                        foreach (BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars)
                        {
                            if (bc.Buffs.FindAll(a => a.BuffData.Debuff && a.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT).Count <= 0)
                            {
                                flag = false;
                            }
                        }
                    }
                    if (flag)
                    {
                        this.APChange = -1;
                    }
                    else
                    {
                        this.APChange = 0;
                    }
                }
            }
        }
        public int frame = 0;
    }
    public class Sex_exc_6 : Sex_ImproveClone, IP_DamageChange_sumoperation
    {

        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (SkillD == this.MySkill)
            {
                foreach (Buff buff in Target.Buffs)
                {
                    if (!buff.IsHide && buff.BuffData.Debuff && buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT)
                    {
                        PlusDamage += buff.DotDMGView();
                    }
                }
            }
        }
    }
    public class Sex_exc_7 : Sex_ImproveClone, IP_SetNoCG, IP_SkillCastingStart, IP_ParticleOut_After_Global
    {
        public bool SetNoCG()
        {
            return true;
        }
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a&", ((int)(0.6 * this.BChar.GetStat.atk)).ToString());
        }
        public override void Init()
        {
            base.Init();
            this.CountingExtedned = true;
            this.OnePassive = true;
        }
        public void SkillCasting(CastingSkill ThisSkill)
        {
            BattleSystem.instance.StartCoroutine(this.Delay());
        }
        public IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.1f);
            this.BChar.BuffAdd("B_SF_Executioner_7", this.BChar, false, 0, false, -1, true);
            yield break;
        }

        public IEnumerator ParticleOut_After_Global(Skill SkillD, List<BattleChar> Targets)
        {
            if (this.MySkill.IsNowCasting && SkillD.IsDamage && !SkillD.Master.Info.Ally && Targets.Contains(this.BChar))
            {

                this.SkillBasePlus.Target_BaseDMG += (int)(0.6 * this.BChar.GetStat.atk);
                if (this.MySkill.MyButton != null)
                {
                    this.MySkill.MyButton.CountingLeft += 1;
                }
            }
            yield break;
        }
    }
    public class Bcl_exc_7 : Buff
    {
        public override void BuffStat()
        {
            base.BuffStat();
            this.PlusStat.dod = this.CheckMainSkill() * 25;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.frame++;
            if (this.frame > 20 || this.frame < 0)
            {
                this.frame = 0;
                int num = this.CheckMainSkill();
                this.PlusStat.dod = num * 25;
                if (num <= 0)
                {
                    this.SelfDestroy();
                }
            }
        }
        public int frame = 0;
        public int CheckMainSkill()
        {
            int i = 0;
            if (BattleSystem.instance == null)
            {
                return 0;
            }
            foreach (CastingSkill castingSkill in this.BChar.BattleInfo.CastSkills)
            {
                if (castingSkill.skill.MySkill.KeyID == "S_SF_Executioner_7" && castingSkill.skill.Master == this.BChar)
                {
                    i++;
                }
            }
            foreach (CastingSkill castingSkill in this.BChar.BattleInfo.SaveSkill)
            {
                if (castingSkill.skill.MySkill.KeyID == "S_SF_Executioner_7" && castingSkill.skill.Master == this.BChar)
                {
                    i++;
                }
            }
            return i;
        }
    }
    public class Sex_exc_8 : Sex_ImproveClone, IP_SetNoCG, IP_EnemySkillUse
    {
        public bool SetNoCG()
        {
            return true;
        }
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&num1", ((int)(0.3 * this.BChar.GetStat.atk)).ToString());
        }
        public override void Init()
        {
            base.Init();
            this.OnePassive = true;
            this.CountingExtedned = true;
        }
        public void EnemySkillUse(BattleEnemy user, Skill skill, List<BattleChar> targets)
        {
            //Debug.Log("EnemySkillUse_point_1");
            if (this.MySkill.IsNowCounting && this.MySkill.MyButton != null && this.MySkill.MyButton.castskill != null && this.MySkill.MyButton.castskill.TargetReturn().Contains(user))
            {
                //Debug.Log("EnemySkillUse_point_2");
                Skill skill_1 = Skill.TempSkill("S_SF_Executioner_8_1", this.BChar);
                skill_1.PlusHit = true;
                skill_1.FreeUse = true;
                this.BChar.ParticleOut(skill_1, user);
            }
        }
    }

    public class Sex_exc_9 : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            int num = 2;
            foreach (BattleChar hit in Targets)
            {
                if (hit is BattleEnemy)
                {
                    BattleEnemy be = hit as BattleEnemy;
                    foreach (CastingSkill cast in be.SkillQueue)
                    {
                        num = Math.Min(cast.CastSpeed, num);
                    }
                }
            }
            if (num <= 1)
            {
                BuffTag btg1 = new BuffTag();
                btg1.BuffData = new GDEBuffData("B_SF_Executioner_9");
                BuffTag btg2 = new BuffTag();
                btg2.BuffData = new GDEBuffData("B_SF_Executioner_dot_1");
                Skill_Extended sex = this.MySkill.ExtendedAdd(new Skill_Extended());
                sex.TargetBuff.Add(btg1);
                sex.TargetBuff.Add(btg2);
            }
            if (num <= 0)
            {
                Skill_Extended sex = this.MySkill.ExtendedAdd(new Skill_Extended());
                sex.PlusSkillStat.HIT_CC = 30;
                sex.PlusSkillStat.HIT_DEBUFF = 30;
                sex.PlusSkillStat.HIT_DOT = 30;
            }
        }
    }
    public class Bcl_exc_9 : Buff, IP_SkillUse_User_After
    {


        public override void Init()
        {
            base.Init();
            this.PlusStat.Weak = true;
            this.PlusPerStat.Damage = -30;
        }
        // Token: 0x06000B04 RID: 2820 RVA: 0x0007D701 File Offset: 0x0007B901
        public void SkillUseAfter(Skill SkillD)
        {
            BattleSystem.DelayInput(this.SkillUseAfter());
        }
        public IEnumerator SkillUseAfter()
        {
            base.SelfStackDestroy();
            yield return null;
            yield break;
        }
    }

    public class Sex_exc_10 : Sex_ImproveClone
    {
        /*
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            
            if (!(Targets[0] is BattleAlly))
            {
                return;
            }
            BattleChar target = Targets[0];
            bool flag = false;
            if(BattleSystem.instance!=null)
            {
                if ( BattleSystem.instance.AllyTeam.Skills.Find(a => a.Master == target) ==null)
                {
                    flag = true;
                }
            }

            List<Skill> list = new List<Skill>();
            List<GDESkillData> list2 = new List<GDESkillData>();
            foreach (GDESkillData gdeskillData in PlayData.ALLSKILLLIST)
            {
                if (gdeskillData.User == target.Info.KeyData && gdeskillData.KeyID!=this.MySkill.MySkill.KeyID)
                {
                    if(!gdeskillData.Disposable)
                    {
                        list2.Add(gdeskillData);
                        list2.Add(gdeskillData);
                        list2.Add(gdeskillData);
                    }
                    else
                    {
                        list2.Add(gdeskillData);
                    }
                }
            }
            for (int i = 0; i < 3; i++)
            {
                GDESkillData gdeskillData2 = list2.Random(this.BChar.GetRandomClass().Main);
                list2.RemoveAll(a=>a.KeyID == gdeskillData2.KeyID);
                list.Add(Skill.TempSkill(gdeskillData2.KeyID, target, BattleSystem.instance.AllyTeam).CloneSkill(false, null, null, false));
                list[i].isExcept = true;
                if(flag)
                {
                    Extended_Lucy_0_1 extended = new Extended_Lucy_0_1();
                    list[i].ExtendedAdd_Battle(extended);
                }

            }
            BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(this.Del), ScriptLocalization.System_SkillSelect.CreateSkill, false, true, true, false, true));
        }

        // Token: 0x06000D7A RID: 3450 RVA: 0x00085C24 File Offset: 0x00083E24
        public void Del(SkillButton Mybutton)
        {
            BattleSystem.instance.AllyTeam.Add(Mybutton.Myskill, true);
        }
        */
    }
    public class Bcl_exc_10 : Buff,IP_Hit
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&num1", ((int)(0.4 * this.BChar.GetStat.atk)).ToString());
        }
    
        public override void Init()
        {
            base.Init();
            this.PlusStat.AggroPer = 66;
            this.PlusStat.Strength = true;
        }
        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            if(SP.UseStatus.Info.Ally!=this.BChar.Info.Ally && Dmg>0)
            {
                Skill skill_1 = Skill.TempSkill("S_SF_Executioner_10_1", this.BChar);
                skill_1.PlusHit = true;
                skill_1.FreeUse = true;
                this.BChar.ParticleOut(skill_1, SP.UseStatus);
                this.SelfStackDestroy();
            }
        }
    }

    public class Sex_exc_11 : Sex_ImproveClone
    {
        // Token: 0x06000018 RID: 24 RVA: 0x000027C8 File Offset: 0x000009C8
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            //Debug.Log("测试点0");
            List<BattleChar> tars = new List<BattleChar>();
            tars.AddRange(Targets);
            foreach (BattleChar bc in tars)
            {
                if(bc is BattleEnemy be)
                {
                    foreach(BattleEnemy be2 in BattleSystem.instance.EnemyList)
                    {
                        if(be2.istaunt==be.istaunt)
                        {
                            if(!Targets.Contains(be2))
                            {
                                Targets.Add(be2);
                            }
                        }
                    }
                }
            }
            //Debug.Log("测试点1");
            Skill skill = Skill.TempSkill("S_SF_Executioner_11_1", this.BChar);//this.MySkill.CloneSkill(true, null, null, true);
            //Debug.Log("测试点2");
            //skill.isExcept = true;
            skill.Counting = 2;
                skill.FreeUse = true;
                skill.PlusHit = true;
            //this.SetNonQuick = true;
            //Debug.Log("测试点3");
            for (int i = 0; i < skill.AllExtendeds.Count; i++)
                {
                    if (!skill.AllExtendeds[i].BattleExtended && !skill.AllExtendeds[i].isDataExtended)
                    {
                        skill.AllExtendeds[i].SelfDestroy();
                    }
                }
            //Debug.Log("测试点4");
            //BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, Targets[0] , false, false, true, null));
            BattleTeam.SkillRandomUse(this.BChar, skill, false, true, false);
            //Debug.Log("测试点5");


        }
    }



    public class Sex_exc_12 : Sex_ImproveClone
    {
        public override void BattleStartDeck(List<Skill> Skills_Deck)
        {
            this.BChar.MyTeam.Draw(this.MySkill, null);
        }
    }
    public interface IP_CheckB12
    {
        // Token: 0x060025CE RID: 9678
        int CheckB12();
    }
    public class Bcl_exc_12 : Buff,IP_BuffAdd, IP_CheckB12
    {
        public static int StaticCheckB12()
        {
            if(BattleSystem.instance == null)
            {
                return 0;
            }
            
            int num1 = 0;
            foreach (IP_CheckB12 ip_patch in BattleSystem.instance.IReturn<IP_CheckB12>())
            {
                //Debug.Log("only damage drm");
                bool flag = ip_patch != null;
                if (flag)
                {
                    num1 = num1 + ip_patch.CheckB12();
                }
            }
            return num1;
        }
        public int CheckB12()
        {
            return 1;
        }
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {
            if(BuffUser==this.BChar)
            {
                if(this.lastKey== addedbuff.BuffData.Key && Time.frameCount==this.frame)
                {
                    return;
                }
                this.frame=Time.frameCount;
                this.lastKey=addedbuff.BuffData.Key;
                    
                if (addedbuff.BuffData.Key == "B_SF_Executioner_dot_1")
                {
                    if(BuffTaker.BuffFind("B_SF_Executioner_dot_2",false))
                    {
                        Buff buff = BuffTaker.BuffReturn("B_SF_Executioner_dot_2", false);
                        foreach(StackBuff sta in buff.StackInfo)
                        {
                            if(sta.RemainTime > 0)
                            {
                                sta.RemainTime++;
                            }
                        }
                    }
                }
                if (addedbuff.BuffData.Key == "B_SF_Executioner_dot_2")
                {
                    if (BuffTaker.BuffFind("B_SF_Executioner_dot_1", false))
                    {
                        Buff buff = BuffTaker.BuffReturn("B_SF_Executioner_dot_1", false);
                        foreach (StackBuff sta in buff.StackInfo)
                        {
                            if (sta.RemainTime > 0)
                            {
                                sta.RemainTime++;
                            }
                        }
                    }
                }
            }
        }

        public int frame = 0;
        public string lastKey = string.Empty;
    }


    public class Sex_exc_13 : Sex_ImproveClone
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
            list_skill.AddRange(BattleSystem.instance.AllyTeam.Skills_Deck.FindAll(a => a.IsDamage));


            if (list_skill.Count <= 0)
            {
                BattleSystem.instance.AllyTeam.Draw();
            }
            else
            {


                list_skill[0].ExtendedAdd("EX_SF_Executioner_13");
                yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyTeam._ForceDraw(list_skill[0], null));
            }
            yield return null;
            yield break;
        }
    }


    public class EX_exc_13 : Sex_ImproveClone
    {
        public override void SkillKill(SkillParticle SP)
        {
            base.SkillKill(SP);
            if(SP.SkillData==this.MySkill)
            {
                BattleSystem.instance.AllyTeam.Draw();
            }
        }
    }

    public class SkillEn_SF_Executioner_0 : Sex_ImproveClone,IP_CriPerChange, IP_ChangeDamageState,IP_SkillUse_Target//, IP_SkillUse_User_After
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.IsDamage;
        }

        public void CriPerChange(Skill skill, BattleChar Target, ref float CriPer)
        {
            //this.target = Targets[0];
            if(Target!=null && !Target.IsDead && skill.AllExtendeds.Contains(this))
            {
                if(Target.BuffFind("B_SF_Executioner_dot_1",false))
                {
                    Buff buff = Target.BuffReturn("B_SF_Executioner_dot_1", false);
                    CriPer += (15 * buff.StackNum);
                }
            }
        }

        public void ChangeDamageState(SkillParticle SP, BattleChar Target, int DMG, bool Cri, ref bool ToHeal, ref bool ToPain)
        {
            if(SP.SkillData.AllExtendeds.Contains(this))
            {
                if (Target.BuffFind("B_SF_Executioner_dot_2", false))
                {
                    Buff buff = Target.BuffReturn("B_SF_Executioner_dot_2", false);
                    int crinum = 15 * buff.StackNum;
                    Buff buff2 = this.BChar.BuffAdd("B_SF_Executioner_En_0", this.BChar, false, 0, false, -1, false);
                    buff.PlusStat.PlusCriDmg = crinum;
                    this.BChar.Info.ForceGetStat();
                    Stat stat=this.BChar.GetStat;
                }
            }
          
        }
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if(SP.SkillData.AllExtendeds.Contains(this))
            {
                if (hit.BuffFind("B_SF_Executioner_En_0", false))
                {
                    
                    Buff buff=hit.BuffReturn("B_SF_Executioner_dot_2", false);
                    buff.PlusStat.PlusCriDmg = 0;
                    this.BChar.Info.ForceGetStat();
                    Stat stat = this.BChar.GetStat;
                    //buff.SelfDestroy();
                }
            }
        }


    }
    public class Bcl_exc_en_0 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.frame = 0;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(this.frame!=0 && !this.DestroyBuff)
            {
                this.PlusStat.PlusCriDmg = 0;
                this.BChar.Info.ForceGetStat();
                Stat stat = this.BChar.GetStat;
            }
            this.frame++;

            if(!BattleSystem.instance.DelayWait && BattleSystem.instance.Particles.Count == 0)
            {
                this.SelfDestroy();
            }
        }
        public int frame = 0;
    }
    public class SkillEn_SF_Executioner_1 : Sex_ImproveClone
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.IsDamage;
        }
        public override void Init()
        {
            base.Init();
            this.triggered = false;
        }
        public override void SkillKill(SkillParticle SP)
        {
            base.SkillKill(SP);
            if(SP.SkillData.AllExtendeds.Contains(this))
            {
                if(!this.triggered)
                {
                    this.triggered=true;
                    if(this.MySkill.UsedApNum>0)
                    {
                        BattleSystem.instance.AllyTeam.AP += this.MySkill.UsedApNum;

                    }
                }
            }
        }
        public bool triggered = false;
    }






    public interface IP_CheckEquip
    {
        // Token: 0x060025CE RID: 9678
        bool CheckEquip(BattleChar bc);
    }
    public class E_SF_Executioner_0 : EquipBase,IP_CriPerChange, IP_CheckEquip
    {
        public override void Init()
        {
            base.Init();
            this.PlusPerStat.Damage = 10;
            this.PlusPerStat.MaxHP = 20;
            this.PlusStat.cri = 10;
            this.PlusStat.HIT_DOT = 20;
        }
        public bool CheckEquip(BattleChar bc)
        {
            if (bc == this.BChar)
            {
                return true;
            }
            return false;
        }
        public void CriPerChange(Skill skill, BattleChar Target, ref float CriPer)
        {
            if(Target!=null && skill.Master==this.BChar)
            {
                int num = 0;
                num = (int)((Target.GetStat.maxhp-Target.HP)*50f / Target.GetStat.maxhp);
                if (num > 0)
                {
                    CriPer += num;
                }
            }
        }



    }




}
