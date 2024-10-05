using UnityEngine;

public class Line 
{
    private readonly Vector3 _start;
    private readonly Vector3 _end;
    
    private readonly LineRenderer _lineRenderer;

    public Line(Vector3 start, Vector3 end)
    {
        _start = start;
        _end = end;
        
        var lineObject = new GameObject("Line");
        
        _lineRenderer = lineObject.AddComponent<LineRenderer>();
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startColor = Color.gray;
        _lineRenderer.endColor = Color.gray;

        // Set the width of the line
        _lineRenderer.widthMultiplier = 0.1f;
        //_lineRenderer.startWidth = 0.1f;
        //_lineRenderer.endWidth = 0.1f;

        // Set the number of positions (points) to draw the line
        _lineRenderer.positionCount = 2;
        
    }

    public void Render()
    {
        _lineRenderer.SetPosition(0, _start);
        _lineRenderer.SetPosition(1, _end);
    }
}
