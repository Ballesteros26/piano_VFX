using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Defines the tag prefix used in a Web page to identify custom controls. This class cannot be inherited.</summary>
	// Token: 0x0200022D RID: 557
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class TagPrefixAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.TagPrefixAttribute" /> class.</summary>
		/// <param name="namespaceName">A string that identifies the custom control namespace. </param>
		/// <param name="tagPrefix">A string that identifies the custom control prefix. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="namespaceName" /> or the <paramref name="tagPrefix" /> is null or an empty string ("").</exception>
		// Token: 0x060016ED RID: 5869 RVA: 0x0003D8E8 File Offset: 0x0003BAE8
		public TagPrefixAttribute(string namespaceName, string tagPrefix)
		{
			if (namespaceName == null || namespaceName.Length == 0)
			{
				throw new ArgumentNullException("namespaceName");
			}
			if (tagPrefix == null || tagPrefix.Length == 0)
			{
				throw new ArgumentNullException("tagPrefix");
			}
			this.namespaceName = namespaceName;
			this.tagPrefix = tagPrefix;
		}

		/// <summary>Gets the namespace prefix for the specified control.</summary>
		/// <returns>The namespace name.</returns>
		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x0003D935 File Offset: 0x0003BB35
		public string NamespaceName
		{
			get
			{
				return this.namespaceName;
			}
		}

		/// <summary>Gets the tag prefix for the specified control.</summary>
		/// <returns>The tag prefix.</returns>
		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x0003D93D File Offset: 0x0003BB3D
		public string TagPrefix
		{
			get
			{
				return this.tagPrefix;
			}
		}

		// Token: 0x04001586 RID: 5510
		private string namespaceName;

		// Token: 0x04001587 RID: 5511
		private string tagPrefix;
	}
}
