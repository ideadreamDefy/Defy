using System;
using Tool;
using Model;
namespace Hello
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			MainHello hello = new MainHello();
			hello.printTable();
			Console.WriteLine ("Hello World!");

//			List<People> people = new List<People>();
//			People p1 = new People(21,"guojing");
//			People p2 = new People(21,"wujunmin");
//			People p3 = new People(21,"muqing");
//			People p4 = new People(21,"lipan");
//			people.add(p1);
//			people.add(p2);
//			people.add(p3);
//			people.add(p4);

//			int a = 30;
//			uint b = 100;
			//uint字节数
			Console.WriteLine(sizeof(uint));
			//int字节数
			Console.WriteLine(sizeof(int));

			Console.WriteLine(sizeof(float));

			Console.WriteLine(sizeof(byte));

			Console.WriteLine(sizeof(char));

			Console.WriteLine(sizeof(bool));

			Console.WriteLine(sizeof(double));

			Console.WriteLine(sizeof(decimal));
			// Console.WriteLine(a);
			// Console.WriteLine(b);
			int myInteger;

			string myString;

			myInteger = 17;

			myString = "\"myInteger\"is";

			Console.WriteLine("{0},{1}.",myString,myInteger);

			People p = new People();
			p.showForEach();

			int [] myArray = {1,8,3,6,2,5,9,3,0,2};
			int maxIndex;
			Console.WriteLine("The MaxImum value in myArray is {0}",p.maxValue(myArray,out maxIndex));

			p.delegateTest();
			
			
		}

	}
}
