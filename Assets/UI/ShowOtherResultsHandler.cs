using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.UI
{

    public enum Direction
    {
        Previous,
        Next
    }

    public class ShowOtherResultsHandler : MonoBehaviour
    {
        public Direction Direction;
        public GameObject DetailsView;

        public void ChangeDisplayResult()
        {
            var handler = DetailsView.GetComponent<UserDisplayHandler>().Handler;
            if (Direction == Direction.Next)
                handler.Index++;
            else
                handler.Index--;
        }

    }
}
