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
using System.Reflection.Emit;
using System.Runtime.CompilerServices;



using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

using TMPro;
using TileTypes;
using Steamworks;
using System.Reflection;




namespace MoreSupportChoice
{
    [PluginConfig("M200_MoreSupportChoice_CharMod", "MoreSupportChoice_CharMod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class MoreSupportChoice_CharacterModPlugin : ChronoArkPlugin
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
        public class MoreSupportChoiceDef : ModDefinition
        {

        }

    }


    [HarmonyPatch(typeof(CharSelect_CampUI))]
    public class CharSelect_CampUI_Plugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("Init")]

        [HarmonyTranspiler]//ldarg.0 >> this
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var methodInfo = AccessTools.Method(typeof(Test1), nameof(Test1.Check1), new System.Type[] { typeof(CharSelect_CampUI), typeof(GDECharacterData) });
            var codes = instructions.ToList();

            for (int i = 0; i < codes.Count; i++)
            {
                if (i > 6)
                {
                    /*
                    bool f1 = codes[i - 4].opcode == OpCodes.Ldarg_0;
                    bool f2 = codes[i - 3].opcode == OpCodes.Ldfld;
                    bool f3 = codes[i - 2].opcode == OpCodes.Ldsfld;
                    bool f4 = codes[i - 1].opcode == OpCodes.Call;
                    bool f5 = codes[i].opcode == OpCodes.Stloc_S;
                    */
                    bool f1 = codes[i - 6].opcode == OpCodes.Ldarg_0;
                    bool f2 = codes[i - 5].opcode == OpCodes.Ldfld;
                    bool f3 = codes[i - 4].opcode == OpCodes.Ldarg_0;
                    bool f4 = codes[i - 3].opcode == OpCodes.Ldfld;
                    bool f5 = codes[i - 2].opcode == OpCodes.Call;
                    bool f6 = codes[i - 1].opcode == OpCodes.Callvirt;
                    bool f7 = codes[i].opcode == OpCodes.Stloc_S;

                    if (f1 && f2 && f3 && f4 && f5 && f6 && f7)
                    {
                        yield return codes[i];

                        yield return new CodeInstruction(OpCodes.Ldarg_0);
                        yield return new CodeInstruction(OpCodes.Ldloc_S, 13);
                        yield return new CodeInstruction(OpCodes.Call, methodInfo);
                        //yield return codes[i];
                        yield return new CodeInstruction(OpCodes.Stloc_S, 13);

                    }
                    else
                    {
                        yield return codes[i];

                    }

                }
                else
                {
                    yield return codes[i];
                }

            }
            /*
            for(int i=0;i<codes.Count;i++) 
            {
                if(i+4<codes.Count)
                {
                    bool f1 = codes[i].opcode == OpCodes.Ldarg_0;
                    bool f2 = codes[i + 1].opcode == OpCodes.Ldfld;
                    bool f3 = codes[i + 2].opcode == OpCodes.Ldsfld;
                    bool f4 = codes[i + 3].opcode == OpCodes.Call;
                    bool f5 = codes[i + 4].opcode == OpCodes.Stloc_S;
                    if (f1 && f2 && f3 && f4 && f5)
                    {
                        yield return codes[i];
                        yield return new CodeInstruction(OpCodes.Call, methodInfo);
                        i = i + 5;
                    }
                    else
                    {
                        yield return codes[i];
                    }
                }
                else
                {
                    yield return codes[i];
                }

            }
            */
        }

    }


    public static class Test1
    {
        public static GDECharacterData Check1(CharSelect_CampUI CSC,GDECharacterData gDECharacterData)
        {
            //GDECharacterData test = gDECharacterData;
                //test = Test1.Check1(CSC, gDECharacterData);


            //Debug.Log("Check1_1");
            string cha1= ModManager.getModInfo("camp_specific").GetSetting<InputFieldSetting>("cha1").Value;
            string cha2 = ModManager.getModInfo("camp_specific").GetSetting<InputFieldSetting>("cha2").Value;
            List<string> chas= new List<string>();
            if (!cha1.IsNullOrEmpty() && PlayData.TSavedata.Party.Find(a => a.GetData.name == cha1) == null && CSC.CharList.Find(a => a.data.name == cha1) == null && CSC.CharDatas.Find(a => a.name == cha1) != null) 
            {
                chas.Add(cha1);
            }
            if (!cha2.IsNullOrEmpty() && PlayData.TSavedata.Party.Find(a => a.GetData.name == cha2) == null && CSC.CharList.Find(a => a.data.name == cha2) == null && CSC.CharDatas.Find(a => a.name == cha2) != null)
            {
                chas.Add(cha2);
            }
            if(chas.Count > 0) 
            {
                GDECharacterData gde = CSC.CharDatas.Find(a => a.name == chas[0]);
                if (gde != null)
                {
                    Debug.Log("选人:" + gde.Key);
                    return gde;
                }
                else
                {
                    return gDECharacterData;
                }
            }
            else
            {
                return gDECharacterData;
            }

            
        }

    }
    /*
    public class Test2
    {
        public GDECharacterData Check3(Test2 CSC, GDECharacterData gDECharacterData)
        {
            GDECharacterData test1 = new GDECharacterData("test");
            test1 = Test1.Check2(this, test1);
            return new GDECharacterData("test2");
        }
    }
    */


    /*
    [HarmonyPatch(typeof(HexGenerator))]

    public static class GeneratorMapPlugin
    {
        [HarmonyPatch("GeneratorMap")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            //this line is 
            var methodInfo = AccessTools.Method(typeof(MapTest), nameof(MapTest.AddHideWall), new System.Type[] { typeof( HexMap).MakeByRefType() });

            var codes = instructions.ToList();
            for (int i = 0; i < codes.Count; i++)
            {
                //ignore this part
                if(i>=862 && i<=867)
                {
                    Debug.Log(i + ":" + codes[i].opcode+":"+ ((codes[i].operand!=null)?(codes[i].operand.GetType()):null));
                }
                

                if (i > 5)
                {
                    //this part is to find the position you want to insert new code
                    //by compare opcode and operand.Most of the time, comparing with opcode is enough.
                    bool f1 = true;
                    f1 = f1 && codes[i - 5].opcode == OpCodes.Ldloc_S;
                    try
                    {
                        f1 = f1 && (codes[i - 5].operand is LocalBuilder && ((LocalBuilder)(codes[i - 5].operand)).LocalIndex==13);
                    }
                    catch { f1 = false; }

                    f1 = f1 && codes[i - 4].opcode == OpCodes.Ldloca_S;
                    f1 = f1 && codes[i - 3].opcode == OpCodes.Ldloc_S;
                    try
                    {
                        f1 = f1 && (codes[i - 3].operand is LocalBuilder && ((LocalBuilder)(codes[i - 3].operand)).LocalIndex == 13);
                    }
                    catch { f1 = false; }

                    f1 = f1 && codes[i - 2].opcode == OpCodes.Ldloc_0;

                    f1 = f1 && codes[i - 1].opcode == OpCodes.Callvirt;
                    f1 = f1 && codes[i - 0].opcode == OpCodes.Callvirt;
                    //---------------------------
                    //this part is to insert new code
                    if (f1)
                    {
                        yield return codes[i];//The original code
                        yield return new CodeInstruction(OpCodes.Ldloca_S,0);//new code
                        yield return new CodeInstruction(OpCodes.Call, methodInfo);//new code
                        continue;
                    }
                    //----------------------------
                }
                yield return codes[i];//The original code
            }
        }

    }
    public static class MapTest
    {
        public static void AddHideWall(ref HexMap map)//this is the function I insert to the original code
        {
            bool flag = true;//Change this paragraph to the conditions that need to be met
            {
                HiddenWall wall = new HiddenWall();
                if (PlayData.TSavedata.StageNum != 0)
                {
                    wall.Add(ref map, wall.PosSet(map));
                }
            }

        }
    }
    */



    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("BuffAdd")]
    public static class BuffAddPlugin
    {
        [HarmonyPrefix]
        public static void BuffAdd_Before(BattleChar __instance, string key, BattleChar UseState, bool hide, int PlusTagPer, bool debuffnonuser, int RemainTime, bool StringHide)
        {
            //Debug.Log("BuffAdd检查点1");
            foreach (IP_BuffAdd_Before ip_patch in __instance.IReturn<IP_BuffAdd_Before>())
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    //Debug.Log("BuffAdd检查点2");
                    ip_patch.BuffAdd_Before(__instance, key, UseState, hide, PlusTagPer, debuffnonuser, RemainTime, StringHide);
                }
            }

            //Debug.Log("BuffAdd检查点3");
        }

    }


    public interface IP_BuffAdd_Before
    {
        // Token: 0x06000001 RID: 1
        void BuffAdd_Before(BattleChar BC, string key, BattleChar UseState, bool hide, int PlusTagPer, bool debuffnonuser, int RemainTime, bool StringHide);
    }


    
}
