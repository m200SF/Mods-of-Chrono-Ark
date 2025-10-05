using ChronoArkMod.Plugin;
using ChronoArkMod;
using GameDataEditor;
using HarmonyLib;
using Paradise_Basic;
using SF_Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ChronoArkMod.ModData;
using ChronoArkMod.Template;

namespace PD_Paradise_Enemy
{
    [PluginConfig("M200_PD_Paradise_Enemy_CharMod", "PD_Paradise_Enemy_EnemyMod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class PD_Paradise_Enemy_CharacterModPlugin : ChronoArkPlugin
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
        public class Paradise_EnemyDef : ModDefinition
        {
        }

        public class S_PD_Patmos_1_Particle : CustomSkillGDE<PD_Paradise_Enemy_CharacterModPlugin.Paradise_EnemyDef>
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

        public class S_PD_Patmos_0_Particle : CustomSkillGDE<PD_Paradise_Enemy_CharacterModPlugin.Paradise_EnemyDef>
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
    }


    public class AI_PD_NytoRF : AI_PD_Paradise_Base, IP_ForceShieldData
    {
        public void ForceShieldData(ref int maxnum, ref int num)
        {
            maxnum = 120;
            num = 48;
        }
        public override int SpeedChange(Skill skill, int ActionCount, int OriginSpeed)
        {
            if (OriginSpeed < 1)
            {
                return 1;
            }
            return base.SpeedChange(skill, ActionCount, OriginSpeed);
        }
    }
    public class Bcl_NytoRF_p : Buff
    {
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if ((this.BChar as BattleEnemy).SkillQueue.Count <= 0)
            {
                this.PlusStat.Vanish = true;
                return;
            }

            this.PlusStat.Vanish = false;

        }
    }
    public class Sex_NytoRF_1 : Sex_ImproveClone
    {

    }

    public class AI_PD_NytoHammer : AI_PD_Paradise_Base, IP_ForceShieldData
    {
        public void ForceShieldData(ref int maxnum, ref int num)
        {
            maxnum = 200;
            num = 120;
        }
    }
    public class Bcl_NytoHammer_p : Buff, IP_HPChange
    {
        public void HPChange(BattleChar Char, bool Healed)
        {
            if (Char == this.BChar)
            {
                this.PlusStat.def = 60 * Mathf.Clamp01(1f - ((float)this.BChar.HP / (float)this.BChar.GetStat.maxhp));
            }
        }
    }
    public class Sex_NytoHammer_1 : Sex_ImproveClone
    {

    }

