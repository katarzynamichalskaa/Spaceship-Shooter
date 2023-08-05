using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Transform[] Points;
    [SerializeField] float rotationSpeed = 200f;
    private float movementSpeed = 3f;
    private int pointsIndex;


    void Start()
    {
        transform.position = Points[pointsIndex].transform.position;
    }
    void Update()
    {
        MoveInLoop();
    }

    void MoveInLoop()
    {
        if (pointsIndex <= Points.Length - 1)
        {
            //rotation
            Vector3 targetDirection = Points[pointsIndex].transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            //position
            transform.position = Vector3.MoveTowards(transform.position, Points[pointsIndex].transform.position, movementSpeed * Time.deltaTime);

            if (transform.position == Points[pointsIndex].transform.position)
            {
                pointsIndex += 1;
            }

            if (pointsIndex == Points.Length)
            {
                pointsIndex = 0;
            }

        }
    }
}
