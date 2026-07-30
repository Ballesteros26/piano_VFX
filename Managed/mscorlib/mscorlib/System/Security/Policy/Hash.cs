using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using Unity;

namespace System.Security.Policy
{
	/// <summary>Provides evidence about the hash value for an assembly. This class cannot be inherited.</summary>
	// Token: 0x02000569 RID: 1385
	[ComVisible(true)]
	[Serializable]
	public sealed class Hash : EvidenceBase, ISerializable, IBuiltInEvidence
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Hash" /> class.</summary>
		/// <param name="assembly">The assembly for which to compute the hash value. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="assembly" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The hash cannot be generated.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assembly" /> is not a run-time <see cref="T:System.Reflection.Assembly" /> object.</exception>
		// Token: 0x06003E32 RID: 15922 RVA: 0x000DEE7D File Offset: 0x000DD07D
		public Hash(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			this.assembly = assembly;
		}

		// Token: 0x06003E33 RID: 15923 RVA: 0x000DED7F File Offset: 0x000DCF7F
		internal Hash()
		{
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x000DEEA0 File Offset: 0x000DD0A0
		internal Hash(SerializationInfo info, StreamingContext context)
		{
			this.data = (byte[])info.GetValue("RawData", typeof(byte[]));
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.MD5" /> hash value for the assembly.</summary>
		/// <returns>A byte array that represents the <see cref="T:System.Security.Cryptography.MD5" /> hash value for the assembly.</returns>
		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06003E35 RID: 15925 RVA: 0x000DEEC8 File Offset: 0x000DD0C8
		public byte[] MD5
		{
			get
			{
				if (this._md5 != null)
				{
					return this._md5;
				}
				if (this.assembly == null && this._sha1 != null)
				{
					throw new SecurityException(Locale.GetText("No assembly data. This instance was initialized with an MSHA1 digest value."));
				}
				HashAlgorithm hashAlgorithm = global::System.Security.Cryptography.MD5.Create();
				this._md5 = this.GenerateHash(hashAlgorithm);
				return this._md5;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.SHA1" /> hash value for the assembly.</summary>
		/// <returns>A byte array that represents the <see cref="T:System.Security.Cryptography.SHA1" /> hash value for the assembly.</returns>
		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06003E36 RID: 15926 RVA: 0x000DEF24 File Offset: 0x000DD124
		public byte[] SHA1
		{
			get
			{
				if (this._sha1 != null)
				{
					return this._sha1;
				}
				if (this.assembly == null && this._md5 != null)
				{
					throw new SecurityException(Locale.GetText("No assembly data. This instance was initialized with an MD5 digest value."));
				}
				HashAlgorithm hashAlgorithm = global::System.Security.Cryptography.SHA1.Create();
				this._sha1 = this.GenerateHash(hashAlgorithm);
				return this._sha1;
			}
		}

		/// <summary>Computes the hash value for the assembly using the specified hash algorithm.</summary>
		/// <returns>A byte array that represents the hash value for the assembly.</returns>
		/// <param name="hashAlg">The hash algorithm to use to compute the hash value for the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hashAlg" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The hash value for the assembly cannot be generated.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003E37 RID: 15927 RVA: 0x000DEF7F File Offset: 0x000DD17F
		public byte[] GenerateHash(HashAlgorithm hashAlg)
		{
			if (hashAlg == null)
			{
				throw new ArgumentNullException("hashAlg");
			}
			return hashAlg.ComputeHash(this.GetData());
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003E38 RID: 15928 RVA: 0x000DEF9B File Offset: 0x000DD19B
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("RawData", this.GetData());
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Hash" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.Hash" />.</returns>
		// Token: 0x06003E39 RID: 15929 RVA: 0x000DEFBC File Offset: 0x000DD1BC
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement(base.GetType().FullName);
			securityElement.AddAttribute("version", "1");
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array = this.GetData();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("X2"));
			}
			securityElement.AddChild(new SecurityElement("RawData", stringBuilder.ToString()));
			return securityElement.ToString();
		}

		// Token: 0x06003E3A RID: 15930 RVA: 0x000DF03C File Offset: 0x000DD23C
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private byte[] GetData()
		{
			if (this.assembly == null && this.data == null)
			{
				throw new SecurityException(Locale.GetText("No assembly data."));
			}
			if (this.data == null)
			{
				FileStream fileStream = new FileStream(this.assembly.Location, FileMode.Open, FileAccess.Read);
				this.data = new byte[fileStream.Length];
				fileStream.Read(this.data, 0, (int)fileStream.Length);
			}
			return this.data;
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x000DF0B7 File Offset: 0x000DD2B7
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			if (!verbose)
			{
				return 0;
			}
			return 5;
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06003E3D RID: 15933 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		/// <summary>Creates a <see cref="T:System.Security.Policy.Hash" /> object that contains an <see cref="T:System.Security.Cryptography.MD5" /> hash value.</summary>
		/// <returns>An object that contains the hash value provided by the <paramref name="md5" /> parameter.</returns>
		/// <param name="md5">A byte array that contains an <see cref="T:System.Security.Cryptography.MD5" /> hash value.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="md5" /> parameter is null. </exception>
		// Token: 0x06003E3E RID: 15934 RVA: 0x000DF0BF File Offset: 0x000DD2BF
		public static Hash CreateMD5(byte[] md5)
		{
			if (md5 == null)
			{
				throw new ArgumentNullException("md5");
			}
			return new Hash
			{
				_md5 = md5
			};
		}

		/// <summary>Creates a <see cref="T:System.Security.Policy.Hash" /> object that contains a <see cref="T:System.Security.Cryptography.SHA1" /> hash value.</summary>
		/// <returns>An object that contains the hash value provided by the <paramref name="sha1" /> parameter.</returns>
		/// <param name="sha1">A byte array that contains a <see cref="T:System.Security.Cryptography.SHA1" /> hash value.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sha1" /> parameter is null. </exception>
		// Token: 0x06003E3F RID: 15935 RVA: 0x000DF0DB File Offset: 0x000DD2DB
		public static Hash CreateSHA1(byte[] sha1)
		{
			if (sha1 == null)
			{
				throw new ArgumentNullException("sha1");
			}
			return new Hash
			{
				_sha1 = sha1
			};
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.SHA256" /> hash value for the assembly.</summary>
		/// <returns>A byte array that represents the <see cref="T:System.Security.Cryptography.SHA256" /> hash value for the assembly.</returns>
		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06003E40 RID: 15936 RVA: 0x00032521 File Offset: 0x00030721
		public byte[] SHA256
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Creates a <see cref="T:System.Security.Policy.Hash" /> object that contains a <see cref="T:System.Security.Cryptography.SHA256" /> hash value.</summary>
		/// <returns>A hash object that contains the hash value provided by the <paramref name="sha256" /> parameter.</returns>
		/// <param name="sha256">A byte array that contains a <see cref="T:System.Security.Cryptography.SHA256" /> hash value.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sha256" /> parameter is null. </exception>
		// Token: 0x06003E41 RID: 15937 RVA: 0x00032521 File Offset: 0x00030721
		public static Hash CreateSHA256(byte[] sha256)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x04001FDA RID: 8154
		private Assembly assembly;

		// Token: 0x04001FDB RID: 8155
		private byte[] data;

		// Token: 0x04001FDC RID: 8156
		internal byte[] _md5;

		// Token: 0x04001FDD RID: 8157
		internal byte[] _sha1;
	}
}
