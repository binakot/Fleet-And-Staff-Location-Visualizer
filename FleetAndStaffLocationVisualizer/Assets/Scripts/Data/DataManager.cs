using Assets.Scripts.Data.Interfaces;
using Assets.Scripts.Data.Model.Objects;
using Assets.Scripts.Data.Providers;
using Assets.Scripts.Storages;
using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data
{
    public sealed class DataManager : Singleton<DataManager>
    {
        [Header("Common")]
        public double DataUpdateFrequency = TimeSpan.FromMinutes(1).TotalSeconds;
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
            CreateObjects();            
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

        private void CreateObjects()
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
               
                moveObject.PlaceTo(moveObject.Latitude, moveObject.Longitude, moveObject.Course);
            }
        }        
    }
}
