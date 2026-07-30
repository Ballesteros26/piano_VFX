using System;

namespace System.Diagnostics
{
	/// <summary>Holds instance data associated with a performance counter sample.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000200 RID: 512
	public class InstanceData
	{
		/// <summary>Initializes a new instance of the InstanceData class, using the specified sample and performance counter instance.</summary>
		/// <param name="instanceName">The name of an instance associated with the performance counter. </param>
		/// <param name="sample">A <see cref="T:System.Diagnostics.CounterSample" /> taken from the instance specified by the <paramref name="instanceName" /> parameter. </param>
		// Token: 0x0600108F RID: 4239 RVA: 0x00049E9C File Offset: 0x0004809C
		public InstanceData(string instanceName, CounterSample sample)
		{
			this.instanceName = instanceName;
			this.sample = sample;
		}

		/// <summary>Gets the instance name associated with this instance data.</summary>
		/// <returns>The name of an instance associated with the performance counter.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x00049EB2 File Offset: 0x000480B2
		public string InstanceName
		{
			get
			{
				return this.instanceName;
			}
		}

		/// <summary>Gets the raw data value associated with the performance counter sample.</summary>
		/// <returns>The raw value read by the performance counter sample associated with the <see cref="P:System.Diagnostics.InstanceData.Sample" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x00049EBA File Offset: 0x000480BA
		public long RawValue
		{
			get
			{
				return this.sample.RawValue;
			}
		}

		/// <summary>Gets the performance counter sample that generated this data.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.CounterSample" /> taken from the instance specified by the <see cref="P:System.Diagnostics.InstanceData.InstanceName" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x00049EC7 File Offset: 0x000480C7
		public CounterSample Sample
		{
			get
			{
				return this.sample;
			}
		}

		// Token: 0x0400117A RID: 4474
		private string instanceName;

		// Token: 0x0400117B RID: 4475
		private CounterSample sample;
	}
}
