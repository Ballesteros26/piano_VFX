using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x0200001B RID: 27
	internal class DebugHelper
	{
		// Token: 0x060000DB RID: 219 RVA: 0x000044E8 File Offset: 0x000026E8
		static DebugHelper()
		{
			Debug.AutoFlush = true;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000044FC File Offset: 0x000026FC
		[Conditional("DEBUG")]
		internal static void DumpCallers()
		{
			StackTrace stackTrace = new StackTrace(true);
			int frameCount = stackTrace.FrameCount;
			for (int i = 1; i < frameCount; i++)
			{
				StackFrame frame = stackTrace.GetFrame(i);
				MethodBase method = frame.GetMethod();
				string text = frame.GetFileName();
				if (text != null && text.Length > 1)
				{
					text = text.Substring(text.LastIndexOf(Path.DirectorySeparatorChar) + 1);
				}
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000456C File Offset: 0x0000276C
		[Conditional("DEBUG")]
		internal static void DumpCallers(int count)
		{
			StackTrace stackTrace = new StackTrace(true);
			int num = ((count <= stackTrace.FrameCount) ? count : stackTrace.FrameCount);
			for (int i = 1; i < num; i++)
			{
				StackFrame frame = stackTrace.GetFrame(i);
				MethodBase method = frame.GetMethod();
				string text = frame.GetFileName();
				if (text != null && text.Length > 1)
				{
					text = text.Substring(text.LastIndexOf(Path.DirectorySeparatorChar) + 1);
				}
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000045F0 File Offset: 0x000027F0
		[Conditional("DEBUG")]
		internal static void Enter()
		{
			StackTrace stackTrace = new StackTrace();
			DebugHelper.methods.Push(new DebugHelper.Data(stackTrace.GetFrame(1).GetMethod(), null));
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004620 File Offset: 0x00002820
		[Conditional("DEBUG")]
		internal static void Enter(object[] args)
		{
			StackTrace stackTrace = new StackTrace();
			DebugHelper.methods.Push(new DebugHelper.Data(stackTrace.GetFrame(1).GetMethod(), args));
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004650 File Offset: 0x00002850
		[Conditional("DEBUG")]
		internal static void Leave()
		{
			if (DebugHelper.methods.Count > 0)
			{
				DebugHelper.methods.Pop();
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004670 File Offset: 0x00002870
		[Conditional("DEBUG")]
		internal static void Print()
		{
			if (DebugHelper.methods.Count == 0)
			{
				return;
			}
			DebugHelper.Data data = DebugHelper.methods.Peek();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004698 File Offset: 0x00002898
		[Conditional("DEBUG")]
		internal static void Print(int index)
		{
			if (DebugHelper.methods.Count == 0 || DebugHelper.methods.Count <= index || index < 0)
			{
				return;
			}
			Stack<DebugHelper.Data> stack = new Stack<DebugHelper.Data>(index - 1);
			for (int i = 0; i < index; i++)
			{
				stack.Push(DebugHelper.methods.Pop());
			}
			DebugHelper.Data data = DebugHelper.methods.Peek();
			for (int j = 0; j < stack.Count; j++)
			{
				DebugHelper.methods.Push(stack.Pop());
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000472C File Offset: 0x0000292C
		[Conditional("DEBUG")]
		internal static void Print(string methodName, string parameterName)
		{
			if (DebugHelper.methods.Count == 0)
			{
				return;
			}
			Stack<DebugHelper.Data> stack = new Stack<DebugHelper.Data>();
			DebugHelper.Data data = DebugHelper.methods.Peek();
			bool flag = false;
			for (int i = 0; i < DebugHelper.methods.Count; i++)
			{
				data = DebugHelper.methods.Peek();
				if (data.method.Name.Equals(methodName))
				{
					flag = true;
					break;
				}
				stack.Push(DebugHelper.methods.Pop());
			}
			for (int j = 0; j < stack.Count; j++)
			{
				DebugHelper.methods.Push(stack.Pop());
			}
			if (!flag)
			{
				return;
			}
			ParameterInfo[] parameters = data.method.GetParameters();
			for (int k = 0; k < parameters.Length; k++)
			{
				if (!(parameters[k].Name == parameterName) || parameters[k].ParameterType == typeof(IntPtr))
				{
				}
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004840 File Offset: 0x00002A40
		[Conditional("DEBUG")]
		internal static void Print(string parameterName)
		{
			if (DebugHelper.methods.Count == 0)
			{
				return;
			}
			ParameterInfo[] parameters = DebugHelper.methods.Peek().method.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				if (!(parameters[i].Name == parameterName) || parameters[i].ParameterType == typeof(IntPtr))
				{
				}
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000048B8 File Offset: 0x00002AB8
		[Conditional("DEBUG")]
		internal static void WriteLine(object arg)
		{
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000048BC File Offset: 0x00002ABC
		[Conditional("DEBUG")]
		internal static void WriteLine(string format, params object[] arg)
		{
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000048C0 File Offset: 0x00002AC0
		[Conditional("DEBUG")]
		internal static void WriteLine(string message)
		{
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000048C4 File Offset: 0x00002AC4
		[Conditional("DEBUG")]
		internal static void Indent()
		{
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000048C8 File Offset: 0x00002AC8
		[Conditional("DEBUG")]
		internal static void Unindent()
		{
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000048CC File Offset: 0x00002ACC
		[Conditional("TRACE")]
		internal static void TraceWriteLine(string format, params object[] arg)
		{
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000048D0 File Offset: 0x00002AD0
		[Conditional("TRACE")]
		internal static void TraceWriteLine(string message)
		{
		}

		// Token: 0x04000058 RID: 88
		private static Stack<DebugHelper.Data> methods = new Stack<DebugHelper.Data>();

		// Token: 0x0200001C RID: 28
		private struct Data
		{
			// Token: 0x060000EC RID: 236 RVA: 0x000048D4 File Offset: 0x00002AD4
			public Data(MethodBase m, object[] a)
			{
				this.method = m;
				this.args = a;
			}

			// Token: 0x04000059 RID: 89
			public MethodBase method;

			// Token: 0x0400005A RID: 90
			public object[] args;
		}
	}
}
