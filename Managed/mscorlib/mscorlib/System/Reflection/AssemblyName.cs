using System;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using Mono;
using Mono.Security;
using Mono.Security.Cryptography;

namespace System.Reflection
{
	/// <summary>Describes an assembly's unique identity in full.</summary>
	// Token: 0x0200030E RID: 782
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_AssemblyName))]
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AssemblyName : ICloneable, ISerializable, IDeserializationCallback, _AssemblyName
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyName" /> class.</summary>
		// Token: 0x0600220E RID: 8718 RVA: 0x0008051F File Offset: 0x0007E71F
		public AssemblyName()
		{
			this.versioncompat = AssemblyVersionCompatibility.SameMachine;
		}

		// Token: 0x0600220F RID: 8719
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ParseAssemblyName(IntPtr name, out MonoAssemblyName aname, out bool is_version_definited, out bool is_token_defined);

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyName" /> class with the specified display name.</summary>
		/// <param name="assemblyName">The display name of the assembly, as returned by the <see cref="P:System.Reflection.AssemblyName.FullName" /> property.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyName" /> is a zero length string. </exception>
		/// <exception cref="T:System.IO.FileLoadException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.The referenced assembly could not be found, or could not be loaded.</exception>
		// Token: 0x06002210 RID: 8720 RVA: 0x00080530 File Offset: 0x0007E730
		public unsafe AssemblyName(string assemblyName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (assemblyName.Length < 1)
			{
				throw new ArgumentException("assemblyName cannot have zero length.");
			}
			using (SafeStringMarshal safeStringMarshal = RuntimeMarshal.MarshalString(assemblyName))
			{
				MonoAssemblyName monoAssemblyName;
				bool flag;
				bool flag2;
				if (!AssemblyName.ParseAssemblyName(safeStringMarshal.Value, out monoAssemblyName, out flag, out flag2))
				{
					throw new FileLoadException("The assembly name is invalid.");
				}
				try
				{
					this.FillName(&monoAssemblyName, null, flag, false, flag2, false);
				}
				finally
				{
					RuntimeMarshal.FreeAssemblyName(ref monoAssemblyName, false);
				}
			}
		}

		/// <summary>Gets or sets a value that identifies the processor and bits-per-word of the platform targeted by an executable.</summary>
		/// <returns>One of the enumeration values that identifies the processor and bits-per-word of the platform targeted by an executable.</returns>
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06002211 RID: 8721 RVA: 0x000805D0 File Offset: 0x0007E7D0
		// (set) Token: 0x06002212 RID: 8722 RVA: 0x000805D8 File Offset: 0x0007E7D8
		[MonoLimitation("Not used, as the values are too limited;  Mono supports more")]
		public ProcessorArchitecture ProcessorArchitecture
		{
			get
			{
				return this.processor_architecture;
			}
			set
			{
				this.processor_architecture = value;
			}
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x000805E4 File Offset: 0x0007E7E4
		internal AssemblyName(SerializationInfo si, StreamingContext sc)
		{
			this.name = si.GetString("_Name");
			this.codebase = si.GetString("_CodeBase");
			this.version = (Version)si.GetValue("_Version", typeof(Version));
			this.publicKey = (byte[])si.GetValue("_PublicKey", typeof(byte[]));
			this.keyToken = (byte[])si.GetValue("_PublicKeyToken", typeof(byte[]));
			this.hashalg = (AssemblyHashAlgorithm)si.GetValue("_HashAlgorithm", typeof(AssemblyHashAlgorithm));
			this.keypair = (StrongNameKeyPair)si.GetValue("_StrongNameKeyPair", typeof(StrongNameKeyPair));
			this.versioncompat = (AssemblyVersionCompatibility)si.GetValue("_VersionCompatibility", typeof(AssemblyVersionCompatibility));
			this.flags = (AssemblyNameFlags)si.GetValue("_Flags", typeof(AssemblyNameFlags));
			int @int = si.GetInt32("_CultureInfo");
			if (@int != -1)
			{
				this.cultureinfo = new CultureInfo(@int);
			}
		}

		/// <summary>Gets or sets the simple name of the assembly. This is usually, but not necessarily, the file name of the manifest file of the assembly, minus its extension.</summary>
		/// <returns>The simple name of the assembly.</returns>
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x00080715 File Offset: 0x0007E915
		// (set) Token: 0x06002215 RID: 8725 RVA: 0x0008071D File Offset: 0x0007E91D
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the location of the assembly as a URL.</summary>
		/// <returns>A string that is the URL location of the assembly. </returns>
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x00080726 File Offset: 0x0007E926
		// (set) Token: 0x06002217 RID: 8727 RVA: 0x0008072E File Offset: 0x0007E92E
		public string CodeBase
		{
			get
			{
				return this.codebase;
			}
			set
			{
				this.codebase = value;
			}
		}

		/// <summary>Gets the URI, including escape characters, that represents the codebase.</summary>
		/// <returns>A URI with escape characters.</returns>
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06002218 RID: 8728 RVA: 0x00080737 File Offset: 0x0007E937
		public string EscapedCodeBase
		{
			get
			{
				if (this.codebase == null)
				{
					return null;
				}
				return Uri.EscapeString(this.codebase, false, true, true);
			}
		}

		/// <summary>Gets or sets the culture supported by the assembly.</summary>
		/// <returns>An object that represents the culture supported by the assembly.</returns>
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06002219 RID: 8729 RVA: 0x00080751 File Offset: 0x0007E951
		// (set) Token: 0x0600221A RID: 8730 RVA: 0x00080759 File Offset: 0x0007E959
		public CultureInfo CultureInfo
		{
			get
			{
				return this.cultureinfo;
			}
			set
			{
				this.cultureinfo = value;
			}
		}

		/// <summary>Gets or sets the attributes of the assembly.</summary>
		/// <returns>A value that represents the attributes of the assembly.</returns>
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600221B RID: 8731 RVA: 0x00080762 File Offset: 0x0007E962
		// (set) Token: 0x0600221C RID: 8732 RVA: 0x0008076A File Offset: 0x0007E96A
		public AssemblyNameFlags Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		/// <summary>Gets the full name of the assembly, also known as the display name.</summary>
		/// <returns>A string that is the full name of the assembly, also known as the display name.</returns>
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x00080774 File Offset: 0x0007E974
		public string FullName
		{
			get
			{
				if (this.name == null)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				if (char.IsWhiteSpace(this.name[0]))
				{
					stringBuilder.Append("\"" + this.name + "\"");
				}
				else
				{
					stringBuilder.Append(this.name);
				}
				if (this.Version != null)
				{
					stringBuilder.Append(", Version=");
					stringBuilder.Append(this.Version.ToString());
				}
				if (this.cultureinfo != null)
				{
					stringBuilder.Append(", Culture=");
					if (this.cultureinfo.LCID == CultureInfo.InvariantCulture.LCID)
					{
						stringBuilder.Append("neutral");
					}
					else
					{
						stringBuilder.Append(this.cultureinfo.Name);
					}
				}
				byte[] array = this.InternalGetPublicKeyToken();
				if (array != null)
				{
					if (array.Length == 0)
					{
						stringBuilder.Append(", PublicKeyToken=null");
					}
					else
					{
						stringBuilder.Append(", PublicKeyToken=");
						for (int i = 0; i < array.Length; i++)
						{
							stringBuilder.Append(array[i].ToString("x2"));
						}
					}
				}
				if ((this.Flags & AssemblyNameFlags.Retargetable) != AssemblyNameFlags.None)
				{
					stringBuilder.Append(", Retargetable=Yes");
				}
				return stringBuilder.ToString();
			}
		}

		/// <summary>Gets or sets the hash algorithm used by the assembly manifest.</summary>
		/// <returns>The hash algorithm used by the assembly manifest.</returns>
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x000808B8 File Offset: 0x0007EAB8
		// (set) Token: 0x0600221F RID: 8735 RVA: 0x000808C0 File Offset: 0x0007EAC0
		public AssemblyHashAlgorithm HashAlgorithm
		{
			get
			{
				return this.hashalg;
			}
			set
			{
				this.hashalg = value;
			}
		}

		/// <summary>Gets or sets the public and private cryptographic key pair that is used to create a strong name signature for the assembly.</summary>
		/// <returns>The public and private cryptographic key pair to be used to create a strong name for the assembly.</returns>
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06002220 RID: 8736 RVA: 0x000808C9 File Offset: 0x0007EAC9
		// (set) Token: 0x06002221 RID: 8737 RVA: 0x000808D1 File Offset: 0x0007EAD1
		public StrongNameKeyPair KeyPair
		{
			get
			{
				return this.keypair;
			}
			set
			{
				this.keypair = value;
			}
		}

		/// <summary>Gets or sets the major, minor, build, and revision numbers of the assembly.</summary>
		/// <returns>An object that represents the major, minor, build, and revision numbers of the assembly.</returns>
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06002222 RID: 8738 RVA: 0x000808DA File Offset: 0x0007EADA
		// (set) Token: 0x06002223 RID: 8739 RVA: 0x000808E4 File Offset: 0x0007EAE4
		public Version Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
				if (value == null)
				{
					this.major = (this.minor = (this.build = (this.revision = 0)));
					return;
				}
				this.major = value.Major;
				this.minor = value.Minor;
				this.build = value.Build;
				this.revision = value.Revision;
			}
		}

		/// <summary>Gets or sets the information related to the assembly's compatibility with other assemblies.</summary>
		/// <returns>A value that represents information about the assembly's compatibility with other assemblies.</returns>
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06002224 RID: 8740 RVA: 0x00080954 File Offset: 0x0007EB54
		// (set) Token: 0x06002225 RID: 8741 RVA: 0x0008095C File Offset: 0x0007EB5C
		public AssemblyVersionCompatibility VersionCompatibility
		{
			get
			{
				return this.versioncompat;
			}
			set
			{
				this.versioncompat = value;
			}
		}

		/// <summary>Returns the full name of the assembly, also known as the display name.</summary>
		/// <returns>The full name of the assembly, or the class name if the full name cannot be determined.</returns>
		// Token: 0x06002226 RID: 8742 RVA: 0x00080968 File Offset: 0x0007EB68
		public override string ToString()
		{
			string fullName = this.FullName;
			if (fullName == null)
			{
				return base.ToString();
			}
			return fullName;
		}

		/// <summary>Gets the public key of the assembly.</summary>
		/// <returns>A byte array that contains the public key of the assembly.</returns>
		/// <exception cref="T:System.Security.SecurityException">A public key was provided (for example, by using the <see cref="M:System.Reflection.AssemblyName.SetPublicKey(System.Byte[])" /> method), but no public key token was provided. </exception>
		// Token: 0x06002227 RID: 8743 RVA: 0x00080987 File Offset: 0x0007EB87
		public byte[] GetPublicKey()
		{
			return this.publicKey;
		}

		/// <summary>Gets the public key token, which is the last 8 bytes of the SHA-1 hash of the public key under which the application or assembly is signed.</summary>
		/// <returns>A byte array that contains the public key token.</returns>
		// Token: 0x06002228 RID: 8744 RVA: 0x00080990 File Offset: 0x0007EB90
		public byte[] GetPublicKeyToken()
		{
			if (this.keyToken != null)
			{
				return this.keyToken;
			}
			if (this.publicKey == null)
			{
				return null;
			}
			if (this.publicKey.Length == 0)
			{
				return EmptyArray<byte>.Value;
			}
			if (!this.IsPublicKeyValid)
			{
				throw new SecurityException("The public key is not valid.");
			}
			this.keyToken = this.ComputePublicKeyToken();
			return this.keyToken;
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06002229 RID: 8745 RVA: 0x000809EC File Offset: 0x0007EBEC
		private bool IsPublicKeyValid
		{
			get
			{
				if (this.publicKey.Length == 16)
				{
					int i = 0;
					int num = 0;
					while (i < this.publicKey.Length)
					{
						num += (int)this.publicKey[i++];
					}
					if (num == 4)
					{
						return true;
					}
				}
				byte b = this.publicKey[0];
				if (b != 0)
				{
					if (b != 6)
					{
						if (b != 7)
						{
							return false;
						}
						return false;
					}
				}
				else
				{
					if (this.publicKey.Length <= 12 || this.publicKey[12] != 6)
					{
						return false;
					}
					try
					{
						CryptoConvert.FromCapiPublicKeyBlob(this.publicKey, 12);
						return true;
					}
					catch (CryptographicException)
					{
						return false;
					}
				}
				try
				{
					CryptoConvert.FromCapiPublicKeyBlob(this.publicKey);
					return true;
				}
				catch (CryptographicException)
				{
				}
				return false;
			}
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x00080AA4 File Offset: 0x0007ECA4
		private byte[] InternalGetPublicKeyToken()
		{
			if (this.keyToken != null)
			{
				return this.keyToken;
			}
			if (this.publicKey == null)
			{
				return null;
			}
			if (this.publicKey.Length == 0)
			{
				return EmptyArray<byte>.Value;
			}
			if (!this.IsPublicKeyValid)
			{
				throw new SecurityException("The public key is not valid.");
			}
			return this.ComputePublicKeyToken();
		}

		// Token: 0x0600222B RID: 8747
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void get_public_token(byte* token, byte* pubkey, int len);

		// Token: 0x0600222C RID: 8748 RVA: 0x00080AF4 File Offset: 0x0007ECF4
		private unsafe byte[] ComputePublicKeyToken()
		{
			byte[] array2;
			byte[] array = (array2 = new byte[8]);
			byte* ptr;
			if (array == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			byte[] array3;
			byte* ptr2;
			if ((array3 = this.publicKey) == null || array3.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array3[0];
			}
			AssemblyName.get_public_token(ptr, ptr2, this.publicKey.Length);
			array3 = null;
			array2 = null;
			return array;
		}

		/// <summary>Returns a value indicating whether two assembly names are the same. The comparison is based on the simple assembly names.</summary>
		/// <returns>true if the simple assembly names are the same; otherwise, false.</returns>
		/// <param name="reference">The reference assembly name.</param>
		/// <param name="definition">The assembly name that is compared to the reference assembly.</param>
		// Token: 0x0600222D RID: 8749 RVA: 0x00080B4F File Offset: 0x0007ED4F
		public static bool ReferenceMatchesDefinition(AssemblyName reference, AssemblyName definition)
		{
			if (reference == null)
			{
				throw new ArgumentNullException("reference");
			}
			if (definition == null)
			{
				throw new ArgumentNullException("definition");
			}
			return string.Equals(reference.Name, definition.Name, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>Sets the public key identifying the assembly.</summary>
		/// <param name="publicKey">A byte array containing the public key of the assembly. </param>
		// Token: 0x0600222E RID: 8750 RVA: 0x00080B7F File Offset: 0x0007ED7F
		public void SetPublicKey(byte[] publicKey)
		{
			if (publicKey == null)
			{
				this.flags ^= AssemblyNameFlags.PublicKey;
			}
			else
			{
				this.flags |= AssemblyNameFlags.PublicKey;
			}
			this.publicKey = publicKey;
		}

		/// <summary>Sets the public key token, which is the last 8 bytes of the SHA-1 hash of the public key under which the application or assembly is signed.</summary>
		/// <param name="publicKeyToken">A byte array containing the public key token of the assembly. </param>
		// Token: 0x0600222F RID: 8751 RVA: 0x00080BA9 File Offset: 0x0007EDA9
		public void SetPublicKeyToken(byte[] publicKeyToken)
		{
			this.keyToken = publicKeyToken;
		}

		/// <summary>Gets serialization information with all the data needed to recreate an instance of this AssemblyName.</summary>
		/// <param name="info">The object to be populated with serialization information. </param>
		/// <param name="context">The destination context of the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06002230 RID: 8752 RVA: 0x00080BB4 File Offset: 0x0007EDB4
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("_Name", this.name);
			info.AddValue("_PublicKey", this.publicKey);
			info.AddValue("_PublicKeyToken", this.keyToken);
			info.AddValue("_CultureInfo", (this.cultureinfo != null) ? this.cultureinfo.LCID : (-1));
			info.AddValue("_CodeBase", this.codebase);
			info.AddValue("_Version", this.Version);
			info.AddValue("_HashAlgorithm", this.hashalg);
			info.AddValue("_HashAlgorithmForControl", AssemblyHashAlgorithm.None);
			info.AddValue("_StrongNameKeyPair", this.keypair);
			info.AddValue("_VersionCompatibility", this.versioncompat);
			info.AddValue("_Flags", this.flags);
			info.AddValue("_HashForControl", null);
		}

		/// <summary>Makes a copy of this <see cref="T:System.Reflection.AssemblyName" /> object.</summary>
		/// <returns>An object that is a copy of this <see cref="T:System.Reflection.AssemblyName" /> object.</returns>
		// Token: 0x06002231 RID: 8753 RVA: 0x00080CB8 File Offset: 0x0007EEB8
		public object Clone()
		{
			return new AssemblyName
			{
				name = this.name,
				codebase = this.codebase,
				major = this.major,
				minor = this.minor,
				build = this.build,
				revision = this.revision,
				version = this.version,
				cultureinfo = this.cultureinfo,
				flags = this.flags,
				hashalg = this.hashalg,
				keypair = this.keypair,
				publicKey = this.publicKey,
				keyToken = this.keyToken,
				versioncompat = this.versioncompat,
				processor_architecture = this.processor_architecture
			};
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event. </param>
		// Token: 0x06002232 RID: 8754 RVA: 0x00080D7E File Offset: 0x0007EF7E
		public void OnDeserialization(object sender)
		{
			this.Version = this.version;
		}

		/// <summary>Gets the <see cref="T:System.Reflection.AssemblyName" /> for a given file.</summary>
		/// <returns>An object that represents the given assembly file.</returns>
		/// <param name="assemblyFile">The path for the assembly whose <see cref="T:System.Reflection.AssemblyName" /> is to be returned. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyFile" /> is invalid, such as an assembly with an invalid culture. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have path discovery permission. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different sets of evidence. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002233 RID: 8755 RVA: 0x00080D8C File Offset: 0x0007EF8C
		public unsafe static AssemblyName GetAssemblyName(string assemblyFile)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			AssemblyName assemblyName = new AssemblyName();
			MonoAssemblyName monoAssemblyName;
			string text;
			Assembly.InternalGetAssemblyName(Path.GetFullPath(assemblyFile), out monoAssemblyName, out text);
			try
			{
				assemblyName.FillName(&monoAssemblyName, text, true, false, true, false);
			}
			finally
			{
				RuntimeMarshal.FreeAssemblyName(ref monoAssemblyName, false);
			}
			return assemblyName;
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array that receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002234 RID: 8756 RVA: 0x0002126B File Offset: 0x0001F46B
		void _AssemblyName.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002235 RID: 8757 RVA: 0x0002126B File Offset: 0x0001F46B
		void _AssemblyName.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002236 RID: 8758 RVA: 0x0002126B File Offset: 0x0001F46B
		void _AssemblyName.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DispIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002237 RID: 8759 RVA: 0x0002126B File Offset: 0x0001F46B
		void _AssemblyName.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets the name of the culture associated with the assembly.</summary>
		/// <returns>The culture name.</returns>
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06002238 RID: 8760 RVA: 0x00080DE8 File Offset: 0x0007EFE8
		// (set) Token: 0x06002239 RID: 8761 RVA: 0x0002126B File Offset: 0x0001F46B
		public string CultureName
		{
			get
			{
				if (this.cultureinfo != null)
				{
					return this.cultureinfo.Name;
				}
				return null;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that indicates what type of content the assembly contains.</summary>
		/// <returns>A value that indicates what type of content the assembly contains.</returns>
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600223A RID: 8762 RVA: 0x00080DFF File Offset: 0x0007EFFF
		// (set) Token: 0x0600223B RID: 8763 RVA: 0x00080E07 File Offset: 0x0007F007
		[ComVisible(false)]
		public AssemblyContentType ContentType
		{
			get
			{
				return this.contentType;
			}
			set
			{
				this.contentType = value;
			}
		}

		// Token: 0x0600223C RID: 8764
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern MonoAssemblyName* GetNativeName(IntPtr assembly_ptr);

		// Token: 0x0600223D RID: 8765 RVA: 0x00080E10 File Offset: 0x0007F010
		internal unsafe void FillName(MonoAssemblyName* native, string codeBase, bool addVersion, bool addPublickey, bool defaultToken, bool assemblyRef)
		{
			this.name = RuntimeMarshal.PtrToUtf8String(native->name);
			this.major = (int)native->major;
			this.minor = (int)native->minor;
			this.build = (int)native->build;
			this.revision = (int)native->revision;
			this.flags = (AssemblyNameFlags)native->flags;
			this.hashalg = (AssemblyHashAlgorithm)native->hash_alg;
			this.versioncompat = AssemblyVersionCompatibility.SameMachine;
			this.processor_architecture = (ProcessorArchitecture)native->arch;
			if (addVersion)
			{
				this.version = new Version(this.major, this.minor, this.build, this.revision);
			}
			this.codebase = codeBase;
			if (native->culture != IntPtr.Zero)
			{
				this.cultureinfo = CultureInfo.CreateCulture(RuntimeMarshal.PtrToUtf8String(native->culture), assemblyRef);
			}
			if (native->public_key != IntPtr.Zero)
			{
				this.publicKey = RuntimeMarshal.DecodeBlobArray(native->public_key);
				this.flags |= AssemblyNameFlags.PublicKey;
			}
			else if (addPublickey)
			{
				this.publicKey = EmptyArray<byte>.Value;
				this.flags |= AssemblyNameFlags.PublicKey;
			}
			if (*(&native->public_key_token.FixedElementField) != 0)
			{
				byte[] array = new byte[8];
				int i = 0;
				int num = 0;
				while (i < 8)
				{
					array[i] = (byte)(RuntimeMarshal.AsciHexDigitValue((int)(&native->public_key_token.FixedElementField)[num++]) << 4);
					byte[] array2 = array;
					int num2 = i;
					array2[num2] |= (byte)RuntimeMarshal.AsciHexDigitValue((int)(&native->public_key_token.FixedElementField)[num++]);
					i++;
				}
				this.keyToken = array;
				return;
			}
			if (defaultToken)
			{
				this.keyToken = EmptyArray<byte>.Value;
			}
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x00080FB0 File Offset: 0x0007F1B0
		internal unsafe static AssemblyName Create(Assembly assembly, bool fillCodebase)
		{
			AssemblyName assemblyName = new AssemblyName();
			MonoAssemblyName* nativeName = AssemblyName.GetNativeName(assembly._mono_assembly);
			assemblyName.FillName(nativeName, fillCodebase ? assembly.CodeBase : null, true, true, true, false);
			return assemblyName;
		}

		// Token: 0x040012F7 RID: 4855
		private string name;

		// Token: 0x040012F8 RID: 4856
		private string codebase;

		// Token: 0x040012F9 RID: 4857
		private int major;

		// Token: 0x040012FA RID: 4858
		private int minor;

		// Token: 0x040012FB RID: 4859
		private int build;

		// Token: 0x040012FC RID: 4860
		private int revision;

		// Token: 0x040012FD RID: 4861
		private CultureInfo cultureinfo;

		// Token: 0x040012FE RID: 4862
		private AssemblyNameFlags flags;

		// Token: 0x040012FF RID: 4863
		private AssemblyHashAlgorithm hashalg;

		// Token: 0x04001300 RID: 4864
		private StrongNameKeyPair keypair;

		// Token: 0x04001301 RID: 4865
		private byte[] publicKey;

		// Token: 0x04001302 RID: 4866
		private byte[] keyToken;

		// Token: 0x04001303 RID: 4867
		private AssemblyVersionCompatibility versioncompat;

		// Token: 0x04001304 RID: 4868
		private Version version;

		// Token: 0x04001305 RID: 4869
		private ProcessorArchitecture processor_architecture;

		// Token: 0x04001306 RID: 4870
		private AssemblyContentType contentType;
	}
}
