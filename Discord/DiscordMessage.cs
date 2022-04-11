using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Discord
{
	// Token: 0x02000002 RID: 2
	public struct DiscordMessage
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public override string ToString()
		{
			return Utils.StructToJson(this).ToString(Formatting.None, Array.Empty<JsonConverter>());
		}

		// Token: 0x04000001 RID: 1
		public string Content;

		// Token: 0x04000002 RID: 2
		public bool TTS;

		// Token: 0x04000003 RID: 3
		public string Username;

		// Token: 0x04000004 RID: 4
		public string AvatarUrl;

		// Token: 0x04000005 RID: 5
		public List<DiscordEmbed> Embeds;
	}
}
