using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManagementTool.Uis;

namespace ProjectManagementTool;

class Program
{
    static void Main(string[] args)
    {
        MainUi mainUi = new MainUi();

        mainUi.Run();
    }
}
