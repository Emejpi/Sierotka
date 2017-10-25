using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for

        public float moveSpeed;
        public bool monkay;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }

        void Update()
        {
            //if (enabled)
            {
                if (target != null)
                    agent.SetDestination(target.position);

                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    character.Move(agent.desiredVelocity * moveSpeed, false, false);

                    if (monkay)
                    {
                        if (agent.remainingDistance > agent.stoppingDistance + 3)
                            agent.autoBraking = false;
                        else
                            agent.autoBraking = true;
                    }
                }
                else
                {
                    character.Move(Vector3.zero, false, false);

                    if (monkay)
                        agent.autoBraking = true;
                    //GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                }
            }
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
