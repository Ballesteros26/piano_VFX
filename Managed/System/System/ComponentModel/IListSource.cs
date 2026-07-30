using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Provides functionality to an object to return a list that can be bound to a data source.</summary>
	// Token: 0x02000282 RID: 642
	[TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Editor("System.Windows.Forms.Design.DataSourceListEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[MergableProperty(false)]
	public interface IListSource
	{
		/// <summary>Gets a value indicating whether the collection is a collection of <see cref="T:System.Collections.IList" /> objects.</summary>
		/// <returns>true if the collection is a collection of <see cref="T:System.Collections.IList" /> objects; otherwise, false.</returns>
		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600146A RID: 5226
		bool ContainsListCollection { get; }

		/// <summary>Returns an <see cref="T:System.Collections.IList" /> that can be bound to a data source from an object that does not implement an <see cref="T:System.Collections.IList" /> itself.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> that can be bound to a data source from the object.</returns>
		// Token: 0x0600146B RID: 5227
		IList GetList();
	}
}
