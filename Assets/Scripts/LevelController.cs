using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelController : MonoBehaviour
{
    public GameObject punch;
    public GameObject saw;
    public GameObject plane;
    public float speed = 1f;

    
    
    void Start()
    {
        punch.transform.DOMoveX(-10, 1).SetLoops(-1, LoopType.Yoyo);
        saw.transform.DORotate(new Vector3(0, -90, 0), 1).SetLoops(-1, LoopType.Yoyo);
    }

    
    void Update()
    {
		plane.transform.Rotate(speed * Time.fixedDeltaTime,0,0,Space.Self);
	}

	

   
}
