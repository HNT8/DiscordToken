using System;
using Newtonsoft.Json;

namespace Discord
{
	// Token: 0x02000006 RID: 6
	public struct EmbedProvider
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020C4 File Offset: 0x000002C4
		public override string ToString()
		{
			return Utils.StructToJson(this).ToString(Formatting.None, Array.Empty<JsonConverter>());
		}

		// Token: 0x04000019 RID: 25
		public string Name;

		// Token: 0x0400001A RID: 26
		public string Url;
	}
}
