using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Storages
{
    [CreateAssetMenu(fileName = "Models", menuName = "Storages/Models")]
    public sealed class ModelStorage : ScriptableObject
    {
        public List<GameObject> VehicleModels;
        public List<GameObject> EmployeeModels;
        public GameObject TargetWayPoint;
    }
}