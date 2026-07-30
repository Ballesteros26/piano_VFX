using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies the name of the property that accesses the attributed field.</summary>
	// Token: 0x0200085E RID: 2142
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class AccessedThroughPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the AccessedThroughPropertyAttribute class with the name of the property used to access the attributed field.</summary>
		/// <param name="propertyName">The name of the property used to access the attributed field. </param>
		// Token: 0x06005436 RID: 21558 RVA: 0x0012729E File Offset: 0x0012549E
		public AccessedThroughPropertyAttribute(string propertyName)
		{
			this.propertyName = propertyName;
		}

		/// <summary>Gets the name of the property used to access the attributed field.</summary>
		/// <returns>The name of the property used to access the attributed field.</returns>
		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06005437 RID: 21559 RVA: 0x001272AD File Offset: 0x001254AD
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x04002BB2 RID: 11186
		private readonly string propertyName;
	}
}
