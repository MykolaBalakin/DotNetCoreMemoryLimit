using System;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace WebApp
{
    [JsonConverter(typeof(SizeConverter))]
    public struct Size
    {
        private class Unit
        {
            public Unit(string name, int multiplier)
            {
                Name = name;
                Multiplier = multiplier;
            }

            public string Name { get; }
            public int Multiplier { get; }
        }

        private static readonly Regex Pattern = new Regex("(?<number>[0-9.]+) ?(?<unit>B|KB|MB|GB)?", RegexOptions.Compiled);

        private static readonly Unit[] Units = {
            new Unit("GB", 1024 * 1024 * 1024),
            new Unit("MB", 1024 * 1024),
            new Unit("KB", 1024),
            new Unit("B", 1),
        };

        public static Size Parse(string s)
        {
            var match = Pattern.Match(s);
            if (!match.Success)
            {
                throw new ArgumentOutOfRangeException(nameof(s));
            }

            var numberGroup = match.Groups["number"];
            var unitGroup = match.Groups["unit"];

            var number = int.Parse(numberGroup.Value);
            var multiplier = 1;

            if (unitGroup.Success)
            {
                var unit = Units.First(u => u.Name == unitGroup.Value);
                multiplier = unit.Multiplier;
            }

            var bytes = number * multiplier;
            return new Size(bytes);
        }

        public int Bytes { get; }

        public Size(int bytes)
        {
            Bytes = bytes;
        }

        public override string ToString()
        {
            var bytes = Bytes;
            var unit = Units.First(u => u.Multiplier < bytes);

            return $"{bytes / unit.Multiplier:0.#} {unit.Name}";
        }
    }
}