using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fairy
{
    [HarmonyPatch(typeof(BattleSystem))]
    [HarmonyPatch("DebuffResist")]
    public static class DebuffResistPlugin
    {
        [HarmonyPostfix]
        public static void DebuffResist_After(BattleSystem __instance, Buff buff)
        {
            //Debug.Log("抵抗减益:" + Time.frameCount);
            foreach (IP_DebuffResist_After ip_patch in BattleSystem.instance.IReturn<IP_DebuffResist_After>())
            {
                bool flag = ip_patch != null;
                if (flag)
                {
                    ip_patch.DebuffResist_After(buff);
                }
            }
        }
    }
    public interface IP_DebuffResist_After
    {
        // Token: 0x06000001 RID: 1
        void DebuffResist_After(Buff buff);
    }
}
