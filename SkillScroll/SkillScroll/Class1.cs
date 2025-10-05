using ChronoArkMod.Plugin;
using ChronoArkMod;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Diagnostics;

namespace SkillScroll
{
    [PluginConfig("M200_SkillScroll_Mod", "SkillScroll", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class SkillScroll_CharacterModPlugin : ChronoArkPlugin
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
        public class SkillScrollDef : ModDefinition
        {
        }
    }


    //----------------滚轮浏览技能
    [HarmonyPatch(typeof(ToolTipWindow))]
    public class ToolTipWindowPlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("SkillToolTip")]
        [HarmonyPostfix]
        public static void SkillToolTip_Patch(Transform ParentTransform, Skill SkillData, BattleChar data, int pivotx = 0, int pivoty = 1, bool tooltipdestroy = true, bool ViewSkill = false, bool ForceManaView = false)//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
            if (ToolTipWindowPlugin.viewingObject == true && ToolTipWindowPlugin.skillkey == SkillData.MySkill.KeyID)
            {
              //Debug.Log("重复SkillToolTip");
            }
            if(ToolTipWindowPlugin.ModAuthor(SkillData.MySkill.KeyID) != "m200")
            {
                ToolTipWindowPlugin.viewingObject = true;
                ToolTipWindowPlugin.skillkey = SkillData.MySkill.KeyID;
                ToolTipWindowPlugin.skill = SkillData;
                if(ToolTipWindow.ToolTip!=null)
                {
                    ToolTipWindowPlugin.origin_y= ToolTipWindow.ToolTip.transform.localPosition.y;
                }
            }
        }
        [HarmonyPatch("SkillToolTip_Collection")]
        [HarmonyPostfix]
        public static void SkillToolTip_Collection_Patch(Transform ParentTransform, Skill SkillData, BattleChar data, int pivotx = 0, int pivoty = 1, bool tooltipdestroy = true, bool ViewSkill = false, bool ForceManaView = false)//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
            if (ToolTipWindowPlugin.viewingObject == true && ToolTipWindowPlugin.skillkey == SkillData.MySkill.KeyID)
            {
                //Debug.Log("重复SkillToolTip_Collection");
            }
            if (ToolTipWindowPlugin.ModAuthor(SkillData.MySkill.KeyID) != "m200")
            {
                ToolTipWindowPlugin.viewingObject = true;
                ToolTipWindowPlugin.skillkey = SkillData.MySkill.KeyID;
                ToolTipWindowPlugin.skill = SkillData;
                if (ToolTipWindow.ToolTip != null)
                {
                    ToolTipWindowPlugin.origin_y = ToolTipWindow.ToolTip.transform.localPosition.y;
                }
            }

        }

        [HarmonyPatch("BuffToolTip")]
        [HarmonyPatch(new Type[] { typeof(Transform), typeof(Buff), typeof(int), typeof(int) })]
        [HarmonyPostfix]
        public static void BuffToolTip_Patch(Transform ParentTransform, Buff BuffData, int pivotx , int pivoty )
        {
            if (ToolTipWindowPlugin.viewingObject == true && ToolTipWindowPlugin.skillkey == BuffData.BuffData.Key)
            {
              //Debug.Log("重复BuffToolTip");
            }
            if (ToolTipWindowPlugin.ModAuthor(BuffData.BuffData.Key) != "m200")
            {
              //Debug.Log("监测点BuffToolTip");
                ToolTipWindowPlugin.viewingObject = true;
                ToolTipWindowPlugin.skillkey = BuffData.BuffData.Key;
                //ToolTipWindowPlugin.skill = SkillData;
                if (ToolTipWindow.ToolTip != null)
                {
                    ToolTipWindowPlugin.origin_y = ToolTipWindow.ToolTip.transform.localPosition.y;
                }
            }
        }
        
        [HarmonyPatch("itemTooltip")]
        [HarmonyPostfix]
        public static void itemTooltip_Patch(Transform trans, ItemBase Item)
        {
            if (ToolTipWindowPlugin.viewingObject == true && ToolTipWindowPlugin.skillkey == Item.itemkey)
            {
              //Debug.Log("重复BuffToolTip");
            }
            if (ToolTipWindowPlugin.ModAuthor(Item.itemkey) != "m200")
            {
              //UnityEngine.Debug.Log("监测点itemTooltip");
                ToolTipWindowPlugin.viewingObject = true;
                ToolTipWindowPlugin.skillkey = Item.itemkey;
                if (ToolTipWindow.ToolTip != null)
                {
                    ToolTipWindowPlugin.origin_y = ToolTipWindow.ToolTip.transform.localPosition.y;
                }
            }
        }
        
        [HarmonyPatch("NewToolTip")]
        [HarmonyPatch(new Type[] { typeof(Transform), typeof(string), typeof(int), typeof(Vector2), typeof(Vector2), typeof(Vector2) })]
        [HarmonyPostfix]
        public static void NewToolTip_Patch(Transform ParentTransform, string ToolTipString, int MaxWidth, Vector2 AnchorMin, Vector2 AnchorMax, Vector2 Pivot)
        {
            if(ParentTransform.gameObject.GetComponent<ItemPrefab>()!=null)
            {
                string testkey = ParentTransform.gameObject.GetComponent<ItemPrefab>().itemkey;
                //UnityEngine.Debug.Log("监测点itemTooltip:"+ testkey);
                if (ToolTipWindowPlugin.viewingObject == true && ToolTipWindowPlugin.skillkey == testkey)
                {
                    //Debug.Log("重复BuffToolTip");
                }
                if (ToolTipWindowPlugin.ModAuthor(testkey) != "m200")
                {
                    //UnityEngine.Debug.Log("监测点itemTooltip");
                    ToolTipWindowPlugin.viewingObject = true;
                    ToolTipWindowPlugin.skillkey = testkey;
                    if (ToolTipWindow.ToolTip != null)
                    {
                        ToolTipWindowPlugin.origin_y = ToolTipWindow.ToolTip.transform.localPosition.y;
                    }
                }
            }
        }


        [HarmonyPatch("ToolTipDestroy")]
        [HarmonyPostfix]
        public static void ToolTipDestroy_Patch()
        {
            ToolTipWindowPlugin.viewingObject = false;
            ToolTipWindowPlugin.skillkey = string.Empty;
            ToolTipWindowPlugin.skill = new Skill();
        }


        public static string ModAuthor(string key)
        {
            if (ToolTipWindow.ModInfoCheck(key))
            {
                foreach (string id in ModManager.LoadedMods)
                {
                    if (ModManager.getModInfo(id).gdeinfo.Added.Contains(key))
                    {
                        return ModManager.getModInfo(id).Author;
                    }
                }
            }
            return string.Empty;
        }
        public static bool viewingObject = false;
        public static string skillkey = string.Empty;
        public static Skill skill = new Skill();
        public static float origin_y = -1000;




        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        public static void Update_Patch()//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
            if (ToolTipWindow.ToolTip != null && Input.GetAxis("Mouse ScrollWheel") != 0 && ToolTipWindowPlugin.viewingObject && ToolTipWindowPlugin.ModAuthor(ToolTipWindowPlugin.skillkey) != "m200" && ToolTipWindowPlugin.frame != Time.frameCount)
            {
              //Debug.Log("监测点item_Update_Patch");
                ToolTipWindowPlugin.frame = Time.frameCount;
                float sizey_1 = ToolTipWindow.ToolTip.GetComponent<RectTransform>().rect.yMax - ToolTipWindow.ToolTip.GetComponent<RectTransform>().rect.yMin;
                float sizey_plus = sizey_1;
                if (ToolTipWindow.ToolTip.GetComponent<SkillToolTip>() != null)
                {
                    sizey_plus = ToolTipWindow.ToolTip.GetComponent<SkillToolTip>().PlusTooltip.GetComponent<RectTransform>().rect.yMax - ToolTipWindow.ToolTip.GetComponent<SkillToolTip>().PlusTooltip.GetComponent<RectTransform>().rect.yMin;

                }
                float sizey=Math.Max(sizey_1,sizey_plus);

                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    float x = ToolTipWindow.ToolTip.transform.localPosition.x;
                    float y = ToolTipWindow.ToolTip.transform.localPosition.y;
                    float z = ToolTipWindow.ToolTip.transform.localPosition.z;

                    if (y > 540 || y > ToolTipWindowPlugin.origin_y)
                    {
                        y = Math.Max(Math.Min(540, ToolTipWindowPlugin.origin_y), y - 100);
                    }

                    ToolTipWindow.ToolTip.transform.localPosition = new Vector3(x, y, z);
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    float x = ToolTipWindow.ToolTip.transform.localPosition.x;
                    float y = ToolTipWindow.ToolTip.transform.localPosition.y;
                    float z = ToolTipWindow.ToolTip.transform.localPosition.z;

                    if (y - sizey < -540 + ToolTipWindowPlugin.redundancy)
                    {
                        y = Math.Min(y + 100, -540 + ToolTipWindowPlugin.redundancy + sizey);
                    }

                    ToolTipWindow.ToolTip.transform.localPosition = new Vector3(x, y, z);
                }


            }
            //Debug.Log("检查点1:SkillToolTip_Collection");
        }
        public static int frame = -1;
        public static int redundancy = 150;
    }

    [HarmonyPatch(typeof(Collections))]//阻止图鉴中的滚轮效果
    public class CollectionsPlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("get_Scroll")]
        [HarmonyPostfix]
        public static void get_Scroll_Patch(ref float __result)//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
            if (ToolTipWindowPlugin.viewingObject && ToolTipWindowPlugin.ModAuthor(ToolTipWindowPlugin.skillkey) != "m200")
            {
               
                __result = 0f;
            }
        }
    }

    [HarmonyPatch(typeof(ScrollEvent))]//阻止倒计时栏中的滚轮效果
    public class OnScrollPlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("OnScroll")]
        [HarmonyPrefix]
        public static bool OnScroll_Patch()//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
            if (ToolTipWindowPlugin.viewingObject && ToolTipWindowPlugin.ModAuthor(ToolTipWindowPlugin.skillkey) != "m200")
            {
                if (ToolTipWindowPlugin.skill.MyButton != null)// && ToolTipWindowPlugin.skill.MyButton.castskill != null)
                {
                    return false;
                }
                //return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(CharStatV4))]//阻止倒计时栏中的滚轮效果
    public class CharStatV4_Scroll_Plugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("FaceClickNext")]
        [HarmonyPrefix]
        public static bool OnScroll_Next_Patch()//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
            if (ToolTipWindowPlugin.viewingObject && ToolTipWindowPlugin.ModAuthor(ToolTipWindowPlugin.skillkey) != "m200")
            {
                if (ToolTipWindow.ToolTip!=null)// && ToolTipWindowPlugin.skill.MyButton.castskill != null)
                {
                    StackTrace stackTrace = new StackTrace();
                    StackFrame callerFrame2 = stackTrace.GetFrame(2);
                    if (callerFrame2 != null)
                    {
                        // 获取调用方法的名称
                        string callerMethodName = callerFrame2.GetMethod().Name;
                        if(callerMethodName=="InputKey")
                        {
                            return false;
                        }
                    }

                }
                //return false;
            }
            return true;
        }

        [HarmonyPatch("FaceClickPrev")]
        [HarmonyPrefix]
        public static bool OnScroll_Prew_Patch()//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
            if (ToolTipWindowPlugin.viewingObject && ToolTipWindowPlugin.ModAuthor(ToolTipWindowPlugin.skillkey) != "m200")
            {
                if (ToolTipWindow.ToolTip != null)// && ToolTipWindowPlugin.skill.MyButton.castskill != null)
                {
                    StackTrace stackTrace = new StackTrace();
                    StackFrame callerFrame2 = stackTrace.GetFrame(2);
                    if (callerFrame2 != null)
                    {
                        // 获取调用方法的名称
                        string callerMethodName = callerFrame2.GetMethod().Name;
                        if (callerMethodName == "InputKey")
                        {
                            return false;
                        }
                    }

                }
                //return false;
            }
            return true;
        }
    }
    /*
    [HarmonyPatch(typeof(ItemCollection))]//阻止倒计时栏中的滚轮效果
    public class ItemCollection_Plugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("LeftFlipBtn")]
        [HarmonyPrefix]
        public static bool LeftFlipBtn_Patch()//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
          //UnityEngine.Debug.Log("监测点LeftFlipBtn_1");
          //UnityEngine.Debug.Log(ToolTipWindowPlugin.viewingObject);
          //UnityEngine.Debug.Log(ToolTipWindowPlugin.ModAuthor(ToolTipWindowPlugin.skillkey) != "m200");
            if (ToolTipWindow.ToolTip != null )//&& ToolTipWindowPlugin.ModAuthor(ToolTipWindowPlugin.skillkey) != "m200")
            {
              //UnityEngine.Debug.Log("监测点LeftFlipBtn_2");
                if (ToolTipWindow.ToolTip != null)// && ToolTipWindowPlugin.skill.MyButton.castskill != null)
                {
                  //UnityEngine.Debug.Log("监测点LeftFlipBtn_3");
                    return false;

                }
                //return false;
            }
            return true;
        }

        [HarmonyPatch("RightFlipBtn")]
        [HarmonyPrefix]
        public static bool RightFlipBtn_Patch()//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
          //UnityEngine.Debug.Log("监测点RightFlipBtn_1");
          //UnityEngine.Debug.Log(ToolTipWindowPlugin.viewingObject);
          //UnityEngine.Debug.Log(ToolTipWindowPlugin.ModAuthor(ToolTipWindowPlugin.skillkey) != "m200");
            if (ToolTipWindow.ToolTip != null )//&& ToolTipWindowPlugin.ModAuthor(ToolTipWindowPlugin.skillkey) != "m200")
            {
              //UnityEngine.Debug.Log("监测点RightFlipBtn_2");
                if (ToolTipWindow.ToolTip != null)// && ToolTipWindowPlugin.skill.MyButton.castskill != null)
                {
                  //UnityEngine.Debug.Log("监测点RightFlipBtn_3");
                    return false;

                }
                //return false;
            }
            return true;
        }
    }
    */

    /*
    [HarmonyPatch(typeof(RecordViewScroll))]
    public class RecordViewScroll_Plugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("Update")]

        [HarmonyTranspiler]//ldarg.0 >> this
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var methodInfo = AccessTools.Method(typeof(RecordViewScroll_Update_Transpiler_Method), nameof(RecordViewScroll_Update_Transpiler_Method.Check_Scroll), new System.Type[] { typeof(float) });

            var codes = instructions.ToList();

            for (int i = 0; i < codes.Count; i++)
            {
                if (i > 3)
                {
                    bool f1 = codes[i - 2].opcode == OpCodes.Ldstr;
                    try
                    {
                        f1 = f1 && ((string)codes[i - 2].operand == "Mouse ScrollWheel");
                    }
                    catch
                    {
                        f1 = false;
                    }
                    f1 = f1 && codes[i - 1].opcode == OpCodes.Call;
                    f1 = f1 && codes[i].opcode == OpCodes.Stloc_0;


                    if (f1)
                    {
                      //Debug.Log("替换成功");
                        yield return codes[i];

                        yield return new CodeInstruction(OpCodes.Ldloc_0);
                        yield return new CodeInstruction(OpCodes.Call, methodInfo);
                        //yield return codes[i];
                        yield return new CodeInstruction(OpCodes.Stloc_0);
                        continue;
                        
                    }

                }

                    yield return codes[i];
                

            }
        }
    }
    
    public static class RecordViewScroll_Update_Transpiler_Method
    {
        public static float Check_Scroll(float axis)
        {
          //Debug.Log("尝试");
            if (ToolTipWindowPlugin.viewingObject && ToolTipWindowPlugin.ModAuthor(ToolTipWindowPlugin.skillkey) != "m200")
            {
              //Debug.Log("阻止");
                return 0f;
            }
            return axis;
        }
    }
    */



    /*
    [HarmonyPatch(typeof(SkillToolTip))]
    public class UpdatePlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        public static void Update_Patch()//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
            if (ToolTipWindow.ToolTip != null && Input.GetAxis("Mouse ScrollWheel") != 0 && ToolTipWindowPlugin.viewingObject && ToolTipWindowPlugin.ModAuthor(ToolTipWindowPlugin.skillkey) != "m200" && UpdatePlugin.frame != Time.frameCount)
            {
              //Debug.Log("监测点Update_Patch");
                UpdatePlugin.frame = Time.frameCount;
                float sizey = ToolTipWindow.ToolTip.GetComponent<RectTransform>().rect.yMax - ToolTipWindow.ToolTip.GetComponent<RectTransform>().rect.yMin;

                float sizey_plus = sizey;
                if(ToolTipWindow.ToolTip.GetComponent<SkillToolTip>()!=null)
                {
                    sizey_plus = ToolTipWindow.ToolTip.GetComponent<SkillToolTip>().PlusTooltip.GetComponent<RectTransform>().rect.yMax - ToolTipWindow.ToolTip.GetComponent<SkillToolTip>().PlusTooltip.GetComponent<RectTransform>().rect.yMin;

                }
                //float sizey_plus = ToolTipWindow.ToolTip.GetComponent<SkillToolTip>().PlusTooltip.GetComponent<RectTransform>().rect.yMax - ToolTipWindow.ToolTip.GetComponent<SkillToolTip>().PlusTooltip.GetComponent<RectTransform>().rect.yMin;

                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    float x = ToolTipWindow.ToolTip.transform.localPosition.x;
                    float y = ToolTipWindow.ToolTip.transform.localPosition.y;
                    float z = ToolTipWindow.ToolTip.transform.localPosition.z;
                    if (y > 540  || y> ToolTipWindowPlugin.origin_y)
                    {
                        y = Math.Max(Math.Min(540, ToolTipWindowPlugin.origin_y), y - 100);
                    }

                    ToolTipWindow.ToolTip.transform.localPosition = new Vector3(x, y, z);
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    float x = ToolTipWindow.ToolTip.transform.localPosition.x;
                    float y = ToolTipWindow.ToolTip.transform.localPosition.y;
                    float z = ToolTipWindow.ToolTip.transform.localPosition.z;

                    float sizey_2 = Math.Max(sizey_plus, sizey);
                    if (y - sizey_2 < -540+ UpdatePlugin.redundancy)
                    {
                        y = Math.Min(y + 100, -540+ UpdatePlugin.redundancy + sizey_2);
                    }

                    ToolTipWindow.ToolTip.transform.localPosition = new Vector3(x, y, z);
                }


            }
            //Debug.Log("检查点1:SkillToolTip_Collection");
        }
        public static int frame = -1;
        public static int redundancy = 150;
    }

    
    [HarmonyPatch(typeof(BuffTooltip))]
    public class BuffTooltip_UpdatePlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        public static void Update_Patch()//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
            if (ToolTipWindow.ToolTip != null && Input.GetAxis("Mouse ScrollWheel") != 0 && ToolTipWindowPlugin.viewingObject && ToolTipWindowPlugin.ModAuthor(ToolTipWindowPlugin.skillkey) != "m200" && BuffTooltip_UpdatePlugin.frame != Time.frameCount)
            {
              //Debug.Log("监测点Update_Patch");
                BuffTooltip_UpdatePlugin.frame = Time.frameCount;
                float sizey = ToolTipWindow.ToolTip.GetComponent<RectTransform>().rect.yMax - ToolTipWindow.ToolTip.GetComponent<RectTransform>().rect.yMin;


                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    float x = ToolTipWindow.ToolTip.transform.localPosition.x;
                    float y = ToolTipWindow.ToolTip.transform.localPosition.y;
                    float z = ToolTipWindow.ToolTip.transform.localPosition.z;

                    if (y > 540 || y > ToolTipWindowPlugin.origin_y)
                    {
                        y = Math.Max(Math.Min(540, ToolTipWindowPlugin.origin_y), y - 100);
                    }

                    ToolTipWindow.ToolTip.transform.localPosition = new Vector3(x, y, z);
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    float x = ToolTipWindow.ToolTip.transform.localPosition.x;
                    float y = ToolTipWindow.ToolTip.transform.localPosition.y;
                    float z = ToolTipWindow.ToolTip.transform.localPosition.z;

                    if (y - sizey < -540 + BuffTooltip_UpdatePlugin.redundancy)
                    {
                        y = Math.Min(y + 100, -540 + BuffTooltip_UpdatePlugin.redundancy + sizey);
                    }

                    ToolTipWindow.ToolTip.transform.localPosition = new Vector3(x, y, z);
                }


            }
            //Debug.Log("检查点1:SkillToolTip_Collection");
        }
        public static int frame = -1;
        public static int redundancy = 150;
    }
    
    */

}
