using System;
using System.IO;
using System.Net;

namespace Discord.Webhook
{
	// Token: 0x0200000A RID: 10
	public class DiscordWebhook
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002447 File Offset: 0x00000647
		// (set) Token: 0x06000010 RID: 16 RVA: 0x0000244F File Offset: 0x0000064F
		public string Url { get; set; }

		// Token: 0x06000011 RID: 17 RVA: 0x00002458 File Offset: 0x00000658
		private void AddField(MemoryStream stream, string bound, string cDisposition, string cType, byte[] data)
		{
			byte[] array = Utils.Encode(((stream.Length > 0L) ? "\r\n--" : "--") + bound + "\r\n", "utf-8");
			byte[] array2 = Utils.Encode(cDisposition, "utf-8");
			byte[] array3 = Utils.Encode(cType, "utf-8");
			stream.Write(array, 0, array.Length);
			stream.Write(array2, 0, array2.Length);
			stream.Write(array3, 0, array3.Length);
			stream.Write(data, 0, data.Length);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024D8 File Offset: 0x000006D8
		private void SetJsonPayload(MemoryStream stream, string bound, string json)
		{
			string cDisposition = "Content-Disposition: form-data; name=\"payload_json\"\r\n";
			string cType = "Content-Type: application/octet-stream\r\n\r\n";
			this.AddField(stream, bound, cDisposition, cType, Utils.Encode(json, "utf-8"));
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002508 File Offset: 0x00000708
		private void SetFile(MemoryStream stream, string bound, int index, FileInfo file)
		{
			string cDisposition = string.Format("Content-Disposition: form-data; name=\"file_{0}\"; filename=\"{1}\"\r\n", index, file.Name);
			string cType = "Content-Type: application/octet-stream\r\n\r\n";
			this.AddField(stream, bound, cDisposition, cType, File.ReadAllBytes(file.FullName));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000254C File Offset: 0x0000074C
		public void Send(DiscordMessage message, params FileInfo[] files)
		{
			if (string.IsNullOrEmpty(this.Url))
			{
				throw new ArgumentNullException("Invalid Webhook URL.");
			}
			string text = "------------------------" + DateTime.Now.Ticks.ToString("x");
			WebClient webClient = new WebClient();
			webClient.Headers.Add("Content-Type", "multipart/form-data; boundary=" + text);
			MemoryStream memoryStream = new MemoryStream();
			for (int i = 0; i < files.Length; i++)
			{
				this.SetFile(memoryStream, text, i, files[i]);
			}
			string json = message.ToString();
			this.SetJsonPayload(memoryStream, text, json);
			byte[] array = Utils.Encode("\r\n--" + text + "--", "utf-8");
			memoryStream.Write(array, 0, array.Length);
			try
			{
				webClient.UploadData(this.Url, memoryStream.ToArray());
			}
			catch (WebException ex)
			{
				throw new WebException(Utils.Decode(ex.Response.GetResponseStream()));
			}
			memoryStream.Dispose();
		}
	}
}
