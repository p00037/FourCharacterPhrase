using System;
using System.Collections.Generic;
using System.Text;

namespace FourCharacterPhrase.Shared
{
    public class CellEntity
    {
        public string Name { get; set; }

        public int No { get; set; }

        public char Value { get; set; }

        public CellStatus Status { get; set; } = CellStatus.None;

        public void ChangeStatus()
        {
            if (Status == CellStatus.Completed)
            {
                return;
            }

            if (Status == CellStatus.Selecting)
            {
                Status = CellStatus.None;
                return;
            }

            if (Status == CellStatus.None)
            {
                Status = CellStatus.Selecting;
                return;
            }
        }
    }
}
