using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;

namespace System.Runtime.Serialization
{
	/// <summary>Stores all the data needed to serialize or deserialize an object. This class cannot be inherited.</summary>
	// Token: 0x020006EF RID: 1775
	[ComVisible(true)]
	public sealed class SerializationInfo
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to serialize. </param>
		/// <param name="converter">The <see cref="T:System.Runtime.Serialization.IFormatterConverter" /> used during deserialization. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="converter" /> is null. </exception>
		// Token: 0x06004ABB RID: 19131 RVA: 0x0010B6E6 File Offset: 0x001098E6
		[CLSCompliant(false)]
		public SerializationInfo(Type type, IFormatterConverter converter)
			: this(type, converter, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to serialize.</param>
		/// <param name="converter">The <see cref="T:System.Runtime.Serialization.IFormatterConverter" /> used during deserialization.</param>
		/// <param name="requireSameTokenInPartialTrust">Indicates whether the object requires same token in partial trust.</param>
		// Token: 0x06004ABC RID: 19132 RVA: 0x0010B6F4 File Offset: 0x001098F4
		[CLSCompliant(false)]
		public SerializationInfo(Type type, IFormatterConverter converter, bool requireSameTokenInPartialTrust)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			this.objectType = type;
			this.m_fullTypeName = type.FullName;
			this.m_assemName = type.Module.Assembly.FullName;
			this.m_members = new string[4];
			this.m_data = new object[4];
			this.m_types = new Type[4];
			this.m_nameToIndex = new Dictionary<string, int>();
			this.m_converter = converter;
			this.requireSameTokenInPartialTrust = requireSameTokenInPartialTrust;
		}

		/// <summary>Gets or sets the full name of the <see cref="T:System.Type" /> to serialize.</summary>
		/// <returns>The full name of the type to serialize.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value this property is set to is null. </exception>
		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06004ABD RID: 19133 RVA: 0x0010B789 File Offset: 0x00109989
		// (set) Token: 0x06004ABE RID: 19134 RVA: 0x0010B791 File Offset: 0x00109991
		public string FullTypeName
		{
			get
			{
				return this.m_fullTypeName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_fullTypeName = value;
				this.isFullTypeNameSetExplicit = true;
			}
		}

