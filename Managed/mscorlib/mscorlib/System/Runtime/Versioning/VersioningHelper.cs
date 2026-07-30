using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using Microsoft.Win32;

namespace System.Runtime.Versioning
{
	/// <summary>Provides methods to aid developers in writing version-safe code. This class cannot be inherited.</summary>
	// Token: 0x020006C4 RID: 1732
	public static class VersioningHelper
	{
		// Token: 0x06004986 RID: 18822
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRuntimeId();

		/// <summary>Returns a version-safe name based on the specified resource name and the intended resource consumption source.</summary>
		/// <returns>A version-safe name.</returns>
		/// <param name="name">The name of the resource.</param>
		/// <param name="from">The scope of the resource.</param>
		/// <param name="to">The desired resource consumption scope.</param>
		// Token: 0x06004987 RID: 18823 RVA: 0x00107CDA File Offset: 0x00105EDA
		public static string MakeVersionSafeName(string name, ResourceScope from, ResourceScope to)
		{
			return VersioningHelper.MakeVersionSafeName(name, from, to, null);
		}

		/// <summary>Returns a version-safe name based on the specified resource name, the intended resource consumption scope, and the type using the resource.</summary>
		/// <returns>A version-safe name.</returns>
		/// <param name="name">The name of the resource.</param>
		/// <param name="from">The beginning of the scope range.</param>
		/// <param name="to">The end of the scope range.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the resource.</param>
		/// <exception cref="T:System.ArgumentException">The values for <paramref name="from " />and <paramref name="to " />are invalid. The resource type in the <see cref="T:System.Runtime.Versioning.ResourceScope" />  enumeration is going from a more restrictive resource type to a more general resource type.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type " />is null.</exception>
		// Token: 0x06004988 RID: 18824 RVA: 0x00107CE8 File Offset: 0x00105EE8
		[SecuritySafeCritical]
		public static string MakeVersionSafeName(string name, ResourceScope from, ResourceScope to, Type type)
		{
			ResourceScope resourceScope = from & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library);
			ResourceScope resourceScope2 = to & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library);
			if (resourceScope > resourceScope2)
			{
				throw new ArgumentException(Environment.GetResourceString("Resource type in the ResourceScope enum is going from a more restrictive resource type to a more general one.  From: \"{0}\"  To: \"{1}\"", new object[] { resourceScope, resourceScope2 }), "from");
			}
			SxSRequirements requirements = VersioningHelper.GetRequirements(to, from);
			if ((requirements & (SxSRequirements.AssemblyName | SxSRequirements.TypeName)) != SxSRequirements.None && type == null)
			{
				throw new ArgumentNullException("type", Environment.GetResourceString("The type parameter cannot be null when scoping the resource's visibility to Private or Assembly."));
			}
			StringBuilder stringBuilder = new StringBuilder(name);
			char c = '_';
			if ((requirements & SxSRequirements.ProcessID) != SxSRequirements.None)
			{
				stringBuilder.Append(c);
				stringBuilder.Append('p');
				stringBuilder.Append(NativeMethods.GetCurrentProcessId());
			}
			if ((requirements & SxSRequirements.CLRInstanceID) != SxSRequirements.None)
			{
				string clrinstanceString = VersioningHelper.GetCLRInstanceString();
				stringBuilder.Append(c);
				stringBuilder.Append('r');
				stringBuilder.Append(clrinstanceString);
			}
			if ((requirements & SxSRequirements.AppDomainID) != SxSRequirements.None)
			{
				stringBuilder.Append(c);
				stringBuilder.Append("ad");
				stringBuilder.Append(AppDomain.CurrentDomain.Id);
			}
			if ((requirements & SxSRequirements.TypeName) != SxSRequirements.None)
			{
				stringBuilder.Append(c);
				stringBuilder.Append(type.Name);
			}
			if ((requirements & SxSRequirements.AssemblyName) != SxSRequirements.None)
			{
				stringBuilder.Append(c);
				stringBuilder.Append(type.Assembly.FullName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004989 RID: 18825 RVA: 0x00107E18 File Offset: 0x00106018
		private static string GetCLRInstanceString()
		{
			return VersioningHelper.GetRuntimeId().ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600498A RID: 18826 RVA: 0x00107E38 File Offset: 0x00106038
		private static SxSRequirements GetRequirements(ResourceScope consumeAsScope, ResourceScope calleeScope)
		{
			SxSRequirements sxSRequirements = SxSRequirements.None;
			switch (calleeScope & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library))
			{
			case ResourceScope.Machine:
				switch (consumeAsScope & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library))
				{
				case ResourceScope.Machine:
					goto IL_009F;
				case ResourceScope.Process:
					sxSRequirements |= SxSRequirements.ProcessID;
					goto IL_009F;
				case ResourceScope.AppDomain:
					sxSRequirements |= SxSRequirements.AppDomainID | SxSRequirements.ProcessID | SxSRequirements.CLRInstanceID;
					goto IL_009F;
				}
				throw new ArgumentException(Environment.GetResourceString("Unknown value for the ResourceScope: {0}  Too many resource type bits may be set.", new object[] { consumeAsScope }), "consumeAsScope");
			case ResourceScope.Process:
				if ((consumeAsScope & ResourceScope.AppDomain) != ResourceScope.None)
				{
					sxSRequirements |= SxSRequirements.AppDomainID | SxSRequirements.CLRInstanceID;
					goto IL_009F;
				}
				goto IL_009F;
			case ResourceScope.AppDomain:
				goto IL_009F;
			}
			throw new ArgumentException(Environment.GetResourceString("Unknown value for the ResourceScope: {0}  Too many resource type bits may be set.", new object[] { calleeScope }), "calleeScope");
			IL_009F:
			ResourceScope resourceScope = calleeScope & (ResourceScope.Private | ResourceScope.Assembly);
			if (resourceScope != ResourceScope.None)
			{
				if (resourceScope != ResourceScope.Private)
				{
					if (resourceScope != ResourceScope.Assembly)
					{
						throw new ArgumentException(Environment.GetResourceString("Unknown value for the ResourceScope: {0}  Too many resource visibility bits may be set.", new object[] { calleeScope }), "calleeScope");
					}
					if ((consumeAsScope & ResourceScope.Private) != ResourceScope.None)
					{
						sxSRequirements |= SxSRequirements.TypeName;
					}
				}
			}
			else
			{
				resourceScope = consumeAsScope & (ResourceScope.Private | ResourceScope.Assembly);
				if (resourceScope != ResourceScope.None)
				{
					if (resourceScope != ResourceScope.Private)
					{
						if (resourceScope != ResourceScope.Assembly)
						{
							throw new ArgumentException(Environment.GetResourceString("Unknown value for the ResourceScope: {0}  Too many resource visibility bits may be set.", new object[] { consumeAsScope }), "consumeAsScope");
						}
						sxSRequirements |= SxSRequirements.AssemblyName;
					}
					else
					{
						sxSRequirements |= SxSRequirements.AssemblyName | SxSRequirements.TypeName;
					}
				}
			}
			return sxSRequirements;
		}

		// Token: 0x04002699 RID: 9881
		private const ResourceScope ResTypeMask = ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library;

		// Token: 0x0400269A RID: 9882
		private const ResourceScope VisibilityMask = ResourceScope.Private | ResourceScope.Assembly;
	}
}
