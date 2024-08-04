using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCurve : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints;
    [SerializeField]

    private Vector2 gizmosPosition;

    private void OnDrawGizmos()
    {
        float enemyCurveY = transform.position.y;

        for (float t = 0; t <= 1; t += 0.05f)
        {
            Vector3[] adjustedPos = new Vector3[controlPoints.Length];
            for (int i = 0; i < controlPoints.Length; i++)
            {
                adjustedPos[i] = controlPoints[i].position;
                adjustedPos[i].y += enemyCurveY;
            }

            gizmosPosition = Mathf.Pow(1 - t, 7) * adjustedPos[0] +
                             7 * Mathf.Pow(1 - t, 6) * t * adjustedPos[1] +
                             21 * Mathf.Pow(1 - t, 5) * Mathf.Pow(t, 2) * adjustedPos[2] +
                             35 * Mathf.Pow(1 - t, 4) * Mathf.Pow(t, 3) * adjustedPos[3] +
                             35 * Mathf.Pow(1 - t, 3) * Mathf.Pow(t, 4) * adjustedPos[4] +
                             21 * Mathf.Pow(1 - t, 2) * Mathf.Pow(t, 5) * adjustedPos[5] +
                             7 * (1 - t) * Mathf.Pow(t, 6) * adjustedPos[6] +
                             Mathf.Pow(t, 7) * adjustedPos[7];

            Gizmos.DrawSphere(gizmosPosition, 0.25f);
        }

        for (int i = 0; i < controlPoints.Length - 1; i++)
        {
            Gizmos.DrawLine(new Vector2(controlPoints[i].position.x, controlPoints[i].position.y + enemyCurveY),
                            new Vector2(controlPoints[i + 1].position.x, controlPoints[i + 1].position.y + enemyCurveY));
        }
    }
}
