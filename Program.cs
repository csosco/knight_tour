using System;
//HUSZÁR VÁNDORLÁS
namespace Knight_Tour_Matrix
{
    class Program
    {
        static int táblaMéret = 8;
        static int kisérletekSzáma = 0;

        //lehetséges mozgások x-vizszintes , y-függőleges

        static int[] xMozgás = { 2, 1, -1, -2, -2, -1, 1, 2 };
        static int[] yMozgás = { 1, 2, 2, 1, -1, -2, -2, -1 };

        //táblaMátrix létrehozása -->táblát szimbolizálja

        static int[,] táblaMátrix = new int[táblaMéret, táblaMéret];


        static void Main(string[] args)
        {   //HV -->HuszárVándorlás
            megoldásHV();
            Console.ReadLine();
        }

        static void megoldásHV()
        {
            //nem látható négyzetek létrehozása ---> NEM VIZSGÁLTAK , még nem voltak benne a kalkulációban ( MERRE HALADJON? )
            for (int i = 0; i < táblaMéret; i++)
            {
                for (int j = 0; j < táblaMéret; j++)
                {
                    //Mátrix feltöltése -1-el ---> NEM VIZSGÁLT STÁTUSZ
                    táblaMátrix[i, j] = -1;
                }
            }
            //kezdőpont létrehozás, mozgások számolása

            int startX = 0;
            int startY = 4;
            //kezdőpont kinullázása, kisérletek száma kinullázása ( többi -1)
            táblaMátrix[startX, startY] = 0;
            kisérletekSzáma = 0;

            //rekurzív próbálkozás minden lehetséges megoldással (BRUTE FORCE)  -->Visszavezetni minden zsákutca megoldásból
            if (!solveHVUtil(startX, startY, 1))
            {
                Console.WriteLine("Nincs megoldás {0} {1} kezdetű körbejáráshoz!!!");
            }
            else
            {
                kiírTábla(táblaMátrix);
                Console.WriteLine("Teljes kisérletek száma: {0}", kisérletekSzáma);
            }

            bool solveHVUtil(int x, int y, int mozgásokSzáma)
            {
                //minden 1 millió kisérletnél kiíírja hogy hol tart
                kisérletekSzáma++;
                if (kisérletekSzáma % 1000000 == 0) {Console.WriteLine("Kisérletek száma : {0})", kisérletekSzáma);}

                int k ; //számolni a következő lépéseket nextX , nextY tömbökön keresztül
                int next_x, next_y;  //a következő lépés heylzete a rekurzíóban

                //checkoljuk ha már mególdottuk a problémát
                if (mozgásokSzáma == táblaMéret * táblaMéret)
                    return true;

                //ciklus a lehetséges következő lépésre
                for ( k = 0 ; k < 8; k++)
                {
                    next_x = x + xMozgás[k]; 
                    next_y = y + yMozgás[k];

                    if (safeSquare(next_x, next_y))
                    {
                        táblaMátrix[next_x, next_y] = mozgásokSzáma;
                        if (solveHVUtil(next_x, next_y, mozgásokSzáma + 1))
                            return true;
                        
                        else
                        { 
                            táblaMátrix[next_x, next_y] = -1; 
                        }
                    }
                }
            }

            //Csekkolja hogy x , y a táblán van-e?  ||  Illetve ha látható-e? ( megnézi -1-e? )   -->> Nem kell if memrt maga logikai a függvény!
            bool safeSquare(int x, int y)
            {
                return (x >= 0 && x < táblaMéret && y >= 0 && y < táblaMéret && táblaMátrix[x, y] == -1);
            }

            void kiírTábla(int[,] táblaÍráshoz)
            {
                for (int i = 0; i < táblaMéret; i++)
                {
                    for (int j = 0; j < táblaMéret; j++)
                    {

                        Console.Write(táblaMátrix[i, j] + " ");
                    }
                    Console.WriteLine();
                }

            }
        }
    } 
}
