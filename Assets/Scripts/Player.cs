using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void ActionDelegate(Rigidbody2D param1);

    private Dictionary<KeyCode, ActionDelegate> keyCodeToActions;
    // Start is called before the first frame update
    void Start()
    {
         keyCodeToActions = new Dictionary<KeyCode, ActionDelegate>()
        {
            {
                KeyCode.Space, (player) =>
                {

                    player.AddForce(Vector3.up, ForceMode2D.Impulse );
                }
            },
            {
                KeyCode.W, (player) =>
                {

                    player.AddForce(Vector3.forward, ForceMode2D.Impulse );
                }
            },
            {
                KeyCode.A, (player) =>
                {

                    player.AddForce(Vector3.left, ForceMode2D.Impulse );
                }
            },
            {
                KeyCode.D, (player) =>
                {

                    player.AddForce(Vector3.right, ForceMode2D.Impulse );
                }
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        var player = GetComponent<Rigidbody2D>();
        foreach (var keyCodeToAction in keyCodeToActions)
        {
            if (Input.GetKeyDown(keyCodeToAction.Key))
            {
                keyCodeToAction.Value(player);
            }
        }
    }
}
