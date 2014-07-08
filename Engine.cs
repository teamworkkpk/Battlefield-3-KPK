namespace BattleFiled
{
    using System;
    using Interfaces;
    using System.Collections.Generic;

    class Engine
    {
        //int sizeOfField;
        //public Engine(int sizeOfField)
        //{
        //    this.sizeOfField = sizeOfField;
        //}

        public Engine()
        {
           
        }

        //public int SizeOfField
        //{
        //    get
        //    {
        //        return this.sizeOfField;
        //    }
        //}

        private int ReadSize()
        {
            Console.Write("Enter the size of the battle field: n = ");

            int sizeOfField = int.Parse(Console.ReadLine());

            return sizeOfField;
        }

        private Playfield InitializeField(int sizeOfField)
        {
            Playfield field = Playfield.Instance;

            field.SetFieldSize(sizeOfField);
            field.InitializeEmptyField();
            field.PlaceMines();

            return field;
        }

        public virtual void Run()
        {
            int sizeOfField = ReadSize();
            List<IGameObject> gameObjects = new List<IGameObject>();
            Playfield field = InitializeField(sizeOfField);
            Render render = new Render();
            gameObjects.Add(field);
            render.DrawGameElements(gameObjects);
            // TODO Render Field, Handle Exceptions...

            while (true)
            {
                // Game Logic...
            }            
        }
    }
}
