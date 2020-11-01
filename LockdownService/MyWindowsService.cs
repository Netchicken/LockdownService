using log4net;

namespace LockdownService
{
    public class MyWindowsService
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(MyWindowsService));

        URlListener uRlListener = new URlListener();
        public void Start()
        {
            _log.Info("LockdownService Starting...");

            try
            {
                //    uRlListener.SimpleListener();
                //   Audio.Play();
                uRlListener.OpenWebpage();
            }
            catch (System.Exception e)
            {

                _log.Info(e.ToString());
                Stop();
                _log.Info("LockdownService crashed .");
            }



            _log.Info("LockdownService Started succesfully.");
        }

        public void Stop()
        {
            _log.Info("LockdownService Stopping...");
            // business logic goes here
            _log.Info("LockdownService Stopped succesfully.");
        }
        public void Pause()
        {
        }

        public void Resume()
        {
        }

        public void Shutdown()
        {
        }





    }
}
