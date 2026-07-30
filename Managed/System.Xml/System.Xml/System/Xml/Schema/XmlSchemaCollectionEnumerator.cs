using System;
using System.Collections;
using Unity;

namespace System.Xml.Schema
{
	/// <summary>Supports a simple iteration over a collection. This class cannot be inherited. </summary>
	// Token: 0x02000441 RID: 1089
	public sealed class XmlSchemaCollectionEnumerator : IEnumerator
	{
		// Token: 0x06002B4D RID: 11085 RVA: 0x00105662 File Offset: 0x00103862
		internal XmlSchemaCollectionEnumerator(Hashtable collection)
		{
			this.enumerator = collection.GetEnumerator();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaCollectionEnumerator.System.Collections.IEnumerator.Reset" />.</summary>
		// Token: 0x06002B4E RID: 11086 RVA: 0x00105676 File Offset: 0x00103876
		void IEnumerator.Reset()
		{
			this.enumerator.Reset();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaCollectionEnumerator.MoveNext" />.</summary>
		/// <returns>Returns the next node.</returns>
		// Token: 0x06002B4F RID: 11087 RVA: 0x00105683 File Offset: 0x00103883
		bool IEnumerator.MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>Advances the enumerator to the next schema in the collection.</summary>
		/// <returns>true if the move was successful; false if the enumerator has passed the end of the collection.</returns>
		// Token: 0x06002B50 RID: 11088 RVA: 0x00105683 File Offset: 0x00103883
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.Schema.XmlSchemaCollectionEnumerator.Current" />.</summary>
		/// <returns>Returns the current node.</returns>
		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06002B51 RID: 11089 RVA: 0x00105690 File Offset: 0x00103890
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		/// <summary>Gets the current <see cref="T:System.Xml.Schema.XmlSchema" /> in the collection.</summary>
		/// <returns>The current XmlSchema in the collection.</returns>
		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06002B52 RID: 11090 RVA: 0x00105698 File Offset: 0x00103898
		public XmlSchema Current
		{
			get
			{
				XmlSchemaCollectionNode xmlSchemaCollectionNode = (XmlSchemaCollectionNode)this.enumerator.Value;
				if (xmlSchemaCollectionNode != null)
				{
					return xmlSchemaCollectionNode.Schema;
				}
				return null;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06002B53 RID: 11091 RVA: 0x001056C1 File Offset: 0x001038C1
		internal XmlSchemaCollectionNode CurrentNode
		{
			get
			{
				return (XmlSchemaCollectionNode)this.enumerator.Value;
			}
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x000728B0 File Offset: 0x00070AB0
		internal XmlSchemaCollectionEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001D4D RID: 7501
		private IDictionaryEnumerator enumerator;
	}
}
