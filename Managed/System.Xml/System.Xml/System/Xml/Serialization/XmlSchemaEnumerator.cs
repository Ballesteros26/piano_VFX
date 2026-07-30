using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	/// <summary>Enables iteration over a collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects. </summary>
	// Token: 0x02000347 RID: 839
	public class XmlSchemaEnumerator : IEnumerator<XmlSchema>, IDisposable, IEnumerator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSchemaEnumerator" /> class. </summary>
		/// <param name="list">The <see cref="T:System.Xml.Serialization.XmlSchemas" /> object you want to iterate over.</param>
		// Token: 0x060020A8 RID: 8360 RVA: 0x000B67AB File Offset: 0x000B49AB
		public XmlSchemaEnumerator(XmlSchemas list)
		{
			this.list = list;
			this.idx = -1;
			this.end = list.Count - 1;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Xml.Serialization.XmlSchemaEnumerator" />.</summary>
		// Token: 0x060020A9 RID: 8361 RVA: 0x00002F50 File Offset: 0x00001150
		public void Dispose()
		{
		}

		/// <summary>Advances the enumerator to the next item in the collection.</summary>
		/// <returns>true if the move is successful; otherwise, false.</returns>
		// Token: 0x060020AA RID: 8362 RVA: 0x000B67CF File Offset: 0x000B49CF
		public bool MoveNext()
		{
			if (this.idx >= this.end)
			{
				return false;
			}
			this.idx++;
			return true;
		}

		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current <see cref="T:System.Xml.Schema.XmlSchema" /> object in the collection.</returns>
		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060020AB RID: 8363 RVA: 0x000B67F0 File Offset: 0x000B49F0
		public XmlSchema Current
		{
			get
			{
				return this.list[this.idx];
			}
		}

		/// <summary>Gets the current element in the collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects.</summary>
		/// <returns>The current element in the collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects.</returns>
		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060020AC RID: 8364 RVA: 0x000B67F0 File Offset: 0x000B49F0
		object IEnumerator.Current
		{
			get
			{
				return this.list[this.idx];
			}
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects.</summary>
		// Token: 0x060020AD RID: 8365 RVA: 0x000B6803 File Offset: 0x000B4A03
		void IEnumerator.Reset()
		{
			this.idx = -1;
		}

		// Token: 0x0400178A RID: 6026
		private XmlSchemas list;

		// Token: 0x0400178B RID: 6027
		private int idx;

		// Token: 0x0400178C RID: 6028
		private int end;
	}
}
