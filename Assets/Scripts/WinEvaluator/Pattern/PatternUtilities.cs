using System.Collections.Generic;

namespace FIAR
{
    public static class PatternUtilities
    {
        public static Pattern[] ConvertShapesIntoPatterns(in PatternShape[] shapes)
        {
            Pattern[] patterns = new Pattern[shapes.Length];
            for (int i = 0; i < patterns.Length; i++)
                patterns[i] = ConvertShapeIntoPattern(shapes[i]);
            return patterns;
        }

        public static Pattern ConvertShapeIntoPattern(in PatternShape shape)
        {
            return new Pattern(shape.coodinates);
        }

    }
}
