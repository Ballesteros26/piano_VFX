using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Assemblies
{
	/// <summary>Represents a hash of an assembly manifest's contents.</summary>
	// Token: 0x02000260 RID: 608
	[ComVisible(true)]
	[Obsolete]
	[Serializable]
	public struct AssemblyHash : ICloneable
	{
		/// <summary>Gets or sets the hash algorithm.</summary>
		/// <returns>An assembly hash algorithm.</returns>
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001C15 RID: 7189 RVA: 0x000698D4 File Offset: 0x00067AD4
		// (set) Token: 0x06001C16 RID: 7190 RVA: 0x000698DC File Offset: 0x00067ADC
		[Obsolete]
		public AssemblyHashAlgorithm Algorithm
		{
			get
			{
				return this._algorithm;
			}
			set
			{
				this._algorithm = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Assemblies.AssemblyHash" /> structure with the specified hash algorithm and the hash value.</summary>
		/// <param name="algorithm">The algorithm used to generate the hash. Values for this parameter come from the <see cref="T:System.Configuration.Assemblies.AssemblyHashAlgorithm" /> enumeration. </param>
		/// <param name="value">The hash value. </param>
		// Token: 0x06001C17 RID: 7191 RVA: 0x000698E5 File Offset: 0x00067AE5
		[Obsolete]
		public AssemblyHash(AssemblyHashAlgorithm algorithm, byte[] value)
		{
			this._algorithm = algorithm;
			if (value != null)
			{
				this._value = (byte[])value.Clone();
				return;
			}
			this._value = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Assemblies.AssemblyHash" /> structure with the specified hash value. The hash algorithm defaults to <see cref="F:System.Configuration.Assemblies.AssemblyHashAlgorithm.SHA1" />.</summary>
		/// <param name="value">The hash value. </param>
		// Token: 0x06001C18 RID: 7192 RVA: 0x0006990A File Offset: 0x00067B0A
		[Obsolete]
		public AssemblyHash(byte[] value)
		{
			this = new AssemblyHash(AssemblyHashAlgorithm.SHA1, value);
		}

		/// <summary>Clones this object.</summary>
		/// <returns>An exact copy of this object.</returns>
		// Token: 0x06001C19 RID: 7193 RVA: 0x00069918 File Offset: 0x00067B18
		[Obsolete]
		public object Clone()
		{
			return new AssemblyHash(this._algorithm, this._value);
		}

		/// <summary>Gets the hash value.</summary>
		/// <returns>The hash value.</returns>
		// Token: 0x06001C1A RID: 7194 RVA: 0x00069930 File Offset: 0x00067B30
		[Obsolete]
		public byte[] GetValue()
		{
			return this._value;
		}

		/// <summary>Sets the hash value.</summary>
		/// <param name="value">The hash value. </param>
		// Token: 0x06001C1B RID: 7195 RVA: 0x00069938 File Offset: 0x00067B38
		[Obsolete]
		public void SetValue(byte[] value)
		{
			this._value = value;
		}

		// Token: 0x04000FA8 RID: 4008
		private AssemblyHashAlgorithm _algorithm;

		// Token: 0x04000FA9 RID: 4009
		private byte[] _value;

		/// <summary>An empty <see cref="T:System.Configuration.Assemblies.AssemblyHash" /> object.</summary>
		// Token: 0x04000FAA RID: 4010
		[Obsolete]
		public static readonly AssemblyHash Empty = new AssemblyHash(AssemblyHashAlgorithm.None, null);
	}
}
