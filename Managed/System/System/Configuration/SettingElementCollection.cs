using System;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.SettingElement" /> objects. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000189 RID: 393
	public sealed class SettingElementCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Configuration.SettingElement" /> object to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Configuration.SettingElement" /> object to add to the collection.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000BC2 RID: 3010 RVA: 0x0003C157 File Offset: 0x0003A357
		public void Add(SettingElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all <see cref="T:System.Configuration.SettingElement" /> objects from the collection.</summary>
		// Token: 0x06000BC3 RID: 3011 RVA: 0x0003C160 File Offset: 0x0003A360
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Gets a <see cref="T:System.Configuration.SettingElement" /> object from the collection. </summary>
		/// <returns>A <see cref="T:System.Configuration.SettingElement" /> object.</returns>
		/// <param name="elementKey">A string value representing the <see cref="T:System.Configuration.SettingElement" /> object in the collection.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BC4 RID: 3012 RVA: 0x0003C168 File Offset: 0x0003A368
		public SettingElement Get(string elementKey)
		{
			foreach (object obj in this)
			{
				SettingElement settingElement = (SettingElement)obj;
				if (settingElement.Name == elementKey)
				{
					return settingElement;
				}
			}
			return null;
		}

		/// <summary>Removes a <see cref="T:System.Configuration.SettingElement" /> object from the collection.</summary>
		/// <param name="element">A <see cref="T:System.Configuration.SettingElement" /> object.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000BC5 RID: 3013 RVA: 0x0003C1CC File Offset: 0x0003A3CC
		public void Remove(SettingElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(element.Name);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0003C1E8 File Offset: 0x0003A3E8
		protected override ConfigurationElement CreateNewElement()
		{
			return new SettingElement();
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0003C1EF File Offset: 0x0003A3EF
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SettingElement)element).Name;
		}

		/// <summary>Gets the type of the configuration collection.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> object of the collection.</returns>
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x00004240 File Offset: 0x00002440
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0003C1FC File Offset: 0x0003A3FC
		protected override string ElementName
		{
			get
			{
				return "setting";
			}
		}
	}
}
