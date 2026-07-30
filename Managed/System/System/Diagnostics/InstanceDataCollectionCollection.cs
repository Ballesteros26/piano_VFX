using System;
using System.Collections;

namespace System.Diagnostics
{
	/// <summary>Provides a strongly typed collection of <see cref="T:System.Diagnostics.InstanceDataCollection" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000202 RID: 514
	public class InstanceDataCollectionCollection : DictionaryBase
	{
		// Token: 0x0600109B RID: 4251 RVA: 0x00049ECF File Offset: 0x000480CF
		private static void CheckNull(object value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.InstanceDataCollectionCollection" /> class.</summary>
		// Token: 0x0600109C RID: 4252 RVA: 0x00049F5D File Offset: 0x0004815D
		[Obsolete("Use PerformanceCounterCategory.ReadCategory()")]
		public InstanceDataCollectionCollection()
		{
		}

		/// <summary>Gets the instance data for the specified counter.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.InstanceDataCollection" /> item, by which the <see cref="T:System.Diagnostics.InstanceDataCollectionCollection" /> object is indexed.</returns>
		/// <param name="counterName">The name of the performance counter. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="counterName" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700035A RID: 858
		public InstanceDataCollection this[string counterName]
		{
			get
			{
				InstanceDataCollectionCollection.CheckNull(counterName, "counterName");
				return (InstanceDataCollection)base.Dictionary[counterName];
			}
		}

		/// <summary>Gets the object and counter registry keys for the objects associated with this instance data collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that represents a set of object-specific registry keys.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x0600109E RID: 4254 RVA: 0x00049F1B File Offset: 0x0004811B
		public ICollection Keys
		{
			get
			{
				return base.Dictionary.Keys;
			}
		}

		/// <summary>Gets the instance data values that comprise the collection of instances for the counter.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that represents the counter's instances and their associated data values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x00049F28 File Offset: 0x00048128
		public ICollection Values
		{
			get
			{
				return base.Dictionary.Values;
			}
		}

		/// <summary>Determines whether an instance data collection for the specified counter (identified by one of the indexed <see cref="T:System.Diagnostics.InstanceDataCollection" /> objects) exists in the collection.</summary>
		/// <returns>true if an instance data collection containing the specified counter exists in the collection; otherwise, false.</returns>
		/// <param name="counterName">The name of the performance counter. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="counterName" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010A0 RID: 4256 RVA: 0x00049F83 File Offset: 0x00048183
		public bool Contains(string counterName)
		{
			InstanceDataCollectionCollection.CheckNull(counterName, "counterName");
			return base.Dictionary.Contains(counterName);
		}

		/// <summary>Copies an array of <see cref="T:System.Diagnostics.InstanceDataCollection" /> instances to the collection, at the specified index.</summary>
		/// <param name="counters">An array of <see cref="T:System.Diagnostics.InstanceDataCollection" /> instances (identified by the counters they contain) to add to the collection. </param>
		/// <param name="index">The location at which to add the new instances. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010A1 RID: 4257 RVA: 0x00049F4E File Offset: 0x0004814E
		public void CopyTo(InstanceDataCollection[] counters, int index)
		{
			base.Dictionary.CopyTo(counters, index);
		}
	}
}
