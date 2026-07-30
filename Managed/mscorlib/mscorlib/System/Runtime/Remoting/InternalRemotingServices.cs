using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Remoting
{
	/// <summary>Defines utility methods for use by the .NET Framework remoting infrastructure.</summary>
	// Token: 0x02000751 RID: 1873
	[ComVisible(true)]
	public class InternalRemotingServices
	{
		/// <summary>Sends a message concerning a remoting channel to an unmanaged debugger. </summary>
		/// <param name="s">A string to place in the message.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004D56 RID: 19798 RVA: 0x00014B5A File Offset: 0x00012D5A
		[Conditional("_LOGGING")]
		public static void DebugOutChnl(string s)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets an appropriate SOAP-related attribute for the specified class member or method parameter. </summary>
		/// <returns>The SOAP-related attribute for the specified class member or method parameter.</returns>
		/// <param name="reflectionObject">A class member or method parameter.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004D57 RID: 19799 RVA: 0x00116E04 File Offset: 0x00115004
		public static SoapAttribute GetCachedSoapAttribute(object reflectionObject)
		{
			object syncRoot = InternalRemotingServices._soapAttributes.SyncRoot;
			SoapAttribute soapAttribute2;
			lock (syncRoot)
			{
				SoapAttribute soapAttribute = InternalRemotingServices._soapAttributes[reflectionObject] as SoapAttribute;
				if (soapAttribute != null)
				{
					soapAttribute2 = soapAttribute;
				}
				else
				{
					object[] customAttributes = ((ICustomAttributeProvider)reflectionObject).GetCustomAttributes(typeof(SoapAttribute), true);
					if (customAttributes.Length != 0)
					{
						soapAttribute = (SoapAttribute)customAttributes[0];
					}
					else if (reflectionObject is Type)
					{
						soapAttribute = new SoapTypeAttribute();
					}
					else if (reflectionObject is FieldInfo)
					{
						soapAttribute = new SoapFieldAttribute();
					}
					else if (reflectionObject is MethodBase)
					{
						soapAttribute = new SoapMethodAttribute();
					}
					else if (reflectionObject is ParameterInfo)
					{
						soapAttribute = new SoapParameterAttribute();
					}
					soapAttribute.SetReflectionObject(reflectionObject);
					InternalRemotingServices._soapAttributes[reflectionObject] = soapAttribute;
					soapAttribute2 = soapAttribute;
				}
			}
			return soapAttribute2;
		}

		/// <summary>Instructs an internal debugger to check for a condition and display a message if the condition is false. </summary>
		/// <param name="condition">true to prevent a message from being displayed; otherwise, false.</param>
		/// <param name="message">The message to display if <paramref name="condition" /> is false.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004D58 RID: 19800 RVA: 0x00014B5A File Offset: 0x00012D5A
		[Conditional("_DEBUG")]
		public static void RemotingAssert(bool condition, string message)
		{
			throw new NotSupportedException();
		}

		/// <summary>Sends any number of messages concerning remoting channels to an internal debugger. </summary>
		/// <param name="messages">An array of type <see cref="T:System.Object" /> that contains any number of messages.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004D59 RID: 19801 RVA: 0x00014B5A File Offset: 0x00012D5A
		[Conditional("_LOGGING")]
		public static void RemotingTrace(params object[] messages)
		{
			throw new NotSupportedException();
		}

		/// <summary>Sets internal identifying information for a remoted server object for each method call from client to server.</summary>
		/// <param name="m">A <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" /> that represents a method call on a remote object.</param>
		/// <param name="srvID">Internal identifying information for a remoted server object.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004D5A RID: 19802 RVA: 0x00116EDC File Offset: 0x001150DC
		[CLSCompliant(false)]
		public static void SetServerIdentity(MethodCall m, object srvID)
		{
			Identity identity = srvID as Identity;
			if (identity == null)
			{
				throw new ArgumentException("srvID");
			}
			RemotingServices.SetMessageTargetIdentity(m, identity);
		}

		// Token: 0x04002999 RID: 10649
		private static Hashtable _soapAttributes = new Hashtable();
	}
}
