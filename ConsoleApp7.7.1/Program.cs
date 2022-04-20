using System;
using System.Collections.Generic;

namespace Namespace77
{
    abstract class Delivery
    {
        public string Address;

        public Delivery(string address)
        {
            Address = address;
        }
        public virtual void Print()
        {
            Console.WriteLine("Адрес доставки {0}", Address);
            Console.WriteLine();
        }
    }

    class HomeDelivery : Delivery
    {
        public bool Courier { get; set; }
        public string CompanyDelivery { get; set; }
        public HomeDelivery(bool Courier, string Company, string Address) : base(Address)
        {
            CompanyDelivery = Company;
            this.Courier = Courier;
        }
        public override void Print()
        {
            base.Print();
            if (Courier)
            {
                Console.WriteLine("Доставка курьером.");
            }
            else
            {
                Console.WriteLine("Доставка в курьерской компании.");
            }
            Console.WriteLine($"Доставка курьерской компании - {CompanyDelivery}.");
        }
    }

    class PickPointDelivery : Delivery
    {
        public int Cell; // номер ячейки
        public string Password; //пароль ячейки
        public PickPointDelivery(int Cell, string Password, string Address) : base(Address)
        {

            this.Cell = Cell;
            this.Password = Password;
        }


        public override void Print()
        {
            base.Print();
            Console.WriteLine("Доставка PickPoint.");
            Console.WriteLine("Номер ячейки - " + Cell.ToString());
            Console.WriteLine("Пароль ячейки - " + Password);
        }
    }

    class ShopDelivery : Delivery
    {
        public string Shop { get; set; }
        public ShopDelivery(string Shop, string Address) : base(Address)
        {
            this.Shop = Shop;
        }
        public override void Print()
        {
            base.Print();
            Console.WriteLine("Доставка в магазин - .");
        }
    }

    class Product
    {
        public int code;
        public string name;
        public double price;
        public int amount;

        public Product(int code, string name, double price)
        {
            this.code = code;
            this.name = name;
            this.price = price;
            amount = 1;
        }
        public Product(int code, string name, double price, int amount)
        {
            this.code = code;
            this.name = name;
            this.price = price;
            this.amount = amount;
        }
        public void Print()
        {
            Console.WriteLine($"Товар {name} по цене {price} в количестве {amount}.");
        }
        public double Sum()
        {
            return (price * amount);
        }


    }

    class ProductCollection
    {
        // Закрытое поле, хранящее товары в виде массива
        private Product[] collection;
        public int CountProduct;
        // Конструктор с добавлением массива товаров
        public ProductCollection(Product[] collection)
        {
            this.collection = collection;
            CountProduct = collection.Length;
        }

        // Индексатор по массиву
        public Product this[int index]
        {
            get
            {
                // Проверяем, чтобы индекс был в диапазоне для массива
                if (index >= 0 && index < collection.Length)
                {
                    return collection[index];
                }
                else
                {
                    return null;
                }
            }

            private set
            {
                // Проверяем, чтобы индекс был в диапазоне для массива
                if (index >= 0 && index < collection.Length)
                {
                    collection[index] = value;
                }
            }
        }
    }

    struct SOrder
    {
        public int Number;
        public string Customer;
        public string Description;
    }

    class Order<TDelivery, TStruct> where TDelivery : Delivery
        
    {
        public TDelivery Delivery;

        public int Number;

        public string Customer;

        public string Description;

        public ProductCollection ProductCollection;


        
        public void OrderPrint()
        {
            Console.WriteLine($"Номер {Number} заказчик {Customer}");
        }
        public void DisplayAddress()
        {
            Console.WriteLine(Delivery.Address); 
        }

    }


    class Program
    {

        static void Main(string[] args)
        {
            var array = new Product[]
            {
        new Product(1,"Карандаш",50),
        new Product(2, "Ручка", 70),
        new Product(3, "Маркер", 100),
        new Product(4, "Тетрадь", 10, 10)

            };

            ProductCollection collection = new ProductCollection(array);
            for (int i = 0; i < collection.CountProduct; i++)
            {
                Console.WriteLine(collection[i].code);
                Console.WriteLine(collection[i].name);
                Console.WriteLine(collection[i].amount);
                Console.WriteLine(collection[i].price);
            }
            PickPointDelivery PPDelivery = new PickPointDelivery(25, "Password", "Волгоградская обл.г.Урюпинск ул.Ленина 10 кв.5");

            SOrder A = new SOrder();
            A.Number = 1;
            A.Customer = "Иванов Иван";
            A.Description = "Доставка быстро ";

            Order<PickPointDelivery, SOrder> order = new Order<PickPointDelivery, SOrder>();
                        
            order.Delivery = PPDelivery;
            order.Customer = "Иванов Иван";
            order.Description = "Доставка быстро ";
            order.Number = 1;
            order.ProductCollection = collection;

            List<Order<PickPointDelivery, SOrder>> Orders = new List<Order<PickPointDelivery, SOrder>>();// список моих заказов
            Orders.Add(order);

            foreach (Order<PickPointDelivery, SOrder> o in Orders)
            {
                o.OrderPrint();
                for (int i = 0; i < o.ProductCollection.CountProduct; i++)
                {
                    Product p = o.ProductCollection[i];
                    if (p != null)
                        p.Print();
                }

            }


            Console.ReadKey();
        }
    }
}