using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Represents a stack trace, which is an ordered collection of one or more stack frames.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000A6D RID: 2669
	[ComVisible(true)]
	[MonoTODO("Serialized objects are not compatible with .NET")]
	[Serializable]
	public class StackTrace
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class from the caller's frame.</summary>
		// Token: 0x060061AC RID: 25004 RVA: 0x001405A7 File Offset: 0x0013E7A7
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackTrace()
		{
			this.init_frames(0, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class from the caller's frame, optionally capturing source information.</summary>
		/// <param name="fNeedFileInfo">true to capture the file name, line number, and column number; otherwise, false. </param>
		// Token: 0x060061AD RID: 25005 RVA: 0x001405B7 File Offset: 0x0013E7B7
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackTrace(bool fNeedFileInfo)
		{
			this.init_frames(0, fNeedFileInfo);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class from the caller's frame, skipping the specified number of frames.</summary>
		/// <param name="skipFrames">The number of frames up the stack from which to start the trace. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="skipFrames" /> parameter is negative. </exception>
		// Token: 0x060061AE RID: 25006 RVA: 0x001405C7 File Offset: 0x0013E7C7
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackTrace(int skipFrames)
		{
			this.init_frames(skipFrames, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class from the caller's frame, skipping the specified number of frames and optionally capturing source information.</summary>
		/// <param name="skipFrames">The number of frames up the stack from which to start the trace. </param>
		/// <param name="fNeedFileInfo">true to capture the file name, line number, and column number; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="skipFrames" /> parameter is negative. </exception>
		// Token: 0x060061AF RID: 25007 RVA: 0x001405D7 File Offset: 0x0013E7D7
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackTrace(int skipFrames, bool fNeedFileInfo)
		{
			this.init_frames(skipFrames, fNeedFileInfo);
		}

		// Token: 0x060061B0 RID: 25008 RVA: 0x001405E8 File Offset: 0x0013E7E8
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void init_frames(int skipFrames, bool fNeedFileInfo)
		{
			if (skipFrames < 0)
			{
				throw new ArgumentOutOfRangeException("< 0", "skipFrames");
			}
			List<StackFrame> list = new List<StackFrame>();
			skipFrames += 2;
			StackFrame stackFrame;
			while ((stackFrame = new StackFrame(skipFrames, fNeedFileInfo)) != null && stackFrame.GetMethod() != null)
			{
				list.Add(stackFrame);
				skipFrames++;
			}
			this.debug_info = fNeedFileInfo;
			this.frames = list.ToArray();
		}

		// Token: 0x060061B1 RID: 25009
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern StackFrame[] get_trace(Exception e, int skipFrames, bool fNeedFileInfo);

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class using the provided exception object.</summary>
		/// <param name="e">The exception object from which to construct the stack trace. </param>
		/// <exception cref="T:System.ArgumentNullException">The parameter <paramref name="e" /> is null. </exception>
		// Token: 0x060061B2 RID: 25010 RVA: 0x0014064E File Offset: 0x0013E84E
		public StackTrace(Exception e)
			: this(e, 0, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class, using the provided exception object and optionally capturing source information.</summary>
		/// <param name="e">The exception object from which to construct the stack trace. </param>
		/// <param name="fNeedFileInfo">true to capture the file name, line number, and column number; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The parameter <paramref name="e" /> is null. </exception>
		// Token: 0x060061B3 RID: 25011 RVA: 0x00140659 File Offset: 0x0013E859
		public StackTrace(Exception e, bool fNeedFileInfo)
			: this(e, 0, fNeedFileInfo)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class using the provided exception object and skipping the specified number of frames.</summary>
		/// <param name="e">The exception object from which to construct the stack trace. </param>
		/// <param name="skipFrames">The number of frames up the stack from which to start the trace. </param>
		/// <exception cref="T:System.ArgumentNullException">The parameter <paramref name="e" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="skipFrames" /> parameter is negative. </exception>
		// Token: 0x060061B4 RID: 25012 RVA: 0x00140664 File Offset: 0x0013E864
		public StackTrace(Exception e, int skipFrames)
			: this(e, skipFrames, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class using the provided exception object, skipping the specified number of frames and optionally capturing source information.</summary>
		/// <param name="e">The exception object from which to construct the stack trace. </param>
		/// <param name="skipFrames">The number of frames up the stack from which to start the trace. </param>
		/// <param name="fNeedFileInfo">true to capture the file name, line number, and column number; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The parameter <paramref name="e" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="skipFrames" /> parameter is negative. </exception>
		// Token: 0x060061B5 RID: 25013 RVA: 0x00140670 File Offset: 0x0013E870
		public StackTrace(Exception e, int skipFrames, bool fNeedFileInfo)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (skipFrames < 0)
			{
				throw new ArgumentOutOfRangeException("< 0", "skipFrames");
			}
			this.frames = StackTrace.get_trace(e, skipFrames, fNeedFileInfo);
			this.captured_traces = e.captured_traces;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class that contains a single frame.</summary>
		/// <param name="frame">The frame that the <see cref="T:System.Diagnostics.StackTrace" /> object should contain. </param>
		// Token: 0x060061B6 RID: 25014 RVA: 0x001406BF File Offset: 0x0013E8BF
		public StackTrace(StackFrame frame)
		{
			this.frames = new StackFrame[1];
			this.frames[0] = frame;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class for a specific thread, optionally capturing source information. Do not use this constructor overload.</summary>
		/// <param name="targetThread">The thread whose stack trace is requested. </param>
		/// <param name="needFileInfo">true to capture the file name, line number, and column number; otherwise, false. </param>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread <paramref name="targetThread" /> is not suspended. </exception>
		// Token: 0x060061B7 RID: 25015 RVA: 0x001406DC File Offset: 0x0013E8DC
		[MonoLimitation("Not possible to create StackTraces from other threads")]
		[Obsolete]
		public StackTrace(Thread targetThread, bool needFileInfo)
		{
			if (targetThread == Thread.CurrentThread)
			{
				this.init_frames(0, needFileInfo);
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x060061B8 RID: 25016 RVA: 0x001406FA File Offset: 0x0013E8FA
		internal StackTrace(StackFrame[] frames)
		{
			this.frames = frames;
		}

		/// <summary>Gets the number of frames in the stack trace.</summary>
		/// <returns>The number of frames in the stack trace. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x060061B9 RID: 25017 RVA: 0x00140709 File Offset: 0x0013E909
		public virtual int FrameCount
		{
			get
			{
				if (this.frames != null)
				{
					return this.frames.Length;
				}
				return 0;
			}
		}

		/// <summary>Gets the specified stack frame.</summary>
		/// <returns>The specified stack frame.</returns>
		/// <param name="index">The index of the stack frame requested. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060061BA RID: 25018 RVA: 0x0014071D File Offset: 0x0013E91D
		public virtual StackFrame GetFrame(int index)
		{
			if (index < 0 || index >= this.FrameCount)
			{
				return null;
			}
			return this.frames[index];
		}

		/// <summary>Returns a copy of all stack frames in the current stack trace.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.StackFrame" /> representing the function calls in the stack trace.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060061BB RID: 25019 RVA: 0x00140736 File Offset: 0x0013E936
		[ComVisible(false)]
		public virtual StackFrame[] GetFrames()
		{
			return this.frames;
		}

		// Token: 0x060061BC RID: 25020 RVA: 0x00140740 File Offset: 0x0013E940
		private static string GetAotId()
		{
			if (!StackTrace.isAotidSet)
			{
				StackTrace.aotid = Assembly.GetAotId();
				if (StackTrace.aotid != null)
				{
					StackTrace.aotid = new Guid(StackTrace.aotid).ToString("N");
				}
				StackTrace.isAotidSet = true;
			}
			return StackTrace.aotid;
		}

		// Token: 0x060061BD RID: 25021 RVA: 0x0014078C File Offset: 0x0013E98C
		private bool AddFrames(StringBuilder sb)
		{
			string text = Locale.GetText("<unknown method>");
			string text2 = "  ";
			string text3 = Locale.GetText(" in {0}:{1} ");
			string text4 = string.Format("{0}{1}{2} ", Environment.NewLine, text2, Locale.GetText("at"));
			int i;
			for (i = 0; i < this.FrameCount; i++)
			{
				StackFrame frame = this.GetFrame(i);
				if (i == 0)
				{
					sb.AppendFormat("{0}{1} ", text2, Locale.GetText("at"));
				}
				else
				{
					sb.Append(text4);
				}
				if (frame.GetMethod() == null)
				{
					string internalMethodName = frame.GetInternalMethodName();
					if (internalMethodName != null)
					{
						sb.Append(internalMethodName);
					}
					else
					{
						sb.AppendFormat("<0x{0:x5} + 0x{1:x5}> {2}", frame.GetMethodAddress(), frame.GetNativeOffset(), text);
					}
				}
				else
				{
					this.GetFullNameForStackTrace(sb, frame.GetMethod());
					if (frame.GetILOffset() == -1)
					{
						sb.AppendFormat(" <0x{0:x5} + 0x{1:x5}>", frame.GetMethodAddress(), frame.GetNativeOffset());
						if (frame.GetMethodIndex() != 16777215U)
						{
							sb.AppendFormat(" {0}", frame.GetMethodIndex());
						}
					}
					else
					{
						sb.AppendFormat(" [0x{0:x5}]", frame.GetILOffset());
					}
					string text5 = frame.GetSecureFileName();
					if (text5[0] == '<')
					{
						string text6 = frame.GetMethod().Module.ModuleVersionId.ToString("N");
						string aotId = StackTrace.GetAotId();
						if (frame.GetILOffset() != -1 || aotId == null)
						{
							text5 = string.Format("<{0}>", text6);
						}
						else
						{
							text5 = string.Format("<{0}#{1}>", text6, aotId);
						}
					}
					sb.AppendFormat(text3, text5, frame.GetFileLineNumber());
				}
			}
			return i != 0;
		}

		// Token: 0x060061BE RID: 25022 RVA: 0x00140970 File Offset: 0x0013EB70
		internal void GetFullNameForStackTrace(StringBuilder sb, MethodBase mi)
		{
			Type type = mi.DeclaringType;
			if (type.IsGenericType && !type.IsGenericTypeDefinition)
			{
				type = type.GetGenericTypeDefinition();
			}
			foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (methodInfo.MetadataToken == mi.MetadataToken)
				{
					mi = methodInfo;
					break;
				}
			}
			sb.Append(type.ToString());
			sb.Append(".");
			sb.Append(mi.Name);
			if (mi.IsGenericMethod)
			{
				Type[] genericArguments = mi.GetGenericArguments();
				sb.Append("[");
				for (int j = 0; j < genericArguments.Length; j++)
				{
					if (j > 0)
					{
						sb.Append(",");
					}
					sb.Append(genericArguments[j].Name);
				}
				sb.Append("]");
			}
			ParameterInfo[] parameters = mi.GetParameters();
			sb.Append(" (");
			for (int k = 0; k < parameters.Length; k++)
			{
				if (k > 0)
				{
					sb.Append(", ");
				}
				Type type2 = parameters[k].ParameterType;
				if (type2.IsGenericType && !type2.IsGenericTypeDefinition)
				{
					type2 = type2.GetGenericTypeDefinition();
				}
				sb.Append(type2.ToString());
				if (parameters[k].Name != null)
				{
					sb.Append(" ");
					sb.Append(parameters[k].Name);
				}
			}
			sb.Append(")");
		}

		/// <summary>Builds a readable representation of the stack trace.</summary>
		/// <returns>A readable representation of the stack trace.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060061BF RID: 25023 RVA: 0x00140AEC File Offset: 0x0013ECEC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.captured_traces != null)
			{
				StackTrace[] array = this.captured_traces;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].AddFrames(stringBuilder))
					{
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append("--- End of stack trace from previous location where exception was thrown ---");
						stringBuilder.Append(Environment.NewLine);
					}
				}
			}
			this.AddFrames(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060061C0 RID: 25024 RVA: 0x0003D958 File Offset: 0x0003BB58
		internal string ToString(StackTrace.TraceFormat traceFormat)
		{
			return this.ToString();
		}

		/// <summary>Defines the default for the number of methods to omit from the stack trace. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040030C7 RID: 12487
		public const int METHODS_TO_SKIP = 0;

		// Token: 0x040030C8 RID: 12488
		private StackFrame[] frames;

		// Token: 0x040030C9 RID: 12489
		private readonly StackTrace[] captured_traces;

		// Token: 0x040030CA RID: 12490
		private bool debug_info;

		// Token: 0x040030CB RID: 12491
		private static bool isAotidSet;

		// Token: 0x040030CC RID: 12492
		private static string aotid;

		// Token: 0x02000A6E RID: 2670
		internal enum TraceFormat
		{
			// Token: 0x040030CE RID: 12494
			Normal,
			// Token: 0x040030CF RID: 12495
			TrailingNewLine,
			// Token: 0x040030D0 RID: 12496
			NoResourceLookup
		}
	}
}
