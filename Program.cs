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
using System.ServiceProcess;

namespace ImageCrawler.Observer
{
	public class Program
	{


		static void Main(string[] args)
		{


			if (!Environment.UserInteractive)
			{
				ServiceBase[] ServicesToRun;
				ServicesToRun = new ServiceBase[] { new ObserverService() };
				ServiceBase.Run(ServicesToRun);
			}
			else
			{
				ObserverService service = new ObserverService();
				service.StartFromDebugger(args);
				System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
			}
		}





	}
}
