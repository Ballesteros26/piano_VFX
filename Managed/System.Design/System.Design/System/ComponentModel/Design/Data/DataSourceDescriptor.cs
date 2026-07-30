using System;
using System.Drawing;

namespace System.ComponentModel.Design.Data
{
	/// <summary>Implements the basic functionality required by a single data source at the EnvDTE.Project level.</summary>
	// Token: 0x02000162 RID: 354
	public abstract class DataSourceDescriptor
	{
		/// <summary>When overridden in a derived class, closes this stream and the underlying stream gets the <see cref="T:System.Drawing.Bitmap" /> image that represents the data source.</summary>
		/// <returns>A <see cref="T:System.Drawing.Bitmap" /> image that represents the data source.</returns>
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000AA5 RID: 2725
		public abstract Bitmap Image { get; }

		/// <summary>When overridden in a derived class, gets the value indicating whether the data source is designable.</summary>
		/// <returns>true if the data source is designable; otherwise, false.</returns>
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000AA6 RID: 2726
		public abstract bool IsDesignable { get; }

		/// <summary>When overridden in a derived class, gets the name of the data source.</summary>
		/// <returns>The name of the data source.</returns>
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000AA7 RID: 2727
		public abstract string Name { get; }

		/// <summary>When overridden in a derived class, gets the fully qualified type name of the data source.</summary>
		/// <returns>The fully qualified type name of the data source.</returns>
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000AA8 RID: 2728
		public abstract string TypeName { get; }
	}
}
