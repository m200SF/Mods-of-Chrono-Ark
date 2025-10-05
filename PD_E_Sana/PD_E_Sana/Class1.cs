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
using SF_Standard;
using Paradise_Basic;
using static UnityEngine.UI.Image;
using static DemoToonVFX;
using Newtonsoft.Json;
using PD_E_Sana_SaveData;
using UnityEngine.Events;
using Spine.Unity;
using Spine;



namespace PD_E_Sana
{
    [PluginConfig("M200_PD_E_Sana_CharMod", "PD_E_Sana_Mod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class PD_E_Sana_CharacterModPlugin : ChronoArkPlugin
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
        public class E_SanaDef : ModDefinition
        {
        }
    }

    [HarmonyPatch(typeof(FieldSystem))]
    
    public static class StageStartPlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPatch("StageStart")]
        [HarmonyPrefix]
        public static void StageStart_patch(FieldSystem __instance, string StageKey,out List<string> __state)
        {

            __state = new List<string>();

            if (/*PlayData.TSavedata.StageNum == 0 &&*/ !PlayData.TSavedata.GameStarted)
            {
                if (!PlayData.TSavedata.IsLoaded)
                {
                    SanaTempSaveData savedata= SanaCommonData.GetFairyGameData();
                    if(!savedata.keepItemAdded)
                    {
                        savedata.keepItemAdded = true;



                        PD_E_Sana_Save save = PD_E_Sana_SaveData.PD_E_Sana_Save.Load();
                        /*
                        foreach (string str in save.NextRun_ItemKeys)
                        {
                            try
                            {
                                PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(str));
                            }
                            catch { }
                        }
                        */
                        __state.AddRange(save.NextRun_ItemKeys);
                        //save.NextRun_ItemKeys.Clear();
                        save.Save();
                    }

                    
                }
            }
        }
        [HarmonyPatch("StageStart")]
        [HarmonyPostfix]
        public static void StageStart_After_patch(FieldSystem __instance, string StageKey, List<string> __state)
        {


                if (__state.Count>0)
                {
                    //PD_E_Sana_Save save = PD_E_Sana_SaveData.PD_E_Sana_Save.Load();
                    foreach (string str in __state)
                    {
                        try
                        {
                            PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(str));
                        }
                        catch { }
                    }
                    
                    //save.NextRun_ItemKeys.Clear();
                    //save.Save();
                }
            
        }
    }
    [HarmonyPatch(typeof(SaveManager))]
    [HarmonyPatch("ProgressOneSave")]
    public static class ProgressOneSave_Plugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void ProgressOneSave_patch(SaveManager __instance)
        {
            if (PlayData.TSavedata.StageNum == 0 && !PlayData.TSavedata.GameStarted)
            {
                if (!PlayData.TSavedata.IsLoaded)
                {
                  //Mark_Debug.Log("拦截保存_保存进程");
                    return;
                }
            }

          //Mark_Debug.Log("保存进程");
          //Mark_Debug.Log("保存进程");
          //Mark_Debug.Log("保存进程");

            SaveFunction.Save();
        }
        
    }
    [HarmonyPatch(typeof(DataCollectMgr))]
    [HarmonyPatch("GameEnd")]
    public static class GameEnd_Plugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void GameEnd_patch(bool Win)
        {
            if (PlayData.TSavedata.StageNum == 0 && !PlayData.TSavedata.GameStarted)
            {
                if (!PlayData.TSavedata.IsLoaded)
                {
                  //Mark_Debug.Log("拦截保存_退出进程");
                    return;
                }
            }

          //Mark_Debug.Log("退出进程");
          //Mark_Debug.Log("退出进程");
          //Mark_Debug.Log("退出进程");
            SaveFunction.Save();
        }

    }


    public static class SaveFunction
    {
        public static void Save()
        {
            try
            {


                PD_E_Sana_Save save = PD_E_Sana_SaveData.PD_E_Sana_Save.Load();

                save.NextRun_ItemKeys.Clear();

                List<ItemBase> items2 = PartyInventory.InvenM.InventoryItems.FindAll(a => a != null && SaveFunction.keepItemList.FindAll(b => a.itemkey == b).Count > 0);
              //Mark_Debug.Log("保存节点1");
              //Mark_Debug.Log("保存节点1");
              //Mark_Debug.Log("保存节点1");
                foreach (ItemBase item in items2)
                {
                    //save.NextRun_ItemKeys.Add(item.itemkey);

                    for (int i = 0; i < item.StackCount; i++)
                    {
                        save.NextRun_ItemKeys.Add(item.itemkey);
                    }
                }
              //Mark_Debug.Log("保存节点2");
              //Mark_Debug.Log("保存节点2");
              //Mark_Debug.Log("保存节点2");
                save.Save();
            }
            catch
            {
              //Mark_Debug.Log("保存错误");
              //Mark_Debug.Log("保存错误");
              //Mark_Debug.Log("保存错误");
            }
        }
        public static List<string> keepItemList = new List<string> { "Item_Sana_Chest" };
    }

    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("Effect")]
    public static class EffectPlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void Effect_Before_patch(BattleChar __instance,SkillParticle SP, bool Dodge)
        {

                foreach (IP_Effect_Before ip_patch in __instance.IReturn<IP_Effect_Before>())
                {
                    //Debug.Log("only damage drm");
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        ip_patch.Effect_Before(__instance,SP,Dodge);
                    }
                }

        }
    }
    public interface IP_Effect_Before
    {
        // Token: 0x06000001 RID: 1
        void Effect_Before(BattleChar hit, SkillParticle SP, bool Dodge);
    }

    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("DodgeCheck")]
    public static class DodgeCheckPlugin
    {
        [HarmonyPriority(0)]
        [HarmonyPostfix]
        public static void DodgeCheck2(ref bool __result, BattleChar __instance, SkillParticle SP)
        {
            bool dod = __result;
            //Debug.Log("闪避检测检查点2");
            foreach (IP_DodgeCheck_After2 ip_patch2 in BattleSystem.instance.IReturn<IP_DodgeCheck_After2>())
            {
                bool flag2 = ip_patch2 != null;
                if (flag2)
                {
                    bool candod = dod;
                    ip_patch2.DodgeCheck_After2(__instance,SP, ref candod);
                    dod = candod && dod;
                }
            }

            __result = dod;
        }


    }
    public interface IP_DodgeCheck_After2
    {
        // Token: 0x06000001 RID: 1
        void DodgeCheck_After2(BattleChar hit, SkillParticle SP, ref bool dod);
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

    public interface IP_Predicte
    {
        // Token: 0x06000001 RID: 1
        int PredicteDamage();
    }
    [HarmonyPriority(-1)]

    //商店相关patch
    [HarmonyPatch(typeof(SpecialStore))]
    public static class SpecialStore_Start_Plugin
    {
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        public static void Start_PrePatch(SpecialStore __instance, out bool __state)
        {
            
            __state = false;
            
            if (PlayData.TSavedata.NowStageMapKey == "MasterMap")
            {
                SpecialStoreSave save= PlayData.TSavedata.Data_MasterMapStore;
                save.Load();
                //__instance.Save = PlayData.TSavedata.Data_MasterMapStore;
                //__instance.Save.Load();
                if (save.Data_StoreItems.Count <= 0)
                {

                    __state = true;
                }
                
            }
            
        }
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void Start_PostPatch(SpecialStore __instance, bool __state)
        {
            if (__state)
            {
                __instance.StoreItems.Add(ItemBase.GetItem("Item_PD_Sana_0"));
                __instance.Save.Save();
                PlayData.TSavedata.Data_MasterMapStore = __instance.Save;
            }

        }


    }
    /*
    [HarmonyPatch(typeof(BuyWindow))]
    public static class SpecialStore_OpenStore_Plugin
    {
        [HarmonyPatch("MakeItem")]
        [HarmonyPostfix]
        public static void MakeItem_PostPatch(BuyWindow __instance, ItemBase i)
        {
            if (i.itemkey== "Item_PD_Sana_1")
            {
                foreach(StoreItemButton button in __instance.SellItems)
                {
                    if(button.Item.itemkey== "Item_PD_Sana_1")
                    {
                        button.SetInfo(button.Item, 50);
                    }
                }
            }

        }


    }
    [HarmonyPatch(typeof(BuyWindow))]
    public static class SpecialStore_ToCashDesk_Plugin
    {
        [HarmonyPatch("ToCashDesk")]
        [HarmonyPostfix]
        public static void ToCashDesk_PostPatch(BuyWindow __instance, StoreItemButton ItemButton)
        {
            if (ItemButton.Item.itemkey == "Item_PD_Sana_1")
            {
                foreach (StoreItemButton button in __instance.DeskItems)
                {
                    if (button.Item.itemkey == "Item_PD_Sana_1")
                    {
                        button.SetInfo(button.Item, 50);
                    }
                }
            }

        }
    }
    */
    //--------------

    [HarmonyPatch(typeof(FieldSystem))]
    public static class FieldSystem_BattleStart_Plugin
    {
        [HarmonyPatch("BattleStart")]
        [HarmonyPrefix]
        public static void BattleStart_Patch(FieldSystem __instance,ref GDEEnemyQueueData QueueData)
        {
            SanaTempSaveData savedata = SanaCommonData.GetFairyGameData();
            if (savedata.replace)
            {
                savedata.replace = false;
                
                GDEEnemyQueueData gde = new GDEEnemyQueueData("Queue_Sana");
                    QueueData = gde;
                
                /*
                Debug.Log("替换测试point_1");
                GDEEnemyQueueData gde = new GDEEnemyQueueData("Queue_Sana");
                QueueData.Enemys.Clear();
                QueueData.Enemys.AddRange(gde.Enemys);
                QueueData.Pos.Clear();
                QueueData.Pos.AddRange(gde.Pos);
                QueueData.Wave2.Clear();
                QueueData.Wave2.AddRange(gde.Wave2);
                QueueData.Wave3.Clear();
                QueueData.Wave3.AddRange(gde.Wave3);
                Traverse.Create(QueueData).Field("_CustomeFogTurn").SetValue(gde.CustomeFogTurn);
                Traverse.Create(QueueData).Field("_UseCustomPosition").SetValue(gde.UseCustomPosition);
                Traverse.Create(QueueData).Field("_CustomBGM").SetValue(gde.CustomBGM);
                */
            }
            /*
            if (QueueData.Key == GDEItemKeys.EnemyQueue_LBossFirst_Queue)//EnemyQueue_LastBoss_MasterBattle_1//EnemyQueue_LBossFirst_Queue
            {
                if(PartyInventory.InvenM.ReturnItem("Item_PD_Sana_1")!=null)
                {
                    GDEEnemyQueueData gde = new GDEEnemyQueueData("Queue_Sana");
                    QueueData = gde;
                }

            }
            */
        }
    }
    [HarmonyPatch(typeof(BattleSystem))]
    public static class BattleSystem_MyTurn_Plugin
    {
        [HarmonyPatch("MyTurn")]
        [HarmonyFinalizer]
        public static void BattleSystem_Patch()
        {
            if(BattleSystem.instance==null || BattleSystem.instance.TurnNum<1)
            {
                return;
            }

                foreach (Item_Active item in BattleSystem_MyTurn_Plugin.items)
                {
                    if (PartyInventory.InvenM.InventoryItems.Find(a => a != null && a == item) != null)
                    {
                        Item_Active act = PartyInventory.InvenM.InventoryItems.Find(a =>a!=null && a == item) as Item_Active;
                        act.ChargeNow++;
                    }
                }

            List<Item_Active> items3= new List<Item_Active>();
            items3.AddRange(BattleSystem_MyTurn_Plugin.items);
            BattleSystem_MyTurn_Plugin.items.Clear();

                List<ItemBase> items2 = PartyInventory.InvenM.InventoryItems.FindAll(a =>a!=null && a.itemkey == "Item_PD_Sana_1");
              //Mark_Debug.Log("数量"+items2.Count);
                foreach (ItemBase item in items2)
                {
                    if(item is Item_Active act)
                    {
                        if (!items3.Contains(act)&& act.ChargeNow < act.ChargeMax)
                        {
                            BattleSystem_MyTurn_Plugin.items.Add(act);
                        }
                    }

                }



        }
        public static List<Item_Active> items=new List<Item_Active>();
    }
    public  class Test
    {
        public bool test = false;
    }

    //地图设置脉冲发射器-------------------------




    //地图设置脉冲发射器-------------------------



    public class Item_PD_Sana_0 : UseitemBase
    {
        public override bool Use()
        {
            SanaTempSaveData save=SanaCommonData.GetFairyGameData();
            save.replace = true;
            this.GetItem();
            return true;
        }
        public void GetItem()
        {
            List<ItemBase> list = new List<ItemBase>();
            list.Add(ItemBase.GetItem("Item_PD_Sana_1"));
            InventoryManager.Reward(list);
        }
    }
    public class Item_PD_Sana_1 : UseitemBase
    {

    }

    public class Sex_E_Sana_Item_1:Sex_ImproveClone
    {
        // Token: 0x060011DF RID: 4575 RVA: 0x00094F20 File Offset: 0x00093120
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            //Debug.Log("测试点位");
            foreach(BattleChar bc in Targets)
            {
                if(bc.BuffFind("B_PD_E_Sana_p_1_t",false))
                {
                    Buff buff = bc.BuffReturn("B_PD_E_Sana_p_1_t", false);
                    buff.SelfDestroy();
                    /*
                    foreach(StackBuff sta in buff.StackInfo)
                    {
                        sta.RemainTime = Math.Min(sta.RemainTime, 1);
                    }
                    int num = buff.StackNum;
                    for(int i = 0;i < num-1;i++)
                    {
                        buff.SelfStackDestroy();
                    }
                    */
                }
            }
        }
    }




    public class AI_PD_E_Sana : AI, IP_ForceShieldData
    {
        public void ForceShieldData(ref int maxnum, ref int num)
        {
            maxnum = 600;
            num = 480;
        }

        public override List<BattleChar> TargetSelect(Skill SelectedSkill)
        {
            /*
            if(SelectedSkill.MySkill.KeyID== "S_PD_E_Sana_1")
            {
              //Debug.Log("AI 选择目标");

                List<BattleChar> targets = new List<BattleChar>();
                targets.AddRange(this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => a.BuffFind("B_PD_E_Sana_p_1_t", false)));
                List<BattleAlly> targetAllys = new List<BattleAlly>();
                foreach (BattleChar bc in targets)
                {
                    targetAllys.Add(bc as BattleAlly);
                }
                if (this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => !targets.Contains(a)).Count > 0)
                {
                    if (SelectedSkill.IsNowCasting)
                    {
                      //Debug.Log("AI IsNowCasting");
                      //Debug.Log("AI 已有目标:" + SelectedSkill.MyButton.castskill.Target.Info.Name);
                        BattleChar mainTarget = SelectedSkill.MyButton.castskill.Target;
                        if(!targets.Contains(mainTarget))
                        {
                            targets.Insert(0, mainTarget);
                        }
                        
                    }
                    else
                    {
                        targets.Insert(0, PublicTool.AgrroReturnExcept(this.BChar, targetAllys));
                    }
                }
                return targets;
            }
            */
            return base.TargetSelect(SelectedSkill);
        }
        public override Skill SkillSelect(int ActionCount)
        {
            Skill result=base.SkillSelect(ActionCount);

            if (!this.phase2 && ((this.BChar.HP < 0.75 * this.BChar.GetStat.maxhp) || (this.BChar.HP < 1000) || (BattleSystem.instance.TurnNum > 3)))
            {
                this.phase2 = true;
                this.lastkey_0 = "";
                this.lastkey_1 = "";
                this.lastkey_2 = "";
            }
            if(this.phase2 && !this.phase2atk)
            {
                if(ActionCount==0)
                {
                    result = this.GetSkill("S_PD_E_Sana_10", result);
                }
                else
                {
                    return null;
                }
            }
            else if(!this.phase2)//一阶段选择技能
            {
                if(ActionCount==0)
                {
                    //this.lastkey_0 = "S_PD_E_Sana_1";
                    List<SkillPer> per_list = new List<SkillPer>();
                    Skill skill_1 = Skill.TempSkill("S_PD_E_Sana_1", this.BChar);
                    int dmg_1 = 0;
                    int per_1 = 0;
                    foreach (IP_Predicte ip_1 in skill_1.IReturn<IP_Predicte>())
                    {
                        if (ip_1 != null)
                        {
                            dmg_1 = ip_1.PredicteDamage();
                        }
                    }
                    per_1 = dmg_1;
                    per_list.Add(new SkillPer { key = "S_PD_E_Sana_1", per = per_1 });

                    string key = SkillPer.GetRandomSkill(per_list);
                    this.lastkey_0 = key;
                    result = this.GetSkill(key, result);
                    //result = this.GetSkill("S_PD_E_Sana_1", result);
                }
                else if(ActionCount==1)
                {
                    List<SkillPer> per_list = new List<SkillPer>();
                    //激光折射----------------------
                    Skill skill_4 = Skill.TempSkill("S_PD_E_Sana_4", this.BChar);
                    int dmg_4 = 0;
                    int per_4 = 0;
                    foreach (IP_Predicte ip_1 in skill_4.IReturn<IP_Predicte>())
                    {
                        if (ip_1 != null)
                        {
                            dmg_4 = ip_1.PredicteDamage();
                        }
                    }
                    per_4 = dmg_4;
                    if (per_4 > 5 && this.lastkey_1 == "S_PD_E_Sana_4")
                    {
                        per_4 = (int)Math.Max(5, per_4*0.4f);//连续抑制
                    }
                    per_list.Add(new SkillPer { key = "S_PD_E_Sana_4", per = per_4 });
                    /*
                    List<int> buffnums = new List<int>();
                    foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars)
                    {
                        buffnums.Add(bc.Buffs.FindAll(b => !b.IsHide && !b.BuffData.Debuff).Count);
                    }
                    buffnums.Sort();
                    if(buffnums.Count > 1)
                    {
                        int num1 = buffnums.Sum() - buffnums[0];
                        int num2 = buffnums.Count - 1;
                        int num3 = (int)(num1 / num2);//评判标准1：buff总数/人数（不包括buff最少的敌人）
                        int num4 = (int)(buffnums[buffnums.Count - 1]/4f);//评判标准2：buff最多的单位的buff数/4

                        int num5=Math.Max(num3, num4)*10;
                        if(num5>5 && this.lastkey_1== "S_PD_E_Sana_4")
                        {
                            num5 = Math.Max(5, num5 - 10);//连续抑制
                        }
                        if(num5 > 0)
                        {
                            per_list.Add(new SkillPer { key = "S_PD_E_Sana_4", per = num5 });
                        }
                    }
                    */
                    //火树银花----------------------
                    Skill skill_5 = Skill.TempSkill("S_PD_E_Sana_5", this.BChar);
                    int dmg_5 = 0;
                    int per_5 = 0;
                    foreach (IP_Predicte ip_1 in skill_5.IReturn<IP_Predicte>())
                    {
                        if (ip_1 != null)
                        {
                            dmg_5 = ip_1.PredicteDamage();
                        }
                    }
                    per_5 = dmg_5;
                    if (per_5 > 5 && this.lastkey_1 == "S_PD_E_Sana_5")
                    {
                        per_5 = (int)Math.Max(5, per_5 *0.5f);//连续抑制
                    }
                    per_list.Add(new SkillPer { key = "S_PD_E_Sana_5", per = per_5 });
                    /*
                    int num6 = 10;
                    int s5_num1 = 0;
                    foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars)
                    {
                        s5_num1 += PublicTool.BuffStack(bc, "B_PD_E_Sana_p_1_t");
                    }
                    num6 += (s5_num1 * 8);
                    if (num6 > 5 && this.lastkey_1 == "S_PD_E_Sana_5")
                    {
                        num6 = Math.Max(5, num6 - 10);//连续抑制
                    }
                    per_list.Add(new SkillPer { key = "S_PD_E_Sana_5", per = num6 });
                    */

                    string key=SkillPer.GetRandomSkill(per_list);
                    this.lastkey_1=key;
                    result=this.GetSkill(key,result);
                }
                else if(ActionCount==2)
                {
                    List<SkillPer> per_list = new List<SkillPer>();
                    //显形-------------------
                    int num1 = BattleSystem.instance.AllyTeam.AliveChars.FindAll(a => a.BuffFind("B_PD_E_Sana_p_1_t", false)).Count;
                    int num2 = BattleSystem.instance.AllyTeam.AliveChars.Count - num1;
                    int num3 = (int)(5 + num2*12 );
                    if (num3 > 5 && this.lastkey_2 == "S_PD_E_Sana_2")
                    {
                        num3 = (int)Math.Max(5, num3 *0.5f);//连续抑制
                    }
                    per_list.Add(new SkillPer { key = "S_PD_E_Sana_2", per = num3 });
                    //烟雾弹-------------------
                    int num4 = 30;
                    if (num4 > 5 && this.lastkey_2 == "S_PD_E_Sana_3")
                    {
                        num4 = (int)Math.Max(5, num4*0.4f);//连续抑制
                    }
                    per_list.Add(new SkillPer { key = "S_PD_E_Sana_3", per = num4 });

                    string key = SkillPer.GetRandomSkill(per_list);
                    this.lastkey_2 = key;
                    result = this.GetSkill(key, result);
                }
                else
                {
                    result = this.GetSkill("S_PD_E_Sana_1", result);
                }
            }
            else if(this.phase2)//二阶段选择技能
            {

                if(ActionCount==0)
                {
                    /*
                    int per_s1 = 10;
                    int per_s7 = 0;

                    int num1 = BattleSystem.instance.AllyTeam.AliveChars.FindAll(a => a.BuffFind("B_PD_E_Sana_p_1_t", false)).Count;
                    int num2 = BattleSystem.instance.AllyTeam.AliveChars.Count-1;
                    int num3=Math.Min(num1, num2);
                    per_s1 += (10 * num3);

                    int num4 = 0;
                    int num5 = 0;
                    foreach(BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars)
                    {
                        num4 += PublicTool.BuffStack(bc, "B_PD_E_Sana_p_1_t");
                        num5=Math.Max(PublicTool.BuffStack(bc, "B_PD_E_Sana_p_1_t"), num5);
                    }
                    float value = Bcl_Paradise_force.GetForceShield(this.BChar);
                    float maxvalue=Bcl_Paradise_force.GetForceData(this.BChar);
                    float fnum1 = 2f * (maxvalue - value) / maxvalue;
                    float fnum2 = 0.5f * fnum1 * fnum1;
                    float s7_c1 = 10 * fnum1 * fnum1 * num4;

                    float s7_c2 = 20 * num5;
                    per_s7=(int)Math.Max(s7_c1, s7_c2);

                    if (per_s1 > 5 && this.lastkey_0 == "S_PD_E_Sana_1")
                    {
                        per_s1 = Math.Max(5, per_s1 - 10);//连续抑制
                    }
                    if (per_s7 > 10 && this.lastkey_0 == "S_PD_E_Sana_7")
                    {
                        per_s7 = Math.Max(10, per_s7 - 20);//连续抑制
                    }
                    */
                    List<SkillPer> per_list = new List<SkillPer>();
                    //追踪导弹----------------
                    int per_1 = 0;
                    int dmg_1 = 0;
                    Skill skill_1 = Skill.TempSkill("S_PD_E_Sana_1", this.BChar);
                    foreach (IP_Predicte ip_1 in skill_1.IReturn<IP_Predicte>())
                    {
                        if (ip_1 != null)
                        {
                            dmg_1 = ip_1.PredicteDamage();
                        }
                    }
                    per_1 = dmg_1;
                    if (per_1 > 10 && this.lastkey_0 == "S_PD_E_Sana_1")
                    {
                        per_1 = (int)Math.Max(10f, per_1*0.6);//连续抑制-20
                    }

                    per_list.Add(new SkillPer { key = "S_PD_E_Sana_1", per = per_1 });
                    //裁决落定----------------
                    int per_7 = 0;
                    int dmg_7 = 0;
                    Skill skill_7 = Skill.TempSkill("S_PD_E_Sana_7", this.BChar);
                    foreach (IP_Predicte ip_7 in skill_7.IReturn<IP_Predicte>())
                    {
                        if (ip_7 != null)
                        {
                            dmg_7 = ip_7.PredicteDamage();
                        }
                    }
                    float value = Bcl_Paradise_force.GetForceShield(this.BChar);
                    float maxvalue = Bcl_Paradise_force.GetForceData(this.BChar);
                    float factor_7_1 = 1f-(value / maxvalue);
                    float factor_7_2=0.5f+2f*factor_7_1*factor_7_1;
                    per_7 = (int)(dmg_7 * Math.Max(1,factor_7_2));
                    if (per_7 > 10 && this.lastkey_0 == "S_PD_E_Sana_7")
                    {
                        per_7 = (int)Math.Max(10f, per_7 *0.5f);//连续抑制-25
                    }
                    //火树银花----------------
                    Skill skill_5 = Skill.TempSkill("S_PD_E_Sana_5", this.BChar);
                    int dmg_5 = 0;
                    int per_5 = 0;
                    foreach (IP_Predicte ip_1 in skill_5.IReturn<IP_Predicte>())
                    {
                        if (ip_1 != null)
                        {
                            dmg_5 = ip_1.PredicteDamage();
                        }
                    }
                    per_5 = (int)(0.75f*dmg_5);
                    if (per_5 > 10 && this.lastkey_1 == "S_PD_E_Sana_5")
                    {
                        per_5 = (int)Math.Max(10f, per_5 *0f);//连续抑制
                    }
                    per_list.Add(new SkillPer { key = "S_PD_E_Sana_5", per = per_5 });


                    per_list.Add(new SkillPer { key = "S_PD_E_Sana_7", per = per_7 });


                    string key = SkillPer.GetRandomSkill(per_list);
                    this.lastkey_0 = key;
                    result = this.GetSkill(key, result);
                }
                else if(ActionCount == 1)
                {
                    List<SkillPer> per_list = new List<SkillPer>();
                    //激光折射----------------
                    Skill skill_4 = Skill.TempSkill("S_PD_E_Sana_4", this.BChar);
                    int dmg_4 = 0;
                    int per_4 = 0;
                    foreach (IP_Predicte ip_1 in skill_4.IReturn<IP_Predicte>())
                    {
                        if (ip_1 != null)
                        {
                            dmg_4 = ip_1.PredicteDamage();
                        }
                    }
                    per_4 = dmg_4;
                    if (per_4 > 10 && this.lastkey_1 == "S_PD_E_Sana_4")
                    {
                        per_4 = (int)Math.Max(10, per_4*0.25f);//连续抑制-30
                    }
                    per_list.Add(new SkillPer { key = "S_PD_E_Sana_4", per = per_4 });
                    /*
                    List<int> buffnums = new List<int>();
                    foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars)
                    {
                        buffnums.Add(bc.Buffs.FindAll(b => !b.IsHide && !b.BuffData.Debuff).Count);
                    }
                    buffnums.Sort();
                    if (buffnums.Count > 1)
                    {
                        int num1 = buffnums.Sum() - buffnums[0];
                        int num2 = buffnums.Count - 1;
                        int num3 = (int)(num1 / num2);//评判标准1：buff总数/人数（不包括buff最少的敌人）
                        int num4 = (int)(buffnums[buffnums.Count - 1] / 4f);//评判标准2：buff最多的单位的buff数/4 

                        int per_s4 = Math.Max(num3, num4) * 10;
                        if (per_s4 > 5 && this.lastkey_1 == "S_PD_E_Sana_4")
                        {
                            per_s4 = Math.Max(5, per_s4 - 20);//连续抑制
                        }
                        if (per_s4 > 0)
                        {
                            per_list.Add(new SkillPer { key = "S_PD_E_Sana_4", per = per_s4 });
                        }
                    }
                    */
                    //火树银花----------------
                    if (this.lastkey_0 == "S_PD_E_Sana_5")
                    {
                        goto Tele_001;
                    }
                    Skill skill_5 = Skill.TempSkill("S_PD_E_Sana_5", this.BChar);
                    int dmg_5 = 0;
                    int per_5 = 0;
                    foreach (IP_Predicte ip_1 in skill_5.IReturn<IP_Predicte>())
                    {
                        if (ip_1 != null)
                        {
                            dmg_5 = ip_1.PredicteDamage();
                        }
                    }
                    per_5 = dmg_5;
                    if (per_5 > 10 && this.lastkey_1 == "S_PD_E_Sana_5")
                    {
                        per_5 = (int)Math.Max(10, per_5 *0.5f);//连续抑制-30
                    }
                    per_list.Add(new SkillPer { key = "S_PD_E_Sana_5", per = per_5 });
                    /*
                    int per_s5 = 10;
                    int s5_num1 = 0;
                    foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars)
                    {
                        s5_num1 += PublicTool.BuffStack(bc, "B_PD_E_Sana_p_1_t");
                    }
                    per_s5 += (s5_num1 * 6);//评判标准：暴露单位加1
                    if (per_s5 > 5 && this.lastkey_1 == "S_PD_E_Sana_5")
                    {
                        per_s5 = Math.Max(5, per_s5 - 20);//连续抑制
                    }
                    per_list.Add(new SkillPer { key = "S_PD_E_Sana_5", per = per_s5 });
                    */
                    Tele_001:
                    //黑色哀悼曲----------------
                    Skill skill_6 = Skill.TempSkill("S_PD_E_Sana_6", this.BChar);
                    int dmg_6 = 0;
                    int per_6 = 0;
                    foreach (IP_Predicte ip_1 in skill_6.IReturn<IP_Predicte>())
                    {
                        if (ip_1 != null)
                        {
                            dmg_6 = ip_1.PredicteDamage();
                        }
                    }
                    float value = Bcl_Paradise_force.GetForceShield(this.BChar);
                    float maxvalue = Bcl_Paradise_force.GetForceData(this.BChar);
                    float force_percent = 1f - (value / maxvalue);
                    float factor_6_2 = 0.5f + 2f * force_percent * force_percent;
                    per_6 = (int)(dmg_6 * Math.Max(1, factor_6_2));

                    if (per_6 > 10 && this.lastkey_1 == "S_PD_E_Sana_6")
                    {
                        per_6 = (int)Math.Max(10, per_6 *0.6f);//连续抑制-30
                    }
                    per_list.Add(new SkillPer { key = "S_PD_E_Sana_6", per = per_6 });
                    /*
                    int per_s6 = 5;
                    int num5 = 0;
                    List<BattleChar> list_s6 = new List<BattleChar>();
                    list_s6.AddRange(this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => a.GetStat.def >= this.BChar.BattleInfo.AllyTeam.AliveChars.Max(b => b.GetStat.def)));
                    foreach (BattleChar bc in list_s6)
                    {
                        num5 = Math.Max(PublicTool.BuffStack(bc, "B_PD_E_Sana_p_1_t"), num5);
                    }
                    per_s6 += 15 * num5;
                    float value = Bcl_Paradise_force.GetForceShield(this.BChar);
                    float maxvalue = Bcl_Paradise_force.GetForceData(this.BChar);
                    float fnum1 =  (maxvalue - value) / maxvalue;
                    float fnum2 = fnum1 + 1f;
                    per_s6 = (int)(fnum2 * per_s6);
                    if (per_s6 > 10 && this.lastkey_1 == "S_PD_E_Sana_6")
                    {
                        per_s6 = Math.Max(10, per_s6 - 15);//连续抑制
                    }
                    per_list.Add(new SkillPer { key = "S_PD_E_Sana_6", per = per_s6 });
                    */
                    //全弹发射----------------
                    Skill skill_9 = Skill.TempSkill("S_PD_E_Sana_9", this.BChar);
                    int dmg_9 = 0;
                    int per_9 = 0;
                    foreach (IP_Predicte ip_1 in skill_9.IReturn<IP_Predicte>())
                    {
                        if (ip_1 != null)
                        {
                            dmg_9 = ip_1.PredicteDamage();
                        }
                    }
                    float factor_9_2 = Math.Max(0,-1f+8f * force_percent * force_percent * force_percent);
                    per_9 = Math.Max(0,(int)(factor_9_2 * 40f-this.BChar.BarrierHP));
                    if (per_9 > 0 && this.lastkey_1 == "S_PD_E_Sana_9")
                    {
                        per_9 = (int)Math.Max(0, per_9 *0f);//连续抑制
                    }
                    per_list.Add(new SkillPer { key = "S_PD_E_Sana_9", per = per_9 });
                    /*
                    int per_s9 = 10;
                    float fnum3 = 4f * force_percent;
                    float fnum4 = 1f+0.25f * fnum3 * fnum3;
                    per_s9=(int)(fnum4*per_s9);
                    if (per_s9 > 0 && this.lastkey_1 == "S_PD_E_Sana_9")
                    {
                        per_s9 = Math.Max(0, per_s9 - 1000);//连续抑制
                    }
                    per_list.Add(new SkillPer { key = "S_PD_E_Sana_9", per = per_s9 });

                    string key = SkillPer.GetRandomSkill(per_list);
                    this.lastkey_1 = key;
                    result = this.GetSkill(key, result);
                    */

                    string key = SkillPer.GetRandomSkill(per_list);
                    this.lastkey_1 = key;
                    result = this.GetSkill(key, result);
                }
                else if(ActionCount==2)
                {
                    List<SkillPer> per_list = new List<SkillPer>();
                    

                    int num1 = BattleSystem.instance.AllyTeam.AliveChars.FindAll(a => a.BuffFind("B_PD_E_Sana_p_1_t", false)).Count;
                    int num2 = BattleSystem.instance.AllyTeam.AliveChars.Count - num1;
                    //显形----------------
                    /*
                    int per_s2 = (int)(20 + 10*num2);
                    if (per_s2 > 5 && this.lastkey_2 == "S_PD_E_Sana_2")
                    {
                        per_s2 = (int)Math.Max(5, per_s2 *0.5f);//连续抑制-30
                    }
                    per_list.Add(new SkillPer { key = "S_PD_E_Sana_2", per = per_s2 });
                    */
                    //全域探知----------------
                    int per_s11 = (int)(10 + 15 * num2);
                    if (per_s11 > 0 && this.lastkey_2 == "S_PD_E_Sana_11")
                    {
                        per_s11 = (int)Math.Max(0, per_s11 * 0f);//连续抑制-30
                    }
                    per_list.Add(new SkillPer { key = "S_PD_E_Sana_11", per = per_s11 });
                    //烟雾弹----------------
                    int per_s3 = 30;// 5+6* BattleSystem.instance.AllyTeam.AliveChars.Count;
                    if (per_s3 > 5 && this.lastkey_2 == "S_PD_E_Sana_3")
                    {
                        per_s3 =(int) Math.Max(5, per_s3*0.2f);//连续抑制-15
                    }
                    per_list.Add(new SkillPer { key = "S_PD_E_Sana_3", per = per_s3 });
                    //厄运锁定----------------
                    int num3 = 0;// BattleSystem.instance.AllyTeam.AliveChars.FindAll(a => a.BuffFind("B_PD_E_Sana_p_1_t", false)).Count;
                    foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars)
                    {
                        num3 += PublicTool.BuffStack(bc, "B_PD_E_Sana_p_1_t");
                    }
                    int num4 = 2 * BattleSystem.instance.AllyTeam.AliveChars.Count - num3;

                    int per_s8 = (int)(10 + (num2 > 0 ? 20 : 0)); //(int)(10 + 8*num2)
                    float value = Bcl_Paradise_force.GetForceShield(this.BChar);
                    float maxvalue = Bcl_Paradise_force.GetForceData(this.BChar);
                    float fnum1 = (maxvalue - value) / maxvalue;
                    float fnum2 = 2f * fnum1;
                    float fnum3 = 1f+0.5f * fnum2 * fnum2;
                    per_s8 = (int)(per_s8 * fnum3);
                    if (per_s8 > 0 && this.lastkey_2 == "S_PD_E_Sana_8")
                    {
                        per_s8 =(int) Math.Max(0, per_s8 *0.25f);//连续抑制-30
                    }
                    per_list.Add(new SkillPer { key = "S_PD_E_Sana_8", per = per_s8 });

                    string key = SkillPer.GetRandomSkill(per_list);
                    this.lastkey_2 = key;
                    result = this.GetSkill(key, result);
                }
                else
                {
                    result = this.GetSkill("S_PD_E_Sana_1", result);
                }
            }

            return result.CloneSkill(false,null,null,false);
        }
        public int pnum = 0;
        public override int SpeedChange(Skill skill, int ActionCount, int OriginSpeed)
        {
            int count = OriginSpeed;
            bool flag = false;
            if(count<=0)
            {
                flag = true;
                count += 1;
            }
            if((this.phase2atkturn+1)==BattleSystem.instance.TurnNum)
            {
                if(ActionCount==0)
                {
                    if(flag)
                    {
                        count += 1;
                    }
                    else
                    {
                        count += 2;
                    }
                }
            }
            return count;
            //return base.SpeedChange(skill, ActionCount, OriginSpeed);
        }

        public Skill GetSkill(string key, Skill skill)
        {
            if (this.BChar.Skills.Find(a => a.MySkill.KeyID == key) != null)
            {

                return this.BChar.Skills.Find(a => a.MySkill.KeyID == key);
            }

            return skill;
        }
        public string lastkey_0 = string.Empty;
        public string lastkey_1 = string.Empty;
        public string lastkey_2 = string.Empty;
        public bool phase2=false;
        public bool phase2atk=false;
        public int phase2atkturn = -2;
    }

    public class SkillPer
    {
        public string key = string.Empty;
        public int per = 0;

        public static string GetRandomSkill(List<SkillPer> pers)
        {
            List<SkillPer> list= new List<SkillPer>();
            foreach (SkillPer skill in pers)
            {
                if(list.Find(a=>a.key==skill.key)!=null)
                {
                    SkillPer obj = list.Find(a => a.key == skill.key);
                    obj.per += skill.per;
                }
                else
                {
                    list.Add(new SkillPer { key = skill.key, per = skill.per });
                }
            }
            foreach (SkillPer skill in list)
            {
                if(skill.per<0)
                {
                    skill.per = 0;
                }
            }

            int sum = 0;
            foreach (SkillPer skill in list)
            {
                sum += skill.per;
            }
            Debug.Log("---------------");
            Debug.Log("当前回合："+BattleSystem.instance.TurnNum);
            foreach (SkillPer skill in list)
            {
                try
                {
                    GDESkillData gde = new GDESkillData(skill.key);
                    Debug.Log(gde.Name + ": " + skill.per+"/"+sum);
                }
                catch { }
            }

            System.Random rnd = new System.Random();
            int num=rnd.Next(sum);
            foreach (SkillPer skill in list)
            {
                num -= skill.per;
                if(num < 0)
                {
                    GDESkillData gde = new GDESkillData(skill.key);
                    Debug.Log("选择：" +gde.Name);
                    return skill.key;
                }
            }
            return string.Empty;
        }

        
    }
    public class B_E_Sana_h : Buff,IP_Dead, IP_BattleStart_UIOnBefore
    {
        public void BattleStartUIOnBefore(BattleSystem Ins)
        {
            //Ins.FogTurn = 30;
            BattleSystem.DelayInput(this.Start1());
            MasterAudio.StopBus("BGM");

            if(this.BChar is BattleEnemy enemy)
            {
                Debug.Log("敌人UI:" + (enemy.MyUIObject.custom != null));
                Debug.Log("敌人UI:" + (enemy.MyUIObject.transform != null));
            }
        }
        public IEnumerator Start1()
        {
            MasterAudio.SetBusVolumeByName("BGM", 1f);

                MasterAudio.PlaySound("E_Sana_BGM", 0.8f, null, 0f, null, null, false, false);

            
            yield break;
        }

        public void Dead()
        {
            //BattleSystem.instance.Reward.Add(ItemBase.GetItem("Item_SF_1", 4));
            //Skill skill = Skill.TempSkill("S_PD_Reward_0", BattleSystem.instance.AllyTeam.LucyChar, BattleSystem.instance.AllyTeam);
            //BattleSystem.instance.Reward.Add(ItemBase.GetItem(new GDESkillData("S_PD_Reward_0")));
            BattleSystem.instance.Reward.Clear();

            //BattleSystem.instance.Reward.Add(ItemBase.GetItem("Item_Sana_Chest"));
            BattleSystem.instance.Reward.Add(ItemBase.GetItem("Item_Sana_Chest",2));

            BattleSystem.instance.Reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy_Rare));
            BattleSystem.instance.Reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Misc_Soul, 5));


            List<ItemBase> items2 = PartyInventory.InvenM.InventoryItems.FindAll(a => a != null && a.itemkey == "Item_PD_Sana_1");
            foreach (ItemBase item in items2)
            {
                PartyInventory.InvenM.DelItem(item);
            }


        }
    }
    public class B_E_Sana_p_1 : Buff, IP_TurnEnd,IP_SkillUseHand_Team
    {
        public void SKillUseHand_Team(Skill skill)
        {
            if(skill.AllExtendeds.Find(a=>a is EX_E_Sana_p_1_t)!=null)
            {
                if (BattleSystem.instance.AllyTeam.AliveChars.Contains(skill.Master))
                {
                    skill.Master.BuffAdd("B_PD_E_Sana_p_1_t", this.BChar, false, 0, false, -1, false);
                }
                else
                {
                    //Debug.Log("随机施加【暴露】");
                    BattleChar bc = BattleSystem.instance.AllyTeam.AliveChars.Random(this.BChar.GetRandomClass().Target);
                    bc.BuffAdd("B_PD_E_Sana_p_1_t", this.BChar, false, 0, false, -1, false);
                }
            }
        }
        public void TurnEnd()
        {
            return;
            int x = BattleSystem.instance.TurnNum;
            Skill skill = new Skill();
            bool flag0= B_E_Sana_p_1.GetSkillX(x,ref skill);
            bool flag1 = false;
            if (flag0)
            {
                if(BattleSystem.instance.AllyTeam.AliveChars.Contains(skill.Master))
                {
                    skill.Master.BuffAdd("B_PD_E_Sana_p_1_t",this.BChar,false,0,false,-1,false);
                    flag1 = true;
                }
            }
            if(flag0 && !flag1)
            {
              //Debug.Log("随机施加【暴露】");
                BattleChar bc = BattleSystem.instance.AllyTeam.AliveChars.Random(this.BChar.GetRandomClass().Target);
                bc.BuffAdd("B_PD_E_Sana_p_1_t", this.BChar, false, 0, false, -1, false);
            }
        }
        public static bool GetSkillX(int x1,ref Skill skill)
        {
            if (BattleSystem.instance.AllyTeam.Skills.Count > 0)
            {
                //int x2 = (x1 % BattleSystem.instance.AllyTeam.Skills.Count) - 1;
                int x2 = Math.Max(x1-1,0);
                int loops = 0;
                Tele_001:
                while (x2 >= BattleSystem.instance.AllyTeam.Skills.Count)
                {
                    x2 -= BattleSystem.instance.AllyTeam.Skills.Count;
                }
                if(true || BattleSystem.instance.AllyTeam.AliveChars.Contains(BattleSystem.instance.AllyTeam.Skills[x2].Master))//判断是否为调查员技能，否则顺延。当前未启用
                {
                    skill = BattleSystem.instance.AllyTeam.Skills[x2];
                    return true;
                }
                else
                {
                    if(loops< BattleSystem.instance.AllyTeam.Skills.Count)
                    {
                        loops++;
                        x2++;
                        goto Tele_001;
                    }
                }
            }


            return false;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();

            this.fixedCount++;
            if(this.fixedCount<0|| this.fixedCount>10)
            {
                this.fixedCount = 0;
                if (BattleSystem.instance.AllyTeam.Skills.Count > 0)
                {
                    Skill skill = new Skill();
                    bool flag1= B_E_Sana_p_1.GetSkillX(BattleSystem.instance.TurnNum,ref skill);
                    if (flag1 )
                    {
                        //Debug.Log("施加EX:" + skill.MySkill.Name);
                        this.markSkill = skill;
                        foreach(Skill skill_1 in BattleSystem.instance.AllyTeam.Skills)
                        {
                            if(skill_1.Master==skill.Master && skill_1.AllExtendeds.Find(a => a is EX_E_Sana_p_1_t) == null)
                            {
                                GDESkillExtendedData gdesex = new GDESkillExtendedData("EX_PD_E_Sana_p_1_t");
                                EX_E_Sana_p_1_t sex1 = Skill_Extended.DataToExtended(gdesex) as EX_E_Sana_p_1_t;
                                sex1.mainbuff = this;
                                skill_1.ExtendedAdd(sex1);
                            }

                        }


                        
                        /*
                        Skill_Extended sex1 = skill.ExtendedAdd("EX_PD_E_Sana_p_1_t");
                        (sex1 as EX_E_Sana_p_1_t).mainbuff = this;
                        */
                        //sex1.MySkill = skill;
                    }
                }

            }


        }
        public void Refresh(Skill skill0)
        {
            if(true||Time.frameCount!=this.refreshFrame)
            {
                this.refreshFrame = Time.frameCount;
                if(this.markSkill.Master!=skill0.Master)
                {
                    foreach (Skill_Extended sex in skill0.AllExtendeds)
                    {
                        if (!sex.isDestroy && sex is EX_E_Sana_p_1_t sex2)
                        {
                            sex2.SelfDestroy();
                        }
                    }
                }
            }
        }
        public int fixedCount = 0;
        public int refreshFrame = 0;
        public Skill markSkill=new Skill();
    }
    public class EX_E_Sana_p_1_t: Sex_ImproveClone
    {
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(this.mainbuff!=null)
            {
                this.mainbuff.Refresh(this.MySkill);
            }
            else
            {
                if(!this.isDestroy)
                {
                  //Debug.Log("摧毁2：" + this.MySkill.MySkill.Name);
                    this.SelfDestroy();
                }

            }
        }
        public B_E_Sana_p_1 mainbuff =new B_E_Sana_p_1();
    }
    public class B_E_Sana_p_1_t : Buff,IP_Effect_Before,IP_CriPerChange, IP_DodgeCheck_After2
    {
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&user", this.Usestate_L.Info.Name);
        }

        public void CriPerChange(Skill skill, BattleChar Target, ref float CriPer)
        {
            if(Target==this.BChar && this.StackNum>=2 && this.StackInfo.Find(a=>a.UseState==skill.Master)!=null)
            {
                CriPer += 100;
            }
        }
        public void Effect_Before(BattleChar hit, SkillParticle SP, bool Dodge)
        {
            if(hit==this.BChar && SP.UseStatus==this.Usestate_L)
            {
                Sex_E_Sana_p_1_t sex=new Sex_E_Sana_p_1_t();
                sex.Traking = true;
                
                sex.MySkill = SP.SkillData;
                SP.SkillData.AllExtendeds.Add(sex);
            }
        }
        public void DodgeCheck_After2(BattleChar hit, SkillParticle SP, ref bool dod)
        {
            if(this.StackNum>=2&&hit==this.BChar&& this.StackInfo.Find(a=>a.UseState==SP.UseStatus)!=null)
            {
                dod = false;
            }
        }
    }

    public class B_E_Sana_p_2 : Buff,IP_BuffAdd,IP_DebuffResist_After,IP_TurnEnd
    {
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {
            if(Time.frameCount==this.lastFrame && addedbuff.BuffData.Key==this.lastKey)
            {
                return;
            }
            this.lastFrame = Time.frameCount;
            this.lastKey = addedbuff.BuffData.Key;

            if (BuffTaker==this.BChar && addedbuff.BuffData.Debuff)
            {
                /*
                this.PlusStat.RES_CC += 30;
                this.PlusStat.RES_DEBUFF += 30;
                this.PlusStat.RES_DOT += 30;
                */
                
                if (addedbuff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_CrowdControl)
                {
                    this.PlusStat.RES_CC += 30;
                  //Mark_Debug.Log("干扰抵抗增加至：" + this.PlusStat.RES_CC);
                }
                if (addedbuff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_Debuff)
                {
                    this.PlusStat.RES_DEBUFF += 15;
                  //Mark_Debug.Log("弱化抵抗增加至：" + this.PlusStat.RES_DEBUFF);
                }
                if (addedbuff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT)
                {
                    this.PlusStat.RES_DOT += 10;
                  //Mark_Debug.Log("痛苦抵抗增加至：" + this.PlusStat.RES_DOT);
                }
                
            }
        }
        public void DebuffResist_After(Buff buff)
        {
            if(buff.BChar==this.BChar && buff.BuffData.Debuff)
            {
                /*
                this.PlusStat.RES_CC = (int)(0.7 * this.PlusStat.RES_CC);
                this.PlusStat.RES_DEBUFF = (int)(0.7 * this.PlusStat.RES_DEBUFF);
                this.PlusStat.RES_DOT = (int)(0.2 * this.PlusStat.RES_DOT);
                */
                
                if (buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_CrowdControl)
                {
                    this.PlusStat.RES_CC = (int)(0.7* this.PlusStat.RES_CC);
                  //Mark_Debug.Log("干扰抵抗减低至：" + this.PlusStat.RES_CC);
                }
                if (buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_Debuff)
                {
                    this.PlusStat.RES_DEBUFF = (int)(0.7 * this.PlusStat.RES_DEBUFF);
                  //Mark_Debug.Log("弱化抵抗减低至：" + this.PlusStat.RES_DEBUFF);
                }
                if (buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT)
                {
                    this.PlusStat.RES_DOT = (int)(0.2 * this.PlusStat.RES_DOT);
                  //Mark_Debug.Log("痛苦抵抗减低至：" + this.PlusStat.RES_DOT);
                }
                
            }
        }
        public void TurnEnd()
        {
            this.PlusStat.RES_CC = (int)(0.4 * this.PlusStat.RES_CC);
            this.PlusStat.RES_DEBUFF = (int)(0.1 * this.PlusStat.RES_DEBUFF);
            this.PlusStat.RES_DOT = (int)(0.6 * this.PlusStat.RES_DOT);
        }

        public int lastFrame = 0;
        public string lastKey=string.Empty;
    }


    public class Sex_E_Sana_p_1_t : Sex_ImproveClone, IP_DamageChange_Hit_sumoperation
    {
        public void DamageChange_Hit_sumoperation(Skill SkillD, int Damage, ref bool Cri, bool View, ref int PlusDamage)
        {
            if(SkillD==this.MySkill)
            {
                this.MySkill.AllExtendeds.RemoveAll(a => a is Sex_E_Sana_p_1_t);
            }
        }
    }


    public static class PublicTool
    {
        public static BattleChar AgrroReturnExcept(BattleChar user, List<BattleAlly> Excepts)
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

        public static Skill OriginSkill(Skill skill)
        {
            Skill result = new Skill();
            result = skill;
            while (result.OriginalSelectSkill != null)
            {
                result = result.OriginalSelectSkill;
            }
            return result;
        }

        public static int BuffStack(BattleChar bc,string key)
        {
            if(bc.BuffFind(key,false))
            {
                return bc.BuffReturn(key,false).StackNum;
            }
            return 0;
        }

        public static CastingSkill GetCastSkill(Skill_Extended ex)
        {
            return PublicTool.GetCastSkill(ex.MySkill);
        }
        public static CastingSkill GetCastSkill(Skill skill)
        {
            if(skill.MyButton!=null && skill.MyButton.castskill!=null && skill.MyButton.castskill.skill==skill)
            {
                return skill.MyButton.castskill;
            }
            else if(BattleSystem.instance!=null)
            {
                if(BattleSystem.instance.EnemyCastSkills.Find(a=>a.skill==skill)!=null)
                {
                    return BattleSystem.instance.EnemyCastSkills.Find(a => a.skill == skill);
                }
            }
            return null;
        }

        public static void ResetQueue(BattleChar enemy)
        {
            if (enemy is BattleEnemy)
            {
                PublicTool.ResetQueue(enemy as BattleEnemy);
            }
        }
        public static void ResetQueue(BattleEnemy enemy)
        {
            List<CastingSkill> list = BattleSystem.instance.EnemyCastSkills;//enemy.SkillQueue;
            bool flag = true;
            int round = 0;
            while (flag && round < 1000)
            {
                flag = false;
                round++;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Usestate == enemy)
                    {
                        int num1 = list.FindIndex(a => a.Usestate == enemy && a.CastSpeed > list[i].CastSpeed);
                        if (num1 >= 0 && num1 < i)
                        {
                            flag = true;
                            CastingSkill cast = list[i];
                            list.RemoveAt(i);
                            list.Insert(num1, cast);
                        }
                    }

                }
            }


        }

        public static float HitCorrection(float hit,int stk)
        {
            if(stk>=2)
            {
                return 1.5f;
            }
            else if(stk>=1)
            {
                return (0.5f + 0.5f * hit);
            }
            else
            {
                return hit;
            }
        }
    }


    public class Sex_E_Sana_1 : Sex_ImproveClone,IP_TargetAI, IP_Predicte
    {
        public List<BattleChar> TargetAI(BattleEnemy MyBchar)//B_E_Sana_p_1
        {
            if(this.MySkill.IsNowCasting)
            {
              //Debug.Log("Sex_E_Sana_1在倒计时中");
                List<BattleChar> targets = new List<BattleChar>();
              //Debug.Log("Sex_E_Sana_1:"+ PublicTool.GetCastSkill(this.MySkill).Target.Info.Name);
                targets.AddRange(this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => a.BuffFind("B_PD_E_Sana_p_1_t", false)));

                BattleChar mainTarget = PublicTool.GetCastSkill(this.MySkill).Target;
                if(targets.Contains(mainTarget) && this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a=>!targets.Contains(a)).Count>0)
                {
                  //Debug.Log("Sex_E_Sana_1:在倒计时中重选目标");
                    List<BattleAlly> targetAllys = new List<BattleAlly>();
                    foreach (BattleChar bc in targets)
                    {
                        targetAllys.Add(bc as BattleAlly);
                    }
                    mainTarget = PublicTool.AgrroReturnExcept(this.BChar, targetAllys);

                    if (this.BChar.BattleInfo.AllyTeam.AliveChars.Contains(mainTarget) && !targets.Contains(mainTarget))//判断maintarget可选且不重复
                    {
                        PublicTool.GetCastSkill(this.MySkill).Target = mainTarget;
                    }
                }

                if (this.BChar.BattleInfo.AllyTeam.AliveChars.Contains(mainTarget) && !targets.Contains(mainTarget))//判断maintarget可选且不重复
                {
                    targets.Insert(0, mainTarget);
                }

                return targets;
                
            }
            else
            {
              //Debug.Log("Sex_E_Sana_1不在倒计时中");
                List<BattleChar> targets = new List<BattleChar>();
                targets.AddRange(this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => a.BuffFind("B_PD_E_Sana_p_1_t", false)));

                BattleChar mainTarget = new BattleChar();

                    List<BattleAlly> targetAllys = new List<BattleAlly>();
                    foreach (BattleChar bc in targets)
                    {
                        targetAllys.Add(bc as BattleAlly);
                    }
                mainTarget = PublicTool.AgrroReturnExcept(this.BChar, targetAllys);

                if (this.BChar.BattleInfo.AllyTeam.AliveChars.Contains(mainTarget) && !targets.Contains(mainTarget))//判断maintarget可选且不重复
                {
                    targets.Insert(0, mainTarget);
                }
                return targets;
            }

            return null;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);

                List<BattleChar> plusTargets = new List<BattleChar>();
            plusTargets.AddRange(this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => a.BuffFind("B_PD_E_Sana_p_1_t", false)));
            foreach (BattleChar bc in plusTargets)
            {
                if(!Targets.Contains(bc))
                {
                    Targets.Add(bc);
                }
            }
        }

        public int PredicteDamage()//未考虑暴击
        {
            GDESkillData gde = new GDESkillData("S_PD_E_Sana_1");
            int baseDMG = (int)(gde.Effect_Target.DMG_Per*this.BChar.GetStat.atk / 100f + gde.Effect_Target.DMG_Base);
            float hitper = Math.Min(1f,gde.Effect_Target.HIT * this.BChar.GetStat.hit/100f);
            int sum = 0;

            List <BattleChar> list = this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => a.BuffFind("B_PD_E_Sana_p_1_t", false));

            if(list.Count< this.BChar.BattleInfo.AllyTeam.AliveChars.Count)
            {
                sum += (int)(baseDMG * hitper);
            }
            foreach (BattleChar bc in list)
            {
                int num = PublicTool.BuffStack(bc, "B_PD_E_Sana_p_1_t");
                sum += (int)(baseDMG * PublicTool.HitCorrection(hitper,num));
                
            }
            return sum;
        }
    }

    public class Sex_E_Sana_2 : Sex_ImproveClone,IP_TargetAI,IP_ParticleOut_After
    {
        public List<BattleChar> TargetAI(BattleEnemy MyBchar)//B_E_Sana_p_1
        {
            if (this.MySkill.IsNowCasting)//已在倒计时中
            {
              //Debug.Log("Sex_E_Sana_2在倒计时中");
                List<BattleChar> targets = new List<BattleChar>();
                List<BattleChar> excepts = new List<BattleChar>();
                if(this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => PublicTool.BuffStack(a,"B_PD_E_Sana_p_1_t")<=0).Count>0)
                {
                    excepts.AddRange(this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => PublicTool.BuffStack(a, "B_PD_E_Sana_p_1_t") > 0));
                }
                else
                {
                    excepts.AddRange(this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => PublicTool.BuffStack(a, "B_PD_E_Sana_p_1_t") > 1));
                }



                BattleChar mainTarget = PublicTool.GetCastSkill(this.MySkill).Target;
                if (excepts.Contains(mainTarget) && this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => !excepts.Contains(a)).Count > 0)//如果当前目标被排除，且存在未排除单位，则进行重选。
                {
                    List<BattleAlly> exceptAllys = new List<BattleAlly>();
                    foreach (BattleChar bc in excepts)
                    {
                        exceptAllys.Add(bc as BattleAlly);
                    }
                    mainTarget = PublicTool.AgrroReturnExcept(this.BChar, exceptAllys);
                    if(mainTarget!=null)//判断maintarget可选
                    {
                        PublicTool.GetCastSkill(this.MySkill).Target = mainTarget;
                    }
                    
                }
                if (mainTarget != null)//判断maintarget可选
                {
                    targets.Add(mainTarget);
                }

                return targets;

            }
            else//未开始倒计时
            {
                List<BattleChar> targets = new List<BattleChar>();
                List<BattleChar> excepts = new List<BattleChar>();
                if (this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => PublicTool.BuffStack(a, "B_PD_E_Sana_p_1_t") <= 0).Count > 0)
                {
                    excepts.AddRange(this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => PublicTool.BuffStack(a, "B_PD_E_Sana_p_1_t") > 0));
                }
                else
                {
                    excepts.AddRange(this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => PublicTool.BuffStack(a, "B_PD_E_Sana_p_1_t") > 1));
                }

                BattleChar mainTarget = new BattleChar();

                    List<BattleAlly> exceptAllys = new List<BattleAlly>();
                    foreach (BattleChar bc in excepts)
                    {
                        exceptAllys.Add(bc as BattleAlly);
                    }
                    mainTarget = PublicTool.AgrroReturnExcept(this.BChar, exceptAllys);

                if (mainTarget != null)//判断maintarget可选
                {
                    targets.Add(mainTarget);
                }
                    
                return targets;
            }

            return null;
        }
        public override void Init()
        {
            base.Init();
            BuffTag tag= new BuffTag();
            tag.BuffData = new GDEBuffData("B_PD_E_Sana_p_1_t");
            tag.User=this.BChar;
            this.TargetBuff.Clear();
            this.TargetBuff.Add(tag);
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            this.TargetBuff.Clear();
        }
        public IEnumerator ParticleOut_After(Skill SkillD, List<BattleChar> Targets)
        {
            if(SkillD==this.MySkill)
            {
                foreach (BattleChar bc in Targets)
                {
                    bc.BuffAdd("B_PD_E_Sana_p_1_t", this.BChar);
                }
            }
            yield break;
        }
    }

    public class Sex_E_Sana_3 : Sex_ImproveClone,IP_TargetAI
    {
        public List<BattleChar> TargetAI(BattleEnemy MyBchar)
        {
            List<Skill> logskills1 = new List<Skill>();
            logskills1.AddRange(BattleSystem.instance.BattleLogs.getSkills((BattleLog log) => log.WhoUse.Info.Ally , (Skill s) => !PublicTool.OriginSkill(s).FreeUse, BattleSystem.instance.TurnNum));
            List<Skill> logskills2 = new List<Skill>();
            foreach(Skill sk in logskills1)
            {
                logskills2.Add(PublicTool.OriginSkill(sk));
            }
            int maxnum = 0;
            foreach(BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars)
            {
                maxnum=Math.Max(maxnum,this.UsedSkillNum(logskills2 ,bc));
            }
            List<BattleAlly> excepts = new List<BattleAlly>();
            excepts.AddRange(BattleSystem.instance.AllyList);
            excepts.RemoveAll(a => this.UsedSkillNum(logskills2, a) >= maxnum);
            
            List<BattleChar> result= new List<BattleChar>();
            if(this.MySkill.IsNowCasting)
            {
                if(excepts.Contains(PublicTool.GetCastSkill(this.MySkill).Target))
                {

                    //Debug.Log("重选目标");
                    BattleChar bc = PublicTool.AgrroReturnExcept(this.BChar, excepts);
                    PublicTool.GetCastSkill(this.MySkill).Target = bc;
                    result.Add(PublicTool.GetCastSkill(this.MySkill).Target);
                }
                else
                {
                    //Debug.Log("已有目标");
                    result.Add(PublicTool.GetCastSkill(this.MySkill).Target);
                }
            }
            else
            {
                //Debug.Log("初选目标");
                result.Add(PublicTool.AgrroReturnExcept(this.BChar, excepts));
            }
            return result;
        }

        public int UsedSkillNum(List<Skill> logskills, BattleChar bc)
        {
            int num = 0;
            foreach(Skill sk in logskills)
            {
                if(sk.Master==bc)
                {
                    num++;
                }
            }
            return num;
        }
    }
    public class B_E_Sana_3 : Buff,IP_SkillUse_User_After//,IP_TurnEnd
    {
        public override void BuffStat()
        {
            base.BuffStat();
            double k=2-2*Math.Pow(0.5,this.StackNum);
            this.PlusStat.hit = -(float)(16 * k);
            this.PlusStat.dod = -(float)(16 * k);
            this.PlusPerStat.Damage = -(int)(16 * k);
        }
        public void SkillUseAfter(Skill SkillD)
        {
            if(SkillD.Master==this.BChar && !PublicTool.OriginSkill(SkillD).FreeUse)
            {
                this.SelfStackDestroy();
            }
        }
        public void TurnEnd()
        {
            this.SelfStackDestroy();
        }
    }


    //[Serializable]
    public class Sex_E_Sana_4 : Sex_ImproveClone, IP_TargetAI,IP_Predicte
    {
        public override string DescExtended(string desc)
        {
            int num = 0;
            if(this.MySkill.IsNowCasting)
            {
                num = this.BuffNum(PublicTool.GetCastSkill(this.MySkill).Target) * Sex_E_Sana_4.PlusHitPer;
            }
            return base.DescExtended(desc).Replace("&a",num.ToString()).Replace("&b", Sex_E_Sana_4.PlusHitPer.ToString());
        }
        public List<BattleChar> TargetAI(BattleEnemy MyBchar)
        {

            int maxnum = 0;
            foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars)
            {
                maxnum = Math.Max(maxnum, this.BuffNum(bc));
            }
            List<BattleAlly> excepts = new List<BattleAlly>();
            excepts.AddRange(BattleSystem.instance.AllyList);
            excepts.RemoveAll(a => this.BuffNum( a) >= maxnum);

            List<BattleChar> result = new List<BattleChar>();
            if (this.MySkill.IsNowCasting)
            {
                if (excepts.Contains(PublicTool.GetCastSkill(this.MySkill).Target))
                {

                    //Debug.Log("重选目标");
                    BattleChar bc = PublicTool.AgrroReturnExcept(this.BChar, excepts);
                    PublicTool.GetCastSkill(this.MySkill).Target = bc;
                    result.Add(PublicTool.GetCastSkill(this.MySkill).Target);
                }
                else
                {
                    //Debug.Log("已有目标");
                    result.Add(PublicTool.GetCastSkill(this.MySkill).Target);
                }
            }
            else
            {
                //Debug.Log("初选目标");
                result.Add(PublicTool.AgrroReturnExcept(this.BChar, excepts));
            }
            return result;
        }

        public int BuffNum(BattleChar bc)
        {
            int num = 0;
            foreach (Buff buff in bc.Buffs)
            {
                if (!buff.IsHide /*&& !buff.BuffData.Debuff*/ && !buff.DestroyBuff)
                {
                    num++;
                }
            }
            return num;
        }
        public int PredicteDamage()//为计算命中率，默认可命中不暴击
        {
            GDESkillData gde = new GDESkillData("S_PD_E_Sana_4");
            int baseDMG = (int)(gde.Effect_Target.DMG_Per * this.BChar.GetStat.atk/100f + gde.Effect_Target.DMG_Base);
            float hitper = Math.Min(1f, gde.Effect_Target.HIT * this.BChar.GetStat.hit / 100f);

            int sumDMG = 0;
            List<int> list_1= new List<int>();
            foreach(BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars)
            {
                list_1.Add(this.BuffNum(bc));
            }
            list_1.Sort();
            list_1.Reverse();
            int dmg = baseDMG;
            for (int i = 0;i<list_1.Count-1;i++)
            {
                sumDMG += dmg;
                dmg = (int)(Sex_E_Sana_4.PlusHitPer * list_1[i] * 0.01f * dmg);
                if(dmg<=0)
                {
                    break;
                }
            }
            
            return (int)(0.6f*sumDMG);
        }
        public override void AttackEffectSingle(BattleChar hit, SkillParticle SP, int DMG, int Heal)
        {
            base.AttackEffectSingle(hit, SP, DMG, Heal);
            if (DMG <= 0)
            {
                return;
            }
            this.hits.Add(hit);//Mark
            List<BattleAlly> choices = new List<BattleAlly>();
            choices.AddRange(BattleSystem.instance.AllyList);
            choices.Remove(hit as BattleAlly);
            foreach (BattleChar bc in this.hits)
            {
                choices.Remove(bc as BattleAlly);
            }
            //Debug.Log(hit.Info.Name+"判断折射:" + this.hits.Count);
            if(choices.Count > 0 && this.BuffNum(hit)> 2*(this.hits.Count-1))
            {
                Skill skill=Skill.TempSkill("S_PD_E_Sana_4_1",this.BChar);
                //Debug.Log("复制前");
                //Sex_E_Sana_4 sex = (Sex_E_Sana_4)this.CloneTest2();
                Sex_E_Sana_4 sex = (Sex_E_Sana_4)this.Clone();

                //Debug.Log("复制后1");
                //Sex_E_Sana_4 sex = new Sex_E_Sana_4();
                sex.MySkill = skill;
                sex.MyChar = this.BChar.Info;
                //sex.hits.AddRange(this.hits);
                //sex.hits.Add(hit);//Mark

                sex.SkillBasePlus.Target_BaseDMG = (int)(Sex_E_Sana_4.PlusHitPer * this.BuffNum(hit) * 0.01 * DMG - 1);
                sex.SkillBasePlus.Target_BaseDMG = Math.Max(sex.SkillBasePlus.Target_BaseDMG, 1);
                skill.AllExtendeds.Add(sex);



                int maxnum = 0;
                foreach (BattleChar bc in choices)
                {
                    maxnum = Math.Max(maxnum, this.BuffNum(bc));
                }
                List<BattleAlly> excepts = new List<BattleAlly>();
                excepts.AddRange(BattleSystem.instance.AllyList);
                excepts.RemoveAll(a => this.BuffNum(a) >= maxnum);

                excepts.Add(hit as BattleAlly);
                foreach (BattleChar bc in this.hits)
                {
                    excepts.Add(bc as BattleAlly);
                }

                List<BattleChar> result = new List<BattleChar>();

                    //Debug.Log("初选目标");
                    result.Add(PublicTool.AgrroReturnExcept(this.BChar, excepts));

                BattleSystem.DelayInput(this.PlusHit(skill, result));
                //this.BChar.ParticleOut(skill, result);
                
            }
        }
        public IEnumerator PlusHit(Skill skill, List<BattleChar> target)
        {
            yield return new WaitForSecondsRealtime(1f);
            this.BChar.ParticleOut(skill, target);
            yield break;
        }
        public static int PlusHitPer = 20;
        public List<BattleChar> hits = new List<BattleChar>();
        /*
        public Sex_E_Sana_4 CloneTest1()
        {
            
            
          //Debug.Log("复制节点1");
            MemoryStream memoryStream = new MemoryStream();
          //Debug.Log("复制节点2");
            BinaryFormatter formatter = new BinaryFormatter();
          //Debug.Log("复制节点3");
            formatter.Serialize(memoryStream, this);
          //Debug.Log("复制节点4");
            memoryStream.Position = 0;
          //Debug.Log("复制节点5");
            return (Sex_E_Sana_4)formatter.Deserialize(memoryStream);
        }
        */
        /*
        public object CloneTest2()
        {
            // 基础浅拷贝
            var clone = this.MemberwiseClone();

            // 获取所有实例字段（包括私有）
            var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                // 检查是否为List<T>
                if (field.FieldType.IsGenericType &&
                    field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    // 获取原始列表
                    var originalList = (IEnumerable)field.GetValue(this);

                    if (originalList == null)
                        continue;

                    // 创建新列表并复制元素
                    var elementType = field.FieldType.GetGenericArguments()[0];
                    var newList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));

                    foreach (var item in originalList)
                        newList.Add(item);

                    // 将新列表赋值给克隆对象
                    field.SetValue(clone, newList);
                }
            }

            return clone;
        }
        */
        /*
        public static T DeepCopyByJson<T>(T obj)
        {
            if (!typeof(T).IsSerializable)
            {
              //Debug.Log("类型必须可序列化"+ nameof(obj));
            }
                //throw new ArgumentException("类型必须可序列化", nameof(obj));
          //Debug.Log("复制节点1");
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
          //Debug.Log("复制节点2");
            var result= Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
          //Debug.Log("复制节点3");
            return result;
        }
        public static T DeepCopyByReflection<T>(T obj)
        {
            if (obj == null) return default;

            System.Type type = obj.GetType();
            object copy = Activator.CreateInstance(type);

            foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                object value = field.GetValue(obj);
                if (value is ICloneable cloneable)
                    field.SetValue(copy, cloneable.Clone());
                else if (value != null && value.GetType().IsClass && !value.GetType().IsPrimitive)
                    field.SetValue(copy, DeepCopyByReflection(value)); // 递归处理引用类型
                else
                    field.SetValue(copy, value); // 值类型直接复制
            }

            return (T)copy;
        }
        */
    }


    public class Sex_E_Sana_5 : Sex_ImproveClone,IP_DamageChange, IP_Predicte
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&num1",((int)(this.mul*100)).ToString());
        }
        public int PredicteDamage()
        {
            GDESkillData gde = new GDESkillData("S_PD_E_Sana_5");
            int baseDMG = (int)(gde.Effect_Target.DMG_Per * this.BChar.GetStat.atk/100f + gde.Effect_Target.DMG_Base);
            float hitper = Math.Min(1f, gde.Effect_Target.HIT * this.BChar.GetStat.hit / 100f);

            int sum = 0;
            foreach(BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars)
            {
                int num = PublicTool.BuffStack(bc, "B_PD_E_Sana_p_1_t");
                sum += (int)(baseDMG * (1 + this.mul * num) * PublicTool.HitCorrection(hitper, num));
            }
            return (int)(sum);
        }
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if(SkillD==this.MySkill && Damage>0)
            {
                int num = PublicTool.BuffStack(Target, "B_PD_E_Sana_p_1_t");
                if(num > 0)
                {
                    return (int)((1 + this.mul * num) * Damage);
                }
            }
            return Damage;
        }
        public float mul = 0.33f;
    }


    public class Sex_E_Sana_6 : Sex_ImproveClone, IP_TargetAI,IP_ParticleOut_Before,IP_Predicte
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&user",this.MySkill.Master.Info.Name).Replace("&num1", (this.factor_dmg * 100f).ToString());
        }
        public List<BattleChar> TargetAI(BattleEnemy MyBchar)//B_E_Sana_p_1
        {
            List<BattleChar> excepts = new List<BattleChar>();
            //excepts.AddRange(this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => a.GetStat.def < this.BChar.BattleInfo.AllyTeam.AliveChars.Max(b => b.GetStat.def)));
            excepts.AddRange(this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => PublicTool.BuffStack(a,"B_PD_E_Sana_p_1_t") < this.BChar.BattleInfo.AllyTeam.AliveChars.Max(b => PublicTool.BuffStack(b, "B_PD_E_Sana_p_1_t"))));

            List<BattleAlly> exceptsAlly = new List<BattleAlly>();
            foreach (BattleChar bc in excepts)
            {
                exceptsAlly.Add(bc as BattleAlly);
            }
            List<BattleChar> result = new List<BattleChar>();

            if (this.MySkill.IsNowCasting)
            {

                if (excepts.Contains(PublicTool.GetCastSkill(this.MySkill).Target))
                {
                    BattleChar bc = PublicTool.AgrroReturnExcept(this.BChar, exceptsAlly);
                    PublicTool.GetCastSkill(this.MySkill).Target = bc;
                    result.Add(PublicTool.GetCastSkill(this.MySkill).Target);
                }
                else
                {
                    result.Add(PublicTool.GetCastSkill(this.MySkill).Target);
                }
            }
            else
            {
                result.Add(PublicTool.AgrroReturnExcept(this.BChar, exceptsAlly));
            }

            return result;
        }
        public int PredicteDamage()
        {
            GDESkillData gde = new GDESkillData("S_PD_E_Sana_6");
            int baseDMG = (int)(gde.Effect_Target.DMG_Per * this.BChar.GetStat.atk / 100f + gde.Effect_Target.DMG_Base);
            float hitper = Math.Min(1f, gde.Effect_Target.HIT * this.BChar.GetStat.hit / 100f);
            int sum = 0;

            List<BattleChar> list = new List<BattleChar>();
            list.AddRange(BattleSystem.instance.AllyTeam.AliveChars);
            //list.OrderByDescending(a => a.GetStat.def).ThenBy(b=> PublicTool.BuffStack(b, "B_PD_E_Sana_p_1_t"));
            list.OrderByDescending(a => PublicTool.BuffStack(a, "B_PD_E_Sana_p_1_t"));
            if (list.Count > 0)
            {
                BattleChar target = list[0];

                int num = PublicTool.BuffStack(target, "B_PD_E_Sana_p_1_t");
                sum = (int)(baseDMG * PublicTool.HitCorrection(hitper, num));
                sum = (int)(sum * (1f + this.factor_dmg * num));
            }
            else
            {
                sum = baseDMG;
            }

            GDEBuffData gde2 = new GDEBuffData("B_PD_E_Sana_6_2");
            int baseDMG2 = (int)(gde2.Tick.DMG_Per * this.BChar.GetStat.atk / 100f + gde2.Tick.DMG_Base);
            sum += (int)(baseDMG2 * gde2.LifeTime);

            return sum;
        }

        public void ParticleOut_Before(Skill SkillD, List<BattleChar> Targets)
        {
            if(SkillD == this.MySkill)
            {
                foreach(BattleChar Target in Targets)
                {
                    if (PublicTool.BuffStack(Target, "B_PD_E_Sana_p_1_t") > 0)
                    {
                        int sta = PublicTool.BuffStack(Target, "B_PD_E_Sana_p_1_t");
                        if (Bcl_Paradise_force.GetForceData(this.MySkill.Master) > 0)
                        {
                            Bcl_Paradise_force buff1 = this.MySkill.Master.BuffAdd("B_PD_Paradise_force", this.MySkill.Master) as Bcl_Paradise_force;
                            buff1.ChangeForceShield_per(10 * sta);
                        }
                    }
                }

                
            }
        }
        public override void AttackEffectSingle(BattleChar hit, SkillParticle SP, int DMG, int Heal)
        {
            base.AttackEffectSingle(hit, SP, DMG, Heal);
            if(PublicTool.BuffStack(hit,"B_PD_E_Sana_p_1_t")>0)
            {
                int sta= PublicTool.BuffStack(hit, "B_PD_E_Sana_p_1_t");
                /*
                if(Bcl_Paradise_force.GetForceData(this.BChar)>0)
                {
                    Bcl_Paradise_force buff1 = this.BChar.BuffAdd("B_PD_Paradise_force", this.BChar) as Bcl_Paradise_force;
                    buff1.ChangeForceShield_per(10 * sta);
                }
                */
                int count = hit.MyTeam.AliveChars.Count;
                if (count > 0 && DMG > 0)
                {
                    int plusDMG = Math.Max(1, (int)(this.factor_dmg * sta*DMG / count));
                    BattleSystem.instance.StartCoroutine(this.Delay(hit.MyTeam,SP.UseStatus,plusDMG));
                }
            }

        }
        public IEnumerator Delay(BattleTeam team,BattleChar user,int plusDMG)
        {
            yield return new WaitForSeconds(0.5f);
            foreach (BattleChar bc in team.AliveChars)
            {
                Buff buff = bc.BuffAdd("B_PD_E_Sana_6_3", this.BChar, false, 0, false, -1, false);
                bc.Damage(user, plusDMG, false, true, false, 0, false, false, false);
                buff.SelfDestroy(false);
            }
            yield break;
        }
        public float factor_dmg = 0.5f;
    }
    public class B_E_Sana_6_1 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.def = -20;
        }
    }
    public class B_E_Sana_6_3 : Buff,IP_DeadResist
    {
        public bool DeadResist()
        {
            return true;
        }
    }
    public class Sex_E_Sana_7 : Sex_ImproveClone, IP_TargetAI,IP_Predicte,IP_ParticleOut_Before
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&user", this.MySkill.Master.Info.Name).Replace("&num1", this.reduceNum.ToString()).Replace("&num2", this.reduceMax.ToString());
        }
        public override void Init()
        {
            base.Init();
            this.EnemyTargetAIOnly = true;
        }
        public List<BattleChar> TargetAI(BattleEnemy MyBchar)//B_E_Sana_p_1
        {

                //Debug.Log("Sex_E_Sana_1不在倒计时中");
                List<BattleChar> result = new List<BattleChar>();
            result.AddRange(this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => a.BuffFind("B_PD_E_Sana_p_1_t", false)));
            List<BattleAlly> excepts = new List<BattleAlly>();
            excepts.AddRange(BattleSystem.instance.AllyList);
            excepts.RemoveAll(a => result.Contains(a));
            if(this.MySkill.IsNowCasting)
            {
                if (!result.Contains(PublicTool.GetCastSkill(this.MySkill).Target))
                {
                    if (result.Count > 0)
                    {
                        BattleChar bc = PublicTool.AgrroReturnExcept(this.BChar, excepts);
                        PublicTool.GetCastSkill(this.MySkill).Target = bc;
                    }

                }
                int num = result.Count - 1;
                int num2 = Math.Min(this.reduceNum * num, this.reduceMax);
                this.PlusSkillPerFinal.Damage = -num2;
                this.PlusSkillStat.hit = -num2;
            }


                return result;

        }
        public int PredicteDamage()
        {
            GDESkillData gde = new GDESkillData("S_PD_E_Sana_7");
            int baseDMG = (int)(gde.Effect_Target.DMG_Per * this.BChar.GetStat.atk / 100f + gde.Effect_Target.DMG_Base);

            List<BattleChar> targets = new List<BattleChar>();
            targets.AddRange(this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => a.BuffFind("B_PD_E_Sana_p_1_t", false)));
            int num1=targets.Count;
            float reduceFactor = Math.Min(this.reduceNum * num1, this.reduceMax);
            float hitper = Math.Min(1f, (gde.Effect_Target.HIT- reduceFactor) * this.BChar.GetStat.hit / 100f);
            baseDMG = (int)(baseDMG * (1f - 0.01f * reduceFactor));
            int sum = 0;

            foreach (BattleChar bc in targets)
            {
                sum += (int)(baseDMG * PublicTool.HitCorrection(hitper, PublicTool.BuffStack(bc, "B_PD_E_Sana_p_1_t")));
            }


            return sum;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            this.PlusSkillPerFinal.Damage =0;
            this.PlusSkillStat.hit = 0;

            List<BattleChar> plusTargets = new List<BattleChar>();
            plusTargets.AddRange(this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => a.BuffFind("B_PD_E_Sana_p_1_t", false)));
            Targets.Clear();
            Targets.AddRange(plusTargets);
            /*
            int num = Targets.Count - 1;
            int num2= Math.Min(this.reduceNum*num, this.reduceMax);
            this.PlusSkillPerFinal.Damage = -num2;
            this.PlusSkillStat.hit = -num2;
            */
        }
        public void ParticleOut_Before(Skill SkillD, List<BattleChar> Targets)
        {
            int num = Targets.Count - 1;
            int num2 = Math.Min(this.reduceNum * num, this.reduceMax);
            this.PlusSkillPerFinal.Damage = -num2;
            this.PlusSkillStat.hit = -num2;
        }
        public override void AttackEffectSingle(BattleChar hit, SkillParticle SP, int DMG, int Heal)
        {
            base.AttackEffectSingle(hit, SP, DMG, Heal);
            if (PublicTool.BuffStack(hit, "B_PD_E_Sana_p_1_t") > 0)
            {
                int sta = PublicTool.BuffStack(hit, "B_PD_E_Sana_p_1_t");
                if (Bcl_Paradise_force.GetForceData(this.MySkill.Master) > 0)
                {
                    Bcl_Paradise_force buff1 = this.MySkill.Master.BuffAdd("B_PD_Paradise_force", this.MySkill.Master) as Bcl_Paradise_force;
                    buff1.ChangeForceShield_per(5 * sta);
                }
            }
        }

        public int reduceNum = 20;
        public int reduceMax = 50;
    }


    public class Sex_E_Sana_8 : Sex_ImproveClone, IP_TargetAI
    {
        public List<BattleChar> TargetAI(BattleEnemy MyBchar)//B_E_Sana_p_1
        {
            if (this.MySkill.IsNowCasting)//已在倒计时中
            {
                //Debug.Log("Sex_E_Sana_2在倒计时中");
                List<BattleChar> targets = new List<BattleChar>();
                List<BattleChar> excepts = new List<BattleChar>();
                if (this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => PublicTool.BuffStack(a, "B_PD_E_Sana_p_1_t") <= 0).Count > 0)
                {
                    excepts.AddRange(this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => PublicTool.BuffStack(a, "B_PD_E_Sana_p_1_t") > 0));
                }
                else
                {
                    excepts.AddRange(this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => PublicTool.BuffStack(a, "B_PD_E_Sana_p_1_t") > 1));
                }



                BattleChar mainTarget = PublicTool.GetCastSkill(this.MySkill).Target;
                if (excepts.Contains(mainTarget) && this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => !excepts.Contains(a)).Count > 0)//如果当前目标被排除，且存在未排除单位，则进行重选。
                {
                    List<BattleAlly> exceptAllys = new List<BattleAlly>();
                    foreach (BattleChar bc in excepts)
                    {
                        exceptAllys.Add(bc as BattleAlly);
                    }
                    mainTarget = PublicTool.AgrroReturnExcept(this.BChar, exceptAllys);
                    if (mainTarget != null)//判断maintarget可选
                    {
                        PublicTool.GetCastSkill(this.MySkill).Target = mainTarget;
                    }

                }
                if (mainTarget != null)//判断maintarget可选
                {
                    targets.Add(mainTarget);
                }

                return targets;

            }
            else//未开始倒计时
            {
                List<BattleChar> targets = new List<BattleChar>();
                List<BattleChar> excepts = new List<BattleChar>();
                if (this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => PublicTool.BuffStack(a, "B_PD_E_Sana_p_1_t") <= 0).Count > 0)
                {
                    excepts.AddRange(this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => PublicTool.BuffStack(a, "B_PD_E_Sana_p_1_t") > 0));
                }
                else
                {
                    excepts.AddRange(this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => PublicTool.BuffStack(a, "B_PD_E_Sana_p_1_t") > 1));
                }

                BattleChar mainTarget = new BattleChar();

                List<BattleAlly> exceptAllys = new List<BattleAlly>();
                foreach (BattleChar bc in excepts)
                {
                    exceptAllys.Add(bc as BattleAlly);
                }
                mainTarget = PublicTool.AgrroReturnExcept(this.BChar, exceptAllys);

                if (mainTarget != null)//判断maintarget可选
                {
                    targets.Add(mainTarget);
                }

                return targets;
            }

            return null;
        }
        
    }
    public class B_E_Sana_8 : Buff,IP_Hit
    {
        public override string DescExtended( )
        {
            return base.DescExtended().Replace("&user", this.Usestate_L.Info.Name);
        }
        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            if(SP.UseStatus==this.Usestate_L)
            {
                int sta = 1;
                if(Cri)
                {
                    sta = 2;
                }
                    if (Bcl_Paradise_force.GetForceData(this.Usestate_L) > 0)
                    {
                        Bcl_Paradise_force buff1 = this.Usestate_L.BuffAdd("B_PD_Paradise_force", this.Usestate_L) as Bcl_Paradise_force;
                        buff1.ChangeForceShield_per(5 * sta);
                    }
                
            }
        }
    }

    public class Sex_E_Sana_9 : Sex_ImproveClone, IP_SkillCastingStart,IP_DamageChange,IP_Predicte
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&num1", ((int)(0.08 * this.BChar.GetStat.maxhp)).ToString());
        }
        public int PredicteDamage()
        {
            GDESkillData gde = new GDESkillData("S_PD_E_Sana_9");
            int baseDMG = (int)(gde.Effect_Target.DMG_Per * this.BChar.GetStat.atk / 100f + gde.Effect_Target.DMG_Base);
            float hitper = Math.Min(1f, gde.Effect_Target.HIT * this.BChar.GetStat.hit / 100f);

            int sum = 0;

            foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars)
            {
                sum += (int)(baseDMG * PublicTool.HitCorrection(hitper, PublicTool.BuffStack(bc, "B_PD_E_Sana_p_1_t")));
            }

            return sum;
        }

        public override void Init()
        {
            base.Init();
            this.CountingExtedned = true;
        }
        public void SkillCasting(CastingSkill ThisSkill)
        {
            Buff buff = this.MySkill.Master.BuffAdd("B_PD_Paradise_s", this.BChar, false, 0, false, -1, false);
            buff.BarrierHP += ((int)(0.08 * this.BChar.GetStat.maxhp));
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.frame++;
            if(this.frame>9 || this.frame<0)
            {
                this.frame = 0;
                if (this.MySkill.IsNowCasting && this.BChar.BarrierHP <= 0 && !this.reduceDMG)
                {
                    this.reduceDMG = true;
                    if (Bcl_Paradise_force.GetForceData(this.BChar) > 0)
                    {
                        Bcl_Paradise_force buff1 = this.BChar.BuffAdd("B_PD_Paradise_force", this.BChar) as Bcl_Paradise_force;
                        int num = buff1.maxValue - buff1.value;
                        buff1.ChangeForceShield((int)(0.5 * num));
                    }

                    BattleSystem.instance.StartCoroutine(this.Delay_1());
                }
            }

        }
        public int frame = 0;
        public IEnumerator Delay_1()
        {
            if (PublicTool.GetCastSkill(this).CastSpeed > 0)
            {
                PublicTool.GetCastSkill(this).CastSpeed = BattleSystem.instance.AllyTeam.TurnActionNum;
                /*
                if(!BattleSystem.instance.NowEndedTurn)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                */
                PublicTool.ResetQueue(this.BChar);

            }

            if (BattleSystem.instance.SaveSkill.Find(a => a.skill == this.MySkill) == null)
            {

                while (BattleSystem.instance .EnemyCheck|| BattleSystem.instance.Particles.Count != 0 || BattleSystem.instance.ListWait || BattleSystem.instance.DelayWait)
                {
                    yield return new WaitForSeconds(0.2f);
                }
                yield return new WaitForSeconds(0.2f);
                //yield return new WaitForFixedUpdate();
                yield return BattleSystem.instance.EnemyTurn(false);
            }
            else
            {
                CastingSkill cast = BattleSystem.instance.SaveSkill.Find(a => a.skill == this.MySkill);
                BattleSystem.instance.SaveSkill.Remove(cast);
                BattleSystem.instance.SaveSkill.Insert(0, cast);
            }
            yield return new WaitForSeconds(0.1f);

            yield break;
        }
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if (SkillD == this.MySkill && Damage > 0 && this.reduceDMG)
            {

                    return (int)((1 -0.66) * Damage);
                
            }
            return Damage;
        }

        public bool reduceDMG = false;
    }

    public class Sex_E_Sana_10 : Sex_ImproveClone, IP_SkillCastingStart, IP_DamageChange,IP_Predicte
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&num1", ((int)(0.20 * this.BChar.GetStat.maxhp)).ToString());
        }
        public int PredicteDamage()
        {
            GDESkillData gde = new GDESkillData("S_PD_E_Sana_10");
            int baseDMG = (int)(gde.Effect_Target.DMG_Per * this.BChar.GetStat.atk / 100f + gde.Effect_Target.DMG_Base);
            float hitper = Math.Min(1f, gde.Effect_Target.HIT * this.BChar.GetStat.hit / 100f);

            int sum = 0;

            foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars)
            {
                sum += (int)(baseDMG * PublicTool.HitCorrection(hitper, PublicTool.BuffStack(bc, "B_PD_E_Sana_p_1_t")));
            }

            return sum;
        }
        public override void Init()
        {
            base.Init();
            this.CountingExtedned = true;
        }
        public void SkillCasting(CastingSkill ThisSkill)
        {
            Buff buff = this.MySkill.Master.BuffAdd("B_PD_Paradise_s", this.BChar, false, 0, false, -1, false);
            buff.BarrierHP += ((int)(0.2 * this.BChar.GetStat.maxhp));
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.frame++;
            if (this.frame > 9 || this.frame < 0)
            {
                this.frame = 0;
                if (this.MySkill.IsNowCasting && this.BChar.BarrierHP <= 0 && !this.reduceDMG)
                {
                    this.reduceDMG = true;
                    if (Bcl_Paradise_force.GetForceData(this.BChar) > 0)
                    {
                        Bcl_Paradise_force buff1 = this.BChar.BuffAdd("B_PD_Paradise_force", this.BChar) as Bcl_Paradise_force;
                        int num = buff1.maxValue - buff1.value;
                        buff1.ChangeForceShield((int)(1 * num));
                    }

                    BattleSystem.instance.StartCoroutine(this.Delay_1());
                }
            }

        }
        public int frame = 0;
        public IEnumerator Delay_1()
        {
            if (PublicTool.GetCastSkill(this).CastSpeed > 0)
            {
                PublicTool.GetCastSkill(this).CastSpeed = BattleSystem.instance.AllyTeam.TurnActionNum;
                /*
                if(!BattleSystem.instance.NowEndedTurn)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                */
                PublicTool.ResetQueue(this.BChar);

            }

            if (BattleSystem.instance.SaveSkill.Find(a => a.skill == this.MySkill) == null)
            {
                while (BattleSystem.instance.EnemyCheck || BattleSystem.instance.Particles.Count != 0 || BattleSystem.instance.ListWait || BattleSystem.instance.DelayWait)
                {
                    yield return new WaitForSeconds(0.2f);
                }
                yield return new WaitForSeconds(0.2f);
                //yield return new WaitForFixedUpdate();
                yield return BattleSystem.instance.EnemyTurn(false);
            }
            else
            {
                CastingSkill cast = BattleSystem.instance.SaveSkill.Find(a => a.skill == this.MySkill);
                BattleSystem.instance.SaveSkill.Remove(cast);
                BattleSystem.instance.SaveSkill.Insert(0, cast);
            }
            yield return new WaitForSeconds(0.1f);

            yield break;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            if(this.BChar is BattleEnemy enemy)
            {
                if(enemy.Ai is AI_PD_E_Sana ai)
                {
                    ai.phase2atk = true;
                    ai.phase2atkturn = BattleSystem.instance.TurnNum;
                }
            }
        }
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if (SkillD == this.MySkill && Damage > 0 && this.reduceDMG)
            {

                return (int)((1 - 0.66) * Damage);

            }
            return Damage;
        }

        public bool reduceDMG = false;
    }


    public class Sex_E_Sana_11 : Sex_ImproveClone, IP_TargetAI
    {
        public List<BattleChar> TargetAI(BattleEnemy MyBchar)//B_E_Sana_p_1
        {
            List<BattleChar> list1= new List<BattleChar>();
            list1.AddRange(this.BChar.BattleInfo.AllyTeam.AliveChars);
            //int lowstack = this.BChar.BattleInfo.AllyTeam.AliveChars.Min(a => PublicTool.BuffStack(a, "B_PD_E_Sana_p_1_t"));
            list1.RemoveAll(a => PublicTool.BuffStack(a, "B_PD_E_Sana_p_1_t") > 0);


            return list1;
        }

        public override void Init()
        {
            base.Init();
            this.EnemyTargetAIOnly = true;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            //base.SkillUseSingle(SkillD, Targets);
            /*
            List<BattleChar> plusTargets = new List<BattleChar>();
            plusTargets.AddRange(this.BChar.BattleInfo.AllyTeam.AliveChars.FindAll(a => a.BuffFind("B_PD_E_Sana_p_1_t", false)));
            Targets.Clear();
            foreach (BattleChar bc in plusTargets)
            {
                if (!Targets.Contains(bc))
                {
                    Targets.Add(bc);
                }
            }
            */

            foreach (BattleChar bc in this.BChar.BattleInfo.AllyTeam.AliveChars)
            {
                if(!Targets.Contains(bc))
                {
                    if(bc.BuffFind("B_PD_E_Sana_p_1_t",false))
                    {
                        Buff buff = bc.BuffReturn("B_PD_E_Sana_p_1_t", false);
                        foreach(StackBuff sta in buff.StackInfo)
                        {
                            if(sta.RemainTime>0)
                            {
                                sta.RemainTime++;
                            }
                        }
                    }
                    else
                    {

                    }
                }
            }
        }
    }



    public class Item_Sana_Chest : UseitemBase
    {
        // Token: 0x060033DC RID: 13276 RVA: 0x00169D7C File Offset: 0x00167F7C
        public override bool Use()
        {
            this.GetEquip();
            return true;
        }

        // Token: 0x060033DD RID: 13277 RVA: 0x00169D88 File Offset: 0x00167F88
        private void GetEquip()
        {
            List<ItemBase> list = new List<ItemBase>();
            if(this.CheckEquipAndRelic("Relic_Sana_1"))
            {
                list.Add(ItemBase.GetItem("Relic_Sana_1"));
            }
            if (this.CheckEquipAndRelic("E_PD_E_Sana_0"))
            {
                list.Add(ItemBase.GetItem("E_PD_E_Sana_0"));
            }
            list.Add(ItemBase.GetItem(GDEItemKeys.Item_Consume_EquipPouch));
            list.Add(ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter_Rare));
            list.Add(ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookInfinity, 3));
            UIManager.InstantiateActive(UIManager.inst.SelectItemUI).GetComponent<SelectItemUI>().Init(list, null, false);
        }

        public bool CheckEquipAndRelic(string key)
        {
            foreach(ItemBase item in PartyInventory.InvenM.InventoryItems)
            {
                if (item != null && item.itemkey == key)
                {
                    return false;
                }
            }
            foreach(Character cha in PlayData.TSavedata.Party)
            {
                foreach(ItemBase item in cha.Equip)
                {
                    if (item != null && item.itemkey == key)
                    {
                        return false;
                    }
                }
            }
            foreach (Item_Passive item in PlayData.Passive)
            {
                if (item != null && item.itemkey == key)
                {
                    return false;
                }
            }
            return true;
        }
        /*
        public bool CheckRelic(string key)
        {
            foreach (ItemBase item in PartyInventory.InvenM.InventoryItems)
            {
                if (item != null && item.itemkey == key)
                {
                    return false;
                }
            }
            foreach (Item_Passive item in PlayData.Passive)
            {
                if(item != null && item.itemkey == key)
                {
                    return false;
                }
            }
            
            foreach (ItemBase item in PlayData.TSavedata.Passive_Itembase)
            {
                if (item != null && item.itemkey == key)
                {
                    return false;
                }
            }
            
            return true;
        }
        */
    }
    public class E_PD_E_Sana_0 : EquipBase,IP_SkillUse_Team,IP_TurnEnd,IP_PlayerTurn_EnemyAfter,IP_BattleStart_Ones
    {
        public override void Init()
        {
            base.Init();
            this.PlusPerStat.Damage = 15;
            this.PlusStat.spd = -1;
            this.PlusStat.dod = -15;
        }
        public void SkillUseTeam(Skill skill)
        {
            if(skill.Master==this.BChar && skill.IsDamage)
            {
                int num = skill.UsedApNum;
                for(int i=0; i<num; i++)
                {
                    Skill skill1 = Skill.TempSkill("S_PD_E_Sana_E_1", this.BChar);
                    BattleTeam.SkillRandomUse(this.BChar, skill1, false, true, false);
                }
                
            }
        }
        public void BattleStart(BattleSystem Ins)
        {
            this.aps = 0;
        }
        public void TurnEnd()
        {
            if(BattleSystem.instance.AllyTeam.AP>0)
            {
                this.aps = BattleSystem.instance.AllyTeam.AP;
            }
        }
        public void PlayerTurn_EnemyAfter()
        {
            if(this.aps>0)
            {
                Skill skill1 = Skill.TempSkill("S_PD_E_Sana_E_2", this.BChar);
                if(this.aps>1)
                {
                    Skill_Extended sex=skill1.ExtendedAdd(new Skill_Extended());
                    sex.PlusSkillPerStat.Damage=50*(this.aps-1);
                }
                BattleTeam.SkillRandomUse(this.BChar, skill1, false, true, false);
            }
        }
        private int aps = 0;
    }
    public class Relic_Sana_1 : PassiveBase,IP_CriPerChange,IP_ParticleOut_Before
    {
        // Token: 0x06004F81 RID: 20353 RVA: 0x001FE3A4 File Offset: 0x001FC5A4
        public override void Init()
        {
            base.Init();
            this.OnePassive = true;
            this.PlusStat.HitMaximum = true;
            this.PlusStat.hit = 20;
        }
        public void CriPerChange(Skill skill, BattleChar Target, ref float CriPer)
        {
            if(skill.Master.Info.Ally)
            {
                if (Target.NullCheck())
                {
                    return;
                }
                int num = Target.HitPerNum(skill.Master, skill, false);
                int num2 = 0;
                if (num > 100)
                {
                    num2 = num - 100;
                }
                if (num2 > 0)
                {
                    CriPer += (float)num2;
                }
            }
        }
        public void ParticleOut_Before(Skill SkillD, List<BattleChar> Targets)
        {
            if(SkillD.Master.Info.Ally && SkillD.IsDamage)
            {
                Skill_Extended sex = SkillD.ExtendedAdd(new Skill_Extended());
                sex.Traking = true;
            }
        }
    }
}

