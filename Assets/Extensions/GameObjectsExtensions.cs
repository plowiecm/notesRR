using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Extensions
{
    public static class GameObjectsExtensions
    {
        public static List<GameObject> GetAllChildren(this GameObject gameObject)
        {
            var list = new List<GameObject>();
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                list.Add(gameObject.transform.GetChild(i).gameObject);
            }
            return list;
        }

        public static void DestroyAllChildren(this GameObject gameObject)
        {
            gameObject.GetAllChildren().ForEach(x => GameObject.Destroy(x));
        }

    }
}
