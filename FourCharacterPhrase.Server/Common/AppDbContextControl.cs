using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourCharacterPhrase.Server.Common
{
    public class AppDbContextControl
    {
        public static void ExecSaveChanges(Action action)
        {
            using (AppDbContext contexForSave = DB.CreateAppDbContextForSave(true))
            {
                action();
                contexForSave.SaveChanges();
            }
        }
    }
}
