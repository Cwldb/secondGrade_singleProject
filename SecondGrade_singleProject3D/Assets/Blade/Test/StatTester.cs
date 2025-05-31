using _01_Scripts.Entities;
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
                statCompo.AddModifier(targetStat, this, modifyValue);
            }
            if(Keyboard.current.pKey.wasPressedThisFrame)
            {
                statCompo.RemoveModifier(targetStat, this);
            }
        }
    }
}