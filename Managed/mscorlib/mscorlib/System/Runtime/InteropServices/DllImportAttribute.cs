using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that the attributed method is exposed by an unmanaged dynamic-link library (DLL) as a static entry point.</summary>
	// Token: 0x020008C7 RID: 2247
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public sealed class DllImportAttribute : Attribute
	{
		// Token: 0x06005517 RID: 21783 RVA: 0x00128650 File Offset: 0x00126850
		[SecurityCritical]
		internal static Attribute GetCustomAttribute(RuntimeMethodInfo method)
		{
			if ((method.Attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PrivateScope)
			{
				return null;
			}
			string text = null;
			int metadataToken = method.MetadataToken;
			PInvokeAttributes pinvokeAttributes = PInvokeAttributes.CharSetNotSpec;
			string text2;
			((MonoMethod)method).GetPInvoke(out pinvokeAttributes, out text2, out text);
			CharSet charSet = CharSet.None;
			switch (pinvokeAttributes & PInvokeAttributes.CharSetMask)
			{
			case PInvokeAttributes.CharSetNotSpec:
				charSet = CharSet.None;
				break;
			case PInvokeAttributes.CharSetAnsi:
				charSet = CharSet.Ansi;
				break;
			case PInvokeAttributes.CharSetUnicode:
				charSet = CharSet.Unicode;
				break;
			case PInvokeAttributes.CharSetMask:
				charSet = CharSet.Auto;
				break;
			}
			CallingConvention callingConvention = CallingConvention.Cdecl;
			PInvokeAttributes pinvokeAttributes2 = pinvokeAttributes & PInvokeAttributes.CallConvMask;
			if (pinvokeAttributes2 <= PInvokeAttributes.CallConvCdecl)
			{
				if (pinvokeAttributes2 != PInvokeAttributes.CallConvWinapi)
				{
					if (pinvokeAttributes2 == PInvokeAttributes.CallConvCdecl)
					{
						callingConvention = CallingConvention.Cdecl;
					}
				}
				else
				{
					callingConvention = CallingConvention.Winapi;
				}
			}
			else if (pinvokeAttributes2 != PInvokeAttributes.CallConvStdcall)
			{
				if (pinvokeAttributes2 != PInvokeAttributes.CallConvThiscall)
				{
					if (pinvokeAttributes2 == PInvokeAttributes.CallConvFastcall)
					{
						callingConvention = CallingConvention.FastCall;
					}
				}
				else
				{
					callingConvention = CallingConvention.ThisCall;
				}
			}
			else
			{
				callingConvention = CallingConvention.StdCall;
			}
			bool flag = (pinvokeAttributes & PInvokeAttributes.NoMangle) > PInvokeAttributes.CharSetNotSpec;
			bool flag2 = (pinvokeAttributes & PInvokeAttributes.SupportsLastError) > PInvokeAttributes.CharSetNotSpec;
			bool flag3 = (pinvokeAttributes & PInvokeAttributes.BestFitMask) == PInvokeAttributes.BestFitEnabled;
			bool flag4 = (pinvokeAttributes & PInvokeAttributes.ThrowOnUnmappableCharMask) == PInvokeAttributes.ThrowOnUnmappableCharEnabled;
			bool flag5 = (method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) > MethodImplAttributes.IL;
			return new DllImportAttribute(text, text2, charSet, flag, flag2, flag5, callingConvention, flag3, flag4);
		}

		// Token: 0x06005518 RID: 21784 RVA: 0x00128770 File Offset: 0x00126970
		internal static bool IsDefined(RuntimeMethodInfo method)
		{
			return (method.Attributes & MethodAttributes.PinvokeImpl) > MethodAttributes.PrivateScope;
		}

		// Token: 0x06005519 RID: 21785 RVA: 0x00128784 File Offset: 0x00126984
		internal DllImportAttribute(string dllName, string entryPoint, CharSet charSet, bool exactSpelling, bool setLastError, bool preserveSig, CallingConvention callingConvention, bool bestFitMapping, bool throwOnUnmappableChar)
		{
			this._val = dllName;
			this.EntryPoint = entryPoint;
			this.CharSet = charSet;
			this.ExactSpelling = exactSpelling;
			this.SetLastError = setLastError;
			this.PreserveSig = preserveSig;
			this.CallingConvention = callingConvention;
			this.BestFitMapping = bestFitMapping;
			this.ThrowOnUnmappableChar = throwOnUnmappableChar;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.DllImportAttribute" /> class with the name of the DLL containing the method to import.</summary>
		/// <param name="dllName">The name of the DLL that contains the unmanaged method. This can include an assembly display name, if the DLL is included in an assembly.</param>
		// Token: 0x0600551A RID: 21786 RVA: 0x001287DC File Offset: 0x001269DC
		public DllImportAttribute(string dllName)
		{
			this._val = dllName;
		}

		/// <summary>Gets the name of the DLL file that contains the entry point.</summary>
		/// <returns>The name of the DLL file that contains the entry point.</returns>
		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x0600551B RID: 21787 RVA: 0x001287EB File Offset: 0x001269EB
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002C99 RID: 11417
		internal string _val;

		/// <summary>Indicates the name or ordinal of the DLL entry point to be called.</summary>
		// Token: 0x04002C9A RID: 11418
		public string EntryPoint;

		/// <summary>Indicates how to marshal string parameters to the method and controls name mangling.</summary>
		// Token: 0x04002C9B RID: 11419
		public CharSet CharSet;

		/// <summary>Indicates whether the callee calls the SetLastError Win32 API function before returning from the attributed method.</summary>
		// Token: 0x04002C9C RID: 11420
		public bool SetLastError;

		/// <summary>Controls whether the <see cref="F:System.Runtime.InteropServices.DllImportAttribute.CharSet" /> field causes the common language runtime to search an unmanaged DLL for entry-point names other than the one specified.</summary>
		// Token: 0x04002C9D RID: 11421
		public bool ExactSpelling;

		/// <summary>Indicates whether unmanaged methods that have HRESULT or retval return values are directly translated or whether HRESULT or retval return values are automatically converted to exceptions.</summary>
		// Token: 0x04002C9E RID: 11422
		public bool PreserveSig;

		/// <summary>Indicates the calling convention of an entry point.</summary>
		// Token: 0x04002C9F RID: 11423
		public CallingConvention CallingConvention;

		/// <summary>Enables or disables best-fit mapping behavior when converting Unicode characters to ANSI characters.</summary>
		// Token: 0x04002CA0 RID: 11424
		public bool BestFitMapping;

		/// <summary>Enables or disables the throwing of an exception on an unmappable Unicode character that is converted to an ANSI "?" character.</summary>
		// Token: 0x04002CA1 RID: 11425
		public bool ThrowOnUnmappableChar;
	}
}
