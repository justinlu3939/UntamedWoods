using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed;

        private Animator animator;
        private int direction; // 0 = down, 1 = up, 2 = right, 3 = left

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                dir.x = -1;
                direction = 3;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                dir.x = 1;
                direction = 2;
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                dir.y = 1;
                direction = 1;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                dir.y = -1;
                direction = 0;
            }

            dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);
            animator.SetInteger("Direction", direction);

            GetComponent<Rigidbody2D>().linearVelocity = speed * dir;

            // Trigger the attack animation on left mouse button click
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
        }

        private void Attack()
        {
            animator.SetTrigger("Attack");
        }
    }
}
