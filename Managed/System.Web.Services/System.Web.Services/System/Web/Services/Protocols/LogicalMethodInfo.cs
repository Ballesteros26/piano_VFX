using System;
using System.Collections;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;

namespace System.Web.Services.Protocols
{
	/// <summary>Represents the attributes and metadata for an XML Web service method. This class cannot be inherited.</summary>
	// Token: 0x0200003F RID: 63
	public sealed class LogicalMethodInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> class with the <see cref="T:System.Reflection.MethodInfo" /> passed in.</summary>
		/// <param name="methodInfo">A <see cref="T:System.Reflection.MethodInfo" /> to initialize the properties of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> common to the <see cref="T:System.Reflection.MethodInfo" />. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Reflection.MethodBase.IsStatic" /> property of the <paramref name="methodInfo" /> parameter is true.-or- The <see cref="M:System.Reflection.MethodBase.GetParameters" /> method of the <paramref name="methodInfo" /> parameter does not contain all the parameters required by the method represented by the instance of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />. </exception>
		// Token: 0x06000137 RID: 311 RVA: 0x00005ED0 File Offset: 0x000040D0
		public LogicalMethodInfo(MethodInfo methodInfo)
			: this(methodInfo, null)
		{
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005EDC File Offset: 0x000040DC
		internal LogicalMethodInfo(MethodInfo methodInfo, WebMethod webMethod)
		{
			if (methodInfo.IsStatic)
			{
				throw new InvalidOperationException(Res.GetString("WebMethodStatic", new object[] { methodInfo.Name }));
			}
			this.methodInfo = methodInfo;
			if (webMethod != null)
			{
				this.binding = webMethod.binding;
				this.attribute = webMethod.attribute;
				this.declaration = webMethod.declaration;
			}
			MethodInfo methodInfo2 = ((this.declaration != null) ? this.declaration : methodInfo);
			this.parameters = methodInfo2.GetParameters();
			this.inParams = LogicalMethodInfo.GetInParameters(methodInfo2, this.parameters, 0, this.parameters.Length, false);
			this.outParams = LogicalMethodInfo.GetOutParameters(methodInfo2, this.parameters, 0, this.parameters.Length, false);
			this.retType = methodInfo2.ReturnType;
			this.isVoid = this.retType == typeof(void);
			this.methodName = methodInfo2.Name;
			this.attributes = new Hashtable();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005FE0 File Offset: 0x000041E0
		private LogicalMethodInfo(MethodInfo beginMethodInfo, MethodInfo endMethodInfo, WebMethod webMethod)
		{
			this.methodInfo = beginMethodInfo;
			this.endMethodInfo = endMethodInfo;
			this.methodName = beginMethodInfo.Name.Substring(5);
			if (webMethod != null)
			{
				this.binding = webMethod.binding;
				this.attribute = webMethod.attribute;
				this.declaration = webMethod.declaration;
			}
			ParameterInfo[] array = beginMethodInfo.GetParameters();
			if (array.Length < 2 || array[array.Length - 1].ParameterType != typeof(object) || array[array.Length - 2].ParameterType != typeof(AsyncCallback))
			{
				throw new InvalidOperationException(Res.GetString("WebMethodMissingParams", new object[]
				{
					beginMethodInfo.DeclaringType.FullName,
					beginMethodInfo.Name,
					typeof(AsyncCallback).FullName,
					typeof(object).FullName
				}));
			}
			this.stateParam = array[array.Length - 1];
			this.callbackParam = array[array.Length - 2];
			this.inParams = LogicalMethodInfo.GetInParameters(beginMethodInfo, array, 0, array.Length - 2, true);
			ParameterInfo[] array2 = endMethodInfo.GetParameters();
			this.resultParam = array2[0];
			this.outParams = LogicalMethodInfo.GetOutParameters(endMethodInfo, array2, 1, array2.Length - 1, true);
			this.parameters = new ParameterInfo[this.inParams.Length + this.outParams.Length];
			this.inParams.CopyTo(this.parameters, 0);
			this.outParams.CopyTo(this.parameters, this.inParams.Length);
			this.retType = endMethodInfo.ReturnType;
			this.isVoid = this.retType == typeof(void);
			this.attributes = new Hashtable();
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</returns>
		// Token: 0x0600013A RID: 314 RVA: 0x0000619D File Offset: 0x0000439D
		public override string ToString()
		{
			return this.methodInfo.ToString();
		}

		/// <summary>Invokes the method represented by the current <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</summary>
		/// <returns>An array of type <see cref="T:System.Object" /> representing the return value and out parameters of the invoked method.</returns>
		/// <param name="target">The instance of the <see cref="T:System.Object" /> to invoke the method. </param>
		/// <param name="values">An argument list for the invoked method. This is an array of objects with the same number, order, and type as the parameters of the method. If the method does not require any parameters, the <paramref name="values" /> parameter should be null. </param>
		/// <exception cref="T:System.Reflection.TargetException">The <paramref name="target" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The number, type, and order of parameters in the <paramref name="values" /> parameter do not match the signature of the invoked method. </exception>
		/// <exception cref="T:System.MemberAccessException">The caller does not have permission to invoke the method. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The invoked method throws an exception. </exception>
		// Token: 0x0600013B RID: 315 RVA: 0x000061AC File Offset: 0x000043AC
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public object[] Invoke(object target, object[] values)
		{
			if (this.outParams.Length != 0)
			{
				object[] array = new object[this.parameters.Length];
				for (int i = 0; i < this.inParams.Length; i++)
				{
					array[this.inParams[i].Position] = values[i];
				}
				values = array;
			}
			object obj = this.methodInfo.Invoke(target, values);
			if (this.outParams.Length != 0)
			{
				int num = this.outParams.Length;
				if (!this.isVoid)
				{
					num++;
				}
				object[] array2 = new object[num];
				num = 0;
				if (!this.isVoid)
				{
					array2[num++] = obj;
				}
				for (int j = 0; j < this.outParams.Length; j++)
				{
					array2[num++] = values[this.outParams[j].Position];
				}
				return array2;
			}
			if (this.isVoid)
			{
				return LogicalMethodInfo.emptyObjectArray;
			}
			return new object[] { obj };
		}

		/// <summary>Begins an asynchronous invocation of the method represented by this <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> which is passed to <see cref="M:System.Web.Services.Protocols.LogicalMethodInfo.EndInvoke(System.Object,System.IAsyncResult)" /> to obtain the return values from the remote method call.</returns>
		/// <param name="target">The instance of the <see cref="T:System.Object" /> on which to invoke the method on. </param>
		/// <param name="values">An argument list for the invoked method. This is an array of objects with the same number, order, and type as the parameters of the method. If the method does not require any parameters, <paramref name="values" /> should be null. </param>
		/// <param name="callback">The delegate to call when the asynchronous invoke is complete. If <paramref name="callback" /> is null, the delegate is not called. </param>
		/// <param name="asyncState">State information that is passed on to the delegate. </param>
		/// <exception cref="T:System.Reflection.TargetException">The <paramref name="target" /> parameteris null. </exception>
		/// <exception cref="T:System.ArgumentException">The number, type, and order of parameters in <paramref name="values" /> do not match the signature of the invoked method. </exception>
		/// <exception cref="T:System.MemberAccessException">The caller does not have permission to invoke the method. </exception>
		// Token: 0x0600013C RID: 316 RVA: 0x0000628C File Offset: 0x0000448C
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public IAsyncResult BeginInvoke(object target, object[] values, AsyncCallback callback, object asyncState)
		{
			object[] array = new object[values.Length + 2];
			values.CopyTo(array, 0);
			array[values.Length] = callback;
			array[values.Length + 1] = asyncState;
			return (IAsyncResult)this.methodInfo.Invoke(target, array);
		}

		/// <summary>Ends an asynchronous invocation of the method represented by the current <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</summary>
		/// <returns>An array of objects containing the return value and any by-reference or out parameters of the derived class method.</returns>
		/// <param name="target">The instance of the <see cref="T:System.Object" /> on which to invoke the method. </param>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> returned from <see cref="M:System.Web.Services.Protocols.LogicalMethodInfo.BeginInvoke(System.Object,System.Object[],System.AsyncCallback,System.Object)" />. </param>
		/// <exception cref="T:System.Reflection.TargetException">The <paramref name="target" /> parameter is null. </exception>
		/// <exception cref="T:System.MemberAccessException">The caller does not have permission to invoke the method. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The invoked method throws an exception. </exception>
		// Token: 0x0600013D RID: 317 RVA: 0x000062D0 File Offset: 0x000044D0
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public object[] EndInvoke(object target, IAsyncResult asyncResult)
		{
			object[] array = new object[this.outParams.Length + 1];
			array[0] = asyncResult;
			object obj = this.endMethodInfo.Invoke(target, array);
			if (!this.isVoid)
			{
				array[0] = obj;
				return array;
			}
			if (this.outParams.Length != 0)
			{
				object[] array2 = new object[this.outParams.Length];
				Array.Copy(array, 1, array2, 0, array2.Length);
				return array2;
			}
			return LogicalMethodInfo.emptyObjectArray;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006337 File Offset: 0x00004537
		internal WebServiceBindingAttribute Binding
		{
			get
			{
				return this.binding;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000633F File Offset: 0x0000453F
		internal MethodInfo Declaration
		{
			get
			{
				return this.declaration;
			}
		}

		/// <summary>Gets the class that declares the method represented by the current <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</summary>
		/// <returns>The <see cref="T:System.Type" /> for the class declaring the method represented by the <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</returns>
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006347 File Offset: 0x00004547
		public Type DeclaringType
		{
			get
			{
				return this.methodInfo.DeclaringType;
			}
		}

		/// <summary>Gets the name of the method represented by this <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</summary>
		/// <returns>The name of the method represented by this <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</returns>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00006354 File Offset: 0x00004554
		public string Name
		{
			get
			{
				return this.methodName;
			}
		}

		/// <summary>Gets the return value of a Begin asynchronous method invocation.</summary>
		/// <returns>A <see cref="T:System.Reflection.ParameterInfo" /> representing the <see cref="T:System.IAsyncResult" /> returned from a Begin asynchronous method invocation.</returns>
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000635C File Offset: 0x0000455C
		public ParameterInfo AsyncResultParameter
		{
			get
			{
				return this.resultParam;
			}
		}

		/// <summary>Gets the parameter information for the <paramref name="AsyncCallback" /> parameter of a Begin method in an asynchronous invocation.</summary>
		/// <returns>A <see cref="T:System.Reflection.ParameterInfo" /> representing the <paramref name="AsyncCallback" /> parameter of a Begin asynchronous method invocation.</returns>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00006364 File Offset: 0x00004564
		public ParameterInfo AsyncCallbackParameter
		{
			get
			{
				return this.callbackParam;
			}
		}

		/// <summary>Gets the parameter information for the <paramref name="AsyncState" /> parameter of a Begin method in an asynchronous invocation.</summary>
		/// <returns>A <see cref="T:System.Reflection.ParameterInfo" /> representing the <paramref name="AsyncState" /> parameter of a Begin method in an asynchronous invocation.</returns>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000636C File Offset: 0x0000456C
		public ParameterInfo AsyncStateParameter
		{
			get
			{
				return this.stateParam;
			}
		}

		/// <summary>Gets the return type of this method.</summary>
		/// <returns>The <see cref="T:System.Type" /> returned by this method.</returns>
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00006374 File Offset: 0x00004574
		public Type ReturnType
		{
			get
			{
				return this.retType;
			}
		}

		/// <summary>Gets a value indicating whether the return type for the method represented by the instance of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> is void.</summary>
		/// <returns>true if the return type is void; otherwise, false.</returns>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000637C File Offset: 0x0000457C
		public bool IsVoid
		{
			get
			{
				return this.isVoid;
			}
		}

		/// <summary>Gets a value indicating whether the method represented by the instance of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> is invoked asynchronously.</summary>
		/// <returns>true if the method is invoked asynchronously; otherwise, false.</returns>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00006384 File Offset: 0x00004584
		public bool IsAsync
		{
			get
			{
				return this.endMethodInfo != null;
			}
		}

		/// <summary>Gets the parameters passed into the method represented by the instance of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</summary>
		/// <returns>An array of type <see cref="T:System.Reflection.ParameterInfo" /> containing the parameters passed into the method represented by the instance of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</returns>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00006392 File Offset: 0x00004592
		public ParameterInfo[] InParameters
		{
			get
			{
				return this.inParams;
			}
		}

		/// <summary>Gets the out parameters for the method.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.ParameterInfo" /> representing the out parameters for the method, in order.</returns>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000639A File Offset: 0x0000459A
		public ParameterInfo[] OutParameters
		{
			get
			{
				return this.outParams;
			}
		}

		/// <summary>Gets the parameters for the method.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.ParameterInfo" /> representing the parameters for the method.</returns>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000063A2 File Offset: 0x000045A2
		public ParameterInfo[] Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		/// <summary>Returns the custom attributes applied to the specified type.</summary>
		/// <returns>An array of <see cref="T:System.Object" /> containing the custom attributes applied to <paramref name="type" />.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> to which the custom attributes are applied. </param>
		/// <exception cref="T:System.TypeLoadException">The custom attribute type can not be loaded. </exception>
		// Token: 0x0600014B RID: 331 RVA: 0x000063AC File Offset: 0x000045AC
		public object[] GetCustomAttributes(Type type)
		{
			object[] array = null;
			array = (object[])this.attributes[type];
			if (array != null)
			{
				return array;
			}
			Hashtable hashtable = this.attributes;
			lock (hashtable)
			{
				array = (object[])this.attributes[type];
				if (array == null)
				{
					if (this.declaration != null)
					{
						object[] customAttributes = this.declaration.GetCustomAttributes(type, false);
						object[] customAttributes2 = this.methodInfo.GetCustomAttributes(type, false);
						if (customAttributes2.Length != 0)
						{
							if (!LogicalMethodInfo.CanMerge(type))
							{
								throw new InvalidOperationException(Res.GetString("ContractOverride", new object[]
								{
									this.methodInfo.Name,
									this.methodInfo.DeclaringType.FullName,
									this.declaration.DeclaringType.FullName,
									this.declaration.ToString(),
									customAttributes2[0].ToString()
								}));
							}
							ArrayList arrayList = new ArrayList();
							for (int i = 0; i < customAttributes.Length; i++)
							{
								arrayList.Add(customAttributes[i]);
							}
							for (int j = 0; j < customAttributes2.Length; j++)
							{
								arrayList.Add(customAttributes2[j]);
							}
							array = (object[])arrayList.ToArray(type);
						}
						else
						{
							array = customAttributes;
						}
					}
					else
					{
						array = this.methodInfo.GetCustomAttributes(type, false);
					}
					this.attributes[type] = array;
				}
			}
			return array;
		}

		/// <summary>Returns the first custom attribute applied to the type, if any custom attributes are applied to the type.</summary>
		/// <returns>An <see cref="T:System.Object" /> containing the first custom attribute applied to the <paramref name="type" /> parameter.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> to which the custom attributes are applied. </param>
		/// <exception cref="T:System.TypeLoadException">The custom attribute type can not be loaded. </exception>
		// Token: 0x0600014C RID: 332 RVA: 0x0000653C File Offset: 0x0000473C
		public object GetCustomAttribute(Type type)
		{
			object[] customAttributes = this.GetCustomAttributes(type);
			if (customAttributes.Length == 0)
			{
				return null;
			}
			return customAttributes[0];
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000655A File Offset: 0x0000475A
		internal WebMethodAttribute MethodAttribute
		{
			get
			{
				if (this.attribute == null)
				{
					this.attribute = (WebMethodAttribute)this.GetCustomAttribute(typeof(WebMethodAttribute));
					if (this.attribute == null)
					{
						this.attribute = new WebMethodAttribute();
					}
				}
				return this.attribute;
			}
		}

		/// <summary>Gets the custom attributes applied to the method.</summary>
		/// <returns>An <see cref="T:System.Reflection.ICustomAttributeProvider" /> representing the custom attributes for the method.</returns>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00006598 File Offset: 0x00004798
		public ICustomAttributeProvider CustomAttributeProvider
		{
			get
			{
				return this.methodInfo;
			}
		}

		/// <summary>Gets the custom attributes for the return type.</summary>
		/// <returns>An <see cref="T:System.Reflection.ICustomAttributeProvider" /> representing the custom attributes for the return type.</returns>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000065A0 File Offset: 0x000047A0
		public ICustomAttributeProvider ReturnTypeCustomAttributeProvider
		{
			get
			{
				if (this.declaration != null)
				{
					return this.declaration.ReturnTypeCustomAttributes;
				}
				return this.methodInfo.ReturnTypeCustomAttributes;
			}
		}

		/// <summary>Gets the attributes and metadata for a synchronous method.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> representing the attributes and metadata for a method. If <see cref="P:System.Web.Services.Protocols.LogicalMethodInfo.IsAsync" /> is true, then the value of this property is null.</returns>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000065C7 File Offset: 0x000047C7
		public MethodInfo MethodInfo
		{
			get
			{
				if (!(this.endMethodInfo == null))
				{
					return null;
				}
				return this.methodInfo;
			}
		}

		/// <summary>Gets the attributes and metadata for a Begin method in an asynchronous invocation.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> representing the attributes and metadata for a Begin asynchronous method invocation.</returns>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00006598 File Offset: 0x00004798
		public MethodInfo BeginMethodInfo
		{
			get
			{
				return this.methodInfo;
			}
		}

		/// <summary>Gets the attributes and metadata for an End method of an asynchronous invocation to a method.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> representing the attributes and metadata for an End asynchronous method invocation.</returns>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000065DF File Offset: 0x000047DF
		public MethodInfo EndMethodInfo
		{
			get
			{
				return this.endMethodInfo;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000065E8 File Offset: 0x000047E8
		private static ParameterInfo[] GetInParameters(MethodInfo methodInfo, ParameterInfo[] paramInfos, int start, int length, bool mustBeIn)
		{
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				ParameterInfo parameterInfo = paramInfos[i + start];
				if (LogicalMethodInfo.IsInParameter(parameterInfo))
				{
					num++;
				}
				else if (mustBeIn)
				{
					throw new InvalidOperationException(Res.GetString("WebBadOutParameter", new object[]
					{
						parameterInfo.Name,
						methodInfo.DeclaringType.FullName,
						parameterInfo.Name
					}));
				}
			}
			ParameterInfo[] array = new ParameterInfo[num];
			num = 0;
			for (int j = 0; j < length; j++)
			{
				ParameterInfo parameterInfo2 = paramInfos[j + start];
				if (LogicalMethodInfo.IsInParameter(parameterInfo2))
				{
					array[num++] = parameterInfo2;
				}
			}
			return array;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006688 File Offset: 0x00004888
		private static ParameterInfo[] GetOutParameters(MethodInfo methodInfo, ParameterInfo[] paramInfos, int start, int length, bool mustBeOut)
		{
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				ParameterInfo parameterInfo = paramInfos[i + start];
				if (LogicalMethodInfo.IsOutParameter(parameterInfo))
				{
					num++;
				}
				else if (mustBeOut)
				{
					throw new InvalidOperationException(Res.GetString("WebInOutParameter", new object[]
					{
						parameterInfo.Name,
						methodInfo.DeclaringType.FullName,
						parameterInfo.Name
					}));
				}
			}
			ParameterInfo[] array = new ParameterInfo[num];
			num = 0;
			for (int j = 0; j < length; j++)
			{
				ParameterInfo parameterInfo2 = paramInfos[j + start];
				if (LogicalMethodInfo.IsOutParameter(parameterInfo2))
				{
					array[num++] = parameterInfo2;
				}
			}
			return array;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006725 File Offset: 0x00004925
		private static bool IsInParameter(ParameterInfo paramInfo)
		{
			return !paramInfo.IsOut;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006730 File Offset: 0x00004930
		private static bool IsOutParameter(ParameterInfo paramInfo)
		{
			return paramInfo.IsOut || paramInfo.ParameterType.IsByRef;
		}

		/// <summary>Returns a value indicating whether the method passed in represents a Begin method of an asynchronous invocation.</summary>
		/// <returns>true if the <paramref name="methodInfo" /> parameter is a Begin method of an asynchronous invocation; otherwise, false.</returns>
		/// <param name="methodInfo">The <see cref="T:System.Reflection.MethodInfo" /> that might be a Begin method of an asynchronous invocation. </param>
		// Token: 0x06000157 RID: 343 RVA: 0x00006747 File Offset: 0x00004947
		public static bool IsBeginMethod(MethodInfo methodInfo)
		{
			return typeof(IAsyncResult).IsAssignableFrom(methodInfo.ReturnType) && methodInfo.Name.StartsWith("Begin", StringComparison.Ordinal);
		}

		/// <summary>Returns a value indicating whether the method passed in represents an End method of an asynchronous invocation.</summary>
		/// <returns>true if the <paramref name="methodInfo" /> parameter is an End method of an asynchronous invocation; otherwise, false.</returns>
		/// <param name="methodInfo">The <see cref="T:System.Reflection.MethodInfo" /> that might be an End method of an asynchronous invocation. </param>
		// Token: 0x06000158 RID: 344 RVA: 0x00006774 File Offset: 0x00004974
		public static bool IsEndMethod(MethodInfo methodInfo)
		{
			ParameterInfo[] array = methodInfo.GetParameters();
			return array.Length != 0 && typeof(IAsyncResult).IsAssignableFrom(array[0].ParameterType) && methodInfo.Name.StartsWith("End", StringComparison.Ordinal);
		}

		/// <summary>Given an array of <see cref="T:System.Reflection.MethodInfo" /> that can contain information about both asynchronous and synchronous methods, creates an array of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</summary>
		/// <returns>An array of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />, representing the methods within <paramref name="methodInfos" />.</returns>
		/// <param name="methodInfos">An array of <see cref="T:System.Reflection.MethodInfo" /> representing the asynchronous and synchronous methods for which to create <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> objects. </param>
		/// <exception cref="T:System.InvalidOperationException">A Begin asynchronous method is included in <paramref name="methodInfos" /> without a corresponding End method. </exception>
		// Token: 0x06000159 RID: 345 RVA: 0x000067B8 File Offset: 0x000049B8
		public static LogicalMethodInfo[] Create(MethodInfo[] methodInfos)
		{
			return LogicalMethodInfo.Create(methodInfos, (LogicalMethodTypes)3, null);
		}

		/// <summary>Given an array of <see cref="T:System.Reflection.MethodInfo" />, where the returned array of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> can be restricted to only asynchronous or synchronous methods, creates an array of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</summary>
		/// <returns>An array of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />, representing the methods within <paramref name="methodInfos" />, filtered by the value of <paramref name="types" />.</returns>
		/// <param name="methodInfos">An array of <see cref="T:System.Reflection.MethodInfo" /> representing the asynchronous and synchronous methods for which to create <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> objects. </param>
		/// <param name="types">A bitwise combination of the <see cref="T:System.Web.Services.Protocols.LogicalMethodTypes" /> values. Determines whether just asynchronous or synchronous methods or both are included in the returned array of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />. </param>
		/// <exception cref="T:System.InvalidOperationException">A Begin asynchronous method is included in <paramref name="methodInfos" /> without a corresponding End method. </exception>
		// Token: 0x0600015A RID: 346 RVA: 0x000067C2 File Offset: 0x000049C2
		public static LogicalMethodInfo[] Create(MethodInfo[] methodInfos, LogicalMethodTypes types)
		{
			return LogicalMethodInfo.Create(methodInfos, types, null);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000067CC File Offset: 0x000049CC
		internal static LogicalMethodInfo[] Create(MethodInfo[] methodInfos, LogicalMethodTypes types, Hashtable declarations)
		{
			ArrayList arrayList = (((types & LogicalMethodTypes.Async) != (LogicalMethodTypes)0) ? new ArrayList() : null);
			Hashtable hashtable = (((types & LogicalMethodTypes.Async) != (LogicalMethodTypes)0) ? new Hashtable() : null);
			ArrayList arrayList2 = (((types & LogicalMethodTypes.Sync) != (LogicalMethodTypes)0) ? new ArrayList() : null);
			foreach (MethodInfo methodInfo in methodInfos)
			{
				if (LogicalMethodInfo.IsBeginMethod(methodInfo))
				{
					if (arrayList != null)
					{
						arrayList.Add(methodInfo);
					}
				}
				else if (LogicalMethodInfo.IsEndMethod(methodInfo))
				{
					if (hashtable != null)
					{
						hashtable.Add(methodInfo.Name, methodInfo);
					}
				}
				else if (arrayList2 != null)
				{
					arrayList2.Add(methodInfo);
				}
			}
			int num = ((arrayList == null) ? 0 : arrayList.Count);
			int num2 = ((arrayList2 == null) ? 0 : arrayList2.Count);
			int num3 = num2 + num;
			LogicalMethodInfo[] array = new LogicalMethodInfo[num3];
			num3 = 0;
			for (int j = 0; j < num2; j++)
			{
				MethodInfo methodInfo2 = (MethodInfo)arrayList2[j];
				WebMethod webMethod = ((declarations == null) ? null : ((WebMethod)declarations[methodInfo2]));
				array[num3] = new LogicalMethodInfo(methodInfo2, webMethod);
				array[num3].CheckContractOverride();
				num3++;
			}
			for (int k = 0; k < num; k++)
			{
				MethodInfo methodInfo3 = (MethodInfo)arrayList[k];
				string text = "End" + methodInfo3.Name.Substring(5);
				MethodInfo methodInfo4 = (MethodInfo)hashtable[text];
				if (methodInfo4 == null)
				{
					throw new InvalidOperationException(Res.GetString("WebAsyncMissingEnd", new object[]
					{
						methodInfo3.DeclaringType.FullName,
						methodInfo3.Name,
						text
					}));
				}
				WebMethod webMethod2 = ((declarations == null) ? null : ((WebMethod)declarations[methodInfo3]));
				array[num3++] = new LogicalMethodInfo(methodInfo3, methodInfo4, webMethod2);
			}
			return array;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00006992 File Offset: 0x00004B92
		internal static HashAlgorithm HashAlgorithm
		{
			get
			{
				if (LogicalMethodInfo.hash == null)
				{
					LogicalMethodInfo.hash = SHA1.Create();
				}
				return LogicalMethodInfo.hash;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000069AC File Offset: 0x00004BAC
		internal string GetKey()
		{
			if (this.methodInfo == null)
			{
				return string.Empty;
			}
			string text = this.methodInfo.DeclaringType.FullName + ":" + this.methodInfo.ToString();
			if (text.Length > 1024)
			{
				text = Convert.ToBase64String(LogicalMethodInfo.HashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(text)));
			}
			return text;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006A1C File Offset: 0x00004C1C
		internal void CheckContractOverride()
		{
			if (this.declaration == null)
			{
				return;
			}
			this.methodInfo.GetParameters();
			ParameterInfo[] array = this.methodInfo.GetParameters();
			for (int i = 0; i < array.Length; i++)
			{
				foreach (object obj in array[i].GetCustomAttributes(false))
				{
					if (obj.GetType().Namespace == "System.Xml.Serialization")
					{
						throw new InvalidOperationException(Res.GetString("ContractOverride", new object[]
						{
							this.methodInfo.Name,
							this.methodInfo.DeclaringType.FullName,
							this.declaration.DeclaringType.FullName,
							this.declaration.ToString(),
							obj.ToString()
						}));
					}
				}
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006B01 File Offset: 0x00004D01
		internal static bool CanMerge(Type type)
		{
			return type == typeof(SoapHeaderAttribute) || typeof(SoapExtensionAttribute).IsAssignableFrom(type);
		}

		// Token: 0x04000200 RID: 512
		private MethodInfo methodInfo;

		// Token: 0x04000201 RID: 513
		private MethodInfo endMethodInfo;

		// Token: 0x04000202 RID: 514
		private ParameterInfo[] inParams;

		// Token: 0x04000203 RID: 515
		private ParameterInfo[] outParams;

		// Token: 0x04000204 RID: 516
		private ParameterInfo[] parameters;

		// Token: 0x04000205 RID: 517
		private Hashtable attributes;

		// Token: 0x04000206 RID: 518
		private Type retType;

		// Token: 0x04000207 RID: 519
		private ParameterInfo callbackParam;

		// Token: 0x04000208 RID: 520
		private ParameterInfo stateParam;

		// Token: 0x04000209 RID: 521
		private ParameterInfo resultParam;

		// Token: 0x0400020A RID: 522
		private string methodName;

		// Token: 0x0400020B RID: 523
		private bool isVoid;

		// Token: 0x0400020C RID: 524
		private static object[] emptyObjectArray = new object[0];

		// Token: 0x0400020D RID: 525
		private WebServiceBindingAttribute binding;

		// Token: 0x0400020E RID: 526
		private WebMethodAttribute attribute;

		// Token: 0x0400020F RID: 527
		private MethodInfo declaration;

		// Token: 0x04000210 RID: 528
		private static HashAlgorithm hash;
	}
}
