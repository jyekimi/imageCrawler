using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Configuration;
using RestSharp;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Web;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;




namespace ImageCrawler.Observer
{
	public class ImageObserverInfo
	{
		public string TOTAL_PATH  = System.Configuration.ConfigurationSettings.AppSettings["FolderLocation"];// = @"d:\";
		public string TEMP_PATH = System.Configuration.ConfigurationSettings.AppSettings["TempFolderCategoryListLocation"];// = @"d:\T\";
		public int MAX_COUNT = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["GetMaxCount"]);
		#region 이미지 불러오기


		#region - 해당 카테고리의 상품이미지 url 을 반환한다. 
		
		/// <summary>
		/// 해당 카테고리의 상품이미지 url 을 반환한다.
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public List<string> GetUrlSource(string categoryTarget)
		{
				const int MAX_IN_COUNT = 100000;
				string GdscFolderPath = TOTAL_PATH + categoryTarget;// 소분류 폴더생성
				List<string> stringList = new List<string>();

				try { 
				DirectoryInfo di = new DirectoryInfo(GdscFolderPath);
				if (di.Exists == false)
				{
						di.Create();
				}

				//해당카테고리의 상품 이미지 파일 읽어오기 - 임시폴더에 TEXT 로 저장
				string urlContentsPath = TEMP_PATH + categoryTarget + ".txt";


				string[] stringArray = new string[MAX_IN_COUNT];
				string appendString = string.Empty;
				char[] splitterStr = {'\r','\n'};
				//string.replace("\r\n","|") 
				//using (System.Net.WebClient client = new System.Net.WebClient())
				//{
				//	return client.DownloadString(url);
				//}
				//if(File.Exists(path))
				//{}

				if (File.Exists(urlContentsPath))
				{
						appendString = File.ReadAllText(urlContentsPath,UTF8Encoding.UTF8);
						stringArray = appendString.Split(splitterStr);


						///이미지 url 도 만들기
						foreach (string str in stringArray)
						{
							if (!string.IsNullOrEmpty(str))
							{
								if (str.IndexOf("gdimg", StringComparison.OrdinalIgnoreCase) > -1)
								{

									stringList.Add(str);

								}
								else { 
								string testStr = "http://gdimg.gmarket.co.kr/" + str + "/still/600";
								stringList.Add(testStr);
									}

							}
							else { 

							}

						}


				}
				else{}


			}
				catch (NullReferenceException ex) { 

				
			}
			return stringList;


		}
		#endregion
		
		#region - 여성의류 소분류 카테고리 가져오기- HC
		
		/// <summary>
		/// 여성의류 소분류 카테고리 가져오기- HC
		/// </summary>
		/// <returns></returns>
		public List<string> GetCategory()
		{
				string gdscCategoryNoPath = TEMP_PATH + "gdscCategoryNo.txt";
				List<string> CateList = new List<string>();
				string appendString = string.Empty;
				char[] splitterStr = {'\r','\n'};
				string[] stringArray = new string[MAX_COUNT];



				appendString = File.ReadAllText(gdscCategoryNoPath,UTF8Encoding.UTF8);
				stringArray = appendString.Split(splitterStr);

				foreach(string str in stringArray)
				{
						CateList.Add(str);
				}


				/*
				 * 
				 * 
				 * 
				 * 
				 * 
				 * 
				 
				CateList.Add("300004667");
				CateList.Add("300005739");
				CateList.Add("300010668");
				CateList.Add("300007963");
				CateList.Add("300004673");
				CateList.Add("300004668");
				CateList.Add("300019915");
				CateList.Add("300007107");
				CateList.Add("300004669");
				CateList.Add("300004671");
				CateList.Add("300019916");
				CateList.Add("300004676");
				CateList.Add("300019090");
				CateList.Add("300019917");
				CateList.Add("300004675");
				CateList.Add("300021000");
				CateList.Add("300004679");
				CateList.Add("300006041");
				CateList.Add("300004680");
				CateList.Add("300008745");
				CateList.Add("300020841");
				CateList.Add("300018892");
				CateList.Add("300004681");
				CateList.Add("300017465");
				CateList.Add("300004691");
				CateList.Add("300004688");
				CateList.Add("300006303");
				CateList.Add("300019226");
				CateList.Add("300005743");
				CateList.Add("300006865");
				CateList.Add("300005742");
				CateList.Add("300019911");
				CateList.Add("300017992");
				CateList.Add("300005765");
				CateList.Add("300004700");
				CateList.Add("300010056");
				CateList.Add("300006302");
				CateList.Add("300004701");
				CateList.Add("300009607");
				CateList.Add("300019245");
				CateList.Add("300004692");
				CateList.Add("300004693");
				CateList.Add("300004698");
				CateList.Add("300009453");
				CateList.Add("300004699");
				CateList.Add("300004712");
				CateList.Add("300005884");
				CateList.Add("300019195");
				CateList.Add("300004714");
				CateList.Add("300007828");
				CateList.Add("300004708");
				CateList.Add("300027562");
				CateList.Add("300004707");
				CateList.Add("300018032");
				CateList.Add("300021003");
				CateList.Add("300019900");
				CateList.Add("300004718");
				CateList.Add("300006907");
				CateList.Add("300004719");
				CateList.Add("300004717");
				CateList.Add("300006305");
				CateList.Add("300008686");
				CateList.Add("300004715");
				CateList.Add("300008687");
				CateList.Add("300027420");
				CateList.Add("300023418");
				CateList.Add("300019039");
				CateList.Add("300004721");
				CateList.Add("300004722");
				CateList.Add("300027230");
				CateList.Add("300009409");
				CateList.Add("300004726");
				CateList.Add("300004725");
				CateList.Add("300010411");
				CateList.Add("300004727");
				CateList.Add("300019710");
				CateList.Add("300004723");
				CateList.Add("300004724");
				CateList.Add("300018694");
				CateList.Add("300012258");
				CateList.Add("300009545");
				CateList.Add("300021004");
				CateList.Add("300019920");
				CateList.Add("300007114");
				CateList.Add("300019919");
				CateList.Add("300009406");
				CateList.Add("300011122");
				CateList.Add("300009408");
				CateList.Add("300023319");
				CateList.Add("300019709");
				CateList.Add("300019091");
				CateList.Add("300027563");
				CateList.Add("300016940");
				CateList.Add("300020856");
				CateList.Add("300017464");
				CateList.Add("300008714");
				CateList.Add("300027564");
				CateList.Add("300023320");
				CateList.Add("300004690");
				CateList.Add("300019218");
				CateList.Add("300006840");
				CateList.Add("300019223");
				CateList.Add("300019224");
				CateList.Add("300008730");
				CateList.Add("300020857");
				CateList.Add("300020998");
				CateList.Add("300006928");
				CateList.Add("300004674");
				CateList.Add("300008810");
				CateList.Add("300027565");
				CateList.Add("300015829");
				CateList.Add("300025933");
				CateList.Add("300023417");
				CateList.Add("300026055");
				CateList.Add("300005886");
				CateList.Add("300007866");
				CateList.Add("300008620");
				CateList.Add("300019042");
				CateList.Add("300004694");
				CateList.Add("300004670");
				CateList.Add("300004711");
				CateList.Add("300026275");
				CateList.Add("300004730");
				CateList.Add("300025740");
				CateList.Add("300026274");
				 * */


				return CateList;
		}
		#endregion

