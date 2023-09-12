using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Packages.Contracts
{
    public interface IContextLogger
    {
        void Commit(EventLogEntryType eventLogEntryType, string id);

        void Log(string message);
    }
}
