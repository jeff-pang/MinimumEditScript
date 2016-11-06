using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinimumEditScript;

namespace MinimumEditScriptTest
{
    [TestClass]
    public class MESTest
    {
        [TestMethod]
        public void minimum_edit_script()
        {
            MES mes = new MES();

            {
                var script = mes.Find("SITTING", "KITTEN");
                Assert.AreEqual(3, script.Last().Distance);
                int i = 0;

                var expected = new Tuple<char,char, EditActions>[] {
                    new Tuple<char,char, EditActions>('S','K', EditActions.Substitute),
                    new Tuple<char,char, EditActions>('I','E', EditActions.Substitute),
                    new Tuple<char,char, EditActions>('G','N', EditActions.Insert)
                };

                for (int x=0;x<script.Count;x++)
                {
                    if(script[x].Edit != EditActions.Copy)
                    {
                        Assert.AreEqual(expected[i].Item1, script[x].A);
                        Assert.AreEqual(expected[i].Item2, script[x].B);
                        Assert.AreEqual(expected[i].Item3, script[x].Edit);
                        i++;
                    }
                }
            }

            {
                var script = mes.Find("SATURDAY","SUNDAY");

                Assert.AreEqual(3, script.Last().Distance);
                int i = 0;

                var expected = new Tuple<char, char, EditActions>[] {
                    new Tuple<char,char, EditActions>('A','S', EditActions.Insert),
                    new Tuple<char,char, EditActions>('T','S', EditActions.Insert),
                    new Tuple<char,char, EditActions>('R','N', EditActions.Substitute)
                };

                for (int x = 0; x < script.Count; x++)
                {
                    if (script[x].Edit != EditActions.Copy)
                    {
                        Assert.AreEqual(expected[i].Item1, script[x].A);
                        Assert.AreEqual(expected[i].Item2, script[x].B);
                        Assert.AreEqual(expected[i].Item3, script[x].Edit);
                        i++;
                    }
                }
            }
        }
    }
}