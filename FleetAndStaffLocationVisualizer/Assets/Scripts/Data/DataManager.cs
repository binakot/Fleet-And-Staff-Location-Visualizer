using Assets.Scripts.Data.Interfaces;
using Assets.Scripts.Data.Model.Objects;
using Assets.Scripts.Data.Providers;
using Assets.Scripts.Storages;
using Assets.Scripts.UI;
using Assets.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data
{
    public sealed class DataManager : Singleton<DataManager>
    {
        [Header("Common")]
        public float FirstDataUpdateDelay = 5f;
        public float DataUpdateFrequency = (float) TimeSpan.FromMinutes(1).TotalSeconds;
        public enum DataProviderType { Test }
        public DataProviderType DataProvider = DataProviderType.Test;

        [Header("Storages")]
        public ModelStorage ModelStorage;

        private IDataProvider _dataProvider;
        private List<MoveableObject> _moveableObjects;

        private DataManager()
        {
        }

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

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerator CreateObjects()
        {
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

            yield return null;
        }

        private IEnumerator UpdateObjects()
        {
            yield return new WaitForSeconds(FirstDataUpdateDelay); // Pause before first update.

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

                NEXT_OBJECT:;
                }
                Debug.Log("Locations updated.");

                yield return new WaitForSeconds(DataUpdateFrequency); // Period between every update.
            }
        }
    }
}