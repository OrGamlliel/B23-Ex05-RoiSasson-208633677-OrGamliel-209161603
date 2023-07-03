using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopUI
{
    internal class BoardCellButton : Button
    {
        private readonly int r_Row;
        private readonly int r_Column;

        public BoardCellButton(int i_Row, int i_Column)
        {
            r_Row = i_Row;
            r_Column = i_Column;
        }

        public int ButtonRow
        {
            get
            {
                return r_Row;
            }
        }

        public int ButtonCol
        {
            get
            {
                return r_Column;
            }
        }

        public void changeButtonText(char i_NewText)
        {
            if (i_NewText == ' ')
            {
                this.Text = "";
            }
            else
            {
                this.Text = i_NewText.ToString();
            }
        }
    }
}