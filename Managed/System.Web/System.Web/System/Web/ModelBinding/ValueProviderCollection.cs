using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents the collection of value-provider objects for the application.</summary>
	// Token: 0x0200073E RID: 1854
	public class ValueProviderCollection : Collection<IValueProvider>, IUnvalidatedValueProvider, IValueProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ValueProviderCollection" /> class.</summary>
		// Token: 0x06004C71 RID: 19569 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ValueProviderCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ValueProviderCollection" /> class by using the specified collection of value providers.</summary>
		/// <param name="list">The collection of value providers.</param>
		// Token: 0x06004C72 RID: 19570 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ValueProviderCollection(IList<IValueProvider> list)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a value that indicates whether any value provider in the collection contains the specified prefix.</summary>
		/// <returns>true if any value provider in the collection contains the specified prefix; otherwise, false.</returns>
		/// <param name="prefix">The prefix.</param>
		// Token: 0x06004C73 RID: 19571 RVA: 0x000CAEC4 File Offset: 0x000C90C4
		public virtual bool ContainsPrefix(string prefix)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Returns a value object using the specified key.</summary>
		/// <returns>The value object.</returns>
		/// <param name="key">The key of the value object to retrieve.</param>
		// Token: 0x06004C74 RID: 19572 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual ValueProviderResult GetValue(string key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a value object using the specified key and optionally specifies whether validation should be skipped.</summary>
		/// <returns>The value object.</returns>
		/// <param name="key">The key of the value object to retrieve.</param>
		/// <param name="skipValidation">true to skip validation; otherwise, false. </param>
		// Token: 0x06004C75 RID: 19573 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual ValueProviderResult GetValue(string key, bool skipValidation)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Inserts the specified value provider object into the collection at the specified index location.</summary>
		/// <param name="index">The zero-based index location at which to insert the value provider into the collection.</param>
		/// <param name="item">The value provider object to insert.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="item" /> parameter is null.</exception>
		// Token: 0x06004C76 RID: 19574 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void InsertItem(int index, IValueProvider item)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Replaces the value provider at the specified index location with a new value provider.</summary>
		/// <param name="index">The zero-based index of the element to replace.</param>
		/// <param name="item">The new value for the element at the specified index.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="item" /> parameter is null.</exception>
		// Token: 0x06004C77 RID: 19575 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void SetItem(int index, IValueProvider item)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
