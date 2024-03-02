using System.Collections.Generic;
using UnityEngine;

namespace Alf.Utils
{

public static class MathUtils
{
    public static bool LineLineIntersection(out Vector3 intersection, Vector3 linePoint1,
            Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2){

        Vector3 lineVec3 = linePoint2 - linePoint1;
        Vector3 crossVec1and2 = Vector3.Cross(lineVec1, lineVec2);
        Vector3 crossVec3and2 = Vector3.Cross(lineVec3, lineVec2);

        float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);

        //is coplanar, and not parallel
        if( Mathf.Abs(planarFactor) < 0.0001f 
                && crossVec1and2.sqrMagnitude > 0.0001f)
        {
            float s = Vector3.Dot(crossVec3and2, crossVec1and2) 
                    / crossVec1and2.sqrMagnitude;
            intersection = linePoint1 + (lineVec1 * s);
            return true;
        }
        else
        {
            intersection = Vector3.zero;
            return false;
        }
    }

    /// <summary>
    /// Calculates the x-intercept of line defined by points p1 and p2.
    /// </summary>
    /// <param name="xIntercept"> X-intercept if it can be calculated. Otherwise, zero. </param>
    /// <param name="p1"> First point of line. </param>
    /// <param name="p2"> Second point of line. </param>
    /// <param name="yPoint"> The y-threshold where the x-intercept is located. </param>
    /// <returns> True if the x-intercept can be found. False if the line has no slope. </returns>
    public static bool XIntercept(out float xIntercept, Vector2 p1, Vector2 p2, float yPoint)
    {
        var denom = p1.y - p2.y;
        if (denom == 0)
        {
            xIntercept = 0;
            return false;
        }
        
        xIntercept = (yPoint - p1.y)*(p1.x - p2.x)/denom + p1.x;
        return true;
    }

    public static float LMap(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    public static List<Vector2> PerlinRandomLocations(Bounds area, float sampleSpacing, float density)
    {
        var ret = new List<Vector2>();

        var xSize = area.max.x - area.min.x;
        var ySize = area.max.y - area.min.y;

        var col = Mathf.Floor(Mathf.Abs(xSize) / sampleSpacing);
        var row = Mathf.Floor(Mathf.Abs(ySize) / sampleSpacing);

        var xStart = area.center.x / 2 - (col / 2 - 0.5f) * sampleSpacing;
        var yStart = area.center.y / 2 - (row / 2 - 0.5f) * sampleSpacing;

        for(float x = xStart; x < area.max.x; x += sampleSpacing)
        {
            for(float y = yStart; y < area.max.y; y += sampleSpacing)
            {
                if (Mathf.PerlinNoise(x, y) < density)
                    ret.Add(new Vector2(x, y));
            }
        }

        return ret;
    }

}

}
