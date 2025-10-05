using GameDataEditor;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

namespace SF_Standard
{
    public class Item_SF_2 : UseitemBase
    {
        // Token: 0x060033FF RID: 13311 RVA: 0x0016AC44 File Offset: 0x00168E44
        public override bool Use()
        {
            //UnityEngine.Debug.Log("指定装备契约");
            //Item_SF_2.replaceingRedHammer = true;
            //UIManager.InstantiateActiveAddressable(new GDEGameobjectDatasData(GDEItemKeys.GameobjectDatas_GUI_RedHammer).Gameobject_Path, AddressableLoadManager.ManageType.None).GetComponent<UI_RedHammer>().UseItem = this.MyItem;

            // 1. 实例化UI预制体
            GameObject uiObject = UIManager.InstantiateActiveAddressable(new GDEGameobjectDatasData(GDEItemKeys.GameobjectDatas_GUI_RedHammer).Gameobject_Path, AddressableLoadManager.ManageType.None);
            // 2. 移除原UI_RedHammer组件
            UI_RedHammer originalComponent = uiObject.GetComponent<UI_RedHammer>();

            InventoryManager inven_tele = null;
            List<InventoryManager> equip_tele = new List<InventoryManager>();
            UnityEvent ESCEvent_tele = null;
            RectTransform cancelButton_tele = null;
            if (originalComponent != null)
            {
                inven_tele = originalComponent.Inven;
                equip_tele = originalComponent.Equip;
                ESCEvent_tele = originalComponent.ESCEvent;
                cancelButton_tele = originalComponent.CancelButton;


                // 运行时移除组件用Destroy（编辑模式用DestroyImmediate）
                UnityEngine.Object.Destroy(originalComponent);
            }
            // 3. 添加你的自定义组件
            UI_Item_SF_2 myComponent = uiObject.AddComponent<UI_Item_SF_2>();

            myComponent.Inven = inven_tele;
            myComponent.Equip = equip_tele;
            myComponent.ESCEvent = ESCEvent_tele;
            myComponent.CancelButton = cancelButton_tele;

            if (myComponent.CancelButton.GetComponent<Button>() != null)
            {
                //UnityEngine.Debug.Log("GetButton_1");
                myComponent.CancelButton.GetComponent<Button>().onClick.AddListener(new UnityAction(myComponent.ButtonAction));
            }
            else
            {
                //UnityEngine.Debug.Log("GetButton_2");
                uiObject.AddComponent<Button>().onClick.AddListener(new UnityAction(myComponent.ButtonAction));
            }
            // 4. 传递物品数据（和原来一样）
            myComponent.UseItem = this.MyItem;
            return false;
        }
        public static bool replaceingRedHammer = false;
    }

    public class UI_Item_SF_2 : ScrollUIBase, ITEM_USE
    {
        // Token: 0x1700017C RID: 380
        // (get) Token: 0x06003092 RID: 12434 RVA: 0x0014A298 File Offset: 0x00148498
        private List<RectTransform> Target1
        {
            get
            {
                List<RectTransform> list = new List<RectTransform>();
                for (int i = 0; i < this.Equip[0].Align.transform.childCount; i++)
                {
                    list.Add(this.Equip[0].Align.transform.GetChild(i).GetComponent<RectTransform>());
                }
                for (int j = 0; j < this.Equip[1].Align.transform.childCount; j++)
                {
                    list.Add(this.Equip[1].Align.transform.GetChild(j).GetComponent<RectTransform>());
                }
                for (int k = 0; k < this.Equip[2].Align.transform.childCount; k++)
                {
                    list.Add(this.Equip[2].Align.transform.GetChild(k).GetComponent<RectTransform>());
                }
                for (int l = 0; l < this.Equip[3].Align.transform.childCount; l++)
                {
                    list.Add(this.Equip[3].Align.transform.GetChild(l).GetComponent<RectTransform>());
                }
                return list;
            }
        }

