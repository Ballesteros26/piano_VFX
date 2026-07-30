using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	// Token: 0x02000232 RID: 562
	internal class TermInfoDriver : IConsoleDriver
	{
		// Token: 0x06001AA8 RID: 6824 RVA: 0x00063F04 File Offset: 0x00062104
		private static string TryTermInfoDir(string dir, string term)
		{
			string text = string.Format("{0}/{1:x}/{2}", dir, (int)term[0], term);
			if (File.Exists(text))
			{
				return text;
			}
			text = Path.Combine(dir, term.Substring(0, 1), term);
			if (File.Exists(text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x00063F50 File Offset: 0x00062150
		private static string SearchTerminfo(string term)
		{
			if (term == null || term == string.Empty)
			{
				return null;
			}
			string environmentVariable = Environment.GetEnvironmentVariable("TERMINFO");
			if (environmentVariable != null && Directory.Exists(environmentVariable))
			{
				string text = TermInfoDriver.TryTermInfoDir(environmentVariable, term);
				if (text != null)
				{
					return text;
				}
			}
			foreach (string text2 in TermInfoDriver.locations)
			{
				if (Directory.Exists(text2))
				{
					string text = TermInfoDriver.TryTermInfoDir(text2, term);
					if (text != null)
					{
						return text;
					}
				}
			}
			return null;
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x00063FC3 File Offset: 0x000621C3
		private void WriteConsole(string str)
		{
			if (str == null)
			{
				return;
			}
			this.stdout.InternalWriteString(str);
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x00063FD5 File Offset: 0x000621D5
		public TermInfoDriver()
			: this(Environment.GetEnvironmentVariable("TERM"))
		{
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x00063FE8 File Offset: 0x000621E8
		public TermInfoDriver(string term)
		{
			this.term = term;
			string text = TermInfoDriver.SearchTerminfo(term);
			if (text != null)
			{
				this.reader = new TermInfoReader(term, text);
			}
			else if (term == "xterm")
			{
				this.reader = new TermInfoReader(term, KnownTerminals.xterm);
			}
			else if (term == "linux")
			{
				this.reader = new TermInfoReader(term, KnownTerminals.linux);
			}
			if (this.reader == null)
			{
				this.reader = new TermInfoReader(term, KnownTerminals.ansi);
			}
			if (!(Console.stdout is CStreamWriter))
			{
				this.stdout = new CStreamWriter(Console.OpenStandardOutput(0), Console.OutputEncoding, false);
				this.stdout.AutoFlush = true;
				return;
			}
			this.stdout = (CStreamWriter)Console.stdout;
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x000640F0 File Offset: 0x000622F0
		public bool Initialized
		{
			get
			{
				return this.inited;
			}
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x000640F8 File Offset: 0x000622F8
		public void Init()
		{
			if (this.inited)
			{
				return;
			}
			object obj = this.initLock;
			lock (obj)
			{
				if (!this.inited)
				{
					this.inited = true;
					if (!ConsoleDriver.IsConsole)
					{
						throw new IOException("Not a tty.");
					}
					ConsoleDriver.SetEcho(false);
					string text = null;
					this.keypadXmit = this.reader.Get(TermInfoStrings.KeypadXmit);
					this.keypadLocal = this.reader.Get(TermInfoStrings.KeypadLocal);
					if (this.keypadXmit != null)
					{
						this.WriteConsole(this.keypadXmit);
						if (this.keypadLocal != null)
						{
							text += this.keypadLocal;
						}
					}
					this.origPair = this.reader.Get(TermInfoStrings.OrigPair);
					this.origColors = this.reader.Get(TermInfoStrings.OrigColors);
					this.setfgcolor = this.reader.Get(TermInfoStrings.SetAForeground);
					this.setbgcolor = this.reader.Get(TermInfoStrings.SetABackground);
					this.maxColors = this.reader.Get(TermInfoNumbers.MaxColors);
					this.maxColors = Math.Max(Math.Min(this.maxColors, 16), 1);
					string text2 = ((this.origColors == null) ? this.origPair : this.origColors);
					if (text2 != null)
					{
						text += text2;
					}
					if (!ConsoleDriver.TtySetup(this.keypadXmit, text, out this.control_characters, out TermInfoDriver.native_terminal_size))
					{
						this.control_characters = new byte[17];
						TermInfoDriver.native_terminal_size = null;
					}
					this.stdin = new StreamReader(Console.OpenStandardInput(0), Console.InputEncoding);
					this.clear = this.reader.Get(TermInfoStrings.ClearScreen);
					this.bell = this.reader.Get(TermInfoStrings.Bell);
					if (this.clear == null)
					{
						this.clear = this.reader.Get(TermInfoStrings.CursorHome);
						this.clear += this.reader.Get(TermInfoStrings.ClrEos);
					}
					this.csrVisible = this.reader.Get(TermInfoStrings.CursorNormal);
					if (this.csrVisible == null)
					{
						this.csrVisible = this.reader.Get(TermInfoStrings.CursorVisible);
					}
					this.csrInvisible = this.reader.Get(TermInfoStrings.CursorInvisible);
					if (this.term == "cygwin" || this.term == "linux" || (this.term != null && this.term.StartsWith("xterm")) || this.term == "rxvt" || this.term == "dtterm")
					{
						this.titleFormat = "\u001b]0;{0}\a";
					}
					else if (this.term == "iris-ansi")
					{
						this.titleFormat = "\u001bP1.y{0}\u001b\\";
					}
					else if (this.term == "sun-cmd")
					{
						this.titleFormat = "\u001b]l{0}\u001b\\";
					}
					this.cursorAddress = this.reader.Get(TermInfoStrings.CursorAddress);
					this.GetCursorPosition();
					if (this.noGetPosition)
					{
						this.WriteConsole(this.clear);
						this.cursorLeft = 0;
						this.cursorTop = 0;
					}
				}
			}
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x00064434 File Offset: 0x00062634
		private void IncrementX()
		{
			this.cursorLeft++;
			if (this.cursorLeft >= this.WindowWidth)
			{
				this.cursorTop++;
				this.cursorLeft = 0;
				if (this.cursorTop >= this.WindowHeight)
				{
					if (this.rl_starty != -1)
					{
						this.rl_starty--;
					}
					this.cursorTop--;
				}
			}
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x000644A8 File Offset: 0x000626A8
		public void WriteSpecialKey(ConsoleKeyInfo key)
		{
			switch (key.Key)
			{
			case ConsoleKey.Backspace:
				if (this.cursorLeft > 0 && (this.cursorLeft > this.rl_startx || this.cursorTop != this.rl_starty))
				{
					this.cursorLeft--;
					this.SetCursorPosition(this.cursorLeft, this.cursorTop);
					this.WriteConsole(" ");
					this.SetCursorPosition(this.cursorLeft, this.cursorTop);
					return;
				}
				break;
			case ConsoleKey.Tab:
			{
				int num = 8 - this.cursorLeft % 8;
				for (int i = 0; i < num; i++)
				{
					this.IncrementX();
				}
				this.WriteConsole("\t");
				return;
			}
			case (ConsoleKey)10:
			case (ConsoleKey)11:
			case ConsoleKey.Enter:
				break;
			case ConsoleKey.Clear:
				this.WriteConsole(this.clear);
				this.cursorLeft = 0;
				this.cursorTop = 0;
				break;
			default:
				return;
			}
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x0006458A File Offset: 0x0006278A
		public void WriteSpecialKey(char c)
		{
			this.WriteSpecialKey(this.CreateKeyInfoFromInt((int)c, false));
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x0006459C File Offset: 0x0006279C
		public bool IsSpecialKey(ConsoleKeyInfo key)
		{
			if (!this.inited)
			{
				return false;
			}
			switch (key.Key)
			{
			case ConsoleKey.Backspace:
				return true;
			case ConsoleKey.Tab:
				return true;
			case ConsoleKey.Clear:
				return true;
			case ConsoleKey.Enter:
				this.cursorLeft = 0;
				this.cursorTop++;
				if (this.cursorTop >= this.WindowHeight)
				{
					this.cursorTop--;
				}
				return false;
			}
			this.IncrementX();
			return false;
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x0006461D File Offset: 0x0006281D
		public bool IsSpecialKey(char c)
		{
			return this.IsSpecialKey(this.CreateKeyInfoFromInt((int)c, false));
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x00064630 File Offset: 0x00062830
		private void ChangeColor(string format, ConsoleColor color)
		{
			if ((color & (ConsoleColor)(-16)) != ConsoleColor.Black)
			{
				throw new ArgumentException("Invalid Console Color");
			}
			int num = TermInfoDriver._consoleColorToAnsiCode[(int)color] % this.maxColors;
			this.WriteConsole(ParameterizedStrings.Evaluate(format, new ParameterizedStrings.FormatParam[] { num }));
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x0006467E File Offset: 0x0006287E
		// (set) Token: 0x06001AB6 RID: 6838 RVA: 0x00064694 File Offset: 0x00062894
		public ConsoleColor BackgroundColor
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.bgcolor;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.ChangeColor(this.setbgcolor, value);
				this.bgcolor = value;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x000646B8 File Offset: 0x000628B8
		// (set) Token: 0x06001AB8 RID: 6840 RVA: 0x000646CE File Offset: 0x000628CE
		public ConsoleColor ForegroundColor
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.fgcolor;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.ChangeColor(this.setfgcolor, value);
				this.fgcolor = value;
			}
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x000646F4 File Offset: 0x000628F4
		private void GetCursorPosition()
		{
			int num = 0;
			int num2 = 0;
			int num3 = ConsoleDriver.InternalKeyAvailable(0);
			int num4;
			while (num3-- > 0)
			{
				num4 = this.stdin.Read();
				this.AddToBuffer(num4);
			}
			this.WriteConsole("\u001b[6n");
			if (ConsoleDriver.InternalKeyAvailable(1000) <= 0)
			{
				this.noGetPosition = true;
				return;
			}
			for (num4 = this.stdin.Read(); num4 != 27; num4 = this.stdin.Read())
			{
				this.AddToBuffer(num4);
				if (ConsoleDriver.InternalKeyAvailable(100) <= 0)
				{
					return;
				}
			}
			num4 = this.stdin.Read();
			if (num4 != 91)
			{
				this.AddToBuffer(27);
				this.AddToBuffer(num4);
				return;
			}
			num4 = this.stdin.Read();
			if (num4 != 59)
			{
				num = num4 - 48;
				num4 = this.stdin.Read();
				while (num4 >= 48 && num4 <= 57)
				{
					num = num * 10 + num4 - 48;
					num4 = this.stdin.Read();
				}
				num--;
			}
			num4 = this.stdin.Read();
			if (num4 != 82)
			{
				num2 = num4 - 48;
				num4 = this.stdin.Read();
				while (num4 >= 48 && num4 <= 57)
				{
					num2 = num2 * 10 + num4 - 48;
					num4 = this.stdin.Read();
				}
				num2--;
			}
			this.cursorLeft = num2;
			this.cursorTop = num;
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x00064839 File Offset: 0x00062A39
		// (set) Token: 0x06001ABB RID: 6843 RVA: 0x00064855 File Offset: 0x00062A55
		public int BufferHeight
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.CheckWindowDimensions();
				return this.bufferHeight;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001ABC RID: 6844 RVA: 0x0006486A File Offset: 0x00062A6A
		// (set) Token: 0x06001ABD RID: 6845 RVA: 0x00064855 File Offset: 0x00062A55
		public int BufferWidth
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.CheckWindowDimensions();
				return this.bufferWidth;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001ABE RID: 6846 RVA: 0x00064886 File Offset: 0x00062A86
		public bool CapsLock
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return false;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x00064897 File Offset: 0x00062A97
		// (set) Token: 0x06001AC0 RID: 6848 RVA: 0x000648AD File Offset: 0x00062AAD
		public int CursorLeft
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.cursorLeft;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.SetCursorPosition(value, this.CursorTop);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x000648CA File Offset: 0x00062ACA
		// (set) Token: 0x06001AC2 RID: 6850 RVA: 0x000648E0 File Offset: 0x00062AE0
		public int CursorTop
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.cursorTop;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.SetCursorPosition(this.CursorLeft, value);
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x000648FD File Offset: 0x00062AFD
		// (set) Token: 0x06001AC4 RID: 6852 RVA: 0x00064913 File Offset: 0x00062B13
		public bool CursorVisible
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.cursorVisible;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.cursorVisible = value;
				this.WriteConsole(value ? this.csrVisible : this.csrInvisible);
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x00064941 File Offset: 0x00062B41
		// (set) Token: 0x06001AC6 RID: 6854 RVA: 0x00064952 File Offset: 0x00062B52
		[MonoTODO]
		public int CursorSize
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return 1;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001AC7 RID: 6855 RVA: 0x00064962 File Offset: 0x00062B62
		public bool KeyAvailable
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.writepos > this.readpos || ConsoleDriver.InternalKeyAvailable(0) > 0;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x0006498B File Offset: 0x00062B8B
		public int LargestWindowHeight
		{
			get
			{
				return this.WindowHeight;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x00064993 File Offset: 0x00062B93
		public int LargestWindowWidth
		{
			get
			{
				return this.WindowWidth;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x00064886 File Offset: 0x00062A86
		public bool NumberLock
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return false;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x0006499B File Offset: 0x00062B9B
		// (set) Token: 0x06001ACC RID: 6860 RVA: 0x000649B1 File Offset: 0x00062BB1
		public string Title
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.title;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.title = value;
				this.WriteConsole(string.Format(this.titleFormat, value));
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x000649DA File Offset: 0x00062BDA
		// (set) Token: 0x06001ACE RID: 6862 RVA: 0x000649F0 File Offset: 0x00062BF0
		public bool TreatControlCAsInput
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.controlCAsInput;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				if (this.controlCAsInput == value)
				{
					return;
				}
				ConsoleDriver.SetBreak(value);
				this.controlCAsInput = value;
			}
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x00064A18 File Offset: 0x00062C18
		private unsafe void CheckWindowDimensions()
		{
			if (TermInfoDriver.native_terminal_size == null || TermInfoDriver.terminal_size == *TermInfoDriver.native_terminal_size)
			{
				return;
			}
			if (*TermInfoDriver.native_terminal_size == -1)
			{
				int num = this.reader.Get(TermInfoNumbers.Columns);
				if (num != 0)
				{
					this.windowWidth = num;
				}
				num = this.reader.Get(TermInfoNumbers.Lines);
				if (num != 0)
				{
					this.windowHeight = num;
				}
			}
			else
			{
				TermInfoDriver.terminal_size = *TermInfoDriver.native_terminal_size;
				this.windowWidth = TermInfoDriver.terminal_size >> 16;
				this.windowHeight = TermInfoDriver.terminal_size & 65535;
			}
			this.bufferHeight = this.windowHeight;
			this.bufferWidth = this.windowWidth;
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x00064AB7 File Offset: 0x00062CB7
		// (set) Token: 0x06001AD1 RID: 6865 RVA: 0x00064855 File Offset: 0x00062A55
		public int WindowHeight
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.CheckWindowDimensions();
				return this.windowHeight;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x00064886 File Offset: 0x00062A86
		// (set) Token: 0x06001AD3 RID: 6867 RVA: 0x00064855 File Offset: 0x00062A55
		public int WindowLeft
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return 0;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x00064886 File Offset: 0x00062A86
		// (set) Token: 0x06001AD5 RID: 6869 RVA: 0x00064855 File Offset: 0x00062A55
		public int WindowTop
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return 0;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x00064AD3 File Offset: 0x00062CD3
		// (set) Token: 0x06001AD7 RID: 6871 RVA: 0x00064855 File Offset: 0x00062A55
		public int WindowWidth
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.CheckWindowDimensions();
				return this.windowWidth;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00064AEF File Offset: 0x00062CEF
		public void Clear()
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.WriteConsole(this.clear);
			this.cursorLeft = 0;
			this.cursorTop = 0;
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x00064B19 File Offset: 0x00062D19
		public void Beep(int frequency, int duration)
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.WriteConsole(this.bell);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x00064B35 File Offset: 0x00062D35
		public void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
			if (!this.inited)
			{
				this.Init();
			}
			throw new NotImplementedException();
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x00064B4C File Offset: 0x00062D4C
		private void AddToBuffer(int b)
		{
			if (this.buffer == null)
			{
				this.buffer = new char[1024];
			}
			else if (this.writepos >= this.buffer.Length)
			{
				char[] array = new char[this.buffer.Length * 2];
				Buffer.BlockCopy(this.buffer, 0, array, 0, this.buffer.Length);
				this.buffer = array;
			}
			char[] array2 = this.buffer;
			int num = this.writepos;
			this.writepos = num + 1;
			array2[num] = (ushort)b;
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x00064BCC File Offset: 0x00062DCC
		private void AdjustBuffer()
		{
			if (this.readpos >= this.writepos)
			{
				this.readpos = (this.writepos = 0);
			}
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00064BF8 File Offset: 0x00062DF8
		private ConsoleKeyInfo CreateKeyInfoFromInt(int n, bool alt)
		{
			char c = (char)n;
			ConsoleKey consoleKey = (ConsoleKey)n;
			bool flag = false;
			bool flag2 = false;
			if (n <= 19)
			{
				switch (n)
				{
				case 8:
				case 9:
				case 12:
				case 13:
					goto IL_00C7;
				case 10:
					consoleKey = ConsoleKey.Enter;
					goto IL_00C7;
				case 11:
					break;
				default:
					if (n == 19)
					{
						goto IL_00C7;
					}
					break;
				}
			}
			else
			{
				if (n == 27)
				{
					consoleKey = ConsoleKey.Escape;
					goto IL_00C7;
				}
				if (n == 32)
				{
					consoleKey = ConsoleKey.Spacebar;
					goto IL_00C7;
				}
				switch (n)
				{
				case 42:
					consoleKey = ConsoleKey.Multiply;
					goto IL_00C7;
				case 43:
					consoleKey = ConsoleKey.Add;
					goto IL_00C7;
				case 45:
					consoleKey = ConsoleKey.Subtract;
					goto IL_00C7;
				case 47:
					consoleKey = ConsoleKey.Divide;
					goto IL_00C7;
				}
			}
			if (n >= 1 && n <= 26)
			{
				flag2 = true;
				consoleKey = ConsoleKey.A + n - 1;
			}
			else if (n >= 97 && n <= 122)
			{
				consoleKey = (ConsoleKey)(-32) + n;
			}
			else if (n >= 65 && n <= 90)
			{
				flag = true;
			}
			else if (n < 48 || n > 57)
			{
				consoleKey = (ConsoleKey)0;
			}
			IL_00C7:
			return new ConsoleKeyInfo(c, consoleKey, flag, alt, flag2);
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00064CD8 File Offset: 0x00062ED8
		private object GetKeyFromBuffer(bool cooked)
		{
			if (this.readpos >= this.writepos)
			{
				return null;
			}
			int num = (int)this.buffer[this.readpos];
			if (!cooked || !this.rootmap.StartsWith(num))
			{
				this.readpos++;
				this.AdjustBuffer();
				return this.CreateKeyInfoFromInt(num, false);
			}
			int num2;
			TermInfoStrings termInfoStrings = this.rootmap.Match(this.buffer, this.readpos, this.writepos - this.readpos, out num2);
			if (termInfoStrings == (TermInfoStrings)(-1))
			{
				if (this.buffer[this.readpos] != '\u001b' || this.writepos - this.readpos < 2)
				{
					return null;
				}
				this.readpos += 2;
				this.AdjustBuffer();
				if (this.buffer[this.readpos + 1] == '\u007f')
				{
					return new ConsoleKeyInfo('\b', ConsoleKey.Backspace, false, true, false);
				}
				return this.CreateKeyInfoFromInt((int)this.buffer[this.readpos + 1], true);
			}
			else
			{
				if (this.keymap[termInfoStrings] != null)
				{
					ConsoleKeyInfo consoleKeyInfo = (ConsoleKeyInfo)this.keymap[termInfoStrings];
					this.readpos += num2;
					this.AdjustBuffer();
					return consoleKeyInfo;
				}
				this.readpos++;
				this.AdjustBuffer();
				return this.CreateKeyInfoFromInt(num, false);
			}
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x00064E40 File Offset: 0x00063040
		private ConsoleKeyInfo ReadKeyInternal(out bool fresh)
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.InitKeys();
			object obj;
			if ((obj = this.GetKeyFromBuffer(true)) == null)
			{
				do
				{
					if (ConsoleDriver.InternalKeyAvailable(150) > 0)
					{
						do
						{
							this.AddToBuffer(this.stdin.Read());
						}
						while (ConsoleDriver.InternalKeyAvailable(0) > 0);
					}
					else if (this.stdin.DataAvailable())
					{
						do
						{
							this.AddToBuffer(this.stdin.Read());
						}
						while (this.stdin.DataAvailable());
					}
					else
					{
						if ((obj = this.GetKeyFromBuffer(false)) != null)
						{
							break;
						}
						this.AddToBuffer(this.stdin.Read());
					}
					obj = this.GetKeyFromBuffer(true);
				}
				while (obj == null);
				fresh = true;
			}
			else
			{
				fresh = false;
			}
			return (ConsoleKeyInfo)obj;
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x00064EFA File Offset: 0x000630FA
		private bool InputPending()
		{
			return this.readpos < this.writepos || this.stdin.DataAvailable();
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x00064F18 File Offset: 0x00063118
		private void QueueEcho(char c)
		{
			if (this.echobuf == null)
			{
				this.echobuf = new char[1024];
			}
			char[] array = this.echobuf;
			int num = this.echon;
			this.echon = num + 1;
			array[num] = c;
			if (this.echon == this.echobuf.Length || !this.InputPending())
			{
				this.stdout.InternalWriteChars(this.echobuf, this.echon);
				this.echon = 0;
			}
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x00064F8C File Offset: 0x0006318C
		private void Echo(ConsoleKeyInfo key)
		{
			if (!this.IsSpecialKey(key))
			{
				this.QueueEcho(key.KeyChar);
				return;
			}
			this.EchoFlush();
			this.WriteSpecialKey(key);
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x00064FB2 File Offset: 0x000631B2
		private void EchoFlush()
		{
			if (this.echon == 0)
			{
				return;
			}
			this.stdout.InternalWriteChars(this.echobuf, this.echon);
			this.echon = 0;
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00064FDC File Offset: 0x000631DC
		public int Read([In] [Out] char[] dest, int index, int count)
		{
			bool flag = false;
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			object keyFromBuffer;
			while ((keyFromBuffer = this.GetKeyFromBuffer(true)) != null)
			{
				ConsoleKeyInfo consoleKeyInfo = (ConsoleKeyInfo)keyFromBuffer;
				char c = consoleKeyInfo.KeyChar;
				if (consoleKeyInfo.Key != ConsoleKey.Backspace)
				{
					if (consoleKeyInfo.Key == ConsoleKey.Enter)
					{
						num = stringBuilder.Length;
					}
					stringBuilder.Append(c);
				}
				else if (stringBuilder.Length > num)
				{
					StringBuilder stringBuilder2 = stringBuilder;
					int num2 = stringBuilder2.Length;
					stringBuilder2.Length = num2 - 1;
				}
			}
			this.rl_startx = this.cursorLeft;
			this.rl_starty = this.cursorTop;
			for (;;)
			{
				bool flag2;
				ConsoleKeyInfo consoleKeyInfo = this.ReadKeyInternal(out flag2);
				flag = flag || flag2;
				char c = consoleKeyInfo.KeyChar;
				if (consoleKeyInfo.Key != ConsoleKey.Backspace)
				{
					if (consoleKeyInfo.Key == ConsoleKey.Enter)
					{
						num = stringBuilder.Length;
					}
					stringBuilder.Append(c);
					goto IL_00E0;
				}
				if (stringBuilder.Length > num)
				{
					StringBuilder stringBuilder3 = stringBuilder;
					int num2 = stringBuilder3.Length;
					stringBuilder3.Length = num2 - 1;
					goto IL_00E0;
				}
				IL_00EA:
				if (consoleKeyInfo.Key == ConsoleKey.Enter)
				{
					break;
				}
				continue;
				IL_00E0:
				if (flag)
				{
					this.Echo(consoleKeyInfo);
					goto IL_00EA;
				}
				goto IL_00EA;
			}
			this.EchoFlush();
			this.rl_startx = -1;
			this.rl_starty = -1;
			int num3 = 0;
			while (count > 0 && num3 < stringBuilder.Length)
			{
				dest[index + num3] = stringBuilder[num3];
				num3++;
				count--;
			}
			for (int i = num3; i < stringBuilder.Length; i++)
			{
				this.AddToBuffer((int)stringBuilder[i]);
			}
			return num3;
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x00065144 File Offset: 0x00063344
		public ConsoleKeyInfo ReadKey(bool intercept)
		{
			bool flag;
			ConsoleKeyInfo consoleKeyInfo = this.ReadKeyInternal(out flag);
			if (!intercept && flag)
			{
				this.Echo(consoleKeyInfo);
				this.EchoFlush();
			}
			return consoleKeyInfo;
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x00065170 File Offset: 0x00063370
		public string ReadLine()
		{
			return this.ReadUntilConditionInternal(true);
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00065179 File Offset: 0x00063379
		public string ReadToEnd()
		{
			return this.ReadUntilConditionInternal(false);
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x00065184 File Offset: 0x00063384
		private string ReadUntilConditionInternal(bool haltOnNewLine)
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.GetCursorPosition();
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			this.rl_startx = this.cursorLeft;
			this.rl_starty = this.cursorTop;
			char c = (char)this.control_characters[4];
			for (;;)
			{
				bool flag2;
				ConsoleKeyInfo consoleKeyInfo = this.ReadKeyInternal(out flag2);
				flag = flag || flag2;
				char keyChar = consoleKeyInfo.KeyChar;
				if (keyChar == c && keyChar != '\0' && stringBuilder.Length == 0)
				{
					break;
				}
				bool flag3 = haltOnNewLine && consoleKeyInfo.Key == ConsoleKey.Enter;
				if (flag3)
				{
					goto IL_00AC;
				}
				if (consoleKeyInfo.Key != ConsoleKey.Backspace)
				{
					stringBuilder.Append(keyChar);
					goto IL_00AC;
				}
				if (stringBuilder.Length > 0)
				{
					StringBuilder stringBuilder2 = stringBuilder;
					int length = stringBuilder2.Length;
					stringBuilder2.Length = length - 1;
					goto IL_00AC;
				}
				IL_00B6:
				if (flag3)
				{
					goto Block_10;
				}
				continue;
				IL_00AC:
				if (flag)
				{
					this.Echo(consoleKeyInfo);
					goto IL_00B6;
				}
				goto IL_00B6;
			}
			return null;
			Block_10:
			this.EchoFlush();
			this.rl_startx = -1;
			this.rl_starty = -1;
			return stringBuilder.ToString();
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00065264 File Offset: 0x00063464
		public void ResetColor()
		{
			if (!this.inited)
			{
				this.Init();
			}
			string text = ((this.origPair != null) ? this.origPair : this.origColors);
			this.WriteConsole(text);
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x0006529D File Offset: 0x0006349D
		public void SetBufferSize(int width, int height)
		{
			if (!this.inited)
			{
				this.Init();
			}
			throw new NotImplementedException(string.Empty);
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x000652B8 File Offset: 0x000634B8
		public void SetCursorPosition(int left, int top)
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.CheckWindowDimensions();
			if (left < 0 || left >= this.bufferWidth)
			{
				throw new ArgumentOutOfRangeException("left", "Value must be positive and below the buffer width.");
			}
			if (top < 0 || top >= this.bufferHeight)
			{
				throw new ArgumentOutOfRangeException("top", "Value must be positive and below the buffer height.");
			}
			if (this.cursorAddress == null)
			{
				throw new NotSupportedException("This terminal does not suport setting the cursor position.");
			}
			this.WriteConsole(ParameterizedStrings.Evaluate(this.cursorAddress, new ParameterizedStrings.FormatParam[] { top, left }));
			this.cursorLeft = left;
			this.cursorTop = top;
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x00064952 File Offset: 0x00062B52
		public void SetWindowPosition(int left, int top)
		{
			if (!this.inited)
			{
				this.Init();
			}
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00064952 File Offset: 0x00062B52
		public void SetWindowSize(int width, int height)
		{
			if (!this.inited)
			{
				this.Init();
			}
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x00065368 File Offset: 0x00063568
		private void CreateKeyMap()
		{
			this.keymap = new Hashtable();
			this.keymap[TermInfoStrings.KeyBackspace] = new ConsoleKeyInfo('\0', ConsoleKey.Backspace, false, false, false);
			this.keymap[TermInfoStrings.KeyClear] = new ConsoleKeyInfo('\0', ConsoleKey.Clear, false, false, false);
			this.keymap[TermInfoStrings.KeyDown] = new ConsoleKeyInfo('\0', ConsoleKey.DownArrow, false, false, false);
			this.keymap[TermInfoStrings.KeyF1] = new ConsoleKeyInfo('\0', ConsoleKey.F1, false, false, false);
			this.keymap[TermInfoStrings.KeyF10] = new ConsoleKeyInfo('\0', ConsoleKey.F10, false, false, false);
			this.keymap[TermInfoStrings.KeyF2] = new ConsoleKeyInfo('\0', ConsoleKey.F2, false, false, false);
			this.keymap[TermInfoStrings.KeyF3] = new ConsoleKeyInfo('\0', ConsoleKey.F3, false, false, false);
			this.keymap[TermInfoStrings.KeyF4] = new ConsoleKeyInfo('\0', ConsoleKey.F4, false, false, false);
			this.keymap[TermInfoStrings.KeyF5] = new ConsoleKeyInfo('\0', ConsoleKey.F5, false, false, false);
			this.keymap[TermInfoStrings.KeyF6] = new ConsoleKeyInfo('\0', ConsoleKey.F6, false, false, false);
			this.keymap[TermInfoStrings.KeyF7] = new ConsoleKeyInfo('\0', ConsoleKey.F7, false, false, false);
			this.keymap[TermInfoStrings.KeyF8] = new ConsoleKeyInfo('\0', ConsoleKey.F8, false, false, false);
			this.keymap[TermInfoStrings.KeyF9] = new ConsoleKeyInfo('\0', ConsoleKey.F9, false, false, false);
			this.keymap[TermInfoStrings.KeyHome] = new ConsoleKeyInfo('\0', ConsoleKey.Home, false, false, false);
			this.keymap[TermInfoStrings.KeyLeft] = new ConsoleKeyInfo('\0', ConsoleKey.LeftArrow, false, false, false);
			this.keymap[TermInfoStrings.KeyLl] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad1, false, false, false);
			this.keymap[TermInfoStrings.KeyNpage] = new ConsoleKeyInfo('\0', ConsoleKey.PageDown, false, false, false);
			this.keymap[TermInfoStrings.KeyPpage] = new ConsoleKeyInfo('\0', ConsoleKey.PageUp, false, false, false);
			this.keymap[TermInfoStrings.KeyRight] = new ConsoleKeyInfo('\0', ConsoleKey.RightArrow, false, false, false);
			this.keymap[TermInfoStrings.KeySf] = new ConsoleKeyInfo('\0', ConsoleKey.PageDown, false, false, false);
			this.keymap[TermInfoStrings.KeySr] = new ConsoleKeyInfo('\0', ConsoleKey.PageUp, false, false, false);
			this.keymap[TermInfoStrings.KeyUp] = new ConsoleKeyInfo('\0', ConsoleKey.UpArrow, false, false, false);
			this.keymap[TermInfoStrings.KeyA1] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad7, false, false, false);
			this.keymap[TermInfoStrings.KeyA3] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad9, false, false, false);
			this.keymap[TermInfoStrings.KeyB2] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad5, false, false, false);
			this.keymap[TermInfoStrings.KeyC1] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad1, false, false, false);
			this.keymap[TermInfoStrings.KeyC3] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad3, false, false, false);
			this.keymap[TermInfoStrings.KeyBtab] = new ConsoleKeyInfo('\0', ConsoleKey.Tab, true, false, false);
			this.keymap[TermInfoStrings.KeyBeg] = new ConsoleKeyInfo('\0', ConsoleKey.Home, false, false, false);
			this.keymap[TermInfoStrings.KeyCopy] = new ConsoleKeyInfo('C', ConsoleKey.C, false, true, false);
			this.keymap[TermInfoStrings.KeyEnd] = new ConsoleKeyInfo('\0', ConsoleKey.End, false, false, false);
			this.keymap[TermInfoStrings.KeyEnter] = new ConsoleKeyInfo('\n', ConsoleKey.Enter, false, false, false);
			this.keymap[TermInfoStrings.KeyHelp] = new ConsoleKeyInfo('\0', ConsoleKey.Help, false, false, false);
			this.keymap[TermInfoStrings.KeyPrint] = new ConsoleKeyInfo('\0', ConsoleKey.Print, false, false, false);
			this.keymap[TermInfoStrings.KeyUndo] = new ConsoleKeyInfo('Z', ConsoleKey.Z, false, true, false);
			this.keymap[TermInfoStrings.KeySbeg] = new ConsoleKeyInfo('\0', ConsoleKey.Home, true, false, false);
			this.keymap[TermInfoStrings.KeyScopy] = new ConsoleKeyInfo('C', ConsoleKey.C, true, true, false);
			this.keymap[TermInfoStrings.KeySdc] = new ConsoleKeyInfo('\t', ConsoleKey.Delete, true, false, false);
			this.keymap[TermInfoStrings.KeyShelp] = new ConsoleKeyInfo('\0', ConsoleKey.Help, true, false, false);
			this.keymap[TermInfoStrings.KeyShome] = new ConsoleKeyInfo('\0', ConsoleKey.Home, true, false, false);
			this.keymap[TermInfoStrings.KeySleft] = new ConsoleKeyInfo('\0', ConsoleKey.LeftArrow, true, false, false);
			this.keymap[TermInfoStrings.KeySprint] = new ConsoleKeyInfo('\0', ConsoleKey.Print, true, false, false);
			this.keymap[TermInfoStrings.KeySright] = new ConsoleKeyInfo('\0', ConsoleKey.RightArrow, true, false, false);
			this.keymap[TermInfoStrings.KeySundo] = new ConsoleKeyInfo('Z', ConsoleKey.Z, true, false, false);
			this.keymap[TermInfoStrings.KeyF11] = new ConsoleKeyInfo('\0', ConsoleKey.F11, false, false, false);
			this.keymap[TermInfoStrings.KeyF12] = new ConsoleKeyInfo('\0', ConsoleKey.F12, false, false, false);
			this.keymap[TermInfoStrings.KeyF13] = new ConsoleKeyInfo('\0', ConsoleKey.F13, false, false, false);
			this.keymap[TermInfoStrings.KeyF14] = new ConsoleKeyInfo('\0', ConsoleKey.F14, false, false, false);
			this.keymap[TermInfoStrings.KeyF15] = new ConsoleKeyInfo('\0', ConsoleKey.F15, false, false, false);
			this.keymap[TermInfoStrings.KeyF16] = new ConsoleKeyInfo('\0', ConsoleKey.F16, false, false, false);
			this.keymap[TermInfoStrings.KeyF17] = new ConsoleKeyInfo('\0', ConsoleKey.F17, false, false, false);
			this.keymap[TermInfoStrings.KeyF18] = new ConsoleKeyInfo('\0', ConsoleKey.F18, false, false, false);
			this.keymap[TermInfoStrings.KeyF19] = new ConsoleKeyInfo('\0', ConsoleKey.F19, false, false, false);
			this.keymap[TermInfoStrings.KeyF20] = new ConsoleKeyInfo('\0', ConsoleKey.F20, false, false, false);
			this.keymap[TermInfoStrings.KeyF21] = new ConsoleKeyInfo('\0', ConsoleKey.F21, false, false, false);
			this.keymap[TermInfoStrings.KeyF22] = new ConsoleKeyInfo('\0', ConsoleKey.F22, false, false, false);
			this.keymap[TermInfoStrings.KeyF23] = new ConsoleKeyInfo('\0', ConsoleKey.F23, false, false, false);
			this.keymap[TermInfoStrings.KeyF24] = new ConsoleKeyInfo('\0', ConsoleKey.F24, false, false, false);
			this.keymap[TermInfoStrings.KeyDc] = new ConsoleKeyInfo('\0', ConsoleKey.Delete, false, false, false);
			this.keymap[TermInfoStrings.KeyIc] = new ConsoleKeyInfo('\0', ConsoleKey.Insert, false, false, false);
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x00065C04 File Offset: 0x00063E04
		private void InitKeys()
		{
			if (this.initKeys)
			{
				return;
			}
			this.CreateKeyMap();
			this.rootmap = new ByteMatcher();
			foreach (TermInfoStrings termInfoStrings in new TermInfoStrings[]
			{
				TermInfoStrings.KeyBackspace,
				TermInfoStrings.KeyClear,
				TermInfoStrings.KeyDown,
				TermInfoStrings.KeyF1,
				TermInfoStrings.KeyF10,
				TermInfoStrings.KeyF2,
				TermInfoStrings.KeyF3,
				TermInfoStrings.KeyF4,
				TermInfoStrings.KeyF5,
				TermInfoStrings.KeyF6,
				TermInfoStrings.KeyF7,
				TermInfoStrings.KeyF8,
				TermInfoStrings.KeyF9,
				TermInfoStrings.KeyHome,
				TermInfoStrings.KeyLeft,
				TermInfoStrings.KeyLl,
				TermInfoStrings.KeyNpage,
				TermInfoStrings.KeyPpage,
				TermInfoStrings.KeyRight,
				TermInfoStrings.KeySf,
				TermInfoStrings.KeySr,
				TermInfoStrings.KeyUp,
				TermInfoStrings.KeyA1,
				TermInfoStrings.KeyA3,
				TermInfoStrings.KeyB2,
				TermInfoStrings.KeyC1,
				TermInfoStrings.KeyC3,
				TermInfoStrings.KeyBtab,
				TermInfoStrings.KeyBeg,
				TermInfoStrings.KeyCopy,
				TermInfoStrings.KeyEnd,
				TermInfoStrings.KeyEnter,
				TermInfoStrings.KeyHelp,
				TermInfoStrings.KeyPrint,
				TermInfoStrings.KeyUndo,
				TermInfoStrings.KeySbeg,
				TermInfoStrings.KeyScopy,
				TermInfoStrings.KeySdc,
				TermInfoStrings.KeyShelp,
				TermInfoStrings.KeyShome,
				TermInfoStrings.KeySleft,
				TermInfoStrings.KeySprint,
				TermInfoStrings.KeySright,
				TermInfoStrings.KeySundo,
				TermInfoStrings.KeyF11,
				TermInfoStrings.KeyF12,
				TermInfoStrings.KeyF13,
				TermInfoStrings.KeyF14,
				TermInfoStrings.KeyF15,
				TermInfoStrings.KeyF16,
				TermInfoStrings.KeyF17,
				TermInfoStrings.KeyF18,
				TermInfoStrings.KeyF19,
				TermInfoStrings.KeyF20,
				TermInfoStrings.KeyF21,
				TermInfoStrings.KeyF22,
				TermInfoStrings.KeyF23,
				TermInfoStrings.KeyF24,
				TermInfoStrings.KeyDc,
				TermInfoStrings.KeyIc
			})
			{
				this.AddStringMapping(termInfoStrings);
			}
			this.rootmap.AddMapping(TermInfoStrings.KeyBackspace, new byte[] { this.control_characters[2] });
			this.rootmap.Sort();
			this.initKeys = true;
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x00065C88 File Offset: 0x00063E88
		private void AddStringMapping(TermInfoStrings s)
		{
			byte[] stringBytes = this.reader.GetStringBytes(s);
			if (stringBytes == null)
			{
				return;
			}
			this.rootmap.AddMapping(s, stringBytes);
		}

		// Token: 0x04000D4E RID: 3406
		private unsafe static int* native_terminal_size;

		// Token: 0x04000D4F RID: 3407
		private static int terminal_size;

		// Token: 0x04000D50 RID: 3408
		private static readonly string[] locations = new string[] { "/usr/share/terminfo", "/etc/terminfo", "/usr/lib/terminfo", "/lib/terminfo" };

		// Token: 0x04000D51 RID: 3409
		private TermInfoReader reader;

		// Token: 0x04000D52 RID: 3410
		private int cursorLeft;

		// Token: 0x04000D53 RID: 3411
		private int cursorTop;

		// Token: 0x04000D54 RID: 3412
		private string title = string.Empty;

		// Token: 0x04000D55 RID: 3413
		private string titleFormat = string.Empty;

		// Token: 0x04000D56 RID: 3414
		private bool cursorVisible = true;

		// Token: 0x04000D57 RID: 3415
		private string csrVisible;

		// Token: 0x04000D58 RID: 3416
		private string csrInvisible;

		// Token: 0x04000D59 RID: 3417
		private string clear;

		// Token: 0x04000D5A RID: 3418
		private string bell;

		// Token: 0x04000D5B RID: 3419
		private string term;

		// Token: 0x04000D5C RID: 3420
		private StreamReader stdin;

		// Token: 0x04000D5D RID: 3421
		private CStreamWriter stdout;

		// Token: 0x04000D5E RID: 3422
		private int windowWidth;

		// Token: 0x04000D5F RID: 3423
		private int windowHeight;

		// Token: 0x04000D60 RID: 3424
		private int bufferHeight;

		// Token: 0x04000D61 RID: 3425
		private int bufferWidth;

		// Token: 0x04000D62 RID: 3426
		private char[] buffer;

		// Token: 0x04000D63 RID: 3427
		private int readpos;

		// Token: 0x04000D64 RID: 3428
		private int writepos;

		// Token: 0x04000D65 RID: 3429
		private string keypadXmit;

		// Token: 0x04000D66 RID: 3430
		private string keypadLocal;

		// Token: 0x04000D67 RID: 3431
		private bool controlCAsInput;

		// Token: 0x04000D68 RID: 3432
		private bool inited;

		// Token: 0x04000D69 RID: 3433
		private object initLock = new object();

		// Token: 0x04000D6A RID: 3434
		private bool initKeys;

		// Token: 0x04000D6B RID: 3435
		private string origPair;

		// Token: 0x04000D6C RID: 3436
		private string origColors;

		// Token: 0x04000D6D RID: 3437
		private string cursorAddress;

		// Token: 0x04000D6E RID: 3438
		private ConsoleColor fgcolor = ConsoleColor.White;

		// Token: 0x04000D6F RID: 3439
		private ConsoleColor bgcolor;

		// Token: 0x04000D70 RID: 3440
		private string setfgcolor;

		// Token: 0x04000D71 RID: 3441
		private string setbgcolor;

		// Token: 0x04000D72 RID: 3442
		private int maxColors;

		// Token: 0x04000D73 RID: 3443
		private bool noGetPosition;

		// Token: 0x04000D74 RID: 3444
		private Hashtable keymap;

		// Token: 0x04000D75 RID: 3445
		private ByteMatcher rootmap;

		// Token: 0x04000D76 RID: 3446
		private int rl_startx = -1;

		// Token: 0x04000D77 RID: 3447
		private int rl_starty = -1;

		// Token: 0x04000D78 RID: 3448
		private byte[] control_characters;

		// Token: 0x04000D79 RID: 3449
		private static readonly int[] _consoleColorToAnsiCode = new int[]
		{
			0, 4, 2, 6, 1, 5, 3, 7, 8, 12,
			10, 14, 9, 13, 11, 15
		};

		// Token: 0x04000D7A RID: 3450
		private char[] echobuf;

		// Token: 0x04000D7B RID: 3451
		private int echon;
	}
}
