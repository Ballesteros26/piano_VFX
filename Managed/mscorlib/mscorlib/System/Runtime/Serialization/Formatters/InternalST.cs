using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Logs tracing messages when the .NET Framework serialization infrastructure is compiled.</summary>
	// Token: 0x020006FF RID: 1791
	[ComVisible(true)]
	[SecurityCritical]
	public sealed class InternalST
	{
		// Token: 0x06004B2C RID: 19244 RVA: 0x00002111 File Offset: 0x00000311
		private InternalST()
		{
		}

		/// <summary>Prints SOAP trace messages.</summary>
		/// <param name="messages">An array of trace messages to print.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293" Name="System.Runtime.Serialization.Formatters.Soap" />
		/// </PermissionSet>
		// Token: 0x06004B2D RID: 19245 RVA: 0x00002194 File Offset: 0x00000394
		[Conditional("_LOGGING")]
		public static void InfoSoap(params object[] messages)
		{
		}

		/// <summary>Checks if SOAP tracing is enabled.</summary>
		/// <returns>true, if tracing is enabled; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293" Name="System.Runtime.Serialization.Formatters.Soap" />
		/// </PermissionSet>
		// Token: 0x06004B2E RID: 19246 RVA: 0x0010C7C1 File Offset: 0x0010A9C1
		public static bool SoapCheckEnabled()
		{
			return BCLDebug.CheckEnabled("Soap");
		}

		/// <summary>Processes the specified array of messages.</summary>
		/// <param name="messages">An array of messages to process.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293" Name="System.Runtime.Serialization.Formatters.Soap" />
		/// </PermissionSet>
		// Token: 0x06004B2F RID: 19247 RVA: 0x0010C7CD File Offset: 0x0010A9CD
		[Conditional("SER_LOGGING")]
		public static void Soap(params object[] messages)
		{
			if (!(messages[0] is string))
			{
				messages[0] = messages[0].GetType().Name + " ";
				return;
			}
			messages[0] = messages[0] + " ";
		}

		/// <summary>Asserts the specified message.</summary>
		/// <param name="condition">A Boolean value to use when asserting.</param>
		/// <param name="message">The message to use when asserting.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293" Name="System.Runtime.Serialization.Formatters.Soap" />
		/// </PermissionSet>
		// Token: 0x06004B30 RID: 19248 RVA: 0x00002194 File Offset: 0x00000394
		[Conditional("_DEBUG")]
		public static void SoapAssert(bool condition, string message)
		{
		}

		/// <summary>Sets the value of a field.</summary>
		/// <param name="fi">A <see cref="T:System.Reflection.FieldInfo" /> containing data about the target field.</param>
		/// <param name="target">The field to change.</param>
		/// <param name="value">The value to set.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293" Name="System.Runtime.Serialization.Formatters.Soap" />
		/// </PermissionSet>
		// Token: 0x06004B31 RID: 19249 RVA: 0x0010C804 File Offset: 0x0010AA04
		public static void SerializationSetValue(FieldInfo fi, object target, object value)
		{
			if (fi == null)
			{
				throw new ArgumentNullException("fi");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			FormatterServices.SerializationSetValue(fi, target, value);
		}

		/// <summary>Loads a specified assembly to debug.</summary>
		/// <returns>The <see cref="T:System.Reflection.Assembly" /> to debug.</returns>
		/// <param name="assemblyString">The name of the assembly to load.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293" Name="System.Runtime.Serialization.Formatters.Soap" />
		/// </PermissionSet>
		// Token: 0x06004B32 RID: 19250 RVA: 0x0010C83E File Offset: 0x0010AA3E
		public static Assembly LoadAssemblyFromString(string assemblyString)
		{
			return FormatterServices.LoadAssemblyFromString(assemblyString);
		}
	}
}
