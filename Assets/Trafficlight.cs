using UnityEngine;

public enum TrafficlightColor
{
    Red,
    Yellow,
    Green
}

public class Trafficlight : MonoBehaviour
{
    private Material[] _materials;
    private MeshRenderer _meshRenderer;

    public TrafficlightColor Color { get; private set; }

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _materials = _meshRenderer.materials;
    }

    public void SetColor(TrafficlightColor color)
    {
        switch (color)
        {
            case TrafficlightColor.Red: ShowRed(); break;
            case TrafficlightColor.Yellow: ShowYellow(); break;
            case TrafficlightColor.Green: ShowGreen(); break;
        }

        Color = color;
    }

    private void ShowRed()
    {
        _meshRenderer.materials = new Material[]
        {
            _materials[0],
            _materials[1],
            _materials[0],
            _materials[0]
        };
    }

    private void ShowYellow()
    {
        _meshRenderer.materials = new Material[]
        {
            _materials[0],
            _materials[0],
            _materials[2],
            _materials[0]
        };
    }

    private void ShowGreen()
    {
        _meshRenderer.materials = new Material[]
        {
            _materials[0],
            _materials[0],
            _materials[0],
            _materials[3]
        };
    }
}
