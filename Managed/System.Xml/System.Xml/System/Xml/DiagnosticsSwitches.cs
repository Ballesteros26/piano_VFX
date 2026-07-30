using System;
using System.Diagnostics;

namespace System.Xml
{
	// Token: 0x02000203 RID: 515
	internal static class DiagnosticsSwitches
	{
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x0006D87A File Offset: 0x0006BA7A
		public static BooleanSwitch XmlSchemaContentModel
		{
			get
			{
				if (DiagnosticsSwitches.xmlSchemaContentModel == null)
				{
					DiagnosticsSwitches.xmlSchemaContentModel = new BooleanSwitch("XmlSchemaContentModel", "Enable tracing for the XmlSchema content model.");
				}
				return DiagnosticsSwitches.xmlSchemaContentModel;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x0006D8A2 File Offset: 0x0006BAA2
		public static TraceSwitch XmlSchema
		{
			get
			{
				if (DiagnosticsSwitches.xmlSchema == null)
				{
					DiagnosticsSwitches.xmlSchema = new TraceSwitch("XmlSchema", "Enable tracing for the XmlSchema class.");
				}
				return DiagnosticsSwitches.xmlSchema;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x0006D8CA File Offset: 0x0006BACA
		public static BooleanSwitch KeepTempFiles
		{
			get
			{
				if (DiagnosticsSwitches.keepTempFiles == null)
				{
					DiagnosticsSwitches.keepTempFiles = new BooleanSwitch("XmlSerialization.Compilation", "Keep XmlSerialization generated (temp) files.");
				}
				return DiagnosticsSwitches.keepTempFiles;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x0006D8F2 File Offset: 0x0006BAF2
		public static BooleanSwitch PregenEventLog
		{
			get
			{
				if (DiagnosticsSwitches.pregenEventLog == null)
				{
					DiagnosticsSwitches.pregenEventLog = new BooleanSwitch("XmlSerialization.PregenEventLog", "Log failures while loading pre-generated XmlSerialization assembly.");
				}
				return DiagnosticsSwitches.pregenEventLog;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x0006D91A File Offset: 0x0006BB1A
		public static TraceSwitch XmlSerialization
		{
			get
			{
				if (DiagnosticsSwitches.xmlSerialization == null)
				{
					DiagnosticsSwitches.xmlSerialization = new TraceSwitch("XmlSerialization", "Enable tracing for the System.Xml.Serialization component.");
				}
				return DiagnosticsSwitches.xmlSerialization;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x0006D942 File Offset: 0x0006BB42
		public static TraceSwitch XslTypeInference
		{
			get
			{
				if (DiagnosticsSwitches.xslTypeInference == null)
				{
					DiagnosticsSwitches.xslTypeInference = new TraceSwitch("XslTypeInference", "Enable tracing for the XSLT type inference algorithm.");
				}
				return DiagnosticsSwitches.xslTypeInference;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x0006D96A File Offset: 0x0006BB6A
		public static BooleanSwitch NonRecursiveTypeLoading
		{
			get
			{
				if (DiagnosticsSwitches.nonRecursiveTypeLoading == null)
				{
					DiagnosticsSwitches.nonRecursiveTypeLoading = new BooleanSwitch("XmlSerialization.NonRecursiveTypeLoading", "Turn on non-recursive algorithm generating XmlMappings for CLR types.");
				}
				return DiagnosticsSwitches.nonRecursiveTypeLoading;
			}
		}

		// Token: 0x04000D20 RID: 3360
		private static volatile BooleanSwitch xmlSchemaContentModel;

		// Token: 0x04000D21 RID: 3361
		private static volatile TraceSwitch xmlSchema;

		// Token: 0x04000D22 RID: 3362
		private static volatile BooleanSwitch keepTempFiles;

		// Token: 0x04000D23 RID: 3363
		private static volatile BooleanSwitch pregenEventLog;

		// Token: 0x04000D24 RID: 3364
		private static volatile TraceSwitch xmlSerialization;

		// Token: 0x04000D25 RID: 3365
		private static volatile TraceSwitch xslTypeInference;

		// Token: 0x04000D26 RID: 3366
		private static volatile BooleanSwitch nonRecursiveTypeLoading;
	}
}
