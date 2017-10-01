using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetTheoryAPI
{
    class Program
    {
        /// <summary>
        /// Testing instance with instanced sets and API's.
        /// </summary>
        static void Main(string[] args)
        {

            /////////////////////////////////////// TESTING AREA.

            /////////////////// Testing Sets
            int[] Set1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 };
            int[] Set2 = new int[] { 3, 5, 7, 9, 12, 18 };
            int[] Set2_ = new int[] { 3, 5, 7, 9, 12, 18 };
            int[] Set3 = new int[] { -1, 5, 3, 9, 12, 51 };
            int[] EmptySet = new int[] { };

            HashSet<int> Set1Hash = new HashSet<int>(Set1);
            HashSet<int> Set2Hash = new HashSet<int>(Set2);
            HashSet<int> Set2Hash_ = new HashSet<int>(Set2_);
            HashSet<int> Set3Hash = new HashSet<int>(Set3);
            HashSet<int> EmptySetHash = new HashSet<int>(EmptySet);

            //////////////////// Testing API instances.
            SetHandler SH = new SetHandler();
            SubSetHandler SSH = new SubSetHandler();

            //////////////////// Testing cases:

            // is Set2 a subset of set 1?: Should be true.
            Console.WriteLine("Is Set2 a subset of set1?: "+SSH.Is_Sub_Set(Set2Hash, Set1Hash));
            
            // This should be true.
            Console.WriteLine("is Set2_ the same as Set2?: " + SSH.Is_Sets_Equals(Set2Hash_, Set2Hash));
            
            // Should give 1, since Set2 is a subset of set1.
            Console.WriteLine("Comparing Sets of 1 and 2: "+SSH.CompareSetsEasyWay(Set1Hash, Set2Hash));
            
            // This should give "False", since -1 is not a member of set1 
            Console.WriteLine("Is -1 a member of set1?: " + SH.__Membership(Set1, -1));
            
            // What is the union of set3 and set1
            Console.WriteLine("Union of set1 and set3 is: "+ string.Join(", ", SH.__GetUnion(Set1,Set3))); //SH.__GetUnion(Set3, Set1).ToList().ForEach(Console.WriteLine); // Get union method gets new set, then puts set into a list and foreach elements it will printout to console. This will print vertically.
            
            // What is the Intersection of set2 and set3?
            Console.WriteLine("Intersection of set2 and set3?: "+ string.Join(", ", SH.__GetIntersection(Set2,Set3)));

            // What is the difference on set3 and set1? with other words: What elements does set3 have that set1 does not have?
            Console.WriteLine("The difference of set3 to set1: "+ string.Join(", ", SH.__GetDifference(Set3,Set1)));

            Console.ReadKey();

        }
    }
    /// <summary>
    /// This class is for handling sets, the functions it holds are as follows: Membership, Intersection, Union, Difference, Complement
    /// </summary>
    class SetHandler
    {
        /// <summary>
        /// This method will calculate if the element is a member of a set.
        /// </summary>
        /// <param name="a"> This is the set</param>
        /// <param name="b"> This is the element</param>
        /// <returns>True: if b is a member of A, False: if b is not a member of A</returns>
        public bool __Membership(int[] a, int b)
        {
            bool result = false;
            foreach (var item in a)
            {
                if (item == b)
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the Intersection of 2 sets.
        /// Ä Intersects B
        /// </summary>
        /// <param name="a">Set A </param>
        /// <param name="b">Set B</param>
        /// <returns>Will return elements which members of both A and B</returns>
        public int[] __GetIntersection(int[] a, int[] b)
        {
            return a.Intersect(b).ToArray();
        }

        /// <summary>
        /// Gets the union of 2 sets.
        /// Gets a set which holds all elements which are in both a and b.
        /// </summary>
        /// <param name="a">Set A</param>
        /// <param name="b">Set B</param>
        /// <returns>will return elements which are members of A or B</returns>
        public int[] __GetUnion(int[] a, int[] b)
        {
            return a.Union(b).ToArray();
        }

        /// <summary>
        /// Gets the difference between 2 sets.
        /// A - B
        /// </summary>
        /// <param name="a">Set A</param>
        /// <param name="b">Set B</param>
        /// <returns>Returns a set that holds elements that are apart of A but no elements which B also holds.</returns>
        public int[] __GetDifference(int[] a, int[] b)
        {
            return a.Except(b).ToArray();
        }

        /// <summary>
        /// Just like Difference, but complement is more like: Everything other than the set.
        /// </summary>
        /// <param name="a">Set to calculate complement</param>
        /// <param name="context"> Needs a Context - Could be infinite</param>
        /// <returns>Returns a new set, which is everything but Set A</returns>
        public int[] __GetComplement(int[] a, int[] context)
        {
            return context.Except(a).ToArray();
        }
    }

    /// <summary>
    /// This class is for handling subsets, the functions is tholds are as follows: To find out if a set is a subset of another or if sets are equal.
    /// </summary>
    class SubSetHandler
    {
        public int CompareSetsEasyWay(HashSet<int> a, HashSet<int> b)
        {
            if (a.IsSubsetOf(b)) return -1;
            else if (a.IsSupersetOf(b)) return 1;
            else if (a.SetEquals(b)) return 0;
            else if (!a.IsSubsetOf(b) && !b.IsSubsetOf(a)) return -2;
            else return 2;
        }
        public int CompareSets(int[] a, int[] b)
        {
            int Alternativ_Result = 0; //Starting with sets that haven't been calculated stating equals.

            if (a.Count() == 0)
            {
                if (b.Count() == 0)
                {
                    return 0;
                }
                else { return -1; }
            }
            else if (b.Count() == 0)
            {
                return 1;
            }

            int _a = 0;
            int _b = 0;

            foreach (var ElementA in a)
            {
                foreach (var ElementB in b)
                {
                    if (ElementA==ElementB) { _a++; _b++; }
                    else if (ElementA > ElementB)
                    {
                        if (Alternativ_Result == 1) { return -2; }
                        _b++; Alternativ_Result = -1;
                    }
                    else if (ElementB > ElementA)
                    {
                        if (Alternativ_Result == -1) { return -2; }
                        _a++; Alternativ_Result = 1; 
                    }
                    
                }
            }
            if (_a == a.Count() && _b == b.Count()) { return Alternativ_Result; } else if (_a != a.Count() && _b != b.Count()) { return -2; } else return 2;



        }
        public bool Is_Sub_Set(HashSet<int> a, HashSet<int> b)
        {
            return a.IsSubsetOf(b);
        }
        public bool Is_Sets_Equals(HashSet<int> a, HashSet<int> b)
        {
            return a.SetEquals(b);
        }

    }



    
}
