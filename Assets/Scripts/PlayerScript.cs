using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerScript : MonoBehaviour
{
    public SteamVR_Action_Boolean winEffect = SteamVR_Input.GetBooleanAction("WinEffect");
    public SteamVR_Action_Vector2 locomotion = SteamVR_Input.GetVector2Action("Locomotion");
    bool winEffectActive = false;
    public float maxSpeed;
    public float moveSpeed = 0.0f;
    public float sensitivity;

    public Rigidbody playerRB;

    public GameObject leftHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        winEffectActive = winEffect.GetStateDown(SteamVR_Input_Sources.RightHand);

        if(winEffectActive)
        {
            GameManager.manager.WinEffect();
        }

    }

    private void FixedUpdate()
    {
        // Smooth locomotion movement forward.
        if (locomotion.axis.y > 0)
        {
            Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(0, 0, locomotion.axis.y));

            moveSpeed = locomotion.axis.y * sensitivity;

            moveSpeed = Mathf.Clamp(moveSpeed, 0, maxSpeed);

            transform.position += moveSpeed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
        }
    }
}
