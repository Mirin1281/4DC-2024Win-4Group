using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirin
{
    public class GamaManager : SingletonMonoBehaviour<GamaManager>
    {
        public bool IsFirstWatchTutorial { get; set; } = true;
        public int Score { get; set; }
    }
}