    public class AI_PD_NytoSMG : AI_PD_Paradise_Base, IP_ForceShieldData
    {
        public void ForceShieldData(ref int maxnum, ref int num)
        {
            maxnum = 150;
            num = 60;
        }
    }
    public class Bcl_NytoSMG_p : Buff, IP_Hit
    {
        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            this.PlusStat.dod += 40;
        }
        public override void TurnUpdate()
        {
            base.TurnUpdate();
            this.PlusStat.dod = 0;
        }
    }
    public class Sex_NytoSMG_1 : Sex_ImproveClone
    {

    }

    public class AI_PD_NytoCommander : AI_PD_Paradise_Base, IP_ForceShieldData
    {
        public void ForceShieldData(ref int maxnum, ref int num)
        {
            maxnum = 100;
            num = 70;
        }
        public override List<BattleChar> TargetSelect(Skill SelectedSkill)
        {
            if (SelectedSkill.MySkill.KeyID == this.BChar.Skills[0].MySkill.KeyID)
            {
                List<BattleChar> list1 = new List<BattleChar>();
                foreach (BattleAlly battleAlly in this.BChar.BattleInfo.AllyList)
                {
                    if (!battleAlly.GetStat.Vanish)
                    {
                        list1.Add(battleAlly);
                    }
                }
                if (list1.Count > 0)
                {
                    list1 = (from x in list1
                             orderby this.GetBuffStack(x, "B_PD_NytoCommander_0")
                             select x).ToList<BattleChar>();
                    for (int i = 0; i <= list1.Count - 1; i++)
                    {
                        if (GetBuffStack(list1[i], "B_PD_NytoCommander_0") > GetBuffStack(list1[0], "B_PD_NytoCommander_0"))
                        {
                            list1.RemoveAt(i);
                            i--;
                        }
                    }
                    List<BattleAlly> list2 = new List<BattleAlly>();
                    foreach (BattleChar bc in list1)
                    {
                        try
                        {
                            list2.Add(bc as BattleAlly);
                        }
                        catch
                        {

                        }
                    }
                    BattleChar tar = Aggro.AgrroReturn(this.BChar, list2);
                    List<BattleChar> list3 = new List<BattleChar>();
                    if (tar != null)
                    {
                        list3.Add(tar);
                        return list3;
                    }

                }
            }
            return base.TargetSelect(SelectedSkill);
        }
        public override Skill SkillSelect(int ActionCount)
        {
            if (ActionCount == 0)
            {
                return this.BChar.Skills[0];
            }
            else
            {
                List<Skill> list = new List<Skill>();
                list.AddRange(this.BChar.Skills);
                list.RemoveAt(0);
                return this.SkillRandomSelect(list);
            }
        }

        public override int SpeedChange(Skill skill, int ActionCount, int OriginSpeed)
        {
            if ((this.BChar as BattleEnemy).SkillQueue.Count > 0 && (this.BChar as BattleEnemy).SkillQueue[0].skill.MySkill.KeyID == "S_PD_NytoCommander_0")
            {
                (this.BChar as BattleEnemy).SkillQueue[0].CastSpeed = 0;
            }
            return base.SpeedChange(skill, ActionCount, OriginSpeed);
        }
        public int GetBuffStack(BattleChar bc, string buffkey)
        {
            if (bc.BuffFind(buffkey, false))
            {
                return bc.BuffReturn(buffkey, false).StackNum;
            }
            return 0;
        }
    }
    /*
    public class Bcl_NytoCommander_p : Buff, IP_PlayerTurn_EnemyAfter
    {
        public void PlayerTurn_EnemyAfter()
        {
            Skill skill = this.BChar.Skills[0];
            BattleSystem.instance.EnemyCastEnqueue(this.BChar as BattleEnemy, skill, (this.BChar as BattleEnemy).Ai.TargetSelect(skill), 1, false);
        }
    }
    */


    public class Sex_NytoCommander_0 : Sex_ImproveClone//,IP_TargetAI
    {
        public override void Init()
        {
            base.Init();
            this.PlusSkillStat.Weak = true;
        }


    }

    public class Bcl_NytoCommander_0 : Buff, IP_SkillUse_User, IP_Discard
    {
        public override void BuffStat()
        {
            base.BuffStat();
            if (this.BChar.Info.GetData.Role?.Key == GDEItemKeys.CharRole_Role_Tank)
            {
                this.PlusStat.def = -24 * this.StackNum;
            }
            else if (this.BChar.Info.GetData.Role?.Key == GDEItemKeys.CharRole_Role_Support)
            {
                this.PlusPerStat.Heal = -24 * this.StackNum;
            }
            else if (this.BChar.Info.GetData.Role?.Key == GDEItemKeys.CharRole_Role_DPS)
            {
                this.PlusPerStat.Damage = -24 * this.StackNum;
            }
            else
            {
                this.PlusStat.def = -8 * this.StackNum;
                this.PlusPerStat.Heal = -8 * this.StackNum;
                this.PlusPerStat.Damage = -8 * this.StackNum;
            }
        }
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == this.BChar)
            {
                this.SelfStackDestroy();
            }
        }
        public void Discard(bool Click, Skill skill, bool HandFullWaste)
        {
            if (skill.Master == this.BChar)
            {
                this.SelfStackDestroy();
            }
        }
    }
    public class Sex_NytoCommander_1 : Sex_ImproveClone
    {

    }
    public class Bcl_NytoCommander_1 : Buff
    {
        public override void BuffStat()
        {
            base.BuffStat();
            this.PlusPerStat.Damage = 10 * this.StackNum;
            this.PlusStat.hit = 15 * this.StackNum;
        }
    }
    public class Sex_NytoCommander_2 : SkillExtended_ParadiseShield
    {
        public override void AddShieldPersent()
        {
            base.AddShieldPersent();
            base.SetShield(true, 25, false, 0);
        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            foreach (BattleChar bc in Targets)
            {
                if (Bcl_Paradise_force.GetForceData(bc) > 0)//(bc.BuffFind("B_PD_Paradise_force",false))
                {
                    //Bcl_Paradise_force buff= bc.BuffReturn("B_PD_Paradise_force", false)as Bcl_Paradise_force;
                    Bcl_Paradise_force buff = bc.BuffAdd("B_PD_Paradise_force", this.BChar, false, 0, false, -1, false) as Bcl_Paradise_force;
                    buff.ChangeForceShield_per(25);
                }
            }
        }
    }

    public class AI_PD_Roarer : AI_PD_Paradise_Base, IP_ForceShieldData
    {
        public void ForceShieldData(ref int maxnum, ref int num)
        {
            maxnum = 400;
            num = 120;
        }
    }
    public class Bcl_Roarer_p_1 : Buff, IP_SkillUse_User_After
    {
        public void SkillUseAfter(Skill SkillD)
        {
            if (SkillD.Master == this.BChar)
            {
                if (this.BChar.BuffFind("B_PD_Paradise_force", false))
                {
                    Bcl_Paradise_force buff = this.BChar.BuffReturn("B_PD_Paradise_force", false) as Bcl_Paradise_force;
                    buff.ChangeForceShield_per(20);
                }
            }
        }

    }

    public class Sex_Roarer_1 : Sex_ImproveClone
    {

    }

    public class AI_PD_StreletPlus : AI_PD_Paradise_Base, IP_ForceShieldData
    {
        public void ForceShieldData(ref int maxnum, ref int num)
        {
            maxnum = 150;
            num = 45;
        }
    }

    public class Sex_StreletPlus_1 : Sex_ImproveClone
    {

    }
    public class Bcl_StreletPlus_1 : Buff
    {
        public override void BuffStat()
        {
            base.BuffStat();
            this.PlusStat.dod = -15;
            this.PlusStat.hit = -15;
        }
    }

    public class AI_PD_Ladon : AI_PD_Paradise_Base, IP_ForceShieldData
    {
        public void ForceShieldData(ref int maxnum, ref int num)
        {
            maxnum = 200;
            num = 50;
        }
    }

    public class AI_PD_Defender : AI_PD_Paradise_Base, IP_ForceShieldData
    {
        public void ForceShieldData(ref int maxnum, ref int num)
        {
            maxnum = 200;
            num = 60;
        }
    }
    public class Bcl_Defender_p : Buff, IP_SkillUse_User, IP_PlayerTurn_EnemyAfter
    {
        public override void Init()
        {
            base.Init();
            if (this.defense)
            {
                this.PlusStat.def = 80;
            }
            else
            {
                this.PlusStat.def = 0;
            }
        }
        public void PlayerTurn_EnemyAfter()
        {
            this.defense = true;
            this.Init();
        }
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if (SkillD.Master == this.BChar)
            {
                this.defense = false;
                this.Init();
            }
        }
        private bool defense = true;
    }
    public class Sex_Defender_1 : Sex_ImproveClone
    {

    }
    public class Bcl_Defender_1 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.def = (float)Math.Min(-20, -0.6 * (this.BChar.GetStat.def - this.PlusStat.def));
        }
    }

    public class AI_PD_Patmos : AI_PD_Paradise_Base, IP_ForceShieldData
    {
        public void ForceShieldData(ref int maxnum, ref int num)
        {
            maxnum = 200;
            num = 80;
        }
    }
    public class Sex_Patmos_0 : Sex_ImproveClone
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

            foreach (BattleChar battleChar in BattleSystem.instance.AllyTeam.AliveChars)
            {
                foreach (Buff buff in battleChar.Buffs)
                {
                    if (!buff.BuffData.Hide)
                    {
                        if (!buff.BuffData.Debuff && buff.BuffData.LifeTime != 0f)
                        {
                            buff.TurnUpdate();
                        }

                    }
                }
            }
        }
    }
    public class Sex_Patmos_1 : Sex_ImproveClone//SkillExtended_ParadiseShield
    {
        /*
        public override void AddShieldPersent()
        {
            base.AddShieldPersent();
            base.SetShield(true, 40, false, 0);
        }
        */
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            foreach (BattleChar bc in Targets)
            {
                Buff buff = bc.BuffAdd("B_PD_Patmos_s", this.BChar, false, 0, false, -1, false);
                buff.BarrierHP += (int)(0.5 * this.BChar.GetStat.maxhp);
            }
        }
    }
    public class Bcl_Patmos_s : Buff, IP_TurnEnd, IP_BuffObject_Updata
    {
        public void TurnEnd()
        {
            if (Bcl_Paradise_force.GetForceData(this.BChar) > 0)//(this.BChar.BuffFind("B_PD_Paradise_force", false))
            {
                Bcl_Paradise_force buff = this.BChar.BuffAdd("B_PD_Paradise_force", this.BChar, false, 0, false, -1, false) as Bcl_Paradise_force;
                buff.ChangeForceShield(this.BarrierHP);
                this.BarrierHP = 0;
                this.SelfDestroy();
            }
        }
        public void BuffObject_Updata(BuffObject obj)
        {
            obj.StackText.text = this.BarrierHP.ToString();
        }
    }


    public class AI_PD_Doppelsoldner : AI_PD_Paradise_Base, IP_ForceShieldData
    {
        public void ForceShieldData(ref int maxnum, ref int num)
        {
            maxnum = 250;
            num = 75;
        }
    }
    public class Bcl_Doppelsoldner_p : Buff, IP_DamageChange
    {
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if (SkillD.Master == this.BChar)
            {
                if (Target.BarrierHP > 0 || Target.MyTeam.partybarrier.BarrierHP > 0)
                {
                    return (int)(0.5 * Damage);
                }
            }
            return Damage;
        }
    }

    public class Sex_Doppelsoldner_1 : Sex_ImproveClone
    {

    }

    public class AI_PD_Patroller : AI_PD_Paradise_Base, IP_ForceShieldData
    {
        public void ForceShieldData(ref int maxnum, ref int num)
        {
            maxnum = 200;
            num = 80;
        }
        public override Skill SkillSelect(int ActionCount)
        {
            if (this.modeFlag)
            {
                return this.BChar.Skills[0];
            }
            else
            {
                return this.BChar.Skills[1];
            }
        }
        public bool modeFlag = true;
    }
    public class Bcl_Patroller_p : Buff, IP_PlayerTurn, IP_Awake
    {
        public void Awake()
        {
            this.Turn();
            this.modeFlag = true;
            ((this.BChar as BattleEnemy).Ai as AI_PD_Patroller).modeFlag = this.modeFlag;
        }

        public void Turn()
        {
            this.ClearEffect();
            if (this.modeFlag)
            {
                //this.modeFlag = false;

                Buff buff = this.BChar.BuffAdd("B_PD_Patroller_s", this.BChar, false, 0, false, -1, false);
                buff.BarrierHP += this.BChar.GetStat.maxhp;
            }
            else
            {
                //this.modeFlag=true;
                //this.PlusPerStat.Damage = 60;
                this.PlusStat.dod = 50;
            }
        }
        public void ClearEffect()
        {
            //this.PlusPerStat.Damage = 0;
            this.PlusStat.dod = 0;
            try
            {
                this.BChar.BuffReturn("B_PD_Patroller_s", false).SelfDestroy();
            }
            catch { }
        }
        public override void TurnUpdate()
        {
            base.TurnUpdate();
            if (this.turn != BattleSystem.instance.TurnNum)
            {
                this.modeFlag = !this.modeFlag;
                this.turn = BattleSystem.instance.TurnNum;

                ((this.BChar as BattleEnemy).Ai as AI_PD_Patroller).modeFlag = this.modeFlag;
            }
        }
        public bool modeFlag = true;
        public int turn = 0;
    }
    public class Bcl_Patroller_s : Buff, IP_BuffObject_Updata
    {
        public void BuffObject_Updata(BuffObject obj)
        {
            obj.StackText.text = this.BarrierHP.ToString();
        }
    }
    public class Sex_Patroller_0 : Sex_ImproveClone
    {

    }
    public class Sex_Patroller_1 : Sex_ImproveClone
    {

    }

    public class AI_PD_Strelet : AI_PD_Paradise_Base, IP_ForceShieldData
    {
        public void ForceShieldData(ref int maxnum, ref int num)
        {
            maxnum = 100;
            num = 40;
        }
    }
    public class Sex_Strelet_1 : Sex_ImproveClone
    {

    }


}
