using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000143 RID: 323
	public class ScalableSettingSchema
	{
		// Token: 0x0600095B RID: 2395 RVA: 0x0004BF6C File Offset: 0x0004A16C
		internal static ScalableSettingSchema GetSchemaOrNull(ScalableSettingSchemaId id)
		{
			ScalableSettingSchema scalableSettingSchema;
			if (!ScalableSettingSchema.Schemas.TryGetValue(id, out scalableSettingSchema))
			{
				return null;
			}
			return scalableSettingSchema;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0004BF8C File Offset: 0x0004A18C
		internal static ScalableSettingSchema GetSchemaOrNull(ScalableSettingSchemaId? id)
		{
			ScalableSettingSchema scalableSettingSchema;
			if (id == null || !ScalableSettingSchema.Schemas.TryGetValue(id.Value, out scalableSettingSchema))
			{
				return null;
			}
			return scalableSettingSchema;
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0004BFBA File Offset: 0x0004A1BA
		public int levelCount
		{
			get
			{
				return this.levelNames.Length;
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0004BFC4 File Offset: 0x0004A1C4
		public ScalableSettingSchema(GUIContent[] levelNames)
		{
			this.levelNames = levelNames;
		}

		// Token: 0x04000EF7 RID: 3831
		internal static readonly Dictionary<ScalableSettingSchemaId, ScalableSettingSchema> Schemas = new Dictionary<ScalableSettingSchemaId, ScalableSettingSchema>
		{
			{
				ScalableSettingSchemaId.With3Levels,
				new ScalableSettingSchema(new GUIContent[]
				{
					new GUIContent("Low"),
					new GUIContent("Medium"),
					new GUIContent("High")
				})
			},
			{
				ScalableSettingSchemaId.With4Levels,
				new ScalableSettingSchema(new GUIContent[]
				{
					new GUIContent("Low"),
					new GUIContent("Medium"),
					new GUIContent("High"),
					new GUIContent("Ultra")
				})
			}
		};

		// Token: 0x04000EF8 RID: 3832
		public readonly GUIContent[] levelNames;
	}
}
