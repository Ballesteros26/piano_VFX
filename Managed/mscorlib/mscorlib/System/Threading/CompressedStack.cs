using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using Unity;

namespace System.Threading
{
	/// <summary>Provides methods for setting and capturing the compressed stack on the current thread. This class cannot be inherited. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020004A1 RID: 1185
	[Serializable]
	public sealed class CompressedStack : ISerializable
	{
		// Token: 0x060037A2 RID: 14242 RVA: 0x000CAC34 File Offset: 0x000C8E34
		internal CompressedStack(int length)
		{
			if (length > 0)
			{
				this._list = new ArrayList(length);
			}
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000CAC4C File Offset: 0x000C8E4C
		internal CompressedStack(CompressedStack cs)
		{
			if (cs != null && cs._list != null)
			{
				this._list = (ArrayList)cs._list.Clone();
			}
		}

		/// <summary>Creates a copy of the current compressed stack.</summary>
		/// <returns>A <see cref="T:System.Threading.CompressedStack" /> object representing the current compressed stack.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060037A4 RID: 14244 RVA: 0x000CAC75 File Offset: 0x000C8E75
		[ComVisible(false)]
		public CompressedStack CreateCopy()
		{
			return new CompressedStack(this);
		}

		/// <summary>Captures the compressed stack from the current thread.</summary>
		/// <returns>A <see cref="T:System.Threading.CompressedStack" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060037A5 RID: 14245 RVA: 0x00014B5A File Offset: 0x00012D5A
		public static CompressedStack Capture()
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the compressed stack for the current thread.</summary>
		/// <returns>A <see cref="T:System.Threading.CompressedStack" /> for the current thread.</returns>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.-or-The request for <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> failed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="00000000000000000400000000000000" />
		/// </PermissionSet>
		// Token: 0x060037A6 RID: 14246 RVA: 0x00014B5A File Offset: 0x00012D5A
		[SecurityCritical]
		public static CompressedStack GetCompressedStack()
		{
			throw new NotSupportedException();
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the logical context information needed to recreate an instance of this execution context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to be populated with serialization information. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure representing the destination context of the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060037A7 RID: 14247 RVA: 0x0005CEE0 File Offset: 0x0005B0E0
		[MonoTODO("incomplete")]
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		/// <summary>Runs a method in the specified compressed stack on the current thread.</summary>
		/// <param name="compressedStack">The <see cref="T:System.Threading.CompressedStack" /> to set.</param>
		/// <param name="callback">A <see cref="T:System.Threading.ContextCallback" /> that represents the method to be run in the specified security context.</param>
		/// <param name="state">The object to be passed to the callback method.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="compressedStack" /> is null.</exception>
		// Token: 0x060037A8 RID: 14248 RVA: 0x00014B5A File Offset: 0x00012D5A
		[SecurityCritical]
		public static void Run(CompressedStack compressedStack, ContextCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x000CAC7D File Offset: 0x000C8E7D
		internal bool Equals(CompressedStack cs)
		{
			if (this.IsEmpty())
			{
				return cs.IsEmpty();
			}
			return !cs.IsEmpty() && this._list.Count == cs._list.Count;
		}

		// Token: 0x060037AA RID: 14250 RVA: 0x000CACB3 File Offset: 0x000C8EB3
		internal bool IsEmpty()
		{
			return this._list == null || this._list.Count == 0;
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060037AB RID: 14251 RVA: 0x000CACCD File Offset: 0x000C8ECD
		internal IList List
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal CompressedStack()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001D3C RID: 7484
		private ArrayList _list;
	}
}
