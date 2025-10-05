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
using SF_Standard;
using System.Xml.Serialization;


namespace SF_Judge
{
    [PluginConfig("M200_SF_Judge_CharMod", "SF_Judge_CharMod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class SF_Judge_CharacterModPlugin : ChronoArkPlugin
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

        public class JudgeDef : ModDefinition
        {
        }


        /*
        public class S_SF_Judge_7_Particle : CustomSkillGDE<SF_Judge_CharacterModPlugin.JudgeDef>
        {
            // Token: 0x060000AB RID: 171 RVA: 0x000055A0 File Offset: 0x000037A0
            public override ModGDEInfo.LoadingType GetLoadingType()
            {
                return ModGDEInfo.LoadingType.Replace;
            }

            // Token: 0x060000AC RID: 172 RVA: 0x000055B4 File Offset: 0x000037B4
            public override string Key()
            {
                return "S_SF_Judge_7_no";
            }

            // Token: 0x060000AD RID: 173 RVA: 0x000055CB File Offset: 0x000037CB
            public override void SetValue()
            {
                base.Particle = base.assetInfo.CodeConstuctAsync<GameObject>("Particle/Enemy/Golem_2", new ModAssetInfo.ObjectChangeDelegate<GameObject>(this.ParticleChangeDelegate));//"Particle/Twins/Hel_2"
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


                //this.ParticleColorChange(particle.transform.GetChild(0), new Color(0.43137f, 0f, 0.43137f, 0.241f));
                //this.ParticleColorChange(particle.transform.GetChild(1), new Color(0.43137f, 0f, 0.43137f, 0.4f));
                this.ParticleColorChange(particle.transform, new Color(1f, 0f, 1f, 1f));
                this.ParticleColorChange(particle.transform.GetChild(0), new Color(1f, 0f, 1f, 0f));
                this.ParticleColorChange(particle.transform.GetChild(1), new Color(1f, 0f, 1f, 1f));
                this.ParticleColorChange(particle.transform.GetChild(2), new Color(1f, 0f, 1f, 1f));
                this.ParticleColorChange(particle.transform.GetChild(3), new Color(1f, 0f, 1f, 0.8f));



                return particle;
            }

        }
        */
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
            //MasterAudio.PlaySound("A_jud_4", 1f, null, 0f, null, null, false, false);
            float volume = 0f;
            float volume2 = 0f;
            if (ModManager.getModInfo("SF_Judge").GetSetting<ToggleSetting>("AudioOn").Value)
            {
                volume = ModManager.getModInfo("SF_Judge").GetSetting<SliderSetting>("AudioVolume").Value;
            }
            else
            {
                volume = 0f;
            }

            if (inText.Contains("T_jud_1_1"))
            {
                inText = inText.Replace("T_jud_1_1:", string.Empty);
                MasterAudio.PlaySound("A_jud_1_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_jud_2_1"))
            {
                inText = inText.Replace("T_jud_2_1:", string.Empty);
                MasterAudio.PlaySound("A_jud_2_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_jud_2_2"))
            {
                inText = inText.Replace("T_jud_2_2:", string.Empty);
                MasterAudio.PlaySound("A_jud_2_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_jud_3_1"))
            {
                inText = inText.Replace("T_jud_3_1:", string.Empty);
                MasterAudio.PlaySound("A_jud_3_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_jud_4_1"))
            {
                inText = inText.Replace("T_jud_4_1:", string.Empty);
                MasterAudio.PlaySound("A_jud_4_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_jud_4_2"))
            {
                inText = inText.Replace("T_jud_4_2:", string.Empty);
                MasterAudio.PlaySound("A_jud_4_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_jud_5_1"))
            {
                inText = inText.Replace("T_jud_5_1:", string.Empty);
                MasterAudio.PlaySound("A_jud_5_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_jud_6_1"))
            {
                inText = inText.Replace("T_jud_6_1:", string.Empty);
                MasterAudio.PlaySound("A_jud_6_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_jud_6_2"))
            {
                inText = inText.Replace("T_jud_6_2:", string.Empty);
                MasterAudio.PlaySound("A_jud_6_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_jud_7_1"))
            {
                inText = inText.Replace("T_jud_7_1:", string.Empty);
                MasterAudio.PlaySound("A_jud_7_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_jud_7_2"))
            {
                inText = inText.Replace("T_jud_7_2:", string.Empty);
                MasterAudio.PlaySound("A_jud_7_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_jud_8_1"))
            {
                inText = inText.Replace("T_jud_8_1:", string.Empty);
                MasterAudio.PlaySound("A_jud_8_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_jud_9_1"))
            {
                inText = inText.Replace("T_jud_9_1:", string.Empty);
                MasterAudio.PlaySound("A_jud_9_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_jud_10_1"))
            {
                inText = inText.Replace("T_jud_10_1:", string.Empty);
                MasterAudio.PlaySound("A_jud_10_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_jud_11_1"))
            {
                inText = inText.Replace("T_jud_11_1:", string.Empty);
                MasterAudio.PlaySound("A_jud_11_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_jud_11_2"))
            {
                inText = inText.Replace("T_jud_11_2:", string.Empty);
                MasterAudio.PlaySound("A_jud_11_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_jud_11_3"))
            {
                inText = inText.Replace("T_jud_11_3:", string.Empty);
                MasterAudio.PlaySound("A_jud_11_3", volume, null, 0f, null, null, false, false);
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

            if (CharId == "SF_Judge")
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
    /*
    [HarmonyPatch(typeof(SkillParticle))]
    [HarmonyPatch("Awake")]
    public static class SkillParticleAwakePlugin
    {
        [HarmonyPostfix]
        public static void SkillParticleAwakePatch()
        {

            //Debug.Log("编制保护检查点1");


            //Debug.Log("编制保护检查点2");
        }

    }
    */
    /*
    [HarmonyPatch(typeof(Buff))]
    [HarmonyPatch("DataToBuff")]
    public static class DataToBuffPlugin
    {
        [HarmonyPrefix]
        public static void BuffStatUpdate_Before_Patch()
        {
            foreach (IP_BuffStatUpdate_Before ip_patch in BattleSystem.instance.IReturn<IP_BuffStatUpdate_Before>())
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    ip_patch.BuffStatUpdate_Before();
                }
            }
        }


    }
    public interface IP_BuffStatUpdate_Before
    {
        // Token: 0x06000001 RID: 1
        void BuffStatUpdate_Before();
    }
    */


    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("Damage")]
    public static class DamagePlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void BattleChar_Damage_Before_patch(ref BattleChar __instance, BattleChar User, ref int Dmg, bool Cri, bool Pain, ref bool NOEFFECT, int PlusPenetration, bool IgnoreHealingPro, bool HealingPro, bool OnlyUnscaleTime)
        {
            if (__instance.Info.Ally)
            {
                foreach (IP_Damage_Before ip_before in BattleSystem.instance.IReturn<IP_Damage_Before>())
                {
                    bool flag = ip_before != null;
                    if (flag)
                    {
                        ip_before.Damage_Before(ref __instance, User, ref Dmg, Cri, Pain, ref NOEFFECT, PlusPenetration, IgnoreHealingPro, HealingPro, OnlyUnscaleTime);
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
        void Damage_Before(ref BattleChar battleChar, BattleChar User, ref int Dmg, bool Cri, bool Pain, ref bool NOEFFECT, int PlusPenetration, bool IgnoreHealingPro, bool HealingPro, bool OnlyUnscaleTime);
    }



    /*
    [HarmonyPatch(typeof(PartyBarrier))]
    [HarmonyPatch("TakeDamage")]
    public static class TakeDamagePlugin
    {
        [HarmonyPrefix]
        public static void TakeDamage_BeforePatch( ref int Damage)
        {

            //Debug.Log("编制保护检查点1");
            foreach (IP_TakeDamage_Before ip_patch in BattleSystem.instance.IReturn<IP_TakeDamage_Before>())
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    ip_patch.TakeDamage_Before( ref Damage);
                }
            }

            //Debug.Log("编制保护检查点2");
        }

    }
    public interface IP_TakeDamage_Before
    {
        // Token: 0x06000001 RID: 1
        void TakeDamage_Before( ref int Damage);
    }
    */
    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("BarriarDamage")]
    public static class BarriarDamagePlugin
    {
        [HarmonyPrefix]
        public static void BarriarDamage_BeforePatch(BattleChar __instance, ref int Dmg)//, ref int __result)
        {

            //Debug.Log("编制保护检查点1");
            foreach (IP_BarriarDamage_Before ip_patch in BattleSystem.instance.IReturn<IP_BarriarDamage_Before>())
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    ip_patch.BarriarDamage_Before(__instance, ref Dmg);//,ref __result);
                }
            }

            //Debug.Log("编制保护检查点2");
        }
        [HarmonyPostfix]
        public static void BarriarDamage_AfterPatch(BattleChar __instance, int Dmg)//, ref int __result)
        {

            //Debug.Log("编制保护检查点1");
            foreach (IP_BarriarDamage_After ip_patch in BattleSystem.instance.IReturn<IP_BarriarDamage_After>())
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    ip_patch.BarriarDamage_After(__instance, Dmg);//,ref __result);
                }
            }

            //Debug.Log("编制保护检查点2");
        }

    }
    public interface IP_BarriarDamage_Before
    {
        // Token: 0x06000001 RID: 1
        void BarriarDamage_Before(BattleChar BC, ref int Dmg);//, ref int __result);
    }
    public interface IP_BarriarDamage_After
    {
        // Token: 0x06000001 RID: 1
        void BarriarDamage_After(BattleChar BC, int Dmg);//, ref int __result);
    }






