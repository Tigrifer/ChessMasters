using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Сводное описание для Field
/// </summary>
public class Field
{
  public Field(sbyte _x, sbyte _y)
  {
    this.x = _x;
    this.y = _y;
  }

  public sbyte x;
  public sbyte y;
}