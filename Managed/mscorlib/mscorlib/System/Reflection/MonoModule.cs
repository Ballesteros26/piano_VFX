using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x0200032F RID: 815
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Module))]
	[Serializable]
	internal class MonoModule : RuntimeModule
	{
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x00083085 File Offset: 0x00081285
		public override Assembly Assembly
		{
			get
			{
				return this.assembly;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x00081E8C File Offset: 0x0008008C
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x0008308D File Offset: 0x0008128D
		public override string ScopeName
		{
			get
			{
				return this.scopename;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x00083095 File Offset: 0x00081295
		public override int MDStreamVersion
		{
			get
			{
				if (this._impl == IntPtr.Zero)
				{
					throw new NotSupportedException();
				}
				return Module.GetMDStreamVersion(this._impl);
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060023F1 RID: 9201 RVA: 0x00081E94 File Offset: 0x00080094
		public override Guid ModuleVersionId
		{
			get
			{
				return this.GetModuleVersionId();
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x000830BA File Offset: 0x000812BA
		public override string FullyQualifiedName
		{
			get
			{
				if (SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fqname).Demand();
				}
				return this.fqname;
			}
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x000830DA File Offset: 0x000812DA
		public override bool IsResource()
		{
			return this.is_resource;
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x000830E4 File Offset: 0x000812E4
		public override Type[] FindTypes(TypeFilter filter, object filterCriteria)
		{
			List<Type> list = new List<Type>();
			foreach (Type type in this.GetTypes())
			{
				if (filter(type, filterCriteria))
				{
					list.Add(type);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x0007F7D9 File Offset: 0x0007D9D9
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x0007F7E2 File Offset: 0x0007D9E2
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x00083128 File Offset: 0x00081328
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.IsResource())
			{
				return null;
			}
			Type globalType = base.GetGlobalType();
			if (!(globalType != null))
			{
				return null;
			}
			return globalType.GetField(name, bindingAttr);
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x00083168 File Offset: 0x00081368
		public override FieldInfo[] GetFields(BindingFlags bindingFlags)
		{
			if (this.IsResource())
			{
				return new FieldInfo[0];
			}
			Type globalType = base.GetGlobalType();
			if (!(globalType != null))
			{
				return new FieldInfo[0];
			}
			return globalType.GetFields(bindingFlags);
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060023F9 RID: 9209 RVA: 0x000831A2 File Offset: 0x000813A2
		public override int MetadataToken
		{
			get
			{
				return Module.get_MetadataToken(this);
			}
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x000831AC File Offset: 0x000813AC
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (this.IsResource())
			{
				return null;
			}
			Type globalType = base.GetGlobalType();
			if (globalType == null)
			{
				return null;
			}
			if (types == null)
			{
				return globalType.GetMethod(name);
			}
			return globalType.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x000831F0 File Offset: 0x000813F0
		public override MethodInfo[] GetMethods(BindingFlags bindingFlags)
		{
			if (this.IsResource())
			{
				return new MethodInfo[0];
			}
			Type globalType = base.GetGlobalType();
			if (!(globalType != null))
			{
				return new MethodInfo[0];
			}
			return globalType.GetMethods(bindingFlags);
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x0008322C File Offset: 0x0008142C
		public override void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			base.ModuleHandle.GetPEKind(out peKind, out machine);
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x00083249 File Offset: 0x00081449
		public override Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			if (className == null)
			{
				throw new ArgumentNullException("className");
			}
			if (className == string.Empty)
			{
				throw new ArgumentException("Type name can't be empty");
			}
			return this.assembly.InternalGetType(this, className, throwOnError, ignoreCase);
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x000330F9 File Offset: 0x000312F9
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x00083280 File Offset: 0x00081480
		public override FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = Module.ResolveFieldToken(this._impl, metadataToken, base.ptrs_from_types(genericTypeArguments), base.ptrs_from_types(genericMethodArguments), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw base.resolve_token_exception(metadataToken, resolveTokenError, "Field");
			}
			return FieldInfo.GetFieldFromHandle(new RuntimeFieldHandle(intPtr));
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x000832D0 File Offset: 0x000814D0
		public override MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			ResolveTokenError resolveTokenError;
			MemberInfo memberInfo = Module.ResolveMemberToken(this._impl, metadataToken, base.ptrs_from_types(genericTypeArguments), base.ptrs_from_types(genericMethodArguments), out resolveTokenError);
			if (memberInfo == null)
			{
				throw base.resolve_token_exception(metadataToken, resolveTokenError, "MemberInfo");
			}
			return memberInfo;
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x00083314 File Offset: 0x00081514
		public override MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = Module.ResolveMethodToken(this._impl, metadataToken, base.ptrs_from_types(genericTypeArguments), base.ptrs_from_types(genericMethodArguments), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw base.resolve_token_exception(metadataToken, resolveTokenError, "MethodBase");
			}
			return MethodBase.GetMethodFromHandleNoGenericCheck(new RuntimeMethodHandle(intPtr));
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x00083364 File Offset: 0x00081564
		public override string ResolveString(int metadataToken)
		{
			ResolveTokenError resolveTokenError;
			string text = Module.ResolveStringToken(this._impl, metadataToken, out resolveTokenError);
			if (text == null)
			{
				throw base.resolve_token_exception(metadataToken, resolveTokenError, "string");
			}
			return text;
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x00083394 File Offset: 0x00081594
		public override Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = Module.ResolveTypeToken(this._impl, metadataToken, base.ptrs_from_types(genericTypeArguments), base.ptrs_from_types(genericMethodArguments), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw base.resolve_token_exception(metadataToken, resolveTokenError, "Type");
			}
			return Type.GetTypeFromHandle(new RuntimeTypeHandle(intPtr));
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x000833E4 File Offset: 0x000815E4
		public override byte[] ResolveSignature(int metadataToken)
		{
			ResolveTokenError resolveTokenError;
			byte[] array = Module.ResolveSignature(this._impl, metadataToken, out resolveTokenError);
			if (array == null)
			{
				throw base.resolve_token_exception(metadataToken, resolveTokenError, "signature");
			}
			return array;
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x00083412 File Offset: 0x00081612
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, 5, this.ScopeName, this.GetRuntimeAssembly());
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x00083438 File Offset: 0x00081638
		public override X509Certificate GetSignerCertificate()
		{
			X509Certificate x509Certificate;
			try
			{
				x509Certificate = X509Certificate.CreateFromSignedFile(this.assembly.Location);
			}
			catch
			{
				x509Certificate = null;
			}
			return x509Certificate;
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x00083470 File Offset: 0x00081670
		public override Type[] GetTypes()
		{
			return base.InternalGetTypes();
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x00083478 File Offset: 0x00081678
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x00083480 File Offset: 0x00081680
		internal RuntimeAssembly GetRuntimeAssembly()
		{
			return (RuntimeAssembly)this.assembly;
		}
	}
}
