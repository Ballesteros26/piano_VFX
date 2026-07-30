using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.SessionState
{
	/// <summary>Defines the contract for the collection used by ASP.NET session state to manage session.</summary>
	// Token: 0x02000497 RID: 1175
	public interface ISessionStateItemCollection : ICollection, IEnumerable
	{
		/// <summary>Removes all values and keys from the session-state collection.</summary>
		// Token: 0x0600356F RID: 13679
		void Clear();

		/// <summary>Deletes an item from the collection.</summary>
		/// <param name="name">The name of the item to delete from the collection.</param>
		// Token: 0x06003570 RID: 13680
		void Remove(string name);

		/// <summary>Deletes an item at a specified index from the collection.</summary>
		/// <param name="index">The index of the item to remove from the collection.</param>
		// Token: 0x06003571 RID: 13681
		void RemoveAt(int index);

		/// <summary>Gets or sets a value indicating whether the collection has been marked as changed.</summary>
		/// <returns>true if the <see cref="T:System.Web.SessionState.SessionStateItemCollection" /> contents have been changed; otherwise, false.</returns>
		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x06003572 RID: 13682
		// (set) Token: 0x06003573 RID: 13683
		bool Dirty { get; set; }

		/// <summary>Gets or sets a value in the collection by numerical index.</summary>
		/// <returns>The value in the collection stored at the specified index.</returns>
		/// <param name="index">The numerical index of the value in the collection.</param>
		// Token: 0x170010EE RID: 4334
		object this[int index] { get; set; }

		/// <summary>Gets or sets a value in the collection by name.</summary>
		/// <returns>The value in the collection with the specified name.</returns>
		/// <param name="name">The key name of the value in the collection.</param>
		// Token: 0x170010EF RID: 4335
		object this[string name] { get; set; }

		/// <summary>Gets a collection of the variable names for all values stored in the collection.</summary>
		/// <returns>The <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> that contains all the collection keys.</returns>
		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x06003578 RID: 13688
		NameObjectCollectionBase.KeysCollection Keys { get; }
	}
}
