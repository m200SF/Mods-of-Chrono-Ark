using ChronoArkMod.Plugin;
using ChronoArkMod;
using HarmonyLib;
using System;
using System.Collections.Generic;

using UnityEngine;
using ChronoArkMod.ModData;
using UnityEngine.Events;
using Spine.Unity;
using TMPro;

using System.Reflection.Emit;
using System.Reflection;

using System.Collections;
using System.Xml.Serialization;
using DarkTonic.MasterAudio;
using GameDataEditor;
using I2.Loc;
using Steamworks;
using System.Linq;

using TileTypes;
using System.Security.AccessControl;
using static BattleChar;
using static GFL_Fairy.MapCharCtrl;
using ChronoArkMod.ModData.Settings;
using Fairy;



namespace GFL_Fairy
{
    [PluginConfig("M200_GFL_Fairy_CharMod", "GFL_Fairy_Mod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class GFL_Fairy_CharacterModPlugin : ChronoArkPlugin
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
        public class FairyDef : ModDefinition
        {
        }
    }



    [HarmonyPatch(typeof(BattleSystem))]
    public class BattleSystemPatch
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        [HarmonyPostfix]
        [HarmonyPatch("ModIReturn")]
        public static void ModIReturnPostfix(List<object> __result)
        {
            CommonData.FairySystemIReturn(null, __result);
        }

    }

    [HarmonyPatch(typeof(StoreItemButton))]
    public class StoreItemButtonPatch
    {
        // Token: 0x0600003B RID: 59 RVA: 0x00003388 File Offset: 0x00001588
        [HarmonyPrefix]
        [HarmonyPatch("Update")]
        public static bool UpdatePrefix(StoreItemButton __instance)
        {
            return __instance.Item != null;
        }

        // Token: 0x0600003C RID: 60 RVA: 0x000033B0 File Offset: 0x000015B0
        [HarmonyPostfix]
        [HarmonyPatch("OnPointerEnter")]
        public static void OnPointerEnterPostfix(StoreItemButton __instance)
        {
            bool flag = __instance.GetComponent<StoreItemCtrl>();
            if (flag)
            {
                __instance.GetComponent<StoreItemCtrl>().OnPointrEnter();
            }
        }
    }


    [HarmonyPatch(typeof(StageSystem))]
    public static class ISStageSystemPatch
    {
        // Token: 0x0600003A RID: 58 RVA: 0x0000337D File Offset: 0x0000157D
        [HarmonyPostfix]
        [HarmonyPatch("StageStart")]
        public static void StageStartPostfix(StageSystem __instance)
        {
          //Debug.Log("!!!!StageSystem StageStart");
          //Debug.Log("!!!!StageSystem StageStart");
          //Debug.Log("!!!!StageSystem StageStart");
          //Debug.Log("!!!!StageSystem StageStart");
          //Debug.Log("!!!!StageSystem StageStart");

            //MapChar_Persica.Init(__instance);


            //CommonData.stage = __instance;

            FieldSystem.instance.StartCoroutine(CommonData.SetPersicaAndDliegen());
        }


    }
    [HarmonyPatch(typeof(UIManager))]
    public static class FadeSquare_InPatch
    {
        // Token: 0x0600003A RID: 58 RVA: 0x0000337D File Offset: 0x0000157D
        [HarmonyPrefix]
        [HarmonyPatch("FadeSquare_In")]
        public static void StageStartPatch(StageSystem __instance)
        {
          //Debug.Log("!!!!UIManager FadeSquare_In");
          //Debug.Log("!!!!UIManager FadeSquare_In");
          //Debug.Log("!!!!UIManager FadeSquare_In");
          //Debug.Log("!!!!UIManager FadeSquare_In");
          //Debug.Log("!!!!UIManager FadeSquare_In");
          //Debug.Log("!!!!UIManager FadeSquare_In");

            //MapChar_Persica.Init(__instance);


            //CommonData.stage = __instance;

            FieldSystem.instance.StartCoroutine(CommonData.SetPersicaAndDliegen());
        }


    }


    /*
    // Token: 0x02000030 RID: 48
    [HarmonyPatch(typeof(FieldSystem))]
    [HarmonyPatch("Start")]
    public static class FieldStart_Patch
    {
        // Token: 0x0600008E RID: 142 RVA: 0x00004370 File Offset: 0x00002570
        [HarmonyPostfix]
        public static void StartPostfix(FieldSystem __instance)
        {
            CommonData.stage = __instance.GetComponent<StageSystem>();

          //Debug.Log("!!!!FieldSystem Start");
          //Debug.Log("!!!!FieldSystem Start");
          //Debug.Log("!!!!FieldSystem Start");
          //Debug.Log("!!!!FieldSystem Start");
          //Debug.Log("!!!!FieldSystem Start");

            //FieldSystem.DelayInput(CommonData.DelaySetDliegen());
        }



    }

    */


    [HarmonyPatch(typeof(FieldSystem))]
    public static class BattleStartPatch
    {
        // Token: 0x0600003A RID: 58 RVA: 0x0000337D File Offset: 0x0000157D
        [HarmonyPrefix]
        [HarmonyPatch("BattleStartCo")]
        public static void StartPrefix(FieldSystem __instance)
        {

            if (DroneSystem.Drone != null)
            {
              //Debug.Log("FieldSystem BattleStartCo_1");
                DroneSystem.Drone.transform.localScale = new Vector3(DroneSystem.Drone.transform.localScale.x, 0f, 1f);

            }
            else
            {
                //Debug.Log("未找到无人机");
            }
            if (MapChar_Persica.persicaObject != null)
            {
                //Debug.Log("FieldSystem BattleStartCo_2");
                MapChar_Persica.persicaObject.transform.localScale = new Vector3(1f, 0f, 1f);
            }
        }
    }

