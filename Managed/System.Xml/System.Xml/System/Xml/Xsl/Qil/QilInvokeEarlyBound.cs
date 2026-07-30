using System;
using System.Reflection;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000632 RID: 1586
	internal class QilInvokeEarlyBound : QilTernary
	{
		// Token: 0x06003E93 RID: 16019 RVA: 0x0015785B File Offset: 0x00155A5B
		public QilInvokeEarlyBound(QilNodeType nodeType, QilNode name, QilNode method, QilNode arguments, XmlQueryType resultType)
			: base(nodeType, name, method, arguments)
		{
			this.xmlType = resultType;
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06003E94 RID: 16020 RVA: 0x00157870 File Offset: 0x00155A70
		// (set) Token: 0x06003E95 RID: 16021 RVA: 0x0015787D File Offset: 0x00155A7D
		public QilName Name
		{
			get
			{
				return (QilName)base.Left;
			}
			set
			{
				base.Left = value;
			}
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06003E96 RID: 16022 RVA: 0x00157886 File Offset: 0x00155A86
		// (set) Token: 0x06003E97 RID: 16023 RVA: 0x0015789D File Offset: 0x00155A9D
		public MethodInfo ClrMethod
		{
			get
			{
				return (MethodInfo)((QilLiteral)base.Center).Value;
			}
			set
			{
				((QilLiteral)base.Center).Value = value;
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06003E98 RID: 16024 RVA: 0x001578B0 File Offset: 0x00155AB0
		// (set) Token: 0x06003E99 RID: 16025 RVA: 0x001578BD File Offset: 0x00155ABD
		public QilList Arguments
		{
			get
			{
				return (QilList)base.Right;
			}
			set
			{
				base.Right = value;
			}
		}
	}
}
