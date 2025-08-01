using System.Collections.Generic;
using UnityEngine;

public class Herd : MonoBehaviour
{
    public List<GameObject> herdMembers;
    public GameObject HerdLeader;

    private void Start()
    {
        herdMembers = new List<GameObject>();
        HerdLeader = this.gameObject;
    }

}
