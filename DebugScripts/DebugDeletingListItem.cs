using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDeletingListItem : MonoBehaviour
{
    private void Start()
    {
        //test1();
    }
    private void test1()
    {
        List<int> n = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            n.Add(i);
        }
        Debug.Log(n.Count);
        test(n);
        Debug.Log(n.Count);
    }
    private void test(List<int> a)
    {
        a.Remove(0);
    }
}
