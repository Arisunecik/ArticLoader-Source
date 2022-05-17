using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Management;
using System.Windows.Forms;

namespace ArticLoader
{
    internal static class MainModule
    {

        [DllImport("kernel32", EntryPoint = "SetProcessWorkingSetSize")]
        private static extern int OneWayAttribute([In] IntPtr obj0, [In] int obj1, [In] int obj2);

        internal static void SelfDelete()
        {
            Process.Start(new ProcessStartInfo("cmd.exe",
                    "/C ping 1.1.1.1 -n 1 -w 3000 > Nul & Del \"" +
                    Assembly.GetExecutingAssembly().Location + "\"")
            {
                WindowStyle = ProcessWindowStyle.Hidden
            })
                ?.Dispose();

            Process.GetCurrentProcess().Kill();
        }

        public static async void Update()
        {
            Process.Start("C:\\articupdaterr.exe");
        }


        private static void WellKnownSidType()
        {
            var handle = Process.GetCurrentProcess().Handle;
            while (true)
            {
                do
                {
                    Thread.Sleep(16384);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                } while (Environment.OSVersion.Platform != PlatformID.Win32NT);

                OneWayAttribute(handle, -1, -1);
            }
        }
    }
}