using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Map
{
    public class PatrolPoint : MonoBehaviour
    {
        [ReadOnly] public bool IsAvailable;

        private void Awake()
        {
            IsAvailable = true;
        }
    }
}
