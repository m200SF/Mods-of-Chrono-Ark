
using HarmonyLib;
using SF_Dreamer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using System.Reflection.Emit;
using System.Runtime.InteropServices;
using UnityEngine;


namespace SF_Dreamer
{


    [HarmonyPriority(-1)]

    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("Effect")]
    public static class BattleChar_Effect_Plugin
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var methodInfo_1 = AccessTools.Method(typeof(DamageChange_Check), nameof(DamageChange_Check.Add_DC_rec), new System.Type[] { typeof(SkillParticle), typeof(BattleChar), typeof(BattleChar), typeof(float) });
            var methodInfo_2 = AccessTools.Method(typeof(DamageChange_Check), nameof(DamageChange_Check.Check_DC_rec), new System.Type[] { typeof(SkillParticle), typeof(float) });

            var methodInfo_3 = AccessTools.Method(typeof(DamageChange_Sum_Check), nameof(DamageChange_Sum_Check.Check_DC_S_rec), new System.Type[] { typeof(SkillParticle), typeof(BattleChar), typeof(BattleChar), typeof(int) });
            var methodInfo_4 = AccessTools.Method(typeof(DamageChange_HitSum_Check), nameof(DamageChange_HitSum_Check.Check_DC_HS_rec), new System.Type[] { typeof(SkillParticle), typeof(BattleChar), typeof(BattleChar), typeof(int) });

            var methodInfo_5 = AccessTools.Method(typeof(ChangeDamageState_Check), nameof(ChangeDamageState_Check.Check_CDS_rec), new System.Type[] { typeof(SkillParticle), typeof(BattleChar), typeof(BattleChar), typeof(bool).MakeByRefType(), typeof(bool).MakeByRefType() });


            var codes = instructions.ToList();
            for (int i = 0; i < codes.Count; i++)
            {
                //if(i>=191 && i<=200){Debug.Log(i+": " + codes[i].opcode);}

                //if(i==228){
                //Debug.Log("--------------------------");
                //Debug.Log(codes[i + 8].operand is MethodInfo);
                //Debug.Log(((MethodInfo)codes[i + 8].operand).Name);}
                bool f1 = true;
                if (i < codes.Count - 11)
                {
                    f1 = f1 && codes[i + 0].opcode == OpCodes.Ldloc_S;
                    f1 = f1 && codes[i + 1].opcode == OpCodes.Ldarg_1;
                    f1 = f1 && codes[i + 2].opcode == OpCodes.Ldfld;
                    f1 = f1 && codes[i + 3].opcode == OpCodes.Ldarg_0;
                    f1 = f1 && codes[i + 4].opcode == OpCodes.Ldloc_2;
                    f1 = f1 && codes[i + 5].opcode == OpCodes.Conv_I4;
                    f1 = f1 && codes[i + 6].opcode == OpCodes.Ldloca_S;
                    f1 = f1 && codes[i + 7].opcode == OpCodes.Ldc_I4_0;
                    f1 = f1 && codes[i + 8].opcode == OpCodes.Callvirt;
                    f1 = f1 && codes[i + 8].operand is MethodInfo && ((MethodInfo)codes[i + 8].operand).Name == "DamageChange";
                    f1 = f1 && codes[i + 9].opcode == OpCodes.Conv_R4;
                    f1 = f1 && codes[i + 10].opcode == OpCodes.Stloc_2;
                }
                else
                {
                    f1 = false;
                }

                bool f2 = true;
                if (i > 10)
                {
                    f2 = f2 && codes[i - 9].opcode == OpCodes.Ldloc_S;
                    f2 = f2 && codes[i - 8].opcode == OpCodes.Ldarg_1;
                    f2 = f2 && codes[i - 7].opcode == OpCodes.Ldfld;
                    f2 = f2 && codes[i - 6].opcode == OpCodes.Ldarg_0;
                    f2 = f2 && codes[i - 5].opcode == OpCodes.Ldloc_2;
                    f2 = f2 && codes[i - 4].opcode == OpCodes.Conv_I4;
                    f2 = f2 && codes[i - 3].opcode == OpCodes.Ldloca_S;
                    f2 = f2 && codes[i - 2].opcode == OpCodes.Ldc_I4_0;
                    f2 = f2 && codes[i - 1].opcode == OpCodes.Ldloca_S;
                    f2 = f2 && codes[i - 0].opcode == OpCodes.Callvirt;
                    f2 = f2 && codes[i - 0].operand is MethodInfo && ((MethodInfo)codes[i - 0].operand).Name == "DamageChange_sumoperation";
                }
                else
                {
                    f2 = false;
                }

                bool f3 = true;
                if (i > 10)
                {
                    f3 = f3 && codes[i - 8].opcode == OpCodes.Ldloc_S;
                    f3 = f3 && codes[i - 7].opcode == OpCodes.Ldarg_1;
                    f3 = f3 && codes[i - 6].opcode == OpCodes.Ldfld;
                    f3 = f3 && codes[i - 5].opcode == OpCodes.Ldloc_2;
                    f3 = f3 && codes[i - 4].opcode == OpCodes.Conv_I4;
                    f3 = f3 && codes[i - 3].opcode == OpCodes.Ldloca_S;
                    f3 = f3 && codes[i - 2].opcode == OpCodes.Ldc_I4_0;
                    f3 = f3 && codes[i - 1].opcode == OpCodes.Ldloca_S;
                    f3 = f3 && codes[i - 0].opcode == OpCodes.Callvirt;
                    f3 = f3 && codes[i - 0].operand is MethodInfo && ((MethodInfo)codes[i - 0].operand).Name == "DamageChange_Hit_sumoperation";
                }
                else
                {
                    f3 = false;
                }

                bool f4 = true;
                if (i > 10)
                {
                    f4 = f4 && codes[i - 8].opcode == OpCodes.Ldloc_S;
                    f4 = f4 && codes[i - 7].opcode == OpCodes.Ldarg_1;
                    f4 = f4 && codes[i - 6].opcode == OpCodes.Ldarg_0;
                    f4 = f4 && codes[i - 5].opcode == OpCodes.Ldloc_2;
                    f4 = f4 && codes[i - 4].opcode == OpCodes.Conv_I4;
                    f4 = f4 && codes[i - 3].opcode == OpCodes.Ldloc_3;
                    f4 = f4 && codes[i - 2].opcode == OpCodes.Ldloca_S;
                    f4 = f4 && codes[i - 1].opcode == OpCodes.Ldloca_S;
                    f4 = f4 && codes[i - 0].opcode == OpCodes.Callvirt;
                    f4 = f4 && codes[i - 0].operand is MethodInfo && ((MethodInfo)codes[i - 0].operand).Name == "ChangeDamageState";
                }
                else
                {
                    f4 = false;
                }


                if (f1)
                {
                    
                    //Debug.Log("检测到位置DC");
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(SkillParticle), "UseStatus"));//--------------------
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldloc_2);
                    yield return new CodeInstruction(OpCodes.Call, methodInfo_1);
                    
                    //Debug.Log("检测到位置DC2");
                    yield return codes[i];
                    yield return codes[i + 1];
                    yield return codes[i + 2];
                    yield return codes[i + 3];
                    yield return codes[i + 4];
                    yield return codes[i + 5];
                    yield return codes[i + 6];
                    yield return codes[i + 7];
                    yield return codes[i + 8];
                    yield return codes[i + 9];
                    yield return codes[i + 10];
                    i = i + 10;
                    //Debug.Log("检测到位置DC3");
                    
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Ldloc_2);
                    yield return new CodeInstruction(OpCodes.Call, methodInfo_2);
                    yield return new CodeInstruction(OpCodes.Stloc_2);
                    //Debug.Log("检测到位置DC4");
                    

                    /*
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(SKillCollection), "CharKeys"));
                    //yield return new CodeInstruction(OpCodes.Call, methodInfo);

                    yield return new CodeInstruction(OpCodes.Pop);
                    */
                }
                else if(f2)
                {
                    yield return codes[i];

                    //Debug.Log("检测到位置DC_S");
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(SkillParticle), "UseStatus"));//--------------------
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldloc_S,24);
                    yield return new CodeInstruction(OpCodes.Call, methodInfo_3);
                    yield return new CodeInstruction(OpCodes.Stloc_S,24);
                }
                else if (f3)
                {
                    yield return codes[i];

                    //Debug.Log("检测到位置DC_HS");
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(SkillParticle), "UseStatus"));//--------------------
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldloc_S, 21);
                    yield return new CodeInstruction(OpCodes.Call, methodInfo_4);
                    yield return new CodeInstruction(OpCodes.Stloc_S, 21);
                }
                else if(f4)
                {
                    //Debug.Log("检测到位置f4:"+i);
                    yield return codes[i];

                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(SkillParticle), "UseStatus"));//--------------------
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldloca_S,6);
                    yield return new CodeInstruction(OpCodes.Ldloca_S, 7);
                    yield return new CodeInstruction(OpCodes.Call, methodInfo_5);
                }
                else
                {
                    yield return codes[i];
                }




            }
        }








        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPrefix]
        public static void BattleChar_Effect_Before_patch(BattleChar __instance, SkillParticle SP)
        {
            if(Bcl_drm_4.Check_B4_Peneration(__instance,SP))
            {
                DamageCheck_rec rec= new DamageCheck_rec();
                rec.sp = SP;
                rec.hit = __instance;
                rec.effect_frame = Time.frameCount;
                BattleChar_Effect_Plugin.Check_list.Add(rec);
            }
        }

        [HarmonyPostfix]
        public static void BattleChar_Effect_After_patch(BattleChar __instance, SkillParticle SP)
        {
            BattleChar_Effect_Plugin.Check_list.RemoveAll(rec => rec.effect_frame != Time.frameCount);
            BattleChar_Effect_Plugin.Check_list.RemoveAll(rec => rec.sp == SP);
        }
        //private int recBefore;


        public static List<DamageCheck_rec> Check_list=new List<DamageCheck_rec>();
        
    }
    public class DamageCheck_rec
    {
        public static bool Active(SkillParticle SP=null, BattleChar user = null, BattleChar hit=null)
        {
            if(DamageCheck_rec.activeLevel_1)
            {
                bool ans = true;
                if (SP != null)
                {
                    ans = ans && (BattleChar_Effect_Plugin.Check_list.Find(a => a.sp == SP) != null);
                }
                if (user != null)
                {
                    ans = ans && (BattleChar_Effect_Plugin.Check_list.Find(a => a.sp.UseStatus == user) != null);
                }
                if (hit != null)
                {
                    ans = ans && (BattleChar_Effect_Plugin.Check_list.Find(a => a.hit == hit) != null);
                }

                ans = ans && (BattleChar_Effect_Plugin.Check_list.Find(a => a.effect_frame == Time.frameCount) != null);

                return ans;
            }
            else
            {
                return false;
            }
        }
        public SkillParticle sp=new SkillParticle();
        public BattleChar hit=new BattleChar();
        public int effect_frame = 0;

        public static bool activeLevel_1 = true;
        public static bool activeLevel_2 = true;
    }


    public static class ChangeDamageState_Check
    {

        public static void Check_CDS_rec(SkillParticle mark, BattleChar user, BattleChar bc, ref bool toHeal,ref bool toPain)
        {
            if (DamageCheck_rec.activeLevel_2 && DamageCheck_rec.Active(mark, user, bc))
            {
                //Debug.Log("DC_S触发");
                toHeal = false;
                toPain = false;
            }
        }
    }

    public static class DamageChange_HitSum_Check
    {

        public static int Check_DC_HS_rec(SkillParticle mark, BattleChar user, BattleChar bc, int num)
        {
            if (DamageCheck_rec.Active(mark, user, bc))
            {
              //Debug.Log("DC_HS触发");
                return Math.Max(0, num);
            }
            return num;
        }
    }
    public static class DamageChange_Sum_Check
    {

        public static int Check_DC_S_rec(SkillParticle mark, BattleChar user, BattleChar bc, int num)
        {
            if (DamageCheck_rec.Active(mark, user, bc))
            {
              //Debug.Log("DC_S触发");
                return Math.Max(0, num);
            }
            return num;
        }
    }


    public static class DamageChange_Check
    {
        public static void Add_DC_rec(SkillParticle mark, BattleChar user, BattleChar hit, float dmg)
        {
            if (DamageCheck_rec.Active(mark, user, hit))
            {
                DC_rec rec = new DC_rec();
                rec.markObject = mark;
                rec.frame = Time.frameCount;
                rec.dmg_old = dmg;
                DamageChange_Check.DC_rec_list.Add(rec);
            }
        }
        public static float Check_DC_rec(SkillParticle mark, float dmg)
        {
            if (DamageCheck_rec.Active())
            {
                DC_rec rec = DamageChange_Check.DC_rec_list.Find(a => a.markObject == mark);
                if (rec != null)
                {
                  //Debug.Log("DC触发");
                    float num = rec.dmg_old;
                    DamageChange_Check.Clear_DC_rec(rec.markObject);
                    return Math.Max(num, dmg);
                }

            }
            return dmg;
        }
        public static void Clear_DC_rec(SkillParticle mark = null)
        {
            if (mark != null)
            {
                DamageChange_Check.DC_rec_list.RemoveAll(a => a.markObject == mark);
            }
            DamageChange_Check.DC_rec_list.RemoveAll(a => a.frame != Time.frameCount);
        }
        public static List<DC_rec> DC_rec_list = new List<DC_rec>();
    }
    public class DC_rec
    {
        public SkillParticle markObject = null;
        public int frame = 0;
        public float dmg_old = 0;
    }










    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("Damage")]
    public static class BattleChar_Damage_Plugin
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var methodInfo_1 = AccessTools.Method(typeof(DamageTakeChange_Check), nameof(DamageTakeChange_Check.Add_DTC_rec), new System.Type[] { typeof(GameObject),typeof(BattleChar), typeof(BattleChar), typeof(int) });
            var methodInfo_2 = AccessTools.Method(typeof(DamageTakeChange_Check), nameof(DamageTakeChange_Check.Check_DTC_rec), new System.Type[] { typeof(GameObject), typeof(int) });

            var methodInfo_3 = AccessTools.Method(typeof(DamageTakeChange_Sum_Check), nameof(DamageTakeChange_Sum_Check.Check_DTC_S_rec), new System.Type[] { typeof(GameObject), typeof(BattleChar), typeof(BattleChar), typeof(int) });

            var methodInfo_4 = AccessTools.Method(typeof(CriDamageChange_Check), nameof(CriDamageChange_Check.Add_CriD_rec), new System.Type[] { typeof(GameObject), typeof(BattleChar), typeof(BattleChar), typeof(int) });
            var methodInfo_5 = AccessTools.Method(typeof(CriDamageChange_Check), nameof(CriDamageChange_Check.Check_CriD_rec), new System.Type[] { typeof(GameObject), typeof(int) });

            var methodInfo_6 = AccessTools.Method(typeof(DamageTake_Check), nameof(DamageTake_Check.Check_DT_rec), new System.Type[] { typeof(GameObject), typeof(BattleChar), typeof(BattleChar), typeof(bool).MakeByRefType() });


            var codes = instructions.ToList();
            for (int i = 0; i < codes.Count; i++)
            {
                //if(i>=191 && i<=200){Debug.Log(i+": " + codes[i].opcode);}

                //if(i==228){
                //Debug.Log("--------------------------");
                //Debug.Log(codes[i + 8].operand is MethodInfo);
                //Debug.Log(((MethodInfo)codes[i + 8].operand).Name);}

                if ( i<codes.Count-10) 
                {
               
                    bool f1 = true;
                    if (i < codes.Count - 10)
                    {
                        f1 = f1 && codes[i + 0].opcode == OpCodes.Ldloc_S;
                        f1 = f1 && codes[i + 1].opcode == OpCodes.Ldarg_0;
                        f1 = f1 && codes[i + 2].opcode == OpCodes.Ldarg_1;
                        f1 = f1 && codes[i + 3].opcode == OpCodes.Ldarg_2;
                        f1 = f1 && codes[i + 4].opcode == OpCodes.Ldarg_3;
                        f1 = f1 && codes[i + 5].opcode == OpCodes.Ldarg_S;
                        f1 = f1 && codes[i + 6].opcode == OpCodes.Ldarg_S;
                        f1 = f1 && codes[i + 7].opcode == OpCodes.Ldc_I4_0;
                        f1 = f1 && codes[i + 8].opcode == OpCodes.Callvirt;
                        f1 = f1 && codes[i + 8].operand is MethodInfo && ((MethodInfo)codes[i + 8].operand).Name == "DamageTakeChange";
                        f1 = f1 && codes[i + 9].opcode == OpCodes.Starg_S;
                    }
                    else
                    {
                        f1 = false;
                    }

                    bool f2 = true;
                    if (i > 10)
                    {
                        f2 = f2 && codes[i - 9].opcode == OpCodes.Ldloc_S;
                        f2 = f2 && codes[i - 8].opcode == OpCodes.Ldarg_0;
                        f2 = f2 && codes[i - 7].opcode == OpCodes.Ldarg_1;
                        f2 = f2 && codes[i - 6].opcode == OpCodes.Ldarg_2;
                        f2 = f2 && codes[i - 5].opcode == OpCodes.Ldarg_3;
                        f2 = f2 && codes[i - 4].opcode == OpCodes.Ldloca_S;
                        f2 = f2 && codes[i - 3].opcode == OpCodes.Ldarg_S;
                        f2 = f2 && codes[i - 2].opcode == OpCodes.Ldarg_S;
                        f2 = f2 && codes[i - 1].opcode == OpCodes.Ldc_I4_0;
                        f2 = f2 && codes[i - 0].opcode == OpCodes.Callvirt;
                        f2 = f2 && codes[i - 0].operand is MethodInfo && ((MethodInfo)codes[i - 0].operand).Name == "DamageTakeChange_sumoperation";
                    }
                    else
                    {
                        f2 = false;
                    }

                    bool f3 = true;
                    if (i < codes.Count - 11)
                    {
                        f3 = f3 && codes[i + 0].opcode == OpCodes.Ldarg_2;
                        f3 = f3 && codes[i + 1].opcode == OpCodes.Ldarg_2;
                        f3 = f3 && codes[i + 2].opcode == OpCodes.Conv_R4;
                        f3 = f3 && codes[i + 3].opcode == OpCodes.Ldarg_0;
                        f3 = f3 && codes[i + 4].opcode == OpCodes.Call;
                        f3 = f3 && codes[i + 5].opcode == OpCodes.Ldfld;
                        f3 = f3 && codes[i + 5].operand is FieldInfo && ((FieldInfo)codes[i + 5].operand).Name == "DMGTaken";
                        f3 = f3 && codes[i + 6].opcode == OpCodes.Conv_I4;
                        f3 = f3 && codes[i + 7].opcode == OpCodes.Conv_R4;
                        f3 = f3 && codes[i + 8].opcode == OpCodes.Call;
                        //f3 = f3 && codes[i + 8].operand is MethodInfo && ((MethodInfo)codes[i + 8].operand).Name == "DamageTakeChange";
                        f3 = f3 && codes[i + 9].opcode == OpCodes.Conv_I4;
                        f3 = f3 && codes[i + 10].opcode == OpCodes.Add;
                        f3 = f3 && codes[i + 11].opcode == OpCodes.Starg_S;
                    }
                    else
                    {
                        f3 = false;
                    }

                    bool f4 = true;
                    if (i < codes.Count - 16)
                    {
                        f4 = f4 && codes[i + 0].opcode == OpCodes.Ldarg_2;
                        f4 = f4 && codes[i + 1].opcode == OpCodes.Conv_R4;
                        f4 = f4 && codes[i + 2].opcode == OpCodes.Ldc_R4;
                        f4 = f4 && codes[i + 3].opcode == OpCodes.Ldarg_1;
                        f4 = f4 && codes[i + 4].opcode == OpCodes.Callvirt;
                        f4 = f4 && codes[i + 5].opcode == OpCodes.Ldfld;
                        f4 = f4 && codes[i + 5].operand is FieldInfo && ((FieldInfo)codes[i + 5].operand).Name == "PlusCriDmg";
                        f4 = f4 && codes[i + 6].opcode == OpCodes.Ldarg_0;
                        f4 = f4 && codes[i + 7].opcode == OpCodes.Call;
                        f4 = f4 && codes[i + 8].opcode == OpCodes.Ldfld;
                        f4 = f4 && codes[i + 8].operand is FieldInfo && ((FieldInfo)codes[i + 8].operand).Name == "CRIGetDMG";
                        f4 = f4 && codes[i + 9].opcode == OpCodes.Conv_R4;
                        f4 = f4 && codes[i + 10].opcode == OpCodes.Add;
                        f4 = f4 && codes[i + 11].opcode == OpCodes.Ldc_R4;
                        f4 = f4 && codes[i + 12].opcode == OpCodes.Mul;
                        f4 = f4 && codes[i + 13].opcode == OpCodes.Add;
                        f4 = f4 && codes[i + 14].opcode == OpCodes.Mul;
                        f4 = f4 && codes[i + 15].opcode == OpCodes.Conv_I4;
                        f4 = f4 && codes[i + 16].opcode == OpCodes.Starg_S;
                    }
                    else
                    {
                        f4 = false;
                    }

                    bool f5 = true;
                    if (i > 10)
                    {
                        f5 = f5 && codes[i - 8].opcode == OpCodes.Ldloc_S;
                        f5 = f5 && codes[i - 7].opcode == OpCodes.Ldarg_1;
                        f5 = f5 && codes[i - 6].opcode == OpCodes.Ldarg_2;
                        f5 = f5 && codes[i - 5].opcode == OpCodes.Ldarg_3;
                        f5 = f5 && codes[i - 4].opcode == OpCodes.Ldloca_S;
                        f5 = f5 && codes[i - 3].opcode == OpCodes.Ldc_I4_0;
                        f5 = f5 && codes[i - 2].opcode == OpCodes.Ldc_I4_0;
                        f5 = f5 && codes[i - 1].opcode == OpCodes.Ldarg_0;
                        f5 = f5 && codes[i - 0].opcode == OpCodes.Callvirt;
                        f5 = f5 && codes[i - 0].operand is MethodInfo && ((MethodInfo)codes[i - 0].operand).Name == "DamageTake";
                    }
                    else
                    {
                        f5 = false;
                    }


                    if (f1)
                    {
                        //Debug.Log("检测到位置f1");
                        yield return new CodeInstruction(OpCodes.Ldloc_0);
                        yield return new CodeInstruction(OpCodes.Ldarg_1);
                        yield return new CodeInstruction(OpCodes.Ldarg_0);
                        yield return new CodeInstruction(OpCodes.Ldarg_2);
                        yield return new CodeInstruction(OpCodes.Call, methodInfo_1);

                        yield return codes[i];
                        yield return codes[i + 1];
                        yield return codes[i + 2];
                        yield return codes[i + 3];
                        yield return codes[i + 4];
                        yield return codes[i + 5];
                        yield return codes[i + 6];
                        yield return codes[i + 7];
                        yield return codes[i + 8];
                        yield return codes[i + 9];
                        i = i + 9;

                        yield return new CodeInstruction(OpCodes.Ldloc_0);
                        yield return new CodeInstruction(OpCodes.Ldarg_2);
                        yield return new CodeInstruction(OpCodes.Call, methodInfo_2);
                        yield return new CodeInstruction(OpCodes.Starg_S,2);
                        /*
                        yield return new CodeInstruction(OpCodes.Ldarg_0);
                        yield return new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(SKillCollection), "CharKeys"));
                        //yield return new CodeInstruction(OpCodes.Call, methodInfo);

                        yield return new CodeInstruction(OpCodes.Pop);
                        */
                    }
                    else if(f2)
                    {
                        //Debug.Log("检测到位置f2");
                        yield return codes[i];
                        
                        yield return new CodeInstruction(OpCodes.Ldloc_0);
                        yield return new CodeInstruction(OpCodes.Ldarg_1);
                        yield return new CodeInstruction(OpCodes.Ldarg_0);
                        yield return new CodeInstruction(OpCodes.Ldloc_S,8);
                        yield return new CodeInstruction(OpCodes.Call, methodInfo_3);
                        yield return new CodeInstruction(OpCodes.Stloc_S, 8);
                        
                    }
                    else if (f3)
                    {
                        //Debug.Log("检测到位置dmgT");
                        yield return new CodeInstruction(OpCodes.Ldloc_0);
                        yield return new CodeInstruction(OpCodes.Ldarg_1);
                        yield return new CodeInstruction(OpCodes.Ldarg_0);
                        yield return new CodeInstruction(OpCodes.Ldarg_2);
                        yield return new CodeInstruction(OpCodes.Call, methodInfo_1);

                        yield return codes[i];
                        yield return codes[i + 1];
                        yield return codes[i + 2];
                        yield return codes[i + 3];
                        yield return codes[i + 4];
                        yield return codes[i + 5];
                        yield return codes[i + 6];
                        yield return codes[i + 7];
                        yield return codes[i + 8];
                        yield return codes[i + 9];
                        yield return codes[i + 10];
                        yield return codes[i + 11];
                        i = i + 11;

                        yield return new CodeInstruction(OpCodes.Ldloc_0);
                        yield return new CodeInstruction(OpCodes.Ldarg_2);
                        yield return new CodeInstruction(OpCodes.Call, methodInfo_2);
                        yield return new CodeInstruction(OpCodes.Starg_S, 2);
                    }
                    else if (f4)
                    {
                        //Debug.Log("检测到位置dmgT");
                        yield return new CodeInstruction(OpCodes.Ldloc_0);
                        yield return new CodeInstruction(OpCodes.Ldarg_1);
                        yield return new CodeInstruction(OpCodes.Ldarg_0);
                        yield return new CodeInstruction(OpCodes.Ldarg_2);
                        yield return new CodeInstruction(OpCodes.Call, methodInfo_4);

                        yield return codes[i];
                        yield return codes[i + 1];
                        yield return codes[i + 2];
                        yield return codes[i + 3];
                        yield return codes[i + 4];
                        yield return codes[i + 5];
                        yield return codes[i + 6];
                        yield return codes[i + 7];
                        yield return codes[i + 8];
                        yield return codes[i + 9];
                        yield return codes[i + 10];
                        yield return codes[i + 11];
                        yield return codes[i + 12];
                        yield return codes[i + 13];
                        yield return codes[i + 14];
                        yield return codes[i + 15];
                        yield return codes[i + 16];
                        i = i + 16;

                        yield return new CodeInstruction(OpCodes.Ldloc_0);
                        yield return new CodeInstruction(OpCodes.Ldarg_2);
                        yield return new CodeInstruction(OpCodes.Call, methodInfo_5);
                        yield return new CodeInstruction(OpCodes.Starg_S, 2);
                    }
                    else if(f5)
                    {
                        //Debug.Log("检测到位置f5:"+i);
                        yield return codes[i];

                        yield return new CodeInstruction(OpCodes.Ldloc_0);
                        yield return new CodeInstruction(OpCodes.Ldarg_1);
                        yield return new CodeInstruction(OpCodes.Ldarg_0);
                        //yield return codes[i-4];
                        yield return new CodeInstruction(OpCodes.Ldloca_S, 1);
                        yield return new CodeInstruction(OpCodes.Call, methodInfo_6);
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
        }

    }


    public static class DamageTake_Check
    {
        public static void Check_DT_rec(GameObject mark, BattleChar user, BattleChar bc, ref bool flag)
        {
            //Debug.Log("DT触发1"+ user.Info.Name+"---"+bc.Info.Name);
            //Debug.Log("DT触发2" + DamageCheck_rec.Active(null, user, bc));
            if (DamageCheck_rec.activeLevel_2 && DamageCheck_rec.Active(null, user, bc))
            {
                //Debug.Log("DT触发3");
                flag = false;
            }
        }
    }

    public static class DamageTakeChange_Sum_Check
    {

        public static int Check_DTC_S_rec(GameObject mark,BattleChar user,BattleChar bc, int num)
        {
            if (DamageCheck_rec.Active(null, user,bc))
            {
              //Debug.Log("DTC_S触发");
                return Math.Max(0, num);
            }
            return num;
        }
    }


    public static class DamageTakeChange_Check
    {
        public static void Add_DTC_rec(GameObject mark, BattleChar user, BattleChar hit, int dmg)
        {
            if(DamageCheck_rec.Active(null,user,hit))
            {
                DTC_rec rec = new DTC_rec();
                rec.markObject = mark;
                rec.frame = Time.frameCount;
                rec.dmg_old = dmg;
                DamageTakeChange_Check.DTC_rec_list.Add(rec);
            }
        }
        public static int Check_DTC_rec(GameObject mark, int dmg)
        {
            if (DamageCheck_rec.Active())
            {
                DTC_rec rec = DamageTakeChange_Check.DTC_rec_list.Find(a => a.markObject == mark);
                if (rec != null)
                {
                  //Debug.Log("DTC/dmgT触发");
                    int num = rec.dmg_old;
                    DamageTakeChange_Check.Clear_DTC_rec(rec.markObject);
                    //DamageTakeChange_Check.DTC_rec_list.RemoveAll(a => a.markObject == mark);
                    return Math.Max(num, dmg);
                }
                
            }
            return dmg;
        }
        public static void Clear_DTC_rec(GameObject mark=null)
        {
            if(mark!=null)
            {
                DamageTakeChange_Check.DTC_rec_list.RemoveAll(a => a.markObject == mark);
            }
            DamageTakeChange_Check.DTC_rec_list.RemoveAll(a => a.frame != Time.frameCount);
        }
        public static List<DTC_rec> DTC_rec_list=new List<DTC_rec>();
    }
    public class DTC_rec
    {
        public GameObject markObject = null;
        public int frame = 0;
        public int dmg_old = 0;
    }


    public static class CriDamageChange_Check
    {
        public static void Add_CriD_rec(GameObject mark, BattleChar user, BattleChar hit, int dmg)
        {
            if (DamageCheck_rec.Active(null, user, hit))
            {
                CriD_rec rec = new CriD_rec();
                rec.markObject = mark;
                rec.frame = Time.frameCount;
                rec.dmg_old = dmg;
                rec.mul = 1.5f + (Math.Max(0,user.GetStat.PlusCriDmg) + Math.Max(0, (float)hit.GetStat.CRIGetDMG)) * 0.01f;
                CriDamageChange_Check.CriD_rec_list.Add(rec);
            }
        }
        public static int Check_CriD_rec(GameObject mark, int dmg)
        {
            if (DamageCheck_rec.Active())
            {
                CriD_rec rec = CriDamageChange_Check.CriD_rec_list.Find(a => a.markObject == mark);
                if (rec != null)
                {
                    //Debug.Log("CriD触发");
                    int num = (int)(rec.mul * rec.dmg_old);
                    CriDamageChange_Check.Clear_CriD_rec(rec.markObject);
                    //DamageTakeChange_Check.CriD_rec_list.RemoveAll(a => a.markObject == mark);
                    return Math.Max(num, dmg);
                }

            }
            return dmg;
        }
        public static void Clear_CriD_rec(GameObject mark = null)
        {
            if (mark != null)
            {
                CriDamageChange_Check.CriD_rec_list.RemoveAll(a => a.markObject == mark);
            }
            CriDamageChange_Check.CriD_rec_list.RemoveAll(a => a.frame != Time.frameCount);
        }
        public static List<CriD_rec> CriD_rec_list = new List<CriD_rec>();
    }
    public class CriD_rec
    {
        public GameObject markObject = null;
        public int frame = 0;
        public int dmg_old = 0;
        public float mul = 1.5f;
    }








    //--------------额外伤害修正
    [HarmonyPatch(typeof(SkillTargetTooltip))]
    public class InputInfo_Plugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("InputInfo")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var methodInfo = AccessTools.Method(typeof(DamageCaculate), nameof(DamageCaculate.DamageChangePlus_View), new System.Type[] { typeof(Skill), typeof(BattleChar), typeof(int), typeof(bool), typeof(bool) });
            //var FieldInfo = AccessTools.Field(typeof(SkillParticle), "SkillData");
            var codes = instructions.ToList();

            bool triggered = false;
            /*
            for (int i = 0; i < codes.Count; i++)
            {
                if (i > 7)
                {
                    bool f0 = true;
                    f0 = f0 && codes[i - 6].opcode == OpCodes.Ldarg_2;
                    f0 = f0 && codes[i - 5].opcode == OpCodes.Ldarg_1;
                    f0 = f0 && codes[i - 4].opcode == OpCodes.Ldloc_0;
                    f0 = f0 && codes[i - 3].opcode == OpCodes.Ldloc_S;
                    
                    try
                    {
                        f0 = f0 && (codes[i - 3].operand is int && ((int)codes[i - 3].operand) == 14);
                    }
                    catch { f0 = false; }
                    
                    f0 = f0 && codes[i - 2].opcode == OpCodes.Ldc_I4_1;
                    f0 = f0 && codes[i - 1].opcode == OpCodes.Call;
                    try
                    {
                        f0 = f0 && (codes[i - 1].operand is MethodInfo && ((MethodInfo)(codes[i - 1].operand)).DeclaringType == typeof(DamageCaculate) && ((MethodInfo)(codes[i - 1].operand)).Name == "DamageChangePlus_View");
                    }
                    catch { f0 = false; }
                    f0 = f0 && codes[i - 0].opcode == OpCodes.Stloc_0;

                    if (f0)
                    {
                        //Debug.Log("test:" + codes[i - 3].operand.GetType());
                        triggered = true;
                    }
                }
            }
            */
            for (int i = 0; i < codes.Count; i++)
            {
                if (i > 5)
                {

                    bool f1 = true;
                    f1 = f1 && codes[i - 3].opcode == OpCodes.Ldloc_0;
                    f1 = f1 && codes[i - 2].opcode == OpCodes.Ldloc_S;
                    try
                    {
                        f1 = f1 && (codes[i - 2].operand is LocalBuilder && ((LocalBuilder)codes[i - 2].operand).LocalIndex == 5);
                    }
                    catch { f1 = false; }
                    f1 = f1 && codes[i - 1].opcode == OpCodes.Add;
                    f1 = f1 && codes[i - 0].opcode == OpCodes.Stloc_0;
                    if (f1)
                    {
                        yield return codes[i];

                        if (!triggered)
                        {
                            //Debug.Log("InputInfo_Pluginx修改：" + i);
                            yield return new CodeInstruction(OpCodes.Ldarg_2);
                            //yield return new CodeInstruction(OpCodes.Ldfld,FieldInfo);
                            yield return new CodeInstruction(OpCodes.Ldarg_1);
                            yield return new CodeInstruction(OpCodes.Ldloc_0);
                            yield return new CodeInstruction(OpCodes.Ldloc_S,14);
                            yield return new CodeInstruction(OpCodes.Ldc_I4_1);

                            yield return new CodeInstruction(OpCodes.Call, methodInfo);
                            yield return new CodeInstruction(OpCodes.Stloc_0);
                        }
                        else
                        {
                            //Debug.Log("InputInfo_Pluginx阻止修改：" + i);
                        }

                        continue;
                    }
                }
                yield return codes[i];
            }
        }

    }
    
    [HarmonyPatch(typeof(BattleChar))]
    public class Effect_Plugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("Effect")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var methodInfo = AccessTools.Method(typeof(DamageCaculate), nameof(DamageCaculate.DamageChangePlus), new System.Type[] { typeof(SkillParticle), typeof(BattleChar), typeof(int), typeof(bool), typeof(bool) });
            //var FieldInfo = AccessTools.Field(typeof(SkillParticle), "SkillData");
            var codes = instructions.ToList();

            bool triggered = false;
            /*
            for (int i = 0; i < codes.Count; i++)
            {
                if (i > 7)
                {
                    bool f0 = true;
                    f0 = f0 && codes[i - 7].opcode == OpCodes.Ldarg_1;
                    f0 = f0 && codes[i - 6].opcode == OpCodes.Ldarg_0;
                    f0 = f0 && codes[i - 5].opcode == OpCodes.Ldloc_2;
                    f0 = f0 && codes[i - 4].opcode == OpCodes.Conv_I4;
                    f0 = f0 && codes[i - 3].opcode == OpCodes.Ldloc_3;
                    f0 = f0 && codes[i - 2].opcode == OpCodes.Ldc_I4_0;
                    f0 = f0 && codes[i - 1].opcode == OpCodes.Call;
                    try
                    {
                        f0 = f0 && (codes[i - 1].operand is MethodInfo && ((MethodInfo)(codes[i - 1].operand)).DeclaringType == typeof(DamageCaculate) && ((MethodInfo)(codes[i - 1].operand)).Name== "DamageChangePlus");
                    }
                    catch { f0 = false; }
                    f0 = f0 && codes[i - 0].opcode == OpCodes.Stloc_2;

                    if(f0 )
                    {
                        triggered = true;
                    }
                }
            }
            */
            for (int i = 0; i < codes.Count; i++)
            {
                if (i > 5)
                {

                    bool f1 = true;
                    f1 = f1 && codes[i - 4].opcode == OpCodes.Ldloc_2;
                    f1 = f1 && codes[i - 3].opcode == OpCodes.Ldloc_S;
                    
                    try
                    {
                        f1 = f1 && (codes[i - 3].operand is LocalBuilder && ((LocalBuilder)codes[i - 3].operand).LocalIndex== 4);
                    }
                    catch { f1 = false;}
                    
                    f1 = f1 && codes[i - 2].opcode == OpCodes.Conv_R4;             
                    f1 = f1 && codes[i - 1].opcode == OpCodes.Add;
                    f1 = f1 && codes[i - 0].opcode == OpCodes.Stloc_2;
                    if (f1)
                    {
                        yield return codes[i];

                        if (!triggered)
                        {
                            //Debug.Log("Effect_Pluginx修改：" + i);
                            yield return new CodeInstruction(OpCodes.Ldarg_1);
                            //yield return new CodeInstruction(OpCodes.Ldfld,FieldInfo);
                            yield return new CodeInstruction(OpCodes.Ldarg_0);
                            yield return new CodeInstruction(OpCodes.Ldloc_2);
                            yield return new CodeInstruction(OpCodes.Conv_I4);
                            yield return new CodeInstruction(OpCodes.Ldloc_3);
                            yield return new CodeInstruction(OpCodes.Ldc_I4_0);

                            yield return new CodeInstruction(OpCodes.Call, methodInfo);
                            yield return new CodeInstruction(OpCodes.Stloc_2);
                        }
                        else
                        {
                            //Debug.Log("Effect_Pluginx阻止修改：" + i);
                        }
                        
                        continue;
                    }
                }
                yield return codes[i];
            }
        }
        
    }
    public static class DamageCaculate
    {
        public static int DamageChangePlus_View(Skill skill, BattleChar Target, int Damage, bool Cri, bool View)
        {
            //Debug.Log("触发:IP_DamageChangeFinal_V——1");
            float result = Damage;
            foreach (IP_DamageChangeFinal ip_1 in skill.Master.IReturn<IP_DamageChangeFinal>(skill))
            {
                try
                {
                    if (ip_1 != null)
                    {
                        result = (float)ip_1.DamageChangeFinal(skill, Target, (int)result, Cri, View);
                    }
                }
                catch
                {
                }
            }
            //Debug.Log("触发:IP_DamageChangeFinal_V——2");
            return (int)result;
        }
        public static float DamageChangePlus(SkillParticle SP, BattleChar Target, int Damage, bool Cri, bool View)
        {
            //Debug.Log("触发:IP_DamageChangeFinal——1");
            float result = Damage;
            foreach (IP_DamageChangeFinal ip_1 in SP.UseStatus.IReturn<IP_DamageChangeFinal>(SP.SkillData))
            {
                try
                {
                    if (ip_1 != null)
                    {
                        result = (float)ip_1.DamageChangeFinal(SP.SkillData, Target, (int)result, Cri, View);
                    }
                }
                catch
                {
                }
            }
            //Debug.Log("触发:IP_DamageChangeFinal——2");
            return result;
        }
        //public static bool changed = false;
    }
    public interface IP_DamageChangeFinal
    {
        // Token: 0x06002783 RID: 10115
        int DamageChangeFinal(Skill SkillD, BattleChar Target, int Damage, bool Cri, bool View);
    }
    

}
