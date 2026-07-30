using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Ntlm
{
	// Token: 0x0200006C RID: 108
	[Obsolete("Use of this API is highly discouraged, it selects legacy-mode LM/NTLM authentication, which sends your password in very weak encryption over the wire even if the server supports the more secure NTLMv2 / NTLMv2 Session. You need to use the new `Type3Message (Type2Message)' constructor to use the more secure NTLMv2 / NTLMv2 Session authentication modes. These require the Type 2 message from the server to compute the response.")]
	public class ChallengeResponse : IDisposable
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x00015756 File Offset: 0x00013956
		public ChallengeResponse()
		{
			this._disposed = false;
			this._lmpwd = new byte[21];
			this._ntpwd = new byte[21];
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001577F File Offset: 0x0001397F
		public ChallengeResponse(string password, byte[] challenge)
			: this()
		{
			this.Password = password;
			this.Challenge = challenge;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00015798 File Offset: 0x00013998
		~ChallengeResponse()
		{
			if (!this._disposed)
			{
				this.Dispose();
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x000157CC File Offset: 0x000139CC
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x000157D0 File Offset: 0x000139D0
		public string Password
		{
			get
			{
				return null;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("too late");
				}
				DES des = DES.Create();
				des.Mode = CipherMode.ECB;
				if (value == null || value.Length < 1)
				{
					Buffer.BlockCopy(ChallengeResponse.nullEncMagic, 0, this._lmpwd, 0, 8);
				}
				else
				{
					des.Key = this.PasswordToKey(value, 0);
					des.CreateEncryptor().TransformBlock(ChallengeResponse.magic, 0, 8, this._lmpwd, 0);
				}
				if (value == null || value.Length < 8)
				{
					Buffer.BlockCopy(ChallengeResponse.nullEncMagic, 0, this._lmpwd, 8, 8);
				}
				else
				{
					des.Key = this.PasswordToKey(value, 7);
					des.CreateEncryptor().TransformBlock(ChallengeResponse.magic, 0, 8, this._lmpwd, 8);
				}
				HashAlgorithm hashAlgorithm = MD4.Create();
				byte[] array = ((value == null) ? new byte[0] : Encoding.Unicode.GetBytes(value));
				byte[] array2 = hashAlgorithm.ComputeHash(array);
				Buffer.BlockCopy(array2, 0, this._ntpwd, 0, 16);
				Array.Clear(array, 0, array.Length);
				Array.Clear(array2, 0, array2.Length);
				des.Clear();
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x000158DC File Offset: 0x00013ADC
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x000158DF File Offset: 0x00013ADF
		public byte[] Challenge
		{
			get
			{
				return null;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Challenge");
				}
				if (this._disposed)
				{
					throw new ObjectDisposedException("too late");
				}
				this._challenge = (byte[])value.Clone();
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00015913 File Offset: 0x00013B13
		public byte[] LM
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("too late");
				}
				return this.GetResponse(this._lmpwd);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00015934 File Offset: 0x00013B34
		public byte[] NT
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("too late");
				}
				return this.GetResponse(this._ntpwd);
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00015955 File Offset: 0x00013B55
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00015964 File Offset: 0x00013B64
		private void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				Array.Clear(this._lmpwd, 0, this._lmpwd.Length);
				Array.Clear(this._ntpwd, 0, this._ntpwd.Length);
				if (this._challenge != null)
				{
					Array.Clear(this._challenge, 0, this._challenge.Length);
				}
				this._disposed = true;
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000159C4 File Offset: 0x00013BC4
		private byte[] GetResponse(byte[] pwd)
		{
			byte[] array = new byte[24];
			DES des = DES.Create();
			des.Mode = CipherMode.ECB;
			des.Key = this.PrepareDESKey(pwd, 0);
			des.CreateEncryptor().TransformBlock(this._challenge, 0, 8, array, 0);
			des.Key = this.PrepareDESKey(pwd, 7);
			des.CreateEncryptor().TransformBlock(this._challenge, 0, 8, array, 8);
			des.Key = this.PrepareDESKey(pwd, 14);
			des.CreateEncryptor().TransformBlock(this._challenge, 0, 8, array, 16);
			return array;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00015A54 File Offset: 0x00013C54
		private byte[] PrepareDESKey(byte[] key56bits, int position)
		{
			return new byte[]
			{
				key56bits[position],
				(byte)(((int)key56bits[position] << 7) | (key56bits[position + 1] >> 1)),
				(byte)(((int)key56bits[position + 1] << 6) | (key56bits[position + 2] >> 2)),
				(byte)(((int)key56bits[position + 2] << 5) | (key56bits[position + 3] >> 3)),
				(byte)(((int)key56bits[position + 3] << 4) | (key56bits[position + 4] >> 4)),
				(byte)(((int)key56bits[position + 4] << 3) | (key56bits[position + 5] >> 5)),
				(byte)(((int)key56bits[position + 5] << 2) | (key56bits[position + 6] >> 6)),
				(byte)(key56bits[position + 6] << 1)
			};
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00015AE8 File Offset: 0x00013CE8
		private byte[] PasswordToKey(string password, int position)
		{
			byte[] array = new byte[7];
			int num = Math.Min(password.Length - position, 7);
			Encoding.ASCII.GetBytes(password.ToUpper(CultureInfo.CurrentCulture), position, num, array, 0);
			byte[] array2 = this.PrepareDESKey(array, 0);
			Array.Clear(array, 0, array.Length);
			return array2;
		}

		// Token: 0x040001F1 RID: 497
		private static byte[] magic = new byte[] { 75, 71, 83, 33, 64, 35, 36, 37 };

		// Token: 0x040001F2 RID: 498
		private static byte[] nullEncMagic = new byte[] { 170, 211, 180, 53, 181, 20, 4, 238 };

		// Token: 0x040001F3 RID: 499
		private bool _disposed;

		// Token: 0x040001F4 RID: 500
		private byte[] _challenge;

		// Token: 0x040001F5 RID: 501
		private byte[] _lmpwd;

		// Token: 0x040001F6 RID: 502
		private byte[] _ntpwd;
	}
}