        public void ButtonAction()
        {
            //UnityEngine.Debug.Log("路径点ButtonAction");
            this.Delete();
        }
        // Token: 0x06003093 RID: 12435 RVA: 0x0014A3E8 File Offset: 0x001485E8
        private new void Start()
        {
            UIManager.CharstatClose();
            this.Inven.CreateInven(PlayData.TSavedata.Inventory, false, false);
            this.Inven.UseScript = this;
            for (int i = 0; i < PlayData.TSavedata.Party.Count; i++)
            {
                this.Equip[i].CreateInven(PlayData.TSavedata.Party[i].Equip, false, false);
                this.Equip[i].UseScript = this;
            }
            if (this.Layouts.Count == 0)
            {
                int layoutMax = GamepadManager.LayoutMax;
                this.InvenLayout = new PadLayout
                {
                    ColumnNum = 9,
                    Index = layoutMax++,
                    Size = new Vector2(80f, 80f),
                    TargetType = typeof(ItemSlot),
                    LayoutType = typeof(UI_Item_SF_2),
                    CanChangeLayoutPrev = false
                };
                for (int j = 0; j < this.Inven.Align.transform.childCount; j++)
                {
                    this.InvenLayout.Targets.Add(this.Inven.Align.transform.GetChild(j).GetComponent<RectTransform>());
                }
                this.EquipLayout = new PadLayout
                {
                    ColumnNum = 4,
                    Index = layoutMax++,
                    Size = new Vector2(80f, 80f),
                    TargetType = typeof(ItemSlot),
                    LayoutType = typeof(UI_Item_SF_2)
                };
                this.ButtonLayout = new PadLayout
                {
                    ColumnNum = 1,
                    Index = layoutMax++,
                    TargetType = typeof(Button),
                    LayoutType = typeof(UI_Item_SF_2),
                    UseBox = false,
                    CanChangeLayoutNext = false
                };
                this.ButtonLayout.Targets.Add(this.CancelButton);
                this.CancelButton.parent.gameObject.AddComponent<Button>().onClick.AddListener(new UnityAction(this.Delete));
                this.Layouts.Add(this.ButtonLayout);
                this.Layouts.Add(this.EquipLayout);
                this.Layouts.Add(this.InvenLayout);
                GamepadManager.Add(this.ButtonLayout, false);
                GamepadManager.Add(this.EquipLayout, false);
                GamepadManager.Add(this.InvenLayout, false);
                if (GamepadManager.IsPad)
                {
                    GamepadManager.IsLayoutMode = true;
                    UIManager.inst.StartCoroutine(this.Co_Delay());

                }
            }
            BaseInputModule obj1 = (BaseInputModule)Traverse.Create(EventSystem.current).Field("m_CurrentInputModule").GetValue();

        }

        // Token: 0x06003094 RID: 12436 RVA: 0x0014A636 File Offset: 0x00148836
        private IEnumerator Co_Delay()
        {
            yield return new WaitForSeconds(0.25f);
            GamepadManager.SetTargetIndex(0);
            GamepadManager.ChangeLayout(this.InvenLayout, true);
            yield break;
        }

        // Token: 0x06003095 RID: 12437 RVA: 0x0014A645 File Offset: 0x00148845
        private void Update()
        {
            if (this.Layouts.Count > 0)
            {
                this.EquipLayout.Targets = this.Target1;
            }
        }

        // Token: 0x06003096 RID: 12438 RVA: 0x00148D0C File Offset: 0x00146F0C
        public override void SelfDestroy()
        {


            GamepadManager.Remove(this.Layouts);
            GamepadManager.SetTargetIndex(0);
            GamepadManager.IsLayoutMode = false;
            PartyInventory.InvenM.ItemUpdateFromInven();
            base.SelfDestroy();
        }

