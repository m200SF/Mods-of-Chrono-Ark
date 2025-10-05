using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PD_E_Sana
{
    public class SanaCommonData
    {
        // Token: 0x06000013 RID: 19 RVA: 0x00002258 File Offset: 0x00000458
        public static SanaTempSaveData GetFairyGameData()
        {
            SanaTempSaveData savedata = PlayData.TSavedata.GetCustomValue<SanaTempSaveData>();
            bool flag = savedata == null;
            if (flag)
            {
                savedata = new SanaTempSaveData();
                PlayData.TSavedata.AddCustomValue(savedata);
            }
            return savedata;
        }
    }

        [Serializable]
    public class SanaTempSaveData : CustomValue
    {
        // Token: 0x17000001 RID: 1
        // (get) Token: 0x06000011 RID: 17 RVA: 0x00002188 File Offset: 0x00000388
        // Token: 0x04000003 RID: 3

        public bool replace = false;
        public bool keepItemAdded = false;

    }
}
