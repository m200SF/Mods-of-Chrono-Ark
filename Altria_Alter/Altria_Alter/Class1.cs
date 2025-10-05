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
using DG.Tweening;


namespace FGO_Altria_Alter
{
    [PluginConfig("M200_FGO_Altria_Alter_CharMod", "FGO_Altria_Alter_CharMod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class FGO_Altria_Alter_CharacterModPlugin : ChronoArkPlugin
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
        public class Altria_AlterDef : ModDefinition
        {
        }
    }

    [HarmonyPatch(typeof(Character))]
    public class TestCharacterPlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("Set_AllyData")]
        [HarmonyPostfix]
        public static void Set_AllyData_Patch(Character __instance, GDECharacterData Data)//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
            bool flag = Data.Key == "FGO_Altria_Alter";
            if (flag)
            {
                if(__instance.SkillDatas.Count>0)
                {
                    __instance.SkillDatas.RemoveAt(0);
                }
                __instance.SkillDatas.Insert(0,new CharInfoSkillData("S_FGO_Altria_Alter_2"));
            }
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

            if (__instance.MyBuff != null )
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

    [HarmonyPatch(typeof(SkillParticle))]
    public class SkillParticle_init_Plugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("init")]
        [HarmonyPatch(new Type[] { typeof(Skill),typeof(BattleChar), typeof(List<BattleChar>) })]
        [HarmonyPrefix]
        public static void init_Patch(SkillParticle __instance,Skill skill, BattleChar User, List<BattleChar> Target)
        {
            if(skill == null)
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


    public class Skill_7_Particle : CustomSkillGDE<FGO_Altria_Alter_CharacterModPlugin.Altria_AlterDef>
    {
        // Token: 0x060000AB RID: 171 RVA: 0x000055A0 File Offset: 0x000037A0
        public override ModGDEInfo.LoadingType GetLoadingType()
        {
            return ModGDEInfo.LoadingType.Replace;
        }

        // Token: 0x060000AC RID: 172 RVA: 0x000055B4 File Offset: 0x000037B4
        public override string Key()
        {
            return "S_FGO_Altria_Alter_7";
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
            return particle;

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
            bool flag = inputitem.itemkey == "E_FGO_Altria_Alter_0" && __instance.Info.GetData.Key != "FGO_Altria_Alter";
            if (flag)
            {
                __result = true;
            }
            //Debug.Log("成功进入OnDropSlot的patch");
        }
    }
    /*
    [HarmonyPatch(typeof(FriendShipUI))]
    [HarmonyPatch("FriendShipLevelUp")]
    public static class FriendShipLevelUpPlugin
    {
        [HarmonyPostfix]
        public static void FriendShipLevelUp_AfterPatch(FriendShipUI __instance, string CharId, int NowLV)
        {

            if (CharId == "FGO_Altria_Alter" && NowLV >= 3)
            {
                Statistics_Character sc = SaveManager.NowData.statistics.GetCharData(CharId);
                Debug.Log("友谊等级：" + sc.FriendshipLV);
                //Debug.Log("友谊进度：" + sc.FriendshipAmount);
                //Debug.Log("深红对话：" + sc.IsCrimsonDialogue);
                if (NowLV == 3)
                {
                    //Debug.Log("梦想家升级");
                    sc.IsCrimsonDialogue = true;
                    UIManager.InstantiateActiveAddressable(UIManager.inst.CompleteUI, AddressableLoadManager.ManageType.EachStage).GetComponent<FriendShipCompleteUI>().MaxLevel(CharId);
                }
            }

        }

    }
    */
    [HarmonyPatch(typeof(FriendShipUI))]
    [HarmonyPatch("Gift")]
    public static class GiftPlugin
    {
        [HarmonyPostfix]
        public static void Gift_AfterPatch(FriendShipUI __instance, string CharId)
        {

            if (CharId == "FGO_Altria_Alter" )
            {
                Statistics_Character sc = SaveManager.NowData.statistics.GetCharData(CharId);
                int NowLV = sc.FriendshipLV;
                if(NowLV >= 3)
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

    public class P_Altria_Alter : Passive_Char,IP_PlayerTurn, IP_LevelUp,IP_SkillUse_Team
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
            if (!this.ItemTake && this.MyChar.LV >= 6)
            {
                this.ItemTake = true;
                List<ItemBase> list = new List<ItemBase>();
                list.Add(ItemBase.GetItem("E_FGO_Altria_Alter_0", 1));
                InventoryManager.Reward(list);
            }
            yield return null;
            yield break;
        }
        public bool ItemTake;
        public void Turn()
        {
            if(BattleSystem.instance.TurnNum>1 && BattleSystem.instance.TurnNum%2==1)
            {
                if (this.BChar.BuffFind("B_FGO_Altria_Alter_p_1", false))
                {
                    Bcl_Altr_p_1 buff = this.BChar.BuffReturn("B_FGO_Altria_Alter_p_1", false) as Bcl_Altr_p_1;
                    int num = buff.CurrentStack;
                    for (int i = 0; i < num; i++)
                    {
                        this.BChar.BuffAdd("B_FGO_Altria_Alter_p_2", this.BChar);
                    }
                    if(num > 20)
                    {
                        for (int i = 20; i < num; i++)
                        {
                            this.BChar.BuffAdd("B_FGO_Altria_Alter_p_3", this.BChar);
                        }
                    }

                }
            }
        }
        public void SkillUseTeam(Skill skill)
        {
            if(skill.Master==this.BChar && !skill.FreeUse)
            {
                BattleSystem.DelayInputAfter(this.Delay_1());
            }
        }
        public IEnumerator Delay_1()
        {
            Bcl_Altr_p_1.StackChange(this.BChar, 1);
            yield break;
        }
    }
    public class Bcl_Altr_p_1 : Buff,IP_BuffNameEx,IP_BuffObject_Updata
    {
        public string BuffNameEx(string str)
        {
            return str + Misc.InputColor(string.Concat(new object[]
            {
                " - ",
                this.CurrentStack,
                "/",
                (Altria_Alter_Equip.GetEquip(this.BChar)?"∞":this.maxStack.ToString())
            }), "FFF4BF");
        }
        public void BuffObject_Updata(BuffObject obj)
        {
            obj.StackText.text = this.CurrentStack.ToString();
        }
        public static Buff StackChange(BattleChar bc, int change_num)
        {
            Bcl_Altr_p_1 buff;
            if(true||!bc.BuffFind("B_FGO_Altria_Alter_p_1",false))
            {
                buff = bc.BuffAdd("B_FGO_Altria_Alter_p_1", bc, false, 0, false, -1, true) as Bcl_Altr_p_1;
            }
            else
            {
                buff = bc.BuffReturn("B_FGO_Altria_Alter_p_1", false) as Bcl_Altr_p_1;
            }
            buff.CurrentStack += change_num;
            buff.CheckStack();
            return buff;
        }
        public static Buff StackChangeTo(BattleChar bc, int num)
        {
            Bcl_Altr_p_1 buff;
            if (true||!bc.BuffFind("B_FGO_Altria_Alter_p_1", false))
            {
                buff = bc.BuffAdd("B_FGO_Altria_Alter_p_1", bc,false,0,false,-1,true) as Bcl_Altr_p_1;
            }
            else
            {
                buff = bc.BuffReturn("B_FGO_Altria_Alter_p_1", false) as Bcl_Altr_p_1;
            }
            buff.CurrentStack =num;
            buff.CheckStack();
            return buff;
        }
        public static int GetStack(BattleChar bc)
        {
            if (!bc.BuffFind("B_FGO_Altria_Alter_p_1", false))
            {
                return 0;
            }
            else
            {
                Bcl_Altr_p_1 buff = bc.BuffReturn("B_FGO_Altria_Alter_p_1", false) as Bcl_Altr_p_1;
                return buff.CurrentStack;
            }
        }
        public void CheckStack()
        {
            if(!Altria_Alter_Equip.GetEquip(this.BChar))
            {
                this.CurrentStack=Math.Min(25,this.CurrentStack);
            }
            if(this.CurrentStack<=0)
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
                this._currentStack = Math.Max(0,value);
            }
        }
        private int _currentStack = 0;
        private int maxStack = 25;
    }
    public class Bcl_Altr_p_2 : Buff,IP_BuffAdd,IP_BuffAddAfter, IP_BuffNameEx
    {
        public string BuffNameEx(string str)
        {
            return str+ Misc.InputColor(string.Concat(new object[]
            {
                " (",
                this.StackNum,
                ")"
            }), "FF7C34");
        }
        public override void BuffStat()
        {
            base.BuffStat();
            this.PlusStat.cri = 10 * this.StackNum;
            //this.PlusStat.PlusCriDmg = 10 * this.StackNum;
        }
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {


            if (!oldbuff && BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key)
            {
                int stk = 0;
                stk = this.StackNum;
                if (stk >= this.BuffData.MaxStack)
                {
                    StackBuff s1 = this.StackInfo[0];
                    this.StackInfo.Insert(0, s1);
                }
            }
            this.oldbuff = true;
        }
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            this.oldbuff = false;
        }
        public bool oldbuff;
    }
    public class Bcl_Altr_p_3 : Buff, IP_BuffAdd, IP_BuffAddAfter, IP_BuffNameEx
    {
        public string BuffNameEx(string str)
        {
            return str + Misc.InputColor(string.Concat(new object[]
            {
                " (",
                this.StackNum,
                ")"
            }), "FF7C34");
        }
        public override void BuffStat()
        {
            base.BuffStat();
            //this.PlusStat.cri = 10 * this.StackNum;
            this.PlusStat.PlusCriDmg = 10 * this.StackNum;
        }
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {


            if (!oldbuff && BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key)
            {
                int stk = 0;
                stk = this.StackNum;
                if (stk >= this.BuffData.MaxStack)
                {
                    StackBuff s1 = this.StackInfo[0];
                    this.StackInfo.Insert(0, s1);
                }
            }
            this.oldbuff = true;
        }
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            this.oldbuff = false;
        }
        public bool oldbuff;
    }


    public class Sex_Altr_1:Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            Bcl_Altr_p_1.StackChange(this.BChar, 2);
        }
    }

    public class Bcl_Altr_1 : Buff,IP_DamageChange, IP_SkillUse_User//, IP_BuffAdd, IP_BuffAddAfter, IP_BuffNameEx
    {
        /*
        public string BuffNameEx(string str)
        {
            return str + Misc.InputColor(string.Concat(new object[]
            {
                " (",
                this.StackNum,
                ")"
            }), "FF7C34");
        }
        */
        
        public override string DescExtended()
        {
            return base.DescExtended().Replace("&a",(30*this.StackNum).ToString());
        }
        
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if (SkillD .Master==this.BChar && Damage>0 && View)
            {
                return (int)((1 + 0.3 * this.StackNum) * Damage);
            }
            return Damage;
        }
        public void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            if(SkillD.Master==this.BChar && SkillD.IsDamage)
            {
                Sex_Altr_1_1 sex = SkillD.ExtendedAdd(new Sex_Altr_1_1()) as Sex_Altr_1_1;
                sex.stack = this.StackNum;
                this.SelfDestroy();
            }
        }
        /*
        public void Buffadded(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff)
        {


            if (!oldbuff && BuffTaker == this.BChar && addedbuff.BuffData.Key == this.BuffData.Key)
            {
                int stk = 0;
                stk = this.StackNum;
                if (stk >= this.BuffData.MaxStack)
                {
                    StackBuff s1 = this.StackInfo[0];
                    this.StackInfo.Insert(0, s1);
                }
            }
            this.oldbuff = true;
        }
        public void BuffaddedAfter(BattleChar BuffUser, BattleChar BuffTaker, Buff addedbuff, StackBuff stackBuff)
        {
            this.oldbuff = false;
        }
        public bool oldbuff;
        */

        public bool triggered = false;
        public int frame = 0;
    }
    public class Sex_Altr_1_1 : Skill_Extended,IP_DamageChange
    {
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if(SkillD==this.MySkill && Damage > 0)
            {
                return (int)((1 + 0.5 * this.stack) * Damage);
            }
            return Damage;
        }
        public int stack = 0;
    }

    public class Sex_Altr_2 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            Bcl_Altr_p_1.StackChange(this.BChar, 4);

            List<Skill> list = new List<Skill>();
            List<GDESkillData> list2 = new List<GDESkillData>();
            foreach (GDESkillData gdeskillData in PlayData.ALLSKILLLIST)
            {
                if (gdeskillData.User == Targets[0].Info.KeyData)
                {
                    list2.Add(gdeskillData);
                }
            }
            foreach (GDESkillData gdeskillData2 in list2)
            {
                if (gdeskillData2 != null && !gdeskillData2.KeyID.IsNullOrEmpty())
                {
                    Skill skill = Skill.TempSkill(gdeskillData2.KeyID, Targets[0], BattleSystem.instance.AllyTeam).CloneSkill(false, null, null, false);
                    skill.isExcept = true;
                    list.Add(skill);
                }
            }
            BattleSystem.instance.EffectDelays.Enqueue(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(this.Del), ScriptLocalization.System_SkillSelect.CreateSkill, false, true, true, false, true));
        }
        public void Del(SkillButton Mybutton)
        {
            BattleSystem.instance.AllyTeam.Add(Mybutton.Myskill, true);
        }
    }

    public class Sex_Altr_3 : Skill_Extended, IP_SetNoCG
    {
        public bool SetNoCG()
        {
            return true;
        }
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&a",((int)(this.BChar.GetStat.atk*0.1)).ToString());
        }
        public override void Init()
        {
            base.Init();
            this.MySkill.MySkill.VFX = false;

        }
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            int turn = Bcl_Altr_p_1.GetStack(this.BChar);
            foreach(BattleChar bc in Targets)
            {
                BattleSystem.DelayInput(this.Delay(turn, bc));

            }
            Bcl_Altr_p_1.StackChange(this.BChar,-3);
        }

        public IEnumerator Delay(int turn, BattleChar Target)
        {
            for (int i = 0; i < turn; i++)
            {
                if(i==0)
                {
                    yield return new WaitForSeconds(0.35f);
                }
                    yield return new WaitForSeconds(0.15f);
                
                Skill skill = Skill.TempSkill("S_FGO_Altria_Alter_3_1", this.BChar);
                skill.PlusHit = true;
                skill.FreeUse = true;
                if(Target.IsDead)
                {
                    if(BattleSystem.instance.EnemyTeam.AliveChars.Count != 0)
                    {
                        this.BChar.ParticleOut(this.MySkill, skill, BattleSystem.instance.EnemyTeam.AliveChars.Random(this.BChar.GetRandomClass().Main));
                    }
                }
                else
                {
                    this.BChar.ParticleOut(this.MySkill, skill, Target);
                }
            }
        }
    }
    public class Sex_Altr_4 : Skill_Extended
    {

    }

    public class Bcl_Altr_4 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.invincibility = true;
            this.PlusPerStat.Damage = 30;
            this.PlusStat.hit = 30;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (Bcl_Altr_p_1.GetStack(this.BChar) != 10)
            {
                Bcl_Altr_p_1.StackChangeTo(this.BChar,10);
            }
        }
    }
    public class Sex_Altr_5 : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&num1",((int)this.BChar.GetStat.atk).ToString()).Replace("&num2", ((int)this.BChar.GetStat.HIT_DOT+125).ToString());
        }
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
            if (flag1!=this.currentFire)
            {
                this.currentFire = flag1;
                if(flag1)
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
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            if(Bcl_Altr_p_1.GetStack(this.BChar)>20)
            {
                BuffTag btg= new BuffTag();
                btg.BuffData = new GDEBuffData("B_FGO_Altria_Alter_5");
                this.TargetBuff.Add(btg);
            }
        }
        public override void SkillKill(SkillParticle SP)
        {
            base.SkillKill(SP);
            if(SP.SkillData==this.MySkill)
            {
                Bcl_Altr_p_1.StackChange(this.BChar, 2);
            }
        }
        
    }
    public class Bcl_Altr_5 : Buff
    {

    }
    public class Sex_Altr_6 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            foreach(BattleChar hit in Targets)
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
    public class Bcl_Altr_6 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.RES_CC = 300;
            this.PlusStat.RES_DEBUFF = 300;
        }
    }

    public class Sex_Altr_7 : Skill_Extended,IP_DamageChange
    {

        public override void Init()
        {
            base.Init();
            this.OnePassive = true;
        }
        public override bool Terms()
        {
            if (this.MySkill.FreeUse)
            {
                return true;
            }
            if(BattleSystem.instance!=null)
            {
                if (Bcl_Altr_p_1.GetStack(this.BChar) > 15)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return base.Terms();
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.frame++;

            if (this.frame>20||this.frame<0)
            {
                if (Bcl_Altr_p_1.GetStack(this.BChar) != this.mana)
                {
                    this.mana = Bcl_Altr_p_1.GetStack(this.BChar);

                        Traverse.Create(this.MySkill.MySkill.Effect_Target).Field("_DMG_Per").SetValue(10 * Bcl_Altr_p_1.GetStack(this.BChar));

                }

                
            }

        }
        public int mana = 0;
        public int frame = 0;

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            if(Bcl_Altr_p_1.GetStack(this.BChar)>30)
            {
                Bcl_Altr_p_1.StackChangeTo(this.BChar, 2 * Bcl_Altr_p_1.GetStack(this.BChar));
            }
            if (Bcl_Altr_p_1.GetStack(this.BChar) > 15)
            {
                Traverse.Create(this.MySkill.MySkill.Effect_Target).Field("_DMG_Per").SetValue(10* Bcl_Altr_p_1.GetStack(this.BChar));
            }
            if (Bcl_Altr_p_1.GetStack(this.BChar) > 30)
            {
                this.SkillBasePlus.Target_BaseDMG=(int)(0.5*this.BChar.GetStat.atk * Bcl_Altr_p_1.GetStack(this.BChar));
                this.BChar.BuffAdd("B_FGO_Altria_Alter_7", this.BChar);
            }

            this.BChar.MyTeam.AP = 0;
        }
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if(SkillD==this.MySkill && Bcl_Altr_p_1.GetStack(this.BChar) > 20)
            {
                float num = (float)this.MySkill.GetCriPer(Target, 0);
                int num2 = 0;
                if (num > 100f)
                {
                    num2 = (int)(num - 100f);
                }
                if(num2 >0)
                {
                    return (int)(Damage * (1 + num2*0.01));
                }
            }
            return Damage;
        }
    }
    public class Bcl_Altr_7 : Buff
    {
        public override string DescExtended()
        {
            int num1 = (int)Math.Pow(2, this.level);
            int num2 = (int)(1000 / num1);
            float num3 = (float)(0.1 * num2);
            return base.DescExtended().Replace("&a",(100-num3).ToString());
        }
        public override void Init()
        {
            base.Init();
            this.level++;
        }
        public override void SelfdestroyPlus()
        {
            base.SelfdestroyPlus();
            int num1 = (int)Math.Pow(2, this.level);
            int num2 = Bcl_Altr_p_1.GetStack(this.BChar);
            int num3 = (int)(num2 / num1);
            Bcl_Altr_p_1.StackChangeTo(this.BChar, num3);
        }

        public int level = 0;
    }


    public class Sex_Altr_8 : Skill_Extended
    {

    }
    public class Bcl_Altr_8 : Buff
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

    public class Sex_Altr_9 : Skill_Extended
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            Bcl_Altr_p_1.StackChangeTo(this.BChar, 2 * Bcl_Altr_p_1.GetStack(this.BChar));
            bool equip=Altria_Alter_Equip.GetEquip(this.BChar);
            foreach (BattleChar bc in Targets)
            {
                Buff buff=bc.BuffAdd("B_FGO_Altria_Alter_9",this.BChar);
                if(equip)
                {
                    foreach(StackBuff sta in buff.StackInfo)
                    {
                        sta.RemainTime += 2;
                    }
                }
            }
        }
    }
    public class Bcl_Altr_9 : Buff
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

    public class Sex_Altr_10 : Skill_Extended
    {
        // Token: 0x06001345 RID: 4933 RVA: 0x0009A88C File Offset: 0x00098A8C
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            foreach (BattleChar battleChar in BattleSystem.instance.AllyTeam.AliveChars)
            {
                if (battleChar.Info.KeyData == "FGO_Altria_Alter")
                {
                    Bcl_Altr_p_1.StackChange(battleChar, 3);
                }
            }
            BattleSystem.DelayInput(this.DrawEffect());
        }

        // Token: 0x06001346 RID: 4934 RVA: 0x0009A90C File Offset: 0x00098B0C
        public IEnumerator DrawEffect()
        {

            for (int i = 0; i < 2; i ++)
            {
                List<Skill> list = new List<Skill>();
                list.AddRange(this.BChar.MyTeam.Skills_Deck);
                if (list.Count != 0)
                {
                    list.Sort((Skill x1, Skill x2) => x2._AP.CompareTo(x1._AP));
                    yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyTeam._ForceDraw(list[0], null));
                }
                else
                {
                    yield return BattleSystem.instance.StartCoroutine(BattleSystem.instance.AllyTeam._Draw(1));

                }
            }
            yield break;
        }
    }
    public class Sex_Altr_11 : Skill_Extended
    {

    }
    public class Bcl_Altr_11_1 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.def = 20;
        }
    }
    public class Bcl_Altr_11_2 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusPerStat.Damage = -50;
            this.PlusStat.hit = -50;
        }
    }

    public class Sex_Altr_12 : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            return base.DescExtended(desc).Replace("&num1", ((int)(this.BChar.GetStat.atk * 0.5)).ToString()).Replace("&num2", ((int)this.BChar.GetStat.HIT_DEBUFF + 110).ToString());

        }
        public override bool Terms()
        {
            if (this.MySkill.FreeUse)
            {
                return true;
            }
            if (BattleSystem.instance != null)
            {
                if (Bcl_Altr_p_1.GetStack(this.BChar) >=5)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return base.Terms();
        }
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
            bool flag1 = Bcl_Altr_p_1.GetStack(this.BChar) >=12;
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
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            if(Bcl_Altr_p_1.GetStack(this.BChar) >= 12 && Targets.Count>0)
            {
                BattleSystem.DelayInputAfter(this.Delay(Targets[0]));
            }
        }
        public IEnumerator Delay(BattleChar target)
        {


                yield return new WaitForSeconds(0.15f);

                Skill skill = Skill.TempSkill("S_FGO_Altria_Alter_12_1", this.BChar);
                skill.PlusHit = true;
                skill.FreeUse = true;
            if (target.MyTeam.AliveChars.Count>0)
            {
                this.BChar.ParticleOut(this.MySkill, skill, target.MyTeam.AliveChars);

            }


        }
    }
    public class Sex_Altr_12_1 : Skill_Extended
    {

    }
    public class Bcl_Altr_12_1 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.def = -10;
        }
    }

    public static class Altria_Alter_Equip
    {
        public static bool GetEquip(BattleChar bc)
        {
            bool equip = false;
            try
            {
                foreach (IP_Equip_Altria_Alter ip_patch in bc.IReturn<IP_Equip_Altria_Alter>())
                {
                    //Debug.Log("point_add");
                    if (equip)
                    {
                        break;
                    }
                    bool flag = ip_patch != null;
                    if (flag)
                    {
                        bool flag2 = ip_patch.Equip_Altria_Alter(bc);
                        equip = equip || flag2;
                    }
                }
            }
            catch
            {
                //Debug.Log("error_Add");
            }
            return equip;
        }
    }
    public interface IP_Equip_Altria_Alter
    {
        // Token: 0x06000001 RID: 1
        bool Equip_Altria_Alter(BattleChar bchar);
    }
    public class E_Altr_0 : EquipBase, IP_Equip_Altria_Alter
    {
        // Token: 0x060051DF RID: 20959 RVA: 0x00204120 File Offset: 0x00202320
        public override void Init()
        {
            this.PlusPerStat.Damage = 30;
            this.PlusStat.IgnoreTaunt = true;
            base.Init();
        }
        public bool Equip_Altria_Alter(BattleChar bchar)
        {
            if(bchar==this.BChar)
            {
                return true;
            }
            return false;
        }
    }

    public class SkillEn_Altr_0 : Skill_Extended//, IP_SkillUse_User_After
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.IsDamage||MainSkill.IsHeal;
        }
        public override void FixedUpdate()
        {
            this.frame++;
            if(this.frame>=20 || this.frame<0)
            {
                this.frame=0;
                
                if (BattleSystem.instance.AllyList.Find((BattleAlly a) => a.Info.KeyData == "FGO_Altria_Alter"))
                {
                    BattleChar bc = BattleSystem.instance.AllyList.Find((BattleAlly a) => a.Info.KeyData == "FGO_Altria_Alter");
                    if(Bcl_Altr_p_1.GetStack(bc)>=5)
                    {
                        this.PlusSkillPerFinal.Damage = 50;
                        this.PlusSkillPerFinal.Heal = 50;
                        return;
                    }
                }
                this.PlusSkillPerFinal.Damage = 0;
                this.PlusSkillPerFinal.Heal = 0;
            }

        }
        public int frame = 0;

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            this.PlusSkillPerFinal.Damage = 0;
            this.PlusSkillPerFinal.Heal = 0;
            if (BattleSystem.instance.AllyList.Find((BattleAlly a) => a.Info.KeyData == "FGO_Altria_Alter"))
            {
                BattleChar bc = BattleSystem.instance.AllyList.Find((BattleAlly a) => a.Info.KeyData == "FGO_Altria_Alter");
                if (Bcl_Altr_p_1.GetStack(bc) >= 5)
                {
                    this.PlusSkillPerFinal.Damage = 50;
                    this.PlusSkillPerFinal.Heal = 50;
                    Bcl_Altr_p_1.StackChange(bc, -5);
                }
            }
            
        }

    }

    public class SkillEn_Altr_1 : Skill_Extended//, IP_SkillUse_User_After
    {
        public override bool CanSkillEnforce(Skill MainSkill)
        {
            return MainSkill.IsDamage || MainSkill.IsHeal;
        }
        public override void FixedUpdate()
        {
            this.frame++;
            if (this.frame >= 20 || this.frame < 0)
            {
                this.frame = 0;

                if (BattleSystem.instance.AllyList.Find((BattleAlly a) => a.Info.KeyData == "FGO_Altria_Alter"))
                {
                    BattleChar bc = BattleSystem.instance.AllyList.Find((BattleAlly a) => a.Info.KeyData == "FGO_Altria_Alter");
                    int num = (int)(Bcl_Altr_p_1.GetStack(bc) / 10);
                    this.PlusSkillPerStat.Damage = 30*num;
                    this.PlusSkillPerStat.Heal = 30*num;
                    return;
                }
                this.PlusSkillPerStat.Damage = 0;
                this.PlusSkillPerStat.Heal = 0;
            }

        }
        public int frame = 0;

        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            this.PlusSkillPerStat.Damage = 0;
            this.PlusSkillPerStat.Heal = 0;
            if (BattleSystem.instance.AllyList.Find((BattleAlly a) => a.Info.KeyData == "FGO_Altria_Alter"))
            {
                BattleChar bc = BattleSystem.instance.AllyList.Find((BattleAlly a) => a.Info.KeyData == "FGO_Altria_Alter");
                int num = (int)(Bcl_Altr_p_1.GetStack(bc) / 10);
                this.PlusSkillPerStat.Damage = 30 * num;
                this.PlusSkillPerStat.Heal = 30 * num;
            }

        }

    }
}
