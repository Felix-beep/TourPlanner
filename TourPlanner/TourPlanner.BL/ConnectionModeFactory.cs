using TourPlanner.DAL;

namespace TourPlanner.BL
{
    public class ConnectionModeFactory
    {
        private APITourRepository _onlinerepo;

        private MemoryTourRepository _offlinerepo;

        private bool _appIsOnline;

        private bool _isConnected = false;

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
            _appIsOnline = _isConnected;
        }

        public ITourRepository GetRepo() 
        {
            return (_appIsOnline) ? _onlinerepo : _offlinerepo;
        }

        public bool SwapMode()
        {
            _appIsOnline = !_appIsOnline;

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