    [HarmonyPatch(typeof(Buff))]
    [HarmonyPatch("set_PartyBarrierHP")]
    public static class set_PartyBarrierHPPlugin
    {
        [HarmonyPrefix]
        public static void set_PartyBarrierHP(Buff __instance, ref int value)
        {
            foreach (IP_Set_PartyBarrierHP ip_patch in __instance.BChar.IReturn<IP_Set_PartyBarrierHP>())
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    ip_patch.Set_PartyBarrierHP(__instance, ref value);
                }
            }
        }

    }
    public interface IP_Set_PartyBarrierHP
    {
        // Token: 0x06000001 RID: 1
        void Set_PartyBarrierHP(Buff buff, ref int num);
    }





    /*
    [HarmonyPatch(typeof(Buff))]
    [HarmonyPatch("get_PartyBarrierHP")]
    //[HarmonyPatch(new Type[] { typeof(int) })]
    
    public static class BuffPartyBarrierHPPlugin2
    {
        [HarmonyPostfix]
        public static void Get_PartyBarrierHP_Patch(Buff __instance,ref int __result)
        {

            //Debug.Log("Get_PartyBarrierHP:"+__instance.BuffData.Name+":"+__result);
            //Debug.Log("Get_PartyBarrierHP:" + __instance.BuffData.Name + ":" + (int)(Traverse.Create(__instance).Field("_PartyBarrrierHP").GetValue()));
            foreach (IP_Get_PartyBarrierHP ip_patch in __instance.BChar.IReturn<IP_Get_PartyBarrierHP>())
            {
                
                bool flag = ip_patch != null;
                if (flag)
                {
                    ip_patch.Get_PartyBarrierHP(__instance,ref __result);
                }
            }
        }

    }

    public interface IP_Get_PartyBarrierHP
    {
        // Token: 0x06000001 RID: 1
        void Get_PartyBarrierHP(Buff buff,ref int result);
    }
    */
    /*
    [HarmonyPatch(typeof(SkillParticle))]
    [HarmonyPatch("init")]
    public static class SkillParticle_init_Plugin
    {
        [HarmonyPostfix]
        public static void SkillParticle_init_BeforePatch(SkillParticle __instance,Skill skill, BattleChar User, List<BattleChar> Target)//, ref int __result)
        {
            
            if(skill.MySkill.KeyID=="S_SF_Judge_4")
            {
                __instance.AllHit = false;
                __instance.AllHitForce = false;
            }
            
        }
            
    }
    */

    [HarmonyPatch(typeof(CharEquipInven))]
    [HarmonyPatch("OnDropSlot")]
    public static class CharEquipInvenPlugin
    {
        // Token: 0x06000015 RID: 21 RVA: 0x00002410 File Offset: 0x00000610
        [HarmonyPostfix]
        public static void CharEquipInven_OnDropSlot_patch(ItemBase inputitem, ref bool __result, CharEquipInven __instance)
        {
            bool flag = inputitem.itemkey == "E_SF_Judge_0" && __instance.Info.GetData.Key != "SF_Judge";
            if (flag)
            {
                __result = true;
            }
            //Debug.Log("成功进入OnDropSlot的patch");
        }
    }

    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("Heal")]
    [HarmonyPatch(new Type[] { typeof(BattleChar), typeof(float), typeof(bool), typeof(bool), typeof(BattleChar.ChineHeal) })]
    public static class HealPlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void BattleChar_Heal_Before_patch(BattleChar __instance, BattleChar User, float Hel, bool Cri, bool Force = false, BattleChar.ChineHeal heal = null)//, BattleChar User, float Hel, bool Cri, bool Force)
        {

            foreach (IP_Heal_Before ip_before in BattleSystem.instance.IReturn<IP_Heal_Before>())
            {
                bool flag = ip_before != null;
                if (flag)
                {
                    ip_before.Heal_Before(__instance, User, Hel, Cri, Force);
                }
            }


        }
    }

    public interface IP_Heal_Before
    {
        // Token: 0x06000001 RID: 1
        void Heal_Before(BattleChar HealTarget, BattleChar User, float Hel, bool Cri, bool Force = false);
    }




    public class P_Judge : Passive_Char, IP_LevelUp, IP_DamageCriCheck, IP_BarriarDamage_Before//, IP_TakeDamage_Before
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
                list.Add(ItemBase.GetItem("E_SF_Judge_0", 1));
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

        public override void Init()
        {
            base.Init();
            this.OnePassive = true;
        }

        public override void FixedUpdate()
        {
            if (this.PlusStat.AggroPer != 7 * this.BChar.BarrierHP)
            {
                this.PlusStat.AggroPer = 7 * this.BChar.BarrierHP;
                //this.Init();
            }
        }

        public void DamageCriCheck(BattleChar Hit, BattleChar User, int Dmg, ref bool Cri, bool Pain, bool NOEFFECT = false)
        {
            if (Dmg > 0 && !Pain && Hit.Info.Ally && this.BChar.MyTeam.partybarrier.BarrierHP > 0)
            {
                Hit.BuffAdd("B_SF_Judge_p", this.BChar, false, 0, false, -1, false);
                BattleSystem.DelayInputAfter(this.WeakEnd(Hit));
            }
            else if (Dmg > 0 && !Pain && Hit == this.BChar && this.BChar.BarrierHP > 0)
            {
                Hit.BuffAdd("B_SF_Judge_p", this.BChar, false, 0, false, -1, false);
                BattleSystem.DelayInputAfter(this.WeakEnd(Hit));
            }
        }
        public IEnumerator WeakEnd(BattleChar bc)
        {
            foreach (Buff buff in bc.Buffs)
            {
                if (buff is Bcl_jud_p bcl)
                {
                    buff.SelfDestroy();
                }
            }
            yield return null;
            yield break;
        }
        /*
        public void Damage_Before(ref BattleChar battleChar, BattleChar User, ref int Dmg, bool Cri, bool Pain, ref bool NOEFFECT, int PlusPenetration, bool IgnoreHealingPro, bool HealingPro, bool OnlyUnscaleTime)
        {
            if(Dmg > 0 && !Pain && this.BChar.MyTeam.partybarrier.BarrierHP > 0 )
            {
                Dmg = (int)(0.75 * Dmg);
                Dmg=Math.Max(1, Dmg);
            }
            else if(Dmg > 0 && !Pain && battleChar == this.BChar && this.BChar.BarrierHP>0)
            {
                Dmg = (int)(0.75 * Dmg);
                Dmg = Math.Max(1, Dmg);
            }
        }
        */
        /*
        public void TakeDamage_Before( ref int Damage)
        {
            int frame = Time.frameCount;

            if (this.BChar.MyTeam.partybarrier.BarrierHP > 0 || frame < this.timeFrame1 + 2)
            {
                //Damage = (int)(0.75 * Damage);
                if (Damage > this.BChar.MyTeam.partybarrier.BarrierHP)
                {
                    Damage = Math.Min(Damage, this.BChar.MyTeam.partybarrier.BarrierHP);
                }
                this.timeFrame1 = frame;

            }

        }
        */
        private int timeFrame1;
        public void BarriarDamage_Before(BattleChar BC, ref int Dmg)//, ref int __result)
        {
            int frame = Time.frameCount;
            if (BC.Info.Ally&&(frame < this.timeFrame1 + 2 || this.BChar.MyTeam.partybarrier.BarrierHP > 0))
            {
                if (Dmg+ BattleSystem.instance.AllyTeam.partybarrier.SaveDamage > this.BChar.MyTeam.partybarrier.BarrierHP)
                {
                    Dmg = Math.Min(Dmg, this.BChar.MyTeam.partybarrier.BarrierHP- BattleSystem.instance.AllyTeam.partybarrier.SaveDamage);
                }
                this.timeFrame1 = frame;
            }
            else if (BC == this.BChar && (BC.BarrierHP > 0 || frame < this.timeFrame2 + 2) && this.BChar.MyTeam.partybarrier.BarrierHP <= 0)
            {
                //Dmg = (int)(0.75 * Dmg);
                if (Dmg > BC.BarrierHP)
                {
                    Dmg = Math.Min(Dmg, BC.BarrierHP);
                }
                this.timeFrame2 = frame;

            }

            //this.timeFrame2 = frame;
        }
        private int timeFrame2;

    }
    public class Bcl_jud_p : Buff, IP_DamageTakeChange
    {
        public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {
            int num = Dmg;
            if (Dmg > 0)
            {
                num = (int)(0.8 * Dmg);
                num = Math.Max(1, num);

            }
            this.SelfDestroy();
            return num;
        }
    }

    public class Bcl_jud_p_2 : Buff
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", this.PlusStat.AggroPer.ToString());


        }
        public override void Init()
        {
            base.Init();
        }
        public override void FixedUpdate()
        {
            if (this.PlusStat.AggroPer != 1 * this.BChar.BarrierHP)
            {
                this.PlusStat.AggroPer = 1 * this.BChar.BarrierHP;
                //this.Init();
            }
        }
    }
    public interface IP_JudgeShieldInit
    {
        // Token: 0x06000001 RID: 1
        void JudgeShieldInit();
    }
    public class Bcl_jud_S : Buff//, IP_BuffUpdate
    {
        public override void Init()
        {
            //Debug.Log("Init");

            base.Init();
            /*
            foreach (IP_JudgeShieldInit ip_patch in BattleSystem.instance.IReturn<IP_JudgeShieldInit>())
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    ip_patch.JudgeShieldInit();
                }
            }
            */
            //this.BuffStatUpdate();
            //this.PlusStat.DMGTaken = -25;
        }

        public override void FixedUpdate()
        {
            if (this.BarrierHP <= 0 && this.pflag1)
            {
                this.SelfDestroy();

            }
            else if (this.BarrierHP <= 0 && !this.pflag1)
            {
                this.pflag1 = true;
            }
        }



        private bool pflag1 = false;
    }

    public class SkillExtended_JudgeShield : Sex_ImproveClone//, IP_BuffUpdate
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
        /*
        public void BuffUpdate(Buff MyBuff)
        {
            //Debug.Log("BuffUpdata");
            this.AddShieldPersent();
        }
        */

        public override void Init()
        {
            GDESkillKeywordData k1 = new GDESkillKeywordData("KeyWord_SF_JudgeShield_des");

            base.Init();
            BuffTag buffTag = new BuffTag();
            buffTag.BuffData = new GDEBuffData("B_SF_Judge_S").ShallowClone();
            buffTag.User = this.BChar;
            buffTag.BuffData.Description = k1.Desc;
            //this.TargetBuff.Add(buffTag);
            BuffTag buffTag2 = new BuffTag();
            buffTag2.BuffData = new GDEBuffData("B_SF_Judge_S").ShallowClone();
            buffTag2.User = this.BChar;
            buffTag2.BuffData.Description = k1.Desc;

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
            //if (!Target.BuffFind("B_SF_Judge_S", false))
            //{
            Buff buff = Target.BuffAdd("B_SF_Judge_S", this.BChar, false, 0, false, -1, false);
            buff.BarrierHP += num;
            //}
            //else
            //{
            //Buff buff = Target.BuffReturn("B_SF_Judge_S",  false);
            //buff.BarrierHP += num;
            //}
            /*
            foreach (Buff buff2 in Target.Buffs)
            {
                if ( buff2.BuffData.Key == "B_SF_Judge_S")
                {
                    buff2.BarrierHP += num;
                    break;
                }
            }
            */
        }
        private bool Tar;
        private int shieldTar;
        private bool Self;
        private int shieldSelf;
        public BuffTag MainBuffTar;
        public BuffTag MainBuffSelf;
    }


    public class Sex_jud_1 : SkillExtended_JudgeShield//, IP_BattleStart_Ones
    {
        /*
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (this.BChar.BarrierHP <= 0 && this.MySkill.BasicSkill && (this.BChar as BattleAlly).MyBasicSkill.InActive)
            {
                this.Recharge();
            }
        }
        public void Recharge()
        {


            (this.BChar as BattleAlly).MyBasicSkill.CoolDownNum = 0;

            if ((this.BChar as BattleAlly).MyBasicSkill.buttonData == null || (this.BChar as BattleAlly).MyBasicSkill.ThisSkillUse)
            {

                Skill coskill = this.MySkill.CloneSkill(false, null, null, false);

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

        public void BattleStart(BattleSystem Ins)//IP_BattleStart_Ones
        {
            if (!SaveManager.IsUnlock(this.MySkill.MySkill.KeyID, SaveManager.NowData.unlockList.SkillPreView))
            {
                SaveManager.NowData.unlockList.SkillPreView.Add(this.MySkill.MySkill.KeyID);
            }

        }
        */
        public override void Init()
        {
            base.Init();

        }

        public override void AddShieldPersent()
        {
            base.AddShieldPersent();
            base.SetShield(true, (int)((this.plus ? 0.48 : 0.32) * this.BChar.GetStat.maxhp), false, 0);
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (Targets[0] == this.BChar || Targets[0].BarrierHP <= 0)
            {
                this.plus = true;
            }
            else
            {
                this.plus = false;
            }
            base.SkillUseSingle(SkillD, Targets);
        }
        public bool plus = false;
    }

    public class Bcl_jud_1 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.BarrierHP += (int)(0.6 * this.Usestate_L.GetStat.maxhp);
        }
    }

    public class Sex_jud_2 : Sex_ImproveClone
    {
        public override string DescExtended(string desc)
        {

            return base.DescExtended(desc).Replace("&a", ((int)(0.2 * this.BChar.GetStat.maxhp)).ToString());


        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            foreach (BattleChar BC in Targets)
            {
                int sumbarrier = 0;
                foreach (Buff buff in BC.Buffs)
                {
                    if (buff.BarrierHP > 0)
                    {
                        sumbarrier += buff.BarrierHP;
                        buff.BarrierHP = 0;
                    }

                }

                Buff buffsum = BC.BuffAdd("B_SF_Judge_2", this.BChar, false, 0, false, -1, false);
                    //BC.BuffReturn("B_SF_Judge_2", false);
                    //buffsum.BarrierHP += Math.Max((int)(1.5 * sumbarrier), (int)(0.2 * this.BChar.GetStat.maxhp));
                    buffsum.BarrierHP = Math.Max((int)(1.5 * sumbarrier), (int)(0.2 * this.BChar.GetStat.maxhp));
                
            }

        }
        public IEnumerator FixTest(BattleChar BC,int sumbarrier)
        {
            yield return new WaitForFixedUpdate();
            Debug.Log("Wrong");
            BC.BuffAdd("B_SF_Judge_2", this.BChar, false, 0, false, -1, false);
            Buff buffsum = BC.BuffReturn("B_SF_Judge_2", false);
            buffsum.BarrierHP = Math.Max((int)(1.5 * sumbarrier), (int)(0.2 * this.BChar.GetStat.maxhp));

            yield return new WaitForFixedUpdate();
            yield break;
        }
    }
    public class Bcl_jud_2 : Buff, IP_BuffUpdate
    {
        public override void Init()
        {
            base.Init();
            //this.BarrierHP += (int)(0.2 * this.Usestate_L.GetStat.maxhp);
        }

        public void BuffUpdate(Buff MyBuff)
        {
            if (this.BarrierHP <= 0)
            {
                //this.SelfDestroy();
                BattleSystem.DelayInputAfter(this.Distroy());
            }
        }

        public IEnumerator Distroy()
        {
            yield return new WaitForFixedUpdate();
            if (this.BarrierHP <= 0)
            {
                this.SelfDestroy();
            }
            yield return null;
            yield break;
        }
    }

    public class Sex_jud_3 : Sex_ImproveClone, IP_BuffUpdate, IP_DamageTake, IP_DamageChange, IP_Dodge
    {
        /*
        public override void HandInit()
        {
            this.SkillBasePlus.Target_BaseDMG = (int)(0.6 * this.BChar.GetStat.maxhp / (1 - 0.01 * this.BChar.GetStat.def)) - 1;
        }
        */
        public override void Init()
        {
            this.CountingExtedned = true;
            this.SkillBasePlus.Target_BaseDMG = (int)(0.6 * this.BChar.GetStat.maxhp / (1 - 0.01 * this.BChar.GetStat.def)) - 1;
        }
        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if (this.MySkill.IsNowCounting && Target == this.BChar)
            {
                this.tar = User;
                this.Attack();
            }


        }
        public void Dodge(BattleChar Char, SkillParticle SP)
        {
            if (Char == this.BChar)
            {
                this.tar = SP.SkillData.Master;
                this.Attack();
            }
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
        }
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if (SkillD == this.MySkill && Target == this.tar)
            {
                Cri = true;
            }
            return Damage;
        }
        public void BuffUpdate(Buff MyBuff)
        {
            this.SkillBasePlus.Target_BaseDMG = (int)(0.6 * this.BChar.GetStat.maxhp / (1 - 0.01 * this.BChar.GetStat.def)) - 1;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            //float def = this.BChar.GetStat.def;
            this.SkillBasePlus.Target_BaseDMG = (int)(0.6 * this.BChar.GetStat.maxhp / (1 - 0.01 * this.BChar.GetStat.def)) - 1;

        }
        private BattleChar tar = new BattleChar();
    }

    public class Bcl_jud_3 : Buff, IP_TurnEnd
    {
        public override void Init()
        {
            float arm = (int)(this.BChar.GetStat.def);
            this.PlusStat.def = Math.Min(-arm, this.PlusStat.def);
            this.PlusStat.dod = (int)(-0.5 * this.PlusStat.def);
        }

        public void TurnEnd()
        {
            this.PlusStat.def = (int)(0.5 * this.PlusStat.def);
            this.PlusStat.dod = (int)(-0.5 * this.PlusStat.def);
        }
    }

    public class Sex_jud_4 : Sex_ImproveClone, IP_SkillUse_Target//, IP_ParticleOut_After
    {

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {

            base.SkillUseSingle(SkillD, Targets);
            this.main = this.MySkill;
            //this.sumDmg = 0;
            this.tarNum = 1;


            int num = 0;
            List<BattleEnemy> list = (Targets[0] as BattleEnemy).EnemyPosNum(out num);
            List<BattleChar> list2 = new List<BattleChar>();


            if (num != 0)
            {
                list2.Add(list[num - 1]);
                this.tarNum++;
            }
            if (list.Count > num + 1)
            {
                list2.Add(list[num + 1]);
                this.tarNum++;
            }
            Targets.AddRange(list2);

            if (list2.Count <= 0)
            {
                //this.MySkill.MySkill.Effect_Target.Buffs.Add(new GDEBuffData("B_SF_Judge_4"));
                BuffTag btg=new BuffTag();
                btg.BuffData = new GDEBuffData("B_SF_Judge_4");
                btg.User = this.BChar;
                this.TargetBuff.Add(btg);
            }
            //this.plist = list2;

        }


        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            /*
            if (DMG >= 0 )
            {
                this.sumDmg += DMG ;
            }
            */
            if (DMG >= 0 && SP.SkillData == this.MySkill)
            {
                Buff buff = new Buff();
                if (!this.BChar.BuffFind("B_SF_Judge_S", false) || !this.sidehit)
                {
                    buff = this.BChar.BuffAdd("B_SF_Judge_S", this.BChar, false, 0, false, -1, false);
                }
                else
                {
                    buff = this.BChar.BuffReturn("B_SF_Judge_S", false);
                }

                int shieldNum = (int)(DMG / this.tarNum);

                /*
                foreach (Skill_Extended sex in this.main.AllExtendeds)
                {
                    if (sex is Sex_jud_4 sex4)
                    {
                        shieldNum = (int)((DMG + sex4.num) / this.tarNum);
                        sex4.num = DMG + sex4.num - shieldNum * this.tarNum;
                        break;
                    }
                }
                */
                shieldNum = (int)((DMG + this.num) / this.tarNum);
                this.num = DMG + this.num - shieldNum * this.tarNum;


                buff.BarrierHP += shieldNum;

            }

        }
        /*
        public IEnumerator ParticleOut_After(Skill SkillD, List<BattleChar> Targets)
        {
            

            if (!this.sidehit && SkillD == this.MySkill)
            {
                this.sidehit = true;

                Skill skill2 = this.MySkill.CloneSkill(true, null, null, false);

                for (int i = 0; i < skill2.AllExtendeds.Count; i++)
                {
                    if (!skill2.AllExtendeds[i].BattleExtended && !skill2.AllExtendeds[i].isDataExtended)
                    {
                        skill2.AllExtendeds[i].SelfDestroy();

                    }
                }

                this.sidehit = false;

                //skill2.PlusHit = true;
                skill2.UseOtherParticle_Path = "Particle/impact";

                this.BChar.ParticleOut(this.MySkill, skill2, this.plist);


            }
        


            yield break;
        }
        */


        private bool sidehit;
        private Skill main = new Skill();
        public int num = 0;

        private List<BattleChar> plist = new List<BattleChar>();
        //private int sumDmg = 0;
        private int tarNum = 0;
    }

    public class Bcl_jud_4 : B_Taunt, IP_Awake, IP_SkillUse_User
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

    public class Sex_jud_5 : Sex_ImproveClone
    {

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {

            int num = 0;
            List<BattleEnemy> list = (Targets[0] as BattleEnemy).EnemyPosNum(out num);
            List<BattleChar> list2 = new List<BattleChar>();

            Targets.RemoveAt(0);
            if (num != 0)
            {
                list2.Add(list[num - 1]);
                Targets.Add(list[num - 1]);
            }
            else
            {
                Targets.Add(list[num]);
            }
            if (list.Count > num + 1)
            {
                list2.Add(list[num + 1]);
                Targets.Add(list[num + 1]);
            }
            else
            {
                Targets.Add(list[num]);
            }
            base.SkillUseSingle(SkillD, Targets);

            Skill skill = this.MySkill.CloneSkill(true, null, null, true);
            skill.isExcept = true;
            skill.FreeUse = true;
            skill.PlusHit = true;
            for (int i = 1; i <= 2; i++)
            {
                if (BattleSystem.instance.EnemyTeam.AliveChars.Count >= 1)
                {
                    BattleSystem.DelayInput(this.Plushit(list[num], list2, skill));
                }
            }
        }

        public IEnumerator Plushit(BattleChar tar0, List<BattleChar> sideTar, Skill skill)//追加射击
        {
            List<BattleChar> list2 = new List<BattleChar>();
            foreach (BattleChar bc in sideTar)
            {
                if (!bc.IsDead)
                {
                    list2.Add(bc);
                }
            }
            for (int i = 0; i < 2 && list2.Count < 2; i++)
            {
                if (!tar0.IsDead)
                {
                    list2.Add(tar0);
                }
                else
                {
                    list2.Add(tar0.MyTeam.AliveChars.Random(this.BChar.GetRandomClass().Main));
                }
            }
            this.BChar.ParticleOut(this.MySkill, skill, list2);
            yield return new WaitForSecondsRealtime(0.3f);
            yield break;
        }
    }

    public class Bcl_jud_5 : Buff, IP_SkillUse_User_After
    {

        public override void BuffStat()
        {
            base.BuffStat();

            //base.Init();
            this.PlusStat.hit = (int)Math.Round(-24 + 24 * (float)Math.Pow(0.5, this.StackNum));
            this.PlusPerStat.Damage = (int)Math.Round(-24 + 24 * (float)Math.Pow(0.5, this.StackNum));
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

    public class Sex_jud_6 : SkillExtended_JudgeShield
    {
        public override void Init()
        {
            base.Init();
        }
        public override void AddShieldPersent()
        {
            base.AddShieldPersent();
            base.SetShield(false, 0, true, (int)(1 * this.BChar.GetStat.maxhp));
        }
    }
    /*
    public class Bcl_jud_6_1 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.BarrierHP += (int)(1 * this.Usestate_L.GetStat.maxhp);
        }
    }
    */
    public class Bcl_jud_6_2 : Buff, IP_Damage_Before//,IP_BarriarDamage_Before
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.def = 15;
        }
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            foreach (BattleChar bc in this.BChar.MyTeam.AliveChars_Vanish)
            {
                if (bc != this.BChar && bc.BuffFind("B_SF_Judge_6_2", false))
                {
                    bc.BuffReturn("B_SF_Judge_6_2", false).SelfDestroy();
                }
            }
        }
        /*
        public void ParticleOutEnd_Before(BattleChar Attacker, Skill skill, ref List<BattleChar> SaveTargets)
        {
            if (this.BChar.BarrierHP > 0 && !Attacker.Info.Ally)
            {
                for (int i = 0;i<SaveTargets.Count;i++)
                {
                    if (SaveTargets[i].Info.Ally && SaveTargets[i]!=this.BChar)
                    {
                        SaveTargets[i] = this.BChar;
                    }
                }
            }
            
        }
        */
        public override void FixedUpdate()
        {
            if (this.BChar.BarrierHP > 0)
            {
                this.shieldFrame = Time.frameCount;
            }
        }
        public void Damage_Before(ref BattleChar battleChar, BattleChar User, ref int Dmg, bool Cri, bool Pain, ref bool NOEFFECT, int PlusPenetration, bool IgnoreHealingPro, bool HealingPro, bool OnlyUnscaleTime)
        {
            //Debug.Log("Damage_Before1");
            if ((this.BChar.BarrierHP > 0 || Time.frameCount < this.shieldFrame + 5) && battleChar.Info.Ally && battleChar != this.BChar && Dmg > 0 && !Pain)
            {
                //Debug.Log("Damage_Before2");
                int num = Dmg;
                battleChar = this.BChar.MyTeam.DummyChar;
                Dmg = 0;
                this.BChar.Damage(User, num, Cri, Pain, NOEFFECT, PlusPenetration, IgnoreHealingPro, HealingPro, OnlyUnscaleTime);
                //battleChar =this.BChar;
                //Dmg = (int)(Dmg * 0.5);
            }
        }


        /*
        public void BarriarDamage_Before(BattleChar BC, ref int Dmg)
        {
            if(this.BChar.BarrierHP<=0 && Time.frameCount<this.shieldFrame+5)
            {
                Dmg = 0;
            }
        }
        */
        private int shieldFrame;
    }

    public class Sex_jud_7 : SkillExtended_JudgeShield
    {
        public override void Init()
        {
            base.Init();
        }
        public override void AddShieldPersent()
        {
            base.AddShieldPersent();
            base.SetShield(false, 0, true, (int)(0.5 * this.BChar.GetStat.maxhp));
        }
    }


    public class Sex_jud_8 : Sex_ImproveClone
    {
        public override string DescExtended(string desc)
        {
            int setnum = 0;
            if (BattleSystem.instance != null)
            {
                foreach (BattleAlly battleAlly in this.BChar.BattleInfo.AllyList)
                {
                    setnum += battleAlly.BarrierHP;
                }

            }
            int num = (int)(setnum / this.Constant);
            if (num * this.Constant < setnum)
            {
                num++;
            }

            return base.DescExtended(desc).Replace("&a", num.ToString()).Replace("&b",this.Constant.ToString());


        }
        /*
        public override void BattleStartDeck(List<Skill> Skills_Deck)
        {
            this.BChar.MyTeam.Draw(this.MySkill, null);

        }
        */

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {

            Bcl_jud_8 b8 = this.BChar.BuffAdd("B_SF_Judge_8", this.MySkill.Master, false, 0, false, -1, false) as Bcl_jud_8;
            //Bcl_jud_8 b8 = BattleSystem.instance.AllyTeam.LucyChar.BuffAdd("B_SF_Judge_8", this.MySkill.Master, false, 0, false, -1, false) as Bcl_jud_8;

            //Bcl_int_13 b13 = this.BChar.BuffReturn("B_SF_Intruder_13", false)as Bcl_int_13;
            int setnum = 0;
            foreach (BattleAlly battleAlly in this.BChar.BattleInfo.AllyList)
            {
                setnum += battleAlly.BarrierHP;
            }
            int num = (int)(setnum / this.Constant);
            if(num*this.Constant<setnum)
            {
                num++;
            }
            b8.B8Updata(num);

            foreach (BattleAlly battleAlly in this.BChar.BattleInfo.AllyList)
            {
                BattleChar battleChar = battleAlly as BattleChar;
                foreach (Buff buff in battleChar.Barriers)
                {
                    buff.BarrierHP = 0;
                }
            }

        }
        public int Constant
        {
            get 
            {
                return this.constant;
                /*
                Stat stat=default(Stat);
                stat = this.BChar.Info.OriginStat;
                stat += this.BChar.Info.AllyLevelPlusStat(0);
                return (int)Math.Round(0.48*stat.maxhp); 
                */
            } 
        }
        private int constant = 12;
    }



    public class Bcl_jud_8 : Buff, IP_PartybarrierDamage, IP_Set_PartyBarrierHP//,IP_Get_PartyBarrierHP
    {
        /*
        public override void FixedUpdate()
        {
            BattleSystem.instance.AllyTeam.partybarrier.BarrierTextUpadte();

        }
        */

        public void PartybarrierDamage(int Damage)

        {
            //Debug.Log("Bcl_jud_8:PartybarrierDamage1");
            //int num=this.lastNum;
            //Debug.Log("Bcl_jud_8：PartybarrierDamage2,lastnum=" + this.lastNum);
            //Debug.Log("Bcl_jud_8：PartybarrierDamage2,buff_barrier=" + this.PartyBarrierHP);
            //Debug.Log("Bcl_jud_8：PartybarrierDamage2,all_barrier=" + (int)Traverse.Create(this.BChar.MyTeam.partybarrier).Field("_BarrierHP").GetValue());
            Traverse.Create(this).Field("_PartyBarrrierHP").SetValue(Math.Min(this.lastNum, this.BChar.MyTeam.partybarrier.BarrierHP));
            BattleSystem.instance.StartCoroutine(this.Delay_1());

            //Debug.Log("Bcl_jud_8:PartybarrierDamage3:" + Traverse.Create(this).Field("_PartyBarrrierHP").GetValue());
        }



        public void Set_PartyBarrierHP(Buff buff, ref int num)
        {
            //Debug.Log("Bcl_jud_8：point1,lastnum="+this.lastNum );

            //Debug.Log("Bcl_jud_8：point2,PartyBarrierHP=" + this.PartyBarrierHP);

            int HPold = this.PartyBarrierHP;
            //int HPold = this.barrierLock;

            //this.barrierLock = 0;
            if (buff == this)
            {

                int frameCount = Time.frameCount;
                //Debug.Log("Bcl_jud_8：point3,frameCount=" + frameCount+ "  this.timerem="+ this.timerem);
                if (this.timerem > frameCount)
                {
                    this.timerem = frameCount;
                }
                if ((this.timerem+2) < frameCount )
                {
                    this.timeprotect = this.PartyBarrierHP - this.protectNum;
                    //Debug.Log("Bcl_jud_8：point4,ResetTime");

                }
                this.timerem = frameCount;

                num = Math.Max(this.timeprotect, num);
                num = Math.Max(0, num);
                //Debug.Log("Bcl_jud_8：point5,newnum=" + num);

                int numnew = num;
                this.lastNum = this.lastNum - HPold + numnew;
                //Debug.Log("Bcl_jud_8：point6,lastnum=" + this.lastNum);

                num = 0;

                //Debug.Log("HPold：" + HPold);
                //Debug.Log("numnew：" + numnew);
                int partyBar = (int)Traverse.Create(this.BChar.MyTeam.partybarrier).Field("_BarrierHP").GetValue();
                //Debug.Log("编制保护前：" + (int)Traverse.Create(this.BChar.MyTeam.partybarrier).Field("_BarrierHP").GetValue());
                Traverse.Create(this.BChar.MyTeam.partybarrier).Field("_BarrierHP").SetValue(Math.Max(0, partyBar - HPold + numnew));
                //this.BChar.MyTeam.partybarrier.BarrierHP = partyBar - HPold + numnew;
                //Debug.Log("编制保护后：" + (int)Traverse.Create(this.BChar.MyTeam.partybarrier).Field("_BarrierHP").GetValue());



                if (this.lastNum <= 0 || this.BChar.MyTeam.partybarrier.BarrierHP <= 0)
                {
                    this.SelfDestroy();
                }
                BattleSystem.instance.AllyTeam.partybarrier.BarrierTextUpadte();
            }
        }
        public IEnumerator Delay_1()
        {
            yield return new WaitForFixedUpdate();
            if (this.PartyBarrierHP > 0)
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
        public void B8Updata(int num = 0)
        {
            //Debug.Log("Bcl_jud_8:Updata");

            //Traverse.Create(this).Field("_PartyBarrrierHP").SetValue(10);
            this.BChar.MyTeam.partybarrier.BarrierHP += num;
            this.lastNum += num;
            this.protectNum = 1;
            //Debug.Log("LastNum=" + this.lastNum);
            //BattleSystem.instance.AllyTeam.partybarrier.BarrierTextUpadte();

        }


        private int lastNum = 0;
        private int protectNum;

        private int timerem;
        private int timeprotect;
    }

    public class Sex_jud_9 : SkillExtended_JudgeShield
    {

        public override void Init()
        {
            base.Init();

        }

        public override void AddShieldPersent()
        {
            base.AddShieldPersent();
            base.SetShield(true, (int)(0.2 * this.BChar.GetStat.maxhp), false, 0);
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            foreach (BattleChar b in Targets)
            {
                b.BuffAdd("B_SF_Judge_9", this.BChar, false, 0, false, -1, false);
            }
        }
    }
    public class Bcl_jud_9 : Buff
    {
        public override void Init()
        {
            base.Init();
            if (this.BChar.Info.GetData.Role.Key == GDEItemKeys.CharRole_Role_Tank)
            {
                this.PlusStat.def = 10;
            }
            else if (this.BChar.Info.GetData.Role.Key == GDEItemKeys.CharRole_Role_Support)
            {
                this.PlusPerStat.Heal = 15;
            }
            else if (this.BChar.Info.GetData.Role.Key == GDEItemKeys.CharRole_Role_DPS)
            {
                this.PlusPerStat.Damage = 15;
            }
            else
            {
                this.PlusStat.def = 3;
                this.PlusPerStat.Heal = 5;
                this.PlusPerStat.Damage = 5;
            }
        }
    }
    public class Sex_jud_10 : SkillExtended_JudgeShield
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

    }
    public class Bcl_jud_10 : Buff, IP_PlayerTurn
    {
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!this.pflag2 && this.BChar.BarrierHP <= 0 && !this.pflag)
            {
                this.pflag = true;
                this.SelfDestroy();
            }
        }
        public void Turn()
        {
            this.pflag2 = false;
        }
        public void TurnEnd()
        {
            this.pflag2 = true;
        }
        public override void SelfdestroyPlus()
        {
            if (this.pflag)
            {
                this.BChar.BuffAdd("B_SF_Judge_10_1", this.Usestate_L, false, 0, false, -1, false);

            }
            else
            {
                this.BChar.BuffAdd("B_SF_Judge_10_2", this.Usestate_L, false, 0, false, -1, false);
            }
            base.SelfdestroyPlus();
        }
        private bool pflag = false;
        private bool pflag2 = false;
    }

    public class Bcl_jud_10_1 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.def = 10;
        }
    }
    public class Bcl_jud_10_2 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusPerStat.Damage = 15; ;
        }
    }



    public class Sex_jud_11 : SkillExtended_JudgeShield
    {

        public override void Init()
        {
            base.Init();

        }

        public override void AddShieldPersent()
        {
            base.AddShieldPersent();
            base.SetShield(true, (int)(0.6 * this.BChar.GetStat.maxhp), false, 0);
        }

    }
    public class Bcl_jud_11 : Buff, IP_DamageTake, IP_BarriarDamage_After
    {
        public override string DescExtended()
        {


            return base.DescExtended().Replace("&a", (this.sumDMG).ToString());


        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (this.BChar.BarrierHP <= 0 && !this.pflag)
            {
                this.pflag = true;
                this.SelfDestroy();
            }
        }

        private bool pflag;


        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)//, ref int __result);
        {

            this.pflag2 = true;
            this.barrierHP_before = this.BChar.BarrierHP;


        }

        // Token: 0x06000001 RID: 1

        public void BarriarDamage_After(BattleChar BC, int Dmg)//, ref int __result);
        {
            if (this.pflag2 && BC == this.BChar)
            {
                this.sumDMG = this.sumDMG + this.barrierHP_before - this.BChar.BarrierHP;
            }
            this.pflag2 = false;

            if (this.BChar.BarrierHP <= 0)
            {
                this.SelfDestroy();

            }
        }


        private bool pflag2;
        private int barrierHP_before = 0;
        /*
        public void DamakeTakeFinal(BattleChar User, int Dmg, bool Cri, bool Pain, ref bool resist)
        {
            if(this.pflag2 )
            {
                this.sumDMG =this.sumDMG+ this.barrierHP_before - this.BChar.BarrierHP;
            }
            this.pflag2 = false;

            if(this.BChar.BarrierHP<=0)
            {
                this.SelfDestroy();

            }
        
        }
        */
        public override void SelfdestroyPlus()
        {
            //base.SelfdestroyPlus();
            this.ShieldAttack();
        }
        public void ShieldAttack()
        {
            if (this.sumDMG > 0)
            {
                //Debug.Log("ShieldAttack point 1");
                Skill skill = Skill.TempSkill("S_SF_Judge_11_1", this.Usestate_L, this.Usestate_L.MyTeam);
                //Debug.Log("ShieldAttack point 2");
                skill.FreeUse = true;
                skill.PlusHit = true;
                skill.MySkill.Effect_Target.DMG_Per = 0;
                skill.MySkill.Effect_Target.DMG_Base = (int)(1.5 * this.sumDMG);
                if(BattleSystem.instance.EnemyTeam.AliveChars.Count>0)
                {
                    this.Usestate_L.ParticleOut(skill, BattleSystem.instance.EnemyTeam.AliveChars);
                }
                //Debug.Log("ShieldAttack point 3");

            }
        }
        private int sumDMG = 0;
    }

    public class Sex_jud_12 : SkillExtended_JudgeShield
    {

        public override void Init()
        {
            base.Init();

        }

        public override void AddShieldPersent()
        {
            base.AddShieldPersent();
            base.SetShield(false, 0, true, (int)(0.3 * this.BChar.GetStat.maxhp));
        }

    }


    public class Bcl_jud_12 : Buff, IP_Healed, IP_HealedForAll, IP_Heal_Before//,IP_HealChange
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.Stun = true;
            this.PlusStat.def = 25;
        }
        /*
        public void HealChange(BattleChar Healer, BattleChar HealedChar, ref int HealNum, bool Cri, ref bool Force)
        {
            if(HealedChar==this.BChar)
            {
                HealNum=(int)(0.5*HealNum);
            }
        }
        */
        public void Healed(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal)
        {
            if (HealedChar == this.BChar)
            {
                Buff buff = this.BChar.BuffAdd("B_SF_Judge_S", this.BChar, false, 0, false, -1, false);
                buff.BarrierHP += 1 * HealNum;
            }
            /*
            else if(HealedChar!=this.BChar && HealedChar.Info.Ally)
            {
                Buff buff = this.BChar.BuffAdd("B_SF_Judge_S", this.BChar, false, 0, false, -1, false);
                buff.BarrierHP += (int)(0.5 * HealNum);
            }
            */
        }

        public void HealedForAll(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal)
        {
            if (HealedChar != this.BChar && HealedChar.Info.Ally)
            {
                Buff buff = this.BChar.BuffAdd("B_SF_Judge_S", this.BChar, false, 0, false, -1, false);
                buff.BarrierHP += (int)(0.5 * HealNum);
            }

        }

        public void Heal_Before(BattleChar HealTarget, BattleChar User, float Hel, bool Cri, bool Force = false)
        {
            if (HealTarget != this.BChar)
            {
                HealTarget.BuffAdd("B_SF_Judge_12_1", this.BChar, false, 0, false, -1, true);

            }
        }

    }

    public class Bcl_jud_12_1 : Buff, IP_Healed
    {
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            BattleSystem.DelayInputAfter(this.Destroy());
        }

        public void Healed(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal)
        {
            if (HealedChar == this.BChar)
            {
                foreach (IP_HealedForAll ip_Healed in BattleSystem.instance.IReturn<IP_HealedForAll>())
                {
                    if (ip_Healed != null)
                    {
                        ip_Healed.HealedForAll(Healer, HealedChar, HealNum, Cri, OverHeal);
                    }
                }
            }

        }

        public IEnumerator Destroy()
        {
            yield return new WaitForFixedUpdate();
            this.SelfDestroy();
            yield break;
        }
    }
    public interface IP_HealedForAll
    {
        // Token: 0x060025D9 RID: 9689
        void HealedForAll(BattleChar Healer, BattleChar HealedChar, int HealNum, bool Cri, int OverHeal);
    }


    public class Sex_jud_13 : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            List<Skill> list = new List<Skill>();
            foreach (Skill skill in BattleSystem.instance.AllyTeam.Skills_Deck)
            {
                //Debug.Log("抽牌堆1:" + skill.MySkill.Name);
                List<BuffTag> tags = skill.ReturnAllBuffs();

                foreach (BuffTag tag in tags)
                {
                    //Debug.Log( skill.MySkill.Name+":barrier:"+ tag.BuffData.Barrier);

                    //if (tag.BuffData.Barrier > 0)
                    if (Buff.DataToBuff(tag.BuffData, null, skill.Master, -1, true, 0).BarrierHP > 0)
                    {
                        list.Add(skill);
                        //flag = true;
                        //Debug.Log("list.Add:" + skill.MySkill.Name);
                        //goto IL_jud_13_1;
                        break;
                    }
                }


                //IL_jud_13_1:;
            }
            List<Skill> list2 = new List<Skill>();
            for (int i = 0; i < 2 && list.Count > 0; i++)
            {
                //System.Random ran = new System.Random();
                int x = UnityEngine.Random.Range(0, list.Count);
                list2.Add(list[x]);

                //Debug.Log("list2.Add:"+list[x].MySkill.Name);
                list.RemoveAt(x);
            }


            int rounds = 2;

            Skill plusskill = new Skill();

            if (list2.Count < 2)
            {
                rounds = 3;

            }



            for (int i = 0; i < 2; i++)
            {
                if (list2.Count > 0)
                {
                    BattleSystem.DelayInputAfter(this.Draw(list2[0]));
                    //yield return BattleSystem.instance.StartCoroutine(this.Draw(list2[0]));
                    list2.RemoveAt(0);
                }
                else
                {
                    BattleSystem.DelayInputAfter(this.Draw(null));
                    //yield return BattleSystem.instance.StartCoroutine(this.Draw(null));
                }
            }



        }

        public IEnumerator Draw(Skill skill)
        {



            if (skill == null)
            {
                //this.pflag1 = true;
                BattleSystem.instance.AllyTeam.Draw(new BattleTeam.DrawInput(this.DrawInput));
            }
            else
            {


                yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyTeam._ForceDraw(skill, new BattleTeam.DrawInput(this.DrawInput))); ;
            }
            yield return null;
            yield break;
        }


        public void DrawInput(Skill skill)
        {
            //Debug.Log("DreawInput1");
            bool shield = false;
            List<BuffTag> tags = skill.ReturnAllBuffs();
            foreach (BuffTag tag in tags)
            {
                //if (tag.BuffData.Barrier > 0)
                if (Buff.DataToBuff(tag.BuffData, null, skill.Master, -1, true, 0).BarrierHP > 0)
                {
                    shield = true;
                    break;
                }
            }

            if (!shield && !this.pflag1)
            {
                this.pflag1 = true;
                //Debug.Log("DreawInput1");
                BattleSystem.DelayInputAfter(this.PlusDraw());
            }
        }

        public IEnumerator PlusDraw()
        {
            //Debug.Log("PlusDraw1");

            yield return BattleSystem.instance.StartCoroutine(this.ReCharge());
            //yield return this.ReCharge();

            //Debug.Log("PlusDraw2");
            List<Skill> list = new List<Skill>();
            foreach (Skill skill in BattleSystem.instance.AllyTeam.Skills_Deck)
            {
                //Debug.Log("抽牌堆2:" + skill.MySkill.Name);
                List<BuffTag> tags2 = skill.ReturnAllBuffs();
                foreach (BuffTag tag in tags2)
                {
                    //if (tag.BuffData.Barrier > 0)
                    if (Buff.DataToBuff(tag.BuffData, null, skill.Master, -1, true, 0).BarrierHP > 0)
                    {
                        list.Add(skill);
                        break;
                    }
                }
            }
            if (list.Count > 0)
            {
                int x = UnityEngine.Random.Range(0, list.Count);
                BattleSystem.DelayInputAfter(this.Draw(list[x]));
            }
            else
            {
                BattleSystem.DelayInputAfter(this.Draw(null));
            }


            yield return null;
            yield break;
        }
        public IEnumerator ReCharge()
        {
            //Debug.Log("Recharge1");
            for (int i = 0; i < BattleSystem.instance.AllyTeam.Skills_UsedDeck.Count; i++)//将弃牌库中自己的技能洗入牌库
            {
                //Debug.Log("弃牌堆:" + BattleSystem.instance.AllyTeam.Skills_UsedDeck[i].MySkill.Name);
                bool flag1 = false;
                List<BuffTag> tags = BattleSystem.instance.AllyTeam.Skills_UsedDeck[i].ReturnAllBuffs();
                foreach (BuffTag tag in tags)
                {
                    //if (tag.BuffData.Barrier > 0)
                    if (Buff.DataToBuff(tag.BuffData, null, BattleSystem.instance.AllyTeam.Skills_UsedDeck[i].Master, -1, true, 0).BarrierHP > 0)
                    {
                        flag1 = true;
                        break;
                    }
                }

                if (flag1)
                {
                    //Debug.Log("Recharge2");
                    int index = UnityEngine.Random.Range(0, BattleSystem.instance.AllyTeam.Skills_Deck.Count + 1);
                    BattleSystem.instance.AllyTeam.Skills_Deck.Insert(index, BattleSystem.instance.AllyTeam.Skills_UsedDeck[i]);

                    BattleSystem.instance.AllyTeam.Skills_UsedDeck.RemoveAt(i);
                    i--;
                    //Debug.Log("Recharge3");
                }
            }
            yield return null;
            yield break;
        }

        private bool pflag1;
        private int pint = 0;


    }

    public class E_jud_0 : EquipBase, IP_BarriarDamage_Before//, IP_TakeDamage_Before
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.def = 15;
            this.PlusPerStat.MaxHP = 15;
            this.PlusStat.dod = -20;

        }
        /*
        public void TakeDamage_Before(PartyBarrier partyBarrier, ref int Damage)
        {
            int frame = Time.frameCount;
            if (this.BChar.MyTeam.partybarrier.BarrierHP > 0 || frame < this.timeFrame1 + 5)
            {
                //Damage = (int)(0.75 * Damage);
                if (Damage > this.BChar.MyTeam.partybarrier.BarrierHP)
                {
                    Damage = Math.Min(Damage, this.BChar.MyTeam.partybarrier.BarrierHP);
                }
                this.timeFrame1 = frame;

            }
        }

        private int timeFrame1;
        */
        public void BarriarDamage_Before(BattleChar BC, ref int Dmg)//, ref int __result)
        {
            int frame = Time.frameCount;
            Charframe save = this.list.Find(a => a.BC == BC);
            if (BC.Info.Ally && (BC.BarrierHP > 0 || (save != null && frame < save.frame + 2)) && BC.MyTeam.partybarrier.BarrierHP <= 0)
            {
                //Dmg = (int)(0.75 * Dmg);
                if (Dmg > BC.BarrierHP)
                {
                    Dmg = Math.Min(Dmg, BC.BarrierHP);
                }

            }
            this.list.Remove(save);
            Charframe updata = new Charframe();
            updata.BC = BC;
            updata.frame = frame;
            this.list.Add(updata);

            //this.timeFrame2 = frame;
        }
        private int timeFrame2;

        private List<Charframe> list = new List<Charframe>();
    }


    public class E_jud_1 : EquipBase, IP_TurnEnd, IP_PlayerTurn//, IP_TakeDamage_Before
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.def = 15;
            this.PlusPerStat.MaxHP = 15;
            this.PlusStat.dod = -20;

        }
        public void CheckBarrier()
        {
            this.frame = 0;
            if (this.BChar.BattleInfo == null)
            {
                return;
            }
            foreach (BattleChar bc in this.BChar.MyTeam.AliveChars_Vanish)
            {
                if (!this.activedChar.Contains(bc) && bc.BarrierHP <= 0)
                {
                    //Debug.Log("反应防护触发：" + bc.Info.Name + "-" + this.BChar.BattleInfo.TurnNum);
                    this.activedChar.Add(bc);

                    Buff buff = bc.BuffAdd("B_SF_Judge_S", this.BChar, false, 0, false, -1, false);
                    int num = (int)(0.15f * this.BChar.GetStat.maxhp);
                    buff.BarrierHP += num;
                }
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(BattleSystem.instance != null)
            {
                this.frame++;
                if (this.frame > 10&&this.active)
                {
                    this.CheckBarrier();
                }
            }

        }
        public void Turn()
        {
            if(this.BChar.BattleInfo.TurnNum>0)
            {
                this.active = true;
            }
            this.activedChar.Clear();
        }
        public void TurnEnd()
        {
            if(this.BChar.BattleInfo==null)
            {
                return;
            }
            foreach (BattleChar bc in this.BChar.MyTeam.AliveChars_Vanish)
            {
                if (!this.activedChar.Contains(bc))
                {
                    this.activedChar.Add(bc);

                    Buff buff = bc.BuffAdd("B_SF_Judge_S", this.BChar, false, 0, false, -1, false);
                    int num = (int)(0.15f * this.BChar.GetStat.maxhp);
                    buff.BarrierHP += num;
                }
            }
        }
        public bool active = false;

        public int frame = 0;
        [XmlIgnore]
        public List<BattleChar> activedChar = new List<BattleChar>();
    }
    public class Charframe
    {
        public BattleChar BC = new BattleChar();
        public int frame = -5;
    }

    public class SkillEn_SF_Judge_0 : Sex_ImproveClone
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return true;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (this.BChar.BarrierHP > 0 != this.active)
            {
                this.active = this.BChar.BarrierHP > 0;

            }
            /*
            if (this.NotCount != this.active )
            {
                this.NotCount = this.active;
            }
            */
            
            if (this.APChange != (this.active ? -1 : 0))
            {
                this.APChange = this.active ? -1 : 0;
            }
            
        }
        bool active = false;
    }


    public class SkillEn_SF_Judge_1 : Sex_ImproveClone
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return true;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            int sum = 0;
            foreach (BattleChar bc in this.BChar.MyTeam.AliveChars)
            {
                if (bc != this.BChar)
                {
                    sum += bc.BarrierHP;
                }
            }
            Buff buff = this.BChar.BuffAdd("B_SF_Judge_S", this.BChar, false, 0, false, -1, false);
            buff.BarrierHP += (int)(0.5 * sum);
        }
    }
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


