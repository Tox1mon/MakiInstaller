﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;

namespace Maki_Installer.PS
{
    class PSObjectInvoker
    {
        private Collection<PSObject> invokePS(string script)
        {
            Collection<PSObject> result;
            using (Runspace myRunSpace = RunspaceFactory.CreateRunspace())
            {
                myRunSpace.Open();
                PowerShell powershell = PowerShell.Create();
                powershell.Runspace = myRunSpace;
                using (powershell)
                {
                    powershell.AddScript(script);
                    result = powershell.Invoke();
                }
                powershell = null;
                if (result == null || result.Count != 1)
                {
                    throw new InvalidOperationException("Algo ha fallado::no hay resultados");
                }
                myRunSpace.Close();
                return result;
            }//using runspace
        }//method
        /*
                PSMemberInfo count = result[0].Properties["Id"];
                string a = count.Value.ToString();
                if (count == null) { throw new InvalidOperationException("The object returned doesn't have a 'count' property"); }
        */
        private void invokeVoid(string script)
        {
            using (Runspace myRunSpace = RunspaceFactory.CreateRunspace())
            {
                myRunSpace.Open();

                using (PowerShell powershell = PowerShell.Create())
                {
                    powershell.Runspace = myRunSpace;
                    powershell.AddScript("c:\\_\\script.ps1");
                    powershell.Invoke();
                }
                myRunSpace.Close();

            }//using runspace
        }//method
    }
}
