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
using System.Reflection.Emit;
using static UnityEngine.UI.Image;



namespace FGO_Altria_Alter_B
{
    [PluginConfig("M200_FGO_Altria_Alter_B_CharMod", "FGO_Altria_Alter_B_CharMod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class FGO_Altria_Alter_B_CharacterModPlugin : ChronoArkPlugin
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
        public class Altria_Alter_BDef : ModDefinition
        {
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
                    f1 = f1 && codes[i - 5].opcode == OpCodes.Ldloc_1;
                    f1 = f1 && codes[i - 4].opcode == OpCodes.Ldarg_0;
                    f1 = f1 && codes[i - 3].opcode == OpCodes.Ldfld;
                    f1 = f1 && codes[i - 2].opcode == OpCodes.Callvirt;
                    try
                    {
                        f1 = f1 && (codes[i - 2].operand is MethodInfo && ((MethodInfo)(codes[i - 2].operand)).Name.Contains("get_Name"));
                    }
                    catch { f1 = false; }
                    f1 = f1 && codes[i - 1].opcode == OpCodes.Call;
                    f1 = f1 && codes[i - 0].opcode == OpCodes.Stloc_1;
                    if (f1)
                    {
                        yield return codes[i];
                        yield return new CodeInstruction(OpCodes.Ldarg_0);
                        yield return new CodeInstruction(OpCodes.Ldloc_1);
                        yield return new CodeInstruction(OpCodes.Call, methodInfo);
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
        public static string NameExtended(Buff buff, string str)
        {
            string str1 = str;
            IP_BuffNameEx ip = buff as IP_BuffNameEx;
            if (ip != null)
            {
                str1 = ip.BuffNameEx(str1);
            }
            return str1;

        }

    }
    public interface IP_BuffNameEx
    {
        // Token: 0x06000001 RID: 1
        string BuffNameEx(string str);
    }

    [HarmonyPatch(typeof(BuffObject))]
    public class BuffObjectUpdatePlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        public static void Update_Patch(BuffObject __instance)
        {

            if (__instance.MyBuff != null)
            {
                IP_BuffObject_Updata ip_patch = __instance.MyBuff as IP_BuffObject_Updata;
                if (ip_patch != null)
                {
                    ip_patch.BuffObject_Updata(__instance);
                }
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
    [HarmonyPatch(typeof(BattleEnemy))]
    public class BattleEnemyNewTurnPlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("NewTurn")]
        [HarmonyPostfix]
        public static void Update_Patch(BattleEnemy __instance)
        {

            if (__instance.Ai != null)
            {
                IP_EnemyNewTurnAfter ip_patch = __instance.Ai as IP_EnemyNewTurnAfter;
                if (ip_patch != null)
                {
                    ip_patch.EnemyNewTurnAfter();
                }
            }
            return;
        }
    }

    public interface IP_EnemyNewTurnAfter
    {
        // Token: 0x06000001 RID: 1
        void EnemyNewTurnAfter();
    }


    public class Skill_10_Particle : CustomSkillGDE<FGO_Altria_Alter_B_CharacterModPlugin.Altria_Alter_BDef>
    {
        // Token: 0x060000AB RID: 171 RVA: 0x000055A0 File Offset: 0x000037A0
        public override ModGDEInfo.LoadingType GetLoadingType()
        {
            return ModGDEInfo.LoadingType.Replace;
        }

        // Token: 0x060000AC RID: 172 RVA: 0x000055B4 File Offset: 0x000037B4
        public override string Key()
        {
            return "S_FGO_Altria_Alter_B_10";
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
            //skillParticle.GroundOut = true;
            return particle;

        }

    }

    public static class BasicTool
    {
        public static void ResetQueue(BattleChar enemy)
        {
            if(enemy is BattleEnemy)
            {
                BasicTool.ResetQueue(enemy as BattleEnemy);
            }
        }
        public static void ResetQueue(BattleEnemy enemy)
        {
            List<CastingSkill> list = BattleSystem.instance.EnemyCastSkills;//enemy.SkillQueue;
            bool flag = true;
            int round = 0;
            while (flag&& round<1000)
            {
                flag = false;
                round++;
                for (int i = 0; i < list.Count; i++)
                {
                    if(list[i].Usestate==enemy)
                    {
                        int num1 = list.FindIndex(a =>a.Usestate==enemy && a.CastSpeed > list[i].CastSpeed);
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
    }
    public class AI_FGO_Altria_Alter_B : AI, IP_EnemyNewTurnAfter
    {
        public override List<BattleChar> TargetSelect(Skill SelectedSkill)
        {

            return base.TargetSelect(SelectedSkill);
        }
        public override Skill SkillSelect(int ActionCount)
        {
            Skill result = base.SkillSelect(ActionCount);
            //result = this.GetSkill("S_FGO_Altria_Alter_B_2", base.SkillSelect(ActionCount));
            if (this.phase != 1 && this.phase != 2 && this.phase != 3)
            {
                if (this.BChar.HP > (int)(0.5 * this.BChar.GetStat.maxhp))
                {
                    this.phase = 1;
                }
                else if (this.BChar.HP > (int)(0.2 * this.BChar.GetStat.maxhp))
                {
                    this.phase = 2;
                }
                else
                {
                    this.phase = 3;
                }
            }

            if (this.phase==1)
            {

                if (this.lastkey== "S_FGO_Altria_Alter_B_3")
                {
                    result = this.GetSkill("S_FGO_Altria_Alter_B_2", base.SkillSelect(ActionCount));
                }
                else if (this.lastkey == "S_FGO_Altria_Alter_B_2")
                {
                    result = this.GetSkill("S_FGO_Altria_Alter_B_1", base.SkillSelect(ActionCount));
                }
                else if (this.lastkey == "S_FGO_Altria_Alter_B_1")
                {
                    result = this.GetSkill("S_FGO_Altria_Alter_B_4", base.SkillSelect(ActionCount));
                }
                else if (this.lastkey == "S_FGO_Altria_Alter_B_4")
                {
                    result = this.GetSkill("S_FGO_Altria_Alter_B_3", base.SkillSelect(ActionCount));
                }
                else
                {
                    result = this.GetSkill("S_FGO_Altria_Alter_B_3", base.SkillSelect(ActionCount));
                }
            }
            else if (this.phase == 2)
            {
                /*
                if (this.lastkey == "S_FGO_Altria_Alter_B_1")
                {
                    result = this.GetSkill("S_FGO_Altria_Alter_B_6", base.SkillSelect(ActionCount));
                }
                else if (this.lastkey == "S_FGO_Altria_Alter_B_6")
                {
                    result = this.GetSkill("S_FGO_Altria_Alter_B_7", base.SkillSelect(ActionCount));
                }
                else if (this.lastkey == "S_FGO_Altria_Alter_B_7")
                {
                    result = this.GetSkill("S_FGO_Altria_Alter_B_8", base.SkillSelect(ActionCount));
                }
                else if (this.lastkey == "S_FGO_Altria_Alter_B_8")
                {
                    result = this.GetSkill("S_FGO_Altria_Alter_B_1", base.SkillSelect(ActionCount));
                }
                else
                {
                    result = this.GetSkill("S_FGO_Altria_Alter_B_1", base.SkillSelect(ActionCount));
                }
                */
                if(ActionCount==0)
                {
                    result = this.GetSkill("S_FGO_Altria_Alter_B_1", base.SkillSelect(ActionCount));
                }
                else if(ActionCount==1)
                {
                    if (this.p3switchKey == "S_FGO_Altria_Alter_B_6")
                    {
                        this.p3switchKey = "S_FGO_Altria_Alter_B_7";
                        result = this.GetSkill("S_FGO_Altria_Alter_B_7", base.SkillSelect(ActionCount));
                    }
                    else
                    {
                        this.p3switchKey = "S_FGO_Altria_Alter_B_6";
                        result = this.GetSkill("S_FGO_Altria_Alter_B_6", base.SkillSelect(ActionCount));
                    }
                }
            }
            else if (this.phase == 3)
            {
                if ((this.lastkey == "S_FGO_Altria_Alter_B_6") || (this.lastkey == "S_FGO_Altria_Alter_B_7"))
                {
                    result = this.GetSkill("S_FGO_Altria_Alter_B_10", base.SkillSelect(ActionCount));
                }
                else if (this.lastkey == "S_FGO_Altria_Alter_B_10")
                {
                    if(this.p3switchKey== "S_FGO_Altria_Alter_B_6")
                    {
                        this.p3switchKey = "S_FGO_Altria_Alter_B_7";
                        result = this.GetSkill("S_FGO_Altria_Alter_B_7", base.SkillSelect(ActionCount));
                    }
                    else
                    {
                        this.p3switchKey = "S_FGO_Altria_Alter_B_6";
                        result = this.GetSkill("S_FGO_Altria_Alter_B_6", base.SkillSelect(ActionCount));
                    }
                }
                else
                {
                    if (this.p3switchKey == "S_FGO_Altria_Alter_B_6")
                    {
                        this.p3switchKey = "S_FGO_Altria_Alter_B_7";
                        result = this.GetSkill("S_FGO_Altria_Alter_B_7", base.SkillSelect(ActionCount));
                    }
                    else
                    {
                        this.p3switchKey = "S_FGO_Altria_Alter_B_6";
                        result = this.GetSkill("S_FGO_Altria_Alter_B_6", base.SkillSelect(ActionCount));
                    }
                }
            }



            //result = this.GetSkill("S_FGO_Altria_Alter_B_8", base.SkillSelect(ActionCount));

            this.lastkey = result.MySkill.KeyID;
            return result.CloneSkill(false, null, null, false);
        }
        public override int SpeedChange(Skill skill, int ActionCount, int OriginSpeed)
        {
            return base.SpeedChange(skill, ActionCount, OriginSpeed);
        }

        public Skill GetSkill(string key,Skill skill)
        {
            if(this.BChar.Skills.Find(a=>a.MySkill.KeyID==key)!=null)
            {

                return this.BChar.Skills.Find(a => a.MySkill.KeyID == key);
            }

            return skill;
        }

        public void EnemyNewTurnAfter()
        {
            if (this.phase == 2)
            {
                Skill skill = Skill.TempSkill("S_FGO_Altria_Alter_B_8", this.BChar);
                CastingSkill cast=BattleSystem.instance.EnemyCastEnqueue(this.BChar as BattleEnemy, skill, (this.BChar as BattleEnemy).Ai.TargetSelect(skill), 0, false);
                BasicTool.ResetQueue(this.BChar);
                //this.BChar.BattleUpdate();
            }
        }
        public int phase = 1;
        public string lastkey = string.Empty;
        public string p3switchKey= string.Empty;
    }
    public class Bcl_Altr_B_phase : Buff,IP_HPChange,IP_Dead,IP_BattleStart_UIOnBefore
    {
        public void BattleStartUIOnBefore(BattleSystem Ins)
        {
            Ins.FogTurn = 35;
        }
        public void Dead()
        {
            //BattleSystem.instance.Reward.Add(ItemBase.GetItem("Item_SF_1", 4));
            //Skill skill = Skill.TempSkill("S_PD_Reward_0", BattleSystem.instance.AllyTeam.LucyChar, BattleSystem.instance.AllyTeam);
            //BattleSystem.instance.Reward.Add(ItemBase.GetItem(new GDESkillData("S_PD_Reward_0")));

            BattleSystem.instance.Reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Consume_EquipPouch));
            BattleSystem.instance.Reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Consume_EquipPouch));
            BattleSystem.instance.Reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy_Rare));
            BattleSystem.instance.Reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Misc_Soul, 5));
        }

