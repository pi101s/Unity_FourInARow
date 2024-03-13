namespace FIAR
{
    public readonly struct Pattern
    {
        public readonly BoardCoordinate[] coordinates;
        public readonly int width;
        public readonly int height;

        public Pattern(in BoardCoordinate[] coordinates)
        {
            this.coordinates = coordinates;
            width = CalculateWidth(coordinates);
            height = CalculateHeight(coordinates);
        }

        private static int CalculateWidth(in BoardCoordinate[] coordinates)
        {
            int minX = int.MaxValue;
            int maxX = int.MinValue;
            foreach (BoardCoordinate coordinate in coordinates)
            {
                if (coordinate.column < minX)
                    minX = coordinate.column;
                if (coordinate.column > maxX)
                    maxX = coordinate.column;
            }
            return maxX - minX + 1;
        }

        private static int CalculateHeight(in BoardCoordinate[] coordinates)
        {
            int minY = int.MaxValue;
            int maxY = int.MinValue;
            foreach (BoardCoordinate coordinate in coordinates)
            {
                if (coordinate.row < minY)
                    minY = coordinate.row;
                if (coordinate.row > maxY)
                    maxY = coordinate.row;
            }
            return maxY - minY + 1;
        }
    }
}
