using System;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> objects. </summary>
	// Token: 0x020006A4 RID: 1700
	[ConfigurationCollection(typeof(FolderLevelBuildProvider))]
	public sealed class FolderLevelBuildProviderCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.FolderLevelBuildProviderCollection" /> class.</summary>
		// Token: 0x060047F9 RID: 18425 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public FolderLevelBuildProviderCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object at the specified index in the <see cref="T:System.Web.Configuration.FolderLevelBuildProviderCollection" /> object.</summary>
		/// <returns>The folder-level build provider object that is located at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object to get.</param>
		// Token: 0x17001645 RID: 5701
		public FolderLevelBuildProvider this[int index]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Adds a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object to the <see cref="T:System.Web.Configuration.FolderLevelBuildProviderCollection" /> object.</summary>
		/// <param name="buildProvider">The <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object to add.</param>
		// Token: 0x060047FC RID: 18428 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Add(FolderLevelBuildProvider buildProvider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Clears all <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> objects from the <see cref="T:System.Web.Configuration.FolderLevelBuildProviderCollection" /> object.</summary>
		// Token: 0x060047FD RID: 18429 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Clear()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x060047FE RID: 18430 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override ConfigurationElement CreateNewElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060047FF RID: 18431 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override object GetElementKey(ConfigurationElement element)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object from the <see cref="T:System.Web.Configuration.FolderLevelBuildProviderCollection" /> object.</summary>
		/// <param name="name">The <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object to remove.</param>
		// Token: 0x06004800 RID: 18432 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void Remove(string name)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object at the specified index in a <see cref="T:System.Web.Configuration.FolderLevelBuildProviderCollection" /> object.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object to remove. </param>
		// Token: 0x06004801 RID: 18433 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void RemoveAt(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
