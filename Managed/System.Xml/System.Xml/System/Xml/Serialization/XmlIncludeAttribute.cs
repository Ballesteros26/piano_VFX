using System;

namespace System.Xml.Serialization
{
	/// <summary>Allows the <see cref="T:System.Xml.Serialization.XmlSerializer" /> to recognize a type when it serializes or deserializes an object.</summary>
	// Token: 0x02000334 RID: 820
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = true)]
	public class XmlIncludeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlIncludeAttribute" /> class.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to include. </param>
		// Token: 0x06001F60 RID: 8032 RVA: 0x000A9C82 File Offset: 0x000A7E82
		public XmlIncludeAttribute(Type type)
		{
			this.type = type;
		}

		/// <summary>Gets or sets the type of the object to include.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object to include.</returns>
		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001F61 RID: 8033 RVA: 0x000A9C91 File Offset: 0x000A7E91
		// (set) Token: 0x06001F62 RID: 8034 RVA: 0x000A9C99 File Offset: 0x000A7E99
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x0400173C RID: 5948
		private Type type;
	}
}
