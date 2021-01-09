using FourCharacterPhrase.Server.Common;
using FourCharacterPhrase.Server.Framework;
using FourCharacterPhrase.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourCharacterPhrase.Server.Dao
{
    public class DaoD_AnswerNumber : DaoBase
    {
        public List<AnswerNumberEntity> GetAnswerNumberList()
        {
            return this.context.AnswerNumberEntitys.ToList();
        }

        public AnswerNumberEntity GetAnswerNumber(string Name)
        {
            return this.context.AnswerNumberEntitys.Find(Name);
        }

        public void Save(AnswerNumberEntity data)
        {
            var saveBeforeData = GetAnswerNumber(data.Name);

            EnmEditMode editMode;

            if (saveBeforeData == null)
            {
                editMode = EnmEditMode.Insert;
            }
            else
            {
                editMode = EnmEditMode.Update;

                if (data.Count < saveBeforeData.Count) return;

                if (data.Count == saveBeforeData.Count && data.ElapsedTime > saveBeforeData.ElapsedTime) return;
            }

            using (AppDbContext contexForSave = DB.CreateAppDbContextForSave(true))
            {
                contexForSave.Entry(data).State = ConvertEnmEditModeToEntityState(editMode);
                contexForSave.SaveChanges();
            }
        }

        public void Delete()
        {
            var answerNumberList = GetAnswerNumberList();

            using (AppDbContext contexForSave = DB.CreateAppDbContextForSave(true))
            {
                foreach (var data in answerNumberList)
                {
                    contexForSave.Entry(data).State = ConvertEnmEditModeToEntityState(EnmEditMode.Delete);
                }

                contexForSave.SaveChanges();
            } 
        }
    }
}
