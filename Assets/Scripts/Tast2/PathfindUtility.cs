using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct Line
{
    public Line(float a, float b, Vector2 startPoint, Vector2 endPoint)
    {
        A = a;
        B = b;
        StartPoint = startPoint;
        EndPoint = endPoint;
    }

    public float A { get; }
    public float B { get; }

    public Vector2 StartPoint { get; }
    public Vector2 EndPoint { get; }

    public float GetY(float x)
    {
        return A * x + B;
    }

    public float DeterminationCoeff(IEnumerable<Vector2> points)
    {
        var R = 0f;
        float sum1 = 0;
        float sum2 = 0;
        float avgY = 0;
        int quantity = 0;

        foreach (var p in points)
        {
            avgY += p.y;
            quantity++;
        }

        avgY /= quantity;

        foreach (var p in points)
        {
            sum1 += Mathf.Pow((GetY(p.x) - p.y) - p.y, 2);
            sum2 += Mathf.Pow((GetY(p.x) - p.y) - avgY, 2);
        }

        R = 1 - (sum1 / sum2);

        return R;
    }
}

public class PathfindUtility : IPathfinder
{
    public IEnumerable<Vector2> GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges)
    {
        var avgPoints = GetAVGPointsFromEdges(edges);
        var list = new List<Vector2>() { A, C };
        list.AddRange(avgPoints);
        var lines = GetApproxLinesFromPoints(list, A, C);

        Debug.Log(lines.Count);

        int it = 0;
        foreach (var l in lines)
        {
            Debug.Log($"{it} {l.StartPoint} {l.EndPoint}");
            it++;
        }

        return null;
    }

    private List<Vector2> GetAVGPointsFromEdges(IEnumerable<Edge> edges)
    {
        var list = new List<Vector2>();

        foreach (var e in edges)
        {
            var avgX = (e.End.x + e.Start.x) / 2;
            var avgY = (e.End.y + e.Start.y) / 2;
            list.Add(new Vector2(avgX, avgY));
        }

        return list;
    }

    private List<Line> GetApproxLinesFromPoints(IEnumerable<Vector2> points, Vector2 start, Vector2 end)
    {
        var pointsList = points.ToList();
        List<Line> list = new List<Line>();
        if (pointsList.Count < 2)
            return list;


        for (int i = pointsList.Count; i > 1; i--)
        {
            var poses = new int[i];
            for (int j = -1; j < poses.Length - 1; j++)
                poses[j + 1] = j + 1;
            var l = GetApproxLinesFromByQuantity(ref poses, ref pointsList, i, start, end, out var usedPoints);
            if (l.Count > 0)
            {
                list.AddRange(l);
                foreach (var u in usedPoints)
                {
                    pointsList.Remove(u);
                }

                return GetApproxLinesFromPoints(pointsList, start, end);
            }
        }

        return list;
    }

    private List<Line> GetApproxLinesFromByQuantity(ref int[] posesArray, ref List<Vector2> pointsArray,
        int quantityPoints,
        Vector2 start,
        Vector2 end,
        out List<Vector2> usedPoints, float R = 0.04f)
    {
        usedPoints = new List<Vector2>();
        var list = new List<Line>();
        if (quantityPoints > pointsArray.Count)
            return list;

        do
        {
            var pickedPoints = new List<Vector2>();
            //get local points from array
            for (int i = 0; i < quantityPoints; i++)
                pickedPoints.Add(pointsArray[posesArray[i]]);

            if (pickedPoints.Contains(start) && pickedPoints.Contains(end))
                continue;

            var r = GetApproxLine(pickedPoints);

            var determCoeff = r.DeterminationCoeff(pointsArray);
            if (determCoeff < R)
                continue;
            list.Add(r);
            usedPoints.AddRange(pickedPoints.ToList());
        } while (AddIterator(ref posesArray, quantityPoints));


        return list;
    }

    private bool AddIterator(ref int[] posesArray, int arrayLenght)
    {
        //from end to start
        for (int i = posesArray.Length - 1; i > 0; i--)
        {
            if (posesArray[i] < arrayLenght - 1)
            {
                if(i + (arrayLenght - i) >= posesArray.Length || posesArray[i + (arrayLenght - i)] >= arrayLenght)
                    continue;
                posesArray[i]++;
                for (int o = i + 1; o < posesArray.Length; o++)
                {
                    posesArray[o] = o;
                }
                return true;
            }
        }

        return false;
    }

    private Line GetApproxLine(IEnumerable<Vector2> points)
    {
        float a = default;
        float b = default;
        Vector2 first = default;
        Vector2 second = default;

        float sumX = 0;
        float sumY = 0;
        float sumX2 = 0;
        float sumXY = 0;
        int quantity = 0;

        foreach (var p in points)
        {
            sumX += p.x;
            sumY += p.y;
            sumX2 += p.x * p.x;
            sumXY += p.x * p.y;
            quantity++;
        }

        first = GetMinVector(points);
        second = GetMaxVector(points);


        a = (quantity * sumXY - (sumX * sumY)) / (quantity * sumX2 - sumX * sumX);
        b = (sumY - a * sumX) / quantity;

        return new Line(a, b, first, second);
    }

    private Vector2 GetMinVector(IEnumerable<Vector2> vectors)
    {
        var vec = new Vector2(float.MaxValue, float.MaxValue);
        foreach (var v in vectors)
        {
            if (v.x < vec.x && v.y < vec.y)
                vec = v;
        }

        return vec;
    }


    private Vector2 GetMaxVector(IEnumerable<Vector2> vectors)
    {
        var vec = new Vector2(float.MinValue, float.MinValue);
        foreach (var v in vectors)
        {
            if (v.x > vec.x && v.y > vec.y)
                vec = v;
        }

        return vec;
    }
}