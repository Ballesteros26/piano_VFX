using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Provides version information for a physical file on disk.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001FE RID: 510
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	public sealed class FileVersionInfo
	{
		// Token: 0x0600106D RID: 4205 RVA: 0x00049AD8 File Offset: 0x00047CD8
		private FileVersionInfo()
		{
			this.comments = null;
			this.companyname = null;
			this.filedescription = null;
			this.filename = null;
			this.fileversion = null;
			this.internalname = null;
			this.language = null;
			this.legalcopyright = null;
			this.legaltrademarks = null;
			this.originalfilename = null;
			this.privatebuild = null;
			this.productname = null;
			this.productversion = null;
			this.specialbuild = null;
			this.isdebug = false;
			this.ispatched = false;
			this.isprerelease = false;
			this.isprivatebuild = false;
			this.isspecialbuild = false;
			this.filemajorpart = 0;
			this.fileminorpart = 0;
			this.filebuildpart = 0;
			this.fileprivatepart = 0;
			this.productmajorpart = 0;
			this.productminorpart = 0;
			this.productbuildpart = 0;
			this.productprivatepart = 0;
		}

		/// <summary>Gets the comments associated with the file.</summary>
		/// <returns>The comments associated with the file or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x00049BA8 File Offset: 0x00047DA8
		public string Comments
		{
			get
			{
				return this.comments;
			}
		}

		/// <summary>Gets the name of the company that produced the file.</summary>
		/// <returns>The name of the company that produced the file or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x00049BB0 File Offset: 0x00047DB0
		public string CompanyName
		{
			get
			{
				return this.companyname;
			}
		}

		/// <summary>Gets the build number of the file.</summary>
		/// <returns>A value representing the build number of the file or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x00049BB8 File Offset: 0x00047DB8
		public int FileBuildPart
		{
			get
			{
				return this.filebuildpart;
			}
		}

		/// <summary>Gets the description of the file.</summary>
		/// <returns>The description of the file or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x00049BC0 File Offset: 0x00047DC0
		public string FileDescription
		{
			get
			{
				return this.filedescription;
			}
		}

		/// <summary>Gets the major part of the version number.</summary>
		/// <returns>A value representing the major part of the version number or 0 (zero) if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x00049BC8 File Offset: 0x00047DC8
		public int FileMajorPart
		{
			get
			{
				return this.filemajorpart;
			}
		}

		/// <summary>Gets the minor part of the version number of the file.</summary>
		/// <returns>A value representing the minor part of the version number of the file or 0 (zero) if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x00049BD0 File Offset: 0x00047DD0
		public int FileMinorPart
		{
			get
			{
				return this.fileminorpart;
			}
		}

		/// <summary>Gets the name of the file that this instance of <see cref="T:System.Diagnostics.FileVersionInfo" /> describes.</summary>
		/// <returns>The name of the file described by this instance of <see cref="T:System.Diagnostics.FileVersionInfo" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x00049BD8 File Offset: 0x00047DD8
		public string FileName
		{
			get
			{
				return this.filename;
			}
		}

		/// <summary>Gets the file private part number.</summary>
		/// <returns>A value representing the file private part number or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x00049BE0 File Offset: 0x00047DE0
		public int FilePrivatePart
		{
			get
			{
				return this.fileprivatepart;
			}
		}

		/// <summary>Gets the file version number.</summary>
		/// <returns>The version number of the file or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x00049BE8 File Offset: 0x00047DE8
		public string FileVersion
		{
			get
			{
				return this.fileversion;
			}
		}

		/// <summary>Gets the internal name of the file, if one exists.</summary>
		/// <returns>The internal name of the file. If none exists, this property will contain the original name of the file without the extension.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x00049BF0 File Offset: 0x00047DF0
		public string InternalName
		{
			get
			{
				return this.internalname;
			}
		}

		/// <summary>Gets a value that specifies whether the file contains debugging information or is compiled with debugging features enabled.</summary>
		/// <returns>true if the file contains debugging information or is compiled with debugging features enabled; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x00049BF8 File Offset: 0x00047DF8
		public bool IsDebug
		{
			get
			{
				return this.isdebug;
			}
		}

		/// <summary>Gets a value that specifies whether the file has been modified and is not identical to the original shipping file of the same version number.</summary>
		/// <returns>true if the file is patched; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x00049C00 File Offset: 0x00047E00
		public bool IsPatched
		{
			get
			{
				return this.ispatched;
			}
		}

		/// <summary>Gets a value that specifies whether the file is a development version, rather than a commercially released product.</summary>
		/// <returns>true if the file is prerelease; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x00049C08 File Offset: 0x00047E08
		public bool IsPreRelease
		{
			get
			{
				return this.isprerelease;
			}
		}

		/// <summary>Gets a value that specifies whether the file was built using standard release procedures.</summary>
		/// <returns>true if the file is a private build; false if the file was built using standard release procedures or if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x00049C10 File Offset: 0x00047E10
		public bool IsPrivateBuild
		{
			get
			{
				return this.isprivatebuild;
			}
		}

		/// <summary>Gets a value that specifies whether the file is a special build.</summary>
		/// <returns>true if the file is a special build; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x00049C18 File Offset: 0x00047E18
		public bool IsSpecialBuild
		{
			get
			{
				return this.isspecialbuild;
			}
		}

		/// <summary>Gets the default language string for the version info block.</summary>
		/// <returns>The description string for the Microsoft Language Identifier in the version resource or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x00049C20 File Offset: 0x00047E20
		public string Language
		{
			get
			{
				return this.language;
			}
		}

		/// <summary>Gets all copyright notices that apply to the specified file.</summary>
		/// <returns>The copyright notices that apply to the specified file.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x00049C28 File Offset: 0x00047E28
		public string LegalCopyright
		{
			get
			{
				return this.legalcopyright;
			}
		}

		/// <summary>Gets the trademarks and registered trademarks that apply to the file.</summary>
		/// <returns>The trademarks and registered trademarks that apply to the file or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x00049C30 File Offset: 0x00047E30
		public string LegalTrademarks
		{
			get
			{
				return this.legaltrademarks;
			}
		}

		/// <summary>Gets the name the file was created with.</summary>
		/// <returns>The name the file was created with or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x00049C38 File Offset: 0x00047E38
		public string OriginalFilename
		{
			get
			{
				return this.originalfilename;
			}
		}

		/// <summary>Gets information about a private version of the file.</summary>
		/// <returns>Information about a private version of the file or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x00049C40 File Offset: 0x00047E40
		public string PrivateBuild
		{
			get
			{
				return this.privatebuild;
			}
		}

		/// <summary>Gets the build number of the product this file is associated with.</summary>
		/// <returns>A value representing the build number of the product this file is associated with or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x00049C48 File Offset: 0x00047E48
		public int ProductBuildPart
		{
			get
			{
				return this.productbuildpart;
			}
		}

		/// <summary>Gets the major part of the version number for the product this file is associated with.</summary>
		/// <returns>A value representing the major part of the product version number or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x00049C50 File Offset: 0x00047E50
		public int ProductMajorPart
		{
			get
			{
				return this.productmajorpart;
			}
		}

		/// <summary>Gets the minor part of the version number for the product the file is associated with.</summary>
		/// <returns>A value representing the minor part of the product version number or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x00049C58 File Offset: 0x00047E58
		public int ProductMinorPart
		{
			get
			{
				return this.productminorpart;
			}
		}

		/// <summary>Gets the name of the product this file is distributed with.</summary>
		/// <returns>The name of the product this file is distributed with or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00049C60 File Offset: 0x00047E60
		public string ProductName
		{
			get
			{
				return this.productname;
			}
		}

		/// <summary>Gets the private part number of the product this file is associated with.</summary>
		/// <returns>A value representing the private part number of the product this file is associated with or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x00049C68 File Offset: 0x00047E68
		public int ProductPrivatePart
		{
			get
			{
				return this.productprivatepart;
			}
		}

		/// <summary>Gets the version of the product this file is distributed with.</summary>
		/// <returns>The version of the product this file is distributed with or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x00049C70 File Offset: 0x00047E70
		public string ProductVersion
		{
			get
			{
				return this.productversion;
			}
		}

		/// <summary>Gets the special build information for the file.</summary>
		/// <returns>The special build information for the file or null if the file did not contain version information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x00049C78 File Offset: 0x00047E78
		public string SpecialBuild
		{
			get
			{
				return this.specialbuild;
			}
		}

		// Token: 0x06001089 RID: 4233
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVersionInfo_internal(string fileName);

		/// <summary>Returns a <see cref="T:System.Diagnostics.FileVersionInfo" /> representing the version information associated with the specified file.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.FileVersionInfo" /> containing information about the file. If the file did not contain version information, the <see cref="T:System.Diagnostics.FileVersionInfo" /> contains only the name of the file requested.</returns>
		/// <param name="fileName">The fully qualified path and name of the file to retrieve the version information for. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified cannot be found. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600108A RID: 4234 RVA: 0x00049C80 File Offset: 0x00047E80
		public static FileVersionInfo GetVersionInfo(string fileName)
		{
			if (!File.Exists(Path.GetFullPath(fileName)))
			{
				throw new FileNotFoundException(fileName);
			}
			FileVersionInfo fileVersionInfo = new FileVersionInfo();
			fileVersionInfo.GetVersionInfo_internal(fileName);
			return fileVersionInfo;
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x00049CA2 File Offset: 0x00047EA2
		private static void AppendFormat(StringBuilder sb, string format, params object[] args)
		{
			sb.AppendFormat(format, args);
		}

		/// <summary>Returns a partial list of properties in the <see cref="T:System.Diagnostics.FileVersionInfo" /> and their values.</summary>
		/// <returns>A list of the following properties in this class and their values: <see cref="P:System.Diagnostics.FileVersionInfo.FileName" />, <see cref="P:System.Diagnostics.FileVersionInfo.InternalName" />, <see cref="P:System.Diagnostics.FileVersionInfo.OriginalFilename" />, <see cref="P:System.Diagnostics.FileVersionInfo.FileVersion" />, <see cref="P:System.Diagnostics.FileVersionInfo.FileDescription" />, <see cref="P:System.Diagnostics.FileVersionInfo.ProductName" />, <see cref="P:System.Diagnostics.FileVersionInfo.ProductVersion" />, <see cref="P:System.Diagnostics.FileVersionInfo.IsDebug" />, <see cref="P:System.Diagnostics.FileVersionInfo.IsPatched" />, <see cref="P:System.Diagnostics.FileVersionInfo.IsPreRelease" />, <see cref="P:System.Diagnostics.FileVersionInfo.IsPrivateBuild" />, <see cref="P:System.Diagnostics.FileVersionInfo.IsSpecialBuild" />,<see cref="P:System.Diagnostics.FileVersionInfo.Language" />.If the file did not contain version information, this list will contain only the name of the requested file. Boolean values will be false, and all other entries will be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600108C RID: 4236 RVA: 0x00049CB0 File Offset: 0x00047EB0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			FileVersionInfo.AppendFormat(stringBuilder, "File:             {0}{1}", new object[]
			{
				this.FileName,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "InternalName:     {0}{1}", new object[]
			{
				this.internalname,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "OriginalFilename: {0}{1}", new object[]
			{
				this.originalfilename,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "FileVersion:      {0}{1}", new object[]
			{
				this.fileversion,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "FileDescription:  {0}{1}", new object[]
			{
				this.filedescription,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "Product:          {0}{1}", new object[]
			{
				this.productname,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "ProductVersion:   {0}{1}", new object[]
			{
				this.productversion,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "Debug:            {0}{1}", new object[]
			{
				this.isdebug,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "Patched:          {0}{1}", new object[]
			{
				this.ispatched,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "PreRelease:       {0}{1}", new object[]
			{
				this.isprerelease,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "PrivateBuild:     {0}{1}", new object[]
			{
				this.isprivatebuild,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "SpecialBuild:     {0}{1}", new object[]
			{
				this.isspecialbuild,
				Environment.NewLine
			});
			FileVersionInfo.AppendFormat(stringBuilder, "Language          {0}{1}", new object[]
			{
				this.language,
				Environment.NewLine
			});
			return stringBuilder.ToString();
		}

		// Token: 0x0400115F RID: 4447
		private string comments;

		// Token: 0x04001160 RID: 4448
		private string companyname;

		// Token: 0x04001161 RID: 4449
		private string filedescription;

		// Token: 0x04001162 RID: 4450
		private string filename;

		// Token: 0x04001163 RID: 4451
		private string fileversion;

		// Token: 0x04001164 RID: 4452
		private string internalname;

		// Token: 0x04001165 RID: 4453
		private string language;

		// Token: 0x04001166 RID: 4454
		private string legalcopyright;

		// Token: 0x04001167 RID: 4455
		private string legaltrademarks;

		// Token: 0x04001168 RID: 4456
		private string originalfilename;

		// Token: 0x04001169 RID: 4457
		private string privatebuild;

		// Token: 0x0400116A RID: 4458
		private string productname;

		// Token: 0x0400116B RID: 4459
		private string productversion;

		// Token: 0x0400116C RID: 4460
		private string specialbuild;

		// Token: 0x0400116D RID: 4461
		private bool isdebug;

		// Token: 0x0400116E RID: 4462
		private bool ispatched;

		// Token: 0x0400116F RID: 4463
		private bool isprerelease;

		// Token: 0x04001170 RID: 4464
		private bool isprivatebuild;

		// Token: 0x04001171 RID: 4465
		private bool isspecialbuild;

		// Token: 0x04001172 RID: 4466
		private int filemajorpart;

		// Token: 0x04001173 RID: 4467
		private int fileminorpart;

		// Token: 0x04001174 RID: 4468
		private int filebuildpart;

		// Token: 0x04001175 RID: 4469
		private int fileprivatepart;

		// Token: 0x04001176 RID: 4470
		private int productmajorpart;

		// Token: 0x04001177 RID: 4471
		private int productminorpart;

		// Token: 0x04001178 RID: 4472
		private int productbuildpart;

		// Token: 0x04001179 RID: 4473
		private int productprivatepart;
	}
}
