using System;
using Internal.Runtime.Augments;

namespace System.Diagnostics.Private
{
	// Token: 0x02000A8F RID: 2703
	internal static class Debug
	{
		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x06006260 RID: 25184 RVA: 0x00003B29 File Offset: 0x00001D29
		// (set) Token: 0x06006261 RID: 25185 RVA: 0x00002194 File Offset: 0x00000394
		public static bool AutoFlush
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x06006262 RID: 25186 RVA: 0x00141197 File Offset: 0x0013F397
		// (set) Token: 0x06006263 RID: 25187 RVA: 0x0014119E File Offset: 0x0013F39E
		public static int IndentLevel
		{
			get
			{
				return Debug.s_indentLevel;
			}
			set
			{
				Debug.s_indentLevel = ((value < 0) ? 0 : value);
			}
		}

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x06006264 RID: 25188 RVA: 0x001411AD File Offset: 0x0013F3AD
		// (set) Token: 0x06006265 RID: 25189 RVA: 0x001411B4 File Offset: 0x0013F3B4
		public static int IndentSize
		{
			get
			{
				return Debug.s_indentSize;
			}
			set
			{
				Debug.s_indentSize = ((value < 0) ? 0 : value);
			}
		}

		// Token: 0x06006266 RID: 25190 RVA: 0x00002194 File Offset: 0x00000394
		[Conditional("DEBUG")]
		public static void Close()
		{
		}

		// Token: 0x06006267 RID: 25191 RVA: 0x00002194 File Offset: 0x00000394
		[Conditional("DEBUG")]
		public static void Flush()
		{
		}

		// Token: 0x06006268 RID: 25192 RVA: 0x001411C3 File Offset: 0x0013F3C3
		[Conditional("DEBUG")]
		public static void Indent()
		{
			Debug.IndentLevel++;
		}

		// Token: 0x06006269 RID: 25193 RVA: 0x001411D1 File Offset: 0x0013F3D1
		[Conditional("DEBUG")]
		public static void Unindent()
		{
			Debug.IndentLevel--;
		}

		// Token: 0x0600626A RID: 25194 RVA: 0x001411DF File Offset: 0x0013F3DF
		[Conditional("DEBUG")]
		public static void Print(string message)
		{
			Debug.Write(message);
		}

		// Token: 0x0600626B RID: 25195 RVA: 0x001411E7 File Offset: 0x0013F3E7
		[Conditional("DEBUG")]
		public static void Print(string format, params object[] args)
		{
			Debug.Write(string.Format(null, format, args));
		}

		// Token: 0x0600626C RID: 25196 RVA: 0x001411F6 File Offset: 0x0013F3F6
		[Conditional("DEBUG")]
		public static void Assert(bool condition)
		{
			Debug.Assert(condition, string.Empty, string.Empty);
		}

		// Token: 0x0600626D RID: 25197 RVA: 0x00141208 File Offset: 0x0013F408
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string message)
		{
			Debug.Assert(condition, message, string.Empty);
		}

