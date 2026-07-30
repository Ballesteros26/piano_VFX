using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Security.Permissions
{
	/// <summary>Controls the ability to access files and folders. This class cannot be inherited.</summary>
	// Token: 0x0200058C RID: 1420
	[ComVisible(true)]
	[Serializable]
	public sealed class FileIOPermission : CodeAccessPermission, IBuiltInPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with fully restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06003F78 RID: 16248 RVA: 0x000E3044 File Offset: 0x000E1244
		public FileIOPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this.m_Unrestricted = true;
				this.m_AllFilesAccess = FileIOPermissionAccess.AllAccess;
				this.m_AllLocalFilesAccess = FileIOPermissionAccess.AllAccess;
			}
			this.CreateLists();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with the specified access to the designated file or directory.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> enumeration values. </param>
		/// <param name="path">The absolute path of the file or directory. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- The <paramref name="path" /> parameter is not a valid string.-or- The <paramref name="path" /> parameter does not specify the absolute path to the file or directory. </exception>
		// Token: 0x06003F79 RID: 16249 RVA: 0x000E3073 File Offset: 0x000E1273
		public FileIOPermission(FileIOPermissionAccess access, string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			this.CreateLists();
			this.AddPathList(access, path);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with the specified access to the designated files and directories.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> enumeration values. </param>
		/// <param name="pathList">An array containing the absolute paths of the files and directories. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- An entry in the <paramref name="pathList" /> array is not a valid string. </exception>
		// Token: 0x06003F7A RID: 16250 RVA: 0x000E3097 File Offset: 0x000E1297
		public FileIOPermission(FileIOPermissionAccess access, string[] pathList)
		{
			if (pathList == null)
			{
				throw new ArgumentNullException("pathList");
			}
			this.CreateLists();
			this.AddPathList(access, pathList);
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x000E30BB File Offset: 0x000E12BB
		internal void CreateLists()
		{
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
			this.appendList = new ArrayList();
			this.pathList = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with the specified access to the designated file or directory and the specified access rights to file control information.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> enumeration values.</param>
		/// <param name="control">A bitwise combination of the <see cref="T:System.Security.AccessControl.AccessControlActions" />  enumeration values.</param>
		/// <param name="path">The absolute path of the file or directory.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- The <paramref name="path" /> parameter is not a valid string.-or- The <paramref name="path" /> parameter does not specify the absolute path to the file or directory. </exception>
		// Token: 0x06003F7C RID: 16252 RVA: 0x000E30E9 File Offset: 0x000E12E9
		[MonoTODO("(2.0) Access Control isn't implemented")]
		public FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string path)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with the specified access to the designated files and directories and the specified access rights to file control information.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> enumeration values. </param>
		/// <param name="control">A bitwise combination of the <see cref="T:System.Security.AccessControl.AccessControlActions" />  enumeration values.</param>
		/// <param name="pathList">An array containing the absolute paths of the files and directories.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- An entry in the <paramref name="pathList" /> array is not a valid string. </exception>
		// Token: 0x06003F7D RID: 16253 RVA: 0x000E30E9 File Offset: 0x000E12E9
		[MonoTODO("(2.0) Access Control isn't implemented")]
		public FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string[] pathList)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x000E30F6 File Offset: 0x000E12F6
		internal FileIOPermission(FileIOPermissionAccess access, string[] pathList, bool checkForDuplicates, bool needFullPath)
		{
		}

		/// <summary>Gets or sets the permitted access to all files.</summary>
		/// <returns>The set of file I/O flags for all files.</returns>
		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06003F7F RID: 16255 RVA: 0x000E30FE File Offset: 0x000E12FE
		// (set) Token: 0x06003F80 RID: 16256 RVA: 0x000E3106 File Offset: 0x000E1306
		public FileIOPermissionAccess AllFiles
		{
			get
			{
				return this.m_AllFilesAccess;
			}
			set
			{
				if (!this.m_Unrestricted)
				{
					this.m_AllFilesAccess = value;
				}
			}
		}

		/// <summary>Gets or sets the permitted access to all local files.</summary>
		/// <returns>The set of file I/O flags for all local files.</returns>
		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06003F81 RID: 16257 RVA: 0x000E3117 File Offset: 0x000E1317
		// (set) Token: 0x06003F82 RID: 16258 RVA: 0x000E311F File Offset: 0x000E131F
		public FileIOPermissionAccess AllLocalFiles
		{
			get
			{
				return this.m_AllLocalFilesAccess;
			}
			set
			{
				if (!this.m_Unrestricted)
				{
					this.m_AllLocalFilesAccess = value;
				}
			}
		}

		/// <summary>Adds access for the specified file or directory to the existing state of the permission.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values. </param>
		/// <param name="path">The absolute path of a file or directory. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- The <paramref name="path" /> parameter is not a valid string.-or- The <paramref name="path" /> parameter did not specify the absolute path to the file or directory. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="path" /> parameter has an invalid format. </exception>
		// Token: 0x06003F83 RID: 16259 RVA: 0x000E3130 File Offset: 0x000E1330
		public void AddPathList(FileIOPermissionAccess access, string path)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			FileIOPermission.ThrowIfInvalidPath(path);
			this.AddPathInternal(access, path);
		}

		/// <summary>Adds access for the specified files and directories to the existing state of the permission.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values. </param>
		/// <param name="pathList">An array containing the absolute paths of the files and directories. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- An entry in the <paramref name="pathList" /> array is not valid. </exception>
		/// <exception cref="T:System.NotSupportedException">An entry in the <paramref name="pathList" /> array has an invalid format. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pathList" /> parameter is null. </exception>
		// Token: 0x06003F84 RID: 16260 RVA: 0x000E3150 File Offset: 0x000E1350
		public void AddPathList(FileIOPermissionAccess access, string[] pathList)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			FileIOPermission.ThrowIfInvalidPath(pathList);
			foreach (string text in pathList)
			{
				this.AddPathInternal(access, text);
			}
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x000E3190 File Offset: 0x000E1390
		internal void AddPathInternal(FileIOPermissionAccess access, string path)
		{
			path = Path.InsecureGetFullPath(path);
			if ((access & FileIOPermissionAccess.Read) == FileIOPermissionAccess.Read)
			{
				this.readList.Add(path);
			}
			if ((access & FileIOPermissionAccess.Write) == FileIOPermissionAccess.Write)
			{
				this.writeList.Add(path);
			}
			if ((access & FileIOPermissionAccess.Append) == FileIOPermissionAccess.Append)
			{
				this.appendList.Add(path);
			}
			if ((access & FileIOPermissionAccess.PathDiscovery) == FileIOPermissionAccess.PathDiscovery)
			{
				this.pathList.Add(path);
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06003F86 RID: 16262 RVA: 0x000E31F4 File Offset: 0x000E13F4
		public override IPermission Copy()
		{
			if (this.m_Unrestricted)
			{
				return new FileIOPermission(PermissionState.Unrestricted);
			}
			return new FileIOPermission(PermissionState.None)
			{
				readList = (ArrayList)this.readList.Clone(),
				writeList = (ArrayList)this.writeList.Clone(),
				appendList = (ArrayList)this.appendList.Clone(),
				pathList = (ArrayList)this.pathList.Clone(),
				m_AllFilesAccess = this.m_AllFilesAccess,
				m_AllLocalFilesAccess = this.m_AllLocalFilesAccess
			};
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding used to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not compatible. </exception>
		// Token: 0x06003F87 RID: 16263 RVA: 0x000E3288 File Offset: 0x000E1488
		[SecuritySafeCritical]
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this.m_Unrestricted = true;
				return;
			}
			this.m_Unrestricted = false;
			string text = esd.Attribute("Read");
			if (text != null)
			{
				string[] array = text.Split(new char[] { ';' });
				this.AddPathList(FileIOPermissionAccess.Read, array);
			}
			text = esd.Attribute("Write");
			if (text != null)
			{
				string[] array = text.Split(new char[] { ';' });
				this.AddPathList(FileIOPermissionAccess.Write, array);
			}
			text = esd.Attribute("Append");
			if (text != null)
			{
				string[] array = text.Split(new char[] { ';' });
				this.AddPathList(FileIOPermissionAccess.Append, array);
			}
			text = esd.Attribute("PathDiscovery");
			if (text != null)
			{
				string[] array = text.Split(new char[] { ';' });
				this.AddPathList(FileIOPermissionAccess.PathDiscovery, array);
			}
		}

		/// <summary>Gets all files and directories with the specified <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.</summary>
		/// <returns>An array containing the paths of the files and directories to which access specified by the <paramref name="access" /> parameter is granted.</returns>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values that represents a single type of file access. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="access" /> is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- <paramref name="access" /> is <see cref="F:System.Security.Permissions.FileIOPermissionAccess.AllAccess" />, which represents more than one type of file access, or <see cref="F:System.Security.Permissions.FileIOPermissionAccess.NoAccess" />, which does not represent any type of file access. </exception>
		// Token: 0x06003F88 RID: 16264 RVA: 0x000E3360 File Offset: 0x000E1560
		public string[] GetPathList(FileIOPermissionAccess access)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			ArrayList arrayList = new ArrayList();
			switch (access)
			{
			case FileIOPermissionAccess.NoAccess:
				goto IL_007F;
			case FileIOPermissionAccess.Read:
				arrayList.AddRange(this.readList);
				goto IL_007F;
			case FileIOPermissionAccess.Write:
				arrayList.AddRange(this.writeList);
				goto IL_007F;
			case FileIOPermissionAccess.Append:
				arrayList.AddRange(this.appendList);
				goto IL_007F;
			case FileIOPermissionAccess.PathDiscovery:
				arrayList.AddRange(this.pathList);
				goto IL_007F;
			}
			FileIOPermission.ThrowInvalidFlag(access, false);
			IL_007F:
			if (arrayList.Count <= 0)
			{
				return null;
			}
			return (string[])arrayList.ToArray(typeof(string));
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003F89 RID: 16265 RVA: 0x000E340C File Offset: 0x000E160C
		public override IPermission Intersect(IPermission target)
		{
			FileIOPermission fileIOPermission = FileIOPermission.Cast(target);
			if (fileIOPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted())
			{
				return fileIOPermission.Copy();
			}
			if (fileIOPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			FileIOPermission fileIOPermission2 = new FileIOPermission(PermissionState.None);
			fileIOPermission2.AllFiles = this.m_AllFilesAccess & fileIOPermission.AllFiles;
			fileIOPermission2.AllLocalFiles = this.m_AllLocalFilesAccess & fileIOPermission.AllLocalFiles;
			FileIOPermission.IntersectKeys(this.readList, fileIOPermission.readList, fileIOPermission2.readList);
			FileIOPermission.IntersectKeys(this.writeList, fileIOPermission.writeList, fileIOPermission2.writeList);
			FileIOPermission.IntersectKeys(this.appendList, fileIOPermission.appendList, fileIOPermission2.appendList);
			FileIOPermission.IntersectKeys(this.pathList, fileIOPermission.pathList, fileIOPermission2.pathList);
			if (!fileIOPermission2.IsEmpty())
			{
				return fileIOPermission2;
			}
			return null;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003F8A RID: 16266 RVA: 0x000E34D8 File Offset: 0x000E16D8
		public override bool IsSubsetOf(IPermission target)
		{
			FileIOPermission fileIOPermission = FileIOPermission.Cast(target);
			if (fileIOPermission == null)
			{
				return false;
			}
			if (fileIOPermission.IsEmpty())
			{
				return this.IsEmpty();
			}
			if (this.IsUnrestricted())
			{
				return fileIOPermission.IsUnrestricted();
			}
			return fileIOPermission.IsUnrestricted() || ((this.m_AllFilesAccess & fileIOPermission.AllFiles) == this.m_AllFilesAccess && (this.m_AllLocalFilesAccess & fileIOPermission.AllLocalFiles) == this.m_AllLocalFilesAccess && FileIOPermission.KeyIsSubsetOf(this.appendList, fileIOPermission.appendList) && FileIOPermission.KeyIsSubsetOf(this.readList, fileIOPermission.readList) && FileIOPermission.KeyIsSubsetOf(this.writeList, fileIOPermission.writeList) && FileIOPermission.KeyIsSubsetOf(this.pathList, fileIOPermission.pathList));
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06003F8B RID: 16267 RVA: 0x000E359C File Offset: 0x000E179C
		public bool IsUnrestricted()
		{
			return this.m_Unrestricted;
		}

		/// <summary>Sets the specified access to the specified file or directory, replacing the existing state of the permission.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values. </param>
		/// <param name="path">The absolute path of the file or directory. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- The <paramref name="path" /> parameter is not a valid string.-or- The <paramref name="path" /> parameter did not specify the absolute path to the file or directory. </exception>
		// Token: 0x06003F8C RID: 16268 RVA: 0x000E35A4 File Offset: 0x000E17A4
		public void SetPathList(FileIOPermissionAccess access, string path)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			FileIOPermission.ThrowIfInvalidPath(path);
			this.Clear(access);
			this.AddPathInternal(access, path);
		}

		/// <summary>Sets the specified access to the specified files and directories, replacing the current state for the specified access with the new set of paths.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values. </param>
		/// <param name="pathList">An array containing the absolute paths of the files and directories. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- An entry in the <paramref name="pathList" /> parameter is not a valid string. </exception>
		// Token: 0x06003F8D RID: 16269 RVA: 0x000E35CC File Offset: 0x000E17CC
		public void SetPathList(FileIOPermissionAccess access, string[] pathList)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			FileIOPermission.ThrowIfInvalidPath(pathList);
			this.Clear(access);
			foreach (string text in pathList)
			{
				this.AddPathInternal(access, text);
			}
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06003F8E RID: 16270 RVA: 0x000E3610 File Offset: 0x000E1810
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.m_Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				string[] array = this.GetPathList(FileIOPermissionAccess.Append);
				if (array != null && array.Length != 0)
				{
					securityElement.AddAttribute("Append", string.Join(";", array));
				}
				array = this.GetPathList(FileIOPermissionAccess.Read);
				if (array != null && array.Length != 0)
				{
					securityElement.AddAttribute("Read", string.Join(";", array));
				}
				array = this.GetPathList(FileIOPermissionAccess.Write);
				if (array != null && array.Length != 0)
				{
					securityElement.AddAttribute("Write", string.Join(";", array));
				}
				array = this.GetPathList(FileIOPermissionAccess.PathDiscovery);
				if (array != null && array.Length != 0)
				{
					securityElement.AddAttribute("PathDiscovery", string.Join(";", array));
				}
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="other">A permission to combine with the current permission. It must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="other" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003F8F RID: 16271 RVA: 0x000E36D8 File Offset: 0x000E18D8
		public override IPermission Union(IPermission other)
		{
			FileIOPermission fileIOPermission = FileIOPermission.Cast(other);
			if (fileIOPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || fileIOPermission.IsUnrestricted())
			{
				return new FileIOPermission(PermissionState.Unrestricted);
			}
			if (this.IsEmpty() && fileIOPermission.IsEmpty())
			{
				return null;
			}
			FileIOPermission fileIOPermission2 = (FileIOPermission)this.Copy();
			fileIOPermission2.AllFiles |= fileIOPermission.AllFiles;
			fileIOPermission2.AllLocalFiles |= fileIOPermission.AllLocalFiles;
			string[] array = fileIOPermission.GetPathList(FileIOPermissionAccess.Read);
			if (array != null)
			{
				FileIOPermission.UnionKeys(fileIOPermission2.readList, array);
			}
			array = fileIOPermission.GetPathList(FileIOPermissionAccess.Write);
			if (array != null)
			{
				FileIOPermission.UnionKeys(fileIOPermission2.writeList, array);
			}
			array = fileIOPermission.GetPathList(FileIOPermissionAccess.Append);
			if (array != null)
			{
				FileIOPermission.UnionKeys(fileIOPermission2.appendList, array);
			}
			array = fileIOPermission.GetPathList(FileIOPermissionAccess.PathDiscovery);
			if (array != null)
			{
				FileIOPermission.UnionKeys(fileIOPermission2.pathList, array);
			}
			return fileIOPermission2;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.Permissions.FileIOPermission" /> object is equal to the current <see cref="T:System.Security.Permissions.FileIOPermission" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Security.Permissions.FileIOPermission" /> is equal to the current <see cref="T:System.Security.Permissions.FileIOPermission" /> object; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Security.Permissions.FileIOPermission" /> object to compare with the current <see cref="T:System.Security.Permissions.FileIOPermission" />. </param>
		// Token: 0x06003F90 RID: 16272 RVA: 0x00015ED5 File Offset: 0x000140D5
		[ComVisible(false)]
		[MonoTODO("(2.0)")]
		public override bool Equals(object obj)
		{
			return false;
		}

		/// <summary>Gets a hash code for the <see cref="T:System.Security.Permissions.FileIOPermission" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.Permissions.FileIOPermission" /> object.</returns>
		// Token: 0x06003F91 RID: 16273 RVA: 0x000E37AE File Offset: 0x000E19AE
		[ComVisible(false)]
		[MonoTODO("(2.0)")]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x0001EFC9 File Offset: 0x0001D1C9
		int IBuiltInPermission.GetTokenIndex()
		{
			return 2;
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x000E37B8 File Offset: 0x000E19B8
		private bool IsEmpty()
		{
			return !this.m_Unrestricted && this.appendList.Count == 0 && this.readList.Count == 0 && this.writeList.Count == 0 && this.pathList.Count == 0;
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x000E3804 File Offset: 0x000E1A04
		private static FileIOPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			FileIOPermission fileIOPermission = target as FileIOPermission;
			if (fileIOPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(FileIOPermission));
			}
			return fileIOPermission;
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x000E3824 File Offset: 0x000E1A24
		internal static void ThrowInvalidFlag(FileIOPermissionAccess access, bool context)
		{
			string text;
			if (context)
			{
				text = Locale.GetText("Unknown flag '{0}'.");
			}
			else
			{
				text = Locale.GetText("Invalid flag '{0}' in this context.");
			}
			throw new ArgumentException(string.Format(text, access), "access");
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x000E3864 File Offset: 0x000E1A64
		internal static void ThrowIfInvalidPath(string path)
		{
			string directoryName = Path.GetDirectoryName(path);
			if (directoryName != null && directoryName.LastIndexOfAny(FileIOPermission.BadPathNameCharacters) >= 0)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid path characters in path: '{0}'"), path), "path");
			}
			string fileName = Path.GetFileName(path);
			if (fileName != null && fileName.LastIndexOfAny(FileIOPermission.BadFileNameCharacters) >= 0)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid filename characters in path: '{0}'"), path), "path");
			}
			if (!Path.IsPathRooted(path))
			{
				throw new ArgumentException(Locale.GetText("Absolute path information is required."), "path");
			}
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x000E38F4 File Offset: 0x000E1AF4
		internal static void ThrowIfInvalidPath(string[] paths)
		{
			for (int i = 0; i < paths.Length; i++)
			{
				FileIOPermission.ThrowIfInvalidPath(paths[i]);
			}
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x000E391C File Offset: 0x000E1B1C
		internal void Clear(FileIOPermissionAccess access)
		{
			if ((access & FileIOPermissionAccess.Read) == FileIOPermissionAccess.Read)
			{
				this.readList.Clear();
			}
			if ((access & FileIOPermissionAccess.Write) == FileIOPermissionAccess.Write)
			{
				this.writeList.Clear();
			}
			if ((access & FileIOPermissionAccess.Append) == FileIOPermissionAccess.Append)
			{
				this.appendList.Clear();
			}
			if ((access & FileIOPermissionAccess.PathDiscovery) == FileIOPermissionAccess.PathDiscovery)
			{
				this.pathList.Clear();
			}
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x000E3970 File Offset: 0x000E1B70
		internal static bool KeyIsSubsetOf(IList local, IList target)
		{
			bool flag = false;
			foreach (object obj in local)
			{
				string text = (string)obj;
				using (IEnumerator enumerator2 = target.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (Path.IsPathSubsetOf((string)enumerator2.Current, text))
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x000E3A18 File Offset: 0x000E1C18
		internal static void UnionKeys(IList list, string[] paths)
		{
			foreach (string text in paths)
			{
				int count = list.Count;
				if (count == 0)
				{
					list.Add(text);
				}
				else
				{
					int j;
					for (j = 0; j < count; j++)
					{
						string text2 = (string)list[j];
						if (Path.IsPathSubsetOf(text, text2))
						{
							list[j] = text;
							break;
						}
						if (Path.IsPathSubsetOf(text2, text))
						{
							break;
						}
					}
					if (j == count)
					{
						list.Add(text);
					}
				}
			}
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x000E3A98 File Offset: 0x000E1C98
		internal static void IntersectKeys(IList local, IList target, IList result)
		{
			foreach (object obj in local)
			{
				string text = (string)obj;
				foreach (object obj2 in target)
				{
					string text2 = (string)obj2;
					if (text2.Length > text.Length)
					{
						if (Path.IsPathSubsetOf(text, text2))
						{
							result.Add(text2);
						}
					}
					else if (Path.IsPathSubsetOf(text2, text))
					{
						result.Add(text);
					}
				}
			}
		}

		// Token: 0x0400202B RID: 8235
		private const int version = 1;

		// Token: 0x0400202C RID: 8236
		private static char[] BadPathNameCharacters = Path.GetInvalidPathChars();

		// Token: 0x0400202D RID: 8237
		private static char[] BadFileNameCharacters = Path.GetInvalidFileNameChars();

		// Token: 0x0400202E RID: 8238
		private bool m_Unrestricted;

		// Token: 0x0400202F RID: 8239
		private FileIOPermissionAccess m_AllFilesAccess;

		// Token: 0x04002030 RID: 8240
		private FileIOPermissionAccess m_AllLocalFilesAccess;

		// Token: 0x04002031 RID: 8241
		private ArrayList readList;

		// Token: 0x04002032 RID: 8242
		private ArrayList writeList;

		// Token: 0x04002033 RID: 8243
		private ArrayList appendList;

		// Token: 0x04002034 RID: 8244
		private ArrayList pathList;
	}
}
