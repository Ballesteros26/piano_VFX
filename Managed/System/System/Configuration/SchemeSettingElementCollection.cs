using System;
using Unity;

namespace System.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Configuration.SchemeSettingElement" /> objects.</summary>
	// Token: 0x020007D2 RID: 2002
	[ConfigurationCollection(typeof(SchemeSettingElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap, AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
	public sealed class SchemeSettingElementCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SchemeSettingElementCollection" /> class. </summary>
		// Token: 0x06004015 RID: 16405 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		public SchemeSettingElementCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets an item at the specified index in the <see cref="T:System.Configuration.SchemeSettingElementCollection" /> collection.</summary>
		/// <returns>The specified <see cref="T:System.Configuration.SchemeSettingElement" />.</returns>
		/// <param name="index">The index of the <see cref="T:System.Configuration.SchemeSettingElement" /> to return.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The <paramref name="index" /> parameter is less than zero.-or- The item specified by the parameter is null or has been removed.</exception>
		// Token: 0x17000F7C RID: 3964
		public SchemeSettingElement this[int index]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x06004017 RID: 16407 RVA: 0x0003D2D0 File Offset: 0x0003B4D0
		protected override ConfigurationElement CreateNewElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004018 RID: 16408 RVA: 0x0003D2D0 File Offset: 0x0003B4D0
		protected override object GetElementKey(ConfigurationElement element)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>The index of the specified <see cref="T:System.Configuration.SchemeSettingElement" />.</summary>
		/// <returns>The index of the specified <see cref="T:System.Configuration.SchemeSettingElement" />; otherwise, -1.</returns>
		/// <param name="element">The <see cref="T:System.Configuration.SchemeSettingElement" /> for the specified index location.</param>
		// Token: 0x06004019 RID: 16409 RVA: 0x000E0D64 File Offset: 0x000DEF64
		public int IndexOf(SchemeSettingElement element)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
