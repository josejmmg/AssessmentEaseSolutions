using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechAssessmentMM
{
    /// <summary>
    /// Clase que encapsula el valor del indice, el valor del nodo, el valor del aculuado y si ha sido vistado
    /// </summary>
    public class Item
    {
        public int Indice { get; set; }
        public int ValorAcum { get; set; }
        public int ValorNodo { get; set; }
        public bool Visited { get; set; }

        /// <summary>
        /// Constructor que recibe los parámetros e inicializa el indice, el valor del nodo, el valor acumulado 
        /// y si fue visitado
        /// </summary>
        /// <param name="indice"></param>
        /// <param name="value"></param>
        /// <param name="nodoAcum"></param>
        /// <param name="visited"></param>
        public Item(int indice, int value, int nodoAcum, bool visited)
        {
            Indice = indice;
            ValorAcum = nodoAcum;
            ValorNodo = value;
            Visited = true;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Item()
        {

        }
    }

}
