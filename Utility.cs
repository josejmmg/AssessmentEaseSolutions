using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechAssessmentMM
{
    /// <summary>
    /// Clase de utilidad para leer archivos y escribir en el archivo de salida
    /// </summary>
    public static class Utility
    {
        

        /// <summary>
        /// Permite leer los datos 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Map ReadFile(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);

            
            
            System.Console.WriteLine("Read lines:");

            string[] firstline = lines[0].Split(' ').ToArray();
            int MaxX = 0;
            int MaxY = 0;
            int value = 0;

            if (!int.TryParse(firstline[0], out MaxX))
            {
                throw new Exception("Valor X no válido");
            }

            if (!int.TryParse(firstline[1], out MaxY))
            {
                throw new Exception("Valor Y no válido");
            }
            
            
            
            int top = MaxX;



            int[,] mapArray = new int[MaxX, MaxY];

            Console.WriteLine("Matriz: {0} x {1}", MaxX, MaxY);


            List<int[]> lista = new List<int[]>();

            int[] maximo = new int[top];
            int k = 0;


            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] linea = line.Split(' ');

                //Console.WriteLine("{0}", line);

                for (int j = 0; j < MaxY; j++)
                {
                    if (int.TryParse(linea[j], out value))
                    {
                        //se arma la matriz 

                        mapArray[i - 1, j] = value;


                        // Se arma de una vez una lista con los datos

                        lista.Add(new int[] { k, mapArray[i - 1, j] });

                        k++;
                    }

                    //Console.Write("{0}", array[i - 1, j]);

                }
                //Console.WriteLine("");
            }

            
            //
            // Se devuelve un objeto Mapa con los datos que representan los valores de nodos
            //
            Map map = new Map(MaxX,MaxY);

            map.MapArray = mapArray;
            map.MapList = lista;
     
            return map;
        }

        /// <summary>
        /// Escritura de la solución en el archivo de salida
        /// </summary>
        /// <param name="pathOutput"></param>
        /// <param name="lineString"></param>
        public static void WriteFile(string pathOutput, List<string> lineString)
        {
            System.IO.File.WriteAllLines(pathOutput, lineString);
        }

    }
}
