using System;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Represents configuration settings that enable use of the <see cref="T:System.Web.Compilation.BuildProvider" /> class for specific folders. </summary>
	// Token: 0x020006A5 RID: 1701
	public sealed class FolderLevelBuildProvider : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> class by using an identifier and the fully qualified name. </summary>
		/// <param name="name">The identifier for the <see cref="T:System.Web.Compilation.BuildProvider" /> to use.</param>
		/// <param name="type">The fully qualified name of the <see cref="T:System.Web.Compilation.BuildProvider" /> to use.</param>
		// Token: 0x06004802 RID: 18434 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public FolderLevelBuildProvider(string name, string type)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object.</summary>
		/// <returns>The name of the build provider object.</returns>
		// Token: 0x17001646 RID: 5702
		// (get) Token: 0x06004803 RID: 18435 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004804 RID: 18436 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string Name
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

		/// <summary>Gets or set the fully qualified name of the build provider class.</summary>
		/// <returns>The fully qualified name of the build provider class.</returns>
		// Token: 0x17001647 RID: 5703
		// (get) Token: 0x06004805 RID: 18437 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004806 RID: 18438 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string Type
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
	}
}
