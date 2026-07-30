using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Provides a basic utility class that is used to store three related objects.</summary>
	// Token: 0x0200023D RID: 573
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[Serializable]
	public sealed class Triplet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Triplet" /> class. </summary>
		// Token: 0x060017B3 RID: 6067 RVA: 0x00002050 File Offset: 0x00000250
		public Triplet()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Triplet" /> class and sets the first two objects. </summary>
		/// <param name="x">Object assigned to <see cref="F:System.Web.UI.Triplet.First" />.</param>
		/// <param name="y">Object assigned to <see cref="F:System.Web.UI.Triplet.Second" />.</param>
		// Token: 0x060017B4 RID: 6068 RVA: 0x00040714 File Offset: 0x0003E914
		public Triplet(object x, object y)
		{
			this.First = x;
			this.Second = y;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Triplet" /> class with the provided three objects. </summary>
		/// <param name="x">Object assigned to <see cref="F:System.Web.UI.Triplet.First" />.</param>
		/// <param name="y">Object assigned to <see cref="F:System.Web.UI.Triplet.Second" />.</param>
		/// <param name="z">Object assigned to <see cref="F:System.Web.UI.Triplet.Third" />.</param>
		// Token: 0x060017B5 RID: 6069 RVA: 0x0004072A File Offset: 0x0003E92A
		public Triplet(object x, object y, object z)
		{
			this.First = x;
			this.Second = y;
			this.Third = z;
		}

		/// <summary>Gets or sets the first object of the triplet.</summary>
		// Token: 0x040015EF RID: 5615
		public object First;

		/// <summary>Gets or sets the second object of the triplet.</summary>
		// Token: 0x040015F0 RID: 5616
		public object Second;

		/// <summary>Gets or sets the third object of the triplet.</summary>
		// Token: 0x040015F1 RID: 5617
		public object Third;
	}
}
