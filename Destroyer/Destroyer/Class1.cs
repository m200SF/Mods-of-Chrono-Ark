using ChronoArkMod.Plugin;

using GameDataEditor;
using HarmonyLib;
using I2.Loc;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.Events;

using UnityEngine.UI;

using ChronoArkMod;


using System;
using System.Collections;
using System.Linq;

using System.Threading.Tasks;
using UnityEngine.EventSystems;

using DarkTonic.MasterAudio;

using ChronoArkMod.ModData;
using ChronoArkMod.Template;
using ChronoArkMod.ModData.Settings;

using SF_Standard;


namespace SF_Destroyer
{
    
    [PluginConfig("M200_SF_Destroyer_CharMod", "SF_Destroyer_CharMod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class SF_Destroyer_CharacterModPlugin : ChronoArkPlugin
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

        public class DestroyerDef : ModDefinition
        {
        }
    }

    
    public class S_SF_Destroyer_10_Particle : CustomSkillGDE<SF_Destroyer_CharacterModPlugin.DestroyerDef>
    {
        // Token: 0x060000AB RID: 171 RVA: 0x000055A0 File Offset: 0x000037A0
        public override ModGDEInfo.LoadingType GetLoadingType()
        {
            return ModGDEInfo.LoadingType.Replace;
        }

        // Token: 0x060000AC RID: 172 RVA: 0x000055B4 File Offset: 0x000037B4
        public override string Key()
        {
            return "S_SF_Destroyer_10";
        }

        // Token: 0x060000AD RID: 173 RVA: 0x000055CB File Offset: 0x000037CB
        public override void SetValue()
        {
            base.Particle = base.assetInfo.CodeConstuctAsync<GameObject>("Particle/Enemy/Shiranui/S_Shiranui_0", new ModAssetInfo.ObjectChangeDelegate<GameObject>(this.ParticleChangeDelegate));//"Particle/Twins/Hel_2"
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
            skillParticle.GroundOut = true;

            /*
            ParticleColorChange(particle.transform.GetChild(12), new Color(1, 0.8f, 0.3f, 1));
            ParticleColorChange(particle.transform.GetChild(11), new Color(1, 1, 1, 0));
            ParticleColorChange(particle.transform.GetChild(10).GetChild(1), new Color(1, 1, 1, 1));
            ParticleColorChange(particle.transform.GetChild(9), new Color(1, 0.7882908f, 0.4858491f, 1));
            ParticleColorChange(particle.transform.GetChild(10), new Color(1, 1, 1, 1));
            ParticleColorChange(particle.transform.GetChild(7), new Color(1, 0.7f, 0.3f, 1));
            ParticleColorChange(particle.transform.GetChild(7).GetChild(0), new Color(1, 0.7f, 0.3f, 1));
            ParticleColorChange(particle.transform.GetChild(8), new Color(0.3490566f, 0.3490566f, 0.3490566f, 1));
            ParticleColorChange(particle.transform.GetChild(4), new Color(1, 1, 1, 1));
            ParticleColorChange(particle.transform.GetChild(1), new Color(1, 1, 1, 1));
            ParticleColorChange(particle.transform.GetChild(0), new Color(1, 1, 1, 1));
            ParticleColorChange(particle.transform.GetChild(1).GetChild(0), new Color(0, 0, 0, 1));
            ParticleColorChange(particle.transform.GetChild(10).GetChild(0), new Color(1, 0.8759255f, 0.4666667f, 1));
            */
            return particle;

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
            //MasterAudio.PlaySound("A_des_4", 1f, null, 0f, null, null, false, false);
            float volume = 0f;
            float volume2 = 0f;
            if (ModManager.getModInfo("SF_Destroyer").GetSetting<ToggleSetting>("AudioOn").Value)
            {
                volume = ModManager.getModInfo("SF_Destroyer").GetSetting<SliderSetting>("AudioVolume").Value;
            }
            else
            {
                volume = 0f;
            }

            if (inText.Contains("T_des_1_1"))
            {
                inText = inText.Replace("T_des_1_1:", string.Empty);
                MasterAudio.PlaySound("A_des_1_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_des_2_1"))
            {
                inText = inText.Replace("T_des_2_1:", string.Empty);
                MasterAudio.PlaySound("A_des_2_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_des_2_2"))
            {
                inText = inText.Replace("T_des_2_2:", string.Empty);
                MasterAudio.PlaySound("A_des_2_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_des_2_3"))
            {
                inText = inText.Replace("T_des_2_3:", string.Empty);
                MasterAudio.PlaySound("A_des_2_3", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_des_3_1"))
            {
                inText = inText.Replace("T_des_3_1:", string.Empty);
                MasterAudio.PlaySound("A_des_3_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_des_4_1"))
            {
                inText = inText.Replace("T_des_4_1:", string.Empty);
                MasterAudio.PlaySound("A_des_4_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_des_4_2"))
            {
                inText = inText.Replace("T_des_4_2:", string.Empty);
                MasterAudio.PlaySound("A_des_4_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_des_5_1"))
            {
                inText = inText.Replace("T_des_5_1:", string.Empty);
                MasterAudio.PlaySound("A_des_5_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_des_6_1"))
            {
                inText = inText.Replace("T_des_6_1:", string.Empty);
                MasterAudio.PlaySound("A_des_6_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_des_6_2"))
            {
                inText = inText.Replace("T_des_6_2:", string.Empty);
                MasterAudio.PlaySound("A_des_6_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_des_7_1"))
            {
                inText = inText.Replace("T_des_7_1:", string.Empty);
                MasterAudio.PlaySound("A_des_7_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_des_11_1"))
            {
                inText = inText.Replace("T_des_11_1:", string.Empty);
                MasterAudio.PlaySound("A_des_11_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_des_11_2"))
            {
                inText = inText.Replace("T_des_11_2:", string.Empty);
                MasterAudio.PlaySound("A_des_11_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_des_11_3"))
            {
                inText = inText.Replace("T_des_11_3:", string.Empty);
                MasterAudio.PlaySound("A_des_11_3", volume, null, 0f, null, null, false, false);
            }



            else if (inText.Contains("T_cer_1_1"))
            {
                inText = inText.Replace("T_cer_1_1:", string.Empty);
                MasterAudio.PlaySound("A_cer_1_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_cer_2_1"))
            {
                inText = inText.Replace("T_cer_2_1:", string.Empty);
                MasterAudio.PlaySound("A_cer_2_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_cer_3_1"))
            {
                inText = inText.Replace("T_cer_3_1:", string.Empty);
                MasterAudio.PlaySound("A_cer_3_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_cer_4_1"))
            {
                inText = inText.Replace("T_cer_4_1:", string.Empty);
                MasterAudio.PlaySound("A_cer_4_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_cer_5_1"))
            {
                inText = inText.Replace("T_cer_5_1:", string.Empty);
                MasterAudio.PlaySound("A_cer_5_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_cer_6_1"))
            {
                inText = inText.Replace("T_cer_6_1:", string.Empty);
                MasterAudio.PlaySound("A_cer_6_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_cer_7_1"))
            {
                inText = inText.Replace("T_cer_7_1:", string.Empty);
                MasterAudio.PlaySound("A_cer_7_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_cer_11_1"))
            {
                inText = inText.Replace("T_cer_11_1:", string.Empty);
                MasterAudio.PlaySound("A_cer_11_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_cer_11_2"))
            {
                inText = inText.Replace("T_cer_11_2:", string.Empty);
                MasterAudio.PlaySound("A_cer_11_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_cer_11_3"))
            {
                inText = inText.Replace("T_cer_11_3:", string.Empty);
                MasterAudio.PlaySound("A_cer_11_3", volume, null, 0f, null, null, false, false);
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

            if (CharId == "SF_Destroyer")
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
            bool flag = inputitem.itemkey == "E_SF_Destroyer_0" && __instance.Info.GetData.Key != "SF_Destroyer";
            if (flag)
            {
                __result = true;
            }
            //Debug.Log("成功进入OnDropSlot的patch");
        }
    }

    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("SkillUseEffect")]
    public static class SkillUseEffectPlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void SkillUseEffect_Before_Patch(BattleChar __instance, Skill skill, List<BattleChar> Target)
        {

            foreach (IP_SkillUseEffect_Before ip_patch in __instance.IReturn<IP_SkillUseEffect_Before>(null))
            {
                try
                {
                    if (ip_patch != null)
                    {
                        ip_patch.SkillUseEffect_Before(skill, Target);
                    }
                }
                catch
                {
                }
            }
            //Debug.Log("成功进入OnlyDamage前的patch");

        }
        
    }
    
    public interface IP_SkillUseEffect_Before
    {
        // Token: 0x06000001 RID: 1
        void SkillUseEffect_Before(Skill SkillD, List<BattleChar> Target);
    }

    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("Effect")]
    public static class EffectPlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void Effect_Before_Patch(BattleChar __instance, SkillParticle SP, bool Dodge)
        {

            foreach (IP_Effect_Before ip_patch in BattleSystem.instance.IReturn<IP_Effect_Before>())
            {
                try
                {
                    if (ip_patch != null)
                    {
                        ip_patch.Effect_Before(SP);
                    }
                }
                catch
                {
                }
            }
            //Debug.Log("成功进入OnlyDamage前的patch");

        }

    }

    public interface IP_Effect_Before
    {
        // Token: 0x06000001 RID: 1
        void Effect_Before(SkillParticle SP);
    }

    public class P_Destroyer : Passive_Char, IP_SkillUseEffect_Before, IP_LevelUp
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
                list.Add(ItemBase.GetItem("E_SF_Destroyer_0", 1));
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

        public override void TurnUpdate()
        {
            base.TurnUpdate();
            this.turnfired = false;


        }
        public override void FixedUpdate()
        {
            
            //base.FixedUpdate();
            if(!this.BChar.BuffFind("B_SF_Destroyer_p_1",false))
            {
                this.BChar.BuffAdd("B_SF_Destroyer_p_1", this.BChar, false, 0, false, -1, false);
            }

            if (!this.BChar.BuffFind("B_SF_Destroyer_p_2_S", false) && !this.turnfired)
            {
                this.BChar.BuffAdd("B_SF_Destroyer_p_2_S", this.BChar, false, 0, false, -1, false);
            }
        }
        
