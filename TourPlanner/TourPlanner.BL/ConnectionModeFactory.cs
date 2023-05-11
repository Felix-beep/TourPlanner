﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DAL;

namespace TourPlanner.BL
{
    public class ConnectionModeFactory
    {
        private APITourRepository _onlinerepo;

        private MemoryTourRepository _offlinerepo;

        private bool _appIsOnline;

        private bool _isConnected;

        public ConnectionModeFactory(APITourRepository onlinerepo, MemoryTourRepository offlinerepo) 
        {
            _onlinerepo = onlinerepo;
            _isConnected = _onlinerepo.Connect(new Uri("https://dev2.gasstationsoftware.net/"));

            _offlinerepo = offlinerepo;
            _offlinerepo.LoadSampleData();

            _appIsOnline = false;
        }

        public ITourRepository GetRepo() 
        {
            return (_appIsOnline) ? _onlinerepo : _offlinerepo;
        }

        public void SwapMode()
        {
            _appIsOnline = !_appIsOnline;

            if (_appIsOnline && _isConnected == false)
            {
                _isConnected = _onlinerepo.Connect(new Uri("https://dev2.gasstationsoftware.net/"));
                _appIsOnline = _isConnected;
            }
        }
    }
}
