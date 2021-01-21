using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Handlers.Groups
{
    public class AddNewGroupHandler : MonoBehaviour
    {

        public GameObject Panel;
        // Start is called before the first frame update
        void Start()
        {
            Panel.SetActive(false);
        }

    

        public void AddNewGroupBtnClicked()
        {
            Panel.SetActive(true);
        }
    }
}

