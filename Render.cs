namespace BattleFiled
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Interfaces;
    class Render
    {
        /// <summary>
        /// Empty constructor. No parameters needed for the Render creation
        /// </summary>
        public Render() {

        }

        /// <summary>
        /// Gets a list of IGameObject objects, calls ToString() for every of the objects and writes the result on the console
        /// </summary>
        /// <param name="listWithElements"></param>
        public void DrawGameElements(IList<IGameObject> listWithElements) {
            foreach(var element in listWithElements)
            {
                Console.WriteLine(element.ToString());
               // Console.WriteLine(listWithElements.Count);
            }
        }
    }
}
