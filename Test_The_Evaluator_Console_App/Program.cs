///```
///Author: [Hayoung Im]
///Partner: None
///Date:       21 - Oct - 2023
///Course: CS 3500, ECE
///GitHub ID:  [Hayoung-Im]
///Repo: https://github.com/Fall-2023-CS3500-Class/spreadsheet-Hayoung-Im
///Commit #:   295c6b1b5e8b4c43eda989405bbd7f4676be358f
///Solution:   Spreadsheet
///Copyright:  CS 3500 and [Hayoung Im] -This work may not be copied for use in Academic Coursework.
///```
///
///add AS5
///
///Author: [Hayoung Im]
///Partner: None
///Date:       01 - Nov - 2023
///Course: CS 3500, ECE
///GitHub ID:  [Hayoung-Im]
///Repo: https://github.com/Fall-2023-CS3500-Class/spreadsheet-Hayoung-Im
///Commit #:   f34a0ee4d2e2d716bf4789d443b3f9aee7ff5243
///Solution:   Spreadsheet
///Copyright:  CS 3500 and [Hayoung Im] -This work may not be copied for use in Academic Coursework.
///```
///
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using SpreadsheetUtilities;


namespace SS
{
    public class Spreadsheet : AbstractSpreadsheet
    {
        // To store cell contents
        private Dictionary<string, Cell> cellContents;
        // To store cell dependencies
        private DependencyGraph depedencyGraph;

        public Spreadsheet()
            : base(s => true, s => s, "default")
        {
            cellContents = new Dictionary<string, Cell>();
            depedencyGraph = new DependencyGraph();
            Changed = false;
        }
        public Spreadsheet(Func<string, bool> isValid, Func<string, string> normalize, string version)
            : base(isValid, normalize, version)
        {
            cellContents = new Dictionary<string, Cell>();
            depedencyGraph = new DependencyGraph();
            Changed = false;

        }
        public Spreadsheet(string filepath, Func<string, bool> isValid, Func<string, string> normalize, string version)
            : base(isValid, normalize, version)
        {
            Changed = false;
            cellContents = new Dictionary<string, Cell>();
            depedencyGraph = new DependencyGraph();
            if (version != GetSavedVersion(filepath))
                throw new SpreadsheetReadWriteException("Error: mismatched version");
            Readfile(filepath);
        }

        public override bool Changed
        {
            get;
            protected set;
        }

