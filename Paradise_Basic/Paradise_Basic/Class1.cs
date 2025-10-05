using ChronoArkMod.Plugin;
using ChronoArkMod;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using GameDataEditor;
using SF_Standard;

namespace Paradise_Basic
{

    public class Paradise_Basic_ModPlugin : ChronoArkPlugin
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
        public class Paradise_BasicDef : ModDefinition
        {
        }
    }
    [HarmonyPatch(typeof(BuffObject))]
    public class BuffObjectUpdatePlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        public static void Update_Patch(BuffObject __instance)//BuffObjectUpdate_Patch(BuffObject __instance)
        {

            if (__instance.MyBuff != null)
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
        void BuffObject_Updata(BuffObject obj);
    }

    public class AI_PD_Paradise_Base : AI
    {
        /*
        public virtual void ForceShieldData(ref int maxnum,ref int num)
        {

        }
        */
    }


    public interface IP_ForceShieldData
    {
        // Token: 0x06000001 RID: 1
        void ForceShieldData(ref int maxnum, ref int num);

    }
    public class Bcl_Paradise_force : Buff, IP_DamageTakeChange, IP_BeforeHit, IP_Hit, IP_BuffObject_Updata//paradise base
    {
        public override string DescExtended()
        {
            if (this.maxValue > 0)
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
            string str1 = "0%";
            if (this.maxValue > 0)
            {
                str1 = ((int)(100 * this.value / this.maxValue)).ToString() + "%";

            }
            else
            {
                str1 = "0%";
            }
            int num = 3*str1.Length - 7;
            for (int i = 0; i < num; i++)
            {
                str1 = str1 + " ";
            }

            obj.StackText.text = str1;
        }
        public static int GetForceShield(BattleChar bc)
        {
            if(bc.BuffFind("B_PD_Paradise_force",false))
            {
                Bcl_Paradise_force buff= bc.BuffReturn("B_PD_Paradise_force", false) as Bcl_Paradise_force;
                return buff.value;
            }
            return 0;
        }
        public static int GetForceData(BattleChar bc)
        {
            if (bc is BattleEnemy)
            {
                AI ai = (bc as BattleEnemy).Ai;
                int maxnum = 0;
                int num = 0;
                IP_ForceShieldData ip_patch = ai as IP_ForceShieldData;
                if (ip_patch != null)
                {
                    ip_patch.ForceShieldData(ref maxnum, ref num);
                }
                return maxnum;
            }
            return 0;
        }
        public override void BuffOneAwake()

        {
            base.BuffOneAwake();
            if (this.BChar is BattleEnemy)
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
            base.TurnUpdate();
            if (this.BChar is BattleEnemy)
            {
                (this.BChar as BattleEnemy).UseCustomPos = true;

            }
        }
        public void BeforeHit(SkillParticle SP, int DMG, bool Cri)
        {
            if (SP.TargetChar.Contains(this.BChar))
            {
                this.saveDmg = DMG;
            }
        }
        public void Hit(SkillParticle SP, int Dmg, bool Cri)
        {
            if (this.saveDmg <= 0)
            {
                return;
            }
            if (SP.UseStatus == BattleSystem.instance.AllyTeam.LucyChar)// && SP.SkillData.TargetTypeKey!="all_enemy" && SP.SkillData.TargetTypeKey != "all_ally" && SP.SkillData.TargetTypeKey != "all_allyorenemy" && SP.SkillData.TargetTypeKey != "all")
            {
                this.ChangeForceShield(-3 * this.saveDmg);
            }
            else
            {
                this.ChangeForceShield(-this.saveDmg);
            }
            this.saveDmg = 0;
        }
        public int DamageTakeChange(BattleChar Hit, BattleChar User, int Dmg, bool Cri, bool NODEF = false, bool NOEFFECT = false, bool Preview = false)
        {

            if (User != BattleSystem.instance.AllyTeam.LucyChar && Hit == this.BChar && this.maxValue > 0 && Dmg > 0)
            {
                float per = (float)(this.maxValue - this.value) / this.maxValue;
                if (NODEF)
                {
                    per = (float)(this.maxValue - this.value / 2) / this.maxValue;
                }
                //Debug.Log(per);
                per = Mathf.Clamp01(per);
                //Debug.Log(per);
                float num = Math.Max(1, Dmg * per);
                return (int)num;
            }
            return Dmg;
        }
        

        public void SetForceShield(int maxNum, int num = -1)
        {
            this.maxValue = Math.Max(0, maxNum);
            if (num >= 0)
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
            this.value = Mathf.Clamp(this.value + num, 0, this.maxValue);
            if (this.candistroy && this.value <= 0)
            {
                this.SelfDestroy();
            }
        }
        public void ChangeForceShield_per(int num)
        {
            this.value = Mathf.Clamp(this.value + (int)(num * this.maxValue / 100), 0, this.maxValue);
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
            if (this.BChar is BattleEnemy)
            {
                if (this.MainBE == null)
                {
                    this.MainBE = (this.BChar as BattleEnemy);
                    this.MainBE.MasterBossSmallHPBar.gameObject.SetActive(true);
                }
                this.MainBE.MasterBossSmallHPBar.MainBar.fillAmount = Misc.NumToPer((float)this.maxValue, (float)this.value) * 0.01f;
            }

        }
        public BattleEnemy MainBE;
        
    }



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
    public class Bcl_Paradise_s : Buff, IP_BuffObject_Updata//paradise base
    {
        public void BuffObject_Updata(BuffObject obj)
        {
            obj.StackText.text = this.BarrierHP.ToString();
        }
    }
}