    [HarmonyPatch(typeof(FieldSystem))]
    public static class BattleEndPatch
    {
        // Token: 0x0600003A RID: 58 RVA: 0x0000337D File Offset: 0x0000157D
        [HarmonyPostfix]
        [HarmonyPatch("BattleEnd")]
        public static void AwakePostfix(FieldSystem __instance)
        {

            if (DroneSystem.Drone != null)
            {
                //Debug.Log("找到无人机");
                DroneSystem.Drone.transform.localScale = new Vector3(DroneSystem.Drone.transform.localScale.x, 1f, 1f);

                DroneSystem.progress = FieldSystem.instance.StartCoroutine(DroneSystem.Follow(DroneSystem.Drone));
            }
            else
            {
                //Debug.Log("未找到无人机");
            }
            if(MapChar_Persica.persicaObject != null)
            {
                MapChar_Persica.persicaObject.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }

    [HarmonyPatch(typeof(FieldSystem))]
    public static class NextStagePatch
    {
        // Token: 0x0600003A RID: 58 RVA: 0x0000337D File Offset: 0x0000157D
        [HarmonyPrefix]
        [HarmonyPatch("NextStage")]
        public static void NextStageBefore(FieldSystem __instance)
        {
            foreach(FairyBase fairybase in CommonData.GetFairyGameData().handlerList)
            {
                IP_NextStage_Before ip = fairybase as IP_NextStage_Before;
                if(ip!=null)
                {
                    ip.NextStage_Before();
                }
            }
            

        }
        [HarmonyPostfix]
        [HarmonyPatch("NextStage")]
        public static void NextStageAfter(FieldSystem __instance)
        {
            foreach (FairyBase fairybase in CommonData.GetFairyGameData().handlerList)
            {
                IP_NextStage_After ip = fairybase as IP_NextStage_After;
                if (ip != null)
                {
                    ip.NextStage_After();
                }
            }

        }
    }
    public interface IP_NextStage_Before
    {
        // Token: 0x06002766 RID: 10086
        void NextStage_Before();
    }
    public interface IP_NextStage_After
    {
        // Token: 0x06002766 RID: 10086
        void NextStage_After();
    }


    [HarmonyPatch(typeof(SkillToolTip))]
    public static class SkillToolTip_InputPatch
    {
        // Token: 0x0600003A RID: 58 RVA: 0x0000337D File Offset: 0x0000157D
        [HarmonyPrefix]
        [HarmonyPatch("Input")]
        public static void Input_Before(Skill Skill, Stat _stat, ToolTipWindow.SkillTooltipValues skillvalues, bool View = false, SkillPrefab sp = null)
        {
            foreach (Skill_Extended sex in Skill.AllExtendeds)
            {
                IP_SkillToolTip_Input ip = sex as IP_SkillToolTip_Input;
                if (ip != null)
                {
                    ip.SkillToolTip_Input();
                }
            }


        }
    }
    public interface IP_SkillToolTip_Input
    {
        // Token: 0x06002766 RID: 10086
        void SkillToolTip_Input();
    }

    [HarmonyPatch(typeof(SKillCollection))]//移除嘲讽靶机图鉴
    public class SKillCollection_Plugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("Init")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var methodInfo = AccessTools.Method(typeof(SKillCollection_Plugin), nameof(SKillCollection_Plugin.RemoveTempDoll), new System.Type[] { typeof(List<string>) });


            var codes = instructions.ToList();
            for (int i = 0; i < codes.Count; i++)
            {
                if (i >= 4)
                {
                    bool fm4 = codes[i - 4].opcode == OpCodes.Ldsfld;
                    bool fm3 = codes[i - 3].opcode == OpCodes.Ldarg_0;
                    bool fm2 = codes[i - 2].opcode == OpCodes.Ldflda;
                    bool fm1 = codes[i - 1].opcode == OpCodes.Call;
                    bool fm0 = codes[i].opcode == OpCodes.Pop;

                    bool fm4_2 = false;
                    if (codes[i - 4].operand is FieldInfo && ((FieldInfo)(codes[i - 4].operand)).Name.Contains("Character"))
                    {
                        fm4_2 = true;
                    }
                    bool fm1_2 = false;
                    if (codes[i - 1].operand is MethodInfo && ((MethodInfo)(codes[i - 1].operand)).Name.Contains("GetAllDataKeysBySchema"))
                    {
                        fm1_2 = true;
                    }
                    if (fm4 && fm4_2 && fm3 && fm2 && fm1 && fm1_2 && fm0)
                    {

                        yield return codes[i];

                        yield return new CodeInstruction(OpCodes.Ldarg_0);
                        yield return new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(SKillCollection), "CharKeys"));
                        yield return new CodeInstruction(OpCodes.Call, methodInfo);

                        yield return new CodeInstruction(OpCodes.Pop);
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


        public static bool RemoveTempDoll(List<string> charkeys)
        {

            for (int i = 0; i < charkeys.Count; i++)
            {
                if (SKillCollection_Plugin.ModID(charkeys[i]) == "GFL_Fairy")
                {
                    charkeys.RemoveAt(i);
                    i--;
                }

            }
            return true;
        }

        public static string ModID(string key)
        {
            if (ToolTipWindow.ModInfoCheck(key))
            {
                foreach (string id in ModManager.LoadedMods)
                {
                    if (ModManager.getModInfo(id).gdeinfo.Added.Contains(key))
                    {
                        //return ModManager.getModInfo(id).Author;
                        return ModManager.getModInfo(id).id;
                    }
                }
            }
            return string.Empty;
        }
    }














    public class MapChar_Persica
    {
        // Token: 0x06000032 RID: 50 RVA: 0x00002D04 File Offset: 0x00000F04
        public static void Init(StageSystem stageSystem)
        {
            bool flag2 = true;
            MapTile mapTile = null;
            if (stageSystem != null)
            {
                
                MapTile[,] mapObject = stageSystem.Map.MapObject;
                int upperBound = mapObject.GetUpperBound(0);
                int upperBound2 = mapObject.GetUpperBound(1);
                for (int i = mapObject.GetLowerBound(0); i <= upperBound; i++)
                {
                    for (int j = mapObject.GetLowerBound(1); j <= upperBound2; j++)
                    {
                        MapTile mapTile2 = mapObject[i, j];
                        bool flag = mapTile2.Info.Type.SpriteType == TILESPRITE.START;
                        if (flag)
                        {
                            mapTile = mapTile2;
                            goto IL_84;
                        }
                    }
                }
                
            IL_84:
                flag2 = mapTile == null;
            }

            
            if (!flag2)
            {
              //Debug.Log("生成帕斯卡_1");
                if (MapChar_Persica.persicaObject != null)
                {
                    UnityEngine.Object.Destroy(MapChar_Persica.persicaObject);
                }


                GameObject gameObject = MapCharCtrl.CreateSkeletonObj(MapCharEnum.PERSICA);

                CharCtrl_Persica charCtrl_Persica = gameObject.AddComponent<CharCtrl_Persica>();
                gameObject.GetComponent<EventObject>().TargetEvent.AddListener(delegate ()
                {
                    charCtrl_Persica.OpenStore();
                });

                gameObject.transform.parent = mapTile.HexTileComponent.transform;
                gameObject.transform.localPosition = new Vector3(3f, 1f, 0f);
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                gameObject.SetActive(true);

                MapChar_Persica.persicaObject = gameObject;


                FairySaveData save = CommonData.GetFairyGameData();
                save.running = true;
            }
            else
            {
              //Debug.Log("生成帕斯卡_2");
                if (MapChar_Persica.persicaObject != null)
                {
                    UnityEngine.Object.Destroy(MapChar_Persica.persicaObject);
                }


                GameObject gameObject = MapCharCtrl.CreateSkeletonObj(MapCharEnum.PERSICA);

                CharCtrl_Persica charCtrl_Persica = gameObject.AddComponent<CharCtrl_Persica>();
                gameObject.GetComponent<EventObject>().TargetEvent.AddListener(delegate ()
                {
                    charCtrl_Persica.OpenStore();
                });

                gameObject.transform.parent = FieldSystem.instance.Player.transform.parent;
                //gameObject.transform.localPosition = new Vector3(3f, -2f, 0f);
                gameObject.transform.localPosition = new Vector3(FieldSystem.instance.Player.transform.position.x + 3, FieldSystem.instance.Player.transform.position.y + 1, FieldSystem.instance.Player.transform.position.z);
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                gameObject.SetActive(true);

                MapChar_Persica.persicaObject = gameObject;
            }
        }

        public static void Init2()
        {

            if (true)
            {
                if (MapChar_Persica.persicaObject != null)
                {
                    UnityEngine.Object.Destroy(MapChar_Persica.persicaObject);
                }


                GameObject gameObject = MapCharCtrl.CreateSkeletonObj(MapCharEnum.PERSICA);

                CharCtrl_Persica charCtrl_Persica = gameObject.AddComponent<CharCtrl_Persica>();
                gameObject.GetComponent<EventObject>().TargetEvent.AddListener(delegate ()
                {
                    charCtrl_Persica.OpenStore();
                });

                gameObject.transform.parent = FieldSystem.instance.Player.transform.parent;
                //gameObject.transform.localPosition = new Vector3(3f, -2f, 0f);
                gameObject.transform.localPosition = new Vector3(FieldSystem.instance.Player.transform.position.x+3, FieldSystem.instance.Player.transform.position.y+1 , FieldSystem.instance.Player.transform.position.z);
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                gameObject.SetActive(true);

                MapChar_Persica.persicaObject = gameObject;
            }
        }

        public static void Init3()
        {
            bool flag2 = true;
            MapTile mapTile = null;
            try
            {
                StageSystem stageSystem = StageSystem.instance;
                if (stageSystem != null)
                {

                    MapTile[,] mapObject = stageSystem.Map.MapObject;
                    int upperBound = mapObject.GetUpperBound(0);
                    int upperBound2 = mapObject.GetUpperBound(1);
                    for (int i = mapObject.GetLowerBound(0); i <= upperBound; i++)
                    {
                        for (int j = mapObject.GetLowerBound(1); j <= upperBound2; j++)
                        {
                            MapTile mapTile2 = mapObject[i, j];
                            bool flag = mapTile2.Info.Type.SpriteType == TILESPRITE.START;
                            if (flag)
                            {
                                mapTile = mapTile2;
                                goto IL_84;
                            }
                        }
                    }

                IL_84:
                    flag2 = mapTile == null;
                }
            }
            catch
            {
              //Debug.Log("????1");
            }


            if (!flag2)
            {
              //Debug.Log("生成帕斯卡_1");
                if (MapChar_Persica.persicaObject != null)
                {
                    UnityEngine.Object.Destroy(MapChar_Persica.persicaObject);
                }


                GameObject gameObject = MapCharCtrl.CreateSkeletonObj(MapCharEnum.PERSICA);

                CharCtrl_Persica charCtrl_Persica = gameObject.AddComponent<CharCtrl_Persica>();
                gameObject.GetComponent<EventObject>().TargetEvent.AddListener(delegate ()
                {
                    charCtrl_Persica.OpenStore();
                });

                gameObject.transform.parent = mapTile.HexTileComponent.transform;
                gameObject.transform.localPosition = new Vector3(3f, 1f, 0f);
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                gameObject.SetActive(true);

                MapChar_Persica.persicaObject = gameObject;


                FairySaveData save = CommonData.GetFairyGameData();
                save.running = true;
            }
            else
            {
              //Debug.Log("生成帕斯卡_2");
                if (MapChar_Persica.persicaObject != null)
                {
                    UnityEngine.Object.Destroy(MapChar_Persica.persicaObject);
                }


                GameObject gameObject = MapCharCtrl.CreateSkeletonObj(MapCharEnum.PERSICA);

                CharCtrl_Persica charCtrl_Persica = gameObject.AddComponent<CharCtrl_Persica>();
                gameObject.GetComponent<EventObject>().TargetEvent.AddListener(delegate ()
                {
                    charCtrl_Persica.OpenStore();
                });

                gameObject.transform.parent = FieldSystem.instance.Player.transform.parent;
                //gameObject.transform.localPosition = new Vector3(3f, -2f, 0f);
                gameObject.transform.localPosition = new Vector3(FieldSystem.instance.Player.transform.position.x + 3, FieldSystem.instance.Player.transform.position.y + 1, FieldSystem.instance.Player.transform.position.z);
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                gameObject.SetActive(true);

                MapChar_Persica.persicaObject = gameObject;
            }
        }

        public static GameObject persicaObject = null;
    }

    public class MapChar_Battle_Fairy_Fliegen//生成战斗妖精无人机
    {
        // Token: 0x06000032 RID: 50 RVA: 0x00002D04 File Offset: 0x00000F04
        public static void Init()
        {
            /*
            MapTile mapTile = null;
            MapTile[,] mapObject = stageSystem.Map.MapObject;
            int upperBound = mapObject.GetUpperBound(0);
            int upperBound2 = mapObject.GetUpperBound(1);
            for (int i = mapObject.GetLowerBound(0); i <= upperBound; i++)
            {
                for (int j = mapObject.GetLowerBound(1); j <= upperBound2; j++)
                {
                    MapTile mapTile2 = mapObject[i, j];
                    bool flag = mapTile2.Info.Type.SpriteType == TILESPRITE.START;
                    if (flag)
                    {
                        mapTile = mapTile2;
                        goto IL_84;
                    }
                }
            }
        IL_84:
            bool flag2 = mapTile == null;
            */
            if (true)//!flag2)
            {
                GameObject gameObject = MapCharCtrl.CreateSkeletonObj(MapCharEnum.Battle_Fairy_Fliegen);

                CharCtrl_Battle_Fairy_Fliegen charCtrl_Battle_Fairy_Fliegen = gameObject.AddComponent<CharCtrl_Battle_Fairy_Fliegen>();
                gameObject.GetComponent<EventObject>().TargetEvent.AddListener(delegate ()
                {
                    charCtrl_Battle_Fairy_Fliegen.ClickFliegen();
                });

                gameObject.transform.parent = FieldSystem.instance.Player.transform.parent;//mapTile.HexTileComponent.transform;
                gameObject.transform.localPosition = new Vector3(FieldSystem.instance.Player.transform.position.x, FieldSystem.instance.Player.transform.position.y + 1, FieldSystem.instance.Player.transform.position.z);
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                gameObject.SetActive(true);

                if (FieldSystem.instance != null)
                {
                    DroneSystem.EndCoroutine();
                    //DroneSystem.progress = FieldSystem.instance.StartCoroutine(DroneSystem.Follow(gameObject));
                    DroneSystem.progress = FieldSystem.instance.StartCoroutine(DroneSystem.Follow(gameObject));
                }



            }
        }

    }
    public class MapChar_Strategy_Fairy_Fliegen//生成策略妖精无人机
    {
        // Token: 0x06000032 RID: 50 RVA: 0x00002D04 File Offset: 0x00000F04
        public static void Init()
        {
            /*
            MapTile mapTile = null;
            MapTile[,] mapObject = stageSystem.Map.MapObject;
            int upperBound = mapObject.GetUpperBound(0);
            int upperBound2 = mapObject.GetUpperBound(1);
            for (int i = mapObject.GetLowerBound(0); i <= upperBound; i++)
            {
                for (int j = mapObject.GetLowerBound(1); j <= upperBound2; j++)
                {
                    MapTile mapTile2 = mapObject[i, j];
                    bool flag = mapTile2.Info.Type.SpriteType == TILESPRITE.START;
                    if (flag)
                    {
                        mapTile = mapTile2;
                        goto IL_84;
                    }
                }
            }
        IL_84:
            bool flag2 = mapTile == null;
            */
            if (true)//!flag2)
            {
                GameObject gameObject = MapCharCtrl.CreateSkeletonObj(MapCharEnum.Strategy_Fairy_Fliegen);

                CharCtrl_Strategy_Fairy_Fliegen charCtrl_Strategy_Fairy_Fliegen = gameObject.AddComponent<CharCtrl_Strategy_Fairy_Fliegen>();
                gameObject.GetComponent<EventObject>().TargetEvent.AddListener(delegate ()
                {
                    charCtrl_Strategy_Fairy_Fliegen.ClickFliegen();
                });

                gameObject.transform.parent = FieldSystem.instance.Player.transform.parent;
                gameObject.transform.localPosition = new Vector3(FieldSystem.instance.Player.transform.position.x, FieldSystem.instance.Player.transform.position.y + 1, FieldSystem.instance.Player.transform.position.z);
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                gameObject.SetActive(true);

                if (FieldSystem.instance != null)
                {
                    //Debug.Log("尝试开始协程" + Time.frameCount);
                    DroneSystem.EndCoroutine();
                    //DroneSystem.progress = FieldSystem.instance.StartCoroutine(DroneSystem.Follow(gameObject));
                    DroneSystem.progress = FieldSystem.instance.StartCoroutine(DroneSystem.Follow(gameObject));
                }

            }
        }
    }
    public static class DroneSystem
    {
        public static void EndCoroutine()
        {
            if (DroneSystem.progress != null)
            {
                //FieldSystem.instance.StopCoroutine(DroneSystem.progress);
                FieldSystem.instance.StopCoroutine(DroneSystem.progress);

            }
            UnityEngine.Object.Destroy(DroneSystem.Drone);
            DroneSystem.Drone = null;
        }
        public static IEnumerator Follow(GameObject gameobject)
        {
            //Debug.Log("开始协程" + Time.frameCount);
            DroneSystem.Drone = gameobject;
            int frame = 0;
            while (FieldSystem.instance != null)
            {
                //yield return new WaitForSecondsRealtime(0.02f);
                //yield return new WaitForSeconds(0.02f);
                yield return new WaitForFixedUpdate();
                if (FieldSystem.instance != null && DroneSystem.Drone != null)
                {
                    //Debug.Log("原野不为空" + Time.frameCount);

                    Vector3 pos_p = new Vector3(FieldSystem.instance.Player.transform.position.x, FieldSystem.instance.Player.transform.position.y, FieldSystem.instance.Player.transform.position.z);
                    Vector3 pos_d = new Vector3(DroneSystem.Drone.transform.position.x, DroneSystem.Drone.transform.position.y, DroneSystem.Drone.transform.position.z);
                    float dx = pos_p.x - pos_d.x;
                    float dy = pos_p.y - pos_d.y - 0.09f;
                    float distance = (float)Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
                    /*
                    frame++;
                    if(frame>20)
                    {
                        frame = 0;
                        Debug.Log("无人机距离：" + distance);
                    }
                    */
                    if (distance > 3)
                    {
                        if (dx > 0)
                        {
                            DroneSystem.Drone.transform.localScale = new Vector3(-1f, 1f, 1f);
                        }
                        else
                        {
                            DroneSystem.Drone.transform.localScale = new Vector3(1f, 1f, 1f);
                        }
                        /*
                        float x1 = pos_d.x + dx / distance * (distance - 3);
                        float y1 = pos_d.y + dy / distance * (distance - 3);

                        pos_d.x = x1;
                        pos_d.y = y1;
                        */
                        float x1 = pos_d.x + (float)(0.1 * dx / distance * (distance - 3));
                        float y1 = pos_d.y + (float)(0.1 * dy / distance * (distance - 3));

                        pos_d.x = x1;
                        pos_d.y = y1;

                        DroneSystem.Drone.transform.position = pos_d;

                    }
                    else if (distance < 0.8) 
                    {
                        if (dx > 0)
                        {
                            DroneSystem.Drone.transform.localScale = new Vector3(1f, 1f, 1f);
                        }
                        else
                        {
                            DroneSystem.Drone.transform.localScale = new Vector3(-1f, 1f, 1f);
                        }
                        /*
                        float x1 = pos_d.x + dx / distance * (distance - 3);
                        float y1 = pos_d.y + dy / distance * (distance - 3);

                        pos_d.x = x1;
                        pos_d.y = y1;
                        */
                        float x1 = pos_d.x + (float)(1 * dx / distance * (distance - 0.8));
                        float y1 = pos_d.y + (float)(1 * dy / distance * (distance - 0.8));

                        pos_d.x = x1;
                        pos_d.y = y1;

                        DroneSystem.Drone.transform.position = pos_d;
                    }

                }
                else
                {
                    //Debug.Log("原野为空" + Time.frameCount);
                }



            }
            //Debug.Log("结束协程" + Time.frameCount);
            UnityEngine.Object.Destroy(DroneSystem.Drone);
            DroneSystem.Drone = null;
            if (DroneSystem.progress != null)
            {
                FieldSystem.instance.StopCoroutine(DroneSystem.progress);
            }
            yield break;
        }
        public static Coroutine progress = null;
        public static GameObject Drone = null;
    }


    public enum MapCharEnum
    {
        // Token: 0x0400003B RID: 59
        DEFAULT,
        // Token: 0x0400003C RID: 60
        PERSICA,
        Battle_Fairy_Fliegen,
        Strategy_Fairy_Fliegen
    }
    public class MapCharCtrl
    {
        // Token: 0x0600002D RID: 45 RVA: 0x00002ADC File Offset: 0x00000CDC
        public static GameObject GetSkeletonCharPrefab()
        {
            GameObject gameObject = AddressableLoadManager.Instantiate("FieldObject/Shard/Door", AddressableLoadManager.ManageType.Stage);
            Dorchi_Quest componentInChildren = gameObject.transform.GetComponentInChildren<Dorchi_Quest>(true);
            GameObject gameObject2 = componentInChildren.gameObject;
            gameObject2.SetActive(false);
            return gameObject2;
        }

        // Token: 0x0600002E RID: 46 RVA: 0x00002B18 File Offset: 0x00000D18
        public static void InitSkeletonCharObj(GameObject skeletonCharObj)
        {
            skeletonCharObj.GetComponent<EventObject>().TargetEvent = new UnityEvent();
            Dorchi_Quest componentInChildren = skeletonCharObj.transform.GetComponentInChildren<Dorchi_Quest>(true);
            UnityEngine.Object.Destroy(componentInChildren);
            foreach (Dialogue obj in skeletonCharObj.GetComponentsInChildren<Dialogue>())
            {
                UnityEngine.Object.Destroy(obj);
            }
            for (int j = 0; j < 100; j++)
            {
                Transform transform = skeletonCharObj.transform.Find("Dialogue");
                bool flag = transform == null;
                if (flag)
                {
                    break;
                }
                UnityEngine.Object.Destroy(transform.gameObject);
            }
        }

        // Token: 0x0600002F RID: 47 RVA: 0x00002BB8 File Offset: 0x00000DB8
        public static GameObject CreateSkeletonObj(MapCharEnum mapCharEnum)
        {
            GameObject skeletonCharPrefab = MapCharCtrl.GetSkeletonCharPrefab();
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(skeletonCharPrefab);
            MapCharCtrl.InitSkeletonCharObj(gameObject);
            bool flag = mapCharEnum == MapCharEnum.DEFAULT;
            GameObject result;
            if (flag)
            {
                result = gameObject;
            }
            else
            {
                MapCharCtrl.MapCharData mapCharData = MapCharCtrl.mapCharDataDic[mapCharEnum];
                gameObject.name = mapCharData.keyName;
                ModInfo modInfo = ModManager.getModInfo("GFL_Fairy");
                AssetBundle assetBundle = modInfo.assetInfo.GetAssetBundle("fairy");
                SkeletonDataAsset skeletonDataAsset = assetBundle.LoadAsset<SkeletonDataAsset>(mapCharData.skeletonPath);
                gameObject.GetComponentInChildren<SkeletonAnimation>().skeletonDataAsset = skeletonDataAsset;
                string name = skeletonDataAsset.GetSkeletonData(false).Skins.Items[0].Name;
                gameObject.GetComponentInChildren<SkeletonAnimation>().initialSkinName = name;
                gameObject.GetComponentInChildren<SkeletonAnimation>().Initialize(true);
                gameObject.GetComponentInChildren<SkeletonAnimation>().loop = true;
                gameObject.GetComponentInChildren<SkeletonAnimation>().AnimationName = mapCharData.skeletonIdleName;
                UnityEngine.Object.Destroy(skeletonCharPrefab);
                result = gameObject;
            }
            return result;
        }

        // Token: 0x0400003D RID: 61
        public static Dictionary<MapCharEnum, MapCharCtrl.MapCharData> mapCharDataDic = new Dictionary<MapCharEnum, MapCharCtrl.MapCharData>
        {
            {
                MapCharEnum.PERSICA,
                new MapCharCtrl.MapCharData
                {
                    keyName = "Persica",
                    name = "帕斯卡",
                    standingPath = "Assets/fairy/Persica/Image_persica.prefab",
                    skeletonPath = "Assets/fairy/Q_Persica/npc_persica_s38_SkeletonData.asset",
                    //standingPath = "Assets/fairy/Persica2/Image_persica2.prefab",
                    //skeletonPath = "Assets/fairy/Q_Persica_2/NPC_Persica_210_s38_SkeletonData.asset",
                    skeletonIdleName = "wait"
                }
            },
                        {
                MapCharEnum.Battle_Fairy_Fliegen,
                new MapCharCtrl.MapCharData
                {
                    keyName = "Battle_Fairy_Fliegen",
                    name = "战斗妖精无人机",
                    standingPath = "Assets/fairy/Fliegen2/Fliegen2.png",
                    skeletonPath = "Assets/fairy/Fliegen2/Fliegen2_s38_SkeletonData.asset",
                    skeletonIdleName = "wait"
                }
            },
                        {
                MapCharEnum.Strategy_Fairy_Fliegen,
                new MapCharCtrl.MapCharData
                {
                    keyName = "Strategy_Fairy_Fliegen",
                    name = "策略妖精无人机",
                    standingPath = "Assets/fairy/Fliegen/Fliegen.png",
                    skeletonPath = "Assets/fairy/Fliegen/Fliegen_s38_SkeletonData.asset",
                    skeletonIdleName = "wait"
                }
            }
        };

        // Token: 0x02000031 RID: 49
        public struct MapCharData
        {
            // Token: 0x04000063 RID: 99
            public string keyName;

            // Token: 0x04000064 RID: 100
            public string name;

            // Token: 0x04000065 RID: 101
            public string standingPath;

            // Token: 0x04000066 RID: 102
            public string skeletonPath;

            // Token: 0x04000067 RID: 103
            public string skeletonIdleName;
        }
    }

    public class CharCtrl_Persica : MonoBehaviour
    {
        // Token: 0x06000034 RID: 52 RVA: 0x00002E34 File Offset: 0x00001034
        public void OpenStore()
        {
            GameObject gameObject = UIManager.InstantiateActiveAddressable(UIManager.inst.AR_StoreUI, AddressableLoadManager.ManageType.Battle);
            this.buyWindow = gameObject.GetComponent<BuyWindow>();
            GameObject gameObject2 = this.buyWindow.transform.Find("Image").gameObject;
            ModInfo modInfo = ModManager.getModInfo("GFL_Fairy");
            AssetBundle assetBundle = modInfo.assetInfo.GetAssetBundle("fairy");

            MapCharCtrl.MapCharData mapCharData = MapCharCtrl.mapCharDataDic[MapCharEnum.PERSICA];
            GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(assetBundle.LoadAsset<GameObject>(mapCharData.standingPath)); //"Assets/fairy/Persica2/Image_persica2.prefab"));//Assets/fairy/Persica/Image_persica.prefab
            gameObject3.GetComponent<RectTransform>().parent = gameObject2.GetComponent<RectTransform>().parent;
            gameObject3.GetComponent<RectTransform>().SetAsFirstSibling();
            gameObject3.GetComponent<RectTransform>().localPosition = Vector3.zero;
            gameObject3.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0.9f);
            gameObject2.gameObject.SetActive(false);
            this.buyWindow.BuyButton.SetActive(false);
            this.buyWindow.transform.Find("BuyWindow/Line/BuyText").gameObject.SetActive(false);
            this.buyWindow.transform.Find("BuyWindow/Line/Image (1)").gameObject.SetActive(false);
            this.buyWindow.transform.Find("BuyWindow/Line/Image (2)").gameObject.SetActive(false);
            this.buyWindow.transform.Find("TitleAlign/Text (TMP)").GetComponent<TextMeshProUGUI>().text = CommonData.GetTranslation("GFL_Fairy/System/Shop_text_1");//"妖精商店";//待翻译
            this.buyWindow.transform.Find("BuyWindow/Line/ItemText").GetComponent<TextMeshProUGUI>().text = CommonData.GetTranslation("GFL_Fairy/System/Shop_text_2");// "妖精支援";//待翻译
            this.buyWindow.transform.Find("TitleAlign/Text (TMP) (1)").gameObject.SetActive(false);
            this.buyWindow.OriginItems = new List<ItemBase>();

            CharCtrl_Persica.shop = this;

            this.MakeItem_FairyCommand();

            FairySaveData hetgameData = CommonData.GetFairyGameData();
            using (Dictionary<FairyEnum, FairyConfig>.KeyCollection.Enumerator enumerator = Fairys.config.Keys.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    FairyEnum fairyEnum = enumerator.Current;
                    FairyConfig fairyConfig = Fairys.config[fairyEnum];
                    bool flag = hetgameData.handlerList.FindAll((FairyBase checkHandler) => checkHandler.GetFairyEnum() == fairyEnum).Count > 0;
                    if (!flag)
                    {
                        this.MakeItem(fairyEnum);
                    }
                }
            }
            
        }

        // Token: 0x06000035 RID: 53 RVA: 0x000030A4 File Offset: 0x000012A4
        public void MakeItem(FairyEnum fairyEnum)
        {
            bool multy_flag = ModManager.getModInfo("GFL_Fairy").GetSetting<ToggleSetting>("MulryFairy").Value;


            FairyConfig fairyConfig = Fairys.config[fairyEnum];
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.buyWindow.StoreItem);
            StoreItemCtrl_Fairy storeItemCtrl = gameObject.AddComponent<StoreItemCtrl_Fairy>();
            storeItemCtrl.fairyEnum = fairyEnum;
            storeItemCtrl.price = multy_flag?fairyConfig.shop_price_multy: fairyConfig.shop_price_single;
            gameObject.transform.SetParent(this.buyWindow.ItemList.transform);
            gameObject.GetComponent<ScrollEvent>().SR = this.buyWindow.SellScrollRect;
            Misc.UIInit(gameObject);
            ModInfo modInfo = ModManager.getModInfo("GFL_Fairy");
            StoreItemButton component = gameObject.GetComponent<StoreItemButton>();
            if(fairyConfig.itemIconPath.StartsWith("Assets/"))
            {
                AssetBundle assetBundle = modInfo.assetInfo.GetAssetBundle("fairy");
                component.Icon.sprite = assetBundle.LoadAsset<Sprite>(fairyConfig.itemIconPath);
            }
            else
            {
                string path = ((modInfo != null) ? modInfo.assetInfo.ImageFromFile(fairyConfig.itemIconPath) : null);
                component.Icon.sprite = AddressableLoadManager.LoadAsyncCompletion<Sprite>(path, AddressableLoadManager.ManageType.None);
            }

            component.Name.text = fairyConfig.name;
            component.Price.text = (multy_flag ? fairyConfig.shop_price_multy : fairyConfig.shop_price_single).ToString();
            component.MyButton.interactable = true;

            component.MyButton.onClick.RemoveAllListeners();
            component.MyButton.onClick.AddListener(delegate ()
            {
                storeItemCtrl.TryBuy();
            });
            this.buyWindow.SellItems.Add(component);
            Traverse.Create(this.buyWindow).Field("TargetUpdate").SetValue(true);
        }

        public void MakeItem_FairyCommand()
        {


            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.buyWindow.StoreItem);
            StoreItemCtrl_Command storeItemCtrl = gameObject.AddComponent<StoreItemCtrl_Command>();
            storeItemCtrl.item = ItemBase.GetItem("Item_Fairy_Command");
            storeItemCtrl.price =75;
            gameObject.transform.SetParent(this.buyWindow.ItemList.transform);
            gameObject.GetComponent<ScrollEvent>().SR = this.buyWindow.SellScrollRect;
            Misc.UIInit(gameObject);
            ModInfo modInfo = ModManager.getModInfo("GFL_Fairy");
            StoreItemButton component = gameObject.GetComponent<StoreItemButton>();

                AssetBundle assetBundle = modInfo.assetInfo.GetAssetBundle("fairy");
                component.Icon.sprite = assetBundle.LoadAsset<Sprite>("Assets/fairy/Fairys/Fairy_Command.png");

            component.Name.text = storeItemCtrl.item.GetName;
            component.Price.text = "75";
            component.MyButton.interactable = true;