        // Token: 0x06003097 RID: 12439 RVA: 0x0014A668 File Offset: 0x00148868
        public void ItemUse(ItemObject select)
        {
            //UnityEngine.Debug.Log("ItemUse节点-1");
            if (select.Item is Item_Equip)
            {
                List<string> list_except = new List<string>();
                for (int i = 1; i < this.Inven.InventoryItems.Count; i++)
                {
                    if (this.Inven.InventoryItems[i] != null && this.Inven.InventoryItems[i] is Item_Equip item_exc)
                    {
                        if (item_exc.ItemClassNum >= 3)
                        {
                            list_except.Add(item_exc.itemkey);

                        }
                    }
                }
                new SelectEquip_Item_SF_2().SelectEquipWindow(6, (select.Item as Item_Equip).ItemClassNum, null, list_except);
                this.Inven.DelItem(select.Item);
                for (int i = 0; i < PlayData.TSavedata.Party.Count; i++)
                {
                    this.Equip[i].DelItem(select.Item);
                }

                /*
                if ((select.Item as Item_Equip).ItemClassNum == 3)
                {
                    PlayData.TSavedata.EquipList_Unique.Remove(select.Item.itemkey);
                }
                if ((select.Item as Item_Equip).ItemClassNum == 4)
                {
                    PlayData.TSavedata.EquipList_Legendary.Remove(select.Item.itemkey);
                }
                */

                this.UseItem.MyManager.DelItem(this.UseItem, 1);
                PartyInventory.InvenM.ItemUpdateFromInven();
                if (FieldSystem.instance != null)
                {
                    foreach (AllyWindow allyWindow in FieldSystem.instance.PartyWindow)
                    {
                        allyWindow.EquipUpdate();
                    }
                }
                this.SelfDestroy();
                return;
            }
        }

        public static List<ItemBase> GetEquip_NoOverlap_AllItemBase(int ItemClass, bool Duplicatepossible, List<string> Exept = null)
        {
            List<string> list = new List<string>();
            List<ItemBase> list2 = new List<ItemBase>();
            if (Exept != null)
            {
                list.AddRange(Exept);
            }
            if (!CharacterSkinData.SteamDLCCheck(2780000U))
            {
                list.AddRange(CharacterSkinData.SwimDLCItemKeyList);
            }
            if (!CharacterSkinData.SteamDLCCheck(3193720U))
            {
                list.AddRange(CharacterSkinData.CasinoDLCItemKeyList);
            }
            List<string> keys = UI_Item_SF_2.GetEquipAllString(ItemClass, Duplicatepossible, list);
            foreach (string key in keys)
            {
                list2.Add(ItemBase.GetItem(key));
            }

            return list2;
        }

        public static List<string> GetEquipAllString(int Class, bool Duplicatepossible, List<string> Exept)
        {
            if (Class == -1)
            {
                return new List<string>();
            }
            List<ItemBase> list = new List<ItemBase>();
            foreach (ItemBase itemBase in PlayData.ALLITEMLIST)
            {
                if (itemBase.GetisEquip && !itemBase.GetOff && !itemBase.GetLock && !itemBase.GetNoDrop && (itemBase as Item_Equip).ItemClassNum == Class)
                {
                    list.Add(itemBase);
                }
            }
            string itemkey;
            List<string> list2 = new List<string>();
            /*
            if (!Duplicatepossible)
            {
                if (Class == 3)
                {
                    list2 = PlayData.TSavedata.EquipList_Unique;
                }
                if (Class == 4)
                {
                    list2 = PlayData.TSavedata.EquipList_Legendary;
                }
            }
            */
            List<string> list3 = new List<string>();
            if (Exept != null)
            {
                list3.AddRange(Exept);
            }
            if (list2 != null)
            {
                list3.AddRange(list2);
            }
            List<string> list_ans = new List<string>();
            foreach (ItemBase item in list)
            {
                if (list3.Find(a => a == item.itemkey) == null)
                {
                    list_ans.Add(item.itemkey);
                }
            }
            return list_ans;
        }

        // Token: 0x0400234A RID: 9034
        public InventoryManager Inven;

