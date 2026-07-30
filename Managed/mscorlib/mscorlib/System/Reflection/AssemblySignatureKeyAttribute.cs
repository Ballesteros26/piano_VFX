using System;

namespace System.Reflection
{
	/// <summary>Provides migration from an older, simpler strong name key to a larger key with a stronger hashing algorithm.</summary>
	// Token: 0x020002D2 RID: 722
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	public sealed class AssemblySignatureKeyAttribute : Attribute
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Reflection.AssemblySignatureKeyAttribute" /> class by using the specified public key and countersignature.</summary>
		/// <param name="publicKey">The public or identity key.</param>
		/// <param name="countersignature">The countersignature, which is the signature key portion of the strong-name key.</param>
		// Token: 0x06002046 RID: 8262 RVA: 0x0007DF86 File Offset: 0x0007C186
		public AssemblySignatureKeyAttribute(string publicKey, string countersignature)
		{
			this._publicKey = publicKey;
			this._countersignature = countersignature;
		}

		/// <summary>Gets the public key for the strong name used to sign the assembly.</summary>
		/// <returns>The public key for this assembly.</returns>
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06002047 RID: 8263 RVA: 0x0007DF9C File Offset: 0x0007C19C
		public string PublicKey
		{
			get
			{
				return this._publicKey;
			}
		}

		/// <summary>Gets the countersignature for the strong name for this assembly.</summary>
		/// <returns>The countersignature for this signature key.</returns>
		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06002048 RID: 8264 RVA: 0x0007DFA4 File Offset: 0x0007C1A4
		public string Countersignature
		{
			get
			{
				return this._countersignature;
			}
		}

		// Token: 0x04001173 RID: 4467
		private string _publicKey;

		// Token: 0x04001174 RID: 4468
		private string _countersignature;
	}
}
