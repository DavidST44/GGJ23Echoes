using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FollowObject : MonoBehaviour
{

    public Transform Follow;

    private Camera MainCamera;

    public List<Transform> Circles = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var screenPos = MainCamera.WorldToScreenPoint(Follow.position);

        transform.position = screenPos;
    }

    private void Update()
    {
        foreach(Transform a in Circles)
        {
            Image circle = a.GetComponent<Image>();
            PlayerController player = Follow.GetComponent<PlayerController>();
            Debug.Log((player.Firerate / player.FirerateMax) * 1000);
            circle.fillAmount = (player.Firerate / player.FirerateMax) * 1;
        }
    }
}
