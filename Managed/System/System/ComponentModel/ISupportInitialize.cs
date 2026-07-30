using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that this object supports a simple, transacted notification for batch initialization.</summary>
	// Token: 0x0200028B RID: 651
	[SRDescription("Specifies support for transacted initialization.")]
	public interface ISupportInitialize
	{
		/// <summary>Signals the object that initialization is starting.</summary>
		// Token: 0x0600147D RID: 5245
		void BeginInit();

		/// <summary>Signals the object that initialization is complete.</summary>
		// Token: 0x0600147E RID: 5246
		void EndInit();
	}
}