		/// <summary>Gets or sets the assembly name of the type to serialize during serialization only.</summary>
		/// <returns>The full name of the assembly of the type to serialize.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value the property is set to is null. </exception>
		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06004ABF RID: 19135 RVA: 0x0010B7AF File Offset: 0x001099AF
		// (set) Token: 0x06004AC0 RID: 19136 RVA: 0x0010B7B7 File Offset: 0x001099B7
		public string AssemblyName
		{
			get
			{
				return this.m_assemName;
			}
			[SecuritySafeCritical]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.requireSameTokenInPartialTrust)
				{
					SerializationInfo.DemandForUnsafeAssemblyNameAssignments(this.m_assemName, value);
				}
				this.m_assemName = value;
				this.isAssemblyNameSetExplicit = true;
			}
		}

		/// <summary>Sets the <see cref="T:System.Type" /> of the object to serialize.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the object to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null. </exception>
		// Token: 0x06004AC1 RID: 19137 RVA: 0x0010B7EC File Offset: 0x001099EC
		[SecuritySafeCritical]
		public void SetType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this.requireSameTokenInPartialTrust)
			{
				SerializationInfo.DemandForUnsafeAssemblyNameAssignments(this.ObjectType.Assembly.FullName, type.Assembly.FullName);
			}
			if (this.objectType != type)
			{
				this.objectType = type;
				this.m_fullTypeName = type.FullName;
				this.m_assemName = type.Module.Assembly.FullName;
				this.isFullTypeNameSetExplicit = false;
				this.isAssemblyNameSetExplicit = false;
			}
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x0010B870 File Offset: 0x00109A70
		private static bool Compare(byte[] a, byte[] b)
		{
			if (a == null || b == null || a.Length == 0 || b.Length == 0 || a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x0010B8AE File Offset: 0x00109AAE
		[SecuritySafeCritical]
		internal static void DemandForUnsafeAssemblyNameAssignments(string originalAssemblyName, string newAssemblyName)
		{
			SerializationInfo.IsAssemblyNameAssignmentSafe(originalAssemblyName, newAssemblyName);
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x0010B8B8 File Offset: 0x00109AB8
		internal static bool IsAssemblyNameAssignmentSafe(string originalAssemblyName, string newAssemblyName)
		{
			if (originalAssemblyName == newAssemblyName)
			{
				return true;
			}
			AssemblyName assemblyName = new AssemblyName(originalAssemblyName);
			AssemblyName assemblyName2 = new AssemblyName(newAssemblyName);
			return !string.Equals(assemblyName2.Name, "mscorlib", StringComparison.OrdinalIgnoreCase) && !string.Equals(assemblyName2.Name, "mscorlib.dll", StringComparison.OrdinalIgnoreCase) && SerializationInfo.Compare(assemblyName.GetPublicKeyToken(), assemblyName2.GetPublicKeyToken());
		}

		/// <summary>Gets the number of members that have been added to the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The number of members that have been added to the current <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</returns>
		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06004AC5 RID: 19141 RVA: 0x0010B917 File Offset: 0x00109B17
		public int MemberCount
		{
			get
			{
				return this.m_currMember;
			}
		}

		/// <summary>Returns the type of the object to be serialized.</summary>
		/// <returns>The type of the object being serialized.</returns>
		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06004AC6 RID: 19142 RVA: 0x0010B91F File Offset: 0x00109B1F
		public Type ObjectType
		{
			get
			{
				return this.objectType;
			}
		}

		/// <summary>Gets whether the full type name has been explicitly set.</summary>
		/// <returns>True if the full type name has been explicitly set; otherwise false.</returns>
		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06004AC7 RID: 19143 RVA: 0x0010B927 File Offset: 0x00109B27
		public bool IsFullTypeNameSetExplicit
		{
			get
			{
				return this.isFullTypeNameSetExplicit;
			}
		}

		/// <summary>Gets whether the assembly name has been explicitly set.</summary>
		/// <returns>True if the assembly name has been explicitly set; otherwise false.</returns>
		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06004AC8 RID: 19144 RVA: 0x0010B92F File Offset: 0x00109B2F
		public bool IsAssemblyNameSetExplicit
		{
			get
			{
				return this.isAssemblyNameSetExplicit;
			}
		}

		/// <summary>Returns a <see cref="T:System.Runtime.Serialization.SerializationInfoEnumerator" /> used to iterate through the name-value pairs in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>A <see cref="T:System.Runtime.Serialization.SerializationInfoEnumerator" /> for parsing the name-value pairs contained in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</returns>
		// Token: 0x06004AC9 RID: 19145 RVA: 0x0010B937 File Offset: 0x00109B37
		public SerializationInfoEnumerator GetEnumerator()
		{
			return new SerializationInfoEnumerator(this.m_members, this.m_data, this.m_types, this.m_currMember);
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x0010B958 File Offset: 0x00109B58
		private void ExpandArrays()
		{
			int num = this.m_currMember * 2;
			if (num < this.m_currMember && 2147483647 > this.m_currMember)
			{
				num = int.MaxValue;
			}
			string[] array = new string[num];
			object[] array2 = new object[num];
			Type[] array3 = new Type[num];
			Array.Copy(this.m_members, array, this.m_currMember);
			Array.Copy(this.m_data, array2, this.m_currMember);
			Array.Copy(this.m_types, array3, this.m_currMember);
			this.m_members = array;
			this.m_data = array2;
			this.m_types = array3;
		}

		/// <summary>Adds a value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store, where <paramref name="value" /> is associated with <paramref name="name" /> and is serialized as being of <see cref="T:System.Type" /><paramref name="type" />.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The value to be serialized. Any children of this object will automatically be serialized. </param>
		/// <param name="type">The <see cref="T:System.Type" /> to associate with the current object. This parameter must always be the type of the object itself or of one of its base classes. </param>
		/// <exception cref="T:System.ArgumentNullException">If <paramref name="name" /> or <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06004ACB RID: 19147 RVA: 0x0010B9EA File Offset: 0x00109BEA
		public void AddValue(string name, object value, Type type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.AddValueInternal(name, value, type);
		}

		/// <summary>Adds the specified object into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store, where it is associated with a specified name.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The value to be serialized. Any children of this object will automatically be serialized. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06004ACC RID: 19148 RVA: 0x0010BA11 File Offset: 0x00109C11
		public void AddValue(string name, object value)
		{
			if (value == null)
			{
				this.AddValue(name, value, typeof(object));
				return;
			}
			this.AddValue(name, value, value.GetType());
		}

		/// <summary>Adds a Boolean value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The Boolean value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06004ACD RID: 19149 RVA: 0x0010BA37 File Offset: 0x00109C37
		public void AddValue(string name, bool value)
		{
			this.AddValue(name, value, typeof(bool));
		}

		/// <summary>Adds a Unicode character value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The character value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06004ACE RID: 19150 RVA: 0x0010BA50 File Offset: 0x00109C50
		public void AddValue(string name, char value)
		{
			this.AddValue(name, value, typeof(char));
		}

		/// <summary>Adds an 8-bit signed integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The Sbyte value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06004ACF RID: 19151 RVA: 0x0010BA69 File Offset: 0x00109C69
		[CLSCompliant(false)]
		public void AddValue(string name, sbyte value)
		{
			this.AddValue(name, value, typeof(sbyte));
		}

		/// <summary>Adds an 8-bit unsigned integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The byte value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06004AD0 RID: 19152 RVA: 0x0010BA82 File Offset: 0x00109C82
		public void AddValue(string name, byte value)
		{
			this.AddValue(name, value, typeof(byte));
		}

		/// <summary>Adds a 16-bit signed integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The Int16 value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06004AD1 RID: 19153 RVA: 0x0010BA9B File Offset: 0x00109C9B
		public void AddValue(string name, short value)
		{
			this.AddValue(name, value, typeof(short));
		}

		/// <summary>Adds a 16-bit unsigned integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The UInt16 value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06004AD2 RID: 19154 RVA: 0x0010BAB4 File Offset: 0x00109CB4
		[CLSCompliant(false)]
		public void AddValue(string name, ushort value)
		{
			this.AddValue(name, value, typeof(ushort));
		}

		/// <summary>Adds a 32-bit signed integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The Int32 value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06004AD3 RID: 19155 RVA: 0x0010BACD File Offset: 0x00109CCD
		public void AddValue(string name, int value)
		{
			this.AddValue(name, value, typeof(int));
		}

		/// <summary>Adds a 32-bit unsigned integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The UInt32 value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06004AD4 RID: 19156 RVA: 0x0010BAE6 File Offset: 0x00109CE6
		[CLSCompliant(false)]
		public void AddValue(string name, uint value)
		{
			this.AddValue(name, value, typeof(uint));
		}

		/// <summary>Adds a 64-bit signed integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The Int64 value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06004AD5 RID: 19157 RVA: 0x0010BAFF File Offset: 0x00109CFF
		public void AddValue(string name, long value)
		{
			this.AddValue(name, value, typeof(long));
		}

		/// <summary>Adds a 64-bit unsigned integer value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The UInt64 value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06004AD6 RID: 19158 RVA: 0x0010BB18 File Offset: 0x00109D18
		[CLSCompliant(false)]
		public void AddValue(string name, ulong value)
		{
			this.AddValue(name, value, typeof(ulong));
		}

		/// <summary>Adds a single-precision floating-point value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The single value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06004AD7 RID: 19159 RVA: 0x0010BB31 File Offset: 0x00109D31
		public void AddValue(string name, float value)
		{
			this.AddValue(name, value, typeof(float));
		}

		/// <summary>Adds a double-precision floating-point value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The double value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06004AD8 RID: 19160 RVA: 0x0010BB4A File Offset: 0x00109D4A
		public void AddValue(string name, double value)
		{
			this.AddValue(name, value, typeof(double));
		}

		/// <summary>Adds a decimal value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The decimal value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">If The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">If a value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06004AD9 RID: 19161 RVA: 0x0010BB63 File Offset: 0x00109D63
		public void AddValue(string name, decimal value)
		{
			this.AddValue(name, value, typeof(decimal));
		}

		/// <summary>Adds a <see cref="T:System.DateTime" /> value into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <param name="name">The name to associate with the value, so it can be deserialized later. </param>
		/// <param name="value">The <see cref="T:System.DateTime" /> value to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A value has already been associated with <paramref name="name" />. </exception>
		// Token: 0x06004ADA RID: 19162 RVA: 0x0010BB7C File Offset: 0x00109D7C
		public void AddValue(string name, DateTime value)
		{
			this.AddValue(name, value, typeof(DateTime));
		}

		// Token: 0x06004ADB RID: 19163 RVA: 0x0010BB98 File Offset: 0x00109D98
		internal void AddValueInternal(string name, object value, Type type)
		{
			if (this.m_nameToIndex.ContainsKey(name))
			{
				throw new SerializationException(Environment.GetResourceString("Cannot add the same member twice to a SerializationInfo object."));
			}
			this.m_nameToIndex.Add(name, this.m_currMember);
			if (this.m_currMember >= this.m_members.Length)
			{
				this.ExpandArrays();
			}
			this.m_members[this.m_currMember] = name;
			this.m_data[this.m_currMember] = value;
			this.m_types[this.m_currMember] = type;
			this.m_currMember++;
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x0010BC24 File Offset: 0x00109E24
		internal void UpdateValue(string name, object value, Type type)
		{
			int num = this.FindElement(name);
			if (num < 0)
			{
				this.AddValueInternal(name, value, type);
				return;
			}
			this.m_data[num] = value;
			this.m_types[num] = type;
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x0010BC5C File Offset: 0x00109E5C
		private int FindElement(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			int num;
			if (this.m_nameToIndex.TryGetValue(name, out num))
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x0010BC8C File Offset: 0x00109E8C
		private object GetElement(string name, out Type foundType)
		{
			int num = this.FindElement(name);
			if (num == -1)
			{
				throw new SerializationException(Environment.GetResourceString("Member '{0}' was not found.", new object[] { name }));
			}
			foundType = this.m_types[num];
			return this.m_data[num];
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x0010BCD4 File Offset: 0x00109ED4
		[ComVisible(true)]
		private object GetElementNoThrow(string name, out Type foundType)
		{
			int num = this.FindElement(name);
			if (num == -1)
			{
				foundType = null;
				return null;
			}
			foundType = this.m_types[num];
			return this.m_data[num];
		}

		/// <summary>Retrieves a value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The object of the specified <see cref="T:System.Type" /> associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the value to retrieve. If the stored value cannot be converted to this type, the system will throw a <see cref="T:System.InvalidCastException" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to <paramref name="type" />. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06004AE0 RID: 19168 RVA: 0x0010BD04 File Offset: 0x00109F04
		[SecuritySafeCritical]
		public object GetValue(string name, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a runtime Type object."));
			}
			Type type2;
			object element = this.GetElement(name, out type2);
			if (RemotingServices.IsTransparentProxy(element))
			{
				if (RemotingServices.ProxyCheckCast(RemotingServices.GetRealProxy(element), runtimeType))
				{
					return element;
				}
			}
			else if (type2 == type || type.IsAssignableFrom(type2) || element == null)
			{
				return element;
			}
			return this.m_converter.Convert(element, type);
		}

		// Token: 0x06004AE1 RID: 19169 RVA: 0x0010BD80 File Offset: 0x00109F80
		[ComVisible(true)]
		[SecuritySafeCritical]
		internal object GetValueNoThrow(string name, Type type)
		{
			Type type2;
			object elementNoThrow = this.GetElementNoThrow(name, out type2);
			if (elementNoThrow == null)
			{
				return null;
			}
			if (RemotingServices.IsTransparentProxy(elementNoThrow))
			{
				if (RemotingServices.ProxyCheckCast(RemotingServices.GetRealProxy(elementNoThrow), (RuntimeType)type))
				{
					return elementNoThrow;
				}
			}
			else if (type2 == type || type.IsAssignableFrom(type2) || elementNoThrow == null)
			{
				return elementNoThrow;
			}
			return this.m_converter.Convert(elementNoThrow, type);
		}

		/// <summary>Retrieves a Boolean value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The Boolean value associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a Boolean value. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06004AE2 RID: 19170 RVA: 0x0010BDD8 File Offset: 0x00109FD8
		public bool GetBoolean(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(bool))
			{
				return (bool)element;
			}
			return this.m_converter.ToBoolean(element);
		}

		/// <summary>Retrieves a Unicode character value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The Unicode character associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a Unicode character. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06004AE3 RID: 19171 RVA: 0x0010BE10 File Offset: 0x0010A010
		public char GetChar(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(char))
			{
				return (char)element;
			}
			return this.m_converter.ToChar(element);
		}

		/// <summary>Retrieves an 8-bit signed integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The 8-bit signed integer associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to an 8-bit signed integer. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06004AE4 RID: 19172 RVA: 0x0010BE48 File Offset: 0x0010A048
		[CLSCompliant(false)]
		public sbyte GetSByte(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(sbyte))
			{
				return (sbyte)element;
			}
			return this.m_converter.ToSByte(element);
		}

		/// <summary>Retrieves an 8-bit unsigned integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The 8-bit unsigned integer associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to an 8-bit unsigned integer. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06004AE5 RID: 19173 RVA: 0x0010BE80 File Offset: 0x0010A080
		public byte GetByte(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(byte))
			{
				return (byte)element;
			}
			return this.m_converter.ToByte(element);
		}

		/// <summary>Retrieves a 16-bit signed integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The 16-bit signed integer associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a 16-bit signed integer. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06004AE6 RID: 19174 RVA: 0x0010BEB8 File Offset: 0x0010A0B8
		public short GetInt16(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(short))
			{
				return (short)element;
			}
			return this.m_converter.ToInt16(element);
		}

		/// <summary>Retrieves a 16-bit unsigned integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The 16-bit unsigned integer associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a 16-bit unsigned integer. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06004AE7 RID: 19175 RVA: 0x0010BEF0 File Offset: 0x0010A0F0
		[CLSCompliant(false)]
		public ushort GetUInt16(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(ushort))
			{
				return (ushort)element;
			}
			return this.m_converter.ToUInt16(element);
		}

		/// <summary>Retrieves a 32-bit signed integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The 32-bit signed integer associated with <paramref name="name" />.</returns>
		/// <param name="name">The name of the value to retrieve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a 32-bit signed integer. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06004AE8 RID: 19176 RVA: 0x0010BF28 File Offset: 0x0010A128
		public int GetInt32(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(int))
			{
				return (int)element;
			}
			return this.m_converter.ToInt32(element);
		}

		/// <summary>Retrieves a 32-bit unsigned integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The 32-bit unsigned integer associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a 32-bit unsigned integer. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06004AE9 RID: 19177 RVA: 0x0010BF60 File Offset: 0x0010A160
		[CLSCompliant(false)]
		public uint GetUInt32(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(uint))
			{
				return (uint)element;
			}
			return this.m_converter.ToUInt32(element);
		}

		/// <summary>Retrieves a 64-bit signed integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The 64-bit signed integer associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a 64-bit signed integer. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06004AEA RID: 19178 RVA: 0x0010BF98 File Offset: 0x0010A198
		public long GetInt64(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(long))
			{
				return (long)element;
			}
			return this.m_converter.ToInt64(element);
		}

		/// <summary>Retrieves a 64-bit unsigned integer value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The 64-bit unsigned integer associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a 64-bit unsigned integer. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06004AEB RID: 19179 RVA: 0x0010BFD0 File Offset: 0x0010A1D0
		[CLSCompliant(false)]
		public ulong GetUInt64(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(ulong))
			{
				return (ulong)element;
			}
			return this.m_converter.ToUInt64(element);
		}

		/// <summary>Retrieves a single-precision floating-point value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The single-precision floating-point value associated with <paramref name="name" />.</returns>
		/// <param name="name">The name of the value to retrieve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a single-precision floating-point value. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06004AEC RID: 19180 RVA: 0x0010C008 File Offset: 0x0010A208
		public float GetSingle(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(float))
			{
				return (float)element;
			}
			return this.m_converter.ToSingle(element);
		}

		/// <summary>Retrieves a double-precision floating-point value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The double-precision floating-point value associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a double-precision floating-point value. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06004AED RID: 19181 RVA: 0x0010C040 File Offset: 0x0010A240
		public double GetDouble(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(double))
			{
				return (double)element;
			}
			return this.m_converter.ToDouble(element);
		}

		/// <summary>Retrieves a decimal value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>A decimal value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.  </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a decimal. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06004AEE RID: 19182 RVA: 0x0010C078 File Offset: 0x0010A278
		public decimal GetDecimal(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(decimal))
			{
				return (decimal)element;
			}
			return this.m_converter.ToDecimal(element);
		}

		/// <summary>Retrieves a <see cref="T:System.DateTime" /> value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> value associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.  </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a <see cref="T:System.DateTime" /> value. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06004AEF RID: 19183 RVA: 0x0010C0B0 File Offset: 0x0010A2B0
		public DateTime GetDateTime(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(DateTime))
			{
				return (DateTime)element;
			}
			return this.m_converter.ToDateTime(element);
		}

		/// <summary>Retrieves a <see cref="T:System.String" /> value from the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> store.</summary>
		/// <returns>The <see cref="T:System.String" /> associated with <paramref name="name" />.</returns>
		/// <param name="name">The name associated with the value to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The value associated with <paramref name="name" /> cannot be converted to a <see cref="T:System.String" />. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element with the specified name is not found in the current instance. </exception>
		// Token: 0x06004AF0 RID: 19184 RVA: 0x0010C0E8 File Offset: 0x0010A2E8
		public string GetString(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(string) || element == null)
			{
				return (string)element;
			}
			return this.m_converter.ToString(element);
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06004AF1 RID: 19185 RVA: 0x0010C122 File Offset: 0x0010A322
		internal string[] MemberNames
		{
			get
			{
				return this.m_members;
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06004AF2 RID: 19186 RVA: 0x0010C12A File Offset: 0x0010A32A
		internal object[] MemberValues
		{
			get
			{
				return this.m_data;
			}
		}

		// Token: 0x04002708 RID: 9992
		private const int defaultSize = 4;

		// Token: 0x04002709 RID: 9993
		private const string s_mscorlibAssemblySimpleName = "mscorlib";

		// Token: 0x0400270A RID: 9994
		private const string s_mscorlibFileName = "mscorlib.dll";

		// Token: 0x0400270B RID: 9995
		internal string[] m_members;

		// Token: 0x0400270C RID: 9996
		internal object[] m_data;

		// Token: 0x0400270D RID: 9997
		internal Type[] m_types;

		// Token: 0x0400270E RID: 9998
		private Dictionary<string, int> m_nameToIndex;

		// Token: 0x0400270F RID: 9999
		internal int m_currMember;

		// Token: 0x04002710 RID: 10000
		internal IFormatterConverter m_converter;

		// Token: 0x04002711 RID: 10001
		private string m_fullTypeName;

		// Token: 0x04002712 RID: 10002
		private string m_assemName;

		// Token: 0x04002713 RID: 10003
		private Type objectType;

		// Token: 0x04002714 RID: 10004
		private bool isFullTypeNameSetExplicit;

		// Token: 0x04002715 RID: 10005
		private bool isAssemblyNameSetExplicit;

		// Token: 0x04002716 RID: 10006
		private bool requireSameTokenInPartialTrust;
	}
}
