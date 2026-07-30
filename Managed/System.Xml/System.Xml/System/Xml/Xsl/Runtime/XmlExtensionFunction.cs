using System;
using System.Globalization;
using System.Reflection;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000600 RID: 1536
	internal class XmlExtensionFunction
	{
		// Token: 0x06003BD7 RID: 15319 RVA: 0x000020FD File Offset: 0x000002FD
		public XmlExtensionFunction()
		{
		}

		// Token: 0x06003BD8 RID: 15320 RVA: 0x0014F629 File Offset: 0x0014D829
		public XmlExtensionFunction(string name, string namespaceUri, MethodInfo meth)
		{
			this.name = name;
			this.namespaceUri = namespaceUri;
			this.Bind(meth);
		}

		// Token: 0x06003BD9 RID: 15321 RVA: 0x0014F646 File Offset: 0x0014D846
		public XmlExtensionFunction(string name, string namespaceUri, int numArgs, Type objectType, BindingFlags flags)
		{
			this.Init(name, namespaceUri, numArgs, objectType, flags);
		}

		// Token: 0x06003BDA RID: 15322 RVA: 0x0014F65C File Offset: 0x0014D85C
		public void Init(string name, string namespaceUri, int numArgs, Type objectType, BindingFlags flags)
		{
			this.name = name;
			this.namespaceUri = namespaceUri;
			this.numArgs = numArgs;
			this.objectType = objectType;
			this.flags = flags;
			this.meth = null;
			this.argClrTypes = null;
			this.retClrType = null;
			this.argXmlTypes = null;
			this.retXmlType = null;
			this.hashCode = namespaceUri.GetHashCode() ^ name.GetHashCode() ^ (int)((int)flags << 16) ^ numArgs;
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06003BDB RID: 15323 RVA: 0x0014F6CC File Offset: 0x0014D8CC
		public MethodInfo Method
		{
			get
			{
				return this.meth;
			}
		}

		// Token: 0x06003BDC RID: 15324 RVA: 0x0014F6D4 File Offset: 0x0014D8D4
		public Type GetClrArgumentType(int index)
		{
			return this.argClrTypes[index];
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06003BDD RID: 15325 RVA: 0x0014F6DE File Offset: 0x0014D8DE
		public Type ClrReturnType
		{
			get
			{
				return this.retClrType;
			}
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x0014F6E6 File Offset: 0x0014D8E6
		public XmlQueryType GetXmlArgumentType(int index)
		{
			return this.argXmlTypes[index];
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06003BDF RID: 15327 RVA: 0x0014F6F0 File Offset: 0x0014D8F0
		public XmlQueryType XmlReturnType
		{
			get
			{
				return this.retXmlType;
			}
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x0014F6F8 File Offset: 0x0014D8F8
		public bool CanBind()
		{
			MethodInfo[] methods = this.objectType.GetMethods(this.flags);
			StringComparison stringComparison = (((this.flags & BindingFlags.IgnoreCase) > BindingFlags.Default) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			foreach (MethodInfo methodInfo in methods)
			{
				if (methodInfo.Name.Equals(this.name, stringComparison) && (this.numArgs == -1 || methodInfo.GetParameters().Length == this.numArgs) && !methodInfo.IsGenericMethodDefinition)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x0014F774 File Offset: 0x0014D974
		public void Bind()
		{
			MethodInfo[] methods = this.objectType.GetMethods(this.flags);
			MethodInfo methodInfo = null;
			StringComparison stringComparison = (((this.flags & BindingFlags.IgnoreCase) > BindingFlags.Default) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			foreach (MethodInfo methodInfo2 in methods)
			{
				if (methodInfo2.Name.Equals(this.name, stringComparison) && (this.numArgs == -1 || methodInfo2.GetParameters().Length == this.numArgs))
				{
					if (methodInfo != null)
					{
						throw new XslTransformException("Ambiguous method call. Extension object '{0}' contains multiple '{1}' methods that have {2} parameter(s).", new string[]
						{
							this.namespaceUri,
							this.name,
							this.numArgs.ToString(CultureInfo.InvariantCulture)
						});
					}
					methodInfo = methodInfo2;
				}
			}
			if (methodInfo == null)
			{
				foreach (MethodInfo methodInfo3 in this.objectType.GetMethods(this.flags | BindingFlags.NonPublic))
				{
					if (methodInfo3.Name.Equals(this.name, stringComparison) && methodInfo3.GetParameters().Length == this.numArgs)
					{
						throw new XslTransformException("Method '{1}' of extension object '{0}' cannot be called because it is not public.", new string[] { this.namespaceUri, this.name });
					}
				}
				throw new XslTransformException("Extension object '{0}' does not contain a matching '{1}' method that has {2} parameter(s).", new string[]
				{
					this.namespaceUri,
					this.name,
					this.numArgs.ToString(CultureInfo.InvariantCulture)
				});
			}
			if (methodInfo.IsGenericMethodDefinition)
			{
				throw new XslTransformException("Method '{1}' of extension object '{0}' cannot be called because it is generic.", new string[] { this.namespaceUri, this.name });
			}
			this.Bind(methodInfo);
		}

		// Token: 0x06003BE2 RID: 15330 RVA: 0x0014F910 File Offset: 0x0014DB10
		private void Bind(MethodInfo meth)
		{
			ParameterInfo[] parameters = meth.GetParameters();
			this.meth = meth;
			this.argClrTypes = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				this.argClrTypes[i] = this.GetClrType(parameters[i].ParameterType);
			}
			this.retClrType = this.GetClrType(this.meth.ReturnType);
			this.argXmlTypes = new XmlQueryType[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				this.argXmlTypes[i] = this.InferXmlType(this.argClrTypes[i]);
				if (this.namespaceUri.Length == 0)
				{
					if (this.argXmlTypes[i] == XmlQueryTypeFactory.NodeNotRtf)
					{
						this.argXmlTypes[i] = XmlQueryTypeFactory.Node;
					}
					else if (this.argXmlTypes[i] == XmlQueryTypeFactory.NodeSDod)
					{
						this.argXmlTypes[i] = XmlQueryTypeFactory.NodeS;
					}
				}
				else if (this.argXmlTypes[i] == XmlQueryTypeFactory.NodeSDod)
				{
					this.argXmlTypes[i] = XmlQueryTypeFactory.NodeNotRtfS;
				}
			}
			this.retXmlType = this.InferXmlType(this.retClrType);
		}

		// Token: 0x06003BE3 RID: 15331 RVA: 0x0014FA20 File Offset: 0x0014DC20
		public object Invoke(object extObj, object[] args)
		{
			object obj;
			try
			{
				obj = this.meth.Invoke(extObj, this.flags, null, args, CultureInfo.InvariantCulture);
			}
			catch (TargetInvocationException ex)
			{
				throw new XslTransformException(ex.InnerException, "An error occurred during a call to extension function '{0}'. See InnerException for a complete description of the error.", new string[] { this.name });
			}
			catch (Exception ex2)
			{
				if (!XmlException.IsCatchableException(ex2))
				{
					throw;
				}
				throw new XslTransformException(ex2, "An error occurred during a call to extension function '{0}'. See InnerException for a complete description of the error.", new string[] { this.name });
			}
			return obj;
		}

		// Token: 0x06003BE4 RID: 15332 RVA: 0x0014FAAC File Offset: 0x0014DCAC
		public override bool Equals(object other)
		{
			XmlExtensionFunction xmlExtensionFunction = other as XmlExtensionFunction;
			return this.hashCode == xmlExtensionFunction.hashCode && this.name == xmlExtensionFunction.name && this.namespaceUri == xmlExtensionFunction.namespaceUri && this.numArgs == xmlExtensionFunction.numArgs && this.objectType == xmlExtensionFunction.objectType && this.flags == xmlExtensionFunction.flags;
		}

		// Token: 0x06003BE5 RID: 15333 RVA: 0x0014FB25 File Offset: 0x0014DD25
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x06003BE6 RID: 15334 RVA: 0x0014FB2D File Offset: 0x0014DD2D
		private Type GetClrType(Type clrType)
		{
			if (clrType.IsEnum)
			{
				return Enum.GetUnderlyingType(clrType);
			}
			if (clrType.IsByRef)
			{
				throw new XslTransformException("Method '{1}' of extension object '{0}' cannot be called because it has one or more ByRef parameters.", new string[] { this.namespaceUri, this.name });
			}
			return clrType;
		}

		// Token: 0x06003BE7 RID: 15335 RVA: 0x0014FB6A File Offset: 0x0014DD6A
		private XmlQueryType InferXmlType(Type clrType)
		{
			return XsltConvert.InferXsltType(clrType);
		}

		// Token: 0x04002762 RID: 10082
		private string namespaceUri;

		// Token: 0x04002763 RID: 10083
		private string name;

		// Token: 0x04002764 RID: 10084
		private int numArgs;

		// Token: 0x04002765 RID: 10085
		private Type objectType;

		// Token: 0x04002766 RID: 10086
		private BindingFlags flags;

		// Token: 0x04002767 RID: 10087
		private int hashCode;

		// Token: 0x04002768 RID: 10088
		private MethodInfo meth;

		// Token: 0x04002769 RID: 10089
		private Type[] argClrTypes;

		// Token: 0x0400276A RID: 10090
		private Type retClrType;

		// Token: 0x0400276B RID: 10091
		private XmlQueryType[] argXmlTypes;

		// Token: 0x0400276C RID: 10092
		private XmlQueryType retXmlType;
	}
}
