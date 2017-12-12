using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.RPGPlugin
{
    public class RPGPuzzle
    {
        public static Random rand = new Random();
        public int _puzzleId;
        public IList<bool> b = new List<bool>();

        public RPGPuzzle(int depth)
        {
            if (depth == 0)
            {
                _puzzleId = 0;
                b.Add(false);
                b.Add(false);
            }
            else
            {
                _puzzleId = rand.Next(1, 3);
                b.Add(false);
                b.Add(false);
                b.Add(false);
                b.Add(false);
            }
        }

        public string Toggle(int id)
        {
            if (id < b.Count && id >= 0)
            {
                bool wasSolved = IsSolved();
                b[id] = !b[id];
                if (!IsSolved())
                {
                    var result = "Klick! Das Muster verändert sich.<br/>";
                    if (wasSolved)
                        result += "Die Felsen brechen aus der magischen Verankerung und donnern zurück in den Abgrund...<br/>";

                    return result;
                }
                else
                {
                    var result = "Klick! Das Muster verändert sich.<br/>";
                    if (!wasSolved)
                        result += "Aus der Tiefe heben sich Kiesel und Felsen empor und bilden einen erstaunlich stabilen Weg über den Abgrund...<br/>";

                    return result;
                }
            }
            else return "Dieser Knopf existiert nicht!<br/>";
        }

        public bool IsSolved()
        {
            switch (_puzzleId)
            {
                default:
                case 0:
                    return b[0] && b[1];
                case 1:
                    return (b[0] || b[1]) ^ (b[2] && b[3]);
                case 2:
                    return b[0] && b[1] && !b[2] && b[3];

            }
        }

        public string PuzzleDisplay()
        {
            string result = "";
            switch (_puzzleId)
            {
                default:
                case 0:
                    result = "" +
                   "##################<br/>" +
                   "#................#<br/>" +
                   "#..A.............#<br/>" +
                   "#..a.............#<br/>" +
                   "#..a.............#<br/>" +
                   "#..CCyyyyyyyyyZ..#<br/>" +
                   "#..x.............#<br/>" +
                   "#..xxB...........#<br/>" +
                   "##################<br/>";


                    result = result.Replace("a", string.Format("<font color={0}>%</font>", b[0] ? "00ff00" : "ff0000"));
                    result = result.Replace("A", string.Format("<font color={0}>A</font>", b[0] ? "00ff00" : "ff0000"));
                    result = result.Replace("B", string.Format("<font color={0}>B</font>", b[1] ? "00ff00" : "ff0000"));
                    result = result.Replace("x", string.Format("<font color={0}>%</font>", b[1] ? "00ff00" : "ff0000"));
                    result = result.Replace("CC", string.Format("<font color={0}>&amp;&#8594;</font>", b[0] && b[1] ? "00ff00" : "ff0000"));
                    result = result.Replace("y", string.Format("<font color={0}>%</font>", b[0] && b[1] ? "00ff00" : "ff0000"));
                    result = result.Replace("Z", string.Format("<font color={0}>Z</font>", b[0] && b[1] ? "00ff00" : "ff0000"));


                    return result;

                case 1:
                    result = "" +
                    "##################<br/>" +
                    "#................#<br/>" +
                    "#.AaaaaaEEe......#<br/>" +
                    "#.......x.e......#<br/>" +
                    "#.Bxxxxxx.e......#<br/>" +
                    "#.........e......#<br/>" +
                    "#.CyyyyFFfGGgggZ.#<br/>" +
                    "#......D.........#<br/>" +
                    "##################<br/>";

                    result = result.Replace("f", string.Format("<font color={0}>%</font>", b[2] && b[3] ? "#00ff00" : "ff0000"));
                    result = result.Replace("FF", string.Format("<font color={0}>&amp;&#8594;</font>", b[2] && b[3] ? "00ff00" : "ff0000"));

                    result = result.Replace("a", string.Format("<font color={0}>%</font>", b[0] ? "00ff00" : "ff0000"));
                    result = result.Replace("x", string.Format("<font color={0}>%</font>", b[1] ? "00ff00" : "ff0000"));
                    result = result.Replace("y", string.Format("<font color={0}>%</font>", b[2] ? "00ff00" : "ff0000"));
                    result = result.Replace("d", string.Format("<font color={0}>%</font>", b[3] ? "00ff00" : "ff0000"));
                    result = result.Replace("e", string.Format("<font color={0}>%</font>", b[0] || b[1] ? "00ff00" : "ff0000"));
                    result = result.Replace("EE", string.Format("<font color={0}>|&#8594;</font>", b[0] || b[1] ? "00ff00" : "ff0000"));
                    result = result.Replace("g", string.Format("<font color={0}>%</font>", (b[0] || b[1]) ^ (b[2] && b[3]) ? "00ff00" : "ff0000"));
                    result = result.Replace("GG", string.Format("<font color={0}>^&#8594;</font>", (b[0] || b[1]) ^ (b[2] && b[3]) ? "00ff00" : "ff0000"));
                    result = result.Replace("Z", string.Format("<font color={0}>Z</font>", (b[0] || b[1]) ^ (b[2] && b[3]) ? "00ff00" : "ff0000"));

                    result = result.Replace("A", string.Format("<font color={0}>A</font>", b[0] ? "00ff00" : "ff0000"));
                    result = result.Replace("B", string.Format("<font color={0}>B</font>", b[1] ? "00ff00" : "ff0000"));
                    result = result.Replace("C", string.Format("<font color={0}>C</font>", b[2] ? "00ff00" : "ff0000"));
                    result = result.Replace("D", string.Format("<font color={0}>D</font>", b[3] ? "00ff00" : "ff0000"));

                    return result;

                case 2:
                    result = "" +
                    "##################<br/>" +
                    "#..Aaaaaaaa......#<br/>" +
                    "#.........EEe....#<br/>" +
                    "#..Bxxxxxxx.e....#<br/>" +
                    "#...........GGgZ.#<br/>" +
                    "#.CYyyyyyyy.f....#<br/>" +
                    "#.........FFf....#<br/>" +
                    "#...Ddddddd......#<br/>" +
                    "##################<br/>";

                    result = result.Replace("f", string.Format("<font color={0}>%</font>", !b[2] && b[3] ? "00ff00" : "ff0000"));
                    result = result.Replace("FF", string.Format("<font color={0}>&amp;&#8594;</font>", !b[2] && b[3] ? "00ff00" : "ff0000"));

                    result = result.Replace("a", string.Format("<font color={0}>%</font>", b[0] ? "00ff00" : "ff0000"));
                    result = result.Replace("x", string.Format("<font color={0}>%</font>", b[1] ? "00ff00" : "ff0000"));
                    result = result.Replace("Y", string.Format("<font color={0}>!</font>", !b[2] ? "00ff00" : "ff0000"));
                    result = result.Replace("y", string.Format("<font color={0}>%</font>", !b[2] ? "00ff00" : "ff0000"));
                    result = result.Replace("d", string.Format("<font color={0}>%</font>", b[3] ? "00ff00" : "ff0000"));
                    result = result.Replace("e", string.Format("<font color={0}>%</font>", b[0] && b[1] ? "00ff00" : "ff0000"));
                    result = result.Replace("EE", string.Format("<font color={0}>&amp;&#8594;</font>", b[0] && b[1] ? "00ff00" : "ff0000"));
                    result = result.Replace("g", string.Format("<font color={0}>%</font>", b[0] && b[1] && !b[2] && b[3] ? "00ff00" : "ff0000"));
                    result = result.Replace("GG", string.Format("<font color={0}>&amp;&#8594;</font>", b[0] && b[1] && !b[2] && b[3] ? "00ff00" : "ff0000"));
                    result = result.Replace("Z", string.Format("<font color={0}>Z</font>", b[0] && b[1] && !b[2] && b[3] ? "00ff00" : "ff0000"));


                    result = result.Replace("A", string.Format("<font color={0}>A</font>", b[0] ? "00ff00" : "ff0000"));
                    result = result.Replace("B", string.Format("<font color={0}>B</font>", b[1] ? "00ff00" : "ff0000"));
                    result = result.Replace("C", string.Format("<font color={0}>C</font>", b[2] ? "00ff00" : "ff0000"));
                    result = result.Replace("D", string.Format("<font color={0}>D</font>", b[3] ? "00ff00" : "ff0000"));

                    return result;
            }
        }
    }
}
