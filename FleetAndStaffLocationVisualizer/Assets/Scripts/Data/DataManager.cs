using Assets.Scripts.Data.Interfaces;
using Assets.Scripts.Data.Model.Objects;
using Assets.Scripts.Data.Providers;
using Assets.Scripts.Storages;
using Assets.Scripts.UI;
using Assets.Scripts.Utils;
using Mapbox.Unity.MeshGeneration;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data
{
    public sealed class DataManager : Singleton<DataManager>
    {
        [Header("Common")]
        public float DataUpdateFrequency = (float)TimeSpan.FromMinutes(1).TotalSeconds;
        public enum DataProviderType { Test, Fmk }
        public DataProviderType DataProvider = DataProviderType.Test;

        [Header("Storages")]
        public ModelStorage ModelStorage;

        private IDataProvider _dataProvider;
        private List<MoveableObject> _moveableObjects;

        private DataManager() { }

        private void Start()
        {
            InitDataProvider();
            StartCoroutine(CreateObjects());            
        }        

        private void InitDataProvider()
        {
            switch (DataProvider)
            {
                case DataProviderType.Test:
                    _dataProvider = new TestDataProvider();
                    break;

                case DataProviderType.Fmk:
                    _dataProvider = new FmkDataProvider();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerator CreateObjects()
        {
            while (MapController.ReferenceTileRect == null)
                yield return new WaitForSeconds(1f); // HOTFIX Impossible to create objects before MapController didn't create the first map tile.

            var random = new System.Random();

            _moveableObjects = _dataProvider.LoadMoveableObjects();
            foreach (var moveObject in _moveableObjects)
            {
                moveObject.transform.SetParent(transform);

                GameObject model;
                if (moveObject is Vehicle)
                    model = Instantiate(ModelStorage.VehicleModels[random.Next(0, ModelStorage.VehicleModels.Count)]); 
                else if (moveObject is Employee)
                    model = Instantiate(ModelStorage.EmployeeModels[random.Next(0, ModelStorage.EmployeeModels.Count)]); 
                else
                    throw new InvalidCastException();
                model.transform.SetParent(moveObject.transform);
                
                moveObject.TargetWayPoint = Instantiate(ModelStorage.TargetWayPoint, transform);
                
                moveObject.PlaceTo(moveObject.Latitude, moveObject.Longitude, moveObject.Course);

                UiManager.Instance.AddObjectLabel(moveObject.gameObject);
            }
            Debug.Log("Objects loaded.");

            StartCoroutine(UpdateObjects());
        }

        private IEnumerator UpdateObjects()
        {
            yield return new WaitForSeconds(DataUpdateFrequency); // Pause before first update.

            while (true)
            {
                var locations = _dataProvider.UpdateMoveableObjectLocations();
                foreach (var curObject in _moveableObjects)
                {
                    foreach (var curPoint in locations)
                    {
                        if (curObject.Id == curPoint.Id)
                        {
                            curObject.MoveTo(curPoint.Latitude, curPoint.Longitude, curPoint.Speed, curPoint.Course);
                            goto NEXT_OBJECT;
                        }
                    }

                    NEXT_OBJECT: ;
                }
                Debug.Log("Locations updated.");

                yield return new WaitForSeconds(DataUpdateFrequency);
            }
        }
    }
}
