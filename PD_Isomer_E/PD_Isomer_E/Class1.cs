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
using SF_Standard;
using Paradise_Basic;
using TileTypes;

namespace PD_Isomer
{
    [PluginConfig("M200_PD_Isomer_CharMod", "PD_Isomer_EnemyMod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class PD_Isomer_CharacterModPlugin : ChronoArkPlugin
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
        public class IsomerDef : ModDefinition
        {
        }




        /*

        public class S_PD_Patmos_1_Particle : CustomSkillGDE<PD_Isomer_CharacterModPlugin.IsomerDef>
        {
            // Token: 0x060000AB RID: 171 RVA: 0x000055A0 File Offset: 0x000037A0
            public override ModGDEInfo.LoadingType GetLoadingType()
            {
                return ModGDEInfo.LoadingType.Replace;
            }

            // Token: 0x060000AC RID: 172 RVA: 0x000055B4 File Offset: 0x000037B4
            public override string Key()
            {
                return "S_PD_Patmos_1";
            }

            // Token: 0x060000AD RID: 173 RVA: 0x000055CB File Offset: 0x000037CB
            public override void SetValue()
            {
                base.Particle = base.assetInfo.CodeConstuctAsync<GameObject>("Particle/Joey/Joey_6", new ModAssetInfo.ObjectChangeDelegate<GameObject>(this.ParticleChangeDelegate));//"Particle/Twins/Hel_2"
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


                ParticleColorChange(particle.transform.GetChild(0), new Color(1f, 0.459f, 0f, 0.1f));
                ParticleColorChange(particle.transform.GetChild(1), new Color(0.3529412f, 0.09492903f, 0f, 0f));



                return particle;
            }

        }

        public class S_PD_Patmos_0_Particle : CustomSkillGDE<PD_Isomer_CharacterModPlugin.IsomerDef>
        {
            // Token: 0x060000AB RID: 171 RVA: 0x000055A0 File Offset: 0x000037A0
            public override ModGDEInfo.LoadingType GetLoadingType()
            {
                return ModGDEInfo.LoadingType.Replace;
            }

            // Token: 0x060000AC RID: 172 RVA: 0x000055B4 File Offset: 0x000037B4
            public override string Key()
            {
                return "S_PD_Patmos_0";
            }

            // Token: 0x060000AD RID: 173 RVA: 0x000055CB File Offset: 0x000037CB
            public override void SetValue()
            {
                base.Particle = base.assetInfo.CodeConstuctAsync<GameObject>("Particle/Joey/Joey_6", new ModAssetInfo.ObjectChangeDelegate<GameObject>(this.ParticleChangeDelegate));//"Particle/Twins/Hel_2"
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


                ParticleColorChange(particle.transform.GetChild(0), new Color(0f, 0.313f, 0.5f, 0.241f));
                ParticleColorChange(particle.transform.GetChild(1), new Color(0.3529412f, 0.09492903f, 0f, 0f));



                return particle;
            }

        }
        */






















    }


    [HarmonyPatch(typeof(BuffObject))]
    public class BuffObjectUpdatePlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        public static void Update_Patch(BuffObject __instance)//BuffObjectUpdate_Patch(BuffObject __instance)
        {

            if (__instance.MyBuff != null )
            {
                IP_BuffObject_Updata ip_patch = __instance.MyBuff as IP_BuffObject_Updata;
                if (ip_patch != null)
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
        /*
        public void BuffObject_Updata(BuffObject obj)
        {
            obj.StackText.text = this.range.ToString();
        }
        */
    }

    /*
    [HarmonyPatch(typeof(FieldEventSelect))]
    public class GetEventListPlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("GetEventList")]
        [HarmonyPostfix]
        public static void Update_Patch(FieldEventSelect __instance, List<string> __result, bool IsReroll , List<string> HasEventKey)//BuffObjectUpdate_Patch(BuffObject __instance)
        {

            if(ModManager.getModInfo("PD_Boss_Nyto_Isomer").GetSetting<ToggleSetting>("AlwaysEvent").Value)
            {
                if (__result.Contains("RE_IsomerBossBattleEvent"))
                {
                    List<string> list1 = new List<string>();
                    List<string> list2 = new List<string>();
                    list1.AddRange(__result);
                    list1.Remove("RE_IsomerBossBattleEvent");
                    list2.Add("RE_IsomerBossBattleEvent");

                    if (PlayData.TSavedata.Passive_Itembase.Find((ItemBase a) => a != null && a.itemkey == GDEItemKeys.Item_Passive_Sign) != null)
                    {
                        list2.AddRange(list1.Random(RandomClassKey.Event, 2));
                    }
                    else
                    {
                        list2.AddRange(list1.Random(RandomClassKey.Event, 1));
                    }
                    __result.Clear();
                    __result.AddRange(list2);
                }
            }


        }
    }
    */
    [HarmonyPatch(typeof(FieldEventSelect))]
    public class GetEventListPlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("GetEventList")]
        [HarmonyPostfix]
        public static void Update_Patch(FieldEventSelect __instance, List<string> __result, bool IsReroll, List<string> HasEventKey)//BuffObjectUpdate_Patch(BuffObject __instance)
        {

            if (ModManager.getModInfo("PD_Boss_Nyto_Isomer").GetSetting<ToggleSetting>("AlwaysEvent").Value)
            {
                if (__result.Contains("RE_IsomerBossBattleEvent"))
                {
                    GetEventListPlugin.frame = Time.frameCount;
                }
                else
                {
                    GetEventListPlugin.frame = -1;
                }
            }


        }
        public static int frame = -1;

        [HarmonyPatch("FieldEventSelectOpen")]
        [HarmonyPrefix]
        public static void Update_Patch(FieldEventSelect __instance, ref List<string> EventList, EventObject EVObject, GameObject Mainobject , bool IsDebug)//BuffObjectUpdate_Patch(BuffObject __instance)
        {

            if (ModManager.getModInfo("PD_Boss_Nyto_Isomer").GetSetting<ToggleSetting>("AlwaysEvent").Value)
            {
                if (GetEventListPlugin.frame == Time.frameCount && !EventList.Contains("RE_IsomerBossBattleEvent"))
                {
                    try
                    {
                        List<string> strs = EventList.Random(RandomClassKey.Event, 1);
                        EventList.RemoveAll(a => strs.Contains(a));
                        EventList.Add("RE_IsomerBossBattleEvent");
                    }
                    catch { }
                }
            }


        }
    }



    [HarmonyPatch(typeof(SkillToolTip))]
    public class SkillToolTipPlugin
    {
        [HarmonyPatch("Input")]
        [HarmonyPrefix]
        public static void Input_Patch(Skill Skill)//BuffObjectUpdate_Patch(BuffObject __instance)
        {
            //UnityEngine.Debug.Log("Input_Patch_1"+Skill.MySkill.Name);
            if(BattleSystem.instance==null)
            {
                return;
            }
            if (BattleSystem.instance.EnemyCastSkills.Find(a => a.skill == Skill) !=null)
            {
                //UnityEngine.Debug.Log("Input_Patch_2");


                    SkillToolTipPlugin.skill = BattleSystem.instance.EnemyCastSkills.Find(a => a.skill== Skill).skill;
                    return;
                

            }
            SkillToolTipPlugin.skill = new Skill();
        }
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8

        [HarmonyPatch("ViewBuffs")]
        [HarmonyPostfix]
        public static void ViewBuffs_Patch(List<BuffTag> SkillBuffs, BattleChar Master, List<Buff> __result)//BuffObjectUpdate_Patch(BuffObject __instance)
        {
            //UnityEngine.Debug.Log("ViewBuffs_Patch_1");
            if (BattleSystem.instance == null)
            {
                return;
            }
            foreach (Buff buff in __result)
            {
                IP_ViewBuffs_After ip_patch = buff as IP_ViewBuffs_After;
                if (ip_patch != null)
                {
                    ip_patch.ViewBuffs_After(SkillToolTipPlugin.skill);
                }

                //string str1 = (((__instance.MyBuff) as Bcl_drm_0).range).ToString();
                //__instance.StackText.text = str1;
                return;
            }
            return;
        }

        public static Skill skill=new Skill();
    }

    public interface IP_ViewBuffs_After
    {
        // Token: 0x06000001 RID: 1
        void ViewBuffs_After(Skill skill);
        /*
        public void BuffObject_Updata(BuffObject obj)
        {
            obj.StackText.text = this.range.ToString();
        }
        */
    }








    public class RE_IsomerBossBattleEvent : RandomEventBaseScript
    {
        public override void EventInit()
        {
            base.EventInit();
            this.m = UnityEngine.Object.FindObjectOfType<MiniBossObject>();
        }

        // Token: 0x06001FCD RID: 8141 RVA: 0x000D7114 File Offset: 0x000D5314
        public override void EventOpen()
        {
            base.EventOpen();
            if (this.m == null)
            {
                this.m = UnityEngine.Object.FindObjectOfType<MiniBossObject>();
            }
            
            if (this.m.BossClear)
            {
                base.ChangeDesc(this.MyUI.MainEventData.OrderStrings[2], true);
                return;
            }
            
            base.ChangeDesc(this.MyUI.MainEventData.EventDetails, false);
        }
        public override void UseButton1()
        {

            UIManager.inst.StartCoroutine(this.Co_OnlyEvent());
        }
        private IEnumerator Co_OnlyEvent()
        {
            if (this.m == null)
            {
                this.m = UnityEngine.Object.FindObjectOfType<MiniBossObject>();
            }
            this.m.BossClear = true;

            base.ChangeDesc(this.MyUI.MainEventData.OrderStrings[0], true);
            yield return new WaitForSeconds(1f);
            base.EventDisable();
            this.MyUI.Delete();
            FieldSystem.instance.BattleStart(new GDEEnemyQueueData("Queue_Isomer_Twins"), GDEItemKeys.BattleMaps_BattleMap_Snow, true, false, "", "", false);
            UIManager.inst.StartCoroutine(UIManager.inst.FadeBlack_In(0.5f));
            yield break;
        }

        private MiniBossObject m;
    }





    public class BattleExtended_IsomerBoss : PassiveBase,IP_SomeOneDead, IP_BattleStart_UIOnBefore, IP_BattleEndRewardChange//,IP_PlayerTurn
    {
        public void BattleStartUIOnBefore(BattleSystem Ins)
        {
            BattleSystem.DelayInput(this.Start1());
            MasterAudio.StopBus("BGM");
        }
        
        public void Turn()
        {
             BattleSystem.DelayInputAfter(Summon.ResetPos());
        }
        public void SomeOneDead(BattleChar DeadChar)
        {
            BattleSystem.DelayInputAfter(Summon.ResetPos());
        }

