using log4net;
using System.Diagnostics;
using TourPlanner.DAL;

namespace TourPlanner.BL
{
    public class ConnectionModeFactory
    {
        readonly ILog log = LogManager.GetLogger(typeof(ConnectionModeFactory));

        private APITourRepository _onlinerepo;

        private MemoryTourRepository _offlinerepo;

        private bool _appIsOnline;

        public bool _isConnected = false;

        public ConnectionModeFactory(APITourRepository onlinerepo, MemoryTourRepository offlinerepo) 
        {
            _onlinerepo = onlinerepo;
            _offlinerepo = offlinerepo;
            if (_onlinerepo != null)
            {
                _isConnected = _onlinerepo.Connect(new Uri("https://dev2.gasstationsoftware.net/"));
            }
            
            if( _offlinerepo != null)
            {
                var loadSampleDataTask = _offlinerepo.LoadSampleDataAsync();
                loadSampleDataTask.Wait();
            }

            // defines whether the app is in offline or online made on start
            _appIsOnline = false;
        }

        public ITourRepository GetRepo() 
        {
            return (_appIsOnline) ? _onlinerepo : _offlinerepo;
        }

        public bool SwapMode()
        {
            _appIsOnline = !_appIsOnline;

            log.DebugFormat("Swapping Repository mode to {0} mode", _appIsOnline ? "online" : "offline");

            if (_appIsOnline && _isConnected == false)
            {
                if(_onlinerepo != null)
                {
                    _isConnected = _onlinerepo.Connect(new Uri("https://dev2.gasstationsoftware.net/"));
                }
                _appIsOnline = _isConnected;
            }
            return _appIsOnline;
        }
    }
}
