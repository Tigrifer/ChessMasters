using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Сводное описание для FigureState
/// </summary>
[Serializable]
public class FigureState
{
    internal FigureState()
    {

    }
    public Field field;
    public FigureTypes type;
    public Color color;
    public short moveCount;
}