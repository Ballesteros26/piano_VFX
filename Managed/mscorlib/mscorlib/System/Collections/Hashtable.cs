using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
	/// <summary>Represents a collection of key/value pairs that are organized based on the hash code of the key.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020009C3 RID: 2499
	[DebuggerTypeProxy(typeof(Hashtable.HashtableDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(true)]
	[Serializable]
	public class Hashtable : IDictionary, ICollection, IEnumerable, ISerializable, IDeserializationCallback, ICloneable
	{
		/// <summary>Gets or sets the object that can dispense hash codes.</summary>
		/// <returns>The object that can dispense hash codes.</returns>
		/// <exception cref="T:System.ArgumentException">The property is set to a value, but the hash table was created using an <see cref="T:System.Collections.IEqualityComparer" />. </exception>
		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x06005C78 RID: 23672 RVA: 0x001316CD File Offset: 0x0012F8CD
		// (set) Token: 0x06005C79 RID: 23673 RVA: 0x00131708 File Offset: 0x0012F908
		[Obsolete("Please use EqualityComparer property.")]
		protected IHashCodeProvider hcp
		{
			get
			{
				if (this._keycomparer is CompatibleComparer)
				{
					return ((CompatibleComparer)this._keycomparer).HashCodeProvider;
				}
				if (this._keycomparer == null)
				{
					return null;
				}
				throw new ArgumentException(Environment.GetResourceString("The usage of IKeyComparer and IHashCodeProvider/IComparer interfaces cannot be mixed; use one or the other."));
			}
			set
			{
				if (this._keycomparer is CompatibleComparer)
				{
					CompatibleComparer compatibleComparer = (CompatibleComparer)this._keycomparer;
					this._keycomparer = new CompatibleComparer(compatibleComparer.Comparer, value);
					return;
				}
				if (this._keycomparer == null)
				{
					this._keycomparer = new CompatibleComparer(null, value);
					return;
				}
				throw new ArgumentException(Environment.GetResourceString("The usage of IKeyComparer and IHashCodeProvider/IComparer interfaces cannot be mixed; use one or the other."));
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Collections.IComparer" /> to use for the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>The <see cref="T:System.Collections.IComparer" /> to use for the <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property is set to a value, but the hash table was created using an <see cref="T:System.Collections.IEqualityComparer" />. </exception>
		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x06005C7A RID: 23674 RVA: 0x00131766 File Offset: 0x0012F966
		// (set) Token: 0x06005C7B RID: 23675 RVA: 0x001317A0 File Offset: 0x0012F9A0
		[Obsolete("Please use KeyComparer properties.")]
		protected IComparer comparer
		{
			get
			{
				if (this._keycomparer is CompatibleComparer)
				{
					return ((CompatibleComparer)this._keycomparer).Comparer;
				}
				if (this._keycomparer == null)
				{
					return null;
				}
				throw new ArgumentException(Environment.GetResourceString("The usage of IKeyComparer and IHashCodeProvider/IComparer interfaces cannot be mixed; use one or the other."));
			}
			set
			{
				if (this._keycomparer is CompatibleComparer)
				{
					CompatibleComparer compatibleComparer = (CompatibleComparer)this._keycomparer;
					this._keycomparer = new CompatibleComparer(value, compatibleComparer.HashCodeProvider);
					return;
				}
				if (this._keycomparer == null)
				{
					this._keycomparer = new CompatibleComparer(value, null);
					return;
				}
				throw new ArgumentException(Environment.GetResourceString("The usage of IKeyComparer and IHashCodeProvider/IComparer interfaces cannot be mixed; use one or the other."));
			}
		}

		/// <summary>Gets the <see cref="T:System.Collections.IEqualityComparer" /> to use for the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>The <see cref="T:System.Collections.IEqualityComparer" /> to use for the <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property is set to a value, but the hash table was created using an <see cref="T:System.Collections.IHashCodeProvider" /> and an <see cref="T:System.Collections.IComparer" />. </exception>
		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x06005C7C RID: 23676 RVA: 0x001317FE File Offset: 0x0012F9FE
		protected IEqualityComparer EqualityComparer
		{
			get
			{
				return this._keycomparer;
			}
		}

		// Token: 0x06005C7D RID: 23677 RVA: 0x00002111 File Offset: 0x00000311
		internal Hashtable(bool trash)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the default initial capacity, load factor, hash code provider, and comparer.</summary>
		// Token: 0x06005C7E RID: 23678 RVA: 0x00131806 File Offset: 0x0012FA06
		public Hashtable()
			: this(0, 1f)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the specified initial capacity, and the default load factor, hash code provider, and comparer.</summary>
		/// <param name="capacity">The approximate number of elements that the <see cref="T:System.Collections.Hashtable" /> object can initially contain. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x06005C7F RID: 23679 RVA: 0x00131814 File Offset: 0x0012FA14
		public Hashtable(int capacity)
			: this(capacity, 1f)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the specified initial capacity and load factor, and the default hash code provider and comparer.</summary>
		/// <param name="capacity">The approximate number of elements that the <see cref="T:System.Collections.Hashtable" /> object can initially contain. </param>
		/// <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.-or- <paramref name="loadFactor" /> is less than 0.1.-or- <paramref name="loadFactor" /> is greater than 1.0. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="capacity" /> is causing an overflow.</exception>
		// Token: 0x06005C80 RID: 23680 RVA: 0x00131824 File Offset: 0x0012FA24
		public Hashtable(int capacity, float loadFactor)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("Non-negative number required."));
			}
			if (loadFactor < 0.1f || loadFactor > 1f)
			{
				throw new ArgumentOutOfRangeException("loadFactor", Environment.GetResourceString("Load factor needs to be between 0.1 and 1.0.", new object[] { 0.1, 1.0 }));
			}
			this.loadFactor = 0.72f * loadFactor;
			double num = (double)((float)capacity / this.loadFactor);
			if (num > 2147483647.0)
			{
				throw new ArgumentException(Environment.GetResourceString("Hashtable's capacity overflowed and went negative. Check load factor, capacity and the current size of the table."));
			}
			int num2 = ((num > 3.0) ? HashHelpers.GetPrime((int)num) : 3);
			this.buckets = new Hashtable.bucket[num2];
			this.loadsize = (int)(this.loadFactor * (float)num2);
			this.isWriterInProgress = false;
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the specified initial capacity, load factor, hash code provider, and comparer.</summary>
		/// <param name="capacity">The approximate number of elements that the <see cref="T:System.Collections.Hashtable" /> object can initially contain. </param>
		/// <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
		/// <param name="hcp">The <see cref="T:System.Collections.IHashCodeProvider" /> object that supplies the hash codes for all keys in the <see cref="T:System.Collections.Hashtable" />.-or- null to use the default hash code provider, which is each key's implementation of <see cref="M:System.Object.GetHashCode" />. </param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> object to use to determine whether two keys are equal.-or- null to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.-or- <paramref name="loadFactor" /> is less than 0.1.-or- <paramref name="loadFactor" /> is greater than 1.0. </exception>
		// Token: 0x06005C81 RID: 23681 RVA: 0x0013190D File Offset: 0x0012FB0D
		[Obsolete("Please use Hashtable(int, float, IEqualityComparer) instead.")]
		public Hashtable(int capacity, float loadFactor, IHashCodeProvider hcp, IComparer comparer)
			: this(capacity, loadFactor)
		{
			if (hcp == null && comparer == null)
			{
				this._keycomparer = null;
				return;
			}
			this._keycomparer = new CompatibleComparer(comparer, hcp);
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the specified initial capacity, load factor, and <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="capacity">The approximate number of elements that the <see cref="T:System.Collections.Hashtable" /> object can initially contain. </param>
		/// <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object that defines the hash code provider and the comparer to use with the <see cref="T:System.Collections.Hashtable" />.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="M:System.Object.GetHashCode" /> and the default comparer is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.-or- <paramref name="loadFactor" /> is less than 0.1.-or- <paramref name="loadFactor" /> is greater than 1.0. </exception>
		// Token: 0x06005C82 RID: 23682 RVA: 0x00131934 File Offset: 0x0012FB34
		public Hashtable(int capacity, float loadFactor, IEqualityComparer equalityComparer)
			: this(capacity, loadFactor)
		{
			this._keycomparer = equalityComparer;
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the default initial capacity and load factor, and the specified hash code provider and comparer.</summary>
		/// <param name="hcp">The <see cref="T:System.Collections.IHashCodeProvider" /> object that supplies the hash codes for all keys in the <see cref="T:System.Collections.Hashtable" /> object.-or- null to use the default hash code provider, which is each key's implementation of <see cref="M:System.Object.GetHashCode" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> object to use to determine whether two keys are equal.-or- null to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		// Token: 0x06005C83 RID: 23683 RVA: 0x00131945 File Offset: 0x0012FB45
		[Obsolete("Please use Hashtable(IEqualityComparer) instead.")]
		public Hashtable(IHashCodeProvider hcp, IComparer comparer)
			: this(0, 1f, hcp, comparer)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the default initial capacity and load factor, and the specified <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object that defines the hash code provider and the comparer to use with the <see cref="T:System.Collections.Hashtable" /> object.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="M:System.Object.GetHashCode" /> and the default comparer is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		// Token: 0x06005C84 RID: 23684 RVA: 0x00131955 File Offset: 0x0012FB55
		public Hashtable(IEqualityComparer equalityComparer)
			: this(0, 1f, equalityComparer)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the specified initial capacity, hash code provider, comparer, and the default load factor.</summary>
		/// <param name="capacity">The approximate number of elements that the <see cref="T:System.Collections.Hashtable" /> object can initially contain. </param>
		/// <param name="hcp">The <see cref="T:System.Collections.IHashCodeProvider" /> object that supplies the hash codes for all keys in the <see cref="T:System.Collections.Hashtable" />.-or- null to use the default hash code provider, which is each key's implementation of <see cref="M:System.Object.GetHashCode" />. </param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> object to use to determine whether two keys are equal.-or- null to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x06005C85 RID: 23685 RVA: 0x00131964 File Offset: 0x0012FB64
		[Obsolete("Please use Hashtable(int, IEqualityComparer) instead.")]
		public Hashtable(int capacity, IHashCodeProvider hcp, IComparer comparer)
			: this(capacity, 1f, hcp, comparer)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the specified initial capacity and <see cref="T:System.Collections.IEqualityComparer" />, and the default load factor.</summary>
		/// <param name="capacity">The approximate number of elements that the <see cref="T:System.Collections.Hashtable" /> object can initially contain. </param>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object that defines the hash code provider and the comparer to use with the <see cref="T:System.Collections.Hashtable" />.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="M:System.Object.GetHashCode" /> and the default comparer is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x06005C86 RID: 23686 RVA: 0x00131974 File Offset: 0x0012FB74
		public Hashtable(int capacity, IEqualityComparer equalityComparer)
			: this(capacity, 1f, equalityComparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Hashtable" /> class by copying the elements from the specified dictionary to the new <see cref="T:System.Collections.Hashtable" /> object. The new <see cref="T:System.Collections.Hashtable" /> object has an initial capacity equal to the number of elements copied, and uses the default load factor, hash code provider, and comparer.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> object to copy to a new <see cref="T:System.Collections.Hashtable" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		// Token: 0x06005C87 RID: 23687 RVA: 0x00131983 File Offset: 0x0012FB83
		public Hashtable(IDictionary d)
			: this(d, 1f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Hashtable" /> class by copying the elements from the specified dictionary to the new <see cref="T:System.Collections.Hashtable" /> object. The new <see cref="T:System.Collections.Hashtable" /> object has an initial capacity equal to the number of elements copied, and uses the specified load factor, and the default hash code provider and comparer.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> object to copy to a new <see cref="T:System.Collections.Hashtable" /> object.</param>
		/// <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="loadFactor" /> is less than 0.1.-or- <paramref name="loadFactor" /> is greater than 1.0. </exception>
		// Token: 0x06005C88 RID: 23688 RVA: 0x00131991 File Offset: 0x0012FB91
		public Hashtable(IDictionary d, float loadFactor)
			: this(d, loadFactor, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Hashtable" /> class by copying the elements from the specified dictionary to the new <see cref="T:System.Collections.Hashtable" /> object. The new <see cref="T:System.Collections.Hashtable" /> object has an initial capacity equal to the number of elements copied, and uses the default load factor, and the specified hash code provider and comparer. This API is obsolete. For an alternative, see <see cref="M:System.Collections.Hashtable.#ctor(System.Collections.IDictionary,System.Collections.IEqualityComparer)" />.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> object to copy to a new <see cref="T:System.Collections.Hashtable" /> object.</param>
		/// <param name="hcp">The <see cref="T:System.Collections.IHashCodeProvider" /> object that supplies the hash codes for all keys in the <see cref="T:System.Collections.Hashtable" />.-or- null to use the default hash code provider, which is each key's implementation of <see cref="M:System.Object.GetHashCode" />. </param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> object to use to determine whether two keys are equal.-or- null to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		// Token: 0x06005C89 RID: 23689 RVA: 0x0013199C File Offset: 0x0012FB9C
		[Obsolete("Please use Hashtable(IDictionary, IEqualityComparer) instead.")]
		public Hashtable(IDictionary d, IHashCodeProvider hcp, IComparer comparer)
			: this(d, 1f, hcp, comparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Hashtable" /> class by copying the elements from the specified dictionary to a new <see cref="T:System.Collections.Hashtable" /> object. The new <see cref="T:System.Collections.Hashtable" /> object has an initial capacity equal to the number of elements copied, and uses the default load factor and the specified <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> object to copy to a new <see cref="T:System.Collections.Hashtable" /> object.</param>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object that defines the hash code provider and the comparer to use with the <see cref="T:System.Collections.Hashtable" />.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="M:System.Object.GetHashCode" /> and the default comparer is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		// Token: 0x06005C8A RID: 23690 RVA: 0x001319AC File Offset: 0x0012FBAC
		public Hashtable(IDictionary d, IEqualityComparer equalityComparer)
			: this(d, 1f, equalityComparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Hashtable" /> class by copying the elements from the specified dictionary to the new <see cref="T:System.Collections.Hashtable" /> object. The new <see cref="T:System.Collections.Hashtable" /> object has an initial capacity equal to the number of elements copied, and uses the specified load factor, hash code provider, and comparer.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> object to copy to a new <see cref="T:System.Collections.Hashtable" /> object.</param>
		/// <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
		/// <param name="hcp">The <see cref="T:System.Collections.IHashCodeProvider" /> object that supplies the hash codes for all keys in the <see cref="T:System.Collections.Hashtable" />.-or- null to use the default hash code provider, which is each key's implementation of <see cref="M:System.Object.GetHashCode" />. </param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> object to use to determine whether two keys are equal.-or- null to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="loadFactor" /> is less than 0.1.-or- <paramref name="loadFactor" /> is greater than 1.0. </exception>
		// Token: 0x06005C8B RID: 23691 RVA: 0x001319BC File Offset: 0x0012FBBC
		[Obsolete("Please use Hashtable(IDictionary, float, IEqualityComparer) instead.")]
		public Hashtable(IDictionary d, float loadFactor, IHashCodeProvider hcp, IComparer comparer)
			: this((d != null) ? d.Count : 0, loadFactor, hcp, comparer)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", Environment.GetResourceString("Dictionary cannot be null."));
			}
			IDictionaryEnumerator enumerator = d.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.Add(enumerator.Key, enumerator.Value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Hashtable" /> class by copying the elements from the specified dictionary to the new <see cref="T:System.Collections.Hashtable" /> object. The new <see cref="T:System.Collections.Hashtable" /> object has an initial capacity equal to the number of elements copied, and uses the specified load factor and <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> object to copy to a new <see cref="T:System.Collections.Hashtable" /> object.</param>
		/// <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object that defines the hash code provider and the comparer to use with the <see cref="T:System.Collections.Hashtable" />.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="M:System.Object.GetHashCode" /> and the default comparer is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="loadFactor" /> is less than 0.1.-or- <paramref name="loadFactor" /> is greater than 1.0. </exception>
		// Token: 0x06005C8C RID: 23692 RVA: 0x00131A1C File Offset: 0x0012FC1C
		public Hashtable(IDictionary d, float loadFactor, IEqualityComparer equalityComparer)
			: this((d != null) ? d.Count : 0, loadFactor, equalityComparer)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", Environment.GetResourceString("Dictionary cannot be null."));
			}
			IDictionaryEnumerator enumerator = d.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.Add(enumerator.Key, enumerator.Value);
			}
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class that is serializable using the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Hashtable" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Hashtable" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06005C8D RID: 23693 RVA: 0x00131A78 File Offset: 0x0012FC78
		protected Hashtable(SerializationInfo info, StreamingContext context)
		{
			HashHelpers.SerializationInfoTable.Add(this, info);
		}

		// Token: 0x06005C8E RID: 23694 RVA: 0x00131A8C File Offset: 0x0012FC8C
		private uint InitHash(object key, int hashsize, out uint seed, out uint incr)
		{
			uint num = (uint)(this.GetHash(key) & int.MaxValue);
			seed = num;
			incr = 1U + seed * 101U % (uint)(hashsize - 1);
			return num;
		}

		/// <summary>Adds an element with the specified key and value into the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <param name="key">The key of the element to add. </param>
		/// <param name="value">The value of the element to add. The value can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Hashtable" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Hashtable" /> is read-only.-or- The <see cref="T:System.Collections.Hashtable" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005C8F RID: 23695 RVA: 0x00131AB9 File Offset: 0x0012FCB9
		public virtual void Add(object key, object value)
		{
			this.Insert(key, value, true);
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Hashtable" /> is read-only. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005C90 RID: 23696 RVA: 0x00131AC4 File Offset: 0x0012FCC4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public virtual void Clear()
		{
			if (this.count == 0 && this.occupancy == 0)
			{
				return;
			}
			Thread.BeginCriticalRegion();
			this.isWriterInProgress = true;
			for (int i = 0; i < this.buckets.Length; i++)
			{
				this.buckets[i].hash_coll = 0;
				this.buckets[i].key = null;
				this.buckets[i].val = null;
			}
			this.count = 0;
			this.occupancy = 0;
			this.UpdateVersion();
			this.isWriterInProgress = false;
			Thread.EndCriticalRegion();
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005C91 RID: 23697 RVA: 0x00131B5C File Offset: 0x0012FD5C
		public virtual object Clone()
		{
			Hashtable.bucket[] array = this.buckets;
			Hashtable hashtable = new Hashtable(this.count, this._keycomparer);
			hashtable.version = this.version;
			hashtable.loadFactor = this.loadFactor;
			hashtable.count = 0;
			int i = array.Length;
			while (i > 0)
			{
				i--;
				object key = array[i].key;
				if (key != null && key != array)
				{
					hashtable[key] = array[i].val;
				}
			}
			return hashtable;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Hashtable" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Hashtable" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Hashtable" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005C92 RID: 23698 RVA: 0x00131BDB File Offset: 0x0012FDDB
		public virtual bool Contains(object key)
		{
			return this.ContainsKey(key);
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Hashtable" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Hashtable" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Hashtable" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005C93 RID: 23699 RVA: 0x00131BE4 File Offset: 0x0012FDE4
		public virtual bool ContainsKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
			}
			Hashtable.bucket[] array = this.buckets;
			uint num2;
			uint num3;
			uint num = this.InitHash(key, array.Length, out num2, out num3);
			int num4 = 0;
			int num5 = (int)(num2 % (uint)array.Length);
			for (;;)
			{
				Hashtable.bucket bucket = array[num5];
				if (bucket.key == null)
				{
					break;
				}
				if ((long)(bucket.hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(bucket.key, key))
				{
					return true;
				}
				num5 = (int)(((long)num5 + (long)((ulong)num3)) % (long)((ulong)array.Length));
				if (bucket.hash_coll >= 0 || ++num4 >= array.Length)
				{
					return false;
				}
			}
			return false;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Hashtable" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Hashtable" /> contains an element with the specified <paramref name="value" />; otherwise, false.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Hashtable" />. The value can be null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005C94 RID: 23700 RVA: 0x00131C88 File Offset: 0x0012FE88
		public virtual bool ContainsValue(object value)
		{
			if (value == null)
			{
				int num = this.buckets.Length;
				while (--num >= 0)
				{
					if (this.buckets[num].key != null && this.buckets[num].key != this.buckets && this.buckets[num].val == null)
					{
						return true;
					}
				}
			}
			else
			{
				int num2 = this.buckets.Length;
				while (--num2 >= 0)
				{
					object val = this.buckets[num2].val;
					if (val != null && val.Equals(value))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005C95 RID: 23701 RVA: 0x00131D24 File Offset: 0x0012FF24
		private void CopyKeys(Array array, int arrayIndex)
		{
			Hashtable.bucket[] array2 = this.buckets;
			int num = array2.Length;
			while (--num >= 0)
			{
				object key = array2[num].key;
				if (key != null && key != this.buckets)
				{
					array.SetValue(key, arrayIndex++);
				}
			}
		}

		// Token: 0x06005C96 RID: 23702 RVA: 0x00131D6C File Offset: 0x0012FF6C
		private void CopyEntries(Array array, int arrayIndex)
		{
			Hashtable.bucket[] array2 = this.buckets;
			int num = array2.Length;
			while (--num >= 0)
			{
				object key = array2[num].key;
				if (key != null && key != this.buckets)
				{
					DictionaryEntry dictionaryEntry = new DictionaryEntry(key, array2[num].val);
					array.SetValue(dictionaryEntry, arrayIndex++);
				}
			}
		}

		/// <summary>Copies the <see cref="T:System.Collections.Hashtable" /> elements to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.Hashtable" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Hashtable" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Hashtable" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C97 RID: 23703 RVA: 0x00131DD0 File Offset: 0x0012FFD0
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", Environment.GetResourceString("Array cannot be null."));
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Only single dimensional arrays are supported for the requested action."));
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", Environment.GetResourceString("Non-negative number required."));
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException(Environment.GetResourceString("Destination array is not long enough to copy all the items in the collection. Check array index and length."));
			}
			this.CopyEntries(array, arrayIndex);
		}

		// Token: 0x06005C98 RID: 23704 RVA: 0x00131E50 File Offset: 0x00130050
		internal virtual KeyValuePairs[] ToKeyValuePairsArray()
		{
			KeyValuePairs[] array = new KeyValuePairs[this.count];
			int num = 0;
			Hashtable.bucket[] array2 = this.buckets;
			int num2 = array2.Length;
			while (--num2 >= 0)
			{
				object key = array2[num2].key;
				if (key != null && key != this.buckets)
				{
					array[num++] = new KeyValuePairs(key, array2[num2].val);
				}
			}
			return array;
		}

		// Token: 0x06005C99 RID: 23705 RVA: 0x00131EB8 File Offset: 0x001300B8
		private void CopyValues(Array array, int arrayIndex)
		{
			Hashtable.bucket[] array2 = this.buckets;
			int num = array2.Length;
			while (--num >= 0)
			{
				object key = array2[num].key;
				if (key != null && key != this.buckets)
				{
					array.SetValue(array2[num].val, arrayIndex++);
				}
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new element using the specified key.</returns>
		/// <param name="key">The key whose value to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.Hashtable" /> is read-only.-or- The property is set, <paramref name="key" /> does not exist in the collection, and the <see cref="T:System.Collections.Hashtable" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001034 RID: 4148
		public virtual object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
				}
				Hashtable.bucket[] array = this.buckets;
				uint num2;
				uint num3;
				uint num = this.InitHash(key, array.Length, out num2, out num3);
				int num4 = 0;
				int num5 = (int)(num2 % (uint)array.Length);
				Hashtable.bucket bucket;
				for (;;)
				{
					int num6 = 0;
					int num7;
					do
					{
						num7 = this.version;
						bucket = array[num5];
						if (++num6 % 8 == 0)
						{
							Thread.Sleep(1);
						}
					}
					while (this.isWriterInProgress || num7 != this.version);
					if (bucket.key == null)
					{
						break;
					}
					if ((long)(bucket.hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(bucket.key, key))
					{
						goto Block_7;
					}
					num5 = (int)(((long)num5 + (long)((ulong)num3)) % (long)((ulong)array.Length));
					if (bucket.hash_coll >= 0 || ++num4 >= array.Length)
					{
						goto IL_00D2;
					}
				}
				return null;
				Block_7:
				return bucket.val;
				IL_00D2:
				return null;
			}
			set
			{
				this.Insert(key, value, false);
			}
		}

		// Token: 0x06005C9C RID: 23708 RVA: 0x00131FF8 File Offset: 0x001301F8
		private void expand()
		{
			int num = HashHelpers.ExpandPrime(this.buckets.Length);
			this.rehash(num, false);
		}

		// Token: 0x06005C9D RID: 23709 RVA: 0x0013201B File Offset: 0x0013021B
		private void rehash()
		{
			this.rehash(this.buckets.Length, false);
		}

		// Token: 0x06005C9E RID: 23710 RVA: 0x0013202C File Offset: 0x0013022C
		private void UpdateVersion()
		{
			this.version++;
		}

		// Token: 0x06005C9F RID: 23711 RVA: 0x00132040 File Offset: 0x00130240
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private void rehash(int newsize, bool forceNewHashCode)
		{
			this.occupancy = 0;
			Hashtable.bucket[] array = new Hashtable.bucket[newsize];
			for (int i = 0; i < this.buckets.Length; i++)
			{
				Hashtable.bucket bucket = this.buckets[i];
				if (bucket.key != null && bucket.key != this.buckets)
				{
					int num = (forceNewHashCode ? this.GetHash(bucket.key) : bucket.hash_coll) & int.MaxValue;
					this.putEntry(array, bucket.key, bucket.val, num);
				}
			}
			Thread.BeginCriticalRegion();
			this.isWriterInProgress = true;
			this.buckets = array;
			this.loadsize = (int)(this.loadFactor * (float)newsize);
			this.UpdateVersion();
			this.isWriterInProgress = false;
			Thread.EndCriticalRegion();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06005CA0 RID: 23712 RVA: 0x001320FC File Offset: 0x001302FC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Hashtable.HashtableEnumerator(this, 3);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that iterates through the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005CA1 RID: 23713 RVA: 0x001320FC File Offset: 0x001302FC
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new Hashtable.HashtableEnumerator(this, 3);
		}

		/// <summary>Returns the hash code for the specified key.</summary>
		/// <returns>The hash code for <paramref name="key" />.</returns>
		/// <param name="key">The <see cref="T:System.Object" /> for which a hash code is to be returned. </param>
		/// <exception cref="T:System.NullReferenceException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x06005CA2 RID: 23714 RVA: 0x00132105 File Offset: 0x00130305
		protected virtual int GetHash(object key)
		{
			if (this._keycomparer != null)
			{
				return this._keycomparer.GetHashCode(key);
			}
			return key.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Hashtable" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Hashtable" /> is read-only; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x06005CA3 RID: 23715 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Hashtable" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Hashtable" /> has a fixed size; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x06005CA4 RID: 23716 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Hashtable" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.Hashtable" /> is synchronized (thread safe); otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x06005CA5 RID: 23717 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Compares a specific <see cref="T:System.Object" /> with a specific key in the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>true if <paramref name="item" /> and <paramref name="key" /> are equal; otherwise, false.</returns>
		/// <param name="item">The <see cref="T:System.Object" /> to compare with <paramref name="key" />. </param>
		/// <param name="key">The key in the <see cref="T:System.Collections.Hashtable" /> to compare with <paramref name="item" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="item" /> is null.-or- <paramref name="key" /> is null. </exception>
		// Token: 0x06005CA6 RID: 23718 RVA: 0x00132122 File Offset: 0x00130322
		protected virtual bool KeyEquals(object item, object key)
		{
			if (this.buckets == item)
			{
				return false;
			}
			if (item == key)
			{
				return true;
			}
			if (this._keycomparer != null)
			{
				return this._keycomparer.Equals(item, key);
			}
			return item != null && item.Equals(key);
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x06005CA7 RID: 23719 RVA: 0x00132157 File Offset: 0x00130357
		public virtual ICollection Keys
		{
			get
			{
				if (this.keys == null)
				{
					this.keys = new Hashtable.KeyCollection(this);
				}
				return this.keys;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x06005CA8 RID: 23720 RVA: 0x00132173 File Offset: 0x00130373
		public virtual ICollection Values
		{
			get
			{
				if (this.values == null)
				{
					this.values = new Hashtable.ValueCollection(this);
				}
				return this.values;
			}
		}

		// Token: 0x06005CA9 RID: 23721 RVA: 0x00132190 File Offset: 0x00130390
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private void Insert(object key, object nvalue, bool add)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
			}
			if (this.count >= this.loadsize)
			{
				this.expand();
			}
			else if (this.occupancy > this.loadsize && this.count > 100)
			{
				this.rehash();
			}
			uint num2;
			uint num3;
			uint num = this.InitHash(key, this.buckets.Length, out num2, out num3);
			int num4 = 0;
			int num5 = -1;
			int num6 = (int)(num2 % (uint)this.buckets.Length);
			for (;;)
			{
				if (num5 == -1 && this.buckets[num6].key == this.buckets && this.buckets[num6].hash_coll < 0)
				{
					num5 = num6;
				}
				if (this.buckets[num6].key == null || (this.buckets[num6].key == this.buckets && ((long)this.buckets[num6].hash_coll & (long)((ulong)(-2147483648))) == 0L))
				{
					break;
				}
				if ((long)(this.buckets[num6].hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(this.buckets[num6].key, key))
				{
					goto Block_12;
				}
				if (num5 == -1 && this.buckets[num6].hash_coll >= 0)
				{
					Hashtable.bucket[] array = this.buckets;
					int num7 = num6;
					array[num7].hash_coll = array[num7].hash_coll | int.MinValue;
					this.occupancy++;
				}
				num6 = (int)(((long)num6 + (long)((ulong)num3)) % (long)((ulong)this.buckets.Length));
				if (++num4 >= this.buckets.Length)
				{
					goto Block_16;
				}
			}
			if (num5 != -1)
			{
				num6 = num5;
			}
			Thread.BeginCriticalRegion();
			this.isWriterInProgress = true;
			this.buckets[num6].val = nvalue;
			this.buckets[num6].key = key;
			Hashtable.bucket[] array2 = this.buckets;
			int num8 = num6;
			array2[num8].hash_coll = array2[num8].hash_coll | (int)num;
			this.count++;
			this.UpdateVersion();
			this.isWriterInProgress = false;
			Thread.EndCriticalRegion();
			return;
			Block_12:
			if (add)
			{
				throw new ArgumentException(Environment.GetResourceString("Item has already been added. Key in dictionary: '{0}'  Key being added: '{1}'", new object[]
				{
					this.buckets[num6].key,
					key
				}));
			}
			Thread.BeginCriticalRegion();
			this.isWriterInProgress = true;
			this.buckets[num6].val = nvalue;
			this.UpdateVersion();
			this.isWriterInProgress = false;
			Thread.EndCriticalRegion();
			return;
			Block_16:
			if (num5 != -1)
			{
				Thread.BeginCriticalRegion();
				this.isWriterInProgress = true;
				this.buckets[num5].val = nvalue;
				this.buckets[num5].key = key;
				Hashtable.bucket[] array3 = this.buckets;
				int num9 = num5;
				array3[num9].hash_coll = array3[num9].hash_coll | (int)num;
				this.count++;
				this.UpdateVersion();
				this.isWriterInProgress = false;
				Thread.EndCriticalRegion();
				return;
			}
			throw new InvalidOperationException(Environment.GetResourceString("Hashtable insert failed. Load factor too high. The most common cause is multiple threads writing to the Hashtable simultaneously."));
		}

		// Token: 0x06005CAA RID: 23722 RVA: 0x00132498 File Offset: 0x00130698
		private void putEntry(Hashtable.bucket[] newBuckets, object key, object nvalue, int hashcode)
		{
			uint num = (uint)(1 + hashcode * 101 % (newBuckets.Length - 1));
			int num2 = hashcode % newBuckets.Length;
			while (newBuckets[num2].key != null && newBuckets[num2].key != this.buckets)
			{
				if (newBuckets[num2].hash_coll >= 0)
				{
					int num3 = num2;
					newBuckets[num3].hash_coll = newBuckets[num3].hash_coll | int.MinValue;
					this.occupancy++;
				}
				num2 = (int)(((long)num2 + (long)((ulong)num)) % (long)((ulong)newBuckets.Length));
			}
			newBuckets[num2].val = nvalue;
			newBuckets[num2].key = key;
			int num4 = num2;
			newBuckets[num4].hash_coll = newBuckets[num4].hash_coll | hashcode;
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <param name="key">The key of the element to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Hashtable" /> is read-only.-or- The <see cref="T:System.Collections.Hashtable" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005CAB RID: 23723 RVA: 0x0013254C File Offset: 0x0013074C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public virtual void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
			}
			uint num2;
			uint num3;
			uint num = this.InitHash(key, this.buckets.Length, out num2, out num3);
			int num4 = 0;
			int num5 = (int)(num2 % (uint)this.buckets.Length);
			for (;;)
			{
				Hashtable.bucket bucket = this.buckets[num5];
				if ((long)(bucket.hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(bucket.key, key))
				{
					break;
				}
				num5 = (int)(((long)num5 + (long)((ulong)num3)) % (long)((ulong)this.buckets.Length));
				if (bucket.hash_coll >= 0 || ++num4 >= this.buckets.Length)
				{
					return;
				}
			}
			Thread.BeginCriticalRegion();
			this.isWriterInProgress = true;
			Hashtable.bucket[] array = this.buckets;
			int num6 = num5;
			array[num6].hash_coll = array[num6].hash_coll & int.MinValue;
			if (this.buckets[num5].hash_coll != 0)
			{
				this.buckets[num5].key = this.buckets;
			}
			else
			{
				this.buckets[num5].key = null;
			}
			this.buckets[num5].val = null;
			this.count--;
			this.UpdateVersion();
			this.isWriterInProgress = false;
			Thread.EndCriticalRegion();
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x06005CAC RID: 23724 RVA: 0x00132699 File Offset: 0x00130899
		public virtual object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x06005CAD RID: 23725 RVA: 0x001326BB File Offset: 0x001308BB
		public virtual int Count
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>Returns a synchronized (thread-safe) wrapper for the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>A synchronized (thread-safe) wrapper for the <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <param name="table">The <see cref="T:System.Collections.Hashtable" /> to synchronize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="table" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005CAE RID: 23726 RVA: 0x001326C3 File Offset: 0x001308C3
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static Hashtable Synchronized(Hashtable table)
		{
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			return new Hashtable.SyncHashtable(table);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Hashtable" />. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Hashtable" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005CAF RID: 23727 RVA: 0x001326DC File Offset: 0x001308DC
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				int num = this.version;
				info.AddValue("LoadFactor", this.loadFactor);
				info.AddValue("Version", this.version);
				IEqualityComparer keycomparer = this._keycomparer;
				if (keycomparer == null)
				{
					info.AddValue("Comparer", null, typeof(IComparer));
					info.AddValue("HashCodeProvider", null, typeof(IHashCodeProvider));
				}
				else if (keycomparer is CompatibleComparer)
				{
					CompatibleComparer compatibleComparer = keycomparer as CompatibleComparer;
					info.AddValue("Comparer", compatibleComparer.Comparer, typeof(IComparer));
					info.AddValue("HashCodeProvider", compatibleComparer.HashCodeProvider, typeof(IHashCodeProvider));
				}
				else
				{
					info.AddValue("KeyComparer", keycomparer, typeof(IEqualityComparer));
				}
				info.AddValue("HashSize", this.buckets.Length);
				object[] array = new object[this.count];
				object[] array2 = new object[this.count];
				this.CopyKeys(array, 0);
				this.CopyValues(array2, 0);
				info.AddValue("Keys", array, typeof(object[]));
				info.AddValue("Values", array2, typeof(object[]));
				if (this.version != num)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
				}
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event. </param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Hashtable" /> is invalid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005CB0 RID: 23728 RVA: 0x00132880 File Offset: 0x00130A80
		public virtual void OnDeserialization(object sender)
		{
			if (this.buckets != null)
			{
				return;
			}
			SerializationInfo serializationInfo;
			HashHelpers.SerializationInfoTable.TryGetValue(this, out serializationInfo);
			if (serializationInfo == null)
			{
				throw new SerializationException(Environment.GetResourceString("OnDeserialization method was called while the object was not being deserialized."));
			}
			int num = 0;
			IComparer comparer = null;
			IHashCodeProvider hashCodeProvider = null;
			object[] array = null;
			object[] array2 = null;
			SerializationInfoEnumerator enumerator = serializationInfo.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				uint num2 = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num2 <= 1613443821U)
				{
					if (num2 != 891156946U)
					{
						if (num2 != 1228509323U)
						{
							if (num2 == 1613443821U)
							{
								if (name == "Keys")
								{
									array = (object[])serializationInfo.GetValue("Keys", typeof(object[]));
								}
							}
						}
						else if (name == "KeyComparer")
						{
							this._keycomparer = (IEqualityComparer)serializationInfo.GetValue("KeyComparer", typeof(IEqualityComparer));
						}
					}
					else if (name == "Comparer")
					{
						comparer = (IComparer)serializationInfo.GetValue("Comparer", typeof(IComparer));
					}
				}
				else if (num2 <= 2484309429U)
				{
					if (num2 != 2370642523U)
					{
						if (num2 == 2484309429U)
						{
							if (name == "HashCodeProvider")
							{
								hashCodeProvider = (IHashCodeProvider)serializationInfo.GetValue("HashCodeProvider", typeof(IHashCodeProvider));
							}
						}
					}
					else if (name == "Values")
					{
						array2 = (object[])serializationInfo.GetValue("Values", typeof(object[]));
					}
				}
				else if (num2 != 3356145248U)
				{
					if (num2 == 3483216242U)
					{
						if (name == "LoadFactor")
						{
							this.loadFactor = serializationInfo.GetSingle("LoadFactor");
						}
					}
				}
				else if (name == "HashSize")
				{
					num = serializationInfo.GetInt32("HashSize");
				}
			}
			this.loadsize = (int)(this.loadFactor * (float)num);
			if (this._keycomparer == null && (comparer != null || hashCodeProvider != null))
			{
				this._keycomparer = new CompatibleComparer(comparer, hashCodeProvider);
			}
			this.buckets = new Hashtable.bucket[num];
			if (array == null)
			{
				throw new SerializationException(Environment.GetResourceString("The Keys for this Hashtable are missing."));
			}
			if (array2 == null)
			{
				throw new SerializationException(Environment.GetResourceString("The values for this dictionary are missing."));
			}
			if (array.Length != array2.Length)
			{
				throw new SerializationException(Environment.GetResourceString("The keys and values arrays have different sizes."));
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					throw new SerializationException(Environment.GetResourceString("One of the serialized keys is null."));
				}
				this.Insert(array[i], array2[i], true);
			}
			this.version = serializationInfo.GetInt32("Version");
			HashHelpers.SerializationInfoTable.Remove(this);
		}

		// Token: 0x04002F3C RID: 12092
		internal const int HashPrime = 101;

		// Token: 0x04002F3D RID: 12093
		private const int InitialSize = 3;

		// Token: 0x04002F3E RID: 12094
		private const string LoadFactorName = "LoadFactor";

		// Token: 0x04002F3F RID: 12095
		private const string VersionName = "Version";

		// Token: 0x04002F40 RID: 12096
		private const string ComparerName = "Comparer";

		// Token: 0x04002F41 RID: 12097
		private const string HashCodeProviderName = "HashCodeProvider";

		// Token: 0x04002F42 RID: 12098
		private const string HashSizeName = "HashSize";

		// Token: 0x04002F43 RID: 12099
		private const string KeysName = "Keys";

		// Token: 0x04002F44 RID: 12100
		private const string ValuesName = "Values";

		// Token: 0x04002F45 RID: 12101
		private const string KeyComparerName = "KeyComparer";

		// Token: 0x04002F46 RID: 12102
		private Hashtable.bucket[] buckets;

		// Token: 0x04002F47 RID: 12103
		private int count;

		// Token: 0x04002F48 RID: 12104
		private int occupancy;

		// Token: 0x04002F49 RID: 12105
		private int loadsize;

		// Token: 0x04002F4A RID: 12106
		private float loadFactor;

		// Token: 0x04002F4B RID: 12107
		private volatile int version;

		// Token: 0x04002F4C RID: 12108
		private volatile bool isWriterInProgress;

		// Token: 0x04002F4D RID: 12109
		private ICollection keys;

		// Token: 0x04002F4E RID: 12110
		private ICollection values;

		// Token: 0x04002F4F RID: 12111
		private IEqualityComparer _keycomparer;

		// Token: 0x04002F50 RID: 12112
		private object _syncRoot;

		// Token: 0x020009C4 RID: 2500
		private struct bucket
		{
			// Token: 0x04002F51 RID: 12113
			public object key;

			// Token: 0x04002F52 RID: 12114
			public object val;

			// Token: 0x04002F53 RID: 12115
			public int hash_coll;
		}

		// Token: 0x020009C5 RID: 2501
		[Serializable]
		private class KeyCollection : ICollection, IEnumerable
		{
			// Token: 0x06005CB1 RID: 23729 RVA: 0x00132B7F File Offset: 0x00130D7F
			internal KeyCollection(Hashtable hashtable)
			{
				this._hashtable = hashtable;
			}

			// Token: 0x06005CB2 RID: 23730 RVA: 0x00132B90 File Offset: 0x00130D90
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Only single dimensional arrays are supported for the requested action."));
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", Environment.GetResourceString("Non-negative number required."));
				}
				if (array.Length - arrayIndex < this._hashtable.count)
				{
					throw new ArgumentException(Environment.GetResourceString("Destination array is not long enough to copy all the items in the collection. Check array index and length."));
				}
				this._hashtable.CopyKeys(array, arrayIndex);
			}

			// Token: 0x06005CB3 RID: 23731 RVA: 0x00132C0F File Offset: 0x00130E0F
			public virtual IEnumerator GetEnumerator()
			{
				return new Hashtable.HashtableEnumerator(this._hashtable, 1);
			}

			// Token: 0x1700103C RID: 4156
			// (get) Token: 0x06005CB4 RID: 23732 RVA: 0x00132C1D File Offset: 0x00130E1D
			public virtual bool IsSynchronized
			{
				get
				{
					return this._hashtable.IsSynchronized;
				}
			}

			// Token: 0x1700103D RID: 4157
			// (get) Token: 0x06005CB5 RID: 23733 RVA: 0x00132C2A File Offset: 0x00130E2A
			public virtual object SyncRoot
			{
				get
				{
					return this._hashtable.SyncRoot;
				}
			}

			// Token: 0x1700103E RID: 4158
			// (get) Token: 0x06005CB6 RID: 23734 RVA: 0x00132C37 File Offset: 0x00130E37
			public virtual int Count
			{
				get
				{
					return this._hashtable.count;
				}
			}

			// Token: 0x04002F54 RID: 12116
			private Hashtable _hashtable;
		}

		// Token: 0x020009C6 RID: 2502
		[Serializable]
		private class ValueCollection : ICollection, IEnumerable
		{
			// Token: 0x06005CB7 RID: 23735 RVA: 0x00132C44 File Offset: 0x00130E44
			internal ValueCollection(Hashtable hashtable)
			{
				this._hashtable = hashtable;
			}

			// Token: 0x06005CB8 RID: 23736 RVA: 0x00132C54 File Offset: 0x00130E54
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Only single dimensional arrays are supported for the requested action."));
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", Environment.GetResourceString("Non-negative number required."));
				}
				if (array.Length - arrayIndex < this._hashtable.count)
				{
					throw new ArgumentException(Environment.GetResourceString("Destination array is not long enough to copy all the items in the collection. Check array index and length."));
				}
				this._hashtable.CopyValues(array, arrayIndex);
			}

			// Token: 0x06005CB9 RID: 23737 RVA: 0x00132CD3 File Offset: 0x00130ED3
			public virtual IEnumerator GetEnumerator()
			{
				return new Hashtable.HashtableEnumerator(this._hashtable, 2);
			}

			// Token: 0x1700103F RID: 4159
			// (get) Token: 0x06005CBA RID: 23738 RVA: 0x00132CE1 File Offset: 0x00130EE1
			public virtual bool IsSynchronized
			{
				get
				{
					return this._hashtable.IsSynchronized;
				}
			}

			// Token: 0x17001040 RID: 4160
			// (get) Token: 0x06005CBB RID: 23739 RVA: 0x00132CEE File Offset: 0x00130EEE
			public virtual object SyncRoot
			{
				get
				{
					return this._hashtable.SyncRoot;
				}
			}

			// Token: 0x17001041 RID: 4161
			// (get) Token: 0x06005CBC RID: 23740 RVA: 0x00132CFB File Offset: 0x00130EFB
			public virtual int Count
			{
				get
				{
					return this._hashtable.count;
				}
			}

			// Token: 0x04002F55 RID: 12117
			private Hashtable _hashtable;
		}

		// Token: 0x020009C7 RID: 2503
		[Serializable]
		private class SyncHashtable : Hashtable, IEnumerable
		{
			// Token: 0x06005CBD RID: 23741 RVA: 0x00132D08 File Offset: 0x00130F08
			internal SyncHashtable(Hashtable table)
				: base(false)
			{
				this._table = table;
			}

			// Token: 0x06005CBE RID: 23742 RVA: 0x00132D18 File Offset: 0x00130F18
			internal SyncHashtable(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
				this._table = (Hashtable)info.GetValue("ParentTable", typeof(Hashtable));
				if (this._table == null)
				{
					throw new SerializationException(Environment.GetResourceString("Insufficient state to return the real object."));
				}
			}

			// Token: 0x06005CBF RID: 23743 RVA: 0x00132D68 File Offset: 0x00130F68
			[SecurityCritical]
			public override void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					info.AddValue("ParentTable", this._table, typeof(Hashtable));
				}
			}

			// Token: 0x17001042 RID: 4162
			// (get) Token: 0x06005CC0 RID: 23744 RVA: 0x00132DD0 File Offset: 0x00130FD0
			public override int Count
			{
				get
				{
					return this._table.Count;
				}
			}

			// Token: 0x17001043 RID: 4163
			// (get) Token: 0x06005CC1 RID: 23745 RVA: 0x00132DDD File Offset: 0x00130FDD
			public override bool IsReadOnly
			{
				get
				{
					return this._table.IsReadOnly;
				}
			}

			// Token: 0x17001044 RID: 4164
			// (get) Token: 0x06005CC2 RID: 23746 RVA: 0x00132DEA File Offset: 0x00130FEA
			public override bool IsFixedSize
			{
				get
				{
					return this._table.IsFixedSize;
				}
			}

			// Token: 0x17001045 RID: 4165
			// (get) Token: 0x06005CC3 RID: 23747 RVA: 0x00003B29 File Offset: 0x00001D29
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001046 RID: 4166
			public override object this[object key]
			{
				get
				{
					return this._table[key];
				}
				set
				{
					object syncRoot = this._table.SyncRoot;
					lock (syncRoot)
					{
						this._table[key] = value;
					}
				}
			}

			// Token: 0x17001047 RID: 4167
			// (get) Token: 0x06005CC6 RID: 23750 RVA: 0x00132E54 File Offset: 0x00131054
			public override object SyncRoot
			{
				get
				{
					return this._table.SyncRoot;
				}
			}

			// Token: 0x06005CC7 RID: 23751 RVA: 0x00132E64 File Offset: 0x00131064
			public override void Add(object key, object value)
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.Add(key, value);
				}
			}

			// Token: 0x06005CC8 RID: 23752 RVA: 0x00132EB0 File Offset: 0x001310B0
			public override void Clear()
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.Clear();
				}
			}

			// Token: 0x06005CC9 RID: 23753 RVA: 0x00132EFC File Offset: 0x001310FC
			public override bool Contains(object key)
			{
				return this._table.Contains(key);
			}

			// Token: 0x06005CCA RID: 23754 RVA: 0x00132F0A File Offset: 0x0013110A
			public override bool ContainsKey(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
				}
				return this._table.ContainsKey(key);
			}

			// Token: 0x06005CCB RID: 23755 RVA: 0x00132F30 File Offset: 0x00131130
			public override bool ContainsValue(object key)
			{
				object syncRoot = this._table.SyncRoot;
				bool flag2;
				lock (syncRoot)
				{
					flag2 = this._table.ContainsValue(key);
				}
				return flag2;
			}

			// Token: 0x06005CCC RID: 23756 RVA: 0x00132F80 File Offset: 0x00131180
			public override void CopyTo(Array array, int arrayIndex)
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x06005CCD RID: 23757 RVA: 0x00132FCC File Offset: 0x001311CC
			public override object Clone()
			{
				object syncRoot = this._table.SyncRoot;
				object obj;
				lock (syncRoot)
				{
					obj = Hashtable.Synchronized((Hashtable)this._table.Clone());
				}
				return obj;
			}

			// Token: 0x06005CCE RID: 23758 RVA: 0x00133024 File Offset: 0x00131224
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._table.GetEnumerator();
			}

			// Token: 0x06005CCF RID: 23759 RVA: 0x00133024 File Offset: 0x00131224
			public override IDictionaryEnumerator GetEnumerator()
			{
				return this._table.GetEnumerator();
			}

			// Token: 0x17001048 RID: 4168
			// (get) Token: 0x06005CD0 RID: 23760 RVA: 0x00133034 File Offset: 0x00131234
			public override ICollection Keys
			{
				get
				{
					object syncRoot = this._table.SyncRoot;
					ICollection keys;
					lock (syncRoot)
					{
						keys = this._table.Keys;
					}
					return keys;
				}
			}

			// Token: 0x17001049 RID: 4169
			// (get) Token: 0x06005CD1 RID: 23761 RVA: 0x00133080 File Offset: 0x00131280
			public override ICollection Values
			{
				get
				{
					object syncRoot = this._table.SyncRoot;
					ICollection values;
					lock (syncRoot)
					{
						values = this._table.Values;
					}
					return values;
				}
			}

			// Token: 0x06005CD2 RID: 23762 RVA: 0x001330CC File Offset: 0x001312CC
			public override void Remove(object key)
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.Remove(key);
				}
			}

			// Token: 0x06005CD3 RID: 23763 RVA: 0x00002194 File Offset: 0x00000394
			public override void OnDeserialization(object sender)
			{
			}

			// Token: 0x06005CD4 RID: 23764 RVA: 0x00133118 File Offset: 0x00131318
			internal override KeyValuePairs[] ToKeyValuePairsArray()
			{
				return this._table.ToKeyValuePairsArray();
			}

			// Token: 0x04002F56 RID: 12118
			protected Hashtable _table;
		}

		// Token: 0x020009C8 RID: 2504
		[Serializable]
		private class HashtableEnumerator : IDictionaryEnumerator, IEnumerator, ICloneable
		{
			// Token: 0x06005CD5 RID: 23765 RVA: 0x00133125 File Offset: 0x00131325
			internal HashtableEnumerator(Hashtable hashtable, int getObjRetType)
			{
				this.hashtable = hashtable;
				this.bucket = hashtable.buckets.Length;
				this.version = hashtable.version;
				this.current = false;
				this.getObjectRetType = getObjRetType;
			}

			// Token: 0x06005CD6 RID: 23766 RVA: 0x0002C3A3 File Offset: 0x0002A5A3
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x1700104A RID: 4170
			// (get) Token: 0x06005CD7 RID: 23767 RVA: 0x0013315E File Offset: 0x0013135E
			public virtual object Key
			{
				get
				{
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
					}
					return this.currentKey;
				}
			}

			// Token: 0x06005CD8 RID: 23768 RVA: 0x00133180 File Offset: 0x00131380
			public virtual bool MoveNext()
			{
				if (this.version != this.hashtable.version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
				}
				while (this.bucket > 0)
				{
					this.bucket--;
					object key = this.hashtable.buckets[this.bucket].key;
					if (key != null && key != this.hashtable.buckets)
					{
						this.currentKey = key;
						this.currentValue = this.hashtable.buckets[this.bucket].val;
						this.current = true;
						return true;
					}
				}
				this.current = false;
				return false;
			}

			// Token: 0x1700104B RID: 4171
			// (get) Token: 0x06005CD9 RID: 23769 RVA: 0x0013322F File Offset: 0x0013142F
			public virtual DictionaryEntry Entry
			{
				get
				{
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
					}
					return new DictionaryEntry(this.currentKey, this.currentValue);
				}
			}

			// Token: 0x1700104C RID: 4172
			// (get) Token: 0x06005CDA RID: 23770 RVA: 0x0013325C File Offset: 0x0013145C
			public virtual object Current
			{
				get
				{
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
					}
					if (this.getObjectRetType == 1)
					{
						return this.currentKey;
					}
					if (this.getObjectRetType == 2)
					{
						return this.currentValue;
					}
					return new DictionaryEntry(this.currentKey, this.currentValue);
				}
			}

			// Token: 0x1700104D RID: 4173
			// (get) Token: 0x06005CDB RID: 23771 RVA: 0x001332B7 File Offset: 0x001314B7
			public virtual object Value
			{
				get
				{
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
					}
					return this.currentValue;
				}
			}

			// Token: 0x06005CDC RID: 23772 RVA: 0x001332D8 File Offset: 0x001314D8
			public virtual void Reset()
			{
				if (this.version != this.hashtable.version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
				}
				this.current = false;
				this.bucket = this.hashtable.buckets.Length;
				this.currentKey = null;
				this.currentValue = null;
			}

			// Token: 0x04002F57 RID: 12119
			private Hashtable hashtable;

			// Token: 0x04002F58 RID: 12120
			private int bucket;

			// Token: 0x04002F59 RID: 12121
			private int version;

			// Token: 0x04002F5A RID: 12122
			private bool current;

			// Token: 0x04002F5B RID: 12123
			private int getObjectRetType;

			// Token: 0x04002F5C RID: 12124
			private object currentKey;

			// Token: 0x04002F5D RID: 12125
			private object currentValue;

			// Token: 0x04002F5E RID: 12126
			internal const int Keys = 1;

			// Token: 0x04002F5F RID: 12127
			internal const int Values = 2;

			// Token: 0x04002F60 RID: 12128
			internal const int DictEntry = 3;
		}

		// Token: 0x020009C9 RID: 2505
		internal class HashtableDebugView
		{
			// Token: 0x06005CDD RID: 23773 RVA: 0x00133332 File Offset: 0x00131532
			public HashtableDebugView(Hashtable hashtable)
			{
				if (hashtable == null)
				{
					throw new ArgumentNullException("hashtable");
				}
				this.hashtable = hashtable;
			}

			// Token: 0x1700104E RID: 4174
			// (get) Token: 0x06005CDE RID: 23774 RVA: 0x0013334F File Offset: 0x0013154F
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public KeyValuePairs[] Items
			{
				get
				{
					return this.hashtable.ToKeyValuePairsArray();
				}
			}

			// Token: 0x04002F61 RID: 12129
			private Hashtable hashtable;
		}
	}
}
