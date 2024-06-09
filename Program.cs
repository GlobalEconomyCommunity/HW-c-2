using System;

class KolichestvoException : Exception
{
    public KolichestvoException() : base("Количество месяцев не может быть отрицательным")
    {
    }
}

class VkladException : Exception
{
    public VkladException(double amount) : base($"Невозможно создать вклад - указана отрицательная сумма вклада: {amount}")
    {
    }
}

class Vklad
{
    public string ФИО_вкладчика { get; set; }
    private double сумма_вклада;
    public double Сумма_Вклада
    {
        get { return сумма_вклада; }
        set
        {
            if (value < 0)
                throw new VkladException(value);
            сумма_вклада = value;
        }
    }

    public Vklad(string ФИО, double сумма)
    {
        ФИО_вкладчика = ФИО;
        Сумма_Вклада = сумма;
    }
}

class Bank
{
    public string Название { get; set; }
}

class Филиал : Bank
{
    public string Название_Филиала { get; set; }
    public double Общая_Сумма_Вкладов { get; set; }
}

class Долгосрочный_Вклад : Vklad
{
    public Долгосрочный_Вклад(string ФИО, double сумма) : base(ФИО, сумма) { }

    public double РассчитатьСуммуВклада(int количество_месяцев)
    {
        try
        {
            if (количество_месяцев < 0)
                throw new KolichestvoException();

            double процент = 0.16;
            double сумма = Сумма_Вклада + (Сумма_Вклада * процент * количество_месяцев);
            return сумма;
        }
        catch (KolichestvoException e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }
}

class Краткосрочный_Вклад : Vklad
{
    public Краткосрочный_Вклад(string ФИО, double сумма) : base(ФИО, сумма) { }

    public double РассчитатьСуммуВклада(int количество_месяцев)
    {
        try
        {
            if (количество_месяцев < 0)
                throw new KolichestvoException();

            double процент = 0.03;
            double сумма = Сумма_Вклада + (Сумма_Вклада * процент * количество_месяцев);
            return сумма;
        }
        catch (KolichestvoException e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }
}

class Program
{
    static void Main()
    {
        try
        {
            Vklad vklad = new Vklad("Иванов Иван Иванович", -5000);
        }
        catch (VkladException e)
        {
            Console.WriteLine(e.Message);
        }

        Долгосрочный_Вклад longTermVklad = new Долгосрочный_Вклад("Петров Петр Петрович", 10000);
        double сумма_долгосрочного_вклада = longTermVklad.РассчитатьСуммуВклада(12);
        Console.WriteLine($"Сумма долгосрочного вклада: {сумма_долгосрочного_вклада}");

        Краткосрочный_Вклад shortTermVklad = new Краткосрочный_Вклад("Сидоров Сидор Сидорович", 5000);
        double сумма_краткосрочного_вклада = shortTermVklad.РассчитатьСуммуВклада(6);
        Console.WriteLine($"Сумма краткосрочного вклада: {сумма_краткосрочного_вклада}");
    }
}