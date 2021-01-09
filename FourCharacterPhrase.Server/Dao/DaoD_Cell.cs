using FourCharacterPhrase.Server.Common;
using FourCharacterPhrase.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourCharacterPhrase.Server.Dao
{
    public class DaoD_Cell : Framework.DaoBase
    {
        public List<CellEntity> GetCellList(string name)
        {
            return this.context.CellEntitys.Where(m => m.Name == name).ToList();
        }

        public void Save(List<CellEntity> data)
        {
            var saveBeforeData = GetCellList(data.First().Name);

            using (AppDbContext contexForSave = DB.CreateAppDbContextForSave(true))
            {
                foreach (var item in data)
                {
                    EnmEditMode editMode;
                    if (saveBeforeData.Any(m => m.No == item.No)){
                        editMode = EnmEditMode.Update;
                    }
                    else
                    {
                        editMode = EnmEditMode.Insert;
                    }

                    contexForSave.Entry(item).State = ConvertEnmEditModeToEntityState(editMode);
                }
                
                contexForSave.SaveChanges();
            }
        }
    }
}
