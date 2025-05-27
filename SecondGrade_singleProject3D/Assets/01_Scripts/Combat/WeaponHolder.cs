using _01_Scripts.Entities;
using UnityEngine;

namespace _01_Scripts.Combat
{
    public class WeaponHolder : MonoBehaviour, IEntityComponent
    {
        private Entity _entity;
        [SerializeField] private Weapon[] weapons;

        public void Initialize(Entity entity)
        {
            _entity = entity;
        }

        public void DropWeapons()
        {
            foreach (Weapon weapon in weapons)
            {
                weapon.Drop();
            }
        }
    }
}
