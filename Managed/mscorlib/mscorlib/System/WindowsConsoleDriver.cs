using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	// Token: 0x0200025C RID: 604
	internal class WindowsConsoleDriver : IConsoleDriver
	{
		// Token: 0x06001BAE RID: 7086 RVA: 0x00068A14 File Offset: 0x00066C14
		public WindowsConsoleDriver()
		{
			this.outputHandle = WindowsConsoleDriver.GetStdHandle(Handles.STD_OUTPUT);
			this.inputHandle = WindowsConsoleDriver.GetStdHandle(Handles.STD_INPUT);
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			this.defaultAttribute = consoleScreenBufferInfo.Attribute;
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x00068A63 File Offset: 0x00066C63
		private static ConsoleColor GetForeground(short attr)
		{
			attr &= 15;
			return (ConsoleColor)attr;
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x00068A6D File Offset: 0x00066C6D
		private static ConsoleColor GetBackground(short attr)
		{
			attr &= 240;
			attr = (short)(attr >> 4);
			return (ConsoleColor)attr;
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x00068A80 File Offset: 0x00066C80
		private static short GetAttrForeground(int attr, ConsoleColor color)
		{
			attr &= -16;
			return (short)(attr | (int)color);
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x00068A8C File Offset: 0x00066C8C
		private static short GetAttrBackground(int attr, ConsoleColor color)
		{
			attr &= -241;
			int num = (int)((int)color << 4);
			return (short)(attr | num);
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x00068AAC File Offset: 0x00066CAC
		// (set) Token: 0x06001BB4 RID: 7092 RVA: 0x00068ADC File Offset: 0x00066CDC
		public ConsoleColor BackgroundColor
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return WindowsConsoleDriver.GetBackground(consoleScreenBufferInfo.Attribute);
			}
			set
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				short attrBackground = WindowsConsoleDriver.GetAttrBackground((int)consoleScreenBufferInfo.Attribute, value);
				WindowsConsoleDriver.SetConsoleTextAttribute(this.outputHandle, attrBackground);
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x00068B1C File Offset: 0x00066D1C
		// (set) Token: 0x06001BB6 RID: 7094 RVA: 0x00068B4A File Offset: 0x00066D4A
		public int BufferHeight
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.Size.Y;
			}
			set
			{
				this.SetBufferSize(this.BufferWidth, value);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x00068B5C File Offset: 0x00066D5C
		// (set) Token: 0x06001BB8 RID: 7096 RVA: 0x00068B8A File Offset: 0x00066D8A
		public int BufferWidth
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.Size.X;
			}
			set
			{
				this.SetBufferSize(value, this.BufferHeight);
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x00068B99 File Offset: 0x00066D99
		public bool CapsLock
		{
			get
			{
				return (WindowsConsoleDriver.GetKeyState(20) & 1) == 1;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001BBA RID: 7098 RVA: 0x00068BA8 File Offset: 0x00066DA8
		// (set) Token: 0x06001BBB RID: 7099 RVA: 0x00068BD6 File Offset: 0x00066DD6
		public int CursorLeft
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.CursorPosition.X;
			}
			set
			{
				this.SetCursorPosition(value, this.CursorTop);
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x00068BE8 File Offset: 0x00066DE8
		// (set) Token: 0x06001BBD RID: 7101 RVA: 0x00068C14 File Offset: 0x00066E14
		public int CursorSize
		{
			get
			{
				ConsoleCursorInfo consoleCursorInfo = default(ConsoleCursorInfo);
				WindowsConsoleDriver.GetConsoleCursorInfo(this.outputHandle, out consoleCursorInfo);
				return consoleCursorInfo.Size;
			}
			set
			{
				if (value < 1 || value > 100)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				ConsoleCursorInfo consoleCursorInfo = default(ConsoleCursorInfo);
				WindowsConsoleDriver.GetConsoleCursorInfo(this.outputHandle, out consoleCursorInfo);
				consoleCursorInfo.Size = value;
				if (!WindowsConsoleDriver.SetConsoleCursorInfo(this.outputHandle, ref consoleCursorInfo))
				{
					throw new Exception("SetConsoleCursorInfo failed");
				}
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x00068C70 File Offset: 0x00066E70
		// (set) Token: 0x06001BBF RID: 7103 RVA: 0x00068C9E File Offset: 0x00066E9E
		public int CursorTop
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.CursorPosition.Y;
			}
			set
			{
				this.SetCursorPosition(this.CursorLeft, value);
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x00068CB0 File Offset: 0x00066EB0
		// (set) Token: 0x06001BC1 RID: 7105 RVA: 0x00068CDC File Offset: 0x00066EDC
		public bool CursorVisible
		{
			get
			{
				ConsoleCursorInfo consoleCursorInfo = default(ConsoleCursorInfo);
				WindowsConsoleDriver.GetConsoleCursorInfo(this.outputHandle, out consoleCursorInfo);
				return consoleCursorInfo.Visible;
			}
			set
			{
				ConsoleCursorInfo consoleCursorInfo = default(ConsoleCursorInfo);
				WindowsConsoleDriver.GetConsoleCursorInfo(this.outputHandle, out consoleCursorInfo);
				if (consoleCursorInfo.Visible == value)
				{
					return;
				}
				consoleCursorInfo.Visible = value;
				if (!WindowsConsoleDriver.SetConsoleCursorInfo(this.outputHandle, ref consoleCursorInfo))
				{
					throw new Exception("SetConsoleCursorInfo failed");
				}
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x00068D2C File Offset: 0x00066F2C
		// (set) Token: 0x06001BC3 RID: 7107 RVA: 0x00068D5C File Offset: 0x00066F5C
		public ConsoleColor ForegroundColor
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return WindowsConsoleDriver.GetForeground(consoleScreenBufferInfo.Attribute);
			}
			set
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				short attrForeground = WindowsConsoleDriver.GetAttrForeground((int)consoleScreenBufferInfo.Attribute, value);
				WindowsConsoleDriver.SetConsoleTextAttribute(this.outputHandle, attrForeground);
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x00068D9C File Offset: 0x00066F9C
		public bool KeyAvailable
		{
			get
			{
				InputRecord inputRecord = default(InputRecord);
				int num;
				while (WindowsConsoleDriver.PeekConsoleInput(this.inputHandle, out inputRecord, 1, out num))
				{
					if (num == 0)
					{
						return false;
					}
					if (inputRecord.EventType == 1 && inputRecord.KeyDown && !WindowsConsoleDriver.IsModifierKey(inputRecord.VirtualKeyCode))
					{
						return true;
					}
					if (!WindowsConsoleDriver.ReadConsoleInput(this.inputHandle, out inputRecord, 1, out num))
					{
						throw new InvalidOperationException("Error in ReadConsoleInput " + Marshal.GetLastWin32Error());
					}
				}
				throw new InvalidOperationException("Error in PeekConsoleInput " + Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool Initialized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x00068E30 File Offset: 0x00067030
		public int LargestWindowHeight
		{
			get
			{
				Coord largestConsoleWindowSize = WindowsConsoleDriver.GetLargestConsoleWindowSize(this.outputHandle);
				if (largestConsoleWindowSize.X == 0 && largestConsoleWindowSize.Y == 0)
				{
					throw new Exception("GetLargestConsoleWindowSize" + Marshal.GetLastWin32Error());
				}
				return (int)largestConsoleWindowSize.Y;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x00068E7C File Offset: 0x0006707C
		public int LargestWindowWidth
		{
			get
			{
				Coord largestConsoleWindowSize = WindowsConsoleDriver.GetLargestConsoleWindowSize(this.outputHandle);
				if (largestConsoleWindowSize.X == 0 && largestConsoleWindowSize.Y == 0)
				{
					throw new Exception("GetLargestConsoleWindowSize" + Marshal.GetLastWin32Error());
				}
				return (int)largestConsoleWindowSize.X;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x00068EC5 File Offset: 0x000670C5
		public bool NumberLock
		{
			get
			{
				return (WindowsConsoleDriver.GetKeyState(144) & 1) == 1;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x00068ED8 File Offset: 0x000670D8
		// (set) Token: 0x06001BCA RID: 7114 RVA: 0x00068F35 File Offset: 0x00067135
		public string Title
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				if (WindowsConsoleDriver.GetConsoleTitle(stringBuilder, 1024) == 0)
				{
					stringBuilder = new StringBuilder(26001);
					if (WindowsConsoleDriver.GetConsoleTitle(stringBuilder, 26000) == 0)
					{
						throw new Exception("Got " + Marshal.GetLastWin32Error());
					}
				}
				return stringBuilder.ToString();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!WindowsConsoleDriver.SetConsoleTitle(value))
				{
					throw new Exception("Got " + Marshal.GetLastWin32Error());
				}
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001BCB RID: 7115 RVA: 0x00068F68 File Offset: 0x00067168
		// (set) Token: 0x06001BCC RID: 7116 RVA: 0x00068FA4 File Offset: 0x000671A4
		public bool TreatControlCAsInput
		{
			get
			{
				int num;
				if (!WindowsConsoleDriver.GetConsoleMode(this.inputHandle, out num))
				{
					throw new Exception("Failed in GetConsoleMode: " + Marshal.GetLastWin32Error());
				}
				return (num & 1) == 0;
			}
			set
			{
				int num;
				if (!WindowsConsoleDriver.GetConsoleMode(this.inputHandle, out num))
				{
					throw new Exception("Failed in GetConsoleMode: " + Marshal.GetLastWin32Error());
				}
				if ((num & 1) == 0 == value)
				{
					return;
				}
				if (value)
				{
					num &= -2;
				}
				else
				{
					num |= 1;
				}
				if (!WindowsConsoleDriver.SetConsoleMode(this.inputHandle, num))
				{
					throw new Exception("Failed in SetConsoleMode: " + Marshal.GetLastWin32Error());
				}
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001BCD RID: 7117 RVA: 0x0006901C File Offset: 0x0006721C
		// (set) Token: 0x06001BCE RID: 7118 RVA: 0x00069058 File Offset: 0x00067258
		public int WindowHeight
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)(consoleScreenBufferInfo.Window.Bottom - consoleScreenBufferInfo.Window.Top + 1);
			}
			set
			{
				this.SetWindowSize(this.WindowWidth, value);
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x00069068 File Offset: 0x00067268
		// (set) Token: 0x06001BD0 RID: 7120 RVA: 0x00069096 File Offset: 0x00067296
		public int WindowLeft
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.Window.Left;
			}
			set
			{
				this.SetWindowPosition(value, this.WindowTop);
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x000690A8 File Offset: 0x000672A8
		// (set) Token: 0x06001BD2 RID: 7122 RVA: 0x000690D6 File Offset: 0x000672D6
		public int WindowTop
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.Window.Top;
			}
			set
			{
				this.SetWindowPosition(this.WindowLeft, value);
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x000690E8 File Offset: 0x000672E8
		// (set) Token: 0x06001BD4 RID: 7124 RVA: 0x00069124 File Offset: 0x00067324
		public int WindowWidth
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)(consoleScreenBufferInfo.Window.Right - consoleScreenBufferInfo.Window.Left + 1);
			}
			set
			{
				this.SetWindowSize(value, this.WindowHeight);
			}
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x00069133 File Offset: 0x00067333
		public void Beep(int frequency, int duration)
		{
			WindowsConsoleDriver._Beep(frequency, duration);
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x0006913C File Offset: 0x0006733C
		public void Clear()
		{
			Coord coord = default(Coord);
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			int num = (int)(consoleScreenBufferInfo.Size.X * consoleScreenBufferInfo.Size.Y);
			int num2;
			WindowsConsoleDriver.FillConsoleOutputCharacter(this.outputHandle, ' ', num, coord, out num2);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			WindowsConsoleDriver.FillConsoleOutputAttribute(this.outputHandle, consoleScreenBufferInfo.Attribute, num, coord, out num2);
			WindowsConsoleDriver.SetConsoleCursorPosition(this.outputHandle, coord);
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x000691C4 File Offset: 0x000673C4
		public unsafe void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
			if (sourceForeColor < ConsoleColor.Black)
			{
				throw new ArgumentException("Cannot be less than 0.", "sourceForeColor");
			}
			if (sourceBackColor < ConsoleColor.Black)
			{
				throw new ArgumentException("Cannot be less than 0.", "sourceBackColor");
			}
			if (sourceWidth == 0 || sourceHeight == 0)
			{
				return;
			}
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			CharInfo[] array = new CharInfo[sourceWidth * sourceHeight];
			Coord coord = new Coord(sourceWidth, sourceHeight);
			Coord coord2 = new Coord(0, 0);
			SmallRect smallRect = new SmallRect(sourceLeft, sourceTop, sourceLeft + sourceWidth - 1, sourceTop + sourceHeight - 1);
			fixed (CharInfo* ptr = &array[0])
			{
				void* ptr2 = (void*)ptr;
				if (!WindowsConsoleDriver.ReadConsoleOutput(this.outputHandle, ptr2, coord, coord2, ref smallRect))
				{
					throw new ArgumentException(string.Empty, "Cannot read from the specified coordinates.");
				}
			}
			short num = WindowsConsoleDriver.GetAttrForeground(0, sourceForeColor);
			num = WindowsConsoleDriver.GetAttrBackground((int)num, sourceBackColor);
			coord2 = new Coord(sourceLeft, sourceTop);
			int i = 0;
			while (i < sourceHeight)
			{
				int num2;
				WindowsConsoleDriver.FillConsoleOutputCharacter(this.outputHandle, sourceChar, sourceWidth, coord2, out num2);
				WindowsConsoleDriver.FillConsoleOutputAttribute(this.outputHandle, num, sourceWidth, coord2, out num2);
				i++;
				coord2.Y += 1;
			}
			coord2 = new Coord(0, 0);
			smallRect = new SmallRect(targetLeft, targetTop, targetLeft + sourceWidth - 1, targetTop + sourceHeight - 1);
			if (!WindowsConsoleDriver.WriteConsoleOutput(this.outputHandle, array, coord, coord2, ref smallRect))
			{
				throw new ArgumentException(string.Empty, "Cannot write to the specified coordinates.");
			}
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x00002194 File Offset: 0x00000394
		public void Init()
		{
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x00069324 File Offset: 0x00067524
		public string ReadLine()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag;
			do
			{
				ConsoleKeyInfo consoleKeyInfo = this.ReadKey(false);
				flag = consoleKeyInfo.KeyChar == '\n';
				if (!flag)
				{
					stringBuilder.Append(consoleKeyInfo.KeyChar);
				}
			}
			while (!flag);
			return stringBuilder.ToString();
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x00069368 File Offset: 0x00067568
		public ConsoleKeyInfo ReadKey(bool intercept)
		{
			InputRecord inputRecord = default(InputRecord);
			int num;
			while (WindowsConsoleDriver.ReadConsoleInput(this.inputHandle, out inputRecord, 1, out num))
			{
				if (inputRecord.KeyDown && inputRecord.EventType == 1 && !WindowsConsoleDriver.IsModifierKey(inputRecord.VirtualKeyCode))
				{
					bool flag = (inputRecord.ControlKeyState & 3) != 0;
					bool flag2 = (inputRecord.ControlKeyState & 12) != 0;
					bool flag3 = (inputRecord.ControlKeyState & 16) != 0;
					return new ConsoleKeyInfo(inputRecord.Character, (ConsoleKey)inputRecord.VirtualKeyCode, flag3, flag, flag2);
				}
			}
			throw new InvalidOperationException("Error in ReadConsoleInput " + Marshal.GetLastWin32Error());
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x00069403 File Offset: 0x00067603
		public void ResetColor()
		{
			WindowsConsoleDriver.SetConsoleTextAttribute(this.outputHandle, this.defaultAttribute);
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x00069418 File Offset: 0x00067618
		public void SetBufferSize(int width, int height)
		{
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			if (width - 1 > (int)consoleScreenBufferInfo.Window.Right)
			{
				throw new ArgumentOutOfRangeException("width");
			}
			if (height - 1 > (int)consoleScreenBufferInfo.Window.Bottom)
			{
				throw new ArgumentOutOfRangeException("height");
			}
			Coord coord = new Coord(width, height);
			if (!WindowsConsoleDriver.SetConsoleScreenBufferSize(this.outputHandle, coord))
			{
				throw new ArgumentOutOfRangeException("height/width", "Cannot be smaller than the window size.");
			}
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x00069498 File Offset: 0x00067698
		public void SetCursorPosition(int left, int top)
		{
			Coord coord = new Coord(left, top);
			WindowsConsoleDriver.SetConsoleCursorPosition(this.outputHandle, coord);
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x000694BC File Offset: 0x000676BC
		public void SetWindowPosition(int left, int top)
		{
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			SmallRect window = consoleScreenBufferInfo.Window;
			window.Left = (short)left;
			window.Top = (short)top;
			if (!WindowsConsoleDriver.SetConsoleWindowInfo(this.outputHandle, true, ref window))
			{
				throw new ArgumentOutOfRangeException("left/top", "Windows error " + Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x00069528 File Offset: 0x00067728
		public void SetWindowSize(int width, int height)
		{
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			SmallRect window = consoleScreenBufferInfo.Window;
			window.Right = (short)((int)window.Left + width - 1);
			window.Bottom = (short)((int)window.Top + height - 1);
			if (!WindowsConsoleDriver.SetConsoleWindowInfo(this.outputHandle, true, ref window))
			{
				throw new ArgumentOutOfRangeException("left/top", "Windows error " + Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x000695A5 File Offset: 0x000677A5
		private static bool IsModifierKey(short virtualKeyCode)
		{
			return virtualKeyCode - 16 <= 2 || virtualKeyCode == 20 || virtualKeyCode - 144 <= 1;
		}

		// Token: 0x06001BE1 RID: 7137
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern IntPtr GetStdHandle(Handles handle);

		// Token: 0x06001BE2 RID: 7138
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "Beep", SetLastError = true)]
		private static extern void _Beep(int frequency, int duration);

		// Token: 0x06001BE3 RID: 7139
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool GetConsoleScreenBufferInfo(IntPtr handle, out ConsoleScreenBufferInfo info);

		// Token: 0x06001BE4 RID: 7140
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool FillConsoleOutputCharacter(IntPtr handle, char c, int size, Coord coord, out int written);

		// Token: 0x06001BE5 RID: 7141
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool FillConsoleOutputAttribute(IntPtr handle, short c, int size, Coord coord, out int written);

		// Token: 0x06001BE6 RID: 7142
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleCursorPosition(IntPtr handle, Coord coord);

		// Token: 0x06001BE7 RID: 7143
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleTextAttribute(IntPtr handle, short attribute);

		// Token: 0x06001BE8 RID: 7144
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleScreenBufferSize(IntPtr handle, Coord newSize);

		// Token: 0x06001BE9 RID: 7145
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleWindowInfo(IntPtr handle, bool absolute, ref SmallRect rect);

		// Token: 0x06001BEA RID: 7146
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int GetConsoleTitle(StringBuilder sb, int size);

		// Token: 0x06001BEB RID: 7147
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleTitle(string title);

		// Token: 0x06001BEC RID: 7148
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool GetConsoleCursorInfo(IntPtr handle, out ConsoleCursorInfo info);

		// Token: 0x06001BED RID: 7149
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleCursorInfo(IntPtr handle, ref ConsoleCursorInfo info);

		// Token: 0x06001BEE RID: 7150
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern short GetKeyState(int virtKey);

		// Token: 0x06001BEF RID: 7151
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool GetConsoleMode(IntPtr handle, out int mode);

		// Token: 0x06001BF0 RID: 7152
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleMode(IntPtr handle, int mode);

		// Token: 0x06001BF1 RID: 7153
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool PeekConsoleInput(IntPtr handle, out InputRecord record, int length, out int eventsRead);

		// Token: 0x06001BF2 RID: 7154
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool ReadConsoleInput(IntPtr handle, out InputRecord record, int length, out int nread);

		// Token: 0x06001BF3 RID: 7155
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern Coord GetLargestConsoleWindowSize(IntPtr handle);

		// Token: 0x06001BF4 RID: 7156
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private unsafe static extern bool ReadConsoleOutput(IntPtr handle, void* buffer, Coord bsize, Coord bpos, ref SmallRect region);

		// Token: 0x06001BF5 RID: 7157
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool WriteConsoleOutput(IntPtr handle, CharInfo[] buffer, Coord bsize, Coord bpos, ref SmallRect region);

		// Token: 0x04000FA1 RID: 4001
		private IntPtr inputHandle;

		// Token: 0x04000FA2 RID: 4002
		private IntPtr outputHandle;

		// Token: 0x04000FA3 RID: 4003
		private short defaultAttribute;
	}
}
