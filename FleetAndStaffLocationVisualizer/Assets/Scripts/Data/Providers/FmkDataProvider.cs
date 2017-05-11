using Assets.Scripts.Data.Interfaces;
using System.Collections.Generic;
using Assets.Scripts.Data.Model.Objects;
using Assets.Scripts.Data.Model.Telemetrics;
using Assets.Scripts.Data.Providers.Fmk;
using UnityEngine;

namespace Assets.Scripts.Data.Providers
{
    /// <summary>
    /// The author is working in First Monitoring Company. http://firstmk.ru
    /// Current provider let to load navigation data from company's servers. 
    /// This data is confidential, therefore the data transfer protocol is not present in this repository.
    /// </summary>
    public sealed class FmkDataProvider : IDataProvider
    { 
        private FmkApiClient _client;

        public FmkDataProvider()
        {
            var credentials = Resources.Load<FmkCredentials>("Storages/FMK");
            _client = new FmkApiClient(credentials.Key, credentials.Login, credentials.Password);
        }

        public List<Building> LoadBuildings()
        {
            return new List<Building>(0); // Buildings are not supported.
        }

        public List<MoveableObject> LoadMoveableObjects()
        {      
            return _client.GetDevices();
        }

        public List<TrackPoint> UpdateMoveableObjectLocations()
        {
            return _client.GetLocations();
        }
    }
}
