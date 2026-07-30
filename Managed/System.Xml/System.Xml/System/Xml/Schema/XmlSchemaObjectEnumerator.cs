using System;
using System.Collections;
using Unity;

namespace System.Xml.Schema
{
	/// <summary>Represents the enumerator for the <see cref="T:System.Xml.Schema.XmlSchemaObjectCollection" />.</summary>
	// Token: 0x0200046F RID: 1135
	public class XmlSchemaObjectEnumerator : IEnumerator
	{
		// Token: 0x06002CBB RID: 11451 RVA: 0x00107461 File Offset: 0x00105661
		internal XmlSchemaObjectEnumerator(IEnumerator enumerator)
		{
			this.enumerator = enumerator;
		}

		/// <summary>Resets the enumerator to the start of the collection.</summary>
		// Token: 0x06002CBC RID: 11452 RVA: 0x00107470 File Offset: 0x00105670
		public void Reset()
		{
			this.enumerator.Reset();
		}

		/// <summary>Moves to the next item in the collection.</summary>
		/// <returns>false at the end of the collection.</returns>
		// Token: 0x06002CBD RID: 11453 RVA: 0x0010747D File Offset: 0x0010567D
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>Gets the current <see cref="T:System.Xml.Schema.XmlSchemaObject" /> in the collection.</summary>
		/// <returns>The current <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</returns>
		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06002CBE RID: 11454 RVA: 0x0010748A File Offset: 0x0010568A
		public XmlSchemaObject Current
		{
			get
			{
				return (XmlSchemaObject)this.enumerator.Current;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaObjectEnumerator.Reset" />.</summary>
		// Token: 0x06002CBF RID: 11455 RVA: 0x00107470 File Offset: 0x00105670
		void IEnumerator.Reset()
		{
			this.enumerator.Reset();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaObjectEnumerator.MoveNext" />.</summary>
		/// <returns>The next <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</returns>
		// Token: 0x06002CC0 RID: 11456 RVA: 0x0010747D File Offset: 0x0010567D
		bool IEnumerator.MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.Schema.XmlSchemaObjectEnumerator.Current" />.</summary>
		/// <returns>The current <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</returns>
		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06002CC1 RID: 11457 RVA: 0x0010749C File Offset: 0x0010569C
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x000728B0 File Offset: 0x00070AB0
		internal XmlSchemaObjectEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001DDF RID: 7647
		private IEnumerator enumerator;
	}
}
