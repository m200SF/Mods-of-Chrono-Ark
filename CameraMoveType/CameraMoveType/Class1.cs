using ChronoArkMod.Plugin;
using ChronoArkMod;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static EnemyCastingLineV2;

namespace CameraMoveType
{
    [PluginConfig("M200_CameraMoveType_Mod", "CameraMoveType", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class CameraMoveType_ModPlugin : ChronoArkPlugin
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
        public class CameraMoveTypeDef : ModDefinition
        {
        }
    }


    [HarmonyPatch(typeof(BattleEnemy))]
    public class BattleEnemyPlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("TargetLook")]
        [HarmonyPrefix]
        public static bool TargetLook_Patch(BattleEnemy __instance)
        {
            /*
            Vector3 pos = __instance.GetPos();
            pos.x /= 2f;
            if (pos.y <= 3.5f)
            {
                pos.y = 3.5f;
            }
            if (pos.y >= 6f)
            {
                pos.y = 6f;
            }
            Camera.main.GetComponent<SmoothMove>().Rotate(Quaternion.LookRotation(pos - BattleSystem.instance.battlecamera.PosSave));
            */
            try
            {
               

                Vector3 pos = __instance.GetPos();

                float xmin = BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Min(a => (a as BattleEnemy).GetPos().x);
                float xmax = BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Max(a => (a as BattleEnemy).GetPos().x);
                xmin=Math.Max(xmin, pos.x-3f);
                xmax = Math.Min(xmax, pos.x + 3f);

                pos.x = Mathf.Clamp(BattleSystem.instance.battlecamera.PosSave.x, xmin, xmax);
                BattleSystem.instance.battlecamera.PosSave = new Vector3(pos.x, BattleSystem.instance.battlecamera.PosSave.y, BattleSystem.instance.battlecamera.PosSave.z);


                Vector3 pos2 = pos;
                if (pos2.y <= 3.5f)
                {
                    pos2.y = 3.5f;
                }
                if (pos2.y >= 6f)
                {
                    pos2.y = 6f;
                }
                Vector3 pos3 = pos2 - BattleSystem.instance.battlecamera.PosSave;
                pos3.x = 0;
                Camera.main.GetComponent<SmoothMove>().Rotate(Quaternion.LookRotation(pos3));
            }
            catch
            {
                Debug.Log("TargetLook出错");
            }

            return false;
        }

        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("TargetLookTop")]
        [HarmonyPrefix]
        public static bool TargetLookTop_Patch(BattleEnemy __instance)
        {
            /*
            Vector3 pos = __instance.GetTopPos();
            pos.x /= 2f;
            if (pos.y <= 3.5f)
            {
                pos.y = 3.5f;
            }
            if (pos.y >= 6f)
            {
                pos.y = 6f;
            }
            Camera.main.GetComponent<SmoothMove>().Rotate(Quaternion.LookRotation(pos - BattleSystem.instance.battlecamera.PosSave));
            */
            try
            {
                Vector3 pos = __instance.GetTopPos();
                float xmin = BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Min(a => (a as BattleEnemy).GetTopPos().x);
                float xmax = BattleSystem.instance.EnemyTeam.AliveChars_Vanish.Max(a => (a as BattleEnemy).GetTopPos().x);
                xmin = Math.Max(xmin, pos.x - 3f);
                xmax = Math.Min(xmax, pos.x + 3f);

                pos.x = Mathf.Clamp(BattleSystem.instance.battlecamera.PosSave.x, xmin, xmax);
                BattleSystem.instance.battlecamera.PosSave = new Vector3(pos.x, BattleSystem.instance.battlecamera.PosSave.y, BattleSystem.instance.battlecamera.PosSave.z);

                Vector3 pos2 = pos;
                if (pos2.y <= 3.5f)
                {
                    pos2.y = 3.5f;
                }
                if (pos2.y >= 6f)
                {
                    pos2.y = 6f;
                }
                Vector3 pos3 = pos2 - BattleSystem.instance.battlecamera.PosSave;
                pos3.x = 0;
                Camera.main.GetComponent<SmoothMove>().Rotate(Quaternion.LookRotation(pos3));
            }
            catch
            {
                Debug.Log("TargetLookTop出错");
            }

            return false;
        }
    }

    [HarmonyPatch(typeof(BattleSystem))]
    public class BattleSystemPlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("MyTurn")]
        [HarmonyPostfix]
        public static void MyTurn_Patch(BattleEnemy __instance)
        {
            try
            {

                BattleSystem.instance.battlecamera.PosSave = new Vector3(0, BattleSystem.instance.battlecamera.PosSave.y, BattleSystem.instance.battlecamera.PosSave.z);

            }
            catch
            {

            }

        }

        public static Vector3 CamInitPos = new Vector3();
    }
}
