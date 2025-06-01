using _01_Scripts.Entities;
using KJYLib.StatSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Blade.Test
{
    public class StatTester : MonoBehaviour
    {
        [SerializeField] private EntityStat statCompo;
        [SerializeField] private StatSO targetStat;
        [SerializeField] private float modifyValue;

        private void Update()
        {
            if (Keyboard.current.oKey.wasPressedThisFrame)
            {
                StatSO stat = statCompo.GetStat(targetStat);
                if (stat != null)
                {
                    stat.BaseValue = modifyValue;
                    Debug.Log($"Stat 이름: {stat.statName}, 현재 값: {stat.BaseValue}");
                }
            }
            if(Keyboard.current.pKey.wasPressedThisFrame)
            {
                //statCompo.RemoveModifier(targetStat, this);
            }
        }
    }
}