using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Provides information about a <see cref="T:System.Diagnostics.StackFrame" />, which represents a function call on the call stack for the current thread.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000A6C RID: 2668
	[ComVisible(true)]
	[MonoTODO("Serialized objects are not compatible with MS.NET")]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class StackFrame
	{
		// Token: 0x0600619A RID: 24986
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_frame_info(int skip, bool needFileInfo, out MethodBase method, out int iloffset, out int native_offset, out string file, out int line, out int column);

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class.</summary>
		// Token: 0x0600619B RID: 24987 RVA: 0x00140254 File Offset: 0x0013E454
		public StackFrame()
		{
			bool flag = StackFrame.get_frame_info(2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class, optionally capturing source information.</summary>
		/// <param name="fNeedFileInfo">true to capture the file name, line number, and column number of the stack frame; otherwise, false. </param>
		// Token: 0x0600619C RID: 24988 RVA: 0x001402A4 File Offset: 0x0013E4A4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(bool fNeedFileInfo)
		{
			bool flag = StackFrame.get_frame_info(2, fNeedFileInfo, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that corresponds to a frame above the current stack frame.</summary>
		/// <param name="skipFrames">The number of frames up the stack to skip. </param>
		// Token: 0x0600619D RID: 24989 RVA: 0x001402F4 File Offset: 0x0013E4F4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(int skipFrames)
		{
			bool flag = StackFrame.get_frame_info(skipFrames + 2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that corresponds to a frame above the current stack frame, optionally capturing source information.</summary>
		/// <param name="skipFrames">The number of frames up the stack to skip. </param>
		/// <param name="fNeedFileInfo">true to capture the file name, line number, and column number of the stack frame; otherwise, false. </param>
		// Token: 0x0600619E RID: 24990 RVA: 0x00140344 File Offset: 0x0013E544
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(int skipFrames, bool fNeedFileInfo)
		{
			bool flag = StackFrame.get_frame_info(skipFrames + 2, fNeedFileInfo, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that contains only the given file name and line number.</summary>
		/// <param name="fileName">The file name. </param>
		/// <param name="lineNumber">The line number in the specified file. </param>
		// Token: 0x0600619F RID: 24991 RVA: 0x00140394 File Offset: 0x0013E594
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(string fileName, int lineNumber)
		{
			bool flag = StackFrame.get_frame_info(2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out fileName, out lineNumber, out this.columnNumber);
			this.fileName = fileName;
			this.lineNumber = lineNumber;
			this.columnNumber = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that contains only the given file name, line number, and column number.</summary>
		/// <param name="fileName">The file name. </param>
		/// <param name="lineNumber">The line number in the specified file. </param>
		/// <param name="colNumber">The column number in the specified file. </param>
		// Token: 0x060061A0 RID: 24992 RVA: 0x001403F0 File Offset: 0x0013E5F0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(string fileName, int lineNumber, int colNumber)
		{
			bool flag = StackFrame.get_frame_info(2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out fileName, out lineNumber, out this.columnNumber);
			this.fileName = fileName;
			this.lineNumber = lineNumber;
			this.columnNumber = colNumber;
		}

		/// <summary>Gets the line number in the file that contains the code that is executing. This information is typically extracted from the debugging symbols for the executable.</summary>
		/// <returns>The file line number, or 0 (zero) if the file line number cannot be determined.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060061A1 RID: 24993 RVA: 0x0014044A File Offset: 0x0013E64A
		public virtual int GetFileLineNumber()
		{
			return this.lineNumber;
		}

		/// <summary>Gets the column number in the file that contains the code that is executing. This information is typically extracted from the debugging symbols for the executable.</summary>
		/// <returns>The file column number, or 0 (zero) if the file column number cannot be determined.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060061A2 RID: 24994 RVA: 0x00140452 File Offset: 0x0013E652
		public virtual int GetFileColumnNumber()
		{
			return this.columnNumber;
		}

		/// <summary>Gets the file name that contains the code that is executing. This information is typically extracted from the debugging symbols for the executable.</summary>
		/// <returns>The file name, or null if the file name cannot be determined.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060061A3 RID: 24995 RVA: 0x0014045A File Offset: 0x0013E65A
		public virtual string GetFileName()
		{
			return this.fileName;
		}

		// Token: 0x060061A4 RID: 24996 RVA: 0x00140464 File Offset: 0x0013E664
		internal string GetSecureFileName()
		{
			string text = "<filename unknown>";
			if (this.fileName == null)
			{
				return text;
			}
			try
			{
				text = this.GetFileName();
			}
			catch (SecurityException)
			{
			}
			return text;
		}

		/// <summary>Gets the offset from the start of the Microsoft intermediate language (MSIL) code for the method that is executing. This offset might be an approximation depending on whether or not the just-in-time (JIT) compiler is generating debugging code. The generation of this debugging information is controlled by the <see cref="T:System.Diagnostics.DebuggableAttribute" />.</summary>
		/// <returns>The offset from the start of the MSIL code for the method that is executing.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060061A5 RID: 24997 RVA: 0x001404A0 File Offset: 0x0013E6A0
		public virtual int GetILOffset()
		{
			return this.ilOffset;
		}

		/// <summary>Gets the method in which the frame is executing.</summary>
		/// <returns>The method in which the frame is executing.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060061A6 RID: 24998 RVA: 0x001404A8 File Offset: 0x0013E6A8
		public virtual MethodBase GetMethod()
		{
			return this.methodBase;
		}

		/// <summary>Gets the offset from the start of the native just-in-time (JIT)-compiled code for the method that is being executed. The generation of this debugging information is controlled by the <see cref="T:System.Diagnostics.DebuggableAttribute" /> class.</summary>
		/// <returns>The offset from the start of the JIT-compiled code for the method that is being executed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060061A7 RID: 24999 RVA: 0x001404B0 File Offset: 0x0013E6B0
		public virtual int GetNativeOffset()
		{
			return this.nativeOffset;
		}

		// Token: 0x060061A8 RID: 25000 RVA: 0x001404B8 File Offset: 0x0013E6B8
		internal long GetMethodAddress()
		{
			return this.methodAddress;
		}

		// Token: 0x060061A9 RID: 25001 RVA: 0x001404C0 File Offset: 0x0013E6C0
		internal uint GetMethodIndex()
		{
			return this.methodIndex;
		}

		// Token: 0x060061AA RID: 25002 RVA: 0x001404C8 File Offset: 0x0013E6C8
		internal string GetInternalMethodName()
		{
			return this.internalMethodName;
		}

		/// <summary>Builds a readable representation of the stack trace.</summary>
		/// <returns>A readable representation of the stack trace.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060061AB RID: 25003 RVA: 0x001404D0 File Offset: 0x0013E6D0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.methodBase == null)
			{
				stringBuilder.Append(Locale.GetText("<unknown method>"));
			}
			else
			{
				stringBuilder.Append(this.methodBase.Name);
			}
			stringBuilder.Append(Locale.GetText(" at "));
			if (this.ilOffset == -1)
			{
				stringBuilder.Append(Locale.GetText("<unknown offset>"));
			}
			else
			{
				stringBuilder.Append(Locale.GetText("offset "));
				stringBuilder.Append(this.ilOffset);
			}
			stringBuilder.Append(Locale.GetText(" in file:line:column "));
			stringBuilder.Append(this.GetSecureFileName());
			stringBuilder.AppendFormat(":{0}:{1}", this.lineNumber, this.columnNumber);
			return stringBuilder.ToString();
		}

		/// <summary>Defines the value that is returned from the <see cref="M:System.Diagnostics.StackFrame.GetNativeOffset" /> or <see cref="M:System.Diagnostics.StackFrame.GetILOffset" /> method when the native or Microsoft intermediate language (MSIL) offset is unknown. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040030BD RID: 12477
		public const int OFFSET_UNKNOWN = -1;

		// Token: 0x040030BE RID: 12478
		private int ilOffset = -1;

		// Token: 0x040030BF RID: 12479
		private int nativeOffset = -1;

		// Token: 0x040030C0 RID: 12480
		private long methodAddress;

		// Token: 0x040030C1 RID: 12481
		private uint methodIndex;

		// Token: 0x040030C2 RID: 12482
		private MethodBase methodBase;

		// Token: 0x040030C3 RID: 12483
		private string fileName;

		// Token: 0x040030C4 RID: 12484
		private int lineNumber;

		// Token: 0x040030C5 RID: 12485
		private int columnNumber;

		// Token: 0x040030C6 RID: 12486
		private string internalMethodName;
	}
}