        public void BattleEndRewardChange()
        {
            BattleSystem.instance.Reward.Add(ItemBase.GetItem("Item_SF_1", 4));
            //Skill skill = Skill.TempSkill("S_PD_Reward_0", BattleSystem.instance.AllyTeam.LucyChar, BattleSystem.instance.AllyTeam);
            BattleSystem.instance.Reward.Add(ItemBase.GetItem(new GDESkillData("S_PD_Reward_0")));

            if(IsomerOverallSystem.ins.lastDeadCharKey=="PD_Nimogen")
            {
                BattleSystem.instance.Reward.Add(ItemBase.GetItem("RelicNimogen"));
            }
            else if (IsomerOverallSystem.ins.lastDeadCharKey == "PD_Mercurows")
            {
                BattleSystem.instance.Reward.Add(ItemBase.GetItem("RelicMercurows"));
            }
            else
            {
                int ran = RandomManager.RandomInt(RandomClassKey.Enemy, 0, 2);
                if(ran==0)
                {
                    BattleSystem.instance.Reward.Add(ItemBase.GetItem("RelicNimogen"));
                }
                else
                {
                    BattleSystem.instance.Reward.Add(ItemBase.GetItem("RelicMercurows"));
                }
            }
        }
        public IEnumerator Start1()
        {
            MasterAudio.SetBusVolumeByName("BGM", 1f);
            if(ModManager.getModInfo("PD_Boss_Nyto_Isomer").GetSetting<ToggleSetting>("OldBGM").Value)
            {
                MasterAudio.PlaySound("Isomer_BGM_old", 0.8f, null, 0f, null, null, false, false);
            }
            else
            {
                MasterAudio.PlaySound("Isomer_BGM", 1f, null, 0f, null, null, false, false);

            }
            yield break;
        }
    }










