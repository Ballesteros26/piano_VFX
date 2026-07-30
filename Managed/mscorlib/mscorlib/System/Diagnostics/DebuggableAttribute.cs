using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Modifies code generation for runtime just-in-time (JIT) debugging. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000A64 RID: 2660
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module, AllowMultiple = false)]
	[ComVisible(true)]
	public sealed class DebuggableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggableAttribute" /> class, using the specified tracking and optimization options for the just-in-time (JIT) compiler.</summary>
		/// <param name="isJITTrackingEnabled">true to enable debugging; otherwise, false. </param>
		/// <param name="isJITOptimizerDisabled">true to disable the optimizer for execution; otherwise, false. </param>
		// Token: 0x0600616B RID: 24939 RVA: 0x0013FF29 File Offset: 0x0013E129
		public DebuggableAttribute(bool isJITTrackingEnabled, bool isJITOptimizerDisabled)
		{
			this.m_debuggingModes = DebuggableAttribute.DebuggingModes.None;
			if (isJITTrackingEnabled)
			{
				this.m_debuggingModes |= DebuggableAttribute.DebuggingModes.Default;
			}
			if (isJITOptimizerDisabled)
			{
				this.m_debuggingModes |= DebuggableAttribute.DebuggingModes.DisableOptimizations;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggableAttribute" /> class, using the specified debugging modes for the just-in-time (JIT) compiler. </summary>
		/// <param name="modes">A bitwise combination of the <see cref="T:System.Diagnostics.DebuggableAttribute.DebuggingModes" />  values specifying the debugging mode for the JIT compiler.</param>
		// Token: 0x0600616C RID: 24940 RVA: 0x0013FF5E File Offset: 0x0013E15E
		public DebuggableAttribute(DebuggableAttribute.DebuggingModes modes)
		{
			this.m_debuggingModes = modes;
		}

		/// <summary>Gets a value that indicates whether the runtime will track information during code generation for the debugger.</summary>
		/// <returns>true if the runtime will track information during code generation for the debugger; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x0600616D RID: 24941 RVA: 0x0013FF6D File Offset: 0x0013E16D
		public bool IsJITTrackingEnabled
		{
			get
			{
				return (this.m_debuggingModes & DebuggableAttribute.DebuggingModes.Default) > DebuggableAttribute.DebuggingModes.None;
			}
		}

		/// <summary>Gets a value that indicates whether the runtime optimizer is disabled.</summary>
		/// <returns>true if the runtime optimizer is disabled; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x0600616E RID: 24942 RVA: 0x0013FF7A File Offset: 0x0013E17A
		public bool IsJITOptimizerDisabled
		{
			get
			{
				return (this.m_debuggingModes & DebuggableAttribute.DebuggingModes.DisableOptimizations) > DebuggableAttribute.DebuggingModes.None;
			}
		}

		/// <summary>Gets the debugging modes for the attribute.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.DebuggableAttribute.DebuggingModes" /> values describing the debugging mode for the just-in-time (JIT) compiler. The default is <see cref="F:System.Diagnostics.DebuggableAttribute.DebuggingModes.Default" />. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x0600616F RID: 24943 RVA: 0x0013FF8B File Offset: 0x0013E18B
		public DebuggableAttribute.DebuggingModes DebuggingFlags
		{
			get
			{
				return this.m_debuggingModes;
			}
		}

		// Token: 0x040030A3 RID: 12451
		private DebuggableAttribute.DebuggingModes m_debuggingModes;

		/// <summary>Specifies the debugging mode for the just-in-time (JIT) compiler.</summary>
		// Token: 0x02000A65 RID: 2661
		[Flags]
		[ComVisible(true)]
		public enum DebuggingModes
		{
			/// <summary>In the .NET Framework version 2.0, JIT tracking information is always generated, and this flag has the same effect as <see cref="F:System.Diagnostics.DebuggableAttribute.DebuggingModes.Default" /> with the exception of the <see cref="P:System.Diagnostics.DebuggableAttribute.IsJITTrackingEnabled" /> property being false, which has no meaning in version 2.0.</summary>
			// Token: 0x040030A5 RID: 12453
			None = 0,
			/// <summary>Instructs the just-in-time (JIT) compiler to use its default behavior, which includes enabling optimizations, disabling Edit and Continue support, and using symbol store sequence points if present. In the .NET Framework version 2.0, JIT tracking information, the Microsoft intermediate language (MSIL) offset to the native-code offset within a method, is always generated.</summary>
			// Token: 0x040030A6 RID: 12454
			Default = 1,
			/// <summary>Disable optimizations performed by the compiler to make your output file smaller, faster, and more efficient. Optimizations result in code rearrangement in the output file, which can make debugging difficult. Typically optimization should be disabled while debugging. </summary>
			// Token: 0x040030A7 RID: 12455
			DisableOptimizations = 256,
			/// <summary>Use the implicit MSIL sequence points, not the program database (PDB) sequence points. The symbolic information normally includes at least one Microsoft intermediate language (MSIL) offset for each source line. When the just-in-time (JIT) compiler is about to compile a method, it asks the profiling services for a list of MSIL offsets that should be preserved. These MSIL offsets are called sequence points.</summary>
			// Token: 0x040030A8 RID: 12456
			IgnoreSymbolStoreSequencePoints = 2,
			/// <summary>Enable edit and continue. Edit and continue enables you to make changes to your source code while your program is in break mode. The ability to edit and continue is compiler dependent. </summary>
			// Token: 0x040030A9 RID: 12457
			EnableEditAndContinue = 4
		}
	}
}