		// Token: 0x0600626E RID: 25198 RVA: 0x00141218 File Offset: 0x0013F418
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string message, string detailMessage)
		{
			if (!condition)
			{
				string text;
				try
				{
					text = EnvironmentAugments.StackTrace;
				}
				catch
				{
					text = "";
				}
				Debug.WriteLine(Debug.FormatAssert(text, message, detailMessage));
				Debug.s_ShowAssertDialog(text, message, detailMessage);
			}
		}

		// Token: 0x0600626F RID: 25199 RVA: 0x00141264 File Offset: 0x0013F464
		[Conditional("DEBUG")]
		public static void Fail(string message)
		{
			Debug.Assert(false, message, string.Empty);
		}

		// Token: 0x06006270 RID: 25200 RVA: 0x00141272 File Offset: 0x0013F472
		[Conditional("DEBUG")]
		public static void Fail(string message, string detailMessage)
		{
			Debug.Assert(false, message, detailMessage);
		}

		// Token: 0x06006271 RID: 25201 RVA: 0x0014127C File Offset: 0x0013F47C
		private static string FormatAssert(string stackTrace, string message, string detailMessage)
		{
			string text = Debug.GetIndentString() + Environment.NewLine;
			return string.Concat(new string[]
			{
				"---- DEBUG ASSERTION FAILED ----", text, "---- Assert Short Message ----", text, message, text, "---- Assert Long Message ----", text, detailMessage, text,
				stackTrace
			});
		}

		// Token: 0x06006272 RID: 25202 RVA: 0x001412DF File Offset: 0x0013F4DF
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string message, string detailMessageFormat, params object[] args)
		{
			Debug.Assert(condition, message, string.Format(detailMessageFormat, args));
		}

		// Token: 0x06006273 RID: 25203 RVA: 0x001412EF File Offset: 0x0013F4EF
		[Conditional("DEBUG")]
		public static void WriteLine(string message)
		{
			Debug.Write(message + Environment.NewLine);
		}

		// Token: 0x06006274 RID: 25204 RVA: 0x00141304 File Offset: 0x0013F504
		[Conditional("DEBUG")]
		public static void Write(string message)
		{
			object obj = Debug.s_lock;
			lock (obj)
			{
				if (message == null)
				{
					Debug.s_WriteCore(string.Empty);
				}
				else
				{
					if (Debug.s_needIndent)
					{
						message = Debug.GetIndentString() + message;
						Debug.s_needIndent = false;
					}
					Debug.s_WriteCore(message);
					if (message.EndsWith(Environment.NewLine))
					{
						Debug.s_needIndent = true;
					}
				}
			}
		}

		// Token: 0x06006275 RID: 25205 RVA: 0x0014138C File Offset: 0x0013F58C
		[Conditional("DEBUG")]
		public static void WriteLine(object value)
		{
			Debug.WriteLine((value != null) ? value.ToString() : null);
		}

		// Token: 0x06006276 RID: 25206 RVA: 0x0014139F File Offset: 0x0013F59F
		[Conditional("DEBUG")]
		public static void WriteLine(object value, string category)
		{
			Debug.WriteLine((value != null) ? value.ToString() : null, category);
		}

		// Token: 0x06006277 RID: 25207 RVA: 0x001413B3 File Offset: 0x0013F5B3
		[Conditional("DEBUG")]
		public static void WriteLine(string format, params object[] args)
		{
			Debug.WriteLine(string.Format(null, format, args));
		}

		// Token: 0x06006278 RID: 25208 RVA: 0x001413C2 File Offset: 0x0013F5C2
		[Conditional("DEBUG")]
		public static void WriteLine(string message, string category)
		{
			if (category == null)
			{
				Debug.WriteLine(message);
				return;
			}
			Debug.WriteLine(category + ":" + message);
		}

		// Token: 0x06006279 RID: 25209 RVA: 0x001413DF File Offset: 0x0013F5DF
		[Conditional("DEBUG")]
		public static void Write(object value)
		{
			Debug.Write((value != null) ? value.ToString() : null);
		}

		// Token: 0x0600627A RID: 25210 RVA: 0x001413F2 File Offset: 0x0013F5F2
		[Conditional("DEBUG")]
		public static void Write(string message, string category)
		{
			if (category == null)
			{
				Debug.Write(message);
				return;
			}
			Debug.Write(category + ":" + message);
		}

		// Token: 0x0600627B RID: 25211 RVA: 0x0014140F File Offset: 0x0013F60F
		[Conditional("DEBUG")]
		public static void Write(object value, string category)
		{
			Debug.Write((value != null) ? value.ToString() : null, category);
		}

		// Token: 0x0600627C RID: 25212 RVA: 0x00141423 File Offset: 0x0013F623
		[Conditional("DEBUG")]
		public static void WriteIf(bool condition, string message)
		{
			if (condition)
			{
				Debug.Write(message);
			}
		}

		// Token: 0x0600627D RID: 25213 RVA: 0x0014142E File Offset: 0x0013F62E
		[Conditional("DEBUG")]
		public static void WriteIf(bool condition, object value)
		{
			if (condition)
			{
				Debug.Write(value);
			}
		}

		// Token: 0x0600627E RID: 25214 RVA: 0x00141439 File Offset: 0x0013F639
		[Conditional("DEBUG")]
		public static void WriteIf(bool condition, string message, string category)
		{
			if (condition)
			{
				Debug.Write(message, category);
			}
		}

		// Token: 0x0600627F RID: 25215 RVA: 0x00141445 File Offset: 0x0013F645
		[Conditional("DEBUG")]
		public static void WriteIf(bool condition, object value, string category)
		{
			if (condition)
			{
				Debug.Write(value, category);
			}
		}

		// Token: 0x06006280 RID: 25216 RVA: 0x00141451 File Offset: 0x0013F651
		[Conditional("DEBUG")]
		public static void WriteLineIf(bool condition, object value)
		{
			if (condition)
			{
				Debug.WriteLine(value);
			}
		}

		// Token: 0x06006281 RID: 25217 RVA: 0x0014145C File Offset: 0x0013F65C
		[Conditional("DEBUG")]
		public static void WriteLineIf(bool condition, object value, string category)
		{
			if (condition)
			{
				Debug.WriteLine(value, category);
			}
		}

		// Token: 0x06006282 RID: 25218 RVA: 0x00141468 File Offset: 0x0013F668
		[Conditional("DEBUG")]
		public static void WriteLineIf(bool condition, string message)
		{
			if (condition)
			{
				Debug.WriteLine(message);
			}
		}

		// Token: 0x06006283 RID: 25219 RVA: 0x00141473 File Offset: 0x0013F673
		[Conditional("DEBUG")]
		public static void WriteLineIf(bool condition, string message, string category)
		{
			if (condition)
			{
				Debug.WriteLine(message, category);
			}
		}

		// Token: 0x06006284 RID: 25220 RVA: 0x00141480 File Offset: 0x0013F680
		private static string GetIndentString()
		{
			int num = Debug.IndentSize * Debug.IndentLevel;
			string text = Debug.s_indentString;
			if (text != null && text.Length == num)
			{
				return Debug.s_indentString;
			}
			return Debug.s_indentString = new string(' ', num);
		}

		// Token: 0x06006285 RID: 25221 RVA: 0x00002194 File Offset: 0x00000394
		private static void ShowAssertDialog(string stackTrace, string message, string detailMessage)
		{
		}

		// Token: 0x06006286 RID: 25222 RVA: 0x00002194 File Offset: 0x00000394
		private static void WriteCore(string message)
		{
		}

		// Token: 0x0400310A RID: 12554
		private static readonly object s_lock = new object();

		// Token: 0x0400310B RID: 12555
		[ThreadStatic]
		private static int s_indentLevel;

		// Token: 0x0400310C RID: 12556
		private static int s_indentSize = 4;

		// Token: 0x0400310D RID: 12557
		private static bool s_needIndent;

		// Token: 0x0400310E RID: 12558
		private static string s_indentString;

		// Token: 0x0400310F RID: 12559
		internal static Action<string, string, string> s_ShowAssertDialog = new Action<string, string, string>(Debug.ShowAssertDialog);

		// Token: 0x04003110 RID: 12560
		internal static Action<string> s_WriteCore = new Action<string>(Debug.WriteCore);

		// Token: 0x02000A90 RID: 2704
		private sealed class DebugAssertException : Exception
		{
			// Token: 0x06006288 RID: 25224 RVA: 0x001414F7 File Offset: 0x0013F6F7
			internal DebugAssertException(string message, string detailMessage, string stackTrace)
				: base(string.Concat(new string[]
				{
					message,
					Environment.NewLine,
					detailMessage,
					Environment.NewLine,
					stackTrace
				}))
			{
			}
		}
	}
}