    public class IsomerOverallSystem
    {
        public void CheckAndReset()
        {
            bool exist_Nimogen = false;
            bool exist_Mercurows = false;
            bool phase2 = false;
            foreach(BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {
                if(bc.Info.KeyData=="PD_Nimogen")
                {
                    exist_Nimogen = true;
                    if (bc.HP < 0.7 * bc.GetStat.maxhp) 
                    {
                        phase2 = true;
                    }
                }
                if(bc.Info.KeyData== "PD_Mercurows")
                {
                    exist_Mercurows=true;
                    if (bc.HP < 0.7 * bc.GetStat.maxhp) 
                    {
                        phase2 = true;
                    }
                }
            }
            if(exist_Nimogen && exist_Mercurows)
            {
                if(phase2)
                {
                    this.phase = 2;
                }
                else
                {
                    this.phase=1;
                }
            }
            else if(exist_Nimogen || exist_Mercurows)
            {
                this.phase = 3;
            }
            else
            {
                this.phase = 1;
            }
        }
        public void RaisePhase(int pha)
        {
            int beforepha = this.phase;
            this.phase=Math.Max(this.phase, pha);

            if (beforepha<2 && this.phase==2)
            {
                //Debug.Log("转阶段：2");
                //半血感召仪式,用delayinputafter,同时触发多个只执行1个
            }
            if (beforepha < 3 && this.phase == 3)
            {
                //Debug.Log("转阶段：3");
                //死亡感召仪式,用delayinputafter,同时触发多个只执行1个
            }
        }
        public int ForceSummonPrepare()
        {
            foreach(BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {
                BattleEnemy battleEnemy = bc as BattleEnemy;
                List<CastingSkill> list1 = new List<CastingSkill>();
                list1.AddRange(battleEnemy.SkillQueue);
                for (int i = 0; i < list1.Count; i++)
                {
                    if (list1[i].skill.MySkill.KeyID== "S_PD_Isomer_0")
                    {
                        BattleSystem.instance.ActWindow.CastingWaste(list1[i]);
                        BattleSystem.instance.EnemyCastSkills.Remove(list1[i]);
                        BattleSystem.instance.SaveSkill.Remove(list1[i]);
                    }

                }
            }
            int num = 0;
            foreach (BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {
                if(bc.BuffFind("B_PD_Isomer_Reinforce",false))
                {
                    num += bc.HP;
                    bc.Dead();
                }
            }
            return num;
        }
        public BattleChar SummonSkillChar()
        {


            if (BattleSystem.instance.TurnNum != this.summonTurn || !BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Contains(this.SummonChar))
            {
                this.summonTurn = BattleSystem.instance.TurnNum;

                if (this.phase == 2)
                {
                    if (BattleSystem.instance.EnemyTeam.AliveChars.Contains(this.phase2char))
                    {
                        this.SummonChar = this.phase2char;
                        return this.phase2char;
                    }

                }

                List<BattleChar> list1 = new List<BattleChar>();
                list1.AddRange(BattleSystem.instance.EnemyTeam.AliveChars_Vanish.FindAll(a => a.Info.KeyData == "PD_Nimogen" || a.Info.KeyData == "PD_Mercurows"));
                int ran = RandomManager.RandomInt(RandomClassKey.Enemy, 0, list1.Count );
                this.SummonChar = list1[ran];
                return this.SummonChar;
            }
            else
            {
                return this.SummonChar;
            }


        }
        public BattleChar SummonChar=new BattleChar();
        public int summonTurn = -1;
        public BattleChar EnforceSkillChar()
        {
            if (BattleSystem.instance.TurnNum != this.enforceTurn || !BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Contains(this.enforceChar))
            {
                this.enforceTurn = BattleSystem.instance.TurnNum;

                List<BattleChar> list1 = new List<BattleChar>();
                list1.AddRange(BattleSystem.instance.EnemyTeam.AliveChars_Vanish.FindAll(a => a.Info.KeyData == "PD_Nimogen" || a.Info.KeyData == "PD_Mercurows"));
                if(this.summonTurn == BattleSystem.instance.TurnNum)
                {
                    list1.Remove(this.SummonChar);
                }
                
                if(list1.Count > 0)
                {
                    int ran = RandomManager.RandomInt(RandomClassKey.Enemy, 0, list1.Count);
                    this.enforceChar = list1[ran];
                    return this.enforceChar;
                }

            }
            else
            {
                return this.enforceChar;
            }
            this.enforceChar = new BattleChar();
            return this.enforceChar;

        }
        public BattleChar enforceChar = new BattleChar();
        public int enforceTurn = -1;
        public BattleChar MainSkillChar_Phase1()
        {
            if(BattleSystem.instance.TurnNum!=this.mainSkillTurn)
            {
                
                List<BattleChar> list1 = new List<BattleChar>();
                list1.AddRange(BattleSystem.instance.EnemyTeam.AliveChars_Vanish.FindAll(a => a.Info.KeyData == "PD_Nimogen" || a.Info.KeyData == "PD_Mercurows"));
                
                foreach(BattleChar bc in this.mainSkillrecords)
                {
                    if(list1.Count>1)
                    {
                        list1.RemoveAll(a => a == bc);

                    }
                    else
                    {
                        break;
                    }
                }
                if(list1.Count>0)
                {
                    this.mainSkillTurn = BattleSystem.instance.TurnNum;
                    int ran = RandomManager.RandomInt(RandomClassKey.Enemy, 0, list1.Count );
                    this.mainSkillrecords.Insert(0, list1[ran]);
                    return list1[ran];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return this.mainSkillrecords[0];
            }
        }
        public BattleChar MainSkillChar_Phase2()
        {
            if (BattleSystem.instance.TurnNum != this.mainSkillTurn)
            {
                

                List<BattleChar> list1 = new List<BattleChar>();
                list1.AddRange(BattleSystem.instance.EnemyTeam.AliveChars_Vanish.FindAll(a => a.Info.KeyData == "PD_Nimogen"));
                List<BattleChar> list2 = new List<BattleChar>();
                list2.AddRange(BattleSystem.instance.EnemyTeam.AliveChars_Vanish.FindAll(a =>  a.Info.KeyData == "PD_Mercurows"));

                List<BattleChar> list3 = new List<BattleChar>();
                foreach(BattleChar bc in list1)
                {
                    if(BattleSystem.instance.AllyTeam.AliveChars.Find(a => a.BuffFind("B_PD_Nimogen_0") && a.BuffReturn("B_PD_Nimogen_0").Usestate_L == bc) == null && BattleSystem.instance.TurnNum- ((bc as BattleEnemy).Ai as AI_PD_Nimogen).lastMainSkillTurn>1)
                    {
                        list3.Add(bc);
                    }
                }
                foreach (BattleChar bc in list2)
                {
                    if (BattleSystem.instance.AllyTeam.AliveChars.Find(a => a.BuffFind("B_PD_Mercurows_0") && a.BuffReturn("B_PD_Mercurows_0").Usestate_L == bc) == null && BattleSystem.instance.TurnNum - ((bc as BattleEnemy).Ai as AI_PD_Mercurows).lastMainSkillTurn > 1)
                    {
                        list3.Add(bc);
                    }
                }
                if(list3.Count>0)
                {
                    int ran = RandomManager.RandomInt(RandomClassKey.Enemy, 0, list3.Count - 1);
                    this.mainSkillTurn = BattleSystem.instance.TurnNum;
                    this.mainSkillrecords.Insert(0, list3[ran]);
                    return list3[ran];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return this.mainSkillrecords[0];
            }
        }
        //BattleSystem.instance.AllyTeam.AliveChars.Find(a => a.BuffFind("B_PD_Nimogen_0") && a.BuffReturn("B_PD_Nimogen_0").Usestate_L==this.BChar) ==null
        public static IsomerOverallSystem ins ;
        public int phase = 1;
        public List<BattleChar> mainSkillrecords=new List<BattleChar>();
        public int mainSkillTurn = 0;
        public BattleChar phase2char=new BattleChar();
        public string lastDeadCharKey=string.Empty;
        public static List<string> NimogenEnemyList = new List<string>
        {
        "PD_Defender_Isomer",
        "PD_Doppelsoldner_Isomer",
        "PD_Patmos_Isomer",
        "PD_Roarer_Isomer",
        "PD_StreletPlus_Isomer"
         };
        public static List<string> MercurowsEnemyList = new List<string>
        {
        "PD_Patroller_Isomer",
        "PD_NytoRF_Isomer",
        "PD_NytoSMG_Isomer",
        "PD_NytoCommander_Isomer",
        "PD_NytoHammer_Isomer"
         };
    }
    public class Bcl_Isomer_p_1:Buff
    {
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();

            Button button = this.BuffIcon.AddComponent<Button>();
            button.onClick.AddListener(new UnityAction(this.Go));

        }
        public void Go()
        {
            //Debug.Log("go");
            //BattleSystem.DelayInputAfter(Summon.ResetPos());
            //Summon.AutoPosCalculate();
            BattleEnemy be = this.BChar as BattleEnemy;
            Debug.Log("test1_x:" + be.PosObject.transform.localScale.x);
            Debug.Log("test1_y:" + be.PosObject.transform.localScale.y);
            Debug.Log("test1_z:" + be.PosObject.transform.localScale.z);
        }

    }
    //--------------------------
    public class Bcl_Nimogen_h:Buff,IP_BattleStart_Ones,IP_HPChange,IP_Dead,IP_PlayerTurn_EnemyAfter
    {
        public void BattleStart(BattleSystem Ins)
        {
            IsomerOverallSystem.ins = new IsomerOverallSystem();
            IsomerOverallSystem.ins.CheckAndReset();

            //(this.BChar as BattleEnemy).UseCustomPos = true;
        }
        public override void FixedUpdate()
        {
            if (this.mainAI == null)
            {
                this.mainAI = ((this.BChar as BattleEnemy).Ai as AI_PD_Nimogen);
            }
        }
        public void PlayerTurn_EnemyAfter()
        {
            if (BattleSystem.instance.EnemyTeam.AliveChars_Vanish.FindAll(a => a.BuffFind("B_PD_Isomer_Reinforce", false)).Count <= 0 && this.BChar == IsomerOverallSystem.ins.SummonSkillChar())
            {
                Skill skill = Skill.TempSkill("S_PD_Isomer_0", this.BChar, this.BChar.MyTeam);
                /*
                if (BattleSystem.instance.TurnNum == 1)
                {
                    BattleSystem.DelayInput(this.SummonIE(skill, this.BChar, true));
                    return;
                }
                */
                BattleSystem.instance.EnemyCastEnqueue(this.BChar as BattleEnemy, skill, (this.BChar as BattleEnemy).Ai.TargetSelect(skill), 99, false);
            }
            else if (this.BChar == IsomerOverallSystem.ins.EnforceSkillChar())
            {
                if (BattleSystem.instance.TurnNum == 0)
                {
                    return;
                }
                Skill skill = Skill.TempSkill("S_PD_Isomer_1", this.BChar, this.BChar.MyTeam);
                BattleSystem.instance.EnemyCastEnqueue(this.BChar as BattleEnemy, skill, (this.BChar as BattleEnemy).Ai.TargetSelect(skill), 99, false);

            }
        }
        public void HPChange(BattleChar Char, bool Healed)
        {
            if(Char==this.BChar)
            {
                if (this.BChar.HP < 0.7 * this.BChar.GetStat.maxhp && IsomerOverallSystem.ins.phase < 2) 
                {
                    IsomerOverallSystem.ins.RaisePhase(2);
                    IsomerOverallSystem.ins.phase2char = this.BChar;
                    Skill skill = Skill.TempSkill("S_PD_Isomer_0", this.BChar, this.BChar.MyTeam);
                    skill.FreeUse = true;
                    //BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, this.BChar, false, false, false, null));
                    //this.BChar.UseSkill(skill);
                    //BattleTeam.SkillRandomUse(this.BChar, skill, false, true, false);
                    //BattleSystem.instance.EnemyCastEnqueue(this.BChar as BattleEnemy, skill, (this.BChar as BattleEnemy).Ai.TargetSelect(skill), 0, false);

                    BattleSystem.DelayInput(this.SummonIE(skill, this.BChar));
                }
            }
        }
        public void Dead()
        {
            if (IsomerOverallSystem.ins.phase < 3)
            {
                IsomerOverallSystem.ins.RaisePhase(3);
                BattleChar bc = this.BChar.MyTeam.AliveChars.Find(a => (a.Info.KeyData == "PD_Nimogen" || a.Info.KeyData == "PD_Mercurows") && a != this.BChar);
                if (bc != null)
                {
                    Skill skill = Skill.TempSkill("S_PD_Isomer_0", bc, bc.MyTeam);
                    skill.FreeUse = true;
                    //BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, this.BChar, false, false, false, null));
                    //bc.UseSkill(skill);
                    //BattleTeam.SkillRandomUse(bc, skill, false, true, false);
                    //BattleSystem.instance.EnemyCastEnqueue(bc as BattleEnemy, skill, (bc as BattleEnemy).Ai.TargetSelect(skill), 0, false);

                    BattleSystem.DelayInput(this.SummonIE(skill, bc));
                }
            }
            IsomerOverallSystem.ins.lastDeadCharKey = this.BChar.Info.KeyData;
        }
        public IEnumerator SummonIE(Skill skill,BattleChar bc,bool battlestart=false)
        {
            if(!battlestart)
            {
                int num = IsomerOverallSystem.ins.ForceSummonPrepare();
                yield return new WaitForSecondsRealtime(0.5f);
                if(num > 0)
                {
                    foreach (BattleChar cha in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
                    {
                        if (cha.Info.KeyData == "PD_Nimogen" || cha.Info.KeyData == "PD_Mercurows")
                        {
                            cha.Heal(cha, num, false, false, null);
                        }
                    }
                    yield return new WaitForSecondsRealtime(0.5f);
                }

            }


            CastingSkill skill2 = new CastingSkill
            {
                skill = skill,
                Target = bc,
                Usestate = skill.Master,
                CastSpeed = 0,
                IsEnemyForceTarget = true
            };
            yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.EnemyActionScene(skill2, false));


            yield break;
        }

        public AI_PD_Nimogen mainAI = new AI_PD_Nimogen();
    }
    public class Bcl_Mercurows_h:Buff, IP_BattleStart_Ones, IP_HPChange, IP_Dead, IP_PlayerTurn_EnemyAfter
    {
        public void BattleStart(BattleSystem Ins)
        {
            IsomerOverallSystem.ins = new IsomerOverallSystem();
            IsomerOverallSystem.ins.CheckAndReset();

            //(this.BChar as BattleEnemy).UseCustomPos = true;
        }
        public override void FixedUpdate()
        {
            if (this.mainAI == null)
            {
                this.mainAI = ((this.BChar as BattleEnemy).Ai as AI_PD_Mercurows);
            }
        }
        public void PlayerTurn_EnemyAfter()
        //public void Turn()
        {
            if (BattleSystem.instance.EnemyTeam.AliveChars_Vanish.FindAll(a=>a.BuffFind("B_PD_Isomer_Reinforce",false)).Count<=0 && this.BChar == IsomerOverallSystem.ins.SummonSkillChar())
            {
                Skill skill = Skill.TempSkill("S_PD_Isomer_0", this.BChar, this.BChar.MyTeam);
                /*
                if (BattleSystem.instance.TurnNum<=1)
                {
                    BattleSystem.DelayInput(this.SummonIE(skill, this.BChar,true));
                    return;
                }
                */
                BattleSystem.instance.EnemyCastEnqueue(this.BChar as BattleEnemy, skill, (this.BChar as BattleEnemy).Ai.TargetSelect(skill), 99, false);
            }
            else if(this.BChar==IsomerOverallSystem.ins.EnforceSkillChar())
            {
                if (BattleSystem.instance.TurnNum == 0)
                {
                    return;
                }
                Skill skill = Skill.TempSkill("S_PD_Isomer_1", this.BChar, this.BChar.MyTeam);
                BattleSystem.instance.EnemyCastEnqueue(this.BChar as BattleEnemy, skill, (this.BChar as BattleEnemy).Ai.TargetSelect(skill), 99, false);

            }
        }
        public void HPChange(BattleChar Char, bool Healed)
        {
            if (Char == this.BChar)
            {
                if (this.BChar.HP < 0.7 * this.BChar.GetStat.maxhp && IsomerOverallSystem.ins.phase < 2)
                {
                    IsomerOverallSystem.ins.RaisePhase(2);
                    IsomerOverallSystem.ins.phase2char = this.BChar;
                    Skill skill = Skill.TempSkill("S_PD_Isomer_0", this.BChar, this.BChar.MyTeam);
                    skill.FreeUse = true;
                    //BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, this.BChar, false, false, false, null));
                    //this.BChar.UseSkill(skill);
                    //BattleTeam.SkillRandomUse(this.BChar, skill, false, true, false);
                    //BattleSystem.instance.EnemyCastEnqueue(this.BChar as BattleEnemy, skill, (this.BChar as BattleEnemy).Ai.TargetSelect(skill), 0, false);

                    BattleSystem.DelayInput(this.SummonIE(skill, this.BChar));
                }
            }
            
        }
        public void Dead()
        {
            if (IsomerOverallSystem.ins.phase < 3)
            {
                IsomerOverallSystem.ins.RaisePhase(3);
                BattleChar bc = this.BChar.MyTeam.AliveChars.Find(a => (a.Info.KeyData == "PD_Nimogen" || a.Info.KeyData == "PD_Mercurows") && a != this.BChar);     
                if(bc!=null)
                {
                    Skill skill = Skill.TempSkill("S_PD_Isomer_0", bc, bc.MyTeam);
                    skill.FreeUse = true;
                    //BattleSystem.DelayInput(BattleSystem.instance.ForceAction(skill, this.BChar, false, false, false, null));
                    //bc.UseSkill(skill);
                    //BattleTeam.SkillRandomUse(bc, skill, false, true, false);
                    //BattleSystem.instance.EnemyCastEnqueue(bc as BattleEnemy, skill, (bc as BattleEnemy).Ai.TargetSelect(skill), 0, false);

                    BattleSystem.DelayInput(this.SummonIE(skill, bc));
                }
            }
            IsomerOverallSystem.ins.lastDeadCharKey = this.BChar.Info.KeyData;
        }
        public IEnumerator SummonIE(Skill skill, BattleChar bc, bool battlestart = false)
        {
            if (!battlestart)
            {
                int num = IsomerOverallSystem.ins.ForceSummonPrepare();
                yield return new WaitForSecondsRealtime(0.5f);
                if (num > 0)
                {
                    foreach (BattleChar cha in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
                    {
                        if (cha.Info.KeyData == "PD_Nimogen" || cha.Info.KeyData == "PD_Mercurows")
                        {
                            cha.Heal(cha, num, false, false, null);
                        }
                    }
                    yield return new WaitForSecondsRealtime(0.5f);
                }
            }
            CastingSkill skill2 = new CastingSkill
            {
                skill = skill,
                Target = bc,
                Usestate = skill.Master,
                CastSpeed = 0,
                IsEnemyForceTarget = true
            };
            yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.EnemyActionScene(skill2, false));



            yield break;
        }

        public AI_PD_Mercurows mainAI = new AI_PD_Mercurows();
    }
    /*
    public class AI_PD_Paradise_Base : AI
    {

    }
    */
    //------------------------
    public class AI_PD_Nimogen : AI_PD_Paradise_Base, IP_ForceShieldData
    {
        public override List<BattleChar> TargetSelect(Skill SelectedSkill)
        {
            return base.TargetSelect(SelectedSkill);
        }
        public override Skill SkillSelect(int ActionCount)
        {
            bool back = false;    
            if(BattleSystem.instance.AllyTeam.AliveChars.Find(a => !a.BuffFind("B_PD_Nimogen_0") ) == null)
            {
                goto Point_002;
            }
        Point_001:
            if (IsomerOverallSystem.ins.phase == 1)
            {
                if (BattleSystem.instance.AllyTeam.AliveChars.Find(a => a.BuffFind("B_PD_Nimogen_0") || a.BuffFind("B_PD_Mercurows_0")) == null)
                {
                    if (BattleSystem.instance.TurnNum <= 1)
                    {
                        goto Point_002;
                    }
                    if (this.BChar == IsomerOverallSystem.ins.MainSkillChar_Phase1())
                    {
                        return this.BChar.Skills[0];
                    }
                }
            }
            else if(IsomerOverallSystem.ins.phase == 2)
            {
                if (BattleSystem.instance.AllyTeam.AliveChars.Find(a => a.BuffFind("B_PD_Nimogen_0") && a.BuffReturn("B_PD_Nimogen_0").Usestate_L==this.BChar) ==null && BattleSystem.instance.TurnNum - this.lastMainSkillTurn > 1)
                {
                    return this.BChar.Skills[0];
                    /*
                    if (this.BChar == IsomerOverallSystem.ins.MainSkillChar_Phase2())
                    {
                        return this.BChar.Skills[0];
                    }
                    */
                }
            }
            else if (IsomerOverallSystem.ins.phase == 3)
            {
                int num = BattleSystem.instance.AllyTeam.AliveChars.FindAll(a => a.BuffFind("B_PD_Nimogen_0") && a.BuffReturn("B_PD_Nimogen_0").Usestate_L == this.BChar).Count;
                if (num < 1 || (num < 2 && BattleSystem.instance.TurnNum - this.lastMainSkillTurn > 1)) 
                {
                    return this.BChar.Skills[0];
                }
            }
            else if (!back)
            {
                back = true;
                IsomerOverallSystem.ins.CheckAndReset();
                goto Point_001;
            }
        Point_002:
            if (this.lastSkillKey== this.BChar.Skills[1].MySkill.KeyID || this.lastSkillKey==string.Empty)
            {
                this.lastSkillKey = this.BChar.Skills[2].MySkill.KeyID;
                return this.BChar.Skills[2];
            }
            else if(this.lastSkillKey == this.BChar.Skills[2].MySkill.KeyID)
            {
                this.lastSkillKey = this.BChar.Skills[1].MySkill.KeyID;
                return this.BChar.Skills[1];
            }
            return this.BChar.Skills[0];
        }
        public override int SpeedChange(Skill skill, int ActionCount, int OriginSpeed)
        {
            if (skill.MySkill.KeyID == "S_PD_Nimogen_0" && ActionCount == 0 && OriginSpeed > 1)
            {
                //return OriginSpeed - 1;
                return  1;
            }
            return base.SpeedChange(skill, ActionCount, OriginSpeed);
        }
        public void ForceShieldData(ref int maxnum, ref int num)
        {
            maxnum = 300;
            num = 240;
        }
        public string lastSkillKey=string.Empty;
        public int lastMainSkillTurn = 0;
    }
    public class AI_PD_Mercurows : AI_PD_Paradise_Base, IP_ForceShieldData
    {
        public override List<BattleChar> TargetSelect(Skill SelectedSkill)
        {
            if (SelectedSkill.MySkill.KeyID == this.BChar.Skills[0].MySkill.KeyID)
            {
                if (BattleSystem.instance.AllyTeam.AliveChars.FindAll(a => a.BuffFind("B_PD_Mercurows_0") && a.BuffReturn("B_PD_Mercurows_0").Usestate_L == this.BChar).Count > 0)
                {
                    List<BattleAlly> list = BattleSystem.instance.AllyList.FindAll(a => a.BuffFind("B_PD_Mercurows_0") && a.BuffReturn("B_PD_Mercurows_0").Usestate_L == this.BChar);
                    List<BattleChar> list2 = new List<BattleChar>();
                    list2.Add(Aggro.AgrroReturnExcept(this.BChar, list));
                    return list2;
                }
            }
            return base.TargetSelect(SelectedSkill);
        }
        public override Skill SkillSelect(int ActionCount)
        {
            bool back = false;    
        Point_001:
            if (IsomerOverallSystem.ins.phase == 1)
            {
                if(BattleSystem.instance.AllyTeam.AliveChars.Find(a=>a.BuffFind("B_PD_Nimogen_0")|| a.BuffFind("B_PD_Mercurows_0")) ==null)
                {
                    if(BattleSystem.instance.TurnNum<=1)
                    {
                        goto Point_002;
                    }
                    if (this.BChar == IsomerOverallSystem.ins.MainSkillChar_Phase1())
                    {
                        return this.BChar.Skills[0];
                    }
                }

            }
            else if (IsomerOverallSystem.ins.phase == 2)
            {
                if (BattleSystem.instance.AllyTeam.AliveChars.Find(a => a.BuffFind("B_PD_Mercurows_0") && a.BuffReturn("B_PD_Mercurows_0").Usestate_L == this.BChar) == null && BattleSystem.instance.TurnNum - this.lastMainSkillTurn > 1)
                {
                    return this.BChar.Skills[0];
                    /*
                    if (this.BChar == IsomerOverallSystem.ins.MainSkillChar_Phase2())
                    {
                        return this.BChar.Skills[0];
                    }
                    */
                }
            }
            else if(IsomerOverallSystem.ins.phase == 3)
            {
                int num = BattleSystem.instance.AllyTeam.AliveChars.FindAll(a => a.BuffFind("B_PD_Mercurows_0") && a.BuffReturn("B_PD_Mercurows_0").Usestate_L == this.BChar).Count;
                if (num < 1 || (num < 2 && BattleSystem.instance.TurnNum - this.lastMainSkillTurn > 1)) 
                {
                    return this.BChar.Skills[0];
                }
            }
            else if(!back)
            {
                back = true;
                IsomerOverallSystem.ins.CheckAndReset();
                goto Point_001;
            }
        Point_002:
            if (this.lastSkillKey == this.BChar.Skills[1].MySkill.KeyID )
            {
                this.lastSkillKey = this.BChar.Skills[2].MySkill.KeyID;
                return this.BChar.Skills[2];
            }
            else if (this.lastSkillKey == this.BChar.Skills[2].MySkill.KeyID || this.lastSkillKey == string.Empty)
            {
                this.lastSkillKey = this.BChar.Skills[1].MySkill.KeyID;
                return this.BChar.Skills[1];
            }
            return this.BChar.Skills[2];
            //return base.SkillSelect(ActionCount);
        }
        public override int SpeedChange(Skill skill, int ActionCount, int OriginSpeed)
        {

            return base.SpeedChange(skill, ActionCount, OriginSpeed);
        }
        public void ForceShieldData(ref int maxnum, ref int num)
        {
            maxnum = 300;
            num = 240;
        }
        public string lastSkillKey = string.Empty;
        public int lastMainSkillTurn = 0;
    }


    public static class Summon
    {
        public static IEnumerator SummonEnemy(List<string> keys)
        {
            int num=keys.Count;
            if(num>0)
            {
                List<Vector3> poslist = Summon.CaculatePos_Isomer(num, false, false);//召唤相关
                for(int i=0;i<num;i++)
                {
                    //BattleSystem.instance.CreatEnemy(keys[i], poslist[i], true, false);
                    //yield return new WaitForSeconds(0.25f);
                    yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.NewEnemy(keys[i], poslist[i]));
                }
            }
            //BattleSystem.instance.EnemyTeam.UpdateEnemyList();
            yield return null;
            yield break;
        }
        public static List<Vector3> CaculatePos(int num,bool onlyleft,bool onlyright)
        {
            List<Vector3> list1= new List<Vector3>();
            List<Vector3> list2 = new List<Vector3>();
            foreach(BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {
                list1.Add((bc as BattleEnemy).PosObject.transform.localPosition);
                //Debug.Log("x=" + (bc as BattleEnemy).UI.EnemyUIObject.transform.position.x);
            }
            for(int i=0;i<num;i++)
            {
                int n= 0;
                while(n<50)
                {
                    if (onlyright || !onlyleft)
                    {
                        float posx = n+Summon.midposx;
                        if(list1.FindAll(a=>Math.Abs(a.x-posx)<6).Count <= 0)
                        {
                            Vector3 pos = new Vector3((float)posx, 0f, 0f);
                            list1.Add(pos);
                            list2.Add(pos);
                            break;
                        }
                        else
                        {
                            //Debug.Log("posx=" +posx+"  x="+ list1.Find(a => Math.Abs(a.x - posx) < 6).x);
                        }
                    }
                    if (onlyleft || !onlyright)
                    {
                        float posx = -n + Summon.midposx;
                        if (list1.FindAll(a => Math.Abs(a.x - posx) < 6) .Count<=0)
                        {
                            Vector3 pos = new Vector3((float)posx, 0f, 0f);
                            list1.Add(pos);
                            list2.Add(pos);
                            break;
                        }
                        else
                        {
                            //Debug.Log("posx=" + posx + "  x=" + list1.Find(a => Math.Abs(a.x - posx) < 6).x);
                        }
                    }
                    n++;
                    if(n>=50)
                    {
                        //Debug.Log("错误: pos caculate wrong!");
                    }
                }
            }
            return list2;
        }
        public static List<Vector3> CaculatePos_Isomer(int num, bool onlyleft, bool onlyright)
        {
            List<BattleEnemy> list = new List<BattleEnemy>();
            foreach (BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {
                list.Add(bc as BattleEnemy);
            }
            list.Sort(delegate (BattleEnemy A, BattleEnemy B)
            {
                if (A.PosObject.transform.localPosition.x > B.PosObject.transform.localPosition.x)
                {
                    return 1;
                }
                if (A.PosObject.transform.localPosition.x < B.PosObject.transform.localPosition.x)
                {
                    return -1;
                }
                return 0;
            });
            List<Vector3> list1 = new List<Vector3>();
            List<Vector3> list2 = new List<Vector3>();
            foreach (BattleEnemy bc in list)
            {
                list1.Add((bc as BattleEnemy).PosObject.transform.localPosition);
                //Debug.Log("x=" + (bc as BattleEnemy).UI.EnemyUIObject.transform.position.x);
            }

            bool right = onlyright;
            bool left = onlyleft;   
            if(left==right&& list.FindAll(a=>a.Info.KeyData=="PD_Nimogen"|| a.Info.KeyData == "PD_Mercurows").Count>0)
            {
                int numleft = list.FindIndex(a => a.Info.KeyData == "PD_Nimogen" || a.Info.KeyData == "PD_Mercurows");
                int numright = list.Count - 1 - list.FindLastIndex(a => a.Info.KeyData == "PD_Nimogen" || a.Info.KeyData == "PD_Mercurows");

                for (int i = 0; i < num; i++)
                {
                    if (numleft < numright)
                    {
                        float posx = list1[0].x - 6f;
                        Vector3 pos = new Vector3((float)posx, 0f, 0f);
                        list1.Insert(0, pos);
                        list2.Add(pos);
                        numleft++;
                    }
                    else if (numleft > numright)
                    {
                        float posx = list1[list1.Count - 1].x + 6f;

                        Vector3 pos = new Vector3((float)posx, 0f, 0f);
                        list1.Add(pos);
                        list2.Add(pos);
                        numright++;
                    }
                    else
                    {
                        if (list1[0].x + list1[list1.Count - 1].x > 0)
                        {
                            float posx = list1[0].x - 6f;
                            Vector3 pos = new Vector3((float)posx, 0f, 0f);
                            list1.Insert(0, pos);
                            list2.Add(pos);
                            numleft++;
                        }
                        else
                        {
                            float posx = list1[list1.Count - 1].x + 6f;
                            Vector3 pos = new Vector3((float)posx, 0f, 0f);
                            list1.Add(pos);
                            list2.Add(pos);
                            numright++;
                        }
                    }

                }

                return list2;
            }


            for (int i = 0; i < num; i++)
            {
                if (right || !left)
                {
                    float posx = list1[list1.Count - 1].x + 6f;

                    Vector3 pos = new Vector3((float)posx, 0f, 0f);
                    list1.Insert(0, pos);
                    list2.Add(pos);


                }
                if (left || !right)
                {
                    float posx = list1[0].x - 6f;

                        Vector3 pos = new Vector3((float)posx, 0f, 0f);
                        list1.Add(pos);
                        list2.Add(pos);

                }



            }
            return list2;
        }
        public static IEnumerator ResetPos()
        {
            Vector3 pos = new Vector3 (0f, 0f, 0f);
            Vector3 pos2 = new Vector3(0f, 0f, 0f);
            if (BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Find(a => (a.Info.KeyData == "PD_Nimogen" || a.Info.KeyData == "PD_Mercurows"))!=null)
            {
                pos = (BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Find(a => (a.Info.KeyData == "PD_Nimogen" || a.Info.KeyData == "PD_Mercurows")) as BattleEnemy).GetPos();
                pos2.y = pos.y;
                pos2.z = pos.z;
                //Camera.main.GetComponent<SmoothMove>().Rotate(Quaternion.LookRotation(pos2 - BattleSystem.instance.battlecamera.PosSave));
            }
            else
            {
                pos = (BattleSystem.instance.EnemyTeam.AliveChars_Vanish[0]as BattleEnemy).GetPos();
                pos2.y = pos.y;
                pos2.z = pos.z;
                //Camera.main.GetComponent<SmoothMove>().Rotate(Quaternion.LookRotation(pos2 - BattleSystem.instance.battlecamera.PosSave));

            }

            List<BattleEnemy> list = new List<BattleEnemy>();
            foreach(BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {
                list.Add(bc as BattleEnemy);
            }
            //list.AddRange(BattleSystem.instance.EnemyTeam.AliveChars_Vanish);
            list.Sort(delegate (BattleEnemy A, BattleEnemy B)
            {
                /*
                if (A.UI.EnemyUIObject.transform.position.x > B.UI.EnemyUIObject.transform.position.x)
                {
                    return 1;
                }
                if (A.UI.EnemyUIObject.transform.position.x < B.UI.EnemyUIObject.transform.position.x)
                {
                    return -1;
                }
                */
                if (A.PosObject.transform.localPosition.x > B.PosObject.transform.localPosition.x)
                {
                    return 1;
                }
                if (A.PosObject.transform.localPosition.x < B.PosObject.transform.localPosition.x)
                {
                    return -1;
                }
                return 0;
            });

            foreach (BattleEnemy be in list)
            {
                //Debug.Log(be.Info.Name + "start posx: " + (be as BattleEnemy).PosObject.transform.localPosition.x);


            }

            double sumsize = 0;
            foreach (BattleEnemy A in list)
            {
                sumsize += Summon.ParadiseSize(A.Info.KeyData);
            }
            
            double posx0 = -(0.5*sumsize )+Summon.midposx;
            double posx1 = posx0 + Summon.ParadiseSize(list[0].Info.KeyData);
            List<double> finalx_list = new List<double>();
            List<double> startx_list = new List<double>();
            finalx_list.Add((0.5 * posx0 + 0.5 * posx1));
            startx_list.Add(list[0].PosObject.transform.localPosition.x);
            //startx_list.Add(list[0].PosObject.transform.localPosition.x);
            //Debug.Log(list[0].Info.Name + "new posx: " + (float)(0.5 * posx0 + 0.5 * posx1));

            for (int i = 1; i < list.Count; i++)
            {
                posx0 = posx1;
                posx1 = posx0 + Summon.ParadiseSize(list[i].Info.KeyData);
                finalx_list.Add((double)(0.5 * posx0 + 0.5 * posx1));
                startx_list.Add(list[i].PosObject.transform.localPosition.x);
                //startx_list.Add(list[i].PosObject.transform.localPosition.x);
                //Debug.Log(list[i].Info.Name + "new posx: " + (float)(0.5 * posx0 + 0.5 * posx1));
                //list[i].PosObject.transform.localPosition = new Vector3((float)(0.5 * posx0 + 0.5 * posx1), list[i].gameObject.transform.localPosition.y, list[i].gameObject.transform.localPosition.z);
            }
            for (int j = 1; j <= 15; j++)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    float x = (float)(j * finalx_list[i] / 15  + (15 - j) * startx_list[i] / 15 );
                    //list[i].PosObject.transform.localPosition = new Vector3(x, list[i].UI.EnemyUIObject.transform.position.y, list[i].UI.EnemyUIObject.transform.position.z);
                    list[i].PosObject.transform.localPosition = new Vector3(x, list[i].PosObject.transform.localPosition.y, list[i].PosObject.transform.localPosition.z);

                }
                yield return new WaitForSecondsRealtime(0.02f);
            }
            
            /*
            double posx0 = -sumsize / 2;
            double posx1 = posx0 + Summon.ParadiseSize(list[0].Info.KeyData);
            List<float> finalx_list = new List<float>();
            finalx_list.Add((float)(0.5 * posx0 + 0.5 * posx1));
            //Debug.Log(list[0].Info.Name + "new posx: " + (float)(0.5 * posx0 + 0.5 * posx1));

            list[0].PosObject.GetComponent<SmoothMove>().Move(new Vector3((float)(0.5 * posx0 + 0.5 * posx1), list[0].PosObject.transform.localPosition.y, list[0].PosObject.transform.localPosition.z));
            list[0].PosObject.GetComponent<SmoothMove>().enabled = true;
            //list[0].PosObject.transform.localPosition = new Vector3((float)(0.5 * posx0 + 0.5 * posx1), list[0].PosObject.transform.localPosition.y, list[0].PosObject.transform.localPosition.z);

            for (int i = 1; i < list.Count; i++)
            {
                posx0 = posx1;
                posx1 = posx0 + Summon.ParadiseSize(list[i].Info.KeyData);
                //Debug.Log(list[i].Info.Name + "new posx: " + (float)(0.5 * posx0 + 0.5 * posx1));

                list[i].PosObject.GetComponent<SmoothMove>().Move(new Vector3((float)(0.5 * posx0 + 0.5 * posx1), list[i].PosObject.transform.localPosition.y, list[i].PosObject.transform.localPosition.z));
                list[i].PosObject.GetComponent<SmoothMove>().enabled = true;
                //list[i].PosObject.transform.localPosition = new Vector3((float)(0.5 * posx0 + 0.5 * posx1), list[i].PosObject.transform.localPosition.y, list[i].PosObject.transform.localPosition.z);
            }
            */

            /*
        for (int j = 0; j < 200; j++)
        {

            foreach (BattleEnemy be in list)
            {
                if (!be.PosObject.GetComponent<SmoothMove>().enabled)
                {
                    //flag = true;
                    be.PosObject.GetComponent<SmoothMove>().enabled = true;
                }

            }

            yield return new WaitForFixedUpdate();
        }
            */

            //yield return new WaitForSeconds(2f);
            foreach (BattleEnemy be in list)
            {
                //Debug.Log(be.Info.Name+"end posx: " + (be as BattleEnemy).PosObject.transform.localPosition.x);


            }
            yield return null;
            yield break;
        }

        
        public static double ParadiseSize(string key)
        {
            double num = 720;
            if(key=="PD_Nimogen")
            {
                num = 365;
            }
            else if(key== "PD_Mercurows")
            {
                num = 365;
            }
            else if (key == "PD_Nimogen_shadow")
            {
                num = 365;
            }
            else if (key == "PD_Defender_Isomer")
            {
                num = 644;
            }
            else if (key == "PD_Doppelsoldner_Isomer")
            {
                num = 764;
            }
            else if (key == "PD_Ladon_Isomer")
            {
                num = 1578;
            }
            else if (key == "PD_NytoCommander_Isomer")
            {
                num = 469;
            }
            else if (key == "PD_NytoHammer_Isomer")
            {
                num = 815;
            }
            else if (key == "PD_NytoRF_Isomer")
            {
                num = 524;
            }
            else if (key == "PD_NytoSMG_Isomer")
            {
                num = 468;
            }
            else if (key == "PD_Patmos_Isomer")
            {
                num = 412;
            }
            else if (key == "PD_Patroller_Isomer")
            {
                num = 644;
            }
            else if (key == "PD_Roarer_Isomer")
            {
                num = 1015;
            }
            else if (key == "PD_StreletPlus_Isomer")
            {
                num = 572;
            }
            else if(key== "PD_Strelet_Isomer")
            {
                num = 337;
            }
            double num2 =Math.Max(4,num/110) ;
            //Debug.Log("size:" + num + "/" + num2);
            return num2;
        }
        public static float midposx = 1;
        public static List<string> NimogenEnemyList = new List<string>
        {
        "PD_Defender_Isomer",
        "PD_Doppelsoldner_Isomer",
        "PD_Patmos_Isomer",
        "PD_Roarer_Isomer",
        "PD_StreletPlus_Isomer"
         };
        public static List<string> MercurowsEnemyList = new List<string>
        {
        "PD_Patroller_Isomer",
        "PD_NytoRF_Isomer",
        "PD_NytoSMG_Isomer",
        "PD_NytoCommander_Isomer",
        "PD_NytoHammer_Isomer"
         };
    }
    public class Sex_Isomer_0 : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            this.SummonEnemy(SkillD.Master);
        }
        public void SummonEnemy(BattleChar bc)
        {
            List<string> keylist = new List<string>();
            if (bc.Info.KeyData == "PD_Nimogen" && IsomerOverallSystem.ins.phase >=2)
            {
                keylist.AddRange(IsomerOverallSystem.NimogenEnemyList);
            }
            else if (bc.Info.KeyData == "PD_Mercurows" && IsomerOverallSystem.ins.phase >= 2)
            {
                keylist.AddRange(IsomerOverallSystem.MercurowsEnemyList);
            }
            else
            {
                keylist.Add("PD_Strelet_Isomer");
                keylist.Add("PD_Strelet_Isomer");
            }
            int num = 2;
            if (IsomerOverallSystem.ins.phase == 3)
            {
                num = 3;
            }
            while (keylist.Count > num)
            {
                keylist.RemoveAt(RandomManager.RandomInt(RandomClassKey.Boss, 0, keylist.Count));
            }
            if (keylist.Count > 0)
            {
                BattleSystem.DelayInputAfter(Summon.SummonEnemy(keylist));
            }


            BattleSystem.DelayInputAfter(Summon.ResetPos());
            
        }
    }
    public class Sex_Isomer_1 : Sex_ImproveClone
    {

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            List<BattleChar> list = BattleSystem.instance.EnemyTeam.AliveChars_Vanish.FindAll(a => a.BuffFind("B_PD_Isomer_Reinforce", false));
            Targets.RemoveAll(a=>true);
            Targets.AddRange(list);
        }
    }
    public class Bcl_Isomer_1:Buff
    {
        public override void BuffStat()
        {
            base.BuffStat();
            this.PlusPerStat.Damage = 10 * this.StackNum;
            this.PlusStat.hit = 5 * this.StackNum;
        }
    }



