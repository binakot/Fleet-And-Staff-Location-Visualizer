using Assets.Scripts.Data.Interfaces;
using System;
using System.Collections.Generic;
using Assets.Scripts.Data.Model.Objects;
using Assets.Scripts.Data.Model.Telemetrics;

namespace Assets.Scripts.Data.Providers
{
    /// <summary>
    /// The author is working in First Monitoring Company. http://firstmk.ru
    /// Current provider let to load navigation data from company's servers. 
    /// This data is confidential, therefore the data transfer protocol is not present in this repository.
    /// </summary>
    public sealed class FmkDataProvider : IDataProvider
    {
        public List<Building> LoadBuildings()
        {
            throw new NotImplementedException();
        }

        public List<MoveableObject> LoadMoveableObjects()
        {
            throw new NotImplementedException();
        }

        public List<TrackPoint> UpdateMoveableObjectLocations()
        {
            throw new NotImplementedException();
        }
    }
}
