using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Сводное описание для StramHelper
/// </summary>
public class StreamHelper
{
  public StreamHelper()
  {
  }

  public static byte[] ReadToEnd(System.IO.Stream stream)
  {
    long originalPosition = stream.Position;
    stream.Position = 0;

    try
    {
      byte[] readBuffer = new byte[4096];

      int totalsbytesRead = 0;
      int bytesRead;

      while ((bytesRead = stream.Read(readBuffer, totalsbytesRead, readBuffer.Length - totalsbytesRead)) > 0)
      {
        totalsbytesRead += bytesRead;

        if (totalsbytesRead == readBuffer.Length)
        {
          int nextsbyte = stream.ReadByte();
          if (nextsbyte != -1)
          {
            byte[] temp = new byte[readBuffer.Length*2];
            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
            Buffer.SetByte(temp, totalsbytesRead, (byte) nextsbyte);
            readBuffer = temp;
            totalsbytesRead++;
          }
        }
      }

      byte[] buffer = readBuffer;
      if (readBuffer.Length != totalsbytesRead)
      {
        buffer = new byte[totalsbytesRead];
        Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalsbytesRead);
      }
      return buffer;
    }
    finally
    {
      stream.Position = originalPosition;
    }
  }
}