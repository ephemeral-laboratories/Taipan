using System;
using System.Collections.Generic;

namespace Source.Model
{
    // Logical position of a ship by Row/Column.
    // We don't care what coordinate space your view is working in.
    public class LorchaPosition
    {
        public int Column { get; }

        public int Row { get; }

        private LorchaPosition(int row, int column)
        {
            if (column < 0 || column > 4 || row < 0 || row > 1)
            {
                throw new ArgumentException($"Bogus ship position: {column}, {row}");
            }

            Column = column;
            Row = row;
        }

        public static List<LorchaPosition> All => new List<LorchaPosition>
        {
            new LorchaPosition(0, 0),
            new LorchaPosition(0, 1),
            new LorchaPosition(0, 2),
            new LorchaPosition(0, 3),
            new LorchaPosition(0, 4),
            new LorchaPosition(1, 0),
            new LorchaPosition(1, 1),
            new LorchaPosition(1, 2),
            new LorchaPosition(1, 3),
            new LorchaPosition(1, 4)
        };

        public static IEnumerable<LorchaPosition> AllBackwards => All.FastReverse();
    }

    internal static class ListExtensions
    {
        public static IEnumerable<T> FastReverse<T>(this IList<T> items)
        {
            for (var i = items.Count - 1; i >= 0; i--)
            {
                yield return items[i];
            }
        }
    }
}