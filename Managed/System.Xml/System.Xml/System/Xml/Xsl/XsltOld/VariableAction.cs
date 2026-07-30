using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000552 RID: 1362
	internal class VariableAction : ContainerAction, IXsltContextVariable
	{
		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x060036CC RID: 14028 RVA: 0x001325DD File Offset: 0x001307DD
		internal int Stylesheetid
		{
			get
			{
				return this.stylesheetid;
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x060036CD RID: 14029 RVA: 0x001325E5 File Offset: 0x001307E5
		internal XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x060036CE RID: 14030 RVA: 0x001325ED File Offset: 0x001307ED
		internal string NameStr
		{
			get
			{
				return this.nameStr;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x060036CF RID: 14031 RVA: 0x001325F5 File Offset: 0x001307F5
		internal VariableType VarType
		{
			get
			{
				return this.varType;
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x060036D0 RID: 14032 RVA: 0x001325FD File Offset: 0x001307FD
		internal int VarKey
		{
			get
			{
				return this.varKey;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x060036D1 RID: 14033 RVA: 0x00132605 File Offset: 0x00130805
		internal bool IsGlobal
		{
			get
			{
				return this.varType == VariableType.GlobalVariable || this.varType == VariableType.GlobalParameter;
			}
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x0013261A File Offset: 0x0013081A
		internal VariableAction(VariableType type)
		{
			this.varType = type;
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x00132630 File Offset: 0x00130830
		internal override void Compile(Compiler compiler)
		{
			this.stylesheetid = compiler.Stylesheetid;
			this.baseUri = compiler.Input.BaseURI;
			base.CompileAttributes(compiler);
			base.CheckRequiredAttribute(compiler, this.name, "name");
			if (compiler.Recurse())
			{
				base.CompileTemplate(compiler);
				compiler.ToParent();
				if (this.selectKey != -1 && this.containedActions != null)
				{
					throw XsltException.Create("The variable or parameter '{0}' cannot have both a 'select' attribute and non-empty content.", new string[] { this.nameStr });
				}
			}
			if (this.containedActions != null)
			{
				this.baseUri = this.baseUri + "#" + compiler.GetUnicRtfId();
			}
			else
			{
				this.baseUri = null;
			}
			this.varKey = compiler.InsertVariable(this);
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x001326F0 File Offset: 0x001308F0
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Name))
			{
				this.nameStr = value;
				this.name = compiler.CreateXPathQName(this.nameStr);
			}
			else
			{
				if (!Ref.Equal(localName, compiler.Atoms.Select))
				{
					return false;
				}
				this.selectKey = compiler.AddQuery(value);
			}
			return true;
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x00132768 File Offset: 0x00130968
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			object obj = null;
			switch (frame.State)
			{
			case 0:
				if (this.IsGlobal)
				{
					if (frame.GetVariable(this.varKey) != null)
					{
						frame.Finished();
						return;
					}
					frame.SetVariable(this.varKey, VariableAction.BeingComputedMark);
				}
				if (this.varType == VariableType.GlobalParameter)
				{
					obj = processor.GetGlobalParameter(this.name);
				}
				else if (this.varType == VariableType.LocalParameter)
				{
					obj = processor.GetParameter(this.name);
				}
				if (obj == null)
				{
					if (this.selectKey != -1)
					{
						obj = processor.RunQuery(frame, this.selectKey);
					}
					else
					{
						if (this.containedActions != null)
						{
							NavigatorOutput navigatorOutput = new NavigatorOutput(this.baseUri);
							processor.PushOutput(navigatorOutput);
							processor.PushActionFrame(frame);
							frame.State = 1;
							return;
						}
						obj = string.Empty;
					}
				}
				break;
			case 1:
				obj = ((NavigatorOutput)processor.PopOutput()).Navigator;
				break;
			case 2:
				break;
			default:
				return;
			}
			frame.SetVariable(this.varKey, obj);
			frame.Finished();
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x060036D6 RID: 14038 RVA: 0x000038E3 File Offset: 0x00001AE3
		XPathResultType IXsltContextVariable.VariableType
		{
			get
			{
				return XPathResultType.Any;
			}
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x0013285F File Offset: 0x00130A5F
		object IXsltContextVariable.Evaluate(XsltContext xsltContext)
		{
			return ((XsltCompileContext)xsltContext).EvaluateVariable(this);
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x060036D8 RID: 14040 RVA: 0x0013286D File Offset: 0x00130A6D
		bool IXsltContextVariable.IsLocal
		{
			get
			{
				return this.varType == VariableType.LocalVariable || this.varType == VariableType.LocalParameter;
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x060036D9 RID: 14041 RVA: 0x00132883 File Offset: 0x00130A83
		bool IXsltContextVariable.IsParam
		{
			get
			{
				return this.varType == VariableType.LocalParameter || this.varType == VariableType.GlobalParameter;
			}
		}

		// Token: 0x04002323 RID: 8995
		public static object BeingComputedMark = new object();

		// Token: 0x04002324 RID: 8996
		private const int ValueCalculated = 2;

		// Token: 0x04002325 RID: 8997
		protected XmlQualifiedName name;

		// Token: 0x04002326 RID: 8998
		protected string nameStr;

		// Token: 0x04002327 RID: 8999
		protected string baseUri;

		// Token: 0x04002328 RID: 9000
		protected int selectKey = -1;

		// Token: 0x04002329 RID: 9001
		protected int stylesheetid;

		// Token: 0x0400232A RID: 9002
		protected VariableType varType;

		// Token: 0x0400232B RID: 9003
		private int varKey;
	}
}
