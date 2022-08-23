using System;

namespace TechAssessmentMM
{
    /// <summary>
    /// 
    /// Se lee información de un archivo ubicado en una ruta predeterminada, se calcula el mejor camino, a partir de
    /// nodos de entrada que tengan adyacentes válidos para iniciar la búsqueda y luego seleccionar la mejor solución
    /// que tenga la longitud mas alta o la mayor pendiente si hay empate
    /// 
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" *********************************** ");
            Console.WriteLine(" ********* Tech Assessment ********* ");
            Console.WriteLine(" *********************************** ");
            Console.WriteLine(" Name: Mauricio Martínez");
            Console.WriteLine(" *********************************** ");
            Console.WriteLine(" Coloque la ruta del archivo con nombre C:\\Temp\\map.txt");
            Console.WriteLine(" Consulte la solución en C:\\Temp\\Output.txt");


            //string fileNameInput = @"C:\Temp\4x4.txt";
            string fileNameInput = @"C:\Temp\map.txt";

            string fileNameOutput = @"C:\Temp\output.txt";

            //Los primeros nodos con mayor valor para buscar los nodos más significativos de 1500 a 1500-TopN
            //por ejemplo 1500 a 1400 por ejemplo
            int TopN = 100;


            Map map = Utility.ReadFile(fileNameInput);

            //
            // Obtiene el grafo, formado por los nodos adyacentes que cumplan la condición
            //
            Dictionary<int, List<int>> graph = map.GetGraph();

            GraphMaxPath gmp = new GraphMaxPath(map.SizeX, graph, map.MapArray, map.MapList);


            List<int[]> NodosEntrada = new List<int[]>();

            //Para el ejemplo 4x4 
            //NodosEntrada.Add(new int[] { 6, 9});  //indice Nodo


            ////Una estrategia que busca solo con los de valor N más alto
            NodosEntrada = gmp.TheMaxNodos(TopN);
            int i = 0;
            foreach (int[] nodos in NodosEntrada)
            {
                if (graph[nodos[0]].Count > 0)//tiene adyacente
                {
                    gmp.FindTheBestPath(graph, map.MapArray, nodos[0]); //usa Dijkstra
                    i++;
                }
                //else
                //    Console.WriteLine(".");
            }

            Console.WriteLine("Attempts:{0}", i);

            //
            //
            //Se busca en los que tengan adyacentes y sean válidos

            //int i = 0;
            //for (int vertice = 0; vertice < graph.Count ; vertice++)
            //{
            //    if (graph[vertice].Count > 0)//tiene adyacente
            //    {
            //        gmp.FindTheBestPath(graph, map.MapArray, vertice);
            //        i++;
            //    }
            //}

            //Console.WriteLine("Attempts:{0}", i);

            Console.WriteLine("************  SOLUTION **************");
            Console.WriteLine("Consulte el archivo C:\\Temp\\Output.txt");

            Solutions? solution = gmp.GetTheBestPath();


            List<string> output = new List<string>();
            output.Add(string.Format("NodoSource: {0}", solution!.NodoSource));
            output.Add(string.Format("Length of calculated path: {0}", solution!.Length));
            output.Add(string.Format("Drop of calculated path: {0}", solution!.Drop));
            string tmp = string.Format("Calculated path: ");

            foreach (Item item in solution.Path)
            {
                tmp += string.Format("{0} - ", item.ValorNodo);
            }
            output.Add(tmp);
            output.Add(string.Format("Finished..."));


            //Imprime solución en el archivo
            Utility.WriteFile(fileNameOutput, output);


            foreach (string texto in output)
                Console.WriteLine(texto);


        }
    }
}