using System;

namespace System.Xml
{
	/// <summary>Describes the document order of a node compared to a second node.</summary>
	// Token: 0x020002A1 RID: 673
	public enum XmlNodeOrder
	{
		/// <summary>The current node of this navigator is before the current node of the supplied navigator.</summary>
		// Token: 0x04001036 RID: 4150
		Before,
		/// <summary>The current node of this navigator is after the current node of the supplied navigator.</summary>
		// Token: 0x04001037 RID: 4151
		After,
		/// <summary>The two navigators are positioned on the same node.</summary>
		// Token: 0x04001038 RID: 4152
		Same,
		/// <summary>The node positions cannot be determined in document order, relative to each other. This could occur if the two nodes reside in different trees.</summary>
		// Token: 0x04001039 RID: 4153
		Unknown
	}
}