        // Token: 0x0400234B RID: 9035
        public List<InventoryManager> Equip = new List<InventoryManager>();

        // Token: 0x0400234C RID: 9036
        public ItemBase UseItem;
    }

    public class SelectEquip_Item_SF_2 : MonoBehaviour
    {
        // Token: 0x06002FB1 RID: 12209 RVA: 0x00142990 File Offset: 0x00140B90
        public void SelectEquipWindow(int count, int itemClassNum, Action func = null, List<string> except = null)
        {
            this.EquipList.Clear();
            this.Count = count;
            this.ItemClassNum = itemClassNum;
            this.seUI = UIManager.InstantiateActive(UIManager.inst.SelectItemUI).GetComponent<SelectItemUI>();
            List<ItemBase> equip_NoOverlap = UI_Item_SF_2.GetEquip_NoOverlap_AllItemBase(this.ItemClassNum, false, except);
            this.EquipList.AddRange(equip_NoOverlap);
            this.seUI.Init(this.EquipList, null, false);
        }

        // Token: 0x04002204 RID: 8708
        public List<ItemBase> EquipList = new List<ItemBase>();

        // Token: 0x04002205 RID: 8709
        public InventoryManager MainInven;

        // Token: 0x04002206 RID: 8710
        private int Count;

        // Token: 0x04002207 RID: 8711
        private int ItemClassNum;

        // Token: 0x04002208 RID: 8712
        private SelectItemUI seUI;
    }
    /*
    [HarmonyPatch(typeof(UI_RedHammer))]
    public class ReplaceRedHammerPlugin
    {
        
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("ItemUse")]
        [HarmonyPrefix]
        public static bool ItemUse_Patch(UI_RedHammer __instance,ItemObject select)//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
            if(Item_SF_2.replaceingRedHammer)
            {
                if (select.Item is Item_Equip)
                {
                    new SelectEquip().SelectEquipWindow(6, (select.Item as Item_Equip).ItemClassNum, null);
                    __instance.Inven.DelItem(select.Item);
                    for (int i = 0; i < PlayData.TSavedata.Party.Count; i++)
                    {
                        __instance.Equip[i].DelItem(select.Item);
                    }
                    __instance.UseItem.MyManager.DelItem(__instance.UseItem, 1);
                    PartyInventory.InvenM.ItemUpdateFromInven();
                    if (FieldSystem.instance != null)
                    {
                        foreach (AllyWindow allyWindow in FieldSystem.instance.PartyWindow)
                        {
                            allyWindow.EquipUpdate();
                        }
                    }
                    __instance.SelfDestroy();
                }
                return false;
            }
            return true;
        }
        
        [HarmonyPatch("SelfDestroy")]
        [HarmonyPostfix]
        public static void SelfDestroy_Patch(UI_RedHammer __instance)//Character_Set_AllyData_Patch(Character __instance, GDECharacterData Data)
        {
            Item_SF_2.replaceingRedHammer = false;

            // 创建堆栈跟踪对象，true表示捕获文件名和行号（需要pdb文件）
            StackTrace stackTrace = new StackTrace(true);
            //UnityEngine.Debug.Log("调用链（从直接上级到最外层）：");
            //UnityEngine.Debug.Log("-----------------------------------");
            // 遍历所有堆栈帧（从索引1开始，跳过当前方法自身）
            for (int i = 1; i < stackTrace.FrameCount; i++)
            {
                StackFrame frame = stackTrace.GetFrame(i);
                MethodBase method = frame.GetMethod();
                if (method == null)
                    continue;
                // 获取方法所在的类名
                string className = method.DeclaringType?.Name ?? "未知类型";
                // 获取方法名
                string methodName = method.Name;
                // 获取文件名和行号（调试时需要pdb文件）
                string fileName = frame.GetFileName() ?? "未知文件";
                int lineNumber = frame.GetFileLineNumber();
                //UnityEngine.Debug.Log(i + ": classname:" + className + "." + methodName);
            }
        }
    }
    */
}
