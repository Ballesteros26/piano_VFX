using System;

namespace System.Xml.Serialization
{
	/// <summary>Contains fields that can be used to pass event delegates to a thread-safe <see cref="Overload:System.Xml.Serialization.XmlSerializer.Deserialize" /> method of the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</summary>
	// Token: 0x02000360 RID: 864
	public struct XmlDeserializationEvents
	{
		/// <summary>Gets or sets an object that represents the method that handles the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownNode" /> event.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlNodeEventHandler" /> that points to the event handler.</returns>
		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x000DA9E9 File Offset: 0x000D8BE9
		// (set) Token: 0x0600233E RID: 9022 RVA: 0x000DA9F1 File Offset: 0x000D8BF1
		public XmlNodeEventHandler OnUnknownNode
		{
			get
			{
				return this.onUnknownNode;
			}
			set
			{
				this.onUnknownNode = value;
			}
		}

		/// <summary>Gets or sets an object that represents the method that handles the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownAttribute" /> event.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlAttributeEventHandler" /> that points to the event handler.</returns>
		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x0600233F RID: 9023 RVA: 0x000DA9FA File Offset: 0x000D8BFA
		// (set) Token: 0x06002340 RID: 9024 RVA: 0x000DAA02 File Offset: 0x000D8C02
		public XmlAttributeEventHandler OnUnknownAttribute
		{
			get
			{
				return this.onUnknownAttribute;
			}
			set
			{
				this.onUnknownAttribute = value;
			}
		}

		/// <summary>Gets or sets an object that represents the method that handles the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownElement" /> event.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlElementEventHandler" /> that points to the event handler.</returns>
		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06002341 RID: 9025 RVA: 0x000DAA0B File Offset: 0x000D8C0B
		// (set) Token: 0x06002342 RID: 9026 RVA: 0x000DAA13 File Offset: 0x000D8C13
		public XmlElementEventHandler OnUnknownElement
		{
			get
			{
				return this.onUnknownElement;
			}
			set
			{
				this.onUnknownElement = value;
			}
		}

		/// <summary>Gets or sets an object that represents the method that handles the <see cref="E:System.Xml.Serialization.XmlSerializer.UnreferencedObject" /> event.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.UnreferencedObjectEventHandler" /> that points to the event handler.</returns>
		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06002343 RID: 9027 RVA: 0x000DAA1C File Offset: 0x000D8C1C
		// (set) Token: 0x06002344 RID: 9028 RVA: 0x000DAA24 File Offset: 0x000D8C24
		public UnreferencedObjectEventHandler OnUnreferencedObject
		{
			get
			{
				return this.onUnreferencedObject;
			}
			set
			{
				this.onUnreferencedObject = value;
			}
		}

		// Token: 0x04001857 RID: 6231
		private XmlNodeEventHandler onUnknownNode;

		// Token: 0x04001858 RID: 6232
		private XmlAttributeEventHandler onUnknownAttribute;

		// Token: 0x04001859 RID: 6233
		private XmlElementEventHandler onUnknownElement;

		// Token: 0x0400185A RID: 6234
		private UnreferencedObjectEventHandler onUnreferencedObject;

		// Token: 0x0400185B RID: 6235
		internal object sender;
	}
}
