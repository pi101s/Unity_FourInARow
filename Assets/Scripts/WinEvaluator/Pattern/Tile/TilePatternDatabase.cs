using UnityEngine;

namespace FIAR
{
    public class TilePatternDatabase
    {
        private const string PATTERNS_FOLDER = "Patterns";
        private static Object[] _patterns = null;

        public static PatternShape GetShape(in string name)
        {
            LoadPatterns();
            return FindPatternByName(name);
        }

        private static PatternShape FindPatternByName(in string name)
        {
            foreach (Object pattern in _patterns)
                if (pattern.name == name)
                    return (PatternShape)pattern;
            return null;
        }

        private static void LoadPatterns()
        {
            _patterns ??= Resources.LoadAll(PATTERNS_FOLDER, typeof(PatternShape));
        }
    }
}
