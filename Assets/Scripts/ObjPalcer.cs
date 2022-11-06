using System.Collections.Generic;
using UnityEngine;

public static class ObjPalcer
{
    private static List<Transform> objs;

    public static void PlaceObj(Transform obj, Vector3 pos, int id)
    {
        var newObj = Object.Instantiate(obj, pos, Quaternion.identity);
        newObj.GetComponent<Obj>().id = id;

        objs ??= new List<Transform>();
        objs.Add(newObj);
    }

    public static void RemoveObj(Vector3 pos, int id)
    {
        for (var i = 0; i < objs.Count; i++)
        {
            var item = objs[i];
            if (item.position == pos && item.GetComponent<Obj>().id == id)
            {
                Object.Destroy(item.gameObject);
                objs.RemoveAt(i);
            }
        }
    }
}