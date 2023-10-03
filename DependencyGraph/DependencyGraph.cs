// Skeleton implementation written by Sangeun An for CS 3500, September 2013.
// Version 1.1 (Fixed error in comment for RemoveDependency.)
// Method Implementation by Basil Vetas
// Date: September/30/2023

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpreadsheetUtilities
{

    /// <summary>
    /// A DependencyGraph can be modeled as a set of ordered pairs of strings.  Two ordered pairs
    /// (s1,t1) and (s2,t2) are considered equal if and only if s1 equals s2 and t1 equals t2.
    /// (Recall that sets never contain duplicates.  If an attempt is made to add an element to a 
    /// set, and the element is already in the set, the set remains unchanged.)
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that (s,t) is in DG is called dependents(s).
    ///        
    ///    (2) If s is a string, the set of all strings t such that (t,s) is in DG is called dependees(s).
    //
    // For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    //     dependents("a") = {"b", "c"}
    //     dependents("b") = {"d"}
    //     dependents("c") = {}
    //     dependents("d") = {"d"}
    //     dependees("a") = {}
    //     dependees("b") = {"a"}
    //     dependees("c") = {"a"}
    //     dependees("d") = {"b", "d"}
    /// </summary>
    /// 

    public class DependencyGraph
    {
        // will map a dependees to its set of dependents
        private Dictionary<string, HashSet<string>> Dependees;

        // will map a dependent to its set of dependees
        private Dictionary<string, HashSet<string>> Dependents;

        // a counter variable that keeps track of the number of ordered         
        // pairs in the dependency graph
        private int size = 0;

        /// <summary>
        /// Creates an empty DependencyGraph.
        /// </summary>
        public DependencyGraph()
        {
            //key = string, value = HashSet of strings
            //Dependees: arranges all dependee cells of the key dependent cell
            //Dependents: arranges all dependent cells of the key dependee cell
            Dependees = new Dictionary<string, HashSet<string>>();
            Dependents = new Dictionary<string, HashSet<string>>();
        }

        /// <summary>
        /// The number of ordered pairs in the DependencyGraph.
        /// </summary>
        public int Size 
        {
            get { return size; }
        }


        /// <summary>
        /// The size of dependees(s).
        /// This property is an example of an indexer.  If dg is a DependencyGraph, you would
        /// invoke it like this:
        /// dg["a"]
        /// It should return the size of dependees("s")
        /// </summary>
        public int this[string s]
        {
            get
            {
                if (Dependees.ContainsKey(s))
                {
                    return Dependees[s].Count;
                }
                else
                {
                    return 0;
                }
            } 
        }


        /// <summary>
        /// Reports whether dependents(s) is non-empty.
        /// </summary>
        public bool HasDependents(string s)
        {
            // if the Dependents dictionary contains 's' 
            if (Dependents.ContainsKey(s))
            {
                // if dependee cell 's' has dependents
                if (Dependents[s].Count > 0)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }
            // if the Dependents dictionary doesn't contains 's'
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Reports whether dependees(s) is non-empty.
        /// </summary>
        public bool HasDependees(string s)
        {
            // if the Dependees dictionary contains 's' 
            if (Dependees.ContainsKey(s))
            {
                // if dependent cell 's' has dependents
                if (Dependees[s].Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            // if the Dependees dictionary doesn't contains 's'
            else
            {
                return false;
            } 
        }


        /// <summary>
        /// Enumerates dependents(s).
        /// </summary>
        public IEnumerable<string> GetDependents(string s)
        {            
            if (Dependents.ContainsKey(s))
            {
                // return dependents as the IEnumerable variable                
                return new HashSet<string>(Dependents[s]); 
            }
            else
            {
                // return empty HashSet
                return new HashSet<string>();
            }

        }

        /// <summary>
        /// Enumerates dependees(s).
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            if (Dependees.ContainsKey(s))
            {
                // return dependees as the IEnumerable variable
                return new HashSet<string>(Dependees[s]); 
            }
            else
            {
                // return empty HashSet
                return new HashSet<string>(); 
            }
        }


        /// <summary>
        /// Adds the ordered pair (s,t), if it doesn't exist
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public void AddDependency(string s, string t)
        {
            // s: dependee t: dependent
            // if {'s' , 't'} pair does not have dependee-dependent relationship
            if (!(Dependees.ContainsKey(s) && Dependents.ContainsKey(t)))
            {
                Dependees.Add(t, new HashSet<string>());
                Dependees[t].Add(s);
                Dependents.Add(s, new HashSet<string>());
                Dependees[s].Add(t);
                // increase the size
                size++; 
            }

            // if 's' is in Dependees dictionary ; is already dependee of some cells
            if (Dependees.ContainsKey(s))
            {
                // add 't' to the value HashSet of 's'
                Dependees[s].Add(t);
            }
            // else if 's' is not in Dependees dictionary
            else
            {
                // make a new HashSet matches with 's'
                HashSet<string> addedDependents = new HashSet<string>();
                addedDependents.Add(t);
                Dependees.Add(s, addedDependents);
            }

            // if 't' is in Dependents dictionary ; is already depenent of some cells
            if (Dependents.ContainsKey(t))
            {
                // add 's' to the value HashSet of 't'
                Dependents[t].Add(s);
            }
            // else if 't' is not in Dependents dictionary
            else
            {
                // make a new HashSet matches with 't'
                HashSet<string> AddedDependees = new HashSet<string>();
                AddedDependees.Add(s);
                Dependents.Add(t, AddedDependees);
            }
        }


        /// <summary>
        /// Removes the ordered pair (s,t), if it exists
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public void RemoveDependency(string s, string t)
        {
            // if {'s' , 't'} pair has dependee-dependent relationship
            if (Dependees.ContainsKey(s) && Dependents.ContainsKey(t))
            {
                //decrease the size
                size--;
            }

            // if the dependees dictionary contains 's' 
            if (Dependees.ContainsKey(s))
            {
                // then just remove 't' from the value HashSet of 's'
                Dependees[s].Remove(t);
                if (Dependees[s].Count == 0)
                    Dependees.Remove(s);
            }

            // if the dependents dictionary contains 't'
            if (Dependents.ContainsKey(t))
            {
                // then just add 's' t the value HashSet of 't'
                Dependents[t].Remove(s);
                if (Dependents[t].Count == 0)
                    Dependents.Remove(t);
            }

        }


        /// <summary>
        /// Removes all existing ordered pairs of the form (s,r).  Then, for each
        /// t in newDependents, adds the ordered pair (s,t).
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            // IEnumerable object which contains dependents before replaced
            IEnumerable<string> oldDependents = GetDependents(s);

            // remove 'r' which was a dependent of 's'
            foreach (string r in oldDependents)
                RemoveDependency(s, r);           

            // add 't' as new dependent of 's'
            foreach (string t in newDependents)
                AddDependency(s, t);
        }


        /// <summary>
        /// Removes all existing ordered pairs of the form (r,s).  Then, for each 
        /// t in newDependees, adds the ordered pair (t,s).
        /// </summary>
        public void ReplaceDependees(string s, IEnumerable<string> newDependees)
        {
            // IEnumerable object which contains dependees before replaced
            IEnumerable<string> oldDependees = GetDependees(s);

            // remove 'r' which was a dependee of 's'
            foreach (string r in oldDependees)
                RemoveDependency(r, s); // remove the ordered pair           

            // add 't' as new dependent of 's'
            foreach (string t in newDependees)
                AddDependency(t, s);
        }

    } 

} 

