using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Game : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent navMeshAgent;
    [SerializeField]
    Animator animator;

    [SerializeField]
    Camera cam;

    [SerializeField]
    GameObject lifeOrbPrefab;

    [SerializeField]
    Transform spawnParent;
    Transform[] spawnPoints;

    [SerializeField]
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(spawnParent.childCount);
        spawnPoints = new Transform[spawnParent.childCount];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = spawnParent.GetChild(i);
        }

        Instantiate(lifeOrbPrefab, spawnPoints[Random.Range(0, spawnParent.childCount - 1)]);
    }

    private void OnEnable()
    {
        LifeOrb.OnOrbCollected += AfterLifeOrbsCollected;
    }

    private void OnDisable()
    {
        LifeOrb.OnOrbCollected -= AfterLifeOrbsCollected;
    }


    void AfterLifeOrbsCollected()
    {
        animator.Play("Capture");
        audioSource.Play();
        Instantiate(lifeOrbPrefab, spawnPoints[Random.Range(0, spawnParent.childCount - 1)]);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                navMeshAgent.SetDestination(hit.point);
                animator.Play("Running");


            }
        }

        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    animator.Play("Idle");
                }
            }
        }


    }



}
