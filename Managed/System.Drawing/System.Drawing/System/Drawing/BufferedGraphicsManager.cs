using System;

namespace System.Drawing
{
	/// <summary>Provides access to the main buffered graphics context object for the application domain.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000041 RID: 65
	public sealed class BufferedGraphicsManager
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x00002050 File Offset: 0x00000250
		private BufferedGraphicsManager()
		{
		}

		/// <summary>Gets the <see cref="T:System.Drawing.BufferedGraphicsContext" /> for the current application domain.</summary>
		/// <returns>The <see cref="T:System.Drawing.BufferedGraphicsContext" /> for the current application domain.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00005991 File Offset: 0x00003B91
		public static BufferedGraphicsContext Current
		{
			get
			{
				return BufferedGraphicsManager.graphics_context;
			}
		}

		// Token: 0x0400034E RID: 846
		private static BufferedGraphicsContext graphics_context = new BufferedGraphicsContext();
	}
}
