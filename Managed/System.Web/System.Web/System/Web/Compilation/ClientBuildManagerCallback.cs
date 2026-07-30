using System;
using System.CodeDom.Compiler;

namespace System.Web.Compilation
{
	/// <summary>Receives status information about a build from the <see cref="T:System.Web.Compilation.ClientBuildManager" /> object.</summary>
	// Token: 0x0200064A RID: 1610
	public class ClientBuildManagerCallback : MarshalByRefObject
	{
		/// <summary>Reports compilation errors and warnings that occur during an application build.</summary>
		/// <param name="error">The error or warning encountered during compilation. </param>
		// Token: 0x06004546 RID: 17734 RVA: 0x0000393A File Offset: 0x00001B3A
		public virtual void ReportCompilerError(CompilerError error)
		{
		}

		/// <summary>Reports parsing errors and warnings that occur during an application build.</summary>
		/// <param name="error">The error or warning encountered during parsing.</param>
		// Token: 0x06004547 RID: 17735 RVA: 0x0000393A File Offset: 0x00001B3A
		public virtual void ReportParseError(ParserError error)
		{
		}

		/// <summary>Reports the progress of an application build.</summary>
		/// <param name="message">A <see cref="T:System.String" /> containing the current status of the build.</param>
		// Token: 0x06004548 RID: 17736 RVA: 0x0000393A File Offset: 0x00001B3A
		public virtual void ReportProgress(string message)
		{
		}
	}
}
