using UnityEngine;

public class Rope : MonoBehaviour
{
    public float gamma = 2.2f;
    public float drop = 3f;
    public LineRenderer lr;
    public int resolution = 5;
    float normalizer => (1f-1f/resolution);

    public Transform startPos;
    public Transform endPos;

    void Awake()
    {
        lr.positionCount = resolution;
    }

    void Update()
    {
        lr.positionCount = resolution;

        //Iterate through the list of points on rope.
        for (int i = 0; i < resolution; i++) {
            float factor = 1f / (float)resolution * (i);
            Vector3 indexPos = Vector3.Lerp(startPos.position, endPos.position, factor / normalizer);

            float height = GetHeightOffset(i) - drop;

            indexPos -= Vector3.up * height;

            lr.SetPosition(i, indexPos);
        }
    }

    //funny math i dont understand but somehow i made it haha
    public float GetHeightOffset(int index) {
        float factor = 1f / (float)resolution * (index);
        Vector3 indexPos = Vector3.Lerp(startPos.position, endPos.position, factor / normalizer);
        float slide = Mathf.Min(Vector3.Distance(startPos.position, indexPos), Vector3.Distance(endPos.position, indexPos));
        float dividend = Vector3.Distance(Vector3.Lerp(startPos.position, endPos.position, 0.5f), startPos.position);
        return Mathf.Pow(1f-slide/dividend, gamma) * drop;
    }
}
