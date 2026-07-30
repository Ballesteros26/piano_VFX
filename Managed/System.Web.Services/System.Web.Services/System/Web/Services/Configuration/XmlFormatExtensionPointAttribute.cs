using System;

namespace System.Web.Services.Configuration
{
	/// <summary>Specifies the XML namespace and XML namespace prefix to use for a service description format extension within a service description. This class cannot be inherited.</summary>
	// Token: 0x0200014F RID: 335
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class XmlFormatExtensionPointAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.XmlFormatExtensionPointAttribute" /> class.</summary>
		/// <param name="memberName">The member of the class that implements the service description format extension that can have a service description format extension associated with it.</param>
		// Token: 0x06000A67 RID: 2663 RVA: 0x000456A6 File Offset: 0x000438A6
		public XmlFormatExtensionPointAttribute(string memberName)
		{
			this.name = memberName;
		}

		/// <summary>Specifies that the member of the class that implements the service description format extension can have a service description format extension associated with it.</summary>
		/// <returns>The member of the class that implements the service description format extension that can have a service description format extension associated with it.</returns>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x000456BC File Offset: 0x000438BC
		// (set) Token: 0x06000A69 RID: 2665 RVA: 0x000456D2 File Offset: 0x000438D2
		public string MemberName
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the member of the class that implements the service description format extension specified in the <see cref="P:System.Web.Services.Configuration.XmlFormatExtensionPointAttribute.MemberName" /> property can accept raw XML elements.</summary>
		/// <returns>true if the member of the class that implements the service description format extension specified in the <see cref="P:System.Web.Services.Configuration.XmlFormatExtensionPointAttribute.MemberName" /> property can accept raw XML elements; otherwise, false. The default is true.</returns>
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x000456DB File Offset: 0x000438DB
		// (set) Token: 0x06000A6B RID: 2667 RVA: 0x000456E3 File Offset: 0x000438E3
		public bool AllowElements
		{
			get
			{
				return this.allowElements;
			}
			set
			{
				this.allowElements = value;
			}
		}

		// Token: 0x040005D7 RID: 1495
		private string name;

		// Token: 0x040005D8 RID: 1496
		private bool allowElements = true;
	}
}
