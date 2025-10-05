using ChronoArkMod;
using GameDataEditor;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SF_Standard
{
    [HarmonyPatch(typeof(BattleChar))]
    [HarmonyPatch("SkillUseEffect")]
    public static class BattleChar_Damage_Plugin
    {
        [HarmonyPriority(-2)]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var methodInfo_1 = AccessTools.Method(typeof(SF_Standard.SexProtectCommon), nameof(SF_Standard.SexProtectCommon.SF_Standard_SkillUseSingleAlpha), new System.Type[] { typeof(List<Skill_Extended>),typeof(Skill)  });


            var codes = instructions.ToList();


            bool triggerd = false;
            for (int i = 0; i < codes.Count; i++)
            {
                if(codes[i].operand is MethodInfo && ((MethodInfo)codes[i].operand).Name == "SF_Standard_SkillUseSingleAlpha")
                {
                    triggerd = true;
                    break;
                }
            }
            for (int i = 0; i < codes.Count; i++)
            {
                /*
                if(i==54 )
                {
                    //UnityEngine.Debug.Log(i + ":" + codes[i].opcode);
                    //UnityEngine.Debug.Log(i + ":" + (codes[i].operand));
                    //UnityEngine.Debug.Log(i + ":" + (codes[i].operand).GetType());
                    //UnityEngine.Debug.Log(i + ":" + ((Label)(codes[i].operand)).GetHashCode());
                    
                }
                */
                bool f1 = true;
                if (i >2 && !triggerd)
                {
                    f1 = f1 && codes[i -2].opcode == OpCodes.Ldarg_1;
                    f1 = f1 && codes[i - 1].opcode == OpCodes.Ldfld;
                    f1 = f1 && codes[i - 1].operand is FieldInfo && ((FieldInfo)codes[i - 1].operand).Name == "AllExtendeds";
                    f1 = f1 && codes[i -0].opcode == OpCodes.Callvirt;
                    f1 = f1 && codes[i - 0].operand is MethodInfo && ((MethodInfo)codes[i - 0].operand).Name == "AddRange";

                }
                else
                {
                    f1 = false;
                }
                if(f1)
                {
                    //UnityEngine.Debug.Log("找到位置"+i); 
                    //UnityEngine.Debug.Log("找到位置" + i);
                    //UnityEngine.Debug.Log("找到位置" + i);
                    //UnityEngine.Debug.Log("找到位置" + i);
                    yield return codes[i];
                    //yield return new CodeInstruction(OpCodes.Stloc_S,35);
                    //yield return new CodeInstruction(OpCodes.Ldloc_S,35);
                    //yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Call, methodInfo_1);
                    //yield return new CodeInstruction(OpCodes.Stloc_0);
                    //yield return new CodeInstruction(OpCodes.Ldloc_0);

                }
                else
                {
                    yield return codes[i];
                }


            }
            
        }
    }
    [HarmonyPriority(-1)]

    public static class SexProtectCommon
    {
        public static List<Skill_Extended> SF_Standard_SkillUseSingleAlpha(List<Skill_Extended> list_sex,Skill skill)
        {

            if (SexRecords.activeProtect_Override)
            {
                List<Skill_Extended> list_ans = new List<Skill_Extended>();
                list_ans.AddRange(list_sex);
                //UnityEngine.Debug.Log("监测点：SkillUseSingleAlpha");
                //UnityEngine.Debug.Log(skill.MySkill.Name+ "_Sex数量："+skill.AllExtendeds.Count);
                //UnityEngine.Debug.Log("list_sex的Sex数量：" + list_sex.Count);
                List<Skill_Extended> list = new List<Skill_Extended>();
                list.AddRange(SexRecords.RecordAndCheckSkill(skill));
                SexRecords.RestoreSkill(skill, SexRecords.priorNew_Restore);
                //UnityEngine.Debug.Log("保护数量：" + list.Count);
                list_ans.AddRange(list);


                return list_ans;
            }
            else
            {
                return list_sex;
            }
            
        }
    }


    
    public static class SexRecords
    {
        
        
        public static void RecordSkill(Skill skill,bool priorNew)//增加（刷新）目标技能的Sex记录
        {
            Type typeOfSexI = typeof(Sex_ImproveClone);

            SexRec rec = null;
            if(SexRecords.sexSkillList.Find(x => x.recSkill == skill) != null)
            {
                rec = SexRecords.sexSkillList.Find(x => x.recSkill == skill);
            }
            else
            {
                rec=new SexRec();
                rec.recSkill = skill;
                SexRecords.sexSkillList.Add(rec);
            }

            List<Skill_Extended> list_1 = new List<Skill_Extended>();
            /*
            foreach(Skill_Extended sex in skill.AllExtendeds)//启用后，所有Sex_ImproveClone均会被记录
            {
                if(sex is Sex_ImproveClone sexi && sexi.Recorded())
                {
                    list_1.Add(sex);
                }
            }
            */

            
            if(rec.dataSex==null)
            {
                //Debug.Log("dataEX初始化记录技能：" + skill.MySkill.Name + " 发生帧：" + Time.frameCount + " basic: " + skill.BasicSkill);
                rec.dataSex = new List<SexType> ();
                foreach (string dataSexKey in skill.MySkill.SkillExtended)
                {
                    SexType ST=new SexType ();
                    ST.sex = Skill_Extended.DataToExtendedC(dataSexKey);
                    ST.type=ST.sex.GetType();

                    if (ST.type.IsSubclassOf(typeOfSexI) && (ST.sex as Sex_ImproveClone).RecordAsDataSex())
                    {
                        rec.dataSex.Add(ST);
                    }
                    
                }
            }
            foreach (SexType ST in rec.dataSex)//技能与记录中均没有dataSex时，生成新的dataSex
            {

                //Skill_Extended sex2 = Skill_Extended.DataToExtendedC(dataSexKey)

                //if (ST.type.IsSubclassOf(typeOfSexI) && (ST.sex as Sex_ImproveClone).RecordAsDataSex())// is Sex_ImproveClone sexi && sexi.Recorded())//(type.IsSubclassOf((new Sex_ImproveClone().GetType())))// 
                //{
                    if (skill.AllExtendeds.Find( a => a.GetType() == ST.type) == null)
                    {
                        if (rec.recExtended.Find( a => a.GetType() == ST.type) == null)
                        {
                            //UnityEngine.Debug.Log("增加dataSex:" + dataSexKey);
                            Skill_Extended sex2 = (Skill_Extended)ST.sex.Clone();//Skill_Extended.DataToExtendedC(dataSexKey);
                            sex2.isDataExtended = true;
                            //skill.ExtendedAdd(sex2);
                            //skill.AllExtendeds.Remove(sex2);
                            skill.ExtendedInit(sex2, false);
                            list_1.Add(sex2);
                        }
                    }
                    else//启用后，仅json中填写的Sex_ImproveClone会被记录//判断是否有已记录的同类型Sex在方法末尾，会从list_1中筛选
                    {
                        list_1.Add(skill.AllExtendeds.Find( a => a.GetType() == ST.type));
                    }
                    

                //}

            }




            
            if (rec.plusSex == null)
            {
                //Debug.Log("itemEX初始化记录技能：" + skill.MySkill.Name+" 发生帧："+Time.frameCount+" basic: "+skill.BasicSkill);
                rec.plusSex = new List<SexType>();
                foreach (GDESkillExtendedData exSexGde in skill.MySkill.SKillExtendedItem)
                {
                    SexType ST = new SexType();
                    ST.sex = Skill_Extended.DataToExtended(exSexGde);
                    ST.type = ST.sex.GetType();

                    if (ST.type.IsSubclassOf(typeOfSexI) && (ST.sex as Sex_ImproveClone).RecordAsDataSex())
                    {
                        rec.plusSex.Add(ST);
                    }
                    
                }
            }
            
            foreach (SexType ST in rec.plusSex)//技能与记录中均没有ExSex时，生成新的ExSex
            {
                if (skill.AllExtendeds.Find(a => a.Data != null && a.Data.Key == ST.sex.Data.Key) == null)
                {
                    if (rec.recExtended.Find(a => a.Data != null && a.Data.Key == ST.sex.Data.Key) == null)
                    {
                        if (ST.sex.Data.IsOnesInit)
                        {
                            Skill_Extended skill_Extended5 = (Skill_Extended)ST.sex.Clone();
                            //if (skill_Extended5 is Sex_ImproveClone sexi && sexi.RecordAsItemSex())
                            //{
                                skill_Extended5.isDataExtended = true;
                                //skill.ExtendedAdd(skill_Extended5);
                                //skill.AllExtendeds.Remove(skill_Extended5);
                                skill.ExtendedInit(skill_Extended5, false);
                                list_1.Add(skill_Extended5);
                            //}

                        }
                        else
                        {
                            Skill_Extended skill_Extended5 = (Skill_Extended)ST.sex.Clone();
                            //if (skill_Extended5 is Sex_ImproveClone sexi && sexi.RecordAsItemSex())
                            //{
                                //Skill_Extended sex2 = skill.ExtendedAdd(skill_Extended5);
                                //skill.AllExtendeds.Remove(sex2);
                                Skill_Extended sex2=skill.ExtendedInit(skill_Extended5, false);
                                
                                list_1.Add(sex2);
                            //}

                        }
                    }
                }
                else//启用后，仅json中填写的Sex_ImproveClone会被记录
                {
                    Skill_Extended sex = skill.AllExtendeds.Find((Skill_Extended a) => a.Data != null && a.Data.Key == ST.sex.Data.Key);
                    //if (sex is Sex_ImproveClone)
                    //{
                        list_1.Add(sex);
                    //}

                }
            }





            //--------------------强化Sex
            if (skill.CharinfoSkilldata != null && skill.CharinfoSkilldata.SKillExtended != null)
            {

                if (rec.enforceSex == null)
                {
                    //Debug.Log("itemEX初始化记录技能：" + skill.MySkill.Name+" 发生帧："+Time.frameCount+" basic: "+skill.BasicSkill);
                    rec.enforceSex = new List<SexType>();

                        SexType ST = new SexType();
                        ST.sex = skill.CharinfoSkilldata.SKillExtended;
                        ST.type = ST.sex.GetType();

                    if (ST.type.IsSubclassOf(typeOfSexI) && (ST.sex as Sex_ImproveClone).RecordAsDataSex())
                    {
                        rec.enforceSex.Add(ST);
                    }
                        
                    
                }


                foreach (SexType ST in rec.enforceSex)//技能与记录中均没有ExSex时，生成新的ExSex
                {
                    
                    
                        if (skill.AllExtendeds.Find(a => a.GetType() == ST.type) == null)
                        {
                            if (rec.recExtended.Find(a => a.GetType() == ST.type) == null)
                            {
                                Skill_Extended sex2 = (Skill_Extended)ST.sex.Clone();//Skill_Extended.DataToExtendedC(dataSexKey);

                                skill.ExtendedInit(sex2, true);
                                list_1.Add(sex2);
                            }
                        }
                        else//启用后，仅json中填写的Sex_ImproveClone会被记录
                        {
                            Skill_Extended sex = skill.AllExtendeds.Find((Skill_Extended a) => a.GetType() == ST.type);
                            list_1.Add(sex);
                        }
                    

                }


            }



            /*
            foreach (GDESkillExtendedData exSexGde in skill.MySkill.SKillExtendedItem)//技能与记录中均没有ExSex时，生成新的ExSex
            {
                if (skill.AllExtendeds.Find( a => a.Data != null && a.Data.Key == exSexGde.Key) == null)
                {
                    if (rec.recExtended.Find( a => a.Data != null && a.Data.Key == exSexGde.Key) == null)
                    {
                        if(exSexGde.IsOnesInit)
                        {
                            Skill_Extended skill_Extended5 = Skill_Extended.DataToExtended(exSexGde);
                            if(skill_Extended5 is Sex_ImproveClone sexi && sexi.RecordAsItemSex())
                            {
                                skill_Extended5.isDataExtended = true;
                                skill.ExtendedAdd(skill_Extended5);
                                skill.AllExtendeds.Remove(skill_Extended5);
                                list_1.Add(skill_Extended5);
                            }

                        }
                        else
                        {
                            Skill_Extended skill_Extended5 = Skill_Extended.DataToExtended(exSexGde);
                            if (skill_Extended5 is Sex_ImproveClone sexi )//&& sexi.RecordAsItemSex())
                            {
                                Skill_Extended sex2 = skill.ExtendedAdd(skill_Extended5);
                                skill.AllExtendeds.Remove(sex2);
                                list_1.Add(sex2);
                            }

                        }
                    }
                }
                else//启用后，仅json中填写的Sex_ImproveClone会被记录
                {
                    Skill_Extended sex = skill.AllExtendeds.Find((Skill_Extended a) => a.Data != null && a.Data.Key == exSexGde.Key);
                    if(sex is Sex_ImproveClone)
                    {
                        list_1.Add(sex);
                    }
                    
                }
            }
            */


            foreach (Skill_Extended sex in list_1)
            {
                if (priorNew || rec.recExtended.Find(a=>a.GetType()==sex.GetType())==null)
                {
                    rec.recExtended.Remove(sex);
                    rec.recExtended.Add(sex);
                }
            }
            /*
            if(rec.recExtended.Count <= 0)
            {
                SexRecords.sexSkillList.Remove(rec);
            }
            */
        }

        public static List<Skill_Extended> CheckSkill_Force(Skill skill)
        {
            List<Skill_Extended> list_ans = new List<Skill_Extended>();
            if (SexRecords.sexSkillList.Find(x => x.recSkill == skill) != null)
            {
                SexRec rec = SexRecords.sexSkillList.Find(x => x.recSkill == skill);

                foreach (Skill_Extended sex_protect in rec.recExtended)
                {
                    if (skill.AllExtendeds.Find(a => a.GetType() == sex_protect.GetType()) == null)
                    {
                        list_ans.Add(sex_protect);
                    }
                }
            }
            return list_ans;
        }
        public static List<Skill_Extended> CheckSkill_Force(SexRec rec)
        {
            List<Skill_Extended> list_ans = new List<Skill_Extended>();


                foreach (Skill_Extended sex_protect in rec.recExtended)
                {
                    if (rec.recSkill.AllExtendeds.Find(a => a.GetType() == sex_protect.GetType()) == null)
                    {
                        list_ans.Add(sex_protect);
                    }
                }
            
            return list_ans;
        }

        public static List<Skill_Extended> CheckSkill_Low(Skill skill,Type type)
        {
            SexRec rec = SexRecords.sexSkillList.Find(a => a.recSkill == skill);
            List<Skill_Extended> list_ans = new List<Skill_Extended>();
            if (rec!=null)
            {
                if(BattleSystem.instance.TargetSelecting && BattleSystem.instance.ActWindow.On /*!BattleSystem.instance.DelayWait*/ &&BattleSystem.instance.G_Particle.Count<=0&& rec.frame==Time.frameCount )//&& rec.ipInFrame==type)
                {
                    list_ans.AddRange(rec.resultForFrame);
                }
                else if (rec.frame == Time.frameCount && rec.ipInFrame==type)
                {
                    list_ans.AddRange(rec.resultForFrame);
                }
                else
                {

                    rec.resultForFrame.Clear();
                    rec.resultForFrame.AddRange(SexRecords.CheckSkill_Force(rec));
                    rec.frame = Time.frameCount;
                    rec.ipInFrame = type;

                    list_ans.AddRange(rec.resultForFrame);
                }
            }


            
            return list_ans;
        }


        public static void RestoreSkill(Skill skill, bool priorNew)
        {

            List<Skill_Extended> list_ans = new List<Skill_Extended>();
            if (SexRecords.sexSkillList.Find(x => x.recSkill == skill) != null)
            {
                SexRec rec = SexRecords.sexSkillList.Find(x => x.recSkill == skill);

                foreach (Skill_Extended sex_protect in rec.recExtended)
                {
                    if (skill.AllExtendeds.Find(a => a.GetType() == sex_protect.GetType()) == null)
                    {
                        list_ans.Add(sex_protect);

                    }
                    else
                    {
                        if(!priorNew)
                        {
                            Skill_Extended sex = skill.AllExtendeds.Find(a => a.GetType() == sex_protect.GetType());
                            if(sex!=sex_protect)
                            {
                                skill.AllExtendeds.Remove(sex);
                                skill.AllExtendeds.Add(sex_protect);

                            }
                        }
                    }
                }
            }

            foreach (Skill_Extended sex in list_ans)
            {
                //UnityEngine.Debug.Log("恢复ex："+(sex.GetType()));
                skill.AllExtendeds.Add(sex);

                if (!SexRecords.activeProtect_ModIreturn && SexRecords.SexEnsureType == 1 )
                {
                    //UnityEngine.Debug.Log("进入强保护模式");
                    SexRecords.activeProtect_ModIreturn = true;
                }
            }

        }

        public static List<Skill_Extended> RecordAndCheckSkill(Skill skill)
        {
            SexRecords.RecordSkill(skill, SexRecords.priorNew_Record);
            return SexRecords.CheckSkill_Force(skill);
        }
        /*
        public static List<Skill_Extended> RecordAndRestoreSkill(Skill skill)
        {
            SexRecords.RecordSkill(skill, SexRecords.priorNew_Record);
            return SexRecords.CheckSkill_Force(skill);
        }
        */
        public static void RecordAndRestoreSkill(Skill skill)
        {
            SexRecords.RecordSkill(skill, SexRecords.priorNew_Record);
            SexRecords.RestoreSkill(skill,SexRecords.priorNew_Restore);
        }
        public static void AllRecord()
        {
            if (BattleSystem.instance != null)
            {
                foreach (Skill skill in BattleSystem.instance.AllyTeam.Skills)
                {
                    SexRecords.RecordAndRestoreSkill(skill);
                }
                foreach(Skill skill in BattleSystem.instance.AllyTeam.Skills_Basic)
                {
                    if(skill != null)
                    {
                        SexRecords.RecordAndRestoreSkill(skill);
                    }
                }
                foreach (BattleChar battleChar2 in BattleSystem.instance.AllyTeam.AliveChars_Vanish)
                {

                    if ((battleChar2 as BattleAlly).MyBasicSkill != null && (battleChar2 as BattleAlly).MyBasicSkill.buttonData != null)
                    {
                        SexRecords.RecordAndRestoreSkill((battleChar2 as BattleAlly).MyBasicSkill.buttonData);
                    }

                }

                foreach (SexRec rec in SexRecords.sexSkillList)
                {
                    foreach (Skill_Extended sex in rec.recExtended)
                    {
                        sex.isDestroy = false;
                    }
                }

                for (int i = 0;i<SexRecords.sexSkillList.Count;i++)
                {
                    SexRec rec= SexRecords.sexSkillList[i];
                    if(BattleSystem.instance.G_Particle.Count<=0)
                    {
                        bool flag = false;
                        flag = flag || BattleSystem.instance.AllyTeam.Skills.Contains(rec.recSkill) || BattleSystem.instance.SelectedSkill != rec.recSkill;
                        flag = flag || rec.recSkill.BasicSkill;
                        flag =flag|| BattleSystem.instance.AllyTeam.Skills_Basic.Contains(rec.recSkill);
                        /*
                        foreach (BattleChar battleChar2 in BattleSystem.instance.AllyTeam.AliveChars_Vanish)
                        {
                            if (!flag && (battleChar2 as BattleAlly).MyBasicSkill != null && (battleChar2 as BattleAlly).MyBasicSkill.buttonData != null)
                            {
                                flag=flag|| (battleChar2 as BattleAlly).MyBasicSkill.buttonData==rec.recSkill;
                            }
                        }
                        */
                        if(!flag)
                        {

                            SexRecords.sexSkillList.RemoveAt(i);
                            i--;
                        }
 
                    }
                }
            }
            else
            {
                SexRecords.sexSkillList.Clear();
            }
        }



        public static bool activeProtect_Fixupdata = true;
        public static bool activeProtect_ModIreturn = true;
        public static bool activeProtect_Override = true;

        public static int SexEnsureType = 1;

        public static bool activePassive = true;

        public static bool priorNew_Record = false;
        public static bool priorNew_Restore = true;
        public static List<SexRec> sexSkillList=new List<SexRec>();
    }

    public class SexRec
    {
        public Skill recSkill=new Skill();
        public List<Skill_Extended> recExtended = new List<Skill_Extended>();
        public bool removable = false;

        public List<SexType> dataSex = null;
        public List<SexType> plusSex = null;
        public List<SexType> enforceSex = null;

        public List<Skill_Extended> resultForFrame= new List<Skill_Extended>();
        public int frame = 0;
        public Type ipInFrame = null;
    }

    public class SexType
    {
        public Skill_Extended sex = null;
        public Type type = null;
    }
}
