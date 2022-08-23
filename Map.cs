using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechAssessmentMM
{
    /// <summary>
    /// El mapa es la colección de áreas, representa el mapa digital
    /// </summary>
    public class Map
    {
    
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public int[,] MapArray { get; set; }
        public List<int[]> Top { get; set; }
        public List<int[]> MapList { get; set; }
        public int MaxValue {
            get
            { 
                return MapList.Max(i => i[1]); 
            }
        }
        public int MinValue
        {
            get
            {
                return MapList.Min(i => i[1]);
            }
        }
           
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        public Map(int sizeX, int sizeY)
        {
            this.SizeX = sizeX;
            this.SizeY = sizeY;
        }

   
       
        /// <summary>
        /// Obtiene el grafo a partir de la siguiente lógica: 
        /// Los adyacentes son solo los de arriba, abajo, derecha o izquierda
        /// Solo puede moverse si el nodo es menor al nodo actual
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, List<int>> GetGraph()
        {
            int nodos = 0;
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            for (int posX = 0; posX < SizeX; posX++)
            {
                for (int posY = 0; posY < SizeY; posY++)
                {
                    List<int> Adyacents = GetAdjacentValid(posX, posY);
                    graph[nodos++] = Adyacents;
                }
            }
            Console.WriteLine("Finish The Graph found {0} nodes", nodos);
            return graph;
        }

        /// <summary>
        /// Determina si el movimiento es válido hacia arriba, abajo, izquierda o derecha respetando los limites del arreglo
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <returns></returns>
        internal bool IsValidMove(int posX, int posY)
        {
            if (posX < 0)
            {
                return false;
            }
            else if (posX >= SizeX)
            {
                return false;
            }
            else if (posY < 0)
            {
                return false;
            }
            else if (posY >= SizeY)
            {
                return false;
            }
            else
                return true;

        }

        /// <summary>
        /// Obtiene los adyacentes
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <returns></returns>
        private List<int> GetAdjacentValid(int posX, int posY)
        {
            List<int> Adyacents = new List<int>();

            int valueXY = MapArray[posX, posY];

            if (IsValidMove(posX - 1, posY))
            {
                if (valueXY > MapArray[posX - 1, posY])
                {
                    int kk = ((posX - 1) * SizeX + (posY)); // convierto el indice al indice del grafo  pasar Ej: de [0,4] a 5 
                                                              
                    Adyacents.Add(kk);
                }

            }
            if (IsValidMove(posX + 1, posY))
            {
                if (valueXY > MapArray[posX + 1, posY])
                {
                    int kk = (posX + 1) * SizeX + (posY); // convierto el indice al indice del grafo
                                                              
                    Adyacents.Add(kk);
                }
            }
            if (IsValidMove(posX, posY - 1))
            {
                if (valueXY > MapArray[posX, posY - 1])
                {
                    int kk = (posX * SizeX) + ((posY - 1)); // convierto el indice al indice del grafo
                                                               
                    Adyacents.Add(kk);
                }

            }
            if (IsValidMove(posX, posY + 1))
            {
                if (valueXY > MapArray[posX, posY + 1])
                {
                    int kk = (posX * SizeX) + ((posY + 1)); // convierto el indice al indice del grafo
                                                                
                    Adyacents.Add(kk);
                }
            }

            return Adyacents;
        }

        
    }
}
