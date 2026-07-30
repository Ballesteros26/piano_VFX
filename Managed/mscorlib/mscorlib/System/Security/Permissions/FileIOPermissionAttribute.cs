using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.FileIOPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x0200058E RID: 1422
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class FileIOPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="action" /> parameter is not a valid <see cref="T:System.Security.Permissions.SecurityAction" />. </exception>
		// Token: 0x06003F9C RID: 16284 RVA: 0x000E2D08 File Offset: 0x000E0F08
		public FileIOPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets full access for the file or directory that is specified by the string value.</summary>
		/// <returns>The absolute path of the file or directory for full access.</returns>
		/// <exception cref="T:System.NotSupportedException">The get method is not supported for this property.</exception>
		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06003F9D RID: 16285 RVA: 0x000E2D11 File Offset: 0x000E0F11
		// (set) Token: 0x06003F9E RID: 16286 RVA: 0x000E3B5C File Offset: 0x000E1D5C
		[Obsolete("use newer properties")]
		public string All
		{
			get
			{
				throw new NotSupportedException("All");
			}
			set
			{
				this.append = value;
				this.path = value;
				this.read = value;
				this.write = value;
			}
		}

		/// <summary>Gets or sets append access for the file or directory that is specified by the string value.</summary>
		/// <returns>The absolute path of the file or directory for append access.</returns>
		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06003F9F RID: 16287 RVA: 0x000E3B7A File Offset: 0x000E1D7A
		// (set) Token: 0x06003FA0 RID: 16288 RVA: 0x000E3B82 File Offset: 0x000E1D82
		public string Append
		{
			get
			{
				return this.append;
			}
			set
			{
				this.append = value;
			}
		}

		/// <summary>Gets or sets the file or directory to which to grant path discovery.</summary>
		/// <returns>The absolute path of the file or directory.</returns>
		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06003FA1 RID: 16289 RVA: 0x000E3B8B File Offset: 0x000E1D8B
		// (set) Token: 0x06003FA2 RID: 16290 RVA: 0x000E3B93 File Offset: 0x000E1D93
		public string PathDiscovery
		{
			get
			{
				return this.path;
			}
			set
			{
				this.path = value;
			}
		}

		/// <summary>Gets or sets read access for the file or directory specified by the string value.</summary>
		/// <returns>The absolute path of the file or directory for read access.</returns>
		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06003FA3 RID: 16291 RVA: 0x000E3B9C File Offset: 0x000E1D9C
		// (set) Token: 0x06003FA4 RID: 16292 RVA: 0x000E3BA4 File Offset: 0x000E1DA4
		public string Read
		{
			get
			{
				return this.read;
			}
			set
			{
				this.read = value;
			}
		}

		/// <summary>Gets or sets write access for the file or directory specified by the string value.</summary>
		/// <returns>The absolute path of the file or directory for write access.</returns>
		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06003FA5 RID: 16293 RVA: 0x000E3BAD File Offset: 0x000E1DAD
		// (set) Token: 0x06003FA6 RID: 16294 RVA: 0x000E3BB5 File Offset: 0x000E1DB5
		public string Write
		{
			get
			{
				return this.write;
			}
			set
			{
				this.write = value;
			}
		}

		/// <summary>Gets or sets the permitted access to all files.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values that represents the permissions for all files. The default is <see cref="F:System.Security.Permissions.FileIOPermissionAccess.NoAccess" />.</returns>
		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06003FA7 RID: 16295 RVA: 0x000E3BBE File Offset: 0x000E1DBE
		// (set) Token: 0x06003FA8 RID: 16296 RVA: 0x000E3BC6 File Offset: 0x000E1DC6
		public FileIOPermissionAccess AllFiles
		{
			get
			{
				return this.allFiles;
			}
			set
			{
				this.allFiles = value;
			}
		}

		/// <summary>Gets or sets the permitted access to all local files.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values that represents the permissions for all local files. The default is <see cref="F:System.Security.Permissions.FileIOPermissionAccess.NoAccess" />.</returns>
		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06003FA9 RID: 16297 RVA: 0x000E3BCF File Offset: 0x000E1DCF
		// (set) Token: 0x06003FAA RID: 16298 RVA: 0x000E3BD7 File Offset: 0x000E1DD7
		public FileIOPermissionAccess AllLocalFiles
		{
			get
			{
				return this.allLocalFiles;
			}
			set
			{
				this.allLocalFiles = value;
			}
		}

		/// <summary>Gets or sets the file or directory in which access control information can be changed.</summary>
		/// <returns>The absolute path of the file or directory in which access control information can be changed.</returns>
		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06003FAB RID: 16299 RVA: 0x000E3BE0 File Offset: 0x000E1DE0
		// (set) Token: 0x06003FAC RID: 16300 RVA: 0x000E3BE8 File Offset: 0x000E1DE8
		public string ChangeAccessControl
		{
			get
			{
				return this.changeAccessControl;
			}
			set
			{
				this.changeAccessControl = value;
			}
		}

		/// <summary>Gets or sets the file or directory in which access control information can be viewed.</summary>
		/// <returns>The absolute path of the file or directory in which access control information can be viewed.</returns>
		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06003FAD RID: 16301 RVA: 0x000E3BF1 File Offset: 0x000E1DF1
		// (set) Token: 0x06003FAE RID: 16302 RVA: 0x000E3BF9 File Offset: 0x000E1DF9
		public string ViewAccessControl
		{
			get
			{
				return this.viewAccessControl;
			}
			set
			{
				this.viewAccessControl = value;
			}
		}

		/// <summary>Gets or sets the file or directory in which file data can be viewed and modified.</summary>
		/// <returns>The absolute path of the file or directory in which file data can be viewed and modified.</returns>
		/// <exception cref="T:System.NotSupportedException">The get accessor is called. The accessor is provided only for C# compiler compatibility.</exception>
		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06003FAF RID: 16303 RVA: 0x00014B5A File Offset: 0x00012D5A
		// (set) Token: 0x06003FB0 RID: 16304 RVA: 0x000E3B5C File Offset: 0x000E1D5C
		public string ViewAndModify
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				this.append = value;
				this.path = value;
				this.read = value;
				this.write = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.FileIOPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.FileIOPermission" /> that corresponds to this attribute.</returns>
		/// <exception cref="T:System.ArgumentException">The path information for a file or directory for which access is to be secured contains invalid characters or wildcard specifiers. </exception>
		// Token: 0x06003FB1 RID: 16305 RVA: 0x000E3C04 File Offset: 0x000E1E04
		public override IPermission CreatePermission()
		{
			FileIOPermission fileIOPermission;
			if (base.Unrestricted)
			{
				fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
			}
			else
			{
				fileIOPermission = new FileIOPermission(PermissionState.None);
				if (this.append != null)
				{
					fileIOPermission.AddPathList(FileIOPermissionAccess.Append, this.append);
				}
				if (this.path != null)
				{
					fileIOPermission.AddPathList(FileIOPermissionAccess.PathDiscovery, this.path);
				}
				if (this.read != null)
				{
					fileIOPermission.AddPathList(FileIOPermissionAccess.Read, this.read);
				}
				if (this.write != null)
				{
					fileIOPermission.AddPathList(FileIOPermissionAccess.Write, this.write);
				}
			}
			return fileIOPermission;
		}

		// Token: 0x0400203C RID: 8252
		private string append;

		// Token: 0x0400203D RID: 8253
		private string path;

		// Token: 0x0400203E RID: 8254
		private string read;

		// Token: 0x0400203F RID: 8255
		private string write;

		// Token: 0x04002040 RID: 8256
		private FileIOPermissionAccess allFiles;

		// Token: 0x04002041 RID: 8257
		private FileIOPermissionAccess allLocalFiles;

		// Token: 0x04002042 RID: 8258
		private string changeAccessControl;

		// Token: 0x04002043 RID: 8259
		private string viewAccessControl;
	}
}
