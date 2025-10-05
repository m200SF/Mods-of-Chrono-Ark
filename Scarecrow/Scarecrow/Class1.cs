using ChronoArkMod.Plugin;

using GameDataEditor;
using HarmonyLib;
using I2.Loc;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

using UnityEngine.UI;
using DarkTonic.MasterAudio;
using ChronoArkMod;
using DG.Tweening;
using ChronoArkMod.ModData.Settings;
using UnityEngine.Windows.Speech;
using SF_Standard;
using System.Text.RegularExpressions;

namespace SF_Scarecrow
{
    [PluginConfig("M200_SF_Scarecrow_CharMod", "SF_Scarecrow_CharMod", "1.0.0")]
    public class SF_Scarecrow_CharacterModPlugin : ChronoArkPlugin
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

    }



    [HarmonyPatch(typeof(PrintText))]
    [HarmonyPatch("TextInput")]
    public static class PrintTestPlugin
    {
        // Token: 0x06000014 RID: 20 RVA: 0x00002310 File Offset: 0x00000510
        [HarmonyPrefix]
        public static void TextInput_Before_patch(PrintText __instance, ref string inText)
        {
            float volume = 0f;
            float volume2 = 0f;
            if (ModManager.getModInfo("SF_Scarecrow").GetSetting<ToggleSetting>("AudioOn").Value)
            {
                volume = ModManager.getModInfo("SF_Scarecrow").GetSetting<SliderSetting>("AudioVolume").Value;
            }
            else
            {
                volume = 0f;
            }

            if (inText.Contains("T_scr_1_1"))
            {
                inText = inText.Replace("T_scr_1_1:", string.Empty);
                MasterAudio.PlaySound("A_scr_1_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_1_2"))
            {
                inText = inText.Replace("T_scr_1_2:", string.Empty);
                MasterAudio.PlaySound("A_scr_1_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_2_1"))
            {
                inText = inText.Replace("T_scr_2_1:", string.Empty);
                MasterAudio.PlaySound("A_scr_2_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_2_2"))
            {
                inText = inText.Replace("T_scr_2_2:", string.Empty);
                MasterAudio.PlaySound("A_scr_2_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_2_3"))
            {
                inText = inText.Replace("T_scr_2_3:", string.Empty);
                MasterAudio.PlaySound("A_scr_2_3", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_3_1"))
            {
                inText = inText.Replace("T_scr_3_1:", string.Empty);
                MasterAudio.PlaySound("A_scr_3_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_3_2"))
            {
                inText = inText.Replace("T_scr_3_2:", string.Empty);
                MasterAudio.PlaySound("A_scr_3_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_3_3"))
            {
                inText = inText.Replace("T_scr_3_3:", string.Empty);
                MasterAudio.PlaySound("A_scr_3_3", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_4_1"))
            {
                inText = inText.Replace("T_scr_4_1:", string.Empty);
                MasterAudio.PlaySound("A_scr_4_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_4_2"))
            {
                inText = inText.Replace("T_scr_4_2:", string.Empty);
                MasterAudio.PlaySound("A_scr_4_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_4_3"))
            {
                inText = inText.Replace("T_scr_4_3:", string.Empty);
                MasterAudio.PlaySound("A_scr_4_3", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_5_1"))
            {
                inText = inText.Replace("T_scr_5_1:", string.Empty);
                MasterAudio.PlaySound("A_scr_5_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_5_2"))
            {
                inText = inText.Replace("T_scr_5_2:", string.Empty);
                MasterAudio.PlaySound("A_scr_5_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_6_1"))
            {
                inText = inText.Replace("T_scr_6_1:", string.Empty);
                MasterAudio.PlaySound("A_scr_6_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_6_2"))
            {
                inText = inText.Replace("T_scr_6_2:", string.Empty);
                MasterAudio.PlaySound("A_scr_6_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_6_3"))
            {
                inText = inText.Replace("T_scr_6_3:", string.Empty);
                MasterAudio.PlaySound("A_scr_6_3", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_7_1"))
            {
                inText = inText.Replace("T_scr_7_1:", string.Empty);
                MasterAudio.PlaySound("A_scr_7_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_7_2"))
            {
                inText = inText.Replace("T_scr_7_2:", string.Empty);
                MasterAudio.PlaySound("A_scr_7_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_8_1"))
            {
                inText = inText.Replace("T_scr_8_1:", string.Empty);
                MasterAudio.PlaySound("A_scr_8_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_9_1"))
            {
                inText = inText.Replace("T_scr_9_1:", string.Empty);
                MasterAudio.PlaySound("A_scr_9_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_9_2"))
            {
                inText = inText.Replace("T_scr_9_2:", string.Empty);
                MasterAudio.PlaySound("A_scr_9_2", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_10_1"))
            {
                inText = inText.Replace("T_scr_10_1:", string.Empty);
                MasterAudio.PlaySound("A_scr_10_1", volume2, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_11_1"))
            {
                inText = inText.Replace("T_scr_11_1:", string.Empty);
                MasterAudio.PlaySound("A_scr_11_1", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_11_2"))
            {
                inText = inText.Replace("T_scr_11_2:", string.Empty);
                MasterAudio.PlaySound("A_scr_11_2", volume, null, 0f, null, null, false, false);
            }
            else if (inText.Contains("T_scr_11_3"))
            {
                inText = inText.Replace("T_scr_11_3:", string.Empty);
                MasterAudio.PlaySound("A_scr_11_3", volume, null, 0f, null, null, false, false);
            }
        }
    }










    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("ParticleOutEnd")]
    public static class ParticleOutEndPlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void BattleChar_ParticleOutEnd_Before_patch(BattleChar __instance, Skill skill, List<BattleChar> Target)
        {
            if (__instance.Info.Ally)
            {
                foreach (IP_ParticleOutEnd_Before ip_before in BattleSystem.instance.IReturn<IP_ParticleOutEnd_Before>())
                {
                    bool flag = ip_before != null;
                    if (flag)
                    {
                        ip_before.ParticleOutEnd_Before(skill,Target);
                    }
                }
            }

        }
    }
    public interface IP_ParticleOutEnd_Before
    {
        // Token: 0x06000001 RID: 1
        void ParticleOutEnd_Before(Skill SkillD, List<BattleChar> Targets);
    }

    [HarmonyPatch(typeof(CharEquipInven))]
    [HarmonyPatch("OnDropSlot")]
    public static class CharEquipInvenPlugin
    {
        // Token: 0x06000015 RID: 21 RVA: 0x00002410 File Offset: 0x00000610
        [HarmonyPostfix]
        public static void CharEquipInven_OnDropSlot_patch(ItemBase inputitem, ref bool __result, CharEquipInven __instance)
        {
            bool flag = inputitem.itemkey == "E_SF_Scarecrow_0" && __instance.Info.GetData.Key != "SF_Scarecrow";
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

            if (CharId == "SF_Scarecrow")
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



    public class P_Scarecrow : Passive_Char, IP_BattleStart_Ones, IP_SkillUse_Team_Target, IP_LevelUp//, IP_BuffAdd,IP_BuffAddAfter
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
                list.Add(ItemBase.GetItem("E_SF_Scarecrow_0", 1));
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

        public void BattleStart(BattleSystem Ins)//IP_BattleStart_Ones
        {
            //this.BChar.BuffAdd("B_drm_4_2", this.BChar, false, 0, false, -1, false);
            while (!this.BChar.BuffFind("B_SF_Scarecrow_p", false) || this.BChar.BuffReturn("B_SF_Scarecrow_p", false).StackNum < 5/*(int)(0.5 * (5 + this.BChar.Info.LV))*/)
            {
                this.BChar.BuffAdd("B_SF_Scarecrow_p", this.BChar, false, 0, false, -1, false);
            }
        }
        //public void SkillUseTeam(Skill skill)// IP_SkillUse_User
        public void SkillUseTeam_Target(Skill skill, List<BattleChar> Targets)//IP_SkillUse_Team_Target
        {
            int frame = Time.frameCount;
            if(frame!=this.frame && this.lastskill!=skill && skill.Master.Info.Ally && !skill.FreeUse)
            {
                if (skill.Master == this.BChar)
                {
                    this.ReCharge(skill, 1/*skill.UsedApNum*/ );
                }
                /*
                if (skill.Master == this.BChar)
                {
                    this.ReCharge(skill,skill.UsedApNum+2);
                    //this.ReCharge(skill,skill.AP+1);
                }
                else
                {
                    this.ReCharge(skill,skill.UsedApNum);
                    //this.ReCharge(skill,skill.AP);
                }
                */
            }
            this.lastskill = skill;
            this.frame = frame;
            //int stack = 0;
            //bool buffed = this.BChar.BuffFind("B_SF_Scarecrow_p", false);
            //if (buffed)
            //{
            //stack = this.BChar.BuffReturn("B_SF_Scarecrow_p", false).StackNum;
            //}

        }
        private Skill lastskill;
        private int frame=0;


        private void ReCharge(Skill skill,int num)
        {
            //yield return new WaitForFixedUpdate();
            for (int i = 0; i < num; i++)
            {
                this.BChar.BuffAdd("B_SF_Scarecrow_p", this.BChar, false, 0, false, -1, false);
            }
        }
        

        private bool oldbuff = false;

    }

    public interface IP_ChangeFloatingsMax
    {
        // Token: 0x0600000A RID: 10
        int ChangeFloatingsMax(BattleChar BC);
    }
    public class Bcl_scr_p : Buff, IP_ParticleOutEnd_Before, IP_TurnEnd,/*IP_SkillUse_User,*/ IP_BuffAdd, IP_BuffAddAfter,IP_SkillUse_Team_Target,IP_PlayerTurn, IP_BuffUpdate
    {
        public override string DescExtended()
        {
            string strtype = "未知";

            if (this.Tartype == 0)
            {
                strtype = (new GDESkillData("S_SF_Scarecrow_1_0")).Description;
            }
            else if (this.Tartype == 1)
            {
                strtype = (new GDESkillData("S_SF_Scarecrow_1_1")).Description;
            }
            else if (this.Tartype == 2)
            {
                strtype = (new GDESkillData("S_SF_Scarecrow_1_2")).Description;
            }
            else if (this.Tartype == 3)
            {
                strtype = (new GDESkillData("S_SF_Scarecrow_1_3")).Description;
            }
            string typeturn = "未知";
            if(this.RemainTurn==0 || this.Tartype == 0)
            {
                typeturn = " ";
            }
            else
            {
                GDESkillKeywordData k1 = new GDESkillKeywordData("KeyWord_SF_Scarecrow_p_1");
                typeturn = "("+(this.RemainTurn).ToString()+k1.Desc+")";
            }
            return base.DescExtended().Replace("&a", strtype).Replace("&d", typeturn).Replace("&b", (this.Recharge).ToString()).Replace("&c", (3+1* base.StackNum).ToString()).Replace("&e", this.shootrecharge.ToString()).Replace("&f", this.shootrechargemax.ToString());
        
        }
        public override void Init()
        {
            base.Init();
            //this.BuffData.MaxStack = (int)((5 + this.BChar.Info.LV)/2);
            this.CheckMaxstack();
        }
        public override void BuffStat()
        {
            base.BuffStat();
            bool flag1 = false;
            foreach (IP_CheckEquip ip_this in this.BChar.IReturn<IP_CheckEquip>())
            {
                bool flag = ip_this != null;
                if (flag)
                {
                    flag1=flag1||ip_this.CheckEquip(this.BChar);
                }
            }
            if(flag1)
            {
                this.shootrechargemax = 4;
            }
            else
            {
                this.shootrechargemax = 5;
            }
        }
        public void BuffUpdate(Buff MyBuff)
        {
            this.CheckMaxstack();
        }
        public void CheckMaxstack()
        {
            int maxChange = 0;
            foreach (BattleChar bc in this.BChar.MyTeam.AliveChars_Vanish)
            {
                foreach (Buff buff in bc.Buffs)
                {
                    IP_ChangeFloatingsMax ip_1 = buff as IP_ChangeFloatingsMax;
                    if (ip_1 != null)
                    {
                        maxChange = maxChange + ip_1.ChangeFloatingsMax(this.BChar);
                    }
                }
            }
            this.BuffData.MaxStack = 5 + maxChange;
        }
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();

            for (int i = 0; i < BattleSystem.instance.CastSkills.Count; i++)
            {
                if (BattleSystem.instance.CastSkills[i].skill.MySkill.KeyID == "S_SF_Scarecrow_0" && BattleSystem.instance.CastSkills[i].skill.Master==this.BChar)
                {
                    //Debug.Log("第一层浮游炮清除之前所有浮游炮射击");
                    BattleSystem.instance.ActWindow.CastingWaste(BattleSystem.instance.CastSkills[i]);
                    BattleSystem.instance.CastSkills.RemoveAt(i);
                    i--;
                }
            }
        }
        
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)//满浮游炮时突破上限
        {


            if (!oldbuff && BuffTaker == this.BChar && addedbuff.BuffData.Key == "B_SF_Scarecrow_p")
            {
                bool flag1 = this.BChar.BuffFind("B_SF_Scarecrow_p", false);
                int stk = 0;
                if (flag1)
                {
                    Bcl_scr_p buff = this.BChar.BuffReturn("B_SF_Scarecrow_p", false) as Bcl_scr_p;
                    stk = buff.StackNum;
                    if (stk >= buff.BuffData.MaxStack)
                    {
                        //buff.overStack++;
                        //buff.BuffData.MaxStack++;
                        StackBuff s1 = buff.StackInfo[0];
                        buff.StackInfo.Insert(0, s1);
                    }
                }
            }
            this.oldbuff = true;
        }
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            this.oldbuff = false;
        }
        

        //public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        public void SkillUseTeam_Target(Skill skill, List<BattleChar> Targets)
        {

            int frame = Time.frameCount;
            if (frame != this.frame && this.lastskill != skill && skill.Master.Info.Ally && !skill.FreeUse &&!skill.isExcept)
            {
                this.shootrecharge++;
                if (this.shootrecharge >= this.shootrechargemax)
                {
                    this.shootrecharge = 0;
                    if (BattleSystem.instance.EnemyTeam.AliveChars.Count >= 1)
                    {
                        Skill skill2 = Skill.TempSkill("S_SF_Scarecrow_0", this.BChar, this.BChar.MyTeam);
                        skill2.Counting = 20;
                        BattleTeam.SkillRandomUse(this.BChar, skill2, false, true, false);
                        //Debug.Log("装填触发，浮游炮生成");
                    }
                    else
                    {
                        this.watingShoot++;
                        //Debug.Log("装填触发，浮游炮等待生成计数：" + co.watingShoot);
                    }
                }
            }

        }
        private Skill lastskill;
        private int frame = 0;


        public override void TurnUpdate()
        {
            base.TurnUpdate();


            /*
            if (this.RemainTurn>=1)
            {
                this.RemainTurn--;
                if(this.RemainTurn == 0)
                {
                    this.Tartype = 0;
                }
            }
            */
            for (int i = 0; i < this.logiclist.Count; i++)//攻击逻辑持续回合减少
            {
                logiclist[i].turn--;
                if (logiclist[i].turn <= 0)
                {
                    logiclist.RemoveAt(i);
                    i--;
                }
            }
            if(this.logiclist.Count>0)
            {
                this.Tartype = this.logiclist[this.logiclist.Count-1].type;
                this.RemainTurn = this.logiclist[this.logiclist.Count-1].turn;
            }
            else
            {
                this.Tartype = this.orgType;
                this.RemainTurn = 0;
            }



            this.ClearFloatingGun();
            //this.beforeTurn = false;
        }
        public void Turn()
        {
            this.beforeTurn = false;

            //this.ClearFloatingGun();
        }
        public void TurnEnd()
        {
            this.beforeTurn = true;
            this.turnShooted=false;


            //this.ClearFloatingGun();
        }
        public void ClearFloatingGun()
        {
            bool reset = true;
            bool remainHalf = false;
            foreach (IP_FloatGunReset ip_this in this.BChar.IReturn<IP_FloatGunReset>())
            {
                bool flag = ip_this != null;
                if (flag)
                {
                    bool prevent = false;
                    bool preventHalf = false;
                    ip_this.FloatGunReset(this.BChar, ref prevent,ref preventHalf);
                    reset = reset && !prevent;
                    remainHalf = remainHalf || preventHalf;
                }
            }
            if (reset)
            {
                int num = Math.Max(0,this.StackInfo.FindAll(a => a.UseState == this.BChar).Count - this.BuffData.MaxStack);
                int num2 = 0;
                if(remainHalf && num>0)
                {
                    num2 = (int)(num / 2);
                    num2=Math.Max(1,num2);
                }
                else
                {
                    num2 = num;
                }

                for(int i=0; i<num2; i++) 
                {
                    this.StackInfo.Remove(this.StackInfo.Find(a => a.UseState == this.BChar));
                }
                /*
                while (this.StackNum > this.BuffData.MaxStack && this.StackInfo.Find(a=>a.UseState==this.BChar)!=null)
                {
                    //this.StackInfo.RemoveAt(0);
                    this.StackInfo.Remove(this.StackInfo.Find(a => a.UseState == this.BChar));
                }
                */
            }
        }
        public void ParticleOutEnd_Before(Skill SkillD, List<BattleChar> Targets)
        {
            if (Targets.Count >0 && SkillD.IsDamage)//记录最后攻击的敌人
            {
                this.LastTar = Targets[Targets.Count-1];
                //Debug.Log("稻草人 Sex_1  LastTar:" + this.LastTar.Info.Name);

                this.HistoryTar.RemoveAll(a=>Targets.Contains(a)||a.IsDead ||a==null);
                this.HistoryTar.AddRange(Targets);
                //Debug.Log("记录最后攻击的敌人，技能：" + SkillD.MySkill.Name);
                //if (HistoryTar.Count >= BattleSystem.instance.EnemyTeam.AliveChars.Count)//记录最近攻击的敌人
                //{
                    //HistoryTar.RemoveAt(0);
                //}

            }
            //if (SkillD.MySkill.KeyID == "S_SF_Scarecrow_0_1")
            //{
                //SkillD.PlusHit = true;
            //}
        }
        public override void FixedUpdate()
        {
            /*
            if (HistoryTar.Count >= BattleSystem.instance.EnemyTeam.AliveChars.Count)//记录最近攻击的敌人
            {
                HistoryTar.RemoveAt(0);
            }
            */
            //
            if(BattleSystem.instance!=null && BattleSystem.instance.TurnNum>0)
            {
                this.unlock = true;
            }
            if(!this.unlock)
            {
                return;
            }
            if(this.watingShoot > 0&& BattleSystem.instance.EnemyTeam.AliveChars.Count >= 1)//在有敌人的情况下，生成之前累积的射击
            {

                Skill skill = Skill.TempSkill("S_SF_Scarecrow_0", this.BChar, this.BChar.MyTeam);
                skill.Counting = 20;
                BattleTeam.SkillRandomUse(this.BChar, skill, false, true, false);
                this.watingShoot--;
            }
            //
            if(!this.turnShooted && !this.beforeTurn && BattleSystem.instance.EnemyTeam.AliveChars.Count >= 1)
            {
                Skill skill = Skill.TempSkill("S_SF_Scarecrow_0", this.BChar, this.BChar.MyTeam);
                skill.Counting = 20;
                BattleTeam.SkillRandomUse(this.BChar, skill, false, true, false);
                this.turnShooted = true;
            }
        }
        public bool unlock = false;
        public void SetType(int type, int turnnum)
        {
            this.Tartype = type;
            this.RemainTurn = turnnum;
            if(turnnum > 0)
            {
                FloatingLogic addone = new FloatingLogic();
                addone.type = type;
                addone.turn = turnnum;
                for (int i = 0; i < this.logiclist.Count; i++)
                {
                    if (logiclist[i].turn <= turnnum)
                    {
                        logiclist.RemoveAt(i);
                        i--;
                    }
                }
                logiclist.Add(addone);
            }
            else
            {
                this.orgType = type;
            }
        }
        public static IEnumerator SetTypeDelay(BattleChar bc,int type, int turnnum)
        {
            yield return new WaitForFixedUpdate();
            try
            {
                Bcl_scr_p co = bc.BuffReturn("B_SF_Scarecrow_p", false) as Bcl_scr_p;
                co.SetType(type, turnnum);
            }
            catch { }
            yield break;
        }

        public int Tartype = 0;
        public int RemainTurn = 0;
        public List<FloatingLogic> logiclist = new List<FloatingLogic>();
        public int orgType=0;

        public bool oldbuff;

        public BattleChar LastTar = null;
        public List<BattleChar> HistoryTar = new List<BattleChar>();
        public int Recharge = 0;
        public int watingShoot;
        public bool turnShooted;
        public bool beforeTurn;
        public int shootrecharge = 0;
        public int shootrechargemax = 5;
    }

    public class FloatingLogic
    {
        public int type;
        public int turn;
    
    }

    public interface IP_FloatGunReset
    {
        // Token: 0x06000001 RID: 1
        void FloatGunReset(BattleChar bc,ref bool prevent,ref bool preventHalf);
    }

    public class Sex_scr_0 : Sex_ImproveClone, IP_SkillCastingStart
    {
        public void SkillCasting(CastingSkill ThisSkill)
        {
            //base.BuffOneAwake();
            //Debug.Log("Go check1");
            //CastingSkill cas = BattleSystem.instance.CastSkills.Find(a => a.skill == this.MySkill);
            //Button button1 = ThisSkill.CastButton.MpIconObject.AddComponent<Button>();
            //button1.onClick.AddListener(new UnityAction(this.Go));
            ThisSkill.CastButton.ClickDelegate = new SkillButton.SkillClickDel(this.Go);

            //Debug.Log("Go check2");
            //BattleSystem.instance.ActWindow.CastingWaste(BattleSystem.instance.CastSkills[i].skill);
            //BattleSystem.instance.CastSkills.RemoveAt(i);
        }
        public void Go(SkillButton Mybutton)
        {
            //Debug.Log("Go!!!");
            if(!BattleSystem.instance.DelayWait)
            {
                CastingSkill castingSkill = BattleSystem.instance.CastSkills.Find(a => a.skill == this.MySkill);
                if (castingSkill != null )//&& this.MySkill.Master.Overload<3)
                {
                    //Debug.Log("S0 CheackPoint B1");
                    //this.MySkill.Master.Overload++;
                    BattleSystem.DelayInput(this.Go2(castingSkill));
                    //this.Go2(castingSkill);
                    //Debug.Log("S0 CheackPoint B2");
                }
            }
            
        }
        public IEnumerator Go2(CastingSkill castingSkill)
        {
            //Debug.Log("S0 CheackPoint A1");
            //yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyCastingSkillUse(castingSkill, false));
            //yield return BattleSystem.instance.AllyCastingSkillUse(castingSkill, false);
            castingSkill.Use();
            //BattleSystem.instance.ActWindow.CastingWaste(castingSkill.skill);
            //castingSkill.Usestate.UseSkill(castingSkill.skill);
            //castingSkill.Usestate.SelfEffect(castingSkill.skill);
            //Debug.Log("S0 CheackPoint A2");
            try
            {
                //BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyCastingSkillUse(castingSkill, false));
                //Skill coskill = castingSkill.skill.CloneSkill(true, null, null, true);
                //coskill.Counting = 0;
                //BattleTeam.SkillRandomUse(this.BChar, coskill, false, true, false);
                BattleSystem.instance.ActWindow.CastingWaste(castingSkill);
                BattleSystem.instance.CastSkills.Remove(castingSkill);
                BattleSystem.instance.SaveSkill.Remove(castingSkill);
                //Debug.Log("S0 CheackPoint A3");
            }
            catch { }


            //yield return null;
            yield break;
        }
        //public override void Init()
        //{
            //base.Init();
            //this.CountingExtedned = true;
        //}
        public BattleChar GetTarget()//选择目标
        {
            if (BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Contains(this.unicTarget))//(this.unicType>=0)
            {
                return this.unicTarget;//TarType = this.unicType;
            }

            Bcl_scr_p co = this.BChar.BuffReturn("B_SF_Scarecrow_p",false) as Bcl_scr_p;
            int TarType = co.Tartype;



            BattleChar Tar=new BattleChar();

            if (TarType == 0)//随机
            {
                Tar=BattleSystem.instance.EnemyTeam.AliveChars.Random(this.BChar.GetRandomClass().Main);
            }
            else if (TarType == 1)//上次攻击的目标
            {
                if(co.LastTar == null || co.LastTar.IsDead || co.LastTar.Info.Ally==this.BChar.Info.Ally)
                {
                    //int lastalive=co.HistoryTar.Count-1;
                    //while (co.HistoryTar[lastalive].IsDead&&lastalive>=0)
                    //{
                        //lastalive--;
                    //}
                    //if(lastalive>=0)
                    //{
                        //return co.HistoryTar[lastalive];
                    //}
                    //else
                    //{
                        Tar=BattleSystem.instance.EnemyTeam.AliveChars.Random(this.BChar.GetRandomClass().Main);
                    //}
                }
                else
                {
                    Tar=co.LastTar;
                }

            }
            else if (TarType == 2) //血量最低
            {
                List<BattleChar> list = new List<BattleChar>();
                list.AddRange(BattleSystem.instance.EnemyTeam.AliveChars);
                list = (from x in list
                        orderby x.HP
                        select x).ToList<BattleChar>();

                int tarhp = list[0].HP;
                for (int i = 0; i <= list.Count - 1; i++)
                {
                    if (list[i].HP > tarhp)
                    {
                        list.RemoveAt(i);
                        i--;
                    }
                }

                Tar= list.Random(this.BChar.GetRandomClass().Main);
            }
            else if (TarType == 3)//
            {
                List<BattleChar> list2 = BattleSystem.instance.EnemyTeam.AliveChars;
                List<BattleChar> list3 = new List<BattleChar>();
                list3.AddRange(co.HistoryTar);
                list3.Reverse();

                foreach (BattleChar chra in list3)
                {
                    if(list2.Count<=1)
                    {
                        break;
                    }
                    int at = list2.FindIndex(a => a == chra);
                    if (at != -1)
                    {
                        list2.RemoveAt(at);
                    }
                }
                Tar= list2.Random(this.BChar.GetRandomClass().Main);
            }
            else
            {
                Tar = BattleSystem.instance.EnemyTeam.AliveChars.Random(this.BChar.GetRandomClass().Main);
            }
            //Debug.Log("GetTarget :" + TarType +"--"+ Tar.Info.Name);
            return Tar;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)//射击
        {
            for (int i = 1; i <= SkillD.Master.BuffReturn("B_SF_Scarecrow_p", false).StackNum; i++)
                {
                    if (BattleSystem.instance.EnemyTeam.AliveChars.Count >= 1)
                    {
                    BattleSystem.DelayInput(this.Plushit());
                    }

                }
        }
        public IEnumerator Plushit()//追加射击
        {

            Skill skill = Skill.TempSkill("S_SF_Scarecrow_0_1", this.BChar, this.BChar.MyTeam);//
            skill.PlusHit = true;
            skill.FreeUse = true;
            BattleChar ScrTarget = GetTarget();
            //Debug.Log("PlusHit ScrTarget:" + ScrTarget.Info.Name);
            this.BChar.ParticleOut(skill, ScrTarget);
            yield return new WaitForSecondsRealtime(0.2f);
            yield break;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(this.MySkill.IsNowCasting && !this.BChar.BuffFind("B_SF_Scarecrow_p",false))//释放角色没有浮游炮时，从倒计时栏移除射击
            {
                for (int i = 0; i < BattleSystem.instance.CastSkills.Count; i++)
                {
                    if(BattleSystem.instance.CastSkills[i].skill.MySkill==this.MySkill.MySkill)
                    {
                        //Debug.Log("无浮游炮时移除浮游炮射击");
                        BattleSystem.instance.ActWindow.CastingWaste(BattleSystem.instance.CastSkills[i]);
                        BattleSystem.instance.CastSkills.RemoveAt(i);
                        i--;
                    }
                }

            }
        }
        public int unicType = -1;
        public BattleChar unicTarget=new BattleChar();
    }

    public class Sex_scr_1 : Sex_ImproveClone//指挥
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            /*
            Skill skill = Skill.TempSkill("S_SF_Scarecrow_0", this.BChar, this.BChar.MyTeam);//置入浮游炮射击
            skill.Counting = 20;
            BattleTeam.SkillRandomUse(this.BChar, skill, false, true, false);
            */

            base.SkillUseSingle(SkillD, Targets);
            List<Skill> list = new List<Skill>();
            list.Add(Skill.TempSkill("S_SF_Scarecrow_1_0", this.MySkill.Master, this.MySkill.Master.MyTeam));
            list.Add(Skill.TempSkill("S_SF_Scarecrow_1_1", this.MySkill.Master, this.MySkill.Master.MyTeam));
            list.Add(Skill.TempSkill("S_SF_Scarecrow_1_2", this.MySkill.Master, this.MySkill.Master.MyTeam));
            list.Add(Skill.TempSkill("S_SF_Scarecrow_1_3", this.MySkill.Master, this.MySkill.Master.MyTeam));

            BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(this.Del), ScriptLocalization.System_SkillSelect.EffectSelect, false, false, true, false, false));
        }
        public void Del(SkillButton Mybutton)
        {
            int type = 0;
            if (Mybutton.Myskill.MySkill.KeyID == "S_SF_Scarecrow_1_0")
            {
                type = 0;
            }
            if (Mybutton.Myskill.MySkill.KeyID == "S_SF_Scarecrow_1_1")
            {
                type = 1;
            }
            if (Mybutton.Myskill.MySkill.KeyID == "S_SF_Scarecrow_1_2")
            {
                type = 2;
            }
            if (Mybutton.Myskill.MySkill.KeyID == "S_SF_Scarecrow_1_3")
            {
                type = 3;
            }
            BattleSystem.DelayInput(Bcl_scr_p.SetTypeDelay(this.BChar, type, 0));
            //Bcl_scr_p co = this.BChar.BuffReturn("B_SF_Scarecrow_p") as Bcl_scr_p;
            //co.SetType(type, 0);
        }
    }

    public class Sex_scr_2 : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            //int ind = SkillD.MySkill.Effect_Target.Buffs.FindIndex(a => a.Key == "B_SF_Scarecrow_2_T");
            //SkillD.MySkill.Effect_Target.Buffs.RemoveAt(ind);
            //base.SkillUseSingle(SkillD, Targets);

            BattleSystem.DelayInput(Bcl_scr_p.SetTypeDelay(this.BChar, 3, 1));
            //Bcl_scr_p co = this.BChar.BuffReturn("B_SF_Scarecrow_p") as Bcl_scr_p;
            //co.SetType(3, 1);
        }
    }

    public class Bcl_scr_2_S : Buff, IP_SkillUse_Target, IP_SkillMiss_User //IP_ParticleOutEnd_Before
    {
        public override string DescExtended()
        {
            int num = 70 + (int)(this.BChar.GetStat.HIT_DEBUFF);
            return base.DescExtended().Replace("&a", num.ToString());
        }
        /*
        public void ParticleOutEnd_Before(Skill SkillD, List<BattleChar> Targets)//给技能附加EX版本，EX施加buff版本
        {

            if (SkillD.MySkill.KeyID == "S_SF_Scarecrow_0_1"&& SkillD.ExtendedFind_DataName("EX_SF_Scarecrow_2") == null)
            {
                Extended_scr_2 ex1 = Skill_Extended.DataToExtended("EX_SF_Scarecrow_2")as Extended_scr_2;
                SkillD.ExtendedAdd(ex1);
            }
            
        }
        */
        public void MissEffect(BattleChar hit, SkillParticle SP)//直接施加buff版本
        {
            if (SP.SkillData.MySkill.KeyID == "S_SF_Scarecrow_0_1" && SP.UseStatus==this.BChar)
            {
                hit.BuffAdd("B_SF_Scarecrow_2_T", this.BChar, false, -40, false, -1, false);
            }  
        }
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)//直接施加buff版本
        {
            if (SP.SkillData.MySkill.KeyID == "S_SF_Scarecrow_0_1" && SP.UseStatus == this.BChar)
            {
                hit.BuffAdd("B_SF_Scarecrow_2_T", this.BChar, false, -40, false, -1, false);
            }
        }
    }
    public class Bcl_scr_2_T : Buff, IP_SkillUse_User_After
    {
        // Token: 0x060010B5 RID: 4277 RVA: 0x0008EDFE File Offset: 0x0008CFFE
        public override void Init()
        {
            base.Init();
            double k = 1;
            k = 2 - 2 * Math.Pow(0.5,this.StackNum);
            //this.PlusPerStat.Damage = (int)(-8*k);
            //this.PlusStat.spd = -2;
            this.PlusStat.hit = (float)(-8 * k);
            this.PlusStat.dod= (float)(-16 * k);
            this.PlusStat.Weak = true;
        }

        // Token: 0x060010B6 RID: 4278 RVA: 0x0008EE1F File Offset: 0x0008D01F
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
    public class Extended_scr_2 : Sex_ImproveClone,IP_SkillUse_Target
    {

        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            //this.BChar.BuffAdd("B_SF_Scarecrow_check_2", this.BChar, false, 0, false, -1, false);//check2
            //base.SkillUseSingle(SkillD, Targets);
            if(Standard_Tool.JudgeSkillSex(SP.SkillData,this))
            {
                hit.BuffAdd("B_SF_Scarecrow_2_T", this.BChar, false, -40, false, -1, false);
            }
        }

    }


    public class Sex_scr_3 : Sex_ImproveClone
    {

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            //int ind=SkillD.MySkill.Effect_Target.Buffs.FindIndex(a=>a.Key=="B_SF_Scarecrow_3_T");
            //SkillD.MySkill.Effect_Target.Buffs.RemoveAt(ind);
            //base.SkillUseSingle(SkillD, Targets);

            BattleSystem.DelayInput(Bcl_scr_p.SetTypeDelay(this.BChar, 3, 1));
            //Bcl_scr_p co = this.BChar.BuffReturn("B_SF_Scarecrow_p") as Bcl_scr_p;
            //co.SetType(3, 1);
        }
    }

    public class Bcl_scr_3_S : Buff, IP_SkillUse_Target, IP_SkillMiss_User //IP_ParticleOutEnd_Before
    {
        public override string DescExtended()
        {
            int num = 70 + (int)(this.BChar.GetStat.HIT_DEBUFF);
            return base.DescExtended().Replace("&a", num.ToString());
        }
        /*
        public void ParticleOutEnd_Before(Skill SkillD, List<BattleChar> Targets)//给技能附加EX版本，EX施加buff版本
        {

            if (SkillD.MySkill.KeyID == "S_SF_Scarecrow_0_1" && SkillD.ExtendedFind_DataName("EX_SF_Scarecrow_3") == null)
            {

                Extended_scr_3 ex1 = Skill_Extended.DataToExtended("EX_SF_Scarecrow_3") as Extended_scr_3;
                SkillD.ExtendedAdd(ex1);

            }
            
        }
        */
        public void MissEffect(BattleChar hit, SkillParticle SP)//直接施加buff版本
        {
            if (SP.SkillData.MySkill.KeyID == "S_SF_Scarecrow_0_1" && SP.UseStatus == this.BChar)
            {
                hit.BuffAdd("B_SF_Scarecrow_3_T", this.BChar, false, -40, false, -1, false);
            }
        }
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)//直接施加buff版本
        {
            if (SP.SkillData.MySkill.KeyID == "S_SF_Scarecrow_0_1" && SP.UseStatus == this.BChar)
            {
                hit.BuffAdd("B_SF_Scarecrow_3_T", this.BChar, false, -40, false, -1, false);
            }
        }
    }
    /*
    public class Bcl_scr_3_T : Buff, IP_SkillUse_User_After
    {
        // Token: 0x060010B5 RID: 4277 RVA: 0x0008EDFE File Offset: 0x0008CFFE
        public override void Init()
        {
            base.Init();
            this.PlusStat.spd = -1;
            //this.PlusStat.dod = -40;
            //this.PlusStat.Weak = true;
        }

        // Token: 0x060010B6 RID: 4278 RVA: 0x0008EE1F File Offset: 0x0008D01F
        public void SkillUseAfter(Skill SkillD)
        {
            this.BChar.BuffAdd("B_SF_Scarecrow_3_stun", this.Usestate_L, false, 999, false, -1, false);
        }


    }
    */
    public class Bcl_scr_3_T : Buff,IP_BuffAdd,IP_BuffAddAfter//, IP_SkillUse_User_After
    {
        // Token: 0x060010B5 RID: 4277 RVA: 0x0008EDFE File Offset: 0x0008CFFE
        public override void Init()
        {
            base.Init();
            this.PlusStat.spd = -1;

        }

        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {

            this.remainturn = 0;
                foreach(StackBuff sta in this.StackInfo)
                {
                    this.remainturn = Math.Max(this.remainturn,sta.RemainTime);
                    //Debug.Log("this.remainturn=" + this.remainturn);
                }
            
        }
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {

            //Debug.Log("bcl3_point_1");
            if(this.remainturn>0 && addedbuff==this)
            {
                //Debug.Log("bcl3_point_2");
                foreach (StackBuff sta in this.StackInfo)
                {
                    //Debug.Log("bcl3_point_3");
                    if (this.remainturn < 3)
                    {
                        //Debug.Log("bcl3_point_4:"+ this.remainturn);
                        sta.RemainTime = this.remainturn + 1;
                        //Debug.Log("bcl3_point_5:" + sta.RemainTime);
                    }
                    else
                    {
                        sta.RemainTime = this.remainturn;
                    }
                }

            }
            this.remainturn = 0;

        }
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            BattleEnemy battleEnemy = this.BChar as BattleEnemy;
            if (!this.doing && battleEnemy.SkillQueue.Count > 1)
            {
                this.doing = true;
                BattleSystem.DelayInputAfter(this.ActiveGrounding());
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            BattleEnemy battleEnemy = this.BChar as BattleEnemy;
            if(!this.doing && battleEnemy.SkillQueue.Count>1)
            {
                this.doing = true;
                BattleSystem.DelayInputAfter(this.ActiveGrounding());
            }
        }

        public IEnumerator ActiveGrounding()
        {
            try
            {
                BattleEnemy battleEnemy = this.BChar as BattleEnemy;
                List < CastingSkill > list1= new List<CastingSkill>();
                list1.AddRange(battleEnemy.SkillQueue);

                /*
                SkillButton[] componentsInChildren = BattleSystem.instance.ActWindow.CastingGroup.GetComponentsInChildren<SkillButton>();
                List<SkillButton> list2 = new List<SkillButton>();
                foreach (SkillButton skillButton in componentsInChildren)
                {
                    if (skillButton.CharData == this.BChar && skillButton.Myskill != list1[0].skill)
                    {
                        list2.Add(skillButton);
                    }
                }
                foreach(SkillButton sb in list2 )
                {
                    BattleSystem.instance.ActWindow.CastingWaste(sb.castskill);
                }
                */
                //BattleSystem.instance.ActWindow.CastingWaste(this.BChar);
                for (int i = 1; i < list1.Count; i++)
                {
                    //Debug.Log("移除: "+ list1[i].skill.MySkill.Name);
                    //BattleSystem.instance.ActWindow.CastingWaste(list1[i]);
                    this.CastingWaste(list1[i]);
                    BattleSystem.instance.EnemyCastSkills.Remove(list1[i]);
                    BattleSystem.instance.SaveSkill.Remove(list1[i]);
                }
                /*
                for (int i = 1; i < list1.Count; i++)
                {
                    BattleSystem.instance.SaveSkill.Remove(list1[i]);
                }
                */
            }
            catch
            {
                Debug.Log("稻草人:Bcl_scr_3_T:ERROR");
            }
            this.doing = false;
            yield return null;
            yield break;
        }

        public void CastingWaste(CastingSkill cast)
        {
            Skill skill = cast.skill;
            SkillButton[] componentsInChildren = BattleSystem.instance.ActWindow.CastingGroup.GetComponentsInChildren<SkillButton>();
            SkillButton skillButton = null;
            foreach (SkillButton skillButton2 in componentsInChildren)
            {
                if (skillButton2.castskill == cast)
                {
                    skillButton = skillButton2;
                    break;
                }
            }
            foreach (IP_SkillCastingQuit ip_SkillCastingQuit in cast.skill.IReturn<IP_SkillCastingQuit>())
            {
                if (ip_SkillCastingQuit != null)
                {
                    ip_SkillCastingQuit.SkillCastingQuit(cast);
                }
            }
            if (skillButton != null)
            {
                skillButton.UseWaste();
            }
            BattleSystem.instance.ActWindow.SetCountSkillVL((BattleSystem.instance.ActWindow.CastingGroup.GetComponentsInChildren<SkillButton>().Length >= 13) ? 30 : 45);
        }

        private bool doing = false;
        private int remainturn = 0;
    }
    public class Bcl_scr_3_stun : Buff, IP_PlayerTurn
    {
        public override void Init()
        {
            this.PlusStat.Stun = true;
            //this.NoShowTimeNum_Tooltip = true;
        }

        public void Turn()
        {
            base.SelfDestroy();
        }
    }
    public class Extended_scr_3 : Sex_ImproveClone
    {

        public override void AttackEffectSingle(BattleChar hit, SkillParticle SP, int DMG, int Heal)
        {
            //Debug.Log("命中,bcl3");
            //this.BChar.BuffAdd("B_SF_Scarecrow_check_2", this.BChar, false, 0, false, -1, false);//check2
            //base.SkillUseSingle(SkillD, Targets);
            if (Standard_Tool.JudgeSkillSex(SP.SkillData, this))
            {
                hit.BuffAdd("B_SF_Scarecrow_3_T", this.BChar, false, -40, false, -1, false);

            }
        }

    }


    public class Sex_scr_4 : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BattleSystem.DelayInput(this.Delay());
            //Bcl_scr_p co = this.BChar.BuffReturn("B_SF_Scarecrow_p",false) as Bcl_scr_p;
            //co.SetType(1, 1);


        }
        public IEnumerator Delay()
        {
            yield return Bcl_scr_p.SetTypeDelay(this.BChar, 1, 1);
            Skill skill = Skill.TempSkill("S_SF_Scarecrow_0", this.BChar, this.BChar.MyTeam);
            skill.Counting = 0;
            BattleTeam.SkillRandomUse(this.BChar, skill, false, true, false);

            yield break;
        }
    }




    public class Bcl_scr_5_S : Buff, IP_CriPerChange// ,IP_ParticleOutEnd_Before
    {
        /*
        public void ParticleOutEnd_Before(Skill SkillD, List<BattleChar> Targets)//给技能附加EX版本，EX施加buff版本
        {
            if (SkillD.MySkill.KeyID == "S_SF_Scarecrow_0_1" && SkillD.ExtendedFind_DataName("EX_SF_Scarecrow_5_1") == null && SkillD.ExtendedFind_DataName("EX_SF_Scarecrow_5_2") == null)
            {

                //Character cha = Targets[0].Info;
                GDEEnemyData edata = new GDEEnemyData(Targets[0].Info.KeyData);
                List<GDEBuffData> epas = edata.Passives;
                List<Buff> list = new List<Buff>();

                bool hasDebuff=false;
                foreach (Buff buff in Targets[0].Buffs)
                {
                    if (!buff.BuffData.Debuff && !buff.BuffData.Cantdisable && !buff.BuffData.Hide && !buff.DestroyBuff  && !buff.PlusStat.invincibility)// && buff.LifeTime>0)
                    {
                        if(epas.Find(a => a.Key == buff.BuffData.Key) == null)//排除被动buff
                        {
                            hasDebuff = true;
                            list.Add(buff);
                        }

                    }
                }
                if (hasDebuff)
                {
                    Extended_scr_5_1 ex1 = Skill_Extended.DataToExtended("EX_SF_Scarecrow_5_1") as Extended_scr_5_1;
                    ex1.listcan = list;
                    SkillD.ExtendedAdd(ex1);
                }
                else
                {
                    Extended_scr_5_2 ex2 = Skill_Extended.DataToExtended("EX_SF_Scarecrow_5_2") as Extended_scr_5_2;
                    SkillD.ExtendedAdd(ex2);
                    //Skill_Extended skill_Extended = new Skill_Extended();
                    //skill_Extended.PlusSkillStat.cri = 100f;
                    //SkillD.ExtendedAdd(skill_Extended);
                    //Debug.Log("浮游炮触发必爆");
                }

            }
        }
        */
        public void CriPerChange(Skill skill, BattleChar Target, ref float CriPer)//直接解除buff或增加暴击率版本
        {
            if (skill.MySkill.KeyID == "S_SF_Scarecrow_0_1" && skill.Master==this.BChar)
            {

                //Character cha = Targets[0].Info;
                GDEEnemyData edata = new GDEEnemyData(Target.Info.KeyData);
                List<GDEBuffData> epas = edata.Passives;
                List<Buff> list = new List<Buff>();

                bool hasDebuff = false;
                foreach (Buff buff in Target.Buffs)
                {
                    if (!buff.BuffData.Debuff && !buff.BuffData.Cantdisable && !buff.BuffData.Hide && !buff.DestroyBuff && !buff.PlusStat.invincibility)// && buff.LifeTime>0)
                    {
                        //if (epas.Find(a => a.Key == buff.BuffData.Key) == null)//排除被动buff
                        if (buff.BuffData.MaxStack>1 || buff.StackInfo.FindAll(a=>a.RemainTime<=0).Count<=0)//排除被动buff
                        {
                            hasDebuff = true;
                            list.Add(buff);
                        }

                    }
                }
                if (hasDebuff)
                {
                    Buff tarBuff = list.Random(Target.GetRandomClass().Main);
                    Target.BuffReturn(tarBuff.BuffData.Key, false).SelfStackDestroy();
                }
                else
                {
                    CriPer += 100;
                }

            }
        }
    }
    public class Extended_scr_5_1 : Sex_ImproveClone,IP_SkillUse_Target
    {

        //public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        public void AttackEffect(BattleChar hit, SkillParticle SP, int DMG, bool Cri)
        {
            //if (SkillD.MySkill.KeyID == "S_SF_Scarecrow_0_1" && SkillD.ExtendedFind_DataName("EX_SF_Scarecrow_5") == null)
            //{

            List<Buff> list = this.listcan;
            //foreach (Buff buff in hit.Buffs)
            //{
                //if (!buff.BuffData.Debuff && !buff.BuffData.Cantdisable && !buff.BuffData.Hide && !buff.DestroyBuff && buff.LifeTime > 0)
                //{
                    //list.Add(buff);
                //}
            //}
            if (list.Count != 0)
            {
                Buff tarBuff = list.Random(hit.GetRandomClass().Main);
                hit.BuffReturn(tarBuff.BuffData.Key, false).SelfStackDestroy();
                //Debug.Log("浮游炮触发净化:"+tarBuff.BuffData.Name);
            }
            //}
        }
        public List<Buff> listcan = new List<Buff>();
    }


    public class Extended_scr_5_2 : Sex_ImproveClone
    {
        // Token: 0x06000CE4 RID: 3300 RVA: 0x0007C46F File Offset: 0x0007A66F
        public override void Init()
        {
            base.Init();
            this.PlusSkillStat.cri = 100f;
        }
    }


    public class Sex_scr_6 : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {

            /*
            if (SkillD.MySkill.Effect_Self.Buffs.FindIndex(a => a.Key == "B_SF_Scarecrow_6") != -1)
            {

                int ind = SkillD.MySkill.Effect_Self.Buffs.FindIndex(a => a.Key == "B_SF_Scarecrow_6");//取消技能自带的buff，然后等待重新释放，避免bug

                SkillD.MySkill.Effect_Self.Buffs.RemoveAt(ind);

            }
            */
            base.SkillUseSingle(SkillD, Targets);
            //int num = 0;
            //if(!this.BChar.BuffFind("B_SF_Scarecrow_p",false))
            //{
            //this.BChar.BuffAdd("B_SF_Scarecrow_p", this.BChar, false, 0, false, -1, false);
            //num++;
            //}

            BattleSystem.DelayInput(this.Delay());
            /*
                this.BChar.BuffAdd("B_SF_Scarecrow_6", this.BChar, false, 0, false, -1, false);


            Bcl_scr_6 b6 = this.BChar.BuffReturn("B_SF_Scarecrow_6", false) as Bcl_scr_6;

            b6.needTypeChange = true;
            */

            //Skill skill = Skill.TempSkill("S_SF_Scarecrow_0", this.BChar, this.BChar.MyTeam);//置入浮游炮射击
            //skill.Counting = 20;
            //BattleTeam.SkillRandomUse(this.BChar, skill, false, true, false);




            //while (co.StackNum < co.BuffData.MaxStack)
            //{
            //this.BChar.BuffAdd("B_SF_Scarecrow_p", this.BChar, false, 0, false, -1, false);
            //num++;
            //}


            //Bcl_scr_6 b6 = this.BChar.BuffReturn("B_SF_Scarecrow_6", false) as Bcl_scr_6;
            //b6.AddBackNum(num);
        }
        public IEnumerator Delay()
        {
            yield return new WaitForFixedUpdate();
            Bcl_scr_6 b6 = this.BChar.BuffReturn("B_SF_Scarecrow_6", false) as Bcl_scr_6;
            b6.needTypeChange = true;
            yield break;
        }
    }

    public class Bcl_scr_6 : Buff, IP_PlayerTurn,IP_FloatGunReset//,IP_BuffAdd,IP_BuffAddAfter
    {
        /*
        public override string DescExtended()
        {

            return base.DescExtended().Replace("&a", (this.backNum).ToString());
        }
        */
        public void FloatGunReset(BattleChar bc,ref bool prevent,ref bool preventHalf)
        {
            if(bc==this.BChar && this.prevent)
            {
                this.preventing = true;
                prevent = true;
            }
        }
        public override void DestroyByTurn()
        {
            this.prevent = false;
            if(this.BChar.BuffFind("B_SF_Scarecrow_p", false) && this.preventing)
            {
                Bcl_scr_p co = this.BChar.BuffReturn("B_SF_Scarecrow_p") as Bcl_scr_p;
                co.ClearFloatingGun();
            }
        }
        public override void FixedUpdate()
        {
            /*
            if (!this.BChar.BuffFind("B_SF_Scarecrow_p", false)||this.BChar.BuffReturn("B_SF_Scarecrow_p",false).StackNum< (this.BChar.BuffReturn("B_SF_Scarecrow_p", false).BuffData.MaxStack*2))
            {
                if(!BattleSystem.instance.DelayWait)
                {
                    while(!this.BChar.BuffFind("B_SF_Scarecrow_p", false) || this.BChar.BuffReturn("B_SF_Scarecrow_p", false).StackNum < (this.BChar.BuffReturn("B_SF_Scarecrow_p", false).BuffData.MaxStack * 2))
                    {
                        this.BChar.BuffAdd("B_SF_Scarecrow_p", this.BChar, false, 0, false, -1, false);
                        this.backNum++;
                    }
                }

            }
            */
            if ( !shooted && BattleSystem.instance.EnemyTeam.AliveChars.Count >= 1 && this.BChar.BuffFind("B_SF_Scarecrow_p", false))//启用
            {

                Skill skill = Skill.TempSkill("S_SF_Scarecrow_0", this.BChar, this.BChar.MyTeam);
                skill.Counting = 20;
                BattleTeam.SkillRandomUse(this.BChar, skill, false, true, false);
                this.shooted = true;
            }
            if(this.needTypeChange&& this.BChar.BuffFind("B_SF_Scarecrow_p", false))
            {
                Bcl_scr_p co = this.BChar.BuffReturn("B_SF_Scarecrow_p") as Bcl_scr_p;
                co.SetType(2, 2);
                this.needTypeChange = false;
            }
        }
        public void Turn()
        {
            this.shooted = false;

            this.preventing = false;
        }
        /*
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)//满浮游炮时置入射击
        {


            if (!oldbuff && BuffTaker == this.BChar && addedbuff.BuffData.Key == "B_SF_Scarecrow_p")
            {
                bool flag1 = this.BChar.BuffFind("B_SF_Scarecrow_p", false);
                int stk = 0;
                if (flag1)
                {
                    stk = this.BChar.BuffReturn("B_SF_Scarecrow_p", false).StackNum;
                    if (stk >= this.BChar.BuffReturn("B_SF_Scarecrow_p", false).BuffData.MaxStack)
                    {
                        this.backNum--;
                    }
                }

            }
            this.oldbuff = true;
        }
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            this.oldbuff = false;
        }
        */

        public void AddBackNum(int num)
        {
            this.backNum += num;
        }
        private bool prevent=true;
        private bool preventing = false;
        private bool shooted;
        private int backNum;
        public bool needTypeChange = false;
        public bool oldbuff;
    }


    public class Sex_scr_7 : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BattleSystem.DelayInput(Bcl_scr_p.SetTypeDelay(this.BChar, 0, 0));
            //Bcl_scr_p co = this.BChar.BuffReturn("B_SF_Scarecrow_p") as Bcl_scr_p;
            //co.SetType(0, 0);
        }
    }
    public class Bcl_scr_7_S : Buff, IP_ParticleOutEnd_Before
    {
        public void ParticleOutEnd_Before(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.MySkill.KeyID == "S_SF_Scarecrow_0_1")
            {

                Skill_Extended skill_Extended = new Skill_Extended();
                skill_Extended.PlusSkillPerStat.Damage = (int)(SkillD.MySkill.Effect_Target.DMG_Per*0.5);
                skill_Extended.PlusSkillStat.hit = -20;
                SkillD.ExtendedAdd(skill_Extended);

            }
        }

        //public override void FixedUpdate()
        //{
            //base.FixedUpdate();
            //if(this.BChar.BuffFind("B_SF_Scarecrow_p", false))
            //{
                //Bcl_scr_p Buff_p = this.BChar.BuffReturn("B_SF_Scarecrow_p") as Bcl_scr_p;
                //if(Buff_p.Tartype!=0)
                //{
                    //base.SelfDestroy();
                //}
            //}
        //}
    }


    public class Sex_scr_8 : Sex_ImproveClone
    {
        public override bool Terms()
        {
            bool flag = this.BChar.BuffFind("B_SF_Scarecrow_p", false) && this.BChar.BuffReturn("B_SF_Scarecrow_p", false).StackNum >= 3;
            return flag;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            if (!Targets[0].BuffFind("B_SF_Scarecrow_8", false))
            {
                Targets[0].BuffAdd("B_SF_Scarecrow_8", this.BChar, false, 0, false, -1, false);
                //Bcl_drm_5 buff = Buff.DataToBuff(gdebuffData, this.BChar, this.BChar, -1, false, 0)as Bcl_drm_5;
                //buff.Coskill(this.skill0,3);
                //this.BChar.BuffAddDelay(buff,false);
                //this.BChar.BuffAdd("B_drm_5_1", this.BChar, false, 0, false, -1, false);
            }
            Bcl_scr_8 buff8 = Targets[0].BuffReturn("B_SF_Scarecrow_8", false) as Bcl_scr_8;

            Bcl_scr_p buffps = this.BChar.BuffReturn("B_SF_Scarecrow_p", false) as Bcl_scr_p;
            int type= buffps.Tartype;

            try
            {
                for (int i = 1; i <= 3; i++)
                {
                    this.BChar.BuffReturn("B_SF_Scarecrow_p", false).SelfStackDestroy();
                    Targets[0].BuffAdd("B_SF_Scarecrow_p", this.BChar, false, 0, false, -1, false);
                    buff8.RemUser(this.BChar, 1);//记录出借者与归还回合
                }
            }
            catch
            {

            }

            Bcl_scr_p buffpt = Targets[0].BuffReturn("B_SF_Scarecrow_p", false) as Bcl_scr_p;
            buffpt.Tartype = type;
        }
    }

    public class Bcl_scr_8 : Buff, IP_TurnEnd//, IP_PlayerTurn//,IP_TurnEnd//
    {
        public override string DescExtended()
        {
            //int cdMin = this.cdList.Min();
            List <int>cdNext=this.cdList.FindAll(a => a == this.cdList.Min());

            return base.DescExtended().Replace("&a", (this.cdList.Min()).ToString()).Replace("&b", (cdNext.Count).ToString());
        }
        public void RemUser(BattleChar user, int cd)
        {
            this.userList.Add(user);
            this.cdList.Add(cd);
        }
        public  void TurnEnd()//TurnUpdate()

        {
            for (int i = 0; i < this.cdList.Count; i++)
            {
                if(this.cdList[i]>0)
                {
                    this.cdList[i]--;
                }
                if (this.cdList[i] == 0 && this.BChar.BuffFind("B_SF_Scarecrow_p", false))
                {
                    this.BChar.BuffReturn("B_SF_Scarecrow_p", false).SelfStackDestroy();
                    //this.userList[i].BuffAdd("B_SF_Scarecrow_p", this.userList[i], false, 0, false, -1, false);
                    this.returningList.Add(this.userList[i]);

                    this.cdList.RemoveAt(i);
                    this.userList.RemoveAt(i);
                    i--;
                }

            }

        }
        public override void FixedUpdate()
        {
            if(this.returningList.Count>0 && !BattleSystem.instance.DelayWait && BattleSystem.instance.Particles.Count == 0 && BattleSystem.instance.ActWindow.On)
            {
                while(this.returningList.Count > 0)
                {
                    this.returningList[0].BuffAdd("B_SF_Scarecrow_p", this.returningList[0], false, 0, false, -1, false);
                    this.returningList.RemoveAt(0);
                }
            }

            if (this.cdList.Count <= 0 && this.returningList.Count<=0)
            {
                base.SelfDestroy();
            }
        }
        private List<BattleChar> userList = new List<BattleChar>();
        private List<int> cdList = new List<int>();

        private List<BattleChar> returningList = new List<BattleChar>();
    }

    public class Bcl_scr_8_1 : Buff,IP_SkillUse_User
    {
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if(this.BChar.BuffFind("B_SF_Scarecrow_p",false) && SkillD.IsDamage)
            {
                Bcl_scr_p co = this.BChar.BuffReturn("B_SF_Scarecrow_p", false) as Bcl_scr_p;
                //co.SetType(1, 0);
                Skill skill = Skill.TempSkill("S_SF_Scarecrow_0", this.BChar, this.BChar.MyTeam);
                skill.Counting = 0;
                BattleTeam.SkillRandomUse(this.BChar, skill, false, true, false);
            }

        }
    }

    public class Sex_scr_9 : Sex_ImproveClone
    {
        /*
        public override bool Terms()
        {
            bool flag = this.BChar.BuffFind("B_SF_Scarecrow_p", false) && this.BChar.BuffReturn("B_SF_Scarecrow_p", false).StackNum >= 3;
            return flag;
        }
        */
        /*
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            //Debug.Log("point1");
            
            if(SkillD.MySkill.Effect_Target.Buffs.FindIndex(a => a.Key == "B_SF_Scarecrow_9")!=-1)
            {
                int ind = SkillD.MySkill.Effect_Target.Buffs.FindIndex(a => a.Key == "B_SF_Scarecrow_9");//取消技能自带的buff，然后等待重新释放，避免bug
                SkillD.MySkill.Effect_Target.Buffs.RemoveAt(ind);
                base.SkillUseSingle(SkillD, Targets);
            }

            //Debug.Log("point2");
            for (int i = 1; i <= 3 && this.BChar.BuffFind("B_SF_Scarecrow_p", false); i++)
            {
                this.BChar.BuffReturn("B_SF_Scarecrow_p", false).SelfStackDestroy();
                //Debug.Log("消耗浮游炮");
            }
            //Targets[0].BuffAdd("B_SF_Scarecrow_9", this.BChar, false, 0, false, -1, false);
            GDEBuffData gdebuffData = new GDEBuffData("B_SF_Scarecrow_9");
            Bcl_scr_9 b9= Buff.DataToBuff(gdebuffData, Targets[0], this.BChar, -1, false, 0)as Bcl_scr_9;
            b9.itself = this.MySkill;
            Targets[0].BuffAddDelay(b9, false);
            //Debug.Log("point3");
        }
        */
    }
    

    public class Bcl_scr_9 : Buff, IP_DamageTake,IP_ChangeFloatingsMax//,IP_BuffAdd
    {
        public int ChangeFloatingsMax(BattleChar BC)
        {
            if(this.StackInfo.FindAll(a=>a.UseState==BC).Count>0)
            {
                return -3*(this.StackInfo.FindAll(a => a.UseState == BC).Count);
            }
            return 0;
        }
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", this.Usestate_L.Info.Name);
        }
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();

            BattleSystem.DelayInput(this.Record());



            this.recordHP = this.BChar.HP;
            this.recordRecovery = this.BChar.Recovery;
        }
        public IEnumerator Record()
        {
            Button button = this.BuffIcon.AddComponent<Button>();
            button.onClick.AddListener(new UnityAction(this.Go));

            foreach (Skill skill in BattleSystem.instance.AllyTeam.Skills)
            {
                if (skill.Master == this.BChar /*&& skill != this.itself*/)
                {
                    this.orgskillList.Add(skill);
                    Skill coskill = skill.CloneSkill(true, null, null, false);
                    this.skillList.Add(coskill);
                }
            }

            foreach (Buff buff in this.BChar.Buffs)
            {
                foreach (StackBuff sta in buff.StackInfo)
                {
                    this.buffList.Add(buff);
                    this.staList.Add(sta);
                }
            }
            yield break;
        }

        //public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        //{
            //if(BuffTaker==this.BChar)
            //{
                //if (!addedbuff.IsHide && (addedbuff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT || addedbuff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_Debuff || addedbuff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_CrowdControl))
                //{
                    //this.buffList.Add(addedbuff.BuffData.Key);
                //}
            //}
        //}


        /*
        public override void Init()
        {
            base.Init();
            if(this.BChar!=this.Usestate_L)
            {
                this.PlusPerStat.Damage = -30;
            }
        }
        */
        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if (!resist && this.BChar.HP <= 0 && Dmg >= 1)
            {
                resist = true;
                this.Go();
            }
        }
        public void Go()
        {
            if(!BattleSystem.instance.DelayWait)
            {
                BattleSystem.DelayInput(this.Rebuild());
                this.SelfDestroy();

            }
        }
        public IEnumerator Rebuild()
        {
            Skill skillpar = Skill.TempSkill("S_SF_Scarecrow_9_1", this.BChar, this.BChar.MyTeam);//

            this.BChar.ParticleOut(skillpar, this.BChar);



            this.BChar.Recovery = this.recordRecovery;
            this.BChar.HP = this.recordHP;
            this.BChar.Recovery = this.recordRecovery;

            //foreach (Buff buff in this.BChar.Buffs)
            for(int j=0;j< this.BChar.Buffs.Count;j++)
            {
                Buff buff = this.BChar.Buffs[j];
                if (!buff.IsHide)
                //if (!buff.IsHide && (buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT || buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_Debuff || buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_CrowdControl||(!buff.BuffData.Debuff )))//mark
                //if (!buff.IsHide && (buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT || buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_Debuff || buff.BuffData.BuffTag.Key == GDEItemKeys.BuffTag_CrowdControl))
                {
                    for (int i=0;i<buff.StackInfo.Count;i++)
                    {
                        if (this.staList.Find(a => a == buff.StackInfo[i])==null && buff.StackInfo[i].UseState!=this.BChar)//mark
                        {
                            buff.StackInfo.RemoveAt(i);
                            i--;
                            this.BuffStatUpdate();
                        }
                    }
                }
                if (buff.StackNum <= 0)
                {
                    //buff.SelfDestroy(false);
                    this.BChar.Buffs.RemoveAt(j);
                    if (buff.BuffIcon != null)
                    {
                        UnityEngine.Object.Destroy(buff.BuffIcon);
                    }
                    GamepadManager.RefreshTarget();
                    j--;
                }
            }
            //foreach (string key in this.buffList)//根据buff的key按层删去版
            //{
            //if(this.BChar.BuffFind(key,false))
            //{
            //Buff buff = this.BChar.BuffReturn(key, false);
            //buff.StackInfo.RemoveAt(buff.StackInfo.Count - 1);
            //buff.BuffStatUpdate();
            //if (buff.StackNum <= 0)
            //{
            //buff.SelfDestroy(false);
            //}
            //}

            //}
            List<Skill> list = new List<Skill>();
            foreach (Skill skill in BattleSystem.instance.AllyTeam.Skills)
            {
                if (skill.Master==this.BChar && this.orgskillList.Find(a=>a.CharinfoSkilldata==skill.CharinfoSkilldata) ==null)
                {
                    list.Add(skill);
                }
            }
            foreach (Skill skill2 in list)
            {
                skill2.Delete(false);
            }

            /*
            for (int i = 0; i < this.orgskillList.Count; i++)
            {
                if (BattleSystem.instance.AllyTeam.Skills.Find(a => a.CharinfoSkilldata == this.orgskillList[i].CharinfoSkilldata) == null)
                {
                    Skill coskill = this.skillList[i];
                    coskill.isExcept = true;
                    BattleSystem.instance.EffectDelaysAfter.Enqueue(this.AddSkill(coskill));
                }
            }
            */
            for (int i = 0; i < this.orgskillList.Count; i++)
            {
                if (BattleSystem.instance.AllyTeam.Skills.Find(a => a.CharinfoSkilldata == this.orgskillList[i].CharinfoSkilldata) == null)
                {
                    if(BattleSystem.instance.AllyTeam.Skills_UsedDeck.Find(a => a.CharinfoSkilldata == this.orgskillList[i].CharinfoSkilldata)!=null)
                    {
                        this.BChar.MyTeam.ForceDraw(this.orgskillList[i].CharinfoSkilldata, null);
                    }
                    else if (BattleSystem.instance.AllyTeam.Skills_Deck.Find(a => a.CharinfoSkilldata == this.orgskillList[i].CharinfoSkilldata) != null)
                    {
                        this.BChar.MyTeam.ForceDraw(this.orgskillList[i].CharinfoSkilldata, null);
                    }
                    else
                    {
                        Skill coskill = this.skillList[i];
                        BattleSystem.instance.AllyTeam.Add(coskill, true);
                        //coskill.isExcept = true;
                        //BattleSystem.instance.EffectDelaysAfter.Enqueue(this.AddSkill(coskill));
                    }
                }
            }

            this.orgskillList.Clear();
            this.skillList.Clear();

                //foreach (Skill skill in this.skillList)
                //{
                //Skill coskill = skill;
                //coskill.isExcept = true;

                //BattleSystem.instance.EffectDelaysAfter.Enqueue(this.AddSkill(coskill));
                //}
                yield return null;
            yield break;
        }
        public IEnumerator AddSkill(Skill skill)
        {
            if ( BattleSystem.instance.AllyTeam.Skills.Count < 10)
            {
                BattleSystem.instance.AllyTeam.Add(skill, true);
            }
            yield break;
        }
        public Skill itself;
        private List<Skill> skillList = new List<Skill>();
        private List<Skill> orgskillList = new List<Skill>();
        //private List<string> buffList = new List<string>();
        private List<Buff> buffList = new List<Buff>();
        private List<StackBuff> staList = new List<StackBuff>();
        //private List<int> remainList = new List<int>();
        //private List<BattleChar> charList = new List<BattleChar>();
        private int recordHP;
        private int recordRecovery;

    }



    public class Bcl_scr_10 : Buff, IP_Dodge
    {
        //public override string DescExtended()
        //{

            //return base.DescExtended().Replace("&a", (this.dodnum).ToString());
        //}
        public void Dodge(BattleChar Char, SkillParticle SP)
        {
            if (Char == this.BChar)
            { 
            this.dodnum++;
            //Debug.Log("刀尖起舞触发" + this.BChar.name);
                this.BChar.BuffAdd("B_SF_Scarecrow_10_1", this.Usestate_L, false, 0, false, -1, false);
                this.Init();
            }
        }

        public override void Init()
        {
            base.Init();

            this.PlusStat.dod = 20f;
            //this.PlusPerStat.Damage = 20 * this.dodnum;
            
        }
        private int dodnum = 0;
    }

    public class Bcl_scr_10_1 : Buff, IP_BuffAdd, IP_BuffUpdate, IP_BuffAddAfter
    {
        public override void BuffStat()
        {
            base.BuffStat();

            this.PlusPerStat.Damage = 10 * base.StackNum;

        }
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)//mark
        {

            if (BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key && this.StackNum >= this.BuffData.MaxStack)
            {

                //this.overbuff.StackInfo.Insert(0, this.overbuff.StackInfo[0]);
                //this.overbuff.BuffData.MaxStack++;
                this.isob = true;
                this.oldsta = this.StackInfo[0];
            }

        }

        
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            if (this.isob)
            {
                this.BuffData.MaxStack++;
                this.StackInfo.Insert(0, this.oldsta);
                this.BuffStatUpdate();
            }
            this.isob = false;


        }
        public void BuffUpdate(Buff MyBuff)
        {
            if (this.StackNum != this.BuffData.MaxStack)
            {
                this.BuffData.MaxStack = Math.Max(1, this.StackNum);
            }
            if(this.PlusPerStat.Damage != 10 * base.StackNum)
            {
                this.PlusPerStat.Damage = 10 * base.StackNum;
            }
        }
        private StackBuff oldsta = null;
        private bool isob;
    }

    public class Sex_scr_11 : Sex_ImproveClone
    {
        /*
        public override bool Terms()
        {
            bool flag = this.BChar.BuffFind("B_SF_Scarecrow_p", false) && this.BChar.BuffReturn("B_SF_Scarecrow_p", false).StackNum >= 1;
            return flag;
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            for (int i = 1; i <= 1 && this.BChar.BuffFind("B_SF_Scarecrow_p", false); i++)
            {
                this.BChar.BuffReturn("B_SF_Scarecrow_p", false).SelfStackDestroy();
                //Debug.Log("消耗浮游炮");
            }

        }
        */
    }
    public class Bcl_scr_11 : Buff, IP_BeforeHit, IP_DamageTakeChange,IP_ChangeFloatingsMax//, IP_BuffAdd
    {
        public int ChangeFloatingsMax(BattleChar BC)
        {
            if (this.StackInfo.FindAll(a => a.UseState == BC).Count > 0)
            {
                return -1 * (this.StackInfo.FindAll(a => a.UseState == BC).Count);
            }
            return 0;
        }
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a", this.Usestate_L.Info.Name);
        }
        public void BeforeHit(SkillParticle SP, int Dmg, bool Cri)
        {
            //Debug.Log("BeforeHit");
            if (Dmg >= 1)
            {
                this.activateDef = true;
                this.Init();
            }
        }
        
        public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {
            //Debug.Log("DamageTakeChange" + Hit.Info.Name);
            if (this.activateDef)
            {
                this.activateDef = false;
                this.SelfDestroy();
                return 0;
            }
            else
            {
                return Dmg;
            }

        }
        /*
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)//mark
        {
            if(addedbuff.BuffData.Key ==this.BuffData.Key && BuffTaker == this.BChar &&this.StackNum>=this.BuffData.MaxStack)
            {
                this.StackInfo[0].UseState.BuffAdd("B_SF_Scarecrow_p", this.StackInfo[0].UseState, false, 0, false, -1, false);

            }
        }


        public override void DestroyByTurn()
        {
            this.Usestate_L.BuffAdd("B_SF_Scarecrow_p", this.Usestate_L, false, 0, false, -1, false);
        }
        */
        private bool activateDef = false;
        private bool pflag1;
    }

    public class Sex_scr_12 : Sex_ImproveClone//, IP_SkillCastingStart
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            //BattleSystem.DelayInput(Bcl_scr_p.SetTypeDelay(this.BChar, 1, 1));
            //Bcl_scr_p co = this.BChar.BuffReturn("B_SF_Scarecrow_p", false) as Bcl_scr_p;
            //co.SetType(1, 1);
        }
        /*
        public void SkillCasting(CastingSkill ThisSkill)
        {
            GDEBuffData gdebuffData = new GDEBuffData("B_SF_Scarecrow_12");
            Bcl_scr_12 b12 = Buff.DataToBuff(gdebuffData, this.BChar, this.BChar, -1, false, 0) as Bcl_scr_12;
            b12.useskill = this.MySkill;
            b12.IsHide = true;
            this.BChar.BuffAddDelay(b12, false);
        }
        
        public override void Init()
        {
            base.Init();
            this.CountingExtedned = true;
        }
        */
        /*
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            bool flag1 = this.BChar.BuffFind("B_SF_Scarecrow_p", false);
            int stk = 0;
            if (flag1)
            {
                Bcl_scr_p buff= this.BChar.BuffReturn("B_SF_Scarecrow_p", false) as Bcl_scr_p;
                stk = buff.StackNum;
                if (stk >= buff.BuffData.MaxStack)
                {
                    //buff.overStack++;
                    //buff.BuffData.MaxStack++;
                    StackBuff s1 = buff.StackInfo[0];
                    buff.StackInfo.Insert(0,s1);
                }
            }
        }
        */
    }

    public class Bcl_scr_12 : Buff,IP_ParticleOutEnd_Before
    {

        public override void Init()
        {
            this.PlusStat.hit = 20;
        }
        public void ParticleOutEnd_Before(Skill SkillD, List<BattleChar> Targets)
        {

            if (SkillD.MySkill.KeyID == "S_SF_Scarecrow_0_1" && SkillD.AllExtendeds.Find(a=>a is Extended_scr_12)==null)
            {
                //this.BChar.BuffAdd("B_SF_Scarecrow_check_1", this.BChar, false, 0, false, -1, false);//check1

                Extended_scr_12 ex1 = new Extended_scr_12();
                ex1.Traking = true;
                SkillD.ExtendedAdd(ex1);

                //GDEBuffData gdebuffData = new GDEBuffData("B_SF_Scarecrow_2_T");//给技能添加额外的buff，
                //Buff buff = Buff.DataToBuff(gdebuffData, this.BChar, this.BChar, -1, false, 0);
                //SkillD.MySkill.Effect_Target.Buffs.Add(gdebuffData);

            }

        }
    }
    public class Extended_scr_12:Sex_ImproveClone
    {

    }


    public class Sex_scr_13_LucyDraw : Sex_ImproveClone
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BattleChar BC = Targets[0];//BattleSystem.instance.AllyList.Find((BattleAlly a) => a.Info.KeyData == "SF_Scarecrow");
            /*
            for (int i = 0; i < BattleSystem.instance.AllyTeam.Skills_UsedDeck.Count; i++)//将弃牌库中自己的技能洗入牌库
            {
                if (BattleSystem.instance.AllyTeam.Skills_UsedDeck[i].Master == BC)
                {
                    int index = UnityEngine.Random.Range(0, BattleSystem.instance.AllyTeam.Skills_Deck.Count + 1);
                    BattleSystem.instance.AllyTeam.Skills_Deck.Insert(index, BattleSystem.instance.AllyTeam.Skills_UsedDeck[i]);
                    BattleSystem.instance.AllyTeam.Skills_UsedDeck.RemoveAt(i);
                    i--;
                }
            }
            */
            //this.sumAP = 0;

                BattleSystem.DelayInputAfter(this.Draw(BC));
            



            //P_Scarecrow pas=BC.Info.Passive as P_Scarecrow;
            //pas.ReCharge(sumAP);
        }

        public IEnumerator Draw(BattleChar BC)
        {
            BC.Overload++;
            for (int i = 0; i < 2; i++)
            {
                yield return BattleSystem.instance.AllyTeam._CharacterDraw(BC, null);
            }
            foreach(Skill skill in BattleSystem.instance.AllyTeam.Skills)
            {
                if(skill.Master==BC)
                {
                    Skill_Extended sex=new Skill_Extended();
                    sex.APChange = -1;
                    skill.ExtendedAdd(sex);
                }
            }
                /*
                Skill skill2 = BattleSystem.instance.AllyTeam.Skills_Deck.Find((Skill skill) => skill.Master == BC);
                if (skill2 == null)
                {
                    //this.sumAP += 2*BattleSystem.instance.AllyTeam.Skills_Deck[0].AP;
                    //Debug.Log("sumAP="+this.sumAP);
                    foreach (IP_scr_RechargeInput ip_this in BC.IReturn<IP_scr_RechargeInput>())
                    {
                        bool flag = ip_this != null;
                        if (flag)
                        {
                            ip_this.RechargeInput(BattleSystem.instance.AllyTeam.Skills_Deck[0], 3 * BattleSystem.instance.AllyTeam.Skills_Deck[0].AP);
                        }
                    }
                    BattleSystem.instance.AllyTeam.Draw();
                }
                else
                {
                    //this.sumAP += 2*skill2.AP;
                    //Debug.Log("sumAP=" + this.sumAP);
                    foreach (IP_scr_RechargeInput ip_this in BC.IReturn<IP_scr_RechargeInput>())
                    {
                        bool flag = ip_this != null;
                        if (flag)
                        {
                            ip_this.RechargeInput(skill2, 3 * skill2.AP);
                        }
                    }
                    yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyTeam._ForceDraw(skill2, null));
                }
                */
                yield return null;
            yield break;
        }

        //private int sumAP;
    }
    public interface IP_scr_RechargeInput
    {
        // Token: 0x06000001 RID: 1
        void RechargeInput(Skill skill,int recnum);
    }


    public class SkillEn_SF_Scarecrow_0 : Sex_ImproveClone
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return (MainSkill.TargetTypeKey == "ally" || MainSkill.TargetTypeKey == "self" || MainSkill.TargetTypeKey == "otherally" || MainSkill.TargetTypeKey == "all_onetarget" );

        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            foreach (BattleChar tar in Targets)
            {
                if (Targets[0].Info.Ally)
                {
                    tar.BuffAdd("B_SF_Scarecrow_11", BattleSystem.instance.AllyList.Find((BattleAlly a) => a.Info.KeyData == "SF_Scarecrow"), false, 0, false, -1, false);

                }
            }

        }
    }

    public class SkillEn_SF_Scarecrow_1 : Sex_ImproveClone//释放时稻草人额外获得3倍于费用的装填计数
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return (MainSkill.TargetTypeKey == "enemy" || MainSkill.TargetTypeKey == "random_enemy" || MainSkill.TargetTypeKey == "all_onetarget" || MainSkill.TargetTypeKey == "all_enemy" );

        }
        public override void Init()
        {
            base.Init();
            this.APChange = 1;
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BattleChar bc = BattleSystem.instance.AllyList.Find((BattleAlly a) => a.Info.KeyData == "SF_Scarecrow");
            //bc.BuffAdd("B_SF_Scarecrow_p", bc, false, 0, false, -1, false);


            bool flag1 = !Targets[0].Info.Ally && Targets.Count==1;
            
            Skill skill = Skill.TempSkill("S_SF_Scarecrow_0", bc, bc.MyTeam);
            skill.Counting = 0;
            foreach (Skill_Extended sex in skill.AllExtendeds)
            {
                if (flag1 && sex is Sex_scr_0)
                {
                    (sex as Sex_scr_0).unicTarget = Targets[0];
                    //(sex as Sex_scr_0).unicType = 1;

                }
            }
            BattleTeam.SkillRandomUse(bc, skill, false, true, false);

            //foreach (CastingSkill castingSkill in BattleSystem.instance.CastSkills)
            //{
                //if (castingSkill.Usestate.Info.KeyData == "SF_Scarecrow" &&  castingSkill.skill.MySkill.KeyID=="S_SF_Scarecrow_0")
                //{
                    //Skill skill = castingSkill.skill;
                    //foreach (Skill_Extended sex in skill.AllExtendeds)
                    //{
                        //if (flag1 && sex.GetType() == typeof(Sex_scr_0))
                        //{
                            //(sex as Sex_scr_0).unicType = 1;
                        //}
                    //}
                    //BattleSystem.DelayInput(this.Counter(castingSkill));

                    //goto IL_scr001;
                //}
            //}
            //foreach (CastingSkill castingSkill2 in BattleSystem.instance.SaveSkill)
            //{
                //if (castingSkill2.Usestate.Info.KeyData == "SF_Scarecrow" && castingSkill2.skill.MySkill.KeyID == "S_SF_Scarecrow_0")
                //{
                    //Skill skill2 = castingSkill2.skill;
                    //foreach (Skill_Extended sex2 in skill2.AllExtendeds)
                    //{
                        //if (flag1 && sex2.GetType() == typeof(Sex_scr_0))
                        //{
                            //(sex2 as Sex_scr_0).unicType = 1;
                        //}
                    //}
                    //BattleSystem.DelayInput(this.Counter(castingSkill2));
                    //goto IL_scr001;

                //}
            //}
        //IL_scr001:;
        }


        //public IEnumerator Counter(CastingSkill CastingSkill)
        //{
            //CastingSkill.Use();
            //BattleSystem.instance.ActWindow.CastingWaste(CastingSkill.skill);

                //BattleSystem.instance.CastSkills.Remove(CastingSkill);
                //BattleSystem.instance.SaveSkill.Remove(CastingSkill);

            //yield return null;
            //yield break;
        //}
    }

    public class SkillEn_SF_Scarecrow_2 : Sex_ImproveClone//释放时稻草人额外获得3倍于费用的装填计数
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return true;

        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            BattleChar BC = BattleSystem.instance.AllyList.Find((BattleAlly a) => a.Info.KeyData == "SF_Scarecrow");
            int recnum = 0;
            if (SkillD.UsedApNum >= 0)
            {
                recnum = SkillD.UsedApNum;
            }
            else
            {
                recnum = SkillD.AP;
            }
            foreach (IP_scr_RechargeInput ip_this in BC.IReturn<IP_scr_RechargeInput>())
            {
                bool flag = ip_this != null;
                if (flag)
                {
                    ip_this.RechargeInput(SkillD, Math.Max(0, 3 * this.MySkill.UsedApNum));
                }
            }
        }
    }

    public interface IP_CheckEquip
    {
        // Token: 0x060025CE RID: 9678
        bool CheckEquip(BattleChar bc);
    }
    public class E_scr_0 : EquipBase, IP_CheckEquip//,IP_BuffUpdate, IP_BattleEndOutBattle
    {
        public override void Init()
        {
            base.Init();
            //this.PlusPerStat.Damage = 0;
            this.PlusStat.dod = 20;
            this.PlusStat.hit = 10;
            this.PlusStat.atk = 2;

            base.Init();
        }
        public bool CheckEquip(BattleChar bc)
        {
            if (bc == this.BChar)
            {
                return true;
            }
            return false;
        }

        /*
        public void BuffUpdate(Buff MyBuff)
        {
            //Debug.Log("buff更新0");
            if (this.BChar.BuffFind("B_SF_Scarecrow_p", false))
            {
                Buff buff = this.BChar.BuffReturn("B_SF_Scarecrow_p", false);
                //Debug.Log("buff更新1");
                this.PlusPerStat.Damage = 2 * buff.StackNum;
                //Debug.Log("buff更新2");
            }
            else
            {
                //Debug.Log("buff更新1");
                this.PlusPerStat.Damage = 0;
                //Debug.Log("buff更新2");
            }
        }

        public void BattleEndOutBattle()
        {
            this.PlusPerStat.Damage = 0;
        }
        */
    }
    /*
    public class Item_scr_1 : UseitemBase
    {
        public override bool CantTarget(Character CharInfo)
        {
            return !(CharInfo.KeyData == "SF_Scarecrow");
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






