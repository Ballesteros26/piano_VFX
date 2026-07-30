using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	/// <summary>Describes objects that contain both a managed pointer to a location and a runtime representation of the type that may be stored at that location.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001DF RID: 479
	[ComVisible(true)]
	[CLSCompliant(false)]
	public struct TypedReference
	{
		/// <summary>Makes a TypedReference for a field identified by a specified object and list of field descriptions.</summary>
		/// <returns>A <see cref="T:System.TypedReference" /> for the field described by the last element of <paramref name="flds" />.</returns>
		/// <param name="target">An object that contains the field described by the first element of <paramref name="flds" />. </param>
		/// <param name="flds">A list of field descriptions where each element describes a field that contains the field described by the succeeding element. Each described field must be a value type. The field descriptions must be RuntimeFieldInfo objects supplied by the type system.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> or <paramref name="flds" /> is null.-or- An element of <paramref name="flds" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flds" /> array has no elements.-or- An element of <paramref name="flds" /> is not a RuntimeFieldInfo.-or- The <see cref="P:System.Reflection.FieldInfo.IsInitOnly" /> or <see cref="P:System.Reflection.FieldInfo.IsStatic" /> property of an element of <paramref name="flds" /> is true. </exception>
		/// <exception cref="T:System.MissingMemberException">Parameter <paramref name="target" /> does not contain the field described by the first element of <paramref name="flds" />, or an element of <paramref name="flds" /> describes a field that is not contained in the field described by the succeeding element of <paramref name="flds" />.-or- The field described by an element of <paramref name="flds" /> is not a value type. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060015EF RID: 5615 RVA: 0x00058640 File Offset: 0x00056840
		[CLSCompliant(false)]
		[SecurityCritical]
		public static TypedReference MakeTypedReference(object target, FieldInfo[] flds)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (flds == null)
			{
				throw new ArgumentNullException("flds");
			}
			if (flds.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Array must not be of length zero."));
			}
			IntPtr[] array = new IntPtr[flds.Length];
			RuntimeType runtimeType = (RuntimeType)target.GetType();
			for (int i = 0; i < flds.Length; i++)
			{
				RuntimeFieldInfo runtimeFieldInfo = flds[i] as RuntimeFieldInfo;
				if (runtimeFieldInfo == null)
				{
					throw new ArgumentException(Environment.GetResourceString("FieldInfo must be a runtime FieldInfo object."));
				}
				if (runtimeFieldInfo.IsInitOnly || runtimeFieldInfo.IsStatic)
				{
					throw new ArgumentException(Environment.GetResourceString("Field in TypedReferences cannot be static or init only."));
				}
				if (runtimeType != runtimeFieldInfo.GetDeclaringTypeInternal() && !runtimeType.IsSubclassOf(runtimeFieldInfo.GetDeclaringTypeInternal()))
				{
					throw new MissingMemberException(Environment.GetResourceString("FieldInfo does not match the target Type."));
				}
				RuntimeType runtimeType2 = (RuntimeType)runtimeFieldInfo.FieldType;
				if (runtimeType2.IsPrimitive)
				{
					throw new ArgumentException(Environment.GetResourceString("TypedReferences cannot be redefined as primitives."));
				}
				if (i < flds.Length - 1 && !runtimeType2.IsValueType)
				{
					throw new MissingMemberException(Environment.GetResourceString("TypedReference can only be made on nested value Types."));
				}
				array[i] = runtimeFieldInfo.FieldHandle.Value;
				runtimeType = runtimeType2;
			}
			return TypedReference.MakeTypedReferenceInternal(target, flds);
		}

		// Token: 0x060015F0 RID: 5616
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TypedReference MakeTypedReferenceInternal(object target, FieldInfo[] fields);

		/// <summary>Returns the hash code of this object.</summary>
		/// <returns>The hash code of this object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060015F1 RID: 5617 RVA: 0x00058777 File Offset: 0x00056977
		public override int GetHashCode()
		{
			if (this.Type == IntPtr.Zero)
			{
				return 0;
			}
			return __reftype(this).GetHashCode();
		}

		/// <summary>Checks if this object is equal to the specified object.</summary>
		/// <returns>true if this object is equal to the specified object; otherwise, false.</returns>
		/// <param name="o">The object with which to compare the current object. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060015F2 RID: 5618 RVA: 0x0005879F File Offset: 0x0005699F
		public override bool Equals(object o)
		{
			throw new NotSupportedException(Environment.GetResourceString("This feature is not currently implemented."));
		}

		/// <summary>Converts the specified TypedReference to an Object.</summary>
		/// <returns>An <see cref="T:System.Object" /> converted from a TypedReference.</returns>
		/// <param name="value">The TypedReference to be converted. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015F3 RID: 5619 RVA: 0x000587B0 File Offset: 0x000569B0
		[SecuritySafeCritical]
		public unsafe static object ToObject(TypedReference value)
		{
			return TypedReference.InternalToObject((void*)(&value));
		}

		// Token: 0x060015F4 RID: 5620
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern object InternalToObject(void* value);

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x000587BA File Offset: 0x000569BA
		internal bool IsNull
		{
			get
			{
				return this.Value.IsNull() && this.Type.IsNull();
			}
		}

		/// <summary>Returns the type of the target of the specified TypedReference.</summary>
		/// <returns>The type of the target of the specified TypedReference.</returns>
		/// <param name="value">The value whose target's type is to be returned. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015F6 RID: 5622 RVA: 0x000587D6 File Offset: 0x000569D6
		public static Type GetTargetType(TypedReference value)
		{
			return __reftype(value);
		}

		/// <summary>Returns the internal metadata type handle for the specified TypedReference.</summary>
		/// <returns>The internal metadata type handle for the specified TypedReference.</returns>
		/// <param name="value">The TypedReference for which the type handle is requested. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015F7 RID: 5623 RVA: 0x000587E0 File Offset: 0x000569E0
		public static RuntimeTypeHandle TargetTypeToken(TypedReference value)
		{
			return __reftype(value).TypeHandle;
		}

		/// <summary>Converts the specified value to a TypedReference. This method is not supported.</summary>
		/// <param name="target">The target of the conversion. </param>
		/// <param name="value">The value to be converted. </param>
		/// <exception cref="T:System.NotSupportedException">In all cases. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015F8 RID: 5624 RVA: 0x000587EF File Offset: 0x000569EF
		[CLSCompliant(false)]
		[SecuritySafeCritical]
		public static void SetTypedReference(TypedReference target, object value)
		{
			throw new NotImplementedException("SetTypedReference");
		}

		// Token: 0x04000BB1 RID: 2993
		private RuntimeTypeHandle type;

		// Token: 0x04000BB2 RID: 2994
		private IntPtr Value;

		// Token: 0x04000BB3 RID: 2995
		private IntPtr Type;
	}
}
