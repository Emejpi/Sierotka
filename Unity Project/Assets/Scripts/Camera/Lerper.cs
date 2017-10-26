using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerper : MonoBehaviour {

	public Vector3 LerpVector3(Vector3 vec1, Vector3 vec2, float speed)
    {
        return new Vector3(
            Mathf.Lerp(vec1.x, vec2.x, speed * Time.deltaTime),
            Mathf.Lerp(vec1.y, vec2.y, speed * Time.deltaTime),
            Mathf.Lerp(vec1.z, vec2.z, speed * Time.deltaTime)
            );
    }

    public Vector3 VecXVec(Vector3 vec1, Vector3 vec2)
    {
        return new Vector3(vec1.x * vec2.x, vec1.y * vec2.y, vec1.z * vec2.z);
    }
}
