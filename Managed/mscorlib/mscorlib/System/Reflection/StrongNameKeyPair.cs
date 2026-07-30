using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Permissions;
using Mono.Security;
using Mono.Security.Cryptography;

namespace System.Reflection
{
	/// <summary>Encapsulates access to a public or private key pair used to sign strong name assemblies.</summary>
	// Token: 0x0200033E RID: 830
	[ComVisible(true)]
	[Serializable]
	public class StrongNameKeyPair : ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.StrongNameKeyPair" /> class, building the key pair from a byte array.</summary>
		/// <param name="keyPairArray">An array of type byte containing the key pair. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyPairArray" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060024A1 RID: 9377 RVA: 0x0008417A File Offset: 0x0008237A
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public StrongNameKeyPair(byte[] keyPairArray)
		{
			if (keyPairArray == null)
			{
				throw new ArgumentNullException("keyPairArray");
			}
			this.LoadKey(keyPairArray);
			this.GetRSA();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.StrongNameKeyPair" /> class, building the key pair from a FileStream.</summary>
		/// <param name="keyPairFile">A FileStream containing the key pair. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyPairFile" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060024A2 RID: 9378 RVA: 0x000841A0 File Offset: 0x000823A0
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public StrongNameKeyPair(FileStream keyPairFile)
		{
			if (keyPairFile == null)
			{
				throw new ArgumentNullException("keyPairFile");
			}
			byte[] array = new byte[keyPairFile.Length];
			keyPairFile.Read(array, 0, array.Length);
			this.LoadKey(array);
			this.GetRSA();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.StrongNameKeyPair" /> class, building the key pair from a String.</summary>
		/// <param name="keyPairContainer">A string containing the key pair. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyPairContainer" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060024A3 RID: 9379 RVA: 0x000841E8 File Offset: 0x000823E8
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public StrongNameKeyPair(string keyPairContainer)
		{
			if (keyPairContainer == null)
			{
				throw new ArgumentNullException("keyPairContainer");
			}
			this._keyPairContainer = keyPairContainer;
			this.GetRSA();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.StrongNameKeyPair" /> class, building the key pair from serialized data.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x060024A4 RID: 9380 RVA: 0x0008420C File Offset: 0x0008240C
		protected StrongNameKeyPair(SerializationInfo info, StreamingContext context)
		{
			this._publicKey = (byte[])info.GetValue("_publicKey", typeof(byte[]));
			this._keyPairContainer = info.GetString("_keyPairContainer");
			this._keyPairExported = info.GetBoolean("_keyPairExported");
			this._keyPairArray = (byte[])info.GetValue("_keyPairArray", typeof(byte[]));
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with all the data required to reinstantiate the current <see cref="T:System.Reflection.StrongNameKeyPair" /> object.</summary>
		/// <param name="info">The object to be populated with serialization information.</param>
		/// <param name="context">The destination context of the serialization.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x060024A5 RID: 9381 RVA: 0x00084284 File Offset: 0x00082484
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_publicKey", this._publicKey, typeof(byte[]));
			info.AddValue("_keyPairContainer", this._keyPairContainer);
			info.AddValue("_keyPairExported", this._keyPairExported);
			info.AddValue("_keyPairArray", this._keyPairArray, typeof(byte[]));
		}

		/// <summary>Runs when the entire object graph has been deserialized.</summary>
		/// <param name="sender">The object that initiated the callback.</param>
		// Token: 0x060024A6 RID: 9382 RVA: 0x00002194 File Offset: 0x00000394
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000842EC File Offset: 0x000824EC
		private RSA GetRSA()
		{
			if (this._rsa != null)
			{
				return this._rsa;
			}
			if (this._keyPairArray != null)
			{
				try
				{
					this._rsa = CryptoConvert.FromCapiKeyBlob(this._keyPairArray);
					goto IL_005A;
				}
				catch
				{
					this._keyPairArray = null;
					goto IL_005A;
				}
			}
			if (this._keyPairContainer != null)
			{
				this._rsa = new RSACryptoServiceProvider(new CspParameters
				{
					KeyContainerName = this._keyPairContainer
				});
			}
			IL_005A:
			return this._rsa;
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x0008436C File Offset: 0x0008256C
		private void LoadKey(byte[] key)
		{
			try
			{
				if (key.Length == 16)
				{
					int i = 0;
					int num = 0;
					while (i < key.Length)
					{
						num += (int)key[i++];
					}
					if (num == 4)
					{
						this._publicKey = (byte[])key.Clone();
					}
				}
				else
				{
					this._keyPairArray = key;
				}
			}
			catch
			{
			}
		}

		/// <summary>Gets the public part of the public key or public key token of the key pair.</summary>
		/// <returns>An array of type byte containing the public key or public key token of the key pair.</returns>
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060024A9 RID: 9385 RVA: 0x000843CC File Offset: 0x000825CC
		public byte[] PublicKey
		{
			get
			{
				if (this._publicKey == null)
				{
					RSA rsa = this.GetRSA();
					if (rsa == null)
					{
						throw new ArgumentException("invalid keypair");
					}
					byte[] array = CryptoConvert.ToCapiKeyBlob(rsa, false);
					this._publicKey = new byte[array.Length + 12];
					this._publicKey[0] = 0;
					this._publicKey[1] = 36;
					this._publicKey[2] = 0;
					this._publicKey[3] = 0;
					this._publicKey[4] = 4;
					this._publicKey[5] = 128;
					this._publicKey[6] = 0;
					this._publicKey[7] = 0;
					int num = array.Length;
					this._publicKey[8] = (byte)(num % 256);
					this._publicKey[9] = (byte)(num / 256);
					this._publicKey[10] = 0;
					this._publicKey[11] = 0;
					Buffer.BlockCopy(array, 0, this._publicKey, 12, array.Length);
				}
				return this._publicKey;
			}
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000844B0 File Offset: 0x000826B0
		internal StrongName StrongName()
		{
			RSA rsa = this.GetRSA();
			if (rsa != null)
			{
				return new StrongName(rsa);
			}
			if (this._publicKey != null)
			{
				return new StrongName(this._publicKey);
			}
			return null;
		}

		// Token: 0x04001372 RID: 4978
		private byte[] _publicKey;

		// Token: 0x04001373 RID: 4979
		private string _keyPairContainer;

		// Token: 0x04001374 RID: 4980
		private bool _keyPairExported;

		// Token: 0x04001375 RID: 4981
		private byte[] _keyPairArray;

		// Token: 0x04001376 RID: 4982
		[NonSerialized]
		private RSA _rsa;
	}
}
