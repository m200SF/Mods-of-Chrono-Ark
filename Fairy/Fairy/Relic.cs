using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFL_Fairy
{
    [HarmonyPatch(typeof(FieldSystem))]

    public static class StageStartPlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x0000209C File Offset: 0x0000029C
        [HarmonyPatch("StageStart")]
        [HarmonyPrefix]
        public static void StageStart_patch(FieldSystem __instance, string StageKey, out List<string> __state)
        {

            __state = new List<string>();

            if (/*PlayData.TSavedata.StageNum == 0 &&*/ !PlayData.TSavedata.GameStarted)
            {
                if (!PlayData.TSavedata.IsLoaded)
                {
                    FairySaveData savedata = CommonData.GetFairyGameData();
                    if (!savedata.FairyCommandRelicAdded)
                    {
                        savedata.FairyCommandRelicAdded = true;


                        ItemBase itembase = ItemBase.GetItem("Relic_Fairy_Command");
                        PlayData.TSavedata.ArkPassivePlus++;
                        PlayData.TSavedata.Passive_Itembase.Insert(0, itembase);
                        (itembase as Item_Passive).IsUseStack = true;
                        (itembase as Item_Passive).UseStackNum = 2;
                    }


                }
            }
        }
        [HarmonyPatch("StageStart")]
        [HarmonyPostfix]
        public static void StageStart_After_patch(FieldSystem __instance, string StageKey, List<string> __state)
        {


            if (__state.Count > 0)
            {
                //PD_E_Sana_Save save = PD_E_Sana_SaveData.PD_E_Sana_Save.Load();
                foreach (string str in __state)
                {
                    try
                    {
                        PartyInventory.InvenM.AddNewItem(ItemBase.GetItem(str));
                    }
                    catch { }
                }

                //save.NextRun_ItemKeys.Clear();
                //save.Save();
            }

        }
    }


    public class Relic_Fairy_Command : PassiveItemBase, IP_BattleEndRewardChange
    {
        // Token: 0x06004FF1 RID: 20465 RVA: 0x001FEA5E File Offset: 0x001FCC5E
        public override void Init()
        {
            base.Init();
            this.OnePassive = true;
            this.MyItem.IsUseStack = true;
        }
        public void BattleEndRewardChange()
        {
            this.MyItem.UseStackNum += 1;
        }


    }

    public static class Fairy_Command_Method
    {
        public static int RemainNum
        {
            get
            {
                foreach (ItemBase itemBase in PlayData.TSavedata.Passive_Itembase)
                {
                    if (itemBase != null && itemBase.itemkey== "Relic_Fairy_Command" && itemBase is Item_Passive item_p)
                    {
                        return item_p.UseStackNum;
                    }
                }
                return 0;
            }
            set
            {
                foreach (ItemBase itemBase in PlayData.TSavedata.Passive_Itembase)
                {
                    if (itemBase != null && itemBase.itemkey == "Relic_Fairy_Command" && itemBase is Item_Passive item_p)
                    {
                        item_p.UseStackNum=Math.Max(0,value);
                        return;
                    }
                }
                ItemBase itembase = ItemBase.GetItem("Relic_Fairy_Command");
                PlayData.TSavedata.ArkPassivePlus++;
                PlayData.TSavedata.Passive_Itembase.Insert(0, itembase);
                (itembase as Item_Passive).IsUseStack = true;
                (itembase as Item_Passive).UseStackNum = 0;

                (itembase as Item_Passive).UseStackNum= Math.Max(0, value);


            }
        }
    }
}
