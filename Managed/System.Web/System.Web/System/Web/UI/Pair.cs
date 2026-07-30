using System;

namespace System.Web.UI
{
	/// <summary>Provides a basic utility class that is used to store two related objects. </summary>
	// Token: 0x02000190 RID: 400
	[Serializable]
	public sealed class Pair
	{
		/// <summary>Creates a new, uninitialized instance of the <see cref="T:System.Web.UI.Pair" /> class.</summary>
		// Token: 0x06000FB5 RID: 4021 RVA: 0x00002050 File Offset: 0x00000250
		public Pair()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Pair" /> class, using the specified object pair.</summary>
		/// <param name="x">An object. </param>
		/// <param name="y">An object. </param>
		// Token: 0x06000FB6 RID: 4022 RVA: 0x0002B5E5 File Offset: 0x000297E5
		public Pair(object x, object y)
		{
			this.First = x;
			this.Second = y;
		}

		/// <summary>Gets or sets the first object of the object pair.</summary>
		// Token: 0x0400131F RID: 4895
		public object First;

		/// <summary>Gets or sets the second object of the object pair.</summary>
		// Token: 0x04001320 RID: 4896
		public object Second;
	}
}
