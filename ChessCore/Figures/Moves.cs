using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessCore.Figures
{
  [Serializable]
  public class Moves
  {
    public Field MoveFrom;
    public Field MoveTo;
    public Moves(Field from, Field to)
    {
      MoveFrom = from;
      MoveTo = to;
    }

    public Moves(sbyte from_x,sbyte from_y, sbyte to_x, sbyte to_y)
    {
      MoveFrom = new Field(from_x, from_y);
      MoveTo = new Field(to_x, to_y);
    }
  }
}
