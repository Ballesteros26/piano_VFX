using System;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents a runtime handle for a module.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200021A RID: 538
	[ComVisible(true)]
	public struct ModuleHandle
	{
		// Token: 0x06001978 RID: 6520 RVA: 0x0005F3FC File Offset: 0x0005D5FC
		internal ModuleHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001979 RID: 6521 RVA: 0x0005F405 File Offset: 0x0005D605
		internal IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Gets the metadata stream version.</summary>
		/// <returns>A 32-bit integer representing the metadata stream version. The high-order two bytes represent the major version number, and the low-order two bytes represent the minor version number.</returns>
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x0600197A RID: 6522 RVA: 0x0005F40D File Offset: 0x0005D60D
		public int MDStreamVersion
		{
			get
			{
				if (this.value == IntPtr.Zero)
				{
					throw new ArgumentNullException(string.Empty, "Invalid handle");
				}
				return Module.GetMDStreamVersion(this.value);
			}
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0005F43C File Offset: 0x0005D63C
		internal void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			if (this.value == IntPtr.Zero)
			{
				throw new ArgumentNullException(string.Empty, "Invalid handle");
			}
			Module.GetPEKind(this.value, out peKind, out machine);
		}

		/// <summary>Returns a runtime handle for the field identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.RuntimeFieldHandle" /> for the field identified by <paramref name="fieldToken" />.</returns>
		/// <param name="fieldToken">A metadata token that identifies a field in the module.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.-or-<paramref name="metadataToken" /> is not a token for a field in the scope of the current module.-or-<paramref name="metadataToken" /> identifies a field whose parent TypeSpec has a signature containing element type var or mvar.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method is called on an empty field handle.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600197C RID: 6524 RVA: 0x0005F46D File Offset: 0x0005D66D
		public RuntimeFieldHandle ResolveFieldHandle(int fieldToken)
		{
			return this.ResolveFieldHandle(fieldToken, null, null);
		}

		/// <summary>Returns a runtime method handle for the method or constructor identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.RuntimeMethodHandle" /> for the method or constructor identified by <paramref name="methodToken" />.</returns>
		/// <param name="methodToken">A metadata token that identifies a method or constructor in the module.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="methodToken" /> is not a valid metadata token for a method in the current module.-or-<paramref name="metadataToken" /> is not a token for a method or constructor in the scope of the current module.-or-<paramref name="metadataToken" /> is a MethodSpec whose signature contains element type var or mvar.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method is called on an empty method handle.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600197D RID: 6525 RVA: 0x0005F478 File Offset: 0x0005D678
		public RuntimeMethodHandle ResolveMethodHandle(int methodToken)
		{
			return this.ResolveMethodHandle(methodToken, null, null);
		}

		/// <summary>Returns a runtime type handle for the type identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.RuntimeTypeHandle" /> for the type identified by <paramref name="typeToken" />.</returns>
		/// <param name="typeToken">A metadata token that identifies a type in the module.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="typeToken" /> is not a valid metadata token for a type in the current module.-or-<paramref name="metadataToken" /> is not a token for a type in the scope of the current module.-or-<paramref name="metadataToken" /> is a TypeSpec whose signature contains element type var or mvar.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method is called on an empty type handle.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600197E RID: 6526 RVA: 0x0005F483 File Offset: 0x0005D683
		public RuntimeTypeHandle ResolveTypeHandle(int typeToken)
		{
			return this.ResolveTypeHandle(typeToken, null, null);
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0005F490 File Offset: 0x0005D690
		private IntPtr[] ptrs_from_handles(RuntimeTypeHandle[] handles)
		{
			if (handles == null)
			{
				return null;
			}
			IntPtr[] array = new IntPtr[handles.Length];
			for (int i = 0; i < handles.Length; i++)
			{
				array[i] = handles[i].Value;
			}
			return array;
		}

		/// <summary>Returns a runtime type handle for the type identified by the specified metadata token, specifying the generic type arguments of the type and method where the token is in scope.</summary>
		/// <returns>A <see cref="T:System.RuntimeTypeHandle" /> for the type identified by <paramref name="typeToken" />.</returns>
		/// <param name="typeToken">A metadata token that identifies a type in the module.</param>
		/// <param name="typeInstantiationContext">An array of <see cref="T:System.RuntimeTypeHandle" /> structures representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="methodInstantiationContext">An array of <see cref="T:System.RuntimeTypeHandle" /> structures objects representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="typeToken" /> is not a valid metadata token for a type in the current module.-or-<paramref name="metadataToken" /> is not a token for a type in the scope of the current module.-or-<paramref name="metadataToken" /> is a TypeSpec whose signature contains element type var or mvar.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method is called on an empty type handle.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="typeToken " />is not a valid token.</exception>
		// Token: 0x06001980 RID: 6528 RVA: 0x0005F4CC File Offset: 0x0005D6CC
		public RuntimeTypeHandle ResolveTypeHandle(int typeToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			if (this.value == IntPtr.Zero)
			{
				throw new ArgumentNullException(string.Empty, "Invalid handle");
			}
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = Module.ResolveTypeToken(this.value, typeToken, this.ptrs_from_handles(typeInstantiationContext), this.ptrs_from_handles(methodInstantiationContext), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw new TypeLoadException(string.Format("Could not load type '0x{0:x}' from assembly '0x{1:x}'", typeToken, this.value.ToInt64()));
			}
			return new RuntimeTypeHandle(intPtr);
		}

		/// <summary>Returns a runtime method handle for the method or constructor identified by the specified metadata token, specifying the generic type arguments of the type and method where the token is in scope.</summary>
		/// <returns>A <see cref="T:System.RuntimeMethodHandle" /> for the method or constructor identified by <paramref name="methodToken" />.</returns>
		/// <param name="methodToken">A metadata token that identifies a method or constructor in the module.</param>
		/// <param name="typeInstantiationContext">An array of <see cref="T:System.RuntimeTypeHandle" /> structures representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="methodInstantiationContext">An array of <see cref="T:System.RuntimeTypeHandle" /> structures representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="methodToken" /> is not a valid metadata token for a method in the current module.-or-<paramref name="metadataToken" /> is not a token for a method or constructor in the scope of the current module.-or-<paramref name="metadataToken" /> is a MethodSpec whose signature contains element type var or mvar.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method is called on an empty method handle.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="methodToken " />is not a valid token.</exception>
		// Token: 0x06001981 RID: 6529 RVA: 0x0005F554 File Offset: 0x0005D754
		public RuntimeMethodHandle ResolveMethodHandle(int methodToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			if (this.value == IntPtr.Zero)
			{
				throw new ArgumentNullException(string.Empty, "Invalid handle");
			}
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = Module.ResolveMethodToken(this.value, methodToken, this.ptrs_from_handles(typeInstantiationContext), this.ptrs_from_handles(methodInstantiationContext), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw new Exception(string.Format("Could not load method '0x{0:x}' from assembly '0x{1:x}'", methodToken, this.value.ToInt64()));
			}
			return new RuntimeMethodHandle(intPtr);
		}

		/// <summary>Returns a runtime field handle for the field identified by the specified metadata token, specifying the generic type arguments of the type and method where the token is in scope.</summary>
		/// <returns>A <see cref="T:System.RuntimeFieldHandle" /> for the field identified by <paramref name="fieldToken" />.</returns>
		/// <param name="fieldToken">A metadata token that identifies a field in the module.</param>
		/// <param name="typeInstantiationContext">An array of <see cref="T:System.RuntimeTypeHandle" /> structures representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="methodInstantiationContext">An array of <see cref="T:System.RuntimeTypeHandle" /> structures representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.-or-<paramref name="metadataToken" /> is not a token for a field in the scope of the current module.-or-<paramref name="metadataToken" /> identifies a field whose parent TypeSpec has a signature containing element type var or mvar.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method is called on an empty field handle.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="fieldToken " />is not a valid token.</exception>
		// Token: 0x06001982 RID: 6530 RVA: 0x0005F5DC File Offset: 0x0005D7DC
		public RuntimeFieldHandle ResolveFieldHandle(int fieldToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			if (this.value == IntPtr.Zero)
			{
				throw new ArgumentNullException(string.Empty, "Invalid handle");
			}
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = Module.ResolveFieldToken(this.value, fieldToken, this.ptrs_from_handles(typeInstantiationContext), this.ptrs_from_handles(methodInstantiationContext), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw new Exception(string.Format("Could not load field '0x{0:x}' from assembly '0x{1:x}'", fieldToken, this.value.ToInt64()));
			}
			return new RuntimeFieldHandle(intPtr);
		}

		/// <summary>Returns a runtime handle for the field identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.RuntimeFieldHandle" /> for the field identified by <paramref name="fieldToken" />.</returns>
		/// <param name="fieldToken">A metadata token that identifies a field in the module.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001983 RID: 6531 RVA: 0x0005F661 File Offset: 0x0005D861
		public RuntimeFieldHandle GetRuntimeFieldHandleFromMetadataToken(int fieldToken)
		{
			return this.ResolveFieldHandle(fieldToken);
		}

		/// <summary>Returns a runtime method handle for the method or constructor identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.RuntimeMethodHandle" /> for the method or constructor identified by <paramref name="methodToken" />.</returns>
		/// <param name="methodToken">A metadata token that identifies a method or constructor in the module.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001984 RID: 6532 RVA: 0x0005F66A File Offset: 0x0005D86A
		public RuntimeMethodHandle GetRuntimeMethodHandleFromMetadataToken(int methodToken)
		{
			return this.ResolveMethodHandle(methodToken);
		}

		/// <summary>Returns a runtime type handle for the type identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.RuntimeTypeHandle" /> for the type identified by <paramref name="typeToken" />.</returns>
		/// <param name="typeToken">A metadata token that identifies a type in the module.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001985 RID: 6533 RVA: 0x0005F673 File Offset: 0x0005D873
		public RuntimeTypeHandle GetRuntimeTypeHandleFromMetadataToken(int typeToken)
		{
			return this.ResolveTypeHandle(typeToken);
		}

		/// <summary>Returns a <see cref="T:System.Boolean" /> value indicating whether the specified object is a <see cref="T:System.ModuleHandle" /> structure, and equal to the current <see cref="T:System.ModuleHandle" />.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.ModuleHandle" /> structure, and is equal to the current <see cref="T:System.ModuleHandle" /> structure; otherwise, false.</returns>
		/// <param name="obj">The object to be compared with the current <see cref="T:System.ModuleHandle" /> structure.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001986 RID: 6534 RVA: 0x0005F67C File Offset: 0x0005D87C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((ModuleHandle)obj).Value;
		}

		/// <summary>Returns a <see cref="T:System.Boolean" /> value indicating whether the specified <see cref="T:System.ModuleHandle" /> structure is equal to the current <see cref="T:System.ModuleHandle" />.</summary>
		/// <returns>true if <paramref name="handle" /> is equal to the current <see cref="T:System.ModuleHandle" /> structure; otherwise false.</returns>
		/// <param name="handle">The <see cref="T:System.ModuleHandle" /> structure to be compared with the current <see cref="T:System.ModuleHandle" />.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001987 RID: 6535 RVA: 0x0005F6C4 File Offset: 0x0005D8C4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public bool Equals(ModuleHandle handle)
		{
			return this.value == handle.Value;
		}

		/// <filterpriority>2</filterpriority>
		// Token: 0x06001988 RID: 6536 RVA: 0x0005F6D8 File Offset: 0x0005D8D8
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		/// <summary>Tests whether two <see cref="T:System.ModuleHandle" /> structures are equal.</summary>
		/// <returns>true if the <see cref="T:System.ModuleHandle" /> structures are equal; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.ModuleHandle" /> structure to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.ModuleHandle" /> structure to the right of the equality operator.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001989 RID: 6537 RVA: 0x0005F6E5 File Offset: 0x0005D8E5
		public static bool operator ==(ModuleHandle left, ModuleHandle right)
		{
			return object.Equals(left, right);
		}

		/// <summary>Tests whether two <see cref="T:System.ModuleHandle" /> structures are unequal.</summary>
		/// <returns>true if the <see cref="T:System.ModuleHandle" /> structures are unequal; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.ModuleHandle" /> structure to the left of the inequality operator.</param>
		/// <param name="right">The <see cref="T:System.ModuleHandle" /> structure to the right of the inequality operator.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600198A RID: 6538 RVA: 0x0005F6F8 File Offset: 0x0005D8F8
		public static bool operator !=(ModuleHandle left, ModuleHandle right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x04000CBD RID: 3261
		private IntPtr value;

		/// <summary>Represents an empty module handle.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000CBE RID: 3262
		public static readonly ModuleHandle EmptyHandle = new ModuleHandle(IntPtr.Zero);
	}
}
