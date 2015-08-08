using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;

namespace Slowotok
{
    /// <summary>
    /// Zapisuje rozwiązania do pliku.
    /// </summary>
    public class SolutionsWriter
    {
        /// <summary>
        /// Zapisuje rozwiązania pod wskazaną ścieżką w formacie JSON.
        /// </summary>
        /// <param name="solutions">rozwiązania</param>
        /// <param name="filePathName">ścieżka do zapisu</param>
        public void SaveToJson(IEnumerable<Solution> solutions, string filePathName = "solutions.txt")
        {
            var serializer = new JavaScriptSerializer();

            // przy korzystaniu z wielu słowników mogą wystąpić powtórzenia, których warto się pozbyć
            var distinctSolutions = solutions
                .GroupBy(solution => solution.Word.Value)
                .Select(grouping => grouping.First())
                .OrderByDescending(s => s.Word.Value.Length);
            var serializedSolutions = serializer.Serialize(distinctSolutions);
            using (var sw = File.CreateText(filePathName))
            {
                sw.Write(serializedSolutions);
            }
        }
    }
}