    public class Sex_Nimogen_0 : Sex_ImproveClone,IP_TargetAI, IP_EnemyAttackScene
    {
        public List<BattleChar> TargetAI(BattleEnemy MyBchar)
        {

                return new List<BattleChar>();

        }
        public IEnumerator EnemyAttackScene(Skill UseSkill, List<BattleChar> Targets)
        {
            UnityEngine.Debug.Log("EnemyAttackScene");
            Targets.Clear();
            yield break;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            ((this.BChar as BattleEnemy).Ai as AI_PD_Nimogen).lastMainSkillTurn = BattleSystem.instance.TurnNum;

            Targets.Clear();

            List<BattleChar> list = new List<BattleChar>();
            list.AddRange(BattleSystem.instance.AllyTeam.AliveChars);
            List<Skill> list2 = new List<Skill>();

            foreach (BattleChar battleChar in list)
            {
                if(!battleChar.BuffFind("B_PD_Nimogen_0",false))
                {
                    Skill skill = Skill.TempSkill("S_PD_Nimogen_0_1", battleChar, battleChar.MyTeam);

                    //Skill skill = Skill.TempSkill("S_PD_Nimogen_0_1", this.BChar);
                    Skill_Extended sex = skill.AllExtendeds.Find(a => a is Sex_Nimogen_0_1);
                    BuffTag btg=new BuffTag();
                    btg.BuffData = new GDEBuffData("B_PD_Nimogen_0");
                    btg.PlusTagPer=(int)(this.BChar.GetStat.HIT_CC-battleChar.GetStat.HIT_CC);
                    btg.User = this.BChar;
                    sex.TargetBuff.Add(btg);
                    //if(sex!=null)
                    //{
                        //(sex as Sex_Nimogen_0_1).cha = battleChar;
                    //}

                    list2.Add(skill);
                }

            }
            if(SaveManager.Difficalty==2)
            {
                while (list2.Count > 3)
                {
                    Skill skill = list2.Random(this.BChar.GetRandomClass().Main);
                    list2.Remove(skill);
                }
            }
            else
            {
                while (list2.Count > 4)
                {
                    Skill skill = list2.Random(this.BChar.GetRandomClass().Main);
                    list2.Remove(skill);
                }
            }

            if(list2.Count>0)
            {
                BattleSystem.DelayInput(BattleSystem.I_OtherSkillSelect(list2, new SkillButton.SkillClickDel(this.Del), "", false, false, true, false, true));
            }
        }
        public void Del(SkillButton Mybutton)
        {



            Buff buff=Mybutton.CharData.BuffAdd("B_PD_Nimogen_0", this.BChar, false, 0, false, -1, false);
            /*
            List<Vector3> poslist = Summon.CaculatePos_Isomer(1, false, true);//召唤相关
            BattleChar bc = BattleSystem.instance.CreatEnemy("PD_Nimogen_shadow", poslist[0], true, false);
            BattleSystem.DelayInputAfter(Summon.ResetPos());
            (buff as Bcl_Nimogen_0).concernChars.Add(bc);
            */



            
            /*
            if (SaveManager.Difficalty == 2)
            {
                List<Skill> list = new List<Skill>();
                foreach (Skill skill in BattleSystem.instance.AllyTeam.Skills)
                {
                    if (Mybutton.CharData == skill.MyButton.CharData)
                    {
                        list.Add(skill);
                    }
                }
                if (list.Count >= 1)
                {
                    Skill skill2 = list.Random(this.BChar.GetRandomClass().Main);
                    int num = skill2.AP;
                    if (num >= 2)
                    {
                        num = 2;
                    }
                    skill2.MyButton.WasteForce(false, false);
                    BattleSystem.instance.AllyTeam.AP -= num;
                }
            }
            */
         }
     }
    public class Sex_Nimogen_0_1 : Sex_ImproveClone
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", this.BChar.Info.Name);
            //return base.DescExtended(desc).Replace("&a", this.cha.Info.Name);
        }
        //public BattleChar cha=new BattleChar();
    }
    public class Bcl_Nimogen_0 : Buff,IP_PlayerTurn, IP_SomeOneDead,IP_BuffAddAfter
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", (this.BurstDmg).ToString());
        }
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if(addedbuff==this)
            {
                List<Vector3> poslist = Summon.CaculatePos_Isomer(1, false, true);//召唤相关
                BattleChar bc = BattleSystem.instance.CreatEnemy("PD_Nimogen_shadow", poslist[0], true, false);
                BattleSystem.DelayInputAfter(Summon.ResetPos());
                this.concernChars.Add(bc);
            }

        }
        public BattleChar mainChar
        {
            get
            {
                if(this.View)
                {
                    if(BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Find(a=>a.Info.KeyData=="PD_Nimogen") != null)
                    {
                        return BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Find(a => a.Info.KeyData == "PD_Nimogen");
                        //this.BChar = BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Find(a => a.Info.KeyData == "PD_Nimogen");
                    }
                    return this.Usestate_L;
                }
                if(this.concernChars.Count>0)
                {
                    return this.concernChars[0];
                }
                return new BattleChar();
            }
        }
        public int BurstDmg
        {
            get
            {
                return (int)((1 + 0.3 * this.turns) * this.mainChar.GetStat.atk);
            }
        }
        public override void Init()
        {
            base.Init();
            this.PlusStat.Stun = true;
        }
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            this.MarkSkill();
        }
        public void Turn()
        {
            this.turns++;
            this.MarkSkill();
        }
        public void MarkSkill()
        {
            foreach(BattleChar bc in this.BChar.MyTeam.AliveChars)
            {
                if(bc !=this.BChar)
                {
                    List<Skill> list = new List<Skill>();
                    list.AddRange(this.BChar.MyTeam.Skills.FindAll(a => a.Master == bc));
                    if (list.Count > 0)
                    {
                        Skill skill = list.Random(this.BChar.GetRandomClass().Main);
                        EX_Nimogen_0 sex = skill.ExtendedAdd(Skill_Extended.DataToExtended("EX_PD_Nimogen_0")) as EX_Nimogen_0;
                        //sex.user = this.mainChar;
                        sex.mainbuff = this;
                    }
                    else
                    {
                        List<GDESkillData> list2 = new List<GDESkillData>();
                        foreach (GDESkillData gdeskillData in PlayData.ALLSKILLLIST)
                        {
                            if ((gdeskillData.User == bc.Info.KeyData || gdeskillData.Category.Key == GDEItemKeys.SkillCategory_PublicSkill)&& !gdeskillData.Disposable && !gdeskillData.Except && gdeskillData.UseAp>0)
                            {
                                list2.Add(gdeskillData);
                                if (gdeskillData.User == bc.Info.KeyData)
                                {
                                    list2.Add(gdeskillData);
                                    list2.Add(gdeskillData);
                                    list2.Add(gdeskillData);
                                    list2.Add(gdeskillData);
                                }
                            }
                        }
                        GDESkillData item = list2.Random(this.BChar.GetRandomClass().Main);
                        Skill skill = Skill.TempSkill(item.KeyID,bc, bc.MyTeam);
                        skill.isExcept = true;
                        skill.AutoDelete = 1;
                        EX_Nimogen_0 sex = skill.ExtendedAdd(Skill_Extended.DataToExtended("EX_PD_Nimogen_0")) as EX_Nimogen_0;
                        //sex.user = this.mainChar;
                        sex.mainbuff = this;
                        BattleSystem.instance.AllyTeam.Add(skill, true);
                    }
                }

            }
        }

        public void SomeOneDead(BattleChar DeadChar)
        {
            
            if (this.concernChars.FindAll(a => BattleSystem.instance.EnemyTeam.AliveChars.Contains(a) && a != DeadChar).Count<=0 )
            //if(BattleSystem.instance.EnemyTeam.AliveChars.FindAll(a=>a.Info.KeyData== "PD_Nimogen_shadow" && a!=DeadChar).Count<=0)
            {
                this.SelfDestroy();

            }   
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.frame++;
            if(this.frame>10)
            {
                this.frame = 0;
                for (int i = 0; i < this.concernChars.Count; i++)
                {
                    BattleChar bc = this.concernChars[i];
                    if (bc != null && !bc.IsDead)
                    {
                        if (bc.BuffFind("B_PD_Nimogen_shadow_h", false))
                        {
                            Bcl_Nimogen_shadow_h bcl = bc.BuffReturn("B_PD_Nimogen_shadow_h", false) as Bcl_Nimogen_shadow_h;
                            bcl.frame = 0;
                        }
                        else
                        {
                            this.concernChars.RemoveAt(i);
                            i--;
                        }
                    }
                    else
                    {
                        this.concernChars.RemoveAt(i);
                        i--;
                    }
                }
            }
            
        }
        public override void SelfdestroyPlus()
        {
            base.SelfdestroyPlus();
            foreach (Skill skill in BattleSystem.instance.AllyTeam.Skills)
            {
                foreach (Skill_Extended sex in skill.AllExtendeds)
                {
                    if (sex is EX_Nimogen_0 ex)
                    {
                        if (ex.mainbuff == this)
                        {
                            ex.SelfDestroy();
                        }
                    }
                }
            }
        }
        public int frame = 0;
        public int turns = 0;
        public List<BattleChar> concernChars=new List<BattleChar>();
    }
    public class EX_Nimogen_0 : Sex_ImproveClone,IP_TurnEnd
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a", (this.mainbuff.BurstDmg).ToString()).Replace("&b", (this.BChar.Info.Name).ToString());
        }
        public override void Init()
        {
            base.Init();    
            this.OnePassive = true;
        }
        public void TurnEnd()
        {
            if(!this.used)
            {
                
                this.used = true;
                Skill skill = Skill.TempSkill("S_PD_Nimogen_0_2", this.User, this.User.MyTeam);
                skill.NeverCri = true;
                Skill_Extended sex = skill.ExtendedAdd(new Skill_Extended());
                sex.PlusPerStat.Damage = this.mainbuff.turns * 30;
                this.User.ParticleOut(skill, this.BChar);
                this.SelfDestroy();
            }

        }
        public BattleChar User
        {
            get
            {
                return this.mainbuff.mainChar;
            }
        }
        //public BattleChar user=new BattleChar();
        public Bcl_Nimogen_0 mainbuff = new Bcl_Nimogen_0();
        public bool used = false;
    }
    public class Bcl_Nimogen_shadow_h : Buff//,IP_PlayerTurn
    {
        /*
        public void Turn()
        {
            this.PlusPerStat.Damage += 30;
        }
        */
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(BattleSystem.instance!=null)
            {
                this.frame++;
                if(this.frame>=30)
                {
                    this.BChar.Dead();
                }
            }
        }
        public int frame = 0;
    }

    public class Sex_Mercurows_0 : SkillExtended_ParadiseShield
    {
        public override void AddShieldPersent()
        {
            base.AddShieldPersent();
            base.SetShield(false, 0, true, (int)(0.2*this.BChar.GetStat.maxhp));
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            ((this.BChar as BattleEnemy).Ai as AI_PD_Mercurows).lastMainSkillTurn = BattleSystem.instance.TurnNum;

        }
    }
    public class Bcl_Mercurows_0 : Buff,IP_BuffUpdate,IP_HPChange1,IP_ViewBuffs_After
    {
        public override string DescExtended()
        {
            /*
            if(this.View)
            {
                return base.DescExtended().Replace("&a", ((int)(this.healper * 100)).ToString()).Replace("&b", ((int)(this.forceper * 100)).ToString()).Replace("&c", this.Usestate_L.Info.Name).Replace("&d","\n(具体伤害及恢复比例依据目标属性变化。)");
            }
            */
            return base.DescExtended().Replace("&a", ((int)(this.healper*100)).ToString()).Replace("&b", ((int)(this.forceper * 100)).ToString()).Replace("&c", this.Usestate_L.Info.Name).Replace("&d", "");
        }
        public void ViewBuffs_After(Skill skill)
        {
            //UnityEngine.Debug.Log("ViewBuffs_inbuff_1");
            if(this.View)
            {

                    CastingSkill cast = BattleSystem.instance.EnemyCastSkills.Find(a => a.skill == skill);
                if(cast != null)
                {
                    //UnityEngine.Debug.Log("ViewBuffs_inbuff_2");
                    //UnityEngine.Debug.Log(cast.Target.Info.Name);
                    this.BChar = cast.Target;
                    this.MyChar = cast.Target.Info;
                    this.Init();
                }
                    
            }
        }
        public override void Init()
        {
            base.Init();
            /*
            if(this.View)
            {
                this.PlusDamageTick = (int)(this.Usestate_L.GetStat.atk * 0.5 );
                this.healper = 1;
                this.forceper = 0.5f;
            }
            else
            */
            
                this.PlusDamageTick = (int)(this.Usestate_L.GetStat.atk * 0.5 + this.BChar.GetStat.maxhp * 0.3);
                this.healper = (70 + this.BChar.GetStat.reg * 5) / 100;
                this.forceper = (50 + this.BChar.GetStat.atk * 3) / 100;
            
            

        }
        public void BuffUpdate(Buff MyBuff)
        {
            this.PlusDamageTick = (int)(this.Usestate_L.GetStat.atk * 0.5 + this.BChar.GetStat.maxhp * 0.3);
            this.healper = (70 + this.BChar.GetStat.reg * 5) / 100;
            this.forceper= (50 + this.BChar.GetStat.atk * 3) / 100;


        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.frame++;
            if(this.frame>20 || this.frame<0)
            {
                this.frame = 0;
                if (this.Usestate_L.BarrierHP <= 0)
                {
                    this.SelfDestroy();
                }
            }

        }
        private int frame = 0;
        public void HPChange1(BattleChar Char, bool Healed, int PreHPNum, int NewHPNum)
        {
            if(Char==this.BChar)
            {
                int changehp=Math.Abs(PreHPNum-NewHPNum);

                /*
                if(!this.Usestate_L.BuffFind("B_PD_Paradise_s",false))
                {
                    this.Usestate_L.BuffAdd("B_PD_Paradise_s",this.Usestate_L,false,0,false,-1,false);
                }
                Buff buff = this.Usestate_L.BuffReturn("B_PD_Paradise_s", false);
                buff.BarrierHP += (int)(this.healper * changehp);
                */

                this.Usestate_L.Heal(this.Usestate_L, (this.healper * changehp), false, false, null);
                
                if (this.Usestate_L.BuffFind("B_PD_Paradise_s", false))
                {
                    Bcl_Paradise_force bclp1 = this.Usestate_L.BuffReturn("B_PD_Paradise_force", false) as Bcl_Paradise_force;
                    bclp1.ChangeForceShield((int)(this.forceper * changehp));
                }
                
            }
        }
        public float healper = 0;
        public float forceper = 0;
    }



    public class Sex_Nimogen_1: Sex_ImproveClone
    {

    }
    public class Sex_Mercurows_1 : Sex_ImproveClone
    {

    }
    public class Sex_Nimogen_2 : Sex_ImproveClone 
    {

    }
    public class Sex_Mercurows_2 : Sex_ImproveClone
    {

    }

    /*
    public class SkillExtended_ParadiseShield : Sex_ImproveClone//, IP_BuffUpdate//paradise base
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
            buffTag.BuffData = new GDEBuffData("B_PD_Paradise_s").ShallowClone();
            buffTag.User = this.BChar;
            //this.TargetBuff.Add(buffTag);
            BuffTag buffTag2 = new BuffTag();
            buffTag2.BuffData = new GDEBuffData("B_PD_Paradise_s").ShallowClone();
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
            Buff buff = Target.BuffAdd("B_PD_Paradise_s", this.BChar, false, 0, false, -1, false);
            buff.BarrierHP += num;

        }
        private bool Tar;
        private int shieldTar;
        private bool Self;
        private int shieldSelf;
        public BuffTag MainBuffTar;
        public BuffTag MainBuffSelf;
    }

    */
    /*
    public class Bcl_Paradise_s : Buff,IP_BuffObject_Updata//paradise base
    {
        public void BuffObject_Updata(BuffObject obj)
        {
            obj.StackText.text = this.BarrierHP.ToString();
        }
    }
    */
    /*
    public interface IP_ForceShieldData
    {
        // Token: 0x06000001 RID: 1
        void ForceShieldData(ref int maxnum, ref int num);

    }
    public class Bcl_Paradise_force : Buff,IP_DamageTakeChange,IP_BeforeHit, IP_Hit,IP_BuffObject_Updata//paradise base
    {
        public override string DescExtended()
        {
            if(this.maxValue > 0)
            {
                return base.DescExtended().Replace("&a", this.value.ToString()).Replace("&b", this.maxValue.ToString()).Replace("&c", ((int)(100 * this.value / this.maxValue)).ToString()).Replace("&d", ((int)(50 * this.value / this.maxValue)).ToString());

            }
            else
            {
                return base.DescExtended().Replace("&a", "0").Replace("&b", "0").Replace("&c", "0").Replace("&d", "0");

            }
        }
        public void BuffObject_Updata(BuffObject obj)
        {
            if (this.maxValue > 0)
            {
                obj.StackText.text = ((int)(100 * this.value / this.maxValue)).ToString() + "%";

            }
            else
            {
                obj.StackText.text =  "0%";
            }
        }
        public override void BuffOneAwake()

        {
            base.BuffOneAwake();    
            if(this.BChar is BattleEnemy)
            {
                AI ai = (this.BChar as BattleEnemy).Ai;
                int maxnum = 0;
                int num = 0;
                IP_ForceShieldData ip_patch = ai as IP_ForceShieldData;
                if (ip_patch != null)
                {
                    ip_patch.ForceShieldData(ref maxnum, ref num);
                }
                //ai.ForceShieldData(ref maxnum, ref num);
                this.SetForceShield(maxnum, num);
            }
            else
            {
                this.SetForceShield(0, 0);
                this.candistroy = true;
            }

        }
        public override void TurnUpdate()

        {
            base .TurnUpdate();
            if (this.BChar is BattleEnemy)
            {
                (this.BChar as BattleEnemy).UseCustomPos = true;

            }
        }
        public void BeforeHit(SkillParticle SP, int DMG, bool Cri)
        {
            if(SP.TargetChar.Contains(this.BChar))
            {
                this.saveDmg = DMG;
            }
        }
        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            if(this.saveDmg<=0)
            {
                return;
            }
            if(SP.UseStatus == BattleSystem.instance.AllyTeam.LucyChar)// && SP.SkillData.TargetTypeKey!="all_enemy" && SP.SkillData.TargetTypeKey != "all_ally" && SP.SkillData.TargetTypeKey != "all_allyorenemy" && SP.SkillData.TargetTypeKey != "all")
            {
                this.ChangeForceShield(-3*this.saveDmg);
            }
            else
            {
                this.ChangeForceShield(-this.saveDmg);
            }
            this.saveDmg = 0;
        }
        public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {

            if(User !=BattleSystem.instance.AllyTeam.LucyChar && Hit==this.BChar && this.maxValue>0 && Dmg>0)
            {
                float per=(float)(this.maxValue-this.value)/this.maxValue;
                if(NODEF)
                {
                    per = (float)(this.maxValue - this.value/2) / this.maxValue;
                }
                //Debug.Log(per);
                per = Mathf.Clamp01(per);
                //Debug.Log(per);
                float num = Math.Max(1,Dmg * per);
                return (int)num;
            }
            return Dmg;
        }
        public void SetForceShield(int maxNum,int num=-1)
        {
            this.maxValue = Math.Max(0,maxNum);
            if(num>=0)
            {
                this.value = Mathf.Clamp(num, 0, this.maxValue);
            }
            else
            {
                this.value = Mathf.Clamp(this.value, 0, this.maxValue);
            }
        }
        public void ChangeForceShield(int num)
        {
            this.value = Mathf.Clamp(this.value+num, 0, this.maxValue);
            if(this.candistroy && this.value<=0)
            {
                this.SelfDestroy();
            }
        }
        public void ChangeForceShield_per(int num)
        {
            this.value = Mathf.Clamp(this.value + (int)(num*this.maxValue/100), 0, this.maxValue);
            if (this.candistroy && this.value <= 0)
            {
                this.SelfDestroy();
            }
        }
        public int value = 0;
        public int maxValue = 0;

        public int saveDmg = 0;
        public bool candistroy = false;

        public override void FixedUpdate()//显示护盾白色条
        {
            base.FixedUpdate();
            if(this.BChar is BattleEnemy)
            {
                if (this.MainBE == null)
                {
                    this.MainBE = (this.BChar as BattleEnemy);
                    this.MainBE.MasterBossSmallHPBar.gameObject.SetActive(true);
                }
                this.MainBE.MasterBossSmallHPBar.MainBar.fillAmount = Misc.NumToPer((float)this.BChar.GetStat.maxhp, (float)this.BChar.BarrierHP) * 0.01f;
            }

        }
        public BattleEnemy MainBE;
    }
    */
    public class Bcl_Isomer_Reinforce : Buff
    {
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();

            Button button = this.BuffIcon.AddComponent<Button>();
            button.onClick.AddListener(new UnityAction(this.Go));

        }
        public void Go()
        {
            //Debug.Log("go");
            //BattleSystem.DelayInputAfter(Summon.ResetPos());
            //Summon.AutoPosCalculate();
            BattleEnemy be = this.BChar as BattleEnemy;
            Debug.Log(be.Info.Name);
            try
            {
                Debug.Log("test1_1:" + (be.gameObject.transform.parent.parent.parent.gameObject != null));
                Debug.Log("test1_1:" + be.gameObject.transform.parent.parent.parent.gameObject.name);
            }
            catch { }
            try
            {
                Debug.Log("test1_2:" + (be.CharObject.transform.parent.parent.parent.gameObject != null));
                Debug.Log("test1_2:" +  be.CharObject.transform.parent.parent.parent.gameObject.name);
            }
            catch { }
            try
            {
                Debug.Log("test1_3:" + (be.PosObject.transform.parent.parent.parent.gameObject != null));
                Debug.Log("test1_3:" + be.PosObject.transform.parent.parent.parent.gameObject.name);

                Debug.Log("test1_3:" + (be.PosObject.transform.childCount));
            }
            catch { }
            try
            {
                Debug.Log("test1_4:" + (be.UI.EnemyUIObject.transform.parent.parent.parent.gameObject != null));
                Debug.Log("test1_4:" + be.UI.EnemyUIObject.transform.parent.parent.parent.gameObject.name);
            }
            catch { }
            //Debug.Log("test1_1:" + be.ActAlign.GetComponent<SpriteRenderer>() != null);
            //Debug.Log("test1_x1:" + be.gameObject.GetComponent<SpriteRenderer>().size.x);
            //Debug.Log("test1_x2:" + be.gameObject.GetComponent<SpriteRenderer>().size.y);


        }
    }
   

    
    public static class Aggro
    {
        public static BattleChar AgrroReturn(BattleChar user, List<BattleAlly> Choices = null)
        {
            List<BattleAlly> list = new List<BattleAlly>();
            foreach (BattleAlly battleAlly in user.BattleInfo.AllyList)
            {
                if (!battleAlly.GetStat.Vanish && Choices.Contains(battleAlly))
                {
                    list.Add(battleAlly);
                }
            }
            if (user.BuffFind("Taunt", false))
            {
                Buff buff = user.BuffReturn("Taunt", false);
                if (Choices.Contains(buff.StackInfo[0].UseState) && !buff.StackInfo[0].UseState.GetStat.Vanish && !user.IsDead)
                {
                    return buff.StackInfo[0].UseState;
                }
            }

            list.RemoveAll(a => !Choices.Contains(a));

            int num = 0;
            foreach (BattleAlly battleAlly2 in list)
            {
                int num2 = 100;
                if (battleAlly2.GetStat.AggroPer > 0)
                {
                    num2 += battleAlly2.GetStat.AggroPer * (user.BattleInfo.AllyTeam.AliveChars.Count - 1);
                }
                else
                {
                    num2 += battleAlly2.GetStat.AggroPer;
                }
                if (num2 <= 0)
                {
                    num2 = 0;
                }
                num += num2;
            }
            int num3 = RandomManager.RandomInt(user.GetRandomClass().Target, 0, num);
            if (num > 1)
            {
                int num4 = 0;
                int num5 = 0;
                using (List<BattleAlly>.Enumerator enumerator = list.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        BattleAlly battleAlly3 = enumerator.Current;
                        int num6 = 100;
                        if (battleAlly3.GetStat.AggroPer > 0)
                        {
                            num6 += battleAlly3.GetStat.AggroPer * (user.BattleInfo.AllyTeam.AliveChars.Count - 1);
                        }
                        else
                        {
                            num6 += battleAlly3.GetStat.AggroPer;
                        }
                        if (num6 > 0)
                        {
                            if (num6 <= 0)
                            {
                                num6 = 0;
                            }
                            num5 += num6;
                            if (num3 >= num4 && num3 < num5)
                            {
                                return battleAlly3;
                            }
                            num4 += num6;
                        }
                    }
                    goto IL_273;
                }
                goto IL_252;
            IL_273:
                if (list.Count != 0)
                {
                    return list.Random(user.GetRandomClass().Target);
                }
                return null;
            }
        IL_252:
            if (list.Count != 0)
            {
                return list.Random(user.GetRandomClass().Target);
            }
            return null;
        }

        public static BattleChar AgrroReturnExcept(BattleChar user, List<BattleAlly> Excepts )
        {
            List<BattleAlly> list = new List<BattleAlly>();
            foreach (BattleAlly battleAlly in user.BattleInfo.AllyList)
            {
                if (!battleAlly.GetStat.Vanish && !Excepts.Contains(battleAlly))
                {
                    list.Add(battleAlly);
                }
            }
            if (user.BuffFind("Taunt", false))
            {
                Buff buff = user.BuffReturn("Taunt", false);
                if (!Excepts.Contains(buff.StackInfo[0].UseState) && !buff.StackInfo[0].UseState.GetStat.Vanish && !user.IsDead)
                {
                    return buff.StackInfo[0].UseState;
                }
            }

            list.RemoveAll(a => Excepts.Contains(a));

            int num = 0;
            foreach (BattleAlly battleAlly2 in list)
            {
                int num2 = 100;
                if (battleAlly2.GetStat.AggroPer > 0)
                {
                    num2 += battleAlly2.GetStat.AggroPer * (user.BattleInfo.AllyTeam.AliveChars.Count - 1);
                }
                else
                {
                    num2 += battleAlly2.GetStat.AggroPer;
                }
                if (num2 <= 0)
                {
                    num2 = 0;
                }
                num += num2;
            }
            int num3 = RandomManager.RandomInt(user.GetRandomClass().Target, 0, num);
            if (num > 1)
            {
                int num4 = 0;
                int num5 = 0;
                using (List<BattleAlly>.Enumerator enumerator = list.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        BattleAlly battleAlly3 = enumerator.Current;
                        int num6 = 100;
                        if (battleAlly3.GetStat.AggroPer > 0)
                        {
                            num6 += battleAlly3.GetStat.AggroPer * (user.BattleInfo.AllyTeam.AliveChars.Count - 1);
                        }
                        else
                        {
                            num6 += battleAlly3.GetStat.AggroPer;
                        }
                        if (num6 > 0)
                        {
                            if (num6 <= 0)
                            {
                                num6 = 0;
                            }
                            num5 += num6;
                            if (num3 >= num4 && num3 < num5)
                            {
                                return battleAlly3;
                            }
                            num4 += num6;
                        }
                    }
                    goto IL_273;
                }
                goto IL_252;
            IL_273:
                if (list.Count != 0)
                {
                    return list.Random(user.GetRandomClass().Target);
                }
                return null;
            }
        IL_252:
            if (list.Count != 0)
            {
                return list.Random(user.GetRandomClass().Target);
            }
            return null;
        }
    }




    public class Bcl_Roarer_p_Isomer : Buff
    {
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (this.target == null)
            {
                foreach (BattleEnemy battleEnemy in BattleSystem.instance.EnemyList)
                {
                    if (battleEnemy.Info.KeyData == "PD_Nimogen")
                    {
                        this.target = battleEnemy;
                        break;
                    }
                }
            }
            if (this.target != null && !this.target.BuffFind(GDEItemKeys.Buff_B_Common_Protection_Enemy, false))
            {
                this.target.BuffAdd(GDEItemKeys.Buff_B_Common_Protection_Enemy, this.BChar, false, 0, false, -1, false);
            }
        }

        // Token: 0x04000F2A RID: 3882
        private BattleChar target;
    }


    public class Sex_Reward_0 : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            foreach (BattleChar tar in Targets)
            {
                Bcl_Paradise_force buff = tar.BuffAdd("B_PD_Paradise_force", this.BChar, false, 0, false, -1, false) as Bcl_Paradise_force;
                buff.SetForceShield(2*tar.GetStat.maxhp, 2*tar.GetStat.maxhp);
            }
        }
    }
    public class Sex_NimogenR_0 : Sex_ImproveClone
    {

    }
    public class Bcl_NimogenR_0 : Buff,IP_TurnEnd
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a",this.Usestate_L.GetStat.atk.ToString());
        }
        public override void TurnUpdate()
        {
            base.TurnUpdate();
            List<BattleChar> list1 = new List<BattleChar>();
            if (this.BChar is BattleEnemy)
            {
                List<BattleEnemy> list = new List<BattleEnemy>();
                foreach (BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars)
                {
                    list.Add(bc as BattleEnemy);
                }
                list.Sort(delegate (BattleEnemy A, BattleEnemy B)
                {
                    if (A.PosObject.transform.localPosition.x > B.PosObject.transform.localPosition.x)
                    {
                        return 1;
                    }
                    if (A.PosObject.transform.localPosition.x < B.PosObject.transform.localPosition.x)
                    {
                        return -1;
                    }
                    return 0;
                });

                foreach (BattleChar bc in list)
                {
                    list1.Add(bc);
                }
            }
            else if(BChar is BattleAlly)
            {
                list1.AddRange(BattleSystem.instance.AllyTeam.AliveChars);
            }

            Skill skill = Skill.TempSkill("S_PD_NimogenR_0_2",this.Usestate_L,this.Usestate_L.MyTeam);
            skill.isExcept = true;
            skill.FreeUse = true;
            skill.PlusHit = true;
            BattleSystem.DelayInput(this.Plushit(new List<BattleChar> { this.BChar }, skill));

            int num = 1;
            int mid = 0;//list1.FindIndex(a=>a==this.BChar);
            bool flag = true;
            while(flag && mid>=0)
            {
                List<BattleChar> list2= new List<BattleChar>();
                if(mid-num>=0 && mid - num<list1.Count)
                {
                    list2.Add(list1[mid - num]);
                }
                if (mid + num >= 0 && mid + num < list1.Count)
                {
                    list2.Add(list1[mid + num]);
                }
                if(list2.Count>0)
                {
                    BattleSystem.DelayInput(this.Plushit(list2, skill));

                }
                else
                {
                    break;
                }
                num++;
            }
        }

        public IEnumerator Plushit(List<BattleChar> tars, Skill skill)//追加射击
        {

            this.BChar.ParticleOut( skill, tars);
            yield return new WaitForSecondsRealtime(0.6f);
            yield break;
        }

        public void TurnEnd()
        {
            this.SelfDestroy();
        }
    }

    public class RelicNimogen : PassiveBase, IP_EnemyAwake
    {
        // Token: 0x060050E8 RID: 20712 RVA: 0x002013E8 File Offset: 0x001FF5E8
        public void EnemyAwake(BattleChar Enemy)
        {
            Enemy.BuffAdd("B_PD_RelicNimogen_0", BattleSystem.instance.AllyTeam.DummyChar, false, 0, false, -1, false);
        }
        /*

        */
    }
    public class Bcl_RelicNimogen_0 : Buff
    {
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();

            Button button = this.BuffIcon.AddComponent<Button>();
            button.onClick.AddListener(new UnityAction(this.Go));

        }
        public void Go()
        {
            this.SelfDestroy(false);
            this.BChar.BuffAdd("B_PD_RelicNimogen_0", BattleSystem.instance.AllyTeam.DummyChar, false, 0, false, -1, false);
        }
        public override void TurnUpdate()
        {
            base.TurnUpdate();
            if(this.BChar.GetStat.Stun)
            {
                BattleSystem.DelayInputAfter(this.Delay());
            }
        }
        public IEnumerator Delay()
        {
            List<BattleChar> list1 = new List<BattleChar>();
            if (this.BChar is BattleEnemy)
            {
                //List<BattleEnemy> list = (this.BChar as BattleEnemy).EnemyPosNum(out num1);

                List<BattleEnemy> list = new List<BattleEnemy>();
                foreach (BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars)
                {
                    list.Add(bc as BattleEnemy);
                }
                list.Sort(delegate (BattleEnemy A, BattleEnemy B)
                {
                    if (A.PosObject.transform.localPosition.x > B.PosObject.transform.localPosition.x)
                    {
                        return 1;
                    }
                    if (A.PosObject.transform.localPosition.x < B.PosObject.transform.localPosition.x)
                    {
                        return -1;
                    }
                    return 0;
                });

                foreach (BattleChar bc in list)
                {
                    list1.Add(bc);
                }
            }
            else if (BChar is BattleAlly)
            {
                list1.AddRange(BattleSystem.instance.AllyTeam.AliveChars);
            }

            int dmg= (int)this.BChar.GetStat.atk;
            //UnityEngine.Debug.Log("dmg");
            Skill skill = Skill.TempSkill("S_PD_NimogenR_0_2", BattleSystem.instance.AllyTeam.LucyChar, BattleSystem.instance.AllyTeam);
            skill.isExcept = true;
            skill.FreeUse = true;
            skill.PlusHit = true;
            Skill_Extended sex = skill.ExtendedAdd_Battle(new Skill_Extended());
            sex.SkillBasePlus.Target_BaseDMG = dmg;

            BattleSystem.DelayInput(this.Plushit(new List<BattleChar> { this.BChar }, skill));

            int num = 1;
            int mid = list1.FindIndex(a => a == this.BChar);
            bool flag = true;
            while (flag && mid >= 0)
            {
                List<BattleChar> list2 = new List<BattleChar>();
                if (mid - num >= 0 && mid - num < list1.Count)
                {
                    list2.Add(list1[mid - num]);
                }
                if (mid + num >= 0 && mid + num < list1.Count)
                {
                    list2.Add(list1[mid + num]);
                }
                if (list2.Count > 0)
                {
                    BattleSystem.DelayInput(this.Plushit(list2, skill));

                }
                else
                {
                    break;
                }
                num++;
            }
            yield break;
        }
        public IEnumerator Plushit(List<BattleChar> tars, Skill skill)//追加射击
        {

            this.BChar.ParticleOut(skill, tars);
            yield return new WaitForSecondsRealtime(0.4f);
            yield break;
        }
    }

    public class RelicMercurows : PassiveBase, IP_DealDamage
    {
        // Token: 0x060050E8 RID: 20712 RVA: 0x002013E8 File Offset: 0x001FF5E8
        //public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        public void DealDamage(BattleChar Take, int Damage, bool IsCri, bool IsDot)
        {
            //UnityEngine.Debug.Log("DamageTake1");
            if(Take is BattleEnemy)
            {
                //UnityEngine.Debug.Log("DamageTake2");
                int num = Damage;
                if(IsDot)
                {
                    num = 2 * num;
                }
                float num2 = (float)(num * 0.15);
                if(num2 > 0)
                {
                    foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars)
                    {
                        if (BattleSystem.instance.AllyTeam.AliveChars.Find(a => ((float)a.HP/ (float)a.GetStat.maxhp) < ((float)bc.HP / (float)bc.GetStat.maxhp)) == null)
                        {
                            bc.Heal(BattleSystem.instance.AllyTeam.DummyChar, num2, false, false);//, new BattleChar.ChineHeal());
                            break;
                        }
                    }
                }



                    
                
            }
        }
    }
}
