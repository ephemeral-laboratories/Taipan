using System;
using System.Collections.Generic;
using System.Linq;

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

        public static readonly List<LorchaPosition> All =
        [
            new(0, 0),
            new(0, 1),
            new(0, 2),
            new(0, 3),
            new(0, 4),
            new(1, 0),
            new(1, 1),
            new(1, 2),
            new(1, 3),
            new(1, 4)
        ];

        public static readonly List<LorchaPosition> AllBackwards = Enumerable.Reverse(All).ToList();
    }
}