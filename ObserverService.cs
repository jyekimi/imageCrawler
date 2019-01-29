using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Xml;
using System.Configuration;
using RestSharp;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using HtmlAgilityPack;
using ImageCrawler.Observer.Common;

namespace ImageCrawler.Observer
{
	public class ObserverService : ControllerBase
	{
		//디폴트 설정
		private static int SLEEP = 60000;
		private static int CHECK_COUNT = 5;
		private static bool SMS_SENDING = true;
		public int errorcount = 0;

		public ObserverService()
		{
		}


		protected override void main()
		{

			SLEEP = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["Sleep"]);
		
			Logger.Write("Observer Start");

			while (true)
			{
				List<string> imgUrls = new List<string>();
				//string contents = new ImageObserverInfo().GetSnapshot();
				
				List<string> catagoryPath = new List<string>();
				catagoryPath = new ImageObserverInfo().GetCategory();

				foreach (string categoryTarget in catagoryPath)
				{

					imgUrls = new ImageObserverInfo().GetUrlSource(categoryTarget);
					//중복 ImageUrl 제거
					imgUrls = imgUrls != null ? imgUrls.Distinct().ToList() : new List<string>();

					if (!string.IsNullOrEmpty(categoryTarget))
					{ 
					//image url 들을 파일로 떨굼
					#region 추출된 ImagUrl 중 통합이미지 URL 만 탐색
					foreach (string item in imgUrls)
					{
						string targetUrls = item;

						if (targetUrls.IndexOf("gdimg.gmarket.co.kr", StringComparison.OrdinalIgnoreCase) > -1)
						{
							int returnValue = new ImageObserverInfo().GetImageContents(targetUrls, categoryTarget);
						}
					}
					#endregion
					Logger.Write("Category Number : " + categoryTarget + " is successfully finished ");
					}
					else { }

				}

				Logger.Write("Observer Waiting.." + SLEEP.ToString());
				Thread.Sleep(SLEEP);

			}

		}

		public void StartFromDebugger(string[] args)
		{
			OnStart(args);
		}

	}
}
