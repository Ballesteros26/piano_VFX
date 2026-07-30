using System;
using System.Collections;

namespace System.Web.Compilation
{
	/// <summary>Represents dependencies returned by the build manager.</summary>
	// Token: 0x02000638 RID: 1592
	public sealed class BuildDependencySet
	{
		// Token: 0x0600445F RID: 17503 RVA: 0x00002050 File Offset: 0x00000250
		internal BuildDependencySet()
		{
		}

		/// <summary>Gets a string representing the hash code of the dependent virtual paths.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the hash code of the dependent virtual paths.</returns>
		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x06004460 RID: 17504 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public string HashCode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a list of virtual path dependencies.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> containing the virtual path dependencies.</returns>
		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x06004461 RID: 17505 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public IEnumerable VirtualPaths
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
