using System.Collections.Generic;
using UnityEngine;

public interface IPathfinder 
{
  public IEnumerable<Vector2> GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges);

}
