using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents a field using an internal metadata token.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200022D RID: 557
	[ComVisible(true)]
	[Serializable]
	public struct RuntimeFieldHandle : ISerializable
	{
		// Token: 0x06001A63 RID: 6755 RVA: 0x00063918 File Offset: 0x00061B18
		internal RuntimeFieldHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00063924 File Offset: 0x00061B24
		private RuntimeFieldHandle(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MonoField monoField = (MonoField)info.GetValue("FieldObj", typeof(MonoField));
			this.value = monoField.FieldHandle.Value;
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException(Locale.GetText("Insufficient state."));
			}
		}

		/// <summary>Gets a handle to the field represented by the current instance.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that contains the handle to the field represented by the current instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x00063990 File Offset: 0x00061B90
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data necessary to deserialize the field represented by the current instance.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with serialization information. </param>
		/// <param name="context">(Reserved) The place to store and retrieve serialized data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="P:System.RuntimeFieldHandle.Value" /> property of the current instance is not a valid handle. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A66 RID: 6758 RVA: 0x00063998 File Offset: 0x00061B98
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
			info.AddValue("FieldObj", (MonoField)FieldInfo.GetFieldFromHandle(this), typeof(MonoField));
		}

		/// <summary>Indicates whether the current instance is equal to the specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.RuntimeFieldHandle" /> and equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The object to compare to the current instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A67 RID: 6759 RVA: 0x000639F8 File Offset: 0x00061BF8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimeFieldHandle)obj).Value;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.RuntimeFieldHandle" />.</summary>
		/// <returns>true if the value of <paramref name="handle" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="handle">The <see cref="T:System.RuntimeFieldHandle" /> to compare to the current instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A68 RID: 6760 RVA: 0x00063A40 File Offset: 0x00061C40
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public bool Equals(RuntimeFieldHandle handle)
		{
			return this.value == handle.Value;
		}

		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A69 RID: 6761 RVA: 0x00063A54 File Offset: 0x00061C54
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.RuntimeFieldHandle" /> structures are equal.</summary>
		/// <returns>true if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.RuntimeFieldHandle" /> to compare to <paramref name="right" />.</param>
		/// <param name="right">The <see cref="T:System.RuntimeFieldHandle" /> to compare to <paramref name="left" />.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001A6A RID: 6762 RVA: 0x00063A61 File Offset: 0x00061C61
		public static bool operator ==(RuntimeFieldHandle left, RuntimeFieldHandle right)
		{
			return left.Equals(right);
		}

		/// <summary>Indicates whether two <see cref="T:System.RuntimeFieldHandle" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.RuntimeFieldHandle" /> to compare to <paramref name="right" />.</param>
		/// <param name="right">The <see cref="T:System.RuntimeFieldHandle" /> to compare to <paramref name="left" />.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001A6B RID: 6763 RVA: 0x00063A6B File Offset: 0x00061C6B
		public static bool operator !=(RuntimeFieldHandle left, RuntimeFieldHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001A6C RID: 6764
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetValueInternal(FieldInfo fi, object obj, object value);

		// Token: 0x06001A6D RID: 6765 RVA: 0x00063A78 File Offset: 0x00061C78
		internal static void SetValue(RtFieldInfo field, object obj, object value, RuntimeType fieldType, FieldAttributes fieldAttr, RuntimeType declaringType, ref bool domainInitialized)
		{
			RuntimeFieldHandle.SetValueInternal(field, obj, value);
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x00063A82 File Offset: 0x00061C82
		internal unsafe static object GetValueDirect(RtFieldInfo field, RuntimeType fieldType, void* pTypedRef, RuntimeType contextType)
		{
			throw new NotImplementedException("GetValueDirect");
		}

		// Token: 0x06001A6F RID: 6767
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void SetValueDirect(RtFieldInfo field, RuntimeType fieldType, void* pTypedRef, object value, RuntimeType contextType);

		// Token: 0x04000D1D RID: 3357
		private IntPtr value;
	}
}
