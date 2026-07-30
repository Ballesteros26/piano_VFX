using System;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Policy;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x02000321 RID: 801
	internal abstract class RuntimeAssembly : Assembly
	{
		// Token: 0x06002339 RID: 9017 RVA: 0x0008209A File Offset: 0x0008029A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, 6, this.FullName, this);
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x00014B5A File Offset: 0x00012D5A
		internal static RuntimeAssembly GetExecutingAssembly(ref StackCrawlMark stackMark)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x000820B8 File Offset: 0x000802B8
		[SecurityCritical]
		internal static AssemblyName CreateAssemblyName(string assemblyString, bool forIntrospection, out RuntimeAssembly assemblyFromResolveEvent)
		{
			if (assemblyString == null)
			{
				throw new ArgumentNullException("assemblyString");
			}
			if (assemblyString.Length == 0 || assemblyString[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("String cannot have zero length."));
			}
			if (forIntrospection)
			{
				AppDomain.CheckReflectionOnlyLoadSupported();
			}
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Name = assemblyString;
			assemblyFromResolveEvent = null;
			return assemblyName;
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x0008210B File Offset: 0x0008030B
		internal static RuntimeAssembly InternalLoadAssemblyName(AssemblyName assemblyRef, Evidence assemblySecurity, RuntimeAssembly reqAssembly, ref StackCrawlMark stackMark, bool throwOnFileNotFound, bool forIntrospection, bool suppressSecurityChecks)
		{
			if (assemblyRef == null)
			{
				throw new ArgumentNullException("assemblyRef");
			}
			if (assemblyRef.CodeBase != null)
			{
				AppDomain.CheckLoadFromSupported();
			}
			assemblyRef = (AssemblyName)assemblyRef.Clone();
			if (assemblySecurity != null)
			{
			}
			return (RuntimeAssembly)Assembly.Load(assemblyRef);
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x00082146 File Offset: 0x00080346
		internal static RuntimeAssembly LoadWithPartialNameInternal(string partialName, Evidence securityEvidence, ref StackCrawlMark stackMark)
		{
			return (RuntimeAssembly)Assembly.LoadWithPartialName(partialName, securityEvidence);
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x00082154 File Offset: 0x00080354
		internal static RuntimeAssembly LoadWithPartialNameInternal(AssemblyName an, Evidence securityEvidence, ref StackCrawlMark stackMark)
		{
			return RuntimeAssembly.LoadWithPartialNameInternal(an.ToString(), securityEvidence, ref stackMark);
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x00082163 File Offset: 0x00080363
		public override AssemblyName GetName(bool copiedName)
		{
			if (SecurityManager.SecurityEnabled)
			{
				string codeBase = this.CodeBase;
			}
			return AssemblyName.Create(this, true);
		}
	}
}
