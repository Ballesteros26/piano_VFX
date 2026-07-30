using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents a type using an internal metadata token.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200022F RID: 559
	[ComVisible(true)]
	[Serializable]
	public struct RuntimeTypeHandle : ISerializable
	{
		// Token: 0x06001A7D RID: 6781 RVA: 0x00063C6F File Offset: 0x00061E6F
		internal RuntimeTypeHandle(IntPtr val)
		{
			this.value = val;
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x00063C78 File Offset: 0x00061E78
		internal RuntimeTypeHandle(RuntimeType type)
		{
			this = new RuntimeTypeHandle(type._impl.value);
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x00063C8C File Offset: 0x00061E8C
		private RuntimeTypeHandle(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			RuntimeType runtimeType = (RuntimeType)info.GetValue("TypeObj", typeof(RuntimeType));
			this.value = runtimeType.TypeHandle.Value;
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException(Locale.GetText("Insufficient state."));
			}
		}

		/// <summary>Gets a handle to the type represented by this instance.</summary>
		/// <returns>A handle to the type represented by this instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x00063CF8 File Offset: 0x00061EF8
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data necessary to deserialize the type represented by the current instance.</summary>
		/// <param name="info">The object to be populated with serialization information. </param>
		/// <param name="context">(Reserved) The location where serialized data will be stored and retrieved. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">
		///   <see cref="P:System.RuntimeTypeHandle.Value" /> is invalid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A81 RID: 6785 RVA: 0x00063D00 File Offset: 0x00061F00
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
			info.AddValue("TypeObj", Type.GetTypeHandle(this), typeof(RuntimeType));
		}

		/// <summary>Indicates whether the specified object is equal to the current <see cref="T:System.RuntimeTypeHandle" /> structure.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.RuntimeTypeHandle" /> structure and is equal to the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare to the current instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A82 RID: 6786 RVA: 0x00063D64 File Offset: 0x00061F64
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimeTypeHandle)obj).Value;
		}

		/// <summary>Indicates whether the specified <see cref="T:System.RuntimeTypeHandle" /> structure is equal to the current <see cref="T:System.RuntimeTypeHandle" /> structure.</summary>
		/// <returns>true if the value of <paramref name="handle" /> is equal to the value of this instance; otherwise, false.</returns>
		/// <param name="handle">The <see cref="T:System.RuntimeTypeHandle" /> structure to compare to the current instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A83 RID: 6787 RVA: 0x00063DAC File Offset: 0x00061FAC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public bool Equals(RuntimeTypeHandle handle)
		{
			return this.value == handle.Value;
		}

		/// <summary>Returns the hash code for the current instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001A84 RID: 6788 RVA: 0x00063DC0 File Offset: 0x00061FC0
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		/// <summary>Indicates whether a <see cref="T:System.RuntimeTypeHandle" /> structure is equal to an object.</summary>
		/// <returns>true if <paramref name="right" /> is a <see cref="T:System.RuntimeTypeHandle" /> and is equal to <paramref name="left" />; otherwise, false.</returns>
		/// <param name="left">A <see cref="T:System.RuntimeTypeHandle" /> structure to compare to <paramref name="right" />.</param>
		/// <param name="right">An object to compare to <paramref name="left" />.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001A85 RID: 6789 RVA: 0x00063DCD File Offset: 0x00061FCD
		public static bool operator ==(RuntimeTypeHandle left, object right)
		{
			return right != null && right is RuntimeTypeHandle && left.Equals((RuntimeTypeHandle)right);
		}

		/// <summary>Indicates whether a <see cref="T:System.RuntimeTypeHandle" /> structure is not equal to an object.</summary>
		/// <returns>true if <paramref name="right" /> is a <see cref="T:System.RuntimeTypeHandle" /> structure and is not equal to <paramref name="left" />; otherwise, false.</returns>
		/// <param name="left">A <see cref="T:System.RuntimeTypeHandle" /> structure to compare to <paramref name="right" />.</param>
		/// <param name="right">An object to compare to <paramref name="left" />.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001A86 RID: 6790 RVA: 0x00063DE9 File Offset: 0x00061FE9
		public static bool operator !=(RuntimeTypeHandle left, object right)
		{
			return right == null || !(right is RuntimeTypeHandle) || !left.Equals((RuntimeTypeHandle)right);
		}

		/// <summary>Indicates whether an object and a <see cref="T:System.RuntimeTypeHandle" /> structure are equal.</summary>
		/// <returns>true if <paramref name="left" /> is a <see cref="T:System.RuntimeTypeHandle" /> structure and is equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">An object to compare to <paramref name="right" />.</param>
		/// <param name="right">A <see cref="T:System.RuntimeTypeHandle" /> structure to compare to <paramref name="left" />.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001A87 RID: 6791 RVA: 0x00063E08 File Offset: 0x00062008
		public static bool operator ==(object left, RuntimeTypeHandle right)
		{
			return left != null && left is RuntimeTypeHandle && ((RuntimeTypeHandle)left).Equals(right);
		}

		/// <summary>Indicates whether an object and a <see cref="T:System.RuntimeTypeHandle" /> structure are not equal.</summary>
		/// <returns>true if <paramref name="left" /> is a <see cref="T:System.RuntimeTypeHandle" /> and is not equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">An object to compare to <paramref name="right" />.</param>
		/// <param name="right">A <see cref="T:System.RuntimeTypeHandle" /> structure to compare to <paramref name="left" />.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001A88 RID: 6792 RVA: 0x00063E34 File Offset: 0x00062034
		public static bool operator !=(object left, RuntimeTypeHandle right)
		{
			return left == null || !(left is RuntimeTypeHandle) || !((RuntimeTypeHandle)left).Equals(right);
		}

		// Token: 0x06001A89 RID: 6793
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern TypeAttributes GetAttributes(RuntimeType type);

		/// <summary>Gets a handle to the module that contains the type represented by the current instance.</summary>
		/// <returns>A <see cref="T:System.ModuleHandle" /> structure representing a handle to the module that contains the type represented by the current instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001A8A RID: 6794 RVA: 0x00063E60 File Offset: 0x00062060
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		public ModuleHandle GetModuleHandle()
		{
			if (this.value == IntPtr.Zero)
			{
				throw new InvalidOperationException("Object fields may not be properly initialized");
			}
			return Type.GetTypeFromHandle(this).Module.ModuleHandle;
		}

		// Token: 0x06001A8B RID: 6795
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMetadataToken(RuntimeType type);

		// Token: 0x06001A8C RID: 6796 RVA: 0x00063E94 File Offset: 0x00062094
		internal static int GetToken(RuntimeType type)
		{
			return RuntimeTypeHandle.GetMetadataToken(type);
		}

		// Token: 0x06001A8D RID: 6797
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type GetGenericTypeDefinition_impl(RuntimeType type);

		// Token: 0x06001A8E RID: 6798 RVA: 0x00063E9C File Offset: 0x0006209C
		internal static Type GetGenericTypeDefinition(RuntimeType type)
		{
			return RuntimeTypeHandle.GetGenericTypeDefinition_impl(type);
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x00063EA4 File Offset: 0x000620A4
		internal static bool HasElementType(RuntimeType type)
		{
			return RuntimeTypeHandle.IsArray(type) || RuntimeTypeHandle.IsByRef(type) || RuntimeTypeHandle.IsPointer(type);
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x00063EBE File Offset: 0x000620BE
		internal static bool HasProxyAttribute(RuntimeType type)
		{
			throw new NotImplementedException("HasProxyAttribute");
		}

		// Token: 0x06001A91 RID: 6801
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasInstantiation(RuntimeType type);

		// Token: 0x06001A92 RID: 6802
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsArray(RuntimeType type);

		// Token: 0x06001A93 RID: 6803
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsByRef(RuntimeType type);

		// Token: 0x06001A94 RID: 6804
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsComObject(RuntimeType type);

		// Token: 0x06001A95 RID: 6805
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsInstanceOfType(RuntimeType type, object o);

		// Token: 0x06001A96 RID: 6806
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsPointer(RuntimeType type);

		// Token: 0x06001A97 RID: 6807
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsPrimitive(RuntimeType type);

		// Token: 0x06001A98 RID: 6808
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasReferences(RuntimeType type);

		// Token: 0x06001A99 RID: 6809 RVA: 0x00063ECA File Offset: 0x000620CA
		internal static bool IsComObject(RuntimeType type, bool isGenericCOM)
		{
			return !isGenericCOM && RuntimeTypeHandle.IsComObject(type);
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x00057CEF File Offset: 0x00055EEF
		internal static bool IsContextful(RuntimeType type)
		{
			return typeof(ContextBoundObject).IsAssignableFrom(type);
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal static bool IsEquivalentTo(RuntimeType rtType1, RuntimeType rtType2)
		{
			return false;
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x00063ED7 File Offset: 0x000620D7
		internal static bool IsSzArray(RuntimeType type)
		{
			return RuntimeTypeHandle.IsArray(type) && type.GetArrayRank() == 1;
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x00063EEC File Offset: 0x000620EC
		internal static bool IsInterface(RuntimeType type)
		{
			return (type.Attributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask;
		}

		// Token: 0x06001A9E RID: 6814
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetArrayRank(RuntimeType type);

		// Token: 0x06001A9F RID: 6815
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeAssembly GetAssembly(RuntimeType type);

		// Token: 0x06001AA0 RID: 6816
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetElementType(RuntimeType type);

		// Token: 0x06001AA1 RID: 6817
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeModule GetModule(RuntimeType type);

		// Token: 0x06001AA2 RID: 6818
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsGenericVariable(RuntimeType type);

		// Token: 0x06001AA3 RID: 6819
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetBaseType(RuntimeType type);

		// Token: 0x06001AA4 RID: 6820 RVA: 0x00063EFB File Offset: 0x000620FB
		internal static bool CanCastTo(RuntimeType type, RuntimeType target)
		{
			return RuntimeTypeHandle.type_is_assignable_from(target, type);
		}

		// Token: 0x06001AA5 RID: 6821
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool type_is_assignable_from(Type a, Type b);

		// Token: 0x06001AA6 RID: 6822
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsGenericTypeDefinition(RuntimeType type);

		// Token: 0x06001AA7 RID: 6823
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetGenericParameterInfo(RuntimeType type);

		// Token: 0x04000D1F RID: 3359
		private IntPtr value;
	}
}
