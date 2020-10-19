using log4net;

namespace LockdownService
{
    public class MyWindowsService
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(MyWindowsService));

        public void Start()
        {

            URlListener.SimpleListenerAsync();




            _log.Info("LockdownService Starting...");
            // business logic goes here
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
