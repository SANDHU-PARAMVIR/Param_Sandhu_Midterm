using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Param_Sandhu_Midterm
{

    public class CovidDataReader
    {
        private String filepath;
        private List<CoronaCaseRow> cases;
        private List<String> timeline, countries;

        public List<CoronaCaseRow> Cases { get { return cases; } }
        public List<String> Countries { get { return countries; } }

        public CovidDataReader(string filepath)
        {
            this.filepath = filepath;
            cases = new List<CoronaCaseRow>();
            timeline = new List<string>();
            countries = new List<string>();
        }

        async public Task load()
        {
            var lineCounter = 0;

            using (var reader = new StreamReader(filepath))
            {
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var rowItems = line.Split(',');

                    countries.Add(rowItems[1]);

                    if (lineCounter == 0)
                    {
                        for (int i = 2; i < rowItems.Length; i++)
                            timeline.Add(rowItems[i]);

                        lineCounter++;
                        continue;
                    }

                    for (int i = 2; i < rowItems.Length; i++)
                    {
                        if (rowItems[i] == "0")
                            continue;

                        cases.Add(new CoronaCaseRow{ColCountry = rowItems[1], ColState = rowItems[0],ColNumCases = Int32.Parse(rowItems[i]), ColDate = timeline[i - 2]});
                    }

                    lineCounter++;
                }
            }
        }

        public List<CoronaCaseRow> fetchByCountry(String country)
        {
            List<CoronaCaseRow> casesByCountry = new List<CoronaCaseRow>();

            for (int i = 0; i < cases.Count; i++)
            {
                if (cases[i].ColCountry == country)
                    casesByCountry.Add(cases[i]);
            }

            return casesByCountry;
        }
    }
}
