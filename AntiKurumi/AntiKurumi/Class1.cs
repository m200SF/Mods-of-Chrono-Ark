using ChronoArkMod.Plugin;
using ChronoArkMod;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataEditor;
using EItem;
using ChronoArkMod.ModData.Settings;

namespace AntiKurumi
{
    [PluginConfig("M200_AntiKurumiMod", "SF_AntiKurumi_Mod", "1.0.0")]
    //public class patchtest_CharacterModPlugin : ChronoArkPlugin
    public class AntiKurumi_ModPlugin : ChronoArkPlugin
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
        public class AntiKurumiDef : ModDefinition
        {
        }
    }



    [HarmonyPatch(typeof(BlackSpikedArmor))]
    public class TestCharacterPlugin
    {
        // Token: 0x0600012E RID: 302 RVA: 0x000091E8 File Offset: 0x000073E8
        [HarmonyPatch("DamageTake")]
        [HarmonyPrefix]
        public static bool DamageTake_Patch(BlackSpikedArmor __instance, BattleChar User, int Dmg, bool Cri, ref bool resist, bool NODEF , bool NOEFFECT , BattleChar Target )
        {
            if (BattleSystem.instance.EnemyTeam.AliveChars.Count > 0)
            {
                bool flag = ModManager.getModInfo("AntiKurumi").GetSetting<ToggleSetting>("AOESpikedArmor").Value;
                if (flag)
                {
                    foreach (BattleChar bc in BattleSystem.instance.EnemyTeam.AliveChars)
                    {
                        bc.Damage(__instance.BChar, Dmg / 2, false, true, false, 0, false, false, false);
                    }
                    return false;
                }
                else
                {
                    if (BattleSystem.instance.EnemyTeam.AliveChars.Find(a => a.Info.KeyData == "E_Kurumi") != null)
                    {
                        BattleChar bc = BattleSystem.instance.EnemyTeam.AliveChars.Find(a => a.Info.KeyData == "E_Kurumi");
                        bc.Damage(__instance.BChar, Dmg / 2, false, true, false, 0, false, false, false);
                        return false;
                    }
                }

            }
            return true;
        }
    }
}
