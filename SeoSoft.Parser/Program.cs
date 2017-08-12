using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace WorkWithFIle
{
	class Program
	{
		static void Main(string[] args)
		{

			var doc = new HtmlDocument();
			Stream fileRead = File.OpenRead("test.html");

			doc.Load(fileRead);
			var values = doc.DocumentNode
			.SelectNodes("//html[1]/body[1]/div[4]/div[2]/div[2]/div[2]/div[2]/div[1]/div[1]");
			foreach (var value in values)
			{
				Console.WriteLine(value.InnerHtml);
			}
			var script = HtmlNode.CreateNode("<script src='../scripts/XpathWorker.js'></script>");
			var css = HtmlNode.CreateNode("<script src='../scripts/XpathWorker.js'></script>");
			doc.DocumentNode.SelectSingleNode("//head").PrependChild(css);
			doc.DocumentNode.SelectSingleNode("//head").PrependChild(script);

			fileRead.Dispose();
			using (Stream fileWrit = File.OpenWrite("test.html"))
			{
				doc.Save(fileWrit);
			}


			Console.ReadLine();
			//string filePath = "test.txt";
			//Console.WriteLine("Enter string in file:");
			//string text = LoadData().Result;
			//using (StreamWriter file =
			//new StreamWriter(File.Create(filePath)))
			//{
			//    file.WriteLineAsync(text);
			//}

		}

		static async Task<string> LoadData()
		{
			string page = "https://www.nuget.org/packages/System.IO.FileSystem/";
			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage respone = await client.GetAsync(page))
				{
					using (HttpContent content = respone.Content)
					{
						return await content.ReadAsStringAsync();
					}
				}
			}
		}
	}
}