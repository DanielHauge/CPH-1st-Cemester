using System;
using System.Collections.Generic;

namespace RecursionHanoiTower
{
    class Hanoi
    {
        public int discs;
        private int[][] Tower;
        public List<string> allsteps;


        public Hanoi(int AmountOfDiscs)
        {
            discs = AmountOfDiscs;
            Tower = new[] {new int[AmountOfDiscs], new int[AmountOfDiscs], new int[AmountOfDiscs]};
            FillInPegs();
            allsteps = new List<string>();
        }

        private void FillInPegs()
        {
            for (int i = 0; i < discs; i++)
            {

                Tower[0][i] = discs - i;
            }
        }


        public void WriteTowerToConsole()
        {
            for (int i = discs-1; i > -1; i--)
            {
                Console.Write(GetTowerLayer(Tower[0][i]) + GetTowerLayer(Tower[1][i]) + GetTowerLayer(Tower[2][i]) + "\n");
            }
        }

        private string GetTowerLayer(int v)
        {
            string res = "";

            if (v != discs)
            {
                for (int i = discs-v; i > 0; i--)
                {
                    res += " ";
                }
            }

            


            for (int i = v; i > 0; i--)
            {
                
                res += "#";
            }
            res += v;
            for (int i = 0; i < v; i++)
            {
                res += "#";
            }
            if (v != discs)
            {
                for (int i = discs - v; i > 0; i--)
                {
                    res += " ";
                }
            }



            return res;
        }



        public void Move(int n, int from, int to, int other)
        {
            if (n > 0)
            {
                Move(n -1, from, other, to);
                Console.Clear();
                WriteTowerToConsole();
                Console.WriteLine("Moved: " + from + " to: " + to);
                MoveValues(from, to);
                
                
                Console.ReadKey();
                Move(n -1, other, to, from);
                
            }
        }

        public void MoveValues(int from, int to)
        {
            PutDownpeg(TakeTopPeg(from), to, from);
        }

        private void PutDownpeg(int i, int to, int from)
        {
            int lowestPlace = 0;
            for (int j = discs; j > 0; j--)
            {
                if (Tower[to][j-1] != 0)
                {
                    lowestPlace = j;
                    break;
                }
            }
            Tower[to][lowestPlace] = i;
            Console.WriteLine("Putting Value: "+i+" On: "+to+" At lowest Place of : "+lowestPlace);
            allsteps.Add("Putting: "+i+" From Tower: "+from+" To Tower: "+to);
        }

        public int TakeTopPeg(int from)
        {
            int res = 0;
            for (int i = discs; i > 0; i--)
            {
                if (Tower[from][i-1] != 0)
                {
                    res= Tower[from][i-1];
                    Tower[from][i-1] = 0;
                    return res;
                }

            }
            return res;
        }
    }
}
