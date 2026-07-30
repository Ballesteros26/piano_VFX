using System;

namespace System.Xml
{
	/// <summary>Specifies the type of node change.</summary>
	// Token: 0x02000228 RID: 552
	public enum XmlNodeChangedAction
	{
		/// <summary>A node is being inserted in the tree.</summary>
		// Token: 0x04000DE1 RID: 3553
		Insert,
		/// <summary>A node is being removed from the tree.</summary>
		// Token: 0x04000DE2 RID: 3554
		Remove,
		/// <summary>A node value is being changed.</summary>
		// Token: 0x04000DE3 RID: 3555
		Change
	}
}
