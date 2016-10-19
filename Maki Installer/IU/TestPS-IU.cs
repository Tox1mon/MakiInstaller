﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maki_Installer.IU
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Collection<PSObject> result;
            using (Runspace myRunSpace = RunspaceFactory.CreateRunspace())
            {
                myRunSpace.Open();
                PowerShell powershell = PowerShell.Create();
                powershell.Runspace = myRunSpace;

                using (powershell)
                {
                    powershell.AddCommand("get-process");
                    powershell.AddCommand("Measure");
                    result = powershell.Invoke();
                }

                powershell = null;

                if (result == null || result.Count != 1)
                {
                    throw new InvalidOperationException("Algo ha fallado::no hay resultados");
                }

                PSMemberInfo count = result[0].Properties["Count"];
                string a = count.Value.ToString();
                if (count == null) { throw new InvalidOperationException("The object returned doesn't have a 'count' property"); }
                textBox1.AppendText(a);

                myRunSpace.Close();

            }//using runspace
        }
    }
}
