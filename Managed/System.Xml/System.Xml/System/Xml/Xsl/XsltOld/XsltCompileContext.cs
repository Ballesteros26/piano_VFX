using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Xml.XPath;
using System.Xml.Xsl.Runtime;
using MS.Internal.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000555 RID: 1365
	internal class XsltCompileContext : XsltContext
	{
		// Token: 0x060036E2 RID: 14050 RVA: 0x00132B89 File Offset: 0x00130D89
		internal XsltCompileContext(InputScopeManager manager, Processor processor)
			: base(false)
		{
			this.manager = manager;
			this.processor = processor;
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x00132BA0 File Offset: 0x00130DA0
		internal XsltCompileContext()
			: base(false)
		{
		}

		// Token: 0x060036E4 RID: 14052 RVA: 0x00132BA9 File Offset: 0x00130DA9
		internal void Recycle()
		{
			this.manager = null;
			this.processor = null;
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x00132BB9 File Offset: 0x00130DB9
		internal void Reinitialize(InputScopeManager manager, Processor processor)
		{
			this.manager = manager;
			this.processor = processor;
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x00132BC9 File Offset: 0x00130DC9
		public override int CompareDocument(string baseUri, string nextbaseUri)
		{
			return string.Compare(baseUri, nextbaseUri, StringComparison.Ordinal);
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x060036E7 RID: 14055 RVA: 0x00003065 File Offset: 0x00001265
		public override string DefaultNamespace
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x00132BD3 File Offset: 0x00130DD3
		public override string LookupNamespace(string prefix)
		{
			return this.manager.ResolveXPathNamespace(prefix);
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x00132BE4 File Offset: 0x00130DE4
		public override IXsltContextVariable ResolveVariable(string prefix, string name)
		{
			string text = this.LookupNamespace(prefix);
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, text);
			IXsltContextVariable xsltContextVariable = this.manager.VariableScope.ResolveVariable(xmlQualifiedName);
			if (xsltContextVariable == null)
			{
				throw XsltException.Create("The variable or parameter '{0}' is either not defined or it is out of scope.", new string[] { xmlQualifiedName.ToString() });
			}
			return xsltContextVariable;
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x00132C34 File Offset: 0x00130E34
		internal object EvaluateVariable(VariableAction variable)
		{
			object obj = this.processor.GetVariableValue(variable);
			if (obj == null && !variable.IsGlobal)
			{
				VariableAction variableAction = this.manager.VariableScope.ResolveGlobalVariable(variable.Name);
				if (variableAction != null)
				{
					obj = this.processor.GetVariableValue(variableAction);
				}
			}
			if (obj == null)
			{
				throw XsltException.Create("The variable or parameter '{0}' is either not defined or it is out of scope.", new string[] { variable.Name.ToString() });
			}
			return obj;
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x060036EB RID: 14059 RVA: 0x00132CA3 File Offset: 0x00130EA3
		public override bool Whitespace
		{
			get
			{
				return this.processor.Stylesheet.Whitespace;
			}
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x00132CB5 File Offset: 0x00130EB5
		public override bool PreserveWhitespace(XPathNavigator node)
		{
			node = node.Clone();
			node.MoveToParent();
			return this.processor.Stylesheet.PreserveWhiteSpace(this.processor, node);
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x00132CE0 File Offset: 0x00130EE0
		private MethodInfo FindBestMethod(MethodInfo[] methods, bool ignoreCase, bool publicOnly, string name, XPathResultType[] argTypes)
		{
			int num = methods.Length;
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				if (string.Compare(name, methods[i].Name, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0 && (!publicOnly || methods[i].GetBaseDefinition().IsPublic))
				{
					methods[num2++] = methods[i];
				}
			}
			num = num2;
			if (num == 0)
			{
				return null;
			}
			if (argTypes == null)
			{
				return methods[0];
			}
			num2 = 0;
			for (int j = 0; j < num; j++)
			{
				if (methods[j].GetParameters().Length == argTypes.Length)
				{
					methods[num2++] = methods[j];
				}
			}
			num = num2;
			if (num <= 1)
			{
				return methods[0];
			}
			num2 = 0;
			for (int k = 0; k < num; k++)
			{
				bool flag = true;
				ParameterInfo[] parameters = methods[k].GetParameters();
				for (int l = 0; l < parameters.Length; l++)
				{
					XPathResultType xpathResultType = argTypes[l];
					if (xpathResultType != XPathResultType.Any)
					{
						XPathResultType xpathType = XsltCompileContext.GetXPathType(parameters[l].ParameterType);
						if (xpathType != xpathResultType && xpathType != XPathResultType.Any)
						{
							flag = false;
							break;
						}
					}
				}
				if (flag)
				{
					methods[num2++] = methods[k];
				}
			}
			return methods[0];
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x00132DE8 File Offset: 0x00130FE8
		private IXsltContextFunction GetExtentionMethod(string ns, string name, XPathResultType[] argTypes, out object extension)
		{
			XsltCompileContext.FuncExtension funcExtension = null;
			extension = this.processor.GetScriptObject(ns);
			if (extension != null)
			{
				MethodInfo methodInfo = this.FindBestMethod(extension.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic), true, false, name, argTypes);
				if (methodInfo != null)
				{
					funcExtension = new XsltCompileContext.FuncExtension(extension, methodInfo, null);
				}
				return funcExtension;
			}
			extension = this.processor.GetExtensionObject(ns);
			if (extension != null)
			{
				MethodInfo methodInfo2 = this.FindBestMethod(extension.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic), false, true, name, argTypes);
				if (methodInfo2 != null)
				{
					funcExtension = new XsltCompileContext.FuncExtension(extension, methodInfo2, this.processor.permissions);
				}
				return funcExtension;
			}
			return null;
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x00132E8C File Offset: 0x0013108C
		public override IXsltContextFunction ResolveFunction(string prefix, string name, XPathResultType[] argTypes)
		{
			IXsltContextFunction xsltContextFunction;
			if (prefix.Length == 0)
			{
				xsltContextFunction = XsltCompileContext.s_FunctionTable[name] as IXsltContextFunction;
			}
			else
			{
				string text = this.LookupNamespace(prefix);
				if (text == "urn:schemas-microsoft-com:xslt" && name == "node-set")
				{
					xsltContextFunction = XsltCompileContext.s_FuncNodeSet;
				}
				else
				{
					object obj;
					xsltContextFunction = this.GetExtentionMethod(text, name, argTypes, out obj);
					if (obj == null)
					{
						throw XsltException.Create("Cannot find the script or external object that implements prefix '{0}'.", new string[] { prefix });
					}
				}
			}
			if (xsltContextFunction == null)
			{
				throw XsltException.Create("'{0}()' is an unknown XSLT function.", new string[] { name });
			}
			if (argTypes.Length < xsltContextFunction.Minargs || xsltContextFunction.Maxargs < argTypes.Length)
			{
				throw XsltException.Create("XSLT function '{0}()' has the wrong number of arguments.", new string[]
				{
					name,
					argTypes.Length.ToString(CultureInfo.InvariantCulture)
				});
			}
			return xsltContextFunction;
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x00132F5C File Offset: 0x0013115C
		private Uri ComposeUri(string thisUri, string baseUri)
		{
			XmlResolver resolver = this.processor.Resolver;
			Uri uri = null;
			if (baseUri.Length != 0)
			{
				uri = resolver.ResolveUri(null, baseUri);
			}
			return resolver.ResolveUri(uri, thisUri);
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x00132F90 File Offset: 0x00131190
		private XPathNodeIterator Document(object arg0, string baseUri)
		{
			if (this.processor.permissions != null)
			{
				this.processor.permissions.PermitOnly();
			}
			XPathNodeIterator xpathNodeIterator = arg0 as XPathNodeIterator;
			if (xpathNodeIterator != null)
			{
				ArrayList arrayList = new ArrayList();
				Hashtable hashtable = new Hashtable();
				while (xpathNodeIterator.MoveNext())
				{
					Uri uri = this.ComposeUri(xpathNodeIterator.Current.Value, baseUri ?? xpathNodeIterator.Current.BaseURI);
					if (!hashtable.ContainsKey(uri))
					{
						hashtable.Add(uri, null);
						arrayList.Add(this.processor.GetNavigator(uri));
					}
				}
				return new XPathArrayIterator(arrayList);
			}
			return new XPathSingletonIterator(this.processor.GetNavigator(this.ComposeUri(XmlConvert.ToXPathString(arg0), baseUri ?? this.manager.Navigator.BaseURI)));
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x00133058 File Offset: 0x00131258
		private Hashtable BuildKeyTable(Key key, XPathNavigator root)
		{
			Hashtable hashtable = new Hashtable();
			string queryExpression = this.processor.GetQueryExpression(key.MatchKey);
			Query compiledQuery = this.processor.GetCompiledQuery(key.MatchKey);
			Query compiledQuery2 = this.processor.GetCompiledQuery(key.UseKey);
			XPathNodeIterator xpathNodeIterator = root.SelectDescendants(XPathNodeType.All, false);
			while (xpathNodeIterator.MoveNext())
			{
				XPathNavigator xpathNavigator = xpathNodeIterator.Current;
				XsltCompileContext.EvaluateKey(xpathNavigator, compiledQuery, queryExpression, compiledQuery2, hashtable);
				if (xpathNavigator.MoveToFirstAttribute())
				{
					do
					{
						XsltCompileContext.EvaluateKey(xpathNavigator, compiledQuery, queryExpression, compiledQuery2, hashtable);
					}
					while (xpathNavigator.MoveToNextAttribute());
					xpathNavigator.MoveToParent();
				}
			}
			return hashtable;
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x001330F4 File Offset: 0x001312F4
		private static void AddKeyValue(Hashtable keyTable, string key, XPathNavigator value, bool checkDuplicates)
		{
			ArrayList arrayList = (ArrayList)keyTable[key];
			if (arrayList == null)
			{
				arrayList = new ArrayList();
				keyTable.Add(key, arrayList);
			}
			else if (checkDuplicates && value.ComparePosition((XPathNavigator)arrayList[arrayList.Count - 1]) == XmlNodeOrder.Same)
			{
				return;
			}
			arrayList.Add(value.Clone());
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x00133150 File Offset: 0x00131350
		private static void EvaluateKey(XPathNavigator node, Query matchExpr, string matchStr, Query useExpr, Hashtable keyTable)
		{
			try
			{
				if (matchExpr.MatchNode(node) == null)
				{
					return;
				}
			}
			catch (XPathException)
			{
				throw XsltException.Create("'{0}' is an invalid XSLT pattern.", new string[] { matchStr });
			}
			object obj = useExpr.Evaluate(new XPathSingletonIterator(node, true));
			XPathNodeIterator xpathNodeIterator = obj as XPathNodeIterator;
			if (xpathNodeIterator != null)
			{
				bool flag = false;
				while (xpathNodeIterator.MoveNext())
				{
					XPathNavigator xpathNavigator = xpathNodeIterator.Current;
					XsltCompileContext.AddKeyValue(keyTable, xpathNavigator.Value, node, flag);
					flag = true;
				}
				return;
			}
			string text = XmlConvert.ToXPathString(obj);
			XsltCompileContext.AddKeyValue(keyTable, text, node, false);
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x001331DC File Offset: 0x001313DC
		private DecimalFormat ResolveFormatName(string formatName)
		{
			string text = string.Empty;
			string empty = string.Empty;
			if (formatName != null)
			{
				string text2;
				PrefixQName.ParseQualifiedName(formatName, out text2, out empty);
				text = this.LookupNamespace(text2);
			}
			DecimalFormat decimalFormat = this.processor.RootAction.GetDecimalFormat(new XmlQualifiedName(empty, text));
			if (decimalFormat == null)
			{
				if (formatName != null)
				{
					throw XsltException.Create("Decimal format '{0}' is not defined.", new string[] { formatName });
				}
				decimalFormat = new DecimalFormat(new NumberFormatInfo(), '#', '0', ';');
			}
			return decimalFormat;
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x00133250 File Offset: 0x00131450
		private bool ElementAvailable(string qname)
		{
			string text;
			string text2;
			PrefixQName.ParseQualifiedName(qname, out text, out text2);
			return this.manager.ResolveXmlNamespace(text) == "http://www.w3.org/1999/XSL/Transform" && (text2 == "apply-imports" || text2 == "apply-templates" || text2 == "attribute" || text2 == "call-template" || text2 == "choose" || text2 == "comment" || text2 == "copy" || text2 == "copy-of" || text2 == "element" || text2 == "fallback" || text2 == "for-each" || text2 == "if" || text2 == "message" || text2 == "number" || text2 == "processing-instruction" || text2 == "text" || text2 == "value-of" || text2 == "variable");
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x00133388 File Offset: 0x00131588
		private bool FunctionAvailable(string qname)
		{
			string text;
			string text2;
			PrefixQName.ParseQualifiedName(qname, out text, out text2);
			string text3 = this.LookupNamespace(text);
			if (text3 == "urn:schemas-microsoft-com:xslt")
			{
				return text2 == "node-set";
			}
			if (text3.Length == 0)
			{
				return text2 == "last" || text2 == "position" || text2 == "name" || text2 == "namespace-uri" || text2 == "local-name" || text2 == "count" || text2 == "id" || text2 == "string" || text2 == "concat" || text2 == "starts-with" || text2 == "contains" || text2 == "substring-before" || text2 == "substring-after" || text2 == "substring" || text2 == "string-length" || text2 == "normalize-space" || text2 == "translate" || text2 == "boolean" || text2 == "not" || text2 == "true" || text2 == "false" || text2 == "lang" || text2 == "number" || text2 == "sum" || text2 == "floor" || text2 == "ceiling" || text2 == "round" || (XsltCompileContext.s_FunctionTable[text2] != null && text2 != "unparsed-entity-uri");
			}
			object obj;
			return this.GetExtentionMethod(text3, text2, null, out obj) != null;
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x00133590 File Offset: 0x00131790
		private XPathNodeIterator Current()
		{
			XPathNavigator xpathNavigator = this.processor.Current;
			if (xpathNavigator != null)
			{
				return new XPathSingletonIterator(xpathNavigator.Clone());
			}
			return XPathEmptyIterator.Instance;
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x001335C0 File Offset: 0x001317C0
		private string SystemProperty(string qname)
		{
			string text = string.Empty;
			string text2;
			string text3;
			PrefixQName.ParseQualifiedName(qname, out text2, out text3);
			string text4 = this.LookupNamespace(text2);
			if (text4 == "http://www.w3.org/1999/XSL/Transform")
			{
				if (text3 == "version")
				{
					text = "1";
				}
				else if (text3 == "vendor")
				{
					text = "Microsoft";
				}
				else if (text3 == "vendor-url")
				{
					text = "http://www.microsoft.com";
				}
				return text;
			}
			if (text4 == null && text2 != null)
			{
				throw XsltException.Create("Prefix '{0}' is not defined.", new string[] { text2 });
			}
			return string.Empty;
		}

		// Token: 0x060036FA RID: 14074 RVA: 0x00133654 File Offset: 0x00131854
		public static XPathResultType GetXPathType(Type type)
		{
			TypeCode typeCode = Type.GetTypeCode(type);
			if (typeCode <= TypeCode.Boolean)
			{
				if (typeCode != TypeCode.Object)
				{
					if (typeCode == TypeCode.Boolean)
					{
						return XPathResultType.Boolean;
					}
				}
				else
				{
					if (typeof(XPathNavigator).IsAssignableFrom(type) || typeof(IXPathNavigable).IsAssignableFrom(type))
					{
						return XPathResultType.String;
					}
					if (typeof(XPathNodeIterator).IsAssignableFrom(type))
					{
						return XPathResultType.NodeSet;
					}
					return XPathResultType.Any;
				}
			}
			else
			{
				if (typeCode == TypeCode.DateTime)
				{
					return XPathResultType.Error;
				}
				if (typeCode == TypeCode.String)
				{
					return XPathResultType.String;
				}
			}
			return XPathResultType.Number;
		}

		// Token: 0x060036FB RID: 14075 RVA: 0x001336C4 File Offset: 0x001318C4
		private static Hashtable CreateFunctionTable()
		{
			Hashtable hashtable = new Hashtable(10);
			hashtable["current"] = new XsltCompileContext.FuncCurrent();
			hashtable["unparsed-entity-uri"] = new XsltCompileContext.FuncUnEntityUri();
			hashtable["generate-id"] = new XsltCompileContext.FuncGenerateId();
			hashtable["system-property"] = new XsltCompileContext.FuncSystemProp();
			hashtable["element-available"] = new XsltCompileContext.FuncElementAvailable();
			hashtable["function-available"] = new XsltCompileContext.FuncFunctionAvailable();
			hashtable["document"] = new XsltCompileContext.FuncDocument();
			hashtable["key"] = new XsltCompileContext.FuncKey();
			hashtable["format-number"] = new XsltCompileContext.FuncFormatNumber();
			return hashtable;
		}

		// Token: 0x0400232E RID: 9006
		private InputScopeManager manager;

		// Token: 0x0400232F RID: 9007
		private Processor processor;

		// Token: 0x04002330 RID: 9008
		private static Hashtable s_FunctionTable = XsltCompileContext.CreateFunctionTable();

		// Token: 0x04002331 RID: 9009
		private static IXsltContextFunction s_FuncNodeSet = new XsltCompileContext.FuncNodeSet();

		// Token: 0x04002332 RID: 9010
		private const string f_NodeSet = "node-set";

		// Token: 0x04002333 RID: 9011
		private const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x02000556 RID: 1366
		private abstract class XsltFunctionImpl : IXsltContextFunction
		{
			// Token: 0x060036FD RID: 14077 RVA: 0x000020FD File Offset: 0x000002FD
			public XsltFunctionImpl()
			{
			}

			// Token: 0x060036FE RID: 14078 RVA: 0x0013377E File Offset: 0x0013197E
			public XsltFunctionImpl(int minArgs, int maxArgs, XPathResultType returnType, XPathResultType[] argTypes)
			{
				this.Init(minArgs, maxArgs, returnType, argTypes);
			}

			// Token: 0x060036FF RID: 14079 RVA: 0x00133791 File Offset: 0x00131991
			protected void Init(int minArgs, int maxArgs, XPathResultType returnType, XPathResultType[] argTypes)
			{
				this.minargs = minArgs;
				this.maxargs = maxArgs;
				this.returnType = returnType;
				this.argTypes = argTypes;
			}

			// Token: 0x17000B9C RID: 2972
			// (get) Token: 0x06003700 RID: 14080 RVA: 0x001337B0 File Offset: 0x001319B0
			public int Minargs
			{
				get
				{
					return this.minargs;
				}
			}

			// Token: 0x17000B9D RID: 2973
			// (get) Token: 0x06003701 RID: 14081 RVA: 0x001337B8 File Offset: 0x001319B8
			public int Maxargs
			{
				get
				{
					return this.maxargs;
				}
			}

			// Token: 0x17000B9E RID: 2974
			// (get) Token: 0x06003702 RID: 14082 RVA: 0x001337C0 File Offset: 0x001319C0
			public XPathResultType ReturnType
			{
				get
				{
					return this.returnType;
				}
			}

			// Token: 0x17000B9F RID: 2975
			// (get) Token: 0x06003703 RID: 14083 RVA: 0x001337C8 File Offset: 0x001319C8
			public XPathResultType[] ArgTypes
			{
				get
				{
					return this.argTypes;
				}
			}

			// Token: 0x06003704 RID: 14084
			public abstract object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext);

			// Token: 0x06003705 RID: 14085 RVA: 0x001337D0 File Offset: 0x001319D0
			public static XPathNodeIterator ToIterator(object argument)
			{
				XPathNodeIterator xpathNodeIterator = argument as XPathNodeIterator;
				if (xpathNodeIterator == null)
				{
					throw XsltException.Create("Cannot convert the operand to a node-set.", Array.Empty<string>());
				}
				return xpathNodeIterator;
			}

			// Token: 0x06003706 RID: 14086 RVA: 0x001337EB File Offset: 0x001319EB
			public static XPathNavigator ToNavigator(object argument)
			{
				XPathNavigator xpathNavigator = argument as XPathNavigator;
				if (xpathNavigator == null)
				{
					throw XsltException.Create("Cannot convert the operand to 'Result tree fragment'.", Array.Empty<string>());
				}
				return xpathNavigator;
			}

			// Token: 0x06003707 RID: 14087 RVA: 0x00133806 File Offset: 0x00131A06
			private static string IteratorToString(XPathNodeIterator it)
			{
				if (it.MoveNext())
				{
					return it.Current.Value;
				}
				return string.Empty;
			}

			// Token: 0x06003708 RID: 14088 RVA: 0x00133824 File Offset: 0x00131A24
			public static string ToString(object argument)
			{
				XPathNodeIterator xpathNodeIterator = argument as XPathNodeIterator;
				if (xpathNodeIterator != null)
				{
					return XsltCompileContext.XsltFunctionImpl.IteratorToString(xpathNodeIterator);
				}
				return XmlConvert.ToXPathString(argument);
			}

			// Token: 0x06003709 RID: 14089 RVA: 0x00133848 File Offset: 0x00131A48
			public static bool ToBoolean(object argument)
			{
				XPathNodeIterator xpathNodeIterator = argument as XPathNodeIterator;
				if (xpathNodeIterator != null)
				{
					return Convert.ToBoolean(XsltCompileContext.XsltFunctionImpl.IteratorToString(xpathNodeIterator), CultureInfo.InvariantCulture);
				}
				XPathNavigator xpathNavigator = argument as XPathNavigator;
				if (xpathNavigator != null)
				{
					return Convert.ToBoolean(xpathNavigator.ToString(), CultureInfo.InvariantCulture);
				}
				return Convert.ToBoolean(argument, CultureInfo.InvariantCulture);
			}

			// Token: 0x0600370A RID: 14090 RVA: 0x00133898 File Offset: 0x00131A98
			public static double ToNumber(object argument)
			{
				XPathNodeIterator xpathNodeIterator = argument as XPathNodeIterator;
				if (xpathNodeIterator != null)
				{
					return XmlConvert.ToXPathDouble(XsltCompileContext.XsltFunctionImpl.IteratorToString(xpathNodeIterator));
				}
				XPathNavigator xpathNavigator = argument as XPathNavigator;
				if (xpathNavigator != null)
				{
					return XmlConvert.ToXPathDouble(xpathNavigator.ToString());
				}
				return XmlConvert.ToXPathDouble(argument);
			}

			// Token: 0x0600370B RID: 14091 RVA: 0x001338D7 File Offset: 0x00131AD7
			private static object ToNumeric(object argument, TypeCode typeCode)
			{
				return Convert.ChangeType(XsltCompileContext.XsltFunctionImpl.ToNumber(argument), typeCode, CultureInfo.InvariantCulture);
			}

			// Token: 0x0600370C RID: 14092 RVA: 0x001338F0 File Offset: 0x00131AF0
			public static object ConvertToXPathType(object val, XPathResultType xt, TypeCode typeCode)
			{
				switch (xt)
				{
				case XPathResultType.Number:
					return XsltCompileContext.XsltFunctionImpl.ToNumeric(val, typeCode);
				case XPathResultType.String:
					if (typeCode == TypeCode.String)
					{
						return XsltCompileContext.XsltFunctionImpl.ToString(val);
					}
					return XsltCompileContext.XsltFunctionImpl.ToNavigator(val);
				case XPathResultType.Boolean:
					return XsltCompileContext.XsltFunctionImpl.ToBoolean(val);
				case XPathResultType.NodeSet:
					return XsltCompileContext.XsltFunctionImpl.ToIterator(val);
				case XPathResultType.Any:
				case XPathResultType.Error:
					return val;
				}
				return val;
			}

			// Token: 0x04002334 RID: 9012
			private int minargs;

			// Token: 0x04002335 RID: 9013
			private int maxargs;

			// Token: 0x04002336 RID: 9014
			private XPathResultType returnType;

			// Token: 0x04002337 RID: 9015
			private XPathResultType[] argTypes;
		}

		// Token: 0x02000557 RID: 1367
		private class FuncCurrent : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x0600370D RID: 14093 RVA: 0x00133952 File Offset: 0x00131B52
			public FuncCurrent()
				: base(0, 0, XPathResultType.NodeSet, new XPathResultType[0])
			{
			}

			// Token: 0x0600370E RID: 14094 RVA: 0x00133963 File Offset: 0x00131B63
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				return ((XsltCompileContext)xsltContext).Current();
			}
		}

		// Token: 0x02000558 RID: 1368
		private class FuncUnEntityUri : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x0600370F RID: 14095 RVA: 0x00133970 File Offset: 0x00131B70
			public FuncUnEntityUri()
				: base(1, 1, XPathResultType.String, new XPathResultType[] { XPathResultType.String })
			{
			}

			// Token: 0x06003710 RID: 14096 RVA: 0x00133985 File Offset: 0x00131B85
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				throw XsltException.Create("'{0}()' is an unsupported XSLT function.", new string[] { "unparsed-entity-uri" });
			}
		}

		// Token: 0x02000559 RID: 1369
		private class FuncGenerateId : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x06003711 RID: 14097 RVA: 0x0013399F File Offset: 0x00131B9F
			public FuncGenerateId()
				: base(0, 1, XPathResultType.String, new XPathResultType[] { XPathResultType.NodeSet })
			{
			}

			// Token: 0x06003712 RID: 14098 RVA: 0x001339B4 File Offset: 0x00131BB4
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				if (args.Length == 0)
				{
					return docContext.UniqueId;
				}
				XPathNodeIterator xpathNodeIterator = XsltCompileContext.XsltFunctionImpl.ToIterator(args[0]);
				if (xpathNodeIterator.MoveNext())
				{
					return xpathNodeIterator.Current.UniqueId;
				}
				return string.Empty;
			}
		}

		// Token: 0x0200055A RID: 1370
		private class FuncSystemProp : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x06003713 RID: 14099 RVA: 0x00133970 File Offset: 0x00131B70
			public FuncSystemProp()
				: base(1, 1, XPathResultType.String, new XPathResultType[] { XPathResultType.String })
			{
			}

			// Token: 0x06003714 RID: 14100 RVA: 0x001339EE File Offset: 0x00131BEE
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				return ((XsltCompileContext)xsltContext).SystemProperty(XsltCompileContext.XsltFunctionImpl.ToString(args[0]));
			}
		}

		// Token: 0x0200055B RID: 1371
		private class FuncElementAvailable : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x06003715 RID: 14101 RVA: 0x00133A03 File Offset: 0x00131C03
			public FuncElementAvailable()
				: base(1, 1, XPathResultType.Boolean, new XPathResultType[] { XPathResultType.String })
			{
			}

			// Token: 0x06003716 RID: 14102 RVA: 0x00133A18 File Offset: 0x00131C18
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				return ((XsltCompileContext)xsltContext).ElementAvailable(XsltCompileContext.XsltFunctionImpl.ToString(args[0]));
			}
		}

		// Token: 0x0200055C RID: 1372
		private class FuncFunctionAvailable : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x06003717 RID: 14103 RVA: 0x00133A03 File Offset: 0x00131C03
			public FuncFunctionAvailable()
				: base(1, 1, XPathResultType.Boolean, new XPathResultType[] { XPathResultType.String })
			{
			}

			// Token: 0x06003718 RID: 14104 RVA: 0x00133A32 File Offset: 0x00131C32
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				return ((XsltCompileContext)xsltContext).FunctionAvailable(XsltCompileContext.XsltFunctionImpl.ToString(args[0]));
			}
		}

		// Token: 0x0200055D RID: 1373
		private class FuncDocument : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x06003719 RID: 14105 RVA: 0x00133A4C File Offset: 0x00131C4C
			public FuncDocument()
				: base(1, 2, XPathResultType.NodeSet, new XPathResultType[]
				{
					XPathResultType.Any,
					XPathResultType.NodeSet
				})
			{
			}

			// Token: 0x0600371A RID: 14106 RVA: 0x00133A68 File Offset: 0x00131C68
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				string text = null;
				if (args.Length == 2)
				{
					XPathNodeIterator xpathNodeIterator = XsltCompileContext.XsltFunctionImpl.ToIterator(args[1]);
					if (xpathNodeIterator.MoveNext())
					{
						text = xpathNodeIterator.Current.BaseURI;
					}
					else
					{
						text = string.Empty;
					}
				}
				object obj;
				try
				{
					obj = ((XsltCompileContext)xsltContext).Document(args[0], text);
				}
				catch (Exception ex)
				{
					if (!XmlException.IsCatchableException(ex))
					{
						throw;
					}
					obj = XPathEmptyIterator.Instance;
				}
				return obj;
			}
		}

		// Token: 0x0200055E RID: 1374
		private class FuncKey : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x0600371B RID: 14107 RVA: 0x00133AD8 File Offset: 0x00131CD8
			public FuncKey()
				: base(2, 2, XPathResultType.NodeSet, new XPathResultType[]
				{
					XPathResultType.String,
					XPathResultType.Any
				})
			{
			}

			// Token: 0x0600371C RID: 14108 RVA: 0x00133AF4 File Offset: 0x00131CF4
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				XsltCompileContext xsltCompileContext = (XsltCompileContext)xsltContext;
				string text;
				string text2;
				PrefixQName.ParseQualifiedName(XsltCompileContext.XsltFunctionImpl.ToString(args[0]), out text, out text2);
				string text3 = xsltContext.LookupNamespace(text);
				XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(text2, text3);
				XPathNavigator xpathNavigator = docContext.Clone();
				xpathNavigator.MoveToRoot();
				ArrayList arrayList = null;
				foreach (Key key in xsltCompileContext.processor.KeyList)
				{
					if (key.Name == xmlQualifiedName)
					{
						Hashtable hashtable = key.GetKeys(xpathNavigator);
						if (hashtable == null)
						{
							hashtable = xsltCompileContext.BuildKeyTable(key, xpathNavigator);
							key.AddKey(xpathNavigator, hashtable);
						}
						XPathNodeIterator xpathNodeIterator = args[1] as XPathNodeIterator;
						if (xpathNodeIterator != null)
						{
							xpathNodeIterator = xpathNodeIterator.Clone();
							while (xpathNodeIterator.MoveNext())
							{
								XPathNavigator xpathNavigator2 = xpathNodeIterator.Current;
								arrayList = XsltCompileContext.FuncKey.AddToList(arrayList, (ArrayList)hashtable[xpathNavigator2.Value]);
							}
						}
						else
						{
							arrayList = XsltCompileContext.FuncKey.AddToList(arrayList, (ArrayList)hashtable[XsltCompileContext.XsltFunctionImpl.ToString(args[1])]);
						}
					}
				}
				if (arrayList == null)
				{
					return XPathEmptyIterator.Instance;
				}
				if (arrayList[0] is XPathNavigator)
				{
					return new XPathArrayIterator(arrayList);
				}
				return new XPathMultyIterator(arrayList);
			}

			// Token: 0x0600371D RID: 14109 RVA: 0x00133C2C File Offset: 0x00131E2C
			private static ArrayList AddToList(ArrayList resultCollection, ArrayList newList)
			{
				if (newList == null)
				{
					return resultCollection;
				}
				if (resultCollection == null)
				{
					return newList;
				}
				if (!(resultCollection[0] is ArrayList))
				{
					ArrayList arrayList = resultCollection;
					resultCollection = new ArrayList();
					resultCollection.Add(arrayList);
				}
				resultCollection.Add(newList);
				return resultCollection;
			}
		}

		// Token: 0x0200055F RID: 1375
		private class FuncFormatNumber : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x0600371E RID: 14110 RVA: 0x00133C6B File Offset: 0x00131E6B
			public FuncFormatNumber()
				: base(2, 3, XPathResultType.String, new XPathResultType[]
				{
					XPathResultType.Number,
					XPathResultType.String,
					XPathResultType.String
				})
			{
			}

			// Token: 0x0600371F RID: 14111 RVA: 0x00133C84 File Offset: 0x00131E84
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				DecimalFormat decimalFormat = ((XsltCompileContext)xsltContext).ResolveFormatName((args.Length == 3) ? XsltCompileContext.XsltFunctionImpl.ToString(args[2]) : null);
				return DecimalFormatter.Format(XsltCompileContext.XsltFunctionImpl.ToNumber(args[0]), XsltCompileContext.XsltFunctionImpl.ToString(args[1]), decimalFormat);
			}
		}

		// Token: 0x02000560 RID: 1376
		private class FuncNodeSet : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x06003720 RID: 14112 RVA: 0x00133CC4 File Offset: 0x00131EC4
			public FuncNodeSet()
				: base(1, 1, XPathResultType.NodeSet, new XPathResultType[] { XPathResultType.String })
			{
			}

			// Token: 0x06003721 RID: 14113 RVA: 0x00133CD9 File Offset: 0x00131ED9
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				return new XPathSingletonIterator(XsltCompileContext.XsltFunctionImpl.ToNavigator(args[0]));
			}
		}

		// Token: 0x02000561 RID: 1377
		private class FuncExtension : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x06003722 RID: 14114 RVA: 0x00133CE8 File Offset: 0x00131EE8
			public FuncExtension(object extension, MethodInfo method, PermissionSet permissions)
			{
				this.extension = extension;
				this.method = method;
				this.permissions = permissions;
				XPathResultType xpathType = XsltCompileContext.GetXPathType(method.ReturnType);
				ParameterInfo[] parameters = method.GetParameters();
				int num = parameters.Length;
				int num2 = parameters.Length;
				this.typeCodes = new TypeCode[parameters.Length];
				XPathResultType[] array = new XPathResultType[parameters.Length];
				bool flag = true;
				int num3 = parameters.Length - 1;
				while (0 <= num3)
				{
					this.typeCodes[num3] = Type.GetTypeCode(parameters[num3].ParameterType);
					array[num3] = XsltCompileContext.GetXPathType(parameters[num3].ParameterType);
					if (flag)
					{
						if (parameters[num3].IsOptional)
						{
							num--;
						}
						else
						{
							flag = false;
						}
					}
					num3--;
				}
				base.Init(num, num2, xpathType, array);
			}

			// Token: 0x06003723 RID: 14115 RVA: 0x00133DA8 File Offset: 0x00131FA8
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				int num = args.Length - 1;
				while (0 <= num)
				{
					args[num] = XsltCompileContext.XsltFunctionImpl.ConvertToXPathType(args[num], base.ArgTypes[num], this.typeCodes[num]);
					num--;
				}
				if (this.permissions != null)
				{
					this.permissions.PermitOnly();
				}
				return this.method.Invoke(this.extension, args);
			}

			// Token: 0x04002338 RID: 9016
			private object extension;

			// Token: 0x04002339 RID: 9017
			private MethodInfo method;

			// Token: 0x0400233A RID: 9018
			private TypeCode[] typeCodes;

			// Token: 0x0400233B RID: 9019
			private PermissionSet permissions;
		}
	}
}