		# region - GetCollectImgUrl :  Imge Url 을 반환
		/// <summary>
		/// Imge Url 을 반환한다.
		/// </summary>
		/// <param name="doc"></param>
		/// <returns></returns>
		public static List<string> GetCollectImgUrl(HtmlDocument doc)
		{
			List<string> stringList = new List<string>();

			//Html 도큐먼트 안의 img 태그에서 src 에 있는 url 추출
			if (doc.DocumentNode.SelectNodes("//img") != null)
			{
				foreach (HtmlNode image in doc.DocumentNode.SelectNodes("//img"))
				{
					HtmlAttribute attr = image.Attributes["src"];
					string srcText = string.Empty;
					if (attr != null)
					{
						srcText = attr.Value;
					}
					// 이미지의 경로가 상대경로인 것은 스킵합니다.
					if (srcText.StartsWith(".") || srcText.StartsWith(".."))
					{
						srcText = string.Empty;
					}
					if (string.IsNullOrEmpty(srcText) == false)
					{
						//Escape Sequence 제거(textarea 를 통해 입력되는 \n \t 제거)
						srcText = srcText.Replace("\t", string.Empty);
						srcText = srcText.Replace("\n", string.Empty);
						stringList.Add(srcText);
					}
				}
			}
			return stringList;
		}
		# endregion


		public int GetImageContents(string itemImageUrl, string category)
		{
			int tempCnt = 0;

			try {
				using (WebClient c = new WebClient())
				{
					string gdNo = Regex.Replace(itemImageUrl, @"\D", "");// 숫자만 추출
					gdNo = gdNo.Substring(0,9);
					string gdNoPath = gdNo +".jpg";

				
						byte[] responseData = c.DownloadData(itemImageUrl);
						//string type = c.ResponseHeaders.ToString();
						
						Stream sr = new MemoryStream(responseData);
						//byte[] fileContents = Encoding.UTF8.GetBytes(sr.ReadToEnd());
						string pathString = System.IO.Path.Combine(TOTAL_PATH+category, gdNoPath);


						if (!System.IO.File.Exists(pathString))
						{
								using (System.IO.FileStream fs = System.IO.File.Create(pathString))
								{
									for (byte i = 0; i < fs.Length; i++)
									{
										fs.WriteByte(i);
									}
									Image img = System.Drawing.Image.FromStream(sr);
									img.Save(fs, ImageFormat.Jpeg);
								}

								Logger.Write("[goods img]ItemNo Number : " + gdNo + " is saved");

						}
						else
						{

								//Console.WriteLine("File \"{0}\" already exists.", pathString);
								//tempCnt ++;
								//if (tempCnt > 1200)
								//{

								//}
						}
					
				
				}
			}
			catch (WebException ex) { }
				

				//return new FileStreamResult(new MemoryStream(responseData), "image/jpeg");
				return 1;
		}


	
		#endregion



	}
}
