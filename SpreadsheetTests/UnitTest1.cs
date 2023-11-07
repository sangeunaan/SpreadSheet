using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;
using SpreadsheetUtilities;
using System.Collections.Generic;

namespace SpreadsheetTests
{
    [TestClass]
    public class SpreadsheetTests
    {
        [TestMethod]
        public void TestMethod0()
        {
            AbstractSpreadsheet sheet1 = new Spreadsheet();
            AbstractSpreadsheet sheet2 = new Spreadsheet(s => true, s => s.ToUpper(), "1.0");
        }

        /// <summary>
        ///     Tests if cell name for GetCellValue is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod1()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.GetCellValue(null);
        }

        /// <summary>
        ///     Tests if cell name for GetCellValue is invalid
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod2()
        {
            Spreadsheet sheet = new Spreadsheet();
            object value = sheet.GetCellValue("1$#*&%1");
        }

        /// <summary>
        ///     Tests SetContentsOfCell for a null content parameter
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMethod7()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", null);
        }

        /// <summary>
        ///     Tests SetContentsOfCell for a null name parameter
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod8()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell(null, "hello");
        }

        /// <summary>
        ///     Tests SetContentsOfCell for an invalid name parameter
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod9()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("1111111", "hello");
        }

        /// <summary>
        ///     Tests SetContentsOfCell for an invalid formula exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestMethod10()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "=$%^302045 ++ 2)");
        }

        /// <summary>
        ///     Tests SetContentsOfCell for an invalid formula exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void TestMethod11()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "=A1");
        }

        /// <summary>
        ///     Tests GetCellValue, expected a double given a double.
        ///     Also tests SetContentsOfCell given a double (as a string).
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod14()
        {
            Spreadsheet sheet = new Spreadsheet(s => false, s => s.ToUpper(), "default");
            sheet.SetContentsOfCell("A1", "5");
            object value = sheet.GetCellValue("A1");
            Assert.AreEqual(5.0, value);
        }

        /// <summary>
        ///     Tests GetCellValue, expected a double given a double.
        ///     Also tests SetContentsOfCell given a double (as a string).
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestMethod15()
        {
            Spreadsheet sheet = new Spreadsheet(check_var, s => s.ToUpper(), "default");
            sheet.SetContentsOfCell("A1", "=B1");
            sheet.SetContentsOfCell("B1", "5");
            object value = sheet.GetCellValue("A1");
            Assert.AreEqual(5.0, value);
        }

        bool valid = true;

        /// <summary>
        ///     helper delegate method
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool check_var(string s)
        {
            // will switch off each time if it is true or false
            if (valid)
            {
                valid = false;
                return true;
            }
            else return false;

        }

        ///<summary>
        ///     Tests when we try to save a spreadsheet to a null filepath location
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestMethod17()
        {
            Spreadsheet sheet = new Spreadsheet(s => true, s => s, "2.0");
            sheet.SetContentsOfCell("D1", "1");
            sheet.SetContentsOfCell("E1", "1");
            sheet.SetContentsOfCell("F1", "3");
            sheet.SetContentsOfCell("B1", "=D1 + E1");
            sheet.SetContentsOfCell("C1", "=F1");
            sheet.SetContentsOfCell("A1", "=B1 + C1");
            sheet.Save(null);
        }

        ///<summary>
        ///     Tests when we try to save a spreadsheet to an empty filepath location
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestMethod18()
        {
            Spreadsheet sheet = new Spreadsheet(s => true, s => s, "2.0");
            sheet.SetContentsOfCell("D1", "1");
            sheet.SetContentsOfCell("E1", "1");
            sheet.SetContentsOfCell("F1", "3");
            sheet.SetContentsOfCell("B1", "=D1 + E1");
            sheet.SetContentsOfCell("C1", "=F1");
            sheet.SetContentsOfCell("A1", "=B1 + C1");
            sheet.Save("");
        }

        /// <summary>
        ///     Tests when we try to read a spreadsheet from a null filepath location
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestMethod19()
        {
            Spreadsheet sheet1 = new Spreadsheet(s => true, s => s, "2.0");
            sheet1.SetContentsOfCell("D1", "1");
            sheet1.SetContentsOfCell("E1", "1");
            sheet1.SetContentsOfCell("F1", "3");
            sheet1.SetContentsOfCell("B1", "=D1 + E1");
            sheet1.SetContentsOfCell("C1", "=F1");
            sheet1.SetContentsOfCell("A1", "=B1 + C1");
            sheet1.Save("save.txt");
            Spreadsheet sheet2 = new Spreadsheet();
            string version = sheet2.GetSavedVersion(null);
        }

        /// <summary>
        ///     Tests when we try to read a spreadsheet from an empty filepath location
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestMethod20()
        {
            Spreadsheet sheet1 = new Spreadsheet(s => true, s => s, "2.0");
            sheet1.SetContentsOfCell("D1", "1");
            sheet1.SetContentsOfCell("E1", "1");
            sheet1.SetContentsOfCell("F1", "3");
            sheet1.SetContentsOfCell("B1", "=D1 + E1");
            sheet1.SetContentsOfCell("C1", "=F1");
            sheet1.SetContentsOfCell("A1", "=B1 + C1");
            sheet1.Save("save.txt");
            Spreadsheet sheet2 = new Spreadsheet();
            string version = sheet2.GetSavedVersion("");
        }

        /// <summary
        ///     Tries to get the saved version of a file that doesn not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestMethod44()
        {
            Spreadsheet sheet2 = new Spreadsheet();
            string version = sheet2.GetSavedVersion("save.txt");
        }

        /// <summary
        ///     Tries to get saved version when file startes with a <cell> tag
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestMethod45()
        {
            Spreadsheet sheet2 = new Spreadsheet();
            string version = sheet2.GetSavedVersion("save.txt");
        }

        /// <summary
        ///     Tries to get saved version when file startes with a <name> tag
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestMethod46()
        {
            Spreadsheet sheet2 = new Spreadsheet();
            string version = sheet2.GetSavedVersion("save.txt");
        }

        /// <summary
        ///     Tries to get saved version when file startes with a <contents> tag
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestMethod47()
        {
            Spreadsheet sheet2 = new Spreadsheet();
            string version = sheet2.GetSavedVersion("save.txt");
        }

        /// <summary>
        ///     Tries to save a file to a folder rather than a file (bad path name)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestMethod48()
        {
            Spreadsheet sheet1 = new Spreadsheet(s => true, s => s, "2.0");
            sheet1.SetContentsOfCell("D1", "1");
            sheet1.SetContentsOfCell("E1", "1");
            sheet1.SetContentsOfCell("F1", "3");
            sheet1.SetContentsOfCell("B1", "=D1 + E1");
            sheet1.SetContentsOfCell("C1", "=F1");
            sheet1.SetContentsOfCell("A1", "=B1 + C1");
            sheet1.Save("C:\\Users\\2002s\\source\\");
        }

        /// <summary>
        ///     Tries to construct a new spreadsheet using a bad path name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestMethod49()
        {
            Spreadsheet sheet1 = new Spreadsheet("save.txt", s => true, s => s, "2.0");
        }

        /// <summary>
        ///     Tries to construct a new spreadsheet using a bad path name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestMethod51()
        {
            Spreadsheet sheet1 = new Spreadsheet("save.txt", s => true, s => s, "3.0");
        }

        /// <summary>
        ///     Tries to construct a new spreadsheet using a bad path name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestMethod53()
        {
            Spreadsheet sheet1 = new Spreadsheet("save.txt", s => true, s => s, "2.0");
        }


        /// <summary>
        ///     Tests GetCellValue, expects a string given a string.
        ///     Also tests SetContentsOfCell given a string.
        /// </summary>
        [TestMethod]
        public void TestMethod3()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "hello");
            object value = sheet.GetCellValue("A1");
            Assert.AreEqual("hello", value);
        }

        /// <summary>
        ///     Tests GetCellValue, expected a double given a double.
        ///     Also tests SetContentsOfCell given a double (as a string).
        /// </summary>
        [TestMethod]
        public void TestMethod4()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "5");
            object value = sheet.GetCellValue("A1");
            Assert.AreEqual(5.0, value);
        }

        /// <summary>
        ///     Tests GetCellValue, expects a double given a formula.
        ///     Also tests SetContentsOfCell given a formula (as a string).
        /// </summary>
        [TestMethod]
        public void TestMethod5()
        {
            Spreadsheet sheet = new Spreadsheet(s => true, s => s, "default");
            sheet.SetContentsOfCell("D1", "1");
            sheet.SetContentsOfCell("E1", "1");
            sheet.SetContentsOfCell("F1", "3");
            sheet.SetContentsOfCell("B1", "=D1 + E1");
            sheet.SetContentsOfCell("C1", "=F1");
            sheet.SetContentsOfCell("A1", "=B1 + C1");
            object value = sheet.GetCellValue("A1");
            Assert.AreEqual(5.0, value);
        }

        /// <summary>
        ///     Tests GetCellValue, expects a SpreadsheetUtilities.FormulaError given a formula.
        ///     Also tests SetContentsOfCell given a formula (as a string). 
        /// </summary>
        [TestMethod]
        public void TestMethod6()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "=B1 + C1");
            object value = sheet.GetCellValue("A1");
            Assert.IsInstanceOfType(value, typeof(FormulaError));
        }

        /// <summary>
        ///     Tests GetCellValue, expects a SpreadsheetUtilities.FormulaError given a formula.
        ///     Also tests SetContentsOfCell given a formula (as a string). 
        /// </summary>
        [TestMethod]
        public void TestMethod12()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("B1", "hello");
            sheet.SetContentsOfCell("A1", "=B1");
            object value = sheet.GetCellValue("A1");
            Assert.IsInstanceOfType(value, typeof(FormulaError));
        }

        /// <summary>
        ///     Tests GetCellValue, expects an empty string. Also test 'Changed' for empty constructor
        /// </summary>
        [TestMethod]
        public void TestMethod13()
        {
            Spreadsheet sheet = new Spreadsheet();
            Assert.AreEqual(sheet.Changed, false);
            object value = sheet.GetCellValue("A1");
            Assert.AreEqual("", value);
        }

        /// <summary>
        ///     Tests the save method (see Output). Should write a file that looks like:
        ///     
        ///     <spreadsheet version="2.0">
        ///      <cell>        
        ///       <name>D1</name>
        ///       <contents>1.0</contents>
        ///      </cell>
        ///      <cell>        
        ///       <name>E1</name>
        ///       <contents>1.0</contents>
        ///      </cell>
        ///      <cell>        
        ///       <name>F1</name>
        ///       <contents>3.0</contents>
        ///      </cell>
        ///      <cell>        
        ///       <name>B1</name>
        ///       <contents>=D1+E1</contents>
        ///      </cell>
        ///      <cell>        
        ///       <name>C1</name>
        ///       <contents>=F1</contents>
        ///      </cell>
        ///      <cell>        
        ///       <name>A1</name>
        ///       <contents>=B1+C1</contents>
        ///      </cell>
        ///     </spreadsheet>
        ///     
        ///     Also tests the GetSavedVersion method. 
        /// </summary>
        [TestMethod]
        public void TestMethod16()
        {
            Spreadsheet sheet1 = new Spreadsheet(s => true, s => s, "2.0");
            sheet1.SetContentsOfCell("G1", "hello world");
            sheet1.SetContentsOfCell("D1", "1");
            sheet1.SetContentsOfCell("E1", "1");
            sheet1.SetContentsOfCell("F1", "3");
            sheet1.SetContentsOfCell("B1", "=D1 + E1");
            sheet1.SetContentsOfCell("C1", "=F1");
            sheet1.SetContentsOfCell("A1", "=B1 + C1");
            sheet1.Save("C:\\Users\\Basil\\Desktop\\spreadsheet-test-1.xml");
            Spreadsheet sheet2 = new Spreadsheet();
            string version = sheet2.GetSavedVersion("C:\\Users\\Basil\\Desktop\\spreadsheet-test-1.xml");
            Assert.AreEqual("2.0", version);
        }

        /// <summary>
        ///     Tests ability to save a file, then create a new spreadsheet using
        ///     the 4-argument constructor using the saved xml file.  We test for
        ///     equality between the original spreadsheet and one created using the 
        ///     saved xml file. This also tests the 'Changed' variable to determine 
        ///     if the spreadsheet has been changed or not. 
        /// </summary>
        [TestMethod]
        public void TestMethod21()
        {
            Spreadsheet sheet1 = new Spreadsheet(s => true, s => s, "2.0");
            Assert.AreEqual(sheet1.Changed, false);
            sheet1.SetContentsOfCell("G1", "hello world");
            Assert.AreEqual(sheet1.Changed, true);
            sheet1.SetContentsOfCell("D1", "1");
            sheet1.SetContentsOfCell("E1", "1");
            sheet1.SetContentsOfCell("F1", "3");
            Assert.AreEqual(sheet1.Changed, true);
            sheet1.SetContentsOfCell("B1", "=D1 + E1");
            sheet1.SetContentsOfCell("C1", "=F1");
            Assert.AreEqual(sheet1.Changed, true);
            sheet1.SetContentsOfCell("A1", "=B1 + C1");
            sheet1.Save("C:\\Users\\Basil\\Desktop\\spreadsheet-test-1.xml");
            Assert.AreEqual(sheet1.Changed, false);
            sheet1.Save("C:\\Users\\Basil\\Desktop\\spreadsheet-test-1.xml"); // try to save again (shouldn't do anything)
            Spreadsheet sheet2 = new Spreadsheet("C:\\Users\\Basil\\Desktop\\spreadsheet-test-1.xml", s => true, s => s, "2.0");
            Assert.AreEqual(sheet2.Changed, false);
            HashSet<string> names1 = new HashSet<string>(sheet1.GetNamesOfAllNonemptyCells());
            HashSet<string> names2 = new HashSet<string>(sheet2.GetNamesOfAllNonemptyCells());

            foreach (string cell_name in names1)
            {
                Assert.AreEqual(sheet1.GetCellContents(cell_name), sheet2.GetCellContents(cell_name));
            }
            foreach (string cell_name in names2)
            {
                Assert.AreEqual(sheet1.GetCellContents(cell_name), sheet2.GetCellContents(cell_name));
            }
        }


        /// <summary>
        ///     Tests GetCellValue after changing and having to recalculate
        ///     cell values. 
        /// </summary>
        [TestMethod]
        public void TestMethod50()
        {
            Spreadsheet sheet = new Spreadsheet(s => true, s => s, "default");
            sheet.SetContentsOfCell("D1", "1");
            sheet.SetContentsOfCell("E1", "1");
            sheet.SetContentsOfCell("D1", "2");
            object D1value = sheet.GetCellValue("D1");
            Assert.AreEqual(2.0, D1value);
            sheet.SetContentsOfCell("F1", "3");
            sheet.SetContentsOfCell("B1", "=D1 + E1");
            sheet.SetContentsOfCell("B1", "=F1 + D1");
            object B1value = sheet.GetCellValue("B1");
            Assert.AreEqual(5.0, B1value);
            sheet.SetContentsOfCell("C1", "=F1");
            sheet.SetContentsOfCell("A1", "=B1 + C1");
            object A1value = sheet.GetCellValue("A1");
            Assert.AreEqual(8.0, A1value);
        }

        /// <summary>
        ///     Tests the return value of SetContentsOfCell. Also tests whether
        ///     cells recalculate after having their dependee (D1) changed. 
        /// </summary>
        [TestMethod]
        public void TestMethod55()
        {
            Spreadsheet sheet = new Spreadsheet(s => true, s => s, "default");
            sheet.SetContentsOfCell("D1", "4");
            sheet.SetContentsOfCell("E1", "=D1");
            sheet.SetContentsOfCell("F1", "=D1");
            sheet.SetContentsOfCell("C1", "7");
            sheet.SetContentsOfCell("F1", "=C1");
            sheet.SetContentsOfCell("C1", "10");
            sheet.SetContentsOfCell("G1", "=D1");
            HashSet<string> values_depend_on_D1 = new HashSet<string>(sheet.SetContentsOfCell("D1", "1"));
            object E1value = sheet.GetCellValue("E1");
            object F1value = sheet.GetCellValue("F1");
            object G1value = sheet.GetCellValue("G1");
            Assert.AreEqual(E1value, 1.0);
            Assert.AreEqual(F1value, 10.0);
            Assert.AreEqual(G1value, 1.0);
            HashSet<string> set = new HashSet<string>();
            set.Add("D1");
            set.Add("E1");
            set.Add("F1");
            set.Add("G1");
            foreach (string s in values_depend_on_D1)
            {
                Assert.IsTrue(set.Contains(s));
            }

        }

        // --------------------------- PS4 Tests ------------------------------- //

        // ------------ PS4 Exception Tests ------------ //

        /// <summary>
        /// Tests null exception for all three SetCellContents methods (string)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMethod22()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", null);
        }

        /// <summary>
        /// Tests null name exception for all three SetCellContents method (string)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod23()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell(null, "string");
        }

        /// <summary>
        /// Tests invalid name exception for all three SetCellContents method (string)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod24()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("4356", "string");

        }

        /// <summary>
        /// Tests null name exception for all three GetCellContents methods
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod25()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.GetCellContents(null);

        }

        /// <summary>
        /// Tests invalid name exception for all three GetCellContents methods
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod26()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.GetCellContents("45345");

        }

        /// <summary>
        /// Tests null exception for all three SetCellContents methods (Formula)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMethod27()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", null);
        }

        /// <summary>
        /// Tests null name exception for all three SetCellContents method (Formula)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod28()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell(null, "=1 + 1");
        }

        /// <summary>
        /// Tests invalid name exception for all three SetCellContents method (Formula)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod29()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("4356", "=1 + 1");
        }

        /// <summary>
        /// Tests null name exception for all three SetCellContents method (double)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod30()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell(null, "42");
        }

        /// <summary>
        /// Tests invalid name exception for all three SetCellContents method (double)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod31()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("4356", "42");

        }

        /*
        /// <summary>
        /// Tests null arg exception for GetDirectDependents
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMethod32()
        {
            PrivateObject accessor = new PrivateObject(new Spreadsheet());
            accessor.Invoke("GetDirectDependents", new Object[] { null });

        }

        /// <summary>
        /// Tests null arg exception for GetDirectDependents 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestMethod33()
        {
            PrivateObject accessor = new PrivateObject(new Spreadsheet());
            accessor.Invoke("GetDirectDependents", new Object[] { "4353" });
        }
        */

        /// <summary>
        /// Tests for CircularException 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void TestMethod34()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "=A1 + B1");
        }

        /// <summary>
        /// A more complex test for CircularException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void TestMethod35()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("D1", "=A1+B1");
            sheet.SetContentsOfCell("A1", "=A3*B4");
            sheet.SetContentsOfCell("B1", "=A3*B4");
            sheet.SetContentsOfCell("A3", "=E1+C1");
            sheet.SetContentsOfCell("B4", "=C1-A3");
            sheet.SetContentsOfCell("E1", "2");
            sheet.SetContentsOfCell("C1", "6");
            sheet.SetContentsOfCell("A3", "=A1");

        }

        // --------- PS4 Non-Exception Tests ------------ //

        /// <summary>
        /// Tests for GetDirectDependents
        /// </summary>
        /*
        [TestMethod]
        public void TestMethod36()
        {
            PrivateObject accessor = new PrivateObject(new Spreadsheet());
            accessor.Invoke("GetDirectDependents", new Object[] { "A1" });

        }
        */

        /// <summary>
        /// Tests for SetCellContents (Formula)
        /// </summary>
        [TestMethod]
        public void TestMethod37()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "=C1 + B1");
            object value = sheet.GetCellContents("A1");
            Assert.AreEqual(new Formula("C1 + B1"), value);
        }

        /// <summary>
        /// Tests for SetCellContents (double)
        /// </summary>
        [TestMethod]
        public void TestMethod38()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "42");
            object value = sheet.GetCellContents("A1");
            Assert.AreEqual(42.0, value);
        }

        /// <summary>
        /// Tests for SetCellContents (string)
        /// </summary>
        [TestMethod]
        public void TestMethod39()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "Hello World");
            object value = sheet.GetCellContents("A1");
            Assert.AreEqual("Hello World", value);
        }

        /// <summary>
        /// Tests for GetCellContents
        /// </summary>
        [TestMethod]
        public void TestMethod40()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "Hello World");
            object value = sheet.GetCellContents("A1");
            object value2 = sheet.GetCellContents("D1"); // shouldn't exist
            Assert.AreEqual("Hello World", value);
            Assert.AreEqual("", value2);
        }

        /// <summary>
        /// Tests for SetCellContents (Formula)
        /// </summary>
        [TestMethod]
        public void TestMethod41()
        {
            AbstractSpreadsheet sheet = new Spreadsheet(x => true, x => x.ToUpper(), "default");
            sheet.SetContentsOfCell("A1", "=c1 + b1");
            object value = sheet.GetCellContents("A1");
            Assert.AreEqual(new Formula("C1 + B1"), value);
        }

        /// <summary>
        /// Tests for replacing contents of a cell
        /// </summary>
        [TestMethod]
        public void TestMethod42()
        {
            AbstractSpreadsheet sheet = new Spreadsheet(x => true, x => x.ToUpper(), "default");
            sheet.SetContentsOfCell("A1", "=c1 + b1");
            sheet.SetContentsOfCell("A1", "42");
            sheet.SetContentsOfCell("A1", "=c1 + b1");
            sheet.SetContentsOfCell("A1", "Hello World");
            HashSet<string> s1 = new HashSet<string>(sheet.GetNamesOfAllNonemptyCells());
            HashSet<string> s2 = new HashSet<string>();
            s2.Add("A1");
            Assert.AreEqual(s2.Count, s1.Count);
        }

        /// <summary>
        /// Tests for replacing contents of a cell
        /// </summary>
        [TestMethod]
        public void TestMethod43()
        {
            AbstractSpreadsheet sheet = new Spreadsheet(x => true, x => x.ToUpper(), "default");
            sheet.SetContentsOfCell("A1", "");
            sheet.SetContentsOfCell("B1", "42");
            sheet.SetContentsOfCell("C1", "Hello World");
            HashSet<string> s1 = new HashSet<string>(sheet.GetNamesOfAllNonemptyCells());
            HashSet<string> s2 = new HashSet<string>();
            s2.Add("B1");
            s2.Add("C1");
            Assert.AreEqual(s2.Count, s1.Count);
        }
    }
}