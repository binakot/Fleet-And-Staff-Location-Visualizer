using Assets.Scripts.Data.Model.Objects;
using Assets.Scripts.Data.Model.Telemetrics;
using System.Collections.Generic;

namespace Assets.Scripts.Data.Interfaces
{
    public interface IDataProvider
    {
        List<Building> LoadBuildings();
        List<MoveableObject> LoadMoveableObjects();
        List<TrackPoint> UpdateMoveableObjectLocations();
    }
}
