using _01_Scripts.Entities;
using _01_Scripts.Players;
using KJYLib.Dependencies;
using UnityEngine;

namespace Blade.Managers
{
    [DefaultExecutionOrder(-1)]
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField, Inject] private Player player;
        [SerializeField] private EntityFinderSO playerFinder;
        
        private void Awake()
        {
            playerFinder.SetTarget(player);
        }
    }
}
