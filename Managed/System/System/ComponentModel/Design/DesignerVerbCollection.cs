using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects.</summary>
	// Token: 0x0200031A RID: 794
	[ComVisible(true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class DesignerVerbCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> class.</summary>
		// Token: 0x0600194B RID: 6475 RVA: 0x00046A70 File Offset: 0x00044C70
		public DesignerVerbCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> class using the specified array of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects.</summary>
		/// <param name="value">A <see cref="T:System.ComponentModel.Design.DesignerVerb" /> array that indicates the verbs to contain within the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x0600194C RID: 6476 RVA: 0x00069DB1 File Offset: 0x00067FB1
		public DesignerVerbCollection(DesignerVerb[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.Design.DesignerVerb" /> at the specified index.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerVerb" /> at each valid index in the collection.</returns>
		/// <param name="index">The index at which to get or set the <see cref="T:System.ComponentModel.Design.DesignerVerb" />. </param>
		// Token: 0x17000529 RID: 1321
		public DesignerVerb this[int index]
		{
			get
			{
				return (DesignerVerb)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to the collection.</summary>
		/// <returns>The index in the collection at which the verb was added.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to add to the collection. </param>
		// Token: 0x0600194F RID: 6479 RVA: 0x00049742 File Offset: 0x00047942
		public int Add(DesignerVerb value)
		{
			return base.List.Add(value);
		}

		/// <summary>Adds the specified set of designer verbs to the collection.</summary>
		/// <param name="value">An array of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06001950 RID: 6480 RVA: 0x00069DD4 File Offset: 0x00067FD4
		public void AddRange(DesignerVerb[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Adds the specified collection of designer verbs to the collection.</summary>
		/// <param name="value">A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06001951 RID: 6481 RVA: 0x00069E08 File Offset: 0x00068008
		public void AddRange(DesignerVerbCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int count = value.Count;
			for (int i = 0; i < count; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Inserts the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> at the specified index.</summary>
		/// <param name="index">The index in the collection at which to insert the verb. </param>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to insert in the collection. </param>
		// Token: 0x06001952 RID: 6482 RVA: 0x00049807 File Offset: 0x00047A07
		public void Insert(int index, DesignerVerb value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" />.</summary>
		/// <returns>The index of the specified object if it is found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> whose index to get in the collection. </param>
		// Token: 0x06001953 RID: 6483 RVA: 0x000497F9 File Offset: 0x000479F9
		public int IndexOf(DesignerVerb value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Gets a value indicating whether the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> exists in the collection.</summary>
		/// <returns>true if the specified object exists in the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to search for in the collection. </param>
		// Token: 0x06001954 RID: 6484 RVA: 0x000497DC File Offset: 0x000479DC
		public bool Contains(DesignerVerb value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Removes the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to remove from the collection. </param>
		// Token: 0x06001955 RID: 6485 RVA: 0x00049859 File Offset: 0x00047A59
		public void Remove(DesignerVerb value)
		{
			base.List.Remove(value);
		}

		/// <summary>Copies the collection members to the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> array beginning at the specified destination index.</summary>
		/// <param name="array">The array to copy collection members to. </param>
		/// <param name="index">The destination index to begin copying to. </param>
		// Token: 0x06001956 RID: 6486 RVA: 0x000497EA File Offset: 0x000479EA
		public void CopyTo(DesignerVerb[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Raises the Set event.</summary>
		/// <param name="index">The index at which to set the item. </param>
		/// <param name="oldValue">The old object. </param>
		/// <param name="newValue">The new object. </param>
		// Token: 0x06001957 RID: 6487 RVA: 0x000027E8 File Offset: 0x000009E8
		protected override void OnSet(int index, object oldValue, object newValue)
		{
		}

		/// <summary>Raises the Insert event.</summary>
		/// <param name="index">The index at which to insert an item. </param>
		/// <param name="value">The object to insert. </param>
		// Token: 0x06001958 RID: 6488 RVA: 0x000027E8 File Offset: 0x000009E8
		protected override void OnInsert(int index, object value)
		{
		}

		/// <summary>Raises the Clear event.</summary>
		// Token: 0x06001959 RID: 6489 RVA: 0x000027E8 File Offset: 0x000009E8
		protected override void OnClear()
		{
		}

		/// <summary>Raises the Remove event.</summary>
		/// <param name="index">The index at which to remove the item. </param>
		/// <param name="value">The object to remove. </param>
		// Token: 0x0600195A RID: 6490 RVA: 0x000027E8 File Offset: 0x000009E8
		protected override void OnRemove(int index, object value)
		{
		}

		/// <summary>Raises the Validate event.</summary>
		/// <param name="value">The object to validate. </param>
		// Token: 0x0600195B RID: 6491 RVA: 0x000027E8 File Offset: 0x000009E8
		protected override void OnValidate(object value)
		{
		}
	}
}
