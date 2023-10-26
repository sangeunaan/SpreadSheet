﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetUtilities;
using System.Text.RegularExpressions;

namespace SS
{
    //*** Remember to add XML Documentation and sync into SVN ***//

    /// <summary>
    /// This class represents a Spreadsheet object that extends the abstract 
    /// spreadsheet class
    /// </summary>
    /// <author>Basil Vetas</author>
    /// <date>10-1-2014</date>
    public class Spreadsheet : AbstractSpreadsheet
    {
        /// <summary>
        /// Cell object implementation
        /// </summary>
        private class Cell
        {
            public Object contents { get; private set; }
            public Object value { get; private set; }

            string contents_type;
            string value_type;

            /// <summary>
            /// Constructor for string contents cell
            /// </summary>
            /// <param name="name"></param>
            public Cell(string name)
            {
                contents = name;
                value = name;
                contents_type = "string";
                value_type = contents_type;
            }

            /// <summary>
            /// Constructor for double contents cell
            /// </summary>
            /// <param name="name"></param>
            public Cell(double name)
            {
                contents = name;
                value = name;
                contents_type = "double";
                value_type = contents_type;
            }

            /// <summary>
            /// Constructor for Formula contents cell: complex expressions containing at least one operator
            /// </summary>
            /// <param name="name"></param>
            public Cell(Formula name)
            {
                contents = name;
                //value = name.Evaluate();
                contents_type = "Formula";
                //value_type = value.GetType();
            }

        }


        // Use dictionary to assign cells a cell name and a value(expression)
        // Key: cell name(string)
        // Value: cell value(Cell)
        Dictionary<string, Cell> cells;

        // dependency graph for cells
        DependencyGraph dg;

        /// <summary>
        /// Constructor creates a new spreadsheet and initialized variables
        /// </summary>
        public Spreadsheet()
        {
            // initialize spreadsheet variables;
            cells = new Dictionary<string, Cell>();
            dg = new DependencyGraph();
        }

        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public override IEnumerable<String> GetNamesOfAllNonemptyCells()
        {
            // Name of the Cells: key of cells dictionary
            return cells.Keys;
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.  The return
        /// value should be either a string, a double, or a Formula.
        /// </summary>
        
        public override object GetCellContents(String name)
        {
            //Getter(return) for the Cell contents
            //variable "name": name(key) of the cell

            if (name == null || !(IsValidName(name)))
                throw new InvalidNameException();

            Cell value; // value of name

            if (cells.TryGetValue(name, out value))
                return value.contents;
            else
                return "";
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes number.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override IList<String> SetCellContents(String name, double number)
        {
            //Setter for the Cell contents

            if (ReferenceEquals(name, null) || !(IsValidName(name)))
                throw new InvalidNameException();

            // Create a new cell
            Cell cell = new Cell(number);
            if (cells.ContainsKey(name))    // if it already contains that key
                cells[name] = cell;         // replace the key with the new value
            else
                cells.Add(name, cell);      // otherwise add a new key for that value

            // replace the dependents of 'name' in the dependency graph with an empty hash set
            dg.ReplaceDependees(name, new List<String>());

            // recalculate at end
            List<String> all_dependees = new List<String>(GetCellsToRecalculate(name));
            return all_dependees;
        }

        /// <summary>
        /// If text is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes text.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override IList<String> SetCellContents(String name, String str)
        {
            if (str == null)
                throw new ArgumentNullException();

            if ((name == null) || !(IsValidName(name)))
                throw new InvalidNameException();


            Cell cell = new Cell(str);

            if (cells.ContainsKey(name))    // if it already contains that key
                cells[name] = cell;         // replace the key with the new value
            else
                cells.Add(name, cell);      // otherwise add a new key for that value

            if (cells[name].contents == "") // if the contents is an empty string, we don't want it in the dictionary
                cells.Remove(name);

            // replace the dependents of 'name' in the dependency graph with an empty hash set
            dg.ReplaceDependees(name, new List<String>());

            // recalculate at end
            List<String> all_dependees = new List<String>(GetCellsToRecalculate(name));
            return all_dependees;
        }

        /// <summary>
        /// If the formula parameter is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException.  (No change is made to the spreadsheet.)
        /// 
        /// Otherwise, the contents of the named cell becomes formula.  The method returns a
        /// Set consisting of name plus the names of all other cells whose value depends,
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override IList<String> SetCellContents(String name, Formula formula)
        {
            if (formula == null)
                throw new ArgumentNullException();

            if ((name == null) || !(IsValidName(name)))
                throw new InvalidNameException();

            // temp variable to hold old dependent.
            IEnumerable<String> old_dependees = dg.GetDependees(name);

            // replace the dependents of 'name' in the dependency graph with the variables in formula
            dg.ReplaceDependees(name, formula.GetVariables());

            try // check if the new depdendency graph creates a circular reference
            {
                // if there is no exception
                List<String> all_dependees = new List<String>(GetCellsToRecalculate(name));
                // create a new cell
                Cell cell = new Cell(formula);
                if (cells.ContainsKey(name))    // if it already contains that key
                    cells[name] = cell;         // replace the key with the new value
                else
                    cells.Add(name, cell);      // otherwise add a new key for that value

                return all_dependees;
            }
            catch (CircularException e) // if an exception is caught, we want to keep the old dependents and not change the cell
            {
                dg.ReplaceDependees(name, old_dependees);
                throw new CircularException();
            }

        }

        /// <summary>
        /// If name is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name isn't a valid cell name, throws an InvalidNameException.
        /// 
        /// Otherwise, returns an enumeration, without duplicates, of the names of all cells whose
        /// values depend directly on the value of the named cell.  In other words, returns
        /// an enumeration, without duplicates, of the names of all cells that contain
        /// formulas containing name.
        /// 
        /// For example, suppose that
        /// A1 contains 3
        /// B1 contains the formula A1 * A1
        /// C1 contains the formula B1 + A1
        /// D1 contains the formula B1 - C1
        /// The direct dependents of A1 are B1 and C1
        /// </summary>
        protected override IEnumerable<String> GetDirectDependents(String name)
        {
            if (ReferenceEquals(name, null))
                throw new ArgumentNullException();

            if (!(IsValidName(name)))
                throw new InvalidNameException();

            // GetDependents returns a HashSet ensuring there won't be duplicates
            return dg.GetDependents(name); 
        }

        /// <summary>
        /// Private helper method to check if the name of a cell is valid or not
        /// </summary>
        /// <param name="name"></param>
        private Boolean IsValidName(String name)
        {
            // if it is a valid cell name return true, else return false
            if (Regex.IsMatch(name, @"^[a-zA-Z_](?: [a-zA-Z_]|\d)*$", RegexOptions.Singleline))
                return true;
            else return false;
        }

    } // END Spreadsheet class

} // END SS Namespace