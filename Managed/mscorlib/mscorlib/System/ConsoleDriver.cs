using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200020C RID: 524
	internal static class ConsoleDriver
	{
		// Token: 0x060018B4 RID: 6324 RVA: 0x0005DFC0 File Offset: 0x0005C1C0
		static ConsoleDriver()
		{
			if (!ConsoleDriver.IsConsole)
			{
				ConsoleDriver.driver = ConsoleDriver.CreateNullConsoleDriver();
				return;
			}
			if (Environment.IsRunningOnWindows)
			{
				ConsoleDriver.driver = ConsoleDriver.CreateWindowsConsoleDriver();
				return;
			}
			string environmentVariable = Environment.GetEnvironmentVariable("TERM");
			if (environmentVariable == "dumb")
			{
				ConsoleDriver.is_console = false;
				ConsoleDriver.driver = ConsoleDriver.CreateNullConsoleDriver();
				return;
			}
			ConsoleDriver.driver = ConsoleDriver.CreateTermInfoDriver(environmentVariable);
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0005E025 File Offset: 0x0005C225
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IConsoleDriver CreateNullConsoleDriver()
		{
			return new NullConsoleDriver();
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0005E02C File Offset: 0x0005C22C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IConsoleDriver CreateWindowsConsoleDriver()
		{
			return new WindowsConsoleDriver();
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x0005E033 File Offset: 0x0005C233
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IConsoleDriver CreateTermInfoDriver(string term)
		{
			return new TermInfoDriver(term);
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x0005E03B File Offset: 0x0005C23B
		public static bool Initialized
		{
			get
			{
				return ConsoleDriver.driver.Initialized;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x0005E047 File Offset: 0x0005C247
		// (set) Token: 0x060018BA RID: 6330 RVA: 0x0005E053 File Offset: 0x0005C253
		public static ConsoleColor BackgroundColor
		{
			get
			{
				return ConsoleDriver.driver.BackgroundColor;
			}
			set
			{
				if (value < ConsoleColor.Black || value > ConsoleColor.White)
				{
					throw new ArgumentOutOfRangeException("value", "Not a ConsoleColor value.");
				}
				ConsoleDriver.driver.BackgroundColor = value;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x0005E079 File Offset: 0x0005C279
		// (set) Token: 0x060018BC RID: 6332 RVA: 0x0005E085 File Offset: 0x0005C285
		public static int BufferHeight
		{
			get
			{
				return ConsoleDriver.driver.BufferHeight;
			}
			set
			{
				ConsoleDriver.driver.BufferHeight = value;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x0005E092 File Offset: 0x0005C292
		// (set) Token: 0x060018BE RID: 6334 RVA: 0x0005E09E File Offset: 0x0005C29E
		public static int BufferWidth
		{
			get
			{
				return ConsoleDriver.driver.BufferWidth;
			}
			set
			{
				ConsoleDriver.driver.BufferWidth = value;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x0005E0AB File Offset: 0x0005C2AB
		public static bool CapsLock
		{
			get
			{
				return ConsoleDriver.driver.CapsLock;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x0005E0B7 File Offset: 0x0005C2B7
		// (set) Token: 0x060018C1 RID: 6337 RVA: 0x0005E0C3 File Offset: 0x0005C2C3
		public static int CursorLeft
		{
			get
			{
				return ConsoleDriver.driver.CursorLeft;
			}
			set
			{
				ConsoleDriver.driver.CursorLeft = value;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x0005E0D0 File Offset: 0x0005C2D0
		// (set) Token: 0x060018C3 RID: 6339 RVA: 0x0005E0DC File Offset: 0x0005C2DC
		public static int CursorSize
		{
			get
			{
				return ConsoleDriver.driver.CursorSize;
			}
			set
			{
				ConsoleDriver.driver.CursorSize = value;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x0005E0E9 File Offset: 0x0005C2E9
		// (set) Token: 0x060018C5 RID: 6341 RVA: 0x0005E0F5 File Offset: 0x0005C2F5
		public static int CursorTop
		{
			get
			{
				return ConsoleDriver.driver.CursorTop;
			}
			set
			{
				ConsoleDriver.driver.CursorTop = value;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x0005E102 File Offset: 0x0005C302
		// (set) Token: 0x060018C7 RID: 6343 RVA: 0x0005E10E File Offset: 0x0005C30E
		public static bool CursorVisible
		{
			get
			{
				return ConsoleDriver.driver.CursorVisible;
			}
			set
			{
				ConsoleDriver.driver.CursorVisible = value;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x0005E11B File Offset: 0x0005C31B
		public static bool KeyAvailable
		{
			get
			{
				return ConsoleDriver.driver.KeyAvailable;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x0005E127 File Offset: 0x0005C327
		// (set) Token: 0x060018CA RID: 6346 RVA: 0x0005E133 File Offset: 0x0005C333
		public static ConsoleColor ForegroundColor
		{
			get
			{
				return ConsoleDriver.driver.ForegroundColor;
			}
			set
			{
				if (value < ConsoleColor.Black || value > ConsoleColor.White)
				{
					throw new ArgumentOutOfRangeException("value", "Not a ConsoleColor value.");
				}
				ConsoleDriver.driver.ForegroundColor = value;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x0005E159 File Offset: 0x0005C359
		public static int LargestWindowHeight
		{
			get
			{
				return ConsoleDriver.driver.LargestWindowHeight;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x0005E165 File Offset: 0x0005C365
		public static int LargestWindowWidth
		{
			get
			{
				return ConsoleDriver.driver.LargestWindowWidth;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x0005E171 File Offset: 0x0005C371
		public static bool NumberLock
		{
			get
			{
				return ConsoleDriver.driver.NumberLock;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x0005E17D File Offset: 0x0005C37D
		// (set) Token: 0x060018CF RID: 6351 RVA: 0x0005E189 File Offset: 0x0005C389
		public static string Title
		{
			get
			{
				return ConsoleDriver.driver.Title;
			}
			set
			{
				ConsoleDriver.driver.Title = value;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0005E196 File Offset: 0x0005C396
		// (set) Token: 0x060018D1 RID: 6353 RVA: 0x0005E1A2 File Offset: 0x0005C3A2
		public static bool TreatControlCAsInput
		{
			get
			{
				return ConsoleDriver.driver.TreatControlCAsInput;
			}
			set
			{
				ConsoleDriver.driver.TreatControlCAsInput = value;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x0005E1AF File Offset: 0x0005C3AF
		// (set) Token: 0x060018D3 RID: 6355 RVA: 0x0005E1BB File Offset: 0x0005C3BB
		public static int WindowHeight
		{
			get
			{
				return ConsoleDriver.driver.WindowHeight;
			}
			set
			{
				ConsoleDriver.driver.WindowHeight = value;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x0005E1C8 File Offset: 0x0005C3C8
		// (set) Token: 0x060018D5 RID: 6357 RVA: 0x0005E1D4 File Offset: 0x0005C3D4
		public static int WindowLeft
		{
			get
			{
				return ConsoleDriver.driver.WindowLeft;
			}
			set
			{
				ConsoleDriver.driver.WindowLeft = value;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x0005E1E1 File Offset: 0x0005C3E1
		// (set) Token: 0x060018D7 RID: 6359 RVA: 0x0005E1ED File Offset: 0x0005C3ED
		public static int WindowTop
		{
			get
			{
				return ConsoleDriver.driver.WindowTop;
			}
			set
			{
				ConsoleDriver.driver.WindowTop = value;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x0005E1FA File Offset: 0x0005C3FA
		// (set) Token: 0x060018D9 RID: 6361 RVA: 0x0005E206 File Offset: 0x0005C406
		public static int WindowWidth
		{
			get
			{
				return ConsoleDriver.driver.WindowWidth;
			}
			set
			{
				ConsoleDriver.driver.WindowWidth = value;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x0005E213 File Offset: 0x0005C413
		public static bool IsErrorRedirected
		{
			get
			{
				return !ConsoleDriver.Isatty(MonoIO.ConsoleError);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x0005E222 File Offset: 0x0005C422
		public static bool IsOutputRedirected
		{
			get
			{
				return !ConsoleDriver.Isatty(MonoIO.ConsoleOutput);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x0005E231 File Offset: 0x0005C431
		public static bool IsInputRedirected
		{
			get
			{
				return !ConsoleDriver.Isatty(MonoIO.ConsoleInput);
			}
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0005E240 File Offset: 0x0005C440
		public static void Beep(int frequency, int duration)
		{
			ConsoleDriver.driver.Beep(frequency, duration);
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0005E24E File Offset: 0x0005C44E
		public static void Clear()
		{
			ConsoleDriver.driver.Clear();
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0005E25C File Offset: 0x0005C45C
		public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
		{
			ConsoleDriver.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, ' ', ConsoleColor.Black, ConsoleColor.Black);
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x0005E27C File Offset: 0x0005C47C
		public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
			ConsoleDriver.driver.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, sourceChar, sourceForeColor, sourceBackColor);
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0005E2A1 File Offset: 0x0005C4A1
		public static void Init()
		{
			ConsoleDriver.driver.Init();
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0005E2B0 File Offset: 0x0005C4B0
		public static int Read()
		{
			return (int)ConsoleDriver.ReadKey(false).KeyChar;
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0005E2CB File Offset: 0x0005C4CB
		public static string ReadLine()
		{
			return ConsoleDriver.driver.ReadLine();
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0005E2D7 File Offset: 0x0005C4D7
		public static ConsoleKeyInfo ReadKey(bool intercept)
		{
			return ConsoleDriver.driver.ReadKey(intercept);
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x0005E2E4 File Offset: 0x0005C4E4
		public static void ResetColor()
		{
			ConsoleDriver.driver.ResetColor();
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0005E2F0 File Offset: 0x0005C4F0
		public static void SetBufferSize(int width, int height)
		{
			ConsoleDriver.driver.SetBufferSize(width, height);
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0005E2FE File Offset: 0x0005C4FE
		public static void SetCursorPosition(int left, int top)
		{
			ConsoleDriver.driver.SetCursorPosition(left, top);
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x0005E30C File Offset: 0x0005C50C
		public static void SetWindowPosition(int left, int top)
		{
			ConsoleDriver.driver.SetWindowPosition(left, top);
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x0005E31A File Offset: 0x0005C51A
		public static void SetWindowSize(int width, int height)
		{
			ConsoleDriver.driver.SetWindowSize(width, height);
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x0005E328 File Offset: 0x0005C528
		public static bool IsConsole
		{
			get
			{
				if (ConsoleDriver.called_isatty)
				{
					return ConsoleDriver.is_console;
				}
				ConsoleDriver.is_console = ConsoleDriver.Isatty(MonoIO.ConsoleOutput) && ConsoleDriver.Isatty(MonoIO.ConsoleInput);
				ConsoleDriver.called_isatty = true;
				return ConsoleDriver.is_console;
			}
		}

		// Token: 0x060018EB RID: 6379
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Isatty(IntPtr handle);

		// Token: 0x060018EC RID: 6380
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int InternalKeyAvailable(int ms_timeout);

		// Token: 0x060018ED RID: 6381
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool TtySetup(string keypadXmit, string teardown, out byte[] control_characters, out int* address);

		// Token: 0x060018EE RID: 6382
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SetEcho(bool wantEcho);

		// Token: 0x060018EF RID: 6383
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SetBreak(bool wantBreak);

		// Token: 0x04000C88 RID: 3208
		internal static IConsoleDriver driver;

		// Token: 0x04000C89 RID: 3209
		private static bool is_console;

		// Token: 0x04000C8A RID: 3210
		private static bool called_isatty;
	}
}