        public override string GetSavedVersion(string filename)
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    try
                    {
                        while (reader.Read())
                        {
                            if (reader.IsStartElement())
                            {
                                switch (reader.Name)
                                {
                                    case "spreadsheet":
                                        return reader["version"];
                                    default:
                                        throw new SpreadsheetReadWriteException("Error: encountered malformed Xml while loading spreadsheet");
                                }
                            }
                        }
                    }
                    catch (XmlException ex)
                    {
                        throw new SpreadsheetReadWriteException("error: cannot parse xml" + ex.Message);
                    }
                    throw new SpreadsheetReadWriteException("Error: empty xml document");
                }
            }
            catch (Exception ex)
            {
                if (ex is DirectoryNotFoundException)
                {
                    throw new SpreadsheetReadWriteException("error: invalid directory" + ex.Message);
                }
                if (ex is FileNotFoundException)
                {
                    throw new SpreadsheetReadWriteException("error: specified file does not exist");
                }
                throw new SpreadsheetReadWriteException("something unpredictable happened!" + ex.Message);
            }
        }
        public override void Save(string filename)
        {
            try
            {
                using (XmlWriter writer = XmlWriter.Create(filename))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("spreadsheet");
                    writer.WriteAttributeString("version", Version);
                    foreach (Cell cell in cellContents.Values)
                    {
                        writer.WriteStartElement("cell");
                        writer.WriteElementString("name", cell.Name);
                        writer.WriteElementString("contents", writeContents(cell.Contents));
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Dispose();
                }
            }
            catch { throw new SpreadsheetReadWriteException("error writing to spreadsheet file"); }

        }

        public override object GetCellValue(string name)
        {

            if (name == null || !Regex.IsMatch(name, @"[a-zA-Z]+\d+") || !IsValid(Normalize(name)))
                throw new InvalidNameException();
            if (!cellContents.ContainsKey(name))
                return string.Empty;
            return cellContents[name].Value;

        }

        public override IList<string> SetContentsOfCell(string name, string content)
        {
            if (content == null)
                throw new ArgumentNullException();
            if (name == null || !Regex.IsMatch(name = Normalize(name), @"[a-zA-Z]+\d+"))
                throw new InvalidNameException();
            if (IsValid(name))
            {
                if (content == "")
                {
                    cellContents.Remove(name);
                    depedencyGraph.ReplaceDependents(name, new HashSet<string>());
                    return new List<string>(GetCellsToRecalculate(name));
                }
                if (content[0] == '=')
                    return SetCellContents(name, new Formula(content.Substring(1), Normalize, IsValid));
                double toAdd;
                try
                {
                    NumberStyles styles = NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;
                    toAdd = Double.Parse(content, styles);

                    return SetCellContents(name, toAdd);
                }
                catch
                {
                    return SetCellContents(name, content);
                }
            }
            throw new InvalidNameException();
        }
        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            foreach (KeyValuePair<string, Cell> var in cellContents)
            {
                if (var.Value.Contents is string)
                {
                    if (String.Compare(var.Value.Contents as string, string.Empty) != 0)
                    {
                        yield return var.Key;
                    }
                }
                else
                {
                    yield return var.Key;
                }
            }
        }
        public override object GetCellContents(string name)
        {
            if (String.Compare(name, null) == 0 || !Regex.IsMatch(name = Normalize(name), @"[a-zA-Z_](?: [a-zA-Z_]|\d)*"))
            {
                throw new InvalidNameException();
            }
            if (!cellContents.ContainsKey(name))
                return string.Empty;
            return cellContents[name].Contents;
        }
        protected override IList<string> SetCellContents(string name, double number)
        {
            if (name == null || !Regex.IsMatch(name, @"[a-zA-Z]+\d+"))
            {
                throw new InvalidNameException();
            }
            if (cellContents.ContainsKey(name))
                cellContents[name].Contents = number;
            else
                cellContents.Add(name, new Cell(name, number, lookerupper));
            Changed = true;

            depedencyGraph.ReplaceDependents(name, new List<string>());
            foreach (string var in GetCellsToRecalculate(name))
            {
                cellContents[var].Contents = cellContents[var].Contents;
            }
            List<string> dependents = new List<string>(GetCellsToRecalculate(name));
            Changed = true;
            return dependents;
        }
        protected override IList<string> SetCellContents(string name, string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException();
            }
            if (name == null || !Regex.IsMatch(name, @"[a-zA-Z]+\d+"))
            {
                throw new InvalidNameException();
            }

            if (cellContents.ContainsKey(name))
                cellContents[name].Contents = text;
            else
            {
                cellContents.Add(name, new Cell(name, text, lookerupper));
            }

            depedencyGraph.ReplaceDependents(name, new List<string>());
            foreach (string var in GetCellsToRecalculate(name))
            {
                cellContents[var].Contents = cellContents[var].Contents;
            }
            Changed = true;
            List<string> dependents = new List<string>(GetCellsToRecalculate(name));
            dependents.Add(name);
            return dependents;
        }

        protected override IList<string> SetCellContents(string name, Formula formula)
        {
            if (formula == null)
            {
                throw new ArgumentNullException();
            }
            if (name == null || !Regex.IsMatch(name, @"[a-zA-Z]+\d+"))
            {
                throw new InvalidNameException();
            }
            IEnumerable<string> store = depedencyGraph.GetDependents(name);
            depedencyGraph.ReplaceDependents(name, new HashSet<string>());
            foreach (string var in formula.GetVariables())
            {
                try
                {
                    depedencyGraph.AddDependency(name, var);
                }
                catch (InvalidOperationException)
                {
                    depedencyGraph.ReplaceDependents(name, store);
                    throw new CircularException();
                }
            }

            if (cellContents.ContainsKey(name))
                cellContents[name].Contents = formula;
            else
                cellContents.Add(name, new Cell(name, formula, lookerupper));
            foreach (string var in GetCellsToRecalculate(name))
            {
                cellContents[var].Contents = cellContents[var].Contents;
            }
            Changed = true;
            List<string> toreturn = new List<string>(GetCellsToRecalculate(name));
            return toreturn;
        }
        /// <summary>
        /// Retrieves the direct dependents of a specified variable name from the dependency graph.
        /// </summary>
        /// <param name="name">The name of the variable for which to find direct dependents.</param>
        /// <returns>An IEnumerable of strings representing the direct dependents of the variable.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidNameException"></exception>
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException();
            }
            else if (!Regex.IsMatch(name, @"[a-zA-Z]+\d+"))
            {
                throw new InvalidNameException();
            }
            return depedencyGraph.GetDependees(name);
        }
        /// <summary>
        /// Reads data from an XML file representing a spreadsheet and populates it into the program.
        /// </summary>
        /// <param name="filename"></param>
        /// <exception cref="SpreadsheetReadWriteException">Thrown if the filename is null or empty.</exception>
        private void Readfile(string filename)
        {
            using (XmlReader reader = XmlReader.Create(filename))
            {
                string name = "";
                string contents = "";
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {

                        switch (reader.Name)
                        {
                            case "spreadsheet":
                                Version = reader["version"];
                                break;
                            case "cell":
                                reader.Read();
                                name = reader.ReadElementContentAsString();
                                contents = reader.ReadElementContentAsString();
                                SetContentsOfCell(name, contents);
                                break;
                            default:
                                throw new SpreadsheetReadWriteException("error: malformed XML");
                        }

                    }
                }
            }
        }
        /// <summary>
        /// Converts cell contents into a string representation suitable for writing to a spreadsheet file.
        /// </summary>
        /// <param name="cellContents"></param>
        /// <returns></returns>
        /// <exception cref="SpreadsheetReadWriteException">Thrown if the content type is not supported.</exception>
        private string writeContents(object cellContents)
        {
            if (cellContents is Formula)
                return "=" + cellContents.ToString();
            if (cellContents is double)
                return cellContents.ToString();
            if (cellContents is string)
                return cellContents as string;
            throw new SpreadsheetReadWriteException("error writing Cell Contents");
        }

        public double lookerupper(string name)
        {
            if (cellContents.ContainsKey(name))
            {
                object concern = cellContents[name].Value;
                if (concern is double)
                    return (double)concern;
            }
            throw new ArgumentException("Error: cell value was not a double");
        }
        /// <summary>
        /// Represents a cell containing content associated with a specific name.
        /// </summary>
        private class Cell
        {

            public string Name { get; private set; }
            private object contents;
            public object Contents
            {
                get { return contents; }
                set
                {
                    _value = value;
                    if (value is Formula)
                    {
                        _value = (_value as Formula).Evaluate(MyLookup);
                    }
                    contents = value;
                }
            }
            private object _value;
            public object Value
            {
                get { return _value; }
                private set { _value = value; }
            }
            public Func<string, double> MyLookup { get; private set; }
            public Cell(string _name, object _contents, Func<string, double> _lookup)
            {
                Name = _name;
                MyLookup = _lookup;
                Contents = _contents;
            }
        }
    }
}