using System;
using System.Collections;
using System.Runtime.InteropServices;
using Unity;

namespace System.Runtime.Serialization
{
	/// <summary>Provides a formatter-friendly mechanism for parsing the data in <see cref="T:System.Runtime.Serialization.SerializationInfo" />. This class cannot be inherited.</summary>
	// Token: 0x020006F1 RID: 1777
	[ComVisible(true)]
	public sealed class SerializationInfoEnumerator : IEnumerator
	{
		// Token: 0x06004AF7 RID: 19191 RVA: 0x0010C161 File Offset: 0x0010A361
		internal SerializationInfoEnumerator(string[] members, object[] info, Type[] types, int numItems)
		{
			this.m_members = members;
			this.m_data = info;
			this.m_types = types;
			this.m_numItems = numItems - 1;
			this.m_currItem = -1;
			this.m_current = false;
		}

		/// <summary>Updates the enumerator to the next item.</summary>
		/// <returns>true if a new element is found; otherwise, false.</returns>
		// Token: 0x06004AF8 RID: 19192 RVA: 0x0010C196 File Offset: 0x0010A396
		public bool MoveNext()
		{
			if (this.m_currItem < this.m_numItems)
			{
				this.m_currItem++;
				this.m_current = true;
			}
			else
			{
				this.m_current = false;
			}
			return this.m_current;
		}

		/// <summary>Gets the current item in the collection.</summary>
		/// <returns>A <see cref="T:System.Runtime.Serialization.SerializationEntry" /> that contains the current serialization data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumeration has not started or has already ended. </exception>
		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06004AF9 RID: 19193 RVA: 0x0010C1CC File Offset: 0x0010A3CC
		object IEnumerator.Current
		{
			get
			{
				if (!this.m_current)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
				}
				return new SerializationEntry(this.m_members[this.m_currItem], this.m_data[this.m_currItem], this.m_types[this.m_currItem]);
			}
		}

		/// <summary>Gets the item currently being examined.</summary>
		/// <returns>The item currently being examined.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator has not started enumerating items or has reached the end of the enumeration. </exception>
		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06004AFA RID: 19194 RVA: 0x0010C224 File Offset: 0x0010A424
		public SerializationEntry Current
		{
			get
			{
				if (!this.m_current)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
				}
				return new SerializationEntry(this.m_members[this.m_currItem], this.m_data[this.m_currItem], this.m_types[this.m_currItem]);
			}
		}

		/// <summary>Resets the enumerator to the first item.</summary>
		// Token: 0x06004AFB RID: 19195 RVA: 0x0010C275 File Offset: 0x0010A475
		public void Reset()
		{
			this.m_currItem = -1;
			this.m_current = false;
		}

		/// <summary>Gets the name for the item currently being examined.</summary>
		/// <returns>The item name.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator has not started enumerating items or has reached the end of the enumeration. </exception>
		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06004AFC RID: 19196 RVA: 0x0010C285 File Offset: 0x0010A485
		public string Name
		{
			get
			{
				if (!this.m_current)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
				}
				return this.m_members[this.m_currItem];
			}
		}

		/// <summary>Gets the value of the item currently being examined.</summary>
		/// <returns>The value of the item currently being examined.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator has not started enumerating items or has reached the end of the enumeration. </exception>
		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06004AFD RID: 19197 RVA: 0x0010C2AC File Offset: 0x0010A4AC
		public object Value
		{
			get
			{
				if (!this.m_current)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
				}
				return this.m_data[this.m_currItem];
			}
		}

		/// <summary>Gets the type of the item currently being examined.</summary>
		/// <returns>The type of the item currently being examined.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator has not started enumerating items or has reached the end of the enumeration. </exception>
		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06004AFE RID: 19198 RVA: 0x0010C2D3 File Offset: 0x0010A4D3
		public Type ObjectType
		{
			get
			{
				if (!this.m_current)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
				}
				return this.m_types[this.m_currItem];
			}
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal SerializationInfoEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400271A RID: 10010
		private string[] m_members;

		// Token: 0x0400271B RID: 10011
		private object[] m_data;

		// Token: 0x0400271C RID: 10012
		private Type[] m_types;

		// Token: 0x0400271D RID: 10013
		private int m_numItems;

		// Token: 0x0400271E RID: 10014
		private int m_currItem;

		// Token: 0x0400271F RID: 10015
		private bool m_current;
	}
}
