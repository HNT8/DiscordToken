using System;
using Newtonsoft.Json;

namespace Discord
{
	// Token: 0x02000005 RID: 5
	public struct EmbedMedia
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020A7 File Offset: 0x000002A7
		public override string ToString()
		{
			return Utils.StructToJson(this).ToString(Formatting.None, Array.Empty<JsonConverter>());
		}

		// Token: 0x04000015 RID: 21
		public string Url;

		// Token: 0x04000016 RID: 22
		public string ProxyUrl;

		// Token: 0x04000017 RID: 23
		public int? Height;

		// Token: 0x04000018 RID: 24
		public int? Width;
	}
}
