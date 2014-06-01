namespace BattleFiled
{
    using System;

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

            Playfield field = InitializeField(sizeOfField);

            // TODO Render Field, Handle Exceptions...

            while (true)
            {
                // Game Logic...
            }            
        }
    }
}
