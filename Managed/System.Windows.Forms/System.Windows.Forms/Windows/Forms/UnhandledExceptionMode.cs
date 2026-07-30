using System;

namespace System.Windows.Forms
{
	/// <summary>Defines where a Windows Forms application should send unhandled exceptions.</summary>
	// Token: 0x0200039E RID: 926
	public enum UnhandledExceptionMode
	{
		/// <summary>Route all exceptions to the <see cref="E:System.Windows.Forms.Application.ThreadException" /> handler, unless the application's configuration file specifies otherwise.</summary>
		// Token: 0x04001C6E RID: 7278
		Automatic,
		/// <summary>Never route exceptions to the <see cref="E:System.Windows.Forms.Application.ThreadException" /> handler. Ignore the application configuration file.</summary>
		// Token: 0x04001C6F RID: 7279
		ThrowException,
		/// <summary>Always route exceptions to the <see cref="E:System.Windows.Forms.Application.ThreadException" /> handler. Ignore the application configuration file.</summary>
		// Token: 0x04001C70 RID: 7280
		CatchException
	}
}
