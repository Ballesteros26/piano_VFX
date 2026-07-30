using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Provides access to manifest resources, which are XML files that describe application dependencies.  </summary>
	// Token: 0x020002E5 RID: 741
	[ComVisible(true)]
	public class ManifestResourceInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ManifestResourceInfo" /> class for a resource that is contained by the specified assembly and file, and that has the specified location.</summary>
		/// <param name="containingAssembly">The assembly that contains the manifest resource.</param>
		/// <param name="containingFileName">The name of the file that contains the manifest resource, if the file is not the same as the manifest file.</param>
		/// <param name="resourceLocation">A bitwise combination of enumeration values that provides information about the location of the manifest resource. </param>
		// Token: 0x0600206B RID: 8299 RVA: 0x0007E067 File Offset: 0x0007C267
		public ManifestResourceInfo(Assembly containingAssembly, string containingFileName, ResourceLocation resourceLocation)
		{
			this._containingAssembly = containingAssembly;
			this._containingFileName = containingFileName;
			this._resourceLocation = resourceLocation;
		}

		/// <summary>Gets the containing assembly for the manifest resource. </summary>
		/// <returns>The manifest resource's containing assembly.</returns>
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x0007E084 File Offset: 0x0007C284
		public virtual Assembly ReferencedAssembly
		{
			get
			{
				return this._containingAssembly;
			}
		}

		/// <summary>Gets the name of the file that contains the manifest resource, if it is not the same as the manifest file.  </summary>
		/// <returns>The manifest resource's file name.</returns>
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x0600206D RID: 8301 RVA: 0x0007E08C File Offset: 0x0007C28C
		public virtual string FileName
		{
			get
			{
				return this._containingFileName;
			}
		}

		/// <summary>Gets the manifest resource's location. </summary>
		/// <returns>A bitwise combination of <see cref="T:System.Reflection.ResourceLocation" /> flags that indicates the location of the manifest resource. </returns>
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x0007E094 File Offset: 0x0007C294
		public virtual ResourceLocation ResourceLocation
		{
			get
			{
				return this._resourceLocation;
			}
		}

		// Token: 0x040011C8 RID: 4552
		private Assembly _containingAssembly;

		// Token: 0x040011C9 RID: 4553
		private string _containingFileName;

		// Token: 0x040011CA RID: 4554
		private ResourceLocation _resourceLocation;
	}
}
