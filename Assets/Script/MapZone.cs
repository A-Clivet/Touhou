using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MapZone : MonoBehaviour
{
    public static MapZone SharedInstance;

    [SerializeField] PolygonCollider2D zone1;
    [SerializeField] PolygonCollider2D zone2;
    [SerializeField] PolygonCollider2D zone3;

    float Top;
    float Down;
    float Left;
    float Right;
    List<float> borders = new();

    void Awake()
    {
        SharedInstance = this;
    }

    public List<float> Zone1()
    {
        if (borders.Count > 0) { borders.Clear(); }

        Top   = zone1.points.ElementAt(0).y;                                                                                                                                                                                                                                                                                                                                                   ;
        Down  = zone1.points.ElementAt(2).y;                                                                                                                                                                                                                                                                                                                                                   ;
        Left  = zone1.points.ElementAt(2).x;                                                                                                                                                                                                                                                                                                                                                   ;
        Right = zone1.points.ElementAt(0).x;

        borders.Add(Top);
        borders.Add(Down);
        borders.Add(Left);
        borders.Add(Right);

        return borders.ToList();
    }

    public List<float> Zone2()
    {
        if(borders.Count > 0) { borders.Clear(); }

        Top   = zone2.points.ElementAt(0).y;                                                                                                                                                                                                                                                                                                                                                   ;
        Down  = zone2.points.ElementAt(2).y;                                                                                                                                                                                                                                                                                                                                                   ;
        Left  = zone2.points.ElementAt(2).x;                                                                                                                                                                                                                                                                                                                                                   ;
        Right = zone2.points.ElementAt(0).x;

        borders.Add(Top);
        borders.Add(Down);
        borders.Add(Left);
        borders.Add(Right);

        return borders.ToList();
    }

    public List<float> Zone3()
    {
        if (borders.Count > 0) { borders.Clear(); }

        Top   = zone3.points.ElementAt(0).y;                                                                                                                                                                                                                                                                                                                                                   ;
        Down  = zone3.points.ElementAt(2).y;                                                                                                                                                                                                                                                                                                                                                   ;
        Left  = zone3.points.ElementAt(2).x;                                                                                                                                                                                                                                                                                                                                                   ;
        Right = zone3.points.ElementAt(0).x;

        borders.Add(Top);
        borders.Add(Down);
        borders.Add(Left);
        borders.Add(Right);

        return borders.ToList();
    }
    
}
