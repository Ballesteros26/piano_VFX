using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>An abstract class for <see cref="T:System.Xml.Schema.XmlSchemaAll" />, <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" />.</summary>
	// Token: 0x02000462 RID: 1122
	public abstract class XmlSchemaGroupBase : XmlSchemaParticle
	{
		/// <summary>This collection is used to add new elements to the compositor.</summary>
		/// <returns>An XmlSchemaObjectCollection.</returns>
		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06002C4A RID: 11338
		[XmlIgnore]
		public abstract XmlSchemaObjectCollection Items { get; }

		// Token: 0x06002C4B RID: 11339
		internal abstract void SetItems(XmlSchemaObjectCollection newItems);
	}
}