        public void HPChange(BattleChar Char, bool Healed)
        {
            if(Char == this.BChar)
            {
                
                if ( this.GetPhase() == 1)
                {

                        if (Char.HP <= (int)(0.8 * Char.GetStat.maxhp))
                        {
                            this.AI_Alter.phase = 2;
                        this.AI_Alter.lastkey = string.Empty;
                            Char.HP = (int)(0.8 * Char.GetStat.maxhp);

                            BattleEnemy battleEnemy = this.BChar as BattleEnemy;
                            List<CastingSkill> list1 = new List<CastingSkill>();
                            list1.AddRange(battleEnemy.SkillQueue);
                            for (int i = 0; i < list1.Count; i++)
                            {
                                BattleSystem.instance.ActWindow.CastingWaste(list1[i]);
                                BattleSystem.instance.EnemyCastSkills.Remove(list1[i]);
                                BattleSystem.instance.SaveSkill.Remove(list1[i]);
                            }
                            Skill skill = Skill.TempSkill("S_FGO_Altria_Alter_B_9", this.BChar);
                            BattleSystem.instance.EnemyCastEnqueue(this.BChar as BattleEnemy, skill, (this.BChar as BattleEnemy).Ai.TargetSelect(skill), 99, false);
                        }

            
                }
                else if(this.GetPhase() == 2)
                {
                    if (Char.HP <= (int)(0.3 * Char.GetStat.maxhp))
                    {
                        this.AI_Alter.phase = 3;
                        this.AI_Alter.lastkey = string.Empty;
                        Char.HP = (int)(0.3 * Char.GetStat.maxhp);
                        BattleEnemy battleEnemy = this.BChar as BattleEnemy;
                        List<CastingSkill> list1 = new List<CastingSkill>();
                        list1.AddRange(battleEnemy.SkillQueue);
                        for (int i = 0; i < list1.Count; i++)
                        {
                            BattleSystem.instance.ActWindow.CastingWaste(list1[i]);
                            BattleSystem.instance.EnemyCastSkills.Remove(list1[i]);
                            BattleSystem.instance.SaveSkill.Remove(list1[i]);
                        }

                        Skill skill = Skill.TempSkill("S_FGO_Altria_Alter_B_5", this.BChar);
                        BattleSystem.instance.EnemyCastEnqueue(this.BChar as BattleEnemy, skill, (this.BChar as BattleEnemy).Ai.TargetSelect(skill), 99, false);
                    }
                }
                
            }

        }
        public int GetPhase()
        {
            if(this.BChar is BattleEnemy enemy && enemy.Ai is AI_FGO_Altria_Alter_B AlterAI)
            {
                this.AI_Alter = AlterAI;
                return AlterAI.phase;
            }
            return 0;
        }
        public AI_FGO_Altria_Alter_B AI_Alter=new AI_FGO_Altria_Alter_B();
    }
    public class Bcl_Altr_B_p_0 : Buff,IP_PlayerTurn, IP_MagicUpdate
    {
        public void Turn()
        {
            Bcl_Altr_B_p_1.StackChange(this.BChar, 1);
        }
        public void MagicUpdate()
        {
            int num= Bcl_Altr_B_p_1.GetStack(this.BChar);
            if(num>20)
            {
                this.PlusStat.cri = 100;
            }
            else
            {
                this.PlusStat.cri = 0;
            }
        }
    }
    public class Bcl_Altr_B_p_1 : Buff, IP_BuffNameEx, IP_BuffObject_Updata
    {
        public string BuffNameEx(string str)
        {
            return str + Misc.InputColor(string.Concat(new object[]
            {
                " : ",
                this.CurrentStack
            }), "FFF4BF");
        }
        public void BuffObject_Updata(BuffObject obj)
        {
            obj.StackText.text = this.CurrentStack.ToString();
        }
        public static Buff StackChange(BattleChar bc, int change_num)
        {
            Bcl_Altr_B_p_1 buff;
            if (true || !bc.BuffFind("B_FGO_Altria_Alter_B_p_1", false))
            {
                buff = bc.BuffAdd("B_FGO_Altria_Alter_B_p_1", bc, false, 0, false, -1, true) as Bcl_Altr_B_p_1;
            }
            else
            {
                buff = bc.BuffReturn("B_FGO_Altria_Alter_B_p_1", false) as Bcl_Altr_B_p_1;
            }
            buff.CurrentStack += change_num;
            buff.CheckStack();
            return buff;
        }
        public static Buff StackChangeTo(BattleChar bc, int num)
        {
            Bcl_Altr_B_p_1 buff;
            if (true || !bc.BuffFind("B_FGO_Altria_Alter_B_p_1", false))
            {
                buff = bc.BuffAdd("B_FGO_Altria_Alter_B_p_1", bc, false, 0, false, -1, true) as Bcl_Altr_B_p_1;
            }
            else
            {
                buff = bc.BuffReturn("B_FGO_Altria_Alter_B_p_1", false) as Bcl_Altr_B_p_1;
            }
            buff.CurrentStack = num;
            buff.CheckStack();
            return buff;
        }
        public static int GetStack(BattleChar bc)
        {
            if (!bc.BuffFind("B_FGO_Altria_Alter_B_p_1", false))
            {
                return 0;
            }
            else
            {
                Bcl_Altr_B_p_1 buff = bc.BuffReturn("B_FGO_Altria_Alter_B_p_1", false) as Bcl_Altr_B_p_1;
                return buff.CurrentStack;
            }
        }
        public void CheckStack()
        {
            foreach (IP_MagicUpdate ip_new in this.BChar.IReturn<IP_MagicUpdate>())
            {
                try
                {
                    if (ip_new != null)
                    {
                        ip_new.MagicUpdate();
                    }
                }
                catch
                {
                }
            }

            if (this.CurrentStack <= 0)
            {
                this.SelfDestroy();
            }
        }

        public int CurrentStack
        {
            get
            {
                return this._currentStack;
            }
            set
            {
                this._currentStack = Math.Max(0, value);
            }
        }
        private int _currentStack = 0;
        //private int maxStack = 25;
    }
    public interface IP_MagicUpdate
    {
        // Token: 0x060025CE RID: 9678
        void MagicUpdate();
    }

    public class Bcl_Altr_B_p_2 : Buff,IP_Draw,IP_PlayerTurn_EnemyAfter,IP_BuffObject_Updata,IP_TurnEnd
    {
        public override string DescExtended()
        {
            if (BattleSystem.instance.TurnNum >= 2 && this.remainCount >= 0)
            {
                return base.DescExtended().Replace("&a", this.remainCount.ToString());
            }
            else
            {
                return base.DescExtended().Replace("&a", "N/A");
            }
        }
        public void BuffObject_Updata(BuffObject obj)
        {
            if (BattleSystem.instance.TurnNum >= 2&&this.remainCount>=0)
            {
                obj.StackText.text = this.remainCount.ToString();
            }
            else
            {
                obj.StackText.text = "";
            }
        }
        public void TurnEnd()
        {
            this.remainCount = -1;
        }
        public void PlayerTurn_EnemyAfter()
        {
            this.remainCount = 9;
            this.round = 0;
        }

        public IEnumerator Draw(Skill Drawskill, bool NotDraw)
        {
            if (!NotDraw && BattleSystem.instance.TurnNum >= 2 && this.remainCount >= 0) 
            {
                this.remainCount--;
                if(this.remainCount==0)
                {
                    this.remainCount += 9;
                    this.round++;
                    if(this.round<=1)
                    {
                        BattleSystem.DelayInput(this.Delay_1());
                    }
                    else
                    {
                        BattleSystem.DelayInput(this.Delay_2());
                    }
                    //BattleSystem.DelayInput(BattleSystem.instance.ForceAction(Skill.TempSkill(GDEItemKeys.Skill_S_ProgramMaster2_Main, this.BChar, null), BattleSystem.instance.AllyTeam.AliveChars[0], false, false, false, null));
                }
            }
            yield break;
        }

        public IEnumerator Delay_1()
        {

            if (BattleSystem.instance.BattleWaitList.Count >= 1)
            {
                yield return new WaitForSecondsRealtime(0.2f);
            }
            //yield return new WaitForSecondsRealtime(1f);
            if (!this.firstStun)
            {
                this.firstStun = true;
                yield return BattleText.InstBattleText_Co(this.BChar, "···", true, -1, 0f, false);
                yield return new WaitForSecondsRealtime(0.5f);
            }


            List<BattleChar> bc_list = new List<BattleChar>();
            bc_list.AddRange(BattleSystem.instance.AllyTeam.AliveChars_Vanish);
            bc_list.RemoveAll(a => a.GetStat.Stun);
            if(bc_list.Count <= 0)
            {
                bc_list.AddRange(BattleSystem.instance.AllyTeam.AliveChars_Vanish);
            }
            if(bc_list.Count>0)
            {
                List<Skill> list = new List<Skill>();
                foreach (BattleChar input in bc_list)
                {
                    list.Add(Skill.TempSkill("S_FGO_Altria_Alter_B_p", input, null));
                }
                yield return BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(this.Select), "", false, false, false, false, true);
                if (this.SelectTarget != null)
                {
                    this.SelectTarget.Damage(this.BChar,15,false,true,false,0,false,false,false);
                    this.SelectTarget.BuffAdd("B_FGO_Altria_Alter_B_5", this.BChar);
                    this.SelectTarget.BuffAdd("B_FGO_Altria_Alter_B_p_3", this.BChar);
                }
                else
                {
                    
                }
            }

            
            yield return null;
            yield break;
        }
        public IEnumerator Delay_2()
        {

            if (BattleSystem.instance.BattleWaitList.Count >= 1)
            {
                yield return new WaitForSecondsRealtime(0.2f);
            }


                foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars)
                {
                bc.Damage(this.BChar, 15, false, true, false, 0, false, false, false);
                bc.BuffAdd("B_FGO_Altria_Alter_B_5", this.BChar);
                bc.BuffAdd("B_FGO_Altria_Alter_B_p_3", this.BChar);
                }


            yield return null;
            yield break;
        }
        public bool firstStun = false;
        public void Select(SkillButton Mybutton)
        {
            this.SelectTarget = Mybutton.Myskill.Master;
            //MasterAudio.PlaySound("ProgramMaster_Boss_Theme_Phase2_(Intro)", 1f, null, 0f, null, null, false, false);
        }
        /*
        public IEnumerator EnemyAttackScene(Skill UseSkill, List<BattleChar> Targets)
        {
            if (UseSkill.MySkill.KeyID == GDEItemKeys.Skill_S_ProgramMaster2_Main)
            {
                if (BattleSystem.instance.BattleWaitList.Count >= 1)
                {
                    yield return new WaitForSecondsRealtime(0.2f);
                }
                yield return new WaitForSecondsRealtime(1f);
                yield return BattleText.InstBattleText_Co(this.BChar, ScriptLocalization.CharText_Enemy_ProgramMaster2nd.Extinction, true, -1, 0f, false);
                yield return new WaitForSecondsRealtime(0.5f);
                List<Skill> list = new List<Skill>();
                foreach (BattleChar input in BattleSystem.instance.AllyTeam.AliveChars)
                {
                    list.Add(Skill.TempSkill(GDEItemKeys.Skill_S_ProgramMaster2_LucyGuard, input, null));
                }
                yield return BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(this.Select), "....", false, false, false, false, true);
                if (this.SelectTarget != null)
                {
                    Targets.Clear();
                    Targets.Add(this.SelectTarget);
                }
                else
                {
                    Targets.Add(BattleSystem.instance.AllyTeam.AliveChars[0]);
                }
            }
            yield return null;
            yield break;
        }

        // Token: 0x06001810 RID: 6160 RVA: 0x000AC14C File Offset: 0x000AA34C
        public void Select(SkillButton Mybutton)
        {
            this.SelectTarget = Mybutton.Myskill.Master;
            MasterAudio.PlaySound("ProgramMaster_Boss_Theme_Phase2_(Intro)", 1f, null, 0f, null, null, false, false);
        }
        */
        public int remainCount = 9;
        public int round = 0;
        private BattleChar SelectTarget;
    }

    public class Bcl_Altr_B_p_3 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.CantHeal = true;
        }
    }
    public class Sex_Altr_B_1 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            Bcl_Altr_B_p_1.StackChange(this.BChar, 3);
            //Bcl_Altr_B_p_1.StackChange(this.BChar, 30);
        }
    }

    public class Sex_Altr_B_2 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            foreach (BattleChar hit in Targets)
            {
                for (int i = 0; i < hit.Buffs.Count; i++)
                {
                    if (hit.Buffs[i].BuffData.BuffTag.Key == GDEItemKeys.BuffTag_Debuff && !hit.Buffs[i].CantDisable)
                    {
                        hit.Buffs[i].SelfDestroy(false);
                    }
                }
            }
        }
    }
    public class Bcl_Altr_B_2 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.RES_CC = 300;
            this.PlusStat.RES_DEBUFF = 300;
        }
    }

    public class Sex_Altr_B_3 : Skill_Extended
    {

    }
    public class Bcl_Altr_B_3_1 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.def = 20;
        }
    }
    public class Bcl_Altr_B_3_2 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusPerStat.Damage = -50;
            this.PlusStat.hit = -50;
        }
    }

    public class Sex_Altr_B_4: Skill_Extended
    {

        public override string DescExtended(string desc)
        {
            int turn = Bcl_Altr_B_p_1.GetStack(this.BChar);
            this.PlusSkillPerStat.Damage = 10 * turn;
            return base.DescExtended(desc).Replace("&a", ((int)(this.BChar.GetStat.atk * 0.1)).ToString());
        }

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            int turn = Bcl_Altr_B_p_1.GetStack(this.BChar);
            this.PlusSkillPerStat.Damage = 10 * turn;
            Bcl_Altr_B_p_1.StackChange(this.BChar, -2);
        }


    }


    public class Sex_Altr_B_5 : Skill_Extended, IP_SkillCastingStart
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            Bcl_Altr_B_p_1.StackChangeTo(this.BChar, 2 * Bcl_Altr_B_p_1.GetStack(this.BChar));
            
            foreach (BattleChar bc in Targets)
            {
                Buff buff = bc.BuffAdd("B_FGO_Altria_Alter_B_5", this.BChar);

            }
            
        }
        public void SkillCasting(CastingSkill ThisSkill)
        {
            //BattleSystem.instance.StartCoroutine(this.Delay());
            this.BChar.BuffAdd("B_FGO_Altria_Alter_B_5_1", this.BChar, false, 0, false, -1, false);
        }
        public IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.1f);
            this.BChar.BuffAdd("B_FGO_Altria_Alter_B_5_1", this.BChar, false, 0, false, -1, false);
            yield break;
        }
    }
    /*
    public class Bcl_Altr_5 : Common_Buff_Rest,IP_Discard
    {
        // Token: 0x06000B38 RID: 2872 RVA: 0x0007FC40 File Offset: 0x0007DE40
        
        public override void Init()
        {
            base.Init();
            this.PlusStat.Stun = true;
        }

        // Token: 0x06000B39 RID: 2873 RVA: 0x00078423 File Offset: 0x00076623

        // Token: 0x06000B3A RID: 2874 RVA: 0x0007FC8C File Offset: 0x0007DE8C
        public override void SelfdestroyPlus()
        {
            base.SelfdestroyPlus();
            this.BChar.BuffAdd(GDEItemKeys.Buff_B_Common_CCRsis, this.BChar, false, 0, false, -1, false);
            this.BChar.BuffAdd(GDEItemKeys.Buff_B_Common_CCRsis, this.BChar, false, 0, false, -1, false);
            this.BChar.BuffAdd(GDEItemKeys.Buff_B_Common_CCRsis, this.BChar, false, 0, false, -1, false);
            this.BChar.BuffAdd(GDEItemKeys.Buff_B_Common_CCRsis, this.BChar, false, 0, false, -1, false);
            this.BChar.BuffAdd(GDEItemKeys.Buff_B_Common_CCRsis, this.BChar, false, 0, false, -1, false);
            this.BChar.BuffAdd(GDEItemKeys.Buff_B_Common_CCRsis, this.BChar, false, 0, false, -1, false);
        }
        
    }
        */
    public class Bcl_Altr_B_5_1 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.invincibility = true;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.frame++;
            if (this.frame > 20 || this.frame < 0)
            {
                this.frame = 0;
                bool flag = this.CheckMainSkill();
                if(!flag)
                {
                    this.SelfDestroy();
                }

            }
        }
        public int frame = 0;
        public bool CheckMainSkill()
        {
            if (BattleSystem.instance == null)
            {
                return false;
            }
            
            foreach (CastingSkill castingSkill in this.BChar.BattleInfo.CastSkills)
            {
                if (castingSkill.skill.MySkill.KeyID == "S_FGO_Altria_Alter_B_5" && castingSkill.skill.Master == this.BChar)
                {
                    return true;
                }
            }
            foreach (CastingSkill castingSkill in this.BChar.BattleInfo.SaveSkill)
            {
                if (castingSkill.skill.MySkill.KeyID == "S_FGO_Altria_Alter_B_5" && castingSkill.skill.Master == this.BChar)
                {
                    return true;
                }
            }
            
            foreach (CastingSkill castingSkill in BattleSystem.instance.EnemyCastSkills)
            {
                if (castingSkill.skill.MySkill.KeyID == "S_FGO_Altria_Alter_B_5" && castingSkill.skill.Master == this.BChar)
                {
                    return true;
                }
            }
            return false;
        }
    }


    public class Sex_Altr_B_6 : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&num1", ((int)(this.BChar.GetStat.atk * 0.5)).ToString()).Replace("&num2", ((int)this.BChar.GetStat.HIT_DEBUFF + 110).ToString());

        }

        public override void Init()
        {
            base.Init();
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_MissChain_Ex_P).Particle_Path;
        }
        /*
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.CheckFire();
        }
        public void CheckFire()
        {
            bool flag1 = Bcl_Altr_B_p_1.GetStack(this.BChar) > 12;
            if (flag1 != this.currentFire)
            {
                this.currentFire = flag1;
                if (flag1)
                {
                    base.SkillParticleOn();
                }
                else
                {
                    base.SkillParticleOff();
                }
            }
        }
        */
        public bool currentFire = false;
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            if (Bcl_Altr_B_p_1.GetStack(this.BChar) > 12 && Targets.Count > 0)
            {
                //this.PlusSkillStat.Weak = true;
                this.SkillBasePlus.Target_BaseDMG = (int)(this.BChar.GetStat.atk * 0.5);
                BattleSystem.DelayInputAfter(this.Delay(Targets[0]));
            }
            //Bcl_Altr_B_p_1.StackChange(this.BChar, 6);
        }
        public IEnumerator Delay(BattleChar target)
        {


            yield return new WaitForSeconds(0.15f);

            Skill skill = Skill.TempSkill("S_FGO_Altria_Alter_B_6_1", this.BChar);
            skill.PlusHit = true;
            skill.FreeUse = true;
            List<BattleChar> list = new List<BattleChar>();
            list.AddRange(target.MyTeam.AliveChars);
            list.Remove(target);
            if (list.Count > 0)
            {
                this.BChar.ParticleOut(this.MySkill, skill, list);

            }


        }
    }
    public class Sex_Altr_B_6_1 : Skill_Extended
    {

    }


    public class Sex_Altr_B_7 : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&num1", ((int)this.BChar.GetStat.atk).ToString()).Replace("&num2", ((int)this.BChar.GetStat.HIT_DOT + 125).ToString());
        }
        /*
        public override void Init()
        {
            base.Init();
            this.SkillParticleObject = new GDESkillExtendedData(GDEItemKeys.SkillExtended_MissChain_Ex_P).Particle_Path;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.CheckFire();
        }
        public void CheckFire()
        {
            bool flag1 = Bcl_Altr_p_1.GetStack(this.BChar) > 20;
            if (flag1 != this.currentFire)
            {
                this.currentFire = flag1;
                if (flag1)
                {
                    base.SkillParticleOn();
                }
                else
                {
                    base.SkillParticleOff();
                }
            }
        }
        
        public bool currentFire = false;
        */
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            if (Bcl_Altr_B_p_1.GetStack(this.BChar) > 20)
            {
                BuffTag btg = new BuffTag();
                btg.BuffData = new GDEBuffData("B_FGO_Altria_Alter_B_7");
                this.TargetBuff.Add(btg);
            }

            //Bcl_Altr_B_p_1.StackChange(this.BChar, 10);
        }
        /*
        public override void SkillKill(SkillParticle SP)
        {
            base.SkillKill(SP);
            if (SP.SkillData == this.MySkill)
            {
                Bcl_Altr_p_1.StackChange(this.BChar, 2);
            }
        }
        */
    }
    public class Bcl_Altr_B_7 : Buff
    {

    }

    public class Sex_Altr_B_8 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            foreach (BattleChar hit in Targets)
            {
                for (int i = 0; i < hit.Buffs.Count; i++)
                {
                    if (hit.Buffs[i].BuffData.BuffTag.Key == GDEItemKeys.BuffTag_Debuff && !hit.Buffs[i].CantDisable)
                    {
                        hit.Buffs[i].SelfDestroy(false);
                    }
                    else if(hit.Buffs[i].BuffData.BuffTag.Key == GDEItemKeys.BuffTag_DOT && !hit.Buffs[i].CantDisable)
                    {
                        hit.Buffs[i].SelfDestroy(false);
                    }
                }
            }
        }
    }
    public class Bcl_Altr_B_8 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.RES_CC += 20;
            this.PlusStat.RES_DEBUFF += 20;
            this.PlusStat.RES_DOT += 20;
        }
    }


    public class Sex_Altr_B_9 : Skill_Extended,IP_SkillCastingStart
    {
        public void SkillCasting(CastingSkill ThisSkill)
        {
            //BattleSystem.instance.StartCoroutine(this.Delay());
            this.BChar.BuffAdd("B_FGO_Altria_Alter_B_9_1", this.BChar, false, 0, false, -1, false);
        }
    }
    public class Bcl_Altr_B_9 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusPerStat.Damage = 50;
            this.PlusStat.def = 20;
            this.PlusStat.dod = 30;
            this.PlusStat.hit = 50;
        }
    }
    public class Bcl_Altr_B_9_1 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.invincibility = true;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.frame++;
            if (this.frame > 20 || this.frame < 0)
            {
                this.frame = 0;
                bool flag = this.CheckMainSkill();
                if (!flag)
                {
                    this.SelfDestroy();
                }

            }
        }
        public int frame = 0;
        public bool CheckMainSkill()
        {
            if (BattleSystem.instance == null)
            {
                return false;
            }

            foreach (CastingSkill castingSkill in this.BChar.BattleInfo.CastSkills)
            {
                if (castingSkill.skill.MySkill.KeyID == "S_FGO_Altria_Alter_B_9" && castingSkill.skill.Master == this.BChar)
                {
                    return true;
                }
            }
            foreach (CastingSkill castingSkill in this.BChar.BattleInfo.SaveSkill)
            {
                if (castingSkill.skill.MySkill.KeyID == "S_FGO_Altria_Alter_B_9" && castingSkill.skill.Master == this.BChar)
                {
                    return true;
                }
            }

            foreach (CastingSkill castingSkill in BattleSystem.instance.EnemyCastSkills)
            {
                if (castingSkill.skill.MySkill.KeyID == "S_FGO_Altria_Alter_B_9" && castingSkill.skill.Master == this.BChar)
                {
                    return true;
                }
            }
            return false;
        }
    }


    public class Sex_Altr_B_10 : Skill_Extended, IP_DamageChange
    {

        public override void Init()
        {
            base.Init();
            this.OnePassive = true;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.frame++;

            if (this.frame > 20 || this.frame < 0)
            {
                if (Bcl_Altr_B_p_1.GetStack(this.BChar) != this.mana)
                {
                    this.mana = Bcl_Altr_B_p_1.GetStack(this.BChar);

                    Traverse.Create(this.MySkill.MySkill.Effect_Target).Field("_DMG_Per").SetValue(10 * Bcl_Altr_B_p_1.GetStack(this.BChar));

                }


            }

        }
        public int mana = 0;
        public int frame = 0;

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            if (Bcl_Altr_B_p_1.GetStack(this.BChar) > 30)
            {
                Bcl_Altr_B_p_1.StackChangeTo(this.BChar, 2 * Bcl_Altr_B_p_1.GetStack(this.BChar));
            }

                Traverse.Create(this.MySkill.MySkill.Effect_Target).Field("_DMG_Per").SetValue(10 * Bcl_Altr_B_p_1.GetStack(this.BChar));
            
            if (Bcl_Altr_B_p_1.GetStack(this.BChar) > 30)
            {
                this.SkillBasePlus.Target_BaseDMG = (int)(0.5 * this.BChar.GetStat.atk * Bcl_Altr_B_p_1.GetStack(this.BChar));
                this.BChar.BuffAdd("B_FGO_Altria_Alter_B_10", this.BChar);
            }

        }
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if (SkillD == this.MySkill && Bcl_Altr_B_p_1.GetStack(this.BChar) > 20)
            {
                float num = (float)this.MySkill.GetCriPer(Target, 0);
                int num2 = 0;
                if (num > 100f)
                {
                    num2 = (int)(num - 100f);
                }
                if (num2 > 0)
                {
                    return (int)(Damage * (1 + num2 * 0.01));
                }
            }
            return Damage;
        }
    }
    public class Bcl_Altr_B_10 : Buff,IP_TurnEnd
    {
        public override string DescExtended()
        {
            int num1 = (int)Math.Pow(2, this.level);
            int num2 = (int)(1000 / num1);
            float num3 = (float)(0.1 * num2);
            return base.DescExtended().Replace("&a", (100 - num3).ToString());
        }
        public override void Init()
        {
            base.Init();
            this.level++;
        }
        public void TurnEnd()
        {
            int num1 = (int)Math.Pow(2, this.level);
            int num2 = Bcl_Altr_B_p_1.GetStack(this.BChar);
            int num3 = (int)(num2 / num1);
            Bcl_Altr_B_p_1.StackChangeTo(this.BChar, num3);
            this.SelfDestroy();
        }

        public int level = 0;
    }


    /*
    public class BattleExtended_Altria_Alter_Boss : PassiveBase, IP_BattleEndRewardChange//,IP_PlayerTurn
    {


        public void BattleEndRewardChange()
        {
            //BattleSystem.instance.Reward.Add(ItemBase.GetItem("Item_SF_1", 4));
            //Skill skill = Skill.TempSkill("S_PD_Reward_0", BattleSystem.instance.AllyTeam.LucyChar, BattleSystem.instance.AllyTeam);
            //BattleSystem.instance.Reward.Add(ItemBase.GetItem(new GDESkillData("S_PD_Reward_0")));

            BattleSystem.instance.Reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Consume_EquipPouch));
            BattleSystem.instance.Reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Consume_EquipPouch));
            BattleSystem.instance.Reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy_Rare));
            BattleSystem.instance.Reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Misc_Soul, 5));
        }
    }
    */
}
