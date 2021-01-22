using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vuforia;

namespace Assets.Common
{
    public class VuforiaAR : MonoBehaviour
    {
        public static VuMarkTarget tb { get; private set; }

        void Update()
        {
            IEnumerable<VuMarkTarget> tbs = TrackerManager.Instance.GetStateManager().GetVuMarkManager().GetActiveVuMarks();
            tb = tbs.FirstOrDefault();
        }
    }
}
