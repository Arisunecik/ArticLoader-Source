using ArticLoader.writes;
using ArticLoader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Principal;
using System.Management;
using System.Reflection;
using Colorful;
using Console = Colorful.Console;
using System.Drawing;
using Microsoft.Win32;

namespace ArticLoader
{
	internal class Program
	{

		private static bool thistime;
		private static readonly Random r = new Random();

		private static char R
		{
			get
			{
				var t = r.Next(10);
				if (t <= 2)
					return (char)('0' + r.Next(10));
				if (t <= 4)
					return (char)('a' + r.Next(27));
				if (t <= 6)
					return (char)('A' + r.Next(27));
				return (char)(r.Next(32, 255));
			}
		}

		[DllImport("user32.dll")]
		static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		[DllImport("user32.dll")]
		static extern bool SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);

		[DllImport("user32.dll", SetLastError = true)]
		internal static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr GetConsoleWindow();

		[DllImport("user32.dll", EntryPoint = "GetWindowLong")]
		private static extern IntPtr GetWindowLongPtr32(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
		private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr CreateFile(
			string lpFileName,
			int dwDesiredAccess,
			int dwShareMode,
			IntPtr lpSecurityAttributes,
			int dwCreationDisposition,
			int dwFlagsAndAttributes,
			IntPtr hTemplateFile);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool GetCurrentConsoleFont(
			IntPtr hConsoleOutput,
			bool bMaximumWindow,
			[Out][MarshalAs(UnmanagedType.LPStruct)] ConsoleFontInfo lpConsoleCurrentFont);

		[StructLayout(LayoutKind.Sequential)]
		internal class ConsoleFontInfo
		{
			internal int nFont;
			internal Coord dwFontSize;
		}

		[StructLayout(LayoutKind.Explicit)]
		internal struct Coord
		{
			[FieldOffset(0)]
			internal short X;
			[FieldOffset(2)]
			internal short Y;
		}

		private const int GENERIC_READ = unchecked((int)0x80000000);
		private const int GENERIC_WRITE = 0x40000000;
		private const int FILE_SHARE_READ = 1;
		private const int FILE_SHARE_WRITE = 2;
		private const int INVALID_HANDLE_VALUE = -1;
		private const int OPEN_EXISTING = 3;

		private static void VirtualBox()
		{
			//
		}

		public static void ClearCurrentConsoleLine()
		{
			int currentLineCursor = Console.CursorTop;
			Console.SetCursorPosition(0, Console.CursorTop);
			Console.Write(new string(' ', Console.WindowWidth));
			Console.SetCursorPosition(0, currentLineCursor);
		}

		public static void sexyprint(string prefix, string message, ConsoleColor prefixColor)
		{
			Console.ResetColor();
			Console.Write("  [ ");
			Console.Write(prefix);
			Console.ResetColor();
			Console.Write(" ] ");
			Console.WriteLine(message);
		}

		private static Size GetConsoleFontSize()
		{
			// getting the console out buffer handle
			IntPtr outHandle = CreateFile("CONOUT$", GENERIC_READ | GENERIC_WRITE,
				FILE_SHARE_READ | FILE_SHARE_WRITE,
				IntPtr.Zero,
				OPEN_EXISTING,
				0,
				IntPtr.Zero);
			int errorCode = Marshal.GetLastWin32Error();
			if (outHandle.ToInt32() == INVALID_HANDLE_VALUE)
			{
				throw new IOException("Unable to open CONOUT$", errorCode);
			}

			ConsoleFontInfo cfi = new ConsoleFontInfo();
			if (!GetCurrentConsoleFont(outHandle, false, cfi))
			{
				throw new InvalidOperationException("Unable to get font information.");
			}

			return new Size(cfi.dwFontSize.X, cfi.dwFontSize.Y);
		}


		public enum GWL
		{
			GWL_WNDPROC = (-4),
			GWL_HINSTANCE = (-6),
			GWL_HWNDPARENT = (-8),
			GWL_STYLE = (-16),
			GWL_EXSTYLE = (-20),
			GWL_USERDATA = (-21),
			GWL_ID = (-12)
		}

		public static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
		{
			if (IntPtr.Size == 8)
				return GetWindowLongPtr64(hWnd, nIndex);
			else
				return GetWindowLongPtr32(hWnd, nIndex);
		}

		private static bool RunningAsAdmin()
		{
			WindowsIdentity id = WindowsIdentity.GetCurrent();
			WindowsPrincipal principal = new WindowsPrincipal(id);

			return principal.IsInRole(WindowsBuiltInRole.Administrator);
		}
		static void Main(string[] args)
		{

			WebClient webClientszq = new WebClient();

			string dosyaAdisqweq = "anime.png";
			string sakardesq = @"C:\\";
			webClientszq.DownloadFile("https://cdn.discordapp.com/attachments/970644396406095892/970781139356749935/anime.png", sakardesq + dosyaAdisqweq);


			//yazıyı değiştirirseniz loader bozulabilir :D
			string[] storyFragments = new string[]
			{
				"",
				"",
				"",
				"		  █████╗ ██████╗ ████████╗██╗ ██████╗ ██╗      ██████╗  █████╗ ██████╗ ███████╗██████╗ ",
				"		 ██╔══██╗██╔══██╗╚══██╔══╝██║██╔════╝ ██║     ██╔═══██╗██╔══██╗██╔══██╗██╔════╝██╔══██╗",
				"		 ███████║██████╔╝   ██║   ██║██║      ██║     ██║   ██║███████║██║  ██║█████╗  ██████╔╝",
				"		 ██╔══██║██╔══██╗   ██║   ██║██║      ██║     ██║   ██║██╔══██║██║  ██║██╔══╝  ██╔══██╗",
				"		 ██║  ██║██║  ██║   ██║   ██║╚██████╗ ███████╗╚██████╔╝██║  ██║██████╔╝███████╗██║  ██║",
				"		 ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   ╚═╝ ╚═════╝ ╚══════╝ ╚═════╝ ╚═╝  ╚═╝╚═════╝ ╚══════╝╚═╝  ╚═╝",
				"",
				"",
				"          ～          ～          ～          ～          ～          ～          ～          ～          ～          ～        ",
				"",
				""
			};


			Console.Title = "ArticLoader | <" + Undetected.RandomString() + ">";

			List<char> chars = new List<char>()
			{
			    'r', 'u', 'n', 'n', 'i', 'n', 'g', ' ', 'a', 's', ' ', 'a', 'd', 'm',
			    'i', 'n', ' ', 'r', 'e', 'q', 'u', 'i', 'r', 'e', 'd'
			};

			if (!RunningAsAdmin())
			{
				Console.WriteWithGradient(chars, Color.Yellow, Color.Fuchsia, 14);
				ProcessStartInfo proc = new ProcessStartInfo();
				proc.UseShellExecute = true;
				proc.WorkingDirectory = Environment.CurrentDirectory;
				proc.FileName = Assembly.GetEntryAssembly().CodeBase;
				proc.Verb = "runas";
				try
				{
					Process.Start(proc);
					Environment.Exit(0);
				}
				catch (Exception ex)
				{
					Console.WriteLine("This program must be run as an administrator! \n\n" + ex.ToString());
					Environment.Exit(0);
				}
			}

			//ana yazilar
			{

				Console.Clear();
				WebClient wb = new WebClient();
				string HWIDLIST = wb.DownloadString("https://raw.githubusercontent.com/Arisunecik/articproject/main/blockedhwid"); // hwid list where u saving hwid's
				if (HWIDLIST.Contains(System.Security.Principal.WindowsIdentity.GetCurrent().User.Value))
				{
					MainModule.SelfDelete();
					Environment.Exit(0);
				}
				else
				{
					
				}

				string dllDirectory = System.Environment.GetEnvironmentVariable("USERPROFILE") + @"\AppData\Local\Temp\vRqdBOeShmlwkVgqXGTi";
				if (!Directory.Exists(dllDirectory))
				{
					Directory.CreateDirectory(dllDirectory);
				}

				WebClient wbqwe = new WebClient();
				string HWIDLIST1 = wb.DownloadString("https://raw.githubusercontent.com/Arisunecik/articproject/main/zard"); // hwid list where u saving hwid's
				if (HWIDLIST1.Contains(System.Security.Principal.WindowsIdentity.GetCurrent().User.Value))
				{
					MessageBox.Show("developer mode");
				}
				else
				{
				

				}



				int r = 225;
				int g = 255;
				int b = 250;

				for (int i = 0; i < 14; i++)
				{

					Console.WriteLine(storyFragments[i], Color.FromArgb(r, g, b));


					r -= 17;
					b -= 11;
				}

				Point location = new Point(86, 15);
				Size imageSize = new Size(25, 17); // desired image size in characters

				string path = @"C:\\anime.png";
				using (Graphics ge = Graphics.FromHwnd(GetConsoleWindow()))
				{
					using (Image image = Image.FromFile(path))
					{
						Size fontSize = GetConsoleFontSize();

						// translating the character positions to pixels
						Rectangle imageRect = new Rectangle(
							location.X * fontSize.Width,
							location.Y * fontSize.Height,
							imageSize.Width * fontSize.Width,
							imageSize.Height * fontSize.Height);
						ge.DrawImage(image, imageRect);
					}
				}

				Random rnd = new Random();
				rnd.Next(2000, 9999).ToString();

				Console.ForegroundColor = Color.White;
				string dream = "  [ {0} ]  Welcome to artic!";
				Formatter[] fruits = new Formatter[]
				{
					new Formatter("～", Color.Purple),
				};

				Console.WriteLineFormatted(dream, Color.White, fruits);

				//sunucu check
				Console.Title = "ArticLoader | < Checking servers... >";
				string dream2 = "  [ {0} ]  Checking artic servers...";
				Console.WriteLineFormatted(dream2, Color.White, fruits);
				Thread.Sleep(500);
				WebClient webClient3 = new WebClient();
				if (webClient3.DownloadString("https://raw.githubusercontent.com/Arisunecik/articproject/main/sunucuartic").Contains("artic2"))
				{

					string dream3 = "  [ {0} ]  Connected to servers";
					Console.WriteLineFormatted(dream3, Color.White, fruits);
					Console.Title = "ArticLoader | <" + Undetected.RandomString() + ">";
					Thread.Sleep(500);
				}
				else
				{
					string dream4 = "  [ {0} ]  Cloud not connect to servers, check discord!";
					Console.WriteLineFormatted(dream4, Color.White, fruits);
					Thread.Sleep(4000);
					Environment.Exit(0);
				}

				
				if (File.Exists(@"C:\\artic.exe"))
				{
					
				}
				else
				{
					WebClient webClient311111 = new WebClient();
					string dosyaAdi11 = "artic.exe";
					string dosyakodumu11 = "C:\\";

					webClient311111.DownloadFile("https://github.com/Arisunecik/articdownload/raw/main/artic.exe", dosyakodumu11 + dosyaAdi11);
					Console.ForegroundColor = Color.Green;
					Thread.Sleep(500);
				}

				//client check
				Thread.Sleep(500);
				Console.WriteLine("");
				Console.Title = "ArticLoader | < Checking client updates... >";
				string dream5 = "  [ {0} ]  Checking client updates...";
				Console.WriteLineFormatted(dream5, Color.White, fruits);
				WebClient webClient2 = new WebClient();
				if (webClient2.DownloadString("https://raw.githubusercontent.com/Arisunecik/articproject/main/clientversion").Contains("102"))
				{
					string dream6 = "  [ {0} ]  Update not found.";
					Console.WriteLineFormatted(dream6, Color.White, fruits);
					Console.Title = "ArticLoader | <" + Undetected.RandomString() + ">";
					Thread.Sleep(500);
				}
				else
				{
					Console.Title = "ArticLoader | < UPDATING... >";
					string dream7 = "  [ {0} ]  Update found";
					Console.WriteLineFormatted(dream7, Color.White, fruits);

					if (System.IO.File.Exists("C:\\artic.exe"))
					{
						System.IO.File.Delete("C:\\artic.exe");
					}

					Console.WriteLine("");

					WebClient webClient31111 = new WebClient();
					string dosyaAdi = "artic.exe";
					string dosyakodumu = "C:\\";
					Console.ForegroundColor = Color.White;
					string dream8 = "  [ {0} ]  Downloading...";
					Console.WriteLineFormatted(dream8, Color.White, fruits);
					webClient31111.DownloadFile("https://github.com/Arisunecik/articdownload/raw/main/artic.exe", dosyakodumu + dosyaAdi);
					Console.ForegroundColor = Color.Green;
					string dream15 = "  [ {0} ]  Succes!";
					Console.WriteLineFormatted(dream15, Color.White, fruits);
					string DosyaYolu3 = "C:\\" + "artic" + ".exe";
					if (System.IO.File.Exists(DosyaYolu3))
					{

					}
					else
					{
						Console.ForegroundColor = Color.Red;
						Console.WriteLine(" [ ～ ] Client bulunamadı, lütfen programı yönetici olarak başlatın veyada antivirüs programınızı kapatın,");
						Console.WriteLine(" [ ～ ] Sorun hala devam ediyorsa lütfen discord adresimize gelin!");
						string dosyaAdi151 = "artic.exe";

						Console.WriteLine("");

						//if yapmaya usendım sorgulama oc
						WebClient webClient111 = new WebClient();
						Console.WriteLine(" [ ～ ] Yükleme işlemi tekrar deneniyor...");
						webClient111.DownloadFile("https://github.com/Arisunecik/articdownload/raw/main/artic.exe", dllDirectory + dosyaAdi151);
						Console.WriteLine(" [ ～ ] Client yüklendi, loaderi tekrar baslatin!");
						Console.WriteLine("");
						Console.WriteLine("    biseler yazip enter bas la");
						Console.ReadLine();
						Environment.Exit(0);
					}
				}


				Thread.Sleep(500);
				Console.WriteLine("");
				Console.Title = "ArticLoader | < Checking loader updates... >";
				string dream9 = "  [ {0} ]  Checking loader updates...";
				Console.WriteLineFormatted(dream9, Color.White, fruits);
				WebClient webClientload = new WebClient();
				if (webClientload.DownloadString("https://raw.githubusercontent.com/Arisunecik/articproject/main/versionchecker").Contains("102"))
				{
					string dream10 = "  [ {0} ]  Update not found.";
					Console.WriteLineFormatted(dream10, Color.White, fruits);
					Console.Title = "ArticLoader | <" + Undetected.RandomString() + ">";
					Thread.Sleep(500);
				}
				else
				{
					Console.Title = "ArticLoader | < UPDATING... >";
					string dream11 = "  [ {0} ]  Update found!";
					Console.WriteLineFormatted(dream11, Color.White, fruits);
					MainModule.Update();
					MainModule.SelfDelete();
				}

				Thread.Sleep(500);
				Console.ForegroundColor = Color.Cyan;
				Console.WriteLine("");
				string dream12 = "  [ {0} ]  Starting client, please wait.";
				Console.WriteLineFormatted(dream12, Color.White, fruits);
				Process.Start($"C:\\artic.exe");
				Console.ForegroundColor = Color.Red;
				Thread.Sleep(13000);
				//clientten otomatik loaderi kapattirma yapabilirsiniz

				Environment.Exit(0);
			}
		}
	}
}




