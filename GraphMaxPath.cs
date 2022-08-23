using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechAssessmentMM;
using static System.Net.Mime.MediaTypeNames;


namespace TechAssessmentMM
{

    public class GraphMaxPath
    {
        
        int SizeMapArray { get; set; }

        int NumOfVertices { get; set; }

        List<Item> TheBestPath { get; set; }

        Dictionary<int, List<int>> Graph { get; set; }

        int[,] MapArray { get; set; }

        List<int[]> MapList { get; set; }

        List<Solutions> TheSolutions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sizeArray"></param>
        /// <param name="graph"></param>
        /// <param name="array"></param>
        /// <param name="lista"></param>
        public GraphMaxPath(int sizeArray, Dictionary<int, List<int>> graph, int[,] array, List<int[]> lista)
        {
            this.NumOfVertices = sizeArray * sizeArray;
            this.SizeMapArray = sizeArray;
            this.Graph = graph;
            this.MapArray = array;
            this.MapList = lista;
            this.TheSolutions = new List<Solutions>();
        }



        /// <summary>
        /// Se revisa el mejor acumulado y se marca como visitado
        /// </summary>
        /// <returns></returns>
        Item FindTheBest()
        {
            Item nodo = new Item();

            foreach (Item item in this.TheBestPath)
            {
                if (!item.Visited && item.ValorAcum > nodo.ValorAcum)
                {
                    nodo = item;
                }
            }

            //se marca como finalizado el nodo
            nodo.Visited = true;
            return nodo;
        }


        /// <summary>
        /// Imprime el mejor camino, se decartan acumulados en ceros
        /// </summary>
        /// <param name="nodoSource"></param>
        public void PrintBestpath(int nodoSource)
        {
            //Se revisa la ruta realmente verificando hacia atrás

            TheBestPath = BackReview(TheBestPath, nodoSource);

            

            TheSolutions.Add(new Solutions()
            {
                NodoSource = nodoSource,
                Path = TheBestPath,
                Length = TheBestPath.Count,
                Drop = TheBestPath.Max(i => i.ValorNodo) - TheBestPath.Min(i => i.ValorNodo)

            }) ;

            

        }



        /// <summary>
        /// Procesa del mayor acumulado hacia atrás disminuyendo el valor del nodo y validando que es un adyacente válido
        /// Esto se hace porque se ha guardado acumulados de caminos que no finalizan bien, estos se deben descartar
        /// </summary>
        /// <param name="final"></param>
        /// <param name="graph"></param>
        /// <param name="nodoSource"></param>
        /// <returns></returns>
        List<Item> BackReview(List<Item> final, int nodoSource)
        {
            List<Item> TheBestPathBack = new List<Item>();
            //se obtiene el max
            Item? first = final.OrderByDescending(i => i.ValorAcum).ToList().FirstOrDefault();

            int indice = first!.Indice;
            
            //Console.Write("First Node: {0} - ", first.Indice);
            TheBestPathBack.Add(first);
            
            while (indice != nodoSource)
            {
                Item? nodo = final.Where(i => i.Indice == indice).FirstOrDefault();
                int newAcum = nodo!.ValorAcum - nodo.ValorNodo;
                //Console.WriteLine("newAcum:{0}", newAcum);

                List<Item> next = final.Where(item => item.ValorAcum == newAcum).ToList();

                //
                //Si hay mas de un camino posible hacia atrás
                //
                foreach (Item item in next)
                {
                    //Console.WriteLine("indice:{0}, value:{1}, acum:{2}", first[0], first[1], first[2]);
                    if (ObtenerValor(item.Indice, nodo.Indice) > 0) // es adyacente válido
                    {
                        //Console.Write("{0} - ",item[1]);
                        indice = item.Indice;
                        TheBestPathBack.Add(item);
                    }
                    else
                    {
                        //se descarta no es un adyacente válido
                        //Console.WriteLine("NO VALIDO indice:{0} valor:{1} acum:{2}", item[0], item[1], item[2]);
                        item.Indice = item.ValorNodo = item.ValorNodo = 0;

                    }
                }

            }

            return TheBestPathBack;


        }




        /// <summary>
        /// Implementación del Algoritmo de Dijkstra
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="array"></param>
        /// <param name="nodoSource"></param>
        public void FindTheBestPath(Dictionary<int, List<int>> graph, int[,] array, int nodoSource)
        {
                //int[] TheBestPath = new int[numOfVertices];

                this.TheBestPath = new List<Item>();

                Boolean[] visitado = new Boolean[NumOfVertices];

                int value = ObtenerValor(nodoSource, nodoSource);

                //TheBestPath[nodoSource] = value;

                this.TheBestPath.Add(new Item()
                {
                    Indice = nodoSource,
                    ValorNodo = value,
                    ValorAcum = value,
                    Visited = false
                });



                //
                //Se encuentra el mejor camino
                //
                for (int count = 0; count < NumOfVertices; count++)
                {
                    Item best = FindTheBest();

                    visitado[best.Indice] = true;

                    //
                    // process adjacent nodes of the current vertex
                    //
                    foreach (int AdyacentIndex in graph[best.Indice])
                    {
                        int valueGraph = ObtenerValor(best.Indice, AdyacentIndex);
                        int acum = best.ValorAcum + valueGraph;

                        //
                        // if vertex v not in TheBestPath then update it  
                        //
                        Item? Adyacent = TheBestPath.Where(item => item.Indice == AdyacentIndex).FirstOrDefault();

                        if (!visitado[AdyacentIndex]
                                && valueGraph != 0
                                && best.ValorAcum != 0
                                && acum > best.ValorAcum)
                        {

                            var TheBest = this.TheBestPath.Where(e => e.Indice == AdyacentIndex).FirstOrDefault();

                            if (TheBest == null)
                            {

                                this.TheBestPath.Add(new Item()
                                {
                                    Indice = AdyacentIndex,
                                    ValorNodo = valueGraph,
                                    ValorAcum = acum,
                                    Visited = false
                                });

                            }
                            else
                            {
                                TheBest.ValorAcum = acum;
                                TheBest.Visited = false;
                            }

                            Console.Write("*");
                        }
                    }
                }

                Console.WriteLine("---");
                // print the path array 
                PrintBestpath(nodoSource);

            
        }

