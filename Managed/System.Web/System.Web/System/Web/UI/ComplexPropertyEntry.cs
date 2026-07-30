using System;
using Unity;

namespace System.Web.UI
{
	/// <summary>Serves as the property entry for read/write and read-only properties such as templates.</summary>
	// Token: 0x020001B4 RID: 436
	public class ComplexPropertyEntry : BuilderPropertyEntry
	{
		// Token: 0x060010C3 RID: 4291 RVA: 0x0002E313 File Offset: 0x0002C513
		internal ComplexPropertyEntry(bool isCollectionItem, bool readOnly)
		{
			this.IsCollectionItem = isCollectionItem;
			this.ReadOnly = readOnly;
		}

		/// <summary>Gets a value indicating whether the property is a collection object.</summary>
		/// <returns>true if the property entry represents an item that contains a collection of values; otherwise, false.</returns>
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060010C4 RID: 4292 RVA: 0x0002E329 File Offset: 0x0002C529
		// (set) Token: 0x060010C5 RID: 4293 RVA: 0x0002E331 File Offset: 0x0002C531
		public bool IsCollectionItem { get; private set; }

		/// <summary>Gets or sets a value indicating whether the item represented in the property entry contains a method for setting its value.</summary>
		/// <returns>true if the item represented by the property entry does not contain a set method; otherwise, false.</returns>
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x0002E33A File Offset: 0x0002C53A
		// (set) Token: 0x060010C7 RID: 4295 RVA: 0x0002E342 File Offset: 0x0002C542
		public bool ReadOnly { get; set; }

		// Token: 0x060010C8 RID: 4296 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal ComplexPropertyEntry()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
