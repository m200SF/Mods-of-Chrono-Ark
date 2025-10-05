using ChronoArkMod.ModData;
using ChronoArkMod;
using Spine.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;
using System.Collections;
using HarmonyLib;

namespace PD_E_Sana
{
    /*
    [HarmonyPatch(typeof(MasterSDMap))]
    public static class MasterSDMap_Plugin
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void MasterSDMapPostfix(MasterSDMap __instance)
        {
            Debug.Log("MasterSDMap_Start");
            FieldSystem.instance.StartCoroutine(MapItem_Pluse.SetMapItem());
        }
    }

    [HarmonyPatch(typeof(MasterSDMap2))]
    public static class MasterSDMap2_Plugin
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void MasterSDMap2Postfix(MasterSDMap2 __instance)
        {
            Debug.Log("MasterSDMap_Start");
            FieldSystem.instance.StartCoroutine(MapItem_Pluse.SetMapItem());
        }
    }


    public class MapItem_Pluse
    {
        public static IEnumerator SetMapItem()
        {
            yield return new WaitForSeconds(0.25f);

            MapItem_Pluse.Init3();
            
            yield break;
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
                if (MapItem_Pluse.ItemObject != null)
                {
                    UnityEngine.Object.Destroy(MapItem_Pluse.ItemObject);
                }


                GameObject gameObject = MapCharCtrl.CreateSkeletonObj(MapCharEnum.PLUSE);

                ItemCtrl_Pluse itemChar_Pluse = gameObject.AddComponent<ItemCtrl_Pluse>();
                gameObject.GetComponent<EventObject>().TargetEvent.AddListener(delegate ()
                {
                    itemChar_Pluse.ClickEffect();
                });

                gameObject.transform.parent = mapTile.HexTileComponent.transform;
                gameObject.transform.localPosition = new Vector3(6f, 3f, 0f);
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                gameObject.SetActive(true);

                MapItem_Pluse.ItemObject = gameObject;

            }
            else
            {
                //Debug.Log("生成帕斯卡_2");
                if (MapItem_Pluse.ItemObject != null)
                {
                    UnityEngine.Object.Destroy(MapItem_Pluse.ItemObject);
                }


                GameObject gameObject = MapCharCtrl.CreateSkeletonObj(MapCharEnum.PLUSE);

                ItemCtrl_Pluse itemChar_Pluse = gameObject.AddComponent<ItemCtrl_Pluse>();
                gameObject.GetComponent<EventObject>().TargetEvent.AddListener(delegate ()
                {
                    itemChar_Pluse.ClickEffect();
                });

                gameObject.transform.parent = FieldSystem.instance.Player.transform.parent;
                //gameObject.transform.localPosition = new Vector3(3f, -2f, 0f);
                gameObject.transform.localPosition = new Vector3(FieldSystem.instance.Player.transform.position.x + 6, FieldSystem.instance.Player.transform.position.y + 3, FieldSystem.instance.Player.transform.position.z);
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                gameObject.SetActive(true);

                MapItem_Pluse.ItemObject = gameObject;
            }
        }

        public static GameObject ItemObject = null;
    }

    public class ItemCtrl_Pluse : MonoBehaviour
    {
        public void ClickEffect()//点击后的效果——待完成
        {
            Debug.Log("ClickEffect");
        }
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
                ModInfo modInfo = ModManager.getModInfo("PD_Boss_Sana");
                AssetBundle assetBundle = modInfo.assetInfo.GetAssetBundle("sana");
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
                MapCharEnum.PLUSE,
                new MapCharCtrl.MapCharData
                {
                    keyName = "Pluse",
                    name = "脉冲发生器",
                    standingPath = "Assets/sana/Pluse_SKT/Pluse_test.png",
                    skeletonPath = "Assets/sana/Pluse_SKT/Pluse_test_SkeletonData.asset",
                    //standingPath = "Assets/fairy/Persica2/Image_persica2.prefab",
                    //skeletonPath = "Assets/fairy/Q_Persica_2/NPC_Persica_210_s38_SkeletonData.asset",
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
    public enum MapCharEnum
    {
        // Token: 0x0400003B RID: 59
        DEFAULT,
        // Token: 0x0400003C RID: 60
        PLUSE
    }
    */
}