        /// <summary>
        /// Se valida si se ha visitado
        /// </summary>
        /// <param name="vertice"></param>
        /// <returns></returns>
        private bool VisitadoVertice(int vertice)
        {
            bool? visited;
            visited = TheBestPath.Where(i => i.Indice == vertice).FirstOrDefault()!.Visited;
            return visited.HasValue ? visited.Value : false;
        }

        /// <summary>
        /// Se obtiene el valor válido si es adyacente
        /// </summary>
        /// <param name="origen"></param>
        /// <param name="destino"></param>
        /// <returns></returns>
        private int ObtenerValor(int origen, int destino)
        {
            int value = 0;
            int PosXOrigen = (origen / SizeMapArray) % SizeMapArray;
            int PosYOrigen = (origen % SizeMapArray);
            int PosXDestino = (destino / SizeMapArray) % SizeMapArray;
            int PosYDestino = (destino % SizeMapArray);


            if (origen != destino)
            {
                List<int> adyacentes = Graph[origen];
                if (adyacentes.Exists(item => item == destino))
                {
                    value = MapArray[PosXDestino, PosYDestino];
                }
            }
            else if (origen == destino)
            {
                value = MapArray[PosXOrigen, PosYOrigen];
            }

            return value;
        }

        /// <summary>
        /// Tiene en cuenta la longitud del camino y la mejor pendiente si hay caminos con la misma longitud
        /// </summary>
        /// <returns></returns>
        public Solutions? GetTheBestPath()
        {

            Solutions? best = this.TheSolutions.OrderByDescending(i => i.Length).FirstOrDefault();

            var TheSolutions = this.TheSolutions.Where(item => item.Length == best!.Length).ToList();


            if (TheSolutions.Count > 1)
            {
                //desempate
                best = TheSolutions.OrderByDescending(j => j.Drop).FirstOrDefault();
            }
            return best;
        }

        /// <summary>
        /// Una estrategia para obtener nodos que tenga el mayor número de nodos relacionados
        /// </summary>
        /// <returns></returns>
        public List<int[]> TheMaxAdyacentes()
        {
            List<int[]> candidates = new List<int[]>();
            //Nodo y Valor
            foreach (var item in Graph.OrderByDescending(i => i.Value.Count).Take(10))
            {
                Console.WriteLine("{0} Tiene {1} Adyacentes",item.Key, item.Value.Count);
                candidates.Add(new int[] { item.Key, item.Value.Count });
            }

            return candidates;
        }
        
        /// <summary>
        /// Una estrategia para obtener nodos de entrada, se seleccionan el Top N y se excluyen algunmos que ya encontró
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<int[]> TheMaxNodos(int top)
        {
            List<int[]> candidates = new List<int[]>();
            //Nodo y Valor
            //Que tengan un valor con almenos un adyacente
            candidates = this.MapList.Where(item => item[1]<= 1500 && item[1] < 1500 - top && this.Graph[item[0]].Count > 0).OrderByDescending(item => item[1]).ToList();
            //var Top100 = candidates.Take(top).ToList();


            return candidates;
        }


        /// <summary>
        /// Una estrategia para obtener nodos de entrada, se seleccionan los nodos mas frecuentes
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<int[]> TheMostFrequents(int top)
        {
            int[] count = new int[1500];
            List<int[]> candidates = new List<int[]>();
            //Nodo y Valor
            int tmp = 0;
            int min = 1501;
            int max = 0;
            int[] nodoMin = new int[2];
            int[] nodoMax = new int[2];
            foreach (int[] item in this.MapList )
            {
                count[item[1]]++;
                tmp = count[item[1]];
                if (tmp < min)
                {
                    min = tmp;
                    nodoMin = item;
                }
                tmp = count[item[1]];
                if (tmp > max)
                {
                    max = tmp;
                    nodoMax = item;
                }
            }

           
            //Console.WriteLine("min{0}", min);
            //var lista = this.lista.Where(item => item[1] == min).OrderByDescending(item => item[1]).ToList();
           
            Console.WriteLine("max{0}",min);
            var lista = this.MapList.Where(item => item[1] == max).OrderByDescending(item => item[1]).ToList();
            

            return lista;
        }
    }

}