namespace PD_E_Sana_SaveData
{
    public class PD_E_Sana_Save
    {
        public static PD_E_Sana_Save instance
        {
            get
            {
                bool flag = PD_E_Sana_Save._instance == null;
                if (flag)
                {
                    PD_E_Sana_Save._instance = PD_E_Sana_Save.Load();
                }
                return PD_E_Sana_Save._instance;
            }
        }
        public static string path
        {
            get
            {
                Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "PD_E_Sana"));
                return Path.Combine(Application.persistentDataPath, "PD_E_Sana", "PD_E_Sana_Save.json");
            }
        }
        public void Save()
        {
            File.WriteAllText(PD_E_Sana_Save.path, JsonConvert.SerializeObject(this));
        }
        public static PD_E_Sana_Save Load()
        {
            string value = "";
            bool flag = File.Exists(PD_E_Sana_Save.path);
            if (flag)
            {
                value = File.ReadAllText(PD_E_Sana_Save.path);
            }
            bool flag2 = !string.IsNullOrEmpty(value);
            PD_E_Sana_Save result;
            if (flag2)
            {
                result = JsonConvert.DeserializeObject<PD_E_Sana_Save>(value);
            }
            else
            {
                PD_E_Sana_Save savedata = new PD_E_Sana_Save();
                savedata.Save();
                result = savedata;
            }
            return result;
        }


        public static PD_E_Sana_Save _instance;

        // Token: 0x04000006 RID: 6
        public List<string> NextRun_ItemKeys = new List<string>();
    }

}
