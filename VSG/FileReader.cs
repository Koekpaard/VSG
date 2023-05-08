using VSG.Record;

namespace VSG
{
    internal class FileReader
    {
        private bool _ignoreAsteriskData { get; set; }

        /// <summary>Filereader.</summary>
        /// <param name="ignoreAsteriskData">Data containing asterisks is hidden from the results.</param>
        public FileReader(bool ignoreAsteriskData)
        {
            _ignoreAsteriskData = ignoreAsteriskData;
        }

        internal void ReadFile(string path)
        {
            string? line;
            try
            {
                StreamReader reader = new StreamReader(path);
                var thermoData = new List<ThermoData>();
                while ((line = reader.ReadLine()) != null)
                {
                    if (IsTableData(line))
                    {
                        ThermoData data = new ThermoData
                        {
                            Dd = int.Parse(ReadUntilNextSpace(line, 0)),    // These magic numbers are tricky of course.
                            Max = int.Parse(ReadUntilNextSpace(line, 5)),   // Better check at which char matching header starts
                            Min = int.Parse(ReadUntilNextSpace(line, 10))
                        };
                        thermoData.Add(data);
                    }
                }

                PrintSmallestMinMaxDiff(thermoData);
            }
            catch (Exception ex) when (ex is IOException or OutOfMemoryException)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void PrintSmallestMinMaxDiff(IEnumerable<ThermoData> thermoData)
        {
            var differences = thermoData.Select(x => new KeyValuePair<ThermoData, int>(x, x.Max - x.Min)).OrderBy(x => x.Value);
            var result = differences.First();
            Console.WriteLine($"Verschil is het kleinst op dag {result.Key.Dd} met een min van {result.Key.Min}, een max van {result.Key.Max} en een verschil van {result.Value}.");
        }

        private bool IsTableData(string line)
        {
            if (_ignoreAsteriskData && line.Contains('*'))
            {
                return false;
            }

            // If the line does not start with an int between spaces it's no table data
            return int.TryParse(string.Concat(line.TrimStart().TakeWhile(x => x != ' ')), out _);
        }

        /// <summary>
        /// Reads from given index. Ignores first spaces, takes data, stops when taking spaces for second time.
        /// </summary>
        /// <param name="line">The unedited line to check.</param>
        /// <param name="index">Start looking from this line.</param>
        /// <returns></returns>
        private string ReadUntilNextSpace(string line, int index)
        {
            string substring = string.Concat(line.Skip(index));
            substring = substring.TrimStart();
            string result = string.Concat(substring.TakeWhile(x => x != ' '));
            return result;
        }
    }
}
