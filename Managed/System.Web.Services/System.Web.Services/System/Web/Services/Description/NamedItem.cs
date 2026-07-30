using System;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents a named, item that can be documented.</summary>
	// Token: 0x020000EA RID: 234
	public abstract class NamedItem : DocumentableItem
	{
		/// <summary>Gets or sets the name of the item.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the item.</returns>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001C3E2 File Offset: 0x0001A5E2
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x0001C3EA File Offset: 0x0001A5EA
		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x040003E8 RID: 1000
		private string name;
	}
}
