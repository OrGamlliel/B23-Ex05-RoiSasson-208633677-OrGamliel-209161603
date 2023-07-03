using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DesktopUI
{
  
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            UIManager uIManager = new UIManager();

           uIManager.StartGame();
        }
    }
}
