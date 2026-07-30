using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System
{
	/// <summary>
	///   <see cref="T:System.RuntimeMethodHandle" /> is a handle to the internal metadata representation of a method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200022E RID: 558
	[ComVisible(true)]
	[Serializable]
	public struct RuntimeMethodHandle : ISerializable
	{
		// Token: 0x06001A70 RID: 6768 RVA: 0x00063A8E File Offset: 0x00061C8E
		internal RuntimeMethodHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x00063A98 File Offset: 0x00061C98
		private RuntimeMethodHandle(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MonoMethod monoMethod = (MonoMethod)info.GetValue("MethodObj", typeof(MonoMethod));
			this.value = monoMethod.MethodHandle.Value;
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException(Locale.GetText("Insufficient state."));
			}
		}

		/// <summary>Gets the value of this instance.</summary>
		/// <returns>A <see cref="T:System.RuntimeMethodHandle" /> that is the internal metadata representation of a method.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x00063B04 File Offset: 0x00061D04
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data necessary to deserialize the field represented by this instance.</summary>
		/// <param name="info">The object to populate with serialization information. </param>
		/// <param name="context">(Reserved) The place to store and retrieve serialized data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">
		///   <see cref="P:System.RuntimeMethodHandle.Value" /> is invalid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A73 RID: 6771 RVA: 0x00063B0C File Offset: 0x00061D0C
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException("Object fields may not be properly initialized");
			}
			info.AddValue("MethodObj", (MonoMethod)MethodBase.GetMethodFromHandle(this), typeof(MonoMethod));
		}

		// Token: 0x06001A74 RID: 6772
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetFunctionPointer(IntPtr m);

		/// <summary>Obtains a pointer to the method represented by this instance.</summary>
		/// <returns>A pointer to the method represented by this instance.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the necessary permission to perform this operation.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A75 RID: 6773 RVA: 0x00063B69 File Offset: 0x00061D69
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public IntPtr GetFunctionPointer()
		{
			return RuntimeMethodHandle.GetFunctionPointer(this.value);
		}

		/// <summary>Indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.RuntimeMethodHandle" /> and equal to the value of this instance; otherwise, false.</returns>
		/// <param name="obj">A <see cref="T:System.Object" /> to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A76 RID: 6774 RVA: 0x00063B78 File Offset: 0x00061D78
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimeMethodHandle)obj).Value;
		}

		/// <summary>Indicates whether this instance is equal to a specified <see cref="T:System.RuntimeMethodHandle" />.</summary>
		/// <returns>true if <paramref name="handle" /> is equal to the value of this instance; otherwise, false.</returns>
		/// <param name="handle">A <see cref="T:System.RuntimeMethodHandle" /> to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A77 RID: 6775 RVA: 0x00063BC0 File Offset: 0x00061DC0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public bool Equals(RuntimeMethodHandle handle)
		{
			return this.value == handle.Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A78 RID: 6776 RVA: 0x00063BD4 File Offset: 0x00061DD4
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		/// <summary>Indicates whether two instances of <see cref="T:System.RuntimeMethodHandle" /> are equal.</summary>
		/// <returns>true if the value of <paramref name="left" /> is equal to the value of <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">A <see cref="T:System.RuntimeMethodHandle" /> to compare to <paramref name="right" />.</param>
		/// <param name="right">A <see cref="T:System.RuntimeMethodHandle" /> to compare to <paramref name="left" />.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001A79 RID: 6777 RVA: 0x00063BE1 File Offset: 0x00061DE1
		public static bool operator ==(RuntimeMethodHandle left, RuntimeMethodHandle right)
		{
			return left.Equals(right);
		}

		/// <summary>Indicates whether two instances of <see cref="T:System.RuntimeMethodHandle" /> are not equal.</summary>
		/// <returns>true if the value of <paramref name="left" /> is unequal to the value of <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">A <see cref="T:System.RuntimeMethodHandle" /> to compare to <paramref name="right" />.</param>
		/// <param name="right">A <see cref="T:System.RuntimeMethodHandle" /> to compare to <paramref name="left" />.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001A7A RID: 6778 RVA: 0x00063BEB File Offset: 0x00061DEB
		public static bool operator !=(RuntimeMethodHandle left, RuntimeMethodHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00063BF8 File Offset: 0x00061DF8
		internal static string ConstructInstantiation(RuntimeMethodInfo method, TypeNameFormatFlags format)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Type[] genericArguments = method.GetGenericArguments();
			stringBuilder.Append("[");
			for (int i = 0; i < genericArguments.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(genericArguments[i].Name);
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x00063C5D File Offset: 0x00061E5D
		internal bool IsNullHandle()
		{
			return this.value == IntPtr.Zero;
		}

		// Token: 0x04000D1E RID: 3358
		private IntPtr value;
	}
}