            component.MyButton.onClick.RemoveAllListeners();
            component.MyButton.onClick.AddListener(delegate ()
            {
                storeItemCtrl.TryBuy();
            });
            this.buyWindow.SellItems.Add(component);
            Traverse.Create(this.buyWindow).Field("TargetUpdate").SetValue(true);
        }
        public static CharCtrl_Persica shop = null;
        // Token: 0x0400003E RID: 62
        public BuyWindow buyWindow;

    }


    public class StoreItemCtrl : MonoBehaviour
    {
        public virtual void OnPointrEnter()
        {

        }
        public virtual void TryBuy()
        {

        }
        // Token: 0x04000040 RID: 64
        public int price;
    }
    public class StoreItemCtrl_Fairy : StoreItemCtrl
    {
        // Token: 0x06000037 RID: 55 RVA: 0x00003220 File Offset: 0x00001420
        public override void OnPointrEnter()
        {
            //Debug.Log("展示技能");
            FairyConfig fairyConfig = Fairys.config[this.fairyEnum];
            BattleAlly battleAlly = PlayData.TempBattleChar(new GDECharacterData(PlayData.TSavedata.Party[0].KeyData));
            Skill skillData = Skill.TempSkill(fairyConfig.previewSkillKey, battleAlly, PlayData.TempBattleTeam);

            foreach (Skill_Extended sex in skillData.AllExtendeds)
            {
                if (sex is Sex_Fairy_View sexf)
                {
                    sexf.fairyEnum = this.fairyEnum;
                    sexf.viewInShop = true;
                }
            }

            ToolTipWindow.SkillToolTip(base.transform, skillData, battleAlly, 0, 1, true, false, false);
        }

        // Token: 0x06000038 RID: 56 RVA: 0x00003284 File Offset: 0x00001484
        public override void TryBuy()
        {
            bool flag = PlayData.Gold < this.price;
            if (flag)
            {
                EffectView.SimpleTextout(base.transform, CommonData.GetTranslation("GFL_Fairy/System/Shop_text_3"), 1f, false, 1f);//待翻译
            }
            else
            {
                PlayData.Gold -= this.price;
                MasterAudio.PlaySound("SE_ItemAllGet", 1f, null, 0f, null, null, false, false);
                base.GetComponent<StoreItemButton>().MyButton.interactable = false;
                base.GetComponent<StoreItemButton>().MyButton.gameObject.SetActive(false);


                if(ModManager.getModInfo("GFL_Fairy").GetSetting<ToggleSetting>("MulryFairy").Value)
                {
                    CommonData.AddFairy(this.fairyEnum);
                }
                else
                {
                    CommonData.SwitchFairy(this.fairyEnum);
                }
                /*
                FairyConfig heavyEquipTroopsConfig = Fairys.config[this.fairyEnum];
                FairyBase item = Activator.CreateInstance(heavyEquipTroopsConfig.handleClass) as FairyBase;
                FairySaveData hetgameData = CommonData.GetFairyGameData();
                hetgameData.handlerList.Add(item);
                hetgameData.handlerEnums.Add(this.fairyEnum);
                */
            }
        }

        // Token: 0x0400003F RID: 63
        public FairyEnum fairyEnum;
    }

    public class StoreItemCtrl_Command : StoreItemCtrl
    {
        // Token: 0x06000037 RID: 55 RVA: 0x00003220 File Offset: 0x00001420
        public override void OnPointrEnter()
        {

            if(this.item!=null)
            {
                ToolTipWindow.itemTooltip(base.transform, this.item);
            }
        }

        // Token: 0x06000038 RID: 56 RVA: 0x00003284 File Offset: 0x00001484
        public override void TryBuy()
        {
            bool flag = PlayData.Gold < this.price;
            if (flag)
            {
                EffectView.SimpleTextout(base.transform, CommonData.GetTranslation("GFL_Fairy/System/Shop_text_3"), 1f, false, 1f);//已翻译
            }
            else
            {
                PlayData.Gold -= this.price;
                MasterAudio.PlaySound("SE_ItemAllGet", 1f, null, 0f, null, null, false, false);

                Fairy_Command_Method.RemainNum = Fairy_Command_Method.RemainNum + 1;

                EffectView.SimpleTextout(base.transform, this.item.GetName+" +1", 1f, false, 1f);
            }
        }
        public ItemBase item = null;
    }




    public class CharCtrl_Battle_Fairy_Fliegen : MonoBehaviour
    {
        // Token: 0x06000034 RID: 52 RVA: 0x00002E34 File Offset: 0x00001034
        public void ClickFliegen()
        {

            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            if (save.handlerList.Count > 0)
            {
                //save.handlerList[0].ClickFliegen();
                FairyActiveData.SwitchTrigger();
            }
        }
        // Token: 0x0400003E RID: 62
        public BuyWindow buyWindow;

    }
    public class CharCtrl_Strategy_Fairy_Fliegen : MonoBehaviour
    {
        // Token: 0x06000034 RID: 52 RVA: 0x00002E34 File Offset: 0x00001034
        public void ClickFliegen()
        {

            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            if (save.handlerList.Count > 0)
            {
                //Debug.Log("ClickFliegen:" + save.handlerList[0].GetFairyEnum());
                //save.handlerList[0].ClickFliegen();
                FairyActiveData.SwitchTrigger();
            }

        }
        // Token: 0x0400003E RID: 62
        public BuyWindow buyWindow;

    }









    public enum FairyEnum
    {
        GFL_Command_Fairy,
        GFL_Combo_Fairy,
        GFL_Provocation_Fairy,
        GFL_Illumination_Fairy,
        GFL_Peace_Fairy,
        GFL_Barrier_Fairy,
        GFL_Witch_Fairy,
        GFL_Beach_Fairy,
        GFL_Reinforcement_Fairy,
        GFL_Nurse_Fairy,
        GFL_Airstrike_Fairy,
        GFL_Bombardment_Fairy,
        GFL_Sniper_Fairy,
        GFL_Rocket_Fairy,
        GFL_Warrior_Fairy,
        GFL_Fury_Fairy,
        GFL_Armor_Fairy,
        GFL_Defense_Fairy,
        GFL_Shield_Fairy,
        GFL_Landmine_Fairy,
        GFL_Electromagnetic_Fairy,
        GFL_Golden_Fairy
    }
    public struct FairyConfig
    {
        // Token: 0x04000030 RID: 48
        public string name;
        public string key;
        public bool fairytype;//策略妖精为true，否则为false
        // Token: 0x04000031 RID: 49
        public int shop_price_single;
        public int shop_price_multy;

        public int use_price;

        // Token: 0x04000032 RID: 50
        public string itemIconPath;

        // Token: 0x04000033 RID: 51
        public string previewSkillKey;

        // Token: 0x04000034 RID: 52
        //public string skeletonPath;

        // Token: 0x04000035 RID: 53
        //public string skeletonAtkAniName;

        // Token: 0x04000036 RID: 54
        public Type handleClass;
    }
    public class Fairys
    {
        public static Dictionary<FairyEnum, FairyConfig> config = new Dictionary<FairyEnum, FairyConfig>
        {
            {
                FairyEnum.GFL_Command_Fairy,
                new FairyConfig
                {
                    name =CommonData.FairyName("Command_Fairy"),//待翻译
                    key="Command_Fairy",
                    fairytype=false,
                    shop_price_single = 200,
                    shop_price_multy=200,
                    use_price = 1,
                    itemIconPath = "Assets/fairy/Fairys/Command_Fairy/Command_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Command_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Command_Fairy)
                }
            },
            {
                FairyEnum.GFL_Combo_Fairy,
                new FairyConfig
                {
                    name = CommonData.FairyName("Combo_Fairy"),//待翻译
                    key="Combo_Fairy",
                    fairytype=true,
                    shop_price_single = 400,
                    shop_price_multy = 400,
                    use_price = 6,
                    itemIconPath = "Assets/fairy/Fairys/Combo_Fairy/Combo_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Combo_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Combo_Fairy)
                }
            },
            {
                FairyEnum.GFL_Provocation_Fairy,
                new FairyConfig
                {
                    name =CommonData.FairyName("Provocation_Fairy"),//待翻译
                    key="Provocation_Fairy",
                    fairytype=false,
                    shop_price_single = 200,
                    shop_price_multy = 200,
                    use_price = 3,
                    itemIconPath = "Assets/fairy/Fairys/Provocation_Fairy/Provocation_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Provocation_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Provocation_Fairy)
                }
            },
            {
                FairyEnum.GFL_Illumination_Fairy,
                new FairyConfig
                {
                    name = CommonData.FairyName("Illumination_Fairy"),//待翻译
                    key="Illumination_Fairy",
                    fairytype=true,
                    shop_price_single = 400,
                    shop_price_multy = 400,
                    use_price = 4,
                    itemIconPath = "Assets/fairy/Fairys/Illumination_Fairy/Illumination_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Illumination_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Illumination_Fairy)
                }
            },
                        {
                FairyEnum.GFL_Peace_Fairy,
                new FairyConfig
                {
                    name =CommonData.FairyName("Peace_Fairy"),//待翻译
                    key="Peace_Fairy",
                    fairytype=false,
                    shop_price_single = 200,
                    shop_price_multy = 200,
                    use_price = 3,
                    itemIconPath = "Assets/fairy/Fairys/Peace_Fairy/Peace_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Peace_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Peace_Fairy)
                }
            },
                        {
                FairyEnum.GFL_Barrier_Fairy,
                new FairyConfig
                {
                    name =CommonData.FairyName("Barrier_Fairy"),//待翻译
                    key="Barrier_Fairy",
                    fairytype=false,
                    shop_price_single = 200,
                    shop_price_multy = 200,
                    use_price = 3,
                    itemIconPath = "Assets/fairy/Fairys/Barrier_Fairy/Barrier_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Barrier_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Barrier_Fairy)
                }
            },
                        {
                FairyEnum.GFL_Witch_Fairy,
                new FairyConfig
                {
                    name = CommonData.FairyName("Witch_Fairy"),//待翻译
                    key="Witch_Fairy",
                    fairytype=false,
                    shop_price_single = 200,
                    shop_price_multy = 200,
                    use_price = 1,
                    itemIconPath = "Assets/fairy/Fairys/Witch_Fairy/Witch_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Witch_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Witch_Fairy)
                }
            },
                        {
                FairyEnum.GFL_Beach_Fairy,
                new FairyConfig
                {
                    name = CommonData.FairyName("Beach_Fairy"),//待翻译
                    key="Beach_Fairy",
                    fairytype=false,
                    shop_price_single = 200,
                    shop_price_multy = 200,
                    use_price = 1,
                    itemIconPath = "Assets/fairy/Fairys/Beach_Fairy/Beach_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Beach_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Beach_Fairy)
                }
            },
                        {
                FairyEnum.GFL_Reinforcement_Fairy,
                new FairyConfig
                {
                    name = CommonData.FairyName("Reinforcement_Fairy"),//待翻译
                    key="Reinforcement_Fairy",
                    fairytype=true,
                    shop_price_single = 400,
                    shop_price_multy = 400,
                    use_price = 8,
                    itemIconPath = "Assets/fairy/Fairys/Reinforcement_Fairy/Reinforcement_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Reinforcement_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Reinforcement_Fairy)
                }
            },
                        {
                FairyEnum.GFL_Nurse_Fairy,
                new FairyConfig
                {
                    name = CommonData.FairyName("Nurse_Fairy"),//"护理妖精",//待翻译
                    key="Nurse_Fairy",
                    fairytype=true,
                    shop_price_single = 400,
                    shop_price_multy = 400,
                    use_price = 6,
                    itemIconPath = "Assets/fairy/Fairys/Nurse_Fairy/Nurse_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Nurse_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Nurse_Fairy)
                }
            },
                        {
                FairyEnum.GFL_Airstrike_Fairy,
                new FairyConfig
                {
                    name = CommonData.FairyName("Airstrike_Fairy"),//待翻译
                    key="Airstrike_Fairy",
                    fairytype=false,
                    shop_price_single = 200,
                    shop_price_multy = 200,
                    use_price = 2,
                    itemIconPath = "Assets/fairy/Fairys/Airstrike_Fairy/Airstrike_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Airstrike_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Airstrike_Fairy)
                }
            },
                        {
                FairyEnum.GFL_Bombardment_Fairy,
                new FairyConfig
                {
                    name = CommonData.FairyName("Bombardment_Fairy"),//待翻译
                    key="Bombardment_Fairy",
                    fairytype=false,
                    shop_price_single = 200,
                    shop_price_multy = 200,
                    use_price = 2,
                    itemIconPath = "Assets/fairy/Fairys/Bombardment_Fairy/Bombardment_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Bombardment_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Bombardment_Fairy)
                }
            },
                        {
                FairyEnum.GFL_Sniper_Fairy,
                new FairyConfig
                {
                    name = CommonData.FairyName("Sniper_Fairy"),//待翻译
                    key="Sniper_Fairy",
                    fairytype=false,
                    shop_price_single = 200,
                    shop_price_multy = 200,
                    use_price = 2,
                    itemIconPath = "Assets/fairy/Fairys/Sniper_Fairy/Sniper_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Sniper_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Sniper_Fairy)
                }
            },
                        {
                FairyEnum.GFL_Rocket_Fairy,
                new FairyConfig
                {
                    name = CommonData.FairyName("Rocket_Fairy"),//待翻译
                    key="Rocket_Fairy",
                    fairytype=true,
                    shop_price_single = 400,
                    shop_price_multy = 400,
                    use_price = 7,
                    itemIconPath = "Assets/fairy/Fairys/Rocket_Fairy/Rocket_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Rocket_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Rocket_Fairy)
                }
            },
                        {
                FairyEnum.GFL_Warrior_Fairy,
                new FairyConfig
                {
                    name = CommonData.FairyName("Warrior_Fairy"),//待翻译
                    key="Warrior_Fairy",
                    fairytype=false,
                    shop_price_single = 200,
                    shop_price_multy = 200,
                    use_price = 2,
                    itemIconPath = "Assets/fairy/Fairys/Warrior_Fairy/Warrior_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Warrior_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Warrior_Fairy)
                }
            },
                        {
                FairyEnum.GFL_Fury_Fairy,
                new FairyConfig
                {
                    name = CommonData.FairyName("Fury_Fairy"),//待翻译
                    key="Fury_Fairy",
                    fairytype=false,
                    shop_price_single = 200,
                    shop_price_multy = 200,
                    use_price = 2,
                    itemIconPath = "Assets/fairy/Fairys/Fury_Fairy/Fury_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Fury_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Fury_Fairy)
                }
            },
                        {
                FairyEnum.GFL_Armor_Fairy,
                new FairyConfig
                {
                    name = CommonData.FairyName("Armor_Fairy"),//待翻译
                    key="Armor_Fairy",
                    fairytype=false,
                    shop_price_single = 200,
                    shop_price_multy = 200,
                    use_price = 3,
                    itemIconPath = "Assets/fairy/Fairys/Armor_Fairy/Armor_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Armor_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Armor_Fairy)
                }
            },
                                                {
                FairyEnum.GFL_Defense_Fairy,
                new FairyConfig
                {
                    name = CommonData.FairyName("Defense_Fairy"),//待翻译
                    key="Defense_Fairy",
                    fairytype=false,
                    shop_price_single = 200,
                    shop_price_multy = 200,
                    use_price = 3,
                    itemIconPath = "Assets/fairy/Fairys/Defense_Fairy/Defense_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Defense_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Defense_Fairy)
                }
            },
                        {
                FairyEnum.GFL_Shield_Fairy,
                new FairyConfig
                {
                    name = CommonData.FairyName("Shield_Fairy"),//待翻译
                    key="Shield_Fairy",
                    fairytype=false,
                    shop_price_single = 200,
                    shop_price_multy = 200,
                    use_price = 2,
                    itemIconPath = "Assets/fairy/Fairys/Shield_Fairy/Shield_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Shield_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Shield_Fairy)
                }
            },
                         {
                FairyEnum.GFL_Landmine_Fairy,
                new FairyConfig
                {
                    name = CommonData.FairyName("Landmine_Fairy"),//待翻译
                    key="Landmine_Fairy",
                    fairytype=true,
                    shop_price_single = 400,
                    shop_price_multy = 400,
                    use_price = 5,
                    itemIconPath = "Assets/fairy/Fairys/Landmine_Fairy/Landmine_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Landmine_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Landmine_Fairy)
                }
            },
                        {
                FairyEnum.GFL_Electromagnetic_Fairy,
                new FairyConfig
                {
                    name = CommonData.FairyName("Electromagnetic_Fairy"),//待翻译
                    key="Electromagnetic_Fairy",
                    fairytype=false,
                    shop_price_single = 200,
                    shop_price_multy = 200,
                    use_price = 3,
                    itemIconPath = "FairyChibi/Electromagnetic_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Electromagnetic_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Electromagnetic_Fairy)
                }
            },
                        {
                FairyEnum.GFL_Golden_Fairy,
                new FairyConfig
                {
                    name = CommonData.FairyName("Golden_Fairy"),//待翻译
                    key="Golden_Fairy",
                    fairytype=false,
                    shop_price_single = 200,
                    shop_price_multy = 200,
                    use_price = 0,
                    itemIconPath = "Assets/fairy/Fairys/Golden_Fairy/Golden_Fairy_chibi_3.png",
                    previewSkillKey = "S_GFL_Golden_Fairy_View",
                    //skeletonPath = "Assets/GFHeavyEquipTroops/BGM_71/TOW_SkeletonData.asset",
                    //skeletonAtkAniName = "attack",
                    handleClass = typeof(Ability_Golden_Fairy)
                }
            },
                       

        };
    }


    public static class AllySummon
    {
        public static BattleAlly NewAlly(string charKey, string BasicKey)
        {

            Character character = new Character();

            GDECharacterData gde = new GDECharacterData(charKey);

            character.Set_AllyData(gde);


            bool nobasic = false;
            try
            {
                if (!BasicKey.IsNullOrEmpty())
                {
                    //Debug.Log("有固定技能");
                    character.BasicSkill = new CharInfoSkillData(BasicKey);

                }
                else
                {
                    //Debug.Log("无固定技能");
                    character.BasicSkill = new CharInfoSkillData("S_GFL_Fairy_SummonBasic_0");
                    nobasic = true;
                }
            }
            catch
            {
                //Debug.Log("固定技能报错");
            }
            //Debug.Log("NewAlly_2");
            BattleAlly battleAlly = BattleSystem.instance.CreatTempAlly(character);
            bool flag2 = battleAlly.BasicSkill != null;
            if (flag2)
            {
                battleAlly.MyBasicSkill.SkillInput(battleAlly.BasicSkill.CloneSkill(true, null, null, false));
                battleAlly.BattleBasicskillRefill = battleAlly.BasicSkill.CloneSkill(true, null, null, false);
            }
            int num = BattleSystem.instance.AllyTeam.Chars.IndexOf(battleAlly);
            bool flag3 = num >= 0;
            if (flag2 && flag3)
            {
                BattleSystem.instance.AllyTeam.Skills_Basic = BattleSystem.instance.AllyTeam.Skills_Basic.SetAt(num, battleAlly.BasicSkill.CloneSkill(true, null, null, false));
            }

            if (nobasic)
            {
                battleAlly.MyBasicSkill.InActive = true;
                battleAlly.MyBasicSkill.CoolDownNum = -1;
                //battleAlly.MyBasicSkill.buttonData = null;

            }

            battleAlly.Info.Equip.Clear();

            BattleSystem.instance.StartCoroutine(AllySummon.DelaySetPos());

            //Debug.Log("NewAlly_3");
            return battleAlly;
        }
        private static IEnumerator DelaySetPos()
        {
            yield return new WaitForEndOfFrame();
            AllySummon.SetAlliesTransform();
            yield break;
        }
        public static void SetAlliesTransform()
        {
            List<BattleChar> chars = BattleSystem.instance.AllyTeam.Chars;
            bool flag = chars.Count < 0;
            if (!flag)
            {
                Transform parent = chars[0].transform.parent;
                int num = parent.Cast<Transform>().Count((Transform child) => child.gameObject.activeSelf);
                bool flag2 = num < 0;
                if (!flag2)
                {
                    float num2 = (num <= 4) ? 1f : (4f / (float)num);
                    parent.localPosition = new Vector3(269f - 1000f * (1f - num2), parent.localPosition.y, parent.localPosition.z);
                    parent.localScale = new Vector3(num2, num2, 1f);
                }
            }
        }
        // Token: 0x0600002C RID: 44 RVA: 0x00002C38 File Offset: 0x00000E38
        public static void RemoveAlly(BattleAlly battleAlly)
        {
            int num = BattleSystem.instance.AllyTeam.Chars.IndexOf(battleAlly);
            bool flag = num >= 0;
            if (flag)
            {
                BattleSystem.instance.AllyTeam.Chars.RemoveAt(num);
                BattleSystem.instance.AllyTeam.Skills_Basic.RemoveAt(num);
            }
            UnityEngine.Object.Destroy(battleAlly.gameObject);
            BattleSystem.instance.StartCoroutine(AllySummon.DelaySetPos());
        }

        public static T[] SetAt<T>(this T[] arr, int index, T value)
        {
            bool flag = index < 0;
            T[] result;
            if (flag)
            {
                result = arr;
            }
            else
            {
                bool flag2 = index < arr.Length - 1;
                if (flag2)
                {
                    arr[index] = value;
                    result = arr;
                }
                else
                {
                    T[] array = new T[index + 1];
                    Array.Copy(arr, array, arr.Length);
                    array[index] = value;
                    result = array;
                }
            }
            return result;
        }
        public static void RemoveAt<T>(this T[] arr, int index)
        {
            for (int i = index; i < arr.Length - 1; i++)
            {
                arr[i] = arr[i + 1];
            }
            arr[arr.Length - 1] = default(T);
        }


        public static Stat OrgStat(BattleChar bc)
        {
            Stat G_get_stat = default(Stat);

            {
                G_get_stat = bc.Info.OriginStat;
                Stat stat = default(Stat);
                PerStat perStat = default(PerStat);
                if (bc.Info.Ally)
                {
                    G_get_stat += bc.Info.AllyLevelPlusStat(0);
                    stat += PlayData.TSavedata.PartyPlusStat;
                    if (PlayData.TSavedata.SpRule != null)
                    {
                        stat += PlayData.TSavedata.SpRule.PlusStat;
                    }
                    foreach (Item_Passive item_Passive in PlayData.Passive)
                    {
                        if (item_Passive != null)
                        {
                            stat += item_Passive.ItemScript.PlusStat;
                            perStat += item_Passive.ItemScript.PlusPerStat;
                            stat.PlusDiscard = 0;
                            stat.PlusDraw = 0;
                            stat.MPR = 0;
                            stat.spd = 0;
                        }
                    }
                    if (PlayData.TSavedata.HopeModeLevel == 1)
                    {
                        perStat.MaxHP += 50;
                        stat.hit += 10f;
                    }
                    else if (PlayData.TSavedata.HopeModeLevel == 2)
                    {
                        perStat.MaxHP += 66;
                        perStat.Damage += 25;
                        perStat.Heal += 25;
                        stat.hit += 10f;
                    }
                    else if (PlayData.TSavedata.HopeModeLevel == 3)
                    {
                        perStat.MaxHP += 100;
                        perStat.Damage += 33;
                        perStat.Heal += 33;
                        stat.hit += 10f;
                    }
                    for (int i = 0; i < PlayData.TSavedata.Party.Count; i++)
                    {
                        if (PlayData.TSavedata.Party[i] != null && PlayData.TSavedata.Party[i].Equip != null)
                        {
                            for (int j = 0; j < PlayData.TSavedata.Party[i].Equip.Count; j++)
                            {
                                if (bc.Info.GetData != null && PlayData.TSavedata.Party[i].Equip[j] != null)
                                {
                                    (PlayData.TSavedata.Party[i].Equip[j] as Item_Equip).ItemScript.EquipPlusStat_Team(bc.Info, ref stat, ref perStat);
                                }
                            }
                        }
                    }
                    if (bc.Info.Passive != null)
                    {
                        stat += bc.Info.Passive.PlusStat;
                        perStat += bc.Info.Passive.PlusPerStat;
                    }
                    for (int k = 0; k < bc.Info.Equip.Count; k++)
                    {
                        if (bc.Info.Equip[k] != null && bc.Info.Equip[k] is Item_Equip)
                        {
                            Item_Equip item_Equip = bc.Info.Equip[k] as Item_Equip;
                            if (item_Equip.ItemScript.MyChar == null)
                            {
                                item_Equip.ItemScript.MyChar = bc.Info;
                                item_Equip.Curse.MyChar = bc.Info;
                                item_Equip.Curse.Init();
                            }
                            item_Equip.ItemScript.MyChar = bc.Info;
                            if (!item_Equip.InitFlag)
                            {
                                item_Equip.ItemScript.Init();
                                item_Equip.InitFlag = true;
                            }
                            stat += item_Equip.ItemScript.PlusStat;
                            stat += item_Equip.Enchant.EnchantData.PlusStat;
                            stat += item_Equip.Curse.PlusStat;
                            perStat += item_Equip.ItemScript.PlusPerStat;
                            perStat += item_Equip.Enchant.EnchantData.PlusPerStat;
                            perStat += item_Equip.Curse.PlusPerStat;
                            item_Equip._Isidentify = true;
                            if (PlayData.Passive.Find((Item_Passive a) => a.itemkey == GDEItemKeys.Item_Passive_SixSixSix) != null && item_Equip.IsCurse)
                            {
                                stat.atk += 2f;
                                stat.reg += 2f;
                                stat.DeadImmune += 25;
                            }
                        }
                    }
                    if (bc.Info.Hp <= 0)
                    {
                        stat.AggroPer += 20;
                    }
                }

                stat.atk += Misc.PerToNum(G_get_stat.atk, (float)perStat.Damage);
                stat.reg += Misc.PerToNum(G_get_stat.reg, (float)perStat.Heal);
                stat.maxhp += (int)Misc.PerToNum((float)G_get_stat.maxhp, (float)perStat.MaxHP);
                G_get_stat += stat;
                G_get_stat = Character.StatC(G_get_stat);
                /*
                if (bc.Info.BeforeMaxHP != 0 && bc.Info.BeforeMaxHP != G_get_stat.maxhp)
                {
                    if (bc.Info.Hp >= 1)
                    {
                        int num = (int)Math.Round((double)Misc.PerToNum((float)bc.Info.Hp, Misc.NumToPer((float)bc.Info.BeforeMaxHP, (float)G_get_stat.maxhp)));
                        bc.Info.Hp = ((num > 1) ? num : 1);
                    }
                    if (BattleSystem.instance != null && bc.Info.GetBattleChar != null)
                    {
                        int num2 = (int)Misc.PerToNum((float)bc.Info.GetBattleChar.Recovery, Misc.NumToPer((float)bc.Info.BeforeMaxHP, (float)G_get_stat.maxhp));
                        if (bc.Info.GetBattleChar.Recovery >= 1)
                        {
                            bc.Info.GetBattleChar.Recovery = ((num2 > 1) ? num2 : 1);
                        }
                        else
                        {
                            bc.Info.GetBattleChar.Recovery = num2;
                        }
                    }
                }
                */
            }
            return G_get_stat;
        }

    }
    public class Bcl_Fairy_sum_p : Buff, IP_Dead
    {

        public void Dead()
        {
            if (this.BChar is BattleAlly ally)
            {
                //AllySummon.RemoveAlly(ally);
                BattleSystem.instance.StartCoroutine(this.RemoveUI());
            }
        }

        public IEnumerator RemoveUI()
        {
            yield return new WaitForSeconds(1f);
            BattleAlly ally = this.BChar as BattleAlly;
            if (ally != null)
            {
                AllySummon.RemoveAlly(ally);
            }
            yield break;
        }
        public BattleChar master = new BattleChar();
    }


    public class CommonData
    {
        // Token: 0x06000013 RID: 19 RVA: 0x00002258 File Offset: 0x00000458
        public static FairySaveData GetFairyGameData()
        {
            FairySaveData fairySaveData = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            bool flag = fairySaveData == null;
            if (flag)
            {
                fairySaveData = new FairySaveData();
                PlayData.TSavedata.AddCustomValue(fairySaveData);
            }
            return fairySaveData;
        }
        public static void FairySystemIReturn(Type targetType, List<object> list)
        {
            FairySaveData fairygameData = CommonData.GetFairyGameData();
            list.AddRange(fairygameData.handlerList);
        }



        public static void SwitchFairy(FairyEnum fairyEnum)
        {
            FairyConfig fairyConfig = Fairys.config[fairyEnum];
            //Debug.Log("切换为："+fairyConfig.name);
            CommonData.SetDliegen(fairyConfig.fairytype);
            FairyBase item = Activator.CreateInstance(fairyConfig.handleClass) as FairyBase;

            FairySaveData save = CommonData.GetFairyGameData();


            foreach (FairyEnum fairyenum in save.handlerEnums)
            {
                CharCtrl_Persica.shop.MakeItem(fairyenum);
            }
            Traverse.Create(CharCtrl_Persica.shop.buyWindow).Field("TargetUpdate").SetValue(true);
            save.handlerEnums.RemoveAll(x => true);
            save.handlerEnums.Add(fairyEnum);

            /*
            if (save.handlerList.Count>0)
            {
                save.handlerList[0]=item;
            }
            else
            {
                //save.handlerList.RemoveAll(x => true);
                save.handlerList.Add(item);
            }
            */
            foreach (FairyBase fairyBase in save.handlerList2)
            {
                fairyBase.RemoveEffect();
            }
            save.handlerList2.Clear();
            save.handlerList2.Add(item);
            item.FairyInit();


        }
        public static void AddFairy(FairyEnum fairyEnum)
        {
            FairyConfig fairyConfig = Fairys.config[fairyEnum];
            //Debug.Log("切换为："+fairyConfig.name);
            

            FairyBase item = Activator.CreateInstance(fairyConfig.handleClass) as FairyBase;

            FairySaveData save = CommonData.GetFairyGameData();

            Traverse.Create(CharCtrl_Persica.shop.buyWindow).Field("TargetUpdate").SetValue(true);
            //save.handlerEnums.RemoveAll(x => true);
            save.handlerEnums.Add(fairyEnum);

            //save.handlerList2.Clear();
            save.handlerList2.Add(item);
            item.FairyInit();

            bool flag = true;
            foreach (FairyEnum fairyEnum1 in save.handlerEnums)
            {
                FairyConfig fairyConfig1 = Fairys.config[fairyEnum1];
                if(!fairyConfig1.fairytype)
                {
                    flag = false;
                    break;
                }
            }
            CommonData.SetDliegen(flag);
        }


        public static IEnumerator SetPersicaAndDliegen()
        {
            yield return new WaitForSeconds(0.25f);


            FairySaveData save = CommonData.GetFairyGameData();

            if (save.handlerEnums.Count > 0)
            {
                //Debug.Log("检测点1");
                bool flag = true;
                foreach (FairyEnum fairyEnum1 in save.handlerEnums)
                {
                    FairyConfig fairyConfig1 = Fairys.config[fairyEnum1];
                    if (!fairyConfig1.fairytype)
                    {
                        flag = false;
                        break;
                    }
                }
                CommonData.SetDliegen(flag);


            }
            if(true||save.running)
            {
                MapChar_Persica.Init3();
            }
            yield break;
        }
        public static IEnumerator DelaySetDliegen()
        {
            yield return new WaitForSeconds(0.25f);


            FairySaveData save = CommonData.GetFairyGameData();

            if (save.handlerEnums.Count > 0)
            {
                //Debug.Log("检测点1");
                bool flag = true;
                foreach (FairyEnum fairyEnum1 in save.handlerEnums)
                {
                    FairyConfig fairyConfig1 = Fairys.config[fairyEnum1];
                    if (!fairyConfig1.fairytype)
                    {
                        flag = false;
                        break;
                    }
                }
                CommonData.SetDliegen(flag);


            }
            yield break;
        }
        public static void SetDliegen(bool flag)
        {
            if (flag)
            {
                //Debug.Log("检测点2");
                if (DroneSystem.Drone != null)
                {
                    UnityEngine.Object.Destroy(DroneSystem.Drone);
                }
                //Debug.Log("检测点3");
                MapChar_Strategy_Fairy_Fliegen.Init();//CommonData.stage;
            }
            else
            {
                //Debug.Log("检测点2");
                if (DroneSystem.Drone != null)
                {
                    UnityEngine.Object.Destroy(DroneSystem.Drone);
                }
                //Debug.Log("检测点3");
                MapChar_Battle_Fairy_Fliegen.Init();
            }
        }

        // Token: 0x06000014 RID: 20 RVA: 0x00002294 File Offset: 0x00000494
        public static void FairyBattleSystemIReturn(Type targetType, List<object> list)
        {
            FairySaveData hetgameData = CommonData.GetFairyGameData();
            list.AddRange(hetgameData.handlerList);
        }

        // Token: 0x06000015 RID: 21 RVA: 0x000022B8 File Offset: 0x000004B8
        public static string ReplaceKeyWords(string oldStr, Dictionary<string, string> dic)
        {
            foreach (KeyValuePair<string, string> keyValuePair in dic)
            {
                string oldValue = "&" + keyValuePair.Key;
                oldStr = oldStr.Replace(oldValue, keyValuePair.Value);
            }
            return oldStr;
        }

        public static void BuffRemoveField(BattleChar bc, string key)
        {
            List<Buff> list = bc.Info.Buffs_Field.FindAll(a => a.BuffData.Key == key);
            foreach (Buff buff in list)
            {
                if (buff.BuffIcon != null)
                {
                    UnityEngine.Object.Destroy(buff.BuffIcon);

                }
            }
            bc.Info.Buffs_Field.RemoveAll(a => a.BuffData.Key == key);
            GamepadManager.RefreshTarget();
        }
        public static void BuffRemoveField(Character bcInfo, string key)
        {
            List<Buff> list = bcInfo.Buffs_Field.FindAll(a => a.BuffData.Key == key);
            foreach (Buff buff in list)
            {
                if (buff.BuffIcon != null)
                {
                    UnityEngine.Object.Destroy(buff.BuffIcon);

                }
            }
            bcInfo.Buffs_Field.RemoveAll(a => a.BuffData.Key == key);
            GamepadManager.RefreshTarget();
        }
        public static string GetTranslation(string key)
        {
            try
            {
                string str = ModManager.getModInfo("GFL_Fairy").localizationInfo.SyetemLocalizationUpdate(key);// "&fairy激活:已扣除&num金币";
                return str;
            }
            catch { }
            return string.Empty;
        }
        public static string FairyName(string key)
        {
            try
            {
                //string str = ModManager.getModInfo("GFL_Fairy").localizationInfo.SyetemLocalizationUpdate("GFL_Fairy/System/ActiveFail");//"&fairy激活失败：金币不足";
                string key2 = "GFL_Fairy/Name/" + key;
                string str = ModManager.getModInfo("GFL_Fairy").localizationInfo.SyetemLocalizationUpdate(key2);// "&fairy激活:已扣除&num金币";
                return str;
            }
            catch { }
            return string.Empty;
        }
        public static void FieldUseGoldTextOut(FairyConfig config)
        {

            string str = CommonData.GetTranslation("GFL_Fairy/System/UseGold");
            Vector2 v = FieldSystem.instance.MainCamera.WorldToScreenPoint(FieldSystem.instance.Player.transform.position);
            v.x -= (float)(Screen.width / 2);
            v.y -= (float)(Screen.height / 2);
            v.y += 100f;
            EffectView.SimpleTextout(v, str.Replace("&price", config.use_price.ToString()), true, 0.6f, false, 1f);
        }
        // Token: 0x04000005 RID: 5
        public const string MOD_KEY = "GF_Fairy";

        // Token: 0x04000006 RID: 6
        public const string DEFAULT_ASSETS = "fairy";

        public static StageSystem stage = null;

        // Token: 0x04000007 RID: 7
        //public const int AGL_ATK_TIMES = 3;

        // Token: 0x04000008 RID: 8
        //public const string PHONE_ITEM_KEY = "I_GFHET_Phone";
    }

    [Serializable]
    public class FairySaveData : CustomValue
    {
        // Token: 0x17000001 RID: 1
        // (get) Token: 0x06000011 RID: 17 RVA: 0x00002188 File Offset: 0x00000388
        [XmlIgnore]
        public List<FairyBase> handlerList
        {
            get
            {
                bool flag = this._handlerList.Count <= 0 && this.handlerEnums.Count > 0;
                if (flag)
                {
                    //Debug.Log("重置_handlerList");
                    foreach (FairyEnum key in this.handlerEnums)
                    {
                        FairyConfig fairysConfig = Fairys.config[key];
                        FairyBase item = Activator.CreateInstance(fairysConfig.handleClass) as FairyBase;
                        this._handlerList.Add(item);
                    }
                }
                return this._handlerList;
            }
        }
        [XmlIgnore]
        public List<FairyBase> handlerList2
        {
            get
            {
                return this._handlerList;
            }
        }
        // Token: 0x04000003 RID: 3
        public List<FairyEnum> handlerEnums = new List<FairyEnum>();
        public bool running = false;

        public bool saveFlag_0 = false;
        public int saveNum_0 = 0;

        public bool FairyCommandRelicAdded = false;

        public List<List<object>> saveList = new List<List<object>>();

        public List<FairyEnum> batchFairyEnums = new List<FairyEnum>();
        // Token: 0x04000004 RID: 4
        [XmlIgnore]
        private List<FairyBase> _handlerList = new List<FairyBase>();
    }


    [Serializable]
    public abstract class FairyBase
    {
        // Token: 0x0600001E RID: 30
        public abstract FairyEnum GetFairyEnum();
        public abstract void FairyInit();
        
        public virtual List<object> GetSaveData()
        {
            FairySaveData save = CommonData.GetFairyGameData();
            foreach(List<object> list in save.saveList)
            {
                if ((FairyEnum)list[0] == this.GetFairyEnum())
                {
                    return list; 
                }
            }
            return new List<object>();
        }
        /*
        public virtual void ClickFliegen()
        {
            FairyActiveData.SwitchTrigger();
        }
        */
        public virtual void ActiveFairy()//策略妖精专用
        {
            //Debug.Log(" ");
            /*
            if(!this.preventChargeMark)
            {
                PlayData.BattleLucy.BuffAdd("B_GFL_Stage_Mark_0", PlayData.BattleLucy);
            }
            */
            //Debug.Log(" ");
        }
        public virtual void RemoveEffect()
        {
            //Debug.Log(" ");
            CommonData.BuffRemoveField(PlayData.BattleLucy, "B_GFL_Stage_Mark_0");
            //Debug.Log(" ");
        }
        public abstract bool ActiveOnce { get; set; }
        public abstract bool ActiveAlways { get; set; }
        //public bool actived = false;

        //public int activeNum = 0;
        public bool activeInBattle = false;
        //public bool preventChargeMark = false;
    }

    public class Sex_Fairy_View : Skill_Extended,IP_SkillToolTip_Input
    {
        public void SkillToolTip_Input()
        {
            if (ModManager.getModInfo("GFL_Fairy").GetSetting<ToggleSetting>("MulryFairy").Value)
            {
                int n = this.MySkill.MySkill.PlusKeyWords.FindIndex(a => a.Key == "KeyWord_Fairy_0");
                if (n != -1)
                {
                    this.MySkill.MySkill.PlusKeyWords[n] = new GDESkillKeywordData("KeyWord_Fairy_1");
                }
            }
            else
            {
                int n = this.MySkill.MySkill.PlusKeyWords.FindIndex(a => a.Key == "KeyWord_Fairy_1");
                if (n != -1)
                {
                    this.MySkill.MySkill.PlusKeyWords[n] = new GDESkillKeywordData("KeyWord_Fairy_0");
                }
            }
        }
        public override string DescExtended(string desc)
        {

            
            if (!this.viewInShop && CommonData.GetFairyGameData().handlerEnums.Count > 0)
            {
                FairySaveData save = CommonData.GetFairyGameData();

                //FairyConfig fairyConfig = Fairys.config[CommonData.GetFairyGameData().handlerEnums[0]];

                foreach (FairyEnum fairyenum in save.handlerEnums)
                {
                    FairyConfig config = Fairys.config[fairyenum];
                    if(config.previewSkillKey==this.MySkill.MySkill.KeyID)
                    {
                        return base.DescExtended(desc).Replace("&use_price", config.use_price.ToString());
                    }
                }
                
            }

                FairyConfig fairyConfig = Fairys.config[this.fairyEnum];
                return base.DescExtended(desc).Replace("&use_price", fairyConfig.use_price.ToString());
            
        }

        public FairyEnum fairyEnum = new FairyEnum();
        public bool viewInShop = false;
    }
    public class Sex_Trigger : Skill_Extended
    {
        public override string DescExtended(string desc)
        {

            FairyConfig fairyConfig = Fairys.config[this.fairyEnum];

            string str1 = string.Empty;
            if (fairyConfig.fairytype)
            {
                str1 = CommonData.GetTranslation("GFL_Fairy/System/Trigger_Word_1");
                str1 = str1.Replace("&pricePerTime",fairyConfig.use_price.ToString());
            }
            else
            {
                str1= CommonData.GetTranslation("GFL_Fairy/System/Trigger_Word_2");
            }

            return base.DescExtended(desc).Replace("&activeType",str1).Replace("&FairyName", fairyConfig.name);
        }
        public override bool ButtonSelectTerms()
        {
            return this.canChoose;
        }
        public FairyEnum fairyEnum = new FairyEnum();
        public FairyBase fairyBase_R = null;
        public bool canChoose = true;
    }
    public class Bcl_Stage_Mark_0 : Buff

    {

    }


    public static class FairyActiveData
    {
        /*
         public static void SwitchTrigger()
        {
            List<Skill> list = new List<Skill>();
            int price_sum = 0;

            FairySaveData save = CommonData.GetFairyGameData();
            foreach(FairyBase fairyBase in save.handlerList)
            {
                FairyConfig config = Fairys.config[fairyBase.GetFairyEnum()];

                BattleChar battlechar = PlayData.BattleLucy;
                //Debug.Log("S_GFL_" + config.key + "_Trigger_1");
                //Skill skill1 = Skill.TempSkill("S_GFL_" + config.key + "_Trigger_1", battlechar, PlayData.TempBattleTeam);//Skill.TempSkill("S_GFL_Fairy_Trigger_1", battlechar, PlayData.TempBattleTeam);
                Skill skill1 = Skill.TempSkill("S_GFL_Fairy_Trigger_1", battlechar, PlayData.TempBattleTeam);
                Skill skill1_a = Skill.TempSkill("S_GFL_" + config.key + "_Trigger_1", battlechar, PlayData.TempBattleTeam);
                skill1.Image_Skill = skill1_a.Image_Skill;
                skill1.MySkill.PlusSkillView = "S_GFL_" + config.key + "_View";
                foreach (Skill_Extended sex in skill1.AllExtendeds)
                {
                    if (sex is Sex_Trigger sext)
                    {
                        sext.fairyEnum = fairyBase.GetFairyEnum();
                        sext.fairyBase_R = fairyBase;
                        if (config.fairytype && PlayData.Gold < config.use_price)
                        {
                            sext.canChoose = false;
                        }
                    }
                }
                //Skill skill2 = Skill.TempSkill("S_GFL_" + config.key + "_Trigger_2", battlechar, PlayData.TempBattleTeam);
                Skill skill2 = Skill.TempSkill("S_GFL_Fairy_Trigger_2", battlechar, PlayData.TempBattleTeam);
                Skill skill2_a = Skill.TempSkill("S_GFL_" + config.key + "_Trigger_2", battlechar, PlayData.TempBattleTeam);
                skill2.Image_Skill = skill2_a.Image_Skill;
                skill2.MySkill.PlusSkillView = "S_GFL_" + config.key + "_View";
                foreach (Skill_Extended sex in skill2.AllExtendeds)
                {
                    if (sex is Sex_Trigger sext)
                    {
                        sext.fairyEnum = fairyBase.GetFairyEnum();
                        sext.fairyBase_R = fairyBase;
                    }
                }
                //Skill skill3 = Skill.TempSkill("S_GFL_" + config.key + "_Trigger_3", battlechar, PlayData.TempBattleTeam);
                Skill skill3 = Skill.TempSkill("S_GFL_Fairy_Trigger_3", battlechar, PlayData.TempBattleTeam);
                Skill skill3_a = Skill.TempSkill("S_GFL_" + config.key + "_Trigger_3", battlechar, PlayData.TempBattleTeam);
                skill3.Image_Skill = skill3_a.Image_Skill;
                skill3.MySkill.PlusSkillView="S_GFL_" + config.key + "_View";
                foreach (Skill_Extended sex in skill3.AllExtendeds)
                {
                    if (sex is Sex_Trigger sext)
                    {
                        sext.fairyEnum = fairyBase.GetFairyEnum();
                        sext.fairyBase_R = fairyBase;
                    }
                }
                //Skill skill4 = Skill.TempSkill("S_GFL_" + config.key + "_Trigger_4", battlechar, PlayData.TempBattleTeam);
                Skill skill4 = Skill.TempSkill("S_GFL_Fairy_Trigger_4", battlechar, PlayData.TempBattleTeam);
                Skill skill4_a = Skill.TempSkill("S_GFL_" + config.key + "_Trigger_4", battlechar, PlayData.TempBattleTeam);
                skill4.Image_Skill = skill4_a.Image_Skill;
                skill4.MySkill.PlusSkillView = "S_GFL_" + config.key + "_View";
                foreach (Skill_Extended sex in skill4.AllExtendeds)
                {
                    if (sex is Sex_Trigger sext)
                    {
                        sext.fairyEnum = fairyBase.GetFairyEnum();
                        sext.fairyBase_R = fairyBase;
                        sext.canChoose = false;
                    }
                }


                if (config.fairytype)//为策略妖精
                {
                    if (!fairyBase.ActiveOnce)
                    {
                        list.Add(skill1);
                    }
                    else
                    {
                        list.Add(skill4);
                    }

                }
                else//为战斗妖精
                {
                    if (!fairyBase.ActiveOnce && !fairyBase.ActiveAlways)
                    {
                        list.Add(skill1);

                        list.Add(skill3);

                    }
                    else if (fairyBase.ActiveOnce && !fairyBase.ActiveAlways)
                    {
                        list.Add(skill2);

                        list.Add(skill3);
                        price_sum += config.use_price;
                    }
                    else if (fairyBase.ActiveAlways)
                    {
                        list.Add(skill2);
                        price_sum += config.use_price;
                    }
                }
            }



            if (list.Count > 0)
            {
                string str = ModManager.getModInfo("GFL_Fairy").localizationInfo.SyetemLocalizationUpdate("GFL_Fairy/System/Trigger_Text_1");
                str=str.Replace("&num",price_sum.ToString());

                FieldSystem.DelayInput(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(FairyActiveData.ChooseTrigger), str, true, false, true, false, false));//待翻译
            }
        }
        */
        public static void SwitchTrigger()
        {
            List<Skill> list = new List<Skill>();
            int price_sum = 0;
            int battleFairyNum = 0;

            FairySaveData save = CommonData.GetFairyGameData();
            foreach(FairyBase fairyBase in save.handlerList)
            {
                FairyConfig config = Fairys.config[fairyBase.GetFairyEnum()];

                BattleChar battlechar = PlayData.BattleLucy;
                //Debug.Log("S_GFL_" + config.key + "_Trigger_1");
                //Skill skill1 = Skill.TempSkill("S_GFL_" + config.key + "_Trigger_1", battlechar, PlayData.TempBattleTeam);//Skill.TempSkill("S_GFL_Fairy_Trigger_1", battlechar, PlayData.TempBattleTeam);
                Skill skill1 = Skill.TempSkill("S_GFL_" + config.key + "_View", battlechar, PlayData.TempBattleTeam);
                foreach (Skill_Extended sex in skill1.AllExtendeds)
                {
                    if (sex is Sex_Fairy_View sext)
                    {
                        sext.fairyEnum = fairyBase.GetFairyEnum();
                    }
                }
                list.Add(skill1);


                if (config.fairytype)//为策略妖精
                {

                }
                else//为战斗妖精
                {
                    battleFairyNum++;


                    string str = skill1.MySkill.Name;
                    if (fairyBase.ActiveAlways)
                    {
                        str = str +" - "+ CommonData.GetTranslation("GFL_Fairy/System/Trigger_Text_ActiveAlways"); 
                        price_sum += config.use_price;
                    }
                    else if (fairyBase.ActiveOnce)
                    {
                        str = str + " - " + CommonData.GetTranslation("GFL_Fairy/System/Trigger_Text_ActiveOnce");
                        price_sum += config.use_price;
                    }
                    else
                    {
                        str = str + " - " + CommonData.GetTranslation("GFL_Fairy/System/Trigger_Text_InActive");
                    }
                    Traverse.Create(skill1.MySkill).Field("_Name").SetValue(str);
                }
            }


            if(battleFairyNum>1)
            {
                Skill skill = Skill.TempSkill("S_GFL_Fairy_BatchSet_1", PlayData.BattleLucy, PlayData.TempBattleTeam);
                list.Insert(0, skill);
            }


            if (list.Count > 0)
            {
                string str = ModManager.getModInfo("GFL_Fairy").localizationInfo.SyetemLocalizationUpdate("GFL_Fairy/System/Trigger_Text_1");
                str=str.Replace("&num",price_sum.ToString());

                FieldSystem.instance.StartCoroutine(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(FairyActiveData.Delay_ChooseFairyAfter), str, true, false, true, false, false));//待翻译
                
            }
        }



        public static void Delay_ChooseFairyAfter(SkillButton Mybutton)
        {
            FairySaveData save = CommonData.GetFairyGameData();
            List<Skill> list = new List<Skill>();


            if(Mybutton.Myskill.MySkill.KeyID== "S_GFL_Fairy_BatchSet_1")
            {
                //FairyConfig config = Fairys.config[fairybase.GetFairyEnum()];//获取config的语句

                list.Add(Skill.TempSkill("S_GFL_Fairy_BatchSet_1_1", PlayData.BattleLucy, PlayData.TempBattleTeam));
                list.Add(Skill.TempSkill("S_GFL_Fairy_BatchSet_1_2", PlayData.BattleLucy, PlayData.TempBattleTeam));
                list.Add(Skill.TempSkill("S_GFL_Fairy_BatchSet_1_3", PlayData.BattleLucy, PlayData.TempBattleTeam));
                list.Add(Skill.TempSkill("S_GFL_Fairy_BatchSet_1_4", PlayData.BattleLucy, PlayData.TempBattleTeam));

                FieldSystem.instance.StartCoroutine(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(FairyActiveData.Delay_1_ChooseBatchMethodAfter), ScriptLocalization.System_SkillSelect.EffectSelect, true, false, true, false, false));//待翻译

            }
            else
            {
                FairyBase fairyBase = null;
                foreach (Skill_Extended sex in Mybutton.Myskill.AllExtendeds)
                {
                    if (sex is Sex_Fairy_View sexv)
                    {
                        foreach (FairyBase fairyBase2 in save.handlerList)
                        {
                            if (fairyBase2.GetFairyEnum() == sexv.fairyEnum)
                            {
                                fairyBase = fairyBase2;
                            }
                        }
                    }
                }
                if (fairyBase == null)
                {
                    return;
                }


                FairyConfig config = Fairys.config[fairyBase.GetFairyEnum()];

                BattleChar battlechar = PlayData.BattleLucy;
                //Debug.Log("S_GFL_" + config.key + "_Trigger_1");
                //Skill skill1 = Skill.TempSkill("S_GFL_" + config.key + "_Trigger_1", battlechar, PlayData.TempBattleTeam);//Skill.TempSkill("S_GFL_Fairy_Trigger_1", battlechar, PlayData.TempBattleTeam);
                Skill skill1 = Skill.TempSkill("S_GFL_Fairy_Trigger_1", battlechar, PlayData.TempBattleTeam);
                Skill skill1_a = Skill.TempSkill("S_GFL_" + config.key + "_Trigger_1", battlechar, PlayData.TempBattleTeam);
                skill1.Image_Skill = skill1_a.Image_Skill;
                skill1.MySkill.PlusSkillView = "S_GFL_" + config.key + "_View";
                foreach (Skill_Extended sex in skill1.AllExtendeds)
                {
                    if (sex is Sex_Trigger sext)
                    {
                        sext.fairyEnum = fairyBase.GetFairyEnum();
                        sext.fairyBase_R = fairyBase;
                        if (config.fairytype && /*PlayData.Gold*/Fairy_Command_Method.RemainNum < config.use_price)
                        {
                            sext.canChoose = false;
                        }
                    }
                }
                //Skill skill2 = Skill.TempSkill("S_GFL_" + config.key + "_Trigger_2", battlechar, PlayData.TempBattleTeam);
                Skill skill2 = Skill.TempSkill("S_GFL_Fairy_Trigger_2", battlechar, PlayData.TempBattleTeam);
                Skill skill2_a = Skill.TempSkill("S_GFL_" + config.key + "_Trigger_2", battlechar, PlayData.TempBattleTeam);
                skill2.Image_Skill = skill2_a.Image_Skill;
                skill2.MySkill.PlusSkillView = "S_GFL_" + config.key + "_View";
                foreach (Skill_Extended sex in skill2.AllExtendeds)
                {
                    if (sex is Sex_Trigger sext)
                    {
                        sext.fairyEnum = fairyBase.GetFairyEnum();
                        sext.fairyBase_R = fairyBase;
                    }
                }
                //Skill skill3 = Skill.TempSkill("S_GFL_" + config.key + "_Trigger_3", battlechar, PlayData.TempBattleTeam);
                Skill skill3 = Skill.TempSkill("S_GFL_Fairy_Trigger_3", battlechar, PlayData.TempBattleTeam);
                Skill skill3_a = Skill.TempSkill("S_GFL_" + config.key + "_Trigger_3", battlechar, PlayData.TempBattleTeam);
                skill3.Image_Skill = skill3_a.Image_Skill;
                skill3.MySkill.PlusSkillView = "S_GFL_" + config.key + "_View";
                foreach (Skill_Extended sex in skill3.AllExtendeds)
                {
                    if (sex is Sex_Trigger sext)
                    {
                        sext.fairyEnum = fairyBase.GetFairyEnum();
                        sext.fairyBase_R = fairyBase;
                    }
                }
                //Skill skill4 = Skill.TempSkill("S_GFL_" + config.key + "_Trigger_4", battlechar, PlayData.TempBattleTeam);
                Skill skill4 = Skill.TempSkill("S_GFL_Fairy_Trigger_4", battlechar, PlayData.TempBattleTeam);
                Skill skill4_a = Skill.TempSkill("S_GFL_" + config.key + "_Trigger_4", battlechar, PlayData.TempBattleTeam);
                skill4.Image_Skill = skill4_a.Image_Skill;
                skill4.MySkill.PlusSkillView = "S_GFL_" + config.key + "_View";
                foreach (Skill_Extended sex in skill4.AllExtendeds)
                {
                    if (sex is Sex_Trigger sext)
                    {
                        sext.fairyEnum = fairyBase.GetFairyEnum();
                        sext.fairyBase_R = fairyBase;
                        sext.canChoose = false;
                    }
                }


                if (config.fairytype)//为策略妖精
                {
                    if (!fairyBase.ActiveOnce)
                    {
                        list.Add(skill1);
                    }
                    else
                    {
                        list.Add(skill4);
                    }

                }
                else//为战斗妖精
                {
                    if (!fairyBase.ActiveOnce && !fairyBase.ActiveAlways)
                    {
                        list.Add(skill1);

                        list.Add(skill3);

                    }
                    else if (fairyBase.ActiveOnce && !fairyBase.ActiveAlways)
                    {
                        list.Add(skill2);

                        list.Add(skill3);
                    }
                    else if (fairyBase.ActiveAlways)
                    {
                        list.Add(skill2);
                    }
                }





                if (list.Count > 0)
                {
                    FieldSystem.instance.StartCoroutine(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(FairyActiveData.Delay_0_SetFairyStat), ScriptLocalization.System_SkillSelect.EffectSelect, true, false, true, false, false));//待翻译
                }
            }
        }

        public static void Delay_0_SetFairyStat(SkillButton Mybutton)
        {
            FairyBase fairyBase = null;
            foreach (Skill_Extended sex in Mybutton.Myskill.AllExtendeds)
            {
                if (sex is Sex_Trigger sext)
                {

                    fairyBase = sext.fairyBase_R;
                }
            }
            if (fairyBase == null)
            {
                return;
            }
            FairyConfig config = Fairys.config[fairyBase.GetFairyEnum()];
            if (config.fairytype)//为策略妖精
            {
                if (Mybutton.Myskill.MySkill.KeyID.Contains("Trigger_1"))
                {
                    //fairyBase.ActiveOnce = true;

                    //PlayData.Gold -= config.use_price;
                    fairyBase.ActiveFairy();
                }
            }
            else
            {
                if (Mybutton.Myskill.MySkill.KeyID.Contains("Trigger_1"))
                {
                    fairyBase.ActiveOnce = true;
                    fairyBase.ActiveAlways = false;
                }
                else if (Mybutton.Myskill.MySkill.KeyID.Contains("Trigger_2"))
                {
                    fairyBase.ActiveOnce = false;
                    fairyBase.ActiveAlways = false;
                }
                else if (Mybutton.Myskill.MySkill.KeyID.Contains("Trigger_3"))
                {
                    fairyBase.ActiveOnce = true;
                    fairyBase.ActiveAlways = true;
                }
            }

        }

        public static void Delay_1_ChooseBatchMethodAfter(SkillButton Mybutton)
        {
            if (Mybutton.Myskill.MySkill.KeyID == "S_GFL_Fairy_BatchSet_1_4")
            {
                FairyActiveData.Delay_1a_SwitchBatchSetting();
            }
            else
            {
                FairySaveData save = CommonData.GetFairyGameData();
                if (Mybutton.Myskill.MySkill.KeyID == "S_GFL_Fairy_BatchSet_1_1")
                {
                    foreach (FairyBase fairyBase in save.handlerList)
                    {
                        if (save.batchFairyEnums.Contains(fairyBase.GetFairyEnum()))
                        {
                            fairyBase.ActiveOnce = true;
                            fairyBase.ActiveAlways = false;
                        }
                    }
                }
                else if (Mybutton.Myskill.MySkill.KeyID == "S_GFL_Fairy_BatchSet_1_2")
                {
                    foreach (FairyBase fairyBase in save.handlerList)
                    {
                        if (save.batchFairyEnums.Contains(fairyBase.GetFairyEnum()))
                        {
                            fairyBase.ActiveOnce = true;
                            fairyBase.ActiveAlways = true;
                        }
                    }
                }
                else if (Mybutton.Myskill.MySkill.KeyID == "S_GFL_Fairy_BatchSet_1_3")
                {
                    foreach (FairyBase fairyBase in save.handlerList)
                    {
                        if (save.batchFairyEnums.Contains(fairyBase.GetFairyEnum()))
                        {
                            fairyBase.ActiveOnce = false;
                            fairyBase.ActiveAlways = false;
                        }
                    }
                }
            }   
            
        }

        public static void Delay_1a_SwitchBatchSetting()
        {
            FairySaveData save = CommonData.GetFairyGameData();
            List<Skill> list = new List<Skill>();
            list.Add(Skill.TempSkill("S_GFL_Fairy_BatchSet_1_4_1", PlayData.BattleLucy, PlayData.TempBattleTeam));
            list.Add(Skill.TempSkill("S_GFL_Fairy_BatchSet_1_4_2", PlayData.BattleLucy, PlayData.TempBattleTeam));

            foreach (FairyBase fairyBase in save.handlerList)
            {
                FairyConfig config = Fairys.config[fairyBase.GetFairyEnum()];
                if(config.fairytype)
                {
                    continue;
                }
                BattleChar battlechar = PlayData.BattleLucy;
                Skill skill1 = Skill.TempSkill("S_GFL_" + config.key + "_View", battlechar, PlayData.TempBattleTeam);

                foreach (Skill_Extended sex in skill1.AllExtendeds)
                {
                    if (sex is Sex_Fairy_View sext)
                    {
                        sext.fairyEnum = fairyBase.GetFairyEnum();
                    }
                }
                list.Add(skill1);

                    string str = skill1.MySkill.Name;


                if (save.batchFairyEnums.Contains(fairyBase.GetFairyEnum()))
                {
                    str = str + " - " + CommonData.GetTranslation("GFL_Fairy/System/Trigger_Text_BatchOn");
                }
                else
                {
                    str = str + " - " + CommonData.GetTranslation("GFL_Fairy/System/Trigger_Text_BatchOff");
                }
                Traverse.Create(skill1.MySkill).Field("_Name").SetValue(str);


            }

            FieldSystem.instance.StartCoroutine(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(FairyActiveData.Delay_1_4_ChooseSwitchFairyBatchStatAfter), ScriptLocalization.System_SkillSelect.EffectSelect, true, false, true, false, false));//待翻译

        }
        public static void Delay_1_4_ChooseSwitchFairyBatchStatAfter(SkillButton Mybutton)
        {
            FairySaveData save = CommonData.GetFairyGameData();
            if (Mybutton.Myskill.MySkill.KeyID == "S_GFL_Fairy_BatchSet_1_4_1")
            {
                save.batchFairyEnums.Clear();
                foreach (FairyBase fairyBase in save.handlerList)
                {
                    FairyConfig config = Fairys.config[fairyBase.GetFairyEnum()];
                    if (!config.fairytype)
                    {
                        save.batchFairyEnums.Add(fairyBase.GetFairyEnum());
                    }
                }
            }
            else if (Mybutton.Myskill.MySkill.KeyID == "S_GFL_Fairy_BatchSet_1_4_2")
            {
                save.batchFairyEnums.Clear();
            }
            else
            {
                FairyBase fairyBase = null;
                foreach (Skill_Extended sex in Mybutton.Myskill.AllExtendeds)
                {
                    if (sex is Sex_Fairy_View sexv)
                    {
                        foreach (FairyBase fairyBase2 in save.handlerList)
                        {
                            if (fairyBase2.GetFairyEnum() == sexv.fairyEnum)
                            {
                                fairyBase = fairyBase2;
                            }
                        }
                    }
                }
                if (fairyBase == null)
                {
                    return;
                }
                FairyConfig config = Fairys.config[fairyBase.GetFairyEnum()];
                if (!config.fairytype)
                {
                    if (save.batchFairyEnums.Contains(fairyBase.GetFairyEnum()))
                    {
                        save.batchFairyEnums.RemoveAll(a => a == fairyBase.GetFairyEnum());
                    }
                    else
                    {
                        save.batchFairyEnums.Add(fairyBase.GetFairyEnum());
                    }
                }
                
            }
            FairyActiveData.Delay_1a_SwitchBatchSetting();
        }
    }

    public static class BattleFairyActivate
    {
        public static IEnumerator DelayPrint(string str)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject gameObject = Misc.UIInst(PlayData.BattleLucy.BattleInfo.SkillName);
            gameObject.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = str;//待翻译
            yield break;
        }
        public static void BattleStartCheck(FairyBase fairyBase)
        {
            BattleFairyActivate.checklist.Add(fairyBase);
            BattleFairyActivate.frame = Time.frameCount;

            if (!BattleFairyActivate.checking)
            {
                BattleFairyActivate.checking = true;
                BattleSystem.DelayInputAfter(BattleFairyActivate.CheckAndActivate());   
            }
        }
        public static IEnumerator CheckAndActivate()
        {
            if(Time.frameCount<= BattleFairyActivate.frame)
            {
                yield return new WaitForFixedUpdate();
            }
            int price_sum = 0;
            foreach(FairyBase fairybase in BattleFairyActivate.checklist)
            {
                FairyConfig config = Fairys.config[fairybase.GetFairyEnum()];
                price_sum += config.use_price;
            }
            if (Fairy_Command_Method.RemainNum < price_sum)
            {
                string str = ModManager.getModInfo("GFL_Fairy").localizationInfo.SyetemLocalizationUpdate("GFL_Fairy/System/ActiveFail_2");
                str = str.Replace("&num", price_sum.ToString());
                BattleSystem.DelayInputAfter(BattleFairyActivate.DelayPrint(str));
            }
            else
            {
                foreach (FairyBase fairybase in BattleFairyActivate.checklist)
                {
                    FairyConfig config = Fairys.config[fairybase.GetFairyEnum()];
                    Fairy_Command_Method.RemainNum = Fairy_Command_Method.RemainNum- config.use_price;
                    string str = ModManager.getModInfo("GFL_Fairy").localizationInfo.SyetemLocalizationUpdate("GFL_Fairy/System/ActiveSuccess");// "&fairy激活:已扣除&num金币";
                    str = str.Replace("&fairy", config.name).Replace("&num", config.use_price.ToString());
                    BattleSystem.DelayInputAfter(BattleFairyActivate.DelayPrint(str));

                    fairybase.ActiveFairy();
                }
            }
            BattleFairyActivate.checklist.Clear();
            BattleFairyActivate.checking = false;
            yield break;
        }
        public static int frame = 0;
        public static bool checking = false;
        public static List<FairyBase> checklist=new List<FairyBase>();
    }


    [Serializable]
    public class Ability_Command_Fairy : FairyBase, IP_BattleStart_Ones, IP_BattleEndRewardChange
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Command_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            //save.saveList = new List<object> { FairyEnum.GFL_Command_Fairy, false, false, 0 };
            save.saveList.RemoveAll(a => (FairyEnum)a[0] == FairyEnum.GFL_Command_Fairy);
            save.saveList.Add(new List<object> { FairyEnum.GFL_Command_Fairy, false, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    //return (bool)save.saveList[1];
                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if(base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }
                    
                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[2];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is bool)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { }
            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[3];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[3] is int)
                    {
                        base.GetSaveData()[3] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {
                this.activeInBattle = true;
                BattleSystem.instance.AllyTeam.LucyChar.BuffAdd("B_GFL_Command_Fairy_0", BattleSystem.instance.AllyTeam.LucyChar);  
        }

        public void BattleStart(BattleSystem Ins)
        {
            this.activeInBattle = false;
            if (!this.ActiveOnce && !this.ActiveAlways)
            {
                return;
            }
            if (!this.ActiveAlways)
            {
                this.ActiveOnce = false;
            }

            BattleFairyActivate.BattleStartCheck(this);

        }
        public void BattleEndRewardChange()
        {
            //Soul
            if(this.activeInBattle)
            {
                List<ItemBase> list = new List<ItemBase>();
                list.Add(ItemBase.GetItem("Soul", 1));
                InventoryManager.Reward(list);
            }

        }

    }
    public class Bcl_Command_Fairy_0 : Buff//, IP_BattleEndRewardChange, IP_BattleEndOutBattle
    {
        /*
        public void BattleEndRewardChange()
        {
            //Soul
            List<ItemBase> list = new List<ItemBase>();
            list.Add(ItemBase.GetItem("Soul", 1));
            InventoryManager.Reward(list);
            this.SelfDestroy();
        }
        public void BattleEndOutBattle()
        {
            this.SelfDestroy();
        }
        */
    }



    [Serializable]
    public class Ability_Combo_Fairy : FairyBase,IP_NextStage_Before,IP_NextStage_After
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Combo_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            //save.saveList = new List<object> { FairyEnum.GFL_Combo_Fairy, false, 0 };
            save.saveList.RemoveAll(a => (FairyEnum)a[0] == FairyEnum.GFL_Combo_Fairy);
            save.saveList.Add( new List<object> { FairyEnum.GFL_Combo_Fairy, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                //return save.saveFlag_1;
                try
                {
                    /*
                    if (PlayData.BattleLucy.Info.Buffs_Field.FindAll(a => a.BuffData.Key == "B_GFL_Stage_Mark_0").Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    */
                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                //save.saveFlag_1=value;
                try
                {
                    if(base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }
                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                return false;
            }
            set
            {

            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[2];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is int)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {
            base.ActiveFairy();
            //Debug.Log("连击妖精激活");
            this.ActiveNum++;
            //Debug.Log("连击妖精激活次数：" + this.ActiveNum);
            //Debug.Log(StageSystem.instance.StageData.Key);
            //Debug.Log(StageSystem.instance.StageData.Camp);
            //Debug.Log(FieldSystem.instance);
            //Debug.Log(StageSystem.instance.StageData.Camp);
            FairyConfig config = Fairys.config[this.GetFairyEnum()];
            Fairy_Command_Method.RemainNum -= config.use_price;
            CommonData.FieldUseGoldTextOut(config);

            foreach (BattleAlly ba in PlayData.Battleallys)
            {

                List<Buff> list = ba.Info.Buffs_Field.FindAll(a => a.BuffData.Key == "B_GFL_Combo_Fairy_0");
                if(list.Count<=0)
                {
                    ba.BuffAdd("B_GFL_Combo_Fairy_0", ba, false, 0, false, -1, true);
                }
                
            }
        }
        public override void RemoveEffect()
        {
            base.RemoveEffect();
            foreach (BattleAlly ba in PlayData.Battleallys)
            {
                //Debug.Log("移除buff");
                CommonData.BuffRemoveField(ba, "B_GFL_Combo_Fairy_0");
            }
        }


        public void NextStage_Before()
        {
            this.list_field.Clear();
            foreach (Character character in PlayData.TSavedata.Party)
            {
                if(character.Buffs_Field.Find(a=>a.BuffData.Key== "B_GFL_Combo_Fairy_0")!=null)
                {
                    Buff buff = character.Buffs_Field.Find(a => a.BuffData.Key == "B_GFL_Combo_Fairy_0");
                    this.list_field.Add(buff);
                }
            }
        }
        public void NextStage_After()
        {
            foreach(Buff buff in this.list_field)
            {
                if(PlayData.TSavedata.Party.Contains(buff.BChar.Info))
                {
                    Character cha = buff.BChar.Info;
                    if(cha.Buffs_Field.Find(a => a.BuffData.Key == "B_GFL_Combo_Fairy_0") == null)
                    {
                        cha.Buffs_Field.Add(buff);
                    }
                }
            }
            this.list_field.Clear();
        }
        public List<Buff> list_field= new List<Buff>();
    }

    public class Bcl_Combo_Fairy_0 : Buff, IP_BattleEnd, IP_HPChange1

    {
        public override void BuffStat()
        {
            base.BuffStat();
            this.PlusPerStat.Damage = 15 * this.StackNum;
            this.PlusStat.hit = 15 * this.StackNum;
        }
        public void BattleEnd()
        {
            //Soul
            this.BChar.BuffAdd(this.BuffData.Key, this.BChar, false, 0, false, -1, true);
        }
        public void HPChange1(BattleChar Char, bool Healed, int PreHPNum, int NewHPNum)
        {
            if (Char == this.BChar && NewHPNum <= 0)
            {
                this.SelfDestroy();
            }
        }
    }

    [Serializable]
    public class Ability_Provocation_Fairy : FairyBase, IP_BattleStart_Ones
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Provocation_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            save.saveList.Add( new List<object> { FairyEnum.GFL_Provocation_Fairy, false, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }

                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[2];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is bool)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { }
            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[3];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[3] is int)
                    {
                        base.GetSaveData()[3] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {

                BattleAlly ally = AllySummon.NewAlly("GFL_Provocation_Drone", string.Empty);
                Buff buff = ally.BuffAdd("B_GFL_Fairy_Summon_passive", ally);
                int HP = 12;
                foreach (BattleAlly ally1 in PlayData.Battleallys)
                {
                    HP += (6 * ally1.Info.LV);//AllySummon.OrgStat(ally1).maxhp;
                }
                buff.PlusStat.maxhp = HP;
                ally.Info.Hp = HP;
                ally.BuffAdd("B_GFL_Provocation_Fairy_0", ally);
            
        }

        public void BattleStart(BattleSystem Ins)
        {
            if (!this.ActiveOnce && !this.ActiveAlways)
            {
                return;
            }
            if (!this.ActiveAlways)
            {
                this.ActiveOnce = false;
            }

            BattleFairyActivate.BattleStartCheck(this);


        }


    }
    public class Sex_Provocation_Fairy_View_2 : Skill_Extended
    {
        public override string DescExtended(string desc)
        {
            int HP = 12;
            foreach (BattleAlly ally1 in PlayData.Battleallys)
            {
                HP += (6 * ally1.Info.LV);//AllySummon.OrgStat(ally1).maxhp;
            }
            return base.DescExtended(desc).Replace("&maxHP", HP.ToString());
        }
    }
    public class Bcl_Provocation_Fairy_0 : Buff, IP_EnemyAwake, IP_HPChange1
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.CantHeal = true;
        }
        public override void BuffOneAwake()
        {
            base.BuffOneAwake();
            this.AddTaunt();
        }
        public void EnemyAwake(BattleChar Enemy)
        {
            this.AddTaunt();
        }
        public void AddTaunt()
        {
            foreach (BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {
                if (!bc.BuffFind("B_GFL_Provocation_Fairy_Taunt", false))
                {
                    bc.BuffAdd("B_GFL_Provocation_Fairy_Taunt", this.BChar);
                }
            }
        }
        public void HPChange1(BattleChar Char, bool Healed, int PreHPNum, int NewHPNum)
        {
            if (Char == this.BChar)
            {
                this.BChar.Recovery = this.BChar.HP;
                if (this.BChar.HP <= 0)
                {
                    (this.BChar as BattleAlly).ForceDead();
                }
            }
        }
    }
    public class Bcl_Provocation_Fairy_Taunt : B_Taunt, IP_SkillUse_User
    {
        public override void SkillUse(Skill SkillD, List<BattleChar> Targets)
        {
            //base.SkillUse(SkillD, Targets);
        }
    }


    [Serializable]
    public class Ability_Illumination_Fairy : FairyBase
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Illumination_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            save.saveList.Add(new List<object> { FairyEnum.GFL_Illumination_Fairy, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                //return save.saveFlag_1;
                try
                {

                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                //save.saveFlag_1=value;
                try
                {
                    if (base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }
                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                return false;
            }
            set
            {

            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[2];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is int)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {
            base.ActiveFairy();
            //Debug.Log("连击妖精激活");
            this.ActiveNum++;
            //Debug.Log("连击妖精激活次数：" + this.ActiveNum);
            //Debug.Log(StageSystem.instance.StageData.Key);
            //Debug.Log(StageSystem.instance.StageData.Camp);
            //Debug.Log(FieldSystem.instance);
            //Debug.Log(StageSystem.instance.StageData.Camp);

            FairyConfig config = Fairys.config[this.GetFairyEnum()];
            Fairy_Command_Method.RemainNum -= config.use_price;
            CommonData.FieldUseGoldTextOut(config);

            try
            {
                if (StageSystem.instance.gameObject.activeInHierarchy && StageSystem.instance != null && StageSystem.instance.Map != null)
                {
                    StageSystem.instance.Fogout(false);
                }
                for (int i = 0; i < StageSystem.instance.Map.EventTileList.Count; i++)
                {
                    if (StageSystem.instance.Map.EventTileList[i].Info.Type is HiddenWall)
                    {
                        StageSystem.instance.Map.EventTileList[i].HexTileComponent.HiddenWallOpen();
                    }
                }
                StageSystem.instance.SightView(StageSystem.instance.PlayerPos);
            }
            catch
            {

            }

            PlayData.BattleLucy.BuffAdd("B_GFL_Illumination_Fairy_0", PlayData.BattleLucy, false, 0, false, -1, true);

        }
        public override void RemoveEffect()
        {
            base.RemoveEffect();

            CommonData.BuffRemoveField(PlayData.BattleLucy, "B_GFL_Illumination_Fairy_0");

        }

    }

    public class Bcl_Illumination_Fairy_0 : Buff, IP_BattleEnd, IP_TurnEnd, IP_PlayerTurn

    {
        public void Turn()
        {
            this.pflag = false;
        }
        public void TurnEnd()
        {
            this.pflag = true;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (this.pflag)
            {
                BattleSystem.instance.FogTurn = Math.Max(BattleSystem.instance.FogTurn, BattleSystem.instance.TurnNum + 3);
                BattleSystem.instance.FogTurnText.text = "N/A";
            }
        }

        public void BattleEnd()
        {
            this.SelfDestroy();
        }
        public bool pflag = true;
    }


    [Serializable]
    public class Ability_Peace_Fairy : FairyBase, IP_BattleStart_Ones, IP_EnemyAwake, IP_PlayerTurn, IP_TurnEnd
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Peace_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            save.saveList.Add(new List<object> { FairyEnum.GFL_Peace_Fairy, false, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }

                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[2];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is bool)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { }
            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[3];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[3] is int)
                    {
                        base.GetSaveData()[3] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {


                BattleSystem.instance.AllyTeam.LucyChar.BuffAdd("B_GFL_Peace_Fairy_0", BattleSystem.instance.AllyTeam.LucyChar);
                this.activeInBattle = true;
            
        }

        public void BattleStart(BattleSystem Ins)
        {
            this.activeInBattle = false;
            if (!this.ActiveOnce && !this.ActiveAlways)
            {
                return;
            }
            if (!this.ActiveAlways)
            {
                this.ActiveOnce = false;
            }

            BattleFairyActivate.BattleStartCheck(this);



        }

        public void Turn()
        {
            if (this.activeInBattle && BattleSystem.instance.TurnNum <= 2)
            {
                this.CheckBuff();
            }
        }
        public void TurnEnd()
        {
            if (BattleSystem.instance.TurnNum >= 2)
            {
                this.activeInBattle = false;
            }
        }
        public void EnemyAwake(BattleChar Enemy)
        {
            if (this.activeInBattle && BattleSystem.instance.TurnNum <= 2)
            {
                this.CheckBuff();
            }
        }
        public void CheckBuff()
        {
            foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars_Vanish)
            {
                if (!bc.BuffFind("B_GFL_Peace_Fairy_1"))
                {
                    Bcl_Peace_Fairy_1 buff = bc.BuffAdd("B_GFL_Peace_Fairy_1", BattleSystem.instance.AllyTeam.LucyChar, false, 0, true, -1, false) as Bcl_Peace_Fairy_1;
                    buff.mainclass = this;
                }

            }
            foreach (BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {
                if (!bc.BuffFind("B_GFL_Peace_Fairy_1"))
                {
                    Bcl_Peace_Fairy_1 buff = bc.BuffAdd("B_GFL_Peace_Fairy_1", BattleSystem.instance.AllyTeam.LucyChar, false, 0, true, -1, false) as Bcl_Peace_Fairy_1;
                    buff.mainclass = this;
                }
            }
        }

    }
    public class Bcl_Peace_Fairy_0 : Buff, IP_TurnEnd
    {
        public void TurnEnd()
        {
            if (BattleSystem.instance.TurnNum >= 2)
            {
                this.SelfDestroy();
            }
        }

    }
    public class Bcl_Peace_Fairy_1 : Buff, IP_HPChange, IP_PlayerTurn
    {
        public void HPChange(BattleChar Char, bool Healed)
        {
            if (this.BChar.HP < 1)
            {
                this.BChar.Info.Hp = 1;
            }
        }
        public void Turn()
        {
            if (!this.mainclass.activeInBattle)
            {
                this.SelfDestroy();
            }
        }
        public Ability_Peace_Fairy mainclass = null;
    }

    [Serializable]
    public class Ability_Barrier_Fairy : FairyBase, IP_BattleStart_Ones
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Barrier_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            save.saveList.Add( new List<object> { FairyEnum.GFL_Barrier_Fairy, false, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }

                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[2];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is bool)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { }
            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[3];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[3] is int)
                    {
                        base.GetSaveData()[3] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {


                float num = 20;
                foreach (BattleAlly ally1 in PlayData.Battleallys)
                {
                    num += AllySummon.OrgStat(ally1).def;
                }
                BattleSystem.instance.AllyTeam.partybarrier.BarrierHP += ((int)(num));
            
        }

        public void BattleStart(BattleSystem Ins)
        {
            if (!this.ActiveOnce && !this.ActiveAlways)
            {
                return;
            }
            if (!this.ActiveAlways)
            {
                this.ActiveOnce = false;
            }

            BattleFairyActivate.BattleStartCheck(this);

            
        }


    }

    public class Sex_Barrier_Fairy_View : Sex_Fairy_View
    {
        public override string DescExtended(string desc)
        {
            float num = 20;
            foreach (BattleAlly ally1 in PlayData.Battleallys)
            {
                num += AllySummon.OrgStat(ally1).def;
            }
            return base.DescExtended(desc).Replace("&num", ((int)(num)).ToString());

        }

        //public FairyEnum fairyEnum = new FairyEnum();
        //public bool viewInShop = false;
    }


    [Serializable]
    public class Ability_Witch_Fairy : FairyBase, IP_BattleStart_Ones, IP_PlayerTurn, IP_TurnEnd
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Witch_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            save.saveList.Add(new List<object> { FairyEnum.GFL_Witch_Fairy, false, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }

                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[2];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is bool)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { }
            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[3];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[3] is int)
                    {
                        base.GetSaveData()[3] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {

                this.activeInBattle = true;
                BattleSystem.instance.AllyTeam.LucyChar.BuffAdd("B_GFL_Witch_Fairy_0", BattleSystem.instance.AllyTeam.LucyChar);
            
        }

        public void BattleStart(BattleSystem Ins)
        {
            this.activeInBattle = false;
            if (!this.ActiveOnce && !this.ActiveAlways)
            {
                return;
            }
            if (!this.ActiveAlways)
            {
                this.ActiveOnce = false;
            }

            BattleFairyActivate.BattleStartCheck(this);

            
        }
        public void Turn()
        {
            this.turnstart = true;
            if (this.activeInBattle && BattleSystem.instance.TurnNum == 1)
            {

                foreach (BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
                {
                    bc.Damage(BattleSystem.instance.DummyCharEnemy, 10, false, true, true, 0, false, false, false);
                }
            }
        }
        public void TurnEnd()
        {
            this.turnstart = false;
            if (this.activeInBattle)
            {


                foreach (BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
                {
                    int num = 1;
                    num = (int)Math.Max(num, 0.1 * (bc.GetStat.maxhp - bc.HP));
                    //BattleSystem.instance.StartCoroutine(this.TurnDamage(bc, Math.Min(999, num)));
                    BattleSystem.DelayInputAfter(this.TurnDamage(bc, Math.Min(999, num)));
                }


            }

        }
        public IEnumerator TurnDamage(BattleChar bc, int num)
        {


            bc.Damage(BattleSystem.instance.DummyCharEnemy, num, false, true, true, 0, false, false, false);
            yield break;
        }

        public bool turnstart = false;
    }
    public class Bcl_Witch_Fairy_0 : Buff
    {

    }

    [Serializable]
    public class Ability_Beach_Fairy : FairyBase, IP_BattleStart_Ones, IP_PlayerTurn, IP_EnemyNewTurn
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Beach_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            save.saveList.Add(new List<object> { FairyEnum.GFL_Beach_Fairy, false, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }

                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[2];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is bool)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { }
            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[3];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[3] is int)
                    {
                        base.GetSaveData()[3] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {
                this.activeInBattle = true;
                BattleSystem.instance.AllyTeam.LucyChar.BuffAdd("B_GFL_Beach_Fairy_0", BattleSystem.instance.AllyTeam.LucyChar);
            foreach(BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {
                bc.BuffAdd("B_GFL_Beach_Fairy_1", BattleSystem.instance.AllyTeam.LucyChar);
            }
        }


        public void BattleStart(BattleSystem Ins)
        {
            this.activeInBattle = false;
            if (!this.ActiveOnce && !this.ActiveAlways)
            {
                return;
            }
            if (!this.ActiveAlways)
            {
                this.ActiveOnce = false;
            }

            BattleFairyActivate.BattleStartCheck(this);

        }
        public void Turn()
        {

            if (this.activeInBattle)
            {
                BattleSystem.instance.AllyTeam.WaitCount += 1;

            }
        }
        public void EnemyNewTurn()
        {

            if (this.activeInBattle)
            {

                bool delay = false;
                foreach (BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
                {
                    BattleEnemy be = bc as BattleEnemy;
                    foreach (CastingSkill cast in be.SkillQueue)
                    {
                        if (cast.CastSpeed <= 0)
                        {
                            delay = true;
                            break;
                        }
                    }
                    if (delay)
                    {
                        break;
                    }
                }
                if (delay)
                {
                    foreach (BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
                    {
                        BattleEnemy be = bc as BattleEnemy;
                        foreach (CastingSkill cast in be.SkillQueue)
                        {
                            cast._CastSpeed++;
                            if (cast.skill.MyButton != null)
                            {
                                cast.skill.MyButton.CountingLeft++;
                            }
                        }
                    }
                }
            }
        }


        public bool turnstart = false;
    }
    public class Bcl_Beach_Fairy_0 : Buff
    {

    }
    public class Bcl_Beach_Fairy_1 : Buff
    {
        public override void Init()
        {
            base.Init();
            this.PlusPerStat.Damage = -20;
        }
    }
    [Serializable]
    public class Ability_Reinforcement_Fairy : FairyBase
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Reinforcement_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            save.saveList.Add(new List<object> { FairyEnum.GFL_Reinforcement_Fairy, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                //return save.saveFlag_1;
                try
                {

                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                //save.saveFlag_1=value;
                try
                {
                    if (base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }
                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                return false;
            }
            set
            {

            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[2];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is int)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {
            //this.preventChargeMark = true;
            base.ActiveFairy();
            //Debug.Log("连击妖精激活");
            this.ActiveNum++;
            //Debug.Log("连击妖精激活次数：" + this.ActiveNum);
            //Debug.Log(StageSystem.instance.StageData.Key);
            //Debug.Log(StageSystem.instance.StageData.Camp);
            //Debug.Log(FieldSystem.instance);
            //Debug.Log(StageSystem.instance.StageData.Camp);

            //FairyConfig config = Fairys.config[this.GetFairyEnum()];
            //PlayData.Gold -= config.use_price;
            Reinforcement_Select obj1 = new Reinforcement_Select();
            obj1.fairyBase = this;
            FieldSystem.instance.Usingitem = obj1;

            FieldSystem.instance.TargetSelecting = true;
            FieldSystem.instance.TopWindow.SetActive(true);

            TargetSelects.FieldTarget(FieldSystem.instance.PartyWindow[0]);

        }


    }

    public class Reinforcement_Select : UseitemBase
    {
        public override bool CantTarget(Character CharInfo)
        {
            FieldSystem.instance.UseingItemObject = new ItemBase();
            return false;
        }
        public override bool Use(Character CharInfo)
        {
            //Debug.Log("检查Reinforcement_Select");
            FairyConfig config = Fairys.config[this.fairyBase.GetFairyEnum()];
            Fairy_Command_Method.RemainNum -= config.use_price;
            CommonData.FieldUseGoldTextOut(config);

            if (CharInfo.Incapacitated)
            {
                CharInfo.Incapacitated = false;
            }
            CharInfo.Hp = CharInfo.get_stat.maxhp;

            List<Buff> list2 = new List<Buff>();
            foreach (Buff buff in CharInfo.Buffs_Field)
            {
                if (buff.BuffData.Debuff && !buff.BuffData.Cantdisable)
                {
                    list2.Add(buff);

                }
            }
            foreach (Buff buff in list2)
            {
                CommonData.BuffRemoveField(CharInfo, buff.BuffData.Key);
            }

            return false;
        }

        public FairyBase fairyBase = null;
    }

    public class Ability_Nurse_Fairy : FairyBase
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Nurse_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            save.saveList.Add(new List<object> { FairyEnum.GFL_Nurse_Fairy, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                //return save.saveFlag_1;
                try
                {

                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                //save.saveFlag_1=value;
                try
                {
                    if (base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }
                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                return false;
            }
            set
            {

            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[2];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is int)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {
            //this.preventChargeMark = true;
            base.ActiveFairy();
            //Debug.Log("连击妖精激活");
            this.ActiveNum++;
            //Debug.Log("连击妖精激活次数：" + this.ActiveNum);
            //Debug.Log(StageSystem.instance.StageData.Key);
            //Debug.Log(StageSystem.instance.StageData.Camp);
            //Debug.Log(FieldSystem.instance);
            //Debug.Log(StageSystem.instance.StageData.Camp);

            FairyConfig config = Fairys.config[this.GetFairyEnum()];
            Fairy_Command_Method.RemainNum -= config.use_price;
            CommonData.FieldUseGoldTextOut(config);

            foreach (BattleAlly bc in PlayData.Battleallys)
            {
                if (!bc.Info.Incapacitated)
                {
                    bc.Info.HealHP((int)Misc.PerToNum((float)bc.Info.get_stat.maxhp, 35f), true);

                    List<Buff> list2 = new List<Buff>();
                    foreach (Buff buff in bc.Info.Buffs_Field)
                    {
                        if (buff.BuffData.Debuff && !buff.BuffData.Cantdisable)
                        {
                            list2.Add(buff);

                        }
                    }
                    foreach (Buff buff in list2)
                    {
                        CommonData.BuffRemoveField(bc, buff.BuffData.Key);
                    }
                }
            }
        }


    }

    [Serializable]
    public class Ability_Airstrike_Fairy : FairyBase, IP_BattleStart_Ones, IP_EnemyAwake, IP_BattleEnd
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Airstrike_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            save.saveList.Add(new List<object> { FairyEnum.GFL_Airstrike_Fairy, false, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }

                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[2];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is bool)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { }
            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[3];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[3] is int)
                    {
                        base.GetSaveData()[3] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {

                this.activeInBattle = true;
                BattleSystem.instance.AllyTeam.LucyChar.BuffAdd("B_GFL_Airstrike_Fairy_0", BattleSystem.instance.AllyTeam.LucyChar);

                foreach (BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
                {
                    this.EnemyAwake(bc);
                }
   
        }

        public void BattleStart(BattleSystem Ins)
        {
            this.activeInBattle = false;
            if (!this.ActiveOnce && !this.ActiveAlways)
            {
                return;
            }
            if (!this.ActiveAlways)
            {
                this.ActiveOnce = false;
            }

            BattleFairyActivate.BattleStartCheck(this);


        }

        public void EnemyAwake(BattleChar Enemy)
        {
            if (this.activeInBattle)
            {
                if (this.attacknum == 0)
                {
                    BattleSystem.DelayInputAfter(this.Attack());
                }
                this.attacknum++;
            }
        }
        public IEnumerator Attack()
        {
            yield return new WaitForSeconds(0.5f);
            if (this.attacknum > 0)
            {
                Skill skill = Skill.TempSkill("S_GFL_Airstrike_Fairy_1", BattleSystem.instance.AllyTeam.LucyChar);
                Skill_Extended sex = new Skill_Extended();
                skill.ExtendedAdd(sex);
                sex.SkillBasePlus.Target_BaseDMG = 9 * this.attacknum - 1;
                BattleSystem.instance.AllyTeam.LucyChar.ParticleOut(skill, BattleSystem.instance.EnemyTeam.AliveChars);

                this.attacknum = 0;
            }
            yield break;
        }
        public void BattleEnd()
        {
            this.activeInBattle = false;
        }
        public int attacknum = 0;
    }
    public class Bcl_Airstrike_Fairy_0 : Buff
    {

    }
    [Serializable]
    public class Ability_Bombardment_Fairy : FairyBase, IP_BattleStart_Ones, IP_PlayerTurn
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Bombardment_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            save.saveList.Add(new List<object> { FairyEnum.GFL_Bombardment_Fairy, false, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }

                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[2];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is bool)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { }
            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[3];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[3] is int)
                    {
                        base.GetSaveData()[3] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {


                this.activeInBattle = true;
                //BattleSystem.instance.AllyTeam.LucyChar.BuffAdd("B_GFL_Bombardment_Fairy_0", BattleSystem.instance.AllyTeam.LucyChar);

            
        }

        public void BattleStart(BattleSystem Ins)
        {
            this.activeInBattle = false;
            if (!this.ActiveOnce && !this.ActiveAlways)
            {
                return;
            }
            if (!this.ActiveAlways)
            {
                this.ActiveOnce = false;
            }

            BattleFairyActivate.BattleStartCheck(this);


        }

        public void Turn()
        {
            if (this.activeInBattle)
            {
                if (BattleSystem.instance.TurnNum % 2 == 0)
                {
                    Skill skill = Skill.TempSkill("S_GFL_Bombardment_Fairy_1", PlayData.BattleLucy);
                    BattleTeam.SkillRandomUse(PlayData.BattleLucy, skill, false, true, false);
                }
            }
        }

    }
    public class Sex_Bombardment_Fairy_1 : Skill_Extended, IP_DamageChange
    {
        public override void SkillUseSingle(Skill SkillD, List<BattleChar> Targets)
        {
            base.SkillUseSingle(SkillD, Targets);
            int num = 0;
            foreach (BattleChar bc in Targets)
            {
                num = Math.Max(num, bc.HP);
            }
            this.maintarget.AddRange(Targets.FindAll(a => a.HP >= num));
        }
        public int DamageChange(Skill SkillD, BattleChar Target, int Damage, ref bool Cri, bool View)
        {
            if (SkillD == this.MySkill)
            {
                if (this.maintarget.Contains(Target))
                {
                    return 2 * Damage;
                }
            }
            return Damage;
        }
        List<BattleChar> maintarget = new List<BattleChar>();
    }

    [Serializable]
    public class Ability_Sniper_Fairy : FairyBase, IP_BattleStart_Ones, IP_PlayerTurn
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Sniper_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            save.saveList.Add(new List<object> { FairyEnum.GFL_Sniper_Fairy, false, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }

                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[2];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is bool)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { }
            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[3];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[3] is int)
                    {
                        base.GetSaveData()[3] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {


                this.activeInBattle = true;
                //BattleSystem.instance.AllyTeam.LucyChar.BuffAdd("B_GFL_Bombardment_Fairy_0", BattleSystem.instance.AllyTeam.LucyChar);

            
        }

        public void BattleStart(BattleSystem Ins)
        {
            this.pflag = true;
            this.activeInBattle = false;
            if (!this.ActiveOnce && !this.ActiveAlways)
            {
                return;
            }
            if (!this.ActiveAlways)
            {
                this.ActiveOnce = false;
            }

            BattleFairyActivate.BattleStartCheck(this);


        }

        public void Turn()
        {
            if (this.activeInBattle)
            {
                if (this.pflag)
                {
                    this.pflag = false;
                    Skill skill = Skill.TempSkill("S_GFL_Sniper_Fairy_1", PlayData.BattleLucy);
                    BattleSystem.instance.AllyTeam.Add(skill, true);
                }
                else
                {
                    this.pflag = true;
                }
            }
        }
        public bool pflag = true;
    }
    public class Sex_Sniper_Fairy_1 : Skill_Extended
    {

    }
    public class Bcl_Sniper_Fairy_1 : Buff, IP_TurnEnd
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.DMGTaken = 20;
        }
        public override void TurnUpdate()
        {

        }
        public void TurnEnd()
        {
            base.TurnUpdate();
        }
    }


    [Serializable]
    public class Ability_Rocket_Fairy : FairyBase, IP_NextStage_Before, IP_NextStage_After, IP_BattleEnd, IP_PlayerTurn
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Rocket_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            save.saveList.Add(new List<object> { FairyEnum.GFL_Rocket_Fairy, false, 0, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                //return save.saveFlag_1;
                try
                {

                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                //save.saveFlag_1=value;
                try
                {
                    if (base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }
                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                return false;
            }
            set
            {

            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[2];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is int)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { };
            }
        }
        public int RemainTimes
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[3];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[3] is int)
                    {
                        base.GetSaveData()[3] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {
            //this.preventChargeMark = true;
            base.ActiveFairy();
            //Debug.Log("连击妖精激活");
            this.ActiveNum++;
            //Debug.Log("连击妖精激活次数：" + this.ActiveNum);
            //Debug.Log(StageSystem.instance.StageData.Key);
            //Debug.Log(StageSystem.instance.StageData.Camp);
            //Debug.Log(FieldSystem.instance);
            //Debug.Log(StageSystem.instance.StageData.Camp);
            FairyConfig config = Fairys.config[this.GetFairyEnum()];
            Fairy_Command_Method.RemainNum -= config.use_price;
            CommonData.FieldUseGoldTextOut(config);

            this.RemainTimes = this.RemainTimes + 3;
            Bcl_Rocket_Fairy_0 buff = PlayData.BattleLucy.BuffAdd("B_GFL_Rocket_Fairy_0", PlayData.BattleLucy) as Bcl_Rocket_Fairy_0;
            buff.mainbase = this;
        }
        public override void RemoveEffect()
        {
            base.RemoveEffect();
            this.RemainTimes = 0;
            CommonData.BuffRemoveField(PlayData.BattleLucy, "B_GFL_Rocket_Fairy_0");

        }

        public void Turn()
        {
            if (this.RemainTimes > 0)
            {
                Skill skill = Skill.TempSkill("S_GFL_Rocket_Fairy_1", PlayData.BattleLucy);
                BattleTeam.SkillRandomUse(PlayData.BattleLucy, skill, false, true, false);

            }

        }
        public void BattleEnd()
        {
            if (this.RemainTimes > 0)
            {
                this.RemainTimes--;
            }
            if (this.RemainTimes <= 0)
            {
                //CommonData.BuffRemoveField(PlayData.BattleLucy, "B_GFL_Ricket_Fairy_0");
                if (BattleSystem.instance.AllyTeam.LucyChar.BuffFind("B_GFL_Rocket_Fairy_0", false))
                {
                    BattleSystem.instance.AllyTeam.LucyChar.BuffReturn("B_GFL_Rocket_Fairy_0", false).SelfDestroy();
                }

            }
        }

        public void NextStage_Before()
        {
            this.list_field.Clear();

            if (PlayData.BattleLucy.Info.Buffs_Field.Find(a => a.BuffData.Key == "B_GFL_Rocket_Fairy_0") != null)
            {
                Buff buff = PlayData.BattleLucy.Info.Buffs_Field.Find(a => a.BuffData.Key == "B_GFL_Rocket_Fairy_0");
                this.list_field.Add(buff);
            }

        }
        public void NextStage_After()
        {
            foreach (Buff buff in this.list_field)
            {
                if (PlayData.BattleLucy.Info.Buffs_Field.Find(a => a.BuffData.Key == "B_GFL_Rocket_Fairy_0") == null)
                {
                    PlayData.BattleLucy.Info.Buffs_Field.Add(buff);
                }


            }
            this.list_field.Clear();
        }
        public List<Buff> list_field = new List<Buff>();
    }
    public class Bcl_Rocket_Fairy_0 : Buff
    {
        public override string DescExtended()
        {
            FairySaveData savedata = CommonData.GetFairyGameData();
            if (savedata.handlerList.Count > 0 && savedata.handlerList[0] is Ability_Rocket_Fairy rocket_fairy)
            {
                return base.DescExtended().Replace("&num", rocket_fairy.RemainTimes.ToString());
            }
            if (BattleSystem.instance != null)
            {
                this.SelfDestroy();
            }
            else
            {
                CommonData.BuffRemoveField(this.BChar, "B_GFL_Rocket_Fairy_0");
            }
            return base.DescExtended().Replace("&num", "0");

        }
        public Ability_Rocket_Fairy mainbase = null;
    }
    public class Sex_Rocket_Fairy_1 : Skill_Extended
    {

    }
    public class Bcl_Rocket_Fairy_1 : Buff
    {

    }

    [Serializable]
    public class Ability_Warrior_Fairy : FairyBase, IP_BattleStart_Ones
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Warrior_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            save.saveList.Add(new List<object> { FairyEnum.GFL_Warrior_Fairy, false, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }

                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[2];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is bool)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { }
            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[3];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[3] is int)
                    {
                        base.GetSaveData()[3] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {

                this.activeInBattle = true;
                foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars_Vanish)
                {
                    bc.BuffAdd("B_GFL_Warrior_Fairy_0", BattleSystem.instance.AllyTeam.LucyChar);
                }
            
        }

        public void BattleStart(BattleSystem Ins)
        {
            this.activeInBattle = false;
            if (!this.ActiveOnce && !this.ActiveAlways)
            {
                return;
            }
            if (!this.ActiveAlways)
            {
                this.ActiveOnce = false;
            }

            BattleFairyActivate.BattleStartCheck(this);

            
        }



    }
    public class Bcl_Warrior_Fairy_0 : Buff//, IP_BattleEndRewardChange, IP_BattleEndOutBattle
    {
        public override void Init()
        {
            base.Init();
            this.PlusPerStat.Damage = 15;
            this.PlusStat.Penetration = 50;
        }
    }

    [Serializable]
    public class Ability_Fury_Fairy : FairyBase, IP_BattleStart_Ones
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Fury_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            save.saveList.Add(new List<object> { FairyEnum.GFL_Fury_Fairy, false, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }

                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[2];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is bool)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { }
            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[3];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[3] is int)
                    {
                        base.GetSaveData()[3] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {

                this.activeInBattle = true;
                foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars_Vanish)
                {
                    bc.BuffAdd("B_GFL_Fury_Fairy_0", BattleSystem.instance.AllyTeam.LucyChar);
                }
            
        }

        public void BattleStart(BattleSystem Ins)
        {
            this.activeInBattle = false;
            if (!this.ActiveOnce && !this.ActiveAlways)
            {
                return;
            }
            if (!this.ActiveAlways)
            {
                this.ActiveOnce = false;
            }

            BattleFairyActivate.BattleStartCheck(this);

            
        }



    }
    public class Bcl_Fury_Fairy_0 : Buff//, IP_BattleEndRewardChange, IP_BattleEndOutBattle
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.cri = 20;
            this.PlusStat.PlusCriDmg = 20;
            this.PlusStat.hit = 20;
        }
    }

    [Serializable]
    public class Ability_Armor_Fairy : FairyBase, IP_BattleStart_Ones
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Armor_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            save.saveList.Add(new List<object> { FairyEnum.GFL_Armor_Fairy, false, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }

                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[2];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is bool)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { }
            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[3];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[3] is int)
                    {
                        base.GetSaveData()[3] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {

                this.activeInBattle = true;
                foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars_Vanish)
                {
                    if (true || bc.Info.GetData.Role.Key == "Role_Tank")
                    {
                        bc.BuffAdd("B_GFL_Armor_Fairy_0", BattleSystem.instance.AllyTeam.LucyChar);

                    }
                }
            
        }

        public void BattleStart(BattleSystem Ins)
        {
            this.activeInBattle = false;
            if (!this.ActiveOnce && !this.ActiveAlways)
            {
                return;
            }
            if (!this.ActiveAlways)
            {
                this.ActiveOnce = false;
            }

            BattleFairyActivate.BattleStartCheck(this);

        }



    }
    public class Bcl_Armor_Fairy_0 : Buff//, IP_BattleEndRewardChange, IP_BattleEndOutBattle
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.def = 20;
        }
    }

    [Serializable]
    public class Ability_Defense_Fairy : FairyBase, IP_BattleStart_Ones
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Defense_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            save.saveList.Add(new List<object> { FairyEnum.GFL_Defense_Fairy, false, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }

                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[2];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is bool)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { }
            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[3];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[3] is int)
                    {
                        base.GetSaveData()[3] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {

            this.activeInBattle = true;
            foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars_Vanish)
            {
                if (bc.Info.GetData.Role.Key == "Role_Tank")
                {
                    bc.BuffAdd("B_GFL_Defense_Fairy_0", BattleSystem.instance.AllyTeam.LucyChar);

                }
            }

        }

        public void BattleStart(BattleSystem Ins)
        {
            this.activeInBattle = false;
            if (!this.ActiveOnce && !this.ActiveAlways)
            {
                return;
            }
            if (!this.ActiveAlways)
            {
                this.ActiveOnce = false;
            }

            BattleFairyActivate.BattleStartCheck(this);

        }



    }
    public class Bcl_Defense_Fairy_0 : Buff//, IP_BattleEndRewardChange, IP_BattleEndOutBattle
    {
        public override void Init()
        {
            base.Init();
            this.PlusStat.DMGTaken = -25;
            this.PlusStat.AggroPer = 90;
        }
    }

    [Serializable]
    public class Ability_Shield_Fairy : FairyBase, IP_BattleStart_Ones
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Shield_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            save.saveList.Add(new List<object> { FairyEnum.GFL_Shield_Fairy, false, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }

                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[2];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is bool)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { }
            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[3];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[3] is int)
                    {
                        base.GetSaveData()[3] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {
                this.activeInBattle = true;
                foreach (BattleChar bc in BattleSystem.instance.AllyTeam.AliveChars_Vanish)
                {
                    bc.BuffAdd("B_GFL_Shield_Fairy_0", BattleSystem.instance.AllyTeam.LucyChar);
                }
            
        }

        public void BattleStart(BattleSystem Ins)
        {
            this.activeInBattle = false;
            if (!this.ActiveOnce && !this.ActiveAlways)
            {
                return;
            }
            if (!this.ActiveAlways)
            {
                this.ActiveOnce = false;
            }

            BattleFairyActivate.BattleStartCheck(this);

        }



    }
    public class Sex_Shield_Fairy_View : Sex_Fairy_View
    {
        public override string DescExtended(string desc)
        {
            int num = 0;
            int num2 = 0;
            foreach (BattleAlly ally1 in PlayData.Battleallys)
            {
                num += AllySummon.OrgStat(ally1).maxhp;
                num2++;
            }
            num = (int)(num / num2);
            return base.DescExtended(desc).Replace("&shieldnum", (1 + num).ToString());

        }
    }
    public class Bcl_Shield_Fairy_0 : Buff//, IP_BattleEndRewardChange, IP_BattleEndOutBattle
    {
        public override void Init()
        {
            base.Init();
            this.PlusPerStat.Damage = 10;

            int num = 0;
            int num2 = 0;
            foreach (BattleAlly ally1 in PlayData.Battleallys)
            {
                num += AllySummon.OrgStat(ally1).maxhp;
                num2++;
            }
            num = (int)(num / num2);
            this.BarrierHP += (1 + num);
        }
    }

    [Serializable]
    public class Ability_Golden_Fairy : FairyBase, IP_BattleStart_Ones, IP_BattleEndRewardChange
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Golden_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            save.saveList.Add(new List<object> { FairyEnum.GFL_Golden_Fairy, false, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }

                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[2];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is bool)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { }
            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[3];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[3] is int)
                    {
                        base.GetSaveData()[3] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {
                this.activeInBattle = true;
                //BattleSystem.instance.AllyTeam.LucyChar.BuffAdd("B_GFL_Golden_Fairy_0", BattleSystem.instance.AllyTeam.LucyChar);
            
        }

        public void BattleStart(BattleSystem Ins)
        {
            this.activeInBattle = false;
            if (!this.ActiveOnce && !this.ActiveAlways)
            {
                return;
            }
            if (!this.ActiveAlways)
            {
                this.ActiveOnce = false;
            }

            BattleFairyActivate.BattleStartCheck(this);

        }

        public void BattleEndRewardChange()
        {
            //Soul
            if (this.activeInBattle)
            {
                List<ItemBase> list = BattleSystem.instance.Reward;
                foreach (ItemBase item in list)
                {
                    if (item.itemkey == "Gold")
                    {
                        item.StackCount = (int)(1.5 * item.StackCount);
                    }
                }
            }

        }

    }

    [Serializable]
    public class Ability_Landmine_Fairy : FairyBase, IP_NextStage_Before, IP_NextStage_After
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Landmine_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            save.saveList.Add(new List<object> { FairyEnum.GFL_Landmine_Fairy, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                //return save.saveFlag_1;
                try
                {

                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                //save.saveFlag_1=value;
                try
                {
                    if (base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }
                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                return false;
            }
            set
            {

            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[2];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is int)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {
            //this.preventChargeMark = true;
            base.ActiveFairy();
            //Debug.Log("连击妖精激活");
            this.ActiveNum++;

            Landmine_Select obj1 = new Landmine_Select();
            obj1.fairyBase = this;
            FieldSystem.instance.Usingitem = obj1;

            FieldSystem.instance.TargetSelecting = true;
            FieldSystem.instance.TopWindow.SetActive(true);

            TargetSelects.FieldTarget(FieldSystem.instance.PartyWindow[0]);

        }

        public override void RemoveEffect()
        {
            base.RemoveEffect();
            foreach (BattleAlly ba in PlayData.Battleallys)
            {
                //Debug.Log("移除buff");
                CommonData.BuffRemoveField(ba, "B_GFL_Landmine_Fairy_0");
            }
        }
        public void NextStage_Before()
        {
            this.list_field.Clear();
            foreach (Character character in PlayData.TSavedata.Party)
            {
                if (character.Buffs_Field.Find(a => a.BuffData.Key == "B_GFL_Landmine_Fairy_0") != null)
                {
                    Buff buff = character.Buffs_Field.Find(a => a.BuffData.Key == "B_GFL_Landmine_Fairy_0");
                    this.list_field.Add(buff);
                }
            }
        }
        public void NextStage_After()
        {
            foreach (Buff buff in this.list_field)
            {
                if (PlayData.TSavedata.Party.Contains(buff.BChar.Info))
                {
                    Character cha = buff.BChar.Info;
                    if (cha.Buffs_Field.Find(a => a.BuffData.Key == "B_GFL_Landmine_Fairy_0") == null)
                    {
                        cha.Buffs_Field.Add(buff);
                    }
                }
            }
            this.list_field.Clear();
        }
        public List<Buff> list_field = new List<Buff>();
    }

    public class Landmine_Select : UseitemBase
    {
        public override bool CantTarget(Character CharInfo)
        {
            FieldSystem.instance.UseingItemObject = new ItemBase();
            return false;
        }
        public override bool Use(Character CharInfo)
        {
            //Debug.Log("检查Landmine_Select");
            FairyConfig config = Fairys.config[this.fairyBase.GetFairyEnum()];
            Fairy_Command_Method.RemainNum -= config.use_price;
            CommonData.FieldUseGoldTextOut(config);

            CharInfo.GetBattleChar.BuffAdd("B_GFL_Landmine_Fairy_0", PlayData.BattleLucy);

            return false;
        }

        public FairyBase fairyBase = null;
    }
    public class Bcl_Landmine_Fairy_0 : Buff, IP_DamageTake//, IP_BattleEndRewardChange, IP_BattleEndOutBattle
    {
        public void DamageTake(BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF = false, bool NOEFFECT = false, BattleChar Target = null)
        {
            if ( this.BChar.Info.Hp <= 0 && !NODEF)
            {
                resist = true;
                BattleSystem.DelayInput(this.Effects(User));
            }
        }

        // Token: 0x06005279 RID: 21113 RVA: 0x00206351 File Offset: 0x00204551
        public IEnumerator Effects(BattleChar user)
        {
            if (BattleSystem.instance.EnemyList.Count != 0)
            {
                yield return new WaitForSecondsRealtime(1f);
                Skill skill = Skill.TempSkill("S_GFL_Landmine_Fairy_1", this.BChar, this.BChar.MyTeam);
                skill.FreeUse = true;
                yield return BattleSystem.instance.ForceAction(skill, user, false, false, true, null);
            }
            this.SelfDestroy();
            yield break;
        }
    }


    [Serializable]
    public class Ability_Electromagnetic_Fairy : FairyBase, IP_BattleStart_Ones,IP_PlayerTurn, IP_EnemyAwake
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00003428 File Offset: 0x00001628
        public override FairyEnum GetFairyEnum()
        {
            return FairyEnum.GFL_Electromagnetic_Fairy;
        }

        public override void FairyInit()
        {
            //this.CanActive = true;
            //this.ActiveNum = 0;
            FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
            save.saveList.Add(new List<object> { FairyEnum.GFL_Electromagnetic_Fairy, false, false, 0 });
        }
        public override bool ActiveOnce
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[1];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[1] is bool)
                    {
                        base.GetSaveData()[1] = value;
                    }

                }
                catch { }
            }
        }
        public override bool ActiveAlways
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (bool)base.GetSaveData()[2];
                }
                catch { return false; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[2] is bool)
                    {
                        base.GetSaveData()[2] = value;
                    }
                }
                catch { }
            }
        }
        public int ActiveNum
        {
            get
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    return (int)base.GetSaveData()[3];
                }
                catch { return 0; }
            }
            set
            {
                FairySaveData save = PlayData.TSavedata.GetCustomValue<FairySaveData>();
                try
                {
                    if (base.GetSaveData()[3] is int)
                    {
                        base.GetSaveData()[3] = value;
                    }
                }
                catch { };
            }
        }
        // Token: 0x06000042 RID: 66 RVA: 0x0000343C File Offset: 0x0000163C
        public override void ActiveFairy()
        {

            this.activeInBattle = true;

            this.turnCharge = 1;
            foreach (BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars_Vanish)
            {
                if(!bc.BuffFind("B_GFL_Electromagnetic_Fairy_0",false))
                {
                    bc.BuffAdd("B_GFL_Electromagnetic_Fairy_0", BattleSystem.instance.AllyTeam.LucyChar);

                }
            }

        }

        public void BattleStart(BattleSystem Ins)
        {
            this.activeInBattle = false;
            if (!this.ActiveOnce && !this.ActiveAlways)
            {
                return;
            }
            if (!this.ActiveAlways)
            {
                this.ActiveOnce = false;
            }

            BattleFairyActivate.BattleStartCheck(this);


        }

        public void EnemyAwake(BattleChar Enemy)
        {
            if (this.activeInBattle && !Enemy.BuffFind("B_GFL_Electromagnetic_Fairy_0", false))
            {
                Enemy.BuffAdd("B_GFL_Electromagnetic_Fairy_0", BattleSystem.instance.AllyTeam.LucyChar);

            }
        }
        public void Turn()
        {
            if(this.activeInBattle)
            {
                this.turnCharge++;
                if (this.turnCharge >=3)
                {
                    this.turnCharge = 0;

                    Skill skill = Skill.TempSkill("S_GFL_Electromagnetic_Fairy_1", PlayData.BattleLucy);
                    BattleSystem.instance.AllyTeam.Add(skill, true);
                }
            }
        }

        public int turnCharge=1;
    }
    public class Bcl_Electromagnetic_Fairy_0 : Buff, IP_DebuffResist_After//, IP_BattleEndRewardChange, IP_BattleEndOutBattle
    {
        public override void BuffStat()
        {
            base.BuffStat();
            this.PlusStat.RES_CC =-20-20*this.resistNum;
        }
        public void DebuffResist_After(Buff buff)
        {
            if(buff.BChar==this.BChar && buff.BuffData.Debuff && buff.BuffData.BuffTag.Key== GDEItemKeys.BuffTag_CrowdControl)
            {
                this.resistNum++;
                this.BuffStat();
            }
        }
        public int resistNum = 0;
    }

    public class Sex_Electromagnetic_Fairy_1 : Skill_Extended
    {

    }
}
