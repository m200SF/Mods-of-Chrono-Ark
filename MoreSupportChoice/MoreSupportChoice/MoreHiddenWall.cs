using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TileTypes;

namespace MoreSupportChoice
{
    [HarmonyPatch(typeof(HexGenerator))]

    public static class GeneratorMapPlugin
    {
        [HarmonyPatch("GeneratorMap")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            //this line is 
            var methodInfo = AccessTools.Method(typeof(MapTest), nameof(MapTest.AddHideWall), new System.Type[] { typeof(HexMap).MakeByRefType() });

            var codes = instructions.ToList();
            for (int i = 0; i < codes.Count; i++)
            {
                /*//ignore this part
                if(i>=862 && i<=867)
                {
                    Debug.Log(i + ":" + codes[i].opcode+":"+ ((codes[i].operand!=null)?(codes[i].operand.GetType()):null));
                }
                */

                if (i > 5 && MapTest.Active_MoreHiddenWall)
                {
                    //this part is to find the position you want to insert new code
                    //by compare opcode and operand.Most of the time, comparing with opcode is enough.
                    bool f1 = true;
                    f1 = f1 && codes[i - 5].opcode == OpCodes.Ldloc_S;
                    try
                    {
                        f1 = f1 && (codes[i - 5].operand is LocalBuilder && ((LocalBuilder)(codes[i - 5].operand)).LocalIndex == 13);
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
                        yield return new CodeInstruction(OpCodes.Ldloca_S, 0);//new code
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

        public static bool Active_MoreHiddenWall = false;
    }

    
}
