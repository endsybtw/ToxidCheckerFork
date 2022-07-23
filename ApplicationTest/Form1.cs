using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Security.Principal;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using ToxidChecker;


namespace ApplicationTest
{

    public partial class Form1 : Form
    {
        PerformanceCounter cpuCounter;

        public Form1()
        {
            InitializeComponent();
        }

        public string getCurrentCpuUsage()
        {
            return cpuCounter.NextValue() + "%";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            comboBox1.Items.Clear();
            Process[] processCollection = Process.GetProcesses();
            foreach (Process p in processCollection)
            {
                comboBox1.Items.Add(p.ProcessName);
            }
            try
            {
                string joemama = "https://pastebin.com/raw/sVmBiwzi";
                WebClient wb = new WebClient();
                string changelog = wb.DownloadString(joemama);
                textBox1.Text = changelog;
            }
            catch
            {
                textBox1.Text = "Failed to load changelog";
            }
            Process[] processlist = Process.GetProcesses();
            int size = processlist.Length;
            label6.Text = size.ToString() + " Processes runned.";

            
        }
        
    private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            Process[] processCollection = Process.GetProcesses();
            foreach (Process p in processCollection)
            {
                comboBox1.Items.Add(p.ProcessName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                foreach(var process in Process.GetProcessesByName($"{comboBox1.SelectedItem}"))
            {
                    Process.EnterDebugMode();

                    int status = 0;

                    NtSetInformationProcess(process.Handle, 0x1D, ref status, sizeof(int));

                    process.Kill();
                }
            }
            else
            {
                foreach (var process in Process.GetProcessesByName($"{comboBox1.SelectedItem}"))
                {
                    process.Kill();
                }
            }
            
;        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName($"{comboBox1.SelectedItem}"))
            {
                textBox1.Text = $"PID: {process.Id}{Environment.NewLine}Name: {process.ProcessName}{Environment.NewLine}Process start time: {process.StartTime}{Environment.NewLine}Process priority: {process.BasePriority}{Environment.NewLine}Session ID: {process.SessionId}";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AboutForm af = new AboutForm();
            af.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RegistryKey key1;

            key1 = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");

            key1.SetValue("DisableTaskMgr", "0", RegistryValueKind.DWord);

            key1.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RegistryKey key2;

            key2 = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("HKEY_CURRENT_USER\\Software\\Policies\\Microsoft\\Windows");

            key2.SetValue("DisableCMD", "0", RegistryValueKind.DWord);

            key2.Close();
        }
        private void button7_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "ToxidChecker", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Process.EnterDebugMode();

                int status = 1;

                NtSetInformationProcess(Process.GetCurrentProcess().Handle, 0x1D, ref status, sizeof(int));
            }
            else
            {
                return;
            }
        }
        #region
        [DllImport("ntdll.dll")]
        private static extern int NtSetInformationProcess(IntPtr process, int process_class, ref int process_value, int length);

        [Flags]
        public enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);
        #endregion



        private void button8_Click(object sender, EventArgs e)
        {
            Process.EnterDebugMode();

            int status = 0;

            NtSetInformationProcess(Process.GetCurrentProcess().Handle, 0x1D, ref status, sizeof(int));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName($"{comboBox1.SelectedItem}"))
            {
                Process.EnterDebugMode();

                int status = 0;

                NtSetInformationProcess(process.Handle, 0x1D, ref status, sizeof(int));
            }
        }


        private void button7_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName($"{comboBox1.SelectedItem}"))
            {
                ToxidChecker.Class1.Suspend(Process.GetProcessById(process.Id));
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            /*foreach (var process in Process.GetProcessesByName($"{comboBox1.SelectedItem}"))
            {
                ToxidChecker.Class1.Resume(Process.GetProcessById(process.Id));
            }*/
        }

        private void button8_Click_2(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName($"{comboBox1.SelectedItem}"))
            {
                ToxidChecker.Class1.Resume(Process.GetProcessById(process.Id));
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"\"Suspend process\" button will freeze the process and make it unavailable to execute anyting.\n\"Resume process\" button will unfreeze the process and will make it work again.\n\nThis function can be helpful if malware has detection of bypass.", "Resume / Suspend", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show($"\"Kill process\" button will terminate proccess, much like Task Manager will do with the \"End Task\" button.\nNear it, there's a checkmark \"Bypass process critical\",\nwhich can help, if a program or malware has prevented it's termination\nby setting himself as system process, resulting in Blue Screen of Death upon termination.", "Kill process function", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\"Unlock cmd\" button will unlock the ability to run Command line.\nThis function can be helpful if malware has locked the ability to use Command line through registry.", "Unlock cmd function", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\"Unlock taskmgr\" button will unlock the ability to run Task Manager.\nThis function can be helpful if malware has locked the ability to use Task Manager through registry.", "Unlock taskmgr function", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == "Classic")
            {
                this.ForeColor = Color.FromArgb(255, 128, 255);
                textBox1.ForeColor = Color.FromArgb(255, 128, 255);
            }
            if (comboBox2.SelectedItem == "Strawberry")
            {
                this.ForeColor = Color.FromArgb(237, 78, 98);
                textBox1.ForeColor = Color.FromArgb(237, 78, 98);
            }
            if (comboBox2.SelectedItem == "Blueberry")
            {
                this.ForeColor = Color.FromArgb(128, 88, 245);
                textBox1.ForeColor = Color.FromArgb(128, 88, 245);
            }
            if (comboBox2.SelectedItem == "Midnight")
            {
                this.ForeColor = Color.FromArgb(68, 114, 148);
                textBox1.ForeColor = Color.FromArgb(68, 114, 148);
            }
            if (comboBox2.SelectedItem == "Milk")
            {
                this.ForeColor = Color.FromArgb(248, 228, 204);
                textBox1.ForeColor = Color.FromArgb(248, 228, 204);
            }
            if (comboBox2.SelectedItem == "Custom")
            {
                colorDialog1.ShowDialog();
                    this.ForeColor = colorDialog1.Color;
                    textBox1.ForeColor = colorDialog1.Color;
            }

        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("\"Theme switch\" used to change themes of program.\nIn this situation foreground color.", "Theme switch", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            
        }
    }
}