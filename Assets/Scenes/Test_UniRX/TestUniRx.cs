using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class TestUniRx : MonoBehaviour
    {
        [SerializeField]
        private BasicInfo basicInfo;

        [SerializeField]
        private Text _txtHealth;

        // Consider this is fake player model data.
        private FakeViewModel _viewModel;

        // Start is called before the first frame update
        void Start()
        {
            _viewModel = new FakeViewModel();

            _viewModel.HealthValue
                .Subscribe(health => { _txtHealth.text = health.ToString(); })
                .AddTo(gameObject);
            _viewModel.HealthValue.SetValueAndForceNotify(basicInfo.Health);

            _viewModel.Sacrifice.Subscribe(decreaseHp => { _viewModel.HealthValue.Value += decreaseHp; });

            _viewModel.CheckingAlive.Subscribe(isAlive => { Debug.Log($"IsAlive: {isAlive}"); });
        }

        //[ContextMenu("EndTurn")]
        //public void EndTurn()
        //{
            
        //}

        [ContextMenu("InstantKill")]
        public void InstantKill()
        {
            _viewModel.Sacrifice.Execute(-_viewModel.HealthValue.Value);
            _viewModel.CheckingAlive.OnNext(false);
        }

        [ContextMenu("Sacrifice")]
        public void Sacrifice()
        {
            _viewModel.Sacrifice.Execute(-10);
        }

        [ContextMenu("ChangeHealth")]
        public void ChangeHealth()
        {
            _viewModel.HealthValue.Value += 10;
            _viewModel.CheckingAlive.OnNext(true);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            _viewModel?.Dispose();
        }
    }
}
