using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.XPath;

namespace Mono.Web.Util
{
	// Token: 0x0200000A RID: 10
	internal class SettingsMapping
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002617 File Offset: 0x00000817
		public Type SectionType
		{
			get
			{
				if (this._sectionType == null)
				{
					this._sectionType = Type.GetType(this._sectionTypeName, false);
				}
				return this._sectionType;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002640 File Offset: 0x00000840
		public Type MapperType
		{
			get
			{
				if (this._mapperType == null)
				{
					this._mapperType = Type.GetType(this._mapperTypeName, true);
					if (!typeof(ISectionSettingsMapper).IsAssignableFrom(this._mapperType))
					{
						this._mapperType = null;
						throw new InvalidOperationException("Mapper type does not implement the ISectionSettingsMapper interface");
					}
				}
				return this._mapperType;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000269C File Offset: 0x0000089C
		public SettingsMappingPlatform Platform
		{
			get
			{
				return this._platform;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026A4 File Offset: 0x000008A4
		public SettingsMapping(XPathNavigator nav)
		{
			this._sectionTypeName = nav.GetAttribute("sectionType", string.Empty);
			this._mapperTypeName = nav.GetAttribute("mapperType", string.Empty);
			EnumConverter enumConverter = new EnumConverter(typeof(SettingsMappingPlatform));
			this._platform = (SettingsMappingPlatform)enumConverter.ConvertFromInvariantString(nav.GetAttribute("platform", string.Empty));
			this.LoadContents(nav);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000271C File Offset: 0x0000091C
		public object MapSection(object input, Type type)
		{
			if (type != this.SectionType)
			{
				throw new ArgumentException("type", "Invalid section type for this mapper");
			}
			ISectionSettingsMapper sectionSettingsMapper = Activator.CreateInstance(this.MapperType) as ISectionSettingsMapper;
			if (sectionSettingsMapper == null)
			{
				return input;
			}
			return sectionSettingsMapper.MapSection(input, this._whats);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000276C File Offset: 0x0000096C
		private void LoadContents(XPathNavigator nav)
		{
			XPathNodeIterator xpathNodeIterator = nav.Select("./what[string-length (@value) > 0]");
			this._whats = new List<SettingsMappingWhat>();
			while (xpathNodeIterator.MoveNext())
			{
				XPathNavigator xpathNavigator = xpathNodeIterator.Current;
				this._whats.Add(new SettingsMappingWhat(xpathNavigator));
			}
		}

		// Token: 0x04000D2B RID: 3371
		private string _sectionTypeName;

		// Token: 0x04000D2C RID: 3372
		private Type _sectionType;

		// Token: 0x04000D2D RID: 3373
		private string _mapperTypeName;

		// Token: 0x04000D2E RID: 3374
		private Type _mapperType;

		// Token: 0x04000D2F RID: 3375
		private SettingsMappingPlatform _platform;

		// Token: 0x04000D30 RID: 3376
		private List<SettingsMappingWhat> _whats;
	}
}
