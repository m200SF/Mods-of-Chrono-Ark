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

using static UnityEngine.UI.Image;
using static DemoToonVFX;


namespace M200_FoxOrb_Enhance
{
    [PluginConfig("M200_FoxOrb_Enhance_CharMod", "M200_FoxOrb_Enhance_Mod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class M200_FoxOrb_Enhance_ModPlugin : ChronoArkPlugin
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
        public class M200_FoxOrb_Enhance_Def : ModDefinition
        {
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
            if((inputitem.itemkey == GDEItemKeys.Item_Equip_FoxOrb || inputitem.itemkey == GDEItemKeys.Item_Equip_FoxOrb_0) && !Misc.IsFemale(__instance.Info.KeyData))
            {
                bool flag = false;
                if (__instance.PassiveOnly)
                {
                    flag = !(inputitem is Item_Passive);
                }
                else
                {
                    flag = (!__instance.Equip || !(inputitem is Item_Equip)) && (!__instance.OutEquip || (!(inputitem is Item_Equip) && !(inputitem is Item_Passive))) && (__instance.PassiveOnly || __instance.Equip || __instance.ItemView || __instance.OutEquip);
                }
                bool flag2 = __instance.FindItem(inputitem.itemkey)!=0;

                __result= flag2 || flag;
            }

        }
    }

    /*
    [HarmonyPatch(typeof(ItemBase))]
    [HarmonyPatch("GetItem")]
    [HarmonyPatch(new System.Type[] { typeof(string) })]
    public class GetItem_Patch
    {
        // Token: 0x06000153 RID: 339 RVA: 0x0000782C File Offset: 0x00005A2C
        [HarmonyPrefix]
        public static void Prefix(ref string key)
        {
            if(key== GDEItemKeys.Item_Equip_FoxOrb)
            {
                key = GDEItemKeys.Item_Equip_FoxOrb_0;
            }
        }
    }
    */

    [HarmonyPatch(typeof(InventoryManager))]

    public class GetItem_Patch
    {
        // Token: 0x06000153 RID: 339 RVA: 0x0000782C File Offset: 0x00005A2C
        [HarmonyPatch("Reward")]
        [HarmonyPatch(new System.Type[] { typeof(List<ItemBase>) })]
        [HarmonyPrefix]
        public static void Prefix(ref List<ItemBase> Items)
        {
            bool flag1=ModManager.getModInfo("FoxOrb_Enhance").GetSetting<ToggleSetting>("DirectlUpgrade").Value;
            if(flag1 )
            {
                List<ItemBase> items_1 = Items.FindAll(a => a.itemkey == GDEItemKeys.Item_Equip_FoxOrb);
                if(items_1.Count>0 )
                {
                    int num = items_1.Count;
                    Items.RemoveAll(a => a.itemkey == GDEItemKeys.Item_Equip_FoxOrb);
                    for(int i = 0; i < num; i++)
                    {
                        Items.Add(ItemBase.GetItem(GDEItemKeys.Item_Equip_FoxOrb_0));

                    }
                }
            }
            
            
        }

        [HarmonyPatch("Reward")]
        [HarmonyPatch(new System.Type[] { typeof(ItemBase) })]
        [HarmonyPrefix]
        public static void Prefix(ref ItemBase Item)
        {
            bool flag1 = ModManager.getModInfo("FoxOrb_Enhance").GetSetting<ToggleSetting>("DirectlUpgrade").Value;
            if (flag1)
            {
                if (Item.itemkey == GDEItemKeys.Item_Equip_FoxOrb)
                {

                        Item=ItemBase.GetItem(GDEItemKeys.Item_Equip_FoxOrb_0);

                    
                }
            }


        }
    }
    // && ModManager.getModInfo("FoxOrb_Enhance").GetSetting<ToggleSetting>("IgnoreGender").Value

    [HarmonyPatch(typeof(FieldSystem))]

    public static class StageStartPlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPatch("StageStart")]
        [HarmonyPrefix]
        public static void StageStart_patch(FieldSystem __instance, string StageKey, out bool __state)
        {

            __state = false;

            if (ModManager.getModInfo("FoxOrb_Enhance").GetSetting<ToggleSetting>("GetAtStart").Value && PlayData.TSavedata.StageNum == 0 && !PlayData.TSavedata.GameStarted)
            {
                if (!PlayData.TSavedata.IsLoaded)
                {


                    __state= true;

                }
            }
        }
        [HarmonyPatch("StageStart")]
        [HarmonyPostfix]
        public static void StageStart_After_patch(FieldSystem __instance, string StageKey, bool __state)
        {


            if (__state)
            {
                //PD_E_Sana_Save save = PD_E_Sana_SaveData.PD_E_Sana_Save.Load();

                    try
                    {
                    //PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(GDEItemKeys.Item_Equip_FoxOrb));
                    InventoryManager.Reward(ItemBase.GetItem(GDEItemKeys.Item_Equip_FoxOrb));
                }
                    catch { }
                

                //save.NextRun_ItemKeys.Clear();
                //save.Save();
            }

        }
    }
}
