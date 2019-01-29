using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;

namespace ImageCrawler.Observer.Common
{
	public abstract class ControllerBase : ServiceBase
	{
		Thread t;

		private bool isStopped = false;
		private bool isPaused = false;
		private object locking = new object();

		public bool IsStopped
		{
			get { return isStopped; }
		}

		public bool IsPaused
		{
			get { return isPaused; }
		}

		public ControllerBase()
		{
			this.CanPauseAndContinue = true;
		}

		protected override void OnStart(string[] args)
		{
			try
			{
				Logger.Write("-------------START-------------");
				foreach (string key in ConfigurationManager.AppSettings.AllKeys)
				{
					Logger.Write(string.Format("\t{0} : {1}", key, ConfigurationManager.AppSettings[key].ToString()));
				}
				Logger.Write("-------------------------------");
				t = new Thread(main);
				t.Start();
			}
			catch (Exception ex)
			{
				Logger.Write(ex.ToString());
			}
		}

		protected override void OnPause()
		{
			//Logger.Write("OnPause!!");
			if (isPaused == false)
			{
				lock (locking)
				{
					if (isPaused == false)
					{
						isPaused = true;
						Logger.Write("Process is isPaused. wait...");
					}
				}
			}
		}

		protected override void OnContinue()
		{
			//Logger.Write("OnContinue!!");
			if (isPaused == true)
			{
				lock (locking)
				{
					if (isPaused == true)
					{
						isPaused = false;
						Logger.Write("Restart work.");
					}
				}
			}
		}

		protected override void OnStop()
		{
			//Logger.Write("OnStop!!");
			if (isStopped == false)
			{
				lock (locking)
				{
					if (isStopped == false)
					{
						isStopped = true;
						//Logger.Write("Change isStopped flag to true");
					}
				}
			}

			if (t != null && t.IsAlive)
			{
				Logger.Write("Wait for alive thread...");
				t.Join();
			}

			Logger.Write("-------------END-------------");
		}

		protected abstract void main();
	}
}
