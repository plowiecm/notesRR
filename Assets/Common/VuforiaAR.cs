using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vuforia;

namespace Assets.Common
{
    public class VuforiaAR : MonoBehaviour
    {
        public static TrackableBehaviour tb { get; private set; }

        void Update()
        {
            IEnumerable<TrackableBehaviour> tbs = TrackerManager.Instance.GetStateManager().GetActiveTrackableBehaviours();
            tb = tbs.FirstOrDefault();
        }
    }
}
