using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSys_EPAMTest
{
    interface IBugTrackDB
    {
        string CreateDB();
        string LoadDB();

        string Add();
        string Edit();
        string Delete();

        string Refresh();
    }
}
