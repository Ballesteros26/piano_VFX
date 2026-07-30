using System;
using System.Collections;
using System.Text;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004EA RID: 1258
	internal sealed class Avt
	{
		// Token: 0x06003342 RID: 13122 RVA: 0x001257E7 File Offset: 0x001239E7
		private Avt(string constAvt)
		{
			this.constAvt = constAvt;
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x001257F8 File Offset: 0x001239F8
		private Avt(ArrayList eventList)
		{
			this.events = new TextEvent[eventList.Count];
			for (int i = 0; i < eventList.Count; i++)
			{
				this.events[i] = (TextEvent)eventList[i];
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06003344 RID: 13124 RVA: 0x00125841 File Offset: 0x00123A41
		public bool IsConstant
		{
			get
			{
				return this.events == null;
			}
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x0012584C File Offset: 0x00123A4C
		internal string Evaluate(Processor processor, ActionFrame frame)
		{
			if (this.IsConstant)
			{
				return this.constAvt;
			}
			StringBuilder sharedStringBuilder = processor.GetSharedStringBuilder();
			for (int i = 0; i < this.events.Length; i++)
			{
				sharedStringBuilder.Append(this.events[i].Evaluate(processor, frame));
			}
			processor.ReleaseSharedStringBuilder();
			return sharedStringBuilder.ToString();
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x001258A4 File Offset: 0x00123AA4
		internal static Avt CompileAvt(Compiler compiler, string avtText)
		{
			bool flag;
			ArrayList arrayList = compiler.CompileAvt(avtText, out flag);
			if (!flag)
			{
				return new Avt(arrayList);
			}
			return new Avt(avtText);
		}

		// Token: 0x04002123 RID: 8483
		private string constAvt;

		// Token: 0x04002124 RID: 8484
		private TextEvent[] events;
	}
}
