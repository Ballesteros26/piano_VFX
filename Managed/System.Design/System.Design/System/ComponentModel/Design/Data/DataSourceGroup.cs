using System;
using System.Drawing;

namespace System.ComponentModel.Design.Data
{
	/// <summary>Implements the basic functionality required by a single data source at the EnvDTE.Project level.</summary>
	// Token: 0x02000164 RID: 356
	public abstract class DataSourceGroup
	{
		/// <summary>When overridden in a derived class, gets the collection of descriptors for the data sources in this group.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.Data.DataSourceDescriptorCollection" /> that represents the collection of descriptors for the data sources in this group.</returns>
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000AB3 RID: 2739
		public abstract DataSourceDescriptorCollection DataSources { get; }

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Drawing.Bitmap" /> image that represents the group.</summary>
		/// <returns>A <see cref="T:System.Drawing.Bitmap" /> image that represents the group.</returns>
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000AB4 RID: 2740
		public abstract Bitmap Image { get; }

		/// <summary>When overridden in a derived class, gets the value indicating whether this group is the default group.</summary>
		/// <returns>true if this group is the default group; otherwise, false.</returns>
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000AB5 RID: 2741
		public abstract bool IsDefault { get; }

		/// <summary>When overridden in a derived class, gets the name of the group.</summary>
		/// <returns>The name of the group.</returns>
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000AB6 RID: 2742
		public abstract string Name { get; }
	}
}