        public void SkillUseEffect_Before(Skill SkillD, List<BattleChar> Targets)
        {
            bool canfire = !this.turnfired;
            bool remain = false;
            foreach (IP_CheckPlusFire ip_patch in this.BChar.IReturn<IP_CheckPlusFire>())
            {
                //Debug.Log("IP测试1");
                bool flag = ip_patch != null;
                if (flag)
                {
                    bool check= ip_patch.CheckPlusFire(this.BChar);
                    canfire = canfire || check;
                    remain= remain || check;
                }
            }

            if (canfire && SkillD.IsDamage && !Targets[0].Info.Ally && SkillD.AllExtendeds.Find(a=>a is Sex_des_p_2)==null)
            {

                Sex_des_p_2 sex= new Sex_des_p_2();
                SkillD.ExtendedAdd(sex);

                if (!remain)
                {
                    this.turnfired = true;
                    this.BChar.BuffRemove("B_SF_Destroyer_p_2_S", false);
                }
            }

        }
        
        public bool turnfired = false;
    }

    public interface IP_CheckPlusFire
    {
        // Token: 0x06000001 RID: 1
        bool CheckPlusFire(BattleChar BC=null);
    }

    public interface IP_AttackModeChange
    {
        // Token: 0x06000001 RID: 1
        void AttackModeChange(BattleChar bc, int mode);
    }
    
    public class Bcl_des_p_1 : Buff,IP_ParticleOut_Before,IP_BuffAdd,IP_BuffAddAfter,IP_DamageChange
    {
        
        public override string DescExtended()//模式change
        {
            GDEBuffData bp = new GDEBuffData("B_SF_Destroyer_p_1_1");

            if (this.attackMode == 1)
            {
                bp = new GDEBuffData("B_SF_Destroyer_p_1_1");

                //iconpath = bp.Icon_Path;
            }
            else if (this.attackMode == 2)
            {
                bp = new GDEBuffData("B_SF_Destroyer_p_1_2");

                //iconpath = bp.Icon_Path;
            }
            else if (this.attackMode == 3)
            {
                bp = new GDEBuffData("B_SF_Destroyer_p_1_3");

                //iconpath = bp.Icon_Path;
            }
            else if (this.attackMode == 4)
            {
                bp = new GDEBuffData("B_SF_Destroyer_p_1_4");
                
                //iconpath = bp.Icon_Path;
            }

            string modedesc = bp.Description;
            if(this.attackMode==4)
            {
                return modedesc;
            }
            else
            {
                return base.DescExtended().Replace("&a", modedesc);//base.Usestate_L.Info.Name);//.ToString());

            }
        }
        public int AttackMode
        {
            get
            {
                return this.attackMode;
            }
        }
        public void SetAttackMode(int mode)//模式change
        {

            this.attackMode = mode;
            string iconpath;
            GDEBuffData bp = new GDEBuffData("B_SF_Destroyer_p_1_1");

            if (mode==1)
            {
                bp = new GDEBuffData("B_SF_Destroyer_p_1_1");

                //iconpath = bp.Icon_Path;
            }
            else if(mode==2)
            {
                bp = new GDEBuffData("B_SF_Destroyer_p_1_2");

                //iconpath = bp.Icon_Path;
            }
            else if (mode == 3)
            {
                bp = new GDEBuffData("B_SF_Destroyer_p_1_3");

                //iconpath = bp.Icon_Path;
            }
            else if (mode == 4)
            {
                bp = new GDEBuffData("B_SF_Destroyer_p_1_4");
                goto Point_bcl_p_1_1;
                //iconpath = bp.Icon_Path;
            }
            else
            {
                this.attackMode = 1;
                bp = new GDEBuffData("B_SF_Destroyer_p_1_1");
                //iconpath = bp.Icon_Path;
            }
            this.oldMode = this.attackMode;

            Point_bcl_p_1_1:

            this.BuffData.Name = bp.Name;
            iconpath = bp.Icon_Path;

            if (!string.IsNullOrEmpty(iconpath))
            {
                this.BuffIconScript.Icon.sprite = AddressableLoadManager.LoadAsyncCompletion<Sprite>(iconpath, AddressableLoadManager.ManageType.Stage);
            }


            foreach (IP_AttackModeChange ip_patch in this.BChar.IReturn<IP_AttackModeChange>())
            {
                //Debug.Log("IP测试1");
                bool flag = ip_patch != null;
                if (flag)
                {
                    ip_patch.AttackModeChange(this.BChar, mode);
                }
            }

            this.BuffData.Icon_Path = iconpath;
            this.BChar.BuffAdd(this.BuffData.Key, this.BChar, false, 0, false, -1, false);
        }



        public override void TurnUpdate()
        {
            this.changed = false;
        }
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            this.SetAttackMode(1);
            
            Button button = this.BuffIcon.AddComponent<Button>();
            button.onClick.AddListener(new UnityAction(this.Go));
            
        }

        public void Go()//模式change
        {
            //Debug.Log(this.BuffData.Icon_Path);
            foreach(Buff buff in this.BChar.Buffs)
            {
                if(!buff.IsHide)
                {
                    //Debug.Log(buff.BuffData.Name);
                }
            }

            if (!changed && !BattleSystem.instance.DelayWait && this.attackMode != 4)// && BattleSystem.instance.Particles.Count == 0 && BattleSystem.instance.ActWindow.On)
            {
                //this.changed = true;
                List<Skill> list = new List<Skill>();
                list.Add(Skill.TempSkill("S_SF_Destroyer_p_1_1", this.BChar, BattleSystem.instance.AllyTeam));
                list.Add(Skill.TempSkill("S_SF_Destroyer_p_1_2", this.BChar, BattleSystem.instance.AllyTeam));
                list.Add(Skill.TempSkill("S_SF_Destroyer_p_1_3", this.BChar, BattleSystem.instance.AllyTeam));

                BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(this.Del), ScriptLocalization.System_SkillSelect.EffectSelect, true, false, true, false, false));

            }

        }

        public void Del(SkillButton Mybutton)//模式change
        {
            this.changed = true;
            if (Mybutton.Myskill.MySkill.KeyID == "S_SF_Destroyer_p_1_1")
            {
                this.SetAttackMode(1);

            }
            else if (Mybutton.Myskill.MySkill.KeyID == "S_SF_Destroyer_p_1_2")
            {
                this.SetAttackMode(2);

            }
            else if (Mybutton.Myskill.MySkill.KeyID == "S_SF_Destroyer_p_1_3")
            {
                this.SetAttackMode(3);

            }
            else
            {
                this.SetAttackMode(1);

            }
        }

        public void ParticleOut_Before(Skill SkillD, List<BattleChar> Targets)//模式1234
        {
            //Debug.Log("检查点:ParticleOut_Before_1");
            bool flag = SkillD.TargetTypeKey == "all_enemy" || SkillD.TargetTypeKey == "all_ally" || SkillD.TargetTypeKey == "all" || SkillD.TargetTypeKey == "all_allyorenemy" || SkillD.TargetTypeKey == "all_other";
            if (SkillD.Master == this.BChar && (!flag) && SkillD.IsDamage && SkillD.AllExtendeds.Find(a => a is Sex_des_p_1_0) == null)
            {
                //Debug.Log("检查点:ParticleOut_Before_2");
                if (this.attackMode == 1)
                {
                    //Targets.AddRange(Bcl_des_p_1.GetSideTarget(Targets[0]));
                    List<BattleChar> plustarget = new List<BattleChar>();
                    foreach (BattleChar bc in Targets)
                    {
                        plustarget.AddRange(Bcl_des_p_1.GetSideTarget(bc));
                    }
                    List<BattleChar> plustarget2 = new List<BattleChar>();
                    foreach (BattleChar bc in plustarget)
                    {
                        if (plustarget2.Find(a => a == bc) == null && Targets.Find(a => a == bc) == null)
                        {
                            plustarget2.Add(bc);
                        }
                    }

                    if (SkillD.AllExtendeds.Find(a=>a is Sex_des_p_1_1 )==null)
                    {
                        Sex_des_p_1_1 sex=new Sex_des_p_1_1();

                        //sex.mainTarget = Targets[0];
                        sex.SetTargets(Targets,plustarget2);

                        SkillD.ExtendedAdd(sex);
                    }
                    else
                    {
                        Sex_des_p_1_1 sex = SkillD.AllExtendeds.Find(a => a is Sex_des_p_1_1) as Sex_des_p_1_1;

                        //sex.mainTarget = Targets[0];

                        sex.SetTargets(Targets, plustarget2);
                    }
                    for(int i = 0;i<SkillD.AllExtendeds.Count;i++)
                    {
                        if (SkillD.AllExtendeds[i] is Sex_des_p_1 && !(SkillD.AllExtendeds[i] is Sex_des_p_1_1)) { SkillD.AllExtendeds.RemoveAt(i); i--; }
                    }





                    Targets.AddRange(plustarget2);


                }
                else if(this.attackMode == 2)
                {
                    /*
                    foreach(BattleChar bc in Targets[0].MyTeam.AliveChars)
                    {
                        if (bc != Targets[0])
                        {
                            Targets.Add(bc);
                        }
                    }
                    */
                    if (SkillD.AllExtendeds.Find(a => a is Sex_des_p_1_2) == null)
                    {
                        Sex_des_p_1_2 sex = new Sex_des_p_1_2();
                        SkillD.ExtendedAdd(sex);
                    }

                    for (int i = 0; i < SkillD.AllExtendeds.Count; i++)
                    {
                        if (SkillD.AllExtendeds[i] is Sex_des_p_1 && !(SkillD.AllExtendeds[i] is Sex_des_p_1_2)) { SkillD.AllExtendeds.RemoveAt(i); i--; }
                    }

                    foreach (BattleChar bc in Targets[0].MyTeam.AliveChars)
                    {
                        if (Targets.Find(a=>a==bc)==null)
                        {
                            Targets.Add(bc);
                        }
                    }
                }
                else if (this.attackMode == 3)
                {
                    if (SkillD.AllExtendeds.Find(a => a is Sex_des_p_1_3) == null)
                    {
                        Sex_des_p_1_3 sex = new Sex_des_p_1_3();

                        //sex.mainTarget = Targets[0];
                        sex.SetTargets(Targets,new List<BattleChar>());

                        SkillD.ExtendedAdd(sex);
                    }
                    else
                    {
                        Sex_des_p_1_3 sex = SkillD.AllExtendeds.Find(a => a is Sex_des_p_1_3) as Sex_des_p_1_3;

                        //sex.mainTarget = Targets[0];

                        sex.SetTargets(Targets, new List<BattleChar>());
                    }
                    for (int i = 0; i < SkillD.AllExtendeds.Count; i++)
                    {
                        if (SkillD.AllExtendeds[i] is Sex_des_p_1 && !(SkillD.AllExtendeds[i] is Sex_des_p_1_3)) { SkillD.AllExtendeds.RemoveAt(i); i--; }
                    }
                }
                else if(this.attackMode == 4)
                {
                    /*
                    foreach (BattleChar bc in Targets[0].MyTeam.AliveChars)
                    {
                        if (bc != Targets[0])
                        {
                            Targets.Add(bc);
                        }
                    }
                    */
                    List<BattleChar> plustargets2 = new List<BattleChar>();
                    foreach (BattleChar bc in Targets[0].MyTeam.AliveChars)
                    {
                        if (Targets.Find(a => a == bc) == null && plustargets2.Find(a => a == bc)==null)
                        {
                            plustargets2.Add(bc);
                        }
                    }


                    if (SkillD.AllExtendeds.Find(a => a is Sex_des_p_1_4) == null)
                    {
                        Sex_des_p_1_4 sex = new Sex_des_p_1_4();

                        //sex.mainTarget = Targets[0];
                        sex.SetTargets(Targets,plustargets2);

                        SkillD.ExtendedAdd(sex);
                    }
                    else
                    {
                        Sex_des_p_1_4 sex = SkillD.AllExtendeds.Find(a => a is Sex_des_p_1_4) as Sex_des_p_1_4;

                        //sex.mainTarget = Targets[0];

                        sex.SetTargets(Targets,plustargets2);
                    }

                    for (int i = 0; i < SkillD.AllExtendeds.Count; i++)
                    {
                        if (SkillD.AllExtendeds[i] is Sex_des_p_1 && !(SkillD.AllExtendeds[i] is Sex_des_p_1_4)) { SkillD.AllExtendeds.RemoveAt(i); i--; }
                    }

                    Targets.AddRange(plustargets2);
                }
            }
            else if (SkillD.Master == this.BChar && (flag ) && SkillD.IsDamage && this.attackMode == 4 && SkillD.AllExtendeds.Find(a => a is Sex_des_p_1_0) == null)
            {
                if (SkillD.AllExtendeds.Find(a => a is Sex_des_p_1_4) == null)
                {
                    Sex_des_p_1_4 sex = new Sex_des_p_1_4();
                    //sex.mainTarget = null;
                    sex.SetTargets(new List<BattleChar>(), new List<BattleChar>());
                    SkillD.ExtendedAdd(sex);
                }
                else
                {
                    Sex_des_p_1_4 sex = SkillD.AllExtendeds.Find(a => a is Sex_des_p_1_4) as Sex_des_p_1_4;
                    //sex.mainTarget = null;
                    sex.SetTargets(new List<BattleChar>(), new List<BattleChar>());
                }
                for (int i = 0; i < SkillD.AllExtendeds.Count; i++)
                {
                    if (SkillD.AllExtendeds[i] is Sex_des_p_1 && !(SkillD.AllExtendeds[i] is Sex_des_p_1_4)) { SkillD.AllExtendeds.RemoveAt(i); i--; }
                }
            }
        }

        public static List<BattleChar> GetSideTarget(BattleChar tar0)//模式1
        {
            //Debug.Log("检查点:GetSideTarget_1");
            if (tar0 is BattleEnemy)
            {
                int num = 0;
                List<BattleEnemy> list = (tar0 as BattleEnemy).EnemyPosNum(out num);
                List<BattleChar> list2 = new List<BattleChar>();
                if (num > 0)
                {
                    list2.Add(list[num - 1]);
                }
                if (list.Count > num + 1)
                {
                    list2.Add(list[num + 1]);
                }
                return list2;
            }
            else if(tar0 is BattleAlly)
            {
                int num = 0;
                for (int i = 0; i < BattleSystem.instance.AllyTeam.AliveChars.Count; i++)
                {
                    if (BattleSystem.instance.AllyTeam.AliveChars[i] == tar0)
                    {
                        num = i;
                    }
                }
                List<BattleAlly> allyList = BattleSystem.instance.AllyList;
                List<BattleChar> list = new List<BattleChar>();
                if (allyList.Count > num + 1)
                {
                    list.Add(allyList[num + 1]);
                }
                if (allyList.Count != 0)
                {
                    list.Add(allyList[num - 1]);
                }
                return list;
            }
            return null;
        }

        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)//模式3
        {

            if (this.attackMode==3 && addedbuff.BuffData.Key == "B_SF_Destroyer_p_2" && BuffTaker.BuffFind("B_SF_Destroyer_p_2", false) && BuffUser == this.BChar)
            {
                this.bp2 = BuffTaker.BuffReturn("B_SF_Destroyer_p_2", false);

                if (this.bp2.StackNum >= this.bp2.BuffData.MaxStack)
                {

                    //this.dis.BuffData.MaxStack++;
                    this.isb0 = true;
                    this.oldb0 = this.bp2.StackInfo[0];
                }

            }

        }
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)//模式3
        {
            if (this.isb0)
            {
                this.bp2.StackInfo.Insert(0, this.oldb0);
                this.bp2.BuffStatUpdate();
            }
            this.isb0 = false;
        }

        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if(View)
            {


                bool flag = SkillD.TargetTypeKey == "all_enemy" || SkillD.TargetTypeKey == "all_ally" || SkillD.TargetTypeKey == "all" || SkillD.TargetTypeKey == "all_allyorenemy" || SkillD.TargetTypeKey == "all_other";
                if (SkillD.Master == this.BChar && (!flag) && SkillD.IsDamage && SkillD.AllExtendeds.Find(a => a is Sex_des_p_1_0) == null)
                {
                    int dmg = Damage;
                    if (this.attackMode == 1)
                    {
                        dmg = Math.Max(1, (int)(0.7 * Damage));
                    }
                    else if (this.attackMode == 2)
                    {
                        dmg = Math.Max(1, (int)(0.4 * Damage));
                    }
                    return dmg;
                }


            }
            return Damage;
        }

        public override void FixedUpdate()//模式4
        {
            base.FixedUpdate();
            this.frame++;
            if(this.attackMode==4 && (this.frame<0||this.frame>=10))
            {
                this.frame = 0;
                if(!this.exitingGaia && this.BChar.MyTeam.Skills.Find((Skill a) => a.Master == this.BChar && a.MySkill.KeyID== "S_SF_Destroyer_12_1") == null)
                {
                    this.exitingGaia = true;
                    BattleSystem.DelayInput(this.ExitGaia());

                    return;
                }

                if (!this.DestroyBuff)
                {
                    if (BattleSystem.instance != null)
                    {
                        for (int i = 0; i < BattleSystem.instance.AllyTeam.Skills.Count; i++)
                        {
                            if (BattleSystem.instance.AllyTeam.Skills[i].AllExtendeds.Find(a=>a is Sex_des_p_1_4_1) == null && BattleSystem.instance.AllyTeam.Skills[i].Master==this.BChar)
                            {
                                Sex_des_p_1_4_1 test = new Sex_des_p_1_4_1();
                                test.MainBuff = this;
                                BattleSystem.instance.AllyTeam.Skills[i].ExtendedAdd(test);
                                //(BattleSystem.instance.AllyTeam.Skills[i].ExtendedAdd(Skill_Extended.ExtendedClone(this.BChar, this.LucySkillExBuff, BattleSystem.instance.AllyTeam.Skills[i])) as BuffSkillExHand).MainBuff = this;
                            }
                        }

                        if(BattleSystem.instance.AllyTeam.Skills.Find(a=>a.Master==this.BChar && a.MySkill.KeyID!= "S_SF_Destroyer_12_1") == null && !BattleSystem.instance.DelayWait && BattleSystem.instance.Particles.Count == 0 && BattleSystem.instance.ActWindow.On)
                        {
                            if(this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == this.BChar) != null)
                            {
                                this.drawing = true;
                                this.BChar.MyTeam.ForceDraw(this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == this.BChar), new BattleTeam.DrawInput(this.DrawAfter));
                            }
                            /*
                            else if(this.BChar.MyTeam.Skills_UsedDeck.Find((Skill a) => a.Master == this.BChar) != null)
                            {
                                this.drawing = true;
                                this.BChar.MyTeam.ForceDraw(this.BChar.MyTeam.Skills_Deck.Find((Skill a) => a.Master == this.BChar), new BattleTeam.DrawInput(this.DrawAfter));
                            }
                            */
                        }
                    }
                }


            }
        }
        public int frame = 0;
        public void DrawAfter(Skill skill)
        {
            this.drawing = false;
        }
        public IEnumerator ExitGaia()
        {
            yield return new WaitForFixedUpdate();
            if (this.BChar.MyTeam.Skills.Find((Skill a) => a.Master == this.BChar && a.MySkill.KeyID == "S_SF_Destroyer_12_1") == null)
            {
                this.SetAttackMode(this.oldMode);

            }
            this.exitingGaia = false;
            yield break;
        }


        private Buff bp2;
        private StackBuff oldb0 = null;
        private bool isb0;

        private int attackMode = 0;
        private int oldMode = 0;
        private bool changed = false;
        private bool exitingGaia = false;

        private bool drawing = false;
    }
    
    public class Sex_des_p_1: Sex_ImproveClone
    {
        public void SetTargets(List<BattleChar> Targets, List<BattleChar> Targets2)
        {
            this.mainTargets.Clear();
            this.mainTargets.AddRange(Targets);
            this.ashTargets.Clear();
            this.ashTargets.AddRange(Targets);
            this.plusTargets.Clear();
            this.plusTargets.AddRange(Targets2);
        }
        //public BattleChar mainTarget = new BattleChar();
        public List<BattleChar> mainTargets = new List<BattleChar>();
        public List<BattleChar> plusTargets = new List<BattleChar>();
        public List<BattleChar> ashTargets=new List<BattleChar>();
    }
    public class Sex_des_p_1_0 : Sex_des_p_1
    {
    }
        
    public class Sex_des_p_1_1:Sex_des_p_1,IP_DamageChange,IP_AshPlus
    {

        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            int dmg = Damage;
            if(Damage>0)
            {
                if (SkillD == this.MySkill && this.mainTargets.Find(a=>a==Target)!=null)//Target == this.mainTarget)
                {
                    //Debug.Log("主要目标");
                    dmg = Math.Max(1,(int)(0.7 * Damage));

                    this.mainTargets.Remove(Target);
                }
                else if (SkillD == this.MySkill && this.plusTargets.Find(a => a == Target) != null)
                {
                    //Debug.Log("次要目标");
                    dmg = Math.Max(1, (int)(0.35 * Damage));
                    this.plusTargets.Remove(Target);
                }
            }
            //Debug.Log("主要目标为："+this.mainTarget.Info.Name);

            return dmg;
        }

        public int AshPlus(Skill skill, BattleChar Target)
        {
            if (skill==this.MySkill && this.ashTargets.Find(a => a == Target) != null)
            {
                //return 1;
            }
            return 0;
        }
        
    }
    public class Sex_des_p_1_2 : Sex_des_p_1,IP_DamageChange,IP_DamageChange_sumoperation
    {
        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (SkillD ==this.MySkill && (Target as BattleEnemy).istaunt)
            {
                Cri = true;
            }
        }
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            int dmg = Damage;
            if (dmg>0 && SkillD == this.MySkill)
            {
                dmg = Math.Max(1, (int)(0.4 * Damage));

            }

            return dmg;
        }
    }
    public class Sex_des_p_1_3 : Sex_des_p_1,IP_AshPlus//,IP_SkillUse_Target
    {
        public override void Init()
        {
            base.Init();
            this.Traking = true;
        }
        public int AshPlus(Skill skill, BattleChar Target)
        {
            if (skill == this.MySkill )
            {
                //BattleSystem.DelayInputAfter(this.Burst(Target));

                //return 1;
            }
            return 0;
        }
        /*立即触发一次痛苦伤害的代码
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if(SP.SkillData==this.MySkill) 
            {
                BattleSystem.DelayInput(this.Burst(hit));

            }
        }
        public IEnumerator Burst(BattleChar Target)
        {
            if (Target.BuffFind("B_SF_Destroyer_p_2", false))
            {
                Bcl_des_p_2 buff = Target.BuffReturn("B_SF_Destroyer_p_2", false) as Bcl_des_p_2;
                BattleSystem.DelayInput(buff.Burst());
            }
            yield return null;
            yield break;
        }
        */
    }
    public class Sex_des_p_1_4 : Sex_des_p_1, IP_DamageChange, IP_AshPlus, IP_DamageChange_sumoperation
    {
        public void DamageChange_sumoperation(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if (SkillD == this.MySkill && this.ashTargets.Count <= 0)
            {
                Cri = true;
            }
        }

        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            int dmg = Damage;
            if(dmg>0 && this.ashTargets.Count>0)
            {
                if (SkillD == this.MySkill && this.mainTargets.Find(a => a == Target) != null)
                {
                    //Debug.Log("主要目标");
                    dmg = Damage;

                    this.mainTargets.Remove(Target);
                }
                else if (SkillD == this.MySkill && this.plusTargets.Find(a => a == Target) != null)
                {
                    //Debug.Log("次要目标");
                    dmg = Math.Max(1, (int)(0.5 * Damage));
                    this.plusTargets.Remove(Target);
                }
            }
            //Debug.Log("主要目标为："+this.mainTarget.Info.Name);
            return dmg;
        }

        public int AshPlus(Skill skill, BattleChar Target)
        {
            if (skill == this.MySkill && this.ashTargets.Find(a => a == Target) != null)
            {
                //return 1;
            }
            return 0;
        }
        //public BattleChar mainTarget = new BattleChar();
    }

    public class Sex_des_p_1_4_1: Sex_ImproveClone
    {
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (this.MainBuff == null || this.MainBuff.DestroyBuff || this.MainBuff.AttackMode!=4)
            {
                this.SelfDestroy();
            }
        }

        // Token: 0x06000821 RID: 2081 RVA: 0x00069EE2 File Offset: 0x000680E2
        public override void Init()
        {
            base.Init();
            this.NoClone = true;

            //this.Disposable = true;
        }

        // Token: 0x04000B7E RID: 2942
        public Bcl_des_p_1 MainBuff;
    }


    public class Sex_des_p_2_fake : BuffSkillExHand
    {

    }
        public class Sex_des_p_2 : Sex_ImproveClone, IP_SkillUse_Target
    {
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            if(SP.SkillData == this.MySkill)
            {
                int buffstack = 1;
                //Debug.Log("Sex_des_p_2命中");
                foreach (IP_AshPlus ip_patch in SP.SkillData.IReturn<IP_AshPlus>())
                {
                    //Debug.Log("IP测试1");
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        buffstack += ip_patch.AshPlus(SP.SkillData, hit);
                    }
                }
                for (int i = 0; i < buffstack; i++)
                {
                    hit.BuffAdd("B_SF_Destroyer_p_2", this.BChar, false, 0, false, -1, false);
                }

            }
        }

        /*
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            Bcl_des_p_2_S buff=this.MainBuff as Bcl_des_p_2_S;
            BattleSystem.DelayInputAfter(buff.CheckAndDestroy());
        }
        */
        // Token: 0x060027FC RID: 10236 RVA: 0x0010514E File Offset: 0x0010334E
        /*
        public IEnumerator Del()
        {
            this.MainBuff.SelfStackDestroy();
            yield break;
        }
        */
    }
    public interface IP_AshPlus
    {
        // Token: 0x06000001 RID: 1
        int AshPlus(Skill skill,BattleChar Target);
    }
    public class Bcl_des_p_2 : Buff, IP_BuffUpdate//,IP_BuffAddAfter
    {
        public override void BuffStat()
        {
            base.BuffStat();
            //this.PlusStat.def = -4 * this.StackNum;
            //this.PlusPerStat.Damage=-4 * this.StackNum;
            //this.PlusStat.RES_DOT=-4 * this.StackNum;
            //this.PlusStat.RES_DOT = -15;

            //this.CheckDmg();
        }

        public void BuffUpdate(Buff MyBuff)
        {
            //this.CheckDmg();
        }
        public void CheckDmg()
        {
            if (BattleSystem.instance != null)
            {
                GDESkillEffectData gde = new GDESkillEffectData("SE_SF_Destroyer_p_dot_bac");
                bool b11 = false;
                foreach (IP_CheckPlusFire ip_patch in BattleSystem.instance.IReturn<IP_CheckPlusFire>())
                {
                    //Debug.Log("IP测试1");
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        b11 = b11 || ip_patch.CheckPlusFire();
                    }
                }
                if (b11)
                {
                    this.BuffData.Tick.DMG_Per = (int)(2 * gde.DMG_Per);
                }
                else
                {
                    this.BuffData.Tick.DMG_Per = gde.DMG_Per;
                }
            }
        }
        /*立即触发一次痛苦伤害的代码
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if(addedbuff==this)
            {
                this.canBurst = true;
            }
        }
        public IEnumerator Burst()
        {
            if(this.canBurst)
            {
                this.canBurst = false;
                
                int num = this.DotDMGView();
                this.BChar.Damage(this.Usestate_L, num, false, true, false, 0, false, false, false);
                base.TurnUpdate();

            }

            yield return null;
            yield break;
        }
        
        public bool canBurst=false;
        */
    }

    public class Bcl_des_p_2_S : Buff//,IP_SkillUseEffect_Before//,IP_BuffAddAfter
    {
        
        public override void FixedUpdate()
        {
            if (!this.DestroyBuff)
            {
                if (BattleSystem.instance != null )
                {
                    for (int i = 0; i < BattleSystem.instance.AllyTeam.Skills.Count; i++)
                    {
                        
                        if (BattleSystem.instance.AllyTeam.Skills[i].ExtendedFind_DataName("Ex_SF_Destroyer_p_2_fake") == null && this.CanSkillBuffAdd(BattleSystem.instance.AllyTeam.Skills[i], i))
                        {
                            Skill_Extended test = Skill_Extended.DataToExtended("Ex_SF_Destroyer_p_2_fake");
                            (test as BuffSkillExHand).MainBuff = this;
                            BattleSystem.instance.AllyTeam.Skills[i].ExtendedAdd(test);
                        }
                        /*
                        if (BattleSystem.instance.AllyTeam.Skills[i].AllExtendeds.Find(a=>a is Sex_des_p_2) == null && this.CanSkillBuffAdd(BattleSystem.instance.AllyTeam.Skills[i], i))
                        {
                            Sex_des_p_2 test = new Sex_des_p_2();
                            (test as BuffSkillExHand).MainBuff = this;
                            BattleSystem.instance.AllyTeam.Skills[i].ExtendedAdd(test);
                            //(BattleSystem.instance.AllyTeam.Skills[i].ExtendedAdd(Skill_Extended.ExtendedClone(this.BChar, this.LucySkillExBuff, BattleSystem.instance.AllyTeam.Skills[i])) as BuffSkillExHand).MainBuff = this;
                        }
                        */

                    }
                }
                base.FixedUpdate();
            }
        }
        
        /*
        public void SkillUseEffect_Before(Skill SkillD, List<BattleChar> Targets)
        {


            if ( SkillD.IsDamage && !Targets[0].Info.Ally && SkillD.AllExtendeds.Find(a => a is Sex_des_p_2) == null)
            {
                //this.turnfired = true;
                Sex_des_p_2 sex = new Sex_des_p_2();
                SkillD.ExtendedAdd(sex);

                BattleSystem.DelayInputAfter(this.CheckAndDestroy());
            }
        }
        */
        public override bool CanSkillBuffAdd(Skill AddedSkill, int Index)
        {
            return AddedSkill.Master==this.BChar && AddedSkill.IsDamage;
        }
        /*
        public IEnumerator CheckAndDestroy()
        {
            bool canfire = false;
            foreach (IP_CheckPlusFire ip_patch in this.BChar.IReturn<IP_CheckPlusFire>())
            {
                //Debug.Log("IP测试1");
                bool flag = ip_patch != null;
                if (flag)
                {
                    canfire = canfire || ip_patch.CheckPlusFire(this.BChar);
                }
            }
            if(!canfire)
            {
                this.SelfStackDestroy();
            }
            yield break;
        }
        */
    }



    public class Sex_des_1 : Sex_ImproveClone
    {
        public override void Init()
        {
            base.Init();
            this.PlusSkillStat.Penetration = 50;
        }
    }
    public class Bcl_des_1 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.def = -15;
            this.PlusStat.RES_DOT = -15;
            this.PlusStat.RES_CC = -10;
        }
    }

        public class Sex_des_2 : Sex_ImproveClone, IP_ParticleOut_Before
    {
        public void ParticleOut_Before(Skill SkillD, List<BattleChar> Targets)
        {
            if(SkillD==this.MySkill && !this.isplus)
            {

                this.targets=new List<BattleChar>();
                this.targets.AddRange(Targets);
                //Debug.Log("test1-1:" + this.targets.Count);
                List<BattleChar> list = new List<BattleChar>();
                list.AddRange(this.targets);

                list = (from x in list
                        orderby x.HP
                        select x).ToList<BattleChar>();

                int tarhp = 0;
                tarhp = list[list.Count - 1].HP;
                for (int i = 0; i < this.targets.Count; i++)
                {
                    if (this.targets[i].HP < tarhp)
                    {
                        this.targets.RemoveAt(i);
                        i--;
                    }
                }
                //Debug.Log("test1-2:" + this.targets.Count);


                //Targets.AddRange(list);

                //Debug.Log("Targets:"+Targets.Count);
                //Targets.AddRange(list);


                this.isplus = true;

                Skill skill = this.MySkill.CloneSkill(true, null, null, true);
                skill.PlusHit = true;
                skill.UseOtherParticle_Path = "Particle/Enemy/Pharos_witch";
                foreach(Skill_Extended sex in skill.AllExtendeds)
                {
                    if(sex is Sex_des_2 sex2)
                    {
                        sex2.targets= new List<BattleChar>();
                        sex2.targets.AddRange(this.targets);
                    }
                }

                this.isplus = false;
                BattleSystem.DelayInput(this.Plushit(skill));
                
            }
            else if(SkillD==this.MySkill && this.isplus)
            {
                //Debug.Log("test2:" + this.targets.Count);
                Targets.RemoveAll(a => !this.targets.Contains(a));
            }

        }

        public IEnumerator Plushit(Skill skill)
        {
            this.BChar.ParticleOut(skill,this.targets);
            yield return null;
            yield break;
        }

        public bool isplus = false;
        public List<BattleChar> targets = new List<BattleChar>();
    }

    public class Sex_des_3 : Sex_ImproveClone,IP_AttackModeChange
    {
        public override void HandInit()
        {
            base.HandInit();
            this.Origin = new GDESkillData(this.MySkill.MySkill.KeyID).Target;

            if (this.BChar.BuffFind("B_SF_Destroyer_p_1", false) && (this.BChar.BuffReturn("B_SF_Destroyer_p_1", false)as Bcl_des_p_1).AttackMode==3)
            {
                

                    this.MySkill.MySkill.Target = new GDEs_targettypeData(GDEItemKeys.s_targettype_enemy);
                this.IgnoreTaunt = true;
            }
            else
            {
                this.MySkill.MySkill.Target = this.Origin;
                this.IgnoreTaunt = false;
            }
        }

        public void AttackModeChange(BattleChar bc, int mode)
        {
            this.Origin = new GDESkillData(this.MySkill.MySkill.KeyID).Target;

            if (this.BChar.BuffFind("B_SF_Destroyer_p_1", false) && (this.BChar.BuffReturn("B_SF_Destroyer_p_1", false) as Bcl_des_p_1).AttackMode == 3)
            {


                this.MySkill.MySkill.Target = new GDEs_targettypeData(GDEItemKeys.s_targettype_enemy);
                this.IgnoreTaunt= true;
            }
            else
            {
                this.MySkill.MySkill.Target = this.Origin;
                this.IgnoreTaunt = false;
            }
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            int looptrun = 2;
            Skill skill = this.MySkill.CloneSkill(true, null, null, true);
            //skill.isExcept = true;
            skill.FreeUse = true;
            skill.PlusHit = true;

            for (int i = 1; i <= looptrun; i++)
            {
                if (BattleSystem.instance.EnemyTeam.AliveChars.Count >= 1)
                {
                    if (this.BChar.BuffFind("B_SF_Destroyer_p_1", false) && (this.BChar.BuffReturn("B_SF_Destroyer_p_1", false) as Bcl_des_p_1).AttackMode == 3)
                    {
                        BattleSystem.DelayInput(this.Plushit(Targets[0], skill,false));

                    }
                    else
                    {
                        BattleSystem.DelayInput(this.Plushit(Targets[0], skill,true));
                    }
                }
            }
        }

        public IEnumerator Plushit(BattleChar tar0, Skill skill,bool ran)//追加射击
        {
            yield return new WaitForSecondsRealtime(0.3f);
            if(!ran && !tar0.IsDead) 
            {
                this.BChar.ParticleOut(this.MySkill,skill, tar0);
            }
            else 
            {
                this.BChar.ParticleOut(this.MySkill,skill, this.BChar.BattleInfo.EnemyList.Random(this.BChar.GetRandomClass().Main));
            }

            yield break;
        }

        private GDEs_targettypeData Origin;
    }


    public class Sex_des_4 : Sex_ImproveClone//, IP_TargetedAlly
    {
        
        /*
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            this.MySkill.UseOtherParticle_Path = "Particle/Buff";
            base.SkillUseSingle(SkillD, Targets);
        }
        
        public override void Init()
        {
            base.Init();
            //this.CountingExtedned = true;
            //this.IsDamage = true;
        }
        */
        /*
        public IEnumerator Targeted(BattleChar Attacker, List<BattleChar> SaveTargets, Skill skill)
        {
            CastingSkill castskill = new CastingSkill();
            if (BattleSystem.instance.CastSkills.Find(a => a.skill == this.MySkill) != null)// || BattleSystem.instance.SaveSkill.Find(a => a.skill == this.MySkill) != null)
            {
                castskill = BattleSystem.instance.CastSkills.Find(a => a.skill == this.MySkill);
            }
            else if (BattleSystem.instance.SaveSkill.Find(a => a.skill == this.MySkill) != null)
            {
                castskill = BattleSystem.instance.SaveSkill.Find(a => a.skill == this.MySkill);
            }


            if (castskill != null && castskill.Target == Attacker)
            {
                yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyCastingSkillUse(castskill, false));
                BattleSystem.instance.CastSkills.Remove(castskill);
                BattleSystem.instance.SaveSkill.Remove(castskill);

                if (skill.AllExtendeds.Find(a => a is EX_des_4_1) == null)
                {
                    EX_des_4_1 ex4 = new EX_des_4_1();
                    //ex4.user = this.Usestate_L;
                    skill.ExtendedAdd(ex4);
                }
                yield return new WaitForSeconds(0.2f);
            }

            yield break;
        }
        */
    }
    public class Bcl_des_4_T : Buff, IP_DebuffResist, IP_TargetedAllyForAll,IP_TurnEnd,IP_BuffAdd,IP_BuffAddAfter
    {
        
        public override string DescExtended()
        {



            return base.DescExtended().Replace("&a", ((int)(this.Usestate_L.GetStat.atk*0.8)).ToString()).Replace("&b", ((int)(this.Usestate_L.GetStat.atk * 0.5)).ToString());
        }

        public override void FixedUpdate()//mark
        {
            foreach (StackBuff stack in this.StackInfo)
            {

                if (!stack.UseState.BuffFind("B_SF_Destroyer_4_S", false))
                {
                    stack.UseState.BuffAdd("B_SF_Destroyer_4_S", stack.UseState, false, 0, false, -1, false);
                }
            }
        }
        public override void TurnUpdate()
        {

        }
        public void TurnEnd()
        {
            //Debug.Log("解除触发");
            this.SelfDestroy();
        }
        public void Resist()
        {
            //Debug.Log("抵抗触发");
            this.ActiveBomb(this.Usestate_L);
        }
        public override void SelfdestroyPlus()
        {
            this.ActiveBomb(this.Usestate_L);
        }
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {
            if (BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key)
            {
                //Debug.Log("施加触发");

                this.ActiveBomb(this.Usestate_L);
            }
        }
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key && this.actived)
            {
                this.actived = false;
            }
        }
        public IEnumerator TargetedAllyForAll(BattleChar Attacker, List<BattleChar> SaveTargets, Skill skill)
        {
            if( Attacker==this.BChar && skill.IsDamage)
            {
                if(skill.AllExtendeds.Find(a=>a is EX_des_4_1)==null)
                {
                    EX_des_4_1 ex4 = new EX_des_4_1();
                    ex4.user = this.Usestate_L;
                    skill.ExtendedAdd(ex4);
                }
                this.SelfDestroy();
                yield return new WaitForSeconds(0.2f);
                //Debug.Log("攻击触发");
                

            }

            yield break;
        }
        public void ActiveBomb(BattleChar user)
        {
            if(!this.actived)
            {
                this.actived = true;
                //Skill skill = new Skill();
                //skill.MyTeam = user.MyTeam;
                //GDESkillData gde=(new GDESkillData("S_SF_Destroyer_4")).ShallowClone();
                //gde.Effect_Target = (new GDESkillEffectData("SE_SF_Destroyer_4_A")).ShallowClone();
                //skill.Init(gde,user,user.MyTeam);
               

                //Skill skill2= skill.CloneSkill(false, null, null, false);
                Skill skill2 = Skill.TempSkill("S_SF_Destroyer_4_1", user, user.MyTeam);
                //skill2.FreeUse = true;

                user.ParticleOut(skill2, this.BChar);
            }
        }
        public bool actived = false;
        
    }
        
    public class Bcl_des_4_S : Buff, IP_TargetedAlly

    {
        
            public IEnumerator Targeted(BattleChar Attacker, List<BattleChar> SaveTargets, Skill skill)
        {
            foreach (IP_TargetedAllyForAll ip_patch in BattleSystem.instance.IReturn<IP_TargetedAllyForAll>())
            {
                if (ip_patch != null)
                {
                    yield return ip_patch.TargetedAllyForAll(Attacker, SaveTargets, skill);
                }
            }
            yield return null;
            yield break;
        }
        
    }

    public class EX_des_4_1 : Sex_ImproveClone
    {
        public override void Init()
        {
            this.PlusSkillStat.Weak = true;
            //this.SkillBasePlus.Target_BaseDMG = -(int)Math.Ceiling(Misc.PerToNum((float)this.MySkill.TargetDamage, 50f));
            this.SkillBasePlus.Target_BaseDMG = -(int)(this.user.GetStat.atk * 0.5);


        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BattleSystem.DelayInputAfter(this.WeakEnd());
        }
        public IEnumerator WeakEnd()
        {
            this.SelfDestroy();
            yield return null;
            yield break;
        }
        public BattleChar user=new BattleChar();
    }
    public class Sex_des_5 : Sex_ImproveClone,IP_ParticleOut_After
    {
        public IEnumerator ParticleOut_After(Skill SkillD, List<BattleChar> Targets)
        {
            if(SkillD==this.MySkill)
            {
                foreach (BattleChar target in Targets)
                {
                    foreach (Buff buff in target.Buffs)
                    {
                        if (!buff.IsHide && buff.BuffData.Debuff && buff.BuffData.BuffTag.Key== GDEItemKeys.BuffTag_DOT)
                        {
                            foreach (StackBuff sta in buff.StackInfo)
                            {
                                //Debug.Log("增加前：" + sta.RemainTime);
                                if (buff.BuffData.LifeTime != 0f)//  && sta.RemainTime<buff.BuffData.LifeTime)
                                {
                                    sta.RemainTime+=2;
                                    //Debug.Log("增加");
                                }
                                //Debug.Log("增加后：" + sta.RemainTime);
                            }
                        }
                    }
                }

            }
            yield return null ;
            yield break;
        }
    }

    public interface IP_TargetedAllyForAll
    {
        // Token: 0x060025F8 RID: 9720
        IEnumerator TargetedAllyForAll(BattleChar Attacker, List<BattleChar> SaveTargets, Skill skill);
    }

    public class Sex_des_6 : Sex_ImproveClone
    {

    }
    public class Bcl_des_6 : Buff,IP_DamageTakeChange_sumoperation
    {
        public override string DescExtended()
        {

            return base.DescExtended().Replace("&a", ((int)(this.Usestate_L.GetStat.atk * 0.4)).ToString());
        }
        public void DamageTakeChange_sumoperation(BattleChar Hit, BattleChar User, int Dmg, bool Cri, ref int PlusDmg, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {
            if(Hit==this.BChar && Dmg>0 && !NODEF)
            {
                PlusDmg = (int)(0.4 * this.Usestate_L.GetStat.atk);
            }
        }
    }

    public class Sex_des_7 : Sex_ImproveClone
    {

    }
    public class Bcl_des_7 : Buff
    {

        public override void TurnUpdate()
        {
            base.TurnUpdate();
            BattleSystem.DelayInput(this.Spread());

        }
        public IEnumerator Spread()
        {
            yield return null;
            List<BattleChar> listside = new List<BattleChar>();
            listside.AddRange(Bcl_des_p_1.GetSideTarget(this.BChar));

            List<Buff> listbuff = new List<Buff>();
            listbuff.AddRange(this.BChar.GetBuffs(BattleChar.GETBUFFTYPE.DOT, false, false));
            foreach (Buff buff in listbuff)
            {
                foreach (BattleChar sideChar in listside)
                {
                    if (!sideChar.BuffFind(buff.BuffData.Key, false) || sideChar.BuffReturn(buff.BuffData.Key, false).StackNum < buff.StackNum)
                    {
                        int targetstack = 0;
                        if(sideChar.BuffFind(buff.BuffData.Key, false))
                        {
                            targetstack = sideChar.BuffReturn(buff.BuffData.Key, false).StackNum;
                        }
                        
                        //int stack = buff.StackInfo.Count - 1;
                        int num = Math.Min(buff.StackNum - targetstack, Math.Max(1, (int)(0.5 * buff.StackNum)));

                        for (int i = buff.StackNum - num; i < buff.StackNum; i++)
                        {
                            sideChar.BuffAdd(buff.BuffData.Key, buff.Usestate_L, false, 0, false, buff.StackInfo[i].RemainTime, false);
                        }
                        //sideChar.BuffAdd(buff.BuffData.Key, buff.Usestate_L, false, 0, false, buff.StackInfo[stack].RemainTime, false);
                    }
                }
            }

            yield break;
        }
    }


    public class Sex_des_8 : Sex_ImproveClone
    {
        
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BattleSystem.instance.AllyTeam.CharacterDraw(Targets[0], null);
        }
        
    }
    public class Bcl_des_8 : Buff, IP_SkillUse_Team
    {
        public override string DescExtended()
        {

            return base.DescExtended().Replace("&a", (this.lasttimes).ToString());
        }
        public override void Init()
        {
            base.Init();
            //this.lasttimes += 3;
        }

        public void SkillUseTeam(Skill skill)
        {
            //Debug.Log("Buff8_point1");
            if (skill .Master==this.BChar && !skill.FreeUse )//&& !this.MySkill.NotCount)// this.overload<this.BChar.Overload)
            {
                //Debug.Log("Buff8_point2");
                if (this.overload < this.BChar.Overload)
                {
                    //Debug.Log("Buff8_point3");
                    this.BChar.Overload = this.overload;
                    //this.lasttimes--;
                }


                //this.BChar.Overload = this.overload;
                //this.overload = 9;
                //Bcl_des_8 buff8= this.BChar.BuffReturn("B_SF_Destroyer_8", false) as Bcl_des_8;

            }
        }
        public override void FixedUpdate()
        {

            this.overload = this.BChar.Overload;
            /*
            if (this.lasttimes <= 0)
            {

                this.SelfDestroy();
            }
            */
            base.FixedUpdate();
        }
        /*
        public override void FixedUpdate()
        {
            if (!this.DestroyBuff)
            {
                if (BattleSystem.instance != null)
                {
                    for (int i = 0; i < BattleSystem.instance.AllyTeam.Skills.Count; i++)
                    {
                        if (BattleSystem.instance.AllyTeam.Skills[i].AllExtendeds.Find(a=>a is EX_des_8) == null && this.CanSkillBuffAdd(BattleSystem.instance.AllyTeam.Skills[i], i))
                        {
                            EX_des_8 test = new EX_des_8();
                            (test as BuffSkillExHand).MainBuff = this;
                            BattleSystem.instance.AllyTeam.Skills[i].ExtendedAdd(test);
                        }
                    }

                    if((this.BChar as BattleAlly).MyBasicSkill.buttonData.AllExtendeds.Find(a => a is EX_des_8) == null )
                    {
                        EX_des_8 test = new EX_des_8();
                        (test as BuffSkillExHand).MainBuff = this;
                        (this.BChar as BattleAlly).MyBasicSkill.buttonData.ExtendedAdd(test);
                    }
                }
                this.overload=this.BChar.Overload;
                if(this.lasttimes<=0)
                {
                    this.SelfDestroy();
                }
                base.FixedUpdate();
            }
        }

        public override bool CanSkillBuffAdd(Skill AddedSkill, int Index)
        {
            return AddedSkill.Master == this.BChar ;
        }
        */
        public int lasttimes=0;
        public int overload = 0;
    }
    /*
    public class EX_des_8 : BuffSkillExHand, IP_SkillUse_Team
    {

        public void SkillUseTeam(Skill skill)
        {
            if (skill==this.MySkill && this.BChar.BuffFind("B_SF_Destroyer_8",false) )//&& !this.MySkill.NotCount)// this.overload<this.BChar.Overload)
            {
                //Debug.Log("EX8_point2");

                Bcl_des_8 buff8 = this.MainBuff as Bcl_des_8;
                if(buff8.overload<this.BChar.Overload)
                {
                    //Debug.Log("EX8_point3");

                    this.BChar.Overload = buff8.overload;
                    buff8.lasttimes--;
                }


                //this.BChar.Overload = this.overload;
                //this.overload = 9;
                //Bcl_des_8 buff8= this.BChar.BuffReturn("B_SF_Destroyer_8", false) as Bcl_des_8;

            }
        }
    }
    */

    public class Sex_des_9 : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            if (Targets.Find(a=>a==this.BChar)!=null )//&& this.BChar.MyTeam.Skills.Find((Skill a) => a.Master == this.BChar && a != this.MySkill) == null)
            {
                new List<Skill>();
                List<GDESkillData> list = new List<GDESkillData>();
                List<GDESkillData> listorg = PlayData.GetCharacterSkillNoOverLap(this.BChar.Info, false, null);

                foreach (GDESkillData gdeskillData in listorg)
                {
                    if (gdeskillData.Effect_Target.DMG_Base >= 1 || gdeskillData.Effect_Target.DMG_Per >= 1)
                    {
                        list.Add(gdeskillData);
                    }
                }
                if (list.Count <= 0)
                {
                    foreach (GDESkillData gdeskillData in PlayData.ALLSKILLLIST)
                    {
                        if (gdeskillData.Category.Key == GDEItemKeys.SkillCategory_PublicSkill && (gdeskillData.Effect_Target.DMG_Base >= 1 || gdeskillData.Effect_Target.DMG_Per >= 1))
                        {
                            list.Add(gdeskillData);
                        }
                    }
                }
                Skill skill = Skill.TempSkill(list.Random(this.BChar.GetRandomClass().Main).KeyID, this.BChar, this.BChar.MyTeam);
                skill.isExcept = true;

                Sex_des_9_1 indmg = new Sex_des_9_1();
                indmg.PlusSkillPerStat.Damage = -(int)Misc.PerToNum((float)skill.MySkill.Effect_Target.DMG_Per, 35f);
                skill.ExtendedAdd(indmg);

                //Sex_des_9_1 indmg2 = new Sex_des_9_1();
                //skill.ExtendedAdd(indmg2);

                skill.NotCount = true;
                BattleSystem.instance.AllyTeam.Add(skill, true);
            }

            BattleSystem.DelayInput(this.Setb9tar(Targets[0]));
        }

        public IEnumerator Setb9tar(BattleChar battleChar)
        {
            yield return null;
            if(!this.BChar.BuffFind("B_SF_Destroyer_9",false))
            {
                this.BChar.BuffAdd("B_SF_Destroyer_9", this.BChar);
            }
            Bcl_des_9 b9 = this.BChar.BuffReturn("B_SF_Destroyer_9", false) as Bcl_des_9;
            b9.b9tar = battleChar;

            yield break;
        }
    }
    public class Bcl_des_9 : Buff, IP_SkillUseHand_Basic_Team, IP_SkillUseHand_Team
    {
        public override string DescExtended()
        {
            if(this.b9tar==null)
            {
                GDESkillKeywordData gde = new GDESkillKeywordData("KeyWord_SF_Destroyer_9");
                return base.DescExtended().Replace("&a", this.BChar.Info.Name).Replace("&b", this.lasttime.ToString()).Replace("&c", gde.Desc);
            }
            //return base.DescExtended().Replace("&a", this.Usestate_L.Info.Name).Replace("&b", this.lasttime.ToString());
            return base.DescExtended().Replace("&a", this.BChar.Info.Name).Replace("&b", this.lasttime.ToString()).Replace("&c", this.b9tar.Info.Name);
        }
        public override void Init()
        {
            base.Init();
            this.lasttime += 3;
        }

        public void SKillUseHand_Team(Skill skill)
        {
            this.AddSkill(skill);
        }
        public void SKillUseHand_Basic_Team(Skill skill)
        {
            this.AddSkill(skill);
        }

        public void AddSkill(Skill skill)
        {
            if(skill.Master==this.b9tar && !skill.FreeUse && skill.IsDamage && this.lasttime>0)
            {
                new List<Skill>();
                List<GDESkillData> list = new List<GDESkillData>();
                List<GDESkillData> listorg = PlayData.GetCharacterSkillNoOverLap(this.BChar.Info, false, null);

                foreach (GDESkillData gdeskillData in listorg)
                {
                    if (gdeskillData.Effect_Target.DMG_Base >= 1 || gdeskillData.Effect_Target.DMG_Per >= 1)
                    {
                        list.Add(gdeskillData);
                    }
                }
                if(list.Count <= 0)
                {
                    foreach (GDESkillData gdeskillData in PlayData.ALLSKILLLIST)
                    {
                        if (gdeskillData.Category.Key == GDEItemKeys.SkillCategory_PublicSkill && (gdeskillData.Effect_Target.DMG_Base >= 1 || gdeskillData.Effect_Target.DMG_Per >= 1))
                        {
                            list.Add(gdeskillData);
                        }
                    }
                }



                Skill skill2 = Skill.TempSkill(list.Random(this.BChar.GetRandomClass().Main).KeyID, this.BChar, this.BChar.MyTeam);
                skill2.isExcept = true;

                Sex_des_9_1 indmg = new Sex_des_9_1();
                indmg.PlusSkillPerStat.Damage= -(int)Misc.PerToNum((float)skill2.MySkill.Effect_Target.DMG_Per, 35f);
                skill2.ExtendedAdd(indmg);

                //Sex_des_9_1 indmg2 = new Sex_des_9_1();
                //skill.ExtendedAdd(indmg2);

                skill2.NotCount = true;
                BattleSystem.instance.AllyTeam.Add(skill2, true);


                this.lasttime--;
            }
            if (this.lasttime <= 0)
            {
                this.SelfDestroy();
            }
        }
        public int lasttime = 0;
        public BattleChar b9tar= new BattleChar();
    }
    public class Sex_des_9_1 : Sex_ImproveClone, IP_SkillUseHand_Team
    {
        public override void Init()
        {
            base.Init();
            this.SetAP();
        }
        public void SKillUseHand_Team(Skill skill)
        {
            if (skill != this.MySkill)
            {
                this.pflag = true;
                this.SetAP();

            }

        }
        public void SetAP()
        {
            this.APChange=!this.pflag ? -1 : 0;
        }
        private bool pflag=false;
    }

    public class Sex_des_10 : Sex_ImproveClone,IP_DamageChange//,IP_ParticleOut_After
    {
        /*
        public IEnumerator ParticleOut_After(Skill SkillD, List<BattleChar> Targets)
        {
            foreach(BattleChar bc in Targets)
            {
                BattleEnemy battleEnemy = bc as BattleEnemy;
                List<CastingSkill> list1 = new List<CastingSkill>();
                list1.AddRange(battleEnemy.SkillQueue);
                for (int i = 0; i < list1.Count; i++)
                {
                    if (list1[i].CastSpeed<=1)
                    {
                        BattleSystem.instance.ActWindow.CastingWaste(list1[i].skill);
                        BattleSystem.instance.EnemyCastSkills.Remove(list1[i]);
                        BattleSystem.instance.SaveSkill.Remove(list1[i]);
                    }
                }

            }
            yield break;
        }
        */
        public override void Init()
        {
            base.Init();
            //this.OnePassive = true;
            this.PlusSkillStat.Penetration = 100;
        }
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)

        {
            //Debug.Log("Skill_10_point_1");
            //Debug.Log(SkillD.MySkill.Name);
            //Debug.Log(this.MySkill.MySkill.Name);
            //Debug.Log("相同：" + (SkillD == this.MySkill));
            if (SkillD==this.MySkill)
            {
                //Debug.Log("Skill_10_point_2");
                BattleEnemy battleEnemy = Target as BattleEnemy;
                bool plusdmg = false;
                List<CastingSkill> list1 = new List<CastingSkill>();
                list1.AddRange(battleEnemy.SkillQueue);
                for (int i = 0; i < list1.Count; i++)
                {
                    if ((!View && list1[i].CastSpeed <= 1))
                    {
                        //BattleSystem.instance.ActWindow.CastingWaste(list1[i].skill);
                        //BattleSystem.instance.EnemyCastSkills.Remove(list1[i]);
                        //BattleSystem.instance.SaveSkill.Remove(list1[i]);
                        plusdmg = true;
                    }
                }
                if (plusdmg)
                {
                    return (int)(1.5 * Damage);
                }
            }

            return Damage;
        }

        public override void AttackEffectSingle(BattleChar hit, SkillParticle SP, int DMG, int Heal)

        {
            BattleEnemy battleEnemy = hit as BattleEnemy;
            List<CastingSkill> list1 = new List<CastingSkill>();
            list1.AddRange(battleEnemy.SkillQueue);
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].CastSpeed <= 1)
                {
                    BattleSystem.instance.ActWindow.CastingWaste(list1[i]);
                    BattleSystem.instance.EnemyCastSkills.Remove(list1[i]);
                    BattleSystem.instance.SaveSkill.Remove(list1[i]);
                }
            }

        }
    }
    public class Sex_des_11 : Sex_ImproveClone
    {
        public override void BattleStartDeck(List<Skill> Skills_Deck)
        {
            this.BChar.MyTeam.Draw(this.MySkill, null);
        }
    }
    public class Bcl_des_11 : SF_Standard.InBuff, IP_SkillUseEffect_Before,IP_CheckPlusFire//,IP_BuffAddAfter
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.HIT_DOT = 30f;
        }
        public bool CheckPlusFire(BattleChar BC=null)
        {
            if(BC==null || BC==this.BChar)
            {
                return true;
            }
            return false;
        }
        public void SkillUseEffect_Before(Skill SkillD, List<BattleChar> Targets)
        {

            if (!(this.BChar.Info.Passive is P_Destroyer) && SkillD.IsDamage && !Targets[0].Info.Ally && SkillD.AllExtendeds.Find(a => a is Sex_des_p_2) == null)
            {
                Sex_des_p_2 sex = new Sex_des_p_2();
                SkillD.ExtendedAdd(sex);
            }
        }
        /*
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if(BuffUser==this.BChar && addedbuff is Bcl_des_p_2)
            {
                int num = (int)(0.5f*addedbuff.DotDMGView());
                BuffTaker.Damage(this.BChar, num, false, true, false, 0, false, false, false);
            }
        }
        */
    }



    public class Sex_des_12 : Sex_ImproveClone
    {
        
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);



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


            Skill skill = Skill.TempSkill("S_SF_Destroyer_12_1", this.BChar,this.BChar.MyTeam);
            BattleSystem.instance.AllyTeam.Add(skill, true);

            SkillD.MySkill.Effect_Target.Buffs.Clear();
            if(!this.BChar.BuffFind("B_SF_Destroyer_p_1",false))
            {
                this.BChar.BuffAdd("B_SF_Destroyer_p_1", this.BChar, false, 0, false, -1, true);
            }
            Bcl_des_p_1 buffp1=this.BChar.BuffReturn("B_SF_Destroyer_p_1",false) as Bcl_des_p_1;
            buffp1.SetAttackMode(4);


        }
        
    }
    /*
    public class Bcl_des_12 : Buff,IP_PlayerTurn
    {
        public void Turn()
        {
            this.SelfDestroy();

            Skill skill = Skill.TempSkill("S_SF_Destroyer_12_1", this.BChar, this.BChar.MyTeam);
            BattleSystem.instance.AllyTeam.Add(skill, true);

            if (!this.BChar.BuffFind("B_SF_Destroyer_p_1", false))
            {
                this.BChar.BuffAdd("B_SF_Destroyer_p_1", this.BChar, false, 0, false, -1, true);
            }
            Bcl_des_p_1 buffp1 = this.BChar.BuffReturn("B_SF_Destroyer_p_1", false) as Bcl_des_p_1;
            buffp1.SetAttackMode(4);
        }
    }
    */
    public class Sex_des_12_1 : Sex_ImproveClone,IP_SkillUse_User
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", this.plusattack.ToString());
        }
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == this.BChar && SkillD!=this.MySkill)
            {
                this.plusattack++;
            }
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            int looptrun = this.plusattack;
            Skill skill = this.MySkill.CloneSkill(true, null, null, true);
            skill.PlusHit = true;

            for (int i = 1; i <= looptrun; i++)
            {
                if (BattleSystem.instance.EnemyTeam.AliveChars.Count >= 1)
                {

                        BattleSystem.DelayInput(this.Plushit(Targets, skill, true));
                    
                }
            }
        }

        public IEnumerator Plushit(List<BattleChar> tars, Skill skill, bool ran)//追加射击
        {
            yield return new WaitForSecondsRealtime(0.3f);

            if (BattleSystem.instance.EnemyTeam.AliveChars.Count >= 1)
            {
                this.BChar.ParticleOut(this.MySkill, skill, tars);
            }


            yield break;
        }

        public int plusattack = 0;
    }

    public class Sex_des_13 : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {

            for (int i = 0; i < 2; i++)
            {
                BattleSystem.DelayInputAfter(this.Draw(Targets[0]));
            }


        }

        public IEnumerator Draw(BattleChar target)
        {
            Skill skill2 = BattleSystem.instance.AllyTeam.Skills_Deck.Find((Skill skill) => skill.Master == target);

            if (skill2 == null)
            {
                new List<Skill>();
                List<GDESkillData> list = new List<GDESkillData>();
                List<GDESkillData> listorg = PlayData.GetCharacterSkillNoOverLap(target.Info, false, null);

                foreach (GDESkillData gdeskillData in listorg)
                {
                    if (gdeskillData.Effect_Target.DMG_Base >= 1 || gdeskillData.Effect_Target.DMG_Per >= 1)
                    {
                        list.Add(gdeskillData);
                    }
                }

                Skill skill = Skill.TempSkill(list.Random(target.GetRandomClass().Main).KeyID, target, target.MyTeam);
                skill.isExcept = true;
                BattleSystem.instance.AllyTeam.Add(skill, true);
            }
            else
            {
                yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyTeam._ForceDraw(skill2, null));
            }
            yield return null;
            yield break;
        }
    }
    public class E_SF_Destroyer_0 : EquipBase,IP_ParticleOut_After, IP_Kill//,IP_Effect_Before//,IP_BuffAddAfter//,IP_SkillUse_User_After//, IP_BuffAddAfter
    {
        public override void Init()
        {
            base.Init();
            //this.PlusStat.PlusCriDmg=60;
            this.PlusPerStat.Damage = 15;
            this.PlusStat.hit = 10;
            this.PlusStat.HIT_DOT = 20;

        }

        public IEnumerator ParticleOut_After(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == this.BChar && (Targets.Count > 1)  && this.activenum>0 && !SkillD.PlusHit && !SkillD.FreeUse)
            {
                this.BChar.MyTeam.AP += 1;
                this.activenum --;
            }
            yield break;
        }

        public void KillEffect(SkillParticle SP)
        {
            if(SP.SkillData.Master==this.BChar)
            {
                this.activenum ++;
            }
        }

        public override void TurnUpdate()
        {
            base.TurnUpdate();
            this.activenum = 1;
        }

        public int activenum = 1;
        /*
        public void Effect_Before(SkillParticle SP)//多命中增伤
        {
            if(SP.SkillData.Master==this.BChar  && (SP.ALLTARGET.Count > 1))
            {
                if(SP.SkillData.AllExtendeds.Find(a=>a is Sex_des_E_1)==null)
                {
                    Sex_des_E_1 sex = new Sex_des_E_1();
                    SP.SkillData.ExtendedAdd(sex);

                }
            }
        }
        */
        /*
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)//立即触发痛苦伤害
        {
            if (addedbuff.BuffData.Key == "B_SF_Destroyer_p_2" && BuffUser == this.BChar)
            {
                if(this.burstingchar.Find(a=>a==BuffTaker)==null )
                {

                    this.burstingchar.Add(BuffTaker);
                    //BattleSystem.DelayInput(this.Burst());
                    //this.Burst(BuffTaker);
                }
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(this.burstingchar.Count > 0)
            {
                while(this.burstingchar.Count > 0)
                {
                    if (this.burstingchar[0].BuffFind("B_SF_Destroyer_p_2", false))
                    {
                        Buff buff = this.burstingchar[0].BuffReturn("B_SF_Destroyer_p_2", false);

                        int num = buff.DotDMGView();
                        this.burstingchar[0].Damage(this.BChar, num, false, true, false, 0, false, false, false);
                        buff.TurnUpdate();
                    }
                    this.burstingchar.RemoveAt(0);
                }
            }
        }
        */
        private List<BattleChar> burstingchar = new List<BattleChar>();
    }




    public class FrameRecord
    {
        public BattleChar bc=new BattleChar();
        public int framerecord = 0;
        public int damage = 0;
    
    }
    /*
    public class Sex_des_E_1 : Sex_ImproveClone,IP_DamageChange
    {
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            //Debug.Log("DamageChange1");

            if (SkillD==this.MySkill && Damage > 0 )
            {
                //Debug.Log("DamageChange2");
                return Math.Max((int)(1.2 * Damage), 1);
            }
            return Damage;
        }
    }
    */

    public class SkillEn_SF_Destroyer_0 : Sex_ImproveClone, IP_DamageChange
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.IsDamage;
        }
        /*
        public override void Init()
        {
            base.Init();
            this.OnePassive = true;
        }
        */
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            //Debug.Log("Skill_EN_1_point_1");
            //Debug.Log(SkillD.MySkill.Name);
            //Debug.Log(this.MySkill.MySkill.Name);
            //Debug.Log("相同：" + (SkillD == this.MySkill));

            int dmg = Damage;

            int plusnum = 0;
            //Debug.Log("Skill_EN_1_point_1");
            if (SkillD.AllExtendeds.Find(a => a == this) != null)//SkillD==this.MySkill )
            {
                //Debug.Log("Skill_EN_1_point_2");
                foreach (Buff buff in Target.Buffs)
                    {
                        if (!buff.IsHide && buff.BuffData.Debuff && buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT)
                        {
                            foreach (StackBuff sta in buff.StackInfo)
                            {
                                plusnum++;
                            }
                        }
                    }

                    dmg=(int)(1+0.1*plusnum)*Damage;
            }


            return dmg;
        }
    }

        public class SkillEn_SF_Destroyer_1 : Sex_ImproveClone, IP_ParticleOut_Before
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return (MainSkill.TargetTypeKey == "enemy" || MainSkill.TargetTypeKey == "random_enemy" || MainSkill.TargetTypeKey == "all_onetarget")&& MainSkill.IsDamage;
        }
        /*
        public override void Init()
        {
            base.Init();
            this.OnePassive = true;
        }
        */
        public void ParticleOut_Before(Skill SkillD, List<BattleChar> Targets)
        {
            //Debug.Log("Skill_EN_2_point_1");
            //Debug.Log(SkillD.MySkill.Name);
            //Debug.Log(this.MySkill.MySkill.Name);
            //Debug.Log("相同：" + (SkillD == this.MySkill));

            if (SkillD.AllExtendeds.Find(a => a == this) != null)//SkillD==this.MySkill )
            {

                List<BattleChar> plustarget = new List<BattleChar>();
                foreach (BattleChar bc in Targets)
                {
                    plustarget.AddRange(Bcl_des_p_1.GetSideTarget(bc));
                }
                List<BattleChar> plustarget2 = new List<BattleChar>();
                foreach (BattleChar bc in plustarget)
                {
                    if (Targets.Find(a => a == bc) == null && plustarget2.Find(a => a == bc) == null)
                    {
                        plustarget2.Add(bc);
                    }
                }


                if (SkillD.AllExtendeds.Find(a => a is Sex_des_en_1) == null)
                {
                    Sex_des_en_1 sex = new Sex_des_en_1();

                    //sex.mainTarget = Targets[0];
                    sex.SetTargets(Targets,plustarget2);

                    SkillD.ExtendedAdd(sex);
                }
                else
                {
                    Sex_des_en_1 sex = SkillD.AllExtendeds.Find(a => a is Sex_des_en_1) as Sex_des_en_1;

                    //sex.mainTarget = Targets[0];

                    sex.SetTargets(Targets, plustarget2);
                }

                Targets.AddRange(plustarget2);


            }

        }
    }
    public class Sex_des_en_1 : Sex_ImproveClone,IP_DamageChange
    {
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            int dmg = Damage;
            if (Damage > 0)
            {
                if (SkillD == this.MySkill && this.mainTargets.Find(a => a == Target) != null)
                {
                    //Debug.Log("主要目标");
                    dmg =  Damage;
                }
                else if (SkillD == this.MySkill)
                {
                    //Debug.Log("次要目标");
                    dmg = Math.Max(1, (int)(0.5 * Damage));
                }
            }
            //Debug.Log("主要目标为："+this.mainTarget.Info.Name);

            return dmg;
        }
        //public BattleChar mainTarget=new BattleChar();

        public void SetTargets(List<BattleChar> Targets,List<BattleChar> Targets2)
        {
            this.mainTargets.Clear();
            this.mainTargets.AddRange(Targets);
            this.plusTargets.Clear();
            this.plusTargets.AddRange(Targets2);
            this.ashTargets.Clear();
            this.ashTargets.AddRange(Targets);
        }
        //public BattleChar mainTarget = new BattleChar();
        public List<BattleChar> mainTargets = new List<BattleChar>();
        public List<BattleChar> plusTargets = new List<BattleChar>();
        public List<BattleChar> ashTargets = new List<BattleChar>();
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
