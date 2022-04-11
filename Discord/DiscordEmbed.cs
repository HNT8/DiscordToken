using System;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;

namespace Discord
{
	// Token: 0x02000003 RID: 3
	public struct DiscordEmbed
	{
		// Token: 0x06000002 RID: 2 RVA: 0x0000206D File Offset: 0x0000026D
		public override string ToString()
		{
			return Utils.StructToJson(this).ToString(Formatting.None, Array.Empty<JsonConverter>());
		}

		// Token: 0x04000006 RID: 6
		public string Title;

		// Token: 0x04000007 RID: 7
		public string Description;

		// Token: 0x04000008 RID: 8
		public string Url;

		// Token: 0x04000009 RID: 9
		public DateTime? Timestamp;

		// Token: 0x0400000A RID: 10
		public Color? Color;

		// Token: 0x0400000B RID: 11
		public EmbedFooter? Footer;

		// Token: 0x0400000C RID: 12
		public EmbedMedia? Image;

		// Token: 0x0400000D RID: 13
		public EmbedMedia? Thumbnail;

		// Token: 0x0400000E RID: 14
		public EmbedMedia? Video;

		// Token: 0x0400000F RID: 15
		public EmbedProvider? Provider;

		// Token: 0x04000010 RID: 16
		public EmbedAuthor? Author;

		// Token: 0x04000011 RID: 17
		public List<EmbedField> Fields;
	}
}
