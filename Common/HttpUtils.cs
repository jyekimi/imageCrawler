using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ImageCrawler.Observer
{
	public class HttpUtils
	{
		public HttpStatusCode GetHttpStatusCode(string Url, int timeOut = 5000)
		{
			//URL 공백 제거
			Url = Url.Trim();
			Url = Url.Replace(" ","");


			HttpStatusCode status = new HttpStatusCode();
			HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
			myHttpWebRequest.Timeout = timeOut;

			using (HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse())
			{
				status = myHttpWebResponse.StatusCode;
				myHttpWebResponse.Close();
				return status;
			}
		}
	}
}